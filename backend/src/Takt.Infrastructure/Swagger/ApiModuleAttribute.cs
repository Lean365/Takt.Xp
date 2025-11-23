//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ApiModuleAttribute.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-24 14:30
// 版本号 : V0.0.1
// 描述    : API模块特性
//===================================================================

using System;

namespace Takt.Infrastructure.Swagger
{
    /// <summary>
    /// API模块特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiModuleAttribute : Attribute
    {
        /// <summary>
        /// 模块代码
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">模块代码</param>
        /// <param name="name">模块名称</param>
        public ApiModuleAttribute(string code, string name)
        {
            Code = code;
            Name = name;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">模块代码</param>
        public ApiModuleAttribute(string code)
        {
            Code = code;
        }
    }
} 



