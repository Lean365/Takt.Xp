#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsCommentController.cs
// 创建者 : Takt(Claude AI)
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 新闻评论控制器
//===================================================================

namespace Takt.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻评论控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "新闻评论")]
    [ApiController]
    [ApiModule("routine", "新闻管理")]
    public class TaktNewsCommentController : TaktBaseController
    {
        private readonly ITaktNewsCommentService _commentService;
        private readonly ITaktNewsCommentAuditWorkflowService _auditWorkflowService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commentService">评论服务</param>
        /// <param name="auditWorkflowService">审核工作流服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsCommentController(
            ITaktNewsCommentService commentService,
            ITaktNewsCommentAuditWorkflowService auditWorkflowService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _commentService = commentService;
            _auditWorkflowService = auditWorkflowService;
        }

        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>评论分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:news:comment:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktNewsCommentQueryDto query)
        {
            var result = await _commentService.GetListAsync(query);
            return Success(result, _localization.L("NewsComment.List.Success"));
        }

        /// <summary>
        /// 获取评论详情
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>评论详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:news:comment:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _commentService.GetByIdAsync(id);
            return Success(result, _localization.L("NewsComment.Get.Success"));
        }

        /// <summary>
        /// 创建评论
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>评论ID</returns>
        [HttpPost]
        [TaktLog("创建新闻评论")]
        [TaktPerm("routine:news:comment:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktNewsCommentCreateDto input)
        {
            var result = await _commentService.CreateAsync(input);
            return Success(result, _localization.L("NewsComment.Insert.Success"));
        }

        /// <summary>
        /// 更新评论
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新新闻评论")]
        [TaktPerm("routine:news:comment:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktNewsCommentUpdateDto input)
        {
            var result = await _commentService.UpdateAsync(input);
            return Success(result, _localization.L("NewsComment.Update.Success"));
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除新闻评论")]
        [TaktPerm("routine:news:comment:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _commentService.DeleteAsync(id);
            return Success(result, _localization.L("NewsComment.Delete.Success"));
        }

        /// <summary>
        /// 批量删除评论
        /// </summary>
        /// <param name="ids">评论ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:news:comment:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _commentService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("NewsComment.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取新闻评论列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>评论分页列表</returns>
        [HttpGet("news/{newsId}")]
        [TaktPerm("routine:news:comment:query")]
        public async Task<IActionResult> GetCommentsByNewsIdAsync(long newsId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _commentService.GetCommentsByNewsIdAsync(newsId, pageIndex, pageSize);
            return Success(result, _localization.L("NewsComment.GetByNews.Success"));
        }

        /// <summary>
        /// 获取评论回复列表
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>回复分页列表</returns>
        [HttpGet("{commentId}/replies")]
        [TaktPerm("routine:news:comment:query")]
        public async Task<IActionResult> GetRepliesByCommentIdAsync(long commentId, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _commentService.GetRepliesByCommentIdAsync(commentId, pageIndex, pageSize);
            return Success(result, _localization.L("NewsComment.GetReplies.Success"));
        }

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>审核结果</returns>
        [HttpPost("audit")]
        [TaktLog("审核新闻评论")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> AuditAsync([FromBody] TaktNewsCommentAuditDto input)
        {
            var result = await _commentService.AuditAsync(input);
            return Success(result, _localization.L("NewsComment.Logging.Success"));
        }

        /// <summary>
        /// 批量审核评论
        /// </summary>
        /// <param name="input">批量审核对象</param>
        /// <returns>审核结果</returns>
        [HttpPost("batch-audit")]
        [TaktLog("批量审核新闻评论")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> BatchAuditAsync([FromBody] TaktNewsCommentBatchAuditDto input)
        {
            var result = await _commentService.BatchAuditAsync(input);
            return Success(result, _localization.L("NewsComment.BatchAudit.Success"));
        }

        /// <summary>
        /// 通过评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>审核结果</returns>
        [HttpPost("{commentId}/approve")]
        [TaktLog("通过新闻评论")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> ApproveAsync(long commentId, [FromQuery] string? AuditComments = null)
        {
            var result = await _commentService.ApproveAsync(commentId, AuditComments);
            return Success(result, _localization.L("NewsComment.Approve.Success"));
        }

        /// <summary>
        /// 拒绝评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>审核结果</returns>
        [HttpPost("{commentId}/reject")]
        [TaktLog("拒绝新闻评论")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> RejectAsync(long commentId, [FromQuery] string? AuditComments = null)
        {
            var result = await _commentService.RejectAsync(commentId, AuditComments);
            return Success(result, _localization.L("NewsComment.Reject.Success"));
        }

        /// <summary>
        /// 获取待审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>待审核评论分页列表</returns>
        [HttpGet("pending-audit")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> GetPendingAuditListAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _commentService.GetPendingAuditListAsync(pageIndex, pageSize);
            return Success(result, _localization.L("NewsComment.PendingAudit.Success"));
        }

        /// <summary>
        /// 获取已审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>已审核评论分页列表</returns>
        [HttpGet("audited")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> GetAuditedListAsync([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 20)
        {
            var result = await _commentService.GetAuditedListAsync(pageIndex, pageSize);
            return Success(result, _localization.L("NewsComment.Logginged.Success"));
        }

        /// <summary>
        /// 处理评论审核工作流
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns>处理结果</returns>
        [HttpPost("{commentId}/audit-workflow")]
        [TaktLog("处理评论审核工作流")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> ProcessAuditWorkflowAsync(long commentId)
        {
            var result = await _auditWorkflowService.ProcessCommentAuditWorkflowAsync(commentId);
            return Success(result.Data, _localization.L("NewsComment.LoggingWorkflow.Success"));
        }

        /// <summary>
        /// 获取审核统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("audit-statistics")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> GetAuditStatisticsAsync()
        {
            var result = await _auditWorkflowService.GetAuditStatisticsAsync();
            return Success(result, _localization.L("NewsComment.LoggingStatistics.Success"));
        }

        /// <summary>
        /// 获取审核员工作量统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>审核员工作量统计</returns>
        [HttpGet("auditor-workload")]
        [TaktPerm("routine:news:comment:audit")]
        public async Task<IActionResult> GetAuditorWorkloadAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var result = await _auditWorkflowService.GetAuditorWorkloadAsync(startDate, endDate);
            return Success(result, _localization.L("NewsComment.LoggingorWorkload.Success"));
        }

        /// <summary>
        /// 获取评论字数限制
        /// </summary>
        /// <returns>字数限制信息</returns>
        [HttpGet("length-limit")]
        [TaktPerm("routine:news:comment:query")]
        public IActionResult GetCommentLengthLimitAsync()
        {
            var result = _commentService.GetCommentLengthLimit();
            return Success(new { MinLength = result.minLength, MaxLength = result.maxLength }, _localization.L("NewsComment.LengthLimit.Success"));
        }

        /// <summary>
        /// 验证评论内容
        /// </summary>
        /// <param name="content">评论内容</param>
        /// <returns>验证结果</returns>
        [HttpPost("validate-content")]
        [TaktPerm("routine:news:comment:query")]
        public IActionResult ValidateCommentContentAsync([FromBody] string content)
        {
            var result = _commentService.ValidateCommentContent(content);
            return Success(new { IsValid = result.isValid, Message = result.message }, _localization.L("NewsComment.ValidateContent.Success"));
        }
    }
}




