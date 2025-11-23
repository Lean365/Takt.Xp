#nullable enable


using Takt.Shared.Enums;
using SqlSugar;
using System.Collections.Generic;

//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNewsTopic.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 新闻话题实体
    /// </summary>
    [SugarTable("Takt_routine_news_topic", "新闻话题")]
    public class TaktNewsTopic : TaktBaseEntity
    {
        /// <summary>
        /// 话题名称。话题的标题，用于展示和识别。
        /// </summary>
        [SugarColumn(ColumnName = "topic_name", ColumnDescription = "话题名称", Length = 100, IsNullable = false, ColumnDataType = "nvarchar")]
        public string TopicName { get; set; } = string.Empty;

        /// <summary>
        /// 话题描述。话题的详细描述信息。
        /// </summary>
        [SugarColumn(ColumnName = "topic_description", ColumnDescription = "话题描述", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词。用于搜索和标签功能的关键词。
        /// </summary>
        [SugarColumn(ColumnName = "topic_keywords", ColumnDescription = "话题关键词", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类。话题所属的分类，便于管理和展示。
        /// </summary>
        [SugarColumn(ColumnName = "topic_category", ColumnDescription = "话题分类", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string TopicCategory { get; set; } = string.Empty;

        /// <summary>
        /// 话题标签。多个标签用逗号分隔，便于分类和搜索。
        /// </summary>
        [SugarColumn(ColumnName = "topic_tags", ColumnDescription = "话题标签", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标。话题的图标URL地址。
        /// </summary>
        [SugarColumn(ColumnName = "topic_icon", ColumnDescription = "话题图标", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面。话题的封面图片URL地址。
        /// </summary>
        [SugarColumn(ColumnName = "topic_cover", ColumnDescription = "话题封面", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色。话题的主题颜色，用于UI展示。
        /// </summary>
        [SugarColumn(ColumnName = "topic_color", ColumnDescription = "话题颜色", Length = 20, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态。话题的状态（1=活跃，2=非活跃，3=已归档）。
        /// </summary>
        [SugarColumn(ColumnName = "topic_status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int Status { get; set; } = 1;

        /// <summary>
        /// 是否热门。1=热门话题，0=普通话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_is_hot", ColumnDescription = "是否热门", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐话题，0=普通话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_is_recommend", ColumnDescription = "是否推荐", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶话题，0=普通话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_is_top", ColumnDescription = "是否置顶", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型。1=普通话题，2=活动话题，3=专题话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_type", ColumnDescription = "话题类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int TopicType { get; set; } = 1;

        /// <summary>
        /// 开始时间。话题活动的开始时间，用于活动话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_start_time", ColumnDescription = "开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间。话题活动的结束时间，用于活动话题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_end_time", ColumnDescription = "结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 参与人数。参与该话题的用户数量。
        /// </summary>
        [SugarColumn(ColumnName = "topic_participant_count", ColumnDescription = "参与人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicParticipantCount { get; set; }

        /// <summary>
        /// 新闻数量。该话题下的新闻总数。
        /// </summary>
        [SugarColumn(ColumnName = "topic_news_count", ColumnDescription = "新闻数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicNewsCount { get; set; }

        /// <summary>
        /// 评论数量。该话题下的评论总数。
        /// </summary>
        [SugarColumn(ColumnName = "topic_comment_count", ColumnDescription = "评论数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicCommentCount { get; set; }

        /// <summary>
        /// 点赞数量。该话题被点赞的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "topic_like_count", ColumnDescription = "点赞数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicLikeCount { get; set; }

        /// <summary>
        /// 分享数量。该话题被分享的总次数。
        /// </summary>
        [SugarColumn(ColumnName = "topic_share_count", ColumnDescription = "分享数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicShareCount { get; set; }

        /// <summary>
        /// 阅读数量。该话题的总阅读次数。
        /// </summary>
        [SugarColumn(ColumnName = "topic_read_count", ColumnDescription = "阅读数量", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int TopicReadCount { get; set; }

        /// <summary>
        /// 话题创建者。创建话题的用户ID。
        /// </summary>
        [SugarColumn(ColumnName = "topic_creator_id", ColumnDescription = "话题创建者", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint")]
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名。创建话题的用户姓名。
        /// </summary>
        [SugarColumn(ColumnName = "topic_creator_name", ColumnDescription = "话题创建者姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string TopicCreatorName { get; set; } = string.Empty;

        /// <summary>
        /// 话题管理员。话题的管理员用户ID，多个用逗号分隔。
        /// </summary>
        [SugarColumn(ColumnName = "topic_admin_ids", ColumnDescription = "话题管理员", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名。话题的管理员姓名，多个用逗号分隔。
        /// </summary>
        [SugarColumn(ColumnName = "topic_admin_names", ColumnDescription = "话题管理员姓名", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则。话题的参与规则和说明。
        /// </summary>
        [SugarColumn(ColumnName = "topic_rules", ColumnDescription = "话题规则", ColumnDataType = "ntext", IsNullable = true)]
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置。话题的配置信息，JSON格式存储。
        /// </summary>
        [SugarColumn(ColumnName = "topic_settings", ColumnDescription = "话题设置", ColumnDataType = "ntext", IsNullable = true)]
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        [SugarColumn(ColumnName = "topic_seo_title", ColumnDescription = "SEO标题", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        [SugarColumn(ColumnName = "topic_seo_keywords", ColumnDescription = "SEO关键词", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        [SugarColumn(ColumnName = "topic_seo_description", ColumnDescription = "SEO描述", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义话题在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 话题新闻关联列表。该话题下的所有新闻关联记录。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsTopicRelation.TopicId))]
        public List<TaktNewsTopicRelation>? TopicNewsRelations { get; set; }

        /// <summary>
        /// 话题参与者列表。参与该话题的用户记录。
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNewsTopicParticipant.TopicId))]
        public List<TaktNewsTopicParticipant>? TopicParticipants { get; set; }
    }
} 

