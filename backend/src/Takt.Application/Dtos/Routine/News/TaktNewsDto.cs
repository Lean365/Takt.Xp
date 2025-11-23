//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNewsDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 新闻基础DTO
    /// </summary>
    public class TaktNewsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsDto()
        {
            NewsTitle = string.Empty;
            NewsContent = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        [AdaptMember("Id")]
        public long NewsId { get; set; }

        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        public string NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（0=草稿，1=已发布，2=已下线）。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        public int NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数。新闻被阅读的总次数。
        /// </summary>
        public int NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数。新闻被点赞的总次数。
        /// </summary>
        public int NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数。新闻下的评论总数。
        /// </summary>
        public int NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数。新闻被用户分享的总次数。
        /// </summary>
        public int NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数。新闻被推荐的总次数。
        /// </summary>
        public int NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人。最后一次编辑新闻的用户名称。
        /// </summary>
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间。最后一次编辑新闻的时间。
        /// </summary>
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人。审核新闻的用户名称。
        /// </summary>
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间。新闻被审核的时间。
        /// </summary>
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态。0=待审核，1=审核通过，2=审核拒绝。
        /// </summary>
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见。审核人对新闻的备注或说明。
        /// </summary>
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

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
    }

    /// <summary>
    /// 新闻查询DTO
    /// </summary>
    public class TaktNewsQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsQueryDto()
        {
            NewsTitle = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
            NewsTags = string.Empty;
            Keyword = string.Empty;
        }

        /// <summary>
        /// 新闻标题
        /// </summary>
        public string? NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string? NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类
        /// </summary>
        public string? NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者
        /// </summary>
        public string? NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int? NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int? NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数
        /// </summary>
        public int? NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数
        /// </summary>
        public int? NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数
        /// </summary>
        public int? NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人
        /// </summary>
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间
        /// </summary>
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? OrderNum { get; set; }

        /// <summary>
        /// 关键字（支持标题、内容、标签、作者模糊查询）
        /// </summary>
        public string? Keyword { get; set; }

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
    /// 新闻创建DTO
    /// </summary>
    public class TaktNewsCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCreateDto()
        {
            NewsTitle = string.Empty;
            NewsContent = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
        }

        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        public string NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（Draft=草稿，Published=已发布，Offline=已下线）。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        public int NewsIsHot { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 新闻更新DTO
    /// </summary>
    public class TaktNewsUpdateDto : TaktNewsCreateDto
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        [AdaptMember("Id")]
        public long NewsId { get; set; }
    }

    /// <summary>
    /// 新闻导入DTO
    /// </summary>
    public class TaktNewsImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsImportDto()
        {
            NewsTitle = string.Empty;
            NewsContent = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
        }

        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        public string NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（Draft=草稿，Published=已发布，Offline=已下线）。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        public int NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数。新闻被阅读的总次数。
        /// </summary>
        public int NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数。新闻被点赞的总次数。
        /// </summary>
        public int NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数。新闻下的评论总数。
        /// </summary>
        public int NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数。新闻被用户分享的总次数。
        /// </summary>
        public int NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数。新闻被推荐的总次数。
        /// </summary>
        public int NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人。最后一次编辑新闻的用户名称。
        /// </summary>
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间。最后一次编辑新闻的时间。
        /// </summary>
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人。审核新闻的用户名称。
        /// </summary>
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间。新闻被审核的时间。
        /// </summary>
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态。0=待审核，1=审核通过，2=审核拒绝。
        /// </summary>
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见。审核人对新闻的备注或说明。
        /// </summary>
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// 新闻导出DTO
    /// </summary>
    public class TaktNewsExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsExportDto()
        {
            NewsTitle = string.Empty;
            NewsContent = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
        }

        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        public string NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（Draft=草稿，Published=已发布，Offline=已下线）。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        public int NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数。新闻被阅读的总次数。
        /// </summary>
        public int NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数。新闻被点赞的总次数。
        /// </summary>
        public int NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数。新闻下的评论总数。
        /// </summary>
        public int NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数。新闻被用户分享的总次数。
        /// </summary>
        public int NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数。新闻被推荐的总次数。
        /// </summary>
        public int NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人。最后一次编辑新闻的用户名称。
        /// </summary>
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间。最后一次编辑新闻的时间。
        /// </summary>
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人。审核新闻的用户名称。
        /// </summary>
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间。新闻被审核的时间。
        /// </summary>
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态。0=待审核，1=审核通过，2=审核拒绝。
        /// </summary>
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见。审核人对新闻的备注或说明。
        /// </summary>
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻模板DTO
    /// </summary>
    public class TaktNewsTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsTemplateDto()
        {
            NewsTitle = string.Empty;
            NewsContent = string.Empty;
            NewsCategory = string.Empty;
            NewsAuthor = string.Empty;
        }

        /// <summary>
        /// 新闻标题。用于展示新闻的主标题，必填，唯一性由业务保证。
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻副标题。用于补充说明主标题，便于内容扩展和SEO优化。
        /// </summary>
        public string? NewsSubtitle { get; set; }

        /// <summary>
        /// 新闻内容。存储新闻的正文富文本，支持多种格式。
        /// </summary>
        public string NewsContent { get; set; }

        /// <summary>
        /// 新闻摘要。用于列表页、分享等场景的简要描述。
        /// </summary>
        public string? NewsSummary { get; set; }

        /// <summary>
        /// 新闻分类。字典值，标识新闻所属类别，便于分组和筛选。
        /// </summary>
        public string NewsCategory { get; set; }

        /// <summary>
        /// 新闻标签。多个标签用逗号分隔，便于搜索和聚合。
        /// </summary>
        public string? NewsTags { get; set; }

        /// <summary>
        /// 新闻作者。发布新闻的作者名称。
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻来源。原始新闻出处或转载来源。
        /// </summary>
        public string? NewsSource { get; set; }

        /// <summary>
        /// 新闻来源链接。原始新闻的URL地址。
        /// </summary>
        public string? NewsSourceUrl { get; set; }

        /// <summary>
        /// 封面图片。新闻主图URL，用于列表和详情页展示。
        /// </summary>
        public string? NewsCoverImage { get; set; }

        /// <summary>
        /// 状态。新闻的发布状态（Draft=草稿，Published=已发布，Offline=已下线）。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 发布时间。新闻正式发布的时间。
        /// </summary>
        public DateTime? NewsPublishTime { get; set; }

        /// <summary>
        /// 下线时间。新闻被下线的时间。
        /// </summary>
        public DateTime? NewsOfflineTime { get; set; }

        /// <summary>
        /// 是否置顶。1=置顶，0=不置顶，用于首页或列表优先展示。
        /// </summary>
        public int NewsIsTop { get; set; }

        /// <summary>
        /// 是否推荐。1=推荐，0=不推荐，用于推荐位展示。
        /// </summary>
        public int NewsIsRecommend { get; set; }

        /// <summary>
        /// 是否热门。1=热门，0=普通，用于热门新闻聚合。
        /// </summary>
        public int NewsIsHot { get; set; }

        /// <summary>
        /// 阅读次数。新闻被阅读的总次数。
        /// </summary>
        public int NewsReadCount { get; set; }

        /// <summary>
        /// 点赞次数。新闻被点赞的总次数。
        /// </summary>
        public int NewsLikeCount { get; set; }

        /// <summary>
        /// 评论次数。新闻下的评论总数。
        /// </summary>
        public int NewsCommentCount { get; set; }

        /// <summary>
        /// 分享次数。新闻被用户分享的总次数。
        /// </summary>
        public int NewsShareCount { get; set; }

        /// <summary>
        /// 推荐次数。新闻被推荐的总次数。
        /// </summary>
        public int NewsRecommendCount { get; set; }

        /// <summary>
        /// 编辑人。最后一次编辑新闻的用户名称。
        /// </summary>
        public string? NewsEditorBy { get; set; }

        /// <summary>
        /// 编辑时间。最后一次编辑新闻的时间。
        /// </summary>
        public DateTime? NewsEditTime { get; set; }

        /// <summary>
        /// 审核人。审核新闻的用户名称。
        /// </summary>
        public string? NewsAuditedBy { get; set; }

        /// <summary>
        /// 审核时间。新闻被审核的时间。
        /// </summary>
        public DateTime? NewsAuditedTime { get; set; }

        /// <summary>
        /// 审核状态。0=待审核，1=审核通过，2=审核拒绝。
        /// </summary>
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见。审核人对新闻的备注或说明。
        /// </summary>
        public string? NewsAuditComments { get; set; }

        /// <summary>
        /// SEO标题。用于搜索引擎优化的标题。
        /// </summary>
        public string? NewsSeoTitle { get; set; }

        /// <summary>
        /// SEO关键词。用于搜索引擎优化的关键词，多个用逗号分隔。
        /// </summary>
        public string? NewsSeoKeywords { get; set; }

        /// <summary>
        /// SEO描述。用于搜索引擎优化的描述信息。
        /// </summary>
        public string? NewsSeoDescription { get; set; }

        /// <summary>
        /// 排序号。用于自定义新闻在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻状态DTO
    /// </summary>
    public class TaktNewsStatusDto
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        [AdaptMember("Id")]
        public long NewsId { get; set; }

        /// <summary>
        /// 新闻状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 新闻审核DTO
    /// </summary>
    public class TaktNewsAuditDto
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        [AdaptMember("Id")]
        public long NewsId { get; set; }

        /// <summary>
        /// 审核状态（0待审核 1审核通过 2审核拒绝）
        /// </summary>
        public int NewsAuditStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? NewsAuditComments { get; set; }
    }
}
