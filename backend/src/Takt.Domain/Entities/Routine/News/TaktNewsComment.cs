#nullable enable

//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNewsComment.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻评论实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 新闻评论实体
    /// </summary>
    [SugarTable("Takt_routine_news_comment", "新闻评论")]
    public class TaktNewsComment : TaktBaseEntity
    {
        /// <summary>
        /// 新闻ID。新闻的唯一标识。
        /// </summary>
        [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", IsNullable = false, ColumnDataType = "bigint")]
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容。用户对新闻的评论文本。
        /// </summary>
        [SugarColumn(ColumnName = "comment_content", ColumnDescription = "评论内容", Length = 1000, IsNullable = false, ColumnDataType = "nvarchar")]
        public string CommentContent { get; set; } = string.Empty;

        /// <summary>
        /// 评论者ID。评论者的用户ID。
        /// </summary>
        [SugarColumn(ColumnName = "comment_user_id", ColumnDescription = "评论者ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint")]
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名。评论者的用户名。
        /// </summary>
        [SugarColumn(ColumnName = "comment_user_name", ColumnDescription = "评论者姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string CommentUserName { get; set; } = string.Empty;

        /// <summary>
        /// 评论者头像。评论者的头像URL。
        /// </summary>
        [SugarColumn(ColumnName = "comment_user_avatar", ColumnDescription = "评论者头像", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID。用于实现评论回复功能，0表示一级评论。
        /// </summary>
        [SugarColumn(ColumnName = "parent_comment_id", ColumnDescription = "父评论ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint")]
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID。被回复的用户ID。
        /// </summary>
        [SugarColumn(ColumnName = "reply_user_id", ColumnDescription = "回复用户ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint")]
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名。被回复的用户姓名。
        /// </summary>
        [SugarColumn(ColumnName = "reply_user_name", ColumnDescription = "回复用户姓名", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        [SugarColumn(ColumnName = "comment_status", ColumnDescription = "评论状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        [SugarColumn(ColumnName = "audited_by", ColumnDescription = "审核人", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间。评论被审核的时间。
        /// </summary>
        [SugarColumn(ColumnName = "audited_time", ColumnDescription = "审核时间", IsNullable = true, ColumnDataType = "datetime")]
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        [SugarColumn(ColumnName = "audit_comments", ColumnDescription = "审核意见", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        [SugarColumn(ColumnName = "audit_type", ColumnDescription = "审核类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int AuditType { get; set; } = 1;

        /// <summary>
        /// 点赞次数。评论被点赞的次数。
        /// </summary>
        [SugarColumn(ColumnName = "like_count", ColumnDescription = "点赞次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复次数。评论下的回复总数。
        /// </summary>
        [SugarColumn(ColumnName = "reply_count", ColumnDescription = "回复次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int ReplyCount { get; set; }

        /// <summary>
        /// IP地址。评论者访问时的IP地址。
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理。评论者访问时的浏览器信息。
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号。用于评论的排序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 关联的新闻。评论所属的新闻实体。
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NewsId))]
        public TaktNews? News { get; set; }

        /// <summary>
        /// 父评论。评论的父级评论，用于实现评论回复功能。
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentCommentId))]
        public TaktNewsComment? ParentComment { get; set; }

        /// <summary>
        /// 子评论列表。评论的子级评论列表。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(ParentCommentId))]
        public List<TaktNewsComment>? ChildComments { get; set; }

        /// <summary>
        /// 评论点赞列表。评论被点赞的记录列表。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsCommentLike.CommentId))]
        public List<TaktNewsCommentLike>? CommentLikes { get; set; }
    }
}