//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedPurchaseDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 采购相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedPurchaseDictType
{
    /// <summary>
    /// 获取采购相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetPurchaseDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "采购订单类别", DictType = "mm_purchase_order_category", OrderNum = 1, DictStatus=0, Remark = "采购订单类别字典" },
            new TaktDictType { DictName = "采购订单状态", DictType = "mm_purchase_order_status", OrderNum = 2, DictStatus=0, Remark = "采购订单状态字典" },
            new TaktDictType { DictName = "采购组", DictType = "mm_purchase_group", OrderNum = 3, DictStatus=0, Remark = "采购组字典" },
            new TaktDictType { DictName = "采购类别", DictType = "mm_purchase_category", OrderNum = 4, DictStatus=0, Remark = "采购类别字典" },
            new TaktDictType { DictName = "采购方式", DictType = "mm_purchase_method", OrderNum = 5, DictStatus=0, Remark = "采购方式字典" },
            new TaktDictType { DictName = "采购优先级", DictType = "mm_purchase_priority", OrderNum = 6, DictStatus=0, Remark = "采购优先级字典" },
            new TaktDictType { DictName = "收货状态", DictType = "mm_goods_receipt_status", OrderNum = 7, DictStatus=0, Remark = "收货状态字典" },
            new TaktDictType { DictName = "产品组", DictType = "mm_product_group", OrderNum = 8, DictStatus=0, Remark = "产品组字典" },
            new TaktDictType { DictName = "跨工厂物料状态", DictType = "mm_cross_plant_material_status", OrderNum = 9, DictStatus=0, Remark = "跨工厂物料状态字典" },
            new TaktDictType { DictName = "运输组", DictType = "mm_transport_group", OrderNum = 10, DictStatus=0, Remark = "运输组字典" },
            new TaktDictType { DictName = "原产地国", DictType = "mm_country_of_origin", OrderNum = 11, DictStatus=0, Remark = "原产地国字典" },
            new TaktDictType { DictName = "批量大小", DictType = "mm_batch_size", OrderNum = 12, DictStatus=0, Remark = "批量大小字典" },
            new TaktDictType { DictName = "特殊采购类", DictType = "mm_special_procurement", OrderNum = 13, DictStatus=0, Remark = "特殊采购类字典" },
            new TaktDictType { DictName = "计划交货时间", DictType = "mm_planned_delivery_time", OrderNum = 14, DictStatus=0, Remark = "计划交货时间字典" },
            new TaktDictType { DictName = "自制生产", DictType = "mm_in_house_production", OrderNum = 15, DictStatus=0, Remark = "自制生产字典" },
            new TaktDictType { DictName = "价格控制", DictType = "mm_price_control", OrderNum = 16, DictStatus=0, Remark = "价格控制字典" },
            new TaktDictType { DictName = "评估类别", DictType = "mm_valuation_category", OrderNum = 17, DictStatus=0, Remark = "评估类别字典" }
        };
    }
}

