//===================================================================
// 项目名 : Takt.Shared
// 文件名 : TaktRealTimeNotification.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 实时通知模型
//===================================================================

using System;
using Takt.Shared.Enums;

namespace Takt.Shared.Models
{
    /// <summary>
    /// 实时通知模型
    /// </summary>
    public class TaktRealTimeNotification
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public TaktMessageType Type { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }
    }
} 



