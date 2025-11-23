#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktDept.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 部门实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 部门实体
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  [SugarTable("Takt_identity_dept", "部门表")]
  [SugarIndex("ix_dept_name", nameof(DeptName), OrderByType.Asc)]
  public class TaktDept : TaktBaseEntity
  {
    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 父部门ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父部门ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 负责人
    /// </summary>
    [SugarColumn(ColumnName = "leader", ColumnDescription = "负责人", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "phone", ColumnDescription = "联系电话", Length = 11, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Email { get; set; }

    /// <summary>
    /// 用户数
    /// </summary>
    [SugarColumn(ColumnName = "user_count", ColumnDescription = "用户数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserCount { get; set; } = 0;

    /// <summary>
    /// 部门状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "dept_status", ColumnDescription = "部门状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeptStatus { get; set; } = 0;

    /// <summary>
    /// 角色部门关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktRoleDept.DeptId))]
    public List<TaktRoleDept>? RoleDepts { get; set; }

    /// <summary>
    /// 用户部门关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserDept.DeptId))]
    public List<TaktUserDept>? UserDepts { get; set; }

    /// <summary>
    /// 父部门导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ParentId))]
    public TaktDept? Parent { get; set; }

    /// <summary>
    /// 子部门导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ParentId))]
    public List<TaktDept>? Children { get; set; }
  }
}



