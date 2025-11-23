#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktEquipmentOperationRate.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 机器稼动率实体类 (生产设备运行效率记录)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 机器稼动率实体类 (生产设备运行效率记录)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PM 设备维护模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_equipment_operationrate", "机器稼动率表")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_equipment_code", nameof(EquipmentCode), OrderByType.Asc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    [SugarIndex("ix_time_category", nameof(TimeCategory), OrderByType.Asc, false)]
    [SugarIndex("ix_start_date", nameof(StartDate), OrderByType.Asc, false)]
    [SugarIndex("ix_end_date", nameof(EndDate), OrderByType.Asc, false)]
    public class TaktEquipmentOperationRate : TaktBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 时间类别(1=天 2=周 3=月)
        /// </summary>
        [SugarColumn(ColumnName = "time_category", ColumnDescription = "时间类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TimeCategory { get; set; } = 1;

        /// <summary>
        /// 开始日期
        /// </summary>
        [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 周数(1-53)
        /// </summary>
        [SugarColumn(ColumnName = "week_number", ColumnDescription = "周数", ColumnDataType = "int", IsNullable = true)]
        public int? WeekNumber { get; set; }

        /// <summary>
        /// 月份(1-12)
        /// </summary>
        [SugarColumn(ColumnName = "month_number", ColumnDescription = "月份", ColumnDataType = "int", IsNullable = true)]
        public int? MonthNumber { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 设备类型(1=生产设备 2=检测设备 3=包装设备 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "equipment_type", ColumnDescription = "设备类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int EquipmentType { get; set; } = 1;

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionLine { get; set; }

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false)]
        public int ShiftNo { get; set; }

        /// <summary>
        /// 计划运行时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "planned_runtime", ColumnDescription = "计划运行时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedRuntime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_runtime", ColumnDescription = "实际运行时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualRuntime { get; set; } = 0;

        /// <summary>
        /// 停机时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "downtime", ColumnDescription = "停机时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 机器稼动率(%)
        /// </summary>
        [SugarColumn(ColumnName = "equipment_operation_rate", ColumnDescription = "机器稼动率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 计划产量
        /// </summary>
        [SugarColumn(ColumnName = "planned_output", ColumnDescription = "计划产量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedOutput { get; set; } = 0;

        /// <summary>
        /// 实际产量
        /// </summary>
        [SugarColumn(ColumnName = "actual_output", ColumnDescription = "实际产量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualOutput { get; set; } = 0;

        /// <summary>
        /// 合格品数量
        /// </summary>
        [SugarColumn(ColumnName = "qualified_quantity", ColumnDescription = "合格品数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal QualifiedQuantity { get; set; } = 0;

        /// <summary>
        /// 不良品数量
        /// </summary>
        [SugarColumn(ColumnName = "defective_quantity", ColumnDescription = "不良品数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DefectiveQuantity { get; set; } = 0;

        /// <summary>
        /// 良品率(%)
        /// </summary>
        [SugarColumn(ColumnName = "yield_rate", ColumnDescription = "良品率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal YieldRate { get; set; } = 0;

        /// <summary>
        /// 停机原因类型(1=设备故障 2=换型调试 3=缺料 4=人员不足 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "downtime_reason_type", ColumnDescription = "停机原因类型", ColumnDataType = "int", IsNullable = true)]
        public int? DowntimeReasonType { get; set; }

        /// <summary>
        /// 停机原因描述
        /// </summary>
        [SugarColumn(ColumnName = "downtime_reason", ColumnDescription = "停机原因描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 设备状态(1=正常运行 2=故障停机 3=维护保养 4=换型调试 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "equipment_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int EquipmentStatus { get; set; } = 1;

        /// <summary>
        /// 设备操作员
        /// </summary>
        [SugarColumn(ColumnName = "equipment_operator", ColumnDescription = "设备操作员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentOperator { get; set; }

        /// <summary>
        /// 设备维护员
        /// </summary>
        [SugarColumn(ColumnName = "equipment_maintainer", ColumnDescription = "设备维护员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentMaintainer { get; set; }

        /// <summary>
        /// 班组长
        /// </summary>
        [SugarColumn(ColumnName = "team_leader", ColumnDescription = "班组长", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeader { get; set; }

        /// <summary>
        /// 状态(0=正常  1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
    }
} 



