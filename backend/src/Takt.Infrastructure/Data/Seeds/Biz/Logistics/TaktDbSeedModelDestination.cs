//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedModelDestination.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 机种仕向数据提供类
//===================================================================

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 机种仕向数据提供类
/// </summary>
public class TaktDbSeedModelDestination
{
    /// <summary>
    /// 获取默认机种仕向数据
    /// </summary>
    /// <returns>机种仕向数据列表</returns>
    public List<TaktModelDestination> GetDefaultModelDestinations()
    {
        return new List<TaktModelDestination>
        {
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062000", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062001", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC BSC", DestinationCode = "DM", DestinationName = "DM区域", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC BSC" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062020", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC 500GB", DestinationCode = "TCA", DestinationName = "TCA区域", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC 500GB" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062022", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC FF", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC FF" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062030", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC JPN", DestinationCode = "JPN", DestinationName = "日本", DestinationType = 1, ProductSpecification = "UR-4MD VIDEO REC JPN" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062040", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC 1TB", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC 1TB" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062050", ModelCode = "UR-4MD 6", ModelName = "MDR-600HD VIDEO REC", DestinationCode = "EUR", DestinationName = "欧洲", DestinationType = 4, ProductSpecification = "MDR-600HD VIDEO REC" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062075", ModelCode = "UR-4MD 6", ModelName = "UR-4MD VIDEO REC BRZ", DestinationCode = "BRZ", DestinationName = "巴西", DestinationType = 7, ProductSpecification = "UR-4MD VIDEO REC BRZ" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062101", ModelCode = "UR-4MD 7", ModelName = "UR-4MD R VID REC BSC", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD R VID REC BSC" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062120", ModelCode = "UR-4MD 7", ModelName = "UR-4MD R VID REC 500GB", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD R VID REC 500GB" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062130", ModelCode = "UR-4MD 7", ModelName = "UR-4MD R VID REC JPN", DestinationCode = "JPN", DestinationName = "日本", DestinationType = 1, ProductSpecification = "UR-4MD R VID REC JPN" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062140", ModelCode = "UR-4MD 7", ModelName = "UR-4MD R VID REC 1TB", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "UR-4MD R VID REC 1TB" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062150", ModelCode = "UR-4MD 7", ModelName = "MDR-600HD R VIDEO REC", DestinationCode = "ALL", DestinationName = "全球通用", DestinationType = 7, ProductSpecification = "MDR-600HD R VIDEO REC" },
            new TaktModelDestination { PlantCode = "C100", MaterialCode = "1921062160", ModelCode = "UR-4MD 7", ModelName = "UR-4MD R VID REC TCA", DestinationCode = "TCA", DestinationName = "TCA区域", DestinationType = 7, ProductSpecification = "UR-4MD R VID REC TCA" }
        };
    }
}


