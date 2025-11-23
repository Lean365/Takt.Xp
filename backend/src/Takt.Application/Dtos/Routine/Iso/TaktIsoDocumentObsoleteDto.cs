//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentObsoleteDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档作废数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文档作废基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的作废和回收情况，包括作废原因、回收状态、处理方式等
    /// 功能特点:
    /// 1. 作废流程管理 - 记录文档的作废过程
    /// 2. 申请信息 - 包含作废申请的相关字段（申请编号、申请人、申请原因等）
    /// 3. 审批信息 - 包含审批流程的相关字段（审批结果、审批意见等）
    /// 4. 回收管理 - 记录文档回收、销毁、归档等后续处理
    /// 注意: 申请和审批字段在同一表中，但通过注释明确区分
    /// </remarks>
    public class TaktIsoDocumentObsoleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentObsoleteDto()
        {
            DocumentVersion = string.Empty;
            ObsoleteReason = string.Empty;
            ObsoleteBy = string.Empty;
            ApplicationCode = string.Empty;
            ApplicantBy = string.Empty;
            ApplicationReason = string.Empty;
        }

        /// <summary>
        /// 作废记录ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long ObsoleteId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 作废类型。0=自然作废，1=强制作废，2=替代作废，3=错误作废
        /// </summary>
        public int ObsoleteType { get; set; }

        /// <summary>
        /// 作废原因
        /// </summary>
        public string ObsoleteReason { get; set; } = string.Empty;

        /// <summary>
        /// 作废日期
        /// </summary>
        public DateTime ObsoleteDate { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        public string ObsoleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 替代文档ID
        /// </summary>
        public long? ReplacementDocumentId { get; set; }

        /// <summary>
        /// 替代文档版本号
        /// </summary>
        public string? ReplacementDocumentVersion { get; set; }

        /// <summary>
        /// 状态。0=已作废，1=待回收，2=已回收，3=已销毁，4=已归档
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 回收日期
        /// </summary>
        public DateTime? RecycleDate { get; set; }

        /// <summary>
        /// 回收人
        /// </summary>
        public string? RecycleBy { get; set; }

        /// <summary>
        /// 回收方式。0=电子回收，1=纸质回收，2=混合回收
        /// </summary>
        public int? RecycleMethod { get; set; }

        /// <summary>
        /// 回收说明
        /// </summary>
        public string? RecycleNote { get; set; }

        /// <summary>
        /// 销毁日期
        /// </summary>
        public DateTime? DestroyDate { get; set; }

        /// <summary>
        /// 销毁人
        /// </summary>
        public string? DestroyBy { get; set; }

        /// <summary>
        /// 销毁方式。0=电子销毁，1=物理销毁，2=混合销毁
        /// </summary>
        public int? DestroyMethod { get; set; }

        /// <summary>
        /// 销毁说明
        /// </summary>
        public string? DestroyNote { get; set; }

        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? ArchiveDate { get; set; }

        /// <summary>
        /// 归档人
        /// </summary>
        public string? ArchiveBy { get; set; }

        /// <summary>
        /// 归档位置
        /// </summary>
        public string? ArchiveLocation { get; set; }

        /// <summary>
        /// 归档说明
        /// </summary>
        public string? ArchiveNote { get; set; }

        /// <summary>
        /// 是否通知相关人员
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }

        // ==================== 申请相关字段 ====================

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantBy { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门
        /// </summary>
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 申请状态。0=待审批，1=审批中，2=已批准，3=已拒绝，4=已撤回
        /// </summary>
        public int ApplicationStatus { get; set; }

        // ==================== 审批相关字段 ====================

        /// <summary>
        /// 审批开始日期
        /// </summary>
        public DateTime? ApprovalStartDate { get; set; }

        /// <summary>
        /// 审批完成日期
        /// </summary>
        public DateTime? ApprovalCompleteDate { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=拒绝
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 申请附件
        /// </summary>
        public string? ApplicationAttachment { get; set; }

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
        /// 是否删除（0未删除 1已删除）
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

        /// <summary>
        /// 关联的ISO文档
        /// </summary>
        public TaktIsoDocumentDto? Document { get; set; }

        /// <summary>
        /// 审批记录列表
        /// </summary>
        public List<TaktIsoDocumentApprovalDto>? ApprovalRecords { get; set; }
    }

    /// <summary>
    /// ISO文档作废查询DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentObsoleteQueryDto()
        {
            ObsoleteBy = string.Empty;
            ApplicationCode = string.Empty;
            ApplicantBy = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long? DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 作废类型
        /// </summary>
        public int? ObsoleteType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        public string? ObsoleteBy { get; set; }

        /// <summary>
        /// 替代文档ID
        /// </summary>
        public long? ReplacementDocumentId { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string? ApplicationCode { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string? ApplicantBy { get; set; }

        /// <summary>
        /// 申请人部门
        /// </summary>
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public int? ApplicationStatus { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        public int? ApprovalResult { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 是否通知相关人员
        /// </summary>
        public bool? IsNotified { get; set; }

        /// <summary>
        /// 开始作废日期
        /// </summary>
        public DateTime? StartObsoleteDate { get; set; }

        /// <summary>
        /// 结束作废日期
        /// </summary>
        public DateTime? EndObsoleteDate { get; set; }

        /// <summary>
        /// 开始申请日期
        /// </summary>
        public DateTime? StartApplicationDate { get; set; }

        /// <summary>
        /// 结束申请日期
        /// </summary>
        public DateTime? EndApplicationDate { get; set; }

        /// <summary>
        /// 开始审批日期
        /// </summary>
        public DateTime? StartApprovalDate { get; set; }

        /// <summary>
        /// 结束审批日期
        /// </summary>
        public DateTime? EndApprovalDate { get; set; }
    }

    /// <summary>
    /// ISO文档作废创建DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentObsoleteCreateDto()
        {
            DocumentVersion = string.Empty;
            ObsoleteReason = string.Empty;
            ObsoleteBy = string.Empty;
            ApplicationCode = string.Empty;
            ApplicantBy = string.Empty;
            ApplicationReason = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 作废类型。0=自然作废，1=强制作废，2=替代作废，3=错误作废
        /// </summary>
        public int ObsoleteType { get; set; }

        /// <summary>
        /// 作废原因
        /// </summary>
        public string ObsoleteReason { get; set; } = string.Empty;

        /// <summary>
        /// 作废人
        /// </summary>
        public string ObsoleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 替代文档ID
        /// </summary>
        public long? ReplacementDocumentId { get; set; }

        /// <summary>
        /// 替代文档版本号
        /// </summary>
        public string? ReplacementDocumentVersion { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantBy { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门
        /// </summary>
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 申请附件
        /// </summary>
        public string? ApplicationAttachment { get; set; }
    }

    /// <summary>
    /// ISO文档作废更新DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteUpdateDto : TaktIsoDocumentObsoleteCreateDto
    {
        /// <summary>
        /// 作废记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ObsoleteId { get; set; }

        /// <summary>
        /// 状态。0=已作废，1=待回收，2=已回收，3=已销毁，4=已归档
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 申请状态。0=待审批，1=审批中，2=已批准，3=已拒绝，4=已撤回
        /// </summary>
        public int ApplicationStatus { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=拒绝
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 审批开始日期
        /// </summary>
        public DateTime? ApprovalStartDate { get; set; }

        /// <summary>
        /// 审批完成日期
        /// </summary>
        public DateTime? ApprovalCompleteDate { get; set; }

        /// <summary>
        /// 回收日期
        /// </summary>
        public DateTime? RecycleDate { get; set; }

        /// <summary>
        /// 回收人
        /// </summary>
        public string? RecycleBy { get; set; }

        /// <summary>
        /// 回收方式。0=电子回收，1=纸质回收，2=混合回收
        /// </summary>
        public int? RecycleMethod { get; set; }

        /// <summary>
        /// 回收说明
        /// </summary>
        public string? RecycleNote { get; set; }

        /// <summary>
        /// 销毁日期
        /// </summary>
        public DateTime? DestroyDate { get; set; }

        /// <summary>
        /// 销毁人
        /// </summary>
        public string? DestroyBy { get; set; }

        /// <summary>
        /// 销毁方式。0=电子销毁，1=物理销毁，2=混合销毁
        /// </summary>
        public int? DestroyMethod { get; set; }

        /// <summary>
        /// 销毁说明
        /// </summary>
        public string? DestroyNote { get; set; }

        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? ArchiveDate { get; set; }

        /// <summary>
        /// 归档人
        /// </summary>
        public string? ArchiveBy { get; set; }

        /// <summary>
        /// 归档位置
        /// </summary>
        public string? ArchiveLocation { get; set; }

        /// <summary>
        /// 归档说明
        /// </summary>
        public string? ArchiveNote { get; set; }

        /// <summary>
        /// 是否通知相关人员
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }
    }

    /// <summary>
    /// ISO文档作废导出DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteExportDto
    {
        /// <summary>
        /// 作废记录ID
        /// </summary>
        public long ObsoleteId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 作废类型。0=自然作废，1=强制作废，2=替代作废，3=错误作废
        /// </summary>
        public int ObsoleteType { get; set; }

        /// <summary>
        /// 作废原因
        /// </summary>
        public string ObsoleteReason { get; set; } = string.Empty;

        /// <summary>
        /// 作废日期
        /// </summary>
        public DateTime ObsoleteDate { get; set; }

        /// <summary>
        /// 作废人
        /// </summary>
        public string ObsoleteBy { get; set; } = string.Empty;

        /// <summary>
        /// 替代文档ID
        /// </summary>
        public long? ReplacementDocumentId { get; set; }

        /// <summary>
        /// 替代文档版本号
        /// </summary>
        public string? ReplacementDocumentVersion { get; set; }

        /// <summary>
        /// 状态。0=已作废，1=待回收，2=已回收，3=已销毁，4=已归档
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantBy { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门
        /// </summary>
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 申请状态。0=待审批，1=审批中，2=已批准，3=已拒绝，4=已撤回
        /// </summary>
        public int ApplicationStatus { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=拒绝
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// ISO文档作废申请DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteApplicationDto
    {
        /// <summary>
        /// 作废记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ObsoleteId { get; set; }

        /// <summary>
        /// 申请编号
        /// </summary>
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 申请人
        /// </summary>
        public string ApplicantBy { get; set; } = string.Empty;

        /// <summary>
        /// 申请人部门
        /// </summary>
        public string? ApplicantDept { get; set; }

        /// <summary>
        /// 申请原因
        /// </summary>
        public string ApplicationReason { get; set; } = string.Empty;

        /// <summary>
        /// 申请附件
        /// </summary>
        public string? ApplicationAttachment { get; set; }
    }

    /// <summary>
    /// ISO文档作废审批DTO
    /// </summary>
    public class TaktIsoDocumentObsoleteApprovalDto
    {
        /// <summary>
        /// 作废记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ObsoleteId { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=拒绝
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }
    }
}



