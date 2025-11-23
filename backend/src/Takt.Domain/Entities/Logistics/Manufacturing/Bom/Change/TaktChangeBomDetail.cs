#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// BOM组件明细变更表（记录TaktBomDetail所有字段的变更）
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_detail", "BOM组件明细变更表")]
    [SugarIndex("ix_change_order_id", nameof(ChangeOrderId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_head_id", nameof(BomHeadId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_detail_id", nameof(BomDetailId), OrderByType.Asc, false)]
    public class TaktChangeBomDetail : TaktBaseEntity
    {
        /// <summary>变更单Id</summary>
        [SugarColumn(ColumnName = "change_order_id", ColumnDescription = "变更单Id", IsNullable = false)]
        public long ChangeOrderId { get; set; }

        /// <summary>BOM主表Id（关联TaktBomHead）</summary>
        [SugarColumn(ColumnName = "bom_head_id", ColumnDescription = "BOM主表Id", IsNullable = false)]
        public long BomHeadId { get; set; }

        /// <summary>BOM明细Id（关联TaktBomDetail）</summary>
        [SugarColumn(ColumnName = "bom_detail_id", ColumnDescription = "BOM明细Id", IsNullable = false)]
        public long BomDetailId { get; set; }

        // ========== 旧记录（变更前）==========
        /// <summary>旧组件编码</summary>
        [SugarColumn(ColumnName = "old_component_code", ColumnDescription = "旧组件编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldComponentCode { get; set; }

        /// <summary>旧项目</summary>
        [SugarColumn(ColumnName = "old_item", ColumnDescription = "旧项目", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldItem { get; set; }

        /// <summary>旧项目类型</summary>
        [SugarColumn(ColumnName = "old_item_type", ColumnDescription = "旧项目类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldItemType { get; set; }

        /// <summary>旧组件名称</summary>
        [SugarColumn(ColumnName = "old_component_name", ColumnDescription = "旧组件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldComponentName { get; set; }

        /// <summary>旧数量</summary>
        [SugarColumn(ColumnName = "old_quantity", ColumnDescription = "旧数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldQuantity { get; set; }

        /// <summary>旧单位</summary>
        [SugarColumn(ColumnName = "old_unit", ColumnDescription = "旧单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldUnit { get; set; }

        /// <summary>旧装配</summary>
        [SugarColumn(ColumnName = "old_assembly", ColumnDescription = "旧装配", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldAssembly { get; set; }

        /// <summary>旧有效起始日期</summary>
        [SugarColumn(ColumnName = "old_valid_from", ColumnDescription = "旧有效起始日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OldValidFrom { get; set; }

        /// <summary>旧有效截止日期</summary>
        [SugarColumn(ColumnName = "old_valid_to", ColumnDescription = "旧有效截止日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OldValidTo { get; set; }

        /// <summary>旧设变号码</summary>
        [SugarColumn(ColumnName = "old_change_number", ColumnDescription = "旧设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldChangeNumber { get; set; }

        /// <summary>旧虚拟件标识</summary>
        [SugarColumn(ColumnName = "old_phantom_flag", ColumnDescription = "旧虚拟件标识", ColumnDataType = "bit", IsNullable = true)]
        public bool? OldPhantomFlag { get; set; }

        /// <summary>旧项目ID</summary>
        [SugarColumn(ColumnName = "old_item_id", ColumnDescription = "旧项目ID", IsNullable = true)]
        public long? OldItemId { get; set; }

        // ========== 新记录（变更后）==========
        /// <summary>新组件编码</summary>
        [SugarColumn(ColumnName = "new_component_code", ColumnDescription = "新组件编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewComponentCode { get; set; }

        /// <summary>新项目</summary>
        [SugarColumn(ColumnName = "new_item", ColumnDescription = "新项目", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewItem { get; set; }

        /// <summary>新项目类型</summary>
        [SugarColumn(ColumnName = "new_item_type", ColumnDescription = "新项目类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewItemType { get; set; }

        /// <summary>新组件名称</summary>
        [SugarColumn(ColumnName = "new_component_name", ColumnDescription = "新组件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewComponentName { get; set; }

        /// <summary>新数量</summary>
        [SugarColumn(ColumnName = "new_quantity", ColumnDescription = "新数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewQuantity { get; set; }

        /// <summary>新单位</summary>
        [SugarColumn(ColumnName = "new_unit", ColumnDescription = "新单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewUnit { get; set; }

        /// <summary>新装配</summary>
        [SugarColumn(ColumnName = "new_assembly", ColumnDescription = "新装配", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewAssembly { get; set; }

        /// <summary>新有效起始日期</summary>
        [SugarColumn(ColumnName = "new_valid_from", ColumnDescription = "新有效起始日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewValidFrom { get; set; }

        /// <summary>新有效截止日期</summary>
        [SugarColumn(ColumnName = "new_valid_to", ColumnDescription = "新有效截止日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewValidTo { get; set; }

        /// <summary>新设变号码</summary>
        [SugarColumn(ColumnName = "new_change_number", ColumnDescription = "新设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewChangeNumber { get; set; }

        /// <summary>新虚拟件标识</summary>
        [SugarColumn(ColumnName = "new_phantom_flag", ColumnDescription = "新虚拟件标识", ColumnDataType = "bit", IsNullable = true)]
        public bool? NewPhantomFlag { get; set; }

        /// <summary>新项目ID</summary>
        [SugarColumn(ColumnName = "new_item_id", ColumnDescription = "新项目ID", IsNullable = true)]
        public long? NewItemId { get; set; }

        // ========== 变更信息 ==========
        /// <summary>变更说明</summary>
        [SugarColumn(ColumnName = "change_description", ColumnDescription = "变更说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDescription { get; set; }

        /// <summary>变更时间</summary>
        [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeTime { get; set; }

        /// <summary>变更人</summary>
        [SugarColumn(ColumnName = "changed_by", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangedBy { get; set; }
    }
} 
