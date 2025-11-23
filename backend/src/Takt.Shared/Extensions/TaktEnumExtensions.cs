//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktEnumExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 18:55
// 版本号 : V0.0.1
// 描述   : 枚举扩展方法
//===================================================================

using System.ComponentModel;
using System.Reflection;


namespace Takt.Shared.Extensions;

/// <summary>
/// 枚举扩展方法
/// </summary>
public static class TaktEnumExtensions
{
    /// <summary>
    /// 获取枚举的Description特性描述
    /// </summary>
    /// <param name="value">枚举值</param>
    /// <returns>描述</returns>
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            return string.Empty;
        }

        var field = value.GetType().GetField(value.ToString());
        if (field == null)
        {
            return value.ToString();
        }

        var attribute = field.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }

    /// <summary>
    /// 比较枚举值是否等于指定的整数值
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="enumValue">枚举值</param>
    /// <param name="value">要比较的整数值</param>
    /// <returns>是否相等</returns>
    public static bool EqualsInt<TEnum>(this TEnum enumValue, int value) where TEnum : Enum
    {
        return Convert.ToInt32(enumValue) == value;
    }

    /// <summary>
    /// 将整数值转换为指定的枚举类型
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="value">整数值</param>
    /// <returns>枚举值</returns>
    public static TEnum ToEnum<TEnum>(this int value) where TEnum : Enum
    {
        return (TEnum)Enum.ToObject(typeof(TEnum), value);
    }
} 




