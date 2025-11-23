//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedSalesDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 销售相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 销售相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedSalesDictData
{
    /// <summary>
    /// 获取销售相关字典数据
    /// </summary>
    /// <returns>销售相关字典数据列表</returns>
    public List<TaktDictData> GetSalesDictData()
    {
        return new List<TaktDictData>
        {
            // 销售类型 - 横排格式
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "直销", DictValue = "DIRECT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "直销", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "经销", DictValue = "DISTRIBUTION", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "经销", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "代理", DictValue = "AGENCY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "代理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "零售", DictValue = "RETAIL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "零售", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "批发", DictValue = "WHOLESALE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "批发", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_type", DictLabel = "出口", DictValue = "EXPORT", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "出口", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售订单状态 - 横排格式
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "草稿", DictValue = "DRAFT", OrderNum = 1,  CssClass = 10, ListClass = 10, Remark = "草稿状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "待确认", DictValue = "PENDING", OrderNum = 2,  CssClass = 11, ListClass = 11, Remark = "待确认状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "已确认", DictValue = "CONFIRMED", OrderNum = 3,  CssClass = 12, ListClass = 12, Remark = "已确认状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "已发货", DictValue = "SHIPPED", OrderNum = 4,  CssClass = 13, ListClass = 13, Remark = "已发货状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "已完成", DictValue = "COMPLETED", OrderNum = 5,  CssClass = 14, ListClass = 14, Remark = "已完成状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_status", DictLabel = "已取消", DictValue = "CANCELLED", OrderNum = 6,  CssClass = 15, ListClass = 15, Remark = "已取消状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售订单类型 - 横排格式
            new TaktDictData { DictType = "sd_sales_order_type", DictLabel = "标准订单", DictValue = "STANDARD", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "标准订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_type", DictLabel = "紧急订单", DictValue = "URGENT", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "紧急订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_type", DictLabel = "样品订单", DictValue = "SAMPLE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "样品订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_type", DictLabel = "试产订单", DictValue = "TRIAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "试产订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_order_type", DictLabel = "返修订单", DictValue = "REPAIR", OrderNum = 5,  CssClass = 6, ListClass = 6, Remark = "返修订单", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售组织 - 横排格式
            new TaktDictData { DictType = "sd_sales_org", DictLabel = "华北销售部", DictValue = "NORTH", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "华北销售部", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_org", DictLabel = "华南销售部", DictValue = "SOUTH", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "华南销售部", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_org", DictLabel = "华东销售部", DictValue = "EAST", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "华东销售部", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_org", DictLabel = "华西销售部", DictValue = "WEST", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "华西销售部", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 分销渠道 - 横排格式
            new TaktDictData { DictType = "sd_distribution_channel", DictLabel = "直销渠道", DictValue = "DIRECT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "直销渠道", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_distribution_channel", DictLabel = "经销商渠道", DictValue = "DEALER", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "经销商渠道", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_distribution_channel", DictLabel = "代理商渠道", DictValue = "AGENT", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "代理商渠道", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_distribution_channel", DictLabel = "电商渠道", DictValue = "E_COMMERCE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "电商渠道", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售区域 - 横排格式
            new TaktDictData { DictType = "sd_sales_area", DictLabel = "一线城市", DictValue = "TIER1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "一线城市", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_area", DictLabel = "二线城市", DictValue = "TIER2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "二线城市", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_area", DictLabel = "三线城市", DictValue = "TIER3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "三线城市", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_area", DictLabel = "县级市场", DictValue = "COUNTY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "县级市场", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售方式 - 横排格式
            new TaktDictData { DictType = "sd_sales_method", DictLabel = "电话销售", DictValue = "PHONE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "电话销售", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_method", DictLabel = "上门销售", DictValue = "DOOR_TO_DOOR", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "上门销售", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_method", DictLabel = "网络销售", DictValue = "ONLINE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "网络销售", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_method", DictLabel = "展会销售", DictValue = "EXHIBITION", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "展会销售", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 销售优先级 - 横排格式
            new TaktDictData { DictType = "sd_sales_priority", DictLabel = "低优先级", DictValue = "LOW", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "低优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_priority", DictLabel = "普通优先级", DictValue = "NORMAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "普通优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_priority", DictLabel = "高优先级", DictValue = "HIGH", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "高优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sd_sales_priority", DictLabel = "紧急优先级", DictValue = "URGENT", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "紧急优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

