//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedProfitCenter.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 利润中心数据提供类
//===================================================================

using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 利润中心数据提供类
/// </summary>
public class TaktDbSeedProfitCenter
{
    /// <summary>
    /// 获取默认利润中心数据
    /// </summary>
    /// <returns>利润中心数据列表</returns>
    public List<TaktProfitCenter> GetDefaultProfitCenters()
    {
        return new List<TaktProfitCenter>
        {
            new TaktProfitCenter { ProfitCenterCode = "PC001", ProfitCenterName = "软件开发中心", CompanyCode = "1000", PlantCode = "C100", ProfitCenterCategory = 1, ManagerName = "张经理", DeptName = "研发部", ProfitCenterDescription = "负责软件开发业务", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktProfitCenter { ProfitCenterCode = "PC002", ProfitCenterName = "技术服务中心", CompanyCode = "1000", PlantCode = "C100", ProfitCenterCategory = 1, ManagerName = "李经理", DeptName = "服务部", ProfitCenterDescription = "负责技术支持和维护", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktProfitCenter { ProfitCenterCode = "PC003", ProfitCenterName = "电子产品制造中心", CompanyCode = "2300", PlantCode = "C200", ProfitCenterCategory = 1, ManagerName = "王经理", DeptName = "生产部", ProfitCenterDescription = "负责电子产品生产制造", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktProfitCenter { ProfitCenterCode = "PC004", ProfitCenterName = "供应链管理中心", CompanyCode = "2400", PlantCode = "C300", ProfitCenterCategory = 1, ManagerName = "赵经理", DeptName = "供应链部", ProfitCenterDescription = "负责供应链管理和优化", Status = 0, EffectiveDate = DateTime.Now.Date },
            new TaktProfitCenter { ProfitCenterCode = "PC005", ProfitCenterName = "进出口贸易中心", CompanyCode = "2400", PlantCode = "C300", ProfitCenterCategory = 1, ManagerName = "孙经理", DeptName = "贸易部", ProfitCenterDescription = "负责进出口贸易业务", Status = 0, EffectiveDate = DateTime.Now.Date }
        };
    }
}


