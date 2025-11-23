#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsCommentService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论服务接口
//===================================================================



#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsCommentService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论服务接口
//===================================================================

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻评论服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsCommentService
    {
        /// <summary>
        /// 获取新闻评论分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻评论分页列表</returns>
        Task<TaktPagedResult<TaktNewsCommentDto>> GetListAsync(TaktNewsCommentQueryDto? query);

        /// <summary>
        /// 获取新闻评论详情
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>新闻评论详情</returns>
        Task<TaktNewsCommentDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻评论（包含敏感词过滤和字数限制）
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>评论ID</returns>
        Task<long> CreateAsync(TaktNewsCommentCreateDto input);

        /// <summary>
        /// 更新新闻评论
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsCommentUpdateDto input);

        /// <summary>
        /// 删除新闻评论
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻评论
        /// </summary>
        /// <param name="commentIds">评论ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] commentIds);

        /// <summary>
        /// 获取新闻的评论列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>评论分页列表</returns>
        Task<TaktPagedResult<TaktNewsCommentDto>> GetCommentsByNewsIdAsync(long newsId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取评论的回复列表
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>回复分页列表</returns>
        Task<TaktPagedResult<TaktNewsCommentDto>> GetRepliesByCommentIdAsync(long commentId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> AuditAsync(TaktNewsCommentAuditDto input);

        /// <summary>
        /// 批量审核评论
        /// </summary>
        /// <param name="input">批量审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchAuditAsync(TaktNewsCommentBatchAuditDto input);

        /// <summary>
        /// 通过评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>是否成功</returns>
        Task<bool> ApproveAsync(long commentId, string? AuditComments = null);

        /// <summary>
        /// 拒绝评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>是否成功</returns>
        Task<bool> RejectAsync(long commentId, string? AuditComments = null);

        /// <summary>
        /// 获取待审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>待审核评论分页列表</returns>
        Task<TaktPagedResult<TaktNewsCommentDto>> GetPendingAuditListAsync(int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取已审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>已审核评论分页列表</returns>
        Task<TaktPagedResult<TaktNewsCommentDto>> GetAuditedListAsync(int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取评论字数限制信息
        /// </summary>
        /// <returns>字数限制信息</returns>
        (int minLength, int maxLength) GetCommentLengthLimit();

        /// <summary>
        /// 检查评论内容是否符合字数要求
        /// </summary>
        /// <param name="content">评论内容</param>
        /// <returns>检查结果</returns>
        (bool isValid, string message) ValidateCommentContent(string content);
    }
}



