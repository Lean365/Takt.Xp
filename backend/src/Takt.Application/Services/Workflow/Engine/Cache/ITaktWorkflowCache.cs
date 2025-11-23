#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : IWorkflowCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-23 12:00
// 版本号 : V0.0.1
// 描述    : 工作流缓存接口
//===================================================================

using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow.Engine.Cache
{
    /// <summary>
    /// 工作流缓存接口
    /// </summary>
    public interface ITaktWorkflowCache
    {
        /// <summary>
        /// 获取工作流节点
        /// </summary>
        Task<TaktScheme?> GetNodeAsync(long nodeId);

        /// <summary>
        /// 设置工作流节点缓存
        /// </summary>
        Task SetNodeAsync(TaktScheme node, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流节点缓存
        /// </summary>
        Task RemoveNodeAsync(long nodeId);

        /// <summary>
        /// 获取工作流定义
        /// </summary>
        Task<TaktScheme?> GetDefinitionAsync(long definitionId);

        /// <summary>
        /// 设置工作流定义缓存
        /// </summary>
        Task SetDefinitionAsync(TaktScheme definition, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流定义缓存
        /// </summary>
        Task RemoveDefinitionAsync(long definitionId);

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        Task<TaktInstance?> GetInstanceAsync(long instanceId);

        /// <summary>
        /// 设置工作流实例缓存
        /// </summary>
        Task SetInstanceAsync(TaktInstance instance, TimeSpan? expiry = null);

        /// <summary>
        /// 移除工作流实例缓存
        /// </summary>
        Task RemoveInstanceAsync(long instanceId);
    }
}



