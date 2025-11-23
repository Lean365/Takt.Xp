#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenColumn.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成列实体
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Domain.Entities.Generator;

/// <summary>
/// 代码生成列实体
/// </summary>
[SugarTable("Takt_generator_column", "代码生成列表")]
[SugarIndex("ix_gen_column_table", nameof(TableId), OrderByType.Asc, nameof(ColumnName), OrderByType.Asc, true)]
public class TaktGenColumn : TaktBaseEntity
{
  /// <summary>
  /// 表ID
  /// </summary>
  [SugarColumn(ColumnName = "table_id", ColumnDescription = "表ID", ColumnDataType = "bigint", IsNullable = false)]
  public long TableId { get; set; }

  /// <summary>
  /// 列名
  /// </summary>
  [SugarColumn(ColumnName = "column_name", ColumnDescription = "列名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string ColumnName { get; set; } = string.Empty;

  /// <summary>
  /// 列说明
  /// </summary>
  [SugarColumn(ColumnName = "column_comment", ColumnDescription = "列说明", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string ColumnComment { get; set; } = string.Empty;

  /// <summary>
  /// 数据库列类型
  /// </summary>
  [SugarColumn(ColumnName = "property_name", ColumnDescription = "库列类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string PropertyName { get; set; } = string.Empty;

  /// <summary>
  /// C#类型
  /// </summary>
  [SugarColumn(ColumnName = "data_type", ColumnDescription = "数据类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string DataType { get; set; } = string.Empty;

  /// <summary>
  /// C#列名（首字母大写）
  /// </summary>
  [SugarColumn(ColumnName = "column_data_type", ColumnDescription = "数据列名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string ColumnDataType { get; set; } = string.Empty;

  /// <summary>
  /// C#长度（字符串长度、数值类型的整数位数）
  /// </summary>
  [SugarColumn(ColumnName = "length", ColumnDescription = "数据长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int Length { get; set; } = 0;

  /// <summary>
  /// C#小数位数（decimal等数值类型的小数位数）
  /// </summary>
  [SugarColumn(ColumnName = "decimal_digits", ColumnDescription = "数据精度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int DecimalDigits { get; set; } = 0;

  /// <summary>
  /// 是否自增（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_increment", ColumnDescription = "自增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsIncrement { get; set; } = 0;

  /// <summary>
  /// 是否主键（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_primary_key", ColumnDescription = "主键", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsPrimaryKey { get; set; } = 0;

  /// <summary>
  /// 是否必填（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_required", ColumnDescription = "必填", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsRequired { get; set; } = 0;

  /// <summary>
  /// 是否为新增字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_create", ColumnDescription = "新增", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsCreate { get; set; } = 0;

  /// <summary>
  /// 是否编辑字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_update", ColumnDescription = "编辑", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsUpdate { get; set; } = 0;

  /// <summary>
  /// 是否列表字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_list", ColumnDescription = "列表", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsList { get; set; } = 0;

  /// <summary>
  /// 是否查询字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_query", ColumnDescription = "查询", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsQuery { get; set; } = 0;

  /// <summary>
  /// 查询方式（等于、不等于、大于、小于、范围）
  /// </summary>
  [SugarColumn(ColumnName = "query_type", ColumnDescription = "查询方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "EQ")]
  public string QueryType { get; set; } = "EQ";

  /// <summary>
  /// 是否排序字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_sort", ColumnDescription = "排序字段", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsSort { get; set; } = 0;

  /// <summary>
  /// 是否导出字段（0：是，1：否）
  /// </summary>
  [SugarColumn(ColumnName = "is_export", ColumnDescription = "导出", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int IsExport { get; set; } = 0;

  /// <summary>
  /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
  /// </summary>
  [SugarColumn(ColumnName = "display_type", ColumnDescription = "显示类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string DisplayType { get; set; } = string.Empty;

  /// <summary>
  /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
  /// </summary>
  [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
  public string DictType { get; set; } = string.Empty;

  /// <summary>
  /// 排序
  /// </summary>
  [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
  public int OrderNum { get; set; } = 0;

  #region 额外字段

  /// <summary>
  /// 必填字符串
  /// </summary>
  [SugarColumn(IsIgnore = true)]
  public string RequiredStr
  {
    get
    {
      string[] arr = new string[] { "int", "long" };
      return IsRequired == 0 && DisplayType != "selectMulti" && arr.Any(f => f.Contains(DataType)) || typeof(DateTime).Name == DataType ? "?" : "";
    }
  }

  /// <summary>
  /// 前端排序字符串
  /// </summary>
  [SugarColumn(IsIgnore = true)]
  public string SortStr => IsSort == 0 ? " sortable" : "";

  /// <summary>
  /// C# 字段名 首字母小写，用于前端
  /// </summary>
  [SugarColumn(IsIgnore = true)]
  public string CsharpFieldFl => ColumnDataType[..1].ToLower() + ColumnDataType[1..];

  /// <summary>
  /// 前端 只读字段
  /// </summary>
  [SugarColumn(IsIgnore = true)]
  public string DisabledStr => IsPrimaryKey == 0 && IsRequired == 0 ? " :disabled=\"true\"" : "";

  #endregion
}

