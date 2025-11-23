#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktQualityBusiness.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 品质业务主表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 品质业务主表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 专门用于管理品质业务的基本信息，包括品质问题、处理方案、成本统计等
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business", "品质成本业务主表")]
    [SugarIndex("ix_quality_business_code", nameof(QualityBusinessCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_quality_business", nameof(CompanyCode), OrderByType.Asc, nameof(QualityBusinessCode), OrderByType.Asc, false)]
    [SugarIndex("ix_quality_business_status", nameof(QualityBusinessStatus), OrderByType.Asc, false)]
    public class TaktQualityBusiness : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>品质业务编号</summary>
        [SugarColumn(ColumnName = "quality_business_code", ColumnDescription = "品质业务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string QualityBusinessCode { get; set; } = string.Empty;

        /// <summary>品质业务类型(1=品质问题 2=客户投诉 3=供应商问题 4=内部问题 5=外部问题 6=其他)</summary>
        [SugarColumn(ColumnName = "quality_business_type", ColumnDescription = "品质业务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int QualityBusinessType { get; set; } = 1;

        /// <summary>品质业务标题</summary>
        [SugarColumn(ColumnName = "quality_business_title", ColumnDescription = "品质业务标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string QualityBusinessTitle { get; set; } = string.Empty;

        /// <summary>品质业务描述</summary>
        [SugarColumn(ColumnName = "quality_business_description", ColumnDescription = "品质业务描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityBusinessDescription { get; set; }

        /// <summary>产品编号</summary>
        [SugarColumn(ColumnName = "product_code", ColumnDescription = "产品编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductCode { get; set; }

        /// <summary>产品名称</summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>产品规格</summary>
        [SugarColumn(ColumnName = "product_specification", ColumnDescription = "产品规格", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductSpecification { get; set; }

        /// <summary>批次号</summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批次号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>生产订单编号</summary>
        [SugarColumn(ColumnName = "production_order_code", ColumnDescription = "生产订单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionOrderCode { get; set; }

        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerCode { get; set; }

        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierCode { get; set; }

        /// <summary>供应商名称</summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName { get; set; }

        /// <summary>问题发现日期</summary>
        [SugarColumn(ColumnName = "issue_discovery_date", ColumnDescription = "问题发现日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? IssueDiscoveryDate { get; set; }

        /// <summary>问题发现人</summary>
        [SugarColumn(ColumnName = "issue_discoverer", ColumnDescription = "问题发现人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueDiscoverer { get; set; }

        /// <summary>问题发现地点</summary>
        [SugarColumn(ColumnName = "issue_discovery_location", ColumnDescription = "问题发现地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueDiscoveryLocation { get; set; }

        /// <summary>问题严重程度(1=轻微 2=一般 3=严重 4=紧急)</summary>
        [SugarColumn(ColumnName = "issue_severity", ColumnDescription = "问题严重程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int IssueSeverity { get; set; } = 2;

        /// <summary>问题影响范围</summary>
        [SugarColumn(ColumnName = "issue_impact_scope", ColumnDescription = "问题影响范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueImpactScope { get; set; }

        /// <summary>问题原因分析</summary>
        [SugarColumn(ColumnName = "issue_root_cause", ColumnDescription = "问题原因分析", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueRootCause { get; set; }

        /// <summary>处理方案</summary>
        [SugarColumn(ColumnName = "solution_plan", ColumnDescription = "处理方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SolutionPlan { get; set; }

        /// <summary>预防措施</summary>
        [SugarColumn(ColumnName = "preventive_measures", ColumnDescription = "预防措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PreventiveMeasures { get; set; }

        /// <summary>负责人</summary>
        [SugarColumn(ColumnName = "responsible_person", ColumnDescription = "负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>负责人电话</summary>
        [SugarColumn(ColumnName = "responsible_person_phone", ColumnDescription = "负责人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePersonPhone { get; set; }

        /// <summary>负责人邮箱</summary>
        [SugarColumn(ColumnName = "responsible_person_email", ColumnDescription = "负责人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePersonEmail { get; set; }

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划完成日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际完成日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>品质业务状态(0=草稿 1=已提交 2=处理中 3=已完成 4=已关闭 5=已取消)</summary>
        [SugarColumn(ColumnName = "quality_business_status", ColumnDescription = "品质业务状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int QualityBusinessStatus { get; set; } = 0;

        /// <summary>审批状态(0=未审批 1=已审批 2=已拒绝)</summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; } = 0;

        /// <summary>审批人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>审批日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>审批意见</summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalRemarks { get; set; }

        /// <summary>处理结果</summary>
        [SugarColumn(ColumnName = "processing_result", ColumnDescription = "处理结果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessingResult { get; set; }

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>相关图片</summary>
        [SugarColumn(ColumnName = "related_images", ColumnDescription = "相关图片", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedImages { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "quality_business_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityBusinessRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        #region 导航属性

        /// <summary>IQC成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktQualityBusinessIqcCost.QualityBusinessId))]
        public List<TaktQualityBusinessIqcCost>? IqcCosts { get; set; }

        /// <summary>FQC成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktQualityBusinessFqcCost.QualityBusinessId))]
        public List<TaktQualityBusinessFqcCost>? FqcCosts { get; set; }

        /// <summary>返工调查列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktReworkInvestigation.QualityBusinessId))]
        public List<TaktReworkInvestigation>? ReworkInvestigations { get; set; }

        /// <summary>不良改修对应对策成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktReworkCountermeasure.QualityBusinessId))]
        public List<TaktReworkCountermeasure>? ReworkDefectRepairs { get; set; }

        /// <summary>PCBA成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktReworkPcbaCost.QualityBusinessId))]
        public List<TaktReworkPcbaCost>? ReworkPcbaCosts { get; set; }

        /// <summary>组立Assy成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktReworkAssemblyCost.QualityBusinessId))]
        public List<TaktReworkAssemblyCost>? ReworkAssemblyCosts { get; set; }

        /// <summary>废弃事故成本列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktScrapAccidentCost.QualityBusinessId))]
        public List<TaktScrapAccidentCost>? ScrapAccidentCosts { get; set; }

        #endregion
    }
} 



