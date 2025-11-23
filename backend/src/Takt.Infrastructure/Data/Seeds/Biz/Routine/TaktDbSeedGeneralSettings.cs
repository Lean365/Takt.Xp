//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedGeneralSettings.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 常规业务模块通用设置种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.Settings;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 常规业务模块通用设置种子数据提供类
/// </summary>
public class TaktDbSeedGeneralSettings
{
    /// <summary>
    /// 获取默认通用设置数据
    /// </summary>
    /// <returns>通用设置数据列表</returns>
    public List<TaktGeneralSettings> GetDefaultGeneralSettings()
    {
        return new List<TaktGeneralSettings>
        {
            // 缓存配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "缓存提供程序", SettingsKey = "Cache:Provider", SettingsValue = "Memory", SettingsType = 1, IsBuiltin = 1, OrderNum = 50, Status = 0, Remark = "缓存提供程序类型(Memory/Redis)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "默认过期时间(分钟)", SettingsKey = "Cache:DefaultExpirationMinutes", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 51, Status = 0, Remark = "缓存默认过期时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "启用滑动过期", SettingsKey = "Cache:EnableSlidingExpiration", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 52, Status = 0, Remark = "是否启用滑动过期", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "启用多级缓存", SettingsKey = "Cache:EnableMultiLevelCache", SettingsValue = "false", SettingsType = 1, IsBuiltin = 1, OrderNum = 53, Status = 0, Remark = "是否启用多级缓存", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "内存缓存大小限制", SettingsKey = "Cache:Memory:SizeLimit", SettingsValue = "104857600", SettingsType = 1, IsBuiltin = 1, OrderNum = 54, Status = 0, Remark = "内存缓存大小限制(字节)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "内存缓存压缩阈值", SettingsKey = "Cache:Memory:CompactionThreshold", SettingsValue = "1048576", SettingsType = 1, IsBuiltin = 1, OrderNum = 55, Status = 0, Remark = "内存缓存压缩阈值(字节)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "过期扫描频率", SettingsKey = "Cache:Memory:ExpirationScanFrequency", SettingsValue = "60", SettingsType = 1, IsBuiltin = 1, OrderNum = 56, Status = 0, Remark = "过期扫描频率(秒)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "Redis实例名称", SettingsKey = "Cache:Redis:InstanceName", SettingsValue = "Lean.Takt", SettingsType = 1, IsBuiltin = 1, OrderNum = 57, Status = 0, Remark = "Redis实例名称", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "Redis数据库", SettingsKey = "Cache:Redis:DefaultDatabase", SettingsValue = "0", SettingsType = 1, IsBuiltin = 1, OrderNum = 58, Status = 0, Remark = "Redis默认数据库编号", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "Redis启用压缩", SettingsKey = "Cache:Redis:EnableCompression", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 59, Status = 0, Remark = "是否启用Redis数据压缩", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "Redis压缩阈值", SettingsKey = "Cache:Redis:CompressionThreshold", SettingsValue = "1024", SettingsType = 1, IsBuiltin = 1, OrderNum = 60, Status = 0, Remark = "Redis数据压缩阈值(字节)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // OAuth配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "OAuth启用状态", SettingsKey = "Security:Oidentity:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 70, Status = 0, Remark = "是否启用OAuth认证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub客户端ID", SettingsKey = "Security:Oidentity:Providers:GitHub:ClientId", SettingsValue = "", SettingsType = 1, IsBuiltin = 1, OrderNum = 71, Status = 0, Remark = "GitHub OAuth应用的客户端ID", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub客户端密钥", SettingsKey = "Security:Oidentity:Providers:GitHub:ClientSecret", SettingsValue = "", SettingsType = 1, IsBuiltin = 1, OrderNum = 72, Status = 0, Remark = "GitHub OAuth应用的客户端密钥", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub授权端点", SettingsKey = "Security:Oidentity:Providers:GitHub:AuthorizationEndpoint", SettingsValue = "https://github.com/login/oidentity/authorize", SettingsType = 1, IsBuiltin = 1, OrderNum = 73, Status = 0, Remark = "GitHub OAuth授权端点URL", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub令牌端点", SettingsKey = "Security:Oidentity:Providers:GitHub:TokenEndpoint", SettingsValue = "https://github.com/login/oidentity/access_token", SettingsType = 1, IsBuiltin = 1, OrderNum = 74, Status = 0, Remark = "GitHub OAuth令牌端点URL", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub用户信息端点", SettingsKey = "Security:Oidentity:Providers:GitHub:UserInfoEndpoint", SettingsValue = "https://api.github.com/user", SettingsType = 1, IsBuiltin = 1, OrderNum = 75, Status = 0, Remark = "GitHub OAuth用户信息端点URL", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub回调地址", SettingsKey = "Security:Oidentity:Providers:GitHub:RedirectUri", SettingsValue = "https://localhost:5001/oidentity/callback/github", SettingsType = 1, IsBuiltin = 1, OrderNum = 76, Status = 0, Remark = "GitHub OAuth回调地址", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "GitHub权限范围", SettingsKey = "Security:Oidentity:Providers:GitHub:Scope", SettingsValue = "read:user user:email", SettingsType = 1, IsBuiltin = 1, OrderNum = 77, Status = 0, Remark = "GitHub OAuth所需权限范围", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 日志清理配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "日志清理启用状态", SettingsKey = "LogCleanup:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 90, Status = 0, Remark = "是否启用日志自动清理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "日志保留天数", SettingsKey = "LogCleanup:RetentionDays", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 91, Status = 0, Remark = "日志保留天数，超过该天数的日志将被清理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "日志清理执行时间", SettingsKey = "LogCleanup:ExecutionTime", SettingsValue = "02:00:00", SettingsType = 1, IsBuiltin = 1, OrderNum = 92, Status = 0, Remark = "日志清理的执行时间（24小时制）", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "批次清理数量", SettingsKey = "LogCleanup:BatchSize", SettingsValue = "1000", SettingsType = 1, IsBuiltin = 1, OrderNum = 93, Status = 0, Remark = "每次清理的日志数量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "日志类型", SettingsKey = "LogCleanup:LogTypes", SettingsValue = "Info,Debug,Warning", SettingsType = 1, IsBuiltin = 1, OrderNum = 94, Status = 0, Remark = "需要清理的日志类型，多个类型用逗号分隔", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 日志归档配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "日志归档启用状态", SettingsKey = "LogArchive:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 100, Status = 0, Remark = "是否启用日志自动归档", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档触发天数", SettingsKey = "LogArchive:TriggerDays", SettingsValue = "90", SettingsType = 1, IsBuiltin = 1, OrderNum = 101, Status = 0, Remark = "超过多少天的日志将被归档", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档执行时间", SettingsKey = "LogArchive:ExecutionTime", SettingsValue = "03:00:00", SettingsType = 1, IsBuiltin = 1, OrderNum = 102, Status = 0, Remark = "日志归档的执行时间（24小时制）", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档批次大小", SettingsKey = "LogArchive:BatchSize", SettingsValue = "1000", SettingsType = 1, IsBuiltin = 1, OrderNum = 103, Status = 0, Remark = "每次归档的日志数量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档存储路径", SettingsKey = "LogArchive:StoragePath", SettingsValue = "Archive/Logs", SettingsType = 1, IsBuiltin = 1, OrderNum = 104, Status = 0, Remark = "日志归档文件的存储路径", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档文件格式", SettingsKey = "LogArchive:FileFormat", SettingsValue = "json", SettingsType = 1, IsBuiltin = 1, OrderNum = 105, Status = 0, Remark = "归档文件的格式(json/csv)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档压缩启用", SettingsKey = "LogArchive:Compression:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 106, Status = 0, Remark = "是否启用归档文件压缩", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "归档压缩格式", SettingsKey = "LogArchive:Compression:Format", SettingsValue = "gzip", SettingsType = 1, IsBuiltin = 1, OrderNum = 107, Status = 0, Remark = "归档文件的压缩格式(gzip/zip)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 安全配置 - 密码策略 - 横排格式
            new TaktGeneralSettings { SettingsName = "密码最小长度", SettingsKey = "Security:Password:MinLength", SettingsValue = "8", SettingsType = 1, IsBuiltin = 1, OrderNum = 110, Status = 0, Remark = "密码最小长度要求", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "密码复杂度要求", SettingsKey = "Security:Password:RequireComplexity", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 111, Status = 0, Remark = "是否要求密码包含大小写字母、数字和特殊字符", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "密码过期天数", SettingsKey = "Security:Password:ExpirationDays", SettingsValue = "90", SettingsType = 1, IsBuiltin = 1, OrderNum = 112, Status = 0, Remark = "密码过期天数，0表示永不过期", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "密码历史记录数", SettingsKey = "Security:Password:HistoryCount", SettingsValue = "3", SettingsType = 1, IsBuiltin = 1, OrderNum = 113, Status = 0, Remark = "记住多少个历史密码，防止重复使用", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 安全配置 - 登录锁定 - 横排格式
            new TaktGeneralSettings { SettingsName = "登录失败锁定次数", SettingsKey = "Security:Lockout:MaxFailedAttempts", SettingsValue = "5", SettingsType = 1, IsBuiltin = 1, OrderNum = 114, Status = 0, Remark = "允许的最大登录失败次数", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "锁定时间(分钟)", SettingsKey = "Security:Lockout:DurationMinutes", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 115, Status = 0, Remark = "账户锁定持续时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 安全配置 - 会话管理 - 横排格式
            new TaktGeneralSettings { SettingsName = "会话超时时间(分钟)", SettingsKey = "Security:Session:TimeoutMinutes", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 116, Status = 0, Remark = "用户会话超时时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "允许多端登录", SettingsKey = "Security:Session:AllowMultipleLogin", SettingsValue = "false", SettingsType = 1, IsBuiltin = 1, OrderNum = 117, Status = 0, Remark = "是否允许同一账户多个终端同时登录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 安全配置 - JWT - 横排格式
            new TaktGeneralSettings { SettingsName = "JWT密钥", SettingsKey = "Security:Jwt:SecretKey", SettingsValue = "your-secret-key-here", SettingsType = 1, IsBuiltin = 1, OrderNum = 118, Status = 0, Remark = "JWT令牌加密密钥", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "JWT过期时间(分钟)", SettingsKey = "Security:Jwt:ExpirationMinutes", SettingsValue = "120", SettingsType = 1, IsBuiltin = 1, OrderNum = 119, Status = 0, Remark = "JWT令牌过期时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 安全配置 - CORS - 横排格式
            new TaktGeneralSettings { SettingsName = "启用CORS", SettingsKey = "Security:Cors:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 120, Status = 0, Remark = "是否启用跨域资源共享", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "允许的来源", SettingsKey = "Security:Cors:AllowedOrigins", SettingsValue = "*", SettingsType = 1, IsBuiltin = 1, OrderNum = 121, Status = 0, Remark = "允许的跨域来源，多个用逗号分隔，*表示允许所有", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 数据库配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "数据库类型", SettingsKey = "Database:Type", SettingsValue = "SqlServer", SettingsType = 1, IsBuiltin = 1, OrderNum = 130, Status = 0, Remark = "数据库类型(SqlServer/MySql/PostgreSQL/Oracle/Sqlite)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "数据库连接字符串", SettingsKey = "Database:ConnectionString", SettingsValue = "JA0UGkq0SN+3ZIyvN+W/NKcRz5hM9QIOEOia3zqQinTBjFEx8hSdAFTTV5Xapr2GZR0UXqlVmoKRfmL/3qgXtEmUCNBShOL/CGJcbZyZ8nPQJVvT7akNJd+J2/D2yY59", SettingsType = 1, IsBuiltin = 1, OrderNum = 131, Status = 0, Remark = "数据库连接字符串(已加密)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "最大连接池大小", SettingsKey = "Database:MaxPoolSize", SettingsValue = "100", SettingsType = 1, IsBuiltin = 1, OrderNum = 132, Status = 0, Remark = "数据库最大连接池大小", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "最小连接池大小", SettingsKey = "Database:MinPoolSize", SettingsValue = "5", SettingsType = 1, IsBuiltin = 1, OrderNum = 133, Status = 0, Remark = "数据库最小连接池大小", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "连接超时时间", SettingsKey = "Database:ConnectionTimeout", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 134, Status = 0, Remark = "数据库连接超时时间(秒)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "命令超时时间", SettingsKey = "Database:CommandTimeout", SettingsValue = "30", SettingsType = 1, IsBuiltin = 1, OrderNum = 135, Status = 0, Remark = "数据库命令执行超时时间(秒)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "启用读写分离", SettingsKey = "Database:EnableReadWriteSeparation", SettingsValue = "false", SettingsType = 1, IsBuiltin = 1, OrderNum = 136, Status = 0, Remark = "是否启用读写分离", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "只读连接字符串", SettingsKey = "Database:ReadOnlyConnectionString", SettingsValue = "", SettingsType = 1, IsBuiltin = 1, OrderNum = 137, Status = 0, Remark = "只读数据库连接字符串(读写分离时使用，需加密)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 验证码配置 - 横排格式
            new TaktGeneralSettings { SettingsName = "验证码启用状态", SettingsKey = "Captcha:Enabled", SettingsValue = "true", SettingsType = 1, IsBuiltin = 1, OrderNum = 139, Status = 0, Remark = "是否启用验证码功能", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "验证码类型", SettingsKey = "Captcha:Type", SettingsValue = "Behavior", SettingsType = 1, IsBuiltin = 1, OrderNum = 140, Status = 0, Remark = "验证码类型(Slider/Behavior)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "滑块验证码宽度", SettingsKey = "Captcha:Slider:Width", SettingsValue = "300", SettingsType = 0, IsBuiltin = 2, OrderNum = 141, Status = 0, Remark = "滑块验证码背景图片宽度(像素)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "滑块验证码高度", SettingsKey = "Captcha:Slider:Height", SettingsValue = "150", SettingsType = 0, IsBuiltin = 2, OrderNum = 142, Status = 0, Remark = "滑块验证码背景图片高度(像素)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "滑块宽度", SettingsKey = "Captcha:Slider:SliderWidth", SettingsValue = "50", SettingsType = 0, IsBuiltin = 2, OrderNum = 143, Status = 0, Remark = "滑块宽度(像素)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "滑块验证容差", SettingsKey = "Captcha:Slider:Tolerance", SettingsValue = "5", SettingsType = 0, IsBuiltin = 2, OrderNum = 144, Status = 0, Remark = "滑块验证允许的误差像素", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "滑块验证过期时间", SettingsKey = "Captcha:Slider:ExpirationMinutes", SettingsValue = "5", SettingsType = 0, IsBuiltin = 2, OrderNum = 145, Status = 0, Remark = "滑块验证码的过期时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "行为验证分数阈值", SettingsKey = "Captcha:Behavior:ScoreThreshold", SettingsValue = "0.8", SettingsType = 0, IsBuiltin = 2, OrderNum = 146, Status = 0, Remark = "行为验证通过的最低分数", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "行为数据过期时间", SettingsKey = "Captcha:Behavior:DataExpirationMinutes", SettingsValue = "30", SettingsType = 0, IsBuiltin = 2, OrderNum = 147, Status = 0, Remark = "行为数据的缓存时间(分钟)", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktGeneralSettings { SettingsName = "启用机器学习", SettingsKey = "Captcha:Behavior:EnableMachineLearning", SettingsValue = "false", SettingsType = 1, IsBuiltin = 3, OrderNum = 148, Status = 0, Remark = "是否启用机器学习模型进行行为分析", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

