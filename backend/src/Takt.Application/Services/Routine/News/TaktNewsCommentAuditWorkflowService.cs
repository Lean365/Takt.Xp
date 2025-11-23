#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsCommentAuditWorkflowService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论审核工作流服务
//===================================================================

using Takt.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻评论审核工作流服务
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class TaktNewsCommentAuditWorkflowService : TaktBaseService, ITaktNewsCommentAuditWorkflowService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 评论配置选项
        /// </summary>
        protected readonly TaktCommentOptions _commentOptions;

        /// <summary>
        /// 评论服务
        /// </summary>
        protected readonly ITaktNewsCommentService _commentService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="commentOptions">评论配置选项</param>
        /// <param name="commentService">评论服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsCommentAuditWorkflowService(
            ITaktRepositoryFactory repositoryFactory,
            IOptions<TaktCommentOptions> commentOptions,
            ITaktNewsCommentService commentService,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _commentOptions = commentOptions?.Value ?? throw new ArgumentNullException(nameof(commentOptions));
            _commentService = commentService ?? throw new ArgumentNullException(nameof(commentService));
        }

        /// <summary>
        /// 获取新闻评论仓储
        /// </summary>
        private ITaktRepository<TaktNewsComment> CommentRepository => _repositoryFactory.GetBusinessRepository<TaktNewsComment>();

        /// <summary>
        /// 处理新评论审核流程
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns>审核结果</returns>
        public async Task<TaktApiResult<bool>> ProcessCommentAuditWorkflowAsync(long commentId)
        {
            try
            {
                _logger.Info($"开始处理评论审核工作流，评论ID：{commentId}");

                var comment = await CommentRepository.GetByIdAsync(commentId);
                if (comment == null)
                {
                    return new TaktApiResult<bool> { Code = 404, Msg = "评论不存在", Data = false };
                }

                // 检查评论状态
                if (comment.CommentStatus != 0) // 不是待审核状态
                {
                    return new TaktApiResult<bool> { Code = 400, Msg = "评论状态不正确", Data = false };
                }

                // 根据配置决定审核方式
                if (_commentOptions.EnableCommentAudit)
                {
                    // 需要人工审核
                    await ProcessManualAuditAsync(comment);
                }
                else
                {
                    // 自动审核
                    await ProcessAutoAuditAsync(comment);
                }

                _logger.Info($"评论审核工作流处理完成，评论ID：{commentId}");
                return new TaktApiResult<bool> { Code = 200, Msg = "审核流程处理成功", Data = true };
            }
            catch (Exception ex)
            {
                _logger.Error($"处理评论审核工作流失败，评论ID：{commentId}，错误：{ex.Message}", ex);
                return new TaktApiResult<bool> { Code = 500, Msg = ex.Message, Data = false };
            }
        }

        /// <summary>
        /// 处理人工审核
        /// </summary>
        /// <param name="comment">评论实体</param>
        private async Task ProcessManualAuditAsync(TaktNewsComment comment)
        {
            _logger.Info($"评论需要人工审核，评论ID：{comment.Id}");

            // 更新评论状态为待审核
            comment.CommentStatus = 0; // 待审核
            comment.AuditType = 1;
            comment.UpdateBy = _currentUser.UserName ?? "system";
            comment.UpdateTime = DateTime.Now;

            await CommentRepository.UpdateAsync(comment);

            // TODO: 发送通知给责任编辑
            await SendAuditNotificationAsync(comment);

            _logger.Info($"评论已设置为待审核状态，评论ID：{comment.Id}");
        }

        /// <summary>
        /// 处理自动审核
        /// </summary>
        /// <param name="comment">评论实体</param>
        private async Task ProcessAutoAuditAsync(TaktNewsComment comment)
        {
            _logger.Info($"开始自动审核评论，评论ID：{comment.Id}");

            // 执行自动审核逻辑
            var auditResult = await PerformAutoAuditAsync(comment);

            // 更新评论状态
            comment.CommentStatus = auditResult.IsApproved ? 1 : 2; // 1=已通过，2=已拒绝
            comment.AuditedBy = "系统自动审核";
            comment.AuditedTime = DateTime.Now;
            comment.AuditComments = auditResult.Remark;
            comment.AuditType = 2;
            comment.UpdateBy = _currentUser.UserName ?? "system";
            comment.UpdateTime = DateTime.Now;

            await CommentRepository.UpdateAsync(comment);

            _logger.Info($"自动审核完成，评论ID：{comment.Id}，结果：{(auditResult.IsApproved ? "通过" : "拒绝")}");
        }

        /// <summary>
        /// 执行自动审核
        /// </summary>
        /// <param name="comment">评论实体</param>
        /// <returns>审核结果</returns>
        private async Task<AutoAuditResult> PerformAutoAuditAsync(TaktNewsComment comment)
        {
            var result = new AutoAuditResult { IsApproved = true, Remark = "自动审核通过" };

            // 检查敏感词
            if (_commentOptions.EnableSensitiveWordFilter)
            {
                if (TaktWordFilterHelper.ContainsSensitiveWord(comment.CommentContent))
                {
                    result.IsApproved = false;
                    result.Remark = "检测到敏感词，自动拒绝";
                    return result;
                }
            }

            // 检查字数限制
            if (_commentOptions.EnableLengthLimit)
            {
                var contentLength = comment.CommentContent.Trim().Length;
                if (contentLength < _commentOptions.MinCommentLength || contentLength > _commentOptions.MaxCommentLength)
                {
                    result.IsApproved = false;
                    result.Remark = $"评论字数不符合要求（{_commentOptions.MinCommentLength}-{_commentOptions.MaxCommentLength}字符）";
                    return result;
                }
            }

            // 检查重复评论
            if (_commentOptions.EnableDuplicateCheck)
            {
                var checkTime = DateTime.Now.AddHours(-_commentOptions.DuplicateCheckHours);
                var duplicateComment = await CommentRepository.GetFirstAsync(x =>
                    x.CommentUserId == comment.CommentUserId &&
                    x.CommentContent == comment.CommentContent &&
                    x.Id != comment.Id &&
                    x.CreateTime >= checkTime);

                if (duplicateComment != null)
                {
                    result.IsApproved = false;
                    result.Remark = "检测到重复评论，自动拒绝";
                    return result;
                }
            }

            // 检查用户历史记录
            var userHistory = await GetUserAuditHistoryAsync(comment.CommentUserId);
            if (userHistory.RejectionRate > 0.8) // 拒绝率超过80%
            {
                result.IsApproved = false;
                result.Remark = "用户历史记录不良，自动拒绝";
                return result;
            }

            return result;
        }

        /// <summary>
        /// 获取用户审核历史
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>审核历史统计</returns>
        private async Task<UserAuditHistory> GetUserAuditHistoryAsync(long userId)
        {
            var userComments = await CommentRepository.GetListAsync(x => x.CommentUserId == userId);

            var totalComments = userComments.Count;
            var approvedComments = userComments.Count(x => x.CommentStatus == 1);
            var rejectedComments = userComments.Count(x => x.CommentStatus == 2);

            return new UserAuditHistory
            {
                TotalComments = totalComments,
                ApprovedComments = approvedComments,
                RejectedComments = rejectedComments,
                ApprovalRate = totalComments > 0 ? (double)approvedComments / totalComments : 1.0,
                RejectionRate = totalComments > 0 ? (double)rejectedComments / totalComments : 0.0
            };
        }

        /// <summary>
        /// 发送审核通知
        /// </summary>
        /// <param name="comment">评论实体</param>
        private async Task SendAuditNotificationAsync(TaktNewsComment comment)
        {
            try
            {
                _logger.Info($"发送审核通知，评论ID：{comment.Id}");

                // TODO: 实现通知逻辑
                // 1. 发送邮件通知
                // 2. 发送站内信
                // 3. 发送短信通知
                // 4. 发送推送通知

                await Task.Delay(100); // 模拟异步操作

                _logger.Info($"审核通知发送完成，评论ID：{comment.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error($"发送审核通知失败，评论ID：{comment.Id}，错误：{ex.Message}", ex);
            }
        }

        /// <summary>
        /// 获取待审核评论统计
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<AuditStatistics> GetAuditStatisticsAsync()
        {
            var pendingCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 0);
            var approvedCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 1);
            var rejectedCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 2);

            var today = DateTime.Today;
            var todayPendingCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 0 && x.CreateTime >= today);
            var todayApprovedCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 1 && x.AuditedTime >= today);
            var todayRejectedCount = await CommentRepository.GetCountAsync(x => x.CommentStatus == 2 && x.AuditedTime >= today);

            return new AuditStatistics
            {
                TotalPending = pendingCount,
                TotalApproved = approvedCount,
                TotalRejected = rejectedCount,
                TodayPending = todayPendingCount,
                TodayApproved = todayApprovedCount,
                TodayRejected = todayRejectedCount
            };
        }

        /// <summary>
        /// 获取审核员工作量统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>审核员工作量统计</returns>
        public async Task<List<AuditorWorkload>> GetAuditorWorkloadAsync(DateTime startDate, DateTime endDate)
        {
            var auditRecords = await CommentRepository.GetListAsync(x =>
                x.AuditedTime >= startDate &&
                x.AuditedTime <= endDate &&
                !string.IsNullOrEmpty(x.AuditedBy));

            var auditorGroups = auditRecords
                .GroupBy(x => x.AuditedBy)
                .Select(g => new AuditorWorkload
                {
                    AuditorId = 0, // 不再使用ID
                    AuditorName = g.Key!,
                    TotalAudited = g.Count(),
                    ApprovedCount = g.Count(x => x.CommentStatus == 1),
                    RejectedCount = g.Count(x => x.CommentStatus == 2),
                    ApprovalRate = g.Count() > 0 ? (double)g.Count(x => x.CommentStatus == 1) / g.Count() : 0.0
                })
                .OrderByDescending(x => x.TotalAudited)
                .ToList();

            return auditorGroups;
        }
    }

    /// <summary>
    /// 自动审核结果
    /// </summary>
    public class AutoAuditResult
    {
        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsApproved { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string Remark { get; set; } = string.Empty;
    }

    /// <summary>
    /// 用户审核历史
    /// </summary>
    public class UserAuditHistory
    {
        /// <summary>
        /// 总评论数
        /// </summary>
        public int TotalComments { get; set; }

        /// <summary>
        /// 通过评论数
        /// </summary>
        public int ApprovedComments { get; set; }

        /// <summary>
        /// 拒绝评论数
        /// </summary>
        public int RejectedComments { get; set; }

        /// <summary>
        /// 通过率
        /// </summary>
        public double ApprovalRate { get; set; }

        /// <summary>
        /// 拒绝率
        /// </summary>
        public double RejectionRate { get; set; }
    }

    /// <summary>
    /// 审核统计信息
    /// </summary>
    public class AuditStatistics
    {
        /// <summary>
        /// 总待审核数
        /// </summary>
        public int TotalPending { get; set; }

        /// <summary>
        /// 总通过数
        /// </summary>
        public int TotalApproved { get; set; }

        /// <summary>
        /// 总拒绝数
        /// </summary>
        public int TotalRejected { get; set; }

        /// <summary>
        /// 今日待审核数
        /// </summary>
        public int TodayPending { get; set; }

        /// <summary>
        /// 今日通过数
        /// </summary>
        public int TodayApproved { get; set; }

        /// <summary>
        /// 今日拒绝数
        /// </summary>
        public int TodayRejected { get; set; }
    }

    /// <summary>
    /// 审核员工作量
    /// </summary>
    public class AuditorWorkload
    {
        /// <summary>
        /// 审核员ID
        /// </summary>
        public long AuditorId { get; set; }

        /// <summary>
        /// 审核员姓名
        /// </summary>
        public string AuditorName { get; set; } = string.Empty;

        /// <summary>
        /// 总审核数
        /// </summary>
        public int TotalAudited { get; set; }

        /// <summary>
        /// 通过数
        /// </summary>
        public int ApprovedCount { get; set; }

        /// <summary>
        /// 拒绝数
        /// </summary>
        public int RejectedCount { get; set; }

        /// <summary>
        /// 通过率
        /// </summary>
        public double ApprovalRate { get; set; }
    }
}





