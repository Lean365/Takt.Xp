//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedCompany.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 公司信息数据提供类
//===================================================================

using Takt.Domain.Entities.Accounting.Financial;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 公司信息数据提供类
/// </summary>
public class TaktDbSeedCompany
{
    /// <summary>
    /// 获取默认公司信息数据
    /// </summary>
    /// <returns>公司信息数据列表</returns>
    public List<TaktCompany> GetDefaultCompanies()
    {
        return new List<TaktCompany>
        {
            new TaktCompany { CompanyCode = "1000", CompanyName = "东京TCJ", CompanyShortName = "TCJ", CompanyCity = "东京", CompanyCountry = "JP", CompanyCurrency = 3, CompanyIndustryType = 2, CompanyNature = 3, Status = 0 },
            new TaktCompany { CompanyCode = "2300", CompanyName = "东莞DTA", CompanyShortName = "DTA", CompanyCity = "东莞", CompanyCountry = "CN", CompanyCurrency = 0, CompanyIndustryType = 1, CompanyNature = 3, Status = 0 },
            new TaktCompany { CompanyCode = "2400", CompanyName = "香港TAC", CompanyShortName = "TAC", CompanyCity = "香港", CompanyCountry = "HK", CompanyCurrency = 4, CompanyIndustryType = 3, CompanyNature = 3, Status = 0 }
        };
    }
}


