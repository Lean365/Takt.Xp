//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedHrDictType.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 人力资源相关字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 人力资源相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedHrDictType
{
    /// <summary>
    /// 获取人力资源相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetHrDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "员工状态", DictType = "hr_employee_status", OrderNum = 1, DictStatus=0, Remark = "员工状态字典" },
            new TaktDictType { DictName = "员工类型", DictType = "hr_employee_type", OrderNum = 2, DictStatus=0, Remark = "员工类型字典" },
            new TaktDictType { DictName = "学历类型", DictType = "hr_education_level", OrderNum = 3, DictStatus=0, Remark = "学历类型字典" },
            new TaktDictType { DictName = "婚姻状况", DictType = "hr_marital_status", OrderNum = 4, DictStatus=0, Remark = "婚姻状况字典" },
            new TaktDictType { DictName = "政治面貌", DictType = "hr_political_status", OrderNum = 5, DictStatus=0, Remark = "政治面貌字典" },
            new TaktDictType { DictName = "工作类型", DictType = "hr_work_type", OrderNum = 6, DictStatus=0, Remark = "工作类型字典" },
            new TaktDictType { DictName = "职位类型", DictType = "hr_position_type", OrderNum = 7, DictStatus=0, Remark = "职位类型字典" },
            new TaktDictType { DictName = "职位等级", DictType = "hr_position_level", OrderNum = 8, DictStatus=0, Remark = "职位等级字典" },
            new TaktDictType { DictName = "招聘渠道", DictType = "hr_recruitment_channel", OrderNum = 9, DictStatus=0, Remark = "招聘渠道字典" },
            new TaktDictType { DictName = "合同类型", DictType = "hr_contract_type", OrderNum = 10, DictStatus=0, Remark = "合同类型字典" },
            new TaktDictType { DictName = "晋升类型", DictType = "hr_promotion_type", OrderNum = 11, DictStatus=0, Remark = "晋升类型字典" },
            new TaktDictType { DictName = "离职类型", DictType = "hr_resignation_type", OrderNum = 12, DictStatus=0, Remark = "离职类型字典" },
            new TaktDictType { DictName = "考勤状态", DictType = "hr_attendance_status", OrderNum = 13, DictStatus=0, Remark = "考勤状态字典" },
            new TaktDictType { DictName = "考勤类型", DictType = "hr_attendance_type", OrderNum = 14, DictStatus=0, Remark = "考勤类型字典" },
            new TaktDictType { DictName = "请假类型", DictType = "hr_leave_type", OrderNum = 15, DictStatus=0, Remark = "请假类型字典" },
            new TaktDictType { DictName = "培训类型", DictType = "hr_training_type", OrderNum = 16, DictStatus=0, Remark = "培训类型字典" },
            new TaktDictType { DictName = "加班类型", DictType = "hr_overtime_type", OrderNum = 17, DictStatus=0, Remark = "加班类型字典" },
            new TaktDictType { DictName = "调休类型", DictType = "hr_compensatory_leave_type", OrderNum = 18, DictStatus=0, Remark = "调休类型字典" }
        };
    }
}

