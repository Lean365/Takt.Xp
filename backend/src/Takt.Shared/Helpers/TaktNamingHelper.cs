#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNamingHelper.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 命名辅助类
//===================================================================

using System.Text;

namespace Takt.Shared.Helpers;

/// <summary>
/// 命名辅助类
/// </summary>
public static class TaktNamingHelper
{
    /// <summary>
    /// 从表名中提取模块名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>模块名</returns>
    public static string ExtractTransType(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            return string.Empty;

        // 查找第一个_的位置
        int firstUnderscoreIndex = tableName.IndexOf('_');
        if (firstUnderscoreIndex == -1)
            return string.Empty;

        // 查找第二个_的位置
        int secondUnderscoreIndex = tableName.IndexOf('_', firstUnderscoreIndex + 1);
        if (secondUnderscoreIndex == -1)
            return string.Empty;

        // 提取第一个_和第二个_之间的字符串
        string TransType = tableName.Substring(firstUnderscoreIndex + 1, secondUnderscoreIndex - firstUnderscoreIndex - 1);

        // 转换为帕斯卡命名法
        if (!string.IsNullOrEmpty(TransType))
        {
            return char.ToUpper(TransType[0]) + TransType.Substring(1).ToLower();
        }

        return TransType;
    }

    /// <summary>
    /// 将表名转换为类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>类名</returns>
    private static string ConvertToClassName(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            return string.Empty;

        // 查找第一个_的位置
        int firstUnderscoreIndex = tableName.IndexOf('_');
        if (firstUnderscoreIndex == -1)
            return tableName;

        // 查找第二个_的位置
        int secondUnderscoreIndex = tableName.IndexOf('_', firstUnderscoreIndex + 1);
        if (secondUnderscoreIndex == -1)
            return tableName;

        // 移除第一个_和第二个_之间的所有字符
        string processedName = tableName.Substring(0, firstUnderscoreIndex + 1) + tableName.Substring(secondUnderscoreIndex);

        // 分割字符串（以下划线为分隔符）
        var words = processedName.Split('_');

        // 将每个单词首字母大写
        var result = new StringBuilder();
        foreach (var word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                result.Append(char.ToUpper(word[0]));
                if (word.Length > 1)
                    result.Append(word.Substring(1).ToLower());
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 获取实体命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>实体命名空间</returns>
    public static string GetEntityNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.Domain.Entities.{TransType}";
    }

    /// <summary>
    /// 获取实体类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>实体类名</returns>
    public static string GetEntityClassName(string tableName)
    {
        return ConvertToClassName(tableName);
    }

    /// <summary>
    /// 获取DTO命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>DTO命名空间</returns>
    public static string GetDtoNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.Application.Dtos.{TransType}";
    }

    /// <summary>
    /// 获取DTO类型名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>DTO类型名</returns>
    public static string GetDtoTypeName(string tableName)
    {
        return $"{ConvertToClassName(tableName)}Dto";
    }

    /// <summary>
    /// 获取DTO类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>DTO类名</returns>
    public static string GetDtoClassName(string tableName)
    {
        return $"{ConvertToClassName(tableName)}Dto";
    }

    /// <summary>
    /// 获取服务命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>服务命名空间</returns>
    public static string GetServiceNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.Application.Services.{TransType}";
    }

    /// <summary>
    /// 获取服务接口类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>服务接口类名</returns>
    public static string GetIServiceClassName(string tableName)
    {
        return $"I{ConvertToClassName(tableName)}Service";
    }

    /// <summary>
    /// 获取服务类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>服务类名</returns>
    public static string GetServiceClassName(string tableName)
    {
        return $"{ConvertToClassName(tableName)}Service";
    }

    /// <summary>
    /// 获取仓储接口命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>仓储接口命名空间</returns>
    public static string GetIRepositoryNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.Domain.Repositories.{TransType}";
    }

    /// <summary>
    /// 获取仓储接口类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>仓储接口类名</returns>
    public static string GetIRepositoryClassName(string tableName)
    {
        return $"I{ConvertToClassName(tableName)}Repository";
    }

    /// <summary>
    /// 获取仓储命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>仓储命名空间</returns>
    public static string GetRepositoryNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.Infrastructure.Repositories.{TransType}";
    }

    /// <summary>
    /// 获取仓储类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>仓储类名</returns>
    public static string GetRepositoryClassName(string tableName)
    {
        return $"{ConvertToClassName(tableName)}Repository";
    }

    /// <summary>
    /// 获取控制器命名空间
    /// </summary>
    /// <param name="baseNamespace">基础命名空间</param>
    /// <param name="tableName">表名</param>
    /// <returns>控制器命名空间</returns>
    public static string GetControllerNamespace(string baseNamespace, string tableName)
    {
        string TransType = ExtractTransType(tableName);
        return $"{baseNamespace}.WebApi.Controllers.{TransType}";
    }

    /// <summary>
    /// 获取控制器类名
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>控制器类名</returns>
    public static string GetControllerClassName(string tableName)
    {
        return $"{ConvertToClassName(tableName)}Controller";
    }

    /// <summary>
    /// 获取权限前缀
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>权限前缀</returns>
    public static string GetPermsPrefix(string tableName)
    {
        if (string.IsNullOrEmpty(tableName))
            return tableName.ToLower();

        // 查找第一个_的位置
        int firstUnderscoreIndex = tableName.IndexOf('_');
        if (firstUnderscoreIndex == -1)
            return tableName.ToLower();

        // 查找第二个_的位置
        int secondUnderscoreIndex = tableName.IndexOf('_', firstUnderscoreIndex + 1);
        if (secondUnderscoreIndex == -1)
        {
            // 如果只有一个下划线，直接返回下划线后的部分
            return tableName.Substring(firstUnderscoreIndex + 1).ToLower();
        }

        // 获取模块名（第一个_和第二个_之间的部分）
        string TransType = tableName.Substring(firstUnderscoreIndex + 1, secondUnderscoreIndex - firstUnderscoreIndex - 1).ToLower();

        // 获取功能名（第二个_后的部分）并去掉所有下划线
        string functionName = tableName.Substring(secondUnderscoreIndex + 1).Replace("_", "").ToLower();

        // 用冒号连接模块名和功能名
        return $"{TransType}:{functionName}";
    }

    /// <summary>
    /// 获取业务名称
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>业务名称</returns>
    public static string GetBusinessName(string tableName)
    {
        return ExtractTransType(tableName);
    }
} 


