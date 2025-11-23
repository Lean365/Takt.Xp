#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Logistics.Quality.Master;

namespace Takt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 不合格通知书主表
    /// </summary>
    [SugarTable("Takt_logistics_quality_nonconformity_notice", "不合格通知书主表")]
    [SugarIndex("ix_notice_no", nameof(NoticeNo), OrderByType.Asc, false)]
    public class TaktNonConformityNotice : TaktBaseEntity
    {
        /// <summary>
        /// 通知书编号
        /// </summary>
        [SugarColumn(ColumnName = "notice_no", ColumnDescription = "通知书编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NoticeNo { get; set; } = string.Empty;

        /// <summary>
        /// 通知书日期
        /// </summary>
        [SugarColumn(ColumnName = "notice_date", ColumnDescription = "通知书日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NoticeDate { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [SugarColumn(ColumnName = "product_id", ColumnDescription = "产品ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "lot", ColumnDescription = "批次号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Lot { get; set; }

        /// <summary>
        /// 不合格数量
        /// </summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "不合格数量", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? Quantity { get; set; }

        /// <summary>
        /// 缺陷类型
        /// </summary>
        [SugarColumn(ColumnName = "defect_type", ColumnDescription = "缺陷类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefectType { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [SugarColumn(ColumnName = "responsible_person", ColumnDescription = "责任人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsiblePerson { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }

        /// <summary>
        /// 通知书明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNonConformityNoticeDetail.NoticeNo), nameof(NoticeNo))]
        public List<TaktNonConformityNoticeDetail>? Details { get; set; }



    }
} 
