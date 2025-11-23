//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRoutineDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 常规业务模块字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 常规业务模块字典数据种子数据提供类
/// </summary>
public class TaktDbSeedRoutineDictData
{
    /// <summary>
    /// 获取默认字典数据
    /// </summary>
    /// <returns>字典数据列表</returns>
    public List<TaktDictData> GetDefaultDictData()
    {
        return new List<TaktDictData>
        {
            // 优先级 - 横排格式
            new TaktDictData { DictType = "routine_priority", DictLabel = "低", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "低优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_priority", DictLabel = "中", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "中优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_priority", DictLabel = "高", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "高优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_priority", DictLabel = "紧急", DictValue = "4", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "紧急优先级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 状态类型 - 横排格式
            new TaktDictData { DictType = "routine_status", DictLabel = "草稿", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "草稿状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_status", DictLabel = "进行中", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "进行中状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_status", DictLabel = "已完成", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "已完成状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_status", DictLabel = "已取消", DictValue = "3", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "已取消状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 审核状态 - 横排格式
            new TaktDictData { DictType = "routine_audit_status", DictLabel = "待审核", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "待审核状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_audit_status", DictLabel = "审核中", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "审核中状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_audit_status", DictLabel = "已通过", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "审核通过状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_audit_status", DictLabel = "已拒绝", DictValue = "3", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "审核拒绝状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 通知类型 - 横排格式
            new TaktDictData { DictType = "routine_notify_type", DictLabel = "系统通知", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "系统通知", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_notify_type", DictLabel = "业务通知", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "业务通知", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_notify_type", DictLabel = "提醒通知", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "提醒通知", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_notify_type", DictLabel = "预警通知", DictValue = "4", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "预警通知", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件类型 - 横排格式
            new TaktDictData { DictType = "routine_file_type", DictLabel = "文档", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "文档文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_file_type", DictLabel = "图片", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "图片文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_file_type", DictLabel = "视频", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "视频文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_file_type", DictLabel = "音频", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "音频文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_file_type", DictLabel = "压缩包", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "压缩包文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 业务类型 - 横排格式
            new TaktDictData { DictType = "routine_business_type", DictLabel = "采购", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "采购业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_business_type", DictLabel = "销售", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "销售业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_business_type", DictLabel = "生产", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "生产业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_business_type", DictLabel = "财务", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "财务业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_business_type", DictLabel = "人力资源", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "人力资源业务", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 流程状态 - 横排格式
            new TaktDictData { DictType = "routine_workflow_status", DictLabel = "未开始", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "流程未开始", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_workflow_status", DictLabel = "进行中", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "流程进行中", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_workflow_status", DictLabel = "已完成", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "流程已完成", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_workflow_status", DictLabel = "已暂停", DictValue = "3", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "流程已暂停", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_workflow_status", DictLabel = "已终止", DictValue = "4", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "流程已终止", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 分类类型 - 横排格式
            new TaktDictData { DictType = "routine_category_type", DictLabel = "产品分类", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "产品分类", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_category_type", DictLabel = "客户分类", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "客户分类", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_category_type", DictLabel = "供应商分类", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "供应商分类", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_category_type", DictLabel = "项目分类", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "项目分类", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 标签类型 - 横排格式
            new TaktDictData { DictType = "routine_tag_type", DictLabel = "重要", DictValue = "1", OrderNum = 1,  CssClass = 5, ListClass = 5, Remark = "重要标签", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_tag_type", DictLabel = "紧急", DictValue = "2", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "紧急标签", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_tag_type", DictLabel = "待处理", DictValue = "3", OrderNum = 3,  CssClass = 4, ListClass = 4, Remark = "待处理标签", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_tag_type", DictLabel = "已完成", DictValue = "4", OrderNum = 4,  CssClass = 3, ListClass = 3, Remark = "已完成标签", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 评分等级 - 横排格式
            new TaktDictData { DictType = "routine_rating_level", DictLabel = "很差", DictValue = "1", OrderNum = 1,  CssClass = 5, ListClass = 5, Remark = "1星评分", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_rating_level", DictLabel = "较差", DictValue = "2", OrderNum = 2,  CssClass = 4, ListClass = 4, Remark = "2星评分", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_rating_level", DictLabel = "一般", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "3星评分", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_rating_level", DictLabel = "较好", DictValue = "4", OrderNum = 4,  CssClass = 2, ListClass = 2, Remark = "4星评分", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_rating_level", DictLabel = "很好", DictValue = "5", OrderNum = 5,  CssClass = 1, ListClass = 1, Remark = "5星评分", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 系统内置 - 横排格式
            new TaktDictData { DictType = "routine_system_builtin", DictLabel = "否", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "非系统内置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_system_builtin", DictLabel = "是", DictValue = "1", OrderNum = 2,  CssClass = 1, ListClass = 1, Remark = "系统内置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 是否加密 - 横排格式
            new TaktDictData { DictType = "routine_encryption_status", DictLabel = "不加密", DictValue = "0", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "数据不加密", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_encryption_status", DictLabel = "加密", DictValue = "1", OrderNum = 2,  CssClass = 1, ListClass = 1, Remark = "数据加密", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // CSS类名 - 横排格式
            new TaktDictData { DictType = "routine_css_class", DictLabel = "默认样式", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "默认CSS样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_css_class", DictLabel = "成功样式", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "成功状态样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_css_class", DictLabel = "警告样式", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "警告状态样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_css_class", DictLabel = "信息样式", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "信息状态样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_css_class", DictLabel = "危险样式", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "危险状态样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 列表类名 - 横排格式
            new TaktDictData { DictType = "routine_list_class", DictLabel = "默认列表", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "默认列表样式", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_list_class", DictLabel = "成功列表", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "成功状态列表", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_list_class", DictLabel = "警告列表", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "警告状态列表", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_list_class", DictLabel = "信息列表", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "信息状态列表", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_list_class", DictLabel = "危险列表", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "危险状态列表", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 字典数据源 - 横排格式
            new TaktDictData { DictType = "routine_dict_source", DictLabel = "系统", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "系统内置数据源", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_dict_source", DictLabel = "SQL", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "SQL查询数据源", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_dict_source", DictLabel = "API", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "API接口数据源", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_dict_source", DictLabel = "文件", DictValue = "3", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "文件数据源", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 编码规则类型 - 横排格式
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "采购单", DictValue = "1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "采购单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "销售单", DictValue = "2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "销售单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "生产单", DictValue = "3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "生产单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "入库单", DictValue = "4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "入库单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "出库单", DictValue = "5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "出库单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "调拨单", DictValue = "6", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "调拨单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "盘点单", DictValue = "7", OrderNum = 7,  CssClass = 2, ListClass = 2, Remark = "盘点单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "报损单", DictValue = "8", OrderNum = 8,  CssClass = 3, ListClass = 3, Remark = "报损单编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_number_rule_type", DictLabel = "其他", DictValue = "9", OrderNum = 9,  CssClass = 4, ListClass = 4, Remark = "其他单据编码规则", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 模块名称 - 横排格式
            new TaktDictData { DictType = "routine_module_name", DictLabel = "系统管理", DictValue = "system", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "系统管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "用户管理", DictValue = "identity", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "用户身份管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "采购管理", DictValue = "purchase", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "采购管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "销售管理", DictValue = "sales", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "销售管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "生产管理", DictValue = "production", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "生产管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "财务管理", DictValue = "finance", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "财务管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "人力资源", DictValue = "hr", OrderNum = 7,  CssClass = 2, ListClass = 2, Remark = "人力资源管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_module_name", DictLabel = "工作流", DictValue = "workflow", OrderNum = 8,  CssClass = 3, ListClass = 3, Remark = "工作流管理模块", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 广告类型 - 横排格式
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "横幅广告", DictValue = "banner", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "横幅广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "角落广告", DictValue = "corner", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "角落广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "浮动广告", DictValue = "floating", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "浮动广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "插屏广告", DictValue = "interstitial", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "插屏广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "覆盖广告", DictValue = "overlay", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "覆盖广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "弹窗广告", DictValue = "popup", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "弹窗广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_type", DictLabel = "开屏广告", DictValue = "splash", OrderNum = 7,  CssClass = 2, ListClass = 2, Remark = "开屏广告", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 广告计费方式 - 横排格式
            new TaktDictData { DictType = "routine_ad_billing_type", DictLabel = "CPM", DictValue = "cpm", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "CPM计费", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_billing_type", DictLabel = "CPC", DictValue = "cpc", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "CPC计费", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_billing_type", DictLabel = "CPA", DictValue = "cpa", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "CPA计费", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 广告计费状态 - 横排格式
            new TaktDictData { DictType = "routine_ad_billing_status", DictLabel = "未计费", DictValue = "0", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "未计费", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_billing_status", DictLabel = "计费中", DictValue = "1", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "计费中", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_billing_status", DictLabel = "已暂停", DictValue = "2", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "已暂停", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "routine_ad_billing_status", DictLabel = "已结束", DictValue = "3", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "已结束", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
        };
    }
}


