#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWeeklyManufacturingPlanChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 周生产计划变更记录实体类 (Weekly Manufacturing Plan Change Log)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 周生产计划变更记录实体类 (Weekly Manufacturing Plan Change Log)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 生产计划变更记录
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_aps_weekly_plan_change_log", "周生产计划变更记录表")]
    [SugarIndex("ix_weekly_plan_id", nameof(WeeklyPlanId), OrderByType.Asc, false)]
    [SugarIndex("ix_weekly_plan_code", nameof(WeeklyPlanCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_production_line", nameof(ProductionLine), OrderByType.Asc, false)]
    [SugarIndex("ix_change_date", nameof(ChangeDate), OrderByType.Desc, false)]
    public class TaktWeeklyPlanChangeLog : TaktBaseEntity
    {
        /// <summary>
        /// 周生产计划ID
        /// </summary>
        [SugarColumn(ColumnName = "weekly_plan_id", ColumnDescription = "周生产计划ID", ColumnDataType = "bigint", IsNullable = false)]
        public long WeeklyPlanId { get; set; }

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
        /// 周开始日期
        /// </summary>
        [SugarColumn(ColumnName = "week_start_date", ColumnDescription = "周开始日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime WeekStartDate { get; set; }

        /// <summary>
        /// 周结束日期
        /// </summary>
        [SugarColumn(ColumnName = "week_end_date", ColumnDescription = "周结束日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime WeekEndDate { get; set; }

        /// <summary>
        /// 周次
        /// </summary>
        [SugarColumn(ColumnName = "week_number", ColumnDescription = "周次", ColumnDataType = "int", IsNullable = false)]
        public int WeekNumber { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        [SugarColumn(ColumnName = "year", ColumnDescription = "年度", ColumnDataType = "int", IsNullable = false)]
        public int Year { get; set; }

        /// <summary>
        /// 周计划总数量
        /// </summary>
        [SugarColumn(ColumnName = "weekly_planned_quantity", ColumnDescription = "周计划总数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyPlannedQuantity { get; set; } = 0;

        /// <summary>
        /// 周实际完成数量
        /// </summary>
        [SugarColumn(ColumnName = "weekly_actual_quantity", ColumnDescription = "周实际完成数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyActualQuantity { get; set; } = 0;

        /// <summary>
        /// 周完成率(%)
        /// </summary>
        [SugarColumn(ColumnName = "weekly_completion_rate", ColumnDescription = "周完成率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyCompletionRate { get; set; } = 0;

        /// <summary>
        /// 周计划工时(小时)
        /// </summary>
        [SugarColumn(ColumnName = "weekly_planned_hours", ColumnDescription = "周计划工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyPlannedHours { get; set; } = 0;

        /// <summary>
        /// 周实际工时(小时)
        /// </summary>
        [SugarColumn(ColumnName = "weekly_actual_hours", ColumnDescription = "周实际工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyActualHours { get; set; } = 0;

        /// <summary>
        /// 周平均效率(%)
        /// </summary>
        [SugarColumn(ColumnName = "weekly_efficiency_rate", ColumnDescription = "周平均效率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WeeklyEfficiencyRate { get; set; } = 0;

        /// <summary>
        /// 周计划状态(0=未开始 1=进行中 2=已完成 3=暂停 4=取消)
        /// </summary>
        [SugarColumn(ColumnName = "weekly_status", ColumnDescription = "周计划状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int WeeklyStatus { get; set; } = 0;

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
        /// 变更类型(1=新增 2=修改 3=删除 4=状态变更 5=数量调整 6=日期调整 7=工时调整 8=其他)
        /// </summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>
        /// 变更日期
        /// </summary>
        [SugarColumn(ColumnName = "change_date", ColumnDescription = "变更日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变更人
        /// </summary>
        [SugarColumn(ColumnName = "change_user", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeUser { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 变更前值(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "before_value", ColumnDescription = "变更前值", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeValue { get; set; }

        /// <summary>
        /// 变更后值(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "after_value", ColumnDescription = "变更后值", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterValue { get; set; }

        /// <summary>
        /// 变更字段名
        /// </summary>
        [SugarColumn(ColumnName = "change_field", ColumnDescription = "变更字段名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeField { get; set; }

        /// <summary>
        /// 变更前字段值
        /// </summary>
        [SugarColumn(ColumnName = "before_field_value", ColumnDescription = "变更前字段值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeFieldValue { get; set; }

        /// <summary>
        /// 变更后字段值
        /// </summary>
        [SugarColumn(ColumnName = "after_field_value", ColumnDescription = "变更后字段值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterFieldValue { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 1;

        /// <summary>
        /// 周生产计划 (主表)
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(WeeklyPlanId))]
        public TaktWeeklyPlan? WeeklyManufacturingPlan { get; set; }
    }
}





