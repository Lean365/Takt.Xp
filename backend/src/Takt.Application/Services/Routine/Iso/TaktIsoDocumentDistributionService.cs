#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentDistributionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO文档分发记录服务实现
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
    /// ISO文档分发记录服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoDocumentDistributionService : TaktBaseService, ITaktIsoDocumentDistributionService
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
        public TaktIsoDocumentDistributionService(
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
        /// 获取ISO文档分发记录仓储
        /// </summary>
        private ITaktRepository<TaktIsoDocumentDistribution> IsoDocumentDistributionRepository => _repositoryFactory.GetBusinessRepository<TaktIsoDocumentDistribution>();

        /// <summary>
        /// 获取分发记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分发记录分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoDocumentDistributionDto>> GetListAsync(TaktIsoDocumentDistributionQueryDto query)
        {
            query ??= new TaktIsoDocumentDistributionQueryDto();

            _logger.Info($"查询ISO文档分发记录列表，参数：DocumentId={query.DocumentId}, DistributedToDeptId={query.DistributedToDeptId}, Status={query.Status}");

            var result = await IsoDocumentDistributionRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询ISO文档分发记录列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoDocumentDistributionDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoDocumentDistributionDto>>()
            };
        }

        /// <summary>
        /// 获取分发记录详情
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>分发记录详情</returns>
        public async Task<TaktIsoDocumentDistributionDto> GetByIdAsync(long distributionRecordId)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId);
            return distribution == null ? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId)) : distribution.Adapt<TaktIsoDocumentDistributionDto>();
        }

        /// <summary>
        /// 创建分发记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>分发记录ID</returns>
        public async Task<long> CreateAsync(TaktIsoDocumentDistributionCreateDto input)
        {
            var distribution = input.Adapt<TaktIsoDocumentDistribution>();
            distribution.DistributionDate = DateTime.Now;
            distribution.DistributorBy = _currentUser.UserName ?? string.Empty;
            distribution.Status = 0; // 待分发

            var result = await IsoDocumentDistributionRepository.CreateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentDistribution.Created"),
                    Content = L("Routine.IsoDocumentDistribution.CreatedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });

                return distribution.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentDistribution.CreateFailed"));
        }

        /// <summary>
        /// 更新分发记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoDocumentDistributionUpdateDto input)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(input.DistributionId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", input.DistributionId));

            input.Adapt(distribution);
            var result = await IsoDocumentDistributionRepository.UpdateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.Updated"),
                    Content = L("Routine.IsoDocumentDistribution.UpdatedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除分发记录
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long distributionRecordId)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId));

            var result = await IsoDocumentDistributionRepository.DeleteAsync(distributionRecordId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.Deleted"),
                    Content = L("Routine.IsoDocumentDistribution.DeletedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除分发记录
        /// </summary>
        /// <param name="distributionRecordIds">分发记录ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] distributionRecordIds)
        {
            if (distributionRecordIds == null || distributionRecordIds.Length == 0) return false;

            // 获取要删除的分发记录信息用于通知
            var distributions = await IsoDocumentDistributionRepository.GetListAsync(x => distributionRecordIds.Contains(x.Id));

            var result = await IsoDocumentDistributionRepository.DeleteRangeAsync(distributionRecordIds.Cast<object>().ToList());

            if (result > 0 && distributions.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.BatchDeleted"),
                    Content = L("Routine.IsoDocumentDistribution.BatchDeletedContent", distributions.Count),
                    Timestamp = DateTime.Now,
                    Data = distributions
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出分发记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentDistributionQueryDto query, string? sheetName, string? fileName)
        {
            var distributions = await IsoDocumentDistributionRepository.GetListAsync(QueryExpression(query));
            var exportData = distributions.Adapt<List<TaktIsoDocumentDistributionDto>>();
            var actualSheetName = sheetName ?? "分发记录";
            var actualFileName = fileName ?? "分发记录数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取分发记录选项列表
        /// </summary>
        /// <returns>分发记录选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var distributions = await IsoDocumentDistributionRepository.GetListAsync(x => x.Status == 1); // 已分发
            return distributions.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.DistributedToDeptName} ({x.DocumentVersion})",
                DictValue = x.Id,
                ExtLabel = x.DistributedToDeptName,
                ExtValue = x.DocumentVersion,
                OrderNum = 0,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// 根据ISO文档ID获取分发记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>分发记录列表</returns>
        public async Task<List<TaktIsoDocumentDistributionDto>> GetByDocumentIdAsync(long documentId)
        {
            var distributions = await IsoDocumentDistributionRepository.GetListAsync(x => x.DocumentId == documentId);
            return distributions.OrderByDescending(x => x.DistributionDate).Adapt<List<TaktIsoDocumentDistributionDto>>();
        }

        /// <summary>
        /// 批量创建分发记录
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="userIds">用户ID列表</param>
        /// <param name="distributionType">分发对象类型</param>
        /// <param name="distributionMethod">分发方式</param>
        /// <param name="expireDate">过期日期</param>
        /// <param name="isForced">是否强制分发</param>
        /// <returns>创建结果</returns>
        public async Task<(int success, int fail)> BatchCreateAsync(long documentId, string documentVersion, long[] userIds, int distributionType, int distributionMethod, DateTime? expireDate, bool isForced)
        {
            int successCount = 0;
            int failCount = 0;

            foreach (var userId in userIds)
            {
                try
                {
                    var distribution = new TaktIsoDocumentDistribution
                    {
                        DocumentId = documentId,
                        DocumentVersion = documentVersion,
                        DistributedToDeptId = userId, // 这里假设userId实际上是部门ID
                        DistributedToDeptName = $"部门{userId}", // 这里应该根据实际部门ID获取部门名称
                        DistributionMethod = distributionMethod,
                        DistributionCopies = 1,
                        DistributionDate = DateTime.Now,
                        DistributorBy = _currentUser.UserName ?? string.Empty,
                        Status = 0, // 待分发
                        ExpireDate = expireDate,
                        IsForced = isForced
                    };

                    var result = await IsoDocumentDistributionRepository.CreateAsync(distribution);
                    if (result > 0)
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error($"批量创建分发记录失败，用户ID：{userId}，错误：{ex.Message}");
                    failCount++;
                }
            }

            if (successCount > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentDistribution.BatchCreated"),
                    Content = L("Routine.IsoDocumentDistribution.BatchCreatedContent", successCount),
                    Timestamp = DateTime.Now,
                    Data = new { DocumentId = documentId, SuccessCount = successCount, FailCount = failCount }
                });
            }

            return (successCount, failCount);
        }

        /// <summary>
        /// 接收文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="receiveNote">接收说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ReceiveAsync(long distributionRecordId, string? receiveNote)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId));

            distribution.Status = 2; // 已接收
            distribution.ReceiveDate = DateTime.Now;
            distribution.ReceivedBy = _currentUser.UserName ?? string.Empty;
            distribution.ReceiveNote = receiveNote;

            var result = await IsoDocumentDistributionRepository.UpdateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.DocumentReceived"),
                    Content = L("Routine.IsoDocumentDistribution.DocumentReceivedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 确认文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ConfirmAsync(long distributionRecordId)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId));

            distribution.Status = 3; // 已确认
            distribution.ConfirmDate = DateTime.Now;
            distribution.ConfirmBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoDocumentDistributionRepository.UpdateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.DocumentConfirmed"),
                    Content = L("Routine.IsoDocumentDistribution.DocumentConfirmedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 拒绝文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="rejectReason">拒绝原因</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RejectAsync(long distributionRecordId, string rejectReason)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId));

            distribution.Status = 4; // 已拒绝
            distribution.RejectReason = rejectReason;

            var result = await IsoDocumentDistributionRepository.UpdateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.DocumentRejected"),
                    Content = L("Routine.IsoDocumentDistribution.DocumentRejectedContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> MarkAsReadAsync(long distributionRecordId)
        {
            var distribution = await IsoDocumentDistributionRepository.GetByIdAsync(distributionRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentDistribution.NotFound", distributionRecordId));

            distribution.IsRead = true;
            distribution.ReadCount++;
            distribution.LastReadTime = DateTime.Now;

            var result = await IsoDocumentDistributionRepository.UpdateAsync(distribution);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentDistribution.DocumentRead"),
                    Content = L("Routine.IsoDocumentDistribution.DocumentReadContent", distribution.DistributedToDeptName),
                    Timestamp = DateTime.Now,
                    Data = distribution
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取用户未读分发记录数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>未读数量</returns>
        public async Task<int> GetUnreadCountAsync(long userId)
        {
            // 这里假设userId实际上是部门ID，因为分发是按部门进行的
            var count = await IsoDocumentDistributionRepository.GetCountAsync(x => 
                x.DistributedToDeptId == userId && 
                !x.IsRead && 
                x.Status >= 1); // 已分发及以上状态

            return count;
        }

        /// <summary>
        /// 获取分发记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoDocumentDistributionRepository.GetCountAsync();
            var pendingDistributionCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 0);
            var distributedCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 1);
            var receivedCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 2);
            var confirmedCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 3);
            var rejectedCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 4);
            var expiredCount = await IsoDocumentDistributionRepository.GetCountAsync(x => x.Status == 5);
            var unreadCount = await IsoDocumentDistributionRepository.GetCountAsync(x => !x.IsRead);

            return new
            {
                TotalCount = totalCount,
                PendingDistributionCount = pendingDistributionCount,
                DistributedCount = distributedCount,
                ReceivedCount = receivedCount,
                ConfirmedCount = confirmedCount,
                RejectedCount = rejectedCount,
                ExpiredCount = expiredCount,
                UnreadCount = unreadCount
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoDocumentDistribution, bool>> QueryExpression(TaktIsoDocumentDistributionQueryDto query)
        {
            return Expressionable.Create<TaktIsoDocumentDistribution>()
                .AndIF(query.DocumentId.HasValue, x => x.DocumentId == query.DocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DocumentVersion), x => x.DocumentVersion!.Contains(query.DocumentVersion!))
                .AndIF(query.DistributedToDeptId.HasValue, x => x.DistributedToDeptId == query.DistributedToDeptId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DistributedToDeptName), x => x.DistributedToDeptName!.Contains(query.DistributedToDeptName!))
                .AndIF(query.DistributionMethod.HasValue, x => x.DistributionMethod == query.DistributionMethod!.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DistributorBy), x => x.DistributorBy!.Contains(query.DistributorBy!))
                .AndIF(!string.IsNullOrEmpty(query.ReceivedBy), x => x.ReceivedBy!.Contains(query.ReceivedBy!))
                .AndIF(!string.IsNullOrEmpty(query.ConfirmBy), x => x.ConfirmBy!.Contains(query.ConfirmBy!))
                .AndIF(query.IsForced.HasValue, x => x.IsForced == query.IsForced!.Value)
                .AndIF(query.IsRead.HasValue, x => x.IsRead == query.IsRead!.Value)
                .AndIF(query.StartDistributionDate.HasValue, x => x.DistributionDate >= query.StartDistributionDate!.Value)
                .AndIF(query.EndDistributionDate.HasValue, x => x.DistributionDate <= query.EndDistributionDate!.Value)
                .AndIF(query.StartReceiveDate.HasValue, x => x.ReceiveDate >= query.StartReceiveDate!.Value)
                .AndIF(query.EndReceiveDate.HasValue, x => x.ReceiveDate <= query.EndReceiveDate!.Value)
                .AndIF(query.StartConfirmDate.HasValue, x => x.ConfirmDate >= query.StartConfirmDate!.Value)
                .AndIF(query.EndConfirmDate.HasValue, x => x.ConfirmDate <= query.EndConfirmDate!.Value)
                .ToExpression();
        }
    }
}





