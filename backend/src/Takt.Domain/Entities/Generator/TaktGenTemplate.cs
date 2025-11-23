#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenTemplate.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成模板实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Generator;

/// <summary>
/// 代码生成模板实体
/// </summary>
[SugarTable("Takt_generator_template", "代码生成模板表")]
[SugarIndex("ix_gen_template_name", nameof(TemplateName), OrderByType.Asc, true)]
public class TaktGenTemplate : TaktBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 模板名称
    /// </summary>
    [SugarColumn(ColumnName = "template_name", ColumnDescription = "模板名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "模板名称不能为空")]
    [StringLength(100, ErrorMessage = "模板名称长度不能超过100个字符")]
    public string TemplateName { get; set; } = string.Empty;

    /// <summary>
    /// ORM框架类型（1：Entity Framework Core，2：Dapper，3：SqlSugar，4：MyBatis，5：Hibernate，6：SQLAlchemy，7：TypeORM，8：Prisma，9：Eloquent，10：其他）
    /// </summary>
    [SugarColumn(ColumnName = "template_orm_type", ColumnDescription = "ORM框架类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
    [Required(ErrorMessage = "ORM框架类型不能为空")]
    public int TemplateOrmType { get; set; } = 3;

    /// <summary>
    /// 生成模板分类（1：后端代码，2：前端代码，3：SQL代码）
    /// </summary>
    [SugarColumn(ColumnName = "template_code_type", ColumnDescription = "生成模板分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TemplateCodeType { get; set; } = 1;

    /// <summary>
    /// 模板分类（1：实体类，2：数据访问层，3：服务层，4：控制器，5：API，6：类型，7：视图 ，8：翻译，9：其他）
    /// </summary>
    [SugarColumn(ColumnName = "template_category", ColumnDescription = "模板分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "模板分类不能为空")]
    public int TemplateCategory { get; set; } = 1;

    /// <summary>
    /// 编程语言（1：C#，2：TypeScript，3：JavaScript，4：Python，5：Go，6：PHP，7：Ruby，8：Swift，9：Kotlin，10：其他）
    /// </summary>
    [SugarColumn(ColumnName = "template_language", ColumnDescription = "编程语言", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "编程语言不能为空")]
    public int TemplateLanguage { get; set; } = 1;

    /// <summary>
    /// 版本号
    /// </summary>
    [SugarColumn(ColumnName = "template_version", ColumnDescription = "版本号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    [Required(ErrorMessage = "版本号不能为空")]
    public int TemplateVersion { get; set; } = 1;

    /// <summary>
    /// 模板内容
    /// </summary>
    [SugarColumn(ColumnName = "template_content", ColumnDescription = "模板内容", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; } = string.Empty;

    /// <summary>
    /// 生成配置名称
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    [Required(ErrorMessage = "文件名称不能为空")]
    [StringLength(100, ErrorMessage = "文件名称长度不能超过100个字符")]
    public string FileName { get; set; } = string.Empty;

    #endregion 基本信息

    #region 状态信息

    /// <summary>
    /// 状态（0：正常，1：停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;

    #endregion 状态信息
}

