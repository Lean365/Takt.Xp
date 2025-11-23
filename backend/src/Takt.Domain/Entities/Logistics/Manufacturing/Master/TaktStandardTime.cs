#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStandardTime.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 标准工时实体类 (基于SAP PP标准工时)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.Collections.Generic;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时实体类 (基于SAP PP标准工时)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 标准工时模块
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_standardtime", "标准工时表")]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_work_center", nameof(WorkCenter), OrderByType.Asc, false)]
    public class TaktStandardTime : TaktBaseEntity
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
        /// 工作中心
        /// </summary>
        [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        [SugarColumn(ColumnName = "operation_desc", ColumnDescription = "工序描述", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "standard_minutes", ColumnDescription = "标准工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        [SugarColumn(ColumnName = "time_unit", ColumnDescription = "工时单位", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "MIN")]
        public string TimeUnit { get; set; } = "MIN";    

        /// <summary>
        /// 标准点数
        /// </summary>
        [SugarColumn(ColumnName = "standard_shorts", ColumnDescription = "标准点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        [SugarColumn(ColumnName = "points_unit", ColumnDescription = "点数单位", Length = 5, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "SHORT")]
        public string PointsUnit { get; set; } = "SHORT";
        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        [SugarColumn(ColumnName = "points_to_minutes_rate", ColumnDescription = "点数转分钟汇率", ColumnDataType = "decimal(8,4)", IsNullable = false, DefaultValue = "1")]
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        [SugarColumn(ColumnName = "converted_minutes", ColumnDescription = "转换后标准工时(分钟)", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true, DefaultValue = "9999-12-31")]
        public DateTime? ExpiryDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>
        /// 审核人
        /// </summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审核日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 标准工时变更记录集合 (子表)
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktStandardTimeChangeLog.StandardTimeId))]
        public List<TaktStandardTimeChangeLog>? ChangeLogs { get; set; }
    }
} 



