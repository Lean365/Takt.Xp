//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktVehicleMaintenance.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 车辆维护保养管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆维护保养管理实体
    /// </summary>
    [SugarTable("Takt_routine_vehicle_maintenance", "车辆维护保养管理")]
    [SugarIndex("index_maintenance_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_maintenance_type", nameof(MaintenanceType), OrderByType.Asc)]
    [SugarIndex("index_maintenance_status", nameof(Status), OrderByType.Asc)]
    public class TaktVehicleMaintenance : TaktBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 维护类型（0：日常保养，1：定期保养，2：维修，3：年检，4：保险）
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_type", ColumnDescription = "维护类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int MaintenanceType { get; set; }

        /// <summary>
        /// 维护标题
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_title", ColumnDescription = "维护标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string MaintenanceTitle { get; set; } = string.Empty;

        /// <summary>
        /// 维护内容
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_content", ColumnDescription = "维护内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceContent { get; set; }

        /// <summary>
        /// 维护状态（0：计划中，1：进行中，2：已完成，3：已取消）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "维护状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 计划日期
        /// </summary>
        [SugarColumn(ColumnName = "plan_date", ColumnDescription = "计划日期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 实际开始时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_time", ColumnDescription = "实际开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualStartTime { get; set; }

        /// <summary>
        /// 实际完成时间
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_time", ColumnDescription = "实际完成时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ActualEndTime { get; set; }

        /// <summary>
        /// 维护前里程
        /// </summary>
        [SugarColumn(ColumnName = "before_mileage", ColumnDescription = "维护前里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? BeforeMileage { get; set; }

        /// <summary>
        /// 维护后里程
        /// </summary>
        [SugarColumn(ColumnName = "after_mileage", ColumnDescription = "维护后里程", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? AfterMileage { get; set; }

        /// <summary>
        /// 维护费用
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_cost", ColumnDescription = "维护费用", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? MaintenanceCost { get; set; }

        /// <summary>
        /// 维护厂家
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_shop", ColumnDescription = "维护厂家", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceShop { get; set; }

        /// <summary>
        /// 维护人员
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_person", ColumnDescription = "维护人员", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenancePerson { get; set; }

        /// <summary>
        /// 维护结果
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_result", ColumnDescription = "维护结果", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? MaintenanceResult { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }

        /// <summary>
        /// 导航属性：车辆
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(VehicleId))]
        public TaktVehicle? Vehicle { get; set; }
    }
} 


