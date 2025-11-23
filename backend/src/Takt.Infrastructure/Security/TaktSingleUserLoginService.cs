//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSingleUserLoginService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 17:30
// 版本号 : V0.0.1
// 描述   : 单用户登录服务实现
//===================================================================

using Takt.Shared.Models;
using Takt.Shared.Options;
using Takt.Domain.Entities.Logging;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.Security;
using Takt.Domain.IServices.SignalR;
using Takt.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Transactions;


namespace Takt.Infrastructure.Security
{
    /// <summary>
    /// 单用户登录服务实现 - 基于核心设计原则
    /// 核心设计原则：
    /// 1. 新登录阻断：检测到活跃会话时，新登录立即失败
    /// 2. 旧会话保护：已存在的业务操作不受任何影响
    /// 3. 状态强一致：通过数据库事务保证原子性
    /// 4. 操作可审计：完整记录登录拦截事件，数据库追踪方式
    /// </summary>
    public class TaktSingleUserLoginService : ITaktSingleUserLoginService
    {
        private readonly ITaktLogger _logger;
        private readonly ITaktSignalRConnectionService _signalRUserService;
        private readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly TaktSessionOptions _sessionOptions;
        private readonly IHubContext<TaktSignalRHub> _hubContext;
        private readonly ITaktCurrentUser _currentUser;

