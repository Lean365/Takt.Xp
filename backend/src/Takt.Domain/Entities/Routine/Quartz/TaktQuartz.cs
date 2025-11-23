//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktQuartz.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 定时任务实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Quartz
{
    /// <summary>
    /// 定时任务实体
    /// </summary>
    [SugarTable("Takt_routine_quartz", "定时任务")]
    [SugarIndex("index_quartz_name", nameof(QuartzName), OrderByType.Asc)]
    [SugarIndex("index_quartz_group", nameof(QuartzGroupName), OrderByType.Asc)]
    [SugarIndex("index_quartz_status", nameof(Status), OrderByType.Asc)]
    public class TaktQuartz : TaktBaseEntity
    {
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
        [SugarColumn(ColumnName = "quartz_type", ColumnDescription = "任务类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        [SugarColumn(ColumnName = "quartz_assembly_name", ColumnDescription = "程序集名称", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string QuartzAssemblyName { get; set; } = string.Empty;

        /// <summary>
        /// 任务类名
        /// </summary>
        [SugarColumn(ColumnName = "quartz_class_name", ColumnDescription = "任务类名", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string QuartzClassName { get; set; } = string.Empty;

        /// <summary>
        /// API执行地址
        /// </summary>
        [SugarColumn(ColumnName = "quartz_api_url", ColumnDescription = "API执行地址", Length = 255, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 网络请求方式
        /// </summary>
        [SugarColumn(ColumnName = "quartz_request_method", ColumnDescription = "网络请求方式", Length = 10, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        [SugarColumn(ColumnName = "quartz_sql", ColumnDescription = "SQL语句", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型（0.simple 1.cron）
        /// </summary>
        [SugarColumn(ColumnName = "quartz_trigger_type", ColumnDescription = "触发器类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [SugarColumn(ColumnName = "quartz_cron_expression", ColumnDescription = "Cron表达式", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string QuartzCronExpression { get; set; } = string.Empty;

        /// <summary>
        /// 执行间隔时间（单位秒）
        /// </summary>
        [SugarColumn(ColumnName = "quartz_interval", ColumnDescription = "执行间隔时间（单位秒）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_params", ColumnDescription = "执行参数", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        [SugarColumn(ColumnName = "quartz_concurrent", ColumnDescription = "是否并发执行", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [SugarColumn(ColumnName = "quartz_start_time", ColumnDescription = "开始时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [SugarColumn(ColumnName = "quartz_end_time", ColumnDescription = "结束时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? QuartzEndTime { get; set; }

        /// <summary>
        /// 最近执行时间
        /// </summary>
        [SugarColumn(ColumnName = "quartz_last_run_time", ColumnDescription = "最近执行时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? QuartzLastRunTime { get; set; }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        [SugarColumn(ColumnName = "quartz_next_run_time", ColumnDescription = "下次执行时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? QuartzNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        [SugarColumn(ColumnName = "quartz_execute_count", ColumnDescription = "执行次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int QuartzExecuteCount { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 关联的任务日志列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(Takt.Domain.Entities.Logging.TaktQuartzLog.QuartzId))]
        public List<Takt.Domain.Entities.Logging.TaktQuartzLog>? QuartzLogs { get; set; }
    }
}



