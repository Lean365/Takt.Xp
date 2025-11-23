#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// BOM物料项变更表（记录TaktBomItem所有字段的变更）
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_item", "BOM物料项变更表")]
    [SugarIndex("ix_change_order_id", nameof(ChangeOrderId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_head_id", nameof(BomHeadId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_detail_id", nameof(BomDetailId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_item_id", nameof(BomItemId), OrderByType.Asc, false)]
    public class TaktChangeBomItem : TaktBaseEntity
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

        /// <summary>BOM物料项Id（关联TaktBomItem）</summary>
        [SugarColumn(ColumnName = "bom_item_id", ColumnDescription = "BOM物料项Id", IsNullable = false)]
        public long BomItemId { get; set; }

        // ========== 旧记录（变更前）==========
        /// <summary>旧物料编码</summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialCode { get; set; }

        /// <summary>旧物料名称</summary>
        [SugarColumn(ColumnName = "old_material_name", ColumnDescription = "旧物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialName { get; set; }

        /// <summary>旧物料规格</summary>
        [SugarColumn(ColumnName = "old_material_specification", ColumnDescription = "旧物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialSpecification { get; set; }

        /// <summary>旧数量</summary>
        [SugarColumn(ColumnName = "old_quantity", ColumnDescription = "旧数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldQuantity { get; set; }

        /// <summary>旧单位</summary>
        [SugarColumn(ColumnName = "old_unit", ColumnDescription = "旧单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldUnit { get; set; }

        /// <summary>旧位置</summary>
        [SugarColumn(ColumnName = "old_position", ColumnDescription = "旧位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPosition { get; set; }

        /// <summary>旧物料番号</summary>
        [SugarColumn(ColumnName = "old_item_number", ColumnDescription = "旧物料番号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldItemNumber { get; set; }

        /// <summary>旧图纸编号</summary>
        [SugarColumn(ColumnName = "old_drawing_number", ColumnDescription = "旧图纸编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldDrawingNumber { get; set; }

        /// <summary>旧图纸版本</summary>
        [SugarColumn(ColumnName = "old_drawing_version", ColumnDescription = "旧图纸版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldDrawingVersion { get; set; }

        /// <summary>旧备注</summary>
        [SugarColumn(ColumnName = "old_remarks", ColumnDescription = "旧备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldRemarks { get; set; }

        // ========== 新记录（变更后）==========
        /// <summary>新物料编码</summary>
        [SugarColumn(ColumnName = "new_material_code", ColumnDescription = "新物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialCode { get; set; }

        /// <summary>新物料名称</summary>
        [SugarColumn(ColumnName = "new_material_name", ColumnDescription = "新物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialName { get; set; }

        /// <summary>新物料规格</summary>
        [SugarColumn(ColumnName = "new_material_specification", ColumnDescription = "新物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialSpecification { get; set; }

        /// <summary>新数量</summary>
        [SugarColumn(ColumnName = "new_quantity", ColumnDescription = "新数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewQuantity { get; set; }

        /// <summary>新单位</summary>
        [SugarColumn(ColumnName = "new_unit", ColumnDescription = "新单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewUnit { get; set; }

        /// <summary>新位置</summary>
        [SugarColumn(ColumnName = "new_position", ColumnDescription = "新位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPosition { get; set; }

        /// <summary>新物料番号</summary>
        [SugarColumn(ColumnName = "new_item_number", ColumnDescription = "新物料番号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewItemNumber { get; set; }

        /// <summary>新图纸编号</summary>
        [SugarColumn(ColumnName = "new_drawing_number", ColumnDescription = "新图纸编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewDrawingNumber { get; set; }

        /// <summary>新图纸版本</summary>
        [SugarColumn(ColumnName = "new_drawing_version", ColumnDescription = "新图纸版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewDrawingVersion { get; set; }

        /// <summary>新备注</summary>
        [SugarColumn(ColumnName = "new_remarks", ColumnDescription = "新备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewRemarks { get; set; }

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
