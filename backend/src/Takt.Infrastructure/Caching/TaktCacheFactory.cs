//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktCacheFactory.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 16:30
// 版本号 : V0.0.1
// 描述    : 缓存工厂实现
//===================================================================

using Takt.Domain.IServices.Caching;

namespace Takt.Infrastructure.Caching
{
    /// <summary>
    /// 缓存工厂实现
    /// </summary>
    public class TaktCacheFactory : ITaktCacheFactory
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        public ITaktMemoryCache Memory { get; }

        /// <summary>
        /// Redis缓存
        /// </summary>
        public ITaktRedisCache Redis { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCacheFactory(ITaktMemoryCache memory, ITaktRedisCache redis)
        {
            Memory = memory;
            Redis = redis;
        }
    }
} 



