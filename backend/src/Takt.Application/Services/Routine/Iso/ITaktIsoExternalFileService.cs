#nullable enable

using Takt.Application.Dtos.Routine.Iso;
using Takt.Shared.Models;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoExternalFileService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO外来文件控制服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO外来文件控制服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoExternalFileService
    {
        /// <summary>
        /// 获取外来文件控制分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>外来文件控制分页列表</returns>
        Task<TaktPagedResult<TaktIsoExternalFileDto>> GetListAsync(TaktIsoExternalFileQueryDto query);

        /// <summary>
        /// 获取外来文件控制详情
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>外来文件控制详情</returns>
        Task<TaktIsoExternalFileDto> GetByIdAsync(long externalFileControlId);

        /// <summary>
        /// 创建外来文件控制
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>外来文件控制ID</returns>
        Task<long> CreateAsync(TaktIsoExternalFileCreateDto input);

        /// <summary>
        /// 更新外来文件控制
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoExternalFileUpdateDto input);

        /// <summary>
        /// 删除外来文件控制
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long externalFileControlId);

        /// <summary>
        /// 批量删除外来文件控制
        /// </summary>
        /// <param name="externalFileControlIds">外来文件控制ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] externalFileControlIds);

        /// <summary>
        /// 导出外来文件控制数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoExternalFileQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取外来文件控制选项列表
        /// </summary>
        /// <returns>外来文件控制选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 审核外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="reviewComment">审核意见</param>
        /// <param name="isApproved">是否通过</param>
        /// <returns>是否成功</returns>
        Task<bool> ReviewAsync(long externalFileControlId, string? reviewComment, bool isApproved);

        /// <summary>
        /// 批准外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="approvalComment">批准意见</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveAsync(long externalFileControlId, string? approvalComment);

        /// <summary>
        /// 拒绝外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="rejectionReason">拒绝原因</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long externalFileControlId, string rejectionReason);

        /// <summary>
        /// 分发外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="distributionScope">分发范围</param>
        /// <returns>是否成功</returns>
        Task<bool> DistributeAsync(long externalFileControlId, string distributionScope);

        /// <summary>
        /// 通知相关人员
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>是否成功</returns>
        Task<bool> NotifyStakeholdersAsync(long externalFileControlId);

        /// <summary>
        /// 更新外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="newFilePath">新文件路径</param>
        /// <param name="newFileSize">新文件大小</param>
        /// <param name="newFileMd5">新文件MD5</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateFileAsync(long externalFileControlId, string newVersion, string newFilePath, long newFileSize, string? newFileMd5);

        /// <summary>
        /// 检查文件更新
        /// </summary>
        /// <returns>需要更新的文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> CheckFileUpdatesAsync();

        /// <summary>
        /// 获取待审核文件列表
        /// </summary>
        /// <returns>待审核文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetPendingReviewFilesAsync();

        /// <summary>
        /// 获取待批准文件列表
        /// </summary>
        /// <returns>待批准文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetPendingApprovalFilesAsync();

        /// <summary>
        /// 获取即将失效文件列表
        /// </summary>
        /// <param name="days">提前天数</param>
        /// <returns>即将失效文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetExpiringFilesAsync(int days = 30);

        /// <summary>
        /// 根据文件代码获取外来文件
        /// </summary>
        /// <param name="fileCode">文件代码</param>
        /// <returns>外来文件详情</returns>
        Task<TaktIsoExternalFileDto> GetByFileCodeAsync(string fileCode);

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="reviewComment">审核意见</param>
        /// <returns>是否成功</returns>
        Task<bool> SubmitReviewAsync(long externalFileControlId, string? reviewComment);

        /// <summary>
        /// 更新文件状态
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(long externalFileControlId, int status);

        /// <summary>
        /// 获取待审核文件列表
        /// </summary>
        /// <returns>待审核文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetPendingReviewListAsync();

        /// <summary>
        /// 获取待审批文件列表
        /// </summary>
        /// <returns>待审批文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetPendingApprovalListAsync();

        /// <summary>
        /// 获取我的外来文件列表
        /// </summary>
        /// <param name="receiverId">接收人ID</param>
        /// <returns>我的外来文件列表</returns>
        Task<List<TaktIsoExternalFileDto>> GetMyFilesAsync(long receiverId);

        /// <summary>
        /// 获取外来文件统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();
    }
}





