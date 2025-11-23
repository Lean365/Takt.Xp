#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// BOM变更单主表
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_order", "BOM变更单主表")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, false)]
    [SugarIndex("ix_change_order_no", nameof(ChangeOrderNo), OrderByType.Asc, true)]
    public class TaktChangeBomOrder : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>变更单号</summary>
        [SugarColumn(ColumnName = "change_order_no", ColumnDescription = "变更单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeOrderNo { get; set; } = string.Empty;

        /// <summary>变更类型(1=BOM主表变更 2=BOM明细变更 3=BOM物料项变更 4=综合变更)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ChangeType { get; set; } = 1;

        /// <summary>变更主题</summary>
        [SugarColumn(ColumnName = "change_subject", ColumnDescription = "变更主题", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeSubject { get; set; } = string.Empty;

        /// <summary>发起人</summary>
        [SugarColumn(ColumnName = "initiator", ColumnDescription = "发起人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Initiator { get; set; } = string.Empty;

        /// <summary>发起时间</summary>
        [SugarColumn(ColumnName = "initiate_time", ColumnDescription = "发起时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime InitiateTime { get; set; }

        /// <summary>状态(0=草稿 1=审批中 2=已通过 3=已驳回 4=已生效 5=已作废)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>变更原因</summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>影响分析</summary>
        [SugarColumn(ColumnName = "impact_analysis", ColumnDescription = "影响分析", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImpactAnalysis { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Remarks { get; set; }
    }
} 
