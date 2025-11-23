#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentRevisionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO文档修订历史服务实现
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
    /// ISO文档修订历史服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoDocumentRevisionService : TaktBaseService, ITaktIsoDocumentRevisionService
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
        public TaktIsoDocumentRevisionService(
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
        /// 获取ISO文档修订历史仓储
        /// </summary>
        private ITaktRepository<TaktIsoDocumentRevision> IsoDocumentRevisionRepository => _repositoryFactory.GetBusinessRepository<TaktIsoDocumentRevision>();

        /// <summary>
        /// 获取修订历史分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>修订历史分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoDocumentRevisionDto>> GetListAsync(TaktIsoDocumentRevisionQueryDto query)
        {
            query ??= new TaktIsoDocumentRevisionQueryDto();

            _logger.Info($"查询ISO文档修订历史列表，参数：DocumentId={query.DocumentId}, Version={query.Version}, Status={query.Status}");

            var result = await IsoDocumentRevisionRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询ISO文档修订历史列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoDocumentRevisionDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoDocumentRevisionDto>>()
            };
        }

        /// <summary>
        /// 获取修订历史详情
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>修订历史详情</returns>
        public async Task<TaktIsoDocumentRevisionDto> GetByIdAsync(long revisionHistoryId)
        {
            var revision = await IsoDocumentRevisionRepository.GetByIdAsync(revisionHistoryId);
            return revision == null ? throw new TaktException(L("Routine.IsoDocumentRevision.NotFound", revisionHistoryId)) : revision.Adapt<TaktIsoDocumentRevisionDto>();
        }

        /// <summary>
        /// 创建修订历史
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>修订历史ID</returns>
        public async Task<long> CreateAsync(TaktIsoDocumentRevisionCreateDto input)
        {
            // 验证版本号是否已存在
            await TaktValidateUtils.ValidateFieldsExistsAsync(IsoDocumentRevisionRepository,
                new Dictionary<string, string>
                {
                    { "DocumentId", input.DocumentId.ToString() },
                    { "Version", input.Version }
                });

            var revision = input.Adapt<TaktIsoDocumentRevision>();
            revision.RevisionDate = DateTime.Now;
            revision.RevisionBy = _currentUser.UserName ?? string.Empty;
            revision.Status = 0; // 草稿

            var result = await IsoDocumentRevisionRepository.CreateAsync(revision);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentRevision.Created"),
                    Content = L("Routine.IsoDocumentRevision.CreatedContent", revision.Version),
                    Timestamp = DateTime.Now,
                    Data = revision
                });

                return revision.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentRevision.CreateFailed"));
        }

        /// <summary>
        /// 更新修订历史
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoDocumentRevisionUpdateDto input)
        {
            var revision = await IsoDocumentRevisionRepository.GetByIdAsync(input.RevisionId)
                ?? throw new TaktException(L("Routine.IsoDocumentRevision.NotFound", input.RevisionId));

            // 验证版本号是否已存在
            if (revision.DocumentId != input.DocumentId || revision.Version != input.Version)
                await TaktValidateUtils.ValidateFieldsExistsAsync(IsoDocumentRevisionRepository,
                    new Dictionary<string, string>
                    {
                        { "DocumentId", input.DocumentId.ToString() },
                        { "Version", input.Version }
                    }, input.RevisionId);

            input.Adapt(revision);
            var result = await IsoDocumentRevisionRepository.UpdateAsync(revision);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentRevision.Updated"),
                    Content = L("Routine.IsoDocumentRevision.UpdatedContent", revision.Version),
                    Timestamp = DateTime.Now,
                    Data = revision
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除修订历史
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long revisionHistoryId)
        {
            var revision = await IsoDocumentRevisionRepository.GetByIdAsync(revisionHistoryId)
                ?? throw new TaktException(L("Routine.IsoDocumentRevision.NotFound", revisionHistoryId));

            var result = await IsoDocumentRevisionRepository.DeleteAsync(revisionHistoryId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentRevision.Deleted"),
                    Content = L("Routine.IsoDocumentRevision.DeletedContent", revision.Version),
                    Timestamp = DateTime.Now,
                    Data = revision
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除修订历史
        /// </summary>
        /// <param name="revisionHistoryIds">修订历史ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] revisionHistoryIds)
        {
            if (revisionHistoryIds == null || revisionHistoryIds.Length == 0) return false;

            // 获取要删除的修订历史信息用于通知
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(x => revisionHistoryIds.Contains(x.Id));

            var result = await IsoDocumentRevisionRepository.DeleteRangeAsync(revisionHistoryIds.Cast<object>().ToList());

            if (result > 0 && revisions.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentRevision.BatchDeleted"),
                    Content = L("Routine.IsoDocumentRevision.BatchDeletedContent", revisions.Count),
                    Timestamp = DateTime.Now,
                    Data = revisions
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出修订历史数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentRevisionQueryDto query, string? sheetName, string? fileName)
        {
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(QueryExpression(query));
            var exportData = revisions.Adapt<List<TaktIsoDocumentRevisionDto>>();
            var actualSheetName = sheetName ?? "修订历史";
            var actualFileName = fileName ?? "修订历史数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取修订历史选项列表
        /// </summary>
        /// <returns>修订历史选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(x => x.Status == 3); // 已发布
            return revisions.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.Version} ({x.RevisionDate:yyyy-MM-dd})",
                DictValue = x.Id,
                ExtLabel = x.Version,
                ExtValue = x.RevisionBy,
                OrderNum = 0,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// 根据ISO文档ID获取修订历史列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>修订历史列表</returns>
        public async Task<List<TaktIsoDocumentRevisionDto>> GetByDocumentIdAsync(long documentId)
        {
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(x => x.DocumentId == documentId);
            return revisions.OrderByDescending(x => x.RevisionDate).Adapt<List<TaktIsoDocumentRevisionDto>>();
        }

        /// <summary>
        /// 设置当前版本
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetCurrentVersionAsync(long revisionHistoryId)
        {
            var revision = await IsoDocumentRevisionRepository.GetByIdAsync(revisionHistoryId)
                ?? throw new TaktException(L("Routine.IsoDocumentRevision.NotFound", revisionHistoryId));

            // 先将同一文档的其他版本设为非当前版本
            var otherRevisions = await IsoDocumentRevisionRepository.GetListAsync(x => 
                x.DocumentId == revision.DocumentId && x.Id != revisionHistoryId);
            
            foreach (var otherRevision in otherRevisions)
            {
                otherRevision.IsCurrentVersion = false;
                await IsoDocumentRevisionRepository.UpdateAsync(otherRevision);
            }

            // 设置当前版本
            revision.IsCurrentVersion = true;
            revision.Status = 3; // 已发布
            revision.EffectiveDate = DateTime.Now;

            var result = await IsoDocumentRevisionRepository.UpdateAsync(revision);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentRevision.SetCurrentVersion"),
                    Content = L("Routine.IsoDocumentRevision.SetCurrentVersionContent", revision.Version),
                    Timestamp = DateTime.Now,
                    Data = revision
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 审批修订历史
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="status">审批状态</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveAsync(long revisionHistoryId, string? approvalComment, int status)
        {
            var revision = await IsoDocumentRevisionRepository.GetByIdAsync(revisionHistoryId)
                ?? throw new TaktException(L("Routine.IsoDocumentRevision.NotFound", revisionHistoryId));

            revision.Status = status;
            revision.ApprovalBy = _currentUser.UserName ?? string.Empty;
            revision.ApprovalDate = DateTime.Now;
            revision.ApprovalComment = approvalComment;

            // 如果审批通过且设为当前版本，则更新其他版本
            if (status == 2 && revision.IsCurrentVersion) // 已审批且为当前版本
            {
                var otherRevisions = await IsoDocumentRevisionRepository.GetListAsync(x => 
                    x.DocumentId == revision.DocumentId && x.Id != revisionHistoryId);
                
                foreach (var otherRevision in otherRevisions)
                {
                    otherRevision.IsCurrentVersion = false;
                    await IsoDocumentRevisionRepository.UpdateAsync(otherRevision);
                }
            }

            var result = await IsoDocumentRevisionRepository.UpdateAsync(revision);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentRevision.Approved"),
                    Content = L("Routine.IsoDocumentRevision.ApprovedContent", revision.Version, GetStatusText(status)),
                    Timestamp = DateTime.Now,
                    Data = revision
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 提交修订申请
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="version">版本号</param>
        /// <param name="revisionContent">修订内容</param>
        /// <param name="revisionReason">修订原因</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SubmitRevisionAsync(long documentId, string version, string revisionContent, string revisionReason)
        {
            // 验证版本号是否已存在
            await TaktValidateUtils.ValidateFieldsExistsAsync(IsoDocumentRevisionRepository,
                new Dictionary<string, string>
                {
                    { "DocumentId", documentId.ToString() },
                    { "Version", version }
                });

            var revision = new TaktIsoDocumentRevision
            {
                DocumentId = documentId,
                Version = version,
                RevisionContent = revisionContent,
                RevisionReason = revisionReason,
                RevisionDate = DateTime.Now,
                RevisionBy = _currentUser.UserName ?? string.Empty,
                Status = 1, // 待审批
                IsCurrentVersion = false
            };

            var result = await IsoDocumentRevisionRepository.CreateAsync(revision);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentRevision.Submitted"),
                    Content = L("Routine.IsoDocumentRevision.SubmittedContent", version),
                    Timestamp = DateTime.Now,
                    Data = revision
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 审批修订
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveRevisionAsync(long revisionRecordId, int approvalResult, string? approvalComment)
        {
            return await ApproveAsync(revisionRecordId, approvalComment, approvalResult);
        }

        /// <summary>
        /// 切换当前版本
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SwitchCurrentVersionAsync(long revisionRecordId)
        {
            return await SetCurrentVersionAsync(revisionRecordId);
        }

        /// <summary>
        /// 获取待审批的修订申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        public async Task<List<TaktIsoDocumentRevisionDto>> GetPendingApprovalListAsync()
        {
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(x => x.Status == 1); // 待审批
            return revisions.OrderByDescending(x => x.RevisionDate).Adapt<List<TaktIsoDocumentRevisionDto>>();
        }

        /// <summary>
        /// 获取我的修订申请列表
        /// </summary>
        /// <param name="reviserId">修订人ID</param>
        /// <returns>我的申请列表</returns>
        public async Task<List<TaktIsoDocumentRevisionDto>> GetMyApplicationsAsync(long reviserId)
        {
            var revisions = await IsoDocumentRevisionRepository.GetListAsync(x => x.RevisionBy == _currentUser.UserName);
            return revisions.OrderByDescending(x => x.RevisionDate).Adapt<List<TaktIsoDocumentRevisionDto>>();
        }

        /// <summary>
        /// 获取修订历史统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoDocumentRevisionRepository.GetCountAsync();
            var draftCount = await IsoDocumentRevisionRepository.GetCountAsync(x => x.Status == 0);
            var pendingApprovalCount = await IsoDocumentRevisionRepository.GetCountAsync(x => x.Status == 1);
            var approvedCount = await IsoDocumentRevisionRepository.GetCountAsync(x => x.Status == 2);
            var publishedCount = await IsoDocumentRevisionRepository.GetCountAsync(x => x.Status == 3);
            var currentVersionCount = await IsoDocumentRevisionRepository.GetCountAsync(x => x.IsCurrentVersion);

            return new
            {
                TotalCount = totalCount,
                DraftCount = draftCount,
                PendingApprovalCount = pendingApprovalCount,
                ApprovedCount = approvedCount,
                PublishedCount = publishedCount,
                CurrentVersionCount = currentVersionCount
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoDocumentRevision, bool>> QueryExpression(TaktIsoDocumentRevisionQueryDto query)
        {
            return Expressionable.Create<TaktIsoDocumentRevision>()
                .AndIF(query.DocumentId.HasValue, x => x.DocumentId == query.DocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.Version), x => x.Version!.Contains(query.Version!))
                .AndIF(!string.IsNullOrEmpty(query.RevisionBy), x => x.RevisionBy!.Contains(query.RevisionBy!))
                .AndIF(!string.IsNullOrEmpty(query.ApprovalBy), x => x.ApprovalBy!.Contains(query.ApprovalBy!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(query.IsCurrentVersion.HasValue, x => x.IsCurrentVersion == query.IsCurrentVersion!.Value)
                .AndIF(query.StartRevisionDate.HasValue, x => x.RevisionDate >= query.StartRevisionDate!.Value)
                .AndIF(query.EndRevisionDate.HasValue, x => x.RevisionDate <= query.EndRevisionDate!.Value)
                .AndIF(query.StartApprovalDate.HasValue, x => x.ApprovalDate >= query.StartApprovalDate!.Value)
                .AndIF(query.EndApprovalDate.HasValue, x => x.ApprovalDate <= query.EndApprovalDate!.Value)
                .AndIF(query.StartEffectiveDate.HasValue, x => x.EffectiveDate >= query.StartEffectiveDate!.Value)
                .AndIF(query.EndEffectiveDate.HasValue, x => x.EffectiveDate <= query.EndEffectiveDate!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取状态文本
        /// </summary>
        /// <param name="status">状态值</param>
        /// <returns>状态文本</returns>
        private string GetStatusText(int status)
        {
            return status switch
            {
                0 => "草稿",
                1 => "待审批",
                2 => "已审批",
                3 => "已发布",
                4 => "已作废",
                _ => "未知"
            };
        }
    }
}





