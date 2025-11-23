//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExceptionLogDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 异常日志数据传输对象
//===================================================================

using System;
using System.ComponentModel.DataAnnotations;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logging
{
    /// <summary>
    /// 异常日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktExceptionLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktExceptionLogDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            Parameters = string.Empty;
            ExceptionType = string.Empty;
            ExceptionMessage = string.Empty;
            StackTrace = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long ExceptionLogId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameters { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// 堆栈跟踪
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 异常日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktExceptionLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktExceptionLogQueryDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            ExceptionType = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>

        public string Method { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>

        public string ExceptionType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 异常日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktExceptionLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktExceptionLogExportDto()
        {
            UserName = string.Empty;
            Method = string.Empty;
            ExceptionType = string.Empty;
            ExceptionMessage = string.Empty;
            IpAddress = string.Empty;
            UserAgent = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 异常类型
        /// </summary>
        public string ExceptionType { get; set; }

        /// <summary>
        /// 异常消息
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 





