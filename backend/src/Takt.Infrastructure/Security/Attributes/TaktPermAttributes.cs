//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPermAttributes.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:00
// 版本号 : V0.0.1
// 描述    : 权限特性定义
//===================================================================

using System;

namespace Takt.Infrastructure.Security.Attributes
{
    /// <summary>
    /// 权限特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TaktPermAttribute : Attribute
    {
        /// <summary>
        /// 权限编码
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string Permission { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="permission">权限编码(格式:模块:实体:操作)</param>
        public TaktPermAttribute(string permission)
        {
            Permission = permission;
        }
    }

    /// <summary>
    /// 日志特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TaktLogAttribute : Attribute
    {
        /// <summary>
        /// 日志标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">日志标题</param>
        public TaktLogAttribute(string title)
        {
            Title = title;
        }
    }

    /// <summary>
    /// 认证特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TaktAuthAttribute : Attribute
    {
    }
} 



