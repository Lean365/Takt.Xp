//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktSchedule.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 日程管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Schedule;

/// <summary>
/// 日程管理实体
/// </summary>
[SugarTable("Takt_routine_schedule", "日程管理")]
[SugarIndex("index_schedule_title", nameof(Title), OrderByType.Asc)]
[SugarIndex("index_schedule_type", nameof(ScheduleType), OrderByType.Asc)]
[SugarIndex("index_schedule_status", nameof(Status), OrderByType.Asc)]
public class TaktSchedule : TaktBaseEntity
{
    /// <summary>
    /// 日程标题
    /// </summary>
    [SugarColumn(ColumnName = "title", ColumnDescription = "日程标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 日程内容
    /// </summary>
    [SugarColumn(ColumnName = "content", ColumnDescription = "日程内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 日程类型（0：普通日程，1：重要日程，2：紧急日程）
    /// </summary>
    [SugarColumn(ColumnName = "schedule_type", ColumnDescription = "日程类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public int ScheduleType { get; set; }

    /// <summary>
    /// 日程状态（0：未开始，1：进行中，2：已完成，3：已取消）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "日程状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public int Status { get; set; }

    /// <summary>
    /// 是否全天（0：否，1：是）
    /// </summary>
    [SugarColumn(ColumnName = "is_all_day", ColumnDescription = "是否全天", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public int IsAllDay { get; set; }

    /// <summary>
    /// 提醒时间（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "remind_minutes", ColumnDescription = "提醒时间", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public int? RemindMinutes { get; set; }

    /// <summary>
    /// 重复类型（0：不重复，1：每天，2：每周，3：每月，4：每年）
    /// </summary>
    [SugarColumn(ColumnName = "repeat_type", ColumnDescription = "重复类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public int RepeatType { get; set; }

    /// <summary>
    /// 重复结束时间
    /// </summary>
    [SugarColumn(ColumnName = "repeat_end_time", ColumnDescription = "重复结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public DateTime? RepeatEndTime { get; set; }

    /// <summary>
    /// 地点
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "地点", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
    public string? Location { get; set; }

}



