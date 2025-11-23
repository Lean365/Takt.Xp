//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentRevisionDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档修订数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文档修订基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的修订历史，包括版本号、修订内容、修订原因等
    /// </remarks>
    public class TaktIsoDocumentRevisionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentRevisionDto()
        {
            Version = string.Empty;
            RevisionBy = string.Empty;
        }

        /// <summary>
        /// 修订记录ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long RevisionId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 修订版本号
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 修订日期
        /// </summary>
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// 修订内容
        /// </summary>
        public string? RevisionContent { get; set; }

        /// <summary>
        /// 修订原因
        /// </summary>
        public string? RevisionReason { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        public string RevisionBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审批，2=已审批，3=已发布，4=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

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
    /// ISO文档修订查询DTO
    /// </summary>
    public class TaktIsoDocumentRevisionQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentRevisionQueryDto()
        {
            Version = string.Empty;
            RevisionBy = string.Empty;
            ApprovalBy = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long? DocumentId { get; set; }

        /// <summary>
        /// 修订版本号
        /// </summary>
        public string? Version { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        public string? RevisionBy { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        public bool? IsCurrentVersion { get; set; }

        /// <summary>
        /// 开始修订日期
        /// </summary>
        public DateTime? StartRevisionDate { get; set; }

        /// <summary>
        /// 结束修订日期
        /// </summary>
        public DateTime? EndRevisionDate { get; set; }

        /// <summary>
        /// 开始审批日期
        /// </summary>
        public DateTime? StartApprovalDate { get; set; }

        /// <summary>
        /// 结束审批日期
        /// </summary>
        public DateTime? EndApprovalDate { get; set; }

        /// <summary>
        /// 开始生效日期
        /// </summary>
        public DateTime? StartEffectiveDate { get; set; }

        /// <summary>
        /// 结束生效日期
        /// </summary>
        public DateTime? EndEffectiveDate { get; set; }
    }

    /// <summary>
    /// ISO文档修订创建DTO
    /// </summary>
    public class TaktIsoDocumentRevisionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentRevisionCreateDto()
        {
            Version = string.Empty;
            RevisionBy = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 修订版本号
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 修订内容
        /// </summary>
        public string? RevisionContent { get; set; }

        /// <summary>
        /// 修订原因
        /// </summary>
        public string? RevisionReason { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        public string RevisionBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
    }

    /// <summary>
    /// ISO文档修订更新DTO
    /// </summary>
    public class TaktIsoDocumentRevisionUpdateDto : TaktIsoDocumentRevisionCreateDto
    {
        /// <summary>
        /// 修订记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long RevisionId { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审批，2=已审批，3=已发布，4=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }
    }

    /// <summary>
    /// ISO文档修订导出DTO
    /// </summary>
    public class TaktIsoDocumentRevisionExportDto
    {
        /// <summary>
        /// 修订记录ID
        /// </summary>
        public long RevisionId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 修订版本号
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 修订日期
        /// </summary>
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// 修订内容
        /// </summary>
        public string? RevisionContent { get; set; }

        /// <summary>
        /// 修订原因
        /// </summary>
        public string? RevisionReason { get; set; }

        /// <summary>
        /// 修订人
        /// </summary>
        public string RevisionBy { get; set; } = string.Empty;

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审批，2=已审批，3=已发布，4=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

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
    /// ISO文档修订审批DTO
    /// </summary>
    public class TaktIsoDocumentRevisionApprovalDto
    {
        /// <summary>
        /// 修订记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long RevisionId { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审批，2=已审批，3=已发布，4=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string? ApprovalBy { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 审批日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 是否当前版本
        /// </summary>
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
    }

    /// <summary>
    /// ISO文档修订版本切换DTO
    /// </summary>
    public class TaktIsoDocumentRevisionVersionSwitchDto
    {
        /// <summary>
        /// 修订记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long RevisionId { get; set; }

        /// <summary>
        /// 是否设为当前版本
        /// </summary>
        public bool IsCurrentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }
    }
}



