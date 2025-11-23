//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktDriver.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 驾驶员管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 驾驶员管理实体
    /// </summary>
    [SugarTable("Takt_routine_vehicle_driver", "驾驶员管理")]
    [SugarIndex("index_driver_employee_id", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("index_driver_license_no", nameof(LicenseNo), OrderByType.Asc)]
    [SugarIndex("index_driver_status", nameof(Status), OrderByType.Asc)]
    public class TaktDriver : TaktBaseEntity
    {
        /// <summary>
        /// 员工ID
        /// </summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 驾驶员状态（0：在职，1：离职，2：休假，3：停职）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "驾驶员状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        [SugarColumn(ColumnName = "license_no", ColumnDescription = "驾驶证号码", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string LicenseNo { get; set; } = string.Empty;

        /// <summary>
        /// 驾驶证类型（0：A1，1：A2，2：A3，3：B1，4：B2，5：C1，6：C2，7：D，8：E，9：F，10：M，11：N，12：P）
        /// </summary>
        [SugarColumn(ColumnName = "license_type", ColumnDescription = "驾驶证类型", IsNullable = false, DefaultValue = "5", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        [SugarColumn(ColumnName = "license_authority", ColumnDescription = "驾驶证发证机关", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        [SugarColumn(ColumnName = "license_issue_date", ColumnDescription = "驾驶证发证日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        [SugarColumn(ColumnName = "license_expiry_date", ColumnDescription = "驾驶证有效期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态（0：正常，1：即将到期，2：已过期，3：被吊销，4：被注销）
        /// </summary>
        [SugarColumn(ColumnName = "license_status", ColumnDescription = "驾驶证状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        [SugarColumn(ColumnName = "license_points", ColumnDescription = "驾驶证扣分", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int LicensePoints { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        [SugarColumn(ColumnName = "driving_years", ColumnDescription = "驾龄", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验（0：新手，1：一般，2：熟练，3：专家）
        /// </summary>
        [SugarColumn(ColumnName = "driving_experience", ColumnDescription = "驾驶经验", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        [SugarColumn(ColumnName = "drivable_vehicles", ColumnDescription = "可驾驶车型", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 是否专职司机（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "is_full_time", ColumnDescription = "是否专职司机", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsFullTime { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        [SugarColumn(ColumnName = "work_area", ColumnDescription = "工作区域", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间（0：白班，1：夜班，2：全天，3：灵活）
        /// </summary>
        [SugarColumn(ColumnName = "work_schedule", ColumnDescription = "工作时间", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }

        /// <summary>
        /// 导航属性：员工
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
        public TaktUser? Employee { get; set; }
    }
} 


