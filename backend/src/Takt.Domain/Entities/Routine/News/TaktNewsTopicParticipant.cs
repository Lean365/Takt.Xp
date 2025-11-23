#nullable enable


using SqlSugar;

//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNewsTopicParticipant.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 话题参与者实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 话题参与者实体
    /// </summary>
    [SugarTable("Takt_routine_news_topic_participant", "话题参与者")]
    public class TaktNewsTopicParticipant : TaktBaseEntity
    {
        /// <summary>
        /// 话题ID。参与的话题ID。
        /// </summary>
        [SugarColumn(ColumnName = "topic_id", ColumnDescription = "话题ID", IsNullable = false, ColumnDataType = "bigint")]
        public long TopicId { get; set; }

        /// <summary>
        /// 用户ID。参与话题的用户ID。
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", IsNullable = false, ColumnDataType = "bigint")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名。参与话题的用户姓名。
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像。参与话题的用户头像URL。
        /// </summary>
        [SugarColumn(ColumnName = "user_avatar", ColumnDescription = "用户头像", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 参与类型。1=关注话题，2=参与讨论，3=发布内容，4=管理员。
        /// </summary>
        [SugarColumn(ColumnName = "participation_type", ColumnDescription = "参与类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int ParticipationType { get; set; } = 1;

        /// <summary>
        /// 参与时间。用户参与话题的时间。
        /// </summary>
        [SugarColumn(ColumnName = "participation_time", ColumnDescription = "参与时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ParticipationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 参与状态。1=活跃参与，2=偶尔参与，3=已退出。
        /// </summary>
        [SugarColumn(ColumnName = "participation_status", ColumnDescription = "参与状态", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int ParticipationStatus { get; set; } = 1;

        /// <summary>
        /// 贡献度。用户在话题中的贡献度评分。
        /// </summary>
        [SugarColumn(ColumnName = "contribution_score", ColumnDescription = "贡献度", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int ContributionScore { get; set; }

        /// <summary>
        /// 发布内容数量。用户在话题中发布的内容数量。
        /// </summary>
        [SugarColumn(ColumnName = "content_count", ColumnDescription = "发布内容数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int ContentCount { get; set; }

        /// <summary>
        /// 评论数量。用户在话题中的评论数量。
        /// </summary>
        [SugarColumn(ColumnName = "comment_count", ColumnDescription = "评论数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int CommentCount { get; set; }

        /// <summary>
        /// 点赞数量。用户在话题中获得的点赞数量。
        /// </summary>
        [SugarColumn(ColumnName = "like_count", ColumnDescription = "点赞数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int LikeCount { get; set; }

        /// <summary>
        /// 分享数量。用户在话题中的分享数量。
        /// </summary>
        [SugarColumn(ColumnName = "share_count", ColumnDescription = "分享数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int ShareCount { get; set; }

        /// <summary>
        /// 最后活跃时间。用户最后在话题中活跃的时间。
        /// </summary>
        [SugarColumn(ColumnName = "last_active_time", ColumnDescription = "最后活跃时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastActiveTime { get; set; }

        /// <summary>
        /// 参与备注。用户参与话题时的备注信息。
        /// </summary>
        [SugarColumn(ColumnName = "participation_remark", ColumnDescription = "参与备注", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? ParticipationRemark { get; set; }

        /// <summary>
        /// 是否接收通知。1=接收通知，0=不接收通知。
        /// </summary>
        [SugarColumn(ColumnName = "receive_notification", ColumnDescription = "是否接收通知", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int ReceiveNotification { get; set; } = 1;

        /// <summary>
        /// 通知类型。1=全部通知，2=重要通知，3=不通知。
        /// </summary>
        [SugarColumn(ColumnName = "notification_type", ColumnDescription = "通知类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int NotificationType { get; set; } = 1;

        /// <summary>
        /// 排序号。用于自定义参与者在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 关联的话题。导航属性。
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TopicId))]
        public TaktNewsTopic? Topic { get; set; }
    }
} 
