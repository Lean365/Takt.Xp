//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedGenConfig.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 代码生成配置种子数据
//===================================================================

using Takt.Domain.Entities.Generator;
using Newtonsoft.Json;

namespace Takt.Infrastructure.Data.Seeds.Generator;

/// <summary>
/// 代码生成配置种子数据
/// </summary>
public class TaktDbSeedGenConfig
{
    /// <summary>
    /// 获取默认代码生成配置数据
    /// </summary>
    /// <returns>配置数据列表</returns>
    public List<TaktGenConfig> GetDefaultGenConfigs()
    {
        return new List<TaktGenConfig>
        {
            new TaktGenConfig { GenConfigName = "Default", Author = "Takt365", ProjectName = "Lean.Takt", ModuleName  = "Core", BusinessName = "TaktCore", FunctionName = "TaktCore", GenMethod = 0, GenTplType = 0, GenPath = "src/Takt.WebApi/src", Options = JsonConvert.SerializeObject(new Dictionary<string, object> { { "isSqlDiff", 1 }, { "isSnowflakeId", 1 }, { "isRepository", 0 }, { "crudGroup", new[] { 1, 2, 3, 4, 5, 6, 7, 8 } } }), Status = 0 },
            new TaktGenConfig { GenConfigName = "CustomTemplate", Author = "Takt365", ProjectName = "Lean.Takt", ModuleName = "Core", BusinessName = "TaktCore", FunctionName = "TaktCore", GenMethod = 0, GenTplType = 1, GenPath = "src/Takt.WebApi", Options = JsonConvert.SerializeObject(new Dictionary<string, object> { { "isSqlDiff", 1 }, { "isSnowflakeId", 1 }, { "isRepository", 1 }, { "crudGroup", new[] { 1, 2, 3, 4, 5, 6, 7, 8 } } }), Status = 0 }
        };
    }
}




