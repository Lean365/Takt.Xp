using System.Collections.Generic;
using System.Threading.Tasks;
using Takt.Domain.Entities.Generator;

namespace Takt.Application.Services.Generator.CodeGenerator;

/// <summary>
/// 代码生成服务接口
/// </summary>
public interface ITaktCodeGeneratorService
{
    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>生成结果</returns>
    Task<bool> GenerateCodeAsync(TaktGenTable table);

    /// <summary>
    /// 预览代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>预览结果</returns>
    Task<Dictionary<string, string>> PreviewCodeAsync(TaktGenTable table);

    /// <summary>
    /// 下载代码
    /// </summary>
    /// <param name="table">表信息</param>
    /// <returns>下载结果</returns>
    Task<byte[]> DownloadCodeAsync(TaktGenTable table);
}

