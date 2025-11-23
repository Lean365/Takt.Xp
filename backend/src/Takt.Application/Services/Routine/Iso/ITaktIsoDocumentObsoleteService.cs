#nullable enable

using Takt.Application.Dtos.Routine.Iso;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoDocumentObsoleteService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档作废与回收记录服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文档作废与回收记录服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoDocumentObsoleteService
    {
        /// <summary>
        /// 获取作废回收记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>作废回收记录分页列表</returns>
        Task<TaktPagedResult<TaktIsoDocumentObsoleteDto>> GetListAsync(TaktIsoDocumentObsoleteQueryDto query);

        /// <summary>
        /// 获取作废回收记录详情
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>作废回收记录详情</returns>
        Task<TaktIsoDocumentObsoleteDto> GetByIdAsync(long obsoleteRecycleId);

        /// <summary>
        /// 创建作废回收记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>作废回收记录ID</returns>
        Task<long> CreateAsync(TaktIsoDocumentObsoleteCreateDto input);

        /// <summary>
        /// 更新作废回收记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoDocumentObsoleteUpdateDto input);

        /// <summary>
        /// 删除作废回收记录
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long obsoleteRecycleId);

        /// <summary>
        /// 批量删除作废回收记录
        /// </summary>
        /// <param name="obsoleteRecycleIds">作废回收记录ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] obsoleteRecycleIds);


        /// <summary>
        /// 导出作废回收记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentObsoleteQueryDto query, string? sheetName, string? fileName);


        /// <summary>
        /// 获取作废回收记录选项列表
        /// </summary>
        /// <returns>作废回收记录选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 根据ISO文档ID获取作废回收记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>作废回收记录列表</returns>
        Task<List<TaktIsoDocumentObsoleteDto>> GetByDocumentIdAsync(long documentId);

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
        Task<bool> ObsoleteDocumentAsync(long documentId, string documentVersion, int obsoleteType, string obsoleteReason, long? replacementDocumentId, string? replacementDocumentVersion);

        /// <summary>
        /// 回收文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="recycleMethod">回收方式</param>
        /// <param name="recycleNote">回收说明</param>
        /// <returns>是否成功</returns>
        Task<bool> RecycleDocumentAsync(long obsoleteRecycleId, int recycleMethod, string? recycleNote);

        /// <summary>
        /// 销毁文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="destroyMethod">销毁方式</param>
        /// <param name="destroyNote">销毁说明</param>
        /// <returns>是否成功</returns>
        Task<bool> DestroyDocumentAsync(long obsoleteRecycleId, int destroyMethod, string? destroyNote);

        /// <summary>
        /// 归档文档
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <param name="archiveLocation">归档位置</param>
        /// <param name="archiveNote">归档说明</param>
        /// <returns>是否成功</returns>
        Task<bool> ArchiveDocumentAsync(long obsoleteRecycleId, string archiveLocation, string? archiveNote);

        /// <summary>
        /// 通知相关人员
        /// </summary>
        /// <param name="obsoleteRecycleId">作废回收记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> NotifyStakeholdersAsync(long obsoleteRecycleId);

        /// <summary>
        /// 获取待回收文档列表
        /// </summary>
        /// <returns>待回收文档列表</returns>
        Task<List<TaktIsoDocumentObsoleteDto>> GetPendingRecycleDocumentsAsync();

        /// <summary>
        /// 获取待销毁文档列表
        /// </summary>
        /// <returns>待销毁文档列表</returns>
        Task<List<TaktIsoDocumentObsoleteDto>> GetPendingDestroyDocumentsAsync();

        /// <summary>
        /// 获取作废记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();

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
        Task<bool> SubmitApplicationAsync(long documentId, string documentVersion, int obsoleteType, string obsoleteReason, long? replacementDocumentId, string? replacementDocumentVersion);

        /// <summary>
        /// 审批作废申请
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveApplicationAsync(long obsoleteRecordId, int approvalResult, string? approvalComment);

        /// <summary>
        /// 执行作废
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="obsoleteMethod">作废方式</param>
        /// <param name="obsoleteNote">作废说明</param>
        /// <returns>是否成功</returns>
        Task<bool> ExecuteObsoleteAsync(long obsoleteRecordId, int obsoleteMethod, string? obsoleteNote);

        /// <summary>
        /// 回收作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="recycleMethod">回收方式</param>
        /// <param name="recycleNote">回收说明</param>
        /// <returns>是否成功</returns>
        Task<bool> RecycleObsoleteAsync(long obsoleteRecordId, int recycleMethod, string? recycleNote);

        /// <summary>
        /// 销毁作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="destroyMethod">销毁方式</param>
        /// <param name="destroyNote">销毁说明</param>
        /// <returns>是否成功</returns>
        Task<bool> DestroyObsoleteAsync(long obsoleteRecordId, int destroyMethod, string? destroyNote);

        /// <summary>
        /// 归档作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="archiveLocation">归档位置</param>
        /// <param name="archiveNote">归档说明</param>
        /// <returns>是否成功</returns>
        Task<bool> ArchiveObsoleteAsync(long obsoleteRecordId, string archiveLocation, string? archiveNote);

        /// <summary>
        /// 获取待审批的作废申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        Task<List<TaktIsoDocumentObsoleteDto>> GetPendingApprovalListAsync();

        /// <summary>
        /// 获取我的作废申请列表
        /// </summary>
        /// <param name="applicantId">申请人ID</param>
        /// <returns>我的申请列表</returns>
        Task<List<TaktIsoDocumentObsoleteDto>> GetMyApplicationsAsync(long applicantId);
    }
}




