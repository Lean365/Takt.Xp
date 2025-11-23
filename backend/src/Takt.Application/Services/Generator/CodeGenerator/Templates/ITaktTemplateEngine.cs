using Takt.Application.Services.Generator.CodeGenerator.Models;

namespace Takt.Application.Services.Generator.CodeGenerator.Templates;

/// <summary>
/// 模板引擎接口
/// </summary>
public interface ITaktTemplateEngine
{
    /// <summary>
    /// 渲染模板
    /// </summary>
    Task<string> RenderAsync(string template, TaktGeneratorModel model);

    /// <summary>
    /// 渲染多个模板
    /// </summary>
    Task<Dictionary<string, string>> RenderManyAsync(Dictionary<string, string> templates, TaktGeneratorModel model);
} 
