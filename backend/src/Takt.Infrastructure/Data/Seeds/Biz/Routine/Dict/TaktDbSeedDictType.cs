//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 系统基础字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 系统基础字典类型种子数据提供类
/// </summary>
public class TaktDbSeedDictType
{
    /// <summary>
    /// 获取默认字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetDefaultDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "系统状态", DictType = "sys_normal_disable", OrderNum = 1, DictStatus=0, Remark = "系统状态字典" },
            new TaktDictType { DictName = "是否选项", DictType = "sys_yes_no", OrderNum = 2, DictStatus=0, Remark = "是否选项字典" },
            new TaktDictType { DictName = "操作类型", DictType = "sys_oper_type", OrderNum = 3, DictStatus=0, Remark = "操作类型字典" },
            new TaktDictType { DictName = "是否默认", DictType = "sys_is_default", OrderNum = 4, DictStatus=0, Remark = "是否默认字典" },
            new TaktDictType { DictName = "操作状态", DictType = "sys_common_status", OrderNum = 5, DictStatus=0, Remark = "操作状态字典" },
            new TaktDictType { DictName = "许可类别", DictType = "sys_license_category", OrderNum = 6, DictStatus=0, Remark = "许可类别字典" },
            new TaktDictType { DictName = "加密类别", DictType = "sys_encryption_category", OrderNum = 7, DictStatus=0, Remark = "加密类别字典" }
        };
    }
}

