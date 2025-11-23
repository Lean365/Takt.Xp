#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购价格变更记录表实体类
    /// </summary>
    [SugarTable("Takt_logistics_purchase_price_change_log", "采购价格变更记录表")]
    [SugarIndex("ix_purchase_price_change_log_code", nameof(ChangeLogCode), OrderByType.Asc, true)]
    [SugarIndex("ix_info_record_change_log", nameof(InfoRecordNumber), OrderByType.Asc, nameof(ChangeDate), OrderByType.Desc, false)]
    [SugarIndex("ix_supplier_material_change_log", nameof(SupplierCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, nameof(ChangeDate), OrderByType.Desc, false)]
    public class TaktPurchasePriceChangeLog : TaktBaseEntity
    {
        /// <summary>变更记录编号</summary>
        [SugarColumn(ColumnName = "change_log_code", ColumnDescription = "变更记录编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeLogCode { get; set; } = string.Empty;

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

        /// <summary>变更类型</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeType { get; set; } = string.Empty;

        /// <summary>变更原因</summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>变更日期</summary>
        [SugarColumn(ColumnName = "change_date", ColumnDescription = "变更日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeDate { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>变更前价格单位</summary>
        [SugarColumn(ColumnName = "old_price_unit", ColumnDescription = "变更前价格单位", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldPriceUnit { get; set; }

        /// <summary>变更后价格单位</summary>
        [SugarColumn(ColumnName = "new_price_unit", ColumnDescription = "变更后价格单位", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewPriceUnit { get; set; }

        /// <summary>变更前净价</summary>
        [SugarColumn(ColumnName = "old_net_price", ColumnDescription = "变更前净价", ColumnDataType = "decimal(18,4)", IsNullable = true)]
        public decimal? OldNetPrice { get; set; }

        /// <summary>变更后净价</summary>
        [SugarColumn(ColumnName = "new_net_price", ColumnDescription = "变更后净价", ColumnDataType = "decimal(18,4)", IsNullable = true)]
        public decimal? NewNetPrice { get; set; }

        /// <summary>变更前货币代码</summary>
        [SugarColumn(ColumnName = "old_currency_code", ColumnDescription = "变更前货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldCurrencyCode { get; set; }

        /// <summary>变更后货币代码</summary>
        [SugarColumn(ColumnName = "new_currency_code", ColumnDescription = "变更后货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewCurrencyCode { get; set; }

        /// <summary>变更前采购单位</summary>
        [SugarColumn(ColumnName = "old_purchase_unit", ColumnDescription = "变更前采购单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPurchaseUnit { get; set; }

        /// <summary>变更后采购单位</summary>
        [SugarColumn(ColumnName = "new_purchase_unit", ColumnDescription = "变更后采购单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPurchaseUnit { get; set; }

        /// <summary>变更前有效开始日期</summary>
        [SugarColumn(ColumnName = "old_valid_from_date", ColumnDescription = "变更前有效开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OldValidFromDate { get; set; }

        /// <summary>变更后有效开始日期</summary>
        [SugarColumn(ColumnName = "new_valid_from_date", ColumnDescription = "变更后有效开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewValidFromDate { get; set; }

        /// <summary>变更前有效结束日期</summary>
        [SugarColumn(ColumnName = "old_valid_to_date", ColumnDescription = "变更前有效结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OldValidToDate { get; set; }

        /// <summary>变更后有效结束日期</summary>
        [SugarColumn(ColumnName = "new_valid_to_date", ColumnDescription = "变更后有效结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewValidToDate { get; set; }

        /// <summary>变更前交货时间</summary>
        [SugarColumn(ColumnName = "old_delivery_time_days", ColumnDescription = "变更前交货时间", ColumnDataType = "decimal(18,1)", IsNullable = true)]
        public decimal? OldDeliveryTimeDays { get; set; }

        /// <summary>变更后交货时间</summary>
        [SugarColumn(ColumnName = "new_delivery_time_days", ColumnDescription = "变更后交货时间", ColumnDataType = "decimal(18,1)", IsNullable = true)]
        public decimal? NewDeliveryTimeDays { get; set; }

        /// <summary>变更前最小订单数量</summary>
        [SugarColumn(ColumnName = "old_min_order_quantity", ColumnDescription = "变更前最小订单数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldMinOrderQuantity { get; set; }

        /// <summary>变更后最小订单数量</summary>
        [SugarColumn(ColumnName = "new_min_order_quantity", ColumnDescription = "变更后最小订单数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewMinOrderQuantity { get; set; }

        /// <summary>变更前最小交货数量</summary>
        [SugarColumn(ColumnName = "old_min_delivery_quantity", ColumnDescription = "变更前最小交货数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldMinDeliveryQuantity { get; set; }

        /// <summary>变更后最小交货数量</summary>
        [SugarColumn(ColumnName = "new_min_delivery_quantity", ColumnDescription = "变更后最小交货数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewMinDeliveryQuantity { get; set; }

        /// <summary>变更前最大交货数量</summary>
        [SugarColumn(ColumnName = "old_max_delivery_quantity", ColumnDescription = "变更前最大交货数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? OldMaxDeliveryQuantity { get; set; }

        /// <summary>变更后最大交货数量</summary>
        [SugarColumn(ColumnName = "new_max_delivery_quantity", ColumnDescription = "变更后最大交货数量", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? NewMaxDeliveryQuantity { get; set; }

        /// <summary>变更前标准交货时间</summary>
        [SugarColumn(ColumnName = "old_standard_delivery_time_days", ColumnDescription = "变更前标准交货时间", ColumnDataType = "decimal(18,1)", IsNullable = true)]
        public decimal? OldStandardDeliveryTimeDays { get; set; }

        /// <summary>变更后标准交货时间</summary>
        [SugarColumn(ColumnName = "new_standard_delivery_time_days", ColumnDescription = "变更后标准交货时间", ColumnDataType = "decimal(18,1)", IsNullable = true)]
        public decimal? NewStandardDeliveryTimeDays { get; set; }

        /// <summary>变更前供应商物料编号</summary>
        [SugarColumn(ColumnName = "old_supplier_material_number", ColumnDescription = "变更前供应商物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialNumber { get; set; }

        /// <summary>变更后供应商物料编号</summary>
        [SugarColumn(ColumnName = "new_supplier_material_number", ColumnDescription = "变更后供应商物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialNumber { get; set; }

        /// <summary>变更前供应商物料描述</summary>
        [SugarColumn(ColumnName = "old_supplier_material_description", ColumnDescription = "变更前供应商物料描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialDescription { get; set; }

        /// <summary>变更后供应商物料描述</summary>
        [SugarColumn(ColumnName = "new_supplier_material_description", ColumnDescription = "变更后供应商物料描述", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialDescription { get; set; }

        /// <summary>变更前供应商物料类型</summary>
        [SugarColumn(ColumnName = "old_supplier_material_type", ColumnDescription = "变更前供应商物料类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialType { get; set; }

        /// <summary>变更后供应商物料类型</summary>
        [SugarColumn(ColumnName = "new_supplier_material_type", ColumnDescription = "变更后供应商物料类型", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialType { get; set; }

        /// <summary>变更前供应商物料品牌</summary>
        [SugarColumn(ColumnName = "old_supplier_material_brand", ColumnDescription = "变更前供应商物料品牌", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialBrand { get; set; }

        /// <summary>变更后供应商物料品牌</summary>
        [SugarColumn(ColumnName = "new_supplier_material_brand", ColumnDescription = "变更后供应商物料品牌", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialBrand { get; set; }

        /// <summary>变更前供应商物料型号</summary>
        [SugarColumn(ColumnName = "old_supplier_material_model", ColumnDescription = "变更前供应商物料型号", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialModel { get; set; }

        /// <summary>变更后供应商物料型号</summary>
        [SugarColumn(ColumnName = "new_supplier_material_model", ColumnDescription = "变更后供应商物料型号", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialModel { get; set; }

        /// <summary>变更前供应商物料规格</summary>
        [SugarColumn(ColumnName = "old_supplier_material_specification", ColumnDescription = "变更前供应商物料规格", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldSupplierMaterialSpecification { get; set; }

        /// <summary>变更后供应商物料规格</summary>
        [SugarColumn(ColumnName = "new_supplier_material_specification", ColumnDescription = "变更后供应商物料规格", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewSupplierMaterialSpecification { get; set; }

        /// <summary>变更人</summary>
        [SugarColumn(ColumnName = "changed_by", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>审批状态</summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ApprovalStatus { get; set; } = string.Empty;

        /// <summary>审批人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>审批日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>审批意见</summary>
        [SugarColumn(ColumnName = "approval_remark", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalRemark { get; set; }

        /// <summary>变更备注</summary>
        [SugarColumn(ColumnName = "change_remark", ColumnDescription = "变更备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeRemark { get; set; }

        /// <summary>采购价格主表</summary>
        [Navigate(NavigateType.ManyToOne, nameof(InfoRecordNumber))]
        public TaktPurchasePrice? PurchasePrice { get; set; }

        /// <summary>供应商信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(SupplierCode))]
        public TaktSupplier? Supplier { get; set; }
    }
} 
