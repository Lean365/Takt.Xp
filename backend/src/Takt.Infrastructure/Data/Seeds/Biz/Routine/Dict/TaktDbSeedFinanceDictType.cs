//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFinanceDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 财务相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedFinanceDictType
{
    /// <summary>
    /// 获取财务相关字典类型数据
    /// </summary>
    /// <returns>财务相关字典类型数据列表</returns>
    public List<TaktDictType> GetFinanceDictTypes()
    {
        return new List<TaktDictType>
        {
            // ===== 基础财务字典 =====
            new TaktDictType { DictName = "币种", DictType = "fico_currency_category", OrderNum = 1, DictStatus=0, Remark = "币种字典" },
            new TaktDictType { DictName = "汇率", DictType = "fico_exchange_rate", OrderNum = 2, DictStatus=0, Remark = "汇率字典" },
            new TaktDictType { DictName = "税率", DictType = "fico_tax_rate", OrderNum = 3, DictStatus=0, Remark = "税率字典" },
            
            // ===== 付款相关字典 =====
            new TaktDictType { DictName = "付款条件", DictType = "fico_payment_terms", OrderNum = 4, DictStatus=0, Remark = "付款条件字典" },
            new TaktDictType { DictName = "付款方式", DictType = "fico_payment_method", OrderNum = 5, DictStatus=0, Remark = "付款方式字典" },
            
            // ===== 凭证发票字典 =====
            new TaktDictType { DictName = "发票类别", DictType = "fico_invoice_category", OrderNum = 6, DictStatus=0, Remark = "发票类别字典" },
            new TaktDictType { DictName = "凭证类别", DictType = "fico_voucher_category", OrderNum = 7, DictStatus=0, Remark = "凭证类别字典" },
            new TaktDictType { DictName = "费用类别", DictType = "fico_expense_category", OrderNum = 8, DictStatus=0, Remark = "费用类别字典" },
            new TaktDictType { DictName = "发票校验状态", DictType = "fico_invoice_verification_status", OrderNum = 9, DictStatus=0, Remark = "发票校验状态字典" },
            
            // ===== 会计科目字典 =====
            new TaktDictType { DictName = "资产类别", DictType = "fico_asset_category", OrderNum = 10, DictStatus=0, Remark = "资产类别字典" },
            new TaktDictType { DictName = "科目类别", DictType = "fico_account_category", OrderNum = 11, DictStatus=0, Remark = "会计科目类别字典" },
            
            // ===== 成本控制字典 =====
            new TaktDictType { DictName = "成本要素类别", DictType = "fico_cost_element_category", OrderNum = 12, DictStatus=0, Remark = "成本要素类别字典" },
            new TaktDictType { DictName = "成本中心类别", DictType = "fico_cost_center_category", OrderNum = 13, DictStatus=0, Remark = "成本中心类别字典" },
            new TaktDictType { DictName = "利润中心类别", DictType = "fico_profit_center_category", OrderNum = 14, DictStatus=0, Remark = "利润中心类别字典" }
        };
    }
}

