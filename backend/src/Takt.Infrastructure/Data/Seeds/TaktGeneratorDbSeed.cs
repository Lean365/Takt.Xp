//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneratorDbSeed.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 代码生成数据库种子数据初始化类
//===================================================================

using Takt.Shared.Exceptions;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds.Biz.Dict;
using Takt.Infrastructure.Data.Seeds.Biz.Translation;
using Takt.Infrastructure.Data.Seeds.Generator;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 代码生成数据库种子数据初始化类
/// </summary>
public class TaktGeneratorDbSeed
{
    private readonly TaktGeneratorDbContext _context;
    private readonly ITaktLogger _logger;
    private readonly TaktDbSeedGeneratorCoordinator _generatorCoordinator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGeneratorDbSeed(
        TaktGeneratorDbContext context,
        ITaktLogger logger,
        TaktDbSeedGeneratorCoordinator generatorCoordinator)
    {
        _context = context;
        _logger = logger;
        _generatorCoordinator = generatorCoordinator;
    }

    /// <summary>
    /// 初始化代码生成数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化代码生成数据库种子数据...");


            // 使用协调器初始化所有代码生成种子数据
            var result = await _generatorCoordinator.InitializeAllGeneratorDataAsync();          

            _logger.Info("代码生成数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"代码生成数据库种子数据初始化失败: {ex.Message}", ex);
            throw new TaktException("代码生成数据库种子数据初始化失败", ex);
        }
    }
} 




