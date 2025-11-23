#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Quality.Improvement
{
    /// <summary>
    /// 纠正预防措施主表
    /// </summary>
    [SugarTable("Takt_logistics_quality_corrective_preventive_action", "纠正预防措施主表")]
    [SugarIndex("ix_action_no", nameof(ActionNo), OrderByType.Asc, false)]
    public class TaktCorrectivePreventiveAction : TaktBaseEntity
    {
        /// <summary>
        /// 措施单号
        /// </summary>
        [SugarColumn(ColumnName = "action_no", ColumnDescription = "措施单号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ActionNo { get; set; } = string.Empty;

        /// <summary>
        /// 措施日期
        /// </summary>
        [SugarColumn(ColumnName = "action_date", ColumnDescription = "措施日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActionDate { get; set; }

        /// <summary>
        /// 问题描述
        /// </summary>
        [SugarColumn(ColumnName = "problem_desc", ColumnDescription = "问题描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProblemDesc { get; set; }

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
        /// 纠正预防措施明细集合
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktCorrectivePreventiveActionDetail.ActionNo))]
        public List<TaktCorrectivePreventiveActionDetail>? Details { get; set; }
    }
} 
