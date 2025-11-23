#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyManhours.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 装配生产工数实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.Collections.Generic;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Manhours.Assy
{
    /// <summary>
    /// 装配生产工数实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 功能说明: 记录装配生产过程中的工数信息，包括标准工时、实际工时、效率等
    /// </remarks>
    [SugarTable("Takt_logistics_execution_manhours_assy", "装配生产工数表")]
    [SugarIndex("ix_assy_prod_date", nameof(ProdDate), OrderByType.Asc, false)]
    [SugarIndex("ix_assy_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_assy_prod_line", nameof(ProdLine), OrderByType.Asc, false)]
    [SugarIndex("ix_assy_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_assy_batch_no", nameof(BatchNo), OrderByType.Asc, false)]
    public class TaktAssyManhours : TaktBaseEntity
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "prod_date", ColumnDescription = "生产日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        [SugarColumn(ColumnName = "model_type", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelType { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        [SugarColumn(ColumnName = "batch_no", ColumnDescription = "批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BatchNo { get; set; } = string.Empty;

        /// <summary>
        /// 投入工数(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "input_minutes", ColumnDescription = "投入工数(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 损失工数(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "loss_minutes", ColumnDescription = "损失工数(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal LossMinutes { get; set; } = 0;

        /// <summary>
        /// 作业工数(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "work_minutes", ColumnDescription = "作业工数(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WorkMinutes { get; set; } = 0;


    }
}




