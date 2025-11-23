#nullable enable

using Takt.Application.Dtos.Routine.Iso;
using Takt.Shared.Models;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoDocumentApprovalService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档审批流程服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文档审批流程服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoDocumentApprovalService
    {
        /// <summary>
        /// 获取审批流程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>审批流程分页列表</returns>
        Task<TaktPagedResult<TaktIsoDocumentApprovalDto>> GetListAsync(TaktIsoDocumentApprovalQueryDto query);

        /// <summary>
        /// 获取审批流程详情
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <returns>审批流程详情</returns>
        Task<TaktIsoDocumentApprovalDto> GetByIdAsync(long approvalProcessId);

        /// <summary>
        /// 创建审批流程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>审批流程ID</returns>
        Task<long> CreateAsync(TaktIsoDocumentApprovalCreateDto input);

        /// <summary>
        /// 更新审批流程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoDocumentApprovalUpdateDto input);

        /// <summary>
        /// 删除审批流程
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long approvalProcessId);

        /// <summary>
        /// 批量删除审批流程
        /// </summary>
        /// <param name="approvalProcessIds">审批流程ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] approvalProcessIds);

        /// <summary>
        /// 导出审批流程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentApprovalQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取审批流程选项列表
        /// </summary>
        /// <returns>审批流程选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 根据ISO文档ID获取审批流程列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>审批流程列表</returns>
        Task<List<TaktIsoDocumentApprovalDto>> GetByDocumentIdAsync(long documentId);

        /// <summary>
        /// 提交审批
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="approvalSteps">审批步骤列表</param>
        /// <returns>是否成功</returns>
        Task<bool> SubmitApprovalAsync(long documentId, string documentVersion, List<TaktIsoDocumentApprovalCreateDto> approvalSteps);

        /// <summary>
        /// 审批通过
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveAsync(long approvalProcessId, string? approvalComment, string? approvalAttachment);

        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="approvalAttachment">审批附件</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long approvalProcessId, string? approvalComment, string? approvalAttachment);

        /// <summary>
        /// 跳过审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        Task<bool> SkipAsync(long approvalProcessId, string? approvalComment);

        /// <summary>
        /// 撤回审批
        /// </summary>
        /// <param name="approvalProcessId">审批流程ID</param>
        /// <param name="reason">撤回原因</param>
        /// <returns>是否成功</returns>
        Task<bool> WithdrawAsync(long approvalProcessId, string? reason);

        /// <summary>
        /// 获取待审批列表
        /// </summary>
        /// <param name="approverId">审批人ID</param>
        /// <returns>待审批列表</returns>
        Task<List<TaktIsoDocumentApprovalDto>> GetPendingApprovalsAsync(long approverId);

        /// <summary>
        /// 获取超时审批列表
        /// </summary>
        /// <returns>超时审批列表</returns>
        Task<List<TaktIsoDocumentApprovalDto>> GetTimeoutApprovalsAsync();

        /// <summary>
        /// 检查审批流程是否完成
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <returns>是否完成</returns>
        Task<bool> IsApprovalCompletedAsync(long documentId, string documentVersion);

        /// <summary>
        /// 获取审批流程统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();
    }
}





