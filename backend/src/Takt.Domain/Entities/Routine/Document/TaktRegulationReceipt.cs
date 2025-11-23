#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRegulationReceipt.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 内部规章制度签收实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Document
{
    /// <summary>
    /// 内部规章制度签收实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录内部规章制度的签收情况，包括阅读、确认、培训、考试等状态
    /// </remarks>
    [SugarTable("Takt_routine_document_regulation_receipt", "内部规章制度签收表")]
    [SugarIndex("ix_regulation_receipt_regulation_id", nameof(RegulationId), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_receipt_user_id", nameof(UserId), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_receipt_read_time", nameof(ReadTime), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_receipt_confirm_time", nameof(ConfirmTime), OrderByType.Asc, false)]
    [SugarIndex("ix_regulation_receipt_training_time", nameof(TrainingTime), OrderByType.Asc, false)]
    public class TaktRegulationReceipt : TaktBaseEntity
    {
        /// <summary>规章制度ID</summary>
        [SugarColumn(ColumnName = "regulation_id", ColumnDescription = "规章制度ID", ColumnDataType = "bigint", IsNullable = false)]
        public long RegulationId { get; set; }

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

        /// <summary>是否已培训(0=未培训 1=已培训)</summary>
        [SugarColumn(ColumnName = "is_trained", ColumnDescription = "是否已培训", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsTrained { get; set; } = 0;

        /// <summary>培训时间</summary>
        [SugarColumn(ColumnName = "training_time", ColumnDescription = "培训时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? TrainingTime { get; set; }

        /// <summary>培训时长(分钟)</summary>
        [SugarColumn(ColumnName = "training_duration", ColumnDescription = "培训时长(分钟)", ColumnDataType = "int", IsNullable = true)]
        public int? TrainingDuration { get; set; }

        /// <summary>培训成绩</summary>
        [SugarColumn(ColumnName = "training_score", ColumnDescription = "培训成绩", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? TrainingScore { get; set; }

        /// <summary>培训状态(0=未开始 1=进行中 2=已完成 3=未通过)</summary>
        [SugarColumn(ColumnName = "training_status", ColumnDescription = "培训状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TrainingStatus { get; set; } = 0;

        /// <summary>是否已考试(0=未考试 1=已考试)</summary>
        [SugarColumn(ColumnName = "is_examined", ColumnDescription = "是否已考试", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsExamined { get; set; } = 0;

        /// <summary>考试时间</summary>
        [SugarColumn(ColumnName = "exam_time", ColumnDescription = "考试时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExamTime { get; set; }

        /// <summary>考试时长(分钟)</summary>
        [SugarColumn(ColumnName = "exam_duration", ColumnDescription = "考试时长(分钟)", ColumnDataType = "int", IsNullable = true)]
        public int? ExamDuration { get; set; }

        /// <summary>考试分数</summary>
        [SugarColumn(ColumnName = "exam_score", ColumnDescription = "考试分数", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? ExamScore { get; set; }

        /// <summary>考试状态(0=未开始 1=进行中 2=已完成 3=未通过)</summary>
        [SugarColumn(ColumnName = "exam_status", ColumnDescription = "考试状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ExamStatus { get; set; } = 0;

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

        /// <summary>是否已收藏(0=未收藏 1=已收藏)</summary>
        [SugarColumn(ColumnName = "is_favorite", ColumnDescription = "是否已收藏", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsFavorite { get; set; } = 0;

        /// <summary>收藏时间</summary>
        [SugarColumn(ColumnName = "favorite_time", ColumnDescription = "收藏时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FavoriteTime { get; set; }

        /// <summary>是否已转发(0=未转发 1=已转发)</summary>
        [SugarColumn(ColumnName = "is_forward", ColumnDescription = "是否已转发", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsForward { get; set; } = 0;

        /// <summary>转发时间</summary>
        [SugarColumn(ColumnName = "forward_time", ColumnDescription = "转发时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ForwardTime { get; set; }

        /// <summary>转发对象</summary>
        [SugarColumn(ColumnName = "forward_targets", ColumnDescription = "转发对象", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ForwardTargets { get; set; }

        /// <summary>转发备注</summary>
        [SugarColumn(ColumnName = "forward_remarks", ColumnDescription = "转发备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ForwardRemarks { get; set; }

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
        /// 关联的内部规章制度
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(RegulationId))]
        public TaktRegulation? Regulation { get; set; }
    }
}



