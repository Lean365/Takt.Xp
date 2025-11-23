//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedNatureDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 企业性质相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 企业性质相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedNatureDictData
{
    /// <summary>
    /// 获取企业性质相关字典数据
    /// </summary>
    /// <returns>企业性质相关字典数据列表</returns>
    public List<TaktDictData> GetNatureDictData()
    {
        return new List<TaktDictData>
        {
            // 企业性质 - 横排格式
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "国有企业", DictValue = "STATE_OWNED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "国有企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "集体企业", DictValue = "COLLECTIVE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "集体企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "私营企业", DictValue = "PRIVATE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "私营企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "外商投资企业", DictValue = "FOREIGN_INVESTED", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "外商投资企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "港澳台投资企业", DictValue = "HONG_KONG_TAIWAN", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "港澳台投资企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "联营企业", DictValue = "JOINT_OPERATION", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "联营企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "股份制企业", DictValue = "SHAREHOLDING", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "股份制企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "有限责任公司", DictValue = "LIMITED_LIABILITY", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "有限责任公司", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "股份有限公司", DictValue = "JOINT_STOCK", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "股份有限公司", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "合伙企业", DictValue = "PARTNERSHIP", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "合伙企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "个人独资企业", DictValue = "SOLE_PROPRIETORSHIP", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "个人独资企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_enterprise_nature", DictLabel = "其他企业", DictValue = "OTHER", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "其他企业", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

