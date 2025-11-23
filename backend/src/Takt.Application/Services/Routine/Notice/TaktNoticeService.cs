//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNoticeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 通知服务实现
//===================================================================

using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.Notice
{
    /// <summary>
    /// 通知服务实现
    /// </summary>
    public class TaktNoticeService : TaktBaseService, ITaktNoticeService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取通知仓储
        /// </summary>
        private ITaktRepository<TaktNotice> NoticeRepository => _repositoryFactory.GetBusinessRepository<TaktNotice>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNoticeService(
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
        /// 获取通知分页列表
        /// </summary>
        public async Task<TaktPagedResult<TaktNoticeDto>> GetListAsync(TaktNoticeQueryDto query)
        {
            var exp = Expressionable.Create<TaktNotice>();

            if (!string.IsNullOrEmpty(query.NoticeTitle))
                exp.And(x => x.NoticeTitle.Contains(query.NoticeTitle));

            if (query.NoticeType.HasValue)
                exp.And(x => x.NoticeType == query.NoticeType.Value);

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            var result = await NoticeRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktNoticeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNoticeDto>>()
            };
        }

        /// <summary>
        /// 获取通知详情
        /// </summary>
        public async Task<TaktNoticeDto> GetByIdAsync(long noticeId)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            return notice.Adapt<TaktNoticeDto>();
        }

        /// <summary>
        /// 创建通知
        /// </summary>
        public async Task<long> CreateAsync(TaktNoticeCreateDto input)
        {
            var notice = input.Adapt<TaktNotice>();
            notice.CreateTime = DateTime.Now;

            var result = await NoticeRepository.CreateAsync(notice);
            if (result <= 0)
                throw new TaktException(L("Notice.CreateFailed"));

            return notice.Id;
        }

        /// <summary>
        /// 更新通知
        /// </summary>
        public async Task<bool> UpdateAsync(long noticeId, TaktNoticeDto input)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            input.Adapt(notice);
            var result = await NoticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        public async Task<bool> DeleteAsync(long noticeId)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            var result = await NoticeRepository.DeleteAsync(noticeId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除通知
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] noticeIds)
        {
            if (noticeIds == null || noticeIds.Length == 0)
                throw new TaktException(L("Notice.SelectToDelete"));

            foreach (var noticeId in noticeIds)
            {
                await DeleteAsync(noticeId);
            }
            return true;
        }

        /// <summary>
        /// 导出通知数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktNoticeQueryDto query, string sheetName = "Notice")
        {
            try
            {
                var list = await NoticeRepository.GetListAsync(KpNoticeQueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktNoticeExportDto>>(), sheetName, L("Notice.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Notice.ExportFailed"), ex);
                throw new TaktException(L("Notice.ExportFailed"));
            }
        }

        /// <summary>
        /// 发布通知
        /// </summary>
        public async Task<bool> PublishAsync(long noticeId)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            notice.Status = 1; // 已发布
            notice.NoticePublishTime = DateTime.Now;

            var result = await NoticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 关闭通知
        /// </summary>
        public async Task<bool> CloseAsync(long noticeId)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            notice.Status = 2; // 已关闭

            var result = await NoticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 标记通知已读
        /// </summary>
        public async Task<bool> MarkAsReadAsync(long id)
        {
            var notice = await NoticeRepository.GetByIdAsync(id);
            if (notice == null) return false;

            // 更新已读状态
            var readIds = (notice.NoticeReadIds?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>()).ToList();
            var userId = _currentUser.UserId.ToString();
            if (!readIds.Contains(userId))
            {
                readIds.Add(userId);
                notice.NoticeReadIds = string.Join(",", readIds);
                notice.NoticeReadCount = readIds.Count;
                notice.NoticeLastReceiptTime = DateTime.Now;

                await NoticeRepository.UpdateAsync(notice);

                // 发送已读通知
                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.Notification,
                    Title = L("Notice.ReadTitle"),
                    Content = L("Notice.ReadContent", notice.NoticeTitle),
                    Timestamp = DateTime.Now,
                    Data = notice
                };
                await _signalRClient.ReceivePersonalNotice(notification);
            }

            return true;
        }

        /// <summary>
        /// 确认通知
        /// </summary>
        public async Task<bool> ConfirmAsync(long noticeId)
        {
            var notice = await NoticeRepository.GetByIdAsync(noticeId);
            if (notice == null)
                throw new TaktException(L("Notice.NotFound", noticeId));

            if (!notice.NoticeRequireConfirm)
                throw new TaktException(L("Notice.NoConfirmRequired"));

            // 获取当前已确认人ID列表
            var confirmIds = string.IsNullOrEmpty(notice.NoticeConfirmIds)
                ? new List<long>()
                : notice.NoticeConfirmIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .ToList();

            // 如果当前用户已在已确认列表中，抛出异常
            if (confirmIds.Contains(_currentUser.UserId))
                throw new TaktException(L("Notice.AlreadyConfirmed"));

            // 添加当前用户到已确认列表
            confirmIds.Add(_currentUser.UserId);
            notice.NoticeConfirmIds = string.Join(",", confirmIds);
            notice.NoticeConfirmCount = confirmIds.Count;
            notice.NoticeLastReceiptTime = DateTime.Now;

            var result = await NoticeRepository.UpdateAsync(notice);
            return result > 0;
        }

        /// <summary>
        /// 标记所有通知已读
        /// </summary>
        public async Task<int> MarkAllAsReadAsync(long userId)
        {
            var unreadNotices = await NoticeRepository.GetListAsync(n =>
                string.IsNullOrEmpty(n.NoticeReadIds) ||
                !n.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .Contains(userId));

            if (!unreadNotices.Any())
                return 0;

            foreach (var notice in unreadNotices)
            {
                var readIds = string.IsNullOrEmpty(notice.NoticeReadIds)
                    ? new List<long>()
                    : notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(id => long.Parse(id))
                        .ToList();

                if (!readIds.Contains(userId))
                {
                    readIds.Add(userId);
                    notice.NoticeReadIds = string.Join(",", readIds);
                    notice.NoticeReadCount = readIds.Count;
                    notice.NoticeLastReceiptTime = DateTime.Now;
                }
            }

            var result = await NoticeRepository.UpdateRangeAsync(unreadNotices);
            return result;
        }

        /// <summary>
        /// 标记通知未读
        /// </summary>
        public async Task<bool> MarkAsUnreadAsync(long id)
        {
            var notice = await NoticeRepository.GetByIdAsync(id);
            if (notice == null)
                return false;

            // 获取当前已读人ID列表
            var readIds = string.IsNullOrEmpty(notice.NoticeReadIds)
                ? new List<long>()
                : notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();

            // 如果当前用户不在已读列表中，则返回true
            if (!readIds.Contains(_currentUser.UserId))
                return true;

            // 从已读列表中移除当前用户
            readIds.Remove(_currentUser.UserId);
            notice.NoticeReadIds = string.Join(",", readIds);
            notice.NoticeReadCount = readIds.Count;

            var result = await NoticeRepository.UpdateAsync(notice);
            if (result > 0)
            {
                // 发送实时通知
                var notification = new TaktRealTimeNotification
                {
                    Type = TaktMessageType.Notification,
                    Title = L("Notice.UnreadTitle"),
                    Content = L("Notice.UnreadContent", notice.NoticeTitle),
                    Timestamp = DateTime.Now,
                    Data = notice
                };

                await _signalRClient.ReceivePersonalNotice(notification);
            }

            return result > 0;
        }

        /// <summary>
        /// 标记所有通知未读
        /// </summary>
        public async Task<int> MarkAllAsUnreadAsync(long userId)
        {
            var readNotices = await NoticeRepository.GetListAsync(n =>
                !string.IsNullOrEmpty(n.NoticeReadIds) &&
                n.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .Contains(userId));

            if (!readNotices.Any())
                return 0;

            foreach (var notice in readNotices)
            {
                var readIds = notice.NoticeReadIds.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(id => long.Parse(id))
                    .ToList();
                readIds.Remove(userId);
                notice.NoticeReadIds = string.Join(",", readIds);
                notice.NoticeReadCount = readIds.Count;
            }

            var result = await NoticeRepository.UpdateRangeAsync(readNotices);
            return result;
        }

        private Expression<Func<TaktNotice, bool>> KpNoticeQueryExpression(TaktNoticeQueryDto query)
        {
            return Expressionable.Create<TaktNotice>()
                .AndIF(!string.IsNullOrEmpty(query.NoticeTitle), x => x.NoticeTitle.Contains(query.NoticeTitle))
                .AndIF(query.NoticeType.HasValue, x => x.NoticeType == query.NoticeType.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}



