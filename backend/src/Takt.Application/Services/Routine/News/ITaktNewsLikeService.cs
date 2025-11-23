#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsLikeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻点赞服务接口
//===================================================================



#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsLikeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻点赞服务接口
//===================================================================

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻点赞服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsLikeService
    {
        /// <summary>
        /// 获取新闻点赞分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻点赞分页列表</returns>
        Task<TaktPagedResult<TaktNewsLikeDto>> GetListAsync(TaktNewsLikeQueryDto? query);

        /// <summary>
        /// 获取新闻点赞详情
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>新闻点赞详情</returns>
        Task<TaktNewsLikeDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻点赞
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>点赞ID</returns>
        Task<long> CreateAsync(TaktNewsLikeCreateDto input);

        /// <summary>
        /// 更新新闻点赞
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsLikeUpdateDto input);

        /// <summary>
        /// 删除新闻点赞
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻点赞
        /// </summary>
        /// <param name="likeIds">点赞ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] likeIds);

        /// <summary>
        /// 用户点赞新闻
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        Task<bool> LikeNewsAsync(long newsId, long userId, string userName, string? userAvatar = null);

        /// <summary>
        /// 用户取消点赞
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> UnlikeNewsAsync(long newsId, long userId);

        /// <summary>
        /// 检查用户是否已点赞
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已点赞</returns>
        Task<bool> IsUserLikedAsync(long newsId, long userId);

        /// <summary>
        /// 获取新闻的点赞用户列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞用户分页列表</returns>
        Task<TaktPagedResult<TaktNewsLikeDto>> GetLikedUsersByNewsIdAsync(long newsId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取用户点赞的新闻列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞新闻分页列表</returns>
        Task<TaktPagedResult<TaktNewsLikeDto>> GetLikedNewsByUserIdAsync(long userId, int pageIndex = 1, int pageSize = 20);
    }
}



