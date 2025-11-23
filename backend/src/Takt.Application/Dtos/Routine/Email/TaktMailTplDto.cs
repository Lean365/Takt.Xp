//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktMailTplDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 邮件模板数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Email
{
    /// <summary>
    /// 邮件模板基础DTO
    /// </summary>
    public class TaktMailTplDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
            MailTplSubject = string.Empty;
            MailTplBody = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long MailTplId { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string MailTplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailTplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailTplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

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
    /// 邮件模板查询DTO
    /// </summary>
    public class TaktMailTplQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplQueryDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
        public string? MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [MaxLength(50, ErrorMessage = "模板编码长度不能超过50个字符")]
        public string? MailTplCode { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
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
        /// 创建者（用于当前用户邮件模板过滤）
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 邮件模板创建DTO
    /// </summary>
    public class TaktMailTplCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplCreateDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
            MailTplSubject = string.Empty;
            MailTplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        [Required(ErrorMessage = "模板名称不能为空")]
        [MaxLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
        public string MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        [Required(ErrorMessage = "模板编码不能为空")]
        [MaxLength(50, ErrorMessage = "模板编码长度不能超过50个字符")]
        public string MailTplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "主题不能为空")]
        [MaxLength(255, ErrorMessage = "主题长度不能超过255个字符")]
        public string MailTplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        public string MailTplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [MaxLength(500, ErrorMessage = "参数列表长度不能超过500个字符")]
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500, ErrorMessage = "备注长度不能超过500个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件模板更新DTO
    /// </summary>
    public class TaktMailTplUpdateDto : TaktMailTplCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "模板ID不能为空")]
        [AdaptMember("Id")]
        public long MailTplId { get; set; }
    }

    /// <summary>
    /// 邮件模板删除DTO
    /// </summary>
    public class TaktMailTplDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long MailTplId { get; set; }
    }

    /// <summary>
    /// 邮件模板批量删除DTO
    /// </summary>
    public class TaktMailTplBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> MailTplIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 邮件模板导入DTO
    /// </summary>
    public class TaktMailTplImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplImportDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
            MailTplSubject = string.Empty;
            MailTplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string MailTplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailTplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailTplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件模板导出DTO
    /// </summary>
    public class TaktMailTplExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplExportDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
            MailTplSubject = string.Empty;
            MailTplBody = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string MailTplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailTplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailTplBody { get; set; }

        /// <summary>
        /// 是否HTML
        /// </summary>
        public bool MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 邮件模板导入模板DTO
    /// </summary>
    public class TaktMailTplTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktMailTplTemplateDto()
        {
            MailTplName = string.Empty;
            MailTplCode = string.Empty;
            MailTplSubject = string.Empty;
            MailTplBody = string.Empty;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string MailTplName { get; set; }

        /// <summary>
        /// 模板编码
        /// </summary>
        public string MailTplCode { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string MailTplSubject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string MailTplBody { get; set; }

        /// <summary>
        /// 是否HTML(0=否,1=是)
        /// </summary>
        public bool MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}


