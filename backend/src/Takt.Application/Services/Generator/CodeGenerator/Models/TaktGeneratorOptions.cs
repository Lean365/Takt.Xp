/// <summary>
/// Takt生成器选项
/// </summary>
public class TaktGeneratorOptions
{
    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string? TransType { get; set; }

    /// <summary>
    /// 包名
    /// </summary>
    public string? PackageName { get; set; }

    /// <summary>
    /// 基础命名空间
    /// </summary>
    public string? BaseNamespace { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string? GenPath { get; set; }

    /// <summary>
    /// 是否从数据库同步
    /// </summary>
    public bool IsFromDb { get; set; }
}
