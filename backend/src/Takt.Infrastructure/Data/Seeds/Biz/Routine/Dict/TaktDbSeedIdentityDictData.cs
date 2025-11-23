//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIdentityDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 身份认证模块字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 身份认证模块字典数据种子数据提供类
/// </summary>
public class TaktDbSeedIdentityDictData
{
    /// <summary>
    /// 获取默认字典数据
    /// </summary>
    /// <returns>字典数据列表</returns>
    public List<TaktDictData> GetDefaultDictData()
    {
        return new List<TaktDictData>
        {
            // 用户类型 - 横排格式
            new TaktDictData { DictType = "identity_user_type", DictLabel = "系统用户", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "系统内置用户", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_type", DictLabel = "普通用户", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "普通业务用户", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_type", DictLabel = "管理员", DictValue = "2", OrderNum = 3,  CssClass = 2, ListClass = 2, Remark = "系统管理员", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_type", DictLabel = "OAuth用户", DictValue = "3", OrderNum = 4,  CssClass = 2, ListClass = 2, Remark = "第三方OAuth用户", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 用户性别 - 横排格式
            new TaktDictData { DictType = "identity_user_gender", DictLabel = "男", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "男性", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_gender", DictLabel = "女", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "女性", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_gender", DictLabel = "保密", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "性别保密", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否锁定 - 横排格式
            new TaktDictData { DictType = "identity_user_locked", DictLabel = "正常", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "用户正常状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_user_locked", DictLabel = "锁定", DictValue = "1", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "用户被锁定", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 数据范围 - 横排格式
            new TaktDictData { DictType = "identity_data_scope", DictLabel = "全部数据", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "可查看全部数据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_data_scope", DictLabel = "自定义数据", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "可查看自定义数据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_data_scope", DictLabel = "本部门数据", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "仅可查看本部门数据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_data_scope", DictLabel = "本部门及以下数据", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "可查看本部门及下级部门数据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_data_scope", DictLabel = "仅本人数据", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "仅可查看本人数据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 许可证类型 - 横排格式
            new TaktDictData { DictType = "identity_license_type", DictLabel = "免费版", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "免费版本许可证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_license_type", DictLabel = "标准版", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "标准版本许可证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_license_type", DictLabel = "专业版", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "专业版本许可证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_license_type", DictLabel = "企业版", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "企业版本许可证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 显示状态 - 横排格式
            new TaktDictData { DictType = "identity_menu_visible", DictLabel = "显示", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "菜单显示", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_visible", DictLabel = "隐藏", DictValue = "1", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "菜单隐藏", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否缓存 - 横排格式
            new TaktDictData { DictType = "identity_menu_cache", DictLabel = "不缓存", DictValue = "0", OrderNum = 1,  CssClass = 5, ListClass = 5, Remark = "菜单不缓存", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_cache", DictLabel = "缓存", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "菜单缓存", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否为外链 - 横排格式
            new TaktDictData { DictType = "identity_menu_external", DictLabel = "否", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "非外链菜单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_external", DictLabel = "是", DictValue = "1", OrderNum = 2,  CssClass = 1, ListClass = 1, Remark = "外链菜单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 菜单类型 - 横排格式
            new TaktDictData { DictType = "identity_menu_type", DictLabel = "目录", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "菜单目录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_type", DictLabel = "菜单", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "菜单页面", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_type", DictLabel = "按钮", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "菜单按钮", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            //菜单显示-横排格式
            new TaktDictData { DictType = "identity_menu_visible", DictLabel = "显示", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "菜单显示", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_menu_visible", DictLabel = "隐藏", DictValue = "1", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "菜单隐藏", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },


            // 岗位职级 - 横排格式
            new TaktDictData { DictType = "identity_post_rank", DictLabel = "初级", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "初级职级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_post_rank", DictLabel = "中级", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "中级职级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_post_rank", DictLabel = "高级", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "高级职级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_post_rank", DictLabel = "专家", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "专家职级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_post_rank", DictLabel = "资深专家", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "资深专家职级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // OAuth提供商 - 横排格式
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Alipay", DictValue = "alipay", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "支付宝第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Amazon", DictValue = "amazon", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "Google第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Apple", DictValue = "apple", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "苹果第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "DingTalk", DictValue = "dingtalk", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "钉钉第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Facebook", DictValue = "facebook", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "Facebook第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "GitHub", DictValue = "github", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "GitHub第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Google", DictValue = "google", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "Google第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "LinkedIn", DictValue = "linkedin", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "LinkedIn第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Microsoft", DictValue = "microsoft", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "Microsoft第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "QQ", DictValue = "qq", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "QQ第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Twitter", DictValue = "twitter", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "Twitter第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "WeChat", DictValue = "wechat", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "微信第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "identity_oauth_provider", DictLabel = "Weibo", DictValue = "weibo", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "微博第三方登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }

        };
    }
}


