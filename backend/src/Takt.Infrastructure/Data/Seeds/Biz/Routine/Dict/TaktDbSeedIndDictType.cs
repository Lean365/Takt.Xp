//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIndDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 国民经济行业分类字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 工业相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedIndDictType
{
    /// <summary>
    /// 获取工业相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetIndDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "国民经济行业分类", DictType = "sys_industry_type", OrderNum = 1, DictStatus=0, Remark = "国民经济行业分类字典" }
        };
    }
}

