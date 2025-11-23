#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStandardTimeChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 标准工时历史记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时历史记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP PP 标准工时历史记录
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_standardtime_changelog", "标准工时变更记录表")]
    [SugarIndex("ix_standard_time_id", nameof(StandardTimeId), OrderByType.Asc, false)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_change_date", nameof(ChangeDate), OrderByType.Desc, false)]
    public class TaktStandardTimeChangeLog : TaktBaseEntity
    {
        /// <summary>
        /// 标准工时ID
        /// </summary>
        [SugarColumn(ColumnName = "standard_time_id", ColumnDescription = "标准工时ID", ColumnDataType = "bigint", IsNullable = false)]
        public long StandardTimeId { get; set; }

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
        /// 标准点数
        /// </summary>
        [SugarColumn(ColumnName = "standard_shorts", ColumnDescription = "标准点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StandardShorts { get; set; } = 0;

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
        /// 工时单位
        /// </summary>
        [SugarColumn(ColumnName = "time_unit", ColumnDescription = "工时单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "MIN")]
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 点数单位
        /// </summary>
        [SugarColumn(ColumnName = "points_unit", ColumnDescription = "点数单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "SHORT")]
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 变更类型(1=新增 2=修改 3=删除 4=审核 5=停用 6=启用)
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
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 1;

        /// <summary>
        /// 标准工时 (主表)
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(StandardTimeId))]
        public TaktStandardTime? StandardTime { get; set; }
    }
} 



