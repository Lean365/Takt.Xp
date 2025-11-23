//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginPolicy.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : 登录限制策略实现
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.Entities.Identity;
using Takt.Domain.IServices.Security;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Security
{
    /// <summary>
    /// 登录限制策略实现
    /// </summary>
    /// <remarks>
    /// 登录策略包含三种主要情况：
    /// 1. 正常登录：用户名密码正确，无需验证码
    /// 2. 重复登录：5分钟内重复登录需要验证码
    /// 3. 错误限制：连续登录失败3次后需要验证码，4次失败将触发锁定
    ///    - admin用户：锁定30分钟
    ///    - 普通用户：永久锁定，需要管理员手动解锁
    /// </remarks>
    public class TaktLoginPolicy : ITaktLoginPolicy
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;
        /// <summary>
        /// 仓储工厂
        /// </summary>

        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();

        private readonly TaktLoginPolicyOptions _options;

        // 缓存键前缀
        private const string LOGIN_ATTEMPT_PREFIX = "login_attempt:";      // 登录尝试次数缓存键前缀

        private const string LAST_LOGIN_PREFIX = "last_login:";           // 最后登录时间缓存键前缀
        private const string CAPTCHA_REQUIRED_PREFIX = "captcha_required:"; // 验证码要求标志缓存键前缀

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLoginPolicy(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IOptions<TaktLoginPolicyOptions> options,
            IMemoryCache cache)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
            _options = options.Value;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        /// <summary>
        /// 验证登录尝试
        /// </summary>
        /// <remarks>
        /// 登录验证流程：
        /// 1. 首先检查用户是否被永久锁定
        /// 2. 检查是否在5分钟内重复登录（需要验证码）
        /// 3. 检查失败次数，判断是否需要验证码或锁定
        /// 4. 根据用户类型（admin/普通用户）应用不同的锁定策略
        /// </remarks>
        public async Task<(bool allowed, int? remainingSeconds)> ValidateLoginAttemptAsync(string username)
        {
            // 如果未启用登录限制，直接允许登录
            if (!_options.EnableLoginRestriction)
            {
                return (true, null);
            }

            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var lastLoginKey = $"{LAST_LOGIN_PREFIX}{username}";
            bool needCaptcha = false;

            // 1. 检查用户是否存在且是否被永久锁定
            var user = await UserRepository.GetFirstAsync(u => u.UserName == username);
            if (user != null && user.IsLock == 2) // IsLock=2表示永久锁定
            {
                _logger.Warn("[登录策略] 用户 {Username} 已被永久锁定，需要管理员解锁", username);
                return (false, null);
            }

            // 2. 检查是否在指定时间内重复登录（第二种情况）
            if (_cache.TryGetValue(lastLoginKey, out DateTime lastLoginTime))
            {
                var timeSinceLastLogin = DateTime.UtcNow - lastLoginTime;
                if (timeSinceLastLogin.TotalMinutes <= _options.RepeatLoginMinutes)
                {
                    _logger.Info("[登录策略] 用户 {Username} 在{Minutes}分钟内重复登录，需要验证码",
                        username, _options.RepeatLoginMinutes);
                    needCaptcha = true;
                }
            }

            // 3. 获取失败次数，处理第三种情况（错误限制）
            int failedAttempts = 0;
            _cache.TryGetValue(attemptKey, out failedAttempts);

            bool isAdmin = username.ToLower() == "Takt365";
            int maxAttempts = isAdmin ? _options.MaxFailedAttempts / 2 : _options.MaxFailedAttempts;

            // 4. 检查是否达到最大失败次数
            if (failedAttempts >= maxAttempts)
            {
                if (isAdmin)
                {
                    // 管理员特殊处理：临时锁定
                    var lockoutEndTime = _cache.Get<DateTime?>(attemptKey + "_lockout");
                    if (lockoutEndTime.HasValue && DateTime.UtcNow < lockoutEndTime.Value)
                    {
                        var remainingSeconds = (int)(lockoutEndTime.Value - DateTime.UtcNow).TotalSeconds;
                        return (false, remainingSeconds);
                    }
                }
                else
                {
                    // 普通用户处理：达到最大失败次数后永久锁定
                    if (user != null)
                    {
                        user.IsLock = 2; // 设置永久锁定
                        user.LoginCount = failedAttempts;
                        await UserRepository.UpdateAsync(user);
                        _logger.Warn("[登录策略] 用户 {Username} 已被永久锁定", username);
                        return (false, null);
                    }
                }
            }

            // 5. 检查是否需要验证码（达到指定失败次数）
            if (failedAttempts >= _options.CaptchaRequiredAttempts)
            {
                needCaptcha = true;
            }

            // 如果需要验证码，返回特殊状态
            if (needCaptcha)
            {
                return (false, 0); // 返回0表示需要验证码但不需要等待
            }

            // 第一种情况：正常登录，无需验证码
            return (true, null);
        }

        /// <summary>
        /// 记录登录失败
        /// </summary>
        /// <remarks>
        /// 失败处理逻辑：
        /// 1. admin用户：
        ///    - 达到最大失败次数的一半后锁定
        ///    - 锁定期间禁止登录
        /// 2. 普通用户：
        ///    - 达到最大失败次数后永久锁定
        ///    - 需要管理员手动解锁
        /// </remarks>
        public async Task RecordFailedLoginAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";

            // 获取当前失败次数
            int currentAttempts = 0;
            _cache.TryGetValue(attemptKey, out currentAttempts);
            currentAttempts++;

            bool isAdmin = username.ToLower() == "Takt365";
            int maxAttempts = isAdmin ? _options.MaxFailedAttempts / 2 : _options.MaxFailedAttempts;

            // 更新失败次数
            if (isAdmin)
            {
                if (currentAttempts >= maxAttempts)
                {
                    // admin用户：临时锁定
                    var lockoutEndTime = DateTime.UtcNow.AddMinutes(_options.LockoutMinutes);
                    _cache.Set(attemptKey + "_lockout", lockoutEndTime, TimeSpan.FromMinutes(_options.LockoutMinutes));
                    _logger.Warn("[登录策略] 管理员账号已被临时锁定 {Minutes} 分钟", _options.LockoutMinutes);
                }
            }
            else
            {
                // 普通用户：更新数据库中的失败记录
                var user = await UserRepository.GetFirstAsync(u => u.UserName == username);
                if (user != null)
                {
                    if (currentAttempts >= maxAttempts)
                    {
                        // 达到最大失败次数，设置永久锁定
                        user.IsLock = 2;
                        user.LoginCount = currentAttempts;
                        await UserRepository.UpdateAsync(user);
                        _logger.Warn("[登录策略] 用户 {Username} 已被永久锁定", username);
                    }
                    else
                    {
                        // 更新失败次数
                        user.LoginCount = currentAttempts;
                        await UserRepository.UpdateAsync(user);
                    }
                }
            }

            // 更新缓存中的失败次数（24小时有效期）
            _cache.Set(attemptKey, currentAttempts, TimeSpan.FromHours(24));

            _logger.Warn("[登录策略] 用户 {Username} 登录失败，当前失败次数: {Count}", username, currentAttempts);
        }

        /// <summary>
        /// 记录登录成功
        /// </summary>
        /// <remarks>
        /// 成功登录后的处理：
        /// 1. 清除失败记录
        /// 2. 记录本次登录时间（用于检测重复登录）
        /// 3. 重置用户的锁定状态和失败计数
        /// </remarks>
        public async Task RecordSuccessfulLoginAsync(string username)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{username}";
            var lastLoginKey = $"{LAST_LOGIN_PREFIX}{username}";

            // 清除失败记录
            _cache.Remove(attemptKey);
            _cache.Remove(attemptKey + "_lockout");

            // 记录最后登录时间（用于检测重复登录）
            _cache.Set(lastLoginKey, DateTime.UtcNow, TimeSpan.FromMinutes(_options.RepeatLoginMinutes));

            // 更新用户状态
            var user = await UserRepository.GetFirstAsync(u => u.UserName == username);
            if (user != null)
            {
                user.LoginCount = 0; // 重置登录失败次数
                if (user.IsLock == 1) // 如果是临时锁定，则解除
                {
                    user.IsLock = 0;
                }
                await UserRepository.UpdateAsync(user);
            }

            _logger.Info("[登录策略] 用户 {Username} 登录成功，清除所有限制", username);
        }

        /// <summary>
        /// 验证管理员登录尝试
        /// </summary>
        public Task<(bool allowed, int? remainingSeconds)> ValidateAdminLoginAttemptAsync(string userName)
        {
            return ValidateLoginAttemptAsync(userName);
        }

        /// <summary>
        /// 记录管理员登录失败
        /// </summary>
        public Task RecordAdminFailedLoginAsync(string userName)
        {
            return RecordFailedLoginAsync(userName);
        }

        /// <summary>
        /// 获取剩余登录尝试次数
        /// </summary>
        public Task<int> GetRemainingAttemptsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            int currentAttempts = 0;
            _cache.TryGetValue(attemptKey, out currentAttempts);

            bool isAdmin = userName.ToLower() == "Takt365";
            int maxAttempts = isAdmin ? _options.MaxFailedAttempts / 2 : _options.MaxFailedAttempts;
            int remainingAttempts = maxAttempts - currentAttempts;

            return Task.FromResult(remainingAttempts > 0 ? remainingAttempts : 0);
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        public Task<int> GetLockoutRemainingSecondsAsync(string userName)
        {
            var attemptKey = $"{LOGIN_ATTEMPT_PREFIX}{userName}";
            var lockoutEndTime = _cache.Get<DateTime?>(attemptKey + "_lockout");

            if (lockoutEndTime.HasValue && DateTime.UtcNow < lockoutEndTime.Value)
            {
                return Task.FromResult((int)(lockoutEndTime.Value - DateTime.UtcNow).TotalSeconds);
            }

            return Task.FromResult(0);
        }
    }
}




