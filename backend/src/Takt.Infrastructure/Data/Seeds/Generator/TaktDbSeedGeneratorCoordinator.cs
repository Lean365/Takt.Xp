//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedGeneratorCoordinator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 代码生成种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Generator;

namespace Takt.Infrastructure.Data.Seeds.Generator;

/// <summary>
/// 代码生成种子数据协调器
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 统一管理代码生成相关的种子数据初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktDbSeedGeneratorCoordinator
{
  /// <summary>
  /// 仓储工厂
  /// </summary>
  protected readonly ITaktRepositoryFactory _repositoryFactory;
  private readonly ITaktLogger _logger;

  private ITaktRepository<TaktGenConfig> GenConfigRepository => _repositoryFactory.GetGeneratorRepository<TaktGenConfig>();
  private ITaktRepository<TaktGenTemplate> GenTemplateRepository => _repositoryFactory.GetGeneratorRepository<TaktGenTemplate>();

  /// <summary>
  /// 构造函数
  /// </summary>
  /// <param name="repositoryFactory">仓储工厂</param>
  /// <param name="logger">日志服务</param>
  public TaktDbSeedGeneratorCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
  {
    _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  /// <summary>
  /// 初始化所有代码生成种子数据
  /// </summary>
  /// <returns>初始化结果</returns>
  public async Task<GeneratorSeedResult> InitializeAllGeneratorDataAsync()
  {
    try
    {
      _logger.Info("开始初始化所有代码生成种子数据...");

      var result = new GeneratorSeedResult();

      // 1. 初始化代码生成配置
      var genConfigResult = await InitializeGenConfigAsync();
      result.GenConfigResult = genConfigResult;

      // 2. 初始化代码生成模板
      var genTemplateResult = await InitializeGenTemplateAsync();
      result.GenTemplateResult = genTemplateResult;



      _logger.Info($"代码生成种子数据初始化完成！配置: {genConfigResult.insertCount + genConfigResult.updateCount} 条, 模板: {genTemplateResult.insertCount + genTemplateResult.updateCount} 条");
      return result;
    }
    catch (Exception ex)
    {
      _logger.Error($"初始化代码生成种子数据失败: {ex.Message}", ex);
      throw;
    }
  }

  /// <summary>
  /// 初始化代码生成配置
  /// </summary>
  /// <returns>初始化结果</returns>
  public async Task<(int insertCount, int updateCount)> InitializeGenConfigAsync()
  {
    try
    {
      _logger.Info("开始初始化代码生成配置...");

      var genConfigSeed = new TaktDbSeedGenConfig();
      var genConfigs = genConfigSeed.GetDefaultGenConfigs();

      var insertCount = 0;
      var updateCount = 0;

      foreach (var config in genConfigs)
      {
        var existingConfig = await GenConfigRepository.GetFirstAsync(x => x.GenConfigName == config.GenConfigName);
        if (existingConfig == null)
        {
          await GenConfigRepository.CreateAsync(config);
          insertCount++;
          _logger.Debug($"新增代码生成配置: {config.GenConfigName}");
        }
        else
        {
          // 更新现有配置
          existingConfig.Author = config.Author;
          existingConfig.ProjectName = config.ProjectName;
          existingConfig.ModuleName = config.ModuleName;
          existingConfig.BusinessName = config.BusinessName;
          existingConfig.FunctionName = config.FunctionName;
          existingConfig.GenMethod = config.GenMethod;
          existingConfig.GenTplType = config.GenTplType;
          existingConfig.GenPath = config.GenPath;
          existingConfig.Options = config.Options;
          existingConfig.Status = config.Status;

          await GenConfigRepository.UpdateAsync(existingConfig);
          updateCount++;
          _logger.Debug($"更新代码生成配置: {config.GenConfigName}");
        }
      }

      _logger.Info($"代码生成配置初始化完成: 新增 {insertCount} 条, 更新 {updateCount} 条");
      return (insertCount, updateCount);
    }
    catch (Exception ex)
    {
      _logger.Error($"初始化代码生成配置失败: {ex.Message}", ex);
      throw;
    }
  }

  /// <summary>
  /// 初始化代码生成模板
  /// </summary>
  /// <returns>初始化结果</returns>
  public async Task<(int insertCount, int updateCount)> InitializeGenTemplateAsync()
  {
    try
    {
      _logger.Info("开始初始化代码生成模板...");

      var genTemplateSeed = new TaktDbSeedGenTemplate();
      var genTemplates = genTemplateSeed.GetDefaultTemplate();

      var insertCount = 0;
      var updateCount = 0;

      foreach (var template in genTemplates)
      {
        var existingTemplate = await GenTemplateRepository.GetFirstAsync(x =>
            x.TemplateName == template.TemplateName &&
            x.TemplateCodeType == template.TemplateCodeType &&
            x.TemplateCategory == template.TemplateCategory);

        if (existingTemplate == null)
        {
          await GenTemplateRepository.CreateAsync(template);
          insertCount++;
          _logger.Debug($"新增代码生成模板: {template.TemplateName}");
        }
        else
        {
          // 更新现有模板
          existingTemplate.TemplateLanguage = template.TemplateLanguage;
          existingTemplate.TemplateOrmType = template.TemplateOrmType;
          existingTemplate.TemplateVersion = template.TemplateVersion;
          existingTemplate.TemplateContent = template.TemplateContent;
          existingTemplate.FileName = template.FileName;
          existingTemplate.Status = template.Status;

          await GenTemplateRepository.UpdateAsync(existingTemplate);
          updateCount++;
          _logger.Debug($"更新代码生成模板: {template.TemplateName}");
        }
      }

      _logger.Info($"代码生成模板初始化完成: 新增 {insertCount} 条, 更新 {updateCount} 条");
      return (insertCount, updateCount);
    }
    catch (Exception ex)
    {
      _logger.Error($"初始化代码生成模板失败: {ex.Message}", ex);
      throw;
    }
  }



  /// <summary>
  /// 清理代码生成种子数据
  /// </summary>
  /// <returns>清理结果</returns>
  public async Task<(int configCount, int templateCount)> CleanupGeneratorDataAsync()
  {
    try
    {
      _logger.Info("开始清理代码生成种子数据...");

      var configCount = await GenConfigRepository.DeleteAsync(x => x.Status == 0);
      var templateCount = await GenTemplateRepository.DeleteAsync(x => x.Status == 0);

      _logger.Info($"代码生成种子数据清理完成: 配置 {configCount} 条, 模板 {templateCount} 条");
      return (configCount, templateCount);
    }
    catch (Exception ex)
    {
      _logger.Error($"清理代码生成种子数据失败: {ex.Message}", ex);
      throw;
    }
  }
}

/// <summary>
/// 代码生成种子数据初始化结果
/// </summary>
public class GeneratorSeedResult
{
  /// <summary>
  /// 代码生成配置初始化结果
  /// </summary>
  public (int insertCount, int updateCount) GenConfigResult { get; set; }

  /// <summary>
  /// 代码生成模板初始化结果
  /// </summary>
  public (int insertCount, int updateCount) GenTemplateResult { get; set; }

  /// <summary>
  /// 获取总插入数量
  /// </summary>
  /// <returns>总插入数量</returns>
  public int GetTotalInsertCount()
  {
    return GenConfigResult.insertCount + GenTemplateResult.insertCount;
  }

  /// <summary>
  /// 获取总更新数量
  /// </summary>
  /// <returns>总更新数量</returns>
  public int GetTotalUpdateCount()
  {
    return GenConfigResult.updateCount + GenTemplateResult.updateCount;
  }

  /// <summary>
  /// 获取总处理数量
  /// </summary>
  /// <returns>总处理数量</returns>
  public int GetTotalCount()
  {
    return GetTotalInsertCount() + GetTotalUpdateCount();
  }
}




