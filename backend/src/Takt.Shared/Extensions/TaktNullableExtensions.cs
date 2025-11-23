//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktNullableExtensions.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 22:50
// 版本号 : V.0.0.1
// 描述    : 空值处理扩展方法
//===================================================================

namespace Takt.Shared.Extensions
{
    /// <summary>
    /// 空值处理扩展方法
    /// </summary>
    public static class TaktNullableExtensions
    {
        /// <summary>
        /// 安全获取值，当值为null时返回默认值
        /// </summary>
        public static T SafeValue<T>(this T? value, T defaultValue = default!) where T : class
        {
            return value ?? defaultValue;
        }

        /// <summary>
        /// 安全获取值，当值为null时返回默认值
        /// </summary>
        public static T SafeValue<T>(this T? value, T defaultValue = default) where T : struct
        {
            return value ?? defaultValue;
        }

        /// <summary>
        /// 安全执行方法，当值为null时返回默认值
        /// </summary>
        public static TResult? SafeExecute<T, TResult>(this T? value, Func<T, TResult> selector) where T : class
        {
            return value == null ? default : selector(value);
        }

        /// <summary>
        /// 安全转换为字符串，当值为null时返回空字符串
        /// </summary>
        public static string SafeToString(this object? value)
        {
            return value?.ToString() ?? string.Empty;
        }
    }
} 




