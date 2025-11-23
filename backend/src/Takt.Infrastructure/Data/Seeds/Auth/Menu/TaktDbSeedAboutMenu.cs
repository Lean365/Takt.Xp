//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedAboutMenu.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 关于菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 关于菜单数据初始化类
/// </summary>
public static class TaktDbSeedAboutMenu
{
    /// <summary>
    /// 获取关于二级目录菜单
    /// </summary>
    /// <param name="parentId">父级菜单ID</param>
    /// <returns>二级目录菜单列表</returns>
    public static List<TaktMenu> GetAboutSecondlMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "关于我们",
                TransKey = "menu.about.us",
                ParentId = parentId,
                OrderNum = 1,
                Path = "about",
                Component = "about/us/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "menu:about:view",
                Icon = "InfoCircleOutlined",
                Remark = "关于我们页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "服务条款",
                TransKey = "menu.about.terms",
                ParentId = parentId,
                OrderNum = 2,
                Path = "terms",
                Component = "about/terms/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "about:terms:view",
                Icon = "FileTextOutlined",
                Remark = "服务条款页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "隐私政策",
                TransKey = "menu.about.privacy",
                ParentId = parentId,
                OrderNum = 3,
                Path = "privacy",
                Component = "about/privacy/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "about:privacy:view",
                Icon = "SafetyCertificateOutlined",
                Remark = "隐私政策页面",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }


}


