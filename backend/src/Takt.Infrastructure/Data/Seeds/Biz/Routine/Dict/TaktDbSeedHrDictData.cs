//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedHrDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 人力资源相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 人力资源相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedHrDictData
{
    /// <summary>
    /// 获取人力资源相关字典数据
    /// </summary>
    /// <returns>人力资源相关字典数据列表</returns>
    public List<TaktDictData> GetHrDictData()
    {
        return new List<TaktDictData>
        {
            // 员工状态 - 横排格式
            new TaktDictData { DictType = "hr_employee_status", DictLabel = "在职", DictValue = "ACTIVE", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "在职员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_status", DictLabel = "离职", DictValue = "RESIGNED", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "离职员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_status", DictLabel = "试用期", DictValue = "PROBATION", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "试用期员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_status", DictLabel = "实习", DictValue = "INTERNSHIP", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "实习员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_status", DictLabel = "停薪留职", DictValue = "SUSPENDED", OrderNum = 5,  CssClass = 6, ListClass = 6, Remark = "停薪留职员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 员工类型 - 横排格式
            new TaktDictData { DictType = "hr_employee_type", DictLabel = "正式员工", DictValue = "REGULAR", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "正式员工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_type", DictLabel = "合同工", DictValue = "CONTRACT", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "合同工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_type", DictLabel = "临时工", DictValue = "TEMPORARY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "临时工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_type", DictLabel = "实习生", DictValue = "INTERN", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "实习生", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_employee_type", DictLabel = "顾问", DictValue = "CONSULTANT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "顾问", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 学历类型 - 横排格式
            new TaktDictData { DictType = "hr_education_level", DictLabel = "高中", DictValue = "HIGH_SCHOOL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "高中学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_education_level", DictLabel = "中专", DictValue = "TECHNICAL_SCHOOL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "中专学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_education_level", DictLabel = "大专", DictValue = "COLLEGE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "大专学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_education_level", DictLabel = "本科", DictValue = "BACHELOR", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "本科学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_education_level", DictLabel = "硕士", DictValue = "MASTER", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "硕士学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_education_level", DictLabel = "博士", DictValue = "DOCTOR", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "博士学历", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 婚姻状况 - 横排格式
            new TaktDictData { DictType = "hr_marital_status", DictLabel = "未婚", DictValue = "SINGLE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "未婚", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_marital_status", DictLabel = "已婚", DictValue = "MARRIED", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "已婚", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_marital_status", DictLabel = "离异", DictValue = "DIVORCED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "离异", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_marital_status", DictLabel = "丧偶", DictValue = "WIDOWED", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "丧偶", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 政治面貌 - 横排格式
            new TaktDictData { DictType = "hr_political_status", DictLabel = "群众", DictValue = "MASSES", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "群众", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_political_status", DictLabel = "共青团员", DictValue = "COMMUNIST_YOUTH", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "共青团员", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_political_status", DictLabel = "中共党员", DictValue = "COMMUNIST_PARTY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "中共党员", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_political_status", DictLabel = "民主党派", DictValue = "DEMOCRATIC_PARTY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "民主党派", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 工作类型 - 横排格式
            new TaktDictData { DictType = "hr_work_type", DictLabel = "全职", DictValue = "FULL_TIME", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "全职工作", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_work_type", DictLabel = "兼职", DictValue = "PART_TIME", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "兼职工作", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_work_type", DictLabel = "实习", DictValue = "INTERNSHIP", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "实习工作", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_work_type", DictLabel = "临时", DictValue = "TEMPORARY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "临时工作", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 考勤状态 - 横排格式
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "正常", DictValue = "NORMAL", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "正常出勤", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "迟到", DictValue = "LATE", OrderNum = 2,  CssClass = 4, ListClass = 4, Remark = "迟到", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "早退", DictValue = "EARLY_LEAVE", OrderNum = 3,  CssClass = 4, ListClass = 4, Remark = "早退", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "旷工", DictValue = "ABSENT", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "旷工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "请假", DictValue = "LEAVE", OrderNum = 5,  CssClass = 3, ListClass = 3, Remark = "请假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_status", DictLabel = "出差", DictValue = "BUSINESS_TRIP", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "出差", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 请假类型 - 横排格式
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "年假", DictValue = "ANNUAL_LEAVE", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "年假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "病假", DictValue = "SICK_LEAVE", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "病假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "事假", DictValue = "PERSONAL_LEAVE", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "事假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "婚假", DictValue = "MARRIAGE_LEAVE", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "婚假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "产假", DictValue = "MATERNITY_LEAVE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "产假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_leave_type", DictLabel = "丧假", DictValue = "BEREAVEMENT_LEAVE", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "丧假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 培训类型 - 横排格式
            new TaktDictData { DictType = "hr_training_type", DictLabel = "入职培训", DictValue = "ONBOARDING", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "入职培训", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_training_type", DictLabel = "技能培训", DictValue = "SKILL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "技能培训", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_training_type", DictLabel = "管理培训", DictValue = "MANAGEMENT", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "管理培训", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_training_type", DictLabel = "安全培训", DictValue = "SAFETY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "安全培训", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_training_type", DictLabel = "合规培训", DictValue = "COMPLIANCE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "合规培训", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 职位类型 - 横排格式
            new TaktDictData { DictType = "hr_position_type", DictLabel = "管理职", DictValue = "MANAGEMENT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "管理职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_type", DictLabel = "专业职", DictValue = "PROFESSIONAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "专业职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_type", DictLabel = "技术职", DictValue = "TECHNICAL", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "技术职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_type", DictLabel = "操作职", DictValue = "OPERATIONAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "操作职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_type", DictLabel = "销售职", DictValue = "SALES", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "销售职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_type", DictLabel = "支持职", DictValue = "SUPPORT", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "支持职位", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 职位等级 - 横排格式
            new TaktDictData { DictType = "hr_position_level", DictLabel = "L1", DictValue = "L1", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "初级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_level", DictLabel = "L2", DictValue = "L2", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "中级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_level", DictLabel = "L3", DictValue = "L3", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "高级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_level", DictLabel = "L4", DictValue = "L4", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "专家级", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_position_level", DictLabel = "L5", DictValue = "L5", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "资深专家", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 招聘渠道 - 横排格式
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "智联招聘", DictValue = "ZHAOPIN", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "智联招聘", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "前程无忧", DictValue = "51JOB", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "前程无忧", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "BOSS直聘", DictValue = "BOSS", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "BOSS直聘", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "拉勾网", DictValue = "LAGOU", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "拉勾网", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "猎聘网", DictValue = "LIEPIN", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "猎聘网", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "内推", DictValue = "REFERRAL", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "内部推荐", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "校园招聘", DictValue = "CAMPUS", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "校园招聘", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_recruitment_channel", DictLabel = "其他", DictValue = "OTHER", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "其他渠道", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 合同类型 - 横排格式
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "正式合同", DictValue = "FORMAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "正式劳动合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "试用期合同", DictValue = "PROBATION", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "试用期合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "临时合同", DictValue = "TEMPORARY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "临时合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "实习合同", DictValue = "INTERNSHIP", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "实习合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "劳务合同", DictValue = "SERVICE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "劳务合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_contract_type", DictLabel = "顾问合同", DictValue = "CONSULTANT", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "顾问合同", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 晋升类型 - 横排格式
            new TaktDictData { DictType = "hr_promotion_type", DictLabel = "职位晋升", DictValue = "POSITION", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "职位晋升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_promotion_type", DictLabel = "薪资晋升", DictValue = "SALARY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "薪资晋升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_promotion_type", DictLabel = "职级晋升", DictValue = "LEVEL", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "职级晋升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_promotion_type", DictLabel = "技能晋升", DictValue = "SKILL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "技能晋升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_promotion_type", DictLabel = "管理晋升", DictValue = "MANAGEMENT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "管理晋升", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 离职类型 - 横排格式
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "主动离职", DictValue = "VOLUNTARY", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "主动离职", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "被动离职", DictValue = "INVOLUNTARY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "被动离职", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "合同到期", DictValue = "CONTRACT_EXPIRED", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "合同到期", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "退休", DictValue = "RETIREMENT", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "退休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "解雇", DictValue = "TERMINATION", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "解雇", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_resignation_type", DictLabel = "试用期不合格", DictValue = "PROBATION_FAILED", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "试用期不合格", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 考勤类型 - 横排格式
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "正常出勤", DictValue = "NORMAL", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "正常出勤", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "迟到", DictValue = "LATE", OrderNum = 2,  CssClass = 4, ListClass = 4, Remark = "迟到", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "早退", DictValue = "EARLY_LEAVE", OrderNum = 3,  CssClass = 4, ListClass = 4, Remark = "早退", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "旷工", DictValue = "ABSENT", OrderNum = 4,  CssClass = 5, ListClass = 5, Remark = "旷工", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "请假", DictValue = "LEAVE", OrderNum = 5,  CssClass = 3, ListClass = 3, Remark = "请假", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "出差", DictValue = "BUSINESS_TRIP", OrderNum = 6,  CssClass = 1, ListClass = 1, Remark = "出差", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_attendance_type", DictLabel = "加班", DictValue = "OVERTIME", OrderNum = 7,  CssClass = 6, ListClass = 6, Remark = "加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 加班类型 - 横排格式
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "工作日加班", DictValue = "WEEKDAY", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "工作日加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "周末加班", DictValue = "WEEKEND", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "周末加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "节假日加班", DictValue = "HOLIDAY", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "节假日加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "紧急加班", DictValue = "EMERGENCY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "紧急加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "项目加班", DictValue = "PROJECT", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "项目加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_overtime_type", DictLabel = "值班加班", DictValue = "DUTY", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "值班加班", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 调休类型 - 横排格式
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "加班调休", DictValue = "OVERTIME", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "加班调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "值班调休", DictValue = "DUTY", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "值班调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "出差调休", DictValue = "BUSINESS_TRIP", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "出差调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "培训调休", DictValue = "TRAINING", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "培训调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "会议调休", DictValue = "MEETING", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "会议调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "hr_compensatory_leave_type", DictLabel = "其他调休", DictValue = "OTHER", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "其他调休", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}

