//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRoutineDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 常规业务模块字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 常规业务模块字典类型种子数据提供类
/// </summary>
public class TaktDbSeedRoutineDictType
{
    /// <summary>
    /// 获取默认字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetDefaultDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "优先级", DictType = "routine_priority", OrderNum = 1, DictStatus=0, Remark = "任务优先级字典" },
            new TaktDictType { DictName = "状态类型", DictType = "routine_status", OrderNum = 2, DictStatus=0, Remark = "业务状态字典" },
            new TaktDictType { DictName = "审核状态", DictType = "routine_audit_status", OrderNum = 3, DictStatus=0, Remark = "审核状态字典" },
            new TaktDictType { DictName = "通知类型", DictType = "routine_notify_type", OrderNum = 4, DictStatus=0, Remark = "通知类型字典" },
            new TaktDictType { DictName = "文件类型", DictType = "routine_file_type", OrderNum = 5, DictStatus=0, Remark = "文件类型字典" },
            new TaktDictType { DictName = "业务类型", DictType = "routine_business_type", OrderNum = 6, DictStatus=0, Remark = "业务类型字典" },
            new TaktDictType { DictName = "流程状态", DictType = "routine_workflow_status", OrderNum = 7, DictStatus=0, Remark = "工作流状态字典" },
            new TaktDictType { DictName = "分类类型", DictType = "routine_category_type", OrderNum = 8, DictStatus=0, Remark = "分类类型字典" },
            new TaktDictType { DictName = "标签类型", DictType = "routine_tag_type", OrderNum = 9, DictStatus=0, Remark = "标签类型字典" },
            new TaktDictType { DictName = "评分等级", DictType = "routine_rating_level", OrderNum = 10, DictStatus=0, Remark = "评分等级字典" },
            new TaktDictType { DictName = "系统内置", DictType = "routine_system_builtin", OrderNum = 11, DictStatus=0, Remark = "系统内置标识字典" },
            new TaktDictType { DictName = "是否加密", DictType = "routine_encryption_status", OrderNum = 12, DictStatus=0, Remark = "数据加密状态字典" },
            new TaktDictType { DictName = "CSS类名", DictType = "routine_css_class", OrderNum = 13, DictStatus=0, Remark = "CSS样式类名字典" },
            new TaktDictType { DictName = "列表类名", DictType = "routine_list_class", OrderNum = 14, DictStatus=0, Remark = "列表样式类名字典" },
            new TaktDictType { DictName = "字典数据源", DictType = "routine_dict_source", OrderNum = 15, DictStatus=0, Remark = "字典数据来源字典" },
            new TaktDictType { DictName = "编码规则类型", DictType = "routine_number_rule_type", OrderNum = 16, DictStatus=0, Remark = "编码规则类型字典（1=采购单 2=销售单 3=生产单 4=入库单 5=出库单 6=调拨单 7=盘点单 8=报损单 9=其他）" },
            new TaktDictType { DictName = "模块名称", DictType = "routine_module_name", OrderNum = 17, DictStatus=0, Remark = "系统模块名称字典" },
            new TaktDictType { DictName = "广告类型", DictType = "routine_ad_type", OrderNum = 18, DictStatus=0, Remark = "广告类型字典" },
            new TaktDictType { DictName = "广告计费方式", DictType = "routine_ad_billing_type", OrderNum = 19, DictStatus=0, Remark = "广告计费方式字典" },
            new TaktDictType { DictName = "广告计费状态", DictType = "routine_ad_billing_status", OrderNum = 20, DictStatus=0, Remark = "广告计费状态字典" },
        };
    }
}


