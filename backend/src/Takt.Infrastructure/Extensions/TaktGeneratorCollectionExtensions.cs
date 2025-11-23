//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneratorCollectionExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-04-25
// 版本号 : V0.0.1
// 描述    : 代码生成服务集合扩展
//===================================================================

using Takt.Application.Services.Generator;
using Takt.Application.Services.Generator.CodeGenerator;
using Takt.Application.Services.Generator.CodeGenerator.Templates;
using Takt.Application.Services.Generator.CodeGenerator.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Takt.Infrastructure.Extensions
{
  /// <summary>
  /// 代码生成服务集合扩展
  /// </summary>
  public static class TaktGeneratorCollectionExtensions
  {
    /// <summary>
    /// 添加代码生成服务
    /// </summary>
    /// <remarks>
    /// 注册代码生成相关的所有服务，包括：
    /// 1. 代码生成表服务 - 管理代码生成表
    /// 2. 代码生成配置服务 - 管理生成配置
    /// 3. 代码生成器服务 - 负责代码生成
    /// 4. 模板引擎服务 - 处理模板渲染
    /// 5. 模板管理服务 - 管理代码生成模板
    /// </remarks>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    /// <param name="webHostEnvironment">Web主机环境</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddGeneratorServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
      // 注册代码生成配置
      var config = new TaktCodeGenerationConfig(webHostEnvironment);
      var templatePathConfig = new TaktTemplatePathConfig(webHostEnvironment);
      configuration.GetSection("CodeGeneration:TemplatePaths").Bind(templatePathConfig);
      config.TemplatePaths = templatePathConfig;

      configuration.GetSection("CodeGeneration").Bind(config, options => options.BindNonPublicProperties = true);
      services.AddSingleton(config);

      // 注册代码生成表服务
      services.AddScoped<ITaktGenTableService, TaktGenTableService>();
      services.AddScoped<ITaktGenConfigService, TaktGenConfigService>();
      services.AddScoped<ITaktCodeGeneratorService, TaktCodeGeneratorService>();
      services.AddScoped<ITaktTemplateEngine, TaktTemplateEngine>();
      services.AddScoped<ITaktGenTemplateService, TaktGenTemplateService>();

      return services;
    }
  }
}



