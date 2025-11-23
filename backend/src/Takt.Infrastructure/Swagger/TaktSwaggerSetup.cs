//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSwaggerSetup.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-24 14:30
// 版本号 : V0.0.1
// 描述    : Swagger配置类
//===================================================================


using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Takt.Infrastructure.Swagger
{
  /// <summary>
  /// Swagger配置类
  /// </summary>
  public static class TaktSwaggerSetup
  {
    /// <summary>
    /// API模块列表（按字母顺序排列）
    /// </summary>
    private static readonly Dictionary<string, string> ApiModules = new()
        {
            { "accounting", "财务核算" },
            { "generator", "代码生成" },
            { "humanresource", "人力资源" },
            { "identity", "身份认证" },
            { "logging", "审计日志" },
            { "logistics", "后勤管理" },
            { "routine", "日常事务" },
            { "workflow", "工作流管理" }
        };

    /// <summary>
    /// 添加Swagger服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSwagger(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        // 为每个API模块创建一个SwaggerDoc
        foreach (var module in ApiModules)
        {
          c.SwaggerDoc(module.Key, new OpenApiInfo
          {
            Title = $"Takt.Xp {module.Value} API",
            Version = "v1",
            Description = $"极限节拍 {module.Value} API文档\n\n" +
                                "## API 使用说明\n" +
                                "1. 所有请求需要在 Header 中携带 Authorization Token\n" +
                                "2. 返回格式统一为 ApiResult<T>\n" +
                                "3. 支持多语言,请在 Header 中设置 Accept-Language\n" +
                                "4. 分页查询统一使用 PageRequest 对象\n" +
                                "5. 文件上传请使用 multipart/form-data",
            Contact = new OpenApiContact
            {
              Name = "🚀 Takt(Claude AI)",
              Email = "support@takt.cn",
              Url = new Uri("https://www.takt.cn")
            },
            License = new OpenApiLicense
            {
              Name = "📜 MIT License",
              Url = new Uri("https://opensource.org/licenses/MIT")
            }
          });
        }

        // 添加JWT认证
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "JWT Authorization header using the Bearer scheme.\n\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\n\n" +
                              "Example: 'Bearer 12345abcdef'",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
          {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
          });

        // 对所有API进行分组
        c.DocInclusionPredicate((docName, apiDesc) =>
              {
                if (apiDesc.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor actionDescriptor)
                {
                  var moduleAttr = actionDescriptor.ControllerTypeInfo.GetCustomAttribute<ApiModuleAttribute>();
                  return moduleAttr?.Code == docName;
                }
                return false;
              });

        // 添加操作过滤器
        c.OperationFilter<SwaggerOperationFilter>();

        // 自定义架构ID
        c.CustomSchemaIds(type => type.FullName);

        // 添加XML注释
        var xmlFiles = new[]
              {
                    "Takt.Infrastructure.xml",
                    "Takt.WebApi.xml",
                    "Takt.Application.xml",
                    "Takt.Domain.xml"
          };

        foreach (var xmlFile in xmlFiles)
        {
          var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
          if (File.Exists(xmlPath))
          {
            c.IncludeXmlComments(xmlPath, true);
          }
        }

        // 使用完整的类型名称
        c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

        // 配置枚举处理
        c.SchemaFilter<EnumSchemaFilter>();
      });

      return services;
    }

    /// <summary>
    /// 使用Swagger中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseTaktSwagger(this IApplicationBuilder app)
    {
      app.UseSwagger(c =>
      {
        c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
      });

      app.UseSwaggerUI(c =>
      {
        // 为每个API模块添加一个SwaggerEndpoint
        foreach (var module in ApiModules)
        {
          c.SwaggerEndpoint($"/swagger/{module.Key}/swagger.json", $"Takt.Xp {module.Value} API");
        }

        // 自定义样式 - 暂时注释掉，因为文件不存在
        // c.InjectStylesheet("/swagger-ui/custom.css");
        // c.InjectJavascript("/swagger-ui/custom.js");

        // 配置选项
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Takt.Xp API Documentation";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
        c.DefaultModelsExpandDepth(2);
        c.DefaultModelExpandDepth(2);
        c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
        c.EnableValidator();
      });

      return app;
    }
  }
}



