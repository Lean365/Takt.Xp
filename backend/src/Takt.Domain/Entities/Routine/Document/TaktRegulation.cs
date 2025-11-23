#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRegulation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 内部规章制度实体类（树形结构）
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Document
{
    /// <summary>
    /// 内部规章制度实体类（树形结构）
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录公司内部规章制度文档，支持树形层级结构，包括管理制度、操作规程、工作标准等
    /// </remarks>
    [SugarTable("Takt_routine_document_regulation", "内部规章制度表")]
    [SugarIndex("ix_regulation_code", nameof(RegulationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_regulation_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_type", nameof(RegulationType), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_level", nameof(RegulationLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    public class TaktRegulation : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>父级ID</summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        [SugarColumn(ColumnName = "regulation_code", ColumnDescription = "规章制度编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        [SugarColumn(ColumnName = "regulation_title", ColumnDescription = "规章制度标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>规章制度类型(1=管理制度 2=操作规程 3=工作标准 4=岗位职责 5=考核制度 6=奖惩制度 7=其他)</summary>
        [SugarColumn(ColumnName = "regulation_type", ColumnDescription = "规章制度类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        [SugarColumn(ColumnName = "regulation_level", ColumnDescription = "规章制度级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        [SugarColumn(ColumnName = "regulation_description", ColumnDescription = "规章制度描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        [SugarColumn(ColumnName = "regulation_content", ColumnDescription = "规章制度完整内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        [SugarColumn(ColumnName = "regulation_pdf_path", ColumnDescription = "规章制度PDF文件路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        [SugarColumn(ColumnName = "regulation_version", ColumnDescription = "规章制度版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string RegulationVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        [SugarColumn(ColumnName = "revision_version", ColumnDescription = "修订版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        [SugarColumn(ColumnName = "publish_date", ColumnDescription = "发布日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        [SugarColumn(ColumnName = "publish_department", ColumnDescription = "发布部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        [SugarColumn(ColumnName = "publish_method", ColumnDescription = "发布方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        [SugarColumn(ColumnName = "draft_department", ColumnDescription = "制定部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "制定日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        [SugarColumn(ColumnName = "management_reviewer", ColumnDescription = "管理层审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        [SugarColumn(ColumnName = "management_review_date", ColumnDescription = "管理层审核日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        [SugarColumn(ColumnName = "management_review_comment", ColumnDescription = "管理层审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        [SugarColumn(ColumnName = "staff_vote_date", ColumnDescription = "职工代表大会表决日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        [SugarColumn(ColumnName = "staff_vote_result", ColumnDescription = "职工代表大会表决结果", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        [SugarColumn(ColumnName = "legal_reviewer", ColumnDescription = "法务合规审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        [SugarColumn(ColumnName = "legal_review_date", ColumnDescription = "法务合规审核日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        [SugarColumn(ColumnName = "legal_review_comment", ColumnDescription = "法务合规审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        [SugarColumn(ColumnName = "filing_department", ColumnDescription = "相关备案部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        [SugarColumn(ColumnName = "filing_date", ColumnDescription = "备案日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        [SugarColumn(ColumnName = "filing_number", ColumnDescription = "备案编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FilingNumber { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        [SugarColumn(ColumnName = "related_regulations", ColumnDescription = "相关规章制度", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedRegulations { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态。规章制度的状态（0=草稿，1=已发布，2=已作废，3=已归档）。</summary>
        [SugarColumn(ColumnName = "regulation_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
   

        /// <summary>
        /// 父级规章制度
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktRegulation? Parent { get; set; }

        /// <summary>
        /// 子级规章制度列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<TaktRegulation>? Children { get; set; }
    }
} 



