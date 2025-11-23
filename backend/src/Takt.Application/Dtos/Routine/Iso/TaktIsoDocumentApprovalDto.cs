//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentApprovalDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档审批数据传输对象
//===================================================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文档审批基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 专门处理ISO文档的审批流程，与申请流程分离
    /// 功能特点:
    /// 1. 支持多级审批流程（部门审核、管理者代表批准等）
    /// 2. 区分指定审批人和实际审批人
    /// 3. 支持审批意见和审批结果记录
    /// 4. 完整的审批时间跟踪
    /// 注意: 此表只处理审批流程，不包含申请信息
    /// </remarks>
    public class TaktIsoDocumentApprovalDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentApprovalDto()
        {
            DocumentVersion = string.Empty;
            StepName = string.Empty;
            ApproverBy = string.Empty;
            SubmitterBy = string.Empty;
        }

        /// <summary>
        /// 审批记录ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long ApprovalId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 审批步骤
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 审批环节。1=部门审核，2=管理者代表批准，3=质量经理审批，4=总经理审批，5=其他
        /// </summary>
        public int ApprovalStep { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string ApproverBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 状态。0=待审批，1=已审批，2=已拒绝，3=已跳过，4=已撤回
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=驳回，3=跳过，4=撤回
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitterBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否必须审批
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 是否并行审批
        /// </summary>
        public bool IsParallel { get; set; }

        /// <summary>
        /// 审批时限（小时）
        /// </summary>
        public int? TimeLimitHours { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool IsTimeout { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public DateTime? TimeoutDate { get; set; }

        /// <summary>
        /// 前一步骤ID
        /// </summary>
        public long? PreviousStepId { get; set; }

        /// <summary>
        /// 下一步骤ID
        /// </summary>
        public long? NextStepId { get; set; }

        /// <summary>
        /// 审批附件
        /// </summary>
        public string? ApprovalAttachment { get; set; }

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
    }

    /// <summary>
    /// ISO文档审批查询DTO
    /// </summary>
    public class TaktIsoDocumentApprovalQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentApprovalQueryDto()
        {
            ApproverBy = string.Empty;
            SubmitterBy = string.Empty;
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
        /// 审批步骤
        /// </summary>
        public int? Step { get; set; }

        /// <summary>
        /// 审批环节
        /// </summary>
        public int? ApprovalStep { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 审批结果
        /// </summary>
        public int? ApprovalResult { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string? SubmitterBy { get; set; }

        /// <summary>
        /// 是否必须审批
        /// </summary>
        public bool? IsRequired { get; set; }

        /// <summary>
        /// 是否并行审批
        /// </summary>
        public bool? IsParallel { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool? IsTimeout { get; set; }

        /// <summary>
        /// 开始审批日期
        /// </summary>
        public DateTime? StartApprovalDate { get; set; }

        /// <summary>
        /// 结束审批日期
        /// </summary>
        public DateTime? EndApprovalDate { get; set; }

        /// <summary>
        /// 开始提交日期
        /// </summary>
        public DateTime? StartSubmitDate { get; set; }

        /// <summary>
        /// 结束提交日期
        /// </summary>
        public DateTime? EndSubmitDate { get; set; }
    }

    /// <summary>
    /// ISO文档审批创建DTO
    /// </summary>
    public class TaktIsoDocumentApprovalCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentApprovalCreateDto()
        {
            DocumentVersion = string.Empty;
            StepName = string.Empty;
            ApproverBy = string.Empty;
            SubmitterBy = string.Empty;
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
        /// 审批步骤
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 审批环节。1=部门审核，2=管理者代表批准，3=质量经理审批，4=总经理审批，5=其他
        /// </summary>
        public int ApprovalStep { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string ApproverBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitterBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否必须审批
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// 是否并行审批
        /// </summary>
        public bool IsParallel { get; set; }

        /// <summary>
        /// 审批时限（小时）
        /// </summary>
        public int? TimeLimitHours { get; set; }

        /// <summary>
        /// 前一步骤ID
        /// </summary>
        public long? PreviousStepId { get; set; }

        /// <summary>
        /// 下一步骤ID
        /// </summary>
        public long? NextStepId { get; set; }

        /// <summary>
        /// 审批附件
        /// </summary>
        public string? ApprovalAttachment { get; set; }
    }

    /// <summary>
    /// ISO文档审批更新DTO
    /// </summary>
    public class TaktIsoDocumentApprovalUpdateDto : TaktIsoDocumentApprovalCreateDto
    {
        /// <summary>
        /// 审批记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ApprovalId { get; set; }

        /// <summary>
        /// 状态。0=待审批，1=已审批，2=已拒绝，3=已跳过，4=已撤回
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=驳回，3=跳过，4=撤回
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool IsTimeout { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public DateTime? TimeoutDate { get; set; }
    }

    /// <summary>
    /// ISO文档审批导出DTO
    /// </summary>
    public class TaktIsoDocumentApprovalExportDto
    {
        /// <summary>
        /// 审批记录ID
        /// </summary>
        public long ApprovalId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 审批步骤
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// 审批环节。1=部门审核，2=管理者代表批准，3=质量经理审批，4=总经理审批，5=其他
        /// </summary>
        public int ApprovalStep { get; set; }

        /// <summary>
        /// 审批步骤名称
        /// </summary>
        public string StepName { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string ApproverBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人部门
        /// </summary>
        public string? ApproverDept { get; set; }

        /// <summary>
        /// 状态。0=待审批，1=已审批，2=已拒绝，3=已跳过，4=已撤回
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=驳回，3=跳过，4=撤回
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// 提交人
        /// </summary>
        public string SubmitterBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否必须审批
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// 是否并行审批
        /// </summary>
        public bool IsParallel { get; set; }

        /// <summary>
        /// 审批时限（小时）
        /// </summary>
        public int? TimeLimitHours { get; set; }

        /// <summary>
        /// 是否超时
        /// </summary>
        public bool IsTimeout { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public DateTime? TimeoutDate { get; set; }

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
    /// ISO文档审批状态DTO
    /// </summary>
    public class TaktIsoDocumentApprovalStatusDto
    {
        /// <summary>
        /// 审批记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ApprovalId { get; set; }

        /// <summary>
        /// 状态。0=待审批，1=已审批，2=已拒绝，3=已跳过，4=已撤回
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批结果。0=待审批，1=通过，2=驳回，3=跳过，4=撤回
        /// </summary>
        public int ApprovalResult { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComments { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }
    }
}




