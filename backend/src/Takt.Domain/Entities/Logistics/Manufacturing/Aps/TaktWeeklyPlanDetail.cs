#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWeeklyManufacturingPlanDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 周生产计划明细实体类 (Weekly Manufacturing Plan Detail)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 周生产计划明细实体类 (Weekly Manufacturing Plan Detail)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 生产计划模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_aps_weekly_plan_detail", "周生产计划明细表")]
    [SugarIndex("ix_weekly_plan_code", nameof(WeeklyPlanCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    [SugarIndex("ix_model_code", nameof(ModelCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plan_date", nameof(PlanDate), OrderByType.Asc, false)]
    [SugarIndex("ix_shift_no", nameof(ShiftNo), OrderByType.Asc, false)]
    public class TaktWeeklyPlanDetail : TaktBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 周生产计划编码
        /// </summary>
        [SugarColumn(ColumnName = "weekly_plan_code", ColumnDescription = "周生产计划编码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string WeeklyPlanCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProductionLine { get; set; } = string.Empty;

        /// <summary>
        /// 产品型号编码
        /// </summary>
        [SugarColumn(ColumnName = "model_code", ColumnDescription = "产品型号编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 产品型号名称
        /// </summary>
        [SugarColumn(ColumnName = "model_name", ColumnDescription = "产品型号名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelName { get; set; } = string.Empty;

        /// <summary>
        /// 产品编号
        /// </summary>
        [SugarColumn(ColumnName = "part_number", ColumnDescription = "产品编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PartNumber { get; set; }

        /// <summary>
        /// SAP工单号
        /// </summary>
        [SugarColumn(ColumnName = "sap_work_order", ColumnDescription = "SAP工单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SapWorkOrder { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "lot_number", ColumnDescription = "批次号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LotNumber { get; set; }

        /// <summary>
        /// 目标地
        /// </summary>
        [SugarColumn(ColumnName = "destination", ColumnDescription = "目标地", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Destination { get; set; }

        /// <summary>
        /// 计划日期
        /// </summary>
        [SugarColumn(ColumnName = "plan_date", ColumnDescription = "计划日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 当日计划数量
        /// </summary>
        [SugarColumn(ColumnName = "daily_planned_quantity", ColumnDescription = "当日计划数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyPlannedQuantity { get; set; } = 0;

        /// <summary>
        /// 当日实际完成数量
        /// </summary>
        [SugarColumn(ColumnName = "daily_actual_quantity", ColumnDescription = "当日实际完成数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyActualQuantity { get; set; } = 0;

        /// <summary>
        /// 当日完成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "daily_completion_rate", ColumnDescription = "当日完成率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyCompletionRate { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟/件)
        /// </summary>
        [SugarColumn(ColumnName = "standard_hours", ColumnDescription = "标准工时(分钟/件)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardHours { get; set; } = 0;

        /// <summary>
        /// 当日计划工时(小时)
        /// </summary>
        [SugarColumn(ColumnName = "daily_planned_hours", ColumnDescription = "当日计划工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyPlannedHours { get; set; } = 0;

        /// <summary>
        /// 当日实际工时(小时)
        /// </summary>
        [SugarColumn(ColumnName = "daily_actual_hours", ColumnDescription = "当日实际工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyActualHours { get; set; } = 0;

        /// <summary>
        /// 当日效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "daily_efficiency_rate", ColumnDescription = "当日效率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DailyEfficiencyRate { get; set; } = 0;

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 当日生产状态(0=未开始 1=生产中 2=已完成 3=暂停 4=取消)
        /// </summary>
        [SugarColumn(ColumnName = "daily_production_status", ColumnDescription = "当日生产状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DailyProductionStatus { get; set; } = 0;

        /// <summary>
        /// 计划开始时间
        /// </summary>
        [SugarColumn(ColumnName = "planned_start_time", ColumnDescription = "计划开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PlannedStartTime { get; set; }

        /// <summary>
        /// 计划结束时间
        /// </summary>
        [SugarColumn(ColumnName = "planned_end_time", ColumnDescription = "计划结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PlannedEndTime { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际结束时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 状态(0=正常  1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;
    }
} 



