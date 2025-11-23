#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicParticipantService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题参与者服务接口
//===================================================================




#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicParticipantService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题参与者服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Document;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题参与者服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsTopicParticipantService
    {
        /// <summary>
        /// 获取新闻话题参与者分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题参与者分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetListAsync(TaktNewsTopicParticipantQueryDto? query);

        /// <summary>
        /// 获取新闻话题参与者详情
        /// </summary>
        /// <param name="id">参与者ID</param>
        /// <returns>新闻话题参与者详情</returns>
        Task<TaktNewsTopicParticipantDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻话题参与者
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>参与者ID</returns>
        Task<long> CreateAsync(TaktNewsTopicParticipantCreateDto input);

        /// <summary>
        /// 更新新闻话题参与者
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsTopicParticipantUpdateDto input);

        /// <summary>
        /// 删除新闻话题参与者
        /// </summary>
        /// <param name="id">参与者ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻话题参与者
        /// </summary>
        /// <param name="participantIds">参与者ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] participantIds);

        /// <summary>
        /// 更新参与者状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktNewsTopicParticipantStatusDto input);

        /// <summary>
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <param name="participationType">参与类型</param>
        /// <returns>是否成功</returns>
        Task<bool> JoinTopicAsync(long topicId, long userId, string userName, string? userAvatar = null, int participationType = 1);

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
        /// 获取话题参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetTopicParticipantsAsync(long topicId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题参与者分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetUserJoinedTopicsAsync(long userId, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 更新用户活跃时间
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateUserActiveTimeAsync(long topicId, long userId);

        /// <summary>
        /// 增加用户贡献分数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="score">增加的分数</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseContributionScoreAsync(long topicId, long userId, int score = 1);

        /// <summary>
        /// 增加用户内容数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseContentCountAsync(long topicId, long userId);

        /// <summary>
        /// 增加用户评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseCommentCountAsync(long topicId, long userId);

        /// <summary>
        /// 增加用户点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseLikeCountAsync(long topicId, long userId);

        /// <summary>
        /// 增加用户分享数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseShareCountAsync(long topicId, long userId);

        /// <summary>
        /// 设置用户通知设置
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="receiveNotification">是否接收通知</param>
        /// <param name="notificationType">通知类型</param>
        /// <returns>是否成功</returns>
        Task<bool> SetNotificationSettingsAsync(long topicId, long userId, bool receiveNotification, int notificationType = 1);

        /// <summary>
        /// 获取话题活跃用户列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>活跃用户列表</returns>
        Task<List<TaktNewsTopicParticipantDto>> GetActiveUsersAsync(long topicId, int count = 10);

        /// <summary>
        /// 获取话题贡献者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>贡献者列表</returns>
        Task<List<TaktNewsTopicParticipantDto>> GetTopContributorsAsync(long topicId, int count = 10);

        /// <summary>
        /// 获取用户参与话题统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>统计信息</returns>
        Task<object> GetUserParticipationStatisticsAsync(long userId);

        /// <summary>
        /// 获取话题参与统计
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>统计信息</returns>
        Task<object> GetTopicParticipationStatisticsAsync(long topicId);

        /// <summary>
        /// 搜索参与者
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicParticipantDto>> SearchParticipantsAsync(string keyword, int pageIndex = 1, int pageSize = 20);

        /// <summary>
        /// 获取参与者统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        Task<object> GetParticipantStatisticsAsync();
    }
} 



