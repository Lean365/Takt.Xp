#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Logistics.Material.Master;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 货源清单实体类 (SAP Source List)
    /// </summary>
    [SugarTable("Takt_logistics_material_purchase_supply_source", "货源清单")]
    [SugarIndex("ix_source_list_material_supplier", nameof(MaterialCode), OrderByType.Asc, nameof(SupplierCode), OrderByType.Asc, true)]
    [SugarIndex("ix_source_list_supplier", nameof(SupplierCode), OrderByType.Asc, false)]
    [SugarIndex("ix_source_list_valid_date", nameof(ValidFromDate), OrderByType.Asc, nameof(ValidToDate), OrderByType.Asc, false)]
    [SugarIndex("ix_source_list_plant", nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktSupplySource : TaktBaseEntity
    {
        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>物料代码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料代码", Length = 18, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierCode { get; set; } = string.Empty;

        /// <summary>采购组织</summary>
        [SugarColumn(ColumnName = "purchase_org", ColumnDescription = "采购组织", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PurchaseOrg { get; set; } = string.Empty;

        /// <summary>采购工厂</summary>
        [SugarColumn(ColumnName = "purchase_plant", ColumnDescription = "采购工厂", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchasePlant { get; set; }

        /// <summary>有效日期从</summary>
        [SugarColumn(ColumnName = "valid_from_date", ColumnDescription = "有效日期从", ColumnDataType = "date", IsNullable = false)]
        public DateTime ValidFromDate { get; set; }

        /// <summary>有效日期到</summary>
        [SugarColumn(ColumnName = "valid_to_date", ColumnDescription = "有效日期到", ColumnDataType = "date", IsNullable = true, DefaultValue = "9999-12-31")]
        public DateTime? ValidToDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>固定货源</summary>
        [SugarColumn(ColumnName = "fixed_source", ColumnDescription = "固定货源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int FixedSource { get; set; } = 0;

        /// <summary>MRP相关</summary>
        [SugarColumn(ColumnName = "mrp_relevant", ColumnDescription = "MRP相关", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MrpRelevant { get; set; } = 0;

        /// <summary>MRP组</summary>
        [SugarColumn(ColumnName = "mrp_group", ColumnDescription = "MRP组", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpGroup { get; set; }

        /// <summary>供应商信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(SupplierCode))]
        public TaktSupplier? Supplier { get; set; }

        /// <summary>物料信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(MaterialCode))]
        public TaktPlantMaterial? Material { get; set; }
    }
}
