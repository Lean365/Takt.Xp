//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAuthController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 基础认证控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;
using Takt.Shared.Constants;
using Takt.Shared.Options;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.Security;
using Takt.Domain.IServices.SignalR;
using Takt.Domain.Repositories;
using Takt.Infrastructure.Extensions;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Takt.Application.Services.Routine.SignalR;
using Takt.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 基础认证控制器
    /// </summary>
    /// <remarks>
    /// 本控制器负责处理:
    /// 1. 用户名密码登录
    /// 2. 用户登出
    /// 3. 刷新访问令牌
    /// </remarks>
    [ApiController]
    [Route("api/[controller]")]
    [ApiModule("identity", "身份认证")]
    public class TaktAuthController : TaktBaseController
    {
        private readonly ITaktLoginPolicy _loginPolicy;
        private readonly ITaktSignalROnlineService _signalRUserService;
        private readonly ITaktUserService _userService;
        private readonly ITaktAuthService _loginService;
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktDeviceIdGenerator _deviceIdGenerator;
        private readonly IOptions<TaktJwtOptions> _jwtOptions;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 获取在线用户仓储
        /// </summary>
        private ITaktRepository<TaktSignalROnline> OnlineUserRepository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();

        /// <summary>
        /// 构造函数
        /// <param name="loginPolicy">登录策略</param>
        /// <param name="signalRUserService">SignalR用户服务</param>
        /// <param name="userService">用户服务</param>
        /// <param name="loginService">登录服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="deviceIdGenerator">设备ID生成器</param>
        /// <param name="jwtOptions">JWT配置</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="configuration">配置服务</param>
        /// </summary>
        public TaktAuthController(
            ITaktLoginPolicy loginPolicy,
            ITaktSignalROnlineService signalRUserService,
            ITaktUserService userService,
            ITaktAuthService loginService,
            ITaktRepositoryFactory repositoryFactory,
            ITaktDeviceIdGenerator deviceIdGenerator,
            IOptions<TaktJwtOptions> jwtOptions,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization,
            IConfiguration configuration) : base(logger, currentUser, localization)
        {
            // 记录依赖注入状态
            logger?.Info("[TaktAuthController] 开始构造函数，检查依赖注入状态");
            
            _loginPolicy = loginPolicy ?? throw new ArgumentNullException(nameof(loginPolicy));
            _signalRUserService = signalRUserService ?? throw new ArgumentNullException(nameof(signalRUserService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _deviceIdGenerator = deviceIdGenerator ?? throw new ArgumentNullException(nameof(deviceIdGenerator));
            _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            
            logger?.Info("[TaktAuthController] 构造函数完成，所有依赖注入成功");
        }

        // JWT Bearer Token 模式，无需 Cookie 管理

        /// <summary>
        /// 检查用户是否可以登录
        /// </summary>
        /// <param name="loginDto">登录信息</param>
        /// <returns>检查结果</returns>
        [HttpPost("check-login")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin([FromBody] TaktAuthDto loginDto)
        {
            try
            {
                _logger.Info("[登录检查] 开始检查用户登录状态: {Username}", loginDto.UserName);

                // 获取用户信息
                var user = await UserRepository.GetFirstAsync(u => u.UserName == loginDto.UserName);

                // 生成随机盐值（无论用户是否存在，都返回盐值以防止恶意攻击）
                var randomSalt = GenerateRandomSalt();
                var iterations = 10000; // 默认迭代次数

                if (user == null)
                {
                    _logger.Info("[登录检查] 用户不存在，返回随机盐值");
                    return Success(new
                    {
                        canLogin = true,
                        existingDeviceInfo = (string?)null,
                        salt = randomSalt,
                        iterations = iterations
                    });
                }

                return Success(new
                {
                    canLogin = true,
                    existingDeviceInfo = (string?)null,
                    salt = user.Salt,
                    iterations = user.Iterations
                });
            }
            catch (Exception ex)
            {
                _logger.Error("[登录检查] 检查过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(TaktConstants.ErrorCodes.ServerError));
            }
        }

        /// <summary>
        /// 生成随机盐值
        /// </summary>
        private string GenerateRandomSalt()
        {
            var saltBytes = new byte[32]; // 32字节的盐值
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginDto">登录信息</param>
        /// <returns>登录结果</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] TaktAuthDto loginDto)
        {
            try
            {
                // 检查服务状态
                _logger.Info("[登录] 开始登录方法，检查服务状态");
                _logger.Info("[登录] _loginService状态: {ServiceStatus}", _loginService != null ? "已注入" : "未注入");
                _logger.Info("[登录] _logger状态: {LoggerStatus}", _logger != null ? "已注入" : "未注入");
                
                // 记录原始请求体
                HttpContext.Request.EnableBuffering();
                HttpContext.Request.Body.Position = 0;
                using var reader = new StreamReader(HttpContext.Request.Body, leaveOpen: true);
                var requestBody = await reader.ReadToEndAsync();
                _logger.Info("[登录] 原始请求体: {RequestBody}", requestBody);
                HttpContext.Request.Body.Position = 0;
                
                // 安全地获取设备信息字符串
                string deviceInfoStr = "空";
                if (loginDto?.DeviceType != null || loginDto?.LoginFingerprint != null)
                {
                    try
                    {
                        deviceInfoStr = $"设备类型:{loginDto.DeviceType ?? "空"}, 指纹:{loginDto.LoginFingerprint?.LoginFingerprint ?? "空"}";
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("[登录] 记录设备信息时出错: {Error}", ex.Message);
                        deviceInfoStr = "设备信息记录失败";
                    }
                }
                
                // 详细记录接收到的参数
                _logger.Info("[登录] 开始处理登录请求，完整参数: UserName={UserName}, Password={Password}, CaptchaToken={CaptchaToken}, CaptchaOffset={CaptchaOffset}, CaptchaType={CaptchaType}, LoginIp={LoginIp}, UserAgent={UserAgent}, LoginType={LoginType}, LoginSource={LoginSource}, DeviceInfo={DeviceInfo}", 
                    loginDto?.UserName ?? "空",
                    loginDto?.Password?.Length > 0 ? $"{loginDto.Password.Substring(0, Math.Min(8, loginDto.Password.Length))}..." : "空",
                    loginDto?.CaptchaToken ?? "空",
                    loginDto?.CaptchaOffset ?? 0,
                    loginDto?.CaptchaType ?? "空",
                    loginDto?.LoginIp ?? "空",
                    loginDto?.UserAgent?.Length > 0 ? $"{loginDto.UserAgent.Substring(0, Math.Min(50, loginDto.UserAgent.Length))}..." : "空",
                    loginDto?.LoginType ?? 0,
                    loginDto?.LoginSource ?? 0,
                    deviceInfoStr
                );
                
                // 验证关键参数
                if (loginDto == null)
                {
                    _logger.Error("[登录] 接收到的loginDto为null");
                    return Error("登录参数不能为空", 400);
                }
                
                if (string.IsNullOrEmpty(loginDto.UserName))
                {
                    _logger.Error("[登录] 用户名为空");
                    return Error("用户名不能为空", 400);
                }
                
                if (string.IsNullOrEmpty(loginDto.Password))
                {
                    _logger.Error("[登录] 密码为空");
                    return Error("密码不能为空", 400);
                }
                
                if (loginDto.DeviceType == null)
                {
                    _logger.Error("[登录] 设备类型为空");
                    return Error("设备类型不能为空", 400);
                }

                // 检查服务是否为空
                if (_loginService == null)
                {
                    _logger.Error("[登录] 登录服务为空，依赖注入失败");
                    return Error("系统服务异常", 500);
                }

                _logger.Info("[登录] 开始调用登录服务");
                var result = await _loginService.LoginAsync(loginDto);

                _logger.Info("[登录] 登录成功: UserName={UserName}", loginDto.UserName);

                // JWT Bearer Token 模式，Token 通过响应体返回给前端
                // 前端将 Token 保存到 localStorage 并在请求头中发送

                return Success(result);
            }
            catch (TaktException ex)
            {
                _logger.Warn("[登录] 登录失败: UserName={UserName}, Error={Error}", loginDto.UserName, ex.Message);
                return Error(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.Error("[登录] 登录异常: UserName={UserName}, ExceptionType={ExceptionType}, Message={Message}, StackTrace={StackTrace}", 
                    loginDto?.UserName ?? "未知", 
                    ex.GetType().Name, 
                    ex.Message, 
                    ex.StackTrace);
                return Error("服务器内部错误", 500);
            }
        }

        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns>用户信息</returns>
        [HttpGet("info")]
        [Authorize] // JWT Bearer Token 认证
        public async Task<IActionResult> GetUserInfo()
        {
            _logger.Info("[用户信息] 开始获取用户信息");

            var userId = HttpContext.User.FindFirst("uid")?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                _logger.Warn("[用户信息] 未找到用户ID");
                return Error("未登录", int.Parse(TaktConstants.ErrorCodes.Unauthorized));
            }

            // _logger.Info("[用户信息] 开始获取用户信息: UserId={UserId}", userId);
            var result = await _loginService.GetUserInfoAsync(long.Parse(userId));
            // _logger.Info("[用户信息] 获取成功: UserId={UserId}", userId);
            return Success(result);
        }

        /// <summary>
        /// 用户登出
        /// </summary>
        /// <param name="isSystemRestart">是否是系统重启导致的登出</param>
        /// <returns>操作结果</returns>
        [HttpPost("logout")]
        [AllowAnonymous]
        public async Task<IActionResult> Logout([FromQuery] bool isSystemRestart = false)
        {
            try
            {
                _logger.Info("[登出控制器] 开始处理登出请求, 是否是系统重启: {IsSystemRestart}", isSystemRestart);

                var userId = HttpContext.User.FindFirst("uid")?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.Info("[登出控制器] 未找到用户ID，直接返回成功");
                    return Success();
                }

                // 如果是系统重启导致的登出，直接返回成功
                if (isSystemRestart)
                {
                    _logger.Info("[登出控制器] 系统重启导致的登出，直接返回成功");
                    return Success();
                }

                // 正常登出流程
                _logger.Info("[登出控制器] 用户ID: {UserId}", userId);
                var result = await _loginService.LogoutAsync(long.Parse(userId));

                if (result)
                {
            // JWT Bearer Token 模式，Token 由前端清除
                    
                    _logger.Info("[登出控制器] 登出成功: UserId={UserId}", userId);
                    return Success();
                }
                else
                {
                    _logger.Warn("[登出控制器] 登出失败: UserId={UserId}", userId);
                    return Error(_localization.L("User.LogoutFailed"));
                }
            }
            catch (Exception ex)
            {
                _logger.Error("[登出控制器] 登出过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(TaktConstants.ErrorCodes.ServerError));
            }
        }

        /// <summary>
        /// 刷新访问令牌
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                string? refreshToken = null;

                // 优先从 Cookie 中获取刷新 Token
                refreshToken = HttpContext.Request.Cookies["refresh_token"];
                
                // 如果 Cookie 中没有，则从请求体中读取
                if (string.IsNullOrEmpty(refreshToken))
                {
                    using var reader = new StreamReader(HttpContext.Request.Body);
                    refreshToken = await reader.ReadToEndAsync();
                }

                if (string.IsNullOrEmpty(refreshToken))
                {
                    _logger.Warn("[刷新令牌] 刷新令牌为空");
                    return Error("刷新令牌不能为空", 400);
                }

                _logger.Info("[刷新令牌] 开始刷新令牌: Token={Token}",
                    refreshToken.Substring(0, Math.Min(10, refreshToken.Length)) + "...");

                var result = await _loginService.RefreshTokenAsync(refreshToken);
                
                // JWT Bearer Token 模式，Token 通过响应体返回给前端

                return Success(result);
            }
            catch (TaktException ex)
            {
                _logger.Warn("[刷新令牌] 刷新失败: Error={Error}", ex.Message);
                return Error(ex.Message, ex.Code);
            }
            catch (Exception ex)
            {
                _logger.Error("[刷新令牌] 刷新异常", ex);
                return Error("服务器内部错误", 500);
            }
        }

        /// <summary>
        /// 获取用户盐值
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>用户盐值信息</returns>
        [HttpGet("salt")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSalt([FromQuery] string username)
        {
            try
            {
                _logger.Info("[获取盐值] 开始获取用户盐值: {Username}", username);

                if (string.IsNullOrEmpty(username))
                {
                    return Error("用户名不能为空", 400);
                }

                var saltResult = await _loginService.GetUserSaltAsync(username);
                if (saltResult == null)
                {
                    _logger.Warn("[获取盐值] 用户不存在: {Username}", username);
                    return Error(_localization.L("User.NotFound"), int.Parse(TaktConstants.ErrorCodes.NotFound));
                }

                var (salt, iterations) = saltResult.Value;

                _logger.Info("[获取盐值] 获取成功: {Username}, SaltLength={SaltLength}, Iterations={Iterations}",
                    username, salt.Length, iterations);

                return Success(new { salt, iterations });
            }
            catch (Exception ex)
            {
                _logger.Error("[获取盐值] 获取过程中发生错误: {Message}", ex.Message);
                return Error("服务器内部错误", int.Parse(TaktConstants.ErrorCodes.ServerError));
            }
        }



        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>解锁结果</returns>
        [HttpPost("unlock/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> UnlockUserAsync(string username)
        {
            try
            {
                _logger.Info("正在解锁用户: {Username}", username);
                await _loginPolicy.RecordSuccessfulLoginAsync(username);
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.Error("解锁用户失败: {Message}", ex.Message);
                return Error("解锁用户失败", 500);
            }
        }

        /// <summary>
        /// 获取用户登录尝试剩余次数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余尝试次数</returns>
        [HttpGet("attempts/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRemainingAttemptsAsync(string username)
        {
            try
            {
                _logger.Info("正在获取用户登录尝试剩余次数: {Username}", username);
                var remainingAttempts = await _loginPolicy.GetRemainingAttemptsAsync(username);
                return Success(remainingAttempts);
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户登录尝试剩余次数失败: {Message}", ex.Message);
                return Error("获取用户登录尝试剩余次数失败", 500);
            }
        }

        /// <summary>
        /// 获取账户锁定剩余时间(秒)
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>剩余锁定时间(秒)</returns>
        [HttpGet("lockout/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLockoutRemainingSecondsAsync(string username)
        {
            try
            {
                _logger.Info("正在获取用户账户锁定剩余时间: {Username}", username);
                var remainingSeconds = await _loginPolicy.GetLockoutRemainingSecondsAsync(username);
                return Success(remainingSeconds);
            }
            catch (Exception ex)
            {
                _logger.Error("获取账户锁定剩余时间失败: {Message}", ex.Message);
                return Error("获取账户锁定剩余时间失败", 500);
            }
        }

        /// <summary>
        /// 获取注册配置
        /// </summary>
        /// <returns>注册配置信息</returns>
        [HttpGet("registration-config")]
        [AllowAnonymous]
        public IActionResult GetRegistrationConfig()
        {
            try
            {
                var registrationOptions = _configuration.GetSection("Security:Registration").Get<TaktRegistrationOptions>();
                return Success(registrationOptions, "获取注册配置成功");
            }
            catch (Exception ex)
            {
                _logger.Error("获取注册配置失败: {Message}", ex.Message);
                return Error("获取注册配置失败");
            }
        }
    }
}




