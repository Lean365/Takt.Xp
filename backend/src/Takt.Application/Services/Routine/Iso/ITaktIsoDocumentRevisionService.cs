#nullable enable

using Takt.Application.Dtos.Routine.Iso;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoDocumentRevisionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档修订历史服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文档修订历史服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoDocumentRevisionService
    {
        /// <summary>
        /// 获取修订历史分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>修订历史分页列表</returns>
        Task<TaktPagedResult<TaktIsoDocumentRevisionDto>> GetListAsync(TaktIsoDocumentRevisionQueryDto query);

        /// <summary>
        /// 获取修订历史详情
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>修订历史详情</returns>
        Task<TaktIsoDocumentRevisionDto> GetByIdAsync(long revisionHistoryId);

        /// <summary>
        /// 创建修订历史
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>修订历史ID</returns>
        Task<long> CreateAsync(TaktIsoDocumentRevisionCreateDto input);

        /// <summary>
        /// 更新修订历史
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoDocumentRevisionUpdateDto input);

        /// <summary>
        /// 删除修订历史
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long revisionHistoryId);

        /// <summary>
        /// 批量删除修订历史
        /// </summary>
        /// <param name="revisionHistoryIds">修订历史ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] revisionHistoryIds);

        /// <summary>
        /// 导出修订历史数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentRevisionQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取修订历史选项列表
        /// </summary>
        /// <returns>修订历史选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 根据ISO文档ID获取修订历史列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>修订历史列表</returns>
        Task<List<TaktIsoDocumentRevisionDto>> GetByDocumentIdAsync(long documentId);

        /// <summary>
        /// 设置当前版本
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <returns>是否成功</returns>
        Task<bool> SetCurrentVersionAsync(long revisionHistoryId);

        /// <summary>
        /// 审批修订历史
        /// </summary>
        /// <param name="revisionHistoryId">修订历史ID</param>
        /// <param name="approvalComment">审批意见</param>
        /// <param name="status">审批状态</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveAsync(long revisionHistoryId, string? approvalComment, int status);

        /// <summary>
        /// 提交修订申请
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="version">版本号</param>
        /// <param name="revisionContent">修订内容</param>
        /// <param name="revisionReason">修订原因</param>
        /// <returns>是否成功</returns>
        Task<bool> SubmitRevisionAsync(long documentId, string version, string revisionContent, string revisionReason);

        /// <summary>
        /// 审批修订
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveRevisionAsync(long revisionRecordId, int approvalResult, string? approvalComment);

        /// <summary>
        /// 切换当前版本
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> SwitchCurrentVersionAsync(long revisionRecordId);

        /// <summary>
        /// 获取待审批的修订申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        Task<List<TaktIsoDocumentRevisionDto>> GetPendingApprovalListAsync();

        /// <summary>
        /// 获取我的修订申请列表
        /// </summary>
        /// <param name="reviserId">修订人ID</param>
        /// <returns>我的申请列表</returns>
        Task<List<TaktIsoDocumentRevisionDto>> GetMyApplicationsAsync(long reviserId);

        /// <summary>
        /// 获取修订历史统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();
    }
}




