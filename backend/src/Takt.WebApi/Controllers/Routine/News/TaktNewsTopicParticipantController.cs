//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsTopicParticipantController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题参与者控制器
//===================================================================

using Takt.Application.Dtos.Routine.News;
using Takt.Application.Services.Routine.News;

namespace Takt.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻话题参与者控制器
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "新闻话题参与者管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktNewsTopicParticipantController : TaktBaseController
    {
        private readonly ITaktNewsTopicParticipantService _participantService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="participantService">话题参与者服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsTopicParticipantController(
            ITaktNewsTopicParticipantService participantService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _participantService = participantService;
        }

        /// <summary>
        /// 获取话题参与者分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>话题参与者分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:news:topic-participant:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktNewsTopicParticipantQueryDto query)
        {
            var result = await _participantService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取话题参与者详情
        /// </summary>
        /// <param name="participantId">参与者ID</param>
        /// <returns>话题参与者详情</returns>
        [HttpGet("{participantId}")]
        public async Task<IActionResult> GetByIdAsync(long participantId)
        {
            var result = await _participantService.GetByIdAsync(participantId);
            return Success(result);
        }

        /// <summary>
        /// 创建话题参与者
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>参与者ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaktNewsTopicParticipantCreateDto input)
        {
            var result = await _participantService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新话题参与者
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktNewsTopicParticipantUpdateDto input)
        {
            var result = await _participantService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除话题参与者
        /// </summary>
        /// <param name="participantId">参与者ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{participantId}")]
        public async Task<IActionResult> DeleteAsync(long participantId)
        {
            var result = await _participantService.DeleteAsync(participantId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除话题参与者
        /// </summary>
        /// <param name="participantIds">参与者ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] participantIds)
        {
            var result = await _participantService.BatchDeleteAsync(participantIds);
            return Success(result);
        }

        /// <summary>
        /// 更新参与者状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktNewsTopicParticipantStatusDto input)
        {
            var result = await _participantService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <param name="participationType">参与类型</param>
        /// <returns>是否成功</returns>
        [HttpPost("join")]
        public async Task<IActionResult> JoinTopicAsync(long topicId, long userId, string userName, string? userAvatar = null, int participationType = 1)
        {
            var result = await _participantService.JoinTopicAsync(topicId, userId, userName, userAvatar, participationType);
            return Success(result);
        }

        /// <summary>
        /// 用户退出话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPost("leave")]
        public async Task<IActionResult> LeaveTopicAsync(long topicId, long userId)
        {
            var result = await _participantService.LeaveTopicAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 检查用户是否已参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已参与</returns>
        [HttpGet("check-joined")]
        public async Task<IActionResult> IsUserJoinedTopicAsync(long topicId, long userId)
        {
            var result = await _participantService.IsUserJoinedTopicAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 获取话题参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        [HttpGet("topic/{topicId}/participants")]
        public async Task<IActionResult> GetTopicParticipantsAsync(long topicId, int pageIndex = 1, int pageSize = 20)
        {
            var result = await _participantService.GetTopicParticipantsAsync(topicId, pageIndex, pageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题参与者分页列表</returns>
        [HttpGet("user/{userId}/topics")]
        public async Task<IActionResult> GetUserJoinedTopicsAsync(long userId, int pageIndex = 1, int pageSize = 20)
        {
            var result = await _participantService.GetUserJoinedTopicsAsync(userId, pageIndex, pageSize);
            return Success(result);
        }

        /// <summary>
        /// 更新用户活跃时间
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("active-time")]
        public async Task<IActionResult> UpdateUserActiveTimeAsync(long topicId, long userId)
        {
            var result = await _participantService.UpdateUserActiveTimeAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 增加用户贡献分数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="score">增加的分数</param>
        /// <returns>是否成功</returns>
        [HttpPut("contribution-score")]
        public async Task<IActionResult> IncreaseContributionScoreAsync(long topicId, long userId, int score = 1)
        {
            var result = await _participantService.IncreaseContributionScoreAsync(topicId, userId, score);
            return Success(result);
        }

        /// <summary>
        /// 增加用户内容数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("content-count")]
        public async Task<IActionResult> IncreaseContentCountAsync(long topicId, long userId)
        {
            var result = await _participantService.IncreaseContentCountAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 增加用户评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("comment-count")]
        public async Task<IActionResult> IncreaseCommentCountAsync(long topicId, long userId)
        {
            var result = await _participantService.IncreaseCommentCountAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 增加用户点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("like-count")]
        public async Task<IActionResult> IncreaseLikeCountAsync(long topicId, long userId)
        {
            var result = await _participantService.IncreaseLikeCountAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 增加用户分享数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("share-count")]
        public async Task<IActionResult> IncreaseShareCountAsync(long topicId, long userId)
        {
            var result = await _participantService.IncreaseShareCountAsync(topicId, userId);
            return Success(result);
        }

        /// <summary>
        /// 设置用户通知设置
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="receiveNotification">是否接收通知</param>
        /// <param name="notificationType">通知类型</param>
        /// <returns>是否成功</returns>
        [HttpPut("notification-settings")]
        public async Task<IActionResult> SetNotificationSettingsAsync(long topicId, long userId, bool receiveNotification, int notificationType = 1)
        {
            var result = await _participantService.SetNotificationSettingsAsync(topicId, userId, receiveNotification, notificationType);
            return Success(result);
        }

        /// <summary>
        /// 获取话题活跃用户列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>活跃用户列表</returns>
        [HttpGet("topic/{topicId}/active-users")]
        public async Task<IActionResult> GetActiveUsersAsync(long topicId, int count = 10)
        {
            var result = await _participantService.GetActiveUsersAsync(topicId, count);
            return Success(result);
        }

        /// <summary>
        /// 获取话题贡献者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>贡献者列表</returns>
        [HttpGet("topic/{topicId}/top-contributors")]
        public async Task<IActionResult> GetTopContributorsAsync(long topicId, int count = 10)
        {
            var result = await _participantService.GetTopContributorsAsync(topicId, count);
            return Success(result);
        }

        /// <summary>
        /// 获取用户参与话题统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>统计信息</returns>
        [HttpGet("user/{userId}/statistics")]
        public async Task<IActionResult> GetUserParticipationStatisticsAsync(long userId)
        {
            var result = await _participantService.GetUserParticipationStatisticsAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取话题参与统计
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>统计信息</returns>
        [HttpGet("topic/{topicId}/statistics")]
        public async Task<IActionResult> GetTopicParticipationStatisticsAsync(long topicId)
        {
            var result = await _participantService.GetTopicParticipationStatisticsAsync(topicId);
            return Success(result);
        }

        /// <summary>
        /// 搜索参与者
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchParticipantsAsync(string keyword, int pageIndex = 1, int pageSize = 20)
        {
            var result = await _participantService.SearchParticipantsAsync(keyword, pageIndex, pageSize);
            return Success(result);
        }

        /// <summary>
        /// 获取参与者统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        public async Task<IActionResult> GetParticipantStatisticsAsync()
        {
            var result = await _participantService.GetParticipantStatisticsAsync();
            return Success(result);
        }
    }
} 


