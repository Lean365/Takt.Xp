#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktApproverResolver.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流审批人解析器接口
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Domain.Models.Workflow;

namespace Takt.Application.Services.Workflow.Engine.Resolvers
{
  /// <summary>
  /// 工作流审批人解析器接口
  /// </summary>
  public interface ITaktApproverResolver
  {
    /// <summary>
    /// 解析审批人
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="nodeId">节点ID</param>
    /// <param name="approverType">审批人类型</param>
    /// <param name="approverConfig">审批人配置(JSON格式)</param>
    /// <param name="variables">工作流变量</param>
    /// <returns>审批人ID列表</returns>
    Task<List<long>> ResolveApproversAsync(
        long instanceId,
        string nodeId,
        int approverType,
        string? approverConfig,
        Dictionary<string, object>? variables = null);

    /// <summary>
    /// 验证审批人配置
    /// </summary>
    /// <param name="approverType">审批人类型</param>
    /// <param name="approverConfig">审批人配置</param>
    /// <returns>是否有效</returns>
    bool ValidateConfig(int approverType, string? approverConfig);

    /// <summary>
    /// 获取审批人类型描述
    /// </summary>
    /// <param name="approverType">审批人类型</param>
    /// <returns>类型描述</returns>
    string GetApproverTypeDescription(int approverType);

    /// <summary>
    /// 解析审批人（OpenAuth.Net 标准）
    /// </summary>
    /// <param name="node">流程节点</param>
    /// <param name="instanceCreateUserId">流程实例创建人ID</param>
    /// <param name="runtimeDesignates">运行时指定的审批人（用于 RUNTIME_SPECIAL_USER 和 RUNTIME_SPECIAL_ROLE）</param>
    /// <returns>审批人ID列表（逗号分隔的字符串，或 "1" 表示所有人）</returns>
    Task<string> ResolveApproversByNodeAsync(
        TaktFlowNode node,
        long instanceCreateUserId,
        string[]? runtimeDesignates = null);
  }
}

