#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenConfig.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 代码生成配置实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Generator;

/// <summary>
/// 代码生成配置实体
/// </summary>
[SugarTable("Takt_generator_config", "代码生成配置表")]
[SugarIndex("ix_gen_config_name", nameof(GenConfigName), OrderByType.Asc, true)]
public class TaktGenConfig : TaktBaseEntity
{
    #region 基本信息

    /// <summary>
    /// 配置名称
    /// </summary>
    [SugarColumn(ColumnName = "gen_config_name", ColumnDescription = "配置名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? GenConfigName { get; set; } 

    /// <summary>
    /// 作者
    /// </summary>
    [SugarColumn(ColumnName = "author", ColumnDescription = "作者", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? Author { get; set; }

    #endregion

    #region 模块配置

    /// <summary>
    /// 项目名称
    /// </summary>
    [SugarColumn(ColumnName = "project_name", ColumnDescription = "项目名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? ProjectName { get; set; } 

    /// <summary>
    /// 模块名
    /// </summary>
    [SugarColumn(ColumnName = "module_name", ColumnDescription = "模块名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? ModuleName { get; set; } 

    /// <summary>
    /// 业务名
    /// </summary>
    [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? BusinessName { get; set; } 

    /// <summary>
    /// 功能名
    /// </summary>
    [SugarColumn(ColumnName = "function_name", ColumnDescription = "功能名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? FunctionName { get; set; } 

    #endregion

    #region 生成配置

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）)
    /// </summary>
    [SugarColumn(ColumnName = "gen_method", ColumnDescription = "生成方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int GenMethod { get; set; }

    /// <summary>
    /// 模板类型（0：使用wwwroot/Generator/*.scriban模板，1：使用TaktGenTemplate表中的模板）
    /// </summary>
    [SugarColumn(ColumnName = "gen_tpl_type", ColumnDescription = "模板类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int GenTplType { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    [SugarColumn(ColumnName = "gen_path", ColumnDescription = "生成路径", Length = 200, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? GenPath { get; set; } 

    /// <summary>
    /// 选项配置
    /// </summary>
    [SugarColumn(ColumnName = "options", ColumnDescription = "选项配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Options { get; set; }

    /// <summary>
    /// 状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    #endregion
}

