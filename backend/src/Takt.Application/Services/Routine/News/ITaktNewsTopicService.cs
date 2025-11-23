#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题服务接口
//===================================================================




#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Document;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsTopicService
    {
        /// <summary>
        /// 获取新闻话题分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicDto>> GetListAsync(TaktNewsTopicQueryDto? query);

        /// <summary>
        /// 获取新闻话题详情
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>新闻话题详情</returns>
        Task<TaktNewsTopicDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻话题
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>话题ID</returns>
        Task<long> CreateAsync(TaktNewsTopicCreateDto input);

        /// <summary>
        /// 更新新闻话题
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsTopicUpdateDto input);

        /// <summary>
        /// 删除新闻话题
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻话题
        /// </summary>
        /// <param name="topicIds">话题ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] topicIds);

        /// <summary>
        /// 更新话题状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktNewsTopicStatusDto input);

        /// <summary>
        /// 获取热门话题列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门话题列表</returns>
        Task<List<TaktNewsTopicDto>> GetHotTopicsAsync(int count = 10);

        /// <summary>
        /// 获取推荐话题列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐话题列表</returns>
        Task<List<TaktNewsTopicDto>> GetRecommendedTopicsAsync(int count = 10);

        /// <summary>
        /// 获取话题下的新闻列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>新闻分页列表</returns>
        Task<TaktPagedResult<TaktNewsDto>> GetNewsByTopicIdAsync(long topicId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取话题参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetTopicParticipantsAsync(long topicId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        Task<bool> JoinTopicAsync(long topicId, long userId, string userName, string? userAvatar = null);

        /// <summary>
        /// 用户退出话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> LeaveTopicAsync(long topicId, long userId);

        /// <summary>
        /// 检查用户是否已参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已参与</returns>
        Task<bool> IsUserJoinedTopicAsync(long topicId, long userId);

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicDto>> GetUserJoinedTopicsAsync(long userId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 增加话题参与人数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseParticipantCountAsync(long topicId);

        /// <summary>
        /// 减少话题参与人数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DecreaseParticipantCountAsync(long topicId);

        /// <summary>
        /// 增加话题新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseNewsCountAsync(long topicId);

        /// <summary>
        /// 减少话题新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DecreaseNewsCountAsync(long topicId);

        /// <summary>
        /// 增加话题评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseCommentCountAsync(long topicId);

        /// <summary>
        /// 减少话题评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DecreaseCommentCountAsync(long topicId);

        /// <summary>
        /// 增加话题点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseLikeCountAsync(long topicId);

        /// <summary>
        /// 减少话题点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DecreaseLikeCountAsync(long topicId);

        /// <summary>
        /// 增加话题分享数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseShareCountAsync(long topicId);

        /// <summary>
        /// 增加话题阅读数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseReadCountAsync(long topicId);

        /// <summary>
        /// 搜索话题
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicDto>> SearchTopicsAsync(string keyword, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取话题统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetTopicStatisticsAsync();
    }
} 



