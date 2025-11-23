using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购信息记录
    /// </summary>
    [SugarTable("Takt_logistics_purchase_price", "采购价格表")]
    [SugarIndex("ix_purchase_price_supplier_material", nameof(SupplierCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, true)]
    public class TaktPurchasePrice : TaktBaseEntity
    {
        /// <summary>信息记录号</summary>
        [SugarColumn(ColumnName = "info_record_number", ColumnDescription = "信息记录号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string InfoRecordNumber { get; set; } = string.Empty;
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
        /// <summary>有效开始日期</summary>
        [SugarColumn(ColumnName = "valid_from_date", ColumnDescription = "有效开始日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ValidFromDate { get; set; }
        /// <summary>有效结束日期</summary>
        [SugarColumn(ColumnName = "valid_to_date", ColumnDescription = "有效结束日期", ColumnDataType = "date", IsNullable = true, DefaultValue = "9999-12-31")]
        public DateTime ValidToDate { get; set; } = new DateTime(9999, 12, 31);
        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? PriceUnit { get; set; }
        /// <summary>净价</summary>
        [SugarColumn(ColumnName = "net_price", ColumnDescription = "净价", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? NetPrice { get; set; }
        /// <summary>货币代码</summary>
        [SugarColumn(ColumnName = "currency_code", ColumnDescription = "货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CurrencyCode { get; set; }
        /// <summary>采购单位</summary>
        [SugarColumn(ColumnName = "purchase_unit", ColumnDescription = "采购单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseUnit { get; set; }
        /// <summary>交货时间（天）</summary>
        [SugarColumn(ColumnName = "delivery_time_days", ColumnDescription = "交货时间（天）", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? DeliveryTimeDays { get; set; }
        /// <summary>最小订单数量</summary>
        [SugarColumn(ColumnName = "min_order_quantity", ColumnDescription = "最小订单数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? MinOrderQuantity { get; set; }
        /// <summary>最小交货数量</summary>
        [SugarColumn(ColumnName = "min_delivery_quantity", ColumnDescription = "最小交货数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? MinDeliveryQuantity { get; set; }
        /// <summary>最大交货数量</summary>
        [SugarColumn(ColumnName = "max_delivery_quantity", ColumnDescription = "最大交货数量", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? MaxDeliveryQuantity { get; set; }
        /// <summary>标准交货时间（天）</summary>
        [SugarColumn(ColumnName = "standard_delivery_time_days", ColumnDescription = "标准交货时间（天）", ColumnDataType = "decimal", IsNullable = true)]
        public decimal? StandardDeliveryTimeDays { get; set; }
        /// <summary>供应商物料编号</summary>
        [SugarColumn(ColumnName = "supplier_material_number", ColumnDescription = "供应商物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialNumber { get; set; }
        /// <summary>供应商物料描述</summary>
        [SugarColumn(ColumnName = "supplier_material_description", ColumnDescription = "供应商物料描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialDescription { get; set; }
        /// <summary>供应商物料类型</summary>
        [SugarColumn(ColumnName = "supplier_material_type", ColumnDescription = "供应商物料类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialType { get; set; }
        /// <summary>供应商物料品牌</summary>
        [SugarColumn(ColumnName = "supplier_material_brand", ColumnDescription = "供应商物料品牌", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialBrand { get; set; }
        /// <summary>供应商物料型号</summary>
        [SugarColumn(ColumnName = "supplier_material_model", ColumnDescription = "供应商物料型号", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialModel { get; set; }
        /// <summary>供应商物料规格</summary>
        [SugarColumn(ColumnName = "supplier_material_specification", ColumnDescription = "供应商物料规格", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialSpecification { get; set; }

        /// <summary>采购价格分段计价明细</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceScale.InfoRecordNumber))]
        public List<TaktPurchasePriceScale>? PurchasePriceScales { get; set; }

        /// <summary>采购价格变更记录</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktPurchasePriceChangeLog.InfoRecordNumber))]
        public List<TaktPurchasePriceChangeLog>? PurchasePriceChangeLogs { get; set; }

        /// <summary>供应商信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(SupplierCode))]
        public TaktSupplier? Supplier { get; set; }
    }
} 
