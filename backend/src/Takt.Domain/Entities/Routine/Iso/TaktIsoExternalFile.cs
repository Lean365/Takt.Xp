#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoExternalFile.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO外来文件控制表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO外来文件控制表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO外来文件的控制情况，包括文件来源、控制状态、审批流程等
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_external_file", "ISO外来文件控制表")]
    [SugarIndex("ix_iso_external_file_code", nameof(FileCode), OrderByType.Asc, true)]
    [SugarIndex("ix_iso_external_file_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_external_file_source", nameof(FileSource), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_external_file_type", nameof(FileType), OrderByType.Asc, false)]
    public class TaktIsoExternalFile : TaktBaseEntity
    {
        /// <summary>
        /// 文件编码
        /// </summary>
        [SugarColumn(ColumnName = "file_code", ColumnDescription = "文件编码", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileCode { get; set; } = string.Empty;

        /// <summary>
        /// 文件名称
        /// </summary>
        [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名称", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 文件版本号
        /// </summary>
        [SugarColumn(ColumnName = "file_version", ColumnDescription = "文件版本号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FileVersion { get; set; } = string.Empty;

        /// <summary>
        /// 文件来源。0=外部机构，1=供应商，2=客户，3=政府，4=其他
        /// </summary>
        [SugarColumn(ColumnName = "file_source", ColumnDescription = "文件来源", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FileSource { get; set; }

        /// <summary>
        /// 来源机构名称
        /// </summary>
        [SugarColumn(ColumnName = "source_organization", ColumnDescription = "来源机构名称", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string SourceOrganization { get; set; } = string.Empty;

        /// <summary>
        /// 来源联系人
        /// </summary>
        [SugarColumn(ColumnName = "source_contact", ColumnDescription = "来源联系人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SourceContact { get; set; }

        /// <summary>
        /// 来源联系方式
        /// </summary>
        [SugarColumn(ColumnName = "source_contact_info", ColumnDescription = "来源联系方式", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? SourceContactInfo { get; set; }

        /// <summary>
        /// 文件类型。0=标准，1=法规，2=规范，3=指南，4=其他
        /// </summary>
        [SugarColumn(ColumnName = "file_type", ColumnDescription = "文件类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int FileType { get; set; }

        /// <summary>
        /// 文件分类
        /// </summary>
        [SugarColumn(ColumnName = "file_category", ColumnDescription = "文件分类", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? FileCategory { get; set; }

        /// <summary>
        /// 文件描述
        /// </summary>
        [SugarColumn(ColumnName = "file_description", ColumnDescription = "文件描述", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? FileDescription { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        [SugarColumn(ColumnName = "file_path", ColumnDescription = "文件路径", Length = 500, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string FilePath { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小（字节）
        /// </summary>
        [SugarColumn(ColumnName = "file_size", ColumnDescription = "文件大小", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long FileSize { get; set; }

        /// <summary>
        /// 文件MD5
        /// </summary>
        [SugarColumn(ColumnName = "file_md5", ColumnDescription = "文件MD5", Length = 32, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? FileMd5 { get; set; }

        /// <summary>
        /// 接收日期
        /// </summary>
        [SugarColumn(ColumnName = "receive_date", ColumnDescription = "接收日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ReceiveDate { get; set; }

        /// <summary>
        /// 接收人
        /// </summary>
        [SugarColumn(ColumnName = "receiver_by", ColumnDescription = "接收人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ReceiverBy { get; set; } = string.Empty;

        /// <summary>
        /// 状态。0=待审核，1=已审核，2=已批准，3=已拒绝，4=已归档，5=已作废
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        [SugarColumn(ColumnName = "review_date", ColumnDescription = "审核日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [SugarColumn(ColumnName = "reviewer_by", ColumnDescription = "审核人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReviewerBy { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [SugarColumn(ColumnName = "review_comment", ColumnDescription = "审核意见", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 批准日期
        /// </summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "批准日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 批准人
        /// </summary>
        [SugarColumn(ColumnName = "approver_by", ColumnDescription = "批准人", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApproverBy { get; set; }

        /// <summary>
        /// 批准意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_comment", ColumnDescription = "批准意见", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalComment { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 是否强制更新
        /// </summary>
        [SugarColumn(ColumnName = "is_force_update", ColumnDescription = "是否强制更新", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsForceUpdate { get; set; }

        /// <summary>
        /// 更新频率。0=不定期，1=每月，2=每季度，3=每半年，4=每年
        /// </summary>
        [SugarColumn(ColumnName = "update_frequency", ColumnDescription = "更新频率", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int UpdateFrequency { get; set; }

        /// <summary>
        /// 下次更新日期
        /// </summary>
        [SugarColumn(ColumnName = "next_update_date", ColumnDescription = "下次更新日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NextUpdateDate { get; set; }

        /// <summary>
        /// 分发范围
        /// </summary>
        [SugarColumn(ColumnName = "distribution_scope", ColumnDescription = "分发范围", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? DistributionScope { get; set; }

        /// <summary>
        /// 是否已分发
        /// </summary>
        [SugarColumn(ColumnName = "is_distributed", ColumnDescription = "是否已分发", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsDistributed { get; set; }

        /// <summary>
        /// 分发日期
        /// </summary>
        [SugarColumn(ColumnName = "distribution_date", ColumnDescription = "分发日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? DistributionDate { get; set; }

        /// <summary>
        /// 是否已通知
        /// </summary>
        [SugarColumn(ColumnName = "is_notified", ColumnDescription = "是否已通知", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsNotified { get; set; }

        /// <summary>
        /// 通知日期
        /// </summary>
        [SugarColumn(ColumnName = "notify_date", ColumnDescription = "通知日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NotifyDate { get; set; }

    }
}




