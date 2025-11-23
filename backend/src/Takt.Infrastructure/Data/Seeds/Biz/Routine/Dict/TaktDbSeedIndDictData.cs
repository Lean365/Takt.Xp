//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIndDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 国民经济行业分类字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 国民经济行业分类字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedIndDictData
{
    /// <summary>
    /// 获取国民经济行业分类字典数据
    /// </summary>
    /// <returns>国民经济行业分类字典数据列表</returns>
    public List<TaktDictData> GetIndDictData()
    {
        return new List<TaktDictData>
        {
            // 国民经济行业分类 - 横排格式
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "农、林、牧、渔业", DictValue = "AGRICULTURE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "农、林、牧、渔业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "采矿业", DictValue = "MINING", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "采矿业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "制造业", DictValue = "MANUFACTURING", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "制造业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "电力、热力、燃气及水生产和供应业", DictValue = "UTILITIES", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "电力、热力、燃气及水生产和供应业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "建筑业", DictValue = "CONSTRUCTION", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "建筑业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "批发和零售业", DictValue = "WHOLESALE_RETAIL", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "批发和零售业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "交通运输、仓储和邮政业", DictValue = "TRANSPORTATION", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "交通运输、仓储和邮政业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "住宿和餐饮业", DictValue = "HOSPITALITY", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "住宿和餐饮业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "信息传输、软件和信息技术服务业", DictValue = "INFORMATION_TECHNOLOGY", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "信息传输、软件和信息技术服务业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "金融业", DictValue = "FINANCE", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "金融业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "房地产业", DictValue = "REAL_ESTATE", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "房地产业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "租赁和商务服务业", DictValue = "LEASING_BUSINESS", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "租赁和商务服务业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "科学研究和技术服务业", DictValue = "SCIENTIFIC_RESEARCH", OrderNum = 13,  CssClass = 13, ListClass = 13, Remark = "科学研究和技术服务业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "水利、环境和公共设施管理业", DictValue = "WATER_ENVIRONMENT", OrderNum = 14,  CssClass = 14, ListClass = 14, Remark = "水利、环境和公共设施管理业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "居民服务、修理和其他服务业", DictValue = "RESIDENTIAL_SERVICES", OrderNum = 15,  CssClass = 15, ListClass = 15, Remark = "居民服务、修理和其他服务业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "教育", DictValue = "EDUCATION", OrderNum = 16,  CssClass = 16, ListClass = 16, Remark = "教育", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "卫生和社会工作", DictValue = "HEALTH_SOCIAL", OrderNum = 17,  CssClass = 17, ListClass = 17, Remark = "卫生和社会工作", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "文化、体育和娱乐业", DictValue = "CULTURE_SPORTS", OrderNum = 18,  CssClass = 18, ListClass = 18, Remark = "文化、体育和娱乐业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "公共管理、社会保障和社会组织", DictValue = "PUBLIC_ADMINISTRATION", OrderNum = 19,  CssClass = 19, ListClass = 19, Remark = "公共管理、社会保障和社会组织", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_industry_type", DictLabel = "国际组织", DictValue = "INTERNATIONAL_ORGANIZATION", OrderNum = 20,  CssClass = 20, ListClass = 20, Remark = "国际组织", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

