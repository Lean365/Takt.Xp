//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIdentityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 身份认证模块字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 身份认证模块字典类型种子数据提供类
/// </summary>
public class TaktDbSeedIdentityDictType
{
    /// <summary>
    /// 获取默认字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetDefaultDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "用户类型", DictType = "identity_user_type", OrderNum = 1, DictStatus=0, Remark = "用户类型字典" },
            new TaktDictType { DictName = "用户性别", DictType = "identity_user_gender", OrderNum = 2, DictStatus=0, Remark = "用户性别字典" },
            new TaktDictType { DictName = "是否锁定", DictType = "identity_user_locked", OrderNum = 3, DictStatus=0, Remark = "用户锁定状态字典" },
            new TaktDictType { DictName = "数据范围", DictType = "identity_data_scope", OrderNum = 4, DictStatus=0, Remark = "数据权限范围字典" },
            new TaktDictType { DictName = "许可证类型", DictType = "identity_license_type", OrderNum = 5, DictStatus=0, Remark = "许可证类型字典" },
            new TaktDictType { DictName = "显示状态", DictType = "identity_menu_visible", OrderNum = 6, DictStatus=0, Remark = "菜单显示状态字典" },
            new TaktDictType { DictName = "是否缓存", DictType = "identity_menu_cache", OrderNum = 7, DictStatus=0, Remark = "菜单缓存状态字典" },
            new TaktDictType { DictName = "是否为外链", DictType = "identity_menu_external", OrderNum = 8, DictStatus=0, Remark = "菜单外链状态字典" },
            new TaktDictType { DictName = "菜单类型", DictType = "identity_menu_type", OrderNum = 9, DictStatus=0, Remark = "菜单类型字典" },
            new TaktDictType { DictName = "岗位职级", DictType = "identity_post_rank", OrderNum = 10, DictStatus=0, Remark = "岗位职级字典" },
            new TaktDictType { DictName = "OAuth提供商", DictType = "identity_oauth_provider", OrderNum = 11, DictStatus=0, Remark = "OAuth第三方提供商字典" }
        };
    }
}


