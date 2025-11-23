//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedNatureDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 企业性质字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 企业性质字典类型种子数据提供类
/// </summary>
public class TaktDbSeedNatureDictType
{
    /// <summary>
    /// 获取企业性质字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetNatureDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "企业性质", DictType = "sys_enterprise_nature", OrderNum = 1, DictStatus=0, Remark = "企业性质字典" }
        };
    }
}

