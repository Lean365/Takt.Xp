//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMailDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Email
{
    /// <summary>
    /// 邮件基础DTO
    /// </summary>
    public class TaktMailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailDto()
        {
            MailFrom = string.Empty;
            MailTo = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long MailId { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string MailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 发送状态（0失败 1成功）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? MailSendTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? MailErrorInfo { get; set; }

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
    /// 邮件查询DTO
    /// </summary>
    public class TaktMailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailQueryDto()
        {
            MailFrom = string.Empty;
            MailTo = string.Empty;
            MailSubject = string.Empty;
        }

        /// <summary>
        /// 发件人
        /// </summary>
        [MaxLength(255, ErrorMessage = "发件人长度不能超过255个字符")]
        public string? MailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        public string? MailTo { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string? MailSubject { get; set; }

        /// <summary>
        /// 发送状态（0失败 1成功）
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

        /// <summary>
        /// 创建者（用于当前用户邮件过滤）
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 邮件创建DTO
    /// </summary>
    public class TaktMailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailCreateDto()
        {
            MailFrom = string.Empty;
            MailTo = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
        }

        /// <summary>
        /// 发件人
        /// </summary>
        [Required(ErrorMessage = "发件人不能为空")]
        [MaxLength(255, ErrorMessage = "发件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "发件人邮箱格式不正确")]
        public string MailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "收件人邮箱格式不正确")]
        public string MailTo { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [MaxLength(255, ErrorMessage = "抄送长度不能超过255个字符")]
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件更新DTO
    /// </summary>
    public class TaktMailUpdateDto : TaktMailCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "邮件ID不能为空")]
        public long MailId { get; set; }
    }

    /// <summary>
    /// 邮件删除DTO
    /// </summary>
    public class TaktMailDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long MailId { get; set; }
    }

    /// <summary>
    /// 邮件批量删除DTO
    /// </summary>
    public class TaktMailBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> MailIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 邮件发送DTO
    /// </summary>
    public class TaktMailSendDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailSendDto()
        {
            MailFrom = string.Empty;
            MailTo = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
        }

        /// <summary>
        /// 发件人
        /// </summary>
        [Required(ErrorMessage = "发件人不能为空")]
        [MaxLength(255, ErrorMessage = "发件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "发件人邮箱格式不正确")]
        public string MailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [Required(ErrorMessage = "收件人不能为空")]
        [MaxLength(255, ErrorMessage = "收件人长度不能超过255个字符")]
        [EmailAddress(ErrorMessage = "收件人邮箱格式不正确")]
        public string MailTo { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        [MaxLength(255, ErrorMessage = "抄送长度不能超过255个字符")]
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        [MaxLength(1000, ErrorMessage = "附件长度不能超过1000个字符")]
        public string? MailAttachments { get; set; }
    }

    /// <summary>
    /// 邮件导出DTO
    /// </summary>
    public class TaktMailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailExportDto()
        {
            MailFrom = string.Empty;
            MailTo = string.Empty;
            MailSubject = string.Empty;
            MailBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 发件人
        /// </summary>
        public string MailFrom { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public int MailIsHtml { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string? MailCc { get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public string? MailAttachments { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? MailSendTime { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? MailErrorInfo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}



