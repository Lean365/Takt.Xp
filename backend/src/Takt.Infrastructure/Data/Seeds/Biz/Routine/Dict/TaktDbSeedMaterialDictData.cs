//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedMaterialDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 物料相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 物料相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedMaterialDictData
{
    /// <summary>
    /// 获取物料相关字典数据
    /// </summary>
    /// <returns>物料相关字典数据列表</returns>
    public List<TaktDictData> GetMaterialDictData()
    {
        return new List<TaktDictData>
        {
            // 物料类型 - 横排格式
            new TaktDictData { DictType = "mm_material_type", DictLabel = "原材料", DictValue = "RAW_MATERIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "原材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_type", DictLabel = "半成品", DictValue = "SEMI_FINISHED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "半成品", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_type", DictLabel = "成品", DictValue = "FINISHED_GOODS", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "成品", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_type", DictLabel = "包装材料", DictValue = "PACKAGING", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "包装材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_type", DictLabel = "辅助材料", DictValue = "AUXILIARY", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "辅助材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_type", DictLabel = "备品备件", DictValue = "SPARE_PARTS", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "备品备件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料状态 - 横排格式
            new TaktDictData { DictType = "mm_material_status", DictLabel = "正常", DictValue = "NORMAL", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "正常状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_status", DictLabel = "停用", DictValue = "DISABLED", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "停用状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_status", DictLabel = "淘汰", DictValue = "OBSOLETE", OrderNum = 3,  CssClass = 6, ListClass = 6, Remark = "淘汰状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_status", DictLabel = "开发中", DictValue = "DEVELOPING", OrderNum = 4,  CssClass = 3, ListClass = 3, Remark = "开发中状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料分类 - 横排格式
            new TaktDictData { DictType = "mm_material_category", DictLabel = "金属材料", DictValue = "METAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "金属材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_category", DictLabel = "非金属材料", DictValue = "NON_METAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "非金属材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_category", DictLabel = "化工材料", DictValue = "CHEMICAL", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "化工材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_category", DictLabel = "电子材料", DictValue = "ELECTRONIC", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "电子材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_category", DictLabel = "纺织材料", DictValue = "TEXTILE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "纺织材料", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料组 - 横排格式
            new TaktDictData { DictType = "mm_material_group", DictLabel = "原材料组", DictValue = "RAW_MATERIAL_GROUP", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "原材料组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_group", DictLabel = "半成品组", DictValue = "SEMI_FINISHED_GROUP", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "半成品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_group", DictLabel = "成品组", DictValue = "FINISHED_GOODS_GROUP", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "成品组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_group", DictLabel = "包装材料组", DictValue = "PACKAGING_GROUP", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "包装材料组", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料来源 - 横排格式
            new TaktDictData { DictType = "mm_material_source", DictLabel = "自产", DictValue = "SELF_PRODUCED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "自产", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_source", DictLabel = "外购", DictValue = "PURCHASED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "外购", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_source", DictLabel = "委外加工", DictValue = "OUTSOURCED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "委外加工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_source", DictLabel = "客户提供", DictValue = "CUSTOMER_SUPPLIED", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "客户提供", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料计价方式 - 横排格式
            new TaktDictData { DictType = "mm_material_valuation", DictLabel = "标准成本", DictValue = "STANDARD_COST", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "标准成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_valuation", DictLabel = "移动平均", DictValue = "MOVING_AVERAGE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "移动平均", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_valuation", DictLabel = "先进先出", DictValue = "FIFO", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "先进先出", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_valuation", DictLabel = "后进先出", DictValue = "LIFO", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "后进先出", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料批次管理 - 横排格式
            new TaktDictData { DictType = "mm_material_batch", DictLabel = "需要批次管理", DictValue = "REQUIRED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "需要批次管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_batch", DictLabel = "不需要批次管理", DictValue = "NOT_REQUIRED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "不需要批次管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料序列号管理 - 横排格式
            new TaktDictData { DictType = "mm_material_serial", DictLabel = "需要序列号管理", DictValue = "REQUIRED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "需要序列号管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_serial", DictLabel = "不需要序列号管理", DictValue = "NOT_REQUIRED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "不需要序列号管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料库存管理 - 横排格式
            new TaktDictData { DictType = "mm_material_stock", DictLabel = "需要库存管理", DictValue = "REQUIRED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "需要库存管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_stock", DictLabel = "不需要库存管理", DictValue = "NOT_REQUIRED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "不需要库存管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物料质量管理 - 横排格式
            new TaktDictData { DictType = "mm_material_quality", DictLabel = "需要质量管理", DictValue = "REQUIRED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "需要质量管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_material_quality", DictLabel = "不需要质量管理", DictValue = "NOT_REQUIRED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "不需要质量管理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 自动采购单 - 横排格式
            new TaktDictData { DictType = "mm_auto_purchase_order", DictLabel = "启用", DictValue = "ENABLED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "启用自动采购单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_auto_purchase_order", DictLabel = "禁用", DictValue = "DISABLED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "禁用自动采购单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_auto_purchase_order", DictLabel = "条件启用", DictValue = "CONDITIONAL", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "条件启用自动采购单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 源清单 - 横排格式
            new TaktDictData { DictType = "mm_source_list", DictLabel = "内部生产", DictValue = "INTERNAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "内部生产源清单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_source_list", DictLabel = "外部采购", DictValue = "EXTERNAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "外部采购源清单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_source_list", DictLabel = "委外加工", DictValue = "OUTSOURCING", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "委外加工源清单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_source_list", DictLabel = "混合模式", DictValue = "MIXED", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "混合模式源清单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 制造商 - 横排格式
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "华为", DictValue = "HUAWEI", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "华为技术有限公司", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "苹果", DictValue = "APPLE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "苹果公司", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "三星", DictValue = "SAMSUNG", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "三星电子", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "小米", DictValue = "XIAOMI", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "小米科技", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "联想", DictValue = "LENOVO", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "联想集团", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_manufacturer", DictLabel = "戴尔", DictValue = "DELL", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "戴尔公司", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // MRP类型 - 横排格式
            new TaktDictData { DictType = "mm_mrp_type", DictLabel = "PD", DictValue = "PD", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "PD - 物料需求计划", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_type", DictLabel = "VB", DictValue = "VB", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "VB - 基于消耗的计划", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_type", DictLabel = "VM", DictValue = "VM", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "VM - 基于预测的计划", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_type", DictLabel = "ND", DictValue = "ND", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "ND - 无计划", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_type", DictLabel = "V1", DictValue = "V1", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "V1 - 基于预测的消耗", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // MRP控制者 - 横排格式
            new TaktDictData { DictType = "mm_mrp_controller", DictLabel = "MRP控制者001", DictValue = "001", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "MRP控制者001", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_controller", DictLabel = "MRP控制者002", DictValue = "002", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "MRP控制者002", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_controller", DictLabel = "MRP控制者003", DictValue = "003", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "MRP控制者003", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_controller", DictLabel = "MRP控制者004", DictValue = "004", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "MRP控制者004", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "mm_mrp_controller", DictLabel = "MRP控制者005", DictValue = "005", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "MRP控制者005", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

