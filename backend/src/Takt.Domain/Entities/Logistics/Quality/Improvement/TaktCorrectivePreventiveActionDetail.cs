#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 纠正预防措施明细表
    /// </summary>
    [SugarTable("Takt_logistics_quality_corrective_preventive_action_detail", "纠正预防措施明细表")]
    [SugarIndex("ix_action_no", nameof(ActionNo), OrderByType.Asc, false)]
    public class TaktCorrectivePreventiveActionDetail : TaktBaseEntity
    {
        /// <summary>
        /// 措施单号（外键）
        /// </summary>
        [SugarColumn(ColumnName = "action_no", ColumnDescription = "措施单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ActionNo { get; set; } = string.Empty;

        /// <summary>
        /// 措施类型（纠正/预防）
        /// </summary>
        [SugarColumn(ColumnName = "measure_type", ColumnDescription = "措施类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeasureType { get; set; }

        /// <summary>
        /// 措施描述
        /// </summary>
        [SugarColumn(ColumnName = "measure_desc", ColumnDescription = "措施描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MeasureDesc { get; set; }

        /// <summary>
        /// 完成期限
        /// </summary>
        [SugarColumn(ColumnName = "due_date", ColumnDescription = "完成期限", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Status { get; set; }

        /// <summary>
        /// 导航属性：关联主表
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(ActionNo), nameof(TaktCorrectivePreventiveAction.ActionNo))]
        public TaktCorrectivePreventiveAction? CorrectivePreventiveAction { get; set; }

    }
}
