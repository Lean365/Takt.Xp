//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedOADictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-19
// 版本号 : V0.0.1
// 描述   : OA相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// OA相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedOADictType
{
    /// <summary>
    /// 获取OA相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetOADictTypes()
    {
        return new List<TaktDictType>
        {
            // 会议相关
            new TaktDictType { DictName = "会议类型", DictType = "sys_meeting_type", OrderNum = 1, DictStatus=0, Remark = "会议类型字典" },
            new TaktDictType { DictName = "会议状态", DictType = "sys_meeting_status", OrderNum = 2, DictStatus=0, Remark = "会议状态字典" },
            // 车辆相关
            new TaktDictType { DictName = "车辆类型", DictType = "sys_vehicle_type", OrderNum = 3, DictStatus=0, Remark = "车辆类型字典" },
            new TaktDictType { DictName = "车辆状态", DictType = "sys_vehicle_status", OrderNum = 4, DictStatus=0, Remark = "车辆状态字典" },
            // 日程相关
            new TaktDictType { DictName = "日程类型", DictType = "sys_schedule_type", OrderNum = 5, DictStatus=0, Remark = "日程类型字典" },
            new TaktDictType { DictName = "日程优先级", DictType = "sys_schedule_priority", OrderNum = 6, DictStatus=0, Remark = "日程优先级字典" },
            // 知识相关
            new TaktDictType { DictName = "知识分类", DictType = "sys_knowledge_category", OrderNum = 7, DictStatus=0, Remark = "知识分类字典" },
            new TaktDictType { DictName = "知识权限", DictType = "sys_knowledge_permission", OrderNum = 8, DictStatus=0, Remark = "知识权限字典" },
            // 通讯录相关
            new TaktDictType { DictName = "联系人分组", DictType = "sys_contact_group", OrderNum = 9, DictStatus=0, Remark = "联系人分组字典" },
            new TaktDictType { DictName = "联系人类型", DictType = "sys_contact_type", OrderNum = 10, DictStatus=0, Remark = "联系人类型字典" },
            // 任务相关
            new TaktDictType { DictName = "任务类型", DictType = "sys_task_type", OrderNum = 11, DictStatus=0, Remark = "任务类型字典（1.程序集 2.网络请求 3.SQL语句）" },
            // 合同相关
            new TaktDictType { DictName = "合同类型", DictType = "sys_contract_type", OrderNum = 12, DictStatus=0, Remark = "合同类型字典" },
            new TaktDictType { DictName = "合同方类型", DictType = "sys_contract_party_type", OrderNum = 13, DictStatus=0, Remark = "合同方类型字典" },
            new TaktDictType { DictName = "合同状态", DictType = "sys_contract_status", OrderNum = 14, DictStatus=0, Remark = "合同状态字典" },
            // 文件相关
            new TaktDictType { DictName = "文件扩展名", DictType = "sys_file_extension", OrderNum = 15, DictStatus=0, Remark = "文件扩展名字典" },
            new TaktDictType { DictName = "文件类型", DictType = "sys_file_type", OrderNum = 16, DictStatus=0, Remark = "文件类型字典" },
            new TaktDictType { DictName = "存储类型", DictType = "sys_storage_type", OrderNum = 17, DictStatus=0, Remark = "存储类型字典" },
            // ISO文档相关
            new TaktDictType { DictName = "ISO文档类型", DictType = "sys_iso_document_type", OrderNum = 18, DictStatus=0, Remark = "ISO文档类型字典" },
            // 文档属性相关
            new TaktDictType { DictName = "重要程度", DictType = "sys_importance_level", OrderNum = 19, DictStatus=0, Remark = "文档重要程度字典" },
            new TaktDictType { DictName = "是否强制", DictType = "sys_is_mandatory", OrderNum = 20, DictStatus=0, Remark = "是否强制字典" },
            new TaktDictType { DictName = "是否公开", DictType = "sys_is_public", OrderNum = 21, DictStatus=0, Remark = "是否公开字典" },
            // 法规相关
            new TaktDictType { DictName = "法规类型", DictType = "sys_law_type", OrderNum = 22, DictStatus=0, Remark = "法规类型字典" },
            new TaktDictType { DictName = "规章制度类型", DictType = "sys_regulation_type", OrderNum = 23, DictStatus=0, Remark = "规章制度类型字典" },
            // 会议相关
            new TaktDictType { DictName = "是否需要签到", DictType = "sys_need_signin", OrderNum = 24, DictStatus=0, Remark = "是否需要签到字典" },
            new TaktDictType { DictName = "会议室类型", DictType = "sys_meeting_room_type", OrderNum = 25, DictStatus=0, Remark = "会议室类型字典" },
            // 单据规则相关
            new TaktDictType { DictName = "单据规则类型", DictType = "sys_number_rule_type", OrderNum = 26, DictStatus=0, Remark = "单据规则类型字典（1=日常事务 2=人力资源 3=财务核算 4=后勤管理 5=低代码 6=工作流 7=其他）" },
            // 字典管理相关
            new TaktDictType { DictName = "字典类别", DictType = "sys_dict_category", OrderNum = 27, DictStatus=0, Remark = "字典类别字典（0=系统字典 1=动态SQL）" },
            new TaktDictType { DictName = "翻译类别", DictType = "sys_translation_category", OrderNum = 28, DictStatus=0, Remark = "翻译类别字典（0=前端 1=后端 2=移动端）" },
            new TaktDictType { DictName = "设置类别", DictType = "sys_settings_category", OrderNum = 29, DictStatus=0, Remark = "设置类别字典（0=前端 1=后端 2=移动端）" }
        };
    }
}

