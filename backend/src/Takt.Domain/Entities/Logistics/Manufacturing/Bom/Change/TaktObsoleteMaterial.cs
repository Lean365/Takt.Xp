#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 旧品管理主表实体（基于设变号码控制）
    /// </summary>
    [SugarTable("Takt_logistics_bom_obsolete_item", "旧品管理主表")]
    [SugarIndex("ix_change_number", nameof(ChangeNumber), OrderByType.Asc, false)]
    [SugarIndex("ix_old_material_code", nameof(OldMaterialCode), OrderByType.Asc, false)]
    public class TaktObsoleteMaterial : TaktBaseEntity
    {
        /// <summary>设变号码</summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>设变类型(1=BOM变更 2=工艺变更 3=设计变更 4=其他变更)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "设变类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ChangeType { get; set; } = 1;

        /// <summary>旧物料编码</summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OldMaterialCode { get; set; } = string.Empty;

        /// <summary>旧物料名称</summary>
        [SugarColumn(ColumnName = "old_material_name", ColumnDescription = "旧物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OldMaterialName { get; set; } = string.Empty;

        /// <summary>旧物料规格</summary>
        [SugarColumn(ColumnName = "old_material_specification", ColumnDescription = "旧物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialSpecification { get; set; }

        /// <summary>新物料编码</summary>
        [SugarColumn(ColumnName = "new_material_code", ColumnDescription = "新物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NewMaterialCode { get; set; } = string.Empty;

        /// <summary>新物料名称</summary>
        [SugarColumn(ColumnName = "new_material_name", ColumnDescription = "新物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NewMaterialName { get; set; } = string.Empty;

        /// <summary>新物料规格</summary>
        [SugarColumn(ColumnName = "new_material_specification", ColumnDescription = "新物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialSpecification { get; set; }

        /// <summary>变更原因</summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>设变生效日期</summary>
        [SugarColumn(ColumnName = "change_effective_date", ColumnDescription = "设变生效日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeEffectiveDate { get; set; }

        /// <summary>旧品截止日期</summary>
        [SugarColumn(ColumnName = "obsolete_deadline", ColumnDescription = "旧品截止日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ObsoleteDeadline { get; set; }

        /// <summary>影响范围</summary>
        [SugarColumn(ColumnName = "impact_scope", ColumnDescription = "影响范围", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImpactScope { get; set; }

        /// <summary>状态(0=草稿 1=生效 2=处理中 3=完成 4=取消)</summary>
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
        /// <summary>旧品库存明细列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktObsoleteMaterialDetail.ObsoleteItemId))]
        public List<TaktObsoleteMaterialDetail> ObsoleteItemDetails { get; set; } = new();

        /// <summary>旧品处理记录列表</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktObsoleteMaterialProcess.ObsoleteItemId))]
        public List<TaktObsoleteMaterialProcess> ObsoleteItemProcesses { get; set; } = new();
    }
}