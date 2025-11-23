#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOfficialDocumentReceipt.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 公文文档签收实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Document
{
    /// <summary>
    /// 公文文档签收实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录公文文档的签收情况，包括阅读、确认、转发等状态
    /// </remarks>
    [SugarTable("Takt_routine_document_official_receipt", "公文文档签收表")]
    [SugarIndex("ix_official_receipt_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_official_receipt_user_id", nameof(UserId), OrderByType.Asc, false)]
    [SugarIndex("ix_official_receipt_read_time", nameof(ReadTime), OrderByType.Asc, false)]
    [SugarIndex("ix_official_receipt_confirm_time", nameof(ConfirmTime), OrderByType.Asc, false)]
    public class TaktOfficialDocumentReceipt : TaktBaseEntity
    {
        /// <summary>文档ID</summary>
        [SugarColumn(ColumnName = "document_id", ColumnDescription = "文档ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DocumentId { get; set; }

        /// <summary>用户ID</summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>是否已读(0=未读 1=已读)</summary>
        [SugarColumn(ColumnName = "is_read", ColumnDescription = "是否已读", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRead { get; set; } = 0;

        /// <summary>阅读时间</summary>
        [SugarColumn(ColumnName = "read_time", ColumnDescription = "阅读时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReadTime { get; set; }

        /// <summary>阅读IP地址</summary>
        [SugarColumn(ColumnName = "read_ip", ColumnDescription = "阅读IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReadIp { get; set; }

        /// <summary>阅读设备</summary>
        [SugarColumn(ColumnName = "read_device", ColumnDescription = "阅读设备", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReadDevice { get; set; }

        /// <summary>是否已确认(0=未确认 1=已确认)</summary>
        [SugarColumn(ColumnName = "is_confirm", ColumnDescription = "是否已确认", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsConfirm { get; set; } = 0;

        /// <summary>确认时间</summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>确认IP地址</summary>
        [SugarColumn(ColumnName = "confirm_ip", ColumnDescription = "确认IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConfirmIp { get; set; }

        /// <summary>确认设备</summary>
        [SugarColumn(ColumnName = "confirm_device", ColumnDescription = "确认设备", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConfirmDevice { get; set; }

        /// <summary>确认意见</summary>
        [SugarColumn(ColumnName = "confirm_opinion", ColumnDescription = "确认意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConfirmOpinion { get; set; }

        /// <summary>确认状态(0=待确认 1=同意 2=不同意 3=有意见)</summary>
        [SugarColumn(ColumnName = "confirm_status", ColumnDescription = "确认状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConfirmStatus { get; set; } = 0;

        /// <summary>是否已回复(0=未回复 1=已回复)</summary>
        [SugarColumn(ColumnName = "is_reply", ColumnDescription = "是否已回复", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsReply { get; set; } = 0;

        /// <summary>回复时间</summary>
        [SugarColumn(ColumnName = "reply_time", ColumnDescription = "回复时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReplyTime { get; set; }

        /// <summary>回复内容</summary>
        [SugarColumn(ColumnName = "reply_content", ColumnDescription = "回复内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReplyContent { get; set; }

        /// <summary>回复IP地址</summary>
        [SugarColumn(ColumnName = "reply_ip", ColumnDescription = "回复IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReplyIp { get; set; }

        /// <summary>回复设备</summary>
        [SugarColumn(ColumnName = "reply_device", ColumnDescription = "回复设备", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReplyDevice { get; set; }

        /// <summary>是否已下载(0=未下载 1=已下载)</summary>
        [SugarColumn(ColumnName = "is_downloaded", ColumnDescription = "是否已下载", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDownloaded { get; set; } = 0;

        /// <summary>下载时间</summary>
        [SugarColumn(ColumnName = "download_time", ColumnDescription = "下载时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DownloadTime { get; set; }

        /// <summary>下载IP地址</summary>
        [SugarColumn(ColumnName = "download_ip", ColumnDescription = "下载IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DownloadIp { get; set; }

        /// <summary>下载设备</summary>
        [SugarColumn(ColumnName = "download_device", ColumnDescription = "下载设备", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DownloadDevice { get; set; }



        /// <summary>是否已归档(0=未归档 1=已归档)</summary>
        [SugarColumn(ColumnName = "is_archived", ColumnDescription = "是否已归档", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsArchived { get; set; } = 0;

        /// <summary>归档时间</summary>
        [SugarColumn(ColumnName = "archive_time", ColumnDescription = "归档时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ArchiveTime { get; set; }

        /// <summary>归档分类</summary>
        [SugarColumn(ColumnName = "archive_category", ColumnDescription = "归档分类", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ArchiveCategory { get; set; }

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>阅读时长(秒)</summary>
        [SugarColumn(ColumnName = "read_duration", ColumnDescription = "阅读时长(秒)", ColumnDataType = "int", IsNullable = true)]
        public int? ReadDuration { get; set; }

        /// <summary>最后阅读时间</summary>
        [SugarColumn(ColumnName = "last_read_time", ColumnDescription = "最后阅读时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastReadTime { get; set; }

        /// <summary>
        /// 关联的公文文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(DocumentId))]
        public TaktOfficialDocument? Document { get; set; }
    }
}



