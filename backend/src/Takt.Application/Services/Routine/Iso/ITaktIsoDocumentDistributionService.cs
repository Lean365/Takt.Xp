#nullable enable

using Takt.Application.Dtos.Routine.Iso;
using Takt.Shared.Models;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoDocumentDistributionService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档分发记录服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文档分发记录服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoDocumentDistributionService
    {
        /// <summary>
        /// 获取分发记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分发记录分页列表</returns>
        Task<TaktPagedResult<TaktIsoDocumentDistributionDto>> GetListAsync(TaktIsoDocumentDistributionQueryDto query);

        /// <summary>
        /// 获取分发记录详情
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>分发记录详情</returns>
        Task<TaktIsoDocumentDistributionDto> GetByIdAsync(long distributionRecordId);

        /// <summary>
        /// 创建分发记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>分发记录ID</returns>
        Task<long> CreateAsync(TaktIsoDocumentDistributionCreateDto input);

        /// <summary>
        /// 更新分发记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoDocumentDistributionUpdateDto input);

        /// <summary>
        /// 删除分发记录
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long distributionRecordId);

        /// <summary>
        /// 批量删除分发记录
        /// </summary>
        /// <param name="distributionRecordIds">分发记录ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] distributionRecordIds);


        /// <summary>
        /// 导出分发记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentDistributionQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取分发记录选项列表
        /// </summary>
        /// <returns>分发记录选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 根据ISO文档ID获取分发记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>分发记录列表</returns>
        Task<List<TaktIsoDocumentDistributionDto>> GetByDocumentIdAsync(long documentId);

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
        Task<(int success, int fail)> BatchCreateAsync(long documentId, string documentVersion, long[] userIds, int distributionType, int distributionMethod, DateTime? expireDate, bool isForced);

        /// <summary>
        /// 接收文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="receiveNote">接收说明</param>
        /// <returns>是否成功</returns>
        Task<bool> ReceiveAsync(long distributionRecordId, string? receiveNote);

        /// <summary>
        /// 确认文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> ConfirmAsync(long distributionRecordId);

        /// <summary>
        /// 拒绝文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="rejectReason">拒绝原因</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long distributionRecordId, string rejectReason);

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> MarkAsReadAsync(long distributionRecordId);

        /// <summary>
        /// 获取用户未读分发记录数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>未读数量</returns>
        Task<int> GetUnreadCountAsync(long userId);

        /// <summary>
        /// 获取分发记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();
    }
}