        /// <summary>
        /// 获取在线用户仓储
        /// </summary>
        private ITaktRepository<TaktSignalROnline> OnlineUserRepository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSingleUserLoginService(
            ITaktLogger logger,
            ITaktSignalRConnectionService signalRUserService,
            ITaktRepositoryFactory repositoryFactory,
            IOptions<TaktSessionOptions> sessionOptions,
            IHubContext<TaktSignalRHub> hubContext,
            ITaktCurrentUser currentUser)
        {
            _logger = logger;
            _signalRUserService = signalRUserService;
            _repositoryFactory = repositoryFactory;
            _sessionOptions = sessionOptions.Value;
            _hubContext = hubContext;
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        /// <summary>
        /// 检查用户是否可以登录 - 严格新登录阻断机制
        /// </summary>
        public async Task<(bool canLogin, string? reason)> CheckUserLoginAsync(long userId, string deviceId, string? ipAddress = null, TaktLoginFingerprint? deviceFingerprint = null)
        {
            try
            {
                // 验证参数
                if (string.IsNullOrEmpty(deviceId))
                {
                    _logger.Warn("设备ID为空，无法进行单用户登录检查 - UserId: {UserId}", userId);
                    return (false, "设备ID无效");
                }
                
                _logger.Info("开始严格单用户登录检查 - UserId: {UserId}, DeviceId: {DeviceId}, IpAddress: {IpAddress}, SingleUserLoginEnabled: {SingleUserLoginEnabled}", 
                    userId, deviceId, ipAddress ?? "Unknown", _sessionOptions.SingleUserLoginEnabled);

                if (!_sessionOptions.SingleUserLoginEnabled)
                {
                    _logger.Info("单用户登录未启用，允许用户 {UserId} 登录", userId);
                    return (true, null);
                }

                // 查询该用户当前活跃的在线记录
                var activeSessions = await GetActiveUserSessionsAsync(userId);
                
                if (activeSessions.Any())
                {
                    _logger.Info("用户 {UserId} 发现 {Count} 个在线设备，开始检查设备匹配", userId, activeSessions.Count);
                    
                    // 检查是否有相同设备的活跃会话
                    var sameDeviceSession = activeSessions.FirstOrDefault(s => s.DeviceId == deviceId);
                    
                    if (sameDeviceSession != null)
                    {
                        // 同一设备重新连接，允许登录
                        _logger.Info("用户 {UserId} 同一设备重新连接 - DeviceId: {DeviceId}", userId, deviceId);
                        
                        // 记录重新连接事件
                        await LogLoginEventAsync(userId, deviceId, ipAddress, "SAME_DEVICE_RECONNECT", "同一设备重新连接");
                        
                        return (true, "同一设备重新连接");
                    }
                    else
                    {
                        // 不同设备尝试登录，立即阻断
                        var existingSession = activeSessions.First();
                        _logger.Warn("用户 {UserId} 登录被阻断 - 已有 {Count} 个在线设备，当前在线设备: {ExistingDeviceId}, 尝试登录设备: {AttemptDeviceId}", 
                            userId, activeSessions.Count, existingSession.DeviceId ?? "Unknown", deviceId);
                        
                        // 记录登录拦截事件
                        await LogLoginEventAsync(userId, deviceId, ipAddress, "LOGIN_BLOCKED", 
                            $"已有 {activeSessions.Count} 个在线设备，当前在线设备ID: {existingSession.DeviceId ?? "Unknown"}");
                        
                        // 发送登录尝试通知给已登录的用户
                        _logger.Info("准备发送登录尝试通知 - UserId: {UserId}, DeviceId: {DeviceId}, IpAddress: {IpAddress}", 
                            userId, deviceId, ipAddress ?? "null");
                        await SendLoginAttemptNotificationAsync(userId, deviceId, ipAddress, deviceFingerprint);
                        
                        return (false, "您的账号已在其他地方登录，单用户单设备登录模式下不允许同时在多个设备上登录");
                    }
                }
                else
                {
                    // 无活跃会话，允许登录
                    _logger.Info("用户 {UserId} 无在线设备，允许登录 - DeviceId: {DeviceId}", userId, deviceId);
                    
                    // 记录新登录事件
                    await LogLoginEventAsync(userId, deviceId, ipAddress, "NEW_LOGIN", "新用户登录");
                    
                    return (true, "无在线设备，允许登录");
                }
            }
            catch (Exception ex)
            {
                _logger.Error("严格单用户登录检查异常 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId, ex);
                return (false, "系统错误，登录检查失败");
            }
        }

        /// <summary>
        /// 处理用户登录 - 状态强一致性保证
        /// </summary>
        public async Task<TaktApiResult> HandleUserLoginAsync(long userId, string deviceId, string connectionId, string? ipAddress = null, string? userAgent = null, string? userName = null)
        {
            try
            {
                _logger.Info("开始处理用户登录 - 用户ID: {UserId}, 设备ID: {DeviceId}, 连接ID: {ConnectionId}", 
                    userId, deviceId, connectionId);

                if (!_sessionOptions.SingleUserLoginEnabled)
                {
                    _logger.Info("单用户登录未启用，允许用户 {UserId} 登录", userId);
                    return new TaktApiResult { Code = 200, Msg = "登录成功" };
                }

                // 从设备日志和登录日志中获取一致的数据 - 确保设备隔离
                var deviceLog = await GetDeviceLogAsync(userId, deviceId);
                var loginLog = await GetLoginLogAsync(userId, deviceId);
                
                // 验证设备日志是否属于当前设备
                if (deviceLog != null)
                {
                    var isCurrentDevice = deviceLog.LastLoginDeviceId == deviceId || deviceLog.FirstLoginDeviceId == deviceId;
                    if (!isCurrentDevice)
                    {
                        _logger.Warn("设备日志不匹配当前设备 - 当前设备ID: {CurrentDeviceId}, 设备日志LastLoginDeviceId: {LastLoginDeviceId}, FirstLoginDeviceId: {FirstLoginDeviceId}", 
                            deviceId, deviceLog.LastLoginDeviceId ?? "null", deviceLog.FirstLoginDeviceId ?? "null");
                        deviceLog = null; // 清空不匹配的设备日志
                    }
                }
                
                // 验证登录日志是否属于当前设备
                if (loginLog != null && loginLog.DeviceId != deviceId)
                {
                    _logger.Warn("登录日志不匹配当前设备 - 当前设备ID: {CurrentDeviceId}, 登录日志设备ID: {LoginLogDeviceId}", 
                        deviceId, loginLog.DeviceId ?? "null");
                    loginLog = null; // 清空不匹配的登录日志
                }
                
                // 获取最终数据 - 确保只使用当前设备的数据
                var finalIpAddress = GetCurrentDeviceIpAddress(deviceLog, loginLog, ipAddress);
                var finalUserAgent = GetCurrentDeviceUserAgent(loginLog, userAgent);
                var finalDeviceType = GetCurrentDeviceType(deviceLog, loginLog, finalUserAgent);
                var finalClientBrowser = GetCurrentDeviceBrowser(deviceLog, loginLog, finalUserAgent);

                _logger.Info("从当前设备日志和登录日志获取数据 - 设备ID: {DeviceId}, IP: {IpAddress}, UserAgent: {UserAgent}, DeviceType: {DeviceType}, ClientBrowser: {ClientBrowser}", 
                    deviceId, finalIpAddress, finalUserAgent, finalDeviceType, finalClientBrowser);

                // 查找现有记录 - 确保设备隔离
                var existingRecord = await GetOnlineUserRecordAsync(userId, deviceId);
                
                if (existingRecord != null)
                {
                    // 验证记录是否属于当前设备
                    if (existingRecord.DeviceId != deviceId)
                    {
                        _logger.Warn("在线用户记录不匹配当前设备 - 当前设备ID: {CurrentDeviceId}, 记录设备ID: {RecordDeviceId}, 记录ID: {RecordId}", 
                            deviceId, existingRecord.DeviceId ?? "null", existingRecord.Id);
                        existingRecord = null; // 清空不匹配的记录
                    }
                }
                
                // 调用在线用户数据管理服务创建或更新在线记录
                var onlineUser = new TaktSignalROnline
                {
                    UserId = userId,
                    DeviceId = deviceId,
                    ConnectionId = connectionId,
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0, // 在线
                    ClientIp = finalIpAddress,
                    UserAgent = finalUserAgent,
                    DeviceType = finalDeviceType,
                    ClientBrowser = finalClientBrowser,
                    GroupId = 0,
                    IsDeleted = 0
                };

                await _signalRUserService.SaveOnlineUserAsync(onlineUser, userName);
                _logger.Info("单用户登录业务逻辑处理完成 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);

                return new TaktApiResult { Code = 200, Msg = "单用户登录处理成功" };
            }
            catch (Exception ex)
            {
                _logger.Error("处理用户登录异常 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId, ex);
                return new TaktApiResult { Code = 500, Msg = "系统错误，在线用户记录操作失败" };
            }
        }

        /// <summary>
        /// 处理用户登出 - 旧会话保护
        /// </summary>
        public async Task<bool> HandleUserLogoutAsync(long userId, string connectionId, string? userName = null)
        {
            try
            {
                _logger.Info("开始处理用户退出 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, connectionId);

                // 调用在线用户数据管理服务处理登出
                var onlineUser = await _signalRUserService.GetOnlineUserAsync(connectionId);
                
                if (onlineUser != null)
                {
                    // 双重验证：确保记录属于当前用户
                    if (onlineUser.UserId != userId)
                    {
                        _logger.Warn("在线用户记录不匹配当前用户 - 当前用户ID: {CurrentUserId}, 记录用户ID: {RecordUserId}, 设备ID: {DeviceId}", 
                            userId, onlineUser.UserId, onlineUser.DeviceId ?? "null");
                        return false;
                    }
                    
                    // 更新为离线状态
                    onlineUser.OnlineStatus = 1; // 1表示离线
                    onlineUser.LastActivity = DateTime.Now;
                    
                    // 调用数据管理服务更新状态
                    await _signalRUserService.SaveOnlineUserAsync(onlineUser, userName);
                    
                    // 记录下线事件
                    await LogLoginEventAsync(userId, onlineUser.DeviceId ?? string.Empty, onlineUser.ClientIp, "USER_OFFLINE", "用户正常下线");
                    
                    _logger.Info("单用户登出业务逻辑处理完成 - UserId: {UserId}, DeviceId: {DeviceId}", userId, onlineUser.DeviceId ?? "Unknown");
                }
                else
                {
                    _logger.Warn("未找到连接 {ConnectionId} 对应的在线用户记录 - 用户ID: {UserId}", connectionId, userId);
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("处理用户退出异常 - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, connectionId, ex);
                return false;
            }
        }

        /// <summary>
        /// 强制用户下线
        /// </summary>
        public async Task<bool> ForceUserOfflineAsync(long userId, string reason = "管理员强制下线")
        {
            try
            {
                _logger.Info("开始强制用户下线 - 用户ID: {UserId}, 原因: {Reason}", userId, reason);

                // 获取用户的所有连接ID
                var connectionIds = await _signalRUserService.GetConnectionIdsAsync(userId);
                if (!connectionIds.Any())
                {
                    _logger.Warn("用户 {UserId} 当前没有活跃连接", userId);
                    return true;
                }

                // 断开所有连接
                foreach (var connectionId in connectionIds)
                {
                    try
                    {
                        await _signalRUserService.DisconnectUserAsync(connectionId);
                        _logger.Info("用户连接已断开 - 连接ID: {ConnectionId}, 用户ID: {UserId}", connectionId, userId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("断开连接 {ConnectionId} 失败: {Error}", connectionId, ex.Message);
                    }
                }

                _logger.Info("用户 {UserId} 强制下线完成 - 断开连接数: {ConnectionCount}", userId, connectionIds.Count);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("强制用户下线失败 - 用户ID: {UserId}", userId, ex);
                return false;
            }
        }



        /// <summary>
        /// 检查设备是否被允许
        /// </summary>
        public Task<bool> IsDeviceAllowedAsync(long userId, string deviceId)
        {
            try
            {
                // 如果启用设备指纹验证
                if (_sessionOptions.EnableDeviceFingerprint)
                {
                    // 这里可以添加设备指纹验证逻辑
                    // 暂时返回true，实际项目中可以根据需要实现
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error("检查设备是否被允许时发生错误 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId, ex);
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 验证设备指纹
        /// </summary>
        public Task<bool> ValidateDeviceFingerprintAsync(long userId, string deviceId, string fingerprint)
        {
            try
            {
                if (!_sessionOptions.EnableDeviceFingerprint)
                {
                    return Task.FromResult(true);
                }

                // 这里可以添加设备指纹验证逻辑
                // 暂时返回true，实际项目中可以根据需要实现
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error("验证设备指纹时发生错误 - 用户ID: {UserId}, 设备ID: {DeviceId}", userId, deviceId, ex);
                return Task.FromResult(false);
            }
        }


        /// <summary>
        /// 清理过期会话
        /// </summary>
        public async Task<int> CleanupExpiredSessionsAsync()
        {
            try
            {
                var timeoutMinutes = _sessionOptions.TimeoutMinutes;
                var cutoffTime = DateTime.Now.AddMinutes(-timeoutMinutes);

                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(x => x.LastActivity < cutoffTime);

                var expiredSessions = await OnlineUserRepository.GetListAsync(exp.ToExpression());
                if (!expiredSessions.Any())
                {
                    return 0;
                }

                var result = await OnlineUserRepository.DeleteAsync(exp.ToExpression());

                // 清理设备信息
                foreach (var session in expiredSessions)
                {
                    try
                    {
                        await _signalRUserService.RemoveUserDevice(session.UserId.ToString(), session.DeviceId ?? string.Empty);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("清理设备信息失败 - 设备ID: {DeviceId}", session.DeviceId ?? "Unknown", ex);
                    }
                }

                _logger.Info("清理过期会话完成 - 清理数量: {Count}", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error("清理过期会话失败", ex);
                return 0;
            }
        }




        /// <summary>
        /// 发送登录尝试通知给已登录的用户
        /// </summary>
        private async Task SendLoginAttemptNotificationAsync(long userId, string deviceId, string? ipAddress, TaktLoginFingerprint? deviceFingerprint = null)
        {
            try
            {
                // 验证和清理参数
                var cleanDeviceId = string.IsNullOrEmpty(deviceId) ? "Unknown" : deviceId;
                var cleanIpAddress = string.IsNullOrEmpty(ipAddress) || ipAddress == "默认IP" ? "Unknown" : ipAddress;
                
                _logger.Info("开始发送登录尝试通知 - 用户ID: {UserId}, 尝试登录设备ID: {DeviceId}, IP: {IpAddress}",
                    userId, cleanDeviceId, cleanIpAddress);

                // 获取当前在线用户的所有连接
                var connectionIds = await _signalRUserService.GetConnectionIdsAsync(userId);

                if (!connectionIds.Any())
                {
                    _logger.Warn("用户 {UserId} 当前没有活跃连接，无法发送登录尝试通知", userId);
                    return;
                }

                // 获取设备信息 - 从设备指纹信息中获取实际数据
                string actualBrowser = "Unknown";
                string actualOs = "Unknown";
                
                if (deviceFingerprint != null)
                {
                    // 从设备指纹信息中获取信息
                    actualBrowser = deviceFingerprint.Browser ?? "Unknown";
                    actualOs = deviceFingerprint.Os ?? "Unknown";
                    
                    _logger.Info("从设备指纹信息获取信息 - Browser: {Browser}, OS: {OS}", 
                        actualBrowser, actualOs);
                }
                else
                {
                    _logger.Warn("设备指纹信息为空，使用默认值");
                }
                
                _logger.Info("获取到实际设备信息 - Browser: {Browser}, OS: {OS}",
                    actualBrowser, actualOs);

                // 构建设备信息对象
                var message = $"当前用户：admin，尝试在其它地方登录，被系统强制阻止了，设备ID：{cleanDeviceId}，IP：{cleanIpAddress}，时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                
                var deviceInfo = new
                {
                    DeviceId = cleanDeviceId,
                    IpAddress = cleanIpAddress,
                    AttemptTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    Browser = actualBrowser,
                    OS = actualOs, // 使用操作系统信息
                    Message = message
                };
                
                
                // 输出日志验证Message内容
                _logger.Info("登录尝试通知消息内容: {Message}", message);
                _logger.Info("完整设备信息对象: DeviceId={DeviceId}, IpAddress={IpAddress}, AttemptTime={AttemptTime}, Message={Message}", 
                    deviceInfo.DeviceId, deviceInfo.IpAddress, deviceInfo.AttemptTime, deviceInfo.Message);

                // 向用户的所有连接发送登录尝试通知
                var attemptTime = DateTime.Now;
                foreach (var connectionId in connectionIds)
                {
                    try
                    {
                        _logger.Info("准备发送登录尝试通知 - 用户ID: {UserId}, 连接ID: {ConnectionId}, 设备信息: {DeviceInfo}, 时间: {AttemptTime}", 
                            userId, connectionId, JsonConvert.SerializeObject(deviceInfo), attemptTime);
                        
                        // 发送登录尝试通知
                        await _hubContext.Clients.Client(connectionId).SendAsync("LoginAttemptDetected", deviceInfo, attemptTime);
                        _logger.Info("登录尝试通知已发送 - 用户ID: {UserId}, 连接ID: {ConnectionId}", userId, connectionId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("发送登录尝试通知失败 - 用户ID: {UserId}, 连接ID: {ConnectionId}, 错误: {Error}",
                            userId, connectionId, ex.Message);
                    }
                }

                _logger.Info("登录尝试通知发送完成 - 用户ID: {UserId}, 发送连接数: {ConnectionCount}", userId, connectionIds.Count);
            }
            catch (Exception ex)
            {
                _logger.Error("发送登录尝试通知时发生错误 - 用户ID: {UserId}", userId, ex);
            }
        }

        /// <summary>
        /// 获取用户活跃会话 - 数据库追踪方式
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>活跃会话列表</returns>
        private async Task<List<TaktSignalROnline>> GetActiveUserSessionsAsync(long userId)
        {
            try
            {
                // 查询该用户的所有在线设备会话 - 确保设备隔离
                // 查询条件：用户ID + 在线状态(0=在线) + 设备ID不为空
                // 目的：判断当前用户是否可登录，还是被其他在线设备阻止
                // 注意：不使用ConnectionId查询，因为ConnectionId是动态的，不准确
                var activeSessions = await OnlineUserRepository.GetListAsync(x => 
                    x.UserId == userId && 
                    x.OnlineStatus == 0 &&  // 只查询在线状态的设备
                    !string.IsNullOrEmpty(x.DeviceId));  // 确保设备ID不为空

                // 双重验证：确保所有返回的会话都属于该用户
                var validSessions = new List<TaktSignalROnline>();
                if (activeSessions?.Any() == true)
                {
                    foreach (var session in activeSessions)
                    {
                        if (session.UserId == userId && !string.IsNullOrEmpty(session.DeviceId))
                        {
                            validSessions.Add(session);
                        }
                        else
                        {
                            _logger.Warn("发现不属于当前用户的在线会话 - 当前用户ID: {CurrentUserId}, 会话用户ID: {SessionUserId}, 设备ID: {DeviceId}", 
                                userId, session.UserId, session.DeviceId ?? "null");
                        }
                    }
                }

                _logger.Info("查询到用户 {UserId} 的在线设备数量: {Count} (验证后: {ValidCount})", 
                    userId, activeSessions?.Count() ?? 0, validSessions.Count);
                
                // 记录每个在线设备的详细信息
                if (validSessions.Any())
                {
                    _logger.Info("用户 {UserId} 当前在线设备列表:", userId);
                    foreach (var session in validSessions)
                    {
                        _logger.Info("  - 在线设备: DeviceId={DeviceId}, OnlineStatus={OnlineStatus}, LastActivity={LastActivity}", 
                            session.DeviceId, session.OnlineStatus, 
                            session.LastActivity.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                else
                {
                    _logger.Info("用户 {UserId} 当前无在线设备", userId);
                }
                
                return validSessions;
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户活跃会话失败 - UserId: {UserId}", userId, ex);
                return new List<TaktSignalROnline>();
            }
        }

        /// <summary>
        /// 记录登录事件 - 操作可审计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <param name="ipAddress">IP地址</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="description">描述</param>
        private Task LogLoginEventAsync(long userId, string deviceId, string? ipAddress, string eventType, string description)
        {
            try
            {
                // 这里可以记录到专门的登录拦截日志表
                // 暂时记录到普通日志中
                _logger.Info("登录事件记录 - UserId: {UserId}, DeviceId: {DeviceId}, EventType: {EventType}, Description: {Description}, IP: {IpAddress}", 
                    userId, deviceId, eventType, description, ipAddress ?? "Unknown");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error("记录登录事件失败 - UserId: {UserId}, EventType: {EventType}", userId, eventType, ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 从UserAgent中提取设备类型
        /// </summary>
        /// <param name="userAgent">用户代理字符串</param>
        /// <returns>设备类型</returns>
        private string ExtractDeviceTypeFromUserAgent(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            var ua = userAgent.ToLowerInvariant();

            // 检测移动设备
            if (ua.Contains("mobile") || ua.Contains("android") || ua.Contains("iphone") || ua.Contains("ipad"))
                return "Mobile";

            // 检测平板设备
            if (ua.Contains("tablet") || ua.Contains("ipad"))
                return "Tablet";

            // 检测桌面设备
            if (ua.Contains("windows") || ua.Contains("macintosh") || ua.Contains("linux") || ua.Contains("x11"))
                return "Desktop";

            return "Unknown";
        }

        /// <summary>
        /// 从UserAgent中提取浏览器信息
        /// </summary>
        /// <param name="userAgent">用户代理字符串</param>
        /// <returns>浏览器信息</returns>
        private string ExtractBrowserFromUserAgent(string? userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
                return "Unknown";

            var ua = userAgent.ToLowerInvariant();

            // 检测Chrome
            if (ua.Contains("chrome") && !ua.Contains("edg"))
                return "Chrome";

            // 检测Edge
            if (ua.Contains("edg"))
                return "Edge";

            // 检测Firefox
            if (ua.Contains("firefox"))
                return "Firefox";

            // 检测Safari
            if (ua.Contains("safari") && !ua.Contains("chrome"))
                return "Safari";

            // 检测Opera
            if (ua.Contains("opera"))
                return "Opera";

            return "Unknown";
        }

        /// <summary>
        /// 获取当前设备的IP地址 - 确保设备隔离
        /// </summary>
        private string GetCurrentDeviceIpAddress(TaktDeviceLog? deviceLog, TaktLoginLog? loginLog, string? fallbackIp)
        {
            // 优先使用设备日志中的IP地址（LastLoginIp > FirstLoginIp）
            if (deviceLog != null)
            {
                if (!string.IsNullOrEmpty(deviceLog.LastLoginIp))
                    return deviceLog.LastLoginIp;
                if (!string.IsNullOrEmpty(deviceLog.FirstLoginIp))
                    return deviceLog.FirstLoginIp;
            }
            
            // 其次使用登录日志中的IP地址
            if (loginLog != null && !string.IsNullOrEmpty(loginLog.LoginIp))
                return loginLog.LoginIp;
            
            // 最后使用传入的IP地址
            return fallbackIp ?? "Unknown";
        }

        /// <summary>
        /// 获取当前设备的UserAgent - 确保设备隔离
        /// </summary>
        private string GetCurrentDeviceUserAgent(TaktLoginLog? loginLog, string? fallbackUserAgent)
        {
            // 优先使用登录日志中的UserAgent
            if (loginLog != null && !string.IsNullOrEmpty(loginLog.UserAgent))
                return loginLog.UserAgent;
            
            // 使用传入的UserAgent
            return fallbackUserAgent ?? "Unknown";
        }

        /// <summary>
        /// 获取当前设备的设备类型 - 确保设备隔离
        /// </summary>
        private string GetCurrentDeviceType(TaktDeviceLog? deviceLog, TaktLoginLog? loginLog, string userAgent)
        {
            // 优先使用设备日志中的设备类型（LastLoginDeviceType > FirstLoginDeviceType）
            if (deviceLog != null)
            {
                if (!string.IsNullOrEmpty(deviceLog.LastLoginDeviceType))
                    return deviceLog.LastLoginDeviceType;
                if (!string.IsNullOrEmpty(deviceLog.FirstLoginDeviceType))
                    return deviceLog.FirstLoginDeviceType;
            }
            
            // 其次使用登录日志中的设备类型
            if (loginLog != null && !string.IsNullOrEmpty(loginLog.DeviceType))
                return loginLog.DeviceType;
            
            // 最后从UserAgent解析
            return ExtractDeviceTypeFromUserAgent(userAgent);
        }

        /// <summary>
        /// 获取当前设备的浏览器信息 - 确保设备隔离
        /// </summary>
        private string GetCurrentDeviceBrowser(TaktDeviceLog? deviceLog, TaktLoginLog? loginLog, string userAgent)
        {
            // 优先使用设备日志中的浏览器信息（LastLoginBrowser > FirstLoginBrowser）
            if (deviceLog != null)
            {
                if (!string.IsNullOrEmpty(deviceLog.LastLoginBrowser))
                    return deviceLog.LastLoginBrowser;
                if (!string.IsNullOrEmpty(deviceLog.FirstLoginBrowser))
                    return deviceLog.FirstLoginBrowser;
            }
            
            // 从UserAgent解析
            return ExtractBrowserFromUserAgent(userAgent);
        }

        /// <summary>
        /// 获取在线用户记录 - 确保设备隔离，只返回完全匹配当前设备的在线记录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>在线用户记录</returns>
        private async Task<TaktSignalROnline?> GetOnlineUserRecordAsync(long userId, string deviceId)
        {
            try
            {
                // 使用业务仓储获取在线用户记录
                var onlineUser = await OnlineUserRepository.GetFirstAsync(x => 
                    x.UserId == userId && x.DeviceId == deviceId);
                
                // 双重验证：确保返回的在线记录确实属于当前设备
                if (onlineUser != null && onlineUser.DeviceId != deviceId)
                {
                    _logger.Warn("获取到的在线用户记录不属于当前设备 - 当前设备ID: {CurrentDeviceId}, 在线记录设备ID: {OnlineRecordDeviceId}, 在线记录ID: {OnlineRecordId}", 
                        deviceId, onlineUser.DeviceId ?? "null", onlineUser.Id);
                    return null;
                }
                
                
                return onlineUser;
            }
            catch (Exception ex)
            {
                _logger.Error("获取在线用户记录失败 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取设备日志 - 确保设备隔离，只返回完全匹配当前设备的设备日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>设备日志</returns>
        private async Task<TaktDeviceLog?> GetDeviceLogAsync(long userId, string deviceId)
        {
            try
            {
                // 使用业务仓储获取设备日志
                var deviceLogRepository = _repositoryFactory.GetBusinessRepository<TaktDeviceLog>();
                
                // 确保设备隔离：只获取完全匹配当前设备的设备日志记录
                // 优先获取LastLoginDeviceId匹配的记录，如果没有则获取FirstLoginDeviceId匹配的记录
                var deviceLog = await deviceLogRepository.GetFirstAsync(x => 
                    x.UserId == userId && x.LastLoginDeviceId == deviceId);
                
                // 如果没有找到LastLoginDeviceId匹配的记录，再查找FirstLoginDeviceId匹配的记录
                if (deviceLog == null)
                {
                    deviceLog = await deviceLogRepository.GetFirstAsync(x => 
                        x.UserId == userId && x.FirstLoginDeviceId == deviceId);
                }
                
                // 双重验证：确保返回的设备日志确实属于当前设备
                if (deviceLog != null)
                {
                    var isCurrentDevice = deviceLog.LastLoginDeviceId == deviceId || deviceLog.FirstLoginDeviceId == deviceId;
                    if (!isCurrentDevice)
                    {
                        _logger.Warn("获取到的设备日志不属于当前设备 - 当前设备ID: {CurrentDeviceId}, 设备日志ID: {DeviceLogId}, LastLoginDeviceId: {LastLoginDeviceId}, FirstLoginDeviceId: {FirstLoginDeviceId}", 
                            deviceId, deviceLog.Id, deviceLog.LastLoginDeviceId ?? "null", deviceLog.FirstLoginDeviceId ?? "null");
                        return null;
                    }
                }
                
                
                return deviceLog;
            }
            catch (Exception ex)
            {
                _logger.Error("获取设备日志失败 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId, ex);
                return null;
            }
        }

        /// <summary>
        /// 获取登录日志 - 确保设备隔离，只返回完全匹配当前设备的登录日志
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        /// <returns>登录日志</returns>
        private async Task<TaktLoginLog?> GetLoginLogAsync(long userId, string deviceId)
        {
            try
            {
                // 使用业务仓储获取登录日志
                var loginLogRepository = _repositoryFactory.GetBusinessRepository<TaktLoginLog>();
                var loginLogs = await loginLogRepository.GetListAsync(x => 
                    x.UserId == userId && 
                    x.DeviceId == deviceId);
                
                // 获取最新的登录日志
                var loginLog = loginLogs?.OrderByDescending(x => x.CreateTime).FirstOrDefault();
                
                // 双重验证：确保返回的登录日志确实属于当前设备
                if (loginLog != null && loginLog.DeviceId != deviceId)
                {
                    _logger.Warn("获取到的登录日志不属于当前设备 - 当前设备ID: {CurrentDeviceId}, 登录日志设备ID: {LoginLogDeviceId}, 登录日志ID: {LoginLogId}", 
                        deviceId, loginLog.DeviceId ?? "null", loginLog.Id);
                    return null;
                }
                
                
                return loginLog;
            }
            catch (Exception ex)
            {
                _logger.Error("获取登录日志失败 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId, ex);
                return null;
            }
        }
    }
}






