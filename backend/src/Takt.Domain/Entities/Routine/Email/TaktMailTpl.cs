//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMailTpl.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 邮件模板实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Email
{
    /// <summary>
    /// 邮件模板实体
    /// </summary>
    [SugarTable("Takt_routine_mail_tpl", "邮件模板")]
    [SugarIndex("index_Tpl_code", nameof(MailTplCode), OrderByType.Asc, true)]
    [SugarIndex("index_Tpl_name", nameof(MailTplName), OrderByType.Asc)]
    [SugarIndex("index_Tpl_status", nameof(Status), OrderByType.Asc)]
    public class TaktMailTpl : TaktBaseEntity
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_name", ColumnDescription = "模板名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MailTplName { get; set; } = string.Empty;

        /// <summary>
        /// 模板编码
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_code", ColumnDescription = "模板编码", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MailTplCode { get; set; } = string.Empty;

        /// <summary>
        /// 主题
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_subject", ColumnDescription = "主题", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MailTplSubject { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_body", ColumnDescription = "内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MailTplBody { get; set; } = string.Empty;

        /// <summary>
        /// 是否HTML
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_is_html", ColumnDescription = "是否HTML", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MailTplIsHtml { get; set; }

        /// <summary>
        /// 参数列表
        /// </summary>
        [SugarColumn(ColumnName = "mail_tpl_parameters", ColumnDescription = "参数列表", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MailTplParameters { get; set; }

        /// <summary>
        /// 状态
        /// 0:启用,1:停用
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

    }
}


