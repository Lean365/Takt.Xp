#nullable enable

using Takt.Application.Dtos.Routine.Iso;
using Takt.Shared.Models;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktIsoDocumentAccessService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文件查阅记录服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO文件查阅记录服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktIsoDocumentAccessService
    {
        /// <summary>
        /// 获取文件查阅记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件查阅记录分页列表</returns>
        Task<TaktPagedResult<TaktIsoDocumentAccessDto>> GetListAsync(TaktIsoDocumentAccessQueryDto query);

        /// <summary>
        /// 获取文件查阅记录详情
        /// </summary>
        /// <param name="accessRecordId">文件查阅记录ID</param>
        /// <returns>文件查阅记录详情</returns>
        Task<TaktIsoDocumentAccessDto> GetByIdAsync(long accessRecordId);

        /// <summary>
        /// 创建文件查阅记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件查阅记录ID</returns>
        Task<long> CreateAsync(TaktIsoDocumentAccessCreateDto input);

        /// <summary>
        /// 更新文件查阅记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktIsoDocumentAccessUpdateDto input);

        /// <summary>
        /// 删除文件查阅记录
        /// </summary>
        /// <param name="accessRecordId">文件查阅记录ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long accessRecordId);

        /// <summary>
        /// 批量删除文件查阅记录
        /// </summary>
        /// <param name="accessRecordIds">文件查阅记录ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] accessRecordIds);

        /// <summary>
        /// 导出文件查阅记录数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktIsoDocumentAccessQueryDto query, string? sheetName, string? fileName);

        /// <summary>
        /// 获取文件查阅记录选项列表
        /// </summary>
        /// <returns>文件查阅记录选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 根据ISO文档ID获取查阅记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>查阅记录列表</returns>
        Task<List<TaktIsoDocumentAccessDto>> GetByDocumentIdAsync(long documentId);

        /// <summary>
        /// 记录文件查阅
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="accessType">查阅类型</param>
        /// <param name="accessPurpose">查阅目的</param>
        /// <returns>查阅记录ID</returns>
        Task<long> RecordAccessAsync(long documentId, string documentVersion, int accessType, string? accessPurpose);

        /// <summary>
        /// 更新查阅时长
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="duration">查阅时长（秒）</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAccessDurationAsync(long accessRecordId, int duration);

        /// <summary>
        /// 确认查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="confirmComment">确认意见</param>
        /// <returns>是否成功</returns>
        Task<bool> ConfirmAccessAsync(long accessRecordId, string? confirmComment);

        /// <summary>
        /// 评价查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="rating">评价等级</param>
        /// <param name="ratingComment">评价内容</param>
        /// <returns>是否成功</returns>
        Task<bool> RateAccessAsync(long accessRecordId, int rating, string? ratingComment);

        /// <summary>
        /// 获取用户查阅统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        Task<object> GetUserAccessStatisticsAsync(long userId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 获取文档查阅统计
        /// </summary>
        /// <param name="documentId">文档ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        Task<object> GetDocumentAccessStatisticsAsync(long documentId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// 获取热门文档列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>热门文档列表</returns>
        Task<List<object>> GetPopularDocumentsAsync(int topCount = 10, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// 获取活跃用户列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>活跃用户列表</returns>
        Task<List<object>> GetActiveUsersAsync(int topCount = 10, DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// 获取查阅记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetStatisticsAsync();
    }
}





