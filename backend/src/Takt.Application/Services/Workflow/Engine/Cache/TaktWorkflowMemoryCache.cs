#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : WorkflowMemoryCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-23 12:00
// 版本号 : V0.0.1
// 描述    : 工作流内存缓存实现
//===================================================================

using Microsoft.Extensions.Caching.Memory;
using Takt.Domain.Entities.Workflow;

namespace Takt.Application.Services.Workflow.Engine.Cache
{
    /// <summary>
    /// 工作流内存缓存实现
    /// </summary>
    public class TaktWorkflowMemoryCache : ITaktWorkflowCache
    {
        private readonly IMemoryCache _cache;
        private readonly ITaktLogger _logger;
        private static readonly TimeSpan DefaultExpiry = TimeSpan.FromMinutes(30);

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktWorkflowMemoryCache(IMemoryCache cache, ITaktLogger logger)
        {
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// 获取缓存键
        /// </summary>
        private static string GetNodeKey(long nodeId) => $"workflow:node:{nodeId}";

        private static string GetDefinitionKey(long definitionId) => $"workflow:definition:{definitionId}";

        private static string GetInstanceKey(long instanceId) => $"workflow:instance:{instanceId}";

        /// <summary>
        /// 获取工作流节点
        /// </summary>
        public Task<TaktScheme?> GetNodeAsync(long nodeId)
        {
            try
            {
                var key = GetNodeKey(nodeId);
                var node = _cache.Get<TaktScheme>(key);
                _logger.Debug($"从缓存获取节点: {key}, 结果: {(node != null ? "命中" : "未命中")}");
                return Task.FromResult(node);
            }
            catch (Exception ex)
            {
                _logger.Error($"获取节点缓存失败: {nodeId}", ex);
                return Task.FromResult<TaktScheme?>(null);
            }
        }

        /// <summary>
        /// 设置工作流节点缓存
        /// </summary>
        public Task SetNodeAsync(TaktScheme node, TimeSpan? expiry = null)
        {
            try
            {
                var key = GetNodeKey(node.Id);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expiry ?? DefaultExpiry);
                _cache.Set(key, node, options);
                _logger.Debug($"设置节点缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"设置节点缓存失败: {node.Id}", ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 移除工作流节点缓存
        /// </summary>
        public Task RemoveNodeAsync(long nodeId)
        {
            try
            {
                var key = GetNodeKey(nodeId);
                _cache.Remove(key);
                _logger.Debug($"移除节点缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"移除节点缓存失败: {nodeId}", ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 获取工作流定义
        /// </summary>
        public Task<TaktScheme?> GetDefinitionAsync(long definitionId)
        {
            try
            {
                var key = GetDefinitionKey(definitionId);
                var definition = _cache.Get<TaktScheme>(key);
                _logger.Debug($"从缓存获取定义: {key}, 结果: {(definition != null ? "命中" : "未命中")}");
                return Task.FromResult(definition);
            }
            catch (Exception ex)
            {
                _logger.Error($"获取定义缓存失败: {definitionId}", ex);
                return Task.FromResult<TaktScheme?>(null);
            }
        }

        /// <summary>
        /// 设置工作流定义缓存
        /// </summary>
        public Task SetDefinitionAsync(TaktScheme definition, TimeSpan? expiry = null)
        {
            try
            {
                var key = GetDefinitionKey(definition.Id);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expiry ?? DefaultExpiry);
                _cache.Set(key, definition, options);
                _logger.Debug($"设置定义缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"设置定义缓存失败: {definition.Id}", ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 移除工作流定义缓存
        /// </summary>
        public Task RemoveDefinitionAsync(long definitionId)
        {
            try
            {
                var key = GetDefinitionKey(definitionId);
                _cache.Remove(key);
                _logger.Debug($"移除定义缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"移除定义缓存失败: {definitionId}", ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 获取工作流实例
        /// </summary>
        public Task<TaktInstance?> GetInstanceAsync(long instanceId)
        {
            try
            {
                var key = GetInstanceKey(instanceId);
                var instance = _cache.Get<TaktInstance>(key);
                _logger.Debug($"从缓存获取实例: {key}, 结果: {(instance != null ? "命中" : "未命中")}");
                return Task.FromResult(instance);
            }
            catch (Exception ex)
            {
                _logger.Error($"获取实例缓存失败: {instanceId}", ex);
                return Task.FromResult<TaktInstance?>(null);
            }
        }

        /// <summary>
        /// 设置工作流实例缓存
        /// </summary>
        public Task SetInstanceAsync(TaktInstance instance, TimeSpan? expiry = null)
        {
            try
            {
                var key = GetInstanceKey(instance.Id);
                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expiry ?? DefaultExpiry);
                _cache.Set(key, instance, options);
                _logger.Debug($"设置实例缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"设置实例缓存失败: {instance.Id}", ex);
                return Task.CompletedTask;
            }
        }

        /// <summary>
        /// 移除工作流实例缓存
        /// </summary>
        public Task RemoveInstanceAsync(long instanceId)
        {
            try
            {
                var key = GetInstanceKey(instanceId);
                _cache.Remove(key);
                _logger.Debug($"移除实例缓存: {key}");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.Error($"移除实例缓存失败: {instanceId}", ex);
                return Task.CompletedTask;
            }
        }
    }
}



