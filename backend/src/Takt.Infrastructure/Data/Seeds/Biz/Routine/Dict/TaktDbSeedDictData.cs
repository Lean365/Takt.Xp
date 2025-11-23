//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 字典数据种子数据初始化类 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 字典数据种子数据提供类
/// </summary>
public class TaktDbSeedDictData
{
    /// <summary>
    /// 获取默认字典数据
    /// </summary>
    /// <returns>字典数据列表</returns>
    public List<TaktDictData> GetDefaultDictData()
    {
        return new List<TaktDictData>
        {
            // 系统状态 - 横排格式
            new TaktDictData { DictType = "sys_normal_disable", DictLabel = "正常", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "正常状态" },
            new TaktDictData { DictType = "sys_normal_disable", DictLabel = "停用", DictValue = "1", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "停用状态" },

            // 是否选项 - 横排格式
            new TaktDictData { DictType = "sys_yes_no", DictLabel = "是", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "是" },
            new TaktDictData { DictType = "sys_yes_no", DictLabel = "否", DictValue = "1", OrderNum = 2,  CssClass = 3, ListClass = 3, Remark = "否" },

            // 操作类型 - 横排格式
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "其他", DictValue = "0", OrderNum = 1,  CssClass = 3, ListClass = 3, Remark = "其他操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "查询", DictValue = "1", OrderNum = 2,  CssClass = 1, ListClass = 1, Remark = "查询操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "新增", DictValue = "2", OrderNum = 3,  CssClass = 2, ListClass = 2, Remark = "新增操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "修改", DictValue = "3", OrderNum = 4,  CssClass = 3, ListClass = 3, Remark = "修改操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "删除", DictValue = "4", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "删除操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "导出", DictValue = "5", OrderNum = 6,  CssClass = 3, ListClass = 3, Remark = "导出操作" },
            new TaktDictData { DictType = "sys_oper_type", DictLabel = "导入", DictValue = "6", OrderNum = 7,  CssClass = 3, ListClass = 3, Remark = "导入操作" },

            // 是否默认 - 横排格式
            new TaktDictData { DictType = "sys_is_default", DictLabel = "是", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "是" },
            new TaktDictData { DictType = "sys_is_default", DictLabel = "否", DictValue = "1", OrderNum = 2,  CssClass = 3, ListClass = 3, Remark = "否" },

            // 操作状态 - 横排格式
            new TaktDictData { DictType = "sys_common_status", DictLabel = "成功", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "操作成功" },
            new TaktDictData { DictType = "sys_common_status", DictLabel = "失败", DictValue = "1", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "操作失败" },
            new TaktDictData { DictType = "sys_common_status", DictLabel = "处理中", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "操作处理中" },
            new TaktDictData { DictType = "sys_common_status", DictLabel = "已取消", DictValue = "3", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "操作已取消" },

            // 许可类别 - 横排格式
            new TaktDictData { DictType = "sys_license_category", DictLabel = "商业许可", DictValue = "COMMERCIAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "商业使用许可" },
            new TaktDictData { DictType = "sys_license_category", DictLabel = "开源许可", DictValue = "OPEN_SOURCE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "开源软件许可" },
            new TaktDictData { DictType = "sys_license_category", DictLabel = "免费许可", DictValue = "FREE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "免费使用许可" },
            new TaktDictData { DictType = "sys_license_category", DictLabel = "试用许可", DictValue = "TRIAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "试用期许可" },
            new TaktDictData { DictType = "sys_license_category", DictLabel = "教育许可", DictValue = "EDUCATIONAL", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "教育机构许可" },
            new TaktDictData { DictType = "sys_license_category", DictLabel = "个人许可", DictValue = "PERSONAL", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "个人使用许可" },

            // 加密类别 - 横排格式
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "AES", DictValue = "AES", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "AES对称加密算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "RSA", DictValue = "RSA", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "RSA非对称加密算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "DES", DictValue = "DES", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "DES对称加密算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "3DES", DictValue = "3DES", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "3DES三重数据加密算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "MD5", DictValue = "MD5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "MD5哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SHA1", DictValue = "SHA1", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "SHA1安全哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SHA256", DictValue = "SHA256", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "SHA256安全哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SHA512", DictValue = "SHA512", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "SHA512安全哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "BCrypt", DictValue = "BCRYPT", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "BCrypt密码哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SM2", DictValue = "SM2", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "SM2国产非对称加密算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SM3", DictValue = "SM3", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "SM3国产哈希算法" },
            new TaktDictData { DictType = "sys_encryption_category", DictLabel = "SM4", DictValue = "SM4", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "SM4国产对称加密算法" },

        };
    }
}


