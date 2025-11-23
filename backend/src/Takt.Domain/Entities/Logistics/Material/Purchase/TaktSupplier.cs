namespace Takt.Domain.Entities.Logistics.Material.Purchase
{
    /// <summary>
    /// 供应商主数据
    /// </summary>
    [SugarTable("Takt_logistics_purchase_supplier", "供应商信息")]
    [SugarIndex("ix_supplier_code", nameof(SupplierCode), OrderByType.Asc, true)]
    public class TaktSupplier : TaktBaseEntity
    {

        /// <summary>供应商代码</summary>
        [SugarColumn(ColumnName = "supplier_code", ColumnDescription = "供应商代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SupplierCode { get; set; } = string.Empty;
        /// <summary>供应商名称1</summary>
        [SugarColumn(ColumnName = "supplier_name1", ColumnDescription = "供应商名称1", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName1 { get; set; }
        /// <summary>供应商名称2</summary>
        [SugarColumn(ColumnName = "supplier_name2", ColumnDescription = "供应商名称2", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierName2 { get; set; }
        /// <summary>供应商简称</summary>
        [SugarColumn(ColumnName = "supplier_short_name", ColumnDescription = "供应商简称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierShortName { get; set; }
        /// <summary>国家代码</summary>
        [SugarColumn(ColumnName = "country_code", ColumnDescription = "国家代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CountryCode { get; set; }
        /// <summary>城市</summary>
        [SugarColumn(ColumnName = "city", ColumnDescription = "城市", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? City { get; set; }
        /// <summary>邮政编码</summary>
        [SugarColumn(ColumnName = "postal_code", ColumnDescription = "邮政编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PostalCode { get; set; }
        /// <summary>街道地址</summary>
        [SugarColumn(ColumnName = "street_address", ColumnDescription = "街道地址", Length = 70, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StreetAddress { get; set; }
        /// <summary>电话号码</summary>
        [SugarColumn(ColumnName = "phone_number", ColumnDescription = "电话号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PhoneNumber { get; set; }
        /// <summary>传真号码</summary>
        [SugarColumn(ColumnName = "fax_number", ColumnDescription = "传真号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaxNumber { get; set; }
        /// <summary>银行国家代码</summary>
        [SugarColumn(ColumnName = "bank_country_code", ColumnDescription = "银行国家代码", Length = 6, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankCountryCode { get; set; }
        /// <summary>银行代码</summary>
        [SugarColumn(ColumnName = "bank_code", ColumnDescription = "银行代码", Length = 15, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankCode { get; set; }
        /// <summary>银行账号</summary>
        [SugarColumn(ColumnName = "bank_account_number", ColumnDescription = "银行账号", Length = 18, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccountNumber { get; set; }
        /// <summary>采购组织代码</summary>
        [SugarColumn(ColumnName = "purchase_org_code", ColumnDescription = "采购组织代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrgCode { get; set; }

        /// <summary>供应商分组</summary>
        [SugarColumn(ColumnName = "supplier_group", ColumnDescription = "供应商分组", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierGroup { get; set; }
        /// <summary>采购组织中的删除标志</summary>
        [SugarColumn(ColumnName = "purchase_org_deletion_flag", ColumnDescription = "采购组织中的删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrgDeletionFlag { get; set; }
        /// <summary>工厂代码</summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantCode { get; set; }
        /// <summary>采购组织中的工厂删除标志</summary>
        [SugarColumn(ColumnName = "purchase_org_plant_deletion_flag", ColumnDescription = "采购组织中的工厂删除标志", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrgPlantDeletionFlag { get; set; }

        #region 供应商资质信息

        /// <summary>营业执照号码</summary>
        [SugarColumn(ColumnName = "business_license_number", ColumnDescription = "营业执照号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BusinessLicenseNumber { get; set; }

        /// <summary>营业执照有效期</summary>
        [SugarColumn(ColumnName = "business_license_valid_date", ColumnDescription = "营业执照有效期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BusinessLicenseValidDate { get; set; }

        /// <summary>税务登记证号码</summary>
        [SugarColumn(ColumnName = "tax_registration_number", ColumnDescription = "税务登记证号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaxRegistrationNumber { get; set; }

        /// <summary>组织机构代码证</summary>
        [SugarColumn(ColumnName = "organization_code", ColumnDescription = "组织机构代码证", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OrganizationCode { get; set; }

        /// <summary>开户许可证号码</summary>
        [SugarColumn(ColumnName = "bank_account_license", ColumnDescription = "开户许可证号码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccountLicense { get; set; }

        /// <summary>ISO认证证书</summary>
        [SugarColumn(ColumnName = "iso_certification", ColumnDescription = "ISO认证证书", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IsoCertification { get; set; }

        /// <summary>质量体系认证</summary>
        [SugarColumn(ColumnName = "quality_system_certification", ColumnDescription = "质量体系认证", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QualitySystemCertification { get; set; }

        /// <summary>环境体系认证</summary>
        [SugarColumn(ColumnName = "environmental_system_certification", ColumnDescription = "环境体系认证", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EnvironmentalSystemCertification { get; set; }

        /// <summary>职业健康安全认证</summary>
        [SugarColumn(ColumnName = "ohs_certification", ColumnDescription = "职业健康安全认证", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OhsCertification { get; set; }

        /// <summary>注册资本</summary>
        [SugarColumn(ColumnName = "registered_capital", ColumnDescription = "注册资本", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? RegisteredCapital { get; set; }

        /// <summary>注册资本币种</summary>
        [SugarColumn(ColumnName = "registered_capital_currency", ColumnDescription = "注册资本币种", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RegisteredCapitalCurrency { get; set; }

        /// <summary>成立日期</summary>
        [SugarColumn(ColumnName = "establishment_date", ColumnDescription = "成立日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>法定代表人</summary>
        [SugarColumn(ColumnName = "legal_representative", ColumnDescription = "法定代表人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LegalRepresentative { get; set; }

        /// <summary>经营范围</summary>
        [SugarColumn(ColumnName = "business_scope", ColumnDescription = "经营范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BusinessScope { get; set; }

        /// <summary>企业类型</summary>
        [SugarColumn(ColumnName = "enterprise_type", ColumnDescription = "企业类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EnterpriseType { get; set; }

        /// <summary>行业分类</summary>
        [SugarColumn(ColumnName = "industry_category", ColumnDescription = "行业分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IndustryCategory { get; set; }

        #endregion

        #region 供应商交易信息

        /// <summary>信用等级</summary>
        [SugarColumn(ColumnName = "credit_rating", ColumnDescription = "信用等级", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CreditRating { get; set; }

        /// <summary>信用额度</summary>
        [SugarColumn(ColumnName = "credit_limit", ColumnDescription = "信用额度", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? CreditLimit { get; set; }

        /// <summary>已用信用额度</summary>
        [SugarColumn(ColumnName = "used_credit_limit", ColumnDescription = "已用信用额度", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? UsedCreditLimit { get; set; }

        /// <summary>付款条件</summary>
        [SugarColumn(ColumnName = "payment_terms", ColumnDescription = "付款条件", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentTerms { get; set; }

        /// <summary>付款方式</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "付款方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PaymentMethod { get; set; }

        /// <summary>账期天数</summary>
        [SugarColumn(ColumnName = "payment_days", ColumnDescription = "账期天数", ColumnDataType = "int", IsNullable = true)]
        public int? PaymentDays { get; set; }

        /// <summary>折扣率</summary>
        [SugarColumn(ColumnName = "discount_rate", ColumnDescription = "折扣率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? DiscountRate { get; set; }

        /// <summary>税率</summary>
        [SugarColumn(ColumnName = "tax_rate", ColumnDescription = "税率", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? TaxRate { get; set; }

        /// <summary>供应商状态</summary>
        [SugarColumn(ColumnName = "supplier_status", ColumnDescription = "供应商状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierStatus { get; set; }

        /// <summary>供应商等级</summary>
        [SugarColumn(ColumnName = "supplier_level", ColumnDescription = "供应商等级", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplierLevel { get; set; }

        /// <summary>合作开始日期</summary>
        [SugarColumn(ColumnName = "cooperation_start_date", ColumnDescription = "合作开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CooperationStartDate { get; set; }

        /// <summary>合作结束日期</summary>
        [SugarColumn(ColumnName = "cooperation_end_date", ColumnDescription = "合作结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CooperationEndDate { get; set; }

        /// <summary>年度采购金额</summary>
        [SugarColumn(ColumnName = "annual_purchase_amount", ColumnDescription = "年度采购金额", ColumnDataType = "decimal(18,2)", IsNullable = true)]
        public decimal? AnnualPurchaseAmount { get; set; }

        /// <summary>年度采购金额币种</summary>
        [SugarColumn(ColumnName = "annual_purchase_currency", ColumnDescription = "年度采购金额币种", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AnnualPurchaseCurrency { get; set; }

        /// <summary>供应商评估分数</summary>
        [SugarColumn(ColumnName = "evaluation_score", ColumnDescription = "供应商评估分数", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? EvaluationScore { get; set; }

        /// <summary>最后评估日期</summary>
        [SugarColumn(ColumnName = "last_evaluation_date", ColumnDescription = "最后评估日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastEvaluationDate { get; set; }

        /// <summary>供应商联系人</summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "供应商联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>联系人电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系人电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>联系人邮箱</summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>紧急联系人</summary>
        [SugarColumn(ColumnName = "emergency_contact", ColumnDescription = "紧急联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyContact { get; set; }

        /// <summary>紧急联系电话</summary>
        [SugarColumn(ColumnName = "emergency_phone", ColumnDescription = "紧急联系电话", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyPhone { get; set; }

        #endregion
    }
}
