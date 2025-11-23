//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFinanceDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 财务相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedFinanceDictData
{
    /// <summary>
    /// 获取财务相关字典数据
    /// </summary>
    /// <returns>财务相关字典数据列表</returns>
    public List<TaktDictData> GetFinanceDictData()
    {
        return new List<TaktDictData>
        {
            // ===== 基础财务字典 =====
            // 币种 - 横排格式
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "人民币", DictValue = "CNY", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "人民币", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "美元", DictValue = "USD", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "美元", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "欧元", DictValue = "EUR", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "欧元", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "日元", DictValue = "JPY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "日元", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "港币", DictValue = "HKD", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "港币", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "英镑", DictValue = "GBP", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "英镑", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_currency_category", DictLabel = "澳元", DictValue = "AUD", OrderNum = 7,  CssClass = 2, ListClass = 2, Remark = "澳元", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 汇率 - 横排格式
            new TaktDictData { DictType = "fico_exchange_rate", DictLabel = "即期汇率", DictValue = "SPOT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "即期汇率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_exchange_rate", DictLabel = "远期汇率", DictValue = "FORWARD", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "远期汇率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_exchange_rate", DictLabel = "中间汇率", DictValue = "MIDDLE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "中间汇率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_exchange_rate", DictLabel = "买入汇率", DictValue = "BUY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "买入汇率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_exchange_rate", DictLabel = "卖出汇率", DictValue = "SELL", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "卖出汇率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 税率 - 横排格式
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "0%", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "零税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "1%", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "1%税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "3%", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "3%税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "6%", DictValue = "6", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "6%税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "9%", DictValue = "9", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "9%税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_tax_rate", DictLabel = "13%", DictValue = "13", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "13%税率", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // ===== 付款相关字典 =====

            // 付款条件 - 横排格式
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "立即付款", DictValue = "IMMEDIATE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "立即付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "货到付款", DictValue = "COD", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "货到付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "月结30天", DictValue = "NET30", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "月结30天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "月结60天", DictValue = "NET60", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "月结60天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "月结90天", DictValue = "NET90", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "月结90天", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_terms", DictLabel = "预付款", DictValue = "PREPAID", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "预付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 付款方式 - 横排格式
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "现金", DictValue = "CASH", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "现金付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "银行转账", DictValue = "BANK_TRANSFER", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "银行转账", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "支票", DictValue = "CHECK", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "支票付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "信用卡", DictValue = "CREDIT_CARD", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "信用卡付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "支付宝", DictValue = "ALIPAY", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "支付宝付款", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "微信支付", DictValue = "WECHAT_PAY", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "微信支付", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_payment_method", DictLabel = "其他", DictValue = "OTHER", OrderNum = 7,  CssClass = 2, ListClass = 2, Remark = "其他付款方式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // ===== 凭证发票字典 =====
            // 发票类别 - 横排格式
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "增值税专用发票", DictValue = "VAT_SPECIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "增值税专用发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "增值税普通发票", DictValue = "VAT_NORMAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "增值税普通发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "电子发票", DictValue = "ELECTRONIC", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "电子发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "普通发票", DictValue = "NORMAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "普通发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "收据", DictValue = "RECEIPT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "收据", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_category", DictLabel = "其他发票", DictValue = "OTHER", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "其他类型发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 凭证类别 - 横排格式
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "收款凭证", DictValue = "RECEIPT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "收款凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "付款凭证", DictValue = "PAYMENT", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "付款凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "转账凭证", DictValue = "TRANSFER", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "转账凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "通用凭证", DictValue = "GENERAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "通用凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "调整凭证", DictValue = "ADJUSTMENT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "调整凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_voucher_category", DictLabel = "结转凭证", DictValue = "CARRY_FORWARD", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "结转凭证", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 费用类别 - 横排格式
            new TaktDictData { DictType = "fico_expense_category", DictLabel = "固定费用", DictValue = "FIXED", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "固定费用类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_expense_category", DictLabel = "变动费用", DictValue = "VARIABLE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "变动费用类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_expense_category", DictLabel = "半固定费用", DictValue = "SEMI_FIXED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "半固定费用类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_expense_category", DictLabel = "可控费用", DictValue = "CONTROLLABLE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "可控费用类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_expense_category", DictLabel = "不可控费用", DictValue = "UNCONTROLLABLE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "不可控费用类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 发票校验状态 - 横排格式
            new TaktDictData { DictType = "fico_invoice_verification_status", DictLabel = "未校验发票", DictValue = "", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "尚未收到或处理任何发票", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_verification_status", DictLabel = "部分校验发票", DictValue = "B", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "发票已处理，但仅针对部分订单金额或数量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_verification_status", DictLabel = "完全校验发票", DictValue = "A", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "订单项目的发票已全部处理完毕", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_invoice_verification_status", DictLabel = "发票有冻结", DictValue = "F", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "发票已被处理，但因某种原因（如价格差异）被冻结付款，需要释放", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // ===== 会计科目字典 =====
            // 资产类别 - 横排格式
            new TaktDictData { DictType = "fico_asset_category", DictLabel = "流动资产", DictValue = "CURRENT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "流动资产类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_asset_category", DictLabel = "非流动资产", DictValue = "NON_CURRENT", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "非流动资产类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_asset_category", DictLabel = "固定资产", DictValue = "FIXED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "固定资产类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_asset_category", DictLabel = "无形资产", DictValue = "INTANGIBLE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "无形资产类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_asset_category", DictLabel = "投资性资产", DictValue = "INVESTMENT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "投资性资产类别", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 科目类别 - 横排格式
            new TaktDictData { DictType = "fico_account_category", DictLabel = "资产类", DictValue = "ASSET", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "资产类科目", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_account_category", DictLabel = "负债类", DictValue = "LIABILITY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "负债类科目", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_account_category", DictLabel = "权益类", DictValue = "EQUITY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "权益类科目", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_account_category", DictLabel = "成本类", DictValue = "COST", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "成本类科目", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_account_category", DictLabel = "损益类", DictValue = "PROFIT_LOSS", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "损益类科目", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // ===== 成本控制字典 =====
            // 成本要素类别 - 横排格式
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "直接材料", DictValue = "DIRECT_MATERIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "直接材料成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "直接人工", DictValue = "DIRECT_LABOR", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "直接人工成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "制造费用", DictValue = "MANUFACTURING_OVERHEAD", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "制造费用成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "销售费用", DictValue = "SELLING_EXPENSE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "销售费用成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "管理费用", DictValue = "ADMINISTRATIVE_EXPENSE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "管理费用成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_element_category", DictLabel = "财务费用", DictValue = "FINANCIAL_EXPENSE", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "财务费用成本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 成本中心类别 - 横排格式
            new TaktDictData { DictType = "fico_cost_center_category", DictLabel = "生产中心", DictValue = "PRODUCTION", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "生产成本中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_center_category", DictLabel = "服务中心", DictValue = "SERVICE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "服务成本中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_center_category", DictLabel = "支持中心", DictValue = "SUPPORT", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "支持成本中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_center_category", DictLabel = "研发中心", DictValue = "RESEARCH", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "研发成本中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_cost_center_category", DictLabel = "管理中心", DictValue = "MANAGEMENT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "管理成本中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 利润中心类别 - 横排格式
            new TaktDictData { DictType = "fico_profit_center_category", DictLabel = "产品线", DictValue = "PRODUCT_LINE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "产品线利润中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_profit_center_category", DictLabel = "地区", DictValue = "REGION", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "地区利润中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_profit_center_category", DictLabel = "客户", DictValue = "CUSTOMER", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "客户利润中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_profit_center_category", DictLabel = "渠道", DictValue = "CHANNEL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "渠道利润中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "fico_profit_center_category", DictLabel = "业务单元", DictValue = "BUSINESS_UNIT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "业务单元利润中心", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

        };
    }
}

