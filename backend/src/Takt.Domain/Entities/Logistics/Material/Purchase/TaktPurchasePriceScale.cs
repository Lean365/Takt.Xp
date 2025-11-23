#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购价格分段计价子表实体类
    /// </summary>
    [SugarTable("Takt_logistics_purchase_price_scale", "采购价格分段计价表")]
    [SugarIndex("ix_purchase_price_scale_code", nameof(InfoRecordNumber), OrderByType.Asc, nameof(ScaleNumber), OrderByType.Asc, true)]
    [SugarIndex("ix_supplier_material_scale", nameof(SupplierCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, nameof(ScaleNumber), OrderByType.Asc, false)]
    public class TaktPurchasePriceScale : TaktBaseEntity
    {
        /// <summary>信息记录号</summary>
        [SugarColumn(ColumnName = "info_record_number", ColumnDescription = "信息记录号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string InfoRecordNumber { get; set; } = string.Empty;

        /// <summary>分段编号</summary>
        [SugarColumn(ColumnName = "scale_number", ColumnDescription = "分段编号", ColumnDataType = "int", IsNullable = false)]
        public int ScaleNumber { get; set; }

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierCode { get; set; } = string.Empty;

        /// <summary>物料代码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantCode { get; set; }

        /// <summary>采购组织代码</summary>
        [SugarColumn(ColumnName = "purchase_org_code", ColumnDescription = "采购组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrgCode { get; set; }

        /// <summary>最小数量</summary>
        [SugarColumn(ColumnName = "min_quantity", ColumnDescription = "最小数量", ColumnDataType = "decimal(18,3)", IsNullable = false)]
        public decimal MinQuantity { get; set; }

        /// <summary>最大数量</summary>
        [SugarColumn(ColumnName = "max_quantity", ColumnDescription = "最大数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? MaxQuantity { get; set; }

        /// <summary>分段单价</summary>
        [SugarColumn(ColumnName = "scale_unit_price", ColumnDescription = "分段单价", ColumnDataType = "decimal(18,4)", IsNullable = false)]
        public decimal ScaleUnitPrice { get; set; }

        /// <summary>分段折扣率</summary>
        [SugarColumn(ColumnName = "scale_discount_rate", ColumnDescription = "分段折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? ScaleDiscountRate { get; set; }

        /// <summary>折扣后单价</summary>
        [SugarColumn(ColumnName = "discounted_unit_price", ColumnDescription = "折扣后单价", ColumnDataType = "decimal(18,4)", IsNullable = true)]
        public decimal? DiscountedUnitPrice { get; set; }

        /// <summary>货币代码</summary>
        [SugarColumn(ColumnName = "currency_code", ColumnDescription = "货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>采购单位</summary>
        [SugarColumn(ColumnName = "purchase_unit", ColumnDescription = "采购单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PurchaseUnit { get; set; } = string.Empty;

        /// <summary>有效开始日期</summary>
        [SugarColumn(ColumnName = "valid_from_date", ColumnDescription = "有效开始日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ValidFromDate { get; set; }

        /// <summary>有效结束日期</summary>
        [SugarColumn(ColumnName = "valid_to_date", ColumnDescription = "有效结束日期", ColumnDataType = "date", IsNullable = true, DefaultValue = "9999-12-31")]
        public DateTime ValidToDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? PriceUnit { get; set; }

        /// <summary>分段类型</summary>
        [SugarColumn(ColumnName = "scale_type", ColumnDescription = "分段类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScaleType { get; set; }

        /// <summary>分段描述</summary>
        [SugarColumn(ColumnName = "scale_description", ColumnDescription = "分段描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScaleDescription { get; set; }

        /// <summary>是否启用</summary>
        [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "bit", IsNullable = false)]
        public bool IsEnabled { get; set; } = true;

        /// <summary>优先级</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = true)]
        public int? Priority { get; set; }

        /// <summary>采购价格主表</summary>
        [Navigate(NavigateType.ManyToOne, nameof(InfoRecordNumber))]
        public TaktPurchasePrice? PurchasePrice { get; set; }
    }
} 
