//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNotice.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 通知实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Notice   
{
    /// <summary>
    /// 通知实体
    /// </summary>
    [SugarTable("Takt_routine_notice", "通知")]
    public class TaktNotice : TaktBaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [SugarColumn(ColumnName = "notice_title", ColumnDescription = "标题", Length = 255, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string NoticeTitle { get; set; } = string.Empty;

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(ColumnName = "notice_content", ColumnDescription = "内容", IsNullable = false, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string NoticeContent { get; set; } = string.Empty;

        /// <summary>
        /// 类型（1系统通知 2个人通知）
        /// </summary>
        [SugarColumn(ColumnName = "notice_type", ColumnDescription = "类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NoticeType { get; set; }

        /// <summary>
        /// 状态
        /// 0:已发布,1:未发布
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [SugarColumn(ColumnName = "notice_publish_time", ColumnDescription = "发布时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NoticePublishTime { get; set; }

        /// <summary>
        /// 接收人ID列表
        /// </summary>
        [SugarColumn(ColumnName = "notice_receiver_ids", ColumnDescription = "接收人ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NoticeReceiverIds { get; set; }

        /// <summary>
        /// 优先级
        /// 0:普通,1:重要,2:紧急
        /// </summary>
        [SugarColumn(ColumnName = "notice_priority", ColumnDescription = "优先级", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NoticePriority { get; set; }

        /// <summary>
        /// 是否需要确认
        /// </summary>
        [SugarColumn(ColumnName = "notice_require_confirm", ColumnDescription = "是否需要确认", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool NoticeRequireConfirm { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        [SugarColumn(ColumnName = "notice_attachments", ColumnDescription = "附件列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NoticeAttachments { get; set; }

        /// <summary>
        /// 访问地址
        /// </summary>
        [SugarColumn(ColumnName = "notice_access_url", ColumnDescription = "访问地址", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NoticeAccessUrl { get; set; }

        /// <summary>
        /// 已读人数
        /// </summary>
        [SugarColumn(ColumnName = "notice_read_count", ColumnDescription = "已读人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NoticeReadCount { get; set; }

        /// <summary>
        /// 已读人ID列表
        /// </summary>
        [SugarColumn(ColumnName = "notice_read_ids", ColumnDescription = "已读人ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NoticeReadIds { get; set; }

        /// <summary>
        /// 已确认人数
        /// </summary>
        [SugarColumn(ColumnName = "notice_confirm_count", ColumnDescription = "已确认人数", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int NoticeConfirmCount { get; set; }

        /// <summary>
        /// 已确认人ID列表
        /// </summary>
        [SugarColumn(ColumnName = "notice_confirm_ids", ColumnDescription = "已确认人ID列表", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? NoticeConfirmIds { get; set; }

        /// <summary>
        /// 最后回执时间
        /// </summary>
        [SugarColumn(ColumnName = "notice_last_receipt_time", ColumnDescription = "最后回执时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? NoticeLastReceiptTime { get; set; }

    }
}


