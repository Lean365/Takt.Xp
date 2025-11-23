import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 公司实体
 */
export interface TaktCompany extends TaktBaseEntity {
  /** 公司ID */
  companyId: string
  /** 公司代码 */
  companyCode: string
  /** 公司名称 */
  companyName: string
  /** 公司名称1 */
  companyName1?: string
  /** 公司名称2 */
  companyName2?: string
  /** 公司名称3 */
  companyName3?: string
  /** 公司简称 */
  companyShortName?: string
  /** 公司地址 */
  companyAddress?: string
  /** 公司地址1 */
  companyAddress1?: string
  /** 公司地址2 */
  companyAddress2?: string
  /** 公司地址3 */
  companyAddress3?: string
  /** 公司城市 */
  companyCity?: string
  /** 公司地区/州 */
  companyRegion?: string
  /** 公司邮政编码 */
  companyPostalCode?: string
  /** 公司国家代码 */
  companyCountry?: string
  /** 公司电话 */
  companyPhone?: string
  /** 公司传真 */
  companyFax?: string
  /** 公司邮箱 */
  companyEmail?: string
  /** 公司网站 */
  companyWebsite?: string
  /** 公司法人代表 */
  companyLegalRepresentative?: string
  /** 公司注册资本 */
  companyRegisteredCapital?: number
  /** 成立日期 */
  establishmentDate?: string
  /** 注销日期 */
  dissolutionDate?: string
  /** 公司营业执照号 */
  companyBusinessLicense?: string
  /** 公司税务登记号 */
  companyTaxNumber?: string
  /** 公司组织机构代码 */
  companyOrganizationCode?: string
  /** 公司统一社会信用代码 */
  companyUnifiedCreditCode?: string
  /** 公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他) */
  companyIndustryType?: number
  /** 公司规模 */
  companyScale?: string
  /** 公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他) */
  companyNature?: number
  /** 公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他) */
  companyCurrency: number
  /** 公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他) */
  companyLanguage: number
  /** 公司会计年度变式 */
  companyFiscalYearVariant?: string
  /** 公司科目表 */
  companyChartOfAccounts?: string
  /** 公司财务管理范围 */
  companyFinancialManagementArea?: string
  /** 公司最大汇率偏差幅度百分比 */
  companyMaxExchangeRateDeviation?: number
  /** 公司分配标识符 */
  companyAllocationIdentifier?: string
  /** 公司关联公司 */
  companyRelatedCompany?: string
  /** 公司关联工厂 */
  companyRelatedPlant?: string
  /** 公司地址编号 */
  companyAddressNumber?: string
  /** 排序号 */
  orderNum: number
  /** 状态(0=正常 1=停用) */
  status: number
  /** 状态变更加载状态 */
  statusLoading?: boolean
}

/**
 * 公司查询参数
 */
export interface TaktCompanyQuery extends TaktPagedQuery {
  /** 公司代码 */
  companyCode?: string
  /** 公司名称 */
  companyName?: string
  /** 公司简称 */
  companyShortName?: string
  /** 公司税务登记号 */
  companyTaxNumber?: string
  /** 状态(0=正常 1=停用) */
  status?: number
  /** 公司行业类型 */
  companyIndustryType?: number
  /** 公司性质 */
  companyNature?: number
  /** 公司币种 */
  companyCurrency?: number
  /** 公司语言代码 */
  companyLanguage?: number
}

/**
 * 公司创建参数
 */
export interface TaktCompanyCreate {
  /** 公司代码 */
  companyCode: string
  /** 公司名称 */
  companyName: string
  /** 公司名称1 */
  companyName1?: string
  /** 公司名称2 */
  companyName2?: string
  /** 公司名称3 */
  companyName3?: string
  /** 公司简称 */
  companyShortName?: string
  /** 公司地址 */
  companyAddress?: string
  /** 公司地址1 */
  companyAddress1?: string
  /** 公司地址2 */
  companyAddress2?: string
  /** 公司地址3 */
  companyAddress3?: string
  /** 公司城市 */
  companyCity?: string
  /** 公司地区/州 */
  companyRegion?: string
  /** 公司邮政编码 */
  companyPostalCode?: string
  /** 公司国家代码 */
  companyCountry?: string
  /** 公司电话 */
  companyPhone?: string
  /** 公司传真 */
  companyFax?: string
  /** 公司邮箱 */
  companyEmail?: string
  /** 公司网站 */
  companyWebsite?: string
  /** 公司法人代表 */
  companyLegalRepresentative?: string
  /** 公司注册资本 */
  companyRegisteredCapital?: number
  /** 成立日期 */
  establishmentDate?: string
  /** 注销日期 */
  dissolutionDate?: string
  /** 公司营业执照号 */
  companyBusinessLicense?: string
  /** 公司税务登记号 */
  companyTaxNumber?: string
  /** 公司组织机构代码 */
  companyOrganizationCode?: string
  /** 公司统一社会信用代码 */
  companyUnifiedCreditCode?: string
  /** 公司行业类型(1=制造业 2=服务业 3=贸易业 4=金融业 5=其他) */
  companyIndustryType?: number
  /** 公司规模 */
  companyScale?: string
  /** 公司性质(1=国有企业 2=民营企业 3=外资企业 4=合资企业 5=其他) */
  companyNature?: number
  /** 公司币种(0=人民币 1=美元 2=欧元 3=日元 4=港币 5=其他) */
  companyCurrency: number
  /** 公司语言代码(0=中文 1=英文 2=日文 3=德文 4=法文 5=其他) */
  companyLanguage: number
  /** 公司会计年度变式 */
  companyFiscalYearVariant?: string
  /** 公司科目表 */
  companyChartOfAccounts?: string
  /** 公司财务管理范围 */
  companyFinancialManagementArea?: string
  /** 公司最大汇率偏差幅度百分比 */
  companyMaxExchangeRateDeviation?: number
  /** 公司分配标识符 */
  companyAllocationIdentifier?: string
  /** 公司关联公司 */
  companyRelatedCompany?: string
  /** 公司关联工厂 */
  companyRelatedPlant?: string
  /** 公司地址编号 */
  companyAddressNumber?: string
  /** 排序号 */
  orderNum: number
  /** 状态(0=正常 1=停用) */
  status: number
  /** 备注 */
  remark?: string
}

/**
 * 公司更新参数
 */
export interface TaktCompanyUpdate extends TaktCompanyCreate {
  /** 公司ID */
  companyId: string
}

/**
 * 公司状态更新参数
 */
export interface TaktCompanyStatus {
  /** 公司ID */
  companyId: string
  /** 状态(0=正常 1=停用) */
  status: number
}

/**
 * 公司分页结果
 */
export interface TaktCompanyPagedResult extends TaktPagedResult<TaktCompany> {}
