#nullable enable

//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNewsTopicRelationDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题关联数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 新闻话题关联DTO
    /// </summary>
    public class TaktNewsTopicRelationDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicRelationDto()
        {
            RelationUserName = string.Empty;
        }

        /// <summary>
        /// 关联ID
        /// </summary>
        [AdaptMember("Id")]
        public long RelationId { get; set; }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 话题ID
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// 关联类型
        /// </summary>
        public int RelationType { get; set; }

        /// <summary>
        /// 关联权重
        /// </summary>
        public int RelationWeight { get; set; }

        /// <summary>
        /// 关联时间
        /// </summary>
        public DateTime RelationTime { get; set; }

        /// <summary>
        /// 关联人
        /// </summary>
        public long RelationUserId { get; set; }

        /// <summary>
        /// 关联人姓名
        /// </summary>
        public string RelationUserName { get; set; }

        /// <summary>
        /// 关联备注
        /// </summary>
        public string? RelationRemark { get; set; }

        /// <summary>
        /// 是否自动关联
        /// </summary>
        public int IsAutoRelation { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public int RelationStatus { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 关联的新闻
        /// </summary>
        public TaktNewsDto? News { get; set; }

        /// <summary>
        /// 关联的话题
        /// </summary>
        public TaktNewsTopicDto? Topic { get; set; }
    }

    /// <summary>
    /// 新闻话题关联查询DTO
    /// </summary>
    public class TaktNewsTopicRelationQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public long? NewsId { get; set; }

        /// <summary>
        /// 话题ID
        /// </summary>
        public long? TopicId { get; set; }

        /// <summary>
        /// 关联类型
        /// </summary>
        public int? RelationType { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public int? RelationStatus { get; set; }

        /// <summary>
        /// 是否自动关联
        /// </summary>
        public int? IsAutoRelation { get; set; }

        /// <summary>
        /// 关联人
        /// </summary>
        public long? RelationUserId { get; set; }
    }

    /// <summary>
    /// 新闻话题关联创建DTO
    /// </summary>
    public class TaktNewsTopicRelationCreateDto
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 话题ID
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// 关联类型
        /// </summary>
        public int RelationType { get; set; } = 1;

        /// <summary>
        /// 关联权重
        /// </summary>
        public int RelationWeight { get; set; } = 1;

        /// <summary>
        /// 关联人
        /// </summary>
        public long RelationUserId { get; set; }

        /// <summary>
        /// 关联人姓名
        /// </summary>
        public string RelationUserName { get; set; } = string.Empty;

        /// <summary>
        /// 关联备注
        /// </summary>
        public string? RelationRemark { get; set; }

        /// <summary>
        /// 是否自动关联
        /// </summary>
        public int IsAutoRelation { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public int RelationStatus { get; set; } = 1;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻话题关联更新DTO
    /// </summary>
    public class TaktNewsTopicRelationUpdateDto : TaktNewsTopicRelationCreateDto
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [AdaptMember("Id")]
        public long RelationId { get; set; }
    }

    /// <summary>
    /// 新闻话题关联状态DTO
    /// </summary>
    public class TaktNewsTopicRelationStatusDto
    {
        /// <summary>
        /// 关联ID
        /// </summary>
        [AdaptMember("Id")]
        public long RelationId { get; set; }

        /// <summary>
        /// 关联状态
        /// </summary>
        public int RelationStatus { get; set; }
    }
}
