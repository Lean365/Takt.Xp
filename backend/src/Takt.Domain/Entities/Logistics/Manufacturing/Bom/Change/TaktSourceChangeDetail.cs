#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 源设变明细实体类
    /// </summary>
    [SugarTable("Takt_logistics_bom_source_change_detail", "源设变明细")]
    [SugarIndex("ix_change_number", nameof(ChangeNumber), OrderByType.Asc, false)]
    [SugarIndex("ix_finished_material", nameof(FinishedMaterial), OrderByType.Asc, false)]
    [SugarIndex("ix_parent_material", nameof(ParentMaterial), OrderByType.Asc, false)]
    [SugarIndex("ix_old_material_code", nameof(OldMaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_new_material_code", nameof(NewMaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_bom_number", nameof(BomNumber), OrderByType.Asc, false)]
    public class TaktSourceChangeDetail : TaktBaseEntity
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 成品料号
        /// </summary>
        [SugarColumn(ColumnName = "finished_material", ColumnDescription = "成品料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FinishedMaterial { get; set; }

        /// <summary>
        /// 上阶料号
        /// </summary>
        [SugarColumn(ColumnName = "parent_material", ColumnDescription = "上阶料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentMaterial { get; set; }

        /// <summary>
        /// 旧料号
        /// </summary>
        [SugarColumn(ColumnName = "old_material_code", ColumnDescription = "旧料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialCode { get; set; }

        /// <summary>
        /// 料名（旧）
        /// </summary>
        [SugarColumn(ColumnName = "old_material_name", ColumnDescription = "料名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialName { get; set; }

        /// <summary>
        /// 数量（旧）
        /// </summary>
        [SugarColumn(ColumnName = "old_quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldQuantity { get; set; }

        /// <summary>
        /// 位置（旧）
        /// </summary>
        [SugarColumn(ColumnName = "old_position", ColumnDescription = "位置", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPosition { get; set; }

        /// <summary>
        /// 新料号
        /// </summary>
        [SugarColumn(ColumnName = "new_material_code", ColumnDescription = "新料号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialCode { get; set; }

        /// <summary>
        /// 料名（新）
        /// </summary>
        [SugarColumn(ColumnName = "new_material_name", ColumnDescription = "料名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewMaterialName { get; set; }

        /// <summary>
        /// 数量（新）
        /// </summary>
        [SugarColumn(ColumnName = "new_quantity", ColumnDescription = "数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewQuantity { get; set; }

        /// <summary>
        /// 位置（新）
        /// </summary>
        [SugarColumn(ColumnName = "new_position", ColumnDescription = "位置", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPosition { get; set; }

        /// <summary>
        /// BOM番号
        /// </summary>
        [SugarColumn(ColumnName = "bom_number", ColumnDescription = "BOM番号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BomNumber { get; set; }

        /// <summary>
        /// 互换性
        /// </summary>
        [SugarColumn(ColumnName = "interchangeable", ColumnDescription = "互换性", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Interchangeable { get; set; }

        /// <summary>
        /// 区分
        /// </summary>
        [SugarColumn(ColumnName = "division", ColumnDescription = "区分", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Division { get; set; }

        /// <summary>
        /// 安排指示
        /// </summary>
        [SugarColumn(ColumnName = "arrange_instruction", ColumnDescription = "安排指示", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ArrangeInstruction { get; set; }

        /// <summary>
        /// 旧料处理
        /// </summary>
        [SugarColumn(ColumnName = "old_material_process", ColumnDescription = "旧料处理", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldMaterialProcess { get; set; }

        /// <summary>
        /// BOM生效日期
        /// </summary>
        [SugarColumn(ColumnName = "bom_effective_date", ColumnDescription = "BOM生效日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? BomEffectiveDate { get; set; }

        /// <summary>
        /// 设变主表
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(ChangeNumber))]
        public TaktSourceChange? SourceChange { get; set; }
    }
} 
