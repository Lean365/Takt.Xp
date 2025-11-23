using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Takt.Domain.Caching
{
    /// <summary>
    /// 定义TaktRedis缓存接口
    /// </summary>
    public interface ITaktRedisCache
    {
        /// <summary>
        /// 根据模式搜索键
        /// </summary>
        /// <param name="pattern">搜索模式</param>
        /// <returns>匹配的键列表</returns>
        Task<List<string>> SearchKeysAsync(string pattern);
    }
} 
