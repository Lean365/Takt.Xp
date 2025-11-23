//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedEquipmentDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 设备相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedEquipmentDictType
{
    /// <summary>
    /// 获取设备相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetEquipmentDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "设备类型", DictType = "pm_type", OrderNum = 1, DictStatus=0, Remark = "设备类型字典" },
            new TaktDictType { DictName = "设备状态", DictType = "pm_status", OrderNum = 2, DictStatus=0, Remark = "设备状态字典" },
            new TaktDictType { DictName = "设备维护类型", DictType = "pm_maintenance_type", OrderNum = 3, DictStatus=0, Remark = "设备维护类型字典" },
            new TaktDictType { DictName = "设备故障类型", DictType = "pm_fault_type", OrderNum = 4, DictStatus=0, Remark = "设备故障类型字典" },
            new TaktDictType { DictName = "设备保养周期", DictType = "pm_maintenance_cycle", OrderNum = 5, DictStatus=0, Remark = "设备保养周期字典" },
            new TaktDictType { DictName = "设备计量单位", DictType = "pm_unit", OrderNum = 6, DictStatus=0, Remark = "设备计量单位字典" }
        };
    }
}

