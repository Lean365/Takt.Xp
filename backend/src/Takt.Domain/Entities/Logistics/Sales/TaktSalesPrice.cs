#nullable enable

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using Takt.Domain.Entities.Identity;
using Takt.Domain.Entities.Logistics.Material;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSalesPrice.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 销售价格实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售价格
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_price", "销售价格表")]
    [SugarIndex("ix_sales_price_customer_material", nameof(CustomerCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_sales_price", nameof(CompanyCode), OrderByType.Asc, nameof(CustomerCode), OrderByType.Asc, false)]
    public class TaktSalesPrice : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>价格编号</summary>
        [SugarColumn(ColumnName = "price_code", ColumnDescription = "价格编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PriceCode { get; set; } = string.Empty;

        /// <summary>价格类型(1=标准价格 2=特殊价格 3=促销价格 4=批量价格 5=季节价格)</summary>
        [SugarColumn(ColumnName = "price_type", ColumnDescription = "价格类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PriceType { get; set; } = 1;

        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerCode { get; set; }

        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>物料名称</summary>
        [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialName { get; set; }

        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantCode { get; set; }

        /// <summary>销售组织</summary>
        [SugarColumn(ColumnName = "sales_organization", ColumnDescription = "销售组织", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesOrganization { get; set; }

        /// <summary>分销渠道</summary>
        [SugarColumn(ColumnName = "distribution_channel", ColumnDescription = "分销渠道", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DistributionChannel { get; set; }

        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", ColumnDataType = "decimal(10,3)", IsNullable = false, DefaultValue = "1")]
        public decimal PriceUnit { get; set; } = 1;

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>计量单位</summary>
        [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "PCS")]
        public string UnitOfMeasure { get; set; } = "PCS";

        /// <summary>最小数量</summary>
        [SugarColumn(ColumnName = "min_quantity", ColumnDescription = "最小数量", ColumnDataType = "decimal(15,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinQuantity { get; set; } = 0;

        /// <summary>最大数量</summary>
        [SugarColumn(ColumnName = "max_quantity", ColumnDescription = "最大数量", ColumnDataType = "decimal(15,3)", IsNullable = true)]
        public decimal? MaxQuantity { get; set; }

        /// <summary>生效日期</summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime EffectiveDate { get; set; } = DateTime.Now.Date;

        /// <summary>失效日期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>价格状态(0=停用 1=正常 2=待审核)</summary>
        [SugarColumn(ColumnName = "price_status", ColumnDescription = "价格状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PriceStatus { get; set; } = 1;

        /// <summary>是否含税(0=不含税 1=含税)</summary>
        [SugarColumn(ColumnName = "is_tax_included", ColumnDescription = "是否含税", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsTaxIncluded { get; set; } = 1;

        /// <summary>税率</summary>
        [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "13")]
        public decimal TaxRate { get; set; } = 13;

        /// <summary>折扣率</summary>
        [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DiscountRate { get; set; } = 0;

        /// <summary>特殊说明</summary>
        [SugarColumn(ColumnName = "special_notes", ColumnDescription = "特殊说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialNotes { get; set; }

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

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>销售价格分段计价明细</summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktSalesPriceScale.PriceCode))]
        public List<TaktSalesPriceScale>? SalesPriceScales { get; set; }

        /// <summary>客户信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(CustomerCode))]
        public TaktCustomer? Customer { get; set; }
    }
} 



