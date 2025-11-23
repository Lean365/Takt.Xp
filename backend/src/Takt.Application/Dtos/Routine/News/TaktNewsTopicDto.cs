#nullable enable

//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNewsTopicDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 新闻话题基础DTO
    /// </summary>
    public class TaktNewsTopicDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
            TopicCreatorName = string.Empty;
        }

        /// <summary>
        /// 话题ID
        /// </summary>
        [AdaptMember("Id")]
        public long TopicId { get; set; }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标
        /// </summary>
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面
        /// </summary>
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色
        /// </summary>
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int TopicType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int TopicParticipantCount { get; set; }

        /// <summary>
        /// 新闻数量
        /// </summary>
        public int TopicNewsCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int TopicCommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int TopicLikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int TopicShareCount { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int TopicReadCount { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string TopicCreatorName { get; set; }

        /// <summary>
        /// 话题管理员
        /// </summary>
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名
        /// </summary>
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则
        /// </summary>
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置
        /// </summary>
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

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
        /// 话题新闻关联列表
        /// </summary>
        public List<TaktNewsTopicRelationDto>? TopicNewsRelations { get; set; }

        /// <summary>
        /// 话题参与者列表
        /// </summary>
        public List<TaktNewsTopicParticipantDto>? TopicParticipants { get; set; }
    }

    /// <summary>
    /// 新闻话题查询DTO
    /// </summary>
    public class TaktNewsTopicQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicQueryDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
            TopicCreatorName = string.Empty;
        }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string? TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string? TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int? TopicType { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long? TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string? TopicCreatorName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 新闻话题创建DTO
    /// </summary>
    public class TaktNewsTopicCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicCreateDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
        }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标
        /// </summary>
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面
        /// </summary>
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色
        /// </summary>
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 是否热门
        /// </summary>
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int TopicType { get; set; } = 1;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string TopicCreatorName { get; set; } = string.Empty;

        /// <summary>
        /// 话题管理员
        /// </summary>
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名
        /// </summary>
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则
        /// </summary>
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置
        /// </summary>
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻话题更新DTO
    /// </summary>
    public class TaktNewsTopicUpdateDto : TaktNewsTopicCreateDto
    {
        /// <summary>
        /// 话题ID
        /// </summary>
        [AdaptMember("Id")]
        public long TopicId { get; set; }
    }

    /// <summary>
    /// 新闻话题导入DTO
    /// </summary>
    public class TaktNewsTopicImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicImportDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
            TopicCreatorName = string.Empty;
        }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标
        /// </summary>
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面
        /// </summary>
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色
        /// </summary>
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 是否热门
        /// </summary>
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int TopicType { get; set; } = 1;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int TopicParticipantCount { get; set; }

        /// <summary>
        /// 新闻数量
        /// </summary>
        public int TopicNewsCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int TopicCommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int TopicLikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int TopicShareCount { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int TopicReadCount { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string TopicCreatorName { get; set; }

        /// <summary>
        /// 话题管理员
        /// </summary>
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名
        /// </summary>
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则
        /// </summary>
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置
        /// </summary>
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 话题ID
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// 新闻话题导出DTO
    /// </summary>
    public class TaktNewsTopicExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicExportDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
            TopicCreatorName = string.Empty;
        }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标
        /// </summary>
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面
        /// </summary>
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色
        /// </summary>
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int TopicType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int TopicParticipantCount { get; set; }

        /// <summary>
        /// 新闻数量
        /// </summary>
        public int TopicNewsCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int TopicCommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int TopicLikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int TopicShareCount { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int TopicReadCount { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string TopicCreatorName { get; set; }

        /// <summary>
        /// 话题管理员
        /// </summary>
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名
        /// </summary>
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则
        /// </summary>
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置
        /// </summary>
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻话题模板DTO
    /// </summary>
    public class TaktNewsTopicTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTopicTemplateDto()
        {
            TopicName = string.Empty;
            TopicCategory = string.Empty;
            TopicCreatorName = string.Empty;
        }

        /// <summary>
        /// 话题名称
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 话题描述
        /// </summary>
        public string? TopicDescription { get; set; }

        /// <summary>
        /// 话题关键词
        /// </summary>
        public string? TopicKeywords { get; set; }

        /// <summary>
        /// 话题分类
        /// </summary>
        public string TopicCategory { get; set; }

        /// <summary>
        /// 话题标签
        /// </summary>
        public string? TopicTags { get; set; }

        /// <summary>
        /// 话题图标
        /// </summary>
        public string? TopicIcon { get; set; }

        /// <summary>
        /// 话题封面
        /// </summary>
        public string? TopicCover { get; set; }

        /// <summary>
        /// 话题颜色
        /// </summary>
        public string? TopicColor { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 1;

        /// <summary>
        /// 是否热门
        /// </summary>
        public int TopicIsHot { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int TopicIsRecommend { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int TopicIsTop { get; set; }

        /// <summary>
        /// 话题类型
        /// </summary>
        public int TopicType { get; set; } = 1;

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? TopicStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? TopicEndTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int TopicParticipantCount { get; set; }

        /// <summary>
        /// 新闻数量
        /// </summary>
        public int TopicNewsCount { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int TopicCommentCount { get; set; }

        /// <summary>
        /// 点赞数量
        /// </summary>
        public int TopicLikeCount { get; set; }

        /// <summary>
        /// 分享数量
        /// </summary>
        public int TopicShareCount { get; set; }

        /// <summary>
        /// 阅读数量
        /// </summary>
        public int TopicReadCount { get; set; }

        /// <summary>
        /// 话题创建者
        /// </summary>
        public long TopicCreatorId { get; set; }

        /// <summary>
        /// 话题创建者姓名
        /// </summary>
        public string TopicCreatorName { get; set; }

        /// <summary>
        /// 话题管理员
        /// </summary>
        public string? TopicAdminIds { get; set; }

        /// <summary>
        /// 话题管理员姓名
        /// </summary>
        public string? TopicAdminNames { get; set; }

        /// <summary>
        /// 话题规则
        /// </summary>
        public string? TopicRules { get; set; }

        /// <summary>
        /// 话题设置
        /// </summary>
        public string? TopicSettings { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? TopicSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? TopicSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? TopicSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻话题状态DTO
    /// </summary>
    public class TaktNewsTopicStatusDto
    {
        /// <summary>
        /// 话题ID
        /// </summary>
        [AdaptMember("Id")]
        public long TopicId { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}
