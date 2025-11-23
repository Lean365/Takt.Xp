#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktRoleMenu.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 角色菜单关联实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明：本软件基于 MIT 许可证，按原样提供，开发者不承担任何责任。
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// 角色菜单关联实体
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-16
/// </remarks>
[SugarTable("Takt_identity_role_menu", "角色菜单")]
[SugarIndex("ix_role_menu", nameof(RoleId), OrderByType.Asc, nameof(MenuId), OrderByType.Asc, true)]
public class TaktRoleMenu : TaktBaseEntity
{
  /// <summary>
  /// 角色ID
  /// </summary>
  [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
  public long RoleId { get; set; }

  /// <summary>
  /// 菜单ID
  /// </summary>
  [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID", ColumnDataType = "bigint", IsNullable = false)]
  public long MenuId { get; set; }

  /// <summary>
  /// 角色导航属性
  /// </summary>
  [Navigate(NavigateType.OneToOne, nameof(RoleId))]
  public TaktRole? Role { get; set; }

  /// <summary>
  /// 菜单导航属性
  /// </summary>
  [Navigate(NavigateType.OneToOne, nameof(MenuId))]
  public TaktMenu? Menu { get; set; }
}



