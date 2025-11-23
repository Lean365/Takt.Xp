//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktNoticeDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 通知数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Notice
{
    /// <summary>
    /// 通知基础DTO
    /// </summary>
    public class TaktNoticeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long NoticeId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        public int NoticeReadCount { get; set; }

        /// <summary>
        /// 已读用户ID列表
        /// </summary>
        public string? NoticeReadIds { get; set; }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int NoticeConfirmCount { get; set; }

        /// <summary>
        /// 确认用户ID列表
        /// </summary>
        public string? NoticeConfirmIds { get; set; }

        /// <summary>
        /// 最后回执时间
        /// </summary>
        public DateTime? NoticeLastReceiptTime { get; set; }
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
        /// 是否删除（0未删除 1已删除）
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
    /// 通知查询DTO
    /// </summary>
    public class TaktNoticeQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeQueryDto()
        {
            NoticeTitle = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
        public string? NoticeTitle { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int? NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int? Status { get; set; }

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
    /// 通知创建DTO
    /// </summary>
    public class TaktNoticeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeCreateDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [MaxLength(255, ErrorMessage = "标题长度不能超过255个字符")]
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        [Required(ErrorMessage = "类型不能为空")]
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        [MaxLength(255, ErrorMessage = "访问链接长度不能超过255个字符")]
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255, ErrorMessage = "备注长度不能超过255个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 通知更新DTO
    /// </summary>
    public class TaktNoticeUpdateDto : TaktNoticeCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "通知ID不能为空")]
        public long NoticeId { get; set; }
    }

    /// <summary>
    /// 通知删除DTO
    /// </summary>
    public class TaktNoticeDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long NoticeId { get; set; }
    }

    /// <summary>
    /// 通知批量删除DTO
    /// </summary>
    public class TaktNoticeBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> NoticeIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 通知导入DTO
    /// </summary>
    public class TaktNoticeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeImportDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型（1通知 2公告）
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态（0草稿 1发布 2关闭）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public string? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }
    }

    /// <summary>
    /// 通知导出DTO
    /// </summary>
    public class TaktNoticeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeExportDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public DateTime? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 已读数量
        /// </summary>
        public int NoticeReadCount { get; set; }

        /// <summary>
        /// 确认数量
        /// </summary>
        public int NoticeConfirmCount { get; set; }

        /// <summary>
        /// 最后回执时间
        /// </summary>
        public DateTime? NoticeLastReceiptTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 通知导入模板DTO
    /// </summary>
    public class TaktNoticeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNoticeTemplateDto()
        {
            NoticeTitle = string.Empty;
            NoticeContent = string.Empty;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string NoticeTitle { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string NoticeContent { get; set; }

        /// <summary>
        /// 类型(1=通知,2=公告)
        /// </summary>
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态(0=草稿,1=发布,2=关闭)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string? NoticePublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        public string? NoticeDeadline { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问链接
        /// </summary>
        public string? NoticeAccessUrl { get; set; }
    }
}


