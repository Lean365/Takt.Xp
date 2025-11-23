//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedOperationRate.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 标准稼动率数据提供类
//===================================================================

using Takt.Domain.Entities.Logistics.Manufacturing;

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 标准稼动率数据提供类
/// </summary>
public class TaktDbSeedOperationRate
{
    /// <summary>
    /// 获取默认标准稼动率数据
    /// </summary>
    /// <returns>标准稼动率数据列表</returns>
    public List<TaktOperationRate> GetDefaultOperationRates()
    {
        return new List<TaktOperationRate>
        {
            new TaktOperationRate { PlantCode = "C100", FinancialYear = "2025", OperationType = 1, OperationRate = 85.00m, EffectiveDate = new DateTime(2025, 1, 1), ExpiryDate = new DateTime(9999, 12, 31), Status = 0, Remark = "人工稼动率" }
        };
    }
}


