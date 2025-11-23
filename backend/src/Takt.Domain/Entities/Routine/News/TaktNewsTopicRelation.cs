#nullable enable


using SqlSugar;

//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNewsTopicRelation.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题关联实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 新闻话题关联实体
    /// </summary>
    [SugarTable("Takt_routine_news_topic_relation", "新闻话题关联")]
    public class TaktNewsTopicRelation : TaktBaseEntity
    {
        /// <summary>
        /// 新闻ID。关联的新闻ID。
        /// </summary>
        [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", IsNullable = false, ColumnDataType = "bigint")]
        public long NewsId { get; set; }

        /// <summary>
        /// 话题ID。关联的话题ID。
        /// </summary>
        [SugarColumn(ColumnName = "topic_id", ColumnDescription = "话题ID", IsNullable = false, ColumnDataType = "bigint")]
        public long TopicId { get; set; }

        /// <summary>
        /// 关联类型。1=主要话题，2=次要话题，3=相关话题。
        /// </summary>
        [SugarColumn(ColumnName = "relation_type", ColumnDescription = "关联类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int RelationType { get; set; } = 1;

        /// <summary>
        /// 关联权重。用于排序和推荐，值越大越重要。
        /// </summary>
        [SugarColumn(ColumnName = "relation_weight", ColumnDescription = "关联权重", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int RelationWeight { get; set; } = 1;

        /// <summary>
        /// 关联时间。新闻与话题建立关联的时间。
        /// </summary>
        [SugarColumn(ColumnName = "relation_time", ColumnDescription = "关联时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime RelationTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 关联人。建立关联的用户ID。
        /// </summary>
        [SugarColumn(ColumnName = "relation_user_id", ColumnDescription = "关联人", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint")]
        public long RelationUserId { get; set; }

        /// <summary>
        /// 关联人姓名。建立关联的用户姓名。
        /// </summary>
        [SugarColumn(ColumnName = "relation_user_name", ColumnDescription = "关联人姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string RelationUserName { get; set; } = string.Empty;

        /// <summary>
        /// 关联备注。关联时的备注信息。
        /// </summary>
        [SugarColumn(ColumnName = "relation_remark", ColumnDescription = "关联备注", Length = 200, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? RelationRemark { get; set; }

        /// <summary>
        /// 是否自动关联。1=自动关联，0=手动关联。
        /// </summary>
        [SugarColumn(ColumnName = "is_auto_relation", ColumnDescription = "是否自动关联", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
        public int IsAutoRelation { get; set; }

        /// <summary>
        /// 关联状态。1=有效，0=无效。
        /// </summary>
        [SugarColumn(ColumnName = "relation_status", ColumnDescription = "关联状态", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int RelationStatus { get; set; } = 1;

        /// <summary>
        /// 排序号。用于自定义关联在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 关联的新闻。导航属性。
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NewsId))]
        public TaktNews? News { get; set; }

        /// <summary>
        /// 关联的话题。导航属性。
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TopicId))]
        public TaktNewsTopic? Topic { get; set; }
    }
} 
