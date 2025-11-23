#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 工作流
// 命名空间 : Takt.Domain.Models.Workflow
// 文件名 : TaktWorkflowNodeConfig.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 工作流节点配置
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.Collections.Generic;

namespace Takt.Domain.Models.Workflow
{
  /// <summary>
  /// 工作流节点配置
  /// </summary>
  public class TaktNodeConfig
  {
    /// <summary>
    /// 审批人类型
    /// 0: 未指定
    /// 1: 指定人员
    /// 2: 指定角色
    /// 3: 指定部门
    /// 4: 发起人自选
    /// 5: 发起人上级
    /// 6: 部门主管
    /// </summary>
    public int ApproverType { get; set; }

    /// <summary>
    /// 指定审批人ID列表
    /// </summary>
    public List<long>? ApproverIds { get; set; }

    /// <summary>
    /// 指定角色ID列表
    /// </summary>
    public List<long>? RoleIds { get; set; }

    /// <summary>
    /// 指定部门ID列表
    /// </summary>
    public List<long>? DepartmentIds { get; set; }

    /// <summary>
    /// 审批规则
    /// 0: 所有人
    /// 1: 任意人
    /// 2: 指定数量
    /// 3: 自定义条件
    /// </summary>
    public int ApprovalRule { get; set; }

    /// <summary>
    /// 审批数量(当ApprovalRule为Count时有效)
    /// </summary>
    public int? ApprovalCount { get; set; }

    /// <summary>
    /// 自定义审批条件表达式
    /// </summary>
    public string? CustomCondition { get; set; }

    /// <summary>
    /// 允许多分支(仅对分支节点有效)
    /// </summary>
    public bool AllowMultipleBranches { get; set; }

    /// <summary>
    /// 并行分支数量(仅对并行节点有效)
    /// </summary>
    public int? ParallelBranchCount { get; set; }

    /// <summary>
    /// 汇聚类型(仅对汇聚节点有效)
    /// 0: 所有分支
    /// 1: 任意分支
    /// 2: 自定义条件
    /// </summary>
    public int? JoinType { get; set; }

    /// <summary>
    /// 汇聚条件(当JoinType为Custom时有效)
    /// </summary>
    public string? JoinCondition { get; set; }

    /// <summary>
    /// 超时时间(小时)
    /// </summary>
    public int? TimeoutHours { get; set; }

    /// <summary>
    /// 超时通知
    /// </summary>
    public bool TimeoutNotify { get; set; }

    /// <summary>
    /// 超时动作
    /// 0: 无动作
    /// 1: 自动通过
    /// 2: 自动拒绝
    /// 3: 自动转交
    /// </summary>
    public int? TimeoutAction { get; set; }

    /// <summary>
    /// 自定义属性
    /// </summary>
    public Dictionary<string, object>? CustomProperties { get; set; }
  }
}



