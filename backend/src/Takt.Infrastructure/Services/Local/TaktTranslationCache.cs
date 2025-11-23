//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTranslationCache.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译缓存服务实现
//===================================================================

using Takt.Domain.Entities.Routine.I18n;
using System.Collections.Concurrent;

namespace Takt.Infrastructure.Services
{
    /// <summary>
    /// 翻译缓存服务实现
    /// </summary>
    public class TaktTranslationCache : ITaktTranslationCache
    {
        private readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        // 使用静态缓存，避免重复初始化
        private static readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _staticTranslations = new();
        private static readonly ConcurrentHashSet<string> _staticSupportedLanguages = new();
        private static readonly object _lockObject = new object();
        private static bool _isInitialized = false;

        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, string>> _translations;
        private readonly ConcurrentHashSet<string> _supportedLanguages;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        public TaktTranslationCache(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger
)
        {
            _repositoryFactory = repositoryFactory;
            _logger = logger;
            _translations = new ConcurrentDictionary<string, ConcurrentDictionary<string, string>>();
            _supportedLanguages = new ConcurrentHashSet<string>();

            // 使用静态缓存，避免重复初始化
            lock (_lockObject)
            {
                if (!_isInitialized)
                {
                    InitializeTranslations().GetAwaiter().GetResult();
                    _isInitialized = true;
                }

                // 从静态缓存复制数据到实例缓存
                foreach (var langPair in _staticTranslations)
                {
                    var langDict = new ConcurrentDictionary<string, string>();
                    foreach (var transPair in langPair.Value)
                    {
                        langDict.TryAdd(transPair.Key, transPair.Value);
                    }
                    _translations.TryAdd(langPair.Key, langDict);
                }

                foreach (var lang in _staticSupportedLanguages)
                {
                    _supportedLanguages.Add(lang);
                }
            }
        }

        /// <summary>
        /// 获取指定语言和键的翻译
        /// </summary>
        public string? GetTranslation(string langCode, string key)
        {
            try
            {
                if (_translations.TryGetValue(langCode, out var langTranslations) &&
                    langTranslations.TryGetValue(key, out var translation))
                {
                    return translation;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.Error("获取翻译失败: {LangCode}, {Key}", langCode, key, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 初始化翻译数据
        /// </summary>
        private async Task InitializeTranslations()
        {
            try
            {
                var translationRepository = _repositoryFactory.GetBusinessRepository<TaktTranslation>();
                var translations = await translationRepository.GetListAsync();

                // 填充静态缓存
                foreach (var translation in translations)
                {
                    var langDict = _staticTranslations.GetOrAdd(translation.LangCode,
                        new ConcurrentDictionary<string, string>());
                    langDict.TryAdd(translation.TransKey, translation.TransValue);
                    _staticSupportedLanguages.Add(translation.LangCode);
                }

                _logger.Info("翻译数据初始化成功，共加载 {Count} 条记录", translations.Count);
            }
            catch (Exception ex)
            {
                _logger.Error("初始化翻译数据失败", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 重新加载翻译数据
        /// </summary>
        public async Task ReloadAsync()
        {
            try
            {
                lock (_lockObject)
                {
                    _staticTranslations.Clear();
                    _staticSupportedLanguages.Clear();
                    _isInitialized = false;
                }

                _translations.Clear();
                _supportedLanguages.Clear();
                await InitializeTranslations();
                _logger.Info("翻译数据重新加载成功");
            }
            catch (Exception ex)
            {
                _logger.Error("重新加载翻译数据失败", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 获取支持的语言列表
        /// </summary>
        public Task<IEnumerable<string>> GetSupportedLanguagesAsync()
        {
            try
            {
                return Task.FromResult<IEnumerable<string>>(_supportedLanguages.ToArray());
            }
            catch (Exception ex)
            {
                _logger.Error("获取支持的语言列表失败", ex.Message);
                throw;
            }
        }
    }

    /// <summary>
    /// 线程安全的HashSet实现
    /// </summary>
    public class ConcurrentHashSet<T> : IEnumerable<T>
    {
        private readonly ConcurrentDictionary<T, byte> _dictionary;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ConcurrentHashSet()
        {
            _dictionary = new ConcurrentDictionary<T, byte>();
        }

        /// <summary>
        /// 添加元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Add(T item)
        {
            return _dictionary.TryAdd(item, 0);
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// 判断是否包含元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return _dictionary.ContainsKey(item);
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            return _dictionary.TryRemove(item, out _);
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _dictionary.Keys.GetEnumerator();
        }

        /// <summary>
        /// 获取集合的枚举器
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}



