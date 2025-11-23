//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentApprovalController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档审批记录控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO文档审批记录控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档审批记录")]
    [ApiController]
    [ApiModule("routine", "ISO文档审批管理")]
    public class TaktIsoDocumentApprovalController : TaktBaseController
    {
        private readonly ITaktIsoDocumentApprovalService _approvalService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentApprovalController(
            ITaktIsoDocumentApprovalService approvalService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _approvalService = approvalService;
        }

        /// <summary>
        /// 获取审批记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>审批记录分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:approval:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoDocumentApprovalQueryDto query)
        {
            var result = await _approvalService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocumentApproval.List.Success"));
        }

        /// <summary>
        /// 获取审批记录详情
        /// </summary>
        /// <param name="id">审批记录ID</param>
        /// <returns>审批记录详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _approvalService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocumentApproval.Get.Success"));
        }

        /// <summary>
        /// 创建审批记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>审批记录ID</returns>
        [HttpPost]
        [TaktLog("创建ISO文档审批记录")]
        [TaktPerm("routine:iso:approval:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoDocumentApprovalCreateDto input)
        {
            var result = await _approvalService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocumentApproval.Insert.Success"));
        }

        /// <summary>
        /// 更新审批记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO文档审批记录")]
        [TaktPerm("routine:iso:approval:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoDocumentApprovalUpdateDto input)
        {
            var result = await _approvalService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocumentApproval.Update.Success"));
        }

        /// <summary>
        /// 删除审批记录
        /// </summary>
        /// <param name="id">审批记录ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO文档审批记录")]
        [TaktPerm("routine:iso:approval:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _approvalService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocumentApproval.Delete.Success"));
        }

        /// <summary>
        /// 批量删除审批记录
        /// </summary>
        /// <param name="ids">审批记录ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:approval:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _approvalService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocumentApproval.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取审批流程选项列表
        /// </summary>
        /// <returns>审批流程选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _approvalService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据ISO文档ID获取审批流程列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>审批流程列表</returns>
        [HttpGet("by-document/{documentId}")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetByDocumentIdAsync(long documentId)
        {
            var result = await _approvalService.GetByDocumentIdAsync(documentId);
            return Success(result);
        }

        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="approvalSteps">审批步骤列表</param>
        /// <returns>是否成功</returns>
        [HttpPost("submit")]
        [TaktLog("提交ISO文档审批")]
        [TaktPerm("routine:iso:approval:create")]
        public async Task<IActionResult> SubmitApprovalAsync(long documentId, string documentVersion, [FromBody] List<TaktIsoDocumentApprovalCreateDto> approvalSteps)
        {
            var result = await _approvalService.SubmitApprovalAsync(documentId, documentVersion, approvalSteps);
            return Success(result, _localization.L("IsoDocumentApproval.Submit.Success"));
        }

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        [HttpPut("{approvalProcessId}/approve")]
        [TaktLog("审批通过ISO文档")]
        [TaktPerm("routine:iso:approval:update")]
        public async Task<IActionResult> ApproveAsync(long approvalProcessId, [FromQuery] string? approvalComment, [FromQuery] string? approvalAttachment)
        {
            var result = await _approvalService.ApproveAsync(approvalProcessId, approvalComment, approvalAttachment);
            return Success(result, _localization.L("IsoDocumentApproval.Approve.Success"));
        }

        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        [HttpPut("{approvalProcessId}/reject")]
        [TaktLog("审批拒绝ISO文档")]
        [TaktPerm("routine:iso:approval:update")]
        public async Task<IActionResult> RejectAsync(long approvalProcessId, [FromQuery] string? approvalComment, [FromQuery] string? approvalAttachment)
        {
            var result = await _approvalService.RejectAsync(approvalProcessId, approvalComment, approvalAttachment);
            return Success(result, _localization.L("IsoDocumentApproval.Reject.Success"));
        }

        /// <summary>
        /// 跳过审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{approvalProcessId}/skip")]
        [TaktLog("跳过ISO文档审批")]
        [TaktPerm("routine:iso:approval:update")]
        public async Task<IActionResult> SkipAsync(long approvalProcessId, [FromQuery] string? approvalComment)
        {
            var result = await _approvalService.SkipAsync(approvalProcessId, approvalComment);
            return Success(result, _localization.L("IsoDocumentApproval.Skip.Success"));
        }

        /// <summary>
        /// 撤回审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="reason">撤回原因</param>
        /// <returns>是否成功</returns>
        [HttpPut("{approvalProcessId}/withdraw")]
        [TaktLog("撤回ISO文档审批")]
        [TaktPerm("routine:iso:approval:update")]
        public async Task<IActionResult> WithdrawAsync(long approvalProcessId, [FromQuery] string? reason)
        {
            var result = await _approvalService.WithdrawAsync(approvalProcessId, reason);
            return Success(result, _localization.L("IsoDocumentApproval.Withdraw.Success"));
        }

        /// <summary>
        /// 获取待审批列表
        /// </summary>
        /// <param name="approverId">审批人ID</param>
        /// <returns>待审批列表</returns>
        [HttpGet("pending/{approverId}")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetPendingApprovalsAsync(long approverId)
        {
            var result = await _approvalService.GetPendingApprovalsAsync(approverId);
            return Success(result);
        }

        /// <summary>
        /// 获取超时审批列表
        /// </summary>
        /// <returns>超时审批列表</returns>
        [HttpGet("timeout")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetTimeoutApprovalsAsync()
        {
            var result = await _approvalService.GetTimeoutApprovalsAsync();
            return Success(result);
        }

        /// <summary>
        /// 检查审批流程是否完成
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <returns>是否完成</returns>
        [HttpGet("check-completion")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> IsApprovalCompletedAsync([FromQuery] long documentId, [FromQuery] string documentVersion)
        {
            var result = await _approvalService.IsApprovalCompletedAsync(documentId, documentVersion);
            return Success(result);
        }

        /// <summary>
        /// 获取审批记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:approval:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _approvalService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




