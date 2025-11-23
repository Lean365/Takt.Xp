#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractTemplate.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 合同模板实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Contract
{
    /// <summary>
    /// 合同模板实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录合同模板的相关信息，包括模板基本信息、内容、分类、版本管理等
    /// </remarks>
    [SugarTable("Takt_routine_contract_template", "合同模板表")]
    [SugarIndex("ix_contract_template_code", nameof(ContractTemplateCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract_template", nameof(CompanyCode), OrderByType.Asc, nameof(ContractTemplateCode), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_template_status", nameof(ContractTemplateStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_template_type", nameof(ContractTemplateType), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_template_category", nameof(ContractTemplateCategory), OrderByType.Asc, false)]
    public class TaktContractTemplate : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同模板编号</summary>
        [SugarColumn(ColumnName = "contract_template_code", ColumnDescription = "合同模板编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractTemplateCode { get; set; } = string.Empty;

        /// <summary>合同模板名称</summary>
        [SugarColumn(ColumnName = "contract_template_name", ColumnDescription = "合同模板名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractTemplateName { get; set; } = string.Empty;

        /// <summary>合同模板类型(1=采购合同 2=销售合同 3=服务合同 4=租赁合同 5=劳务合同 6=技术合同 7=其他合同)</summary>
        [SugarColumn(ColumnName = "contract_template_type", ColumnDescription = "合同模板类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractTemplateType { get; set; } = 1;

        /// <summary>合同模板分类</summary>
        [SugarColumn(ColumnName = "contract_template_category", ColumnDescription = "合同模板分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTemplateCategory { get; set; }

        /// <summary>合同模板描述</summary>
        [SugarColumn(ColumnName = "contract_template_description", ColumnDescription = "合同模板描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTemplateDescription { get; set; }

        /// <summary>合同模板内容</summary>
        [SugarColumn(ColumnName = "contract_template_content", ColumnDescription = "合同模板内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTemplateContent { get; set; }

        /// <summary>模板版本</summary>
        [SugarColumn(ColumnName = "template_version", ColumnDescription = "模板版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string TemplateVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        [SugarColumn(ColumnName = "revision_version", ColumnDescription = "修订版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>模板级别(1=公司级 2=部门级 3=岗位级)</summary>
        [SugarColumn(ColumnName = "template_level", ColumnDescription = "模板级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TemplateLevel { get; set; } = 1;

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>是否默认模板(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_default_template", ColumnDescription = "是否默认模板", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDefaultTemplate { get; set; } = 0;

        /// <summary>模板制定人</summary>
        [SugarColumn(ColumnName = "draft_person", ColumnDescription = "模板制定人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftPerson { get; set; }

        /// <summary>模板制定日期</summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "模板制定日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DraftDate { get; set; }

        /// <summary>审核人</summary>
        [SugarColumn(ColumnName = "reviewer", ColumnDescription = "审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Reviewer { get; set; }

        /// <summary>审核日期</summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>审核意见</summary>
        [SugarColumn(ColumnName = "review_comment", ColumnDescription = "审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReviewComment { get; set; }

        /// <summary>批准人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "批准人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>批准日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "批准日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>批准意见</summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "批准意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalComment { get; set; }

        /// <summary>发布人</summary>
        [SugarColumn(ColumnName = "publisher", ColumnDescription = "发布人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Publisher { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        [SugarColumn(ColumnName = "publish_method", ColumnDescription = "发布方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        [SugarColumn(ColumnName = "publish_scope", ColumnDescription = "发布范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PublishScope { get; set; }

        /// <summary>相关模板</summary>
        [SugarColumn(ColumnName = "related_templates", ColumnDescription = "相关模板", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTemplates { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>使用次数</summary>
        [SugarColumn(ColumnName = "usage_count", ColumnDescription = "使用次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UsageCount { get; set; } = 0;

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>合同模板状态(0=草稿 1=待审核 2=已审核 3=已发布 4=已作废 5=已归档)</summary>
        [SugarColumn(ColumnName = "contract_template_status", ColumnDescription = "合同模板状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContractTemplateStatus { get; set; } = 0;

        /// <summary>是否置顶(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_top", ColumnDescription = "是否置顶", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsTop { get; set; } = 0;

        /// <summary>是否推荐(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_recommended", ColumnDescription = "是否推荐", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRecommended { get; set; } = 0;

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "contract_template_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTemplateRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



