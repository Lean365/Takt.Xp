//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSamplingPlan.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 抽样方案字典实体，符合ISO9001质量管理体系标准
//===================================================================

using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Quality.Master;

/// <summary>
/// 抽样方案字典实体
/// 符合ISO9001质量管理体系标准
/// </summary>
[SugarTable("Takt_logistics_quality_sampling_plan", TableDescription = "抽样方案字典表")]
public class TaktSamplingPlan : TaktBaseEntity
{
    /// <summary>
    /// 抽样方案代码（如：S-1、S-2、S-3、S-4）
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "抽样方案代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案名称（如：一次抽样方案、二次抽样方案、多次抽样方案）
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "抽样方案名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 抽样方案描述（如：适用于批量产品的抽样检验方案）
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "抽样方案描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 关联检验标准代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_standard_code", ColumnDescription = "关联检验标准代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionStandardCode { get; set; }

    /// <summary>
    /// 关联检验项目代码
    /// </summary>
    [SugarColumn(ColumnName = "inspection_item_code", ColumnDescription = "关联检验项目代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? InspectionItemCode { get; set; }

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


