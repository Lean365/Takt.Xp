#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSalesOrderDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 销售订单明细表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售订单明细表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_order_detail", "销售订单明细表")]
    [SugarIndex("ix_sales_order_detail_id", nameof(SalesOrderId), OrderByType.Asc, nameof(LineNumber), OrderByType.Asc, true)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    public class TaktSalesOrderDetail : TaktBaseEntity
    {
        /// <summary>销售订单ID</summary>
        [SugarColumn(ColumnName = "sales_order_id", ColumnDescription = "销售订单ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SalesOrderId { get; set; }

        /// <summary>销售订单编号</summary>
        [SugarColumn(ColumnName = "sales_order_code", ColumnDescription = "销售订单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SalesOrderCode { get; set; } = string.Empty;

        /// <summary>行项目号</summary>
        [SugarColumn(ColumnName = "line_number", ColumnDescription = "行项目号", ColumnDataType = "int", IsNullable = false)]
        public int LineNumber { get; set; }

        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>物料名称</summary>
        [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialName { get; set; }

        /// <summary>物料规格</summary>
        [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialSpecification { get; set; }

        /// <summary>物料型号</summary>
        [SugarColumn(ColumnName = "material_model", ColumnDescription = "物料型号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialModel { get; set; }

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantCode { get; set; }

        /// <summary>库存地点</summary>
        [SugarColumn(ColumnName = "storage_location", ColumnDescription = "库存地点", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageLocation { get; set; }

        /// <summary>订单数量</summary>
        [SugarColumn(ColumnName = "order_quantity", ColumnDescription = "订单数量", ColumnDataType = "decimal(15,3)", IsNullable = false, DefaultValue = "0")]
        public decimal OrderQuantity { get; set; } = 0;

        /// <summary>已发货数量</summary>
        [SugarColumn(ColumnName = "delivered_quantity", ColumnDescription = "已发货数量", ColumnDataType = "decimal(15,3)", IsNullable = false, DefaultValue = "0")]
        public decimal DeliveredQuantity { get; set; } = 0;

        /// <summary>已收货数量</summary>
        [SugarColumn(ColumnName = "received_quantity", ColumnDescription = "已收货数量", ColumnDataType = "decimal(15,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ReceivedQuantity { get; set; } = 0;

        /// <summary>计量单位</summary>
        [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "PCS")]
        public string UnitOfMeasure { get; set; } = "PCS";

        /// <summary>单价(不含税)</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价(不含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>税率</summary>
        [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "13")]
        public decimal TaxRate { get; set; } = 13;

        /// <summary>税额</summary>
        [SugarColumn(ColumnName = "tax_amount", ColumnDescription = "税额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TaxAmount { get; set; } = 0;

        /// <summary>单价(含税)</summary>
        [SugarColumn(ColumnName = "unit_price_with_tax", ColumnDescription = "单价(含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPriceWithTax { get; set; } = 0;

        /// <summary>行项目金额(不含税)</summary>
        [SugarColumn(ColumnName = "line_amount", ColumnDescription = "行项目金额(不含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal LineAmount { get; set; } = 0;

        /// <summary>行项目金额(含税)</summary>
        [SugarColumn(ColumnName = "line_amount_with_tax", ColumnDescription = "行项目金额(含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal LineAmountWithTax { get; set; } = 0;

        /// <summary>折扣率</summary>
        [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DiscountRate { get; set; } = 0;

        /// <summary>折扣金额</summary>
        [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DiscountAmount { get; set; } = 0;

        /// <summary>净金额(不含税)</summary>
        [SugarColumn(ColumnName = "net_amount", ColumnDescription = "净金额(不含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NetAmount { get; set; } = 0;

        /// <summary>净金额(含税)</summary>
        [SugarColumn(ColumnName = "net_amount_with_tax", ColumnDescription = "净金额(含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NetAmountWithTax { get; set; } = 0;

        /// <summary>要求交货日期</summary>
        [SugarColumn(ColumnName = "requested_delivery_date", ColumnDescription = "要求交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? RequestedDeliveryDate { get; set; }

        /// <summary>承诺交货日期</summary>
        [SugarColumn(ColumnName = "promised_delivery_date", ColumnDescription = "承诺交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PromisedDeliveryDate { get; set; }

        /// <summary>实际交货日期</summary>
        [SugarColumn(ColumnName = "actual_delivery_date", ColumnDescription = "实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>批次号</summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批次号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>序列号</summary>
        [SugarColumn(ColumnName = "serial_number", ColumnDescription = "序列号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNumber { get; set; }

        /// <summary>项目类型(1=库存项目 2=非库存项目 3=服务项目 4=文本项目)</summary>
        [SugarColumn(ColumnName = "item_type", ColumnDescription = "项目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ItemType { get; set; } = 1;

        /// <summary>是否免费项目(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_free_item", ColumnDescription = "是否免费项目", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsFreeItem { get; set; } = 0;

        /// <summary>是否紧急项目(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent_item", ColumnDescription = "是否紧急项目", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgentItem { get; set; } = 0;

        /// <summary>项目状态(0=草稿 1=已确认 2=生产中 3=已发货 4=已收货 5=已完成 6=已取消)</summary>
        [SugarColumn(ColumnName = "item_status", ColumnDescription = "项目状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ItemStatus { get; set; } = 0;

        /// <summary>生产工单号</summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdOrderCode { get; set; }

        /// <summary>发货单号</summary>
        [SugarColumn(ColumnName = "delivery_number", ColumnDescription = "发货单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryNumber { get; set; }

        /// <summary>发票号</summary>
        [SugarColumn(ColumnName = "invoice_number", ColumnDescription = "发票号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvoiceNumber { get; set; }

        /// <summary>成本中心</summary>
        [SugarColumn(ColumnName = "cost_center", ColumnDescription = "成本中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenter { get; set; }

        /// <summary>利润中心</summary>
        [SugarColumn(ColumnName = "profit_center", ColumnDescription = "利润中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenter { get; set; }

        /// <summary>销售区域</summary>
        [SugarColumn(ColumnName = "sales_region", ColumnDescription = "销售区域", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesRegion { get; set; }

        /// <summary>客户参考号</summary>
        [SugarColumn(ColumnName = "customer_reference", ColumnDescription = "客户参考号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerReference { get; set; }

        /// <summary>内部参考号</summary>
        [SugarColumn(ColumnName = "internal_reference", ColumnDescription = "内部参考号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalReference { get; set; }

        /// <summary>特殊要求</summary>
        [SugarColumn(ColumnName = "special_requirements", ColumnDescription = "特殊要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialRequirements { get; set; }

        /// <summary>技术规格</summary>
        [SugarColumn(ColumnName = "technical_specifications", ColumnDescription = "技术规格", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalSpecifications { get; set; }

        /// <summary>质量要求</summary>
        [SugarColumn(ColumnName = "quality_requirements", ColumnDescription = "质量要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualityRequirements { get; set; }

        /// <summary>包装要求</summary>
        [SugarColumn(ColumnName = "packaging_requirements", ColumnDescription = "包装要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PackagingRequirements { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "line_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LineRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 销售订单主表
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SalesOrderId))]
        public TaktSalesOrder? SalesOrder { get; set; }
    }
} 



