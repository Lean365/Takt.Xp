using System.Text.RegularExpressions;

namespace Takt.Shared.Utils
{
    /// <summary>
    /// 数据脱敏工具类
    /// </summary>
    public static class TaktMaskUtils
    {
        #region 基础脱敏方法

        /// <summary>
        /// 手机号码脱敏
        /// </summary>
        public static string MaskPhoneNumber(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return string.Empty;
            return Regex.Replace(phoneNumber, @"(\d{3})\d{4}(\d{4})", "$1****$2");
        }

        /// <summary>
        /// 邮箱地址脱敏
        /// </summary>
        public static string MaskEmail(string? email)
        {
            if (string.IsNullOrEmpty(email)) return string.Empty;
            return Regex.Replace(email, @"(.{2}).*(@.*)", "$1***$2");
        }

        /// <summary>
        /// 身份证号脱敏
        /// </summary>
        public static string MaskIdCard(string? idCard)
        {
            if (string.IsNullOrEmpty(idCard)) return string.Empty;
            return Regex.Replace(idCard, @"(\d{4}).*(\d{4})", "$1********$2");
        }

        /// <summary>
        /// 银行卡号脱敏
        /// </summary>
        public static string MaskBankCard(string? bankCard)
        {
            if (string.IsNullOrEmpty(bankCard)) return string.Empty;
            return Regex.Replace(bankCard, @"(\d{4}).*(\d{4})", "$1********$2");
        }

        /// <summary>
        /// 密码脱敏
        /// </summary>
        public static string MaskPassword(string? password)
        {
            if (string.IsNullOrEmpty(password)) return string.Empty;
            return "********";
        }

        /// <summary>
        /// 地址脱敏
        /// </summary>
        public static string MaskAddress(string? address)
        {
            if (string.IsNullOrEmpty(address)) return string.Empty;
            if (address.Length <= 6) return "***";
            return address.Substring(0, 6) + "****";
        }

        /// <summary>
        /// 数据库连接字符串脱敏
        /// </summary>
        public static string MaskConnectionString(string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return string.Empty;

            var maskedConnectionString = connectionString;

            // 遮罩用户名
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"(User Id|uid|userid|user)=[^;]*",
                "$1=****",
                RegexOptions.IgnoreCase
            );

            // 遮罩密码
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"(Password|pwd)=[^;]*",
                "$1=********",
                RegexOptions.IgnoreCase
            );

