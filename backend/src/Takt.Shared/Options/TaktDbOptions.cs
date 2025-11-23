//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:40
// 版本号 : V0.0.1
// 描述   : 数据库配置选项
//===================================================================

using SqlSugar;

namespace Takt.Shared.Options;

/// <summary>
/// 数据库配置选项
/// </summary>
public class TaktDbOptions
{


    /// <summary>
    /// 数据库类型
    /// </summary>
    public DbType DbType { get; set; }

    /// <summary>
    /// 是否自动关闭连接
    /// </summary>
    public bool IsAutoCloseConnection { get; set; } = true;

    /// <summary>
    /// 是否启用多数据库模式
    /// </summary>
    public bool Multi { get; set; }

    /// <summary>
    /// 是否启用差异日志
    /// </summary>
    public bool SqlDiffLog { get; set; }

    /// <summary>
    /// 最大连接池大小
    /// </summary>
    public int MaxPoolSize { get; set; }

    /// <summary>
    /// 最小连接池大小
    /// </summary>
    public int MinPoolSize { get; set; }

    /// <summary>
    /// 连接超时时间
    /// </summary>
    public int ConnectionTimeout { get; set; }

    /// <summary>
    /// 命令超时时间
    /// </summary>
    public int CommandTimeout { get; set; }

    /// <summary>
    /// 初始化选项
    /// </summary>
    public TaktDbInitOptions Init { get; set; } = new();

    /// <summary>
    /// 连接字符串
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// 认证数据库连接字符串
    /// </summary>
    public string? AuthConnectionString { get; set; }

    /// <summary>
    /// 业务数据库连接字符串
    /// </summary>
    public string? BusinessConnectionString { get; set; }

    /// <summary>
    /// 工作流数据库连接字符串
    /// </summary>
    public string? WorkflowConnectionString { get; set; }

    /// <summary>
    /// 代码生成数据库连接字符串
    /// </summary>
    public string? GeneratorConnectionString { get; set; }


    /// <summary>
    /// 雪花ID配置选项
    /// </summary>
    public TaktSnowflakeIdOptions SnowflakeId { get; set; } = new();
}

/// <summary>
/// 数据库初始化选项
/// </summary>
public class TaktDbInitOptions
{
    /// <summary>
    /// 是否初始化数据库
    /// </summary>
    public bool InitDatabase { get; set; }

    /// <summary>
    /// 是否初始化种子数据
    /// </summary>
    public bool InitSeedData { get; set; }
}


/// <summary>
/// 多数据库配置选项
/// </summary>
public class TaktMultiDatabaseOptions
{
    /// <summary>
    /// 是否启用多数据库
    /// </summary>
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// 认证数据库连接字符串
    /// </summary>
    public string? AuthConnectionString { get; set; }

    /// <summary>
    /// 业务数据库连接字符串
    /// </summary>
    public string? BusinessConnectionString { get; set; }

    /// <summary>
    /// 工作流数据库连接字符串
    /// </summary>
    public string? WorkflowConnectionString { get; set; }

    /// <summary>
    /// 代码生成数据库连接字符串
    /// </summary>
    public string? GeneratorConnectionString { get; set; }
}

/// <summary>
/// 雪花ID配置选项
/// </summary>
public class TaktSnowflakeIdOptions
{
    /// <summary>
    /// 是否启用雪花ID
    /// </summary>
    public bool Enable { get; set; } = false;

    /// <summary>
    /// 工作机器ID
    /// </summary>
    public int WorkId { get; set; } = 1;
}




