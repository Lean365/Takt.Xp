#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentObsoleteService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO文档作废与回收记录服务实现
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
    /// ISO文档作废与回收记录服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoDocumentObsoleteService : TaktBaseService, ITaktIsoDocumentObsoleteService
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
        public TaktIsoDocumentObsoleteService(
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
        /// 获取ISO文档作废记录仓储
        /// </summary>
        private ITaktRepository<TaktIsoDocumentObsolete> IsoDocumentObsoleteRepository => _repositoryFactory.GetBusinessRepository<TaktIsoDocumentObsolete>();

        /// <summary>
        /// 获取作废回收记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>作废回收记录分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoDocumentObsoleteDto>> GetListAsync(TaktIsoDocumentObsoleteQueryDto query)
        {
            query ??= new TaktIsoDocumentObsoleteQueryDto();

            _logger.Info($"查询ISO文档作废记录列表，参数：DocumentId={query.DocumentId}, ObsoleteType={query.ObsoleteType}, Status={query.Status}");

            var result = await IsoDocumentObsoleteRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询ISO文档作废记录列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoDocumentObsoleteDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoDocumentObsoleteDto>>()
            };
        }

        /// <summary>
        /// 获取作废回收记录详情
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>作废回收记录详情</returns>
        public async Task<TaktIsoDocumentObsoleteDto> GetByIdAsync(long obsoleteRecycleId)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId);
            return obsolete == null ? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId)) : obsolete.Adapt<TaktIsoDocumentObsoleteDto>();
        }

        /// <summary>
        /// 创建作废回收记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>作废回收记录ID</returns>
        public async Task<long> CreateAsync(TaktIsoDocumentObsoleteCreateDto input)
        {
            // 验证申请编号是否已存在
            if (!string.IsNullOrEmpty(input.ApplicationCode))
                await TaktValidateUtils.ValidateFieldExistsAsync(IsoDocumentObsoleteRepository, "ApplicationCode", input.ApplicationCode);

            var obsolete = input.Adapt<TaktIsoDocumentObsolete>();
            obsolete.ObsoleteDate = DateTime.Now;
            obsolete.ObsoleteBy = _currentUser.UserName ?? string.Empty;
            obsolete.Status = 0; // 已作废
            obsolete.ApplicationDate = DateTime.Now;
            obsolete.ApplicantBy = _currentUser.UserName ?? string.Empty;
            obsolete.ApplicationStatus = 0; // 待审批

            var result = await IsoDocumentObsoleteRepository.CreateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentObsolete.Created"),
                    Content = L("Routine.IsoDocumentObsolete.CreatedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });

                return obsolete.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentObsolete.CreateFailed"));
        }

        /// <summary>
        /// 更新作废回收记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoDocumentObsoleteUpdateDto input)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(input.ObsoleteId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", input.ObsoleteId));

            // 验证申请编号是否已存在
            if (!string.IsNullOrEmpty(input.ApplicationCode) && obsolete.ApplicationCode != input.ApplicationCode)
                await TaktValidateUtils.ValidateFieldExistsAsync(IsoDocumentObsoleteRepository, "ApplicationCode", input.ApplicationCode, input.ObsoleteId);

            input.Adapt(obsolete);
            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.Updated"),
                    Content = L("Routine.IsoDocumentObsolete.UpdatedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除作废回收记录
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long obsoleteRecycleId)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId));

            var result = await IsoDocumentObsoleteRepository.DeleteAsync(obsoleteRecycleId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.Deleted"),
                    Content = L("Routine.IsoDocumentObsolete.DeletedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除作废回收记录
        /// </summary>
        /// <param name="obsoleteRecycleIds">作废回收记录ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] obsoleteRecycleIds)
        {
            if (obsoleteRecycleIds == null || obsoleteRecycleIds.Length == 0) return false;

            // 获取要删除的作废记录信息用于通知
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => obsoleteRecycleIds.Contains(x.Id));

            var result = await IsoDocumentObsoleteRepository.DeleteRangeAsync(obsoleteRecycleIds.Cast<object>().ToList());

            if (result > 0 && obsoletes.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.BatchDeleted"),
                    Content = L("Routine.IsoDocumentObsolete.BatchDeletedContent", obsoletes.Count),
                    Timestamp = DateTime.Now,
                    Data = obsoletes
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出作废回收记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentObsoleteQueryDto query, string? sheetName, string? fileName)
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(QueryExpression(query));
            var exportData = obsoletes.Adapt<List<TaktIsoDocumentObsoleteDto>>();
            var actualSheetName = sheetName ?? "作废回收记录";
            var actualFileName = fileName ?? "作废回收记录数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取作废回收记录选项列表
        /// </summary>
        /// <returns>作废回收记录选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.Status == 0); // 已作废
            return obsoletes.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.DocumentVersion} ({x.ObsoleteDate:yyyy-MM-dd})",
                DictValue = x.Id,
                ExtLabel = x.DocumentVersion,
                ExtValue = x.ObsoleteBy,
                OrderNum = 0,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// 根据ISO文档ID获取作废回收记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>作废回收记录列表</returns>
        public async Task<List<TaktIsoDocumentObsoleteDto>> GetByDocumentIdAsync(long documentId)
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.DocumentId == documentId);
            return obsoletes.OrderByDescending(x => x.ObsoleteDate).Adapt<List<TaktIsoDocumentObsoleteDto>>();
        }

        /// <summary>
        /// 作废文档
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="obsoleteType">作废类型</param>
        /// <param name="obsoleteReason">作废原因</param>
        /// <param name="replacementDocumentId">替代文档ID</param>
        /// <param name="replacementDocumentVersion">替代文档版本号</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ObsoleteDocumentAsync(long documentId, string documentVersion, int obsoleteType, string obsoleteReason, long? replacementDocumentId, string? replacementDocumentVersion)
        {
            var obsolete = new TaktIsoDocumentObsolete
            {
                DocumentId = documentId,
                DocumentVersion = documentVersion,
                ObsoleteType = obsoleteType,
                ObsoleteReason = obsoleteReason,
                ObsoleteDate = DateTime.Now,
                ObsoleteBy = _currentUser.UserName ?? string.Empty,
                Status = 0, // 已作废
                ReplacementDocumentId = replacementDocumentId,
                ReplacementDocumentVersion = replacementDocumentVersion,
                ApplicationCode = GenerateApplicationCode(),
                ApplicationDate = DateTime.Now,
                ApplicantBy = _currentUser.UserName ?? string.Empty,
                ApplicationReason = obsoleteReason,
                ApplicationStatus = 0 // 待审批
            };

            var result = await IsoDocumentObsoleteRepository.CreateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentObsolete.DocumentObsoleted"),
                    Content = L("Routine.IsoDocumentObsolete.DocumentObsoletedContent", documentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 回收文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="recycleMethod">回收方式</param>
        /// <param name="recycleNote">回收说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RecycleDocumentAsync(long obsoleteRecycleId, int recycleMethod, string? recycleNote)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId));

            obsolete.Status = 2; // 已回收
            obsolete.RecycleDate = DateTime.Now;
            obsolete.RecycleBy = _currentUser.UserName ?? string.Empty;
            obsolete.RecycleMethod = recycleMethod;
            obsolete.RecycleNote = recycleNote;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.DocumentRecycled"),
                    Content = L("Routine.IsoDocumentObsolete.DocumentRecycledContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 销毁文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="destroyMethod">销毁方式</param>
        /// <param name="destroyNote">销毁说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DestroyDocumentAsync(long obsoleteRecycleId, int destroyMethod, string? destroyNote)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId));

            obsolete.Status = 3; // 已销毁
            obsolete.DestroyDate = DateTime.Now;
            obsolete.DestroyBy = _currentUser.UserName ?? string.Empty;
            obsolete.DestroyMethod = destroyMethod;
            obsolete.DestroyNote = destroyNote;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.DocumentDestroyed"),
                    Content = L("Routine.IsoDocumentObsolete.DocumentDestroyedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 归档文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="archiveLocation">归档位置</param>
        /// <param name="archiveNote">归档说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ArchiveDocumentAsync(long obsoleteRecycleId, string archiveLocation, string? archiveNote)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId));

            obsolete.Status = 4; // 已归档
            obsolete.ArchiveDate = DateTime.Now;
            obsolete.ArchiveBy = _currentUser.UserName ?? string.Empty;
            obsolete.ArchiveLocation = archiveLocation;
            obsolete.ArchiveNote = archiveNote;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.DocumentArchived"),
                    Content = L("Routine.IsoDocumentObsolete.DocumentArchivedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 通知相关人员
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> NotifyStakeholdersAsync(long obsoleteRecycleId)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecycleId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecycleId));

            obsolete.IsNotified = true;
            obsolete.NotifyDate = DateTime.Now;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentObsolete.StakeholdersNotified"),
                    Content = L("Routine.IsoDocumentObsolete.StakeholdersNotifiedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取待回收文档列表
        /// </summary>
        /// <returns>待回收文档列表</returns>
        public async Task<List<TaktIsoDocumentObsoleteDto>> GetPendingRecycleDocumentsAsync()
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.Status == 1); // 待回收
            return obsoletes.OrderByDescending(x => x.ObsoleteDate).Adapt<List<TaktIsoDocumentObsoleteDto>>();
        }

        /// <summary>
        /// 获取待销毁文档列表
        /// </summary>
        /// <returns>待销毁文档列表</returns>
        public async Task<List<TaktIsoDocumentObsoleteDto>> GetPendingDestroyDocumentsAsync()
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.Status == 2); // 已回收
            return obsoletes.OrderByDescending(x => x.RecycleDate).Adapt<List<TaktIsoDocumentObsoleteDto>>();
        }

        /// <summary>
        /// 获取作废记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoDocumentObsoleteRepository.GetCountAsync();
            var obsoleteCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.Status == 0);
            var pendingRecycleCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.Status == 1);
            var recycledCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.Status == 2);
            var destroyedCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.Status == 3);
            var archivedCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.Status == 4);
            var pendingApprovalCount = await IsoDocumentObsoleteRepository.GetCountAsync(x => x.ApplicationStatus == 0);

            return new
            {
                TotalCount = totalCount,
                ObsoleteCount = obsoleteCount,
                PendingRecycleCount = pendingRecycleCount,
                RecycledCount = recycledCount,
                DestroyedCount = destroyedCount,
                ArchivedCount = archivedCount,
                PendingApprovalCount = pendingApprovalCount
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoDocumentObsolete, bool>> QueryExpression(TaktIsoDocumentObsoleteQueryDto query)
        {
            return Expressionable.Create<TaktIsoDocumentObsolete>()
                .AndIF(query.DocumentId.HasValue, x => x.DocumentId == query.DocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DocumentVersion), x => x.DocumentVersion!.Contains(query.DocumentVersion!))
                .AndIF(query.ObsoleteType.HasValue, x => x.ObsoleteType == query.ObsoleteType!.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ObsoleteBy), x => x.ObsoleteBy!.Contains(query.ObsoleteBy!))
                .AndIF(query.ReplacementDocumentId.HasValue, x => x.ReplacementDocumentId == query.ReplacementDocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ApplicationCode), x => x.ApplicationCode!.Contains(query.ApplicationCode!))
                .AndIF(!string.IsNullOrEmpty(query.ApplicantBy), x => x.ApplicantBy!.Contains(query.ApplicantBy!))
                .AndIF(!string.IsNullOrEmpty(query.ApplicantDept), x => x.ApplicantDept!.Contains(query.ApplicantDept!))
                .AndIF(query.ApplicationStatus.HasValue, x => x.ApplicationStatus == query.ApplicationStatus!.Value)
                .AndIF(query.ApprovalResult.HasValue, x => x.ApprovalResult == query.ApprovalResult!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ApproverBy), x => x.ApproverBy!.Contains(query.ApproverBy!))
                .AndIF(query.IsNotified.HasValue, x => x.IsNotified == query.IsNotified!.Value)
                .AndIF(query.StartObsoleteDate.HasValue, x => x.ObsoleteDate >= query.StartObsoleteDate!.Value)
                .AndIF(query.EndObsoleteDate.HasValue, x => x.ObsoleteDate <= query.EndObsoleteDate!.Value)
                .AndIF(query.StartApplicationDate.HasValue, x => x.ApplicationDate >= query.StartApplicationDate!.Value)
                .AndIF(query.EndApplicationDate.HasValue, x => x.ApplicationDate <= query.EndApplicationDate!.Value)
                .AndIF(query.StartApprovalDate.HasValue, x => x.ApprovalStartDate >= query.StartApprovalDate!.Value)
                .AndIF(query.EndApprovalDate.HasValue, x => x.ApprovalStartDate <= query.EndApprovalDate!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 提交作废申请
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="obsoleteType">作废类型</param>
        /// <param name="obsoleteReason">作废原因</param>
        /// <param name="replacementDocumentId">替代文档ID</param>
        /// <param name="replacementDocumentVersion">替代文档版本号</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SubmitApplicationAsync(long documentId, string documentVersion, int obsoleteType, string obsoleteReason, long? replacementDocumentId, string? replacementDocumentVersion)
        {
            return await ObsoleteDocumentAsync(documentId, documentVersion, obsoleteType, obsoleteReason, replacementDocumentId, replacementDocumentVersion);
        }

        /// <summary>
        /// 审批作废申请
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveApplicationAsync(long obsoleteRecordId, int approvalResult, string? approvalComment)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecordId));

            obsolete.ApprovalResult = approvalResult;
            obsolete.ApprovalComment = approvalComment;
            obsolete.ApprovalCompleteDate = DateTime.Now;
            obsolete.ApproverBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.Approved"),
                    Content = L("Routine.IsoDocumentObsolete.ApprovedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 执行作废
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="obsoleteMethod">作废方式</param>
        /// <param name="obsoleteNote">作废说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ExecuteObsoleteAsync(long obsoleteRecordId, int obsoleteMethod, string? obsoleteNote)
        {
            var obsolete = await IsoDocumentObsoleteRepository.GetByIdAsync(obsoleteRecordId)
                ?? throw new TaktException(L("Routine.IsoDocumentObsolete.NotFound", obsoleteRecordId));

            obsolete.Status = 0; // 已作废
            obsolete.ObsoleteDate = DateTime.Now;
            obsolete.ObsoleteBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoDocumentObsoleteRepository.UpdateAsync(obsolete);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentObsolete.Executed"),
                    Content = L("Routine.IsoDocumentObsolete.ExecutedContent", obsolete.DocumentVersion),
                    Timestamp = DateTime.Now,
                    Data = obsolete
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取待审批的作废申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        public async Task<List<TaktIsoDocumentObsoleteDto>> GetPendingApprovalListAsync()
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.ApplicationStatus == 1);
            return obsoletes.OrderByDescending(x => x.ApplicationDate).Adapt<List<TaktIsoDocumentObsoleteDto>>();
        }

        /// <summary>
        /// 获取我的作废申请列表
        /// </summary>
        /// <param name="applicantId">申请人ID</param>
        /// <returns>我的申请列表</returns>
        public async Task<List<TaktIsoDocumentObsoleteDto>> GetMyApplicationsAsync(long applicantId)
        {
            var obsoletes = await IsoDocumentObsoleteRepository.GetListAsync(x => x.ApplicantBy == _currentUser.UserName);
            return obsoletes.OrderByDescending(x => x.ApplicationDate).Adapt<List<TaktIsoDocumentObsoleteDto>>();
        }

        /// <summary>
        /// 回收作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="recycleMethod">回收方式</param>
        /// <param name="recycleNote">回收说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RecycleObsoleteAsync(long obsoleteRecordId, int recycleMethod, string? recycleNote)
        {
            return await RecycleDocumentAsync(obsoleteRecordId, recycleMethod, recycleNote);
        }

        /// <summary>
        /// 销毁作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="destroyMethod">销毁方式</param>
        /// <param name="destroyNote">销毁说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DestroyObsoleteAsync(long obsoleteRecordId, int destroyMethod, string? destroyNote)
        {
            return await DestroyDocumentAsync(obsoleteRecordId, destroyMethod, destroyNote);
        }

        /// <summary>
        /// 归档作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="archiveLocation">归档位置</param>
        /// <param name="archiveNote">归档说明</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ArchiveObsoleteAsync(long obsoleteRecordId, string archiveLocation, string? archiveNote)
        {
            return await ArchiveDocumentAsync(obsoleteRecordId, archiveLocation, archiveNote);
        }

        /// <summary>
        /// 生成申请编号
        /// </summary>
        /// <returns>申请编号</returns>
        private string GenerateApplicationCode()
        {
            return $"OB{DateTime.Now:yyyyMMddHHmmss}{Random.Shared.Next(1000, 9999)}";
        }
    }
}





