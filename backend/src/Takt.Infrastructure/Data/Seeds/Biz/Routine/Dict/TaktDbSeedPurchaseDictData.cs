//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedPurchaseDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 采购相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 采购相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedPurchaseDictData
{
    /// <summary>
    /// 获取采购相关字典数据
    /// </summary>
    /// <returns>采购相关字典数据列表</returns>
    public List<TaktDictData> GetPurchaseDictData()
    {
        return new List<TaktDictData>
        {
            // 采购订单类别 - 横排格式
            new TaktDictData { DictType = "mm_purchase_order_category", DictLabel = "标准订单", DictValue = "NB", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "标准订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_category", DictLabel = "带审批的标准订单", DictValue = "ZNB", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "带审批的标准订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_category", DictLabel = "框架协议", DictValue = "FO", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "框架协议", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 采购订单状态 - 横排格式
            new TaktDictData { DictType = "mm_purchase_order_status", DictLabel = "已释放", DictValue = "REL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "订单已完成审批，可发送给供应商并执行。这是可操作的订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_status", DictLabel = "挂起", DictValue = "PEND", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "订单已被暂时冻结（可能是由于价格过高、需要法务审核等原因），无法进行收货和发票校验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_status", DictLabel = "已接受", DictValue = "ACC", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "（常用于外包加工、服务）供应商已确认接受此订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_status", DictLabel = "已关闭", DictValue = "CLSD", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "订单已完成所有业务操作（完全收货和发票校验），系统自动或手动将其关闭，不允许再发生任何业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_order_status", DictLabel = "删除标志", DictValue = "DEL", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "订单已被标记为删除（逻辑删除），但数据仍保存在数据库中", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 采购组 - 横排格式
            new TaktDictData { DictType = "mm_purchase_group", DictLabel = "原材料采购组", DictValue = "RAW_MATERIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "原材料采购组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_group", DictLabel = "设备采购组", DictValue = "EQUIPMENT", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "设备采购组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_group", DictLabel = "服务采购组", DictValue = "SERVICE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "服务采购组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_group", DictLabel = "办公用品采购组", DictValue = "OFFICE_SUPPLIES", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "办公用品采购组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 采购类别 - 横排格式
            new TaktDictData { DictType = "mm_purchase_category", DictLabel = "正常采购", DictValue = "NORMAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "正常采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_category", DictLabel = "紧急采购", DictValue = "URGENT", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "紧急采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_category", DictLabel = "计划采购", DictValue = "PLANNED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "计划采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_category", DictLabel = "零星采购", DictValue = "SPORADIC", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "零星采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_category", DictLabel = "战略采购", DictValue = "STRATEGIC", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "战略采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 采购方式 - 横排格式
            new TaktDictData { DictType = "mm_purchase_method", DictLabel = "招标采购", DictValue = "TENDER", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "招标采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_method", DictLabel = "询价采购", DictValue = "INQUIRY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "询价采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_method", DictLabel = "竞争性谈判", DictValue = "NEGOTIATION", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "竞争性谈判", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_method", DictLabel = "单一来源", DictValue = "SINGLE_SOURCE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "单一来源", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_method", DictLabel = "直接采购", DictValue = "DIRECT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "直接采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 采购优先级 - 横排格式
            new TaktDictData { DictType = "mm_purchase_priority", DictLabel = "低优先级", DictValue = "LOW", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "低优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_priority", DictLabel = "普通优先级", DictValue = "NORMAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "普通优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_priority", DictLabel = "高优先级", DictValue = "HIGH", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "高优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_purchase_priority", DictLabel = "紧急优先级", DictValue = "URGENT", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "紧急优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 收货状态 - 横排格式
            new TaktDictData { DictType = "mm_goods_receipt_status", DictLabel = "未收货", DictValue = "", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "订单项目已创建，但尚未进行任何收货", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_goods_receipt_status", DictLabel = "部分收货", DictValue = "B", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "已收到部分数量，但未完全收齐", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_goods_receipt_status", DictLabel = "完全收货", DictValue = "A", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "订单项目中的所有数量已全部收到", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 产品组 - 横排格式
            new TaktDictData { DictType = "mm_product_group", DictLabel = "电子产品组", DictValue = "ELECTRONICS", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "电子产品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_product_group", DictLabel = "机械产品组", DictValue = "MACHINERY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "机械产品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_product_group", DictLabel = "化工产品组", DictValue = "CHEMICALS", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "化工产品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_product_group", DictLabel = "纺织产品组", DictValue = "TEXTILES", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "纺织产品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_product_group", DictLabel = "食品产品组", DictValue = "FOOD", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "食品产品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 跨工厂物料状态 - 横排格式
            new TaktDictData { DictType = "mm_cross_plant_material_status", DictLabel = "正常", DictValue = "NORMAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "跨工厂物料状态正常", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_cross_plant_material_status", DictLabel = "停用", DictValue = "DISABLED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "跨工厂物料状态停用", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_cross_plant_material_status", DictLabel = "限制", DictValue = "RESTRICTED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "跨工厂物料状态限制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_cross_plant_material_status", DictLabel = "冻结", DictValue = "FROZEN", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "跨工厂物料状态冻结", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 运输组 - 横排格式
            new TaktDictData { DictType = "mm_transport_group", DictLabel = "陆运组", DictValue = "LAND", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "陆运运输组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_transport_group", DictLabel = "海运组", DictValue = "SEA", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "海运运输组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_transport_group", DictLabel = "空运组", DictValue = "AIR", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "空运运输组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_transport_group", DictLabel = "铁路运输组", DictValue = "RAIL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "铁路运输组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_transport_group", DictLabel = "快递组", DictValue = "EXPRESS", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "快递运输组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 原产地国 - 横排格式
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "中国", DictValue = "CN", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "中华人民共和国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "美国", DictValue = "US", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "美利坚合众国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "德国", DictValue = "DE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "德意志联邦共和国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "日本", DictValue = "JP", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "日本国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "韩国", DictValue = "KR", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "大韩民国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "英国", DictValue = "GB", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "大不列颠及北爱尔兰联合王国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "法国", DictValue = "FR", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "法兰西共和国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_country_of_origin", DictLabel = "意大利", DictValue = "IT", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "意大利共和国", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 批量大小 - 横排格式
            new TaktDictData { DictType = "mm_batch_size", DictLabel = "1", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "批量大小1", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_batch_size", DictLabel = "10", DictValue = "10", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "批量大小10", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_batch_size", DictLabel = "100", DictValue = "100", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "批量大小100", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_batch_size", DictLabel = "1000", DictValue = "1000", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "批量大小1000", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_batch_size", DictLabel = "10000", DictValue = "10000", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "批量大小10000", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 特殊采购类 - 横排格式
            new TaktDictData { DictType = "mm_special_procurement", DictLabel = "空白", DictValue = "", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "空白特殊采购类", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_special_procurement", DictLabel = "E", DictValue = "E", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "E - 外部采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_special_procurement", DictLabel = "F", DictValue = "F", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "F - 从库存", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_special_procurement", DictLabel = "L", DictValue = "L", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "L - 分包", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_special_procurement", DictLabel = "X", DictValue = "X", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "X - 特殊采购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 计划交货时间 - 横排格式
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "1天", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "计划交货时间1天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "3天", DictValue = "3", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "计划交货时间3天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "7天", DictValue = "7", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "计划交货时间7天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "14天", DictValue = "14", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "计划交货时间14天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "30天", DictValue = "30", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "计划交货时间30天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_planned_delivery_time", DictLabel = "60天", DictValue = "60", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "计划交货时间60天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 自制生产 - 横排格式
            new TaktDictData { DictType = "mm_in_house_production", DictLabel = "是", DictValue = "Y", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "自制生产", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_in_house_production", DictLabel = "否", DictValue = "N", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "非自制生产", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 价格控制 - 横排格式
            new TaktDictData { DictType = "mm_price_control", DictLabel = "S", DictValue = "S", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "S - 标准价格控制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_price_control", DictLabel = "V", DictValue = "V", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "V - 移动平均价格控制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_price_control", DictLabel = "P", DictValue = "P", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "P - 期间价格控制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 评估类别 - 横排格式
            new TaktDictData { DictType = "mm_valuation_category", DictLabel = "原材料", DictValue = "RAW_MATERIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "原材料评估类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_valuation_category", DictLabel = "半成品", DictValue = "SEMI_FINISHED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "半成品评估类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_valuation_category", DictLabel = "成品", DictValue = "FINISHED_GOODS", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "成品评估类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_valuation_category", DictLabel = "贸易商品", DictValue = "TRADING_GOODS", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "贸易商品评估类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_valuation_category", DictLabel = "服务", DictValue = "SERVICE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "服务评估类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

