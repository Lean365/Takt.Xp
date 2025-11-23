//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedDashboardMenu.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 仪表盘菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 仪表盘菜单数据初始化类
/// </summary>
public static class TaktDbSeedDashboardMenu
{
    /// <summary>
    /// 获取仪表盘二级目录菜单
    /// </summary>
    /// <param name="parentId">父级菜单ID</param>
    /// <returns>二级目录菜单列表</returns>
    public static List<TaktMenu> GetDashboardSecondMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "工作台",
                TransKey = "menu.dashboard.workplace",
                ParentId = parentId,
                OrderNum = 1,
                Path = "workplace",
                Component = "dashboard/workplace/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "dashboard:workplace:view",
                Icon = "DesktopOutlined",
                Remark = "工作台页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "数据分析",
                TransKey = "menu.dashboard.analysis",
                ParentId = parentId,
                OrderNum = 2,
                Path = "analysis",
                Component = "dashboard/analysis/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "dashboard:analysis:view",
                Icon = "BarChartOutlined",
                Remark = "数据分析页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "数据看板",
                TransKey = "menu.dashboard.kanban",
                ParentId = parentId,
                OrderNum = 3,
                Path = "kanban",
                Component = "dashboard/kanban/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "dashboard:kanban:view",
                Icon = "FundOutlined",
                Remark = "数据看板页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}


