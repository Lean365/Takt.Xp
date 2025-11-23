//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktException.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 19:00
// 版本号 : V0.0.1
// 描述    : 自定义异常类
//===================================================================

using System;
using Takt.Shared.Constants;

namespace Takt.Shared.Exceptions
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktException : Exception
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// 构造函数 - 默认为服务器错误
        /// </summary>
        /// <param name="message">错误消息</param>
        public TaktException(string message) 
            : this(message, TaktConstants.ErrorCodes.ServerError)
        {
        }

        /// <summary>
        /// 构造函数 - 指定错误码
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        public TaktException(string message, string code) : base(message)
        {
            Code = int.Parse(code);
        }

        /// <summary>
        /// 构造函数 - 包含内部异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="innerException">内部异常</param>
        public TaktException(string message, Exception innerException) : base(message, innerException)
        {
            Code = int.Parse(TaktConstants.ErrorCodes.ServerError);
        }

        /// <summary>
        /// 构造函数 - 完整参数
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误码</param>
        /// <param name="innerException">内部异常</param>
        public TaktException(string message, string code, Exception innerException) : base(message, innerException)
        {
            Code = int.Parse(code);
        }

        /// <summary>
        /// 创建验证错误异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static TaktException ValidationError(string message)
        {
            return new TaktException(message, TaktConstants.ErrorCodes.ValidationFailed);
        }

        /// <summary>
        /// 创建未授权异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static TaktException Unauthorized(string message)
        {
            return new TaktException(message, TaktConstants.ErrorCodes.Unauthorized);
        }

        /// <summary>
        /// 创建禁止访问异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static TaktException Forbidden(string message)
        {
            return new TaktException(message, TaktConstants.ErrorCodes.Forbidden);
        }

        /// <summary>
        /// 创建资源未找到异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static TaktException NotFound(string message)
        {
            return new TaktException(message, TaktConstants.ErrorCodes.NotFound);
        }

        /// <summary>
        /// 创建业务错误异常
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <returns>异常实例</returns>
        public static TaktException BusinessError(string message)
        {
            return new TaktException(message, TaktConstants.ErrorCodes.ServerError);
        }
    }
}




