//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedWorkflowDictType.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 工作流字典类型种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz;

/// <summary>
/// 工作流相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedWorkflowDictType
{
    /// <summary>
    /// 获取工作流相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetWorkflowDictTypes()
    {
        return new List<TaktDictType>
        {
            // 表单相关字典
            new TaktDictType { DictName = "表单类型", DictType = "wf_form_type", OrderNum = 1, DictStatus=0, Remark = "工作流表单类型字典(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)" },
            new TaktDictType { DictName = "表单分类", DictType = "wf_form_category", OrderNum = 2, DictStatus=0, Remark = "工作流表单分类字典(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)" },
            new TaktDictType { DictName = "表单状态", DictType = "wf_form_status", OrderNum = 3, DictStatus=0, Remark = "工作流表单状态字典(0:草稿 1:已发布 2:已停用)" },
            
            // 流程定义相关字典
            new TaktDictType { DictName = "流程分类", DictType = "wf_scheme_category", OrderNum = 4, DictStatus=0, Remark = "工作流流程分类字典(1:人事流程 2:财务流程 3:采购流程 4:合同流程 5:其他)" },
            new TaktDictType { DictName = "流程定义状态", DictType = "wf_scheme_status", OrderNum = 5, DictStatus=0, Remark = "工作流定义状态字典(0:草稿 1:已发布 2:已停用)" },
            
            // 流程实例相关字典
            new TaktDictType { DictName = "流程实例状态", DictType = "wf_instance_status", OrderNum = 6, DictStatus=0, Remark = "工作流实例状态字典(0:草稿 1:运行中 2:已完成 3:已终止 4:已暂停)" },
            new TaktDictType { DictName = "优先级", DictType = "wf_instance_priority", OrderNum = 7, DictStatus=0, Remark = "工作流实例优先级字典(1:低 2:普通 3:高 4:紧急 5:特急)" },
            new TaktDictType { DictName = "紧急程度", DictType = "wf_instance_urgency", OrderNum = 8, DictStatus=0, Remark = "工作流实例紧急程度字典(1:普通 2:加急 3:特急)" },
            
            // 实例操作相关字典
            new TaktDictType { DictName = "实体操作类型", DictType = "wf_instance_oper_type", OrderNum = 9, DictStatus=0, Remark = "工作流操作类型字典(1:提交 2:审批 3:驳回 4:转办 5:终止 6:撤回)" },
            
            // 实例流转相关字典
            new TaktDictType { DictName = "流转类型", DictType = "wf_instance_trans_type", OrderNum = 10, DictStatus=0, Remark = "工作流流转类型字典(1:正常流转 2:驳回流转 3:转办流转 4:终止流转)" },

            new TaktDictType { DictName = "流转结果", DictType = "wf_instance_trans_result", OrderNum = 11, DictStatus=0, Remark = "工作流流转结果字典(0:失败 1:成功)" },
            
            // 节点相关字典
            new TaktDictType { DictName = "节点类型", DictType = "wf_node_type", OrderNum = 12, DictStatus=0, Remark = "工作流节点类型字典(start:开始 approval:审批 branch:分支 parallel:并行 join:汇聚 end:结束)" },
            
            // 审批人相关字典
            new TaktDictType { DictName = "审批人类型", DictType = "wf_approver_type", OrderNum = 13, DictStatus=0, Remark = "工作流审批人类型字典(1:指定用户 2:角色 3:部门 4:动态)" },
            
            // 转化状态相关字典
            new TaktDictType { DictName = "转化状态", DictType = "wf_instance_trans_status", OrderNum = 14, DictStatus=0, Remark = "工作流转化状态字典(0:未转化 1:转化中 2:已转化 3:转化失败 4:已取消)" }
        };
    }
}



