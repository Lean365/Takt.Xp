//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktForm.cs 
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单实体类（整合定义和实例）
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Workflow
{
  /// <summary>
  /// 表单定义实体类
  /// </summary>
  [SugarTable("Takt_workflow_form", "表单定义表")]
  [SugarIndex("ix_form_key", nameof(FormKey), OrderByType.Asc)]
  [SugarIndex("ix_form_type", nameof(FormType), OrderByType.Asc)]
  [SugarIndex("ix_form_category", nameof(FormCategory), OrderByType.Asc)]
  [SugarIndex("ix_form_status", nameof(Status), OrderByType.Asc)]
  public class TaktForm : TaktBaseEntity
  {
    /// <summary>
    /// 表单键
    /// </summary>
    [SugarColumn(ColumnName = "form_key", ColumnDescription = "表单键", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string FormKey { get; set; } = string.Empty;

    /// <summary>
    /// 表单名称
    /// </summary>
    [SugarColumn(ColumnName = "form_name", ColumnDescription = "表单名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string FormName { get; set; } = string.Empty;

    /// <summary>
    /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
    /// </summary>
    [SugarColumn(ColumnName = "form_category", ColumnDescription = "表单分类", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int FormCategory { get; set; }

    /// <summary>
    /// 表单类型(0:动态表单 1:自定义表单 2:URL表单)
    /// </summary>
    [SugarColumn(ColumnName = "form_type", ColumnDescription = "表单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FormType { get; set; }

    /// <summary>
    /// 表单版本
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDescription = "表单版本", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "1.0")]
    public string Version { get; set; } = "1.0";

    /// <summary>
    /// 表单配置(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "form_config", ColumnDescription = "表单配置", ColumnDataType = "text", IsNullable = false)]
    public string FormConfig { get; set; } = string.Empty;

    /// <summary>
    /// 表单中的控件属性描述
    /// </summary>
    [SugarColumn(ColumnName = "content_data", ColumnDescription = "表单控件属性描述", ColumnDataType = "text", IsNullable = true)]
    public string? ContentData { get; set; }

    /// <summary>
    /// 表单控件位置模板
    /// </summary>
    [SugarColumn(ColumnName = "content_parse", ColumnDescription = "表单控件位置模板", ColumnDataType = "text", IsNullable = true)]
    public string? ContentParse { get; set; }

    /// <summary>
    /// 表单原html模板（未经处理的）
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "表单原html模板", ColumnDataType = "text", IsNullable = true)]
    public string? Content { get; set; }

    /// <summary>
    /// 业务实体表名（数据库名称）
    /// 当表单类型为自定义表单时，用于直接向对应数据库表中写入数据
    /// </summary>
    [SugarColumn(ColumnName = "business_table_name", ColumnDescription = "业务实体表名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? BusinessTableName { get; set; }

    /// <summary>
    /// 数据库名称（兼容OpenAuth.Net的DbName字段）
    /// </summary>
    [SugarColumn(ColumnName = "db_name", ColumnDescription = "数据库名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DbName { get; set; }

    /// <summary>
    /// 所属部门ID
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "所属部门ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? DeptId { get; set; }

    /// <summary>
    /// 注意事项
    /// </summary>
    [SugarColumn(ColumnName = "notes", ColumnDescription = "注意事项", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Notes { get; set; }


    /// <summary>
    /// 状态(0:草稿 1:已发布 2:已完成 3:已停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

  }
}

