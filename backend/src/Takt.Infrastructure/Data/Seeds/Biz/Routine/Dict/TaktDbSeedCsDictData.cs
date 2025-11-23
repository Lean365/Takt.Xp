//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCustomerServiceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典数据种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCustomerServiceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典数据种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCustomerServiceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典数据种子数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCustomerServiceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : 客户服务和项目管理相关字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 客户服务和项目管理相关字典数据提供类
/// </summary>
public class TaktDbSeedCsDictData
{
    /// <summary>
    /// 获取客户服务和项目管理相关字典数据
    /// </summary>
    /// <returns>字典数据列表</returns>
    public List<TaktDictData> GetCustomerServiceDictData()
    {
        return new List<TaktDictData>
        {
            // 客户类型
            new TaktDictData { DictType = "cs_customer_type", DictLabel = "战略客户", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "战略客户",  },
            new TaktDictData { DictType = "cs_customer_type", DictLabel = "重点客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "重点客户",  },
            new TaktDictData { DictType = "cs_customer_type", DictLabel = "普通客户", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "普通客户",  },
            new TaktDictData { DictType = "cs_customer_type", DictLabel = "潜在客户", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "潜在客户",  },

            // 客户等级
            new TaktDictData { DictType = "cs_customer_level", DictLabel = "A级", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "A级客户",  },
            new TaktDictData { DictType = "cs_customer_level", DictLabel = "B级", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "B级客户",  },
            new TaktDictData { DictType = "cs_customer_level", DictLabel = "C级", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "C级客户",  },
            new TaktDictData { DictType = "cs_customer_level", DictLabel = "D级", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "D级客户",  },

            // 服务请求类型
            new TaktDictData { DictType = "cs_service_request_type", DictLabel = "产品咨询", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "产品咨询",  },
            new TaktDictData { DictType = "cs_service_request_type", DictLabel = "技术支持", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "技术支持",  },
            new TaktDictData { DictType = "cs_service_request_type", DictLabel = "投诉建议", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "投诉建议",  },
            new TaktDictData { DictType = "cs_service_request_type", DictLabel = "其他服务", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "其他服务",  },

            // 服务请求状态
            new TaktDictData { DictType = "cs_service_request_status", DictLabel = "待处理", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "待处理",  },
            new TaktDictData { DictType = "cs_service_request_status", DictLabel = "处理中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "处理中",  },
            new TaktDictData { DictType = "cs_service_request_status", DictLabel = "已完成", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已完成",  },
            new TaktDictData { DictType = "cs_service_request_status", DictLabel = "已关闭", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已关闭",  },

            // 服务请求优先级
            new TaktDictData { DictType = "cs_service_request_priority", DictLabel = "紧急", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "紧急",  },
            new TaktDictData { DictType = "cs_service_request_priority", DictLabel = "高", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "高优先级",  },
            new TaktDictData { DictType = "cs_service_request_priority", DictLabel = "中", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "中优先级",  },
            new TaktDictData { DictType = "cs_service_request_priority", DictLabel = "低", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "低优先级",  },

            // 项目类型
            new TaktDictData { DictType = "cs_project_type", DictLabel = "研发项目", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "研发项目",  },
            new TaktDictData { DictType = "cs_project_type", DictLabel = "实施项目", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "实施项目",  },
            new TaktDictData { DictType = "cs_project_type", DictLabel = "维护项目", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "维护项目",  },
            new TaktDictData { DictType = "cs_project_type", DictLabel = "咨询项目", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "咨询项目",  },

            // 项目状态
            new TaktDictData { DictType = "cs_project_status", DictLabel = "规划中", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "规划中",  },
            new TaktDictData { DictType = "cs_project_status", DictLabel = "进行中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "进行中",  },
            new TaktDictData { DictType = "cs_project_status", DictLabel = "已完成", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已完成",  },
            new TaktDictData { DictType = "cs_project_status", DictLabel = "已暂停", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已暂停",  },
            new TaktDictData { DictType = "cs_project_status", DictLabel = "已取消", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "已取消",  },

            // 项目优先级
            new TaktDictData { DictType = "cs_project_priority", DictLabel = "紧急", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "紧急",  },
            new TaktDictData { DictType = "cs_project_priority", DictLabel = "高", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "高优先级",  },
            new TaktDictData { DictType = "cs_project_priority", DictLabel = "中", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "中优先级",  },
            new TaktDictData { DictType = "cs_project_priority", DictLabel = "低", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "低优先级",  },

            // 项目风险等级
            new TaktDictData { DictType = "cs_project_risk_level", DictLabel = "高风险", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "高风险",  },
            new TaktDictData { DictType = "cs_project_risk_level", DictLabel = "中风险", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "中风险",  },
            new TaktDictData { DictType = "cs_project_risk_level", DictLabel = "低风险", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "低风险",  },
            new TaktDictData { DictType = "cs_project_risk_level", DictLabel = "无风险", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "无风险",  },

            // 项目里程碑类型
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "项目启动", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "项目启动",  },
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "需求确认", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "需求确认",  },
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "设计完成", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "设计完成",  },
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "开发完成", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "开发完成",  },
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "测试完成", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "测试完成",  },
            new TaktDictData { DictType = "cs_project_milestone_type", DictLabel = "项目验收", DictValue = "6",CssClass=6,ListClass=6, OrderNum = 6,   Remark = "项目验收",  },

            // 服务类型
            new TaktDictData { DictType = "cs_service_type", DictLabel = "技术支持", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "技术支持",  },
            new TaktDictData { DictType = "cs_service_type", DictLabel = "产品咨询", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "产品咨询",  },
            new TaktDictData { DictType = "cs_service_type", DictLabel = "培训服务", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "培训服务",  },
            new TaktDictData { DictType = "cs_service_type", DictLabel = "维护服务", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "维护服务",  },
            new TaktDictData { DictType = "cs_service_type", DictLabel = "其他服务", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "其他服务",  },

            // 服务状态
            new TaktDictData { DictType = "cs_service_status", DictLabel = "待处理", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "待处理",  },
            new TaktDictData { DictType = "cs_service_status", DictLabel = "处理中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "处理中",  },
            new TaktDictData { DictType = "cs_service_status", DictLabel = "已完成", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已完成",  },
            new TaktDictData { DictType = "cs_service_status", DictLabel = "已取消", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已取消",  },

            // 满意度等级
            new TaktDictData { DictType = "cs_satisfaction_level", DictLabel = "非常满意", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "非常满意",  },
            new TaktDictData { DictType = "cs_satisfaction_level", DictLabel = "满意", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "满意",  },
            new TaktDictData { DictType = "cs_satisfaction_level", DictLabel = "一般", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "一般",  },
            new TaktDictData { DictType = "cs_satisfaction_level", DictLabel = "不满意", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "不满意",  },
            new TaktDictData { DictType = "cs_satisfaction_level", DictLabel = "非常不满意", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "非常不满意",  },

            // 投诉类型
            new TaktDictData { DictType = "cs_complaint_type", DictLabel = "产品质量", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "产品质量问题",  },
            new TaktDictData { DictType = "cs_complaint_type", DictLabel = "服务质量", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "服务质量问题",  },
            new TaktDictData { DictType = "cs_complaint_type", DictLabel = "交付问题", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "交付问题",  },
            new TaktDictData { DictType = "cs_complaint_type", DictLabel = "价格问题", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "价格问题",  },
            new TaktDictData { DictType = "cs_complaint_type", DictLabel = "其他", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "其他问题",  },

            // 投诉状态
            new TaktDictData { DictType = "cs_complaint_status", DictLabel = "待处理", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "待处理",  },
            new TaktDictData { DictType = "cs_complaint_status", DictLabel = "处理中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "处理中",  },
            new TaktDictData { DictType = "cs_complaint_status", DictLabel = "已解决", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已解决",  },
            new TaktDictData { DictType = "cs_complaint_status", DictLabel = "已关闭", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已关闭",  },

            // 投诉优先级
            new TaktDictData { DictType = "cs_complaint_priority", DictLabel = "紧急", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "紧急",  },
            new TaktDictData { DictType = "cs_complaint_priority", DictLabel = "高", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "高优先级",  },
            new TaktDictData { DictType = "cs_complaint_priority", DictLabel = "中", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "中优先级",  },
            new TaktDictData { DictType = "cs_complaint_priority", DictLabel = "低", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "低优先级",  },

            // 投诉处理方式
            new TaktDictData { DictType = "cs_complaint_handling", DictLabel = "电话处理", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "电话处理",  },
            new TaktDictData { DictType = "cs_complaint_handling", DictLabel = "邮件处理", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "邮件处理",  },
            new TaktDictData { DictType = "cs_complaint_handling", DictLabel = "现场处理", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "现场处理",  },
            new TaktDictData { DictType = "cs_complaint_handling", DictLabel = "其他方式", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "其他方式",  },

            // 客户来源
            new TaktDictData { DictType = "cs_customer_source", DictLabel = "网络推广", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "网络推广",  },
            new TaktDictData { DictType = "cs_customer_source", DictLabel = "电话营销", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "电话营销",  },
            new TaktDictData { DictType = "cs_customer_source", DictLabel = "客户推荐", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "客户推荐",  },
            new TaktDictData { DictType = "cs_customer_source", DictLabel = "展会活动", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "展会活动",  },
            new TaktDictData { DictType = "cs_customer_source", DictLabel = "其他", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "其他来源",  },

            // 客户行业
            new TaktDictData { DictType = "cs_customer_industry", DictLabel = "制造业", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "制造业",  },
            new TaktDictData { DictType = "cs_customer_industry", DictLabel = "服务业", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "服务业",  },
            new TaktDictData { DictType = "cs_customer_industry", DictLabel = "金融业", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "金融业",  },
            new TaktDictData { DictType = "cs_customer_industry", DictLabel = "教育业", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "教育业",  },
            new TaktDictData { DictType = "cs_customer_industry", DictLabel = "其他", DictValue = "5",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "其他行业",  },

            // 客户规模
            new TaktDictData { DictType = "cs_customer_size", DictLabel = "大型企业", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "大型企业",  },
            new TaktDictData { DictType = "cs_customer_size", DictLabel = "中型企业", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "中型企业",  },
            new TaktDictData { DictType = "cs_customer_size", DictLabel = "小型企业", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "小型企业",  },
            new TaktDictData { DictType = "cs_customer_size", DictLabel = "微型企业", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "微型企业",  },

            // 客户关系
            new TaktDictData { DictType = "cs_customer_relationship", DictLabel = "新客户", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "新客户",  },
            new TaktDictData { DictType = "cs_customer_relationship", DictLabel = "老客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "老客户",  },
            new TaktDictData { DictType = "cs_customer_relationship", DictLabel = "VIP客户", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "VIP客户",  },
            new TaktDictData { DictType = "cs_customer_relationship", DictLabel = "合作伙伴", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "合作伙伴",  },

            // 客户状态
            new TaktDictData { DictType = "cs_customer_status", DictLabel = "潜在客户", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "潜在客户",  },
            new TaktDictData { DictType = "cs_customer_status", DictLabel = "活跃客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "活跃客户",  },
            new TaktDictData { DictType = "cs_customer_status", DictLabel = "休眠客户", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "休眠客户",  },
            new TaktDictData { DictType = "cs_customer_status", DictLabel = "流失客户", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "流失客户",  },

            // 客户标签
            new TaktDictData { DictType = "cs_customer_tag", DictLabel = "重要客户", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "重要客户",  },
            new TaktDictData { DictType = "cs_customer_tag", DictLabel = "优质客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "优质客户",  },
            new TaktDictData { DictType = "cs_customer_tag", DictLabel = "风险客户", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "风险客户",  },
            new TaktDictData { DictType = "cs_customer_tag", DictLabel = "普通客户", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "普通客户",  },

            // 客户分类
            new TaktDictData { DictType = "cs_customer_category", DictLabel = "个人客户", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "个人客户",  },
            new TaktDictData { DictType = "cs_customer_category", DictLabel = "企业客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "企业客户",  },
            new TaktDictData { DictType = "cs_customer_category", DictLabel = "政府客户", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "政府客户",  },
            new TaktDictData { DictType = "cs_customer_category", DictLabel = "其他", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "其他分类",  },

            // 客户价值
            new TaktDictData { DictType = "cs_customer_value", DictLabel = "高价值", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "高价值客户",  },
            new TaktDictData { DictType = "cs_customer_value", DictLabel = "中价值", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "中价值客户",  },
            new TaktDictData { DictType = "cs_customer_value", DictLabel = "低价值", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "低价值客户",  },
            new TaktDictData { DictType = "cs_customer_value", DictLabel = "无价值", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "无价值客户",  }
        };
    }

}

