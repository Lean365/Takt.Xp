#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCompany.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 公司代码实体类 (基于SAP FI公司代码主数据)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Financial
{
    /// <summary>
    /// 公司代码实体类 (基于SAP FI公司代码主数据)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 公司代码主数据
    /// </remarks>
    [SugarTable("Takt_accounting_financial_company", "公司信息")]
    [SugarIndex("ix_company_code", nameof(CompanyCode), OrderByType.Asc, true)]
    public class TaktCompany : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 公司名称
        /// </summary>
        [SugarColumn(ColumnName = "company_name", ColumnDescription = "名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>
        /// 公司名称1
        /// </summary>
        [SugarColumn(ColumnName = "company_name1", ColumnDescription = "名称1", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyName1 { get; set; }

        /// <summary>
        /// 公司名称2
        /// </summary>
        [SugarColumn(ColumnName = "company_name2", ColumnDescription = "名称2", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyName2 { get; set; }

        /// <summary>
        /// 公司名称3
        /// </summary>
        [SugarColumn(ColumnName = "company_name3", ColumnDescription = "名称3", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyName3 { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        [SugarColumn(ColumnName = "company_short_name", ColumnDescription = "简称", Length = 25, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyShortName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        [SugarColumn(ColumnName = "company_address", ColumnDescription = "地址", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddress { get; set; }

        /// <summary>
        /// 公司地址1
        /// </summary>
        [SugarColumn(ColumnName = "company_address1", ColumnDescription = "地址1", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddress1 { get; set; }

        /// <summary>
        /// 公司地址2
        /// </summary>
        [SugarColumn(ColumnName = "company_address2", ColumnDescription = "地址2", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddress2 { get; set; }

        /// <summary>
        /// 公司地址3
        /// </summary>
        [SugarColumn(ColumnName = "company_address3", ColumnDescription = "地址3", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddress3 { get; set; }

        /// <summary>
        /// 公司城市
        /// </summary>
        [SugarColumn(ColumnName = "company_city", ColumnDescription = "城市", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyCity { get; set; }

        /// <summary>
        /// 公司地区/州
        /// </summary>
        [SugarColumn(ColumnName = "company_region", ColumnDescription = "地区/州", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyRegion { get; set; }

        /// <summary>
        /// 公司邮政编码
        /// </summary>
        [SugarColumn(ColumnName = "company_postal_code", ColumnDescription = "邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyPostalCode { get; set; }

        /// <summary>
        /// 公司国家代码
        /// </summary>
        [SugarColumn(ColumnName = "company_country", ColumnDescription = "国家代码", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyCountry { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        [SugarColumn(ColumnName = "company_phone", ColumnDescription = "电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyPhone { get; set; }

        /// <summary>
        /// 公司传真
        /// </summary>
        [SugarColumn(ColumnName = "company_fax", ColumnDescription = "传真", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyFax { get; set; }

        /// <summary>
        /// 公司邮箱
        /// </summary>
        [SugarColumn(ColumnName = "company_email", ColumnDescription = "邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyEmail { get; set; }

        /// <summary>
        /// 公司网站
        /// </summary>
        [SugarColumn(ColumnName = "company_website", ColumnDescription = "网站", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyWebsite { get; set; }

        /// <summary>
        /// 公司法人代表
        /// </summary>
        [SugarColumn(ColumnName = "company_legal_representative", ColumnDescription = "法人代表", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>
        /// 公司注册资本
        /// </summary>
        [SugarColumn(ColumnName = "company_registered_capital", ColumnDescription = "注册资本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>
        /// 成立日期
        /// </summary>
        [SugarColumn(ColumnName = "establishment_date", ColumnDescription = "成立日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>
        /// 注销日期
        /// </summary>
        [SugarColumn(ColumnName = "dissolution_date", ColumnDescription = "注销日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DissolutionDate { get; set; }

        /// <summary>
        /// 公司营业执照号
        /// </summary>
        [SugarColumn(ColumnName = "company_business_license", ColumnDescription = "营业执照号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>
        /// 公司税务登记号
        /// </summary>
        [SugarColumn(ColumnName = "company_tax_number", ColumnDescription = "税务登记号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyTaxNumber { get; set; }

        /// <summary>
        /// 公司组织机构代码
        /// </summary>
        [SugarColumn(ColumnName = "company_organization_code", ColumnDescription = "组织机构代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>
        /// 公司统一社会信用代码
        /// </summary>
        [SugarColumn(ColumnName = "company_unified_credit_code", ColumnDescription = "统一社会信用代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>
        /// 公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "company_industry_type", ColumnDescription = "行业类型", ColumnDataType = "int", IsNullable = true, DefaultValue = "1")]
        public int? CompanyIndustryType { get; set; }

        /// <summary>
        /// 公司规模
        /// </summary>
        [SugarColumn(ColumnName = "company_scale", ColumnDescription = "规模", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyScale { get; set; }

        /// <summary>
        /// 公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "company_nature", ColumnDescription = "性质", ColumnDataType = "int", IsNullable = true, DefaultValue = "3")]
        public int? CompanyNature { get; set; }

        /// <summary>
        /// 公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "company_currency", ColumnDescription = "币种", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>
        /// 公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "company_language", ColumnDescription = "语言代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>
        /// 公司会计年度变式
        /// </summary>
        [SugarColumn(ColumnName = "company_fiscal_year_variant", ColumnDescription = "会计年度变式", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>
        /// 公司科目表
        /// </summary>
        [SugarColumn(ColumnName = "company_chart_of_accounts", ColumnDescription = "科目表", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>
        /// 公司财务管理范围
        /// </summary>
        [SugarColumn(ColumnName = "company_financial_management_area", ColumnDescription = "财务管理范围", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>
        /// 公司最大汇率偏差幅度百分比
        /// </summary>
        [SugarColumn(ColumnName = "company_max_exchange_rate_deviation", ColumnDescription = "最大汇率偏差幅度百分比", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>
        /// 公司分配标识符
        /// </summary>
        [SugarColumn(ColumnName = "company_allocation_identifier", ColumnDescription = "分配标识符", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>
        /// 公司关联公司
        /// </summary>
        [SugarColumn(ColumnName = "company_related_company", ColumnDescription = "关联公司", Length = 12, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>
        /// 公司关联工厂
        /// </summary>
        [SugarColumn(ColumnName = "company_related_plant", ColumnDescription = "关联工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>
        /// 公司地址编号
        /// </summary>
        [SugarColumn(ColumnName = "company_address_number", ColumnDescription = "地址编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CompanyAddressNumber { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
}




