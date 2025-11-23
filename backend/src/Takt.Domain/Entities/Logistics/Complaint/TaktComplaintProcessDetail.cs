#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktComplaintProcessDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 客诉处理子表实体类 (基于SAP QM质量管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 客诉处理子表实体类 (基于SAP QM质量管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP QM 质量管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_complaint_process_detail", "客诉处理明细")]
    [SugarIndex("ix_process_id", nameof(ProcessId), OrderByType.Asc, false)]
    [SugarIndex("ix_process_code", nameof(ProcessCode), OrderByType.Asc, false)]
    [SugarIndex("ix_complaint_code", nameof(ComplaintCode), OrderByType.Asc, false)]
    public class TaktComplaintProcessDetail : TaktBaseEntity
    {
        /// <summary>
        /// 处理主表ID
        /// </summary>
        [SugarColumn(ColumnName = "process_id", ColumnDescription = "处理主表ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ProcessId { get; set; }

        /// <summary>
        /// 处理编号
        /// </summary>
        [SugarColumn(ColumnName = "process_code", ColumnDescription = "处理编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProcessCode { get; set; } = string.Empty;

        /// <summary>
        /// 客诉编号
        /// </summary>
        [SugarColumn(ColumnName = "complaint_code", ColumnDescription = "客诉编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComplaintCode { get; set; } = string.Empty;

        /// <summary>
        /// 处理主表关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ProcessCode))]
        public TaktComplaintProcess? Process { get; set; }

        /// <summary>
        /// 行项目编号
        /// </summary>
        [SugarColumn(ColumnName = "line_number", ColumnDescription = "行项目编号", ColumnDataType = "int", IsNullable = false)]
        public int LineNumber { get; set; }

        /// <summary>
        /// 处理步骤(1=问题确认 2=原因分析 3=方案制定 4=方案执行 5=效果验证 6=总结关闭)
        /// </summary>
        [SugarColumn(ColumnName = "process_step", ColumnDescription = "处理步骤", ColumnDataType = "int", IsNullable = false)]
        public int ProcessStep { get; set; }

        /// <summary>
        /// 处理步骤名称
        /// </summary>
        [SugarColumn(ColumnName = "process_step_name", ColumnDescription = "处理步骤名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessStepName { get; set; }

        /// <summary>
        /// 处理措施编号
        /// </summary>
        [SugarColumn(ColumnName = "measure_code", ColumnDescription = "处理措施编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeasureCode { get; set; }

        /// <summary>
        /// 处理措施描述
        /// </summary>
        [SugarColumn(ColumnName = "measure_description", ColumnDescription = "处理措施描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeasureDescription { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        [SugarColumn(ColumnName = "task_code", ColumnDescription = "任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskCode { get; set; }

        /// <summary>
        /// 任务描述
        /// </summary>
        [SugarColumn(ColumnName = "task_description", ColumnDescription = "任务描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskDescription { get; set; }

        /// <summary>
        /// 任务状态(0=待执行 1=执行中 2=已完成 3=已取消)
        /// </summary>
        [SugarColumn(ColumnName = "task_status", ColumnDescription = "任务状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TaskStatus { get; set; } = 0;

        /// <summary>
        /// 任务状态名称
        /// </summary>
        [SugarColumn(ColumnName = "task_status_name", ColumnDescription = "任务状态名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskStatusName { get; set; }

        /// <summary>
        /// 执行人ID
        /// </summary>
        [SugarColumn(ColumnName = "executor_id", ColumnDescription = "执行人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ExecutorId { get; set; }

        /// <summary>
        /// 执行人编码
        /// </summary>
        [SugarColumn(ColumnName = "executor_code", ColumnDescription = "执行人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecutorCode { get; set; }

        /// <summary>
        /// 执行人姓名
        /// </summary>
        [SugarColumn(ColumnName = "executor_name", ColumnDescription = "执行人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecutorName { get; set; }

        /// <summary>
        /// 计划开始日期
        /// </summary>
        [SugarColumn(ColumnName = "plan_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlanStartDate { get; set; }

        /// <summary>
        /// 计划完成日期
        /// </summary>
        [SugarColumn(ColumnName = "plan_complete_date", ColumnDescription = "计划完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlanCompleteDate { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_complete_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualCompleteDate { get; set; }

        /// <summary>
        /// 处理内容
        /// </summary>
        [SugarColumn(ColumnName = "process_content", ColumnDescription = "处理内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessContent { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [SugarColumn(ColumnName = "process_result", ColumnDescription = "处理结果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessResult { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCode { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        [SugarColumn(ColumnName = "document_name", ColumnDescription = "文档名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentName { get; set; }

        /// <summary>
        /// 文档类型(1=图片 2=文档 3=视频 4=音频 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档类型名称
        /// </summary>
        [SugarColumn(ColumnName = "document_type_name", ColumnDescription = "文档类型名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentTypeName { get; set; }

        /// <summary>
        /// 文档路径
        /// </summary>
        [SugarColumn(ColumnName = "document_path", ColumnDescription = "文档路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentPath { get; set; }

        /// <summary>
        /// 文档大小(KB)
        /// </summary>
        [SugarColumn(ColumnName = "document_size", ColumnDescription = "文档大小", ColumnDataType = "bigint", IsNullable = true)]
        public long? DocumentSize { get; set; }

        /// <summary>
        /// 文档链接
        /// </summary>
        [SugarColumn(ColumnName = "document_url", ColumnDescription = "文档链接", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentUrl { get; set; }

        /// <summary>
        /// 处理费用
        /// </summary>
        [SugarColumn(ColumnName = "process_cost", ColumnDescription = "处理费用", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessCost { get; set; } = 0;

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 是否关键步骤
        /// </summary>
        [SugarColumn(ColumnName = "is_key_step", ColumnDescription = "是否关键步骤", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsKeyStep { get; set; } = 0;

        /// <summary>
        /// 是否必须完成
        /// </summary>
        [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必须完成", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsRequired { get; set; } = 1;

        /// <summary>
        /// 前置步骤ID
        /// </summary>
        [SugarColumn(ColumnName = "predecessor_id", ColumnDescription = "前置步骤ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? PredecessorId { get; set; }

        /// <summary>
        /// 后续步骤ID
        /// </summary>
        [SugarColumn(ColumnName = "successor_id", ColumnDescription = "后续步骤ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? SuccessorId { get; set; }

        /// <summary>
        /// 处理时长(小时)
        /// </summary>
        [SugarColumn(ColumnName = "process_hours", ColumnDescription = "处理时长", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessHours { get; set; } = 0;

        /// <summary>
        /// 处理进度(%)
        /// </summary>
        [SugarColumn(ColumnName = "progress_percent", ColumnDescription = "处理进度", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProgressPercent { get; set; } = 0;


        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



