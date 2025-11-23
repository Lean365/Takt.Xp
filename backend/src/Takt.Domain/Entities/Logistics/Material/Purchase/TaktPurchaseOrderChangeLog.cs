#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购订单变更记录表实体类
    /// </summary>
    [SugarTable("Takt_logistics_purchase_order_change_log", "采购订单变更记录表")]
    [SugarIndex("ix_purchase_order_change_log_code", nameof(ChangeLogCode), OrderByType.Asc, true)]
    [SugarIndex("ix_purchase_order_change_log", nameof(PurchaseOrderCode), OrderByType.Asc, nameof(ChangeDate), OrderByType.Desc, false)]
    [SugarIndex("ix_supplier_order_change_log", nameof(SupplierCode), OrderByType.Asc, nameof(PurchaseOrderCode), OrderByType.Asc, nameof(ChangeDate), OrderByType.Desc, false)]
    public class TaktPurchaseOrderChangeLog : TaktBaseEntity
    {
        /// <summary>变更记录编号</summary>
        [SugarColumn(ColumnName = "change_log_code", ColumnDescription = "变更记录编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeLogCode { get; set; } = string.Empty;

        /// <summary>采购订单号</summary>
        [SugarColumn(ColumnName = "purchase_order_code", ColumnDescription = "采购订单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PurchaseOrderCode { get; set; } = string.Empty;

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierCode { get; set; } = string.Empty;

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

        #region 订单基础信息变更

        /// <summary>变更前采购订单日期</summary>
        [SugarColumn(ColumnName = "old_purchase_order_date", ColumnDescription = "变更前采购订单日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OldPurchaseOrderDate { get; set; }

        /// <summary>变更后采购订单日期</summary>
        [SugarColumn(ColumnName = "new_purchase_order_date", ColumnDescription = "变更后采购订单日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewPurchaseOrderDate { get; set; }

        /// <summary>变更前采购组织代码</summary>
        [SugarColumn(ColumnName = "old_purchase_org_code", ColumnDescription = "变更前采购组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPurchaseOrgCode { get; set; }

        /// <summary>变更后采购组织代码</summary>
        [SugarColumn(ColumnName = "new_purchase_org_code", ColumnDescription = "变更后采购组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPurchaseOrgCode { get; set; }

        /// <summary>变更前工厂代码</summary>
        [SugarColumn(ColumnName = "old_plant_code", ColumnDescription = "变更前工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPlantCode { get; set; }

        /// <summary>变更后工厂代码</summary>
        [SugarColumn(ColumnName = "new_plant_code", ColumnDescription = "变更后工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPlantCode { get; set; }

        /// <summary>变更前采购组</summary>
        [SugarColumn(ColumnName = "old_purchase_group", ColumnDescription = "变更前采购组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPurchaseGroup { get; set; }

        /// <summary>变更后采购组</summary>
        [SugarColumn(ColumnName = "new_purchase_group", ColumnDescription = "变更后采购组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPurchaseGroup { get; set; }

        /// <summary>变更前采购员</summary>
        [SugarColumn(ColumnName = "old_purchaser", ColumnDescription = "变更前采购员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPurchaser { get; set; }

        /// <summary>变更后采购员</summary>
        [SugarColumn(ColumnName = "new_purchaser", ColumnDescription = "变更后采购员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPurchaser { get; set; }

        /// <summary>变更前订单类型</summary>
        [SugarColumn(ColumnName = "old_order_type", ColumnDescription = "变更前订单类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldOrderType { get; set; }

        /// <summary>变更后订单类型</summary>
        [SugarColumn(ColumnName = "new_order_type", ColumnDescription = "变更后订单类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewOrderType { get; set; }

        /// <summary>变更前订单状态</summary>
        [SugarColumn(ColumnName = "old_order_status", ColumnDescription = "变更前订单状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldOrderStatus { get; set; }

        /// <summary>变更后订单状态</summary>
        [SugarColumn(ColumnName = "new_order_status", ColumnDescription = "变更后订单状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewOrderStatus { get; set; }

        #endregion

        #region 价格信息变更

        /// <summary>变更前订单总金额</summary>
        [SugarColumn(ColumnName = "old_total_amount", ColumnDescription = "变更前订单总金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? OldTotalAmount { get; set; }

        /// <summary>变更后订单总金额</summary>
        [SugarColumn(ColumnName = "new_total_amount", ColumnDescription = "变更后订单总金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? NewTotalAmount { get; set; }

        /// <summary>变更前货币代码</summary>
        [SugarColumn(ColumnName = "old_currency_code", ColumnDescription = "变更前货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldCurrencyCode { get; set; }

        /// <summary>变更后货币代码</summary>
        [SugarColumn(ColumnName = "new_currency_code", ColumnDescription = "变更后货币代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewCurrencyCode { get; set; }

        /// <summary>变更前税率</summary>
        [SugarColumn(ColumnName = "old_tax_rate", ColumnDescription = "变更前税率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? OldTaxRate { get; set; }

        /// <summary>变更后税率</summary>
        [SugarColumn(ColumnName = "new_tax_rate", ColumnDescription = "变更后税率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? NewTaxRate { get; set; }

        /// <summary>变更前税额</summary>
        [SugarColumn(ColumnName = "old_tax_amount", ColumnDescription = "变更前税额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? OldTaxAmount { get; set; }

        /// <summary>变更后税额</summary>
        [SugarColumn(ColumnName = "new_tax_amount", ColumnDescription = "变更后税额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? NewTaxAmount { get; set; }

        /// <summary>变更前折扣率</summary>
        [SugarColumn(ColumnName = "old_discount_rate", ColumnDescription = "变更前折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? OldDiscountRate { get; set; }

        /// <summary>变更后折扣率</summary>
        [SugarColumn(ColumnName = "new_discount_rate", ColumnDescription = "变更后折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? NewDiscountRate { get; set; }

        /// <summary>变更前折扣金额</summary>
        [SugarColumn(ColumnName = "old_discount_amount", ColumnDescription = "变更前折扣金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? OldDiscountAmount { get; set; }

        /// <summary>变更后折扣金额</summary>
        [SugarColumn(ColumnName = "new_discount_amount", ColumnDescription = "变更后折扣金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? NewDiscountAmount { get; set; }

        #endregion

        #region 付款信息变更

        /// <summary>变更前付款条件</summary>
        [SugarColumn(ColumnName = "old_payment_terms", ColumnDescription = "变更前付款条件", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPaymentTerms { get; set; }

        /// <summary>变更后付款条件</summary>
        [SugarColumn(ColumnName = "new_payment_terms", ColumnDescription = "变更后付款条件", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPaymentTerms { get; set; }

        /// <summary>变更前付款方式</summary>
        [SugarColumn(ColumnName = "old_payment_method", ColumnDescription = "变更前付款方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldPaymentMethod { get; set; }

        /// <summary>变更后付款方式</summary>
        [SugarColumn(ColumnName = "new_payment_method", ColumnDescription = "变更后付款方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewPaymentMethod { get; set; }

        /// <summary>变更前账期天数</summary>
        [SugarColumn(ColumnName = "old_payment_days", ColumnDescription = "变更前账期天数", ColumnDataType = "int", IsNullable = true)]
        public int? OldPaymentDays { get; set; }

        /// <summary>变更后账期天数</summary>
        [SugarColumn(ColumnName = "new_payment_days", ColumnDescription = "变更后账期天数", ColumnDataType = "int", IsNullable = true)]
        public int? NewPaymentDays { get; set; }

        #endregion

        #region 交货信息变更

        /// <summary>变更前预计交货日期</summary>
        [SugarColumn(ColumnName = "old_expected_delivery_date", ColumnDescription = "变更前预计交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OldExpectedDeliveryDate { get; set; }

        /// <summary>变更后预计交货日期</summary>
        [SugarColumn(ColumnName = "new_expected_delivery_date", ColumnDescription = "变更后预计交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewExpectedDeliveryDate { get; set; }

        /// <summary>变更前实际交货日期</summary>
        [SugarColumn(ColumnName = "old_actual_delivery_date", ColumnDescription = "变更前实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? OldActualDeliveryDate { get; set; }

        /// <summary>变更后实际交货日期</summary>
        [SugarColumn(ColumnName = "new_actual_delivery_date", ColumnDescription = "变更后实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NewActualDeliveryDate { get; set; }

        /// <summary>变更前交货地址</summary>
        [SugarColumn(ColumnName = "old_delivery_address", ColumnDescription = "变更前交货地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldDeliveryAddress { get; set; }

        /// <summary>变更后交货地址</summary>
        [SugarColumn(ColumnName = "new_delivery_address", ColumnDescription = "变更后交货地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewDeliveryAddress { get; set; }

        #endregion

        #region 联系信息变更

        /// <summary>变更前联系人</summary>
        [SugarColumn(ColumnName = "old_contact_person", ColumnDescription = "变更前联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldContactPerson { get; set; }

        /// <summary>变更后联系人</summary>
        [SugarColumn(ColumnName = "new_contact_person", ColumnDescription = "变更后联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewContactPerson { get; set; }

        /// <summary>变更前联系电话</summary>
        [SugarColumn(ColumnName = "old_contact_phone", ColumnDescription = "变更前联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldContactPhone { get; set; }

        /// <summary>变更后联系电话</summary>
        [SugarColumn(ColumnName = "new_contact_phone", ColumnDescription = "变更后联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewContactPhone { get; set; }

        /// <summary>变更前紧急联系人</summary>
        [SugarColumn(ColumnName = "old_emergency_contact", ColumnDescription = "变更前紧急联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldEmergencyContact { get; set; }

        /// <summary>变更后紧急联系人</summary>
        [SugarColumn(ColumnName = "new_emergency_contact", ColumnDescription = "变更后紧急联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewEmergencyContact { get; set; }

        /// <summary>变更前紧急联系电话</summary>
        [SugarColumn(ColumnName = "old_emergency_phone", ColumnDescription = "变更前紧急联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldEmergencyPhone { get; set; }

        /// <summary>变更后紧急联系电话</summary>
        [SugarColumn(ColumnName = "new_emergency_phone", ColumnDescription = "变更后紧急联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NewEmergencyPhone { get; set; }

        #endregion

        #region 审批信息

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

        #endregion

        /// <summary>采购订单主表</summary>
        [Navigate(NavigateType.ManyToOne, nameof(PurchaseOrderCode))]
        public TaktPurchaseOrder? PurchaseOrder { get; set; }

        /// <summary>供应商信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(SupplierCode))]
        public TaktSupplier? Supplier { get; set; }
    }
} 
