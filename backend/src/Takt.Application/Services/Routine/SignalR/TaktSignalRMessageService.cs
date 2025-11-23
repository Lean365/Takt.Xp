//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSignalMessageService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述    : 在线消息服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine.SignalR;
using Takt.Application.Dtos.Routine.SignalR;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;

namespace Takt.Application.Services.Routine.SignalR;

/// <summary>
/// 在线消息服务实现
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-20
/// </remarks>
public class TaktSignalRMessageService : TaktBaseService, ITaktSignalRMessageService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private ITaktRepository<TaktSignalRMessage> Repository => _repositoryFactory.GetBusinessRepository<TaktSignalRMessage>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logger">日志服务</param>
    /// <param name="repositoryFactory">在线消息仓储工厂</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktSignalRMessageService(
        ITaktLogger logger,
        ITaktRepositoryFactory repositoryFactory,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取在线消息分页列表
    /// </summary>
    public async Task<TaktPagedResult<TaktSignalRMessageDto>> GetListAsync(TaktSignalMessageQueryDto query)
    {
        try
        {
            // 查询数据
            var result = await Repository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            // 转换数据
            return new TaktPagedResult<TaktSignalRMessageDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktSignalRMessageDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.GetListFailed"), ex);
            throw new TaktException(L("Message.GetListFailed"));
        }
    }

    /// <summary>
    /// 获取消息详情
    /// </summary>
    public async Task<TaktSignalRMessageDto> GetMessageAsync(long id)
    {
        try
        {
            var message = await Repository.GetByIdAsync(id);
            if (message == null)
                throw new TaktException(L("Message.NotFound", id));

            return message.Adapt<TaktSignalRMessageDto>();
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.GetFailed", id), ex);
            throw new TaktException(L("Message.GetFailed", id));
        }
    }

    /// <summary>
    /// 创建消息
    /// </summary>
    public async Task<long> CreateMessageAsync(TaktSignalMessageCreateDto input)
    {
        try
        {
            var message = input.Adapt<TaktSignalRMessage>();
            var result = await Repository.CreateAsync(message);
            if (result <= 0)
                throw new TaktException(L("Message.CreateFailed"));

            return message.Id;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.CreateFailed"), ex);
            throw new TaktException(L("Message.CreateFailed"));
        }
    }

    /// <summary>
    /// 更新消息
    /// </summary>
    public async Task<bool> UpdateMessageAsync(TaktSignalMessageUpdateDto input)
    {
        try
        {
            var message = await Repository.GetByIdAsync(input.SignalRMessageId);
            if (message == null)
                throw new TaktException(L("Message.NotFound", input.SignalRMessageId));

            input.Adapt(message);

            var result = await Repository.UpdateAsync(message);
            if (result <= 0)
                throw new TaktException(L("Message.UpdateFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.UpdateFailed", input.SignalRMessageId), ex);
            throw new TaktException(L("Message.UpdateFailed"));
        }
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    public async Task<bool> DeleteMessageAsync(long id)
    {
        try
        {
            var message = await Repository.GetByIdAsync(id);
            if (message == null)
                throw new TaktException(L("Message.NotFound", id));

            var result = await Repository.DeleteAsync(id);
            if (result <= 0)
                throw new TaktException(L("Message.DeleteFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.DeleteFailed", id), ex);
            throw new TaktException(L("Message.DeleteFailed", id));
        }
    }

    /// <summary>
    /// 批量删除消息
    /// </summary>
    public async Task<int> BatchDeleteMessagesAsync(List<long> ids)
    {
        try
        {
            if (ids == null || !ids.Any())
                throw new TaktException(L("Message.BatchDeleteIdsEmpty"));

            var messages = await Repository.GetListAsync(m => ids.Contains(m.Id));
            if (!messages.Any())
                throw new TaktException(L("Message.BatchDeleteNotFound"));

            var result = await Repository.DeleteAsync(m => ids.Contains(m.Id));
            if (result <= 0)
                throw new TaktException(L("Message.BatchDeleteFailed"));

            _logger.Info("批量删除消息成功: 数量={Count}, IDs={Ids}", result, string.Join(",", ids));
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.BatchDeleteFailed"), ex);
            throw new TaktException(L("Message.BatchDeleteFailed"));
        }
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    public async Task<bool> MarkAsReadAsync(long id)
    {
        try
        {
            var message = await Repository.GetByIdAsync(id);
            if (message == null)
                throw new TaktException(L("Message.NotFound", id));

            message.IsRead = 1;
            message.ReadTime = DateTime.Now;

            var result = await Repository.UpdateAsync(message);
            if (result <= 0)
                throw new TaktException(L("Message.MarkReadFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkReadFailed", id), ex);
            throw new TaktException(L("Message.MarkReadFailed", id));
        }
    }

    /// <summary>
    /// 更新消息已读状态
    /// </summary>
    public async Task<bool> UpdateReadStatusAsync(TaktSignalMessageReadStatusUpdateDto input)
    {
        try
        {
            var message = await Repository.GetByIdAsync(input.SignalRMessageId);
            if (message == null)
                throw new TaktException(L("Message.NotFound", input.SignalRMessageId));

            message.IsRead = input.IsRead;
            if (input.ReadTime.HasValue)
                message.ReadTime = input.ReadTime.Value;
            else if (input.IsRead == 1)
                message.ReadTime = DateTime.Now;
            else
                message.ReadTime = null;

            if (!string.IsNullOrEmpty(input.UserAgent))
                message.UserAgent = input.UserAgent;


            var result = await Repository.UpdateAsync(message);
            if (result <= 0)
                throw new TaktException(L("Message.UpdateReadStatusFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.UpdateReadStatusFailed", input.SignalRMessageId), ex);
            throw new TaktException(L("Message.UpdateReadStatusFailed"));
        }
    }

    /// <summary>
    /// 标记所有消息为已读
    /// </summary>
    public async Task<int> MarkAllAsReadAsync(long userId)
    {
        try
        {
            var exp = Expressionable.Create<TaktSignalRMessage>();
            exp.And(m => m.ReceiverId == userId && m.IsRead == 0);

            var messages = await Repository.GetListAsync(exp.ToExpression());
            if (!messages.Any())
                return 0;

            foreach (var message in messages)
            {
                message.IsRead = 1;
                message.ReadTime = DateTime.Now;
            }

            return await Repository.UpdateRangeAsync(messages);
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkAllReadFailed", userId), ex);
            throw new TaktException(L("Message.MarkAllReadFailed", userId));
        }
    }

    /// <summary>
    /// 标记消息为未读
    /// </summary>
    public async Task<bool> MarkAsUnreadAsync(long id)
    {
        try
        {
            var message = await Repository.GetByIdAsync(id);
            if (message == null)
                throw new TaktException(L("Message.NotFound", id));

            message.IsRead = 0;
            message.ReadTime = null;

            var result = await Repository.UpdateAsync(message);
            if (result <= 0)
                throw new TaktException(L("Message.MarkUnreadFailed"));

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkUnreadFailed", id), ex);
            throw new TaktException(L("Message.MarkUnreadFailed", id));
        }
    }

    /// <summary>
    /// 标记所有消息为未读
    /// </summary>
    public async Task<int> MarkAllAsUnreadAsync(long userId)
    {
        try
        {
            var exp = Expressionable.Create<TaktSignalRMessage>();
            exp.And(m => m.ReceiverId == userId && m.IsRead == 1);

            var messages = await Repository.GetListAsync(exp.ToExpression());
            if (!messages.Any())
                return 0;

            foreach (var message in messages)
            {
                message.IsRead = 0;
                message.ReadTime = null;
            }

            return await Repository.UpdateRangeAsync(messages);
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.MarkAllUnreadFailed", userId), ex);
            throw new TaktException(L("Message.MarkAllUnreadFailed", userId));
        }
    }

    /// <summary>
    /// 清理过期消息
    /// </summary>
    public async Task<int> CleanupExpiredMessagesAsync(int days = 7)
    {
        try
        {
            var expiredTime = DateTime.Now.AddDays(-days);
            var exp = Expressionable.Create<TaktSignalRMessage>();
            exp.And(m => m.CreateTime < expiredTime);

            return await Repository.DeleteAsync(exp.ToExpression());
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.CleanupFailed"), ex);
            throw new TaktException(L("Message.CleanupFailed"));
        }
    }

    /// <summary>
    /// 导出在线消息数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktSignalMessageQueryDto query, string sheetName = "在线消息信息")
    {
        try
        {
            var list = await Repository.GetListAsync(QueryExpression(query));
            return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktSignalMessageExportDto>>(), sheetName, L("Message.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("Message.ExportFailed"), ex);
            throw new TaktException(L("Message.ExportFailed"));
        }
    }

    private Expression<Func<TaktSignalRMessage, bool>> QueryExpression(TaktSignalMessageQueryDto query)
    {
        return Expressionable.Create<TaktSignalRMessage>()
            .AndIF(query.SenderId.HasValue, x => x.SenderId == query.SenderId.Value)
            .AndIF(query.ReceiverId.HasValue, x => x.ReceiverId == query.ReceiverId.Value)
            .AndIF(query.MessageType.HasValue, x => x.MessageType == query.MessageType.Value)
            .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
            .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
            .AndIF(query.IsRead.HasValue, x => x.IsRead == query.IsRead.Value)
            .ToExpression();
    }
}




