#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktRoleDept.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 角色部门关联实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// 角色部门关联实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-16
/// </remarks>
[SugarTable("Takt_identity_role_dept", "角色部门")]
[SugarIndex("ix_role_dept", nameof(RoleId), OrderByType.Asc, nameof(DeptId), OrderByType.Asc, true)]
public class TaktRoleDept : TaktBaseEntity
{
  /// <summary>
  /// 角色ID
  /// </summary>
  [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
  public long RoleId { get; set; }

  /// <summary>
  /// 部门ID
  /// </summary>
  [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
  public long DeptId { get; set; }

  /// <summary>
  /// 角色导航属性
  /// </summary>
  [Navigate(NavigateType.OneToOne, nameof(RoleId))]
  public TaktRole? Role { get; set; }

  /// <summary>
  /// 部门导航属性
  /// </summary>
  [Navigate(NavigateType.OneToOne, nameof(DeptId))]
  public TaktDept? Dept { get; set; }
}



