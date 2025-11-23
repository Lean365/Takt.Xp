#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 旧品处理记录实体（基于设变号码控制）
    /// </summary>
    [SugarTable("Takt_logistics_bom_obsolete_item_process", "旧品处理记录表")]
    [SugarIndex("ix_obsolete_item_id", nameof(ObsoleteItemId), OrderByType.Asc, false)]
    [SugarIndex("ix_change_number", nameof(ChangeNumber), OrderByType.Asc, false)]
    [SugarIndex("ix_process_number", nameof(ProcessNumber), OrderByType.Asc, false)]
    public class TaktObsoleteMaterialProcess : TaktBaseEntity
    {
        /// <summary>处理单号</summary>
        [SugarColumn(ColumnName = "process_number", ColumnDescription = "处理单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProcessNumber { get; set; } = string.Empty;

        /// <summary>旧品管理Id</summary>
        [SugarColumn(ColumnName = "obsolete_item_id", ColumnDescription = "旧品管理Id", IsNullable = false)]
        public long ObsoleteItemId { get; set; }

        /// <summary>设变号码</summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>旧物料编码</summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OldMaterialCode { get; set; } = string.Empty;

        /// <summary>处理类型(1=报废 2=返修 3=降级使用 4=转售 5=其他)</summary>
        [SugarColumn(ColumnName = "process_type", ColumnDescription = "处理类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProcessType { get; set; } = 1;

        /// <summary>处理数量</summary>
        [SugarColumn(ColumnName = "process_quantity", ColumnDescription = "处理数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>处理原因</summary>
        [SugarColumn(ColumnName = "process_reason", ColumnDescription = "处理原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessReason { get; set; }

        /// <summary>处理方式</summary>
        [SugarColumn(ColumnName = "process_method", ColumnDescription = "处理方式", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessMethod { get; set; }

        /// <summary>处理地点</summary>
        [SugarColumn(ColumnName = "process_location", ColumnDescription = "处理地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessLocation { get; set; }

        /// <summary>处理日期</summary>
        [SugarColumn(ColumnName = "process_date", ColumnDescription = "处理日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ProcessDate { get; set; }

        /// <summary>处理人</summary>
        [SugarColumn(ColumnName = "processed_by", ColumnDescription = "处理人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessedBy { get; set; }

        /// <summary>处理费用</summary>
        [SugarColumn(ColumnName = "process_cost", ColumnDescription = "处理费用", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? ProcessCost { get; set; }

        /// <summary>状态(0=待处理 1=处理中 2=已完成 3=已取消)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>审批状态(0=待审批 1=已审批 2=已拒绝)</summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; } = 0;

        /// <summary>审批人</summary>
        [SugarColumn(ColumnName = "approved_by", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovedBy { get; set; }

        /// <summary>审批时间</summary>
        [SugarColumn(ColumnName = "approved_time", ColumnDescription = "审批时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovedTime { get; set; }

        // ========== 导航属性 ==========
        /// <summary>旧品管理主表</summary>
        [Navigate(NavigateType.OneToOne, nameof(ObsoleteItemId))]
        public TaktObsoleteMaterial? ObsoleteItem { get; set; }
    }
} 
