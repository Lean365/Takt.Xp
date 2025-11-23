#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktRole.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 角色实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 角色实体
  /// </summary>
  [SugarTable("Takt_identity_role", "角色表")]
  [SugarIndex("ix_role_key", nameof(RoleKey), OrderByType.Asc, true)]
  public class TaktRole : TaktBaseEntity
  {
    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnName = "role_name", ColumnDescription = "角色名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色标识
    /// </summary>
    [SugarColumn(ColumnName = "role_key", ColumnDescription = "角色标识", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string RoleKey { get; set; } = string.Empty;

    /// <summary>
    /// 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限）
    /// </summary>
    [SugarColumn(ColumnName = "data_scope", ColumnDescription = "数据范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int DataScope { get; set; } = 1;

    /// <summary>
    /// 用户数
    /// </summary>
    [SugarColumn(ColumnName = "user_count", ColumnDescription = "用户数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserCount { get; set; } = 0;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 角色状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "role_status", ColumnDescription = "角色状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RoleStatus { get; set; } = 0;

    /// <summary>
    /// 角色菜单关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktRoleMenu.RoleId))]
    public List<TaktRoleMenu>? RoleMenus { get; set; }

    /// <summary>
    /// 角色部门关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktRoleDept.RoleId))]
    public List<TaktRoleDept>? RoleDepts { get; set; }

    /// <summary>
    /// 用户角色关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserRole.RoleId))]
    public List<TaktUserRole>? UserRoles { get; set; }
  }
}



