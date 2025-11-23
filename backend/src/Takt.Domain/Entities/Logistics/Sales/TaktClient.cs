#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktClient.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 客户（Client）实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 客户（Client）实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_client", "客户信息")]
    [SugarIndex("ix_client_code", nameof(ClientCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_client", nameof(CompanyCode), OrderByType.Asc, nameof(ClientCode), OrderByType.Asc, false)]
    public class TaktClient : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "client_code", ColumnDescription = "客户编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ClientCode { get; set; } = string.Empty;

        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "client_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ClientName { get; set; } = string.Empty;

        /// <summary>客户简称</summary>
        [SugarColumn(ColumnName = "client_short_name", ColumnDescription = "客户简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ClientShortName { get; set; }

        /// <summary>客户类型(1=个人 2=企业 3=政府 4=其他)</summary>
        [SugarColumn(ColumnName = "client_type", ColumnDescription = "客户类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ClientType { get; set; } = 2;

        /// <summary>客户等级(1=A级 2=B级 3=C级 4=D级)</summary>
        [SugarColumn(ColumnName = "client_level", ColumnDescription = "客户等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int ClientLevel { get; set; } = 3;

        /// <summary>客户状态(0=停用 1=正常 2=冻结)</summary>
        [SugarColumn(ColumnName = "client_status", ColumnDescription = "客户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ClientStatus { get; set; } = 1;

        /// <summary>联系人姓名</summary>
        [SugarColumn(ColumnName = "contact_name", ColumnDescription = "联系人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactName { get; set; }

        /// <summary>联系电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>联系邮箱</summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>传真号码</summary>
        [SugarColumn(ColumnName = "fax_number", ColumnDescription = "传真号码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaxNumber { get; set; }

        /// <summary>公司地址</summary>
        [SugarColumn(ColumnName = "company_address", ColumnDescription = "公司地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddress { get; set; }

        /// <summary>所在城市</summary>
        [SugarColumn(ColumnName = "city", ColumnDescription = "所在城市", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? City { get; set; }

        /// <summary>所在省份</summary>
        [SugarColumn(ColumnName = "province", ColumnDescription = "所在省份", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Province { get; set; }

        /// <summary>邮政编码</summary>
        [SugarColumn(ColumnName = "postal_code", ColumnDescription = "邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PostalCode { get; set; }

        /// <summary>国家代码</summary>
        [SugarColumn(ColumnName = "country_code", ColumnDescription = "国家代码", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CountryCode { get; set; }

        /// <summary>统一社会信用代码</summary>
        [SugarColumn(ColumnName = "unified_credit_code", ColumnDescription = "统一社会信用代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnifiedCreditCode { get; set; }

        /// <summary>税务登记号</summary>
        [SugarColumn(ColumnName = "tax_number", ColumnDescription = "税务登记号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaxNumber { get; set; }

        /// <summary>开户银行</summary>
        [SugarColumn(ColumnName = "bank_name", ColumnDescription = "开户银行", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankName { get; set; }

        /// <summary>银行账号</summary>
        [SugarColumn(ColumnName = "bank_account", ColumnDescription = "银行账号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccount { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>信用额度</summary>
        [SugarColumn(ColumnName = "credit_limit", ColumnDescription = "信用额度", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? CreditLimit { get; set; }

        /// <summary>已用信用额度</summary>
        [SugarColumn(ColumnName = "used_credit_limit", ColumnDescription = "已用信用额度", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UsedCreditLimit { get; set; } = 0;

        /// <summary>付款条件(天数)</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件(天数)", ColumnDataType = "int", IsNullable = false, DefaultValue = "30")]
        public int PaymentTerms { get; set; } = 30;

        /// <summary>销售区域</summary>
        [SugarColumn(ColumnName = "sales_region", ColumnDescription = "销售区域", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesRegion { get; set; }

        /// <summary>销售代表</summary>
        [SugarColumn(ColumnName = "sales_representative", ColumnDescription = "销售代表", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalesRepresentative { get; set; }

        /// <summary>客户来源(1=网络推广 2=展会 3=老客户介绍 4=电话营销 5=其他)</summary>
        [SugarColumn(ColumnName = "client_source", ColumnDescription = "客户来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
        public int ClientSource { get; set; } = 5;

        /// <summary>行业类型</summary>
        [SugarColumn(ColumnName = "industry_type", ColumnDescription = "行业类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IndustryType { get; set; }

        /// <summary>年营业额</summary>
        [SugarColumn(ColumnName = "annual_revenue", ColumnDescription = "年营业额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? AnnualRevenue { get; set; }

        /// <summary>员工人数</summary>
        [SugarColumn(ColumnName = "employee_count", ColumnDescription = "员工人数", ColumnDataType = "int", IsNullable = true)]
        public int? EmployeeCount { get; set; }

        /// <summary>成立日期</summary>
        [SugarColumn(ColumnName = "establishment_date", ColumnDescription = "成立日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>合作开始日期</summary>
        [SugarColumn(ColumnName = "cooperation_start_date", ColumnDescription = "合作开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CooperationStartDate { get; set; }

        /// <summary>最后交易日期</summary>
        [SugarColumn(ColumnName = "last_transaction_date", ColumnDescription = "最后交易日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastTransactionDate { get; set; }



        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



