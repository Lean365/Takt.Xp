//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCustomerServiceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 客户服务和项目管理相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedCsDictType
{
    /// <summary>
    /// 获取客户服务和项目管理相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetCustomerServiceDictTypes()
    {
        return new List<TaktDictType>
        {
            // 客户服务相关
            new TaktDictType { DictName = "客户类型", DictType = "cs_customer_type", DictSource = 0, IsBuiltin = 0, OrderNum = 1, DictStatus=0, Remark = "客户类型字典" },
            new TaktDictType { DictName = "客户等级", DictType = "cs_customer_level", DictSource = 0, IsBuiltin = 0, OrderNum = 2, DictStatus=0, Remark = "客户等级字典" },
            new TaktDictType { DictName = "服务请求类型", DictType = "cs_service_request_type", DictSource = 0, IsBuiltin = 0, OrderNum = 3, DictStatus=0, Remark = "服务请求类型字典" },
            new TaktDictType { DictName = "服务请求状态", DictType = "cs_service_request_status", DictSource = 0, IsBuiltin = 0, OrderNum = 4, DictStatus=0, Remark = "服务请求状态字典" },
            new TaktDictType { DictName = "服务请求优先级", DictType = "cs_service_request_priority", DictSource = 0, IsBuiltin = 0, OrderNum = 5, DictStatus=0, Remark = "服务请求优先级字典" },
            new TaktDictType { DictName = "服务类型", DictType = "cs_service_type", DictSource = 0, IsBuiltin = 0, OrderNum = 6, DictStatus=0, Remark = "服务类型字典" },
            new TaktDictType { DictName = "服务状态", DictType = "cs_service_status", DictSource = 0, IsBuiltin = 0, OrderNum = 7, DictStatus=0, Remark = "服务状态字典" },
            new TaktDictType { DictName = "满意度等级", DictType = "cs_satisfaction_level", DictSource = 0, IsBuiltin = 0, OrderNum = 8, DictStatus=0, Remark = "满意度等级字典" },
            new TaktDictType { DictName = "投诉类型", DictType = "cs_complaint_type", DictSource = 0, IsBuiltin = 0, OrderNum = 9, DictStatus=0, Remark = "投诉类型字典" },
            new TaktDictType { DictName = "投诉状态", DictType = "cs_complaint_status", DictSource = 0, IsBuiltin = 0, OrderNum = 10, DictStatus=0, Remark = "投诉状态字典" },
            new TaktDictType { DictName = "投诉优先级", DictType = "cs_complaint_priority", DictSource = 0, IsBuiltin = 0, OrderNum = 11, DictStatus=0, Remark = "投诉优先级字典" },
            new TaktDictType { DictName = "投诉处理方式", DictType = "cs_complaint_handling", DictSource = 0, IsBuiltin = 0, OrderNum = 12, DictStatus=0, Remark = "投诉处理方式字典" },
            new TaktDictType { DictName = "客户来源", DictType = "cs_customer_source", DictSource = 0, IsBuiltin = 0, OrderNum = 13, DictStatus=0, Remark = "客户来源字典" },
            new TaktDictType { DictName = "客户行业", DictType = "cs_customer_industry", DictSource = 0, IsBuiltin = 0, OrderNum = 14, DictStatus=0, Remark = "客户行业字典" },
            new TaktDictType { DictName = "客户规模", DictType = "cs_customer_size", DictSource = 0, IsBuiltin = 0, OrderNum = 15, DictStatus=0, Remark = "客户规模字典" },
            new TaktDictType { DictName = "客户关系", DictType = "cs_customer_relationship", DictSource = 0, IsBuiltin = 0, OrderNum = 16, DictStatus=0, Remark = "客户关系字典" },
            new TaktDictType { DictName = "客户状态", DictType = "cs_customer_status", DictSource = 0, IsBuiltin = 0, OrderNum = 17, DictStatus=0, Remark = "客户状态字典" },
            new TaktDictType { DictName = "客户标签", DictType = "cs_customer_tag", DictSource = 0, IsBuiltin = 0, OrderNum = 18, DictStatus=0, Remark = "客户标签字典" },
            new TaktDictType { DictName = "客户分类", DictType = "cs_customer_category", DictSource = 0, IsBuiltin = 0, OrderNum = 19, DictStatus=0, Remark = "客户分类字典" },
            new TaktDictType { DictName = "客户价值", DictType = "cs_customer_value", DictSource = 0, IsBuiltin = 0, OrderNum = 20, DictStatus=0, Remark = "客户价值字典" },
            // 项目管理相关
            new TaktDictType { DictName = "项目类型", DictType = "cs_project_type", DictSource = 0, IsBuiltin = 0, OrderNum = 21, DictStatus=0, Remark = "项目类型字典" },
            new TaktDictType { DictName = "项目状态", DictType = "cs_project_status", DictSource = 0, IsBuiltin = 0, OrderNum = 22, DictStatus=0, Remark = "项目状态字典" },
            new TaktDictType { DictName = "项目优先级", DictType = "cs_project_priority", DictSource = 0, IsBuiltin = 0, OrderNum = 23, DictStatus=0, Remark = "项目优先级字典" },
            new TaktDictType { DictName = "项目风险等级", DictType = "cs_project_risk_level", DictSource = 0, IsBuiltin = 0, OrderNum = 24, DictStatus=0, Remark = "项目风险等级字典" },
            new TaktDictType { DictName = "项目里程碑类型", DictType = "cs_project_milestone_type", DictSource = 0, IsBuiltin = 0, OrderNum = 25, DictStatus=0, Remark = "项目里程碑类型字典" }
        };
    }
}

