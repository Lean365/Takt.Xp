using System;
using System.Text.RegularExpressions;
using System.Reflection;
using SqlSugar;  // SqlSugar主命名空间
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Takt.Shared.Helpers
{
    /// <summary>
    /// 字符串处理帮助类
    /// </summary>
    public static class TaktStringHelper
    {
        /// <summary>
        /// 确保字符串不超过指定长度
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="suffix">超长时的后缀，默认为"..."</param>
        /// <returns>处理后的字符串</returns>
        public static string EnsureMaxLength(string? input, int maxLength, string suffix = "...")
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            if (input.Length <= maxLength)
                return input;

            var actualMaxLength = maxLength - suffix.Length;
            if (actualMaxLength <= 0)
                return suffix.Substring(0, maxLength);

            return input.Substring(0, actualMaxLength) + suffix;
        }

        /// <summary>
        /// 根据实体类的属性特性自动处理字符串字段长度
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns>处理后的实体对象</returns>
        public static T? EnsureEntityStringLength<T>(T? entity) where T : class
        {
            if (entity == null)
                return null;

            var properties = typeof(T).GetProperties();
            foreach (var prop in properties)
            {
                if (prop.PropertyType != typeof(string))
                    continue;

                var sugarAttr = prop.GetCustomAttribute<SugarColumn>();
                var maxLengthAttr = prop.GetCustomAttribute<MaxLengthAttribute>();
                
                var maxLength = sugarAttr?.Length ?? maxLengthAttr?.Length ?? 0;
                if (maxLength <= 0)
                    continue;

                var value = prop.GetValue(entity) as string;
                if (value == null)
                    continue;

                var safeValue = EnsureMaxLength(value, maxLength);
                prop.SetValue(entity, safeValue);
            }

            return entity;
        }

        /// <summary>
        /// 根据实体类的SugarColumn特性自动处理对象的字符串属性长度
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="obj">对象实例</param>
        /// <param name="entityType">实体类型（用于获取长度限制）</param>
        /// <returns>处理后的对象</returns>
        public static T? EnsureObjectStringLength<T>(T? obj, Type entityType) where T : class
        {
            if (obj == null)
                return null;

            var objProperties = typeof(T).GetProperties();
            var entityProperties = entityType.GetProperties();

            foreach (var objProp in objProperties)
            {
                // 只处理字符串类型的属性
                if (objProp.PropertyType != typeof(string))
                    continue;

                // 查找对应的实体属性
                var entityProp = entityProperties.FirstOrDefault(p => p.Name == objProp.Name);
                if (entityProp == null)
                    continue;

                var attr = entityProp.GetCustomAttribute<SugarColumn>();
                if (attr == null || attr.Length <= 0)
                    continue;

                var value = objProp.GetValue(obj) as string;
                if (value == null)
                    continue;

                var safeValue = EnsureMaxLength(value, attr.Length);
                objProp.SetValue(obj, safeValue);
            }

            return obj;
        }

        /// <summary>
        /// 清理HTML标签
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>清理后的字符串</returns>
        public static string StripHtml(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Regex.Replace(input, "<.*?>", string.Empty);
        }

        /// <summary>
        /// 清理特殊字符
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>清理后的字符串</returns>
        public static string CleanSpecialChars(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Regex.Replace(input, @"[^\w\d\s-]", string.Empty);
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="useSpecialChars">是否使用特殊字符</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomString(int length, bool useSpecialChars = false)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            var allowedChars = useSpecialChars ? chars + specialChars : chars;
            
            var random = new Random();
            return new string(Enumerable.Repeat(allowedChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
} 

