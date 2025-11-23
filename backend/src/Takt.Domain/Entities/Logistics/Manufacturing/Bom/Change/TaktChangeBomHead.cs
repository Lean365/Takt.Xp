#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing
{
    /// <summary>
    /// BOM主表变更表（记录TaktBomHead所有字段的变更）
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_head", "BOM主表变更表")]
    [SugarIndex("ix_change_order_id", nameof(ChangeOrderId), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_head_id", nameof(BomHeadId), OrderByType.Asc, false)]
    public class TaktChangeBomHead : TaktBaseEntity
    {
        /// <summary>变更单Id</summary>
        [SugarColumn(ColumnName = "change_order_id", ColumnDescription = "变更单Id", IsNullable = false)]
        public long ChangeOrderId { get; set; }

        /// <summary>BOM主表Id（关联TaktBomHead）</summary>
        [SugarColumn(ColumnName = "bom_head_id", ColumnDescription = "BOM主表Id", IsNullable = false)]
        public long BomHeadId { get; set; }

        // ========== 旧记录（变更前）==========
        /// <summary>旧物料编码</summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialCode { get; set; }

        /// <summary>旧工厂代码</summary>
        [SugarColumn(ColumnName = "old_factory_code", ColumnDescription = "旧工厂代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldFactoryCode { get; set; }

        /// <summary>旧用途</summary>
        [SugarColumn(ColumnName = "old_usage", ColumnDescription = "旧用途", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldUsage { get; set; }

        /// <summary>旧设变号码</summary>
        [SugarColumn(ColumnName = "old_change_number", ColumnDescription = "旧设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldChangeNumber { get; set; }

        /// <summary>旧生效日期</summary>
        [SugarColumn(ColumnName = "old_effective_date", ColumnDescription = "旧生效日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OldEffectiveDate { get; set; }

        /// <summary>旧版本</summary>
        [SugarColumn(ColumnName = "old_version", ColumnDescription = "旧版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldVersion { get; set; }

        // ========== 新记录（变更后）==========
        /// <summary>新物料编码</summary>
        [SugarColumn(ColumnName = "new_material_code", ColumnDescription = "新物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialCode { get; set; }

        /// <summary>新工厂代码</summary>
        [SugarColumn(ColumnName = "new_factory_code", ColumnDescription = "新工厂代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewFactoryCode { get; set; }

        /// <summary>新用途</summary>
        [SugarColumn(ColumnName = "new_usage", ColumnDescription = "新用途", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewUsage { get; set; }

        /// <summary>新设变号码</summary>
        [SugarColumn(ColumnName = "new_change_number", ColumnDescription = "新设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewChangeNumber { get; set; }

        /// <summary>新生效日期</summary>
        [SugarColumn(ColumnName = "new_effective_date", ColumnDescription = "新生效日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NewEffectiveDate { get; set; }

        /// <summary>新版本</summary>
        [SugarColumn(ColumnName = "new_version", ColumnDescription = "新版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewVersion { get; set; }

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
