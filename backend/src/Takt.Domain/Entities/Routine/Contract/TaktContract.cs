#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContract.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 合同实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Contract
{
    /// <summary>
    /// 合同实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录合同的相关信息，包括合同基本信息、条款、签署、执行等
    /// </remarks>
    [SugarTable("Takt_routine_contract", "合同表")]
    [SugarIndex("ix_contract_code", nameof(ContractCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract", nameof(CompanyCode), OrderByType.Asc, nameof(ContractCode), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_status", nameof(ContractStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_type", nameof(ContractType), OrderByType.Asc, false)]
    [SugarIndex("ix_contract_party", nameof(ContractParty), OrderByType.Asc, false)]
    public class TaktContract : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同编号</summary>
        [SugarColumn(ColumnName = "contract_code", ColumnDescription = "合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>合同名称</summary>
        [SugarColumn(ColumnName = "contract_name", ColumnDescription = "合同名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractName { get; set; } = string.Empty;

        /// <summary>合同类型(1=采购合同 2=销售合同 3=服务合同 4=租赁合同 5=劳务合同 6=技术合同 7=其他合同)</summary>
        [SugarColumn(ColumnName = "contract_type", ColumnDescription = "合同类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractType { get; set; } = 1;

        /// <summary>合同分类</summary>
        [SugarColumn(ColumnName = "contract_category", ColumnDescription = "合同分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractCategory { get; set; }

        /// <summary>合同描述</summary>
        [SugarColumn(ColumnName = "contract_description", ColumnDescription = "合同描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractDescription { get; set; }

        /// <summary>合同内容</summary>
        [SugarColumn(ColumnName = "contract_content", ColumnDescription = "合同内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractContent { get; set; }

        /// <summary>合同模板编号</summary>
        [SugarColumn(ColumnName = "contract_template_code", ColumnDescription = "合同模板编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTemplateCode { get; set; }

        /// <summary>合同版本</summary>
        [SugarColumn(ColumnName = "contract_version", ColumnDescription = "合同版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string ContractVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        [SugarColumn(ColumnName = "revision_version", ColumnDescription = "修订版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RevisionVersion { get; set; }

        /// <summary>合同金额</summary>
        [SugarColumn(ColumnName = "contract_amount", ColumnDescription = "合同金额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ContractAmount { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>合同开始日期</summary>
        [SugarColumn(ColumnName = "contract_start_date", ColumnDescription = "合同开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractStartDate { get; set; }

        /// <summary>合同结束日期</summary>
        [SugarColumn(ColumnName = "contract_end_date", ColumnDescription = "合同结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractEndDate { get; set; }

        /// <summary>合同期限(月)</summary>
        [SugarColumn(ColumnName = "contract_duration", ColumnDescription = "合同期限(月)", ColumnDataType = "int", IsNullable = true)]
        public int? ContractDuration { get; set; }

        /// <summary>合同签署日期</summary>
        [SugarColumn(ColumnName = "contract_sign_date", ColumnDescription = "合同签署日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractSignDate { get; set; }

        /// <summary>合同生效日期</summary>
        [SugarColumn(ColumnName = "contract_effective_date", ColumnDescription = "合同生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>合同到期日期</summary>
        [SugarColumn(ColumnName = "contract_expiry_date", ColumnDescription = "合同到期日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>合同方</summary>
        [SugarColumn(ColumnName = "contract_party", ColumnDescription = "合同方", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractParty { get; set; }

        /// <summary>合同方类型(1=客户 2=供应商 3=合作伙伴 4=其他)</summary>
        [SugarColumn(ColumnName = "contract_party_type", ColumnDescription = "合同方类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ContractPartyType { get; set; } = 1;

        /// <summary>合同方联系人</summary>
        [SugarColumn(ColumnName = "contract_party_contact", ColumnDescription = "合同方联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPartyContact { get; set; }

        /// <summary>合同方联系电话</summary>
        [SugarColumn(ColumnName = "contract_party_phone", ColumnDescription = "合同方联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPartyPhone { get; set; }

        /// <summary>合同方联系邮箱</summary>
        [SugarColumn(ColumnName = "contract_party_email", ColumnDescription = "合同方联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPartyEmail { get; set; }

        /// <summary>合同方地址</summary>
        [SugarColumn(ColumnName = "contract_party_address", ColumnDescription = "合同方地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractPartyAddress { get; set; }

        /// <summary>我方联系人</summary>
        [SugarColumn(ColumnName = "our_contact", ColumnDescription = "我方联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OurContact { get; set; }

        /// <summary>我方联系电话</summary>
        [SugarColumn(ColumnName = "our_phone", ColumnDescription = "我方联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OurPhone { get; set; }

        /// <summary>我方联系邮箱</summary>
        [SugarColumn(ColumnName = "our_email", ColumnDescription = "我方联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OurEmail { get; set; }

        /// <summary>合同条款</summary>
        [SugarColumn(ColumnName = "contract_terms", ColumnDescription = "合同条款", Length = 4000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContractTerms { get; set; }

        /// <summary>付款条件</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentTerms { get; set; }

        /// <summary>交付条件</summary>
        [SugarColumn(ColumnName = "delivery_terms", ColumnDescription = "交付条件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryTerms { get; set; }

        /// <summary>违约责任</summary>
        [SugarColumn(ColumnName = "breach_liability", ColumnDescription = "违约责任", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BreachLiability { get; set; }

        /// <summary>争议解决</summary>
        [SugarColumn(ColumnName = "dispute_resolution", ColumnDescription = "争议解决", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DisputeResolution { get; set; }

        /// <summary>合同起草人</summary>
        [SugarColumn(ColumnName = "draft_person", ColumnDescription = "合同起草人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftPerson { get; set; }

        /// <summary>合同起草日期</summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "合同起草日期", ColumnDataType = "datetime", IsNullable = true)]
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

        /// <summary>签署人</summary>
        [SugarColumn(ColumnName = "signer", ColumnDescription = "签署人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Signer { get; set; }

        /// <summary>签署日期</summary>
        [SugarColumn(ColumnName = "sign_date", ColumnDescription = "签署日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? SignDate { get; set; }

        /// <summary>合同状态(0=草稿 1=待审核 2=已审核 3=已批准 4=已签署 5=执行中 6=已完成 7=已终止 8=已作废)</summary>
        [SugarColumn(ColumnName = "contract_status", ColumnDescription = "合同状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContractStatus { get; set; } = 0;

        /// <summary>执行进度</summary>
        [SugarColumn(ColumnName = "execution_progress", ColumnDescription = "执行进度", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ExecutionProgress { get; set; } = 0;

        /// <summary>执行情况</summary>
        [SugarColumn(ColumnName = "execution_status", ColumnDescription = "执行情况", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecutionStatus { get; set; }

        /// <summary>终止原因</summary>
        [SugarColumn(ColumnName = "termination_reason", ColumnDescription = "终止原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TerminationReason { get; set; }

        /// <summary>终止日期</summary>
        [SugarColumn(ColumnName = "termination_date", ColumnDescription = "终止日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? TerminationDate { get; set; }

        /// <summary>相关项目</summary>
        [SugarColumn(ColumnName = "related_project", ColumnDescription = "相关项目", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProject { get; set; }

        /// <summary>相关文件</summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }


        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



