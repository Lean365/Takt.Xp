//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedMaterialDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 物料相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 物料相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedMaterialDictType
{
    /// <summary>
    /// 获取物料相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetMaterialDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "物料类型", DictType = "mm_material_type", OrderNum = 1, DictStatus=0, Remark = "物料类型字典" },
            new TaktDictType { DictName = "物料组", DictType = "mm_material_group", OrderNum = 2, DictStatus=0, Remark = "物料组字典" },
            new TaktDictType { DictName = "物料分类", DictType = "mm_material_category", OrderNum = 3, DictStatus=0, Remark = "物料分类字典" },
            new TaktDictType { DictName = "物料状态", DictType = "mm_material_status", OrderNum = 4, DictStatus=0, Remark = "物料状态字典" },
            new TaktDictType { DictName = "物料来源", DictType = "mm_material_source", OrderNum = 5, DictStatus=0, Remark = "物料来源字典" },
            new TaktDictType { DictName = "物料计价方式", DictType = "mm_material_valuation", OrderNum = 6, DictStatus=0, Remark = "物料计价方式字典" },
            new TaktDictType { DictName = "物料批次管理", DictType = "mm_material_batch", OrderNum = 7, DictStatus=0, Remark = "物料批次管理字典" },
            new TaktDictType { DictName = "物料序列号管理", DictType = "mm_material_serial", OrderNum = 8, DictStatus=0, Remark = "物料序列号管理字典" },
            new TaktDictType { DictName = "物料库存管理", DictType = "mm_material_stock", OrderNum = 9, DictStatus=0, Remark = "物料库存管理字典" },
            new TaktDictType { DictName = "物料质量管理", DictType = "mm_material_quality", OrderNum = 10, DictStatus=0, Remark = "物料质量管理字典" },
            new TaktDictType { DictName = "自动采购单", DictType = "mm_auto_purchase_order", OrderNum = 11, DictStatus=0, Remark = "自动采购单字典" },
            new TaktDictType { DictName = "源清单", DictType = "mm_source_list", OrderNum = 12, DictStatus=0, Remark = "源清单字典" },
            new TaktDictType { DictName = "制造商", DictType = "mm_manufacturer", OrderNum = 13, DictStatus=0, Remark = "制造商字典" },
            new TaktDictType { DictName = "MRP类型", DictType = "mm_mrp_type", OrderNum = 14, DictStatus=0, Remark = "MRP类型字典" },
            new TaktDictType { DictName = "MRP控制者", DictType = "mm_mrp_controller", OrderNum = 15, DictStatus=0, Remark = "MRP控制者字典" }
        };
    }
}

