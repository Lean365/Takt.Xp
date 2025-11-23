//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedQualityDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 质量相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedQualityDictType
{
    /// <summary>
    /// 获取质量相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetQualityDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "质量等级", DictType = "qm_quality_grade", OrderNum = 1, DictStatus=0, Remark = "质量等级字典" },
            new TaktDictType { DictName = "检验类型", DictType = "qm_inspection_type", OrderNum = 2, DictStatus=0, Remark = "质量检验类型字典" },
            new TaktDictType { DictName = "检验方法", DictType = "qm_inspection_method", OrderNum = 3, DictStatus=0, Remark = "质量检验方法字典" },
            new TaktDictType { DictName = "检验手段", DictType = "qm_inspection_means", OrderNum = 4, DictStatus=0, Remark = "质量检验手段字典" },
            new TaktDictType { DictName = "检验结果", DictType = "qm_inspection_result", OrderNum = 5, DictStatus=0, Remark = "质量检验结果字典" },
            new TaktDictType { DictName = "缺陷类型", DictType = "qm_defect_type", OrderNum = 6, DictStatus=0, Remark = "缺陷类型字典" },
            new TaktDictType { DictName = "缺陷等级", DictType = "qm_defect_level", OrderNum = 7, DictStatus=0, Remark = "缺陷等级字典" },
            new TaktDictType { DictName = "质量状态", DictType = "qm_quality_status", OrderNum = 8, DictStatus=0, Remark = "质量状态字典" },
            new TaktDictType { DictName = "质量体系", DictType = "qm_quality_system", OrderNum = 9, DictStatus=0, Remark = "质量体系字典" }
        };
    }
}

