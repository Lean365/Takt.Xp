//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedUnitDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 计量单位字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 计量单位字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedUnitDictData
{
    /// <summary>
    /// 获取计量单位字典数据
    /// </summary>
    /// <returns>计量单位字典数据列表</returns>
    public List<TaktDictData> GetUnitDictData()
    {
        return new List<TaktDictData>
        {
            // 数量单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "个", DictValue = "PCS", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "个", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "件", DictValue = "PCE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "套", DictValue = "SET", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "套", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "台", DictValue = "UNIT", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "台", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "块", DictValue = "BLK", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "张", DictValue = "SHT", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "张", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "片", DictValue = "SLICE", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "片", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "箱", DictValue = "BOX", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "箱", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "包", DictValue = "PKG", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "包", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "卷", DictValue = "ROLL", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "卷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 重量单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "吨", DictValue = "T", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "吨", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "千克", DictValue = "KG", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "千克", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "克", DictValue = "G", OrderNum = 13,  CssClass = 13, ListClass = 13, Remark = "克", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 长度单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "米", DictValue = "M", OrderNum = 14,  CssClass = 14, ListClass = 14, Remark = "米", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "厘米", DictValue = "CM", OrderNum = 15,  CssClass = 15, ListClass = 15, Remark = "厘米", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "毫米", DictValue = "MM", OrderNum = 16,  CssClass = 16, ListClass = 16, Remark = "毫米", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 面积单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "平方米", DictValue = "M2", OrderNum = 17,  CssClass = 17, ListClass = 17, Remark = "平方米", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 体积单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "立方米", DictValue = "M3", OrderNum = 18,  CssClass = 18, ListClass = 18, Remark = "立方米", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 容量单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "升", DictValue = "L", OrderNum = 19,  CssClass = 19, ListClass = 19, Remark = "升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "毫升", DictValue = "ML", OrderNum = 20,  CssClass = 20, ListClass = 20, Remark = "毫升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 时间单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "小时", DictValue = "HR", OrderNum = 21,  CssClass = 21, ListClass = 21, Remark = "小时", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "分钟", DictValue = "MIN", OrderNum = 22,  CssClass = 22, ListClass = 22, Remark = "分钟", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 电力单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "千瓦时", DictValue = "KWH", OrderNum = 23,  CssClass = 23, ListClass = 23, Remark = "千瓦时", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "千瓦", DictValue = "KW", OrderNum = 24,  CssClass = 24, ListClass = 24, Remark = "千瓦", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "瓦特", DictValue = "W", OrderNum = 25,  CssClass = 25, ListClass = 25, Remark = "瓦特", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 电气单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "伏特", DictValue = "V", OrderNum = 26,  CssClass = 26, ListClass = 26, Remark = "伏特", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "安培", DictValue = "A", OrderNum = 27,  CssClass = 27, ListClass = 27, Remark = "安培", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "欧姆", DictValue = "OHM", OrderNum = 28,  CssClass = 28, ListClass = 28, Remark = "欧姆", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "赫兹", DictValue = "HZ", OrderNum = 29,  CssClass = 29, ListClass = 29, Remark = "赫兹", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 物理单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "摄氏度", DictValue = "C", OrderNum = 30,  CssClass = 30, ListClass = 30, Remark = "摄氏度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "帕斯卡", DictValue = "PA", OrderNum = 31,  CssClass = 31, ListClass = 31, Remark = "帕斯卡", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "牛顿", DictValue = "N", OrderNum = 32,  CssClass = 32, ListClass = 32, Remark = "牛顿", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "焦耳", DictValue = "J", OrderNum = 33,  CssClass = 33, ListClass = 33, Remark = "焦耳", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "坎德拉", DictValue = "CD", OrderNum = 34,  CssClass = 34, ListClass = 34, Remark = "坎德拉", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "摩尔", DictValue = "MOL", OrderNum = 35,  CssClass = 35, ListClass = 35, Remark = "摩尔", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 其他单位 - 横排格式
            new TaktDictData { DictType = "sys_unit_type", DictLabel = "其他", DictValue = "OTHER", OrderNum = 36,  CssClass = 36, ListClass = 36, Remark = "其他计量单位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

