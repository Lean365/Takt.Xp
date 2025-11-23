//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCostCenter.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 成本中心数据提供类
//===================================================================

using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 成本中心数据提供类
/// </summary>
public class TaktDbSeedCostCenter
{
    /// <summary>
    /// 获取默认成本中心数据
    /// </summary>
    /// <returns>成本中心数据列表</returns>
    public List<TaktCostCenter> GetDefaultCostCenters()
    {
        return new List<TaktCostCenter>
        {
            new TaktCostCenter { CostCenterCode = "2U20", CostCenterName = "研发成本中心", CompanyCode = "1000", PlantCode = "C100", CostCenterCategory = 1, ManagerName = "张经理", DeptName = "研发部", CostCenterDescription = "软件开发研发成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostCenter { CostCenterCode = "2U10", CostCenterName = "服务成本中心", CompanyCode = "1000", PlantCode = "C100", CostCenterCategory = 1, ManagerName = "李经理", DeptName = "服务部", CostCenterDescription = "技术服务成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostCenter { CostCenterCode = "3U20", CostCenterName = "制造成本中心", CompanyCode = "2300", PlantCode = "C200", CostCenterCategory = 1, ManagerName = "王经理", DeptName = "生产部", CostCenterDescription = "产品制造成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostCenter { CostCenterCode = "3U10", CostCenterName = "供应链成本中心", CompanyCode = "2400", PlantCode = "C300", CostCenterCategory = 1, ManagerName = "赵经理", DeptName = "供应链部", CostCenterDescription = "供应链运营成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostCenter { CostCenterCode = "4U30", CostCenterName = "贸易成本中心", CompanyCode = "2400", PlantCode = "C300", CostCenterCategory = 1, ManagerName = "孙经理", DeptName = "贸易部", CostCenterDescription = "进出口贸易成本", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktCostCenter { CostCenterCode = "CC006", CostCenterName = "管理成本中心", CompanyCode = "1000", PlantCode = "C100", CostCenterCategory = 1, ManagerName = "陈经理", DeptName = "行政部", CostCenterDescription = "公司管理成本", Status = 0, EffectiveDate = DateTime.Now.Date }
        };
    }
}


