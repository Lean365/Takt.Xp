// -----------------------------------------------------------------------------
// <copyright file="TaktDepartment.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>Department 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// 部门实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-23
/// 功能说明:
/// 1. 存储部门基本信息
/// 2. 支持部门层级结构
/// 3. 关联部门负责人和员工
/// </remarks>
[SugarTable("takt_humanresource_department", "部门表")]
[SugarIndex("ix_department_dept_code", nameof(DeptCode), OrderByType.Asc, true)]
[SugarIndex("ix_department_parent", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_department_status", nameof(Status), OrderByType.Asc)]
public class TaktDepartment : TaktBaseEntity
{
    /// <summary>
    /// 部门编码
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 部门简称
    /// </summary>
    [SugarColumn(ColumnName = "dept_short_name", ColumnDescription = "部门简称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeptShortName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 上级部门ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级部门ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门层级
    /// </summary>
    [SugarColumn(ColumnName = "dept_level", ColumnDescription = "部门层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int DeptLevel { get; set; }

    /// <summary>
    /// 部门路径
    /// </summary>
    [SugarColumn(ColumnName = "dept_path", ColumnDescription = "部门路径", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? DeptPath { get; set; }

    /// <summary>
    /// 部门负责人ID
    /// </summary>
    [SugarColumn(ColumnName = "manager_id", ColumnDescription = "部门负责人ID", ColumnDataType = "bigint", IsNullable = true)]
    public long? ManagerId { get; set; }

    /// <summary>
    /// 部门类型(1=公司 2=部门 3=科室 4=小组)
    /// </summary>
    [SugarColumn(ColumnName = "dept_type", ColumnDescription = "部门类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int DeptType { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    [SugarColumn(ColumnName = "phone", ColumnDescription = "电话", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Phone { get; set; }

    /// <summary>
    /// 传真
    /// </summary>
    [SugarColumn(ColumnName = "fax", ColumnDescription = "传真", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Fax { get; set; }

    /// <summary>
    /// 分机
    /// </summary>
    [SugarColumn(ColumnName = "extension", ColumnDescription = "分机", ColumnDataType = "nvarchar", Length = 10, IsNullable = true)]
    public string? Extension { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? Email { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "部门地址", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 部门描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "部门描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 部门人数
    /// </summary>
    [SugarColumn(ColumnName = "employee_count", ColumnDescription = "部门人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EmployeeCount { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; }
        /// <summary>
    /// 部门状态(0=正常 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "部门状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; }

    /// <summary>
    /// 上级部门
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ParentId))]
    public virtual TaktDepartment? Parent { get; set; }

    /// <summary>
    /// 子部门
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ParentId))]
    public virtual List<TaktDepartment>? Children { get; set; }

    /// <summary>
    /// 部门负责人
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ManagerId))]
    public virtual Employee.TaktEmployee? Manager { get; set; }

    /// <summary>
    /// 部门员工
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(Employee.TaktEmployee.DepartmentId))]
    public virtual List<Employee.TaktEmployee>? Employees { get; set; }
}




