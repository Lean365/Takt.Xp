#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购订单主表实体类
    /// </summary>
    [SugarTable("Takt_logistics_purchase_order", "采购订单主表")]
    [SugarIndex("ix_purchase_order_code", nameof(PurchaseOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_supplier_purchase_order", nameof(SupplierCode), OrderByType.Asc, nameof(PurchaseOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_purchase_order_date", nameof(PurchaseOrderDate), OrderByType.Desc, false)]
    public class TaktPurchaseOrder : TaktBaseEntity
    {
        /// <summary>采购订单号</summary>
        [SugarColumn(ColumnName = "purchase_order_code", ColumnDescription = "采购订单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PurchaseOrderCode { get; set; } = string.Empty;

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierCode { get; set; } = string.Empty;

        /// <summary>采购订单日期</summary>
        [SugarColumn(ColumnName = "purchase_order_date", ColumnDescription = "采购订单日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PurchaseOrderDate { get; set; }

        /// <summary>采购组织代码</summary>
        [SugarColumn(ColumnName = "purchase_org_code", ColumnDescription = "采购组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrgCode { get; set; }

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantCode { get; set; }

        /// <summary>采购组</summary>
        [SugarColumn(ColumnName = "purchase_group", ColumnDescription = "采购组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseGroup { get; set; }

        /// <summary>采购员</summary>
        [SugarColumn(ColumnName = "purchaser", ColumnDescription = "采购员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Purchaser { get; set; }

        /// <summary>订单类型</summary>
        [SugarColumn(ColumnName = "order_type", ColumnDescription = "订单类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OrderType { get; set; }

        /// <summary>订单状态</summary>
        [SugarColumn(ColumnName = "order_status", ColumnDescription = "订单状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string OrderStatus { get; set; } = string.Empty;

        /// <summary>订单总金额</summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "订单总金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal TotalAmount { get; set; }

        /// <summary>货币代码</summary>
        [SugarColumn(ColumnName = "currency_code", ColumnDescription = "货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CurrencyCode { get; set; } = string.Empty;

        /// <summary>税率</summary>
        [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? TaxRate { get; set; }

        /// <summary>税额</summary>
        [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? TaxAmount { get; set; }

        /// <summary>折扣率</summary>
        [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? DiscountRate { get; set; }

        /// <summary>折扣金额</summary>
        [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? DiscountAmount { get; set; }

        /// <summary>付款条件</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentTerms { get; set; }

        /// <summary>付款方式</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentMethod { get; set; }

        /// <summary>账期天数</summary>
        [SugarColumn(ColumnName = "payment_days", ColumnDescription = "账期天数", ColumnDataType = "int", IsNullable = true)]
        public int? PaymentDays { get; set; }

        /// <summary>预计交货日期</summary>
        [SugarColumn(ColumnName = "expected_delivery_date", ColumnDescription = "预计交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>实际交货日期</summary>
        [SugarColumn(ColumnName = "actual_delivery_date", ColumnDescription = "实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>交货地址</summary>
        [SugarColumn(ColumnName = "delivery_address", ColumnDescription = "交货地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryAddress { get; set; }

        /// <summary>联系人</summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>联系电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>紧急联系人</summary>
        [SugarColumn(ColumnName = "emergency_contact", ColumnDescription = "紧急联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyContact { get; set; }

        /// <summary>紧急联系电话</summary>
        [SugarColumn(ColumnName = "emergency_phone", ColumnDescription = "紧急联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyPhone { get; set; }

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

        /// <summary>采购订单明细</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktPurchaseOrderDetail.PurchaseOrderCode))]
        public List<TaktPurchaseOrderDetail>? PurchaseOrderDetails { get; set; }

        /// <summary>供应商信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(SupplierCode))]
        public TaktSupplier? Supplier { get; set; }
    }
} 
