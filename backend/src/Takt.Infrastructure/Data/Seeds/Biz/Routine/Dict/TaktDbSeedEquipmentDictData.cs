//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedEquipmentDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 设备相关字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 设备相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedEquipmentDictData
{
    /// <summary>
    /// 获取设备相关字典数据
    /// </summary>
    /// <returns>设备相关字典数据列表</returns>
    public List<TaktDictData> GetEquipmentDictData()
    {
        return new List<TaktDictData>
        {
            // 设备类型
            new TaktDictData { DictType = "pm_type", DictLabel = "生产设备", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "生产设备",  },
            new TaktDictData { DictType = "pm_type", DictLabel = "检测设备", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "检测设备",  },
            new TaktDictData { DictType = "pm_type", DictLabel = "工装夹具", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "工装夹具",  },
            new TaktDictData { DictType = "pm_type", DictLabel = "模具", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "模具",  },

            // 设备状态
            new TaktDictData { DictType = "pm_status", DictLabel = "正常", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "设备正常运行",  },
            new TaktDictData { DictType = "pm_status", DictLabel = "维修中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "设备正在维修",  },
            new TaktDictData { DictType = "pm_status", DictLabel = "保养中", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "设备正在保养",  },
            new TaktDictData { DictType = "pm_status", DictLabel = "故障", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "设备故障",  },
            new TaktDictData { DictType = "pm_status", DictLabel = "报废", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "设备报废",  },

            // 设备维护类型
            new TaktDictData { DictType = "pm_maintenance_type", DictLabel = "日常保养", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "日常保养",  },
            new TaktDictData { DictType = "pm_maintenance_type", DictLabel = "定期保养", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "定期保养",  },
            new TaktDictData { DictType = "pm_maintenance_type", DictLabel = "预防性维护", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "预防性维护",  },
            new TaktDictData { DictType = "pm_maintenance_type", DictLabel = "故障维修", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "故障维修",  },

            // 设备故障类型
            new TaktDictData { DictType = "pm_fault_type", DictLabel = "机械故障", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "机械故障",  },
            new TaktDictData { DictType = "pm_fault_type", DictLabel = "电气故障", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "电气故障",  },
            new TaktDictData { DictType = "pm_fault_type", DictLabel = "控制系统故障", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "控制系统故障",  },
            new TaktDictData { DictType = "pm_fault_type", DictLabel = "其他故障", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "其他故障",  },

            // 设备保养周期
            new TaktDictData { DictType = "pm_maintenance_cycle", DictLabel = "每日", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "每日保养",  },
            new TaktDictData { DictType = "pm_maintenance_cycle", DictLabel = "每周", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "每周保养",  },
            new TaktDictData { DictType = "pm_maintenance_cycle", DictLabel = "每月", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "每月保养",  },
            new TaktDictData { DictType = "pm_maintenance_cycle", DictLabel = "每季度", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "每季度保养",  },
            new TaktDictData { DictType = "pm_maintenance_cycle", DictLabel = "每年", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "每年保养",  },

            // 设备计量单位
            new TaktDictData { DictType = "pm_unit", DictLabel = "台", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "台",  },
            new TaktDictData { DictType = "pm_unit", DictLabel = "套", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "套",  },
            new TaktDictData { DictType = "pm_unit", DictLabel = "件", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "件",  },
            new TaktDictData { DictType = "pm_unit", DictLabel = "个", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "个",  }
        };
    }
}

