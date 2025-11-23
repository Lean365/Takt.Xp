//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInspectionStandard.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 检验标准实体，符合ISO9001质量管理体系标准，包含AQL抽样检验规则
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 检验标准实体
/// 符合ISO9001质量管理体系标准，包含AQL抽样检验规则
/// </summary>
[SugarTable("Takt_logistics_quality_inspection_standard", TableDescription = "检验标准表")]
public class TaktInspectionStandard : TaktBaseEntity
{
    /// <summary>
    /// 检验标准代码（如：GB2828、ISO2859、MIL-STD-105E）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "检验标准代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准名称（如：GB2828计数抽样检验程序、ISO2859抽样检验标准）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "检验标准名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 检验标准描述（如：适用于计数抽样检验的国家标准）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "检验标准描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 关联检验项目代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_item_code", ColumnDescription = "关联检验项目代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionItemCode { get; set; }

    /// <summary>
    /// 关联检验方法代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_method_code", ColumnDescription = "关联检验方法代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionMethodCode { get; set; }

    /// <summary>
    /// 关联抽样方案代码
    /// </summary>
    [SugarColumn(ColumnName = "sampling_plan_code", ColumnDescription = "关联抽样方案代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? SamplingPlanCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 状态
    /// 0-禁用 1-启用
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;
}


