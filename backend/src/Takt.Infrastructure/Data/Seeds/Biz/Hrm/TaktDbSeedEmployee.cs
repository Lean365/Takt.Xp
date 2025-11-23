//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedEmployee.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : HRM员工数据提供类
//===================================================================

using Takt.Domain.Entities.HumanResource.Employee;

namespace Takt.Infrastructure.Data.Seeds.Biz.Hrm;

/// <summary>
/// HRM员工数据提供类
/// </summary>
public class TaktDbSeedEmployee
{
    /// <summary>
    /// 获取默认员工数据
    /// </summary>
    /// <returns>员工数据列表</returns>
    public List<TaktEmployee> GetDefaultEmployees()
    {
        return new List<TaktEmployee>
        {
            new TaktEmployee { EmployeeNo = "HR001", EmployeeName = "张人力资源", EnglishName = "Zhang HR", Gender = 1, BirthDate = new DateTime(1985, 5, 15), IdCard = "110101198505150001", Mobile = "13800138001", Email = "hr.director@takt.cn", DepartmentId = 1, PositionId = 1, EmployeeType = 1, HireDate = new DateTime(2020, 1, 1), RegularDate = new DateTime(2020, 4, 1), Status = 1, WorkLocation = "北京" },
            new TaktEmployee { EmployeeNo = "HR002", EmployeeName = "李人事", EnglishName = "Li Personnel", Gender = 2, BirthDate = new DateTime(1988, 8, 20), IdCard = "110101198808200002", Mobile = "13800138002", Email = "hr.manager@takt.cn", DepartmentId = 1, PositionId = 2, EmployeeType = 1, HireDate = new DateTime(2021, 3, 1), RegularDate = new DateTime(2021, 6, 1), Status = 1, WorkLocation = "北京" },
            new TaktEmployee { EmployeeNo = "HR003", EmployeeName = "王招聘", EnglishName = "Wang Recruitment", Gender = 2, BirthDate = new DateTime(1990, 12, 10), IdCard = "110101199012100003", Mobile = "13800138003", Email = "recruitment@takt.cn", DepartmentId = 2, PositionId = 4, EmployeeType = 1, HireDate = new DateTime(2022, 6, 1), RegularDate = new DateTime(2022, 9, 1), Status = 1, WorkLocation = "北京" },
            new TaktEmployee { EmployeeNo = "HR004", EmployeeName = "赵培训", EnglishName = "Zhao Training", Gender = 1, BirthDate = new DateTime(1992, 3, 25), IdCard = "110101199203250004", Mobile = "13800138004", Email = "training@takt.cn", DepartmentId = 3, PositionId = 5, EmployeeType = 1, HireDate = new DateTime(2022, 9, 1), RegularDate = new DateTime(2022, 12, 1), Status = 1, WorkLocation = "北京" },
            new TaktEmployee { EmployeeNo = "HR005", EmployeeName = "钱薪酬", EnglishName = "Qian Compensation", Gender = 2, BirthDate = new DateTime(1989, 7, 8), IdCard = "110101198907080005", Mobile = "13800138005", Email = "compensation@takt.cn", DepartmentId = 4, PositionId = 6, EmployeeType = 1, HireDate = new DateTime(2021, 12, 1), RegularDate = new DateTime(2022, 3, 1), Status = 1, WorkLocation = "北京" }
        };
    }
}


