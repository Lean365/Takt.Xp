//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIdentityMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 身份认证菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 身份认证菜单数据初始化类
/// </summary>
public class TaktDbSeedIdentityMenu
{
    /// <summary>
    /// 获取身份认证子菜单
    /// </summary>
    public static List<TaktMenu> GetIdentityThirdMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "用户管理",
                TransKey = "menu.identity.user",
                ParentId = parentId,
                OrderNum = 1,
                Path = "user",
                Component = "identity/user/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "identity:user:list",
                Icon = "UserOutlined",
                Remark = "用户管理菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "菜单管理",
                TransKey = "menu.identity.menu",
                ParentId = parentId,
                OrderNum = 2,
                Path = "menu",
                Component = "identity/menu/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "identity:menu:list",
                Icon = "MenuOutlined",
                Remark = "菜单管理菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "部门管理",
                TransKey = "menu.identity.dept",
                ParentId = parentId,
                OrderNum = 3,
                Path = "dept",
                Component = "identity/dept/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "identity:dept:list",
                Icon = "ApartmentOutlined",
                Remark = "部门管理菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "角色管理",
                TransKey = "menu.identity.role",
                ParentId = parentId,
                OrderNum = 4,
                Path = "role",
                Component = "identity/role/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "identity:role:list",
                Icon = "TeamOutlined",
                Remark = "角色管理菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "岗位管理",
                TransKey = "menu.identity.post",
                ParentId = parentId,
                OrderNum = 5,
                Path = "post",
                Component = "identity/post/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "identity:post:list",
                Icon = "IdcardOutlined",
                Remark = "岗位管理菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}

