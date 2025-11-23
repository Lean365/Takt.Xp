using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 客诉处理主表实体类 (基于SAP QM质量管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP QM 质量管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_complaint_process", "客诉处理")]
    [SugarIndex("ix_process_code", nameof(ProcessCode), OrderByType.Asc, true)]
    [SugarIndex("ix_complaint_code", nameof(ComplaintCode), OrderByType.Asc, false)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktComplaintProcess : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

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
        /// 客诉主表关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ComplaintCode))]
        public TaktComplaint? Complaint { get; set; }

        /// <summary>
        /// 处理类型(1=内部处理 2=外部处理 3=联合处理 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "process_type", ColumnDescription = "处理类型", ColumnDataType = "int", IsNullable = false)]
        public int ProcessType { get; set; }

        /// <summary>
        /// 处理类型名称
        /// </summary>
        [SugarColumn(ColumnName = "process_type_name", ColumnDescription = "处理类型名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessTypeName { get; set; }

        /// <summary>
        /// 处理阶段(1=问题确认 2=原因分析 3=方案制定 4=方案执行 5=效果验证 6=总结关闭)
        /// </summary>
        [SugarColumn(ColumnName = "process_stage", ColumnDescription = "处理阶段", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProcessStage { get; set; } = 1;

        /// <summary>
        /// 处理阶段名称
        /// </summary>
        [SugarColumn(ColumnName = "process_stage_name", ColumnDescription = "处理阶段名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessStageName { get; set; }

        /// <summary>
        /// 处理状态(0=待处理 1=处理中 2=已完成 3=已关闭 4=已取消)
        /// </summary>
        [SugarColumn(ColumnName = "process_status", ColumnDescription = "处理状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProcessStatus { get; set; } = 0;

        /// <summary>
        /// 处理状态名称
        /// </summary>
        [SugarColumn(ColumnName = "process_status_name", ColumnDescription = "处理状态名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessStatusName { get; set; }

        /// <summary>
        /// 处理优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "处理优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 处理优先级名称
        /// </summary>
        [SugarColumn(ColumnName = "priority_name", ColumnDescription = "处理优先级名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriorityName { get; set; }

        /// <summary>
        /// 处理部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "处理部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 处理部门编码
        /// </summary>
        [SugarColumn(ColumnName = "dept_code", ColumnDescription = "处理部门编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptCode { get; set; }

        /// <summary>
        /// 处理部门名称
        /// </summary>
        [SugarColumn(ColumnName = "dept_name", ColumnDescription = "处理部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptName { get; set; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        [SugarColumn(ColumnName = "handler_id", ColumnDescription = "处理人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? HandlerId { get; set; }

        /// <summary>
        /// 处理人编码
        /// </summary>
        [SugarColumn(ColumnName = "handler_code", ColumnDescription = "处理人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HandlerCode { get; set; }

        /// <summary>
        /// 处理人姓名
        /// </summary>
        [SugarColumn(ColumnName = "handler_name", ColumnDescription = "处理人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? HandlerName { get; set; }

        /// <summary>
        /// 处理开始日期
        /// </summary>
        [SugarColumn(ColumnName = "process_start_date", ColumnDescription = "处理开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProcessStartDate { get; set; }

        /// <summary>
        /// 处理完成日期
        /// </summary>
        [SugarColumn(ColumnName = "process_complete_date", ColumnDescription = "处理完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProcessCompleteDate { get; set; }

        /// <summary>
        /// 处理截止日期
        /// </summary>
        [SugarColumn(ColumnName = "process_deadline_date", ColumnDescription = "处理截止日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProcessDeadlineDate { get; set; }

        /// <summary>
        /// 处理方案
        /// </summary>
        [SugarColumn(ColumnName = "solution", ColumnDescription = "处理方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Solution { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        [SugarColumn(ColumnName = "result", ColumnDescription = "处理结果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Result { get; set; }

        /// <summary>
        /// 处理结论(1=问题属实 2=问题不属实 3=部分属实 4=无法确认)
        /// </summary>
        [SugarColumn(ColumnName = "conclusion", ColumnDescription = "处理结论", ColumnDataType = "int", IsNullable = true)]
        public int? Conclusion { get; set; }

        /// <summary>
        /// 处理结论名称
        /// </summary>
        [SugarColumn(ColumnName = "conclusion_name", ColumnDescription = "处理结论名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConclusionName { get; set; }

        /// <summary>
        /// 责任方(1=我方 2=客户方 3=供应商 4=第三方 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "responsible_party", ColumnDescription = "责任方", ColumnDataType = "int", IsNullable = true)]
        public int? ResponsibleParty { get; set; }

        /// <summary>
        /// 责任方名称
        /// </summary>
        [SugarColumn(ColumnName = "responsible_party_name", ColumnDescription = "责任方名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePartyName { get; set; }

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
        /// 客户满意度(1=不满意 2=一般 3=满意 4=很满意)
        /// </summary>
        [SugarColumn(ColumnName = "satisfaction", ColumnDescription = "客户满意度", ColumnDataType = "int", IsNullable = true)]
        public int? Satisfaction { get; set; }

        /// <summary>
        /// 客户反馈
        /// </summary>
        [SugarColumn(ColumnName = "customer_feedback", ColumnDescription = "客户反馈", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerFeedback { get; set; }

        /// <summary>
        /// 是否紧急处理
        /// </summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急处理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>
        /// 是否升级处理
        /// </summary>
        [SugarColumn(ColumnName = "is_escalated", ColumnDescription = "是否升级处理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsEscalated { get; set; } = 0;

        /// <summary>
        /// 升级原因
        /// </summary>
        [SugarColumn(ColumnName = "escalation_reason", ColumnDescription = "升级原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EscalationReason { get; set; }


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

