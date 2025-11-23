//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStringUtils.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 字符串工具类
//===================================================================

namespace Takt.Shared.Utils
{
    /// <summary>
    /// 字符串工具类
    /// </summary>
    public static class TaktStringUtils
    {
        /// <summary>
        /// 转换为Pascal命名
        /// </summary>
        public static string ToPascalCase(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
        }

        /// <summary>
        /// 转换为Camel命名
        /// </summary>
        public static string ToCamelCase(string input)
        {
            var pascal = ToPascalCase(input);
            return pascal.Length > 0 ? char.ToLower(pascal[0]) + pascal.Substring(1) : pascal;
        }

        /// <summary>
        /// 获取C#类型
        /// </summary>
        public static string GetCsharpType(string dbType)
        {
            return dbType.ToLower() switch
            {
                "int" => "int",
                "bigint" => "long",
                "smallint" => "short",
                "tinyint" => "byte",
                "decimal" => "decimal",
                "numeric" => "decimal",
                "float" => "float",
                "real" => "double",
                "datetime" => "DateTime",
                "date" => "DateTime",
                "time" => "TimeSpan",
                "char" => "string",
                "varchar" => "string",
                "nvarchar" => "string",
                "text" => "string",
                "ntext" => "string",
                "bit" => "bool",
                "uniqueidentifier" => "Guid",
                _ => "string"
            };
        }
    }
} 


