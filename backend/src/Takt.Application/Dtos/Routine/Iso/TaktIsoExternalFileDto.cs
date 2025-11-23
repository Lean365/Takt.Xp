//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoExternalFileDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO外来文件控制数据传输对象
//===================================================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO外来文件控制基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO外来文件的控制情况，包括文件来源、控制状态、审批流程等
    /// </remarks>
    public class TaktIsoExternalFileDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoExternalFileDto()
        {
            FileCode = string.Empty;
            FileName = string.Empty;
            FileVersion = string.Empty;
            SourceOrganization = string.Empty;
            FilePath = string.Empty;
            ReceiverBy = string.Empty;
        }

        /// <summary>
        /// 外来文件ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 文件编码
        /// </summary>
        public string FileCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件版本号
        /// </summary>
        public string FileVersion { get; set; } = string.Empty;

        /// <summary>
        /// 文件来源。0=外部机构，1=供应商，2=客户，3=政府，4=其他
        /// </summary>
        public int FileSource { get; set; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        public string SourceOrganization { get; set; } = string.Empty;

        /// <summary>
        /// 来源联系人
        /// </summary>
        public string? SourceContact { get; set; }

        /// <summary>
        /// 来源联系方式
        /// </summary>
        public string? SourceContactInfo { get; set; }

        /// <summary>
        /// 文件类型。0=标准，1=法规，2=规范，3=指南，4=其他
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        public string? FileCategory { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string? FileDescription { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReceiverBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 更新频率。0=不定期，1=每月，2=每季度，3=每半年，4=每年
        /// </summary>
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// 下次更新日期
        /// </summary>
        public DateTime? NextUpdateDate { get; set; }

        /// <summary>
        /// 分发范围
        /// </summary>
        public string? DistributionScope { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool IsDistributed { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime? DistributionDate { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }

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
    }

    /// <summary>
    /// ISO外来文件控制查询DTO
    /// </summary>
    public class TaktIsoExternalFileQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoExternalFileQueryDto()
        {
            FileCode = string.Empty;
            FileName = string.Empty;
            SourceOrganization = string.Empty;
            ReceiverBy = string.Empty;
            ReviewerBy = string.Empty;
            ApproverBy = string.Empty;
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        public string? FileCode { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// 文件版本号
        /// </summary>
        public string? FileVersion { get; set; }

        /// <summary>
        /// 文件来源
        /// </summary>
        public int? FileSource { get; set; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        public string? SourceOrganization { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public int? FileType { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        public string? FileCategory { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string? ReceiverBy { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool? IsForceUpdate { get; set; }

        /// <summary>
        /// 更新频率
        /// </summary>
        public int? UpdateFrequency { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool? IsDistributed { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public bool? IsNotified { get; set; }

        /// <summary>
        /// 开始接收日期
        /// </summary>
        public DateTime? StartReceiveDate { get; set; }

        /// <summary>
        /// 结束接收日期
        /// </summary>
        public DateTime? EndReceiveDate { get; set; }

        /// <summary>
        /// 开始审核日期
        /// </summary>
        public DateTime? StartReviewDate { get; set; }

        /// <summary>
        /// 结束审核日期
        /// </summary>
        public DateTime? EndReviewDate { get; set; }

        /// <summary>
        /// 开始批准日期
        /// </summary>
        public DateTime? StartApprovalDate { get; set; }

        /// <summary>
        /// 结束批准日期
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

        /// <summary>
        /// 开始失效日期
        /// </summary>
        public DateTime? StartExpiryDate { get; set; }

        /// <summary>
        /// 结束失效日期
        /// </summary>
        public DateTime? EndExpiryDate { get; set; }
    }

    /// <summary>
    /// ISO外来文件控制创建DTO
    /// </summary>
    public class TaktIsoExternalFileCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoExternalFileCreateDto()
        {
            FileCode = string.Empty;
            FileName = string.Empty;
            FileVersion = string.Empty;
            SourceOrganization = string.Empty;
            FilePath = string.Empty;
            ReceiverBy = string.Empty;
        }

        /// <summary>
        /// 文件编码
        /// </summary>
        public string FileCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件版本号
        /// </summary>
        public string FileVersion { get; set; } = string.Empty;

        /// <summary>
        /// 文件来源。0=外部机构，1=供应商，2=客户，3=政府，4=其他
        /// </summary>
        public int FileSource { get; set; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        public string SourceOrganization { get; set; } = string.Empty;

        /// <summary>
        /// 来源联系人
        /// </summary>
        public string? SourceContact { get; set; }

        /// <summary>
        /// 来源联系方式
        /// </summary>
        public string? SourceContactInfo { get; set; }

        /// <summary>
        /// 文件类型。0=标准，1=法规，2=规范，3=指南，4=其他
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        public string? FileCategory { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string? FileDescription { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReceiverBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 更新频率。0=不定期，1=每月，2=每季度，3=每半年，4=每年
        /// </summary>
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// 下次更新日期
        /// </summary>
        public DateTime? NextUpdateDate { get; set; }

        /// <summary>
        /// 分发范围
        /// </summary>
        public string? DistributionScope { get; set; }
    }

    /// <summary>
    /// ISO外来文件控制更新DTO
    /// </summary>
    public class TaktIsoExternalFileUpdateDto : TaktIsoExternalFileCreateDto
    {
        /// <summary>
        /// 外来文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool IsDistributed { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime? DistributionDate { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }
    }

    /// <summary>
    /// ISO外来文件控制导出DTO
    /// </summary>
    public class TaktIsoExternalFileExportDto
    {
        /// <summary>
        /// 外来文件ID
        /// </summary>
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 文件编码
        /// </summary>
        public string FileCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件版本号
        /// </summary>
        public string FileVersion { get; set; } = string.Empty;

        /// <summary>
        /// 文件来源。0=外部机构，1=供应商，2=客户，3=政府，4=其他
        /// </summary>
        public int FileSource { get; set; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        public string SourceOrganization { get; set; } = string.Empty;

        /// <summary>
        /// 来源联系人
        /// </summary>
        public string? SourceContact { get; set; }

        /// <summary>
        /// 来源联系方式
        /// </summary>
        public string? SourceContactInfo { get; set; }

        /// <summary>
        /// 文件类型。0=标准，1=法规，2=规范，3=指南，4=其他
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        public string? FileCategory { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        public string? FileDescription { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        public string ReceiverBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 更新频率。0=不定期，1=每月，2=每季度，3=每半年，4=每年
        /// </summary>
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// 下次更新日期
        /// </summary>
        public DateTime? NextUpdateDate { get; set; }

        /// <summary>
        /// 分发范围
        /// </summary>
        public string? DistributionScope { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool IsDistributed { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime? DistributionDate { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }

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
    /// ISO外来文件控制审核DTO
    /// </summary>
    public class TaktIsoExternalFileReviewDto
    {
        /// <summary>
        /// 外来文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }
    }

    /// <summary>
    /// ISO外来文件控制批准DTO
    /// </summary>
    public class TaktIsoExternalFileApprovalDto
    {
        /// <summary>
        /// 外来文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }
    }

    /// <summary>
    /// ISO外来文件控制分发DTO
    /// </summary>
    public class TaktIsoExternalFileDistributionDto
    {
        /// <summary>
        /// 外来文件ID
        /// </summary>
        [AdaptMember("Id")]
        public long ExternalFileId { get; set; }

        /// <summary>
        /// 分发范围
        /// </summary>
        public string? DistributionScope { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        public bool IsDistributed { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        public DateTime? DistributionDate { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        public DateTime? NotifyDate { get; set; }
    }
}




