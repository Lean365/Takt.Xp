#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentApprovalService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO文档审批流程服务实现
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
    /// ISO文档审批流程服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoDocumentApprovalService : TaktBaseService, ITaktIsoDocumentApprovalService
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
        public TaktIsoDocumentApprovalService(
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
        /// 获取ISO文档审批记录仓储
        /// </summary>
        private ITaktRepository<TaktIsoDocumentApproval> IsoDocumentApprovalRepository => _repositoryFactory.GetBusinessRepository<TaktIsoDocumentApproval>();

        /// <summary>
        /// 获取审批流程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>审批流程分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoDocumentApprovalDto>> GetListAsync(TaktIsoDocumentApprovalQueryDto query)
        {
            query ??= new TaktIsoDocumentApprovalQueryDto();

            _logger.Info($"查询ISO文档审批流程列表，参数：DocumentId={query.DocumentId}, Step={query.Step}, Status={query.Status}");

            var result = await IsoDocumentApprovalRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询ISO文档审批流程列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoDocumentApprovalDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoDocumentApprovalDto>>()
            };
        }

        /// <summary>
        /// 获取审批流程详情
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <returns>审批流程详情</returns>
        public async Task<TaktIsoDocumentApprovalDto> GetByIdAsync(long approvalProcessId)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId);
            return approval == null ? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId)) : approval.Adapt<TaktIsoDocumentApprovalDto>();
        }

        /// <summary>
        /// 创建审批流程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>审批流程ID</returns>
        public async Task<long> CreateAsync(TaktIsoDocumentApprovalCreateDto input)
        {
            var approval = input.Adapt<TaktIsoDocumentApproval>();
            approval.SubmitDate = DateTime.Now;
            approval.SubmitterBy = _currentUser.UserName ?? string.Empty;
            approval.Status = 0; // 待审批
            approval.ApprovalResult = 0; // 待审批

            var result = await IsoDocumentApprovalRepository.CreateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentApproval.Created"),
                    Content = L("Routine.IsoDocumentApproval.CreatedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });

                return approval.Id;
            }

            throw new TaktException(L("Routine.IsoDocumentApproval.CreateFailed"));
        }

        /// <summary>
        /// 更新审批流程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoDocumentApprovalUpdateDto input)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(input.ApprovalId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", input.ApprovalId));

            input.Adapt(approval);
            var result = await IsoDocumentApprovalRepository.UpdateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Updated"),
                    Content = L("Routine.IsoDocumentApproval.UpdatedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除审批流程
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long approvalProcessId)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId));

            var result = await IsoDocumentApprovalRepository.DeleteAsync(approvalProcessId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Deleted"),
                    Content = L("Routine.IsoDocumentApproval.DeletedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除审批流程
        /// </summary>
        /// <param name="approvalProcessIds">审批流程ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] approvalProcessIds)
        {
            if (approvalProcessIds == null || approvalProcessIds.Length == 0) return false;

            // 获取要删除的审批记录信息用于通知
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => approvalProcessIds.Contains(x.Id));

            var result = await IsoDocumentApprovalRepository.DeleteRangeAsync(approvalProcessIds.Cast<object>().ToList());

            if (result > 0 && approvals.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.BatchDeleted"),
                    Content = L("Routine.IsoDocumentApproval.BatchDeletedContent", approvals.Count),
                    Timestamp = DateTime.Now,
                    Data = approvals
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出审批流程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentApprovalQueryDto query, string? sheetName, string? fileName)
        {
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(QueryExpression(query));
            var exportData = approvals.Adapt<List<TaktIsoDocumentApprovalDto>>();
            var actualSheetName = sheetName ?? "审批流程";
            var actualFileName = fileName ?? "审批流程数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取审批流程选项列表
        /// </summary>
        /// <returns>审批流程选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => x.Status == 1); // 已审批
            return approvals.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.StepName} ({x.DocumentVersion})",
                DictValue = x.Id,
                ExtLabel = x.StepName,
                ExtValue = x.DocumentVersion,
                OrderNum = x.Step,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// 根据ISO文档ID获取审批流程列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>审批流程列表</returns>
        public async Task<List<TaktIsoDocumentApprovalDto>> GetByDocumentIdAsync(long documentId)
        {
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => x.DocumentId == documentId);
            return approvals.OrderBy(x => x.Step).Adapt<List<TaktIsoDocumentApprovalDto>>();
        }

        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="approvalSteps">审批步骤列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SubmitApprovalAsync(long documentId, string documentVersion, List<TaktIsoDocumentApprovalCreateDto> approvalSteps)
        {
            if (approvalSteps == null || !approvalSteps.Any()) return false;

            var approvals = new List<TaktIsoDocumentApproval>();
            long? previousStepId = null;

            for (int i = 0; i < approvalSteps.Count; i++)
            {
                var step = approvalSteps[i];
                var approval = step.Adapt<TaktIsoDocumentApproval>();
                approval.DocumentId = documentId;
                approval.DocumentVersion = documentVersion;
                approval.Step = i + 1;
                approval.SubmitDate = DateTime.Now;
                approval.SubmitterBy = _currentUser.UserName ?? string.Empty;
                approval.Status = 0; // 待审批
                approval.ApprovalResult = 0; // 待审批
                approval.PreviousStepId = previousStepId;

                if (i < approvalSteps.Count - 1)
                {
                    // 为下一步设置ID（这里先创建临时ID，后续更新）
                    approval.NextStepId = null; // 将在创建后更新
                }

                approvals.Add(approval);
                previousStepId = approval.Id; // 临时ID，将在创建后更新
            }

            // 批量创建审批记录
            var result = await IsoDocumentApprovalRepository.CreateRangeAsync(approvals);

            if (result > 0)
            {
                // 更新下一步骤ID
                for (int i = 0; i < approvals.Count - 1; i++)
                {
                    approvals[i].NextStepId = approvals[i + 1].Id;
                    await IsoDocumentApprovalRepository.UpdateAsync(approvals[i]);
                }

                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoDocumentApproval.Submitted"),
                    Content = L("Routine.IsoDocumentApproval.SubmittedContent", documentVersion, approvals.Count),
                    Timestamp = DateTime.Now,
                    Data = approvals
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveAsync(long approvalProcessId, string? approvalComment, string? approvalAttachment)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId));

            approval.Status = 1; // 已审批
            approval.ApprovalResult = 1; // 通过
            approval.ApprovalComments = approvalComment;
            approval.ApprovalAttachment = approvalAttachment;
            approval.ApprovalDate = DateTime.Now;

            var result = await IsoDocumentApprovalRepository.UpdateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Approved"),
                    Content = L("Routine.IsoDocumentApproval.ApprovedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RejectAsync(long approvalProcessId, string? approvalComment, string? approvalAttachment)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId));

            approval.Status = 2; // 已拒绝
            approval.ApprovalResult = 2; // 驳回
            approval.ApprovalComments = approvalComment;
            approval.ApprovalAttachment = approvalAttachment;
            approval.ApprovalDate = DateTime.Now;

            var result = await IsoDocumentApprovalRepository.UpdateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Rejected"),
                    Content = L("Routine.IsoDocumentApproval.RejectedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 跳过审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SkipAsync(long approvalProcessId, string? approvalComment)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId));

            approval.Status = 3; // 已跳过
            approval.ApprovalResult = 3; // 跳过
            approval.ApprovalComments = approvalComment;
            approval.ApprovalDate = DateTime.Now;

            var result = await IsoDocumentApprovalRepository.UpdateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Skipped"),
                    Content = L("Routine.IsoDocumentApproval.SkippedContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 撤回审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="reason">撤回原因</param>
        /// <returns>是否成功</returns>
        public async Task<bool> WithdrawAsync(long approvalProcessId, string? reason)
        {
            var approval = await IsoDocumentApprovalRepository.GetByIdAsync(approvalProcessId)
                ?? throw new TaktException(L("Routine.IsoDocumentApproval.NotFound", approvalProcessId));

            approval.Status = 4; // 已撤回
            approval.ApprovalResult = 4; // 撤回
            approval.ApprovalComments = reason;
            approval.ApprovalDate = DateTime.Now;

            var result = await IsoDocumentApprovalRepository.UpdateAsync(approval);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoDocumentApproval.Withdrawn"),
                    Content = L("Routine.IsoDocumentApproval.WithdrawnContent", approval.StepName),
                    Timestamp = DateTime.Now,
                    Data = approval
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取待审批列表
        /// </summary>
        /// <param name="approverId">审批人ID</param>
        /// <returns>待审批列表</returns>
        public async Task<List<TaktIsoDocumentApprovalDto>> GetPendingApprovalsAsync(long approverId)
        {
            // 这里假设approverId实际上是审批人用户名
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => 
                x.ApproverBy == approverId.ToString() && 
                x.Status == 0); // 待审批

            return approvals.OrderBy(x => x.Step).Adapt<List<TaktIsoDocumentApprovalDto>>();
        }

        /// <summary>
        /// 获取超时审批列表
        /// </summary>
        /// <returns>超时审批列表</returns>
        public async Task<List<TaktIsoDocumentApprovalDto>> GetTimeoutApprovalsAsync()
        {
            var now = DateTime.Now;
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => 
                x.Status == 0 && // 待审批
                x.TimeLimitHours.HasValue &&
                x.SubmitDate.AddHours(x.TimeLimitHours.Value) < now);

            return approvals.OrderBy(x => x.SubmitDate).Adapt<List<TaktIsoDocumentApprovalDto>>();
        }

        /// <summary>
        /// 检查审批流程是否完成
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <returns>是否完成</returns>
        public async Task<bool> IsApprovalCompletedAsync(long documentId, string documentVersion)
        {
            var approvals = await IsoDocumentApprovalRepository.GetListAsync(x => 
                x.DocumentId == documentId && 
                x.DocumentVersion == documentVersion);

            if (!approvals.Any()) return false;

            // 检查是否所有必须的审批步骤都已完成
            var requiredApprovals = approvals.Where(x => x.IsRequired).ToList();
            var completedApprovals = requiredApprovals.Where(x => x.Status == 1 && x.ApprovalResult == 1).ToList();

            return requiredApprovals.Count == completedApprovals.Count;
        }

        /// <summary>
        /// 获取审批流程统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoDocumentApprovalRepository.GetCountAsync();
            var pendingCount = await IsoDocumentApprovalRepository.GetCountAsync(x => x.Status == 0);
            var approvedCount = await IsoDocumentApprovalRepository.GetCountAsync(x => x.Status == 1);
            var rejectedCount = await IsoDocumentApprovalRepository.GetCountAsync(x => x.Status == 2);
            var skippedCount = await IsoDocumentApprovalRepository.GetCountAsync(x => x.Status == 3);
            var withdrawnCount = await IsoDocumentApprovalRepository.GetCountAsync(x => x.Status == 4);
            var timeoutCount = await GetTimeoutApprovalsAsync();

            return new
            {
                TotalCount = totalCount,
                PendingCount = pendingCount,
                ApprovedCount = approvedCount,
                RejectedCount = rejectedCount,
                SkippedCount = skippedCount,
                WithdrawnCount = withdrawnCount,
                TimeoutCount = timeoutCount.Count
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoDocumentApproval, bool>> QueryExpression(TaktIsoDocumentApprovalQueryDto query)
        {
            return Expressionable.Create<TaktIsoDocumentApproval>()
                .AndIF(query.DocumentId.HasValue, x => x.DocumentId == query.DocumentId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.DocumentVersion), x => x.DocumentVersion!.Contains(query.DocumentVersion!))
                .AndIF(query.Step.HasValue, x => x.Step == query.Step!.Value)
                .AndIF(query.ApprovalStep.HasValue, x => x.ApprovalStep == query.ApprovalStep!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ApproverBy), x => x.ApproverBy!.Contains(query.ApproverBy!))
                .AndIF(!string.IsNullOrEmpty(query.ApproverDept), x => x.ApproverDept!.Contains(query.ApproverDept!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(query.ApprovalResult.HasValue, x => x.ApprovalResult == query.ApprovalResult!.Value)
                .AndIF(!string.IsNullOrEmpty(query.SubmitterBy), x => x.SubmitterBy!.Contains(query.SubmitterBy!))
                .AndIF(query.IsRequired.HasValue, x => x.IsRequired == query.IsRequired!.Value)
                .AndIF(query.IsParallel.HasValue, x => x.IsParallel == query.IsParallel!.Value)
                .AndIF(query.IsTimeout.HasValue, x => 
                    query.IsTimeout!.Value ? 
                    (x.TimeLimitHours.HasValue && x.SubmitDate.AddHours(x.TimeLimitHours.Value) < DateTime.Now) :
                    (!x.TimeLimitHours.HasValue || x.SubmitDate.AddHours(x.TimeLimitHours.Value) >= DateTime.Now))
                .AndIF(query.StartApprovalDate.HasValue, x => x.ApprovalDate >= query.StartApprovalDate!.Value)
                .AndIF(query.EndApprovalDate.HasValue, x => x.ApprovalDate <= query.EndApprovalDate!.Value)
                .AndIF(query.StartSubmitDate.HasValue, x => x.SubmitDate >= query.StartSubmitDate!.Value)
                .AndIF(query.EndSubmitDate.HasValue, x => x.SubmitDate <= query.EndSubmitDate!.Value)
                .ToExpression();
        }
    }
}





