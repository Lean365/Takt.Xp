//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalRConnectionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-24 10:00
// 版本号 : V0.0.1
// 描述    : SignalR连接服务实现 - 专注于连接管理和设备状态管理
//===================================================================

using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.Entities.Logging;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Takt.Infrastructure.SignalR
{
    /// <summary>
    /// SignalR连接服务 - 专注于连接管理和设备状态管理
    /// </summary>
    public class TaktSignalRConnectionService : ITaktSignalRConnectionService
    {
        private readonly IHubContext<TaktSignalRHub> _hubContext;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;
        /// <summary>
        /// 仓储工厂（用于复杂查询）
        /// </summary>
        private readonly ITaktRepositoryFactory _repositoryFactory;
        /// <summary>
        /// HTTP上下文访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;
        /// <summary>
        /// 当前用户服务
        /// </summary>
        private readonly ITaktCurrentUser _currentUser;
        private static readonly ConcurrentDictionary<string, List<TaktSignalROnline>> _userDevices = new();

        private ITaktRepository<TaktSignalROnline> Repository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hubContext">SignalR Hub上下文</param>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        public TaktSignalRConnectionService(
            IHubContext<TaktSignalRHub> hubContext,
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser)
        {
            _hubContext = hubContext;
            _logger = logger;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _httpContextAccessor = httpContextAccessor;
            _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
        }

        /// <summary>
        /// 添加用户设备
        /// 关键逻辑：确保一个设备ID只能记录一次，如果存在就更新，不存在才添加
        /// 数据库层面：只要在线用户表中存在设备ID，就更新现有记录，更新连接ID、状态和时间
        /// </summary>
        public async Task AddUserDevice(TaktDeviceLog device, string connectionId, string? userAgent = null, string? userName = null)
        {
            try
            {
                _logger.Info("开始添加/更新用户设备 - UserId: {UserId}, DeviceId: {DeviceId}",
                    device.UserId, device.LastLoginDeviceId ?? device.FirstLoginDeviceId);

                var userId = device.UserId.ToString();
                if (!_userDevices.ContainsKey(userId))
                {
                    _userDevices[userId] = new List<TaktSignalROnline>();
                    _logger.Info("为用户 {UserId} 创建新的设备列表", userId);
                }

                // 前端已完整传递数据，直接使用
                var deviceId = device.LastLoginDeviceId ?? device.FirstLoginDeviceId;
                if (string.IsNullOrEmpty(deviceId))
                {
                    throw new InvalidOperationException($"用户 {device.UserId} 的设备ID不能为空，LastLoginDeviceId和FirstLoginDeviceId都为空");
                }

                // 直接使用TaktDeviceLog中已经正确的数据，确保三个表数据完全一致
                var clientIp = device.LastLoginIp ?? device.FirstLoginIp;
                var clientBrowser = device.LastLoginBrowser ?? device.FirstLoginBrowser;

                // 使用传入的UserAgent参数，与登录时保持一致
                var finalUserAgent = userAgent ?? "";

                // 将 TaktDeviceLog 转换为 TaktSignalROnline
                var onlineDevice = new TaktSignalROnline
                {
                    UserId = device.UserId,
                    DeviceId = deviceId,
                    DeviceType = device.LastLoginDeviceType ?? device.FirstLoginDeviceType ?? "Desktop",
                    ConnectionId = connectionId, // 使用真正的SignalR连接ID
                    LastActivity = DateTime.Now,
                    LastHeartbeat = DateTime.Now,
                    OnlineStatus = 0,
                    GroupId = 1,
                    ClientIp = clientIp ?? "Unknown",
                    UserAgent = finalUserAgent ?? string.Empty,
                    ClientBrowser = clientBrowser ?? string.Empty,
                    // 审计字段由 TaktSignalRRepository 统一处理
                };

                // 关键逻辑：检查是否存在相同设备ID的设备
                // 这确保了内存中一个设备ID只能有一条记录
                var existingDevices = _userDevices[userId].Where(d => d.DeviceId == onlineDevice.DeviceId).ToList();
                if (existingDevices.Any())
                {
                    _logger.Info("发现相同设备ID的设备，移除旧记录并添加新记录 - 设备ID: {DeviceId}, 旧记录数量: {OldCount}",
                        onlineDevice.DeviceId, existingDevices.Count);

                    // 移除所有相同设备ID的旧记录，确保设备ID唯一性
                    foreach (var existingDevice in existingDevices)
                    {
                        _userDevices[userId].Remove(existingDevice);
                        _logger.Debug("移除旧设备记录: ConnectionId={ConnectionId}, LastActivity={LastActivity}",
                            existingDevice.ConnectionId, existingDevice.LastActivity);
                    }

                    // 添加新设备记录，保持设备ID唯一
                    onlineDevice.LastActivity = DateTime.Now;
                    onlineDevice.LastHeartbeat = DateTime.Now;
                    _userDevices[userId].Add(onlineDevice);
                    _logger.Info("已更新设备记录: DeviceId={DeviceId}, 新ConnectionId={ConnectionId}",
                        onlineDevice.DeviceId, onlineDevice.ConnectionId);
                }
                else
                {
                    _logger.Info("添加新设备记录 - 设备ID: {DeviceId}, ConnectionId: {ConnectionId}",
                        onlineDevice.DeviceId, onlineDevice.ConnectionId);
                    onlineDevice.LastActivity = DateTime.Now;
                    onlineDevice.LastHeartbeat = DateTime.Now;
                    _userDevices[userId].Add(onlineDevice);
                }

                // 更新设备日志的最后在线时间
                device.LastOnlineTime = DateTime.Now;

                // 更新今日在线时段（上线）
                await UpdateTodayOnlinePeriodsAsync(device, true); // true 表示上线

                // 保存到数据库 - 这是关键步骤，确保数据持久化
                // 数据库层面也会确保设备ID的唯一性
                _logger.Info("开始将设备信息保存到数据库: UserId={UserId}, DeviceId={DeviceId}", device.UserId, deviceId);

                // 调用数据库保存方法，该方法会确保设备ID的唯一性
                await SaveOnlineUserAsync(onlineDevice, userName);

                _logger.Info("设备信息已成功同步到数据库 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
                _logger.Info("当前用户 {UserId} 的设备数量: {DeviceCount}", userId, _userDevices[userId].Count);
            }
            catch (Exception ex)
            {
                _logger.Error("添加用户设备时发生错误 - UserId: {UserId}, DeviceId: {DeviceId}, Error: {Error}",
                    device.UserId, device.LastLoginDeviceId ?? device.FirstLoginDeviceId ?? "Unknown", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取用户在线设备列表
        /// </summary>
        public async Task<List<TaktSignalROnline>> GetUserOnlineDevicesAsync(string userId)
        {
            _logger.Info("开始获取用户 {UserId} 的在线设备列表", userId);

            // 先从内存中获取
            if (_userDevices.TryGetValue(userId, out var devices))
            {
                var onlineDevices = devices.Where(d => d.OnlineStatus == 0).ToList();
                if (onlineDevices.Any())
                {
                    _logger.Info("从内存中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, onlineDevices.Count);
                    return onlineDevices;
                }
                // 如果内存中没有在线设备，从字典中移除
                _userDevices.TryRemove(userId, out _);
            }

            // 如果内存中没有，从数据库查询
            var onlineUsers = await Repository.GetListAsync(x => x.UserId == long.Parse(userId) && x.OnlineStatus == 0);

            if (onlineUsers != null && onlineUsers.Any())
            {
                _logger.Info("从数据库中找到用户 {UserId} 的在线设备列表，数量: {DeviceCount}", userId, onlineUsers.Count);
                return onlineUsers;
            }

            _logger.Warn("未找到用户 {UserId} 的在线设备列表", userId);
            return new List<TaktSignalROnline>();
        }


        /// <summary>
        /// 移除用户所有设备
        /// </summary>
        public async Task RemoveUserAllDevices(string userId)
        {
            try
            {
                _logger.Info("开始移除用户 {UserId} 的所有设备", userId);

                // 从内存中移除所有设备
                if (_userDevices.TryGetValue(userId, out var devices))
                {
                    var deviceCount = devices.Count;
                    _userDevices.TryRemove(userId, out _);
                    _logger.Info("从内存中移除用户 {UserId} 的 {Count} 个设备", userId, deviceCount);
                }

                // 从数据库中删除该用户的所有记录
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.UserId == long.Parse(userId));
                
                var deletedCount = await Repository.DeleteAsync(exp.ToExpression());
                _logger.Info("从数据库中删除用户 {UserId} 的 {Count} 条记录", userId, deletedCount);

                _logger.Info("移除用户 {UserId} 所有设备完成", userId);
            }
            catch (Exception ex)
            {
                _logger.Error("移除用户 {UserId} 所有设备失败", userId, ex);
            }
        }

        /// <summary>
        /// 移除用户设备
        /// </summary>
        public async Task RemoveUserDevice(string userId, string deviceId)
        {
            try
            {
                _logger.Info("开始移除用户设备 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);

                // 从内存中移除设备
                if (_userDevices.TryGetValue(userId, out var devices))
                {
                    var device = devices.FirstOrDefault(d => d.DeviceId == deviceId);
                    if (device != null)
                    {
                        devices.Remove(device);
                        _logger.Info("从内存中移除设备 - UserId: {UserId}, DeviceId: {DeviceId}, ConnectionId: {ConnectionId}",
                            userId, deviceId, device.ConnectionId);

                        if (!devices.Any())
                        {
                            _userDevices.TryRemove(userId, out _);
                            _logger.Info("用户 {UserId} 的所有设备已移除，清理用户设备列表", userId);
                        }
                    }
                    else
                    {
                        _logger.Warn("内存中未找到设备 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
                    }
                }
                else
                {
                    _logger.Warn("内存中未找到用户设备列表 - UserId: {UserId}", userId);
                }

                // 更新数据库中的设备状态为离线
                try
                {
                    var exp = Expressionable.Create<TaktSignalROnline>();
                    exp.And(u => u.UserId == long.Parse(userId) && u.DeviceId == deviceId);
                    var existingDevice = await Repository.GetFirstAsync(exp.ToExpression());

                    if (existingDevice != null)
                    {
                        existingDevice.OnlineStatus = 1; // 1表示离线
                        existingDevice.LastActivity = DateTime.Now;
                        // 审计字段由 TaktSignalRRepository 统一处理

                        var updateResult = await Repository.UpdateAsync(existingDevice);
                        if (updateResult > 0)
                        {
                            _logger.Info("数据库中的设备状态已更新为离线 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
                        }
                        else
                        {
                            _logger.Warn("数据库中的设备状态更新失败 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
                        }
                    }
                    else
                    {
                        _logger.Warn("数据库中未找到设备记录 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
                    }
                }
                catch (Exception dbEx)
                {
                    _logger.Error("更新数据库设备状态时发生错误 - UserId: {UserId}, DeviceId: {DeviceId}, Error: {Error}",
                        userId, deviceId, dbEx.Message);
                }

                _logger.Info("移除用户设备完成 - UserId: {UserId}, DeviceId: {DeviceId}", userId, deviceId);
            }
            catch (Exception ex)
            {
                _logger.Error("移除用户设备时发生错误 - UserId: {UserId}, DeviceId: {DeviceId}, Error: {Error}",
                    userId, deviceId, ex.Message);
            }
        }

        /// <summary>
        /// 获取用户设备
        /// </summary>
        public async Task<TaktDeviceLog> GetUserDeviceAsync(string userId, string connectionId)
        {
            try
            {
                _logger.Info("开始获取用户设备 - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, connectionId);

                var devices = await GetUserOnlineDevicesAsync(userId);
                _logger.Info("找到用户设备列表 - UserId: {UserId}, 设备数量: {Count}", userId, devices?.Count ?? 0);

                var device = devices?.FirstOrDefault(d => d.DeviceId == connectionId);
                if (device == null)
                {
                    _logger.Warn("未找到指定连接ID的设备 - UserId: {UserId}, ConnectionId: {ConnectionId}", userId, connectionId);
                    throw new Exception($"未找到连接ID为 {connectionId} 的用户设备");
                }

                _logger.Info("成功获取设备信息 - UserId: {UserId}, DeviceId: {DeviceId}", userId, device.DeviceId);

                // 转换为 TaktDeviceLog 类型
                return new TaktDeviceLog
                {
                    UserId = device.UserId,
                    DeviceToken = device.DeviceId,
                    LoginType = 0,
                    LoginSource = 0,
                    LoginProvider = 0,
                    NetworkType = "Unknown",
                    TimeZone = "Asia/Shanghai",
                    Language = "zh-CN",
                    IsVpn = "Unknown",
                    IsProxy = "Unknown",
                    FirstLoginTime = device.LastActivity,
                    FirstLoginIp = device.ClientIp ?? "127.0.0.1",
                    FirstLoginLocation = "",
                    FirstLoginDeviceId = device.DeviceId,
                    FirstLoginDeviceType = device.DeviceType ?? "Unknown",
                    FirstLoginBrowser = "Unknown",
                    FirstLoginOs = "Unknown",
                    LastLoginTime = device.LastActivity,
                    LastLoginIp = device.ClientIp ?? "127.0.0.1",
                    LastLoginLocation = "",
                    LastLoginDeviceId = device.DeviceId,
                    LastLoginDeviceType = device.DeviceType ?? "Unknown",
                    LastLoginBrowser = "Unknown",
                    LastLoginOs = "Unknown",
                    LastOnlineTime = device.LastActivity,
                    LoginCount = 1,
                    ContinuousLoginDays = 1,
                    DeviceStatus = 0,
                    CreateTime = device.CreateTime,
                    IsDeleted = 0
                };
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户设备时发生错误 - UserId: {UserId}, ConnectionId: {ConnectionId}, Error: {Error}",
                    userId, connectionId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 检查用户是否在线
        /// </summary>
        public bool IsUserOnline(string userId)
        {
            return _userDevices.ContainsKey(userId);
        }

        /// <summary>
        /// 更新设备活动时间
        /// </summary>
        public void UpdateDeviceActivity(string userId, string deviceId)
        {
            if (_userDevices.ContainsKey(userId))
            {
                var device = _userDevices[userId].FirstOrDefault(d => d.DeviceId == deviceId);
                if (device != null)
                {
                    device.LastActivity = DateTime.Now;
                    _logger.Info("更新用户 {UserId} 的设备 {DeviceId} 活动时间", userId, deviceId);
                }
            }
        }

        /// <summary>
        /// 获取所有在线用户
        /// </summary>
        public List<TaktSignalROnline> GetAllOnlineDevices()
        {
            return _userDevices.Values.SelectMany(d => d).ToList();
        }

        /// <summary>
        /// 保存在线用户
        /// 关键逻辑：只要在线用户表中存在设备ID，就更新现有记录，更新连接ID、状态和时间
        /// </summary>
        /// <param name="user">在线用户信息</param>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public async Task SaveOnlineUserAsync(TaktSignalROnline user, string? userName = null)
        {
            try
            {
                _logger.Info("开始保存在线用户到数据库: UserId={UserId}, DeviceId={DeviceId}, ConnectionId={ConnectionId}, UserName={UserName}",
                    user.UserId, user.DeviceId, user.ConnectionId, userName ?? "null");

                // 类型转换和验证
                if (user.DeviceId == null)
                {
                    _logger.Error("DeviceId 不能为空，无法执行查询");
                    throw new ArgumentException("DeviceId 不能为空");
                }

                // 确保 UserId 是 long 类型
                long userId = Convert.ToInt64(user.UserId);
                string deviceId = user.DeviceId;

                // 使用专门的SignalR仓储查询，绕过常规业务逻辑过滤
                // 查找所有状态的记录，包括离线的记录
                var existingUser = await Repository.GetFirstAsync(x => x.UserId == userId && x.DeviceId == deviceId);

                if (existingUser != null)
                {
                    // 发现现有记录：更新现有记录
                    _logger.Info("发现现有记录，开始更新: UserId={UserId}, DeviceId={DeviceId}, 记录ID={RecordId}",
                        user.UserId, user.DeviceId, existingUser.Id);

                    // 更新所有相关字段，保持设备ID不变
                    existingUser.ConnectionId = user.ConnectionId ?? string.Empty;           // 更新连接ID
                    existingUser.LastActivity = DateTime.Now;               // 更新最后活动时间为当前时间
                    existingUser.LastHeartbeat = DateTime.Now;              // 更新最后心跳时间为当前时间
                    existingUser.OnlineStatus = user.OnlineStatus;          // 更新在线状态
                    existingUser.ClientIp = user.ClientIp ?? "Unknown";     // 更新客户端IP，确保不为NULL
                    existingUser.UserAgent = user.UserAgent ?? string.Empty; // 更新用户代理
                    existingUser.ClientBrowser = user.ClientBrowser ?? string.Empty; // 更新客户端浏览器
                    existingUser.DeviceType = user.DeviceType ?? "Desktop"; // 更新设备类型
                    // 审计字段由 TaktSignalRRepository 统一处理

                    var updateResult = await Repository.UpdateAsync(existingUser, userName);
                    if (updateResult > 0)
                    {
                        _logger.Info("在线用户记录更新成功: UserId={UserId}, DeviceId={DeviceId}, 影响行数={UpdateRows}",
                            user.UserId, user.DeviceId, updateResult);
                    }
                    else
                    {
                        _logger.Warn("在线用户记录更新失败，影响行数为0: UserId={UserId}, DeviceId={DeviceId}",
                            user.UserId, user.DeviceId);
                    }
                }
                else
                {
                    // 未发现现有记录：创建新记录
                    _logger.Info("未发现现有记录，开始创建新记录: UserId={UserId}, DeviceId={DeviceId}", user.UserId, user.DeviceId);

                    // 设置新记录的基础字段
                    // 审计字段由 TaktSignalRRepository 统一处理

                    var createResult = await Repository.CreateAsync(user, userName);
                    if (createResult > 0)
                    {
                        _logger.Info("在线用户记录创建成功: UserId={UserId}, DeviceId={DeviceId}, 新记录ID={NewId}",
                            user.UserId, user.DeviceId, createResult);
                    }
                    else
                    {
                        _logger.Warn("在线用户记录创建失败，返回ID为0: UserId={UserId}, DeviceId={DeviceId}",
                            user.UserId, user.DeviceId);
                    }
                }

                _logger.Info("在线用户保存操作完成: UserId={UserId}, DeviceId={DeviceId}, 操作类型={OperationType}",
                    user.UserId, user.DeviceId, existingUser != null ? "更新" : "创建");
            }
            catch (Exception ex)
            {
                _logger.Error("保存在线用户时发生错误: ConnectionId={ConnectionId}, UserId={UserId}, DeviceId={DeviceId}, Error: {Error}",
                    user.ConnectionId, user.UserId, user.DeviceId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取在线用户信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<TaktSignalROnline> GetOnlineUserAsync(string connectionId)
        {
            try
            {
                return await Repository.GetFirstAsync(x => x.ConnectionId == connectionId && x.OnlineStatus == 0) ?? throw new Exception($"未找到连接ID为 {connectionId} 的在线用户");
            }
            catch (Exception)
            {
                _logger.Error("获取在线用户信息时发生错误: ConnectionId={ConnectionId}", connectionId);
                throw;
            }
        }

        /// <summary>
        /// 根据设备ID获取在线用户
        /// </summary>
        public async Task<TaktSignalROnline> GetOnlineUserByDeviceAsync(long userId, string deviceId)
        {
            try
            {
                return await Repository.GetFirstAsync(x => x.UserId == userId && x.DeviceId == deviceId && x.OnlineStatus == 0) ?? throw new Exception($"未找到用户ID为 {userId} 设备ID为 {deviceId} 的在线用户");
            }
            catch (Exception)
            {
                _logger.Error("获取在线用户信息时发生错误: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                throw;
            }
        }

        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task<string> GetDeviceInfo(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await Repository.GetFirstAsync(exp.ToExpression());
                return user?.DeviceId ?? string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error("获取设备信息时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// 删除在线用户（设置为离线状态）
        /// </summary>
        public async Task<bool> DeleteOnlineUserAsync(string connectionId)
        {
            try
            {
                // 先查找在线用户记录
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.ConnectionId == connectionId);
                var onlineUser = await Repository.GetFirstAsync(exp.ToExpression());

                if (onlineUser != null)
                {
                    // 计算在线时长并更新统计信息
                    await UpdateOnlineTimeStatisticsAsync(onlineUser);

                    // 更新设备日志的最后离线时间
                    await UpdateDeviceLogOfflineTimeAsync(onlineUser.UserId, onlineUser.DeviceId);

                    // 更新为离线状态，而不是删除记录
                    onlineUser.OnlineStatus = 1; // 1表示离线
                    onlineUser.LastActivity = DateTime.Now;
                    // 审计字段由 TaktSignalRRepository 统一处理

                    var result = await Repository.UpdateAsync(onlineUser);
                    _logger.Info("用户离线状态更新完成: ConnectionId={ConnectionId}, UserId={UserId}, DeviceId={DeviceId}",
                        connectionId, onlineUser.UserId, onlineUser.DeviceId);
                    return result > 0;
                }
                else
                {
                    _logger.Warn("未找到在线用户记录: ConnectionId={ConnectionId}", connectionId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("更新在线用户状态时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取在线用户列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetConnectionIdsAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0); // 0表示在线状态
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5)); // 只返回5分钟内有活动的连接

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户连接ID列表时发生错误: UserId={UserId}", userId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取用户在线连接ID列表（通用方法）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>连接ID列表</returns>
        private async Task<List<string>> GetUserOnlineConnectionIdsAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.UserId == userId && u.OnlineStatus == 0);
                exp.And(u => u.LastActivity > DateTime.Now.AddMinutes(-5));

                var users = await Repository.GetListAsync(exp.ToExpression());
                return users.Where(u => !string.IsNullOrEmpty(u.ConnectionId))
                    .Select(u => u.ConnectionId!)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户在线连接ID列表时发生错误: UserId={UserId}", userId, ex.Message);
                return new List<string>();
            }
        }





        /// <summary>
        /// 更新用户最后活动时间
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task UpdateUserLastActiveTimeAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await Repository.GetFirstAsync(exp.ToExpression());
                if (user != null)
                {
                    user.LastActivity = DateTime.Now;
                    await Repository.UpdateAsync(user);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("更新用户最后活动时间时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 更新用户心跳时间
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task UpdateUserHeartbeatAsync(string connectionId)
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.ConnectionId == connectionId);

                var user = await Repository.GetFirstAsync(exp.ToExpression());
                if (user != null)
                {
                    user.LastHeartbeat = DateTime.Now;
                    user.LastActivity = DateTime.Now;
                    // 审计字段由 TaktSignalRRepository 统一处理

                    var updateResult = await Repository.UpdateAsync(user);
                    if (updateResult > 0)
                    {
                        _logger.Debug("用户心跳更新成功: ConnectionId={ConnectionId}, UserId={UserId}, 新心跳时间={HeartbeatTime}",
                            connectionId, user.UserId, user.LastHeartbeat.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    else
                    {
                        _logger.Warn("用户心跳更新失败: ConnectionId={ConnectionId}, UserId={UserId}", connectionId, user.UserId);
                    }
                }
                else
                {
                    _logger.Warn("未找到要更新心跳的用户: ConnectionId={ConnectionId}", connectionId);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("更新用户心跳时间时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 批量更新所有在线用户的心跳时间
        /// </summary>
        /// <returns>更新的记录数</returns>
        public async Task<int> UpdateAllOnlineUsersHeartbeatAsync()
        {
            try
            {
                var exp = Expressionable.Create<TaktSignalROnline>();
                exp.And(u => u.OnlineStatus == 0); // 0表示在线状态

                var onlineUsers = await Repository.GetListAsync(exp.ToExpression());
                if (!onlineUsers?.Any() == true)
                {
                    _logger.Debug("没有找到需要更新心跳的在线用户");
                    return 0;
                }

                var now = DateTime.Now;
                foreach (var user in onlineUsers)
                {
                    user.LastHeartbeat = now;    // 批量更新心跳时间
                    user.LastActivity = now;     // 批量更新活动时间
                    // 审计字段由 TaktSignalRRepository 统一处理
                }

                var updateResult = await Repository.UpdateRangeAsync(onlineUsers);
                _logger.Info("批量更新在线用户心跳完成: 总数={TotalCount}, 更新成功数={UpdateCount}",
                    onlineUsers.Count, updateResult);

                return updateResult;
            }
            catch (Exception ex)
            {
                _logger.Error("批量更新在线用户心跳时发生错误: {Error}", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 确保在线用户记录存在，不存在则创建，存在则更新
        /// </summary>
        /// <param name="connectionId">连接ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceInfo">设备信息</param>
        /// <param name="userName">用户名</param>
        public async Task EnsureOnlineUserRecordAsync(string connectionId, long userId, TaktDeviceLog deviceInfo, string? userName = null)
        {
            try
            {
                _logger.Debug("开始确保在线用户记录存在: ConnectionId={ConnectionId}, UserId={UserId}, DeviceId={DeviceId}",
                    connectionId, userId, deviceInfo?.LastLoginDeviceId);

                // 先尝试查找现有记录
                var deviceId = deviceInfo?.LastLoginDeviceId ?? "Unknown";
                var existingUser = await Repository.GetFirstAsync(x => x.UserId == userId && x.DeviceId == deviceId && x.OnlineStatus == 0);

                if (existingUser != null)
                {
                    // 记录存在，更新所有相关字段，与SaveOnlineUserAsync保持完全一致
                    _logger.Debug("发现现有记录，开始更新: 记录ID={RecordId}", existingUser.Id);

                    // 直接使用TaktDeviceLog中已经正确的数据，确保三个表数据完全一致
                    var clientIp = deviceInfo?.LastLoginIp ?? deviceInfo?.FirstLoginIp;
                    var clientBrowser = deviceInfo?.LastLoginBrowser ?? deviceInfo?.FirstLoginBrowser;

                    // 从DeviceInfo中获取UserAgent（如果DeviceInfo中有的话）
                    var deviceInfoJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(deviceInfo?.DeviceInfo ?? "{}");
                    var userAgent = deviceInfoJson?["UserAgent"]?.ToString() ?? deviceInfoJson?["userAgent"]?.ToString();

                    // 更新所有相关字段，保持设备ID不变，与SaveOnlineUserAsync逻辑完全一致
                    existingUser.ConnectionId = connectionId;           // 更新连接ID
                    existingUser.LastActivity = DateTime.Now;          // 更新最后活动时间为当前时间
                    existingUser.LastHeartbeat = DateTime.Now;         // 更新最后心跳时间为当前时间
                    existingUser.OnlineStatus = 0;                     // 更新在线状态为在线
                    existingUser.ClientIp = clientIp ?? "Unknown";     // 使用实际IP，确保不为NULL
                    existingUser.UserAgent = userAgent ?? string.Empty; // 使用实际UserAgent，确保不为NULL
                    existingUser.ClientBrowser = clientBrowser ?? string.Empty; // 使用实际浏览器，确保不为NULL
                    existingUser.DeviceType = deviceInfo?.LastLoginDeviceType ?? deviceInfo?.FirstLoginDeviceType ?? "Desktop"; // 更新设备类型
                    // 审计字段由 TaktSignalRRepository 统一处理

                    var updateResult = await Repository.UpdateAsync(existingUser);
                    if (updateResult > 0)
                    {
                        _logger.Debug("在线用户记录心跳更新成功: 记录ID={RecordId}, 影响行数={UpdateRows}",
                            existingUser.Id, updateResult);
                    }
                    else
                    {
                        _logger.Warn("在线用户记录心跳更新失败: 记录ID={RecordId}", existingUser.Id);
                    }
                }
                else
                {
                    // 记录不存在，创建新记录，与SaveOnlineUserAsync保持完全一致
                    _logger.Debug("未发现现有记录，创建新记录: UserId={UserId}, DeviceId={DeviceId}",
                        userId, deviceInfo?.LastLoginDeviceId);

                    // 直接使用TaktDeviceLog中已经正确的数据，确保三个表数据完全一致
                    var clientIp = deviceInfo?.LastLoginIp ?? deviceInfo?.FirstLoginIp;
                    var clientBrowser = deviceInfo?.LastLoginBrowser ?? deviceInfo?.FirstLoginBrowser;

                    // 从DeviceInfo中获取UserAgent（如果DeviceInfo中有的话）
                    var deviceInfoJson = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(deviceInfo?.DeviceInfo ?? "{}");
                    var userAgent = deviceInfoJson?["UserAgent"]?.ToString() ?? deviceInfoJson?["userAgent"]?.ToString();

                    var newOnlineUser = new TaktSignalROnline
                    {
                        UserId = userId,
                        DeviceId = deviceInfo?.LastLoginDeviceId ?? "Unknown",
                        DeviceType = deviceInfo?.LastLoginDeviceType ?? deviceInfo?.FirstLoginDeviceType ?? "Desktop",
                        ConnectionId = connectionId,
                        LastActivity = DateTime.Now,
                        LastHeartbeat = DateTime.Now,
                        OnlineStatus = 0, // 0表示在线
                        GroupId = 1,                                    // 使用默认分组ID
                        ClientIp = clientIp ?? "Unknown",               // 使用实际IP，确保不为NULL
                        UserAgent = userAgent ?? string.Empty,          // 使用实际UserAgent，确保不为NULL
                        ClientBrowser = clientBrowser ?? string.Empty,  // 使用实际浏览器，确保不为NULL
                        // 审计字段由 TaktSignalRRepository 统一处理
                    };

                    var createResult = await Repository.CreateAsync(newOnlineUser);
                    if (createResult > 0)
                    {
                        _logger.Debug("在线用户记录创建成功: 新记录ID={NewId}", createResult);
                    }
                    else
                    {
                        _logger.Warn("在线用户记录创建失败: UserId={UserId}, DeviceId={DeviceId}",
                            userId, deviceInfo?.LastLoginDeviceId);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("确保在线用户记录时发生错误: ConnectionId={ConnectionId}, UserId={UserId}, Error: {Error}",
                    connectionId, userId, ex.Message);
                throw;
            }
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public async Task DisconnectUserAsync(string connectionId)
        {
            try
            {
                _logger.Info("断开用户连接: ConnectionId={ConnectionId}", connectionId);
                await _hubContext.Clients.Client(connectionId).SendAsync("ForceOffline", "您已被管理员强制下线");
            }
            catch (Exception ex)
            {
                _logger.Error("断开用户连接时发生错误: ConnectionId={ConnectionId}", connectionId, ex.Message);
                throw;
            }
        }


        /// <summary>
        /// 更新在线时长统计信息
        /// </summary>
        /// <param name="onlineUser">在线用户记录</param>
        private Task UpdateOnlineTimeStatisticsAsync(TaktSignalROnline onlineUser)
        {
            try
            {
                var now = DateTime.Now;
                var onlineTime = now - onlineUser.LastActivity;
                var onlineMinutes = (int)onlineTime.TotalMinutes;

                if (onlineMinutes > 0)
                {
                    // 更新总在线时长
                    onlineUser.TotalOnlineMinutes += onlineMinutes;

                    // 更新今日在线时长
                    var today = now.Date;
                    var lastActivityDate = onlineUser.LastActivity.Date;

                    if (today == lastActivityDate)
                    {
                        // 同一天，累加今日在线时长
                        onlineUser.TodayOnlineMinutes += onlineMinutes;
                    }
                    else
                    {
                        // 不同天，重置今日在线时长
                        onlineUser.TodayOnlineMinutes = onlineMinutes;
                    }

                    _logger.Info("更新在线时长统计: UserId={UserId}, DeviceId={DeviceId}, 本次在线={OnlineMinutes}分钟, 总在线={TotalMinutes}分钟, 今日在线={TodayMinutes}分钟",
                        onlineUser.UserId, onlineUser.DeviceId, onlineMinutes, onlineUser.TotalOnlineMinutes, onlineUser.TodayOnlineMinutes);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("更新在线时长统计时发生错误: UserId={UserId}, DeviceId={DeviceId}, Error={Error}",
                    onlineUser.UserId, onlineUser.DeviceId, ex.Message);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// 更新设备日志的最后离线时间
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deviceId">设备ID</param>
        private async Task UpdateDeviceLogOfflineTimeAsync(long userId, string deviceId)
        {
            try
            {
                var deviceLogRepository = _repositoryFactory.GetAuthRepository<TaktDeviceLog>();
                var deviceLog = await deviceLogRepository.GetFirstAsync(d => d.UserId == userId && d.LastLoginDeviceId == deviceId);

                if (deviceLog != null)
                {
                    deviceLog.LastOfflineTime = DateTime.Now;

                    // 更新今日在线时段
                    await UpdateTodayOnlinePeriodsAsync(deviceLog, false); // false 表示离线

                    await deviceLogRepository.UpdateAsync(deviceLog);
                    _logger.Info("更新设备日志最后离线时间: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                }
                else
                {
                    _logger.Warn("未找到对应的设备日志记录: UserId={UserId}, DeviceId={DeviceId}", userId, deviceId);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("更新设备日志最后离线时间时发生错误: UserId={UserId}, DeviceId={DeviceId}, Error={Error}",
                    userId, deviceId, ex.Message);
            }
        }

        /// <summary>
        /// 更新今日在线时段
        /// </summary>
        /// <param name="deviceLog">设备日志</param>
        /// <param name="isOnline">是否上线（true=上线，false=离线）</param>
        private Task UpdateTodayOnlinePeriodsAsync(TaktDeviceLog deviceLog, bool isOnline)
        {
            try
            {
                var now = DateTime.Now;
                var today = now.Date;
                var timeStr = now.ToString("HH:mm");

                // 解析现有的今日在线时段
                var periods = new List<string>();
                if (!string.IsNullOrEmpty(deviceLog.TodayOnlinePeriods))
                {
                    try
                    {
                        periods = System.Text.Json.JsonSerializer.Deserialize<List<string>>(deviceLog.TodayOnlinePeriods) ?? new List<string>();
                    }
                    catch
                    {
                        periods = new List<string>();
                    }
                }

                if (isOnline)
                {
                    // 上线：添加新的开始时间
                    periods.Add($"{timeStr}-");
                    _logger.Info("添加上线时段: UserId={UserId}, Time={Time}", deviceLog.UserId, timeStr);
                }
                else
                {
                    // 离线：完成最后一个时段
                    if (periods.Any() && periods.Last().EndsWith("-"))
                    {
                        var lastPeriod = periods.Last();
                        periods[periods.Count - 1] = lastPeriod + timeStr;
                        _logger.Info("完成离线时段: UserId={UserId}, Period={Period}", deviceLog.UserId, periods.Last());
                    }
                }

                // 序列化并更新
                deviceLog.TodayOnlinePeriods = System.Text.Json.JsonSerializer.Serialize(periods);
                _logger.Debug("更新今日在线时段: UserId={UserId}, Periods={Periods}", deviceLog.UserId, deviceLog.TodayOnlinePeriods);
            }
            catch (Exception ex)
            {
                _logger.Warn("更新今日在线时段时发生错误: UserId={UserId}, Error={Error}", deviceLog.UserId, ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}





