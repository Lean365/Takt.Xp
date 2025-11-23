//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentDistributionDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档分发数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文档分发基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文档的分发情况，以部门为单位进行分发
    /// 功能特点:
    /// 1. 以部门为单位分发ISO文档（符合ISO质量管理体系要求）
    /// 2. 记录分发份数和分发方式
    /// 3. 完整的接收确认流程
    /// 4. 分发状态跟踪和统计
    /// 注意: ISO文档分发必须按部门进行，不支持个人、角色、岗位分发
    /// </remarks>
    public class TaktIsoDocumentDistributionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentDistributionDto()
        {
            DocumentVersion = string.Empty;
            DistributedToDeptName = string.Empty;
            DistributorBy = string.Empty;
        }

        /// <summary>
        /// 分发记录ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long DistributionId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 分发到的部门ID
        /// </summary>
        public long DistributedToDeptId { get; set; }

        /// <summary>
        /// 分发到的部门名称
        /// </summary>
        public string DistributedToDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 分发方式。0=系统通知，1=邮件，2=打印，3=电子文档，4=其他
        /// </summary>
        public int DistributionMethod { get; set; }

        /// <summary>
        /// 分发份数
        /// </summary>
        public int DistributionCopies { get; set; } = 1;

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime DistributionDate { get; set; }

        /// <summary>
        /// 分发人
        /// </summary>
        public string DistributorBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待分发，1=已分发，2=已接收，3=已确认，4=已拒绝，5=已过期
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 分发说明
        /// </summary>
        public string? DistributionNote { get; set; }

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        public string? RejectReason { get; set; }

        /// <summary>
        /// 是否强制分发
        /// </summary>
        public bool IsForced { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        public DateTime? LastReadTime { get; set; }

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
        /// 关联的分发部门
        /// </summary>
        public TaktDeptDto? DistributedToDepartment { get; set; }
    }

    /// <summary>
    /// ISO文档分发查询DTO
    /// </summary>
    public class TaktIsoDocumentDistributionQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentDistributionQueryDto()
        {
            DistributorBy = string.Empty;
            ReceivedBy = string.Empty;
            ConfirmBy = string.Empty;
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
        /// 分发到的部门ID
        /// </summary>
        public long? DistributedToDeptId { get; set; }

        /// <summary>
        /// 分发到的部门名称
        /// </summary>
        public string? DistributedToDeptName { get; set; }

        /// <summary>
        /// 分发方式
        /// </summary>
        public int? DistributionMethod { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 分发人
        /// </summary>
        public string? DistributorBy { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 是否强制分发
        /// </summary>
        public bool? IsForced { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool? IsRead { get; set; }

        /// <summary>
        /// 开始分发日期
        /// </summary>
        public DateTime? StartDistributionDate { get; set; }

        /// <summary>
        /// 结束分发日期
        /// </summary>
        public DateTime? EndDistributionDate { get; set; }

        /// <summary>
        /// 开始接收日期
        /// </summary>
        public DateTime? StartReceiveDate { get; set; }

        /// <summary>
        /// 结束接收日期
        /// </summary>
        public DateTime? EndReceiveDate { get; set; }

        /// <summary>
        /// 开始确认日期
        /// </summary>
        public DateTime? StartConfirmDate { get; set; }

        /// <summary>
        /// 结束确认日期
        /// </summary>
        public DateTime? EndConfirmDate { get; set; }
    }

    /// <summary>
    /// ISO文档分发创建DTO
    /// </summary>
    public class TaktIsoDocumentDistributionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentDistributionCreateDto()
        {
            DocumentVersion = string.Empty;
            DistributedToDeptName = string.Empty;
            DistributorBy = string.Empty;
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
        /// 分发到的部门ID
        /// </summary>
        public long DistributedToDeptId { get; set; }

        /// <summary>
        /// 分发到的部门名称
        /// </summary>
        public string DistributedToDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 分发方式。0=系统通知，1=邮件，2=打印，3=电子文档，4=其他
        /// </summary>
        public int DistributionMethod { get; set; }

        /// <summary>
        /// 分发份数
        /// </summary>
        public int DistributionCopies { get; set; } = 1;

        /// <summary>
        /// 分发人
        /// </summary>
        public string DistributorBy { get; set; } = string.Empty;

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 分发说明
        /// </summary>
        public string? DistributionNote { get; set; }

        /// <summary>
        /// 是否强制分发
        /// </summary>
        public bool IsForced { get; set; }
    }

    /// <summary>
    /// ISO文档分发更新DTO
    /// </summary>
    public class TaktIsoDocumentDistributionUpdateDto : TaktIsoDocumentDistributionCreateDto
    {
        /// <summary>
        /// 分发记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long DistributionId { get; set; }

        /// <summary>
        /// 状态。0=待分发，1=已分发，2=已接收，3=已确认，4=已拒绝，5=已过期
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        public string? RejectReason { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        public DateTime? LastReadTime { get; set; }
    }

    /// <summary>
    /// ISO文档分发导出DTO
    /// </summary>
    public class TaktIsoDocumentDistributionExportDto
    {
        /// <summary>
        /// 分发记录ID
        /// </summary>
        public long DistributionId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 分发到的部门ID
        /// </summary>
        public long DistributedToDeptId { get; set; }

        /// <summary>
        /// 分发到的部门名称
        /// </summary>
        public string DistributedToDeptName { get; set; } = string.Empty;

        /// <summary>
        /// 分发方式。0=系统通知，1=邮件，2=打印，3=电子文档，4=其他
        /// </summary>
        public int DistributionMethod { get; set; }

        /// <summary>
        /// 分发份数
        /// </summary>
        public int DistributionCopies { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime DistributionDate { get; set; }

        /// <summary>
        /// 分发人
        /// </summary>
        public string DistributorBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待分发，1=已分发，2=已接收，3=已确认，4=已拒绝，5=已过期
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 分发说明
        /// </summary>
        public string? DistributionNote { get; set; }

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        public string? RejectReason { get; set; }

        /// <summary>
        /// 是否强制分发
        /// </summary>
        public bool IsForced { get; set; }

        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        public DateTime? LastReadTime { get; set; }

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
    /// ISO文档分发状态DTO
    /// </summary>
    public class TaktIsoDocumentDistributionStatusDto
    {
        /// <summary>
        /// 分发记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long DistributionId { get; set; }

        /// <summary>
        /// 状态。0=待分发，1=已分发，2=已接收，3=已确认，4=已拒绝，5=已过期
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceivedBy { get; set; }

        /// <summary>
        /// 确认日期
        /// </summary>
        public DateTime? ConfirmDate { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string? ConfirmBy { get; set; }

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        public string? RejectReason { get; set; }
    }

    /// <summary>
    /// ISO文档分发接收DTO
    /// </summary>
    public class TaktIsoDocumentDistributionReceiveDto
    {
        /// <summary>
        /// 分发记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long DistributionId { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReceivedBy { get; set; } = string.Empty;

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }
    }

    /// <summary>
    /// ISO文档分发确认DTO
    /// </summary>
    public class TaktIsoDocumentDistributionConfirmDto
    {
        /// <summary>
        /// 分发记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long DistributionId { get; set; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string ConfirmBy { get; set; } = string.Empty;

        /// <summary>
        /// 接收说明
        /// </summary>
        public string? ReceiveNote { get; set; }
    }
}



