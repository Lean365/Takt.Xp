//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedTranslationCoordinator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 翻译种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 翻译种子数据协调器
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 统一管理所有翻译数据的初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 引用现有翻译种子文件中的数据
/// 4. 提供批量初始化功能
/// 5. 支持翻译数据的增量更新
/// </remarks>
public class TaktDbSeedTranslationCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktTranslation> TranslationRepository => _repositoryFactory.GetBusinessRepository<TaktTranslation>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public TaktDbSeedTranslationCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<TranslationSeedResult> InitializeAllTranslationDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有翻译数据...");

            var result = new TranslationSeedResult();

            // 1. 初始化基础翻译数据
            var basicTranslations = await InitializeBasicTranslationsAsync();
            result.TranslationResults.Add("Basic", basicTranslations);

            // 2. 初始化身份认证翻译数据
            var identityTranslations = await InitializeIdentityTranslationsAsync();
            result.TranslationResults.Add("Identity", identityTranslations);

            // 3. 初始化工作流翻译数据
            var workflowTranslations = await InitializeWorkflowTranslationsAsync();
            result.TranslationResults.Add("Workflow", workflowTranslations);

            // 4. 初始化代码生成翻译数据
            var generatorTranslations = await InitializeGeneratorTranslationsAsync();
            result.TranslationResults.Add("Generator", generatorTranslations);

            // 5. 初始化日志翻译数据
            var logsTranslations = await InitializeLogsTranslationsAsync();
            result.TranslationResults.Add("Logs", logsTranslations);

            // 6. 初始化常规翻译数据
            var routineTranslations = await InitializeRoutineTranslationsAsync();
            result.TranslationResults.Add("Routine", routineTranslations);

            // 7. 初始化SignalR翻译数据
            var signalRTranslations = await InitializeSignalRTranslationsAsync();
            result.TranslationResults.Add("SignalR", signalRTranslations);

            // 8. 初始化菜单翻译数据
            var menuTranslations = await InitializeMenuTranslationsAsync();
            result.TranslationResults.Add("Menu", menuTranslations);

            _logger.Info($"翻译数据初始化完成！翻译数据: {result.GetTotalTranslationCount()} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化翻译数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化基础翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeBasicTranslationsAsync()
    {
        var basicSeed = new TaktDbSeedTranslation();
        var translations = basicSeed.GetDefaultTranslations();
        return await InitializeTranslationsAsync(translations, "基础翻译数据");
    }

    /// <summary>
    /// 初始化身份认证翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeIdentityTranslationsAsync()
    {
        var identitySeed = new TaktIdentitySeedTranslation();
        var translations = identitySeed.GetIdentityTranslations();
        return await InitializeTranslationsAsync(translations, "身份认证翻译数据");
    }

    /// <summary>
    /// 初始化工作流翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeWorkflowTranslationsAsync()
    {
        var workflowSeed = new TaktWorkflowSeedTranslation();
        var translations = workflowSeed.GetWorkflowTranslations();
        return await InitializeTranslationsAsync(translations, "工作流翻译数据");
    }

    /// <summary>
    /// 初始化代码生成翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeGeneratorTranslationsAsync()
    {
        var generatorSeed = new TaktGeneratorSeedTranslation();
        var translations = generatorSeed.GetGeneratorTranslations();
        return await InitializeTranslationsAsync(translations, "代码生成翻译数据");
    }

    /// <summary>
    /// 初始化日志翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeLogsTranslationsAsync()
    {
        var logsSeed = new TaktLogsSeedTranslation();
        var translations = logsSeed.GetLogsTranslations();
        return await InitializeTranslationsAsync(translations, "日志翻译数据");
    }

    /// <summary>
    /// 初始化常规翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeRoutineTranslationsAsync()
    {
        var routineSeed = new TaktRoutineSeedTranslation();
        var translations = routineSeed.GetRoutineTranslations();
        return await InitializeTranslationsAsync(translations, "常规翻译数据");
    }

    /// <summary>
    /// 初始化SignalR翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeSignalRTranslationsAsync()
    {
        var signalRSeed = new TaktSignalRSeedTranslation();
        var translations = signalRSeed.GetSignalRTranslations();
        return await InitializeTranslationsAsync(translations, "SignalR翻译数据");
    }


    /// <summary>
    /// 初始化菜单翻译数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeMenuTranslationsAsync()
    {
        var menuSeed = new TaktMenuSeedTranslation();
        var translations = menuSeed.GetMenuTranslations();
        return await InitializeTranslationsAsync(translations, "菜单翻译数据");
    }

    /// <summary>
    /// 初始化翻译数据
    /// </summary>
    /// <param name="translations">翻译数据列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeTranslationsAsync(List<TaktTranslation> translations, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var translation in translations)
        {
            var existingTranslation = await TranslationRepository.GetFirstAsync(t =>
                t.LangCode == translation.LangCode &&
                t.TransKey == translation.TransKey);

            if (existingTranslation == null)
            {
                translation.CreateBy = "Takt365";
                translation.CreateTime = DateTime.Now;
                translation.UpdateBy = "Takt365";
                translation.UpdateTime = DateTime.Now;

                await TranslationRepository.CreateAsync(translation);
                insertCount++;
                _logger.Info($"[创建] {category} '{translation.TransKey}' ({translation.LangCode}) 创建成功");
            }
            else
            {
                existingTranslation.TransValue = translation.TransValue;
                existingTranslation.TransType = translation.TransType;
                existingTranslation.UpdateBy = "Takt365";
                existingTranslation.UpdateTime = DateTime.Now;

                await TranslationRepository.UpdateAsync(existingTranslation);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingTranslation.TransKey}' ({existingTranslation.LangCode}) 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 根据语言代码获取翻译数据
    /// </summary>
    /// <param name="langCode">语言代码</param>
    /// <returns>翻译数据列表</returns>
    public async Task<List<TaktTranslation>> GetTranslationsByLangCodeAsync(string langCode)
    {
        var translations = await TranslationRepository.GetListAsync(t => t.LangCode == langCode);
        return translations.OrderBy(t => t.TransType).ThenBy(t => t.TransKey).ToList();
    }

    /// <summary>
    /// 根据模块名称获取翻译数据
    /// </summary>
    /// <param name="TransType">模块名称</param>
    /// <returns>翻译数据列表</returns>
    public async Task<List<TaktTranslation>> GetTranslationsByModuleAsync(string TransType)
    {
        if (int.TryParse(TransType, out int transTypeInt))
        {
            var translations = await TranslationRepository.GetListAsync(t => t.TransType == transTypeInt);
            return translations.OrderBy(t => t.LangCode).ThenBy(t => t.TransKey).ToList();
        }
        return new List<TaktTranslation>();
    }

    /// <summary>
    /// 获取所有翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public async Task<List<TaktTranslation>> GetAllTranslationsAsync()
    {
        var translations = await TranslationRepository.GetListAsync();
        return translations.OrderBy(t => t.TransType).ThenBy(t => t.LangCode).ThenBy(t => t.TransKey).ToList();
    }

    /// <summary>
    /// 清理翻译数据
    /// </summary>
    /// <param name="TransType">模块名称（可选）</param>
    /// <returns>清理结果</returns>
    public async Task<bool> CleanTranslationsAsync(string? TransType = null)
    {
        try
        {
            int result;
            if (string.IsNullOrEmpty(TransType))
            {
                result = await TranslationRepository.DeleteAsync(t => true);
                _logger.Info($"清理所有翻译数据，共删除 {result} 条记录");
            }
            else
            {
                if (int.TryParse(TransType, out int transTypeInt))
                {
                    result = await TranslationRepository.DeleteAsync(t => t.TransType == transTypeInt);
                    _logger.Info($"清理模块 '{TransType}' 的翻译数据，共删除 {result} 条记录");
                }
                else
                {
                    _logger.Info($"无效的模块名称: {TransType}");
                    return false;
                }
            }
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.Error($"清理翻译数据失败: {ex.Message}", ex);
            return false;
        }
    }
}

/// <summary>
/// 翻译种子初始化结果
/// </summary>
public class TranslationSeedResult
{
    /// <summary>
    /// 翻译数据初始化结果
    /// </summary>
    public Dictionary<string, (int insertCount, int updateCount)> TranslationResults { get; set; } = new();

    /// <summary>
    /// 获取翻译数据总数
    /// </summary>
    /// <returns>总数</returns>
    public int GetTotalTranslationCount()
    {
        return TranslationResults.Values.Sum(x => x.insertCount + x.updateCount);
    }

    /// <summary>
    /// 获取总插入数
    /// </summary>
    /// <returns>插入数</returns>
    public int GetTotalInsertCount()
    {
        return TranslationResults.Values.Sum(x => x.insertCount);
    }

    /// <summary>
    /// 获取总更新数
    /// </summary>
    /// <returns>更新数</returns>
    public int GetTotalUpdateCount()
    {
        return TranslationResults.Values.Sum(x => x.updateCount);
    }
}



