#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowResults.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流执行结果类
//===================================================================

using System.Collections.Generic;

namespace Takt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流审批结果
    /// </summary>
    public class TaktApproveResult
    {
        /// <summary>
        /// 是否审批成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 下一个节点ID
        /// </summary>
        public string? NextNodeId { get; set; }

        /// <summary>
        /// 下一个节点名称
        /// </summary>
        public string? NextNodeName { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int WorkflowStatus { get; set; }

        /// <summary>
        /// 输出变量
        /// </summary>
        public Dictionary<string, object>? OutputVariables { get; set; }
    }

    /// <summary>
    /// 工作流启动结果
    /// </summary>
    public class TaktStartResult
    {
        /// <summary>
        /// 是否启动成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 第一个节点ID
        /// </summary>
        public string? FirstNodeId { get; set; }

        /// <summary>
        /// 第一个节点名称
        /// </summary>
        public string? FirstNodeName { get; set; }
    }

    /// <summary>
    /// 工作流状态结果
    /// </summary>
    public class TaktStatusResult
    {
        /// <summary>
        /// 工作流实例ID
        /// </summary>
        public long InstanceId { get; set; }

        /// <summary>
        /// 工作流状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string? CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string? CurrentNodeName { get; set; }

        /// <summary>
        /// 可用操作列表
        /// </summary>
        public List<string> AvailableOperations { get; set; } = new();
    }
}

