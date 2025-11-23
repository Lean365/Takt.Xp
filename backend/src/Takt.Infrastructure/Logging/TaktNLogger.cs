//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNLogger.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V.0.0.1
// 描述    : NLog日志实现
//===================================================================

using Takt.Domain.IServices.Extensions;
using NLog;

namespace Takt.Infrastructure.Logging
{
    /// <summary>
    /// NLog日志实现
    /// </summary>
    public class TaktNLogger : ITaktLogger
    {
        /// <summary>
        /// NLog日志记录器
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNLogger(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 记录调试级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        /// <summary>
        /// 记录带参数的调试级别日志
        /// </summary>
        /// <param name="message">日志消息模板</param>
        /// <param name="args">参数数组</param>
        public void Debug(string message, params object[] args)
        {
            _logger.Debug(message, args);
        }

        /// <summary>
        /// 记录信息级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Info(string message)
        {
            _logger.Info(message);
        }

        /// <summary>
        /// 记录带参数的信息级别日志
        /// </summary>
        /// <param name="message">日志消息模板</param>
        /// <param name="args">参数数组</param>
        public void Info(string message, params object[] args)
        {
            _logger.Info(message, args);
        }

        /// <summary>
        /// 记录警告级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        /// <summary>
        /// 记录带参数的警告级别日志
        /// </summary>
        /// <param name="message">日志消息模板</param>
        /// <param name="args">参数数组</param>
        public void Warn(string message, params object[] args)
        {
            _logger.Warn(message, args);
        }

        /// <summary>
        /// 记录错误级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Error(string message)
        {
            _logger.Error(message);
        }

        /// <summary>
        /// 记录带异常的错误级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常对象</param>
        public void Error(string message, Exception ex)
        {
            _logger.Error( message);
        }

        /// <summary>
        /// 记录带参数的错误级别日志
        /// </summary>
        /// <param name="message">日志消息模板</param>
        /// <param name="args">参数数组</param>
        public void Error(string message, params object[] args)
        {
            _logger.Error(message, args);
        }

        /// <summary>
        /// 记录致命错误级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        /// <summary>
        /// 记录带异常的致命错误级别日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="ex">异常对象</param>
        public void Fatal(string message, Exception ex)
        {
            _logger.Fatal(ex, message);
        }

        /// <summary>
        /// 记录带参数的致命错误级别日志
        /// </summary>
        /// <param name="message">日志消息模板</param>
        /// <param name="args">参数数组</param>
        public void Fatal(string message, params object[] args)
        {
            _logger.Fatal(message, args);
        }
    }
}



