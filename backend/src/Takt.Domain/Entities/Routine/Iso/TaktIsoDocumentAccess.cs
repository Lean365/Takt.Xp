#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentAccess.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文件查阅表
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文件查阅表
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文件的查阅情况，包括查阅人、查阅时间、查阅方式等
    /// </remarks>
    [SugarTable("Takt_routine_document_iso_access", "ISO文件查阅表")]
    [SugarIndex("ix_iso_access_document_id", nameof(DocumentId), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_access_accessed_by", nameof(AccessedBy), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_access_type", nameof(AccessType), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_access_date", nameof(AccessDate), OrderByType.Asc, false)]
    public class TaktIsoDocumentAccess : TaktBaseEntity
    {
        /// <summary>
        /// ISO文档ID
        /// </summary>
        [SugarColumn(ColumnName = "document_id", ColumnDescription = "ISO文档ID", IsNullable = false, DefaultValue = "0", ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 查阅人
        /// </summary>
        [SugarColumn(ColumnName = "accessed_by", ColumnDescription = "查阅人", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string AccessedBy { get; set; } = string.Empty;


        /// <summary>
        /// 查阅类型。0=在线查阅，1=下载，2=打印，3=复制，4=其他
        /// </summary>
        [SugarColumn(ColumnName = "access_type", ColumnDescription = "查阅类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AccessType { get; set; }

        /// <summary>
        /// 查阅日期
        /// </summary>
        [SugarColumn(ColumnName = "access_date", ColumnDescription = "查阅日期", IsNullable = false, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 查阅时长（秒）
        /// </summary>
        [SugarColumn(ColumnName = "access_duration", ColumnDescription = "查阅时长（秒）", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int AccessDuration { get; set; }

        /// <summary>
        /// 查阅IP地址
        /// </summary>
        [SugarColumn(ColumnName = "access_ip", ColumnDescription = "查阅IP地址", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessIp { get; set; }

        /// <summary>
        /// 查阅设备
        /// </summary>
        [SugarColumn(ColumnName = "access_device", ColumnDescription = "查阅设备", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessDevice { get; set; }

        /// <summary>
        /// 查阅浏览器
        /// </summary>
        [SugarColumn(ColumnName = "access_browser", ColumnDescription = "查阅浏览器", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessBrowser { get; set; }

        /// <summary>
        /// 查阅操作系统
        /// </summary>
        [SugarColumn(ColumnName = "access_os", ColumnDescription = "查阅操作系统", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessOs { get; set; }

        /// <summary>
        /// 查阅位置
        /// </summary>
        [SugarColumn(ColumnName = "access_location", ColumnDescription = "查阅位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessLocation { get; set; }

        /// <summary>
        /// 查阅目的
        /// </summary>
        [SugarColumn(ColumnName = "access_purpose", ColumnDescription = "查阅目的", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessPurpose { get; set; }

        /// <summary>
        /// 查阅结果
        /// </summary>
        [SugarColumn(ColumnName = "access_result", ColumnDescription = "查阅结果", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? AccessResult { get; set; }

        /// <summary>
        /// 是否成功查阅
        /// </summary>
        [SugarColumn(ColumnName = "is_successful", ColumnDescription = "是否成功查阅", IsNullable = false, DefaultValue = "1", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        [SugarColumn(ColumnName = "failure_reason", ColumnDescription = "失败原因", Length = 500, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? FailureReason { get; set; }


        /// <summary>
        /// 是否已评价
        /// </summary>
        [SugarColumn(ColumnName = "is_rated", ColumnDescription = "是否已评价", IsNullable = false, DefaultValue = "0", ColumnDataType = "bit", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public bool IsRated { get; set; }

        /// <summary>
        /// 评价等级。1=很差，2=较差，3=一般，4=较好，5=很好
        /// </summary>
        [SugarColumn(ColumnName = "rating", ColumnDescription = "评价等级", IsNullable = true, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int? Rating { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        [SugarColumn(ColumnName = "rating_comment", ColumnDescription = "评价内容", Length = 1000, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? RatingComment { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        [SugarColumn(ColumnName = "rating_date", ColumnDescription = "评价日期", IsNullable = true, DefaultValue = "1900-01-01", ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? RatingDate { get; set; }

        /// <summary>
        /// 关联的ISO文档
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(DocumentId))]
        public TaktIsoDocument? Document { get; set; }
    }
}




