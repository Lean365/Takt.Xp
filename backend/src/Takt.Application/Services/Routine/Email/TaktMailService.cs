//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMailService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件服务实现
//===================================================================

using System.Linq.Expressions;
using Takt.Application.Dtos.Routine;
using Takt.Shared.Enums;
using Takt.Domain.Entities.Routine;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.Email
{
    /// <summary>
    /// 邮件服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktMailService : TaktBaseService, ITaktMailService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取邮件仓储
        /// </summary>
        private ITaktRepository<TaktMail> MailRepository => _repositoryFactory.GetBusinessRepository<TaktMail>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMailService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktMailDto>> GetListAsync(TaktMailQueryDto query)
        {
            var exp = Expressionable.Create<TaktMail>();

            if (!string.IsNullOrEmpty(query?.MailFrom))
                exp.And(x => x.MailFrom.Contains(query.MailFrom));

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailTo))
                exp.And(x => x.MailTo.Contains(query.MailTo));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            if (!string.IsNullOrEmpty(query?.CreateBy))
                exp.And(x => x.CreateBy == query.CreateBy);

            var result = await MailRepository.GetPagedListAsync(
                exp.ToExpression(),
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktMailDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktMailDto>>()
            };
        }

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>返回邮件详情</returns>
        public async Task<TaktMailDto> GetByIdAsync(long mailId)
        {
            var mail = await MailRepository.GetByIdAsync(mailId);
            if (mail == null)
                throw new TaktException(L("Mail.NotFound", mailId));

            return mail.Adapt<TaktMailDto>();
        }

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回邮件ID</returns>
        public async Task<long> CreateAsync(TaktMailCreateDto input)
        {
            var mail = input.Adapt<TaktMail>();
            var result = await MailRepository.CreateAsync(mail);
            return result;
        }

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktMailUpdateDto input)
        {
            var mail = await MailRepository.GetByIdAsync(input.MailId);
            if (mail == null)
                throw new TaktException(L("Mail.NotFound", input.MailId));

            input.Adapt(mail);
            var result = await MailRepository.UpdateAsync(mail);
            return result > 0;
        }

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long mailId)
        {
            var mail = await MailRepository.GetByIdAsync(mailId);
            if (mail == null)
                throw new TaktException(L("Mail.NotFound", mailId));

            var result = await MailRepository.DeleteAsync(mail);
            return result > 0;
        }

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="mailIds">邮件ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] mailIds)
        {
            if (mailIds == null || mailIds.Length == 0)
                throw new TaktException(L("Mail.SelectToDelete"));

            Expression<Func<TaktMail, bool>> predicate = x => mailIds.Contains(x.Id);
            var result = await MailRepository.DeleteAsync(predicate);
            return result > 0;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> SendAsync(TaktMailSendDto input)
        {
            try
            {
                // TODO: 实现邮件发送逻辑
                var mail = input.Adapt<TaktMail>();
                mail.Status = 1; // 发送成功
                mail.MailSendTime = DateTime.Now;

                var result = await MailRepository.CreateAsync(mail);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Mail.SendFailed", input.MailSubject), ex);
                return false;
            }
        }

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>返回发送结果</returns>
        public async Task<(int success, int fail)> BatchSendAsync(List<TaktMailSendDto> inputs)
        {
            if (inputs == null || !inputs.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var input in inputs)
            {
                if (await SendAsync(input))
                    success++;
                else
                    fail++;
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktMailQueryDto query, string sheetName = "邮件信息")
        {
            var exp = Expressionable.Create<TaktMail>();

            if (!string.IsNullOrEmpty(query?.MailFrom))
                exp.And(x => x.MailFrom.Contains(query.MailFrom));

            if (!string.IsNullOrEmpty(query?.MailSubject))
                exp.And(x => x.MailSubject.Contains(query.MailSubject));

            if (!string.IsNullOrEmpty(query?.MailTo))
                exp.And(x => x.MailTo.Contains(query.MailTo));

            if (query?.Status.HasValue == true)
                exp.And(x => x.Status == query.Status.Value);

            if (query?.StartTime.HasValue == true)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query?.EndTime.HasValue == true)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var mails = await MailRepository.AsQueryable()
                .Where(exp.ToExpression())
                .OrderByDescending(x => x.CreateTime)
                .ToListAsync();

            var exportDtos = mails.Adapt<List<TaktMailExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 标记邮件已读
        /// </summary>
        public async Task<bool> MarkAsReadAsync(long id)
        {
            var mail = await MailRepository.GetByIdAsync(id);
            if (mail == null) return false;

            // 更新已读状态
            var readIds = (mail.MailReadIds?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()).ToList();
            var userId = _currentUser.UserId.ToString();
            if (!readIds.Contains(userId))
            {
                readIds.Add(userId);
                mail.MailReadIds = string.Join(",", readIds);
                mail.MailReadCount = readIds.Count;
                mail.MailLastReadTime = DateTime.Now;

                await MailRepository.UpdateAsync(mail);

                // 发送已读通知
                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.MailRead,
                    Title = L("Mail.Read"),
                    Content = L("Mail.ReadContent", mail.MailSubject),
                    Timestamp = DateTime.Now,
                    Data = mail
                };
                await _signalRClient.ReceivePersonalNotice(notification);
            }

            return true;
        }

        /// <summary>
        /// 标记所有邮件为已读
        /// </summary>
        public async Task<int> MarkAllAsReadAsync(long userId)
        {
            // 查找所有未读邮件（MailReadIds为空或不包含当前用户ID的邮件）
            var unreadMails = await MailRepository.GetListAsync(m =>
                string.IsNullOrEmpty(m.MailReadIds) ||
                !m.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).Contains(userId));

            if (!unreadMails.Any())
                return 0;

            // 遍历每个未读邮件
            foreach (var mail in unreadMails)
            {
                // 获取已读用户列表
                var readIds = string.IsNullOrEmpty(mail.MailReadIds)
                    ? new List<long>()
                    : mail.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).ToList();

                // 如果用户不在已读列表中，添加用户ID
                if (!readIds.Contains(userId))
                {
                    readIds.Add(userId);
                    mail.MailReadIds = string.Join(",", readIds);
                    mail.MailReadCount = readIds.Count;
                    mail.MailLastReadTime = DateTime.Now;
                }
            }

            // 批量更新邮件
            var result = await MailRepository.UpdateRangeAsync(unreadMails);
            return result;
        }

        /// <summary>
        /// 标记邮件未读
        /// </summary>
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            var mail = await MailRepository.GetByIdAsync(id);
            if (mail == null)
                return false;

            // 获取当前已读人ID列表
            var readIds = string.IsNullOrEmpty(mail.MailReadIds)
                ? new List<long>()
                : mail.MailReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            // 如果当前用户不在已读列表中，则返回true
            if (!readIds.Contains(_currentUser.UserId))
                return true;

            // 从已读列表中移除当前用户
            readIds.Remove(_currentUser.UserId);
            mail.MailReadIds = string.Join(",", readIds);
            mail.MailReadCount = readIds.Count;

            var result = await MailRepository.UpdateAsync(mail);
            if (result > 0)
            {
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.MailUnread,
                    Title = L("Mail.Unread"),
                    Content = L("Mail.UnreadContent", mail.MailSubject),
                    Timestamp = DateTime.Now,
                    Data = mail
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 标记所有邮件为未读
        /// </summary>
        public async Task<int> MarkAllAsUnreadAsync(long userId)
        {
            // 查找所有已读邮件（MailReadIds包含当前用户ID的邮件）
            var readMails = await MailRepository.GetListAsync(m =>
                !string.IsNullOrEmpty(m.MailReadIds) &&
                m.MailReadIds.Split(',', StringSplitOptions.None).Select(id => long.Parse(id)).Contains(userId));

            if (!readMails.Any())
                return 0;

            // 遍历每个已读邮件
            foreach (var mail in readMails)
            {
                // 获取已读用户列表并移除当前用户
                var readIds = mail.MailReadIds.Split(',', StringSplitOptions.None)
                    .Select(id => long.Parse(id))
                    .ToList();
                readIds.Remove(userId);
                mail.MailReadIds = string.Join(",", readIds);
                mail.MailReadCount = readIds.Count;
            }

            // 批量更新邮件
            var result = await MailRepository.UpdateRangeAsync(readMails);
            return result;
        }

        /// <summary>
        /// 获取指定用户的邮件状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy)
        {
            if (string.IsNullOrEmpty(createBy))
                throw new ArgumentNullException(nameof(createBy));

            var stats = await MailRepository.AsQueryable()
                .Where(x => x.CreateBy == createBy)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
}




