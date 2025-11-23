#nullable enable

//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNewsCommentDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻评论数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.News
{
    /// <summary>
    /// 新闻评论基础DTO
    /// </summary>
    public class TaktNewsCommentDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
        }

        /// <summary>
        /// 评论ID
        /// </summary>
        [AdaptMember("Id")]
        public long CommentId { get; set; }

        /// <summary>
        /// 新闻ID。新闻的唯一标识。
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容。用户对新闻的评论文本。
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论者ID。评论者的用户ID。
        /// </summary>
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名。评论者的用户名。
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像。评论者的头像URL。
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID。用于实现评论回复功能，0表示一级评论。
        /// </summary>
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID。被回复的用户ID。
        /// </summary>
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名。被回复的用户姓名。
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间。评论被审核的时间。
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int AuditType { get; set; } = 1;

        /// <summary>
        /// 点赞次数。评论被点赞的次数。
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复次数。评论下的回复总数。
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// IP地址。评论者访问时的IP地址。
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理。评论者访问时的浏览器信息。
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号。用于评论的排序，值越小越靠前。
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
        /// 关联的新闻
        /// </summary>
        public TaktNewsDto? News { get; set; }

        /// <summary>
        /// 父评论
        /// </summary>
        public TaktNewsCommentDto? ParentComment { get; set; }

        /// <summary>
        /// 子评论列表
        /// </summary>
        public List<TaktNewsCommentDto>? ChildComments { get; set; }

        /// <summary>
        /// 评论点赞列表
        /// </summary>
        public List<TaktNewsCommentLikeDto>? CommentLikes { get; set; }
    }

    /// <summary>
    /// 新闻评论查询DTO
    /// </summary>
    public class TaktNewsCommentQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentQueryDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
            ReplyUserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long? NewsId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string? CommentContent { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long? CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string? CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long? ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID
        /// </summary>
        public long? ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        public int? CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int? AuditType { get; set; }

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int? LikeCount { get; set; }

        /// <summary>
        /// 回复次数
        /// </summary>
        public int? ReplyCount { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int? OrderNum { get; set; }

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
    /// 新闻评论创建DTO
    /// </summary>
    public class TaktNewsCommentCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentCreateDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID。新闻的唯一标识。
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容。用户对新闻的评论文本。
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论者ID。评论者的用户ID。
        /// </summary>
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名。评论者的用户名。
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像。评论者的头像URL。
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID。用于实现评论回复功能，0表示一级评论。
        /// </summary>
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID。被回复的用户ID。
        /// </summary>
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名。被回复的用户姓名。
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// IP地址。评论者访问时的IP地址。
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理。评论者访问时的浏览器信息。
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号。用于评论的排序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻评论更新DTO
    /// </summary>
    public class TaktNewsCommentUpdateDto : TaktNewsCommentCreateDto
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        [AdaptMember("Id")]
        public long CommentId { get; set; }
    }

    /// <summary>
    /// 新闻评论审核DTO
    /// </summary>
    public class TaktNewsCommentAuditDto
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        [AdaptMember("Id")]
        public long CommentId { get; set; }

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int AuditType { get; set; } = 1;
    }

    /// <summary>
    /// 新闻评论批量审核DTO
    /// </summary>
    public class TaktNewsCommentBatchAuditDto
    {
        /// <summary>
        /// 评论ID集合
        /// </summary>
        public long[] CommentIds { get; set; } = Array.Empty<long>();

        /// <summary>
        /// 评论状态。0=待审核，1=已通过，2=已拒绝。
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int AuditType { get; set; } = 1;
    }

    /// <summary>
    /// 新闻评论导入DTO
    /// </summary>
    public class TaktNewsCommentImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentImportDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID
        /// </summary>
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间。评论被审核的时间。
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型
        /// </summary>
        public int AuditType { get; set; } = 1;

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复次数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 评论ID
        /// </summary>
        public long Id { get; set; }
    }

    /// <summary>
    /// 新闻评论导出DTO
    /// </summary>
    public class TaktNewsCommentExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentExportDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID
        /// </summary>
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间。评论被审核的时间。
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int AuditType { get; set; } = 1;

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复次数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻评论模板DTO
    /// </summary>
    public class TaktNewsCommentTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNewsCommentTemplateDto()
        {
            CommentContent = string.Empty;
            CommentUserName = string.Empty;
        }

        /// <summary>
        /// 新闻ID
        /// </summary>
        public long NewsId { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string CommentContent { get; set; }

        /// <summary>
        /// 评论者ID
        /// </summary>
        public long CommentUserId { get; set; }

        /// <summary>
        /// 评论者姓名
        /// </summary>
        public string CommentUserName { get; set; }

        /// <summary>
        /// 评论者头像
        /// </summary>
        public string? CommentUserAvatar { get; set; }

        /// <summary>
        /// 父评论ID
        /// </summary>
        public long ParentCommentId { get; set; }

        /// <summary>
        /// 回复用户ID
        /// </summary>
        public long ReplyUserId { get; set; }

        /// <summary>
        /// 回复用户姓名
        /// </summary>
        public string? ReplyUserName { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public int CommentStatus { get; set; }

        /// <summary>
        /// 审核人。审核评论的责任编辑姓名。
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间。评论被审核的时间。
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 审核意见。审核时的备注信息。
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核类型。1=责任编辑审核，2=系统自动审核，3=人工审核。
        /// </summary>
        public int AuditType { get; set; } = 1;

        /// <summary>
        /// 点赞次数
        /// </summary>
        public int LikeCount { get; set; }

        /// <summary>
        /// 回复次数
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;
    }

    /// <summary>
    /// 新闻评论状态DTO
    /// </summary>
    public class TaktNewsCommentStatusDto
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        [AdaptMember("Id")]
        public long CommentId { get; set; }

        /// <summary>
        /// 评论状态
        /// </summary>
        public int CommentStatus { get; set; }
    }
}