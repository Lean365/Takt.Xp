#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractPerformance.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 合同履约实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Contract
{
    /// <summary>
    /// 合同履约实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录合同履约的相关信息，包括履约信息、履约进度、履约评价等
    /// </remarks>
    [SugarTable("Takt_routine_contract_performance", "合同履约表")]
    [SugarIndex("ix_contract_performance_code", nameof(ContractPerformanceCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract_performance", nameof(CompanyCode), OrderByType.Asc, nameof(ContractPerformanceCode), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_performance_status", nameof(ContractPerformanceStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_performance_type", nameof(ContractPerformanceType), OrderByType.Asc, false)]
    public class TaktContractPerformance : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同履约编号</summary>
        [SugarColumn(ColumnName = "contract_performance_code", ColumnDescription = "合同履约编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractPerformanceCode { get; set; } = string.Empty;

        /// <summary>履约类型(1=我方履约 2=对方履约 3=双方履约)</summary>
        [SugarColumn(ColumnName = "contract_performance_type", ColumnDescription = "履约类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractPerformanceType { get; set; } = 1;

        /// <summary>履约标题</summary>
        [SugarColumn(ColumnName = "performance_title", ColumnDescription = "履约标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PerformanceTitle { get; set; } = string.Empty;

        /// <summary>履约描述</summary>
        [SugarColumn(ColumnName = "performance_description", ColumnDescription = "履约描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceDescription { get; set; }

        /// <summary>关联合同编号</summary>
        [SugarColumn(ColumnName = "related_contract_code", ColumnDescription = "关联合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedContractCode { get; set; } = string.Empty;

        /// <summary>关联合同名称</summary>
        [SugarColumn(ColumnName = "related_contract_name", ColumnDescription = "关联合同名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedContractName { get; set; } = string.Empty;

        /// <summary>合同方</summary>
        [SugarColumn(ColumnName = "contract_party", ColumnDescription = "合同方", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractParty { get; set; }

        /// <summary>合同方类型(1=客户 2=供应商 3=合作伙伴 4=其他)</summary>
        [SugarColumn(ColumnName = "contract_party_type", ColumnDescription = "合同方类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractPartyType { get; set; } = 1;

        /// <summary>履约内容</summary>
        [SugarColumn(ColumnName = "performance_content", ColumnDescription = "履约内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceContent { get; set; }

        /// <summary>履约标准</summary>
        [SugarColumn(ColumnName = "performance_standards", ColumnDescription = "履约标准", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceStandards { get; set; }

        /// <summary>履约要求</summary>
        [SugarColumn(ColumnName = "performance_requirements", ColumnDescription = "履约要求", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceRequirements { get; set; }

        /// <summary>履约开始日期</summary>
        [SugarColumn(ColumnName = "performance_start_date", ColumnDescription = "履约开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PerformanceStartDate { get; set; }

        /// <summary>履约结束日期</summary>
        [SugarColumn(ColumnName = "performance_end_date", ColumnDescription = "履约结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PerformanceEndDate { get; set; }

        /// <summary>履约期限(天)</summary>
        [SugarColumn(ColumnName = "performance_duration", ColumnDescription = "履约期限(天)", ColumnDataType = "int", IsNullable = true)]
        public int? PerformanceDuration { get; set; }

        /// <summary>履约进度(%)</summary>
        [SugarColumn(ColumnName = "performance_progress", ColumnDescription = "履约进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal PerformanceProgress { get; set; } = 0;

        /// <summary>履约状态(0=未开始 1=进行中 2=已完成 3=已延期 4=已暂停 5=已终止)</summary>
        [SugarColumn(ColumnName = "performance_status", ColumnDescription = "履约状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PerformanceStatus { get; set; } = 0;

        /// <summary>履约质量(1=优秀 2=良好 3=合格 4=不合格)</summary>
        [SugarColumn(ColumnName = "performance_quality", ColumnDescription = "履约质量", ColumnDataType = "int", IsNullable = true)]
        public int? PerformanceQuality { get; set; }

        /// <summary>履约评价</summary>
        [SugarColumn(ColumnName = "performance_evaluation", ColumnDescription = "履约评价", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceEvaluation { get; set; }

        /// <summary>履约评分</summary>
        [SugarColumn(ColumnName = "performance_score", ColumnDescription = "履约评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? PerformanceScore { get; set; }

        /// <summary>履约问题</summary>
        [SugarColumn(ColumnName = "performance_issues", ColumnDescription = "履约问题", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceIssues { get; set; }

        /// <summary>解决方案</summary>
        [SugarColumn(ColumnName = "solutions", ColumnDescription = "解决方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Solutions { get; set; }

        /// <summary>履约风险</summary>
        [SugarColumn(ColumnName = "performance_risks", ColumnDescription = "履约风险", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceRisks { get; set; }

        /// <summary>风险等级(1=低 2=中 3=高 4=极高)</summary>
        [SugarColumn(ColumnName = "risk_level", ColumnDescription = "风险等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RiskLevel { get; set; } = 1;

        /// <summary>风险应对措施</summary>
        [SugarColumn(ColumnName = "risk_response_measures", ColumnDescription = "风险应对措施", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RiskResponseMeasures { get; set; }

        /// <summary>履约部门</summary>
        [SugarColumn(ColumnName = "performance_department", ColumnDescription = "履约部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceDepartment { get; set; }

        /// <summary>履约负责人</summary>
        [SugarColumn(ColumnName = "performance_manager", ColumnDescription = "履约负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceManager { get; set; }

        /// <summary>履约负责人电话</summary>
        [SugarColumn(ColumnName = "performance_manager_phone", ColumnDescription = "履约负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceManagerPhone { get; set; }

        /// <summary>履约负责人邮箱</summary>
        [SugarColumn(ColumnName = "performance_manager_email", ColumnDescription = "履约负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceManagerEmail { get; set; }

        /// <summary>履约团队</summary>
        [SugarColumn(ColumnName = "performance_team", ColumnDescription = "履约团队", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceTeam { get; set; }

        /// <summary>履约开始时间</summary>
        [SugarColumn(ColumnName = "performance_start_time", ColumnDescription = "履约开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PerformanceStartTime { get; set; }

        /// <summary>履约完成时间</summary>
        [SugarColumn(ColumnName = "performance_completion_time", ColumnDescription = "履约完成时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PerformanceCompletionTime { get; set; }

        /// <summary>延期原因</summary>
        [SugarColumn(ColumnName = "delay_reason", ColumnDescription = "延期原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DelayReason { get; set; }

        /// <summary>延期天数</summary>
        [SugarColumn(ColumnName = "delay_days", ColumnDescription = "延期天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DelayDays { get; set; } = 0;

        /// <summary>暂停原因</summary>
        [SugarColumn(ColumnName = "pause_reason", ColumnDescription = "暂停原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PauseReason { get; set; }

        /// <summary>暂停开始时间</summary>
        [SugarColumn(ColumnName = "pause_start_time", ColumnDescription = "暂停开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PauseStartTime { get; set; }

        /// <summary>暂停结束时间</summary>
        [SugarColumn(ColumnName = "pause_end_time", ColumnDescription = "暂停结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PauseEndTime { get; set; }

        /// <summary>终止原因</summary>
        [SugarColumn(ColumnName = "termination_reason", ColumnDescription = "终止原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TerminationReason { get; set; }

        /// <summary>终止时间</summary>
        [SugarColumn(ColumnName = "termination_time", ColumnDescription = "终止时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? TerminationTime { get; set; }

        /// <summary>验收日期</summary>
        [SugarColumn(ColumnName = "acceptance_date", ColumnDescription = "验收日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? AcceptanceDate { get; set; }

        /// <summary>验收结果(1=通过 2=有条件通过 3=不通过)</summary>
        [SugarColumn(ColumnName = "acceptance_result", ColumnDescription = "验收结果", ColumnDataType = "int", IsNullable = true)]
        public int? AcceptanceResult { get; set; }

        /// <summary>验收意见</summary>
        [SugarColumn(ColumnName = "acceptance_comment", ColumnDescription = "验收意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptanceComment { get; set; }

        /// <summary>验收人</summary>
        [SugarColumn(ColumnName = "acceptance_person", ColumnDescription = "验收人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptancePerson { get; set; }

        /// <summary>合同履约状态(0=草稿 1=进行中 2=已完成 3=已验收 4=已终止)</summary>
        [SugarColumn(ColumnName = "contract_performance_status", ColumnDescription = "合同履约状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContractPerformanceStatus { get; set; } = 0;

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "contract_performance_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPerformanceRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



