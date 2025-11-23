#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售价格分段计价子表实体类
    /// </summary>
    [SugarTable("Takt_logistics_sales_price_scale", "销售价格分段计价表")]
    [SugarIndex("ix_sales_price_scale_code", nameof(PriceCode), OrderByType.Asc, nameof(ScaleNumber), OrderByType.Asc, true)]
    [SugarIndex("ix_customer_material_scale", nameof(CustomerCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, nameof(ScaleNumber), OrderByType.Asc, false)]
    public class TaktSalesPriceScale : TaktBaseEntity
    {
        /// <summary>价格代码</summary>
        [SugarColumn(ColumnName = "price_code", ColumnDescription = "价格代码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PriceCode { get; set; } = string.Empty;

        /// <summary>分段编号</summary>
        [SugarColumn(ColumnName = "scale_number", ColumnDescription = "分段编号", ColumnDataType = "int", IsNullable = false)]
        public int ScaleNumber { get; set; }

        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>物料代码</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>销售组织代码</summary>
        [SugarColumn(ColumnName = "sales_org_code", ColumnDescription = "销售组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesOrgCode { get; set; }

        /// <summary>分销渠道</summary>
        [SugarColumn(ColumnName = "distribution_channel", ColumnDescription = "分销渠道", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DistributionChannel { get; set; }

        /// <summary>产品组</summary>
        [SugarColumn(ColumnName = "product_group", ColumnDescription = "产品组", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductGroup { get; set; }

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

        /// <summary>销售单位</summary>
        [SugarColumn(ColumnName = "sales_unit", ColumnDescription = "销售单位", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SalesUnit { get; set; } = string.Empty;

        /// <summary>有效开始日期</summary>
        [SugarColumn(ColumnName = "valid_from_date", ColumnDescription = "有效开始日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ValidFromDate { get; set; }

        /// <summary>有效结束日期</summary>
        [SugarColumn(ColumnName = "valid_to_date", ColumnDescription = "有效结束日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime ValidToDate { get; set; }

        /// <summary>价格单位</summary>
        [SugarColumn(ColumnName = "price_unit", ColumnDescription = "价格单位", ColumnDataType = "decimal(18,3)", IsNullable = true)]
        public decimal? PriceUnit { get; set; }

        /// <summary>分段类型</summary>
        [SugarColumn(ColumnName = "scale_type", ColumnDescription = "分段类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScaleType { get; set; }

        /// <summary>分段描述</summary>
        [SugarColumn(ColumnName = "scale_description", ColumnDescription = "分段描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScaleDescription { get; set; }

        /// <summary>客户等级</summary>
        [SugarColumn(ColumnName = "customer_level", ColumnDescription = "客户等级", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerLevel { get; set; }

        /// <summary>客户类型</summary>
        [SugarColumn(ColumnName = "customer_type", ColumnDescription = "客户类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerType { get; set; }

        /// <summary>价格类型</summary>
        [SugarColumn(ColumnName = "price_type", ColumnDescription = "价格类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriceType { get; set; }

        /// <summary>是否启用</summary>
        [SugarColumn(ColumnName = "is_enabled", ColumnDescription = "是否启用", ColumnDataType = "bit", IsNullable = false)]
        public bool IsEnabled { get; set; } = true;

        /// <summary>优先级</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = true)]
        public int? Priority { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "scale_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ScaleRemark { get; set; }

        /// <summary>销售价格主表</summary>
        [Navigate(NavigateType.ManyToOne, nameof(PriceCode))]
        public TaktSalesPrice? SalesPrice { get; set; }

        /// <summary>客户信息</summary>
        [Navigate(NavigateType.ManyToOne, nameof(CustomerCode))]
        public TaktCustomer? Customer { get; set; }
    }
} 
