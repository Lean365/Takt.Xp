#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSalesOrder.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 销售订单主表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售订单主表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_order", "销售订单主表")]
    [SugarIndex("ix_sales_order_code", nameof(SalesOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_sales_order", nameof(CompanyCode), OrderByType.Asc, nameof(SalesOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_customer_sales_order", nameof(CustomerCode), OrderByType.Asc, nameof(SalesOrderCode), OrderByType.Asc, false)]
    public class TaktSalesOrder : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>销售订单编号</summary>
        [SugarColumn(ColumnName = "sales_order_code", ColumnDescription = "销售订单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SalesOrderCode { get; set; } = string.Empty;

        /// <summary>销售订单类型(1=标准销售 2=寄售销售 3=退货销售 4=样品销售 5=促销销售)</summary>
        [SugarColumn(ColumnName = "sales_order_type", ColumnDescription = "销售订单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SalesOrderType { get; set; } = 1;

        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>联系人姓名</summary>
        [SugarColumn(ColumnName = "contact_name", ColumnDescription = "联系人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactName { get; set; }

        /// <summary>联系电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>联系邮箱</summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>销售组织</summary>
        [SugarColumn(ColumnName = "sales_organization", ColumnDescription = "销售组织", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesOrganization { get; set; }

        /// <summary>分销渠道</summary>
        [SugarColumn(ColumnName = "distribution_channel", ColumnDescription = "分销渠道", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DistributionChannel { get; set; }

        /// <summary>销售代表</summary>
        [SugarColumn(ColumnName = "sales_representative", ColumnDescription = "销售代表", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesRepresentative { get; set; }

        /// <summary>订单日期</summary>
        [SugarColumn(ColumnName = "order_date", ColumnDescription = "订单日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime OrderDate { get; set; } = DateTime.Now.Date;

        /// <summary>要求交货日期</summary>
        [SugarColumn(ColumnName = "requested_delivery_date", ColumnDescription = "要求交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? RequestedDeliveryDate { get; set; }

        /// <summary>承诺交货日期</summary>
        [SugarColumn(ColumnName = "promised_delivery_date", ColumnDescription = "承诺交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PromisedDeliveryDate { get; set; }

        /// <summary>实际交货日期</summary>
        [SugarColumn(ColumnName = "actual_delivery_date", ColumnDescription = "实际交货日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualDeliveryDate { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>汇率</summary>
        [SugarColumn(ColumnName = "exchange_rate", ColumnDescription = "汇率", ColumnDataType = "decimal(10,4)", IsNullable = false, DefaultValue = "1")]
        public decimal ExchangeRate { get; set; } = 1;

        /// <summary>订单总金额(不含税)</summary>
        [SugarColumn(ColumnName = "order_total_amount", ColumnDescription = "订单总金额(不含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OrderTotalAmount { get; set; } = 0;

        /// <summary>订单总税额</summary>
        [SugarColumn(ColumnName = "order_total_tax", ColumnDescription = "订单总税额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OrderTotalTax { get; set; } = 0;

        /// <summary>订单总金额(含税)</summary>
        [SugarColumn(ColumnName = "order_total_amount_with_tax", ColumnDescription = "订单总金额(含税)", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OrderTotalAmountWithTax { get; set; } = 0;

        /// <summary>折扣金额</summary>
        [SugarColumn(ColumnName = "discount_amount", ColumnDescription = "折扣金额", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DiscountAmount { get; set; } = 0;

        /// <summary>运费</summary>
        [SugarColumn(ColumnName = "freight_amount", ColumnDescription = "运费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal FreightAmount { get; set; } = 0;

        /// <summary>付款条件(天数)</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件(天数)", ColumnDataType = "int", IsNullable = false, DefaultValue = "30")]
        public int PaymentTerms { get; set; } = 30;

        /// <summary>付款方式(1=现金 2=银行转账 3=支票 4=信用证 5=其他)</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int PaymentMethod { get; set; } = 2;

        /// <summary>发货地址</summary>
        [SugarColumn(ColumnName = "delivery_address", ColumnDescription = "发货地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryAddress { get; set; }

        /// <summary>发货城市</summary>
        [SugarColumn(ColumnName = "delivery_city", ColumnDescription = "发货城市", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryCity { get; set; }

        /// <summary>发货省份</summary>
        [SugarColumn(ColumnName = "delivery_province", ColumnDescription = "发货省份", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryProvince { get; set; }

        /// <summary>发货邮政编码</summary>
        [SugarColumn(ColumnName = "delivery_postal_code", ColumnDescription = "发货邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryPostalCode { get; set; }

        /// <summary>收货人姓名</summary>
        [SugarColumn(ColumnName = "receiver_name", ColumnDescription = "收货人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReceiverName { get; set; }

        /// <summary>收货人电话</summary>
        [SugarColumn(ColumnName = "receiver_phone", ColumnDescription = "收货人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReceiverPhone { get; set; }

        /// <summary>运输方式(1=公路运输 2=铁路运输 3=航空运输 4=海运 5=快递 6=自提)</summary>
        [SugarColumn(ColumnName = "transport_mode", ColumnDescription = "运输方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int TransportMode { get; set; } = 1;

        /// <summary>承运商</summary>
        [SugarColumn(ColumnName = "carrier", ColumnDescription = "承运商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Carrier { get; set; }

        /// <summary>订单状态(0=草稿 1=待审批 2=已审批 3=已确认 4=生产中 5=已发货 6=已收货 7=已完成 8=已取消)</summary>
        [SugarColumn(ColumnName = "order_status", ColumnDescription = "订单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderStatus { get; set; } = 0;

        /// <summary>审批状态(0=未审批 1=已审批 2=已拒绝)</summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; } = 0;

        /// <summary>审批人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>审批日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>审批意见</summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalRemarks { get; set; }

        /// <summary>订单优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "order_priority", ColumnDescription = "订单优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int OrderPriority { get; set; } = 2;

        /// <summary>是否紧急订单(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急订单", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>是否加急订单(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_rush", ColumnDescription = "是否加急订单", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRush { get; set; } = 0;

        /// <summary>客户参考号</summary>
        [SugarColumn(ColumnName = "customer_reference", ColumnDescription = "客户参考号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerReference { get; set; }

        /// <summary>内部参考号</summary>
        [SugarColumn(ColumnName = "internal_reference", ColumnDescription = "内部参考号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalReference { get; set; }

        /// <summary>特殊要求</summary>
        [SugarColumn(ColumnName = "special_requirements", ColumnDescription = "特殊要求", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialRequirements { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "order_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OrderRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 销售订单明细
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktSalesOrderDetail.SalesOrderId))]
        public List<TaktSalesOrderDetail>? OrderDetails { get; set; }
    }
} 



