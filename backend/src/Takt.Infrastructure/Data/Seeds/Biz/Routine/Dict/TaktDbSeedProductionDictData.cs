//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedProductionDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 生产相关字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 生产相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedProductionDictData
{
    /// <summary>
    /// 获取生产相关字典数据
    /// </summary>
    /// <returns>生产相关字典数据列表</returns>
    public List<TaktDictData> GetProductionDictData()
    {
        return new List<TaktDictData>
        {
            // 生产订单类型
            new TaktDictData { DictType = "pp_order_type", DictLabel = "标准生产订单", DictValue = "ZDTA", CssClass=1,ListClass=1, OrderNum = 1,   Remark = "标准生产订单" },
            new TaktDictData { DictType = "pp_order_type", DictLabel = "返工生产订单", DictValue = "ZDTD", CssClass=2,ListClass=2, OrderNum = 2,   Remark = "返工生产订单" },
            new TaktDictData { DictType = "pp_order_type", DictLabel = "样品生产订单", DictValue = "ZDTS", CssClass=3,ListClass=3, OrderNum = 3,   Remark = "样品生产订单" },
            
            // 生产订单号
            new TaktDictData { DictType = "pp_order_code", DictLabel = "生产订单453887", DictValue = "453887", CssClass=1,ListClass=1, OrderNum = 1,   Remark = "生产订单号453887" },
            new TaktDictData { DictType = "pp_order_code", DictLabel = "生产订单453888", DictValue = "453888", CssClass=2,ListClass=2, OrderNum = 2,   Remark = "生产订单号453888" },
            new TaktDictData { DictType = "pp_order_code", DictLabel = "生产订单453889", DictValue = "453889", CssClass=3,ListClass=3, OrderNum = 3,   Remark = "生产订单号453889" },
            new TaktDictData { DictType = "pp_order_code", DictLabel = "生产订单453957", DictValue = "453957", CssClass=4,ListClass=4, OrderNum = 4,   Remark = "生产订单号453957" },
            new TaktDictData { DictType = "pp_order_code", DictLabel = "生产订单453848", DictValue = "453848", CssClass=5,ListClass=5, OrderNum = 5,   Remark = "生产订单号453848" },
            
            // 生产批次
            new TaktDictData { DictType = "pp_batch", DictLabel = "生产批次UR4MD258", DictValue = "UR4MD258", CssClass=1,ListClass=1, OrderNum = 1,   Remark = "生产批次UR4MD258" },

            // 生产线
            new TaktDictData { DictType = "pp_line", DictLabel = "生产线8", DictValue = "8", CssClass=1,ListClass=1, OrderNum = 1,   Remark = "生产线8" },
            
            // 工作中心
            new TaktDictData { DictType = "pp_work_center", DictLabel = "工作中心ZBSZ", DictValue = "ZBSZ", CssClass=1,ListClass=1, OrderNum = 1,   Remark = "工作中心ZBSZ" }
        };
    }
}

