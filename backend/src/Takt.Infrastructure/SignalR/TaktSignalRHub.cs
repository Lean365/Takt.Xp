//===================================================================
// 项目名 : Takt.Infrastructure
// 文件名 : TaktSignalRHub.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : SignalR集线器 - 专注于连接生命周期管理
//===================================================================

using Takt.Shared.Enums;
using Takt.Shared.Models;
using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.Entities.Logging;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.SignalR;
using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Takt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR集线器 - 专注于连接生命周期管理和基础消息路由
    /// </summary>
    public class TaktSignalRHub : Hub<ITaktSignalRClient>, ITaktSignalRHub
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        private readonly ITaktSignalRConnectionService _userService;
        private readonly ITaktSingleUserLoginService _singleUserLoginService;
        private readonly IOptions<TaktSessionOptions> _sessionOptions;
        private readonly ITaktCurrentUser _currentUser;

        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        private ITaktRepository<TaktSignalROnline> Repository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSignalRHub(
            ITaktLogger logger,
            ITaktSignalRConnectionService userService,
            ITaktSingleUserLoginService singleUserLoginService,
            IOptions<TaktSessionOptions> sessionOptions,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser)
        {
            _logger = logger;
            _userService = userService;
            _singleUserLoginService = singleUserLoginService;
            _sessionOptions = sessionOptions;
            _repositoryFactory = repositoryFactory;
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        /// <summary>
        /// 客户端连接 - 基于数据库追踪的在线用户管理
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            try
            {
                _logger.Info("新连接建立 - 连接ID: {ConnectionId}", Context.ConnectionId);

                // 获取设备信息
                var httpContext = Context.GetHttpContext();
                if (httpContext == null)
                {
                    _logger.Warn("无法获取HTTP上下文");
                    return;
                }

                // 验证用户身份
                var userId = await ValidateUserTokenAsync(httpContext);
                if (userId <= 0)
                {
                    _logger.Warn("用户身份验证失败");
                    Context.Abort();
                    return;
                }

                // 获取设备信息
                var deviceInfo = await GetDeviceInfoAsync(userId, httpContext);
                if (deviceInfo == null)
                {
                    _logger.Error("无法获取设备信息 - 用户ID: {UserId}", userId);
                    Context.Abort();
                    return;
                }

                var deviceId = deviceInfo.LastLoginDeviceId ?? deviceInfo.FirstLoginDeviceId;
                _logger.Info("用户 {UserId} 连接 - 设备ID: {DeviceId}", userId, deviceId);

                // 单用户登录检查由业务层处理，这里只处理连接技术细节

                // 设置连接上下文
                Context.Items["UserId"] = userId;
                Context.Items["DeviceId"] = deviceId;
                Context.Items["DeviceInfo"] = deviceInfo;

                // 添加到用户组
                await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());

                // 更新数据库中的在线状态
                await UpdateOnlineStatusAsync(userId, deviceId, Context.ConnectionId, httpContext);

                // 发送用户上线通知
                await SendUserStatusNotificationAsync(userId, true);

                _logger.Info("用户 {UserId} 连接成功 - 设备ID: {DeviceId}, 连接ID: {ConnectionId}",
                    userId, deviceId, Context.ConnectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("连接处理失败", ex);
                Context.Abort();
            }
        }

        /// <summary>
        /// 客户端断开连接 - 基于数据库追踪的离线处理
        /// </summary>
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var userId = Context.Items.ContainsKey("UserId") ? (long)Context.Items["UserId"] : 0;

                _logger.Info("连接断开 - 连接ID: {ConnectionId}, 用户ID: {UserId}", connectionId, userId);

                if (userId > 0)
                {
                    // 从用户组移除
                    await Groups.RemoveFromGroupAsync(connectionId, userId.ToString());

                    // 更新数据库中的离线状态
                    await SetOfflineStatusAsync(connectionId, userId);

                    // 发送用户下线通知
                    await SendUserStatusNotificationAsync(userId, false);
                }

                _logger.Info("连接断开处理完成 - 连接ID: {ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("断开连接处理失败", ex);
            }
        }

        /// <summary>
        /// 接收心跳 - 基于数据库追踪的心跳更新
        /// </summary>
        public async Task ReceiveHeartbeat()
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var userId = Context.Items.ContainsKey("UserId") ? (long)Context.Items["UserId"] : 0;

                if (userId > 0)
                {
                    // 更新数据库中的心跳时间
                    await UpdateHeartbeatAsync(connectionId, userId);

                    // 发送心跳响应
                    await Clients.Caller.ReceiveHeartbeat(DateTime.Now);
                }
                else
                {
                    _logger.Warn("心跳处理失败 - 未找到用户ID, 连接ID: {ConnectionId}", connectionId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("心跳处理失败", ex);
            }
        }

        /// <summary>
        /// 更新心跳时间
        /// </summary>
        private async Task UpdateHeartbeatAsync(string connectionId, long userId)
        {
            try
            {
                var onlineUser = await Repository.GetFirstAsync(x => x.ConnectionId == connectionId);
                if (onlineUser != null)
                {
                    onlineUser.LastHeartbeat = DateTime.Now;
                    onlineUser.LastActivity = DateTime.Now;
                    onlineUser.UpdateTime = DateTime.Now;
                    // 审计字段由 TaktSignalRRepository 统一处理

                    await Repository.UpdateAsync(onlineUser);
                }
                else
                {
                    _logger.Warn("未找到连接 {ConnectionId} 对应的在线用户记录", connectionId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("更新心跳失败: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, userId, ex);
            }
        }

        /// <summary>
        /// 验证用户令牌并获取用户ID
        /// </summary>
        private Task<long> ValidateUserTokenAsync(HttpContext httpContext)
        {
            try
            {
                // 获取访问令牌
                var token = string.Empty;

                // 1. 从查询参数获取 (access_token)
                token = httpContext.Request.Query["access_token"].ToString();

                // 2. 如果查询参数中没有，尝试从请求头获取
                if (string.IsNullOrEmpty(token))
                {
                    token = httpContext.Request.Headers["Authorization"].ToString();
                    if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer "))
                    {
                        token = token.Substring("Bearer ".Length);
                    }
                }

                // 3. 如果还是没有，尝试从自定义头获取
                if (string.IsNullOrEmpty(token))
                {
                    token = httpContext.Request.Headers["X-Access-Token"].ToString();
                }

                if (string.IsNullOrEmpty(token))
                {
                    _logger.Warn("连接缺少访问令牌");
                    return Task.FromResult(0L);
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                if (!tokenHandler.CanReadToken(token))
                {
                    _logger.Warn("无效的访问令牌格式");
                    return Task.FromResult(0L);
                }

                var jwtToken = tokenHandler.ReadJwtToken(token);
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "uid");
                if (userIdClaim == null || !long.TryParse(userIdClaim.Value, out var userId))
                {
                    _logger.Warn("令牌中缺少用户ID");
                    return Task.FromResult(0L);
                }

                // 关键修复：手动设置 HTTP 上下文的用户身份
                // 这样 TaktCurrentUser 就能正确获取到用户信息
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims, "jwt");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                httpContext.User = claimsPrincipal;

                _logger.Info("SignalR 用户身份设置成功: UserId={UserId}, UserName={UserName}", 
                    userId, jwtToken.Claims.FirstOrDefault(c => c.Type == "unm")?.Value ?? "Unknown");

                return Task.FromResult(userId);
            }
            catch (Exception ex)
            {
                _logger.Error("验证用户令牌失败", ex);
                return Task.FromResult(0L);
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        private async Task<TaktDeviceLog?> GetDeviceInfoAsync(long userId, HttpContext httpContext)
        {
            try
            {
                // 从设备日志获取最新的设备记录
                var deviceLogRepository = _repositoryFactory.GetAuthRepository<TaktDeviceLog>();
                var deviceLog = await deviceLogRepository.SqlSugarClient
                    .Queryable<TaktDeviceLog>()
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.LastLoginTime)
                    .FirstAsync();

                if (deviceLog == null)
                {
                    _logger.Error("未找到用户 {UserId} 的设备记录", userId);
                    return null;
                }

                if (string.IsNullOrEmpty(deviceLog.LastLoginDeviceId) && string.IsNullOrEmpty(deviceLog.FirstLoginDeviceId))
                {
                    _logger.Error("用户 {UserId} 的设备ID为空", userId);
                    return null;
                }

                _logger.Info("获取设备信息成功: UserId={UserId}, DeviceId={DeviceId}", 
                    userId, deviceLog.LastLoginDeviceId ?? deviceLog.FirstLoginDeviceId);

                return deviceLog;
            }
            catch (Exception ex)
            {
                _logger.Error("获取设备信息失败: UserId={UserId}", userId, ex);
                return null;
            }
        }


        /// <summary>
        /// 更新数据库中的在线状态
        /// </summary>
        private async Task UpdateOnlineStatusAsync(long userId, string deviceId, string connectionId, HttpContext httpContext)
        {
            try
            {
                _logger.Info("更新在线状态: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}", 
                    userId, deviceId, connectionId);

                // 使用统一的在线日志管理服务
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString() ?? "Unknown";
                var onlineUser = new TaktSignalROnline
                {
                    UserId = userId,
                    DeviceId = deviceId,
                    ConnectionId = connectionId,
                    ClientIp = TaktIpUtils.GetClientIpAddress(httpContext),
                    UserAgent = userAgent,
                    DeviceType = ExtractDeviceTypeFromUserAgent(userAgent),
                    ClientBrowser = ExtractBrowserFromUserAgent(userAgent),
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0, // 0表示在线状态
                    GroupId = 0,
                    IsDeleted = 0
                };

                // 调用统一的在线日志管理服务进行创建或更新
                var userName = Context.User?.Identity?.Name ?? "SignalR";
                await _userService.SaveOnlineUserAsync(onlineUser, userName);
                _logger.Info("在线状态操作成功: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}", 
                    userId, deviceId, connectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("更新在线状态失败: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}", 
                    userId, deviceId, connectionId, ex);
            }
        }

        /// <summary>
        /// 设置离线状态
        /// </summary>
        private async Task SetOfflineStatusAsync(string connectionId, long userId)
        {
            try
            {
                _logger.Info("设置离线状态: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, userId);

                // 查找该连接对应的在线用户记录
                var onlineUser = await Repository.GetFirstAsync(x => x.ConnectionId == connectionId);
                if (onlineUser != null)
                {
                    // 更新为离线状态
                    onlineUser.OnlineStatus = 1; // 1表示离线
                    onlineUser.LastActivity = DateTime.Now;

                    // 调用统一的在线日志管理服务进行更新
                    var userName = Context.User?.Identity?.Name ?? "SignalR";
                    await _userService.SaveOnlineUserAsync(onlineUser, userName);
                    _logger.Info("离线状态设置成功: UserId={UserId}, DeviceId={DeviceId}", 
                        onlineUser.UserId, onlineUser.DeviceId);
                }
                else
                {
                    _logger.Warn("未找到连接 {ConnectionId} 对应的在线用户记录", connectionId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("设置离线状态失败: ConnectionId={ConnectionId}, UserId={UserId}", 
                    connectionId, userId, ex);
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
        /// 发送消息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="content">消息内容</param>
        /// <returns></returns>
        public async Task SendMessageAsync(string userId, string content)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                var currentUserId = Context.Items.ContainsKey("UserId") ? (long)Context.Items["UserId"] : 0;

                if (currentUserId <= 0)
                {
                    _logger.Warn("发送消息失败 - 未找到当前用户ID, 连接ID: {ConnectionId}", connectionId);
                    return;
                }

                _logger.Info("收到用户 {CurrentUserId} 发送给用户 {TargetUserId} 的消息: {Content}", currentUserId, userId, content);

                // 1. 保存消息到数据库
                var messageEntity = new TaktSignalRMessage
                {
                    SenderId = currentUserId,
                    ReceiverId = long.TryParse(userId, out var targetUserId) ? targetUserId : null,
                    Content = content ?? string.Empty,
                    MessageType = (int)TaktMessageType.User,
                    IsRead = 0, // 0表示未读
                    CreateBy = currentUserId.ToString(),
                    CreateTime = DateTime.Now,
                    UpdateBy = currentUserId.ToString(),
                    UpdateTime = DateTime.Now,
                    IsDeleted = 0
                };

                try
                {
                    var messageRepository = _repositoryFactory.GetBusinessRepository<TaktSignalRMessage>();
                    var messageId = await messageRepository.CreateAsync(messageEntity);
                    _logger.Info("消息已保存到数据库，消息ID: {MessageId}", messageId);
                }
                catch (Exception ex)
                {
                    _logger.Error("保存消息到数据库失败", ex);
                }

                // 2. 转发给目标用户
                if (long.TryParse(userId, out var receiverId) && receiverId > 0)
                {
                    try
                    {
                        var targetNotification = new TaktRealTimeNotification
                        {
                            Type = TaktMessageType.User,
                            Title = "新消息",
                            Content = content,
                            Timestamp = DateTime.Now,
                            Data = new { messageId = messageEntity.Id, senderId = currentUserId }
                        };

                        await Clients.User(receiverId.ToString()).ReceivePersonalNotice(targetNotification);
                        _logger.Info("消息已转发给目标用户: {ReceiverId}", receiverId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("转发消息给目标用户失败: {Error}", ex.Message);
                    }
                }

                // 3. 发送系统通知
                try
                {
                    var systemNotification = new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = "消息发送状态",
                        Content = $"消息已发送给用户 {userId}",
                        Timestamp = DateTime.Now,
                        Data = new { messageId = messageEntity.Id, targetUserId = userId }
                    };

                    await Clients.Others.ReceiveBroadcast(systemNotification);
                    _logger.Info("系统通知已发送");
                }
                catch (Exception ex)
                {
                    _logger.Warn("发送系统通知失败: {Error}", ex.Message);
                }

                // 发送确认消息给发送者
                await Clients.Caller.ReceiveMessage(new
                {
                    Type = "System",
                    Title = "消息发送成功",
                    Content = $"您的消息已发送给用户 {userId}: {content}",
                    Timestamp = DateTime.Now,
                    Data = new { senderId = currentUserId, targetUserId = userId }
                });

                _logger.Info("消息处理完成 - 发送者ID: {CurrentUserId}, 接收者ID: {TargetUserId}, 消息: {Content}",
                    currentUserId, userId, content);
            }
            catch (Exception ex)
            {
                _logger.Error("发送消息处理失败", ex);
            }
        }

        /// <summary>
        /// 加入群组
        /// </summary>
        public async Task JoinGroup(string groupName)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                await Groups.AddToGroupAsync(connectionId, groupName);
                await Clients.Caller.JoinedGroup(connectionId, groupName);
                _logger.Info("用户加入群组 - 连接ID: {ConnectionId}, 群组: {GroupName}", connectionId, groupName);
            }
            catch (Exception ex)
            {
                _logger.Error("加入群组失败", ex);
            }
        }

        /// <summary>
        /// 离开群组
        /// </summary>
        public async Task LeaveGroup(string groupName)
        {
            try
            {
                var connectionId = Context.ConnectionId;
                await Groups.RemoveFromGroupAsync(connectionId, groupName);
                await Clients.Caller.LeftGroup(connectionId, groupName);
                _logger.Info("用户离开群组 - 连接ID: {ConnectionId}, 群组: {GroupName}", connectionId, groupName);
            }
            catch (Exception ex)
            {
                _logger.Error("离开群组失败", ex);
            }
        }

        /// <summary>
        /// 发送用户状态变更通知
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="isOnline">是否上线</param>
        private async Task SendUserStatusNotificationAsync(long userId, bool isOnline)
        {
            try
            {
                if (isOnline)
                {
                    // 发送上线通知给所有在线用户（除了自己）
                    await Clients.Others.UserOnline(userId);
                    _logger.Info("用户上线通知已发送 - 用户ID: {UserId}", userId);
                }
                else
                {
                    // 发送下线通知给所有在线用户（除了自己）
                    await Clients.Others.UserOffline(userId);
                    _logger.Info("用户下线通知已发送 - 用户ID: {UserId}", userId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("发送用户状态通知失败 - 用户ID: {UserId}, 状态: {Status}", userId, isOnline ? "上线" : "下线", ex);
            }
        }

        /// <summary>
        /// 断开客户端连接
        /// </summary>
        public async Task DisconnectClientAsync(string connectionId)
        {
            try
            {
                await _userService.DisconnectUserAsync(connectionId);
                _logger.Info("客户端连接已断开 - 连接ID: {ConnectionId}", connectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("断开客户端连接失败", ex);
            }
        }

        /// <summary>
        /// 强制用户下线
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="reason">下线原因</param>
        public async Task ForceUserOffline(long userId, string reason = "管理员强制下线")
        {
            try
            {
                _logger.Info("开始强制用户下线 - 用户ID: {UserId}, 原因: {Reason}", userId, reason);

                // 获取用户的所有连接ID
                var connectionIds = await _userService.GetConnectionIdsAsync(userId);
                if (!connectionIds.Any())
                {
                    _logger.Warn("用户 {UserId} 当前没有活跃连接", userId);
                    return;
                }

                // 向所有连接发送强制下线通知
                foreach (var connectionId in connectionIds)
                {
                    try
                    {
                        await Clients.Client(connectionId).ForceOffline(reason);
                        _logger.Info("强制下线通知已发送 - 连接ID: {ConnectionId}, 用户ID: {UserId}", connectionId, userId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("向连接 {ConnectionId} 发送强制下线通知失败: {Error}", connectionId, ex.Message);
                    }
                }

                // 断开所有连接
                foreach (var connectionId in connectionIds)
                {
                    try
                    {
                        await _userService.DisconnectUserAsync(connectionId);
                        _logger.Info("用户连接已断开 - 连接ID: {ConnectionId}, 用户ID: {UserId}", connectionId, userId);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn("断开连接 {ConnectionId} 失败: {Error}", connectionId, ex.Message);
                    }
                }

                // 发送用户下线通知给其他用户
                await SendUserStatusNotificationAsync(userId, false);

                // 发送系统广播通知（可选）
                try
                {
                    var systemNotification = new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = "用户强制下线",
                        Content = $"用户 {userId} 已被管理员强制下线，原因：{reason}",
                        Timestamp = DateTime.Now
                    };
                    await Clients.Others.ReceiveBroadcast(systemNotification);
                    _logger.Info("系统广播通知已发送 - 用户ID: {UserId}", userId);
                }
                catch (Exception ex)
                {
                    _logger.Warn("发送系统广播通知失败: {Error}", ex.Message);
                }

                _logger.Info("用户强制下线完成 - 用户ID: {UserId}, 断开连接数: {ConnectionCount}", userId, connectionIds.Count);
            }
            catch (Exception ex)
            {
                _logger.Error("强制用户下线失败 - 用户ID: {UserId}", userId, ex);
                throw;
            }
        }

        /// <summary>
        /// 批量强制用户下线
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <param name="reason">下线原因</param>
        public async Task ForceUsersOffline(List<long> userIds, string reason = "管理员批量强制下线")
        {
            try
            {
                _logger.Info("开始批量强制用户下线 - 用户数量: {UserCount}, 原因: {Reason}", userIds.Count, reason);

                var results = new List<(long userId, bool success, string message)>();

                foreach (var userId in userIds)
                {
                    try
                    {
                        await ForceUserOffline(userId, reason);
                        results.Add((userId, true, "强制下线成功"));
                    }
                    catch (Exception ex)
                    {
                        var message = $"强制下线失败: {ex.Message}";
                        results.Add((userId, false, message));
                        _logger.Error("用户 {UserId} 强制下线失败: {Error}", userId, ex.Message);
                    }
                }

                var successCount = results.Count(r => r.success);
                var failCount = results.Count(r => !r.success);

                _logger.Info("批量强制下线完成 - 成功: {SuccessCount}, 失败: {FailCount}", successCount, failCount);

                // 发送批量操作结果通知
                try
                {
                    var batchNotification = new TaktRealTimeNotification
                    {
                        Type = TaktMessageType.System,
                        Title = "批量强制下线完成",
                        Content = $"批量强制下线操作完成，成功: {successCount} 个用户，失败: {failCount} 个用户",
                        Timestamp = DateTime.Now
                    };
                    await Clients.Others.ReceiveBroadcast(batchNotification);
                }
                catch (Exception ex)
                {
                    _logger.Warn("发送批量操作结果通知失败: {Error}", ex.Message);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("批量强制用户下线失败", ex);
                throw;
            }
        }
    }

    /// <summary>
    /// SignalR配置
    /// </summary>
    public class SignalRConfig
    {
        /// <summary>
        /// 传输配置
        /// </summary>
        public TransportConfig? Transport { get; set; }
        /// <summary>
        /// 认证配置
        /// </summary>
        public AuthenticationConfig? Authentication { get; set; }
        /// <summary>
        /// 用户管理配置
        /// </summary>
        public UserManagementConfig? UserManagement { get; set; }
        /// <summary>
        /// 设备管理配置
        /// </summary>
        public DeviceManagementConfig? DeviceManagement { get; set; }
        /// <summary>
        /// 是否启用详细错误信息
        /// </summary>
        public bool EnableDetailedErrors { get; set; }
        /// <summary>
        /// 最大接收消息大小
        /// </summary>
        public int MaximumReceiveMessageSize { get; set; }
        /// <summary>
        /// 握手超时时间（秒）
        /// </summary>
        public int HandshakeTimeout { get; set; }
        /// <summary>
        /// 保活间隔时间（秒）
        /// </summary>
        public int KeepAliveInterval { get; set; }
        /// <summary>
        /// 客户端超时间隔（秒）
        /// </summary>
        public int ClientTimeoutInterval { get; set; }
        /// <summary>
        /// 流缓冲区容量
        /// </summary>
        public int StreamBufferCapacity { get; set; }
    }

    /// <summary>
    /// 传输配置
    /// </summary>
    public class TransportConfig
    {
        /// <summary>
        /// WebSocket配置
        /// </summary>
        public WebSocketConfig? WebSockets { get; set; }
        /// <summary>
        /// ServerSentEvents配置
        /// </summary>
        public ServerSentEventsConfig? ServerSentEvents { get; set; }
        /// <summary>
        /// LongPolling配置
        /// </summary>
        public LongPollingConfig? LongPolling { get; set; }
    }

    /// <summary>
    /// WebSocket配置
    /// </summary>
    public class WebSocketConfig
    {
        /// <summary>
        /// 关闭超时时间
        /// </summary>
        public int CloseTimeout { get; set; }
        /// <summary>
        /// 子协议
        /// </summary>
        public string? SubProtocol { get; set; }
    }

    /// <summary>
    /// ServerSentEvents配置
    /// </summary>
    public class ServerSentEventsConfig
    {
        /// <summary>
        /// 客户端超时时间
        /// </summary>
        public int ClientTimeoutInterval { get; set; }
    }

    /// <summary>
    /// LongPolling配置
    /// </summary>
    public class LongPollingConfig
    {
        /// <summary>
        /// 轮询超时时间
        /// </summary>
        public int PollTimeout { get; set; }
    }

    /// <summary>
    /// 认证配置
    /// </summary>
    public class AuthenticationConfig
    {
        /// <summary>
        /// 是否需要认证
        /// </summary>
        public bool RequireAuthentication { get; set; }
        /// <summary>
        /// 令牌验证配置
        /// </summary>
        public TokenValidationConfig? TokenValidation { get; set; }
    }

    /// <summary>
    /// 令牌验证配置
    /// </summary>
    public class TokenValidationConfig
    {
        /// <summary>
        /// 是否验证发行者
        /// </summary>
        public bool ValidateIssuer { get; set; }
        /// <summary>
        /// 是否验证受众
        /// </summary>
        public bool ValidateAudience { get; set; }
        /// <summary>
        /// 是否验证签名密钥
        /// </summary>
        public bool ValidateIssuerSigningKey { get; set; }
    }

    /// <summary>
    /// 用户管理配置
    /// </summary>
    public class UserManagementConfig
    {
        /// <summary>
        /// 每个用户最大设备数
        /// </summary>
        public int MaxDevicesPerUser { get; set; }
        /// <summary>
        /// 是否允许多连接
        /// </summary>
        public bool AllowMultipleConnections { get; set; }
        /// <summary>
        /// 默认分组ID
        /// </summary>
        public int DefaultGroupId { get; set; }
    }

    /// <summary>
    /// 设备管理配置
    /// </summary>
    public class DeviceManagementConfig
    {
        /// <summary>
        /// 是否启用设备跟踪
        /// </summary>
        public bool EnableDeviceTracking { get; set; }
        /// <summary>
        /// 每个用户最大设备数
        /// </summary>
        public int MaxDevicesPerUser { get; set; }
        /// <summary>
        /// 设备超时时间
        /// </summary>
        public int DeviceTimeoutMinutes { get; set; }
        /// <summary>
        /// 默认分组ID
        /// </summary>
        public int DefaultGroupId { get; set; }
    }
}




