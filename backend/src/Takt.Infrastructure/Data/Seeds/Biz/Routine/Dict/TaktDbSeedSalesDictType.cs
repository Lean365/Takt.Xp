//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedSalesDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 销售相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedSalesDictType
{
    /// <summary>
    /// 获取销售相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetSalesDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "销售订单类型", DictType = "sd_sales_order_type", OrderNum = 1, DictStatus=0, Remark = "销售订单类型字典" },
            new TaktDictType { DictName = "销售订单状态", DictType = "sd_sales_order_status", OrderNum = 2, DictStatus=0, Remark = "销售订单状态字典" },
            new TaktDictType { DictName = "销售组织", DictType = "sd_sales_org", OrderNum = 3, DictStatus=0, Remark = "销售组织字典" },
            new TaktDictType { DictName = "分销渠道", DictType = "sd_distribution_channel", OrderNum = 4, DictStatus=0, Remark = "分销渠道字典" },
            new TaktDictType { DictName = "销售区域", DictType = "sd_sales_area", OrderNum = 5, DictStatus=0, Remark = "销售区域字典" },
            new TaktDictType { DictName = "销售类型", DictType = "sd_sales_type", OrderNum = 6, DictStatus=0, Remark = "销售类型字典" },
            new TaktDictType { DictName = "销售方式", DictType = "sd_sales_method", OrderNum = 7, DictStatus=0, Remark = "销售方式字典" },
            new TaktDictType { DictName = "销售优先级", DictType = "sd_sales_priority", OrderNum = 8, DictStatus=0, Remark = "销售优先级字典" },
        };
    }
}

