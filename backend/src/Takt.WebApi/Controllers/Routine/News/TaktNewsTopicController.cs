//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsTopicController.cs
// 创建者 : Takt(Claude AI)
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 新闻话题控制器
//===================================================================

namespace Takt.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻话题控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "新闻话题")]
    [ApiController]
    [ApiModule("routine", "新闻管理")]
    public class TaktNewsTopicController : TaktBaseController
    {
        private readonly ITaktNewsTopicService _topicService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="topicService">话题服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsTopicController(
            ITaktNewsTopicService topicService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _topicService = topicService;
        }

        /// <summary>
        /// 获取话题分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>话题分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:news:topic:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktNewsTopicQueryDto query)
        {
            var result = await _topicService.GetListAsync(query);
            return Success(result, _localization.L("NewsTopic.List.Success"));
        }

        /// <summary>
        /// 获取话题详情
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>话题详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _topicService.GetByIdAsync(id);
            return Success(result, _localization.L("NewsTopic.Get.Success"));
        }

        /// <summary>
        /// 创建话题
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>话题ID</returns>
        [HttpPost]
        [TaktLog("创建新闻话题")]
        [TaktPerm("routine:news:topic:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktNewsTopicCreateDto input)
        {
            var result = await _topicService.CreateAsync(input);
            return Success(result, _localization.L("NewsTopic.Insert.Success"));
        }

        /// <summary>
        /// 更新话题
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新新闻话题")]
        [TaktPerm("routine:news:topic:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktNewsTopicUpdateDto input)
        {
            var result = await _topicService.UpdateAsync(input);
            return Success(result, _localization.L("NewsTopic.Update.Success"));
        }

        /// <summary>
        /// 删除话题
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除新闻话题")]
        [TaktPerm("routine:news:topic:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _topicService.DeleteAsync(id);
            return Success(result, _localization.L("NewsTopic.Delete.Success"));
        }

        /// <summary>
        /// 批量删除话题
        /// </summary>
        /// <param name="ids">话题ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:news:topic:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _topicService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("NewsTopic.BatchDelete.Success"));
        }

        /// <summary>
        /// 更新话题状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新话题状态")]
        [TaktPerm("routine:news:topic:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktNewsTopicStatusDto input)
        {
            var result = await _topicService.UpdateStatusAsync(input);
            return Success(result, _localization.L("NewsTopic.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取热门话题
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门话题列表</returns>
        [HttpGet("hot")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetHotTopicsAsync([FromQuery] int count = 10)
        {
            var result = await _topicService.GetHotTopicsAsync(count);
            return Success(result, _localization.L("NewsTopic.Hot.Success"));
        }

        /// <summary>
        /// 获取推荐话题
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐话题列表</returns>
        [HttpGet("recommended")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetRecommendedTopicsAsync([FromQuery] int count = 10)
        {
            var result = await _topicService.GetRecommendedTopicsAsync(count);
            return Success(result, _localization.L("NewsTopic.Recommended.Success"));
        }

        /// <summary>
        /// 获取话题下的新闻列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>新闻分页列表</returns>
        [HttpGet("{topicId}/news")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetNewsByTopicIdAsync(long topicId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _topicService.GetNewsByTopicIdAsync(topicId, pageIndex, pageSize);
            return Success(result, _localization.L("NewsTopic.GetNews.Success"));
        }

        /// <summary>
        /// 获取话题参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        [HttpGet("{topicId}/participants")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetTopicParticipantsAsync(long topicId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _topicService.GetTopicParticipantsAsync(topicId, pageIndex, pageSize);
            return Success(result, _localization.L("NewsTopic.GetParticipants.Success"));
        }

        /// <summary>
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        [HttpPost("{topicId}/join")]
        [TaktLog("用户参与话题")]
        [TaktPerm("routine:news:topic:join")]
        public async Task<IActionResult> JoinTopicAsync(long topicId, [FromQuery] long userId, [FromQuery] string userName, [FromQuery] string? userAvatar = null)
        {
            var result = await _topicService.JoinTopicAsync(topicId, userId, userName, userAvatar);
            return Success(result, _localization.L("NewsTopic.Join.Success"));
        }

        /// <summary>
        /// 用户退出话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{topicId}/leave")]
        [TaktLog("用户退出话题")]
        [TaktPerm("routine:news:topic:join")]
        public async Task<IActionResult> LeaveTopicAsync(long topicId, [FromQuery] long userId)
        {
            var result = await _topicService.LeaveTopicAsync(topicId, userId);
            return Success(result, _localization.L("NewsTopic.Leave.Success"));
        }

        /// <summary>
        /// 检查用户是否已参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已参与</returns>
        [HttpGet("{topicId}/check-joined")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> IsUserJoinedTopicAsync(long topicId, [FromQuery] long userId)
        {
            var result = await _topicService.IsUserJoinedTopicAsync(topicId, userId);
            return Success(result, _localization.L("NewsTopic.CheckJoined.Success"));
        }

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        [HttpGet("user/{userId}/joined")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetUserJoinedTopicsAsync(long userId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _topicService.GetUserJoinedTopicsAsync(userId, pageIndex, pageSize);
            return Success(result, _localization.L("NewsTopic.GetUserJoined.Success"));
        }

        /// <summary>
        /// 搜索话题
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        [HttpGet("search")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> SearchTopicsAsync([FromQuery] string keyword, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _topicService.SearchTopicsAsync(keyword, pageIndex, pageSize);
            return Success(result, _localization.L("NewsTopic.Search.Success"));
        }

        /// <summary>
        /// 获取话题统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:news:topic:query")]
        public async Task<IActionResult> GetTopicStatisticsAsync()
        {
            var result = await _topicService.GetTopicStatisticsAsync();
            return Success(result, _localization.L("NewsTopic.Statistics.Success"));
        }
    }
}



