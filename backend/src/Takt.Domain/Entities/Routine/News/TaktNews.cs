#nullable enable


//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNews.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 新闻实体
    /// </summary>
    [SugarTable("Takt_routine_news", "新闻")]
    public class TaktNews : TaktBaseEntity
    {
        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        [SugarColumn(ColumnName = "news_title", ColumnDescription = "新闻标题", Length = 200, IsNullable = false, ColumnDataType = "nvarchar")]
        public string NewsTitle { get; set; } = string.Empty;

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        [SugarColumn(ColumnName = "news_subtitle", ColumnDescription = "新闻副标题", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        [SugarColumn(ColumnName = "news_content", ColumnDescription = "新闻内容", ColumnDataType = "ntext", IsNullable = false)]
        public string NewsContent { get; set; } = string.Empty;

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        [SugarColumn(ColumnName = "news_summary", ColumnDescription = "新闻摘要", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        [SugarColumn(ColumnName = "news_category", ColumnDescription = "新闻分类（字典值）", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string NewsCategory { get; set; } = string.Empty;

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        [SugarColumn(ColumnName = "news_tags", ColumnDescription = "新闻标签", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        [SugarColumn(ColumnName = "news_author", ColumnDescription = "新闻作者", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string NewsAuthor { get; set; } = string.Empty;

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        [SugarColumn(ColumnName = "news_source", ColumnDescription = "新闻来源", Length = 100, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        [SugarColumn(ColumnName = "news_source_url", ColumnDescription = "新闻来源链接", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        [SugarColumn(ColumnName = "news_cover_image", ColumnDescription = "封面图片", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（0=草稿，1=已发布，2=已下线）。
        /// </summary>
        [SugarColumn(ColumnName = "news_status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        [SugarColumn(ColumnName = "news_publish_time", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        [SugarColumn(ColumnName = "news_offline_time", ColumnDescription = "下线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        [SugarColumn(ColumnName = "news_is_top", ColumnDescription = "是否置顶", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        [SugarColumn(ColumnName = "news_is_recommend", ColumnDescription = "是否推荐", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        [SugarColumn(ColumnName = "news_is_hot", ColumnDescription = "是否热门", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数。新闻被阅读的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "news_read_count", ColumnDescription = "阅读次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数。新闻被点赞的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "news_like_count", ColumnDescription = "点赞次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数。新闻下的评论总数。
        /// </summary>
        [SugarColumn(ColumnName = "news_comment_count", ColumnDescription = "评论次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数。新闻被用户分享的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "news_share_count", ColumnDescription = "分享次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数。新闻被推荐的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "news_recommend_count", ColumnDescription = "推荐次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人。最后一次编辑新闻的用户名称。
        /// </summary>
        [SugarColumn(ColumnName = "news_editor_by", ColumnDescription = "编辑人", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间。最后一次编辑新闻的时间。
        /// </summary>
        [SugarColumn(ColumnName = "news_edit_time", ColumnDescription = "编辑时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人。审核新闻的用户名称。
        /// </summary>
        [SugarColumn(ColumnName = "news_audited_by", ColumnDescription = "审核人", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间。新闻被审核的时间。
        /// </summary>
        [SugarColumn(ColumnName = "news_audited_time", ColumnDescription = "审核时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态。0=待审核，1=审核通过，2=审核拒绝。
        /// </summary>
        [SugarColumn(ColumnName = "news_audit_status", ColumnDescription = "审核状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见。审核人对新闻的备注或说明。
        /// </summary>
        [SugarColumn(ColumnName = "news_audit_comments", ColumnDescription = "审核意见", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        [SugarColumn(ColumnName = "news_seo_title", ColumnDescription = "SEO标题", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        [SugarColumn(ColumnName = "news_seo_keywords", ColumnDescription = "SEO关键词", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        [SugarColumn(ColumnName = "news_seo_description", ColumnDescription = "SEO描述", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 新闻评论列表。该新闻下的所有评论集合。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsComment.NewsId))]
        public List<TaktNewsComment>? NewsComments { get; set; }

        /// <summary>
        /// 新闻点赞列表。该新闻下的所有点赞记录集合。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsLike.NewsId))]
        public List<TaktNewsLike>? NewsLikes { get; set; }

        /// <summary>
        /// 新闻话题关联列表。该新闻关联的所有话题记录。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsTopicRelation.NewsId))]
        public List<TaktNewsTopicRelation>? NewsTopicRelations { get; set; }
    }
}
