//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktExtensions.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 系统通用扩展方法
//===================================================================

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Takt.Shared.Constants;

namespace Takt.Shared.Extensions
{
    /// <summary>
    /// 系统通用扩展方法
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-05
    /// </remarks>
    public static class TaktExtensions
    {
        /// <summary>
        /// 默认的JSON序列化设置
        /// </summary>
        private static readonly JsonSerializerSettings DefaultSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            DateFormatString = TaktConstants.DateTimeFormats.StandardDateTime
        };

        /// <summary>
        /// 将对象序列化为JSON字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson<T>(this T obj)
        {
            if (obj == null) return string.Empty;
            return JsonConvert.SerializeObject(obj, DefaultSettings);
        }

        /// <summary>
        /// 将JSON字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">目标对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>反序列化后的对象</returns>
        public static T? FromJson<T>(this string json)
        {
            if (string.IsNullOrEmpty(json)) return default;
            return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
        }

        /// <summary>
        /// 判断字符串是否为null或空
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <returns>是否为null或空</returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为null或空白字符
        /// </summary>
        /// <param name="str">要判断的字符串</param>
        /// <returns>是否为null或空白字符</returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 将日期时间格式化为日期字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>格式化后的日期字符串</returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString(TaktConstants.DateTimeFormats.StandardDate);
        }

        /// <summary>
        /// 将日期时间格式化为日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>格式化后的日期时间字符串</returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(TaktConstants.DateTimeFormats.StandardDateTime);
        }

        /// <summary>
        /// 将日期时间格式化为带毫秒的日期时间字符串
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>格式化后的带毫秒的日期时间字符串</returns>
        public static string ToDateTimeWithMsString(this DateTime dateTime)
        {
            return dateTime.ToString(TaktConstants.DateTimeFormats.FullDateTime);
        }
    }
}




