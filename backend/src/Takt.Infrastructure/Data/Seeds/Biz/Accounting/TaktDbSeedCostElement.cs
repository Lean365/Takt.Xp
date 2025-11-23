//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCostElement.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 成本要素数据提供类
//===================================================================

using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 成本要素数据提供类
/// </summary>
public class TaktDbSeedCostElement
{
    /// <summary>
    /// 获取默认成本要素数据
    /// </summary>
    /// <returns>成本要素数据列表</returns>
    public List<TaktCostElement> GetDefaultCostElements()
    {
        return new List<TaktCostElement>
        {
            new TaktCostElement { CostElementCode = "CE001", CostElementName = "人工成本", CompanyCode = "1000", PlantCode = "C100", CostElementType = 1, CostElementCategory = "人工费用", Unit = "小时", CostElementDescription = "员工工资、福利等人力成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE002", CostElementName = "材料成本", CompanyCode = "1000", PlantCode = "C100", CostElementType = 1, CostElementCategory = "材料费用", Unit = "件", CostElementDescription = "原材料、辅料等材料成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE003", CostElementName = "制造费用", CompanyCode = "2300", PlantCode = "C200", CostElementType = 2, CostElementCategory = "制造费用", Unit = "元", CostElementDescription = "设备折旧、水电费等制造费用", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE004", CostElementName = "管理费用", CompanyCode = "1000", PlantCode = "C100", CostElementType = 2, CostElementCategory = "管理费用", Unit = "元", CostElementDescription = "办公费用、差旅费等管理费用", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE005", CostElementName = "销售费用", CompanyCode = "2400", PlantCode = "C300", CostElementType = 2, CostElementCategory = "销售费用", Unit = "元", CostElementDescription = "广告费、销售佣金等销售费用", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE006", CostElementName = "研发费用", CompanyCode = "1000", PlantCode = "C100", CostElementType = 2, CostElementCategory = "研发费用", Unit = "元", CostElementDescription = "研发设备、技术引进等研发费用", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE007", CostElementName = "财务费用", CompanyCode = "1000", PlantCode = "C100", CostElementType = 2, CostElementCategory = "财务费用", Unit = "元", CostElementDescription = "利息支出、汇兑损益等财务费用", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostElement { CostElementCode = "CE008", CostElementName = "其他费用", CompanyCode = "1000", PlantCode = "C100", CostElementType = 2, CostElementCategory = "其他费用", Unit = "元", CostElementDescription = "其他未分类的费用项目", Status = 0, EffectiveDate = DateTime.Now.Date }
        };
    }
}


