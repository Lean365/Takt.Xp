#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Logistics.Quality.Master;

namespace Takt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 不合格品主表
    /// </summary>
    [SugarTable("Takt_logistics_quality_nonconforming_product", "不合格品主表")]
    [SugarIndex("ix_nc_no", nameof(NcNo), OrderByType.Asc, false)]
    public class TaktNonconformingProduct : TaktBaseEntity
    {
        /// <summary>
        /// 不合格品单号
        /// </summary>
        [SugarColumn(ColumnName = "nc_no", ColumnDescription = "不合格品单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NcNo { get; set; } = string.Empty;

        /// <summary>
        /// 不合格品日期
        /// </summary>
        [SugarColumn(ColumnName = "nc_date", ColumnDescription = "不合格品日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NcDate { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [SugarColumn(ColumnName = "product_name", ColumnDescription = "产品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductName { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "lot_no", ColumnDescription = "批次号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LotNo { get; set; }

        /// <summary>
        /// 责任人
        /// </summary>
        [SugarColumn(ColumnName = "responsible", ColumnDescription = "责任人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Responsible { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }


        /// <summary>
        /// 不合格品明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktNonconformingProductDetail.NcNo), nameof(NcNo))]
        public List<TaktNonconformingProductDetail>? Details { get; set; }



    }
} 
