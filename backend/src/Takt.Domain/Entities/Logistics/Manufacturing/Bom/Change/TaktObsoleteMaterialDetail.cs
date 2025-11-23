#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// 旧品库存明细实体（基于设变号码控制）
    /// </summary>
    [SugarTable("Takt_logistics_bom_obsolete_item_detail", "旧品库存明细表")]
    [SugarIndex("ix_obsolete_item_id", nameof(ObsoleteItemId), OrderByType.Asc, false)]
    [SugarIndex("ix_change_number", nameof(ChangeNumber), OrderByType.Asc, false)]
    [SugarIndex("ix_warehouse_code", nameof(WarehouseCode), OrderByType.Asc, false)]
    public class TaktObsoleteMaterialDetail : TaktBaseEntity
    {
        /// <summary>旧品管理Id</summary>
        [SugarColumn(ColumnName = "obsolete_item_id", ColumnDescription = "旧品管理Id", IsNullable = false)]
        public long ObsoleteItemId { get; set; }

        /// <summary>设变号码</summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>旧物料编码</summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OldMaterialCode { get; set; } = string.Empty;

        /// <summary>仓库编码</summary>
        [SugarColumn(ColumnName = "warehouse_code", ColumnDescription = "仓库编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string WarehouseCode { get; set; } = string.Empty;

        /// <summary>仓库名称</summary>
        [SugarColumn(ColumnName = "warehouse_name", ColumnDescription = "仓库名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string WarehouseName { get; set; } = string.Empty;

        /// <summary>库位编码</summary>
        [SugarColumn(ColumnName = "location_code", ColumnDescription = "库位编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LocationCode { get; set; }

        /// <summary>批次号</summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批次号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>库存数量</summary>
        [SugarColumn(ColumnName = "stock_quantity", ColumnDescription = "库存数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal StockQuantity { get; set; } = 0;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>已处理数量</summary>
        [SugarColumn(ColumnName = "processed_quantity", ColumnDescription = "已处理数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessedQuantity { get; set; } = 0;

        /// <summary>剩余数量</summary>
        [SugarColumn(ColumnName = "remaining_quantity", ColumnDescription = "剩余数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal RemainingQuantity { get; set; } = 0;

        /// <summary>处理状态(0=未处理 1=部分处理 2=完全处理)</summary>
        [SugarColumn(ColumnName = "process_status", ColumnDescription = "处理状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProcessStatus { get; set; } = 0;

        // ========== 导航属性 ==========
        /// <summary>旧品管理主表</summary>
        [Navigate(NavigateType.OneToOne, nameof(ObsoleteItemId))]
        public TaktObsoleteMaterial? ObsoleteItem { get; set; }
    }
}