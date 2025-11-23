//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 基础实体
// 命名空间 : Takt.Domain.Entities
// 文件名 : TaktBaseEntity.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 实体基类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明：本软件基于 MIT 许可证，按原样提供，开发者不承担任何责任。
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities
{
  /// <summary>
  /// 实体基类
  /// </summary>
  public abstract class TaktBaseEntity
  {
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "id", ColumnDescription = "主键", ColumnDataType = "bigint", IsNullable = false)]
    public long Id { get; set; }

    /// <summary>
    /// 自定义字段1
    /// </summary>
    [SugarColumn(ColumnName = "udf01", ColumnDescription = "自定义文本1", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf01 { get; set; }

    /// <summary>
    /// 自定义字段2
    /// </summary>
    [SugarColumn(ColumnName = "udf02", ColumnDescription = "自定义文本2", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf02 { get; set; }

    /// <summary>
    /// 自定义字段3
    /// </summary>
    [SugarColumn(ColumnName = "udf03", ColumnDescription = "自定义文本3", Length = 16, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf03 { get; set; }

    /// <summary>
    /// 自定义字段4
    /// </summary>
    [SugarColumn(ColumnName = "udf04", ColumnDescription = "自定义文本4", Length = 32, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf04 { get; set; }

    /// <summary>
    /// 自定义字段5
    /// </summary>
    [SugarColumn(ColumnName = "udf05", ColumnDescription = "自定义文本5", Length = 64, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf05 { get; set; }

    /// <summary>
    /// 自定义字段6
    /// </summary>
    [SugarColumn(ColumnName = "udf06", ColumnDescription = "自定义文本6", Length = 128, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Udf06 { get; set; }

    /// <summary>
    /// 自定义数值1
    /// </summary>
    [SugarColumn(ColumnName = "udf51", ColumnDescription = "自定义数值1", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
    public decimal? Udf51 { get; set; } = 0;

    /// <summary>
    /// 自定义数值2
    /// </summary>
    [SugarColumn(ColumnName = "udf52", ColumnDescription = "自定义数值2", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
    public decimal? Udf52 { get; set; } = 0;

    /// <summary>
    /// 自定义数值3
    /// </summary>
    [SugarColumn(ColumnName = "udf53", ColumnDescription = "自定义数值3", ColumnDataType = "decimal(18,5)", IsNullable = false, DefaultValue = "0")]
    public decimal? Udf53 { get; set; } = 0;

    /// <summary>
    /// 自定义字段10
    /// </summary>
    [SugarColumn(ColumnName = "udf54", ColumnDescription = "自定义数值4", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int? Udf54 { get; set; } = 0;

    /// <summary>
    /// 自定义字段11
    /// </summary>
    [SugarColumn(ColumnName = "udf55", ColumnDescription = "自定义数值5", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int? Udf55 { get; set; } = 0;

    /// <summary>
    /// 自定义字段13
    /// </summary>
    [SugarColumn(ColumnName = "udf56", ColumnDescription = "自定义数值6", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int? Udf56 { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [Required(ErrorMessage = "创建者不能为空")]
    [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CreateBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", ColumnDataType = "datetime", IsNullable = false, DefaultValue = "GETDATE()")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新者
    /// </summary>
    [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新者", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0未删除 1已删除）
    /// </summary>
    [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除者
    /// </summary>
    [SugarColumn(ColumnName = "delete_by", ColumnDescription = "删除者", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DeleteTime { get; set; }


  }
}



