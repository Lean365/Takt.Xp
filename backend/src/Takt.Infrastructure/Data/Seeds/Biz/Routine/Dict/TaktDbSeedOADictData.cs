//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedOADictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : OA相关字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// OA相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedOADictData
{
    /// <summary>
    /// 获取OA相关字典数据
    /// </summary>
    /// <returns>OA相关字典数据列表</returns>
    public List<TaktDictData> GetOADictData()
    {
        return new List<TaktDictData>
        {
            // 会议类型
            new TaktDictData { DictType = "sys_meeting_type", DictLabel = "部门会议", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "部门会议" },
            new TaktDictData { DictType = "sys_meeting_type", DictLabel = "项目会议", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "项目会议" },
            new TaktDictData { DictType = "sys_meeting_type", DictLabel = "周例会", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "周例会" },
            new TaktDictData { DictType = "sys_meeting_type", DictLabel = "月度会议", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "月度会议" },

            // 会议状态
            new TaktDictData { DictType = "sys_meeting_status", DictLabel = "未开始", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "未开始状态" },
            new TaktDictData { DictType = "sys_meeting_status", DictLabel = "进行中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "进行中状态" },
            new TaktDictData { DictType = "sys_meeting_status", DictLabel = "已结束", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已结束状态" },
            new TaktDictData { DictType = "sys_meeting_status", DictLabel = "已取消", DictValue = "4",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已取消状态" },

            // 车辆类型
            new TaktDictData { DictType = "sys_vehicle_type", DictLabel = "轿车", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "轿车" },
            new TaktDictData { DictType = "sys_vehicle_type", DictLabel = "商务车", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "商务车" },
            new TaktDictData { DictType = "sys_vehicle_type", DictLabel = "面包车", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "面包车" },

            // 车辆状态
            new TaktDictData { DictType = "sys_vehicle_status", DictLabel = "空闲", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "空闲状态" },
            new TaktDictData { DictType = "sys_vehicle_status", DictLabel = "使用中", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "使用中状态" },
            new TaktDictData { DictType = "sys_vehicle_status", DictLabel = "维修中", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "维修中状态" },

            // 日程类型
            new TaktDictData { DictType = "sys_schedule_type", DictLabel = "工作日程", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "工作日程" },
            new TaktDictData { DictType = "sys_schedule_type", DictLabel = "会议日程", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "会议日程" },
            new TaktDictData { DictType = "sys_schedule_type", DictLabel = "个人日程", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "个人日程" },

            // 日程优先级
            new TaktDictData { DictType = "sys_schedule_priority", DictLabel = "紧急", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "紧急优先级" },
            new TaktDictData { DictType = "sys_schedule_priority", DictLabel = "重要", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "重要优先级" },
            new TaktDictData { DictType = "sys_schedule_priority", DictLabel = "普通", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "普通优先级" },

            // 知识分类
            new TaktDictData { DictType = "sys_knowledge_category", DictLabel = "规章制度", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "规章制度" },
            new TaktDictData { DictType = "sys_knowledge_category", DictLabel = "技术文档", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "技术文档" },
            new TaktDictData { DictType = "sys_knowledge_category", DictLabel = "经验分享", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "经验分享" },

            // 知识权限
            new TaktDictData { DictType = "sys_knowledge_permission", DictLabel = "公开", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "公开权限" },
            new TaktDictData { DictType = "sys_knowledge_permission", DictLabel = "部门可见", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "部门可见" },
            new TaktDictData { DictType = "sys_knowledge_permission", DictLabel = "私有", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "私有权限" },

            // 联系人分组
            new TaktDictData { DictType = "sys_contact_group", DictLabel = "公司内部", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "公司内部联系人" },
            new TaktDictData { DictType = "sys_contact_group", DictLabel = "客户", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "客户联系人" },
            new TaktDictData { DictType = "sys_contact_group", DictLabel = "供应商", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "供应商联系人" },

            // 协作类型
            new TaktDictData { DictType = "sys_collaboration_type", DictLabel = "任务协作", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "任务协作" },
            new TaktDictData { DictType = "sys_collaboration_type", DictLabel = "项目协作", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "项目协作" },
            new TaktDictData { DictType = "sys_collaboration_type", DictLabel = "文档协作", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "文档协作" },

            // 协作状态
            new TaktDictData { DictType = "sys_collaboration_status", DictLabel = "进行中", DictValue = "1",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "进行中状态" },
            new TaktDictData { DictType = "sys_collaboration_status", DictLabel = "已完成", DictValue = "2",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "已完成状态" },
            new TaktDictData { DictType = "sys_collaboration_status", DictLabel = "已取消", DictValue = "3",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已取消状态" },

            // 任务类型
            new TaktDictData { DictType = "sys_task_type", DictLabel = "程序集", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "程序集任务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_task_type", DictLabel = "网络请求", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "网络请求任务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_task_type", DictLabel = "SQL语句", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "SQL语句任务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 合同类型
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "采购合同", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "采购合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "销售合同", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "销售合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "服务合同", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "服务合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "租赁合同", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "租赁合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "劳务合同", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "劳务合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "技术合同", DictValue = "6", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "技术合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_type", DictLabel = "其他合同", DictValue = "7", CssClass = 2, ListClass = 2, OrderNum = 7,  Remark = "其他合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 合同方类型
            new TaktDictData { DictType = "sys_contract_party_type", DictLabel = "客户", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "客户", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_party_type", DictLabel = "供应商", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "供应商", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_party_type", DictLabel = "合作伙伴", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "合作伙伴", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_party_type", DictLabel = "其他", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "其他", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 合同状态
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "草稿", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "草稿状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "待审核", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "待审核状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已审核", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "已审核状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已批准", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "已批准状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已签署", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "已签署状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "执行中", DictValue = "5", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "执行中状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已完成", DictValue = "6", CssClass = 2, ListClass = 2, OrderNum = 7,  Remark = "已完成状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已终止", DictValue = "7", CssClass = 3, ListClass = 3, OrderNum = 8,  Remark = "已终止状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_contract_status", DictLabel = "已作废", DictValue = "8", CssClass = 4, ListClass = 4, OrderNum = 9,  Remark = "已作废状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件扩展名
            new TaktDictData { DictType = "sys_file_extension", DictLabel = "PDF", DictValue = "pdf", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "PDF文档", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_extension", DictLabel = "Word", DictValue = "docx", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "Word文档", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_extension", DictLabel = "Excel", DictValue = "xlsx", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "Excel表格", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_extension", DictLabel = "图片", DictValue = "jpg", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "图片文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_extension", DictLabel = "压缩包", DictValue = "zip", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "压缩包文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件类型
            new TaktDictData { DictType = "sys_file_type", DictLabel = "文档", DictValue = "document", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "文档类型", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_type", DictLabel = "表格", DictValue = "spreadsheet", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "表格类型", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_type", DictLabel = "图片", DictValue = "image", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "图片类型", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_type", DictLabel = "视频", DictValue = "video", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "视频类型", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_file_type", DictLabel = "音频", DictValue = "audio", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "音频类型", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 存储类型
            new TaktDictData { DictType = "sys_storage_type", DictLabel = "本地存储", DictValue = "local", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "本地存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_storage_type", DictLabel = "网络存储", DictValue = "network", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "网络存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_storage_type", DictLabel = "云存储", DictValue = "cloud", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "云存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_storage_type", DictLabel = "数据库存储", DictValue = "database", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "数据库存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // ISO文档类型
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "质量手册", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "质量手册", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "程序文件", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "程序文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "作业指导书", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "作业指导书", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "表格记录", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "表格记录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "外来文件", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "外来文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_iso_document_type", DictLabel = "其他", DictValue = "6", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "其他文档", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 重要程度
            new TaktDictData { DictType = "sys_importance_level", DictLabel = "一般", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "一般重要程度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_importance_level", DictLabel = "重要", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "重要程度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_importance_level", DictLabel = "非常重要", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "非常重要程度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_importance_level", DictLabel = "紧急", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 4,  Remark = "紧急重要程度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否强制
            new TaktDictData { DictType = "sys_is_mandatory", DictLabel = "否", DictValue = "0", CssClass = 2, ListClass = 2, OrderNum = 1,  Remark = "非强制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_is_mandatory", DictLabel = "是", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 2,  Remark = "强制", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否公开
            new TaktDictData { DictType = "sys_is_public", DictLabel = "否", DictValue = "0", CssClass = 2, ListClass = 2, OrderNum = 1,  Remark = "非公开", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_is_public", DictLabel = "是", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 2,  Remark = "公开", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 法规类型
            new TaktDictData { DictType = "sys_law_type", DictLabel = "法律", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "法律", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "行政法规", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "行政法规", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "部门规章", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "部门规章", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "地方性法规", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "地方性法规", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "地方政府规章", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "地方政府规章", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "标准规范", DictValue = "6", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "标准规范", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_law_type", DictLabel = "其他", DictValue = "7", CssClass = 2, ListClass = 2, OrderNum = 7,  Remark = "其他法规", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 规章制度类型
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "管理制度", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "管理制度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "操作规程", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "操作规程", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "工作标准", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "工作标准", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "岗位职责", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "岗位职责", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "考核制度", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "考核制度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "奖惩制度", DictValue = "6", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "奖惩制度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_regulation_type", DictLabel = "其他", DictValue = "7", CssClass = 2, ListClass = 2, OrderNum = 7,  Remark = "其他制度", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否需要签到
            new TaktDictData { DictType = "sys_need_signin", DictLabel = "否", DictValue = "0", CssClass = 2, ListClass = 2, OrderNum = 1,  Remark = "不需要签到", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_need_signin", DictLabel = "是", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 2,  Remark = "需要签到", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 会议室类型
            new TaktDictData { DictType = "sys_meeting_room_type", DictLabel = "小型会议室", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "小型会议室", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_meeting_room_type", DictLabel = "中型会议室", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "中型会议室", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_meeting_room_type", DictLabel = "大型会议室", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "大型会议室", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_meeting_room_type", DictLabel = "培训室", DictValue = "3", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "培训室", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_meeting_room_type", DictLabel = "视频会议室", DictValue = "4", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "视频会议室", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 单据规则类型
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "日常事务", DictValue = "1", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "日常事务模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "人力资源", DictValue = "2", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "人力资源模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "财务核算", DictValue = "3", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "财务核算模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "后勤管理", DictValue = "4", CssClass = 4, ListClass = 4, OrderNum = 4,  Remark = "后勤管理模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "低代码", DictValue = "5", CssClass = 5, ListClass = 5, OrderNum = 5,  Remark = "低代码模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "工作流", DictValue = "6", CssClass = 1, ListClass = 1, OrderNum = 6,  Remark = "工作流模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_number_rule_type", DictLabel = "其他", DictValue = "7", CssClass = 2, ListClass = 2, OrderNum = 7,  Remark = "其他模块单据规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 字典类别
            new TaktDictData { DictType = "sys_dict_category", DictLabel = "系统字典", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "系统内置字典", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_dict_category", DictLabel = "动态SQL", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "动态SQL查询字典", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 翻译类别
            new TaktDictData { DictType = "sys_translation_category", DictLabel = "前端", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "前端翻译", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_translation_category", DictLabel = "后端", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "后端翻译", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_translation_category", DictLabel = "移动端", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "移动端翻译", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 设置类别
            new TaktDictData { DictType = "sys_settings_category", DictLabel = "前端", DictValue = "0", CssClass = 1, ListClass = 1, OrderNum = 1,  Remark = "前端设置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_settings_category", DictLabel = "后端", DictValue = "1", CssClass = 2, ListClass = 2, OrderNum = 2,  Remark = "后端设置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "sys_settings_category", DictLabel = "移动端", DictValue = "2", CssClass = 3, ListClass = 3, OrderNum = 3,  Remark = "移动端设置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

