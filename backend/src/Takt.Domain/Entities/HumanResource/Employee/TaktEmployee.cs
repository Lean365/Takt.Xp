// -----------------------------------------------------------------------------
// <copyright file="TaktEmployee.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Employee 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Employee;

/// <summary>
/// 员工实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-23
/// 功能说明:
/// 1. 存储员工基本信息
/// 2. 关联部门、职位、合同等信息
/// 3. 支持员工状态管理
/// </remarks>
[SugarTable("takt_humanresource_employee", "员工表")]
[SugarIndex("ix_employee_employee_no", nameof(EmployeeNo), OrderByType.Asc, true)]
[SugarIndex("ix_employee_department", nameof(DepartmentId), OrderByType.Asc)]
[SugarIndex("ix_employee_position", nameof(PositionId), OrderByType.Asc)]
[SugarIndex("ix_employee_status", nameof(Status), OrderByType.Asc)]
public class TaktEmployee : TaktBaseEntity
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;    
    /// <summary>
    /// 员工编号
    /// </summary>
    [SugarColumn(ColumnName = "employee_no", ColumnDescription = "员工编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 员工姓名
    /// </summary>
    [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string EmployeeName { get; set; } = string.Empty;

    /// <summary>
    /// 英文姓名
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文姓名", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 性别(0=未知 1=男 2=女)
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "birth_date", ColumnDescription = "出生日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? BirthDate { get; set; }

    /// <summary>
    /// 身份证号
    /// </summary>
    [SugarColumn(ColumnName = "id_card", ColumnDescription = "身份证号", ColumnDataType = "nvarchar", Length = 18, IsNullable = true)]
    public string? IdCard { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(ColumnName = "mobile", ColumnDescription = "手机号码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Mobile { get; set; }

    /// <summary>
    /// 邮箱地址
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱地址", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Email { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [SugarColumn(ColumnName = "department_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    public long DepartmentId { get; set; }

    /// <summary>
    /// 职位ID
    /// </summary>
    [SugarColumn(ColumnName = "position_id", ColumnDescription = "职位ID", ColumnDataType = "bigint", IsNullable = false)]
    public long PositionId { get; set; }

    /// <summary>
    /// 员工类型(1=正式员工 2=试用期 3=实习生 4=兼职 5=外包)
    /// </summary>
    [SugarColumn(ColumnName = "employee_type", ColumnDescription = "员工类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int EmployeeType { get; set; }

    /// <summary>
    /// 入职日期
    /// </summary>
    [SugarColumn(ColumnName = "hire_date", ColumnDescription = "入职日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime HireDate { get; set; }

    /// <summary>
    /// 转正日期
    /// </summary>
    [SugarColumn(ColumnName = "regular_date", ColumnDescription = "转正日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? RegularDate { get; set; }

    /// <summary>
    /// 离职日期
    /// </summary>
    [SugarColumn(ColumnName = "leave_date", ColumnDescription = "离职日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? LeaveDate { get; set; }

    /// <summary>
    /// 员工状态(1=在职 2=试用期 3=离职 4=停薪留职 5=退休)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "员工状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }

    /// <summary>
    /// 工作地点
    /// </summary>
    [SugarColumn(ColumnName = "work_location", ColumnDescription = "工作地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? WorkLocation { get; set; }

    /// <summary>
    /// 直属上级ID
    /// </summary>
    [SugarColumn(ColumnName = "manager_id", ColumnDescription = "直属上级ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? ManagerId { get; set; }

    /// <summary>
    /// 紧急联系人
    /// </summary>
    [SugarColumn(ColumnName = "emergency_contact", ColumnDescription = "紧急联系人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EmergencyContact { get; set; }

    /// <summary>
    /// 紧急联系电话
    /// </summary>
    [SugarColumn(ColumnName = "emergency_phone", ColumnDescription = "紧急联系电话", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? EmergencyPhone { get; set; }

    /// <summary>
    /// 户籍地址
    /// </summary>
    [SugarColumn(ColumnName = "household_address", ColumnDescription = "户籍地址", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? HouseholdAddress { get; set; }

    /// <summary>
    /// 现居住地址
    /// </summary>
    [SugarColumn(ColumnName = "current_address", ColumnDescription = "现居住地址", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? CurrentAddress { get; set; }

    /// <summary>
    /// 学历(1=高中 2=大专 3=本科 4=硕士 5=博士)
    /// </summary>
    [SugarColumn(ColumnName = "education", ColumnDescription = "学历", ColumnDataType = "int", IsNullable = true)]
    public int? Education { get; set; }

    /// <summary>
    /// 毕业院校
    /// </summary>
    [SugarColumn(ColumnName = "graduate_school", ColumnDescription = "毕业院校", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? GraduateSchool { get; set; }

    /// <summary>
    /// 专业
    /// </summary>
    [SugarColumn(ColumnName = "major", ColumnDescription = "专业", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Major { get; set; }

    /// <summary>
    /// 毕业时间
    /// </summary>
    [SugarColumn(ColumnName = "graduate_date", ColumnDescription = "毕业时间", ColumnDataType = "date", IsNullable = true)]
    public DateTime? GraduateDate { get; set; }

    /// <summary>
    /// 部门信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(DepartmentId))]
    public virtual Organization.TaktDepartment? Department { get; set; }

    /// <summary>
    /// 职位信息
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(PositionId))]
    public virtual Organization.TaktPosition? Position { get; set; }

    /// <summary>
    /// 直属上级
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ManagerId))]
    public virtual TaktEmployee? Manager { get; set; }

    /// <summary>
    /// 下属员工
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ManagerId))]
    public virtual List<TaktEmployee>? Subordinates { get; set; }

    /// <summary>
    /// 员工合同
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktEmployeeContract.EmployeeId))]
    public virtual List<TaktEmployeeContract>? Contracts { get; set; }

    /// <summary>
    /// 员工薪资
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Salary.TaktEmployeeSalary.EmployeeId))]
    public virtual List<Salary.TaktEmployeeSalary>? Salaries { get; set; }

    /// <summary>
    /// 考勤记录
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Attendance.TaktAttendance.EmployeeId))]
    public virtual List<Attendance.TaktAttendance>? Attendances { get; set; }

    /// <summary>
    /// 请假记录
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Leave.TaktLeave.EmployeeId))]
    public virtual List<Leave.TaktLeave>? Leaves { get; set; }

    /// <summary>
    /// 培训记录
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Training.TaktTrainingRecord.EmployeeId))]
    public virtual List<Training.TaktTrainingRecord>? TrainingRecords { get; set; }

    /// <summary>
    /// 绩效记录
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Performance.TaktPerformance.EmployeeId))]
    public virtual List<Performance.TaktPerformance>? Performances { get; set; }
}




