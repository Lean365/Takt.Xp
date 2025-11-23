#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsCommentAuditWorkflowService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论审核工作流服务接口
//===================================================================

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻评论审核工作流服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsCommentAuditWorkflowService
    {
        /// <summary>
        /// 处理新评论审核流程
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns>审核结果</returns>
        Task<TaktApiResult<bool>> ProcessCommentAuditWorkflowAsync(long commentId);

        /// <summary>
        /// 获取待审核评论统计
        /// </summary>
        /// <returns>统计信息</returns>
        Task<AuditStatistics> GetAuditStatisticsAsync();

        /// <summary>
        /// 获取审核员工作量统计
        /// </summary>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>审核员工作量统计</returns>
        Task<List<AuditorWorkload>> GetAuditorWorkloadAsync(DateTime startDate, DateTime endDate);
    }
}



