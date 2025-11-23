#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedAdvertisementMenu.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 广告菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 广告菜单数据初始化类
/// </summary>
public class TaktDbSeedAdvertisementMenu
{
    /// <summary>
    /// 获取广告管理二级菜单列表
    /// </summary>
    /// <param name="parentId">父菜单ID</param>
    /// <returns>广告管理二级菜单列表</returns>
    public static List<TaktMenu> GetAdvertisementSecondMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu 
            { 
                MenuName = "广告管理", 
                TransKey = "menu.advertisement.manage", 
                ParentId = parentId, 
                OrderNum = 1, 
                Path = "manage", 
                Component = "advertisement/manage/index", 
                MenuType = 1, 
                Perms = "advertisement:manage", 
                Icon = "PictureOutlined", 
                Remark = "广告管理页面" 
            },
            new TaktMenu 
            { 
                MenuName = "计费管理", 
                TransKey = "menu.advertisement.billing", 
                ParentId = parentId, 
                OrderNum = 2, 
                Path = "billing", 
                Component = "advertisement/billing/index", 
                MenuType = 1, 
                Perms = "advertisement:billing", 
                Icon = "AccountBookOutlined", 
                Remark = "广告计费管理页面" 
            }
        };
    }
} 

