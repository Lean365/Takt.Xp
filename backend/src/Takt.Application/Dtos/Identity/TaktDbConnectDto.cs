//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbConnectionInfo.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20
// 版本号 : V0.0.1
// 描述   : 数据库连接信息
//===================================================================

namespace Takt.Application.Dtos.Identity;

/// <summary>
/// 数据库连接信息
/// </summary>
public class TaktDbConnectDto
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public string DbType { get; set; } = string.Empty;

    /// <summary>
    /// 主机地址
    /// </summary>
    public string Host { get; set; } = string.Empty;

    /// <summary>
    /// 端口号
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    public string Database { get; set; } = string.Empty;

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 连接选项
    /// </summary>
    public string? Options { get; set; }
} 



