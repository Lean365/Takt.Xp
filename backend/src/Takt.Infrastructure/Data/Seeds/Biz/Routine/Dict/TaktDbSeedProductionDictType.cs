//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedProductionDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 生产相关字典类型种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 生产相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedProductionDictType
{
    /// <summary>
    /// 获取生产相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetProductionDictTypes()
    {
        return new List<TaktDictType>
        {
            // 生产订单类型
            new TaktDictType { DictName = "生产订单类型", DictType = "pp_order_type", OrderNum = 1, DictStatus=0, Remark = "生产订单类型字典" },
            
            // 生产订单号
            new TaktDictType { DictName = "生产订单号", DictType = "pp_order_code", OrderNum = 2, DictStatus=0, Remark = "生产订单号字典" },
            
            // 生产批次
            new TaktDictType { DictName = "生产批次", DictType = "pp_batch", OrderNum = 3, DictStatus=0, Remark = "生产批次字典" },
            
            // 生产线
            new TaktDictType { DictName = "生产线", DictType = "pp_line", OrderNum = 4, DictStatus=0, Remark = "生产线字典" },
            
            // 工作中心
            new TaktDictType { DictName = "工作中心", DictType = "work_center", OrderNum = 5, DictStatus=0, Remark = "工作中心字典" }
        };
    }
}

