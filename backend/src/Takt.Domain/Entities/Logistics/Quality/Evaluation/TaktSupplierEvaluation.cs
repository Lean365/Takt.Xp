#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Quality.Evaluation
{
    /// <summary>
    /// 供应商评价（Supplier）实体
    /// </summary>
    [SugarTable("Takt_logistics_quality_evaluation_supplier", "供应商评价")]
    public class TaktSupplierEvaluation : TaktBaseEntity
    {
        /// <summary>
        /// 供应商ID
        /// </summary>
        [SugarColumn(ColumnName = "supplier_id", ColumnDescription = "供应商ID", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierId { get; set; } = string.Empty;

        /// <summary>
        /// 供应商名称
        /// </summary>
        [SugarColumn(ColumnName = "supplier_name", ColumnDescription = "供应商名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierName { get; set; } = string.Empty;

        /// <summary>
        /// 评价日期
        /// </summary>
        [SugarColumn(ColumnName = "evaluation_date", ColumnDescription = "评价日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EvaluationDate { get; set; }

        /// <summary>
        /// 评价人
        /// </summary>
        [SugarColumn(ColumnName = "evaluator", ColumnDescription = "评价人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Evaluator { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        [SugarColumn(ColumnName = "score", ColumnDescription = "评分", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? Score { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        [SugarColumn(ColumnName = "content", ColumnDescription = "评价内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Content { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Memo { get; set; }
    }
} 
