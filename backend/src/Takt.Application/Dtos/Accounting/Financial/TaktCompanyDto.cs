//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktCompanyDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-09
// 版本号 : V0.0.1
// 描述   : 公司代码数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Accounting.Financial
{
    /// <summary>
    /// 公司代码基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-09
    /// 说明: 公司代码基础数据传输对象
    /// </remarks>
    public class TaktCompanyDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long CompanyId { get; set; }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>公司名称</summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>公司名称1</summary>
        public string? CompanyName1 { get; set; }

        /// <summary>公司名称2</summary>
        public string? CompanyName2 { get; set; }

        /// <summary>公司名称3</summary>
        public string? CompanyName3 { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司地址</summary>
        public string? CompanyAddress { get; set; }

        /// <summary>公司地址1</summary>
        public string? CompanyAddress1 { get; set; }

        /// <summary>公司地址2</summary>
        public string? CompanyAddress2 { get; set; }

        /// <summary>公司地址3</summary>
        public string? CompanyAddress3 { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司邮政编码</summary>
        public string? CompanyPostalCode { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司电话</summary>
        public string? CompanyPhone { get; set; }

        /// <summary>公司传真</summary>
        public string? CompanyFax { get; set; }

        /// <summary>公司邮箱</summary>
        public string? CompanyEmail { get; set; }

        /// <summary>公司网站</summary>
        public string? CompanyWebsite { get; set; }

        /// <summary>公司法人代表</summary>
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>公司注册资本</summary>
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>成立日期</summary>
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>注销日期</summary>
        public DateTime? DissolutionDate { get; set; }

        /// <summary>公司营业执照号</summary>
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>公司税务登记号</summary>
        public string? CompanyTaxNumber { get; set; }

        /// <summary>公司组织机构代码</summary>
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>公司统一社会信用代码</summary>
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司规模</summary>
        public string? CompanyScale { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>公司会计年度变式</summary>
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>公司科目表</summary>
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>公司财务管理范围</summary>
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>公司最大汇率偏差幅度百分比</summary>
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>公司分配标识符</summary>
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>公司关联公司</summary>
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>公司关联工厂</summary>
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>公司地址编号</summary>
        public string? CompanyAddressNumber { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }

        /// <summary>创建者</summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>更新者</summary>
        public string? UpdateBy { get; set; }

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>删除者</summary>
        public string? DeleteBy { get; set; }

        /// <summary>删除时间</summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>是否删除(0=未删除,1=已删除)</summary>
        public int IsDeleted { get; set; }

        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 公司代码查询参数DTO
    /// </summary>
    public class TaktCompanyQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyQueryDto()
        {
            CompanyCode = string.Empty;
            CompanyName = string.Empty;
            CompanyShortName = string.Empty;
        }

        /// <summary>公司代码</summary>
        public string? CompanyCode { get; set; }

        /// <summary>公司名称</summary>
        public string? CompanyName { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int? CompanyCurrency { get; set; }

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int? CompanyLanguage { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int? Status { get; set; }

        /// <summary>成立日期开始</summary>
        public DateTime? EstablishmentDateStart { get; set; }

        /// <summary>成立日期结束</summary>
        public DateTime? EstablishmentDateEnd { get; set; }
    }

    /// <summary>
    /// 公司代码创建参数DTO
    /// </summary>
    public class TaktCompanyCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyCreateDto()
        {
            CompanyCode = string.Empty;
            CompanyName = string.Empty;
        }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>公司名称</summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>公司名称1</summary>
        public string? CompanyName1 { get; set; }

        /// <summary>公司名称2</summary>
        public string? CompanyName2 { get; set; }

        /// <summary>公司名称3</summary>
        public string? CompanyName3 { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司地址</summary>
        public string? CompanyAddress { get; set; }

        /// <summary>公司地址1</summary>
        public string? CompanyAddress1 { get; set; }

        /// <summary>公司地址2</summary>
        public string? CompanyAddress2 { get; set; }

        /// <summary>公司地址3</summary>
        public string? CompanyAddress3 { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司邮政编码</summary>
        public string? CompanyPostalCode { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司电话</summary>
        public string? CompanyPhone { get; set; }

        /// <summary>公司传真</summary>
        public string? CompanyFax { get; set; }

        /// <summary>公司邮箱</summary>
        public string? CompanyEmail { get; set; }

        /// <summary>公司网站</summary>
        public string? CompanyWebsite { get; set; }

        /// <summary>公司法人代表</summary>
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>公司注册资本</summary>
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>成立日期</summary>
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>注销日期</summary>
        public DateTime? DissolutionDate { get; set; }

        /// <summary>公司营业执照号</summary>
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>公司税务登记号</summary>
        public string? CompanyTaxNumber { get; set; }

        /// <summary>公司组织机构代码</summary>
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>公司统一社会信用代码</summary>
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司规模</summary>
        public string? CompanyScale { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>公司会计年度变式</summary>
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>公司科目表</summary>
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>公司财务管理范围</summary>
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>公司最大汇率偏差幅度百分比</summary>
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>公司分配标识符</summary>
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>公司关联公司</summary>
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>公司关联工厂</summary>
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>公司地址编号</summary>
        public string? CompanyAddressNumber { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }

        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 公司代码更新参数DTO
    /// </summary>
    public class TaktCompanyUpdateDto : TaktCompanyCreateDto
    {
        /// <summary>公司ID</summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long CompanyId { get; set; }
    }

    /// <summary>
    /// 公司代码状态更新参数DTO
    /// </summary>
    public class TaktCompanyStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyStatusDto()
        {
        }

        /// <summary>公司ID</summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long CompanyId { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 公司代码模板DTO
    /// </summary>
    public class TaktCompanyTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyTemplateDto()
        {
            CompanyCode = string.Empty;
            CompanyName = string.Empty;
        }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>公司名称</summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>公司名称1</summary>
        public string? CompanyName1 { get; set; }

        /// <summary>公司名称2</summary>
        public string? CompanyName2 { get; set; }

        /// <summary>公司名称3</summary>
        public string? CompanyName3 { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司地址</summary>
        public string? CompanyAddress { get; set; }

        /// <summary>公司地址1</summary>
        public string? CompanyAddress1 { get; set; }

        /// <summary>公司地址2</summary>
        public string? CompanyAddress2 { get; set; }

        /// <summary>公司地址3</summary>
        public string? CompanyAddress3 { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司邮政编码</summary>
        public string? CompanyPostalCode { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司电话</summary>
        public string? CompanyPhone { get; set; }

        /// <summary>公司传真</summary>
        public string? CompanyFax { get; set; }

        /// <summary>公司邮箱</summary>
        public string? CompanyEmail { get; set; }

        /// <summary>公司网站</summary>
        public string? CompanyWebsite { get; set; }

        /// <summary>公司法人代表</summary>
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>公司注册资本</summary>
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>成立日期</summary>
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>注销日期</summary>
        public DateTime? DissolutionDate { get; set; }

        /// <summary>公司营业执照号</summary>
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>公司税务登记号</summary>
        public string? CompanyTaxNumber { get; set; }

        /// <summary>公司组织机构代码</summary>
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>公司统一社会信用代码</summary>
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司规模</summary>
        public string? CompanyScale { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>公司会计年度变式</summary>
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>公司科目表</summary>
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>公司财务管理范围</summary>
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>公司最大汇率偏差幅度百分比</summary>
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>公司分配标识符</summary>
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>公司关联公司</summary>
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>公司关联工厂</summary>
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>公司地址编号</summary>
        public string? CompanyAddressNumber { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }

        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 公司代码导入DTO
    /// </summary>
    public class TaktCompanyImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyImportDto()
        {
            CompanyCode = string.Empty;
            CompanyName = string.Empty;
        }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>公司名称</summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>公司名称1</summary>
        public string? CompanyName1 { get; set; }

        /// <summary>公司名称2</summary>
        public string? CompanyName2 { get; set; }

        /// <summary>公司名称3</summary>
        public string? CompanyName3 { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司地址</summary>
        public string? CompanyAddress { get; set; }

        /// <summary>公司地址1</summary>
        public string? CompanyAddress1 { get; set; }

        /// <summary>公司地址2</summary>
        public string? CompanyAddress2 { get; set; }

        /// <summary>公司地址3</summary>
        public string? CompanyAddress3 { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司邮政编码</summary>
        public string? CompanyPostalCode { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司电话</summary>
        public string? CompanyPhone { get; set; }

        /// <summary>公司传真</summary>
        public string? CompanyFax { get; set; }

        /// <summary>公司邮箱</summary>
        public string? CompanyEmail { get; set; }

        /// <summary>公司网站</summary>
        public string? CompanyWebsite { get; set; }

        /// <summary>公司法人代表</summary>
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>公司注册资本</summary>
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>成立日期</summary>
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>注销日期</summary>
        public DateTime? DissolutionDate { get; set; }

        /// <summary>公司营业执照号</summary>
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>公司税务登记号</summary>
        public string? CompanyTaxNumber { get; set; }

        /// <summary>公司组织机构代码</summary>
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>公司统一社会信用代码</summary>
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司规模</summary>
        public string? CompanyScale { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>公司会计年度变式</summary>
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>公司科目表</summary>
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>公司财务管理范围</summary>
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>公司最大汇率偏差幅度百分比</summary>
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>公司分配标识符</summary>
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>公司关联公司</summary>
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>公司关联工厂</summary>
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>公司地址编号</summary>
        public string? CompanyAddressNumber { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }

        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 公司代码导出DTO
    /// </summary>
    public class TaktCompanyExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktCompanyExportDto()
        {
            CompanyCode = string.Empty;
            CompanyName = string.Empty;
        }

        /// <summary>公司代码</summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>公司名称</summary>
        public string CompanyName { get; set; } = string.Empty;

        /// <summary>公司名称1</summary>
        public string? CompanyName1 { get; set; }

        /// <summary>公司名称2</summary>
        public string? CompanyName2 { get; set; }

        /// <summary>公司名称3</summary>
        public string? CompanyName3 { get; set; }

        /// <summary>公司简称</summary>
        public string? CompanyShortName { get; set; }

        /// <summary>公司地址</summary>
        public string? CompanyAddress { get; set; }

        /// <summary>公司地址1</summary>
        public string? CompanyAddress1 { get; set; }

        /// <summary>公司地址2</summary>
        public string? CompanyAddress2 { get; set; }

        /// <summary>公司地址3</summary>
        public string? CompanyAddress3 { get; set; }

        /// <summary>公司城市</summary>
        public string? CompanyCity { get; set; }

        /// <summary>公司地区/州</summary>
        public string? CompanyRegion { get; set; }

        /// <summary>公司邮政编码</summary>
        public string? CompanyPostalCode { get; set; }

        /// <summary>公司国家代码</summary>
        public string? CompanyCountry { get; set; }

        /// <summary>公司电话</summary>
        public string? CompanyPhone { get; set; }

        /// <summary>公司传真</summary>
        public string? CompanyFax { get; set; }

        /// <summary>公司邮箱</summary>
        public string? CompanyEmail { get; set; }

        /// <summary>公司网站</summary>
        public string? CompanyWebsite { get; set; }

        /// <summary>公司法人代表</summary>
        public string? CompanyLegalRepresentative { get; set; }

        /// <summary>公司注册资本</summary>
        public decimal? CompanyRegisteredCapital { get; set; }

        /// <summary>成立日期</summary>
        public DateTime? EstablishmentDate { get; set; }

        /// <summary>注销日期</summary>
        public DateTime? DissolutionDate { get; set; }

        /// <summary>公司营业执照号</summary>
        public string? CompanyBusinessLicense { get; set; }

        /// <summary>公司税务登记号</summary>
        public string? CompanyTaxNumber { get; set; }

        /// <summary>公司组织机构代码</summary>
        public string? CompanyOrganizationCode { get; set; }

        /// <summary>公司统一社会信用代码</summary>
        public string? CompanyUnifiedCreditCode { get; set; }

        /// <summary>公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他)</summary>
        public int? CompanyIndustryType { get; set; }

        /// <summary>公司规模</summary>
        public string? CompanyScale { get; set; }

        /// <summary>公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他)</summary>
        public int? CompanyNature { get; set; }

        /// <summary>公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他)</summary>
        public int CompanyCurrency { get; set; } = 0;

        /// <summary>公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他)</summary>
        public int CompanyLanguage { get; set; } = 0;

        /// <summary>公司会计年度变式</summary>
        public string? CompanyFiscalYearVariant { get; set; }

        /// <summary>公司科目表</summary>
        public string? CompanyChartOfAccounts { get; set; }

        /// <summary>公司财务管理范围</summary>
        public string? CompanyFinancialManagementArea { get; set; }

        /// <summary>公司最大汇率偏差幅度百分比</summary>
        public decimal? CompanyMaxExchangeRateDeviation { get; set; }

        /// <summary>公司分配标识符</summary>
        public string? CompanyAllocationIdentifier { get; set; }

        /// <summary>公司关联公司</summary>
        public string? CompanyRelatedCompany { get; set; }

        /// <summary>公司关联工厂</summary>
        public string? CompanyRelatedPlant { get; set; }

        /// <summary>公司地址编号</summary>
        public string? CompanyAddressNumber { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; }

        /// <summary>状态(0=正常 1=停用)</summary>
        public int Status { get; set; }

        /// <summary>创建者</summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>更新者</summary>
        public string? UpdateBy { get; set; }

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>备注</summary>
        public string? Remark { get; set; }
    }
}

