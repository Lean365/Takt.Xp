//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 登录服务实现 - 使用仓储工厂模式
//===================================================================

using Takt.Shared.Constants;
using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Caching;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Takt.Application.Services.Identity;

/// <summary>
/// 登录服务实现
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class TaktAuthService : TaktBaseService, ITaktAuthService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktCaptchaService _captchaService;
    private readonly ITaktJwtHandler _jwtHandler;
    private readonly ITaktMemoryCache _cache;
    private readonly TaktJwtOptions _jwtOptions;
    private readonly ITaktDeviceIdGenerator _deviceIdGenerator;
    private readonly IConfiguration _configuration;
    private readonly TaktCaptchaOptions _captchaOptions;
    private readonly ITaktSingleUserLoginService _singleUserLoginService;

    private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();
    private ITaktRepository<TaktLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<TaktLoginLog>();
    private ITaktRepository<TaktSignalROnline> OnlineUserRepository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();
    private ITaktRepository<TaktDeviceLog> DeviceLogRepository => _repositoryFactory.GetAuthRepository<TaktDeviceLog>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="captchaService">验证码服务</param>
    /// <param name="jwtHandler">JWT处理器</param>
    /// <param name="cache">缓存服务</param>
    /// <param name="jwtOptions">JWT配置选项</param>
    /// <param name="captchaOptions">验证码配置选项</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    /// <param name="deviceIdGenerator">设备ID生成器</param>
    /// <param name="configuration">配置服务</param>
    /// <param name="singleUserLoginService">单用户登录服务</param>


    public TaktAuthService(
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktRepositoryFactory repositoryFactory,
        ITaktCaptchaService captchaService,
        ITaktJwtHandler jwtHandler,
        ITaktMemoryCache cache,
        IOptions<TaktJwtOptions> jwtOptions,
        IOptions<TaktCaptchaOptions> captchaOptions,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization,
        ITaktDeviceIdGenerator deviceIdGenerator,
        IConfiguration configuration,
        ITaktSingleUserLoginService singleUserLoginService) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _captchaService = captchaService;
        _jwtHandler = jwtHandler;
        _cache = cache;
        _jwtOptions = jwtOptions.Value;
        _captchaOptions = captchaOptions.Value;
        _deviceIdGenerator = deviceIdGenerator;
        _configuration = configuration;
        _singleUserLoginService = singleUserLoginService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="loginDto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<TaktLoginResultDto> LoginAsync(TaktAuthDto loginDto)
    {
        _logger.Info(L("Identity.Auth.LoginStart", loginDto.UserName));

        try
        {
            // 第一步：验证用户是否存在
            // 通过用户名查询用户信息，如果用户不存在则直接返回错误
            var user = await GetUserAsync(loginDto.UserName);
            if (user == null)
            {
                _logger.Warn(L("Identity.Auth.UserNotFound", loginDto.UserName));
                throw new TaktException(L("Identity.User.InvalidCredentials"), TaktConstants.ErrorCodes.Unauthorized);
            }

            // 第二步：生成设备ID（提前生成，用于页面刷新判断）
            var deviceFingerprintJson = loginDto.LoginFingerprint != null 
                ? JsonConvert.SerializeObject(loginDto.LoginFingerprint, Formatting.None) 
                : "{}";
            var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
            var loginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext);
            
            
            var deviceId = _deviceIdGenerator.GenerateDeviceId(deviceFingerprintJson, user.Id.ToString(), userAgent, loginIp);
            
            // 验证生成的设备ID
            if (string.IsNullOrEmpty(deviceId))
            {
                _logger.Warn("设备ID生成失败，使用备用方案 - UserId: {UserId}", user.Id);
                deviceId = $"fallback_{user.Id}_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid().ToString("N")[..8]}";
            }
            

            // 第三步：检查是否是页面刷新
            // 页面刷新判断：只有在同一设备且在线状态且极短时间内重新登录才认为是页面刷新
            bool isPageRefresh = false;
            var onlineUser = await OnlineUserRepository.GetFirstAsync(u => u.UserId == user.Id && u.DeviceId == deviceId);
            if (onlineUser != null && onlineUser.OnlineStatus == 0)  // 修正：检查在线状态（0=在线）
            {
                // 检查是否是同一设备且在30秒内重新登录（页面刷新）
                var timeDiff = DateTime.Now - onlineUser.LastActivity;
                if (timeDiff.TotalSeconds < 30 && onlineUser.DeviceId == deviceId)
                {
                    isPageRefresh = true;
                    _logger.Info("检测到页面刷新: UserId={UserId}, DeviceId={DeviceId}, 时间差={TimeDiff}秒",
                        user.Id, deviceId, timeDiff.TotalSeconds);
                }
                else
                {
                    _logger.Info("检测到正常登录: UserId={UserId}, DeviceId={DeviceId}, 时间差={TimeDiff}秒",
                        user.Id, deviceId, timeDiff.TotalSeconds);
                }
            }
            else if (onlineUser != null)
            {
                _logger.Info("检测到设备离线后重新登录: UserId={UserId}, DeviceId={DeviceId}, OnlineStatus={OnlineStatus}",
                    user.Id, deviceId, onlineUser.OnlineStatus);
            }
            else
            {
                _logger.Info("检测到新设备登录: UserId={UserId}, DeviceId={DeviceId}",
                    user.Id, deviceId);
            }

            _logger.Info("页面刷新判断结果: UserId={UserId}, IsPageRefresh={IsPageRefresh}, IncrementLoginCount={IncrementLoginCount}",
                user.Id, isPageRefresh, !isPageRefresh);

            // 第四步：验证用户状态
            // 检查用户是否被禁用
            _logger.Info(L("Identity.Auth.UserValidationStart", loginDto.UserName));
            if (user.UserStatus != 0)
            {
                throw new TaktException(L("Identity.User.Disabled"));
            }

            // 第五步：单用户登录检查
            // 检查用户是否可以在当前设备登录
            _logger.Info(L("Identity.Auth.SingleUserLoginCheck", user.Id, deviceId));
            var (canLogin, reason) = await _singleUserLoginService.CheckUserLoginAsync(user.Id, deviceId, TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext), loginDto.LoginFingerprint);
            if (!canLogin)
            {
                _logger.Warn("用户 {UserId} 登录被拒绝: {Reason}", user.Id, reason);

                // 记录登录失败的日志
                var loginLogSingleUser = new TaktLoginLog
                {
                    UserId = user.Id,
                    UserName = loginDto.UserName,
                    LoginType = TaktLoginType.Password,
                    LoginStatus = TaktLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    LoginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext),
                    UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                    DeviceType = loginDto.DeviceType ?? "Desktop",
                    DeviceId = deviceId,
                    LoginMessage = reason ?? "单用户登录限制",
                    CreateBy = loginDto.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = loginDto.UserName,
                    UpdateTime = DateTime.Now
                };
                loginLogSingleUser = TaktStringHelper.EnsureEntityStringLength(loginLogSingleUser);
                await LoginLogRepository.CreateAsync(loginLogSingleUser);

                // 单用户登录限制：不记录设备日志，因为该设备从未成功登录过
                _logger.Info("单用户登录限制，跳过设备日志记录: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);
                
                // 确保不会执行后续的设备相关操作
                _logger.Info("单用户登录失败，终止登录流程，不会执行任何设备日志或在线记录操作: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

                throw new TaktException(reason ?? "您的账号已在其他地方登录", TaktConstants.ErrorCodes.Unauthorized);
            }
            _logger.Info("单用户登录检查通过: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

            // 第六步：验证密码
            // 使用盐值和迭代次数验证密码
            _logger.Info(L("Identity.Auth.PasswordValidationStart", user.Id, loginDto.Password?.Length ?? 0));
            var passwordValid = TaktPasswordUtils.VerifyHash(loginDto.Password, user.Password, user.Salt, user.Iterations);
            _logger.Info(L("Identity.Auth.PasswordValidationResult", passwordValid ? "Success" : "Failed"));

            if (!passwordValid)
            {
                // 密码验证失败，更新登录失败记录
                var loginLogPasswordValid = new TaktLoginLog
                {
                    UserId = user.Id,
                    UserName = loginDto.UserName,
                    LoginType = TaktLoginType.Password,
                    LoginStatus = TaktLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    LoginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext),
                    UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                    DeviceType = loginDto.DeviceType ?? "Desktop",
                    DeviceId = deviceId,
                    LoginMessage = "密码错误",
                    CreateBy = loginDto.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = loginDto.UserName,
                    UpdateTime = DateTime.Now
                };
                loginLogPasswordValid = TaktStringHelper.EnsureEntityStringLength(loginLogPasswordValid);
                await LoginLogRepository.CreateAsync(loginLogPasswordValid);

                // 密码验证失败：不记录设备日志，因为该设备从未成功登录过
                _logger.Info("密码验证失败，跳过设备日志记录: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

                throw new TaktException(L("Identity.User.InvalidCredentials"), TaktConstants.ErrorCodes.Unauthorized);
            }

            // 第七步：验证验证码（如果需要）
            // 检查验证码是否启用，如果启用且提供了验证码，则进行验证
            if (_captchaOptions.Enabled && !string.IsNullOrEmpty(loginDto.CaptchaToken))
            {
                _logger.Info(L("Identity.Auth.CaptchaValidationStart", loginDto.CaptchaToken, loginDto.CaptchaOffset));

                bool captchaValid;
                var captchaType = loginDto.CaptchaType?.Trim() ?? _captchaOptions.Type;

                // 根据验证码类型调用对应的验证方法
                if (captchaType.Equals("Behavior", StringComparison.OrdinalIgnoreCase))
                {
                    _logger.Info("使用行为验证码验证: {Token}", loginDto.CaptchaToken);
                    captchaValid = await _captchaService.ValidateBehaviorForLoginAsync(loginDto.CaptchaToken);
                }
                else
                {
                    _logger.Info("使用滑块验证码验证: {Token}, Offset: {Offset}", loginDto.CaptchaToken, loginDto.CaptchaOffset);
                    captchaValid = await _captchaService.ValidateSliderAsync(loginDto.CaptchaToken, loginDto.CaptchaOffset);
                }

                _logger.Info(L("Identity.Auth.CaptchaValidationResult", captchaValid ? "Success" : "Failed"));

                if (!captchaValid)
                {
                    // 验证码验证失败，记录日志
                    var loginLogCaptchaValid = new TaktLoginLog
                    {
                        UserId = user.Id,
                        UserName = loginDto.UserName,
                        LoginType = TaktLoginType.Password,
                        LoginStatus = TaktLoginStatus.Failed,
                        LoginTime = DateTime.Now,
                        LoginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext),
                        UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                        DeviceType = loginDto.DeviceType ?? "Desktop",
                        DeviceId = deviceId,
                        LoginMessage = "验证码错误",
                        CreateBy = loginDto.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = loginDto.UserName,
                        UpdateTime = DateTime.Now
                    };
                    loginLogCaptchaValid = TaktStringHelper.EnsureEntityStringLength(loginLogCaptchaValid);
                    await LoginLogRepository.CreateAsync(loginLogCaptchaValid);

                    // 验证码错误：不记录设备日志，因为该设备从未成功登录过
                    _logger.Info("验证码错误，跳过设备日志记录: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

                    throw new TaktException(L("Identity.User.InvalidCaptcha"), TaktConstants.ErrorCodes.InvalidCaptcha);
                }
            }
            else if (_captchaOptions.Enabled && string.IsNullOrEmpty(loginDto.CaptchaToken))
            {
                // 验证码已启用但未提供验证码，记录日志并抛出异常
                _logger.Warn("验证码已启用但未提供验证码令牌");
                var loginLogCptchaEnabled = new TaktLoginLog
                {
                    UserId = user.Id,
                    UserName = loginDto.UserName,
                    LoginType = TaktLoginType.Password,
                    LoginStatus = TaktLoginStatus.Failed,
                    LoginTime = DateTime.Now,
                    LoginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext),
                    UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
                    DeviceType = loginDto.DeviceType ?? "Desktop",
                    DeviceId = deviceId,
                    LoginMessage = "验证码已启用但未提供验证码",
                    CreateBy = loginDto.UserName,
                    CreateTime = DateTime.Now,
                    UpdateBy = loginDto.UserName,
                    UpdateTime = DateTime.Now
                };
                loginLogCptchaEnabled = TaktStringHelper.EnsureEntityStringLength(loginLogCptchaEnabled);
                await LoginLogRepository.CreateAsync(loginLogCptchaEnabled);

                // 验证码验证失败：不记录设备日志，因为该设备从未成功登录过
                _logger.Info("验证码验证失败，跳过设备日志记录: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

                throw new TaktException(L("Identity.User.CaptchaRequired"), TaktConstants.ErrorCodes.CaptchaRequired);
            }

            // 第八步：获取用户权限
            // 获取用户在租户中的角色和权限
            _logger.Info(L("Identity.Auth.GetUserRolesAndPermissions", user.Id));
            var roles = await UserRepository.GetUserRolesAsync(user.Id);
            var permissions = await UserRepository.GetUserPermissionsAsync(user.Id);
            _logger.Info(L("Identity.Auth.UserRolesAndPermissionsResult", roles.Count, permissions.Count));

            _logger.Info(L("Identity.Auth.GeneratedIds", deviceId));

            // 第九步：生成令牌
            // 生成访问令牌和刷新令牌
            _logger.Info(L("Identity.Auth.GenerateTokens"));
            var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
            var refreshToken = await _jwtHandler.GenerateRefreshTokenAsync();
            _logger.Info(L("Identity.Auth.TokensGenerated", accessToken.Length, refreshToken));

            // 第十步：缓存刷新令牌
            // 将刷新令牌存入缓存，用于后续的令牌刷新
            _logger.Info(L("Identity.Auth.CacheRefreshToken", refreshToken));
            await _cache.SetAsync($"refresh_token:{refreshToken}", user.Id.ToString(),
                TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
            _logger.Info(L("Identity.Auth.RefreshTokenCached"));

            // 第十一步：记录登录日志
            // 记录完整的登录信息
            _logger.Info(L("Identity.Auth.ProcessLoginLog"));
            TaktLoginLog loginLog;
            loginLog = await LoginLogAsync(user.Id, user.UserName,
                loginDto.DeviceType ?? "Desktop", DateTime.Now, deviceId);
            _logger.Info(L("Identity.Auth.LoginLogProcessed"));

            // 第十二步：记录设备日志（合并了原来的设备日志和环境日志）
            // 记录用户的设备信息和环境信息
            _logger.Info(L("Identity.Auth.ProcessDeviceInfo"));
            
            // 重要：此时已经通过了单用户登录检查，可以安全地记录设备日志
            _logger.Info("[设备日志] 单用户登录检查已通过，现在开始记录设备日志: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

            try
            {
                _logger.Info("[设备日志] 开始处理设备日志: UserId={UserId}, DeviceType={DeviceType}, DeviceId={DeviceId}, HasFingerprint={HasFingerprint}",
                    user.Id, loginDto.DeviceType ?? "Desktop", deviceId, loginDto.LoginFingerprint != null);

                var deviceLog = await DeviceLogAsync(user.Id, loginDto.DeviceType ?? "Desktop", DateTime.Now, !isPageRefresh, deviceId, loginDto.LoginFingerprint);

                _logger.Info("[设备日志] 设备日志处理完成: UserId={UserId}, DeviceLogId={DeviceLogId}", user.Id, deviceLog?.Id);
                _logger.Info(L("Identity.Auth.DeviceInfoProcessed"));
            }
            catch (Exception ex)
            {
                _logger.Error("[设备日志] 设备日志处理失败: UserId={UserId}, Error={Error}, StackTrace={StackTrace}",
                    user.Id, ex.Message, ex.StackTrace);
                // 设备日志失败不应该影响登录流程，只记录错误
            }

            _logger.Info(L("Identity.Auth.LoginSuccess", user.Id, user.UserName));

            // 第十三步：创建在线用户记录（最后一步）
            // 只有在所有验证都通过后才创建在线记录
            _logger.Info("开始创建在线用户记录 - UserId: {UserId}, DeviceId: {DeviceId}", user.Id, deviceId);
            var frontendDeviceId = loginDto.LoginFingerprint?.DeviceId;
            var generatedConnectionId = _deviceIdGenerator.GenerateConnectionId(deviceId);
            var connectionId = !string.IsNullOrEmpty(frontendDeviceId) 
                ? $"{frontendDeviceId}_{generatedConnectionId}"
                : $"HTTP_LOGIN_{generatedConnectionId}";
            var loginResult = await _singleUserLoginService.HandleUserLoginAsync(user.Id, deviceId, connectionId, TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext), 
                _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(), user.UserName);
            if (loginResult.Code != 200)
            {
                _logger.Error("在线用户记录创建失败: {Message}", loginResult.Msg);
                throw new TaktException("登录处理失败，请重试", TaktConstants.ErrorCodes.ServerError);
            }
            _logger.Info("在线用户记录创建成功: UserId={UserId}, DeviceId={DeviceId}", user.Id, deviceId);

            // 第十四步：返回登录结果
            // 返回包含令牌和用户信息的登录结果
            return new TaktLoginResultDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
                UserInfo = new TaktUserInfoDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    NickName = user.NickName ?? string.Empty,
                    EnglishName = user.EnglishName ?? string.Empty,
                    FullName = user.FullName ?? string.Empty,
                    RealName = user.RealName ?? string.Empty,
                    UserType = user.UserType,
                    Avatar = user.Avatar ?? string.Empty,
                    Roles = roles,
                    Permissions = permissions
                }
            };
        }
        catch (TaktException)
        {
            throw;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Identity.Auth.LoginError", ex.Message), ex);
            throw new TaktException(L("Identity.Auth.ServerError"), TaktConstants.ErrorCodes.ServerError, ex);
        }
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public async Task<TaktLoginResultDto> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
        {
            _logger.Warn("刷新令牌为空");
            throw new TaktException(L("Identity.Auth.InvalidRefreshToken"));
        }

        _logger.Info("开始刷新令牌: Token={Token}", refreshToken.Substring(0, Math.Min(10, refreshToken.Length)) + "...");


        // 1. 验证刷新令牌 - 使用异步方法
        var cacheKey = $"refresh_token:{refreshToken}";
        _logger.Info("尝试从缓存获取刷新令牌: CacheKey={CacheKey}", cacheKey);

        var userId = await _cache.GetAsync<string>(cacheKey);
        if (string.IsNullOrEmpty(userId))
        {
            _logger.Warn("刷新令牌在缓存中不存在或已过期: CacheKey={CacheKey}", cacheKey);
            throw new TaktException(L("Identity.Auth.InvalidRefreshToken"));
        }

        _logger.Info("从缓存获取到用户ID: UserId={UserId}", userId);

        // 2. 获取用户信息
        var user = await UserRepository.GetByIdAsync(long.Parse(userId));
        if (user == null)
        {
            _logger.Warn("用户不存在: UserId={UserId}", userId);
            throw new TaktException(L("Identity.User.NotFound"));
        }
        if (user.UserStatus != 0)
        {
            _logger.Warn("用户已被禁用: UserId={UserId}, Status={Status}", userId, user.UserStatus);
            throw new TaktException(L("Identity.User.Disabled"));
        }

        _logger.Info("用户验证通过: UserId={UserId}, UserName={UserName}", user.Id, user.UserName);

        // 4. 获取用户角色和权限
        var roles = await UserRepository.GetUserRolesAsync(user.Id);
        var permissions = await UserRepository.GetUserPermissionsAsync(user.Id);

        _logger.Info("获取用户角色和权限完成: RolesCount={RolesCount}, PermissionsCount={PermissionsCount}",
            roles.Count, permissions.Count);

        // 5. 生成新令牌
        var accessToken = await _jwtHandler.GenerateAccessTokenAsync(user, roles.ToArray(), permissions.ToArray());
        var newRefreshToken = await _jwtHandler.GenerateRefreshTokenAsync();

        _logger.Info("新令牌生成完成: AccessTokenLength={AccessTokenLength}, NewRefreshToken={NewRefreshToken}",
            accessToken.Length, newRefreshToken.Substring(0, Math.Min(10, newRefreshToken.Length)) + "...");

        // 6. 更新缓存 - 先设置新令牌，再删除旧令牌，避免竞态条件
        var newCacheKey = $"refresh_token:{newRefreshToken}";
        await _cache.SetAsync(newCacheKey, userId, TimeSpan.FromDays(_jwtOptions.RefreshTokenExpirationDays));
        await _cache.RemoveAsync(cacheKey);

        _logger.Info("缓存更新完成: 新令牌已缓存，旧令牌已删除");

        // 7. 返回登录结果
        return new TaktLoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = _jwtOptions.ExpirationMinutes * 60,
            UserInfo = new TaktUserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                NickName = user.NickName ?? string.Empty,
                EnglishName = user.EnglishName ?? string.Empty,
                FullName = user.FullName ?? string.Empty,
                RealName = user.RealName ?? string.Empty,
                UserType = user.UserType,
                Avatar = user.Avatar ?? string.Empty,
                Roles = roles,
                Permissions = permissions
            }
        };
    }

    /// <summary>
    /// 用户登出
    /// </summary>
    public async Task<bool> LogoutAsync(long userId)
    {
        // 调用带设备信息的登出方法，使用默认值
        return await LogoutAsync(userId, new TaktLogoutDto());
    }

    /// <summary>
    /// 用户登出（带设备信息）
    /// </summary>
    public async Task<bool> LogoutAsync(long userId, TaktLogoutDto logoutDto)
    {
        try
        {
            _logger.Info("开始处理用户登出: UserId={0}", userId);
            var now = DateTime.Now;

            // 获取用户信息
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException(L("Identity.User.NotFound", userId));
            }

            // 第一步：获取设备信息，确保与登录时完全一致
            string? deviceType = null;
            string? deviceId = null;

            // 如果前端传递了完整的设备指纹信息，使用与登录时相同的方式生成设备ID
            if (logoutDto.LoginFingerprint != null)
            {
                deviceType = logoutDto.DeviceType;
                var deviceFingerprintJson = JsonConvert.SerializeObject(logoutDto.LoginFingerprint, Formatting.None);
                var userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
                var loginIp = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext);
                deviceId = _deviceIdGenerator.GenerateDeviceId(deviceFingerprintJson, userId.ToString(), userAgent, loginIp);

                _logger.Info("使用前端设备指纹信息生成设备ID: DeviceType={DeviceType}, DeviceId={DeviceId}", deviceType, deviceId);
            }
            // 如果前端只传递了设备类型和设备ID，直接使用
            else if (!string.IsNullOrEmpty(logoutDto.DeviceType) || !string.IsNullOrEmpty(logoutDto.DeviceId))
            {
                deviceType = logoutDto.DeviceType;
                deviceId = logoutDto.DeviceId;
                _logger.Info("使用前端传递的设备信息: DeviceType={DeviceType}, DeviceId={DeviceId}", deviceType, deviceId);
            }

            // 如果前端没有传递任何设备信息，从当前会话获取
            if (deviceType == null || deviceId == null)
            {
                // 修复：先查询用户的所有在线设备，确保设备隔离
                var onlineUsers = await OnlineUserRepository.GetListAsync(u => u.UserId == userId && u.OnlineStatus == 0);
                
                if (onlineUsers?.Any() == true)
                {
                    // 如果有多个在线设备，选择第一个（或者可以根据业务逻辑选择特定的设备）
                    var onlineUser = onlineUsers.First();
                    deviceId = onlineUser.DeviceId;
                    _logger.Info("从在线用户记录获取设备ID: {DeviceId}", deviceId);

                    // 从设备日志获取设备类型，确保与登录时一致（优化：只查询必要字段）
                    var deviceLog = await DeviceLogRepository.SqlSugarClient
                        .Queryable<TaktDeviceLog>()
                        .Where(x => x.UserId == userId && (x.LastLoginDeviceId == deviceId || x.FirstLoginDeviceId == deviceId))
                        .Select(x => new { x.LastLoginDeviceType, x.FirstLoginDeviceType })
                        .FirstAsync();
                    if (deviceLog != null)
                    {
                        deviceType = deviceLog.LastLoginDeviceType ?? deviceLog.FirstLoginDeviceType;
                        _logger.Info("从设备日志获取设备类型: {DeviceType}", deviceType);
                    }
                    else
                    {
                        _logger.Warn("未找到对应的设备日志记录: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                    }
                }
                else
                {
                    // 如果没有在线记录，无法确定具体设备，使用默认值
                    _logger.Warn("未找到用户在线设备记录，无法确定登出设备: UserId={UserId}", userId);
                    deviceType = "Unknown";
                    deviceId = $"logout_{userId}_{DateTime.Now:yyyyMMddHHmmss}";
                }
            }

            // 验证是否获取到设备信息
            if (string.IsNullOrEmpty(deviceType) || string.IsNullOrEmpty(deviceId))
            {
                _logger.Error("无法获取用户设备信息，登出失败: UserId={UserId}", userId);
                throw new InvalidOperationException("无法获取用户设备信息，登出失败");
            }

            // 记录最终使用的设备信息
            _logger.Info("登出时使用的设备信息: UserId={UserId}, DeviceType={DeviceType}, DeviceId={DeviceId}",
                userId, deviceType, deviceId);

            // 第二步：记录登出日志，使用正确的设备信息
            await LoginLogAsync(userId, user.UserName, deviceType, now, deviceId, true);

            // 第三步：跳过设备登出日志更新（退出登录不更新设备日志）
            _logger.Info("跳过设备登出日志更新: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);

            // 第四步：通过单用户登录服务更新在线用户记录状态和时间
            var onlineUserForUpdate = await OnlineUserRepository.GetFirstAsync(u => u.UserId == userId && u.DeviceId == deviceId);
            if (onlineUserForUpdate != null)
            {
                // 使用单用户登录服务处理登出，确保一致性
                var logoutResult = await _singleUserLoginService.HandleUserLogoutAsync(userId, onlineUserForUpdate.ConnectionId ?? string.Empty, user.UserName);
                if (logoutResult)
                {
                    _logger.Info("已通过单用户登录服务更新用户状态为离线: UserId={0}, ConnectionId={1}, LastActivity={2}",
                        userId, onlineUserForUpdate.ConnectionId, now.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    _logger.Warn("单用户登录服务处理登出失败: UserId={0}", userId);
                }
            }
            else
            {
                _logger.Warn("未找到用户在线记录: UserId={0}", userId);
            }

            // 第五步：记录登出成功日志
            _logger.Info("用户登出处理完成: UserId={0}, UserName={1}, LogoutTime={2}",
                userId, user.UserName, now.ToString("yyyy-MM-dd HH:mm:ss"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("处理用户登出时发生错误: UserId={0}", new[] { userId.ToString() }, ex);
            return false;
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>

    public async Task<TaktUserInfoDto> GetUserInfoAsync(long userId)
    {
        // 获取用户基本信息
        var user = await UserRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new TaktException(L("Identity.User.NotFound", userId), TaktConstants.ErrorCodes.NotFound);
        }

        // 获取用户角色和权限
        var roles = await UserRepository.GetUserRolesAsync(userId);
        var permissions = await UserRepository.GetUserPermissionsAsync(userId);

        // 构建用户信息（不包含租户信息）
        var userInfo = new TaktUserInfoDto
        {
            UserId = user.Id,
            UserName = user.UserName,
            NickName = user.NickName ?? string.Empty,
            EnglishName = user.EnglishName ?? string.Empty,
            FullName = user.FullName ?? string.Empty,
            RealName = user.RealName ?? string.Empty,
            UserType = user.UserType,
            Avatar = user.Avatar ?? string.Empty,
            Roles = roles,
            Permissions = permissions,
        };

        _logger.Info("多租户未启用，获取用户信息成功: UserId={UserId}", userId);

        return userInfo;
    }


    /// <summary>
    /// 获取用户盐值
    /// </summary>
    /// <param name="username">用户名</param>
    /// <returns>用户盐值信息</returns>
    public async Task<(string Salt, int Iterations)?> GetUserSaltAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var user = await UserRepository.GetFirstAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return (user.Salt, user.Iterations);
    }




    /// <summary>
    /// 处理登录/登出日志（新增或更新）
    /// </summary>
    private async Task<TaktLoginLog> LoginLogAsync(long userId, string userName, string deviceType, DateTime now, string deviceId, bool isLogout = false)
    {
        var actionType = isLogout ? "登出" : "登录";
        _logger.Info("创建新的{0}日志: UserId={1}", actionType, userId);

        TaktLoginLog loginLog;
        // 获取IP地址和位置信息
        var backendIpAddress = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext);

        // 使用后端IP地址
        var finalIpAddress = backendIpAddress;

        // 根据IP获取位置信息
        var finalLocation = await TaktIpUtils.GetLocationAsync(finalIpAddress);

        loginLog = new TaktLoginLog
        {
            UserId = userId,
            UserName = userName,
            LoginType = isLogout ? TaktLoginType.Other : TaktLoginType.Password, // 登出时使用Other类型
            LoginStatus = isLogout ? TaktLoginStatus.Offline : TaktLoginStatus.Success,
            LoginTime = now,
            LoginIp = finalIpAddress,
            LoginLocation = finalLocation,
            UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString(),
            DeviceType = deviceType,
            DeviceId = deviceId?.ToString(),
            LoginMessage = isLogout ? $"用户 {userName} 登出成功" : $"用户 {userName} 登录成功",
            CreateBy = userName,
            CreateTime = now,
            UpdateBy = userName,
            UpdateTime = now
        };

        loginLog = TaktStringHelper.EnsureEntityStringLength(loginLog);
        await LoginLogRepository.CreateAsync(loginLog);
        _logger.Info("{0}日志创建完成", actionType);

        return loginLog;
    }

    /// <summary>
    /// 处理设备日志（新增或更新）- 合并了原来的环境日志和设备日志功能
    /// </summary>
    private async Task<TaktDeviceLog> DeviceLogAsync(long userId, string deviceType, DateTime now, bool incrementLoginCount = true, string deviceId = "", TaktLoginFingerprint? deviceFingerprint = null)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        _logger.Info("[设备日志] 开始处理设备日志: UserId={UserId}, DeviceId={DeviceId}, IncrementLoginCount={IncrementLoginCount}",
            userId, deviceId, incrementLoginCount);

        // 获取用户名
        var userStopwatch = System.Diagnostics.Stopwatch.StartNew();
        var user = await UserRepository.GetByIdAsync(userId);
        var userName = user?.UserName ?? throw new InvalidOperationException($"用户 {userId} 不存在");
        userStopwatch.Stop();
        _logger.Info("[设备日志] 用户查询耗时: {ElapsedMs}ms", userStopwatch.ElapsedMilliseconds);

        // 如果设备ID为空，生成默认值
        if (string.IsNullOrEmpty(deviceId))
        {
            deviceId = $"web_{userId}_{DateTime.Now:yyyyMMddHHmmss}";
            _logger.Warn("设备ID为空，生成默认设备ID: {DeviceId}", deviceId);
        }

        // 查询现有设备记录（包含所有必要字段）
        // 修复：确保设备隔离，只查询完全匹配当前设备的记录
        var deviceQueryStopwatch = System.Diagnostics.Stopwatch.StartNew();
        var deviceLog = await DeviceLogRepository.SqlSugarClient
            .Queryable<TaktDeviceLog>()
            .Where(x => x.UserId == userId && x.LastLoginDeviceId == deviceId)
            .FirstAsync();
        
        // 如果没有找到LastLoginDeviceId匹配的记录，再查找FirstLoginDeviceId匹配的记录
        if (deviceLog == null)
        {
            deviceLog = await DeviceLogRepository.SqlSugarClient
                .Queryable<TaktDeviceLog>()
                .Where(x => x.UserId == userId && x.FirstLoginDeviceId == deviceId)
                .FirstAsync();
        }
        
        // 双重验证：确保返回的设备日志确实属于当前设备
        if (deviceLog != null)
        {
            var isCurrentDevice = deviceLog.LastLoginDeviceId == deviceId || deviceLog.FirstLoginDeviceId == deviceId;
            if (!isCurrentDevice)
            {
                _logger.Warn("[设备日志] 获取到的设备日志不属于当前设备 - 当前设备ID: {CurrentDeviceId}, 设备日志ID: {DeviceLogId}, LastLoginDeviceId: {LastLoginDeviceId}, FirstLoginDeviceId: {FirstLoginDeviceId}", 
                    deviceId, deviceLog.Id, deviceLog.LastLoginDeviceId ?? "null", deviceLog.FirstLoginDeviceId ?? "null");
                deviceLog = null; // 清空不匹配的设备日志
            }
        }
        
        deviceQueryStopwatch.Stop();
        _logger.Info("[设备日志] 设备记录查询耗时: {ElapsedMs}ms, 找到记录: {Found}, LastLoginDeviceId: {LastLoginDeviceId}, FirstLoginDeviceId: {FirstLoginDeviceId}", 
            deviceQueryStopwatch.ElapsedMilliseconds, deviceLog != null, 
            deviceLog?.LastLoginDeviceId ?? "null", deviceLog?.FirstLoginDeviceId ?? "null");

        // 注释：移除主要设备记录查询，每个设备独立管理登录次数
        // 原来的逻辑有问题：mainDeviceLog 查询的是用户所有设备中最后登录的设备
        // 这会导致设备之间的登录次数互相影响，造成数据混乱
        // 现在改为每个设备独立管理自己的登录次数

        // 获取IP地址和位置信息
        var backendIpAddress = TaktIpUtils.GetClientIpAddress(_httpContextAccessor.HttpContext);

        // 使用后端IP地址
        var finalIpAddress = backendIpAddress;

        // 根据IP获取位置信息（优化：对于本地IP直接返回，避免查询）
        var locationStopwatch = System.Diagnostics.Stopwatch.StartNew();
        var finalLocation = finalIpAddress == "127.0.0.1" || finalIpAddress == "::1"
            ? "本地"
            : await TaktIpUtils.GetLocationAsync(finalIpAddress);
        locationStopwatch.Stop();
        _logger.Info("[设备日志] IP位置查询耗时: {ElapsedMs}ms", locationStopwatch.ElapsedMilliseconds);

        if (deviceLog == null)
        {
            _logger.Info("创建新的设备日志: UserId={0}, DeviceId={1}", userId, deviceId);

            // 每个设备独立计算登录次数，不共享其他设备的登录次数
            var loginCount = 1;
            var continuousLoginDays = 1;
            
            if (incrementLoginCount)
            {
                _logger.Info("新设备登录，设置登录次数为1: UserId={0}, DeviceId={1}", userId, deviceId);
            }
            else
            {
                _logger.Info("新设备页面刷新，设置登录次数为1: UserId={0}, DeviceId={1}", userId, deviceId);
            }

            deviceLog = new TaktDeviceLog
            {
                UserId = userId,
                DeviceToken = $"conn_{userId}_{DateTime.Now:yyyyMMddHHmmss}",
                DeviceInfo = deviceFingerprint != null ? SerializeDeviceFingerprint(deviceFingerprint) : null,
                ProviderKey = "system",
                ProviderDisplayName = "系统登录",
                NetworkType = deviceFingerprint?.NetworkType ?? "WIFI",
                TimeZone = deviceFingerprint?.Timezone ?? "Asia/Shanghai",
                Language = deviceFingerprint?.Language ?? "zh-CN",
                IsVpn = deviceFingerprint?.IsVpn == "是" ? "是" : "否",
                IsProxy = deviceFingerprint?.IsProxy == "是" ? "是" : "非代理",
                LoginType = 0,
                LoginSource = 0,
                LoginProvider = 0,
                FirstLoginTime = now,
                FirstLoginIp = finalIpAddress,
                FirstLoginLocation = finalLocation,
                FirstLoginDeviceId = deviceId,
                FirstLoginDeviceType = deviceType,
                FirstLoginBrowser = deviceFingerprint?.Browser ?? "Unknown",
                FirstLoginOs = deviceFingerprint?.Os ?? "Unknown",
                LastLoginTime = now,
                LastLoginIp = finalIpAddress,
                LastLoginLocation = finalLocation,
                LastLoginDeviceId = deviceId,
                LastLoginDeviceType = deviceType,
                LastLoginBrowser = deviceFingerprint?.Browser ?? "Unknown",
                LastLoginOs = deviceFingerprint?.Os ?? "Unknown",
                LastOnlineTime = now,
                LoginCount = loginCount,
                ContinuousLoginDays = continuousLoginDays,
                DeviceStatus = 0,
                CreateBy = userName,
                CreateTime = now,
                UpdateBy = userName,
                UpdateTime = now
            };


            _logger.Info("创建新设备日志: UserId={0}, LoginCount={1}, ContinuousLoginDays={2}", userId, deviceLog.LoginCount, deviceLog.ContinuousLoginDays);

            deviceLog = TaktStringHelper.EnsureEntityStringLength(deviceLog);
            var createStopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                _logger.Info("[设备日志] 准备创建设备日志: UserId={UserId}, DeviceId={DeviceId}, DeviceInfo长度={DeviceInfoLength}",
                    userId, deviceId, deviceLog.DeviceInfo?.Length ?? 0);

                var result = await DeviceLogRepository.CreateAsync(deviceLog);
                createStopwatch.Stop();

                _logger.Info("[设备日志] 设备日志创建成功: UserId={UserId}, DeviceId={DeviceId}, 影响行数={Result}, 耗时={ElapsedMs}ms",
                    userId, deviceId, result, createStopwatch.ElapsedMilliseconds);

                // 注释：移除主要设备记录同步更新逻辑
                // 每个设备独立管理自己的登录次数，不需要同步更新其他设备
            }
            catch (Exception ex)
            {
                createStopwatch.Stop();
                _logger.Error("[设备日志] 设备日志创建失败: UserId={UserId}, DeviceId={DeviceId}, 错误={Error}, 耗时={ElapsedMs}ms",
                    userId, deviceId, ex.Message, createStopwatch.ElapsedMilliseconds);
                throw; // 重新抛出异常，让上层处理
            }
        }
        else
        {
            _logger.Info("更新现有设备日志: DeviceId={0}", deviceId);

            // 更新当前设备的登录次数：每个设备独立管理，不依赖其他设备
            if (incrementLoginCount)
            {
                // 正常登录：增加当前设备的登录次数
                var oldLoginCount = deviceLog.LoginCount;
                deviceLog.LoginCount++;
                _logger.Info("正常登录，增加当前设备登录次数: UserId={0}, DeviceId={1}, OldLoginCount={2}, NewLoginCount={3}",
                    userId, deviceId, oldLoginCount, deviceLog.LoginCount);
            }
            else
            {
                // 页面刷新：保持当前设备登录次数不变
                _logger.Info("页面刷新，保持当前设备登录次数不变: UserId={0}, DeviceId={1}, LoginCount={2}",
                    userId, deviceId, deviceLog.LoginCount);
            }

            // 更新设备信息
            deviceLog.LastLoginTime = now;
            deviceLog.LastLoginIp = finalIpAddress;
            deviceLog.LastLoginLocation = finalLocation;
            deviceLog.LastLoginDeviceId = deviceId;
            deviceLog.LastLoginDeviceType = deviceType;
            deviceLog.LastOnlineTime = now; // 更新最后在线时间
            deviceLog.DeviceStatus = 0; // 设置为正常状态（0表示正常，设备可用）
            deviceLog.UpdateTime = now;
            deviceLog.UpdateBy = userName;

            // 如果首次登录字段为空，补充这些字段
            if (!deviceLog.FirstLoginTime.HasValue)
            {
                deviceLog.FirstLoginTime = now;
                deviceLog.FirstLoginIp = finalIpAddress;
                deviceLog.FirstLoginLocation = finalLocation;
                deviceLog.FirstLoginDeviceId = deviceId;
                deviceLog.FirstLoginDeviceType = deviceType;
                deviceLog.FirstLoginBrowser = deviceFingerprint?.Browser ?? "Unknown";
                deviceLog.FirstLoginOs = deviceFingerprint?.Os ?? "Unknown";

                _logger.Info("补充首次登录信息: FirstLoginTime={FirstLoginTime}, FirstLoginIp={FirstLoginIp}, DeviceType={DeviceType}, Browser={Browser}, Os={Os}",
                    now, finalIpAddress, deviceType, deviceFingerprint?.Browser ?? "Unknown", deviceFingerprint?.Os ?? "Unknown");

            }

            // 更新设备Token（如果为空）
            if (string.IsNullOrEmpty(deviceLog.DeviceToken))
            {
                deviceLog.DeviceToken = $"conn_{userId}_{DateTime.Now:yyyyMMddHHmmss}";
                _logger.Info("补充设备Token: {DeviceToken}", deviceLog.DeviceToken);
            }

            // 更新设备指纹信息（如果前端提供了新的指纹信息）
            if (deviceFingerprint != null)
            {
                // 只更新当前设备的指纹信息，不影响其他设备
                deviceLog.NetworkType = deviceFingerprint.NetworkType ?? deviceLog.NetworkType;
                deviceLog.TimeZone = deviceFingerprint.Timezone ?? deviceLog.TimeZone;
                deviceLog.Language = deviceFingerprint.Language ?? deviceLog.Language;
                deviceLog.IsVpn = deviceFingerprint.IsVpn == "是" ? "是" : (deviceFingerprint.IsVpn == "否" ? "否" : deviceLog.IsVpn);
                deviceLog.IsProxy = deviceFingerprint.IsProxy == "是" ? "是" : (deviceFingerprint.IsProxy == "否" ? "非代理" : deviceLog.IsProxy);
                deviceLog.LastLoginBrowser = deviceFingerprint.Browser ?? deviceLog.LastLoginBrowser;
                deviceLog.LastLoginOs = deviceFingerprint.Os ?? deviceLog.LastLoginOs;

                // 更新设备指纹信息（DeviceInfo字段）
                var newDeviceInfo = SerializeDeviceFingerprint(deviceFingerprint);
                if (!string.IsNullOrEmpty(newDeviceInfo))
                {
                    deviceLog.DeviceInfo = newDeviceInfo;
                    _logger.Info("更新当前设备指纹信息: DeviceId={DeviceId}, DeviceInfo长度={DeviceInfoLength}", 
                        deviceId, newDeviceInfo.Length);
                }
            }

            // 注释：登录次数更新逻辑已在上面的代码中处理，这里不需要重复处理

            // 更新连续登录天数
            var lastLoginDate = deviceLog.LastLoginTime.HasValue ? deviceLog.LastLoginTime.Value.Date : DateTime.MinValue;
            var today = now.Date;

            if (lastLoginDate == DateTime.MinValue)
            {
                // 首次登录
                deviceLog.ContinuousLoginDays = 1;
                _logger.Info("当前设备首次登录，设置连续登录天数为1: UserId={0}, DeviceId={1}", userId, deviceId);
            }
            else if (lastLoginDate.AddDays(1) == today)
            {
                // 连续登录（昨天登录，今天又登录）
                deviceLog.ContinuousLoginDays++;
                _logger.Info("当前设备连续登录，增加连续登录天数: UserId={0}, DeviceId={1}, ContinuousLoginDays={2}", userId, deviceId, deviceLog.ContinuousLoginDays);
            }
            else if (lastLoginDate == today)
            {
                // 同一天多次登录，不改变连续登录天数
                _logger.Info("当前设备同一天多次登录，保持连续登录天数不变: UserId={0}, DeviceId={1}, ContinuousLoginDays={2}", userId, deviceId, deviceLog.ContinuousLoginDays);
            }
            else
            {
                // 非连续登录，重置为1
                deviceLog.ContinuousLoginDays = 1;
                _logger.Info("当前设备非连续登录，重置连续登录天数为1: UserId={0}, DeviceId={1}, LastLoginDate={2}, Today={3}", userId, deviceId, lastLoginDate.ToString("yyyy-MM-dd"), today.ToString("yyyy-MM-dd"));
            }

            // 更新今日在线时段
            await UpdateTodayOnlinePeriodsAsync(deviceLog, now);

            deviceLog = TaktStringHelper.EnsureEntityStringLength(deviceLog);
            var updateStopwatch = System.Diagnostics.Stopwatch.StartNew();

            try
            {
                _logger.Info("[设备日志] 准备更新设备日志: UserId={UserId}, DeviceId={DeviceId}, DeviceLogId={DeviceLogId}",
                    userId, deviceId, deviceLog.Id);

                var result = await DeviceLogRepository.UpdateAsync(deviceLog);
                updateStopwatch.Stop();

                _logger.Info("[设备日志] 设备日志更新成功: UserId={UserId}, DeviceId={DeviceId}, 影响行数={Result}, 耗时={ElapsedMs}ms",
                    userId, deviceId, result, updateStopwatch.ElapsedMilliseconds);

                // 每个设备独立计算登录次数，不需要同步更新其他设备
                _logger.Info("当前设备登录次数更新完成: UserId={UserId}, DeviceId={DeviceId}, LoginCount={LoginCount}, ContinuousLoginDays={ContinuousLoginDays}",
                    userId, deviceId, deviceLog.LoginCount, deviceLog.ContinuousLoginDays);
            }
            catch (Exception ex)
            {
                updateStopwatch.Stop();
                _logger.Error("[设备日志] 设备日志更新失败: UserId={UserId}, DeviceId={DeviceId}, 错误={Error}, 耗时={ElapsedMs}ms",
                    userId, deviceId, ex.Message, updateStopwatch.ElapsedMilliseconds);
                throw; // 重新抛出异常，让上层处理
            }
        }

        stopwatch.Stop();
        _logger.Info("[设备日志] 设备日志处理完成: UserId={UserId}, 耗时={ElapsedMs}ms", userId, stopwatch.ElapsedMilliseconds);

        if (stopwatch.ElapsedMilliseconds > 5000) // 超过5秒记录警告
        {
            _logger.Warn("[设备日志] 设备日志处理耗时过长: UserId={UserId}, 耗时={ElapsedMs}ms", userId, stopwatch.ElapsedMilliseconds);
        }

        return deviceLog;
    }

    /// <summary>
    /// 更新今日在线时段
    /// </summary>
    private async Task UpdateTodayOnlinePeriodsAsync(TaktDeviceLog deviceLog, DateTime now)
    {
        await Task.Yield(); // 让出控制权，使方法真正异步
        try
        {
            var today = now.Date;
            var currentTime = now.ToString("HH:mm");

            // 解析现有的今日在线时段
            var existingPeriods = new List<string>();
            if (!string.IsNullOrEmpty(deviceLog.TodayOnlinePeriods))
            {
                existingPeriods = deviceLog.TodayOnlinePeriods.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
            }

            // 检查是否需要更新今日在线时段
            var shouldUpdate = false;

            // 如果是新的一天，清空之前的时段
            if (deviceLog.LastOnlineTime?.Date != today)
            {
                existingPeriods.Clear();
                shouldUpdate = true;
                _logger.Info("新的一天，清空之前的在线时段: UserId={0}, Today={1}", deviceLog.UserId, today.ToString("yyyy-MM-dd"));
            }

            // 添加当前登录时段
            var currentPeriod = $"{currentTime}-{currentTime}"; // 初始时段，后续可以通过心跳更新结束时间
            if (!existingPeriods.Contains(currentPeriod))
            {
                existingPeriods.Add(currentPeriod);
                shouldUpdate = true;
                _logger.Info("添加新的在线时段: UserId={0}, Period={1}", deviceLog.UserId, currentPeriod);
            }

            if (shouldUpdate)
            {
                deviceLog.TodayOnlinePeriods = existingPeriods.Count > 0 ? string.Join(",", existingPeriods) : null;
                _logger.Info("更新今日在线时段: UserId={0}, Periods={1}", deviceLog.UserId, deviceLog.TodayOnlinePeriods);
            }
        }
        catch (Exception ex)
        {
            _logger.Error("更新今日在线时段失败: UserId={0}, Error={1}", deviceLog.UserId, ex.Message);
        }
    }

    /// <summary>
    /// 处理设备登出日志更新
    /// </summary>
    private async Task UpdateDeviceLogoutAsync(long userId, DateTime now)
    {
        try
        {
            // 查询用户的设备记录（优化：只查询必要字段，避免查询大字段DeviceInfo）
            var deviceLog = await DeviceLogRepository.SqlSugarClient
                .Queryable<TaktDeviceLog>()
                .Where(x => x.UserId == userId)
                .Select(x => new TaktDeviceLog
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    LastOfflineTime = x.LastOfflineTime,
                    LastOnlineTime = x.LastOnlineTime,
                    UpdateTime = x.UpdateTime,
                    UpdateBy = x.UpdateBy,
                    LastLoginDeviceId = x.LastLoginDeviceId,
                    FirstLoginDeviceId = x.FirstLoginDeviceId,
                    DeviceStatus = x.DeviceStatus
                    // 不查询DeviceInfo大字段，提高查询性能
                })
                .FirstAsync();
            if (deviceLog != null)
            {
                // 更新设备登出相关信息
                deviceLog.LastOfflineTime = now; // 更新最后离线时间
                // 注意：不修改Status字段，Status表示设备是否被管理员停用，不是在线/离线状态
                // 在线/离线状态通过LastOnlineTime和LastOfflineTime来判断
                deviceLog.UpdateTime = now;
                deviceLog.UpdateBy = userId.ToString();

                // 确保字符串长度符合要求
                deviceLog = TaktStringHelper.EnsureEntityStringLength(deviceLog);
                await DeviceLogRepository.UpdateAsync(deviceLog);

                _logger.Info("设备登出日志更新完成: UserId={0}, DeviceId={1}, LastOfflineTime={2}, Status={3}",
                    userId, deviceLog.LastLoginDeviceId ?? deviceLog.FirstLoginDeviceId, now.ToString("yyyy-MM-dd HH:mm:ss"), deviceLog.DeviceStatus);
            }
            else
            {
                _logger.Warn("未找到用户设备记录，无法更新设备登出日志: UserId={0}", userId);
            }
        }
        catch (Exception ex)
        {
            _logger.Error("更新设备登出日志时发生错误: UserId={0}", new[] { userId.ToString() }, ex);
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    private async Task<TaktUser?> GetUserAsync(string username)
    {
        if (string.IsNullOrEmpty(username))
            return null;

        var user = await UserRepository.GetFirstAsync(x => x.UserName == username);
        if (user == null)
            return null;

        return user;
    }





    /// <summary>
    /// 安全地序列化设备指纹信息（压缩存储）
    /// </summary>
    /// <param name="deviceFingerprint">设备指纹信息</param>
    /// <returns>压缩后的JSON字符串</returns>
    private string SerializeDeviceFingerprint(TaktLoginFingerprint deviceFingerprint)
    {
        try
        {
            // 验证输入参数
            if (deviceFingerprint == null)
            {
                _logger.Warn("设备指纹信息为空，返回空JSON");
                return "{}";
            }

            // 创建完整的设备指纹对象，保留所有信息
            var fullFingerprint = new
            {
                // 核心标识
                deviceId = deviceFingerprint.DeviceId,
                deviceType = deviceFingerprint.DeviceType,
                loginFingerprint = deviceFingerprint.LoginFingerprint,

                // 环境信息
                language = deviceFingerprint.Language,
                timezone = deviceFingerprint.Timezone,
                os = deviceFingerprint.Os,
                browser = deviceFingerprint.Browser,
                // 同时提供大小写版本，确保兼容性
                BrowserVersion = deviceFingerprint.Browser,

                // 用户代理信息（重要：SignalR连接服务需要此字段）
                // 同时提供大小写版本，确保兼容性
                userAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "",
                UserAgent = _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? "",

                // 安全信息
                isVpn = deviceFingerprint.IsVpn,
                isProxy = deviceFingerprint.IsProxy,
                networkType = deviceFingerprint.NetworkType,

                // 硬件信息
                screenResolution = deviceFingerprint.ScreenResolution,
                colorDepth = deviceFingerprint.ColorDepth,
                pixelRatio = deviceFingerprint.PixelRatio,
                hardwareConcurrency = deviceFingerprint.HardwareConcurrency,
                deviceMemory = deviceFingerprint.DeviceMemory,
                platform = deviceFingerprint.Platform,

                // 指纹信息
                canvasFingerprint = deviceFingerprint.CanvasFingerprint,
                webglFingerprint = deviceFingerprint.WebglFingerprint,
                audioFingerprint = deviceFingerprint.AudioFingerprint,
                fontsFingerprint = deviceFingerprint.FontsFingerprint,
                pluginsFingerprint = deviceFingerprint.PluginsFingerprint,
                touchSupport = deviceFingerprint.TouchSupport,

                // 其他信息
                cookieEnabled = deviceFingerprint.CookieEnabled,
                doNotTrack = deviceFingerprint.DoNotTrack
            };

            var json = JsonConvert.SerializeObject(fullFingerprint, Formatting.None);

            // 验证JSON是否有效
            if (string.IsNullOrEmpty(json) || json == "null")
            {
                _logger.Warn("设备指纹序列化结果为空");
                return "{}";
            }

            // 进一步压缩：移除不必要的空格和换行
            json = json.Replace(" ", "").Replace("\n", "").Replace("\r", "");

            // 限制JSON长度，确保数据库性能
            if (json.Length > 5000) // 增加到5000字符以内，保留完整信息
            {
                _logger.Warn("设备指纹信息过大，已截断: 原始长度={OriginalLength}", json.Length);
                return json.Substring(0, 5000) + "...";
            }

            return json;
        }
        catch (Exception ex)
        {
            _logger.Error("设备指纹序列化失败: {Error}", ex.Message);
            return "{}"; // 返回空JSON对象
        }
    }

    /// <summary>
    /// 截断字符串到指定长度
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="maxLength">最大长度</param>
    /// <returns>截断后的字符串</returns>
    private string TruncateString(string? input, int maxLength)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        return input.Length > maxLength ? input.Substring(0, maxLength) : input;
    }
}




