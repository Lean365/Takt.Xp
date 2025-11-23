//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedUnDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 计量单位字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 计量单位字典类型种子数据提供类
/// </summary>
public class TaktDbSeedUnitDictType
{
    /// <summary>
    /// 获取计量单位字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetUnitDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "计量单位", DictType = "sys_unit_type", OrderNum = 4, DictStatus=0, Remark = "计量单位字典" }
        };
    }
}

