//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedWorkflowDictData.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 工作流字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 工作流字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedWorkflowDictData
{
    /// <summary>
    /// 获取工作流相关字典数据
    /// </summary>
    /// <returns>工作流相关字典数据列表</returns>
    public List<TaktDictData> GetWorkflowDictData()
    {
        return new List<TaktDictData>
        {
            // 表单类型 (wf_form_type)
            new TaktDictData { DictType = "wf_form_type", DictLabel = "请假申请", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "员工请假申请表单" },
            new TaktDictData { DictType = "wf_form_type", DictLabel = "报销申请", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "费用报销申请表单" },
            new TaktDictData { DictType = "wf_form_type", DictLabel = "采购申请", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "采购申请表单" },
            new TaktDictData { DictType = "wf_form_type", DictLabel = "合同审批", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "合同审批表单" },
            new TaktDictData { DictType = "wf_form_type", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "其他类型表单" },

            // 表单分类 (wf_form_category)
            new TaktDictData { DictType = "wf_form_category", DictLabel = "日常事务", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "日常事务相关表单" },
            new TaktDictData { DictType = "wf_form_category", DictLabel = "会计核算", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "财务相关表单" },
            new TaktDictData { DictType = "wf_form_category", DictLabel = "后勤管理", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "采购相关表单" },
            new TaktDictData { DictType = "wf_form_category", DictLabel = "人力资源", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "合同相关表单" },
            new TaktDictData { DictType = "wf_form_category", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "其他分类表单" },

            // 表单状态 (wf_form_status)
            new TaktDictData { DictType = "wf_form_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "表单草稿状态" },
            new TaktDictData { DictType = "wf_form_status", DictLabel = "已发布", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "表单已发布状态" },
            new TaktDictData { DictType = "wf_form_status", DictLabel = "已停用", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "表单已停用状态" },

            // 流程分类 (wf_scheme_category)
            new TaktDictData { DictType = "wf_scheme_category", DictLabel = "人事流程", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "人事相关流程" },
            new TaktDictData { DictType = "wf_scheme_category", DictLabel = "财务流程", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "财务相关流程" },
            new TaktDictData { DictType = "wf_scheme_category", DictLabel = "采购流程", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "采购相关流程" },
            new TaktDictData { DictType = "wf_scheme_category", DictLabel = "合同流程", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "合同相关流程" },
            new TaktDictData { DictType = "wf_scheme_category", DictLabel = "其他", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "其他分类流程" },

            // 流程定义状态 (wf_scheme_status)
            new TaktDictData { DictType = "wf_scheme_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "流程定义草稿状态" },
            new TaktDictData { DictType = "wf_scheme_status", DictLabel = "已发布", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "流程定义已发布状态" },
            new TaktDictData { DictType = "wf_scheme_status", DictLabel = "已停用", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "流程定义已停用状态" },

            // 流程实例状态 (wf_instance_status)
            new TaktDictData { DictType = "wf_instance_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "流程实例草稿状态" },
            new TaktDictData { DictType = "wf_instance_status", DictLabel = "运行中", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "流程实例运行中状态" },
            new TaktDictData { DictType = "wf_instance_status", DictLabel = "已完成", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "流程实例已完成状态" },
            new TaktDictData { DictType = "wf_instance_status", DictLabel = "已终止", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "流程实例已终止状态" },
            new TaktDictData { DictType = "wf_instance_status", DictLabel = "已暂停", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "流程实例已暂停状态" },

            // 优先级 (wf_instance_priority)
            new TaktDictData { DictType = "wf_instance_priority", DictLabel = "低", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "低优先级" },
            new TaktDictData { DictType = "wf_instance_priority", DictLabel = "普通", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "普通优先级" },
            new TaktDictData { DictType = "wf_instance_priority", DictLabel = "高", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "高优先级" },
            new TaktDictData { DictType = "wf_instance_priority", DictLabel = "紧急", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "紧急优先级" },
            new TaktDictData { DictType = "wf_instance_priority", DictLabel = "特急", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "特急优先级" },

            // 紧急程度 (wf_instance_urgency)
            new TaktDictData { DictType = "wf_instance_urgency", DictLabel = "普通", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "普通紧急程度" },
            new TaktDictData { DictType = "wf_instance_urgency", DictLabel = "加急", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "加急紧急程度" },
            new TaktDictData { DictType = "wf_instance_urgency", DictLabel = "特急", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "特急紧急程度" },

            // 实体操作类型 (wf_instance_oper_type)
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "提交", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "提交操作" },
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "审批", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "审批操作" },
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "驳回", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "驳回操作" },
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "转办", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "转办操作" },
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "终止", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "终止操作" },
            new TaktDictData { DictType = "wf_instance_oper_type", DictLabel = "撤回", DictValue = "6", CssClass = 6, ListClass = 6, OrderNum = 6,  Remark = "撤回操作" },

            // 流转类型 (wf_instance_trans_type)
            new TaktDictData { DictType = "wf_instance_trans_type", DictLabel = "正常流转", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "正常流转" },
            new TaktDictData { DictType = "wf_instance_trans_type", DictLabel = "驳回流转", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "驳回流转" },
            new TaktDictData { DictType = "wf_instance_trans_type", DictLabel = "转办流转", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "转办流转" },
            new TaktDictData { DictType = "wf_instance_trans_type", DictLabel = "终止流转", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "终止流转" },

            // 流转结果 (wf_instance_trans_result)
            new TaktDictData { DictType = "wf_instance_trans_result", DictLabel = "失败", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "流转失败" },
            new TaktDictData { DictType = "wf_instance_trans_result", DictLabel = "成功", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "流转成功" },

            // 节点类型 (wf_node_type)
            new TaktDictData { DictType = "wf_node_type", DictLabel = "开始", DictValue = "start", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "开始节点" },
            new TaktDictData { DictType = "wf_node_type", DictLabel = "审批", DictValue = "approval", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "审批节点" },
            new TaktDictData { DictType = "wf_node_type", DictLabel = "分支", DictValue = "branch", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "分支节点" },
            new TaktDictData { DictType = "wf_node_type", DictLabel = "并行", DictValue = "parallel", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "并行节点" },
            new TaktDictData { DictType = "wf_node_type", DictLabel = "汇聚", DictValue = "join", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "汇聚节点" },
            new TaktDictData { DictType = "wf_node_type", DictLabel = "结束", DictValue = "end", CssClass = 6, ListClass = 6, OrderNum = 6,  Remark = "结束节点" },

            // 审批人类型 (wf_approver_type)
            new TaktDictData { DictType = "wf_approver_type", DictLabel = "指定用户", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "指定具体用户" },
            new TaktDictData { DictType = "wf_approver_type", DictLabel = "角色", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "指定角色" },
            new TaktDictData { DictType = "wf_approver_type", DictLabel = "部门", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "指定部门" },
            new TaktDictData { DictType = "wf_approver_type", DictLabel = "动态", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "动态指定" },


            // 转化状态 (wf_instance_trans_status)
            new TaktDictData { DictType = "wf_instance_trans_status", DictLabel = "未转化", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "工作流未开始转化" },
            new TaktDictData { DictType = "wf_instance_trans_status", DictLabel = "转化中", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "工作流正在转化中" },
            new TaktDictData { DictType = "wf_instance_trans_status", DictLabel = "已转化", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "工作流转化完成" },
            new TaktDictData { DictType = "wf_instance_trans_status", DictLabel = "转化失败", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "工作流转化失败" },
            new TaktDictData { DictType = "wf_instance_trans_status", DictLabel = "已取消", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "工作流转化已取消" }
        };
    }
}



