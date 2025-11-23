#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktModelDestination.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 机种仕向实体类 (产品型号与销售区域对应关系)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 机种仕向实体类 (产品型号与销售区域对应关系)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_model_destination", "机种仕向表")]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_model_code", nameof(ModelCode), OrderByType.Asc, false)]
    [SugarIndex("ix_destination_code", nameof(DestinationCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktModelDestination : TaktBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种编码
        /// </summary>
        [SugarColumn(ColumnName = "model_code", ColumnDescription = "机种编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种名称
        /// </summary>
        [SugarColumn(ColumnName = "model_name", ColumnDescription = "机种名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ModelName { get; set; }

        /// <summary>
        /// 仕向地编码
        /// </summary>
        [SugarColumn(ColumnName = "destination_code", ColumnDescription = "仕向地编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DestinationCode { get; set; } = string.Empty;

        /// <summary>
        /// 仕向地名称
        /// </summary>
        [SugarColumn(ColumnName = "destination_name", ColumnDescription = "仕向地名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DestinationName { get; set; }

        /// <summary>
        /// 仕向地类型(1=日本 2=美国 3=澳洲 4=德国 5=加拿大 6=国内 7=其他)
        /// </summary>
        [SugarColumn(ColumnName = "destination_type", ColumnDescription = "仕向地类型", ColumnDataType = "int", IsNullable = true)]
        public int? DestinationType { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        [SugarColumn(ColumnName = "product_specification", ColumnDescription = "产品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductSpecification { get; set; }
    }
} 



