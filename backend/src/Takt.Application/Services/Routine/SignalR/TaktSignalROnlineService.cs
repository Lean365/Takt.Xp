//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalROnlineService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线用户服务实现
//===================================================================

using Takt.Application.Dtos.Routine.SignalR;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Domain.IServices.SignalR;
using Takt.Shared.Models;
using Takt.Shared.Utils;
using Takt.Shared.Exceptions;
using Takt.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace Takt.Application.Services.Routine.SignalR;

/// <summary>
/// 在线用户服务实现
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalROnlineService : TaktBaseService, ITaktSignalROnlineService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktSignalRConnectionService _signalRUserService;
    private readonly ITaktSignalRNotificationService _notificationService;

    private ITaktRepository<TaktSignalROnline> Repository => _repositoryFactory.GetBusinessRepository<TaktSignalROnline>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="signalRUserService">SignalR用户服务</param>
    /// <param name="notificationService">SignalR通知服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktSignalROnlineService(
        ITaktLogger logger,
        ITaktRepositoryFactory repositoryFactory,
        IHttpContextAccessor httpContextAccessor,
        ITaktSignalRConnectionService signalRUserService,
        ITaktSignalRNotificationService notificationService,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _signalRUserService = signalRUserService;
        _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
    }

    /// <summary>
    /// 获取在线用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktSignalROnlineDto>> GetListAsync(TaktSignalROnlineQueryDto query)
    {
        try
        {
            // 1.构建查询条件
            var exp = QueryExpression(query);

            // 2.查询数据
            var result = await Repository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            // 3.转换数据
            return new TaktPagedResult<TaktSignalROnlineDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktSignalROnlineDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.GetListFailed"), ex);
            throw new TaktException(L("User.GetListFailed"));
        }
    }

    /// <summary>
    /// 获取用户连接ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>连接ID列表</returns>
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
            _logger.Error("获取用户连接ID列表时发生错误: UserId={UserId}", ex, userId);
            throw;
        }
    }

    /// <summary>
    /// 根据条件获取在线用户信息
    /// </summary>
    /// <param name="predicate">查询条件</param>
    /// <returns>在线用户信息</returns>
    public async Task<TaktSignalROnlineDto> GetFirstAsync(Expression<Func<TaktSignalROnlineDto, bool>> predicate)
    {
        // 由于DTO和实体字段相同，直接使用相同的lambda表达式
        var exp = predicate.Compile();
        var users = await Repository.GetListAsync();
        var dtos = users.Select(x => x.Adapt<TaktSignalROnlineDto>());
        return dtos.FirstOrDefault(exp);
    }

    /// <summary>
    /// 获取所有在线用户
    /// </summary>
    /// <returns>在线用户列表</returns>
    public async Task<List<TaktSignalROnlineDto>> GetAllAsync()
    {
        var users = await Repository.GetListAsync();
        return users.Select(x => x.Adapt<TaktSignalROnlineDto>()).ToList();
    }

    /// <summary>
    /// 创建在线用户
    /// </summary>
    /// <param name="input">在线用户信息</param>
    /// <returns>在线用户ID</returns>
    public async Task<long> CreateOnlineUserAsync(TaktSignalROnlineCreateDto input)
    {
        try
        {
            var now = DateTime.Now;
            var user = input.Adapt<TaktSignalROnline>();
            user.LastActivity = now;
            user.LastHeartbeat = now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.TotalOnlineMinutes = 0; // 初始化总在线时长
            user.TodayOnlineMinutes = 0; // 初始化今日在线时长
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            
            _logger.Info("创建在线用户: UserId={UserId}, DeviceId={DeviceId}, LastActivity={LastActivity}", 
                user.UserId, user.DeviceId, user.LastActivity);
                
            var result = await Repository.CreateAsync(user);
            if (result <= 0)
                throw new TaktException(L("User.CreateFailed"));
            return user.Id;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.CreateFailed"), ex);
            throw new TaktException(L("User.CreateFailed"));
        }
    }

    /// <summary>
    /// 更新在线用户信息
    /// </summary>
    /// <param name="input">在线用户更新信息</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateAsync(TaktSignalROnlineUpdateDto input)
    {
        try
        {
            var user = await Repository.GetByIdAsync(input.SignalROnlineId);
            if (user == null)
                throw new TaktException(L("User.NotFound", input.SignalROnlineId));

            input.Adapt(user);
            var result = await Repository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("User.UpdateFailed"));
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.UpdateFailed", input.SignalROnlineId), ex);
            throw new TaktException(L("User.UpdateFailed"));
        }
    }

    /// <summary>
    /// 更新在线用户状态
    /// </summary>
    /// <param name="input">状态更新信息</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(TaktSignalROnlineStatusUpdateDto input)
    {
        try
        {
            var user = await Repository.GetByIdAsync(input.SignalROnlineId);
            if (user == null)
                throw new TaktException(L("User.NotFound", input.SignalROnlineId));

            var now = DateTime.Now;
            var oldStatus = user.OnlineStatus;
            
            user.OnlineStatus = input.OnlineStatus;
            if (input.LastActivity.HasValue)
                user.LastActivity = input.LastActivity.Value;
            if (input.LastHeartbeat.HasValue)
                user.LastHeartbeat = input.LastHeartbeat.Value;
            if (!string.IsNullOrEmpty(input.Remark))
                user.Remark = input.Remark;
                
            // 处理在线时长更新
            await UpdateOnlineDurationAsync(user, oldStatus, input.OnlineStatus, now);
            
            var result = await Repository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("User.UpdateStatusFailed"));
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.UpdateStatusFailed", input.SignalROnlineId), ex);
            throw new TaktException(L("User.UpdateStatusFailed"));
        }
    }

    /// <summary>
    /// 删除在线用户
    /// </summary>
    /// <param name="id">在线用户ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteOnlineUserAsync(long id)
    {
        try
        {
            var user = await Repository.GetByIdAsync(id);
            if (user == null)
                throw new TaktException(L("User.NotFound", id));

            var result = await Repository.DeleteAsync(id);
            if (result <= 0)
                throw new TaktException(L("User.DeleteFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.DeleteFailed", id), ex);
            throw new TaktException(L("User.DeleteFailed", id));
        }
    }

    /// <summary>
    /// 批量删除在线用户
    /// </summary>
    /// <param name="ids">在线用户ID列表</param>
    /// <returns>删除成功的数量</returns>
    public async Task<int> BatchDeleteOnlineUsersAsync(List<long> ids)
    {
        try
        {
            if (ids == null || !ids.Any())
                throw new TaktException(L("User.BatchDeleteIdsEmpty"));

            var users = await Repository.GetListAsync(u => ids.Contains(u.Id));
            if (!users.Any())
                throw new TaktException(L("User.BatchDeleteNotFound"));

            var result = await Repository.DeleteAsync(u => ids.Contains(u.Id));
            if (result <= 0)
                throw new TaktException(L("User.BatchDeleteFailed"));

            _logger.Info("批量删除在线用户成功: 数量={Count}, IDs={Ids}", result, string.Join(",", ids));
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.BatchDeleteFailed"), ex);
            throw new TaktException(L("User.BatchDeleteFailed"));
        }
    }

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="deviceId">设备ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> ForceOfflineAsync(string deviceId)
    {
        try
        {
            var exp = Expressionable.Create<TaktSignalROnline>();
            exp.And(u => u.DeviceId == deviceId);

            var user = await Repository.GetFirstAsync(exp.ToExpression());
            if (user == null)
                throw new TaktException(L("User.NotFound", deviceId));

            _logger.Info("正在强制用户下线: DeviceId={DeviceId}, UserId={UserId}", deviceId, user.UserId);

            // 1. 发送强制下线消息
            try
            {
                var notification = new TaktRealTimeNotification
                {
                    Title = "强制下线通知",
                    Content = "您已被管理员强制下线",
                    Type = TaktMessageType.System,
                    Timestamp = DateTime.Now
                };
                await _notificationService.SendBusinessNotificationAsync(notification, new List<long> { user.UserId });
                _logger.Info("已发送强制下线消息: DeviceId={DeviceId}", deviceId);
            }
            catch (Exception ex)
            {
                _logger.Error("发送强制下线消息失败: DeviceId={DeviceId}", ex, deviceId);
            }

            // 2. 更新用户状态
            try
            {
                user.LastActivity = DateTime.Now;
                user.OnlineStatus = 1; // 设置为离线状态
                user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
                user.UpdateTime = DateTime.Now;
                await Repository.UpdateAsync(user);
                _logger.Info("已更新用户状态为离线: DeviceId={DeviceId}", deviceId);
            }
            catch (Exception ex)
            {
                _logger.Error("更新用户状态失败: DeviceId={DeviceId}", ex, deviceId);
            }

            // 3. 断开 SignalR 连接
            try
            {
                await _signalRUserService.DisconnectUserAsync(user.ConnectionId);
                _logger.Info("已断开用户连接: DeviceId={DeviceId}, ConnectionId={ConnectionId}", deviceId, user.ConnectionId);
            }
            catch (Exception ex)
            {
                _logger.Error("断开用户连接失败: DeviceId={DeviceId}", ex, deviceId);
            }

            _logger.Info("用户强制下线成功: DeviceId={DeviceId}, UserId={UserId}", deviceId, user.UserId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error("强制用户下线失败: DeviceId={DeviceId}", ex, deviceId);
            throw;
        }
    }

    /// <summary>
    /// 更新所有在线用户的心跳时间
    /// </summary>
    /// <returns>更新的记录数</returns>
    public async Task<int> UpdateHeartbeatAsync()
    {
        var users = await Repository.GetListAsync();
        if (!users.Any()) return 0;

        var now = DateTime.Now;
        foreach (var user in users)
        {
            // 更新在线时长
            await CalculateAndUpdateOnlineDurationAsync(user, now);
            
            user.LastActivity = now;
            user.OnlineStatus = 0; // 设置为在线状态
            user.ClientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            user.UpdateTime = now;
        }

        return await Repository.UpdateRangeAsync(users);
    }

    /// <summary>
    /// 清理过期用户
    /// </summary>
    /// <param name="minutes">超时时间(分钟)</param>
    /// <returns>清理数量</returns>
    public async Task<int> CleanupExpiredUsersAsync(int minutes = 20)
    {
        try
        {
            var expiredTime = DateTime.Now.AddMinutes(-minutes);
            var exp = Expressionable.Create<TaktSignalROnline>();
            exp.And(u => u.LastActivity < expiredTime && u.OnlineStatus == 0); // 清理过期的在线用户

            return await Repository.DeleteAsync(exp.ToExpression());
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.CleanupFailed"), ex);
            throw new TaktException(L("User.CleanupFailed"));
        }
    }

    /// <summary>
    /// 导出在线用户数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktSignalROnlineQueryDto query, string sheetName = "在线用户信息")
    {
        try
        {
            var exp = Expressionable.Create<TaktSignalROnline>();

            if (query.UserId.HasValue)
                exp = exp.And(u => u.UserId == query.UserId.Value);

            if (query.StartTime.HasValue)
                exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

            if (query.OnlineStatus.HasValue)
                exp = exp.And(u => u.OnlineStatus == query.OnlineStatus.Value);

            var list = await Repository.GetListAsync(exp.ToExpression());
            return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktSignalROnlineExportDto>>(), sheetName, L("User.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("User.ExportFailed"), ex);
            throw new TaktException(L("User.ExportFailed"));
        }
    }

    /// <summary>
    /// 构建在线用户查询表达式
    /// </summary>
    private Expression<Func<TaktSignalROnline, bool>> QueryExpression(TaktSignalROnlineQueryDto query)
    {
        var exp = Expressionable.Create<TaktSignalROnline>();

        if (query.UserId.HasValue)
            exp = exp.And(u => u.UserId == query.UserId.Value);

        if (query.StartTime.HasValue)
            exp = exp.And(u => u.LastActivity >= query.StartTime.Value);

        if (query.EndTime.HasValue)
            exp = exp.And(u => u.LastActivity <= query.EndTime.Value);

        if (query.OnlineStatus.HasValue)
            exp = exp.And(u => u.OnlineStatus == query.OnlineStatus.Value);

        return exp.ToExpression();
    }

    /// <summary>
    /// 更新在线时长
    /// </summary>
    /// <param name="user">在线用户实体</param>
    /// <param name="oldStatus">旧状态</param>
    /// <param name="newStatus">新状态</param>
    /// <param name="now">当前时间</param>
    private async Task UpdateOnlineDurationAsync(TaktSignalROnline user, int oldStatus, int newStatus, DateTime now)
    {
        try
        {
            var today = now.Date;
            var lastHeartbeatDate = user.LastHeartbeat.Date;
            
            // 如果是新的一天，重置今日在线时长
            if (lastHeartbeatDate != today)
            {
                user.TodayOnlineMinutes = 0;
                _logger.Info("新的一天，重置今日在线时长: UserId={UserId}, LastHeartbeatDate={LastHeartbeatDate}, Today={Today}", 
                    user.UserId, lastHeartbeatDate.ToString("yyyy-MM-dd"), today.ToString("yyyy-MM-dd"));
            }
            
            // 如果状态从离线变为在线，记录开始时间
            if (oldStatus == 1 && newStatus == 0)
            {
                _logger.Info("用户上线，开始计算在线时长: UserId={UserId}, DeviceId={DeviceId}", user.UserId, user.DeviceId);
            }
            // 如果状态从在线变为离线，计算并更新在线时长
            else if (oldStatus == 0 && newStatus == 1)
            {
                await CalculateAndUpdateOnlineDurationAsync(user, now);
            }
            // 如果保持在线状态，定期更新在线时长（通过心跳）
            else if (oldStatus == 0 && newStatus == 0)
            {
                // 每5分钟更新一次在线时长，避免频繁计算
                if (user.LastHeartbeat.AddMinutes(5) <= now)
                {
                    await CalculateAndUpdateOnlineDurationAsync(user, now);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.Error("更新在线时长失败: UserId={UserId}, Error={Error}", user.UserId, ex.Message);
        }
    }

    /// <summary>
    /// 计算并更新在线时长
    /// </summary>
    /// <param name="user">在线用户实体</param>
    /// <param name="now">当前时间</param>
    private async Task CalculateAndUpdateOnlineDurationAsync(TaktSignalROnline user, DateTime now)
    {
        try
        {
            // 计算从上次心跳到现在的时长（分钟）
            var durationMinutes = (int)(now - user.LastHeartbeat).TotalMinutes;
            
            if (durationMinutes > 0)
            {
                // 更新总在线时长
                user.TotalOnlineMinutes += durationMinutes;
                
                // 更新今日在线时长
                user.TodayOnlineMinutes += durationMinutes;
                
                // 更新最后心跳时间
                user.LastHeartbeat = now;
                
                _logger.Info("更新在线时长: UserId={UserId}, DurationMinutes={DurationMinutes}, TotalMinutes={TotalMinutes}, TodayMinutes={TodayMinutes}", 
                    user.UserId, durationMinutes, user.TotalOnlineMinutes, user.TodayOnlineMinutes);
            }
        }
        catch (Exception ex)
        {
            _logger.Error("计算在线时长失败: UserId={UserId}, Error={Error}", user.UserId, ex.Message);
        }
    }
}




