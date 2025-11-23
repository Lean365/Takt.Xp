//===================================================================
// 项目名 : Takt.Domain.Entities.Logging
// 文件名 : TaktQuartz.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 定时任务日志实体
//===================================================================

using Takt.Domain.Entities.Routine.Quartz;

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// 定时任务日志实体
    /// </summary>
    [SugarTable("Takt_audit_quartz_log", "任务日志")]
    [SugarIndex("index_quartz_task_id", nameof(QuartzId), OrderByType.Asc)]
    [SugarIndex("index_quartz_execute_time", nameof(QuartzExecuteTime), OrderByType.Desc)]
    [SugarIndex("index_quartz_status", nameof(Status), OrderByType.Asc)]
    public class TaktQuartzLog : TaktBaseEntity
    {
        /// <summary>
        /// 任务ID（关联TaktQuartz表）
        /// </summary>
        [SugarColumn(ColumnName = "quartz_id", ColumnDescription = "任务ID（关联TaktQuartz表）", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long QuartzId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "quartz_name", ColumnDescription = "任务名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string QuartzName { get; set; } = string.Empty;

        /// <summary>
        /// 任务组名
        /// </summary>
        [SugarColumn(ColumnName = "quartz_group_name", ColumnDescription = "任务组名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string QuartzGroupName { get; set; } = string.Empty;

        /// <summary>
        /// 任务类型（1.程序集 2.网络请求 3.SQL语句）
        /// </summary>
        [SugarColumn(ColumnName = "quartz_task_type", ColumnDescription = "任务类型（1.程序集 2.网络请求 3.SQL语句）", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzType { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_time", ColumnDescription = "执行时间", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime QuartzExecuteTime { get; set; }

        /// <summary>
        /// 执行耗时（毫秒）
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_duration", ColumnDescription = "执行耗时（毫秒）", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long QuartzExecuteDuration { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_params", ColumnDescription = "执行参数", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 执行消息
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_message", ColumnDescription = "执行消息", Length = 2000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzExecuteMessage { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [SugarColumn(ColumnName = "quartz_error_info", ColumnDescription = "错误信息", Length = 2000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzErrorInfo { get; set; }

        /// <summary>
        /// 执行机器IP
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_ip", ColumnDescription = "执行机器IP", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzExecuteIp { get; set; }

        /// <summary>
        /// 执行机器名
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_host", ColumnDescription = "执行机器名", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzExecuteHost { get; set; }

        /// <summary>
        /// 执行状态（0失败 1成功）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "执行状态（0失败 1成功）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }
        /// <summary>
        /// 关联的定时任务实体
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(QuartzId))]
        public TaktQuartz? QuartzTask { get; set; }
    }
}



