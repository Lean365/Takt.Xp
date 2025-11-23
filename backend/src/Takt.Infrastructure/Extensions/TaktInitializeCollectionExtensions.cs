//===================================================================
// 项目名 : Hbt.Cur
// 文件名 : HbtInitializeCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 数据库和种子数据初始化扩展类
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.IServices;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlSugar;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 数据库和种子数据初始化扩展类
/// </summary>
public static class TaktInitializeCollectionExtensions
{
  /// <summary>
  /// 初始化数据库结构
  /// </summary>
  /// <param name="app">应用程序构建器</param>
  /// <param name="dbConfig">数据库配置</param>
  /// <param name="configuration">配置</param>
  /// <param name="logger">日志记录器</param>
  public static async Task InitializeDatabaseAsync(this IApplicationBuilder app, TaktDbOptions dbConfig, IConfiguration configuration, ITaktLogger logger)
  {
    if (!dbConfig.Init.InitDatabase)
    {
      logger.Info("数据库结构初始化配置未启用，跳过数据库初始化");
      return;
    }

    logger.Info("开始初始化多数据库结构...");

    using var scope = app.ApplicationServices.CreateScope();

    // 按照指定顺序初始化多数据库
    // 1. 初始化认证数据库
    try
    {
      var IdentityDBContext = scope.ServiceProvider.GetService<TaktIdentityDBContext>();
      if (IdentityDBContext != null)
      {
        logger.Info("开始初始化认证数据库...");
        await IdentityDBContext.InitializeAsync();
        logger.Info("认证数据库初始化完成");
      }
      else
      {
        logger.Warn("认证数据库上下文未注册，跳过认证数据库初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化认证数据库失败: {Error}", ex.Message);
      throw;
    }

    // 2. 初始化业务数据库
    try
    {
      var businessDbContext = scope.ServiceProvider.GetService<TaktBusinessDbContext>();
      if (businessDbContext != null)
      {
        logger.Info("开始初始化业务数据库...");
        await businessDbContext.InitializeAsync();
        logger.Info("业务数据库初始化完成");
      }
      else
      {
        logger.Warn("业务数据库上下文未注册，跳过业务数据库初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化业务数据库失败: {Error}", ex.Message);
      throw;
    }

    // 3. 初始化代码生成数据库
    try
    {
      var generatorDbContext = scope.ServiceProvider.GetService<TaktGeneratorDbContext>();
      if (generatorDbContext != null)
      {
        logger.Info("开始初始化代码生成数据库...");
        await generatorDbContext.InitializeAsync();
        logger.Info("代码生成数据库初始化完成");
      }
      else
      {
        logger.Warn("代码生成数据库上下文未注册，跳过代码生成数据库初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化代码生成数据库失败: {Error}", ex.Message);
      throw;
    }

    // 4. 初始化工作流数据库
    try
    {
      var workflowDbContext = scope.ServiceProvider.GetService<TaktWorkflowDbContext>();
      if (workflowDbContext != null)
      {
        logger.Info("开始初始化工作流数据库...");
        await workflowDbContext.InitializeAsync();
        logger.Info("工作流数据库初始化完成");
      }
      else
      {
        logger.Warn("工作流数据库上下文未注册，跳过工作流数据库初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化工作流数据库失败: {Error}", ex.Message);
      throw;
    }

    logger.Info("多数据库结构初始化完成");
  }

  /// <summary>
  /// 初始化种子数据
  /// </summary>
  /// <param name="app">应用程序构建器</param>
  /// <param name="dbConfig">数据库配置</param>
  /// <param name="configuration">配置</param>
  /// <param name="logger">日志记录器</param>
  public static async Task InitializeSeedDataAsync(this IApplicationBuilder app, TaktDbOptions dbConfig, IConfiguration configuration, ITaktLogger logger)
  {
    if (!dbConfig.Init.InitSeedData)
    {
      logger.Info("种子数据初始化配置未启用，跳过种子数据初始化");
      return;
    }

    logger.Info("开始初始化多数据库种子数据...");

    using var scope = app.ApplicationServices.CreateScope();

    // 按照指定顺序初始化多数据库种子数据
    // 1. 初始化认证数据库种子数据
    try
    {
      var IdentityDBSeed = scope.ServiceProvider.GetService<TaktIdentityDBSeed>();
      if (IdentityDBSeed != null)
      {
        logger.Info("开始初始化认证数据库种子数据...");
        await IdentityDBSeed.InitializeAsync();
        logger.Info("认证数据库种子数据初始化完成");
      }
      else
      {
        logger.Warn("认证数据库种子数据服务未注册，跳过认证数据库种子数据初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化认证数据库种子数据失败: {Error}", ex.Message);
      throw;
    }

    // 2. 初始化业务数据库种子数据
    try
    {
      var businessDbSeed = scope.ServiceProvider.GetService<TaktBusinessDbSeed>();
      if (businessDbSeed != null)
      {
        logger.Info("开始初始化业务数据库种子数据...");
        await businessDbSeed.InitializeAsync();
        logger.Info("业务数据库种子数据初始化完成");
      }
      else
      {
        logger.Warn("业务数据库种子数据服务未注册，跳过业务数据库种子数据初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化业务数据库种子数据失败: {Error}", ex.Message);
      throw;
    }

    // 3. 初始化代码生成数据库种子数据
    try
    {
      var generatorDbSeed = scope.ServiceProvider.GetService<TaktGeneratorDbSeed>();
      if (generatorDbSeed != null)
      {
        logger.Info("开始初始化代码生成数据库种子数据...");
        await generatorDbSeed.InitializeAsync();
        logger.Info("代码生成数据库种子数据初始化完成");
      }
      else
      {
        logger.Warn("代码生成数据库种子数据服务未注册，跳过代码生成数据库种子数据初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化代码生成数据库种子数据失败: {Error}", ex.Message);
      throw;
    }

    // 4. 初始化工作流数据库种子数据
    try
    {
      var workflowDbSeed = scope.ServiceProvider.GetService<TaktWorkflowDbSeed>();
      if (workflowDbSeed != null)
      {
        logger.Info("开始初始化工作流数据库种子数据...");
        await workflowDbSeed.InitializeAsync();
        logger.Info("工作流数据库种子数据初始化完成");
      }
      else
      {
        logger.Warn("工作流数据库种子数据服务未注册，跳过工作流数据库种子数据初始化");
      }
    }
    catch (Exception ex)
    {
      logger.Error("初始化工作流数据库种子数据失败: {Error}", ex.Message);
      throw;
    }

    logger.Info("多数据库种子数据初始化完成");
  }

  /// <summary>
  /// 执行系统重启清理
  /// </summary>
  /// <param name="app">应用程序构建器</param>
  /// <param name="logger">日志记录器</param>
  public static async Task ExecuteSystemRestartCleanupAsync(this IApplicationBuilder app, ITaktLogger logger)
  {
    logger.Info("开始执行系统重启清理...");

    using var scope = app.ApplicationServices.CreateScope();
    var systemRestartService = scope.ServiceProvider.GetRequiredService<ITaktRestartService>();
    var result = await systemRestartService.ExecuteRestartCleanupAsync();

    if (result)
    {
      logger.Info("系统重启清理完成");
    }
    else
    {
      logger.Warn("系统重启清理失败");
    }
  }

  /// <summary>
  /// 初始化所有系统组件（数据库结构、种子数据、系统清理）
  /// </summary>
  /// <param name="app">应用程序构建器</param>
  /// <param name="dbConfig">数据库配置</param>
  /// <param name="configuration">配置</param>
  /// <param name="logger">日志记录器</param>
  public static async Task InitializeAllAsync(this IApplicationBuilder app, TaktDbOptions dbConfig, IConfiguration configuration, ITaktLogger logger)
  {
    logger.Info("开始初始化系统组件...");

    // 0. 首先验证配置组合的有效性（只验证一次）

    // 1. 初始化数据库结构（创建数据库和表）
    await app.InitializeDatabaseAsync(dbConfig, configuration, logger);

    // 2. 初始化种子数据（插入初始数据）
    await app.InitializeSeedDataAsync(dbConfig, configuration, logger);

    // 3. 执行系统重启清理
    await app.ExecuteSystemRestartCleanupAsync(logger);

    logger.Info("系统组件初始化完成");
  }
}