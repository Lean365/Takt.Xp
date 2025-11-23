//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFileDictType.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 文件相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedFileDictType
{
    /// <summary>
    /// 获取文件相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetFileDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "文件路径", DictType = "file_path", OrderNum = 1, DictStatus=0, Remark = "文件上传路径数据字典" },
            new TaktDictType { DictName = "存储位置", DictType = "file_storage_location", OrderNum = 2, DictStatus=0, Remark = "文件存储物理位置数据字典" },
            new TaktDictType { DictName = "存储类型", DictType = "file_storage_type", OrderNum = 3, DictStatus=0, Remark = "本地/云存储类型数据字典" },
            new TaktDictType { DictName = "文件命名", DictType = "file_name_rule", OrderNum = 4, DictStatus=0, Remark = "文件命名规则数据字典" },
            new TaktDictType { DictName = "文件分类", DictType = "file_category", OrderNum = 5, DictStatus=0, Remark = "文件分类数据字典" },
            new TaktDictType { DictName = "文件状态", DictType = "file_status", OrderNum = 6, DictStatus=0, Remark = "文件状态数据字典" },
            new TaktDictType { DictName = "文件权限", DictType = "file_permission", OrderNum = 7, DictStatus=0, Remark = "文件权限数据字典" },
            new TaktDictType { DictName = "文件版本", DictType = "file_version", OrderNum = 8, DictStatus=0, Remark = "文件版本数据字典" },
            new TaktDictType { DictName = "文件标签", DictType = "file_tag", OrderNum = 9, DictStatus=0, Remark = "文件标签数据字典" },
            new TaktDictType { DictName = "文件审核", DictType = "file_audit", OrderNum = 10, DictStatus=0, Remark = "文件审核数据字典" }
        };
    }
}



