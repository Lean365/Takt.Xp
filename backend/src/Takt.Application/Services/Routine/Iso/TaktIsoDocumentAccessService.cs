#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentAccessService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO文件查阅记录服务实现
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Domain.Entities.Routine.Iso;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Models;
using Takt.Shared.Helpers;
using Takt.Domain.Utils;
using Takt.Shared.Enums;
using Takt.Shared.Exceptions;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using System.Linq.Expressions;

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文件查阅记录服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoDocumentAccessService : TaktBaseService, ITaktIsoDocumentAccessService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktIsoDocumentAccessService(
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
        /// 获取ISO文档查阅记录仓储
        /// </summary>
        private ITaktRepository<TaktIsoDocumentAccess> IsoDocumentAccessRepository => _repositoryFactory.GetBusinessRepository<TaktIsoDocumentAccess>();

        /// <summary>
        /// 获取文件查阅记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件查阅记录分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoDocumentAccessDto>> GetListAsync(TaktIsoDocumentAccessQueryDto query)
        {
            query ??= new TaktIsoDocumentAccessQueryDto();

            _logger.Info($"查询ISO文档查阅记录列表，参数：DocumentId={query.DocumentId}, AccessedBy={query.AccessedBy}, AccessType={query.AccessType}");

            var result = await IsoDocumentAccessRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询ISO文档查阅记录列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoDocumentAccessDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoDocumentAccessDto>>()
            };
        }

        /// <summary>
        /// 获取文件查阅记录详情
        /// </summary>
        /// <param name="accessRecordId">文件查阅记录ID</param>
        /// <returns>文件查阅记录详情</returns>
        public async Task<TaktIsoDocumentAccessDto> GetByIdAsync(long accessRecordId)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(accessRecordId);
            return access == null ? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", accessRecordId)) : access.Adapt<TaktIsoDocumentAccessDto>();
        }

        /// <summary>
        /// 创建文件查阅记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件查阅记录ID</returns>
        public async Task<long> CreateAsync(TaktIsoDocumentAccessCreateDto input)
        {
            var access = input.Adapt<TaktIsoDocumentAccess>();
            access.AccessDate = DateTime.Now;
            access.AccessedBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoDocumentAccessRepository.CreateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentAccess.Created"),
                    Content = L("Routine.IsoDocumentAccess.CreatedContent", access.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = access
                });

                return access.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentAccess.CreateFailed"));
        }

        /// <summary>
        /// 更新文件查阅记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoDocumentAccessUpdateDto input)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(input.AccessId)
                ?? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", input.AccessId));

            input.Adapt(access);
            var result = await IsoDocumentAccessRepository.UpdateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.Updated"),
                    Content = L("Routine.IsoDocumentAccess.UpdatedContent", access.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = access
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除文件查阅记录
        /// </summary>
        /// <param name="accessRecordId">文件查阅记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long accessRecordId)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(accessRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", accessRecordId));

            var result = await IsoDocumentAccessRepository.DeleteAsync(accessRecordId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.Deleted"),
                    Content = L("Routine.IsoDocumentAccess.DeletedContent", access.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = access
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除文件查阅记录
        /// </summary>
        /// <param name="accessRecordIds">文件查阅记录ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] accessRecordIds)
        {
            if (accessRecordIds == null || accessRecordIds.Length == 0) return false;

            // 获取要删除的查阅记录信息用于通知
            var accesses = await IsoDocumentAccessRepository.GetListAsync(x => accessRecordIds.Contains(x.Id));

            var result = await IsoDocumentAccessRepository.DeleteRangeAsync(accessRecordIds.Cast<object>().ToList());

            if (result > 0 && accesses.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.BatchDeleted"),
                    Content = L("Routine.IsoDocumentAccess.BatchDeletedContent", accesses.Count),
                    Timestamp = DateTime.Now,
                    Data = accesses
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出文件查阅记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentAccessQueryDto query, string? sheetName, string? fileName)
        {
            var accesses = await IsoDocumentAccessRepository.GetListAsync(QueryExpression(query));
            var exportData = accesses.Adapt<List<TaktIsoDocumentAccessDto>>();
            var actualSheetName = sheetName ?? "查阅记录";
            var actualFileName = fileName ?? "查阅记录数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取文件查阅记录选项列表
        /// </summary>
        /// <returns>文件查阅记录选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var accesses = await IsoDocumentAccessRepository.GetListAsync(x => x.IsSuccessful);
            return accesses.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.DocumentVersion} ({x.AccessedBy})",
                DictValue = x.Id,
                ExtLabel = x.DocumentVersion,
                ExtValue = x.AccessedBy,
                OrderNum = 0,
                Status = x.IsSuccessful ? 1 : 0
            }).ToList();
        }

        /// <summary>
        /// 根据ISO文档ID获取查阅记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>查阅记录列表</returns>
        public async Task<List<TaktIsoDocumentAccessDto>> GetByDocumentIdAsync(long documentId)
        {
            var accesses = await IsoDocumentAccessRepository.GetListAsync(x => x.DocumentId == documentId);
            return accesses.OrderByDescending(x => x.AccessDate).Adapt<List<TaktIsoDocumentAccessDto>>();
        }

        /// <summary>
        /// 记录文件查阅
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="accessType">查阅类型</param>
        /// <param name="accessPurpose">查阅目的</param>
        /// <returns>查阅记录ID</returns>
        public async Task<long> RecordAccessAsync(long documentId, string documentVersion, int accessType, string? accessPurpose)
        {
            var access = new TaktIsoDocumentAccess
            {
                DocumentId = documentId,
                DocumentVersion = documentVersion,
                AccessType = accessType,
                AccessPurpose = accessPurpose,
                AccessDate = DateTime.Now,
                AccessedBy = _currentUser.UserName ?? string.Empty,
                AccessDuration = 0,
                IsSuccessful = true
            };

            var result = await IsoDocumentAccessRepository.CreateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentAccess.AccessRecorded"),
                    Content = L("Routine.IsoDocumentAccess.AccessRecordedContent", documentVersion),
                    Timestamp = DateTime.Now,
                    Data = access
                });

                return access.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentAccess.RecordFailed"));
        }

        /// <summary>
        /// 更新查阅时长
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="duration">查阅时长（秒）</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAccessDurationAsync(long accessRecordId, int duration)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(accessRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", accessRecordId));

            access.AccessDuration = duration;
            var result = await IsoDocumentAccessRepository.UpdateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.DurationUpdated"),
                    Content = L("Routine.IsoDocumentAccess.DurationUpdatedContent", access.DocumentVersion, duration),
                    Timestamp = DateTime.Now,
                    Data = access
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 确认查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="confirmComment">确认意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ConfirmAccessAsync(long accessRecordId, string? confirmComment)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(accessRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", accessRecordId));

            access.AccessResult = confirmComment;
            access.IsSuccessful = true;
            var result = await IsoDocumentAccessRepository.UpdateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.AccessConfirmed"),
                    Content = L("Routine.IsoDocumentAccess.AccessConfirmedContent", access.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = access
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 评价查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="rating">评价等级</param>
        /// <param name="ratingComment">评价内容</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RateAccessAsync(long accessRecordId, int rating, string? ratingComment)
        {
            var access = await IsoDocumentAccessRepository.GetByIdAsync(accessRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentAccess.NotFound", accessRecordId));

            access.IsRated = true;
            access.Rating = rating;
            access.RatingComment = ratingComment;
            access.RatingDate = DateTime.Now;

            var result = await IsoDocumentAccessRepository.UpdateAsync(access);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentAccess.AccessRated"),
                    Content = L("Routine.IsoDocumentAccess.AccessRatedContent", access.DocumentVersion, rating),
                    Timestamp = DateTime.Now,
                    Data = access
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取用户查阅统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        public async Task<object> GetUserAccessStatisticsAsync(long userId, DateTime? startDate, DateTime? endDate)
        {
            var query = Expressionable.Create<TaktIsoDocumentAccess>()
                .And(x => x.AccessedBy == userId.ToString())
                .AndIF(startDate.HasValue, x => x.AccessDate >= startDate!.Value)
                .AndIF(endDate.HasValue, x => x.AccessDate <= endDate!.Value)
                .ToExpression();

            var totalCount = await IsoDocumentAccessRepository.GetCountAsync(query);
            var successfulCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.AccessedBy == userId.ToString() && 
                x.IsSuccessful &&
                (!startDate.HasValue || x.AccessDate >= startDate.Value) &&
                (!endDate.HasValue || x.AccessDate <= endDate.Value));
            var ratedCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.AccessedBy == userId.ToString() && 
                x.IsRated &&
                (!startDate.HasValue || x.AccessDate >= startDate.Value) &&
                (!endDate.HasValue || x.AccessDate <= endDate.Value));
            var accesses = await IsoDocumentAccessRepository.GetListAsync(query);
            var totalDuration = accesses.Sum(x => x.AccessDuration);

            return new
            {
                TotalCount = totalCount,
                SuccessfulCount = successfulCount,
                RatedCount = ratedCount,
                TotalDuration = totalDuration,
                AverageDuration = totalCount > 0 ? totalDuration / totalCount : 0
            };
        }

        /// <summary>
        /// 获取文档查阅统计
        /// </summary>
        /// <param name="documentId">文档ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        public async Task<object> GetDocumentAccessStatisticsAsync(long documentId, DateTime? startDate, DateTime? endDate)
        {
            var query = Expressionable.Create<TaktIsoDocumentAccess>()
                .And(x => x.DocumentId == documentId)
                .AndIF(startDate.HasValue, x => x.AccessDate >= startDate!.Value)
                .AndIF(endDate.HasValue, x => x.AccessDate <= endDate!.Value)
                .ToExpression();

            var totalCount = await IsoDocumentAccessRepository.GetCountAsync(query);
            var successfulCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.DocumentId == documentId && 
                x.IsSuccessful &&
                (!startDate.HasValue || x.AccessDate >= startDate.Value) &&
                (!endDate.HasValue || x.AccessDate <= endDate.Value));
            var ratedCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.DocumentId == documentId && 
                x.IsRated &&
                (!startDate.HasValue || x.AccessDate >= startDate.Value) &&
                (!endDate.HasValue || x.AccessDate <= endDate.Value));
            var accesses = await IsoDocumentAccessRepository.GetListAsync(query);
            var totalDuration = accesses.Sum(x => x.AccessDuration);

            return new
            {
                TotalCount = totalCount,
                SuccessfulCount = successfulCount,
                RatedCount = ratedCount,
                TotalDuration = totalDuration,
                AverageDuration = totalCount > 0 ? totalDuration / totalCount : 0
            };
        }

        /// <summary>
        /// 获取热门文档列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>热门文档列表</returns>
        public async Task<List<object>> GetPopularDocumentsAsync(int topCount = 10, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = Expressionable.Create<TaktIsoDocumentAccess>()
                .AndIF(startDate.HasValue, x => x.AccessDate >= startDate!.Value)
                .AndIF(endDate.HasValue, x => x.AccessDate <= endDate!.Value)
                .ToExpression();

            var popularDocuments = await IsoDocumentAccessRepository.GetListAsync(query);
            var grouped = popularDocuments
                .GroupBy(x => new { x.DocumentId, x.DocumentVersion })
                .Select(g => new
                {
                    DocumentId = g.Key.DocumentId,
                    DocumentVersion = g.Key.DocumentVersion,
                    AccessCount = g.Count(),
                    UniqueUsers = g.Select(x => x.AccessedBy).Distinct().Count(),
                    TotalDuration = g.Sum(x => x.AccessDuration),
                    LastAccessDate = g.Max(x => x.AccessDate)
                })
                .OrderByDescending(x => x.AccessCount)
                .Take(topCount)
                .ToList();

            return popularDocuments.Cast<object>().ToList();
        }

        /// <summary>
        /// 获取活跃用户列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>活跃用户列表</returns>
        public async Task<List<object>> GetActiveUsersAsync(int topCount = 10, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = Expressionable.Create<TaktIsoDocumentAccess>()
                .AndIF(startDate.HasValue, x => x.AccessDate >= startDate!.Value)
                .AndIF(endDate.HasValue, x => x.AccessDate <= endDate!.Value)
                .ToExpression();

            var activeUsers = await IsoDocumentAccessRepository.GetListAsync(query);
            var grouped = activeUsers
                .GroupBy(x => x.AccessedBy)
                .Select(g => new
                {
                    UserName = g.Key,
                    AccessCount = g.Count(),
                    UniqueDocuments = g.Select(x => x.DocumentId).Distinct().Count(),
                    TotalDuration = g.Sum(x => x.AccessDuration),
                    LastAccessDate = g.Max(x => x.AccessDate)
                })
                .OrderByDescending(x => x.AccessCount)
                .Take(topCount)
                .ToList();

            return grouped.Cast<object>().ToList();
        }

        /// <summary>
        /// 获取查阅记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoDocumentAccessRepository.GetCountAsync();
            var successfulCount = await IsoDocumentAccessRepository.GetCountAsync(x => x.IsSuccessful);
            var ratedCount = await IsoDocumentAccessRepository.GetCountAsync(x => x.IsRated);
            var todayCount = await IsoDocumentAccessRepository.GetCountAsync(x => x.AccessDate.Date == DateTime.Today);
            var thisWeekCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.AccessDate >= DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek));
            var thisMonthCount = await IsoDocumentAccessRepository.GetCountAsync(x => 
                x.AccessDate.Year == DateTime.Today.Year && x.AccessDate.Month == DateTime.Today.Month);

            return new
            {
                TotalCount = totalCount,
                SuccessfulCount = successfulCount,
                RatedCount = ratedCount,
                TodayCount = todayCount,
                ThisWeekCount = thisWeekCount,
                ThisMonthCount = thisMonthCount
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoDocumentAccess, bool>> QueryExpression(TaktIsoDocumentAccessQueryDto query)
        {
            return Expressionable.Create<TaktIsoDocumentAccess>()
                .AndIF(query.DocumentId.HasValue, x => x.DocumentId == query.DocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DocumentVersion), x => x.DocumentVersion!.Contains(query.DocumentVersion!))
                .AndIF(!string.IsNullOrEmpty(query.AccessedBy), x => x.AccessedBy!.Contains(query.AccessedBy!))
                .AndIF(query.AccessType.HasValue, x => x.AccessType == query.AccessType!.Value)
                .AndIF(query.StartAccessDate.HasValue, x => x.AccessDate >= query.StartAccessDate!.Value)
                .AndIF(query.EndAccessDate.HasValue, x => x.AccessDate <= query.EndAccessDate!.Value)
                .AndIF(query.IsSuccessful.HasValue, x => x.IsSuccessful == query.IsSuccessful!.Value)
                .AndIF(query.IsRated.HasValue, x => x.IsRated == query.IsRated!.Value)
                .AndIF(query.Rating.HasValue, x => x.Rating == query.Rating!.Value)
                .AndIF(!string.IsNullOrEmpty(query.AccessIp), x => x.AccessIp!.Contains(query.AccessIp!))
                .AndIF(!string.IsNullOrEmpty(query.AccessDevice), x => x.AccessDevice!.Contains(query.AccessDevice!))
                .AndIF(!string.IsNullOrEmpty(query.AccessBrowser), x => x.AccessBrowser!.Contains(query.AccessBrowser!))
                .AndIF(!string.IsNullOrEmpty(query.AccessOs), x => x.AccessOs!.Contains(query.AccessOs!))
                .AndIF(!string.IsNullOrEmpty(query.AccessLocation), x => x.AccessLocation!.Contains(query.AccessLocation!))
                .ToExpression();
        }
    }
}





