//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktMenu.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 菜单实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

#nullable enable

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 菜单实体
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  [SugarTable("Takt_identity_menu", "菜单表")]
  [SugarIndex("ix_menu_name", nameof(MenuName), OrderByType.Asc)]
  public class TaktMenu : TaktBaseEntity
  {
    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(ColumnName = "menu_name", ColumnDescription = "菜单名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string? MenuName { get; set; }

    /// <summary>
    /// 翻译Key
    /// </summary>
    [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译Key", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string? TransKey { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [SugarColumn(ColumnName = "icon", ColumnDescription = "菜单图标", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Icon { get; set; }

    /// <summary>
    /// 父菜单ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父菜单ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(ColumnName = "path", ColumnDescription = "路由地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(ColumnName = "component", ColumnDescription = "组件路径", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Component { get; set; }

    /// <summary>
    /// 路由参数
    /// </summary>
    [SugarColumn(ColumnName = "query_params", ColumnDescription = "路由参数", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? QueryParams { get; set; }

    /// <summary>
    /// 是否为外链（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_external", ColumnDescription = "外链", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsExternal { get; set; } = 0;

    /// <summary>
    /// 是否缓存（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_cache", ColumnDescription = "缓存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCache { get; set; } = 0;

    /// <summary>
    /// 菜单类型（0目录 1菜单 2按钮）
    /// </summary>
    [SugarColumn(ColumnName = "menu_type", ColumnDescription = "菜单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MenuType { get; set; } = 0;

    /// <summary>
    /// 显示状态（0显示 1隐藏）
    /// </summary>
    [SugarColumn(ColumnName = "visible", ColumnDescription = "显示状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Visible { get; set; } = 0;

    /// <summary>
    /// 权限标识
    /// </summary>
    [SugarColumn(ColumnName = "perms", ColumnDescription = "权限标识", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Perms { get; set; }

    /// <summary>
    /// 菜单状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "menu_status", ColumnDescription = "菜单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MenuStatus { get; set; } = 0;

    /// <summary>
    /// 角色菜单关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktRoleMenu.MenuId))]
    public List<TaktRoleMenu>? RoleMenus { get; set; }

    /// <summary>
    /// 父菜单导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(ParentId))]
    public TaktMenu? Parent { get; set; }

    /// <summary>
    /// 子菜单导航属性
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(ParentId))]
    public List<TaktMenu>? Children { get; set; }
  }
}



