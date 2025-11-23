#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProdOrder.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 生产工单实体类 (基于SAP PP生产计划)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单实体类 (基于SAP PP生产计划)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 生产计划模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_prodorder", "生产工单表")]
    [SugarIndex("ix_prod_order_code", nameof(ProdOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    public class TaktProdOrder : TaktBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// ZDTA=製造指図：DTA通常生産
        /// ZDTB=製造指図：DTA改造改修
        /// ZDTC=製造指図：DTA開発試作
        /// ZDTD=製造指図：DTA通常生産 PCBA
        /// ZDTE=製造指図：DTA改造改修 PCBA
        /// ZDTF=製造指図：DTA開発試作 PCBA
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_type", ColumnDescription = "生产工单类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "ZDTA")]
        public string ProdOrderType { get; set; } = "ZDTA";
        
        /// <summary>
        /// 生产工单号
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_qty", ColumnDescription = "生产工单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        [SugarColumn(ColumnName = "produced_qty", ColumnDescription = "已生产数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        [SugarColumn(ColumnName = "prod_batch", ColumnDescription = "生产批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        [SugarColumn(ColumnName = "routing_code", ColumnDescription = "工艺路线编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态(0=正常  1=生产中 2=已完成)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 生产工单变更记录集合 (子表)
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktProdOrderChangeLog.ProdOrderId))]
        public List<TaktProdOrderChangeLog>? ChangeLogs { get; set; }
    }
} 