            // 遮罩服务器地址
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"(Server|server|Host|host|Data Source)=[^;]*",
                "$1=***",
                RegexOptions.IgnoreCase
            );

            // 遮罩数据库名
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"(Database|database)=[^;]*",
                "$1=***",
                RegexOptions.IgnoreCase
            );

            // 遮罩端口号
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"(Port|port)=[^;]*",
                "$1=***",
                RegexOptions.IgnoreCase
            );

            // 遮罩所有带 =true 或 =false 的连接参数
            maskedConnectionString = Regex.Replace(
                maskedConnectionString,
                @"([^=;]+)=(true|false)",
                "$1=***",
                RegexOptions.IgnoreCase
            );

            return maskedConnectionString;
        }

        /// <summary>
        /// 文件系统路径脱敏
        /// </summary>
        /// <param name="path">原始路径</param>
        /// <returns>脱敏后的路径</returns>
        public static string MaskPath(string? path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;

            try
            {
                // 特殊处理ASP.NET Core数据保护服务路径
                if (path.Contains("DataProtection-Keys") || path.Contains("AppData\\Local\\ASP.NET"))
                {
                    // 脱敏用户目录和完整路径
                    var parts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    if (parts.Length > 2)
                    {
                        // 脱敏驱动器号
                        if (parts[0].EndsWith(":"))
                        {
                            parts[0] = "***:";
                        }

                        // 脱敏用户目录
                        if (parts.Length > 2)
                        {
                            parts[2] = "***";
                        }

                        // 脱敏AppData目录
                        if (parts.Length > 3)
                        {
                            parts[3] = "***";
                        }

                        // 脱敏Local目录
                        if (parts.Length > 4)
                        {
                            parts[4] = "***";
                        }

                        // 脱敏ASP.NET目录
                        if (parts.Length > 5)
                        {
                            parts[5] = "***";
                        }

                        // 脱敏DataProtection-Keys目录
                        if (parts.Length > 6)
                        {
                            parts[6] = "***";
                        }
                    }

                    return string.Join(Path.DirectorySeparatorChar.ToString(), parts);
                }

                // 如果是绝对路径，脱敏驱动器号和用户目录部分
                if (Path.IsPathRooted(path))
                {
                    var pathParts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                    if (pathParts.Length > 0)
                    {
                        // 脱敏驱动器号（如 C:）
                        if (pathParts[0].EndsWith(":"))
                        {
                            pathParts[0] = "***:";
                        }

                        // 脱敏用户目录部分（通常是第二个部分）
                        if (pathParts.Length > 2)
                        {
                            pathParts[2] = "***";
                        }

                        // 脱敏项目根目录部分（通常是第三个部分）
                        if (pathParts.Length > 3)
                        {
                            pathParts[3] = "***";
                        }
                    }

                    return string.Join(Path.DirectorySeparatorChar.ToString(), pathParts);
                }

                // 如果是相对路径，只脱敏项目相关部分
                var relativePathParts = path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (relativePathParts.Length > 1)
                {
                    // 脱敏第一个目录（通常是项目名）
                    relativePathParts[0] = "***";
                }

                return string.Join(Path.DirectorySeparatorChar.ToString(), relativePathParts);
            }
            catch
            {
                // 如果解析失败，返回脱敏后的原始字符串
                return "***";
            }
        }

        /// <summary>
        /// 自定义脱敏
        /// </summary>
        public static string MaskCustom(string? value, int prefixLength = 1, int suffixLength = 1)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            if (value.Length <= prefixLength + suffixLength) return value;

            var prefix = value.Substring(0, prefixLength);
            var suffix = value.Substring(value.Length - suffixLength);
            var maskLength = value.Length - prefixLength - suffixLength;
            return prefix + new string('*', maskLength) + suffix;
        }

        #endregion

        #region 日志脱敏方法

        /// <summary>
        /// 遮罩敏感信息（通用方法）
        /// </summary>
        /// <param name="message">原始消息</param>
        /// <returns>遮罩后的消息</returns>
        public static string MaskSensitiveInfo(string? message)
        {
            if (string.IsNullOrEmpty(message)) return string.Empty;

            var maskedMessage = message;

            // 遮罩API密钥
            maskedMessage = Regex.Replace(maskedMessage, @"(api[_-]?key|apikey|api_key)\s*[=:]\s*([^\s,;]+)", "$1=***", RegexOptions.IgnoreCase);

            // 遮罩密码
            maskedMessage = Regex.Replace(maskedMessage, @"(password|pwd|pass)\s*[=:]\s*([^\s,;]+)", "$1=***", RegexOptions.IgnoreCase);

            // 遮罩连接字符串
            maskedMessage = Regex.Replace(maskedMessage, @"(server|host|database|uid|userid|user)\s*[=:]\s*([^\s,;]+)", "$1=***", RegexOptions.IgnoreCase);

            // 遮罩数据库连接字符串中的用户名（User Id）
            maskedMessage = Regex.Replace(maskedMessage, @"(User Id|uid|userid|user)\s*=\s*([^;]+)", "$1=****", RegexOptions.IgnoreCase);

            // 遮罩数据库连接字符串中的布尔值参数
            maskedMessage = Regex.Replace(maskedMessage, @"([^=;]+)\s*=\s*(true|false)", "$1=***", RegexOptions.IgnoreCase);

            // 遮罩IP地址
            maskedMessage = Regex.Replace(maskedMessage, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b", "***.***.***.***");

            // 遮罩手机号
            maskedMessage = Regex.Replace(maskedMessage, @"\b(\d{3})\d{4}(\d{4})\b", "$1****$2");

            // 遮罩身份证号
            maskedMessage = Regex.Replace(maskedMessage, @"\b(\d{4})\d{10}(\d{4})\b", "$1********$2");

            return maskedMessage;
        }

        /// <summary>
        /// 遮罩对象中的敏感信息
        /// </summary>
        /// <param name="obj">要遮罩的对象</param>
        /// <returns>遮罩后的字符串表示</returns>
        public static string MaskObject(object? obj)
        {
            if (obj == null) return "null";

            var jsonString = JsonConvert.SerializeObject(obj);
            return MaskSensitiveInfo(jsonString);
        }

        /// <summary>
        /// 遮罩异常中的敏感信息
        /// </summary>
        /// <param name="exception">要遮罩的异常</param>
        /// <returns>遮罩后的异常信息</returns>
        public static string MaskException(Exception? exception)
        {
            if (exception == null) return "null";

            var message = exception.Message;
            var stackTrace = exception.StackTrace;

            var maskedMessage = MaskSensitiveInfo(message);
            var maskedStackTrace = MaskSensitiveInfo(stackTrace);

            return $"Message: {maskedMessage}, StackTrace: {maskedStackTrace}";
        }

        /// <summary>
        /// 遮罩配置对象中的敏感信息
        /// </summary>
        /// <param name="configuration">要遮罩的配置对象</param>
        /// <returns>遮罩后的配置信息</returns>
        public static string MaskConfiguration(object? configuration)
        {
            if (configuration == null) return "null";

            var jsonString = JsonConvert.SerializeObject(configuration);
            return MaskSensitiveInfo(jsonString);
        }

        #endregion

        #region 数据库日志脱敏方法

        /// <summary>
        /// 遮罩数据库表名中的敏感信息
        /// </summary>
        /// <param name="tableName">原始表名</param>
        /// <returns>遮罩后的表名</returns>
        public static string MaskTableName(string? tableName)
        {
            if (string.IsNullOrEmpty(tableName)) return string.Empty;

            // 只对表名中的第二个下划线部分进行脱敏
            // 例如：Takt_accounting_budget_budget_account -> Takt_***_budget_budget_account
            var parts = tableName.Split('_');
            if (parts.Length >= 3)
            {
                // 保留第一部分，脱敏第二部分，保留剩余部分
                return $"{parts[0]}_***_{string.Join("_", parts.Skip(2))}";
            }

            return tableName;
        }

        /// <summary>
        /// 遮罩数据库操作日志
        /// </summary>
        /// <param name="logMessage">原始日志消息</param>
        /// <returns>遮罩后的日志消息</returns>
        public static string MaskDatabaseLog(string? logMessage)
        {
            if (string.IsNullOrEmpty(logMessage)) return string.Empty;

            var maskedMessage = logMessage;

            // 遮罩表名
            maskedMessage = Regex.Replace(
                maskedMessage,
                @"(\[.*?\]) (新建表|更新表) ([^\s]+)",
                match =>
                {
                    var databaseName = match.Groups[1].Value;
                    var operation = match.Groups[2].Value;
                    var tableName = match.Groups[3].Value;
                    var maskedTableName = MaskTableName(tableName);
                    return $"{databaseName} {operation} {maskedTableName}";
                }
            );

            // 遮罩数据库名称
            maskedMessage = Regex.Replace(
                maskedMessage,
                @"(\[)(.*?)(数据库)(\])",
                match =>
                {
                    var prefix = match.Groups[1].Value;
                    var dbType = match.Groups[2].Value;
                    var suffix = match.Groups[3].Value;
                    var endBracket = match.Groups[4].Value;

                    // 保留数据库类型，但遮罩敏感部分
                    var maskedDbType = MaskSensitiveDbType(dbType);
                    return $"{prefix}{maskedDbType}{suffix}{endBracket}";
                }
            );

            return maskedMessage;
        }

        /// <summary>
        /// 遮罩敏感数据库类型名称
        /// </summary>
        /// <param name="dbType">数据库类型名称</param>
        /// <returns>遮罩后的数据库类型名称</returns>
        private static string MaskSensitiveDbType(string dbType)
        {
            // 只对数据库类型名称中的第二个下划线部分进行脱敏
            // 例如：Takt_identity_database -> Takt_***_database
            var parts = dbType.Split('_');
            if (parts.Length >= 3)
            {
                // 保留第一部分，脱敏第二部分，保留剩余部分
                return $"{parts[0]}_***_{string.Join("_", parts.Skip(2))}";
            }

            return dbType;
        }

        /// <summary>
        /// 遮罩完整的数据库日志消息
        /// </summary>
        /// <param name="logMessage">原始日志消息</param>
        /// <returns>遮罩后的日志消息</returns>
        public static string MaskFullDatabaseLog(string? logMessage)
        {
            if (string.IsNullOrEmpty(logMessage)) return string.Empty;

            // 先进行数据库日志脱敏
            var maskedMessage = MaskDatabaseLog(logMessage);

            // 再进行通用敏感信息脱敏
            return MaskSensitiveInfo(maskedMessage);
        }

        #endregion

        #region 脱敏效果示例

        /*
        修复后的脱敏效果示例：
        
        表名脱敏 (MaskTableName):
        - Takt_identity_user -> Takt_***_user
        - Takt_accounting_budget_account -> Takt_***_budget_account
        - Takt_workflow_instance -> Takt_***_instance
        
        数据库类型脱敏 (MaskSensitiveDbType):
        - Takt_identity_database -> Takt_***_database
        - Takt_business_database -> Takt_***_database
        - Takt_workflow_database -> Takt_***_database
        
        脱敏规则：只对第二个下划线部分进行脱敏，保留第一部分和剩余部分
        */

        #endregion
    }
}

