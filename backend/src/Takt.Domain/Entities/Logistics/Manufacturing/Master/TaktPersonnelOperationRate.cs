#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPersonnelOperationRate.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 人员稼动率实体类 (生产线人员作业效率记录)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 人员稼动率实体类 (生产线人员作业效率记录)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP HR 人力资源模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_personnel_operationrate", "人员稼动率表")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    [SugarIndex("ix_time_category", nameof(TimeCategory), OrderByType.Asc, false)]
    [SugarIndex("ix_start_date", nameof(StartDate), OrderByType.Asc, false)]
    [SugarIndex("ix_end_date", nameof(EndDate), OrderByType.Asc, false)]
    public class TaktPersonnelOperationRate : TaktBaseEntity
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
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductionLine { get; set; } = string.Empty;

        /// <summary>
        /// 生产线名称
        /// </summary>
        [SugarColumn(ColumnName = "production_line_name", ColumnDescription = "生产线名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionLineName { get; set; }

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false)]
        public int ShiftNo { get; set; }

        /// <summary>
        /// 计划直接人员数量
        /// </summary>
        [SugarColumn(ColumnName = "planned_direct_personnel_count", ColumnDescription = "计划直接人员数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PlannedDirectPersonnelCount { get; set; } = 0;

        /// <summary>
        /// 实际直接人员数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_direct_personnel_count", ColumnDescription = "实际直接人员数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualDirectPersonnelCount { get; set; } = 0;

        /// <summary>
        /// 计划间接人员数量
        /// </summary>
        [SugarColumn(ColumnName = "planned_indirect_personnel_count", ColumnDescription = "计划间接人员数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PlannedIndirectPersonnelCount { get; set; } = 0;

        /// <summary>
        /// 实际间接人员数量
        /// </summary>
        [SugarColumn(ColumnName = "actual_indirect_personnel_count", ColumnDescription = "实际间接人员数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActualIndirectPersonnelCount { get; set; } = 0;

        /// <summary>
        /// 计划工作时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "planned_work_time", ColumnDescription = "计划工作时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "actual_work_time", ColumnDescription = "实际工作时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 休息时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "break_time", ColumnDescription = "休息时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal BreakTime { get; set; } = 0;

        /// <summary>
        /// 空闲时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "idle_time", ColumnDescription = "空闲时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal IdleTime { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        [SugarColumn(ColumnName = "personnel_operation_rate", ColumnDescription = "人员稼动率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PersonnelOperationRate { get; set; } = 0;

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
        /// 工作效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "work_efficiency", ColumnDescription = "工作效率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WorkEfficiency { get; set; } = 0;

        /// <summary>
        /// 空闲原因类型(1=缺料 2=设备故障 3=换型调试 4=人员调配 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "idle_reason_type", ColumnDescription = "空闲原因类型", ColumnDataType = "int", IsNullable = true)]
        public int? IdleReasonType { get; set; }

        /// <summary>
        /// 空闲原因描述
        /// </summary>
        [SugarColumn(ColumnName = "idle_reason", ColumnDescription = "空闲原因描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IdleReason { get; set; }

        /// <summary>
        /// 加班时间(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "overtime_hours", ColumnDescription = "加班时间(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OvertimeHours { get; set; } = 0;

        /// <summary>
        /// 班组长
        /// </summary>
        [SugarColumn(ColumnName = "team_leader", ColumnDescription = "班组长", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TeamLeader { get; set; }

        /// <summary>
        /// 主管
        /// </summary>
        [SugarColumn(ColumnName = "supervisor", ColumnDescription = "主管", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Supervisor { get; set; }

        /// <summary>
        /// 状态(0=正常  1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
    }
} 



