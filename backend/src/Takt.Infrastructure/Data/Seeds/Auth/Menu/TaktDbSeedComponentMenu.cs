//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedComponentMenu.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 组件菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 组件菜单数据初始化类
/// </summary>
public static class TaktDbSeedComponentMenu
{
    /// <summary>
    /// 获取组件二级菜单
    /// </summary>
    /// <param name="parentId">父级菜单ID</param>
    /// <returns>二级菜单列表</returns>
    public static List<TaktMenu> GetComponentSecondMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "图标组件",
                TransKey = "menu.component.icon",
                ParentId = parentId,
                OrderNum = 1,
                Path = "icon",
                Component = "components/Base/TaktIcon/index.vue",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "component:icon:view",
                Icon = "SmileOutlined",
                Remark = "图标组件页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}


