//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktContractDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 合同数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Contract
{
    /// <summary>
    /// 合同基础DTO
    /// </summary>
    public class TaktContractDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractDto()
        {
            CompanyCode = string.Empty;
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractDescription = string.Empty;
            ContractContent = string.Empty;
            ContractTemplateCode = string.Empty;
            ContractTemplateName = string.Empty;
            ContractVersion = "1.0";
            RevisionVersion = string.Empty;
            Currency = "CNY";
            ContractParty = string.Empty;
            ContractPartyContact = string.Empty;
            ContractPartyPhone = string.Empty;
            ContractPartyEmail = string.Empty;
            ContractPartyAddress = string.Empty;
            OurContact = string.Empty;
            OurPhone = string.Empty;
            OurEmail = string.Empty;
            ContractTerms = string.Empty;
            PaymentTerms = string.Empty;
            DeliveryTerms = string.Empty;
            BreachLiability = string.Empty;
            DisputeResolution = string.Empty;
            DraftPerson = string.Empty;
            Reviewer = string.Empty;
            ReviewComment = string.Empty;
            Approver = string.Empty;
            ApprovalComment = string.Empty;
            Signer = string.Empty;
            ExecutionStatus = string.Empty;
            TerminationReason = string.Empty;
            RelatedProject = string.Empty;
            RelatedFiles = string.Empty;
            ContractRemark = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同描述
        /// </summary>
        public string? ContractDescription { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        public string? ContractContent { get; set; }

        /// <summary>
        /// 合同模板编号
        /// </summary>
        public string? ContractTemplateCode { get; set; }

        /// <summary>
        /// 合同模板名称
        /// </summary>
        public string? ContractTemplateName { get; set; }

        /// <summary>
        /// 合同版本
        /// </summary>
        public string ContractVersion { get; set; }

        /// <summary>
        /// 修订版本
        /// </summary>
        public string? RevisionVersion { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 合同期限(月)
        /// </summary>
        public int? ContractDuration { get; set; }

        /// <summary>
        /// 合同签署日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同方类型
        /// </summary>
        public int ContractPartyType { get; set; }

        /// <summary>
        /// 合同方联系人
        /// </summary>
        public string? ContractPartyContact { get; set; }

        /// <summary>
        /// 合同方联系电话
        /// </summary>
        public string? ContractPartyPhone { get; set; }

        /// <summary>
        /// 合同方联系邮箱
        /// </summary>
        public string? ContractPartyEmail { get; set; }

        /// <summary>
        /// 合同方地址
        /// </summary>
        public string? ContractPartyAddress { get; set; }

        /// <summary>
        /// 我方联系人
        /// </summary>
        public string? OurContact { get; set; }

        /// <summary>
        /// 我方联系电话
        /// </summary>
        public string? OurPhone { get; set; }

        /// <summary>
        /// 我方联系邮箱
        /// </summary>
        public string? OurEmail { get; set; }

        /// <summary>
        /// 合同条款
        /// </summary>
        public string? ContractTerms { get; set; }

        /// <summary>
        /// 付款条件
        /// </summary>
        public string? PaymentTerms { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public string? DeliveryTerms { get; set; }

        /// <summary>
        /// 违约责任
        /// </summary>
        public string? BreachLiability { get; set; }

        /// <summary>
        /// 争议解决
        /// </summary>
        public string? DisputeResolution { get; set; }

        /// <summary>
        /// 合同起草人
        /// </summary>
        public string? DraftPerson { get; set; }

        /// <summary>
        /// 合同起草日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 签署人
        /// </summary>
        public string? Signer { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 执行进度
        /// </summary>
        public decimal ExecutionProgress { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string? ExecutionStatus { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        public string? TerminationReason { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProject { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? ContractRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 合同查询DTO
    /// </summary>
    public class TaktContractQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractQueryDto()
        {
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractParty = string.Empty;
        }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string? ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string? ContractName { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int? ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int? ContractStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 合同创建DTO
    /// </summary>
    public class TaktContractCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractCreateDto()
        {
            CompanyCode = string.Empty;
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractDescription = string.Empty;
            ContractContent = string.Empty;
            ContractTemplateCode = string.Empty;
            ContractTemplateName = string.Empty;
            ContractVersion = "1.0";
            RevisionVersion = string.Empty;
            Currency = "CNY";
            ContractParty = string.Empty;
            ContractPartyContact = string.Empty;
            ContractPartyPhone = string.Empty;
            ContractPartyEmail = string.Empty;
            ContractPartyAddress = string.Empty;
            OurContact = string.Empty;
            OurPhone = string.Empty;
            OurEmail = string.Empty;
            ContractTerms = string.Empty;
            PaymentTerms = string.Empty;
            DeliveryTerms = string.Empty;
            BreachLiability = string.Empty;
            DisputeResolution = string.Empty;
            DraftPerson = string.Empty;
            Reviewer = string.Empty;
            ReviewComment = string.Empty;
            Approver = string.Empty;
            ApprovalComment = string.Empty;
            Signer = string.Empty;
            ExecutionStatus = string.Empty;
            TerminationReason = string.Empty;
            RelatedProject = string.Empty;
            RelatedFiles = string.Empty;
            ContractRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同描述
        /// </summary>
        public string? ContractDescription { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        public string? ContractContent { get; set; }

        /// <summary>
        /// 合同模板编号
        /// </summary>
        public string? ContractTemplateCode { get; set; }

        /// <summary>
        /// 合同模板名称
        /// </summary>
        public string? ContractTemplateName { get; set; }

        /// <summary>
        /// 合同版本
        /// </summary>
        public string ContractVersion { get; set; }

        /// <summary>
        /// 修订版本
        /// </summary>
        public string? RevisionVersion { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 合同期限(月)
        /// </summary>
        public int? ContractDuration { get; set; }

        /// <summary>
        /// 合同签署日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同方类型
        /// </summary>
        public int ContractPartyType { get; set; }

        /// <summary>
        /// 合同方联系人
        /// </summary>
        public string? ContractPartyContact { get; set; }

        /// <summary>
        /// 合同方联系电话
        /// </summary>
        public string? ContractPartyPhone { get; set; }

        /// <summary>
        /// 合同方联系邮箱
        /// </summary>
        public string? ContractPartyEmail { get; set; }

        /// <summary>
        /// 合同方地址
        /// </summary>
        public string? ContractPartyAddress { get; set; }

        /// <summary>
        /// 我方联系人
        /// </summary>
        public string? OurContact { get; set; }

        /// <summary>
        /// 我方联系电话
        /// </summary>
        public string? OurPhone { get; set; }

        /// <summary>
        /// 我方联系邮箱
        /// </summary>
        public string? OurEmail { get; set; }

        /// <summary>
        /// 合同条款
        /// </summary>
        public string? ContractTerms { get; set; }

        /// <summary>
        /// 付款条件
        /// </summary>
        public string? PaymentTerms { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public string? DeliveryTerms { get; set; }

        /// <summary>
        /// 违约责任
        /// </summary>
        public string? BreachLiability { get; set; }

        /// <summary>
        /// 争议解决
        /// </summary>
        public string? DisputeResolution { get; set; }

        /// <summary>
        /// 合同起草人
        /// </summary>
        public string? DraftPerson { get; set; }

        /// <summary>
        /// 合同起草日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 签署人
        /// </summary>
        public string? Signer { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 执行进度
        /// </summary>
        public decimal ExecutionProgress { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string? ExecutionStatus { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        public string? TerminationReason { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProject { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? ContractRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 合同更新DTO
    /// </summary>
    public class TaktContractUpdateDto : TaktContractCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ContractId { get; set; }
    }

    /// <summary>
    /// 合同删除DTO
    /// </summary>
    public class TaktContractDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }
    }

    /// <summary>
    /// 合同批量删除DTO
    /// </summary>
    public class TaktContractBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> ContractIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 合同导入DTO
    /// </summary>
    public class TaktContractImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractImportDto()
        {
            CompanyCode = string.Empty;
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractDescription = string.Empty;
            ContractContent = string.Empty;
            ContractTemplateCode = string.Empty;
            ContractTemplateName = string.Empty;
            ContractVersion = "1.0";
            RevisionVersion = string.Empty;
            Currency = "CNY";
            ContractParty = string.Empty;
            ContractPartyContact = string.Empty;
            ContractPartyPhone = string.Empty;
            ContractPartyEmail = string.Empty;
            ContractPartyAddress = string.Empty;
            OurContact = string.Empty;
            OurPhone = string.Empty;
            OurEmail = string.Empty;
            ContractTerms = string.Empty;
            PaymentTerms = string.Empty;
            DeliveryTerms = string.Empty;
            BreachLiability = string.Empty;
            DisputeResolution = string.Empty;
            DraftPerson = string.Empty;
            Reviewer = string.Empty;
            ReviewComment = string.Empty;
            Approver = string.Empty;
            ApprovalComment = string.Empty;
            Signer = string.Empty;
            ExecutionStatus = string.Empty;
            TerminationReason = string.Empty;
            RelatedProject = string.Empty;
            RelatedFiles = string.Empty;
            ContractRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同描述
        /// </summary>
        public string? ContractDescription { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        public string? ContractContent { get; set; }

        /// <summary>
        /// 合同模板编号
        /// </summary>
        public string? ContractTemplateCode { get; set; }

        /// <summary>
        /// 合同模板名称
        /// </summary>
        public string? ContractTemplateName { get; set; }

        /// <summary>
        /// 合同版本
        /// </summary>
        public string ContractVersion { get; set; }

        /// <summary>
        /// 修订版本
        /// </summary>
        public string? RevisionVersion { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 合同期限(月)
        /// </summary>
        public int? ContractDuration { get; set; }

        /// <summary>
        /// 合同签署日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同方类型
        /// </summary>
        public int ContractPartyType { get; set; }

        /// <summary>
        /// 合同方联系人
        /// </summary>
        public string? ContractPartyContact { get; set; }

        /// <summary>
        /// 合同方联系电话
        /// </summary>
        public string? ContractPartyPhone { get; set; }

        /// <summary>
        /// 合同方联系邮箱
        /// </summary>
        public string? ContractPartyEmail { get; set; }

        /// <summary>
        /// 合同方地址
        /// </summary>
        public string? ContractPartyAddress { get; set; }

        /// <summary>
        /// 我方联系人
        /// </summary>
        public string? OurContact { get; set; }

        /// <summary>
        /// 我方联系电话
        /// </summary>
        public string? OurPhone { get; set; }

        /// <summary>
        /// 我方联系邮箱
        /// </summary>
        public string? OurEmail { get; set; }

        /// <summary>
        /// 合同条款
        /// </summary>
        public string? ContractTerms { get; set; }

        /// <summary>
        /// 付款条件
        /// </summary>
        public string? PaymentTerms { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public string? DeliveryTerms { get; set; }

        /// <summary>
        /// 违约责任
        /// </summary>
        public string? BreachLiability { get; set; }

        /// <summary>
        /// 争议解决
        /// </summary>
        public string? DisputeResolution { get; set; }

        /// <summary>
        /// 合同起草人
        /// </summary>
        public string? DraftPerson { get; set; }

        /// <summary>
        /// 合同起草日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 签署人
        /// </summary>
        public string? Signer { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 执行进度
        /// </summary>
        public decimal ExecutionProgress { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string? ExecutionStatus { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        public string? TerminationReason { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProject { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? ContractRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 合同导出DTO
    /// </summary>
    public class TaktContractExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractExportDto()
        {
            CompanyCode = string.Empty;
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractDescription = string.Empty;
            ContractContent = string.Empty;
            ContractTemplateCode = string.Empty;
            ContractTemplateName = string.Empty;
            ContractVersion = "1.0";
            RevisionVersion = string.Empty;
            Currency = "CNY";
            ContractParty = string.Empty;
            ContractPartyContact = string.Empty;
            ContractPartyPhone = string.Empty;
            ContractPartyEmail = string.Empty;
            ContractPartyAddress = string.Empty;
            OurContact = string.Empty;
            OurPhone = string.Empty;
            OurEmail = string.Empty;
            ContractTerms = string.Empty;
            PaymentTerms = string.Empty;
            DeliveryTerms = string.Empty;
            BreachLiability = string.Empty;
            DisputeResolution = string.Empty;
            DraftPerson = string.Empty;
            Reviewer = string.Empty;
            ReviewComment = string.Empty;
            Approver = string.Empty;
            ApprovalComment = string.Empty;
            Signer = string.Empty;
            ExecutionStatus = string.Empty;
            TerminationReason = string.Empty;
            RelatedProject = string.Empty;
            RelatedFiles = string.Empty;
            ContractRemark = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同描述
        /// </summary>
        public string? ContractDescription { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        public string? ContractContent { get; set; }

        /// <summary>
        /// 合同模板编号
        /// </summary>
        public string? ContractTemplateCode { get; set; }

        /// <summary>
        /// 合同模板名称
        /// </summary>
        public string? ContractTemplateName { get; set; }

        /// <summary>
        /// 合同版本
        /// </summary>
        public string ContractVersion { get; set; }

        /// <summary>
        /// 修订版本
        /// </summary>
        public string? RevisionVersion { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 合同期限(月)
        /// </summary>
        public int? ContractDuration { get; set; }

        /// <summary>
        /// 合同签署日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同方类型
        /// </summary>
        public int ContractPartyType { get; set; }

        /// <summary>
        /// 合同方联系人
        /// </summary>
        public string? ContractPartyContact { get; set; }

        /// <summary>
        /// 合同方联系电话
        /// </summary>
        public string? ContractPartyPhone { get; set; }

        /// <summary>
        /// 合同方联系邮箱
        /// </summary>
        public string? ContractPartyEmail { get; set; }

        /// <summary>
        /// 合同方地址
        /// </summary>
        public string? ContractPartyAddress { get; set; }

        /// <summary>
        /// 我方联系人
        /// </summary>
        public string? OurContact { get; set; }

        /// <summary>
        /// 我方联系电话
        /// </summary>
        public string? OurPhone { get; set; }

        /// <summary>
        /// 我方联系邮箱
        /// </summary>
        public string? OurEmail { get; set; }

        /// <summary>
        /// 合同条款
        /// </summary>
        public string? ContractTerms { get; set; }

        /// <summary>
        /// 付款条件
        /// </summary>
        public string? PaymentTerms { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public string? DeliveryTerms { get; set; }

        /// <summary>
        /// 违约责任
        /// </summary>
        public string? BreachLiability { get; set; }

        /// <summary>
        /// 争议解决
        /// </summary>
        public string? DisputeResolution { get; set; }

        /// <summary>
        /// 合同起草人
        /// </summary>
        public string? DraftPerson { get; set; }

        /// <summary>
        /// 合同起草日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 签署人
        /// </summary>
        public string? Signer { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 执行进度
        /// </summary>
        public decimal ExecutionProgress { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string? ExecutionStatus { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        public string? TerminationReason { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProject { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? ContractRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 合同导入模板DTO
    /// </summary>
    public class TaktContractTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktContractTemplateDto()
        {
            CompanyCode = string.Empty;
            ContractCode = string.Empty;
            ContractName = string.Empty;
            ContractCategory = string.Empty;
            ContractDescription = string.Empty;
            ContractContent = string.Empty;
            ContractTemplateCode = string.Empty;
            ContractTemplateName = string.Empty;
            ContractVersion = "1.0";
            RevisionVersion = string.Empty;
            Currency = "CNY";
            ContractParty = string.Empty;
            ContractPartyContact = string.Empty;
            ContractPartyPhone = string.Empty;
            ContractPartyEmail = string.Empty;
            ContractPartyAddress = string.Empty;
            OurContact = string.Empty;
            OurPhone = string.Empty;
            OurEmail = string.Empty;
            ContractTerms = string.Empty;
            PaymentTerms = string.Empty;
            DeliveryTerms = string.Empty;
            BreachLiability = string.Empty;
            DisputeResolution = string.Empty;
            DraftPerson = string.Empty;
            Reviewer = string.Empty;
            ReviewComment = string.Empty;
            Approver = string.Empty;
            ApprovalComment = string.Empty;
            Signer = string.Empty;
            ExecutionStatus = string.Empty;
            TerminationReason = string.Empty;
            RelatedProject = string.Empty;
            RelatedFiles = string.Empty;
            ContractRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractCode { get; set; }

        /// <summary>
        /// 合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        /// 合同类型(1=采购合同 2=销售合同 3=服务合同 4=租赁合同 5=劳务合同 6=技术合同 7=其他合同)
        /// </summary>
        public int ContractType { get; set; }

        /// <summary>
        /// 合同分类
        /// </summary>
        public string? ContractCategory { get; set; }

        /// <summary>
        /// 合同描述
        /// </summary>
        public string? ContractDescription { get; set; }

        /// <summary>
        /// 合同内容
        /// </summary>
        public string? ContractContent { get; set; }

        /// <summary>
        /// 合同模板编号
        /// </summary>
        public string? ContractTemplateCode { get; set; }

        /// <summary>
        /// 合同模板名称
        /// </summary>
        public string? ContractTemplateName { get; set; }

        /// <summary>
        /// 合同版本
        /// </summary>
        public string ContractVersion { get; set; }

        /// <summary>
        /// 修订版本
        /// </summary>
        public string? RevisionVersion { get; set; }

        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal? ContractAmount { get; set; }

        /// <summary>
        /// 币种(CNY=人民币 USD=美元 EUR=欧元)
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? ContractStartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? ContractEndDate { get; set; }

        /// <summary>
        /// 合同期限(月)
        /// </summary>
        public int? ContractDuration { get; set; }

        /// <summary>
        /// 合同签署日期
        /// </summary>
        public DateTime? ContractSignDate { get; set; }

        /// <summary>
        /// 合同生效日期
        /// </summary>
        public DateTime? ContractEffectiveDate { get; set; }

        /// <summary>
        /// 合同到期日期
        /// </summary>
        public DateTime? ContractExpiryDate { get; set; }

        /// <summary>
        /// 合同方
        /// </summary>
        public string? ContractParty { get; set; }

        /// <summary>
        /// 合同方类型(1=客户 2=供应商 3=合作伙伴 4=其他)
        /// </summary>
        public int ContractPartyType { get; set; }

        /// <summary>
        /// 合同方联系人
        /// </summary>
        public string? ContractPartyContact { get; set; }

        /// <summary>
        /// 合同方联系电话
        /// </summary>
        public string? ContractPartyPhone { get; set; }

        /// <summary>
        /// 合同方联系邮箱
        /// </summary>
        public string? ContractPartyEmail { get; set; }

        /// <summary>
        /// 合同方地址
        /// </summary>
        public string? ContractPartyAddress { get; set; }

        /// <summary>
        /// 我方联系人
        /// </summary>
        public string? OurContact { get; set; }

        /// <summary>
        /// 我方联系电话
        /// </summary>
        public string? OurPhone { get; set; }

        /// <summary>
        /// 我方联系邮箱
        /// </summary>
        public string? OurEmail { get; set; }

        /// <summary>
        /// 合同条款
        /// </summary>
        public string? ContractTerms { get; set; }

        /// <summary>
        /// 付款条件
        /// </summary>
        public string? PaymentTerms { get; set; }

        /// <summary>
        /// 交付条件
        /// </summary>
        public string? DeliveryTerms { get; set; }

        /// <summary>
        /// 违约责任
        /// </summary>
        public string? BreachLiability { get; set; }

        /// <summary>
        /// 争议解决
        /// </summary>
        public string? DisputeResolution { get; set; }

        /// <summary>
        /// 合同起草人
        /// </summary>
        public string? DraftPerson { get; set; }

        /// <summary>
        /// 合同起草日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 签署人
        /// </summary>
        public string? Signer { get; set; }

        /// <summary>
        /// 签署日期
        /// </summary>
        public DateTime? SignDate { get; set; }

        /// <summary>
        /// 合同状态(0=草稿 1=审核中 2=已批准 3=已签署 4=执行中 5=已完成 6=已终止)
        /// </summary>
        public int ContractStatus { get; set; }

        /// <summary>
        /// 执行进度
        /// </summary>
        public decimal ExecutionProgress { get; set; }

        /// <summary>
        /// 执行情况
        /// </summary>
        public string? ExecutionStatus { get; set; }

        /// <summary>
        /// 终止原因
        /// </summary>
        public string? TerminationReason { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? TerminationDate { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProject { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? ContractRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }
} 


