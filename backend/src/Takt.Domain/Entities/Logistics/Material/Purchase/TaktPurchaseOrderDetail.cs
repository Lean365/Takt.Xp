#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 采购订单明细表实体类
    /// </summary>
    [SugarTable("Takt_logistics_purchase_order_detail", "采购订单明细表")]
    [SugarIndex("ix_purchase_order_detail_code", nameof(PurchaseOrderCode), OrderByType.Asc, nameof(LineNumber), OrderByType.Asc, true)]
    [SugarIndex("ix_material_purchase_order", nameof(MaterialCode), OrderByType.Asc, nameof(PurchaseOrderCode), OrderByType.Asc, false)]
    public class TaktPurchaseOrderDetail : TaktBaseEntity
    {
        /// <summary>采购订单号</summary>
        [SugarColumn(ColumnName = "purchase_order_code", ColumnDescription = "采购订单号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PurchaseOrderCode { get; set; } = string.Empty;

        /// <summary>行号</summary>
        [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false)]
        public int LineNumber { get; set; }

        /// <summary>物料代码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>物料描述</summary>
        [SugarColumn(ColumnName = "material_description", ColumnDescription = "物料描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialDescription { get; set; }

        /// <summary>规格型号</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "规格型号", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>订单数量</summary>
        [SugarColumn(ColumnName = "order_quantity", ColumnDescription = "订单数量", ColumnDataType = "decimal(18,3)", IsNullable = false)]
        public decimal OrderQuantity { get; set; }

        /// <summary>已收货数量</summary>
        [SugarColumn(ColumnName = "received_quantity", ColumnDescription = "已收货数量", ColumnDataType = "decimal(18,3)", IsNullable = false)]
        public decimal ReceivedQuantity { get; set; }

        /// <summary>未收货数量</summary>
        [SugarColumn(ColumnName = "unreceived_quantity", ColumnDescription = "未收货数量", ColumnDataType = "decimal(18,3)", IsNullable = false)]
        public decimal UnreceivedQuantity { get; set; }

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(18,4)", IsNullable = false)]
        public decimal UnitPrice { get; set; }

        /// <summary>折扣率</summary>
        [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? DiscountRate { get; set; }

        /// <summary>折扣后单价</summary>
        [SugarColumn(ColumnName = "discounted_unit_price", ColumnDescription = "折扣后单价", ColumnDataType = "decimal(18,4)", IsNullable = true)]
        public decimal? DiscountedUnitPrice { get; set; }

        /// <summary>税率</summary>
        [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? TaxRate { get; set; }

        /// <summary>税额</summary>
        [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? TaxAmount { get; set; }

        /// <summary>行项目金额</summary>
        [SugarColumn(ColumnName = "line_amount", ColumnDescription = "行项目金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal LineAmount { get; set; }

        /// <summary>含税行项目金额</summary>
        [SugarColumn(ColumnName = "tax_line_amount", ColumnDescription = "含税行项目金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? TaxLineAmount { get; set; }

        /// <summary>预计交货日期</summary>
        [SugarColumn(ColumnName = "expected_delivery_date", ColumnDescription = "预计交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        /// <summary>实际交货日期</summary>
        [SugarColumn(ColumnName = "actual_delivery_date", ColumnDescription = "实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>交货地址</summary>
        [SugarColumn(ColumnName = "delivery_address", ColumnDescription = "交货地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryAddress { get; set; }

        /// <summary>库存地点</summary>
        [SugarColumn(ColumnName = "storage_location", ColumnDescription = "库存地点", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageLocation { get; set; }

        /// <summary>批次管理</summary>
        [SugarColumn(ColumnName = "batch_management", ColumnDescription = "批次管理", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchManagement { get; set; }

        /// <summary>批次号</summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批次号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>供应商物料编号</summary>
        [SugarColumn(ColumnName = "supplier_material_number", ColumnDescription = "供应商物料编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialNumber { get; set; }

        /// <summary>供应商物料描述</summary>
        [SugarColumn(ColumnName = "supplier_material_description", ColumnDescription = "供应商物料描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierMaterialDescription { get; set; }

        /// <summary>信息记录号</summary>
        [SugarColumn(ColumnName = "info_record_number", ColumnDescription = "信息记录号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InfoRecordNumber { get; set; }

        /// <summary>采购申请号</summary>
        [SugarColumn(ColumnName = "purchase_requisition_number", ColumnDescription = "采购申请号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseRequisitionNumber { get; set; }

        /// <summary>采购申请行号</summary>
        [SugarColumn(ColumnName = "purchase_requisition_line", ColumnDescription = "采购申请行号", ColumnDataType = "int", IsNullable = true)]
        public int? PurchaseRequisitionLine { get; set; }

        /// <summary>项目编号</summary>
        [SugarColumn(ColumnName = "project_number", ColumnDescription = "项目编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectNumber { get; set; }

        /// <summary>成本中心</summary>
        [SugarColumn(ColumnName = "cost_center", ColumnDescription = "成本中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenter { get; set; }

        /// <summary>资产编号</summary>
        [SugarColumn(ColumnName = "asset_number", ColumnDescription = "资产编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AssetNumber { get; set; }

        /// <summary>订单状态</summary>
        [SugarColumn(ColumnName = "line_status", ColumnDescription = "订单状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LineStatus { get; set; } = string.Empty;


        /// <summary>采购订单主表</summary>
        [Navigate(NavigateType.ManyToOne, nameof(PurchaseOrderCode))]
        public TaktPurchaseOrder? PurchaseOrder { get; set; }
    }
} 
