//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktScheduleDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 日程数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Schedule
{
    /// <summary>
    /// 日程基础DTO
    /// </summary>
    public class TaktScheduleDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long ScheduleId { get; set; }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 日程类型
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// 日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 重复类型
        /// </summary>
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

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
        /// 是否删除
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
    /// 日程查询DTO
    /// </summary>
    public class TaktScheduleQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleQueryDto()
        {
            Title = string.Empty;
            Location = string.Empty;
        }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 日程类型
        /// </summary>
        public int? ScheduleType { get; set; }

        /// <summary>
        /// 日程状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建者（用于当前用户日程过滤）
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 日程创建DTO
    /// </summary>
    public class TaktScheduleCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleCreateDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
        }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 日程类型
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// 日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 重复类型
        /// </summary>
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }
    }

    /// <summary>
    /// 日程更新DTO
    /// </summary>
    public class TaktScheduleUpdateDto : TaktScheduleCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ScheduleId { get; set; }
    }

    /// <summary>
    /// 日程删除DTO
    /// </summary>
    public class TaktScheduleDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long ScheduleId { get; set; }
    }

    /// <summary>
    /// 日程批量删除DTO
    /// </summary>
    public class TaktScheduleBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> ScheduleIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 日程导入DTO
    /// </summary>
    public class TaktScheduleImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleImportDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
        }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 日程类型
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// 日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 重复类型
        /// </summary>
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }
    }

    /// <summary>
    /// 日程导出DTO
    /// </summary>
    public class TaktScheduleExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleExportDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 日程类型
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// 日程状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 重复类型
        /// </summary>
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 日程导入模板DTO
    /// </summary>
    public class TaktScheduleTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktScheduleTemplateDto()
        {
            Title = string.Empty;
            Content = string.Empty;
            Location = string.Empty;
        }

        /// <summary>
        /// 日程标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日程内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 日程类型(0=普通日程 1=重要日程 2=紧急日程)
        /// </summary>
        public int ScheduleType { get; set; }

        /// <summary>
        /// 日程状态(0=未开始 1=进行中 2=已完成 3=已取消)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否全天(0=否 1=是)
        /// </summary>
        public int IsAllDay { get; set; }

        /// <summary>
        /// 提醒时间（分钟）
        /// </summary>
        public int? RemindMinutes { get; set; }

        /// <summary>
        /// 重复类型(0=不重复 1=每天 2=每周 3=每月 4=每年)
        /// </summary>
        public int RepeatType { get; set; }

        /// <summary>
        /// 重复结束时间
        /// </summary>
        public DateTime? RepeatEndTime { get; set; }

        /// <summary>
        /// 地点
        /// </summary>
        public string? Location { get; set; }
    }
} 


