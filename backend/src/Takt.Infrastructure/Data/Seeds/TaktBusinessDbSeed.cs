//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBusinessDbSeed.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 业务数据库种子数据初始化类 - 使用协调器模式
//===================================================================

using Takt.Shared.Exceptions;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds.Biz;
using Takt.Infrastructure.Data.Seeds.Biz.Dict;
using Takt.Infrastructure.Data.Seeds.Biz.Hrm;
using Takt.Infrastructure.Data.Seeds.Biz.Logistics;
using Takt.Infrastructure.Data.Seeds.Biz.Accounting;
using Takt.Infrastructure.Data.Seeds.Biz.Routine;
using Takt.Infrastructure.Data.Seeds.Biz.Translation;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 业务数据库种子数据初始化类
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 统一管理业务数据库的种子数据初始化
/// 2. 使用协调器模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktBusinessDbSeed
{
    private readonly TaktBusinessDbContext _context;
    private readonly ITaktLogger _logger;
    private readonly TaktDbSeedTranslationCoordinator _translationSeed;
    private readonly TaktDbSeedRoutineCoordinator _routineSeed;
    private readonly TaktDbSeedDictCoordinator _dictSeed;
    private readonly TaktDbSeedHrmCoordinator _hrmSeed;
    private readonly TaktDbSeedLogisticsCoordinator _logisticsSeed;
    private readonly TaktDbSeedAccountingCoordinator _accountingSeed;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktBusinessDbSeed(
        TaktBusinessDbContext context,
        ITaktLogger logger,
        TaktDbSeedTranslationCoordinator translationSeed,
        TaktDbSeedRoutineCoordinator routineSeed,
        TaktDbSeedDictCoordinator dictSeed,
        TaktDbSeedHrmCoordinator hrmSeed,
        TaktDbSeedLogisticsCoordinator logisticsSeed,
        TaktDbSeedAccountingCoordinator accountingSeed)
    {
        _context = context;
        _logger = logger;
        _translationSeed = translationSeed;
        _routineSeed = routineSeed;
        _dictSeed = dictSeed;
        _hrmSeed = hrmSeed;
        _logisticsSeed = logisticsSeed;
        _accountingSeed = accountingSeed;
    }

    /// <summary>
    /// 初始化业务数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化业务数据库种子数据...");

            // 初始化常规业务模块数据（配置和语言）
            var routineResult = await _routineSeed.InitializeAllRoutineDataAsync();
            _logger.Info($"常规业务模块数据初始化完成: 配置 {routineResult.ConfigResults.insertCount + routineResult.ConfigResults.updateCount} 条, 语言 {routineResult.LanguageResults.insertCount + routineResult.LanguageResults.updateCount} 条");

            // 初始化翻译数据
            var translationResult = await _translationSeed.InitializeAllTranslationDataAsync();
            _logger.Info($"翻译数据初始化完成: 新增 {translationResult.GetTotalInsertCount()} 条, 更新 {translationResult.GetTotalUpdateCount()} 条");

            // 初始化字典数据
            var dictResult = await _dictSeed.InitializeAllDictDataAsync();
            _logger.Info($"字典数据初始化完成: 字典类型 {dictResult.GetTotalDictTypeCount()} 个, 字典数据 {dictResult.GetTotalDictDataCount()} 条");

            // 初始化人力资源数据
            await _hrmSeed.InitializeAllHrmDataAsync();
            _logger.Info("人力资源数据初始化完成");

            // 初始化后勤数据
            await _logisticsSeed.InitializeAllLogisticsDataAsync();
            _logger.Info("后勤数据初始化完成");

            // 初始化会计数据
            await _accountingSeed.InitializeAllAccountingDataAsync();
            _logger.Info("会计数据初始化完成");

            _logger.Info("业务数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"业务数据库种子数据初始化失败: {ex.Message}", ex);
            throw new TaktException("业务数据库种子数据初始化失败", ex);
        }
    }
} 




