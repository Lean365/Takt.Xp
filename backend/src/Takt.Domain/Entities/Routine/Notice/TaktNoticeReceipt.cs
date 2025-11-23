//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNoticeReceipt.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 通知签收实体
//===================================================================

namespace Takt.Domain.Entities.Routine.Notice
{
    /// <summary>
    /// 通知签收实体
    /// </summary>
    [SugarTable("Takt_routine_notice_receipt", "通知签收")]
    [SugarIndex("index_receipt_notice_id", nameof(NoticeId), OrderByType.Asc)]
    [SugarIndex("index_receipt_user_id", nameof(UserId), OrderByType.Asc)]
    [SugarIndex("index_receipt_read_time", nameof(ReadTime), OrderByType.Asc)]
    [SugarIndex("index_receipt_confirm_time", nameof(ConfirmTime), OrderByType.Asc)]
    public class TaktNoticeReceipt : TaktBaseEntity
    {
        /// <summary>
        /// 通知ID
        /// </summary>
        [SugarColumn(ColumnName = "notice_id", ColumnDescription = "通知ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long NoticeId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 是否已读（0：未读，1：已读）
        /// </summary>
        [SugarColumn(ColumnName = "is_read", ColumnDescription = "是否已读", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsRead { get; set; }

        /// <summary>
        /// 阅读时间
        /// </summary>
        [SugarColumn(ColumnName = "read_time", ColumnDescription = "阅读时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReadTime { get; set; }

        /// <summary>
        /// 阅读IP地址
        /// </summary>
        [SugarColumn(ColumnName = "read_ip", ColumnDescription = "阅读IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReadIp { get; set; }

        /// <summary>
        /// 阅读设备
        /// </summary>
        [SugarColumn(ColumnName = "read_device", ColumnDescription = "阅读设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReadDevice { get; set; }

        /// <summary>
        /// 是否已确认（0：未确认，1：已确认）
        /// </summary>
        [SugarColumn(ColumnName = "is_confirm", ColumnDescription = "是否已确认", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsConfirm { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        [SugarColumn(ColumnName = "confirm_time", ColumnDescription = "确认时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 确认IP地址
        /// </summary>
        [SugarColumn(ColumnName = "confirm_ip", ColumnDescription = "确认IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmIp { get; set; }

        /// <summary>
        /// 确认设备
        /// </summary>
        [SugarColumn(ColumnName = "confirm_device", ColumnDescription = "确认设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmDevice { get; set; }

        /// <summary>
        /// 确认意见
        /// </summary>
        [SugarColumn(ColumnName = "confirm_opinion", ColumnDescription = "确认意见", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ConfirmOpinion { get; set; }

        /// <summary>
        /// 确认状态（0：待确认，1：同意，2：不同意，3：有意见）
        /// </summary>
        [SugarColumn(ColumnName = "confirm_status", ColumnDescription = "确认状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ConfirmStatus { get; set; }

        /// <summary>
        /// 是否已回复（0：未回复，1：已回复）
        /// </summary>
        [SugarColumn(ColumnName = "is_reply", ColumnDescription = "是否已回复", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsReply { get; set; }

        /// <summary>
        /// 回复时间
        /// </summary>
        [SugarColumn(ColumnName = "reply_time", ColumnDescription = "回复时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReplyTime { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [SugarColumn(ColumnName = "reply_content", ColumnDescription = "回复内容", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReplyContent { get; set; }

        /// <summary>
        /// 回复IP地址
        /// </summary>
        [SugarColumn(ColumnName = "reply_ip", ColumnDescription = "回复IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReplyIp { get; set; }

        /// <summary>
        /// 回复设备
        /// </summary>
        [SugarColumn(ColumnName = "reply_device", ColumnDescription = "回复设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ReplyDevice { get; set; }

        /// <summary>
        /// 是否已转发（0：未转发，1：已转发）
        /// </summary>
        [SugarColumn(ColumnName = "is_forward", ColumnDescription = "是否已转发", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsForward { get; set; }

        /// <summary>
        /// 转发时间
        /// </summary>
        [SugarColumn(ColumnName = "forward_time", ColumnDescription = "转发时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ForwardTime { get; set; }

        /// <summary>
        /// 转发对象
        /// </summary>
        [SugarColumn(ColumnName = "forward_targets", ColumnDescription = "转发对象", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ForwardTargets { get; set; }

        /// <summary>
        /// 转发备注
        /// </summary>
        [SugarColumn(ColumnName = "forward_remarks", ColumnDescription = "转发备注", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ForwardRemarks { get; set; }

        /// <summary>
        /// 是否已收藏（0：未收藏，1：已收藏）
        /// </summary>
        [SugarColumn(ColumnName = "is_favorite", ColumnDescription = "是否已收藏", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsFavorite { get; set; }

        /// <summary>
        /// 收藏时间
        /// </summary>
        [SugarColumn(ColumnName = "favorite_time", ColumnDescription = "收藏时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? FavoriteTime { get; set; }



        /// <summary>
        /// 阅读时长（秒）
        /// </summary>
        [SugarColumn(ColumnName = "read_duration", ColumnDescription = "阅读时长（秒）", IsNullable = true, ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? ReadDuration { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ReadCount { get; set; }

        /// <summary>
        /// 最后阅读时间
        /// </summary>
        [SugarColumn(ColumnName = "last_read_time", ColumnDescription = "最后阅读时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? LastReadTime { get; set; }

        /// <summary>
        /// 是否已归档（0：未归档，1：已归档）
        /// </summary>
        [SugarColumn(ColumnName = "is_archived", ColumnDescription = "是否已归档", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int IsArchived { get; set; }

        /// <summary>
        /// 归档时间
        /// </summary>
        [SugarColumn(ColumnName = "archive_time", ColumnDescription = "归档时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ArchiveTime { get; set; }

        /// <summary>
        /// 归档分类
        /// </summary>
        [SugarColumn(ColumnName = "archive_category", ColumnDescription = "归档分类", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ArchiveCategory { get; set; }


    }
}


