//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedPlant.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 工厂信息数据提供类
//===================================================================

using Takt.Domain.Entities.Logistics.Material.Master;

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 工厂信息数据提供类
/// </summary>
public class TaktDbSeedPlant
{
    /// <summary>
    /// 获取默认工厂信息数据
    /// </summary>
    /// <returns>工厂信息数据列表</returns>
    public List<TaktPlant> GetDefaultPlants()
    {
        return new List<TaktPlant>
        {
            new TaktPlant { PlantCode = "C100", PlantName = "东莞DTA", PlantShortName = "DTA", PlantType = 1, Country = "中国", Province = "广东省", City = "东莞市", PlantAddress = "东莞市松山湖科技园", ContactPerson = "张经理", Phone = "0769-12345678", Email = "dta@company.com", Status = 0 },
            new TaktPlant { PlantCode = "H100", PlantName = "香港TAC", PlantShortName = "TAC", PlantType = 1, Country = "中国", Province = "香港特别行政区", City = "香港", PlantAddress = "香港特别行政区", ContactPerson = "李经理", Phone = "00852-12345678", Email = "tac@company.com", Status = 0 },
            new TaktPlant { PlantCode = "1200", PlantName = "日本TMS", PlantShortName = "TMS", PlantType = 1, Country = "日本", Province = "东京都", City = "东京", PlantAddress = "东京都", ContactPerson = "王经理", Phone = "0081-3-12345678", Email = "tms@company.com", Status = 0 }
        };
    }
}


