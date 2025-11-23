//===================================================================
// 项目名: Takt.Application
// 文件名: TaktEmployeeDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 员工数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.HumanResource.Employee
{
    /// <summary>
    /// 员工基础DTO（与TaktEmployee实体字段严格对应）
    /// </summary>
    public class TaktEmployeeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            WorkLocation = string.Empty;
            EmergencyContact = string.Empty;
            EmergencyPhone = string.Empty;
            HouseholdAddress = string.Empty;
            CurrentAddress = string.Empty;
            GraduateSchool = string.Empty;
            Major = string.Empty;
        }

        /// <summary>
        /// 员工ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 性别(0=未知 1=男 2=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 员工类型(1=正式员工 2=试用期 3=实习生 4=兼职 5=外包)
        /// </summary>
        public int EmployeeType { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime? RegularDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 员工状态(1=在职 2=试用期 3=离职 4=停薪留职 5=退休)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级ID
        /// </summary>
        public long? ManagerId { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string? HouseholdAddress { get; set; }

        /// <summary>
        /// 现居住地址
        /// </summary>
        public string? CurrentAddress { get; set; }

        /// <summary>
        /// 学历(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        public int? Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string? GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        public Organization.TaktDepartmentDto? Department { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        public Organization.TaktPositionDto? Position { get; set; }

        /// <summary>
        /// 直属上级
        /// </summary>
        public TaktEmployeeDto? Manager { get; set; }

        /// <summary>
        /// 下属员工
        /// </summary>
        public List<TaktEmployeeDto>? Subordinates { get; set; }

        /// <summary>
        /// 员工合同
        /// </summary>
        public List<TaktEmployeeContractDto>? Contracts { get; set; }

        /// <summary>
        /// 员工薪资
        /// </summary>
        public List<Salary.TaktEmployeeSalaryDto>? Salaries { get; set; }

        /// <summary>
        /// 考勤记录
        /// </summary>
        public List<Attendance.TaktAttendanceDto>? Attendances { get; set; }

        /// <summary>
        /// 请假记录
        /// </summary>
        public List<Leave.TaktLeaveDto>? Leaves { get; set; }

        /// <summary>
        /// 培训记录
        /// </summary>
        public List<Training.TaktTrainingRecordDto>? TrainingRecords { get; set; }

        /// <summary>
        /// 绩效记录
        /// </summary>
        public List<Performance.TaktPerformanceDto>? Performances { get; set; }
    }

    /// <summary>
    /// 员工查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeQueryDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            WorkLocation = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string? EmployeeNo { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long? DepartmentId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public long? PositionId { get; set; }

        /// <summary>
        /// 员工类型
        /// </summary>
        public int? EmployeeType { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级ID
        /// </summary>
        public long? ManagerId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public int? Education { get; set; }

        /// <summary>
        /// 入职开始日期
        /// </summary>
        public DateTime? HireDateStart { get; set; }

        /// <summary>
        /// 入职结束日期
        /// </summary>
        public DateTime? HireDateEnd { get; set; }
    }

    /// <summary>
    /// 员工创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeCreateDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            WorkLocation = string.Empty;
            EmergencyContact = string.Empty;
            EmergencyPhone = string.Empty;
            HouseholdAddress = string.Empty;
            CurrentAddress = string.Empty;
            GraduateSchool = string.Empty;
            Major = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 性别(0=未知 1=男 2=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DepartmentId { get; set; }

        /// <summary>
        /// 职位ID
        /// </summary>
        public long PositionId { get; set; }

        /// <summary>
        /// 员工类型(1=正式员工 2=试用期 3=实习生 4=兼职 5=外包)
        /// </summary>
        public int EmployeeType { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime? RegularDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 员工状态(1=在职 2=试用期 3=离职 4=停薪留职 5=退休)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级ID
        /// </summary>
        public long? ManagerId { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string? HouseholdAddress { get; set; }

        /// <summary>
        /// 现居住地址
        /// </summary>
        public string? CurrentAddress { get; set; }

        /// <summary>
        /// 学历(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        public int? Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string? GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeUpdateDto : TaktEmployeeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeUpdateDto() : base()
        {
        }

        /// <summary>
        /// 员工ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long EmployeeId { get; set; }
    }

    /// <summary>
    /// 员工导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeImportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            WorkLocation = string.Empty;
            EmergencyContact = string.Empty;
            EmergencyPhone = string.Empty;
            HouseholdAddress = string.Empty;
            CurrentAddress = string.Empty;
            GraduateSchool = string.Empty;
            Major = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 性别(0=未知 1=男 2=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 员工类型(1=正式员工 2=试用期 3=实习生 4=兼职 5=外包)
        /// </summary>
        public int EmployeeType { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// 转正日期
        /// </summary>
        public DateTime? RegularDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? LeaveDate { get; set; }

        /// <summary>
        /// 员工状态(1=在职 2=试用期 3=离职 4=停薪留职 5=退休)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string? HouseholdAddress { get; set; }

        /// <summary>
        /// 现居住地址
        /// </summary>
        public string? CurrentAddress { get; set; }

        /// <summary>
        /// 学历(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        public int? Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string? GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? GraduateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            Gender = string.Empty;
            BirthDate = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            DepartmentName = string.Empty;
            PositionName = string.Empty;
            EmployeeType = string.Empty;
            HireDate = string.Empty;
            RegularDate = string.Empty;
            LeaveDate = string.Empty;
            Status = string.Empty;
            WorkLocation = string.Empty;
            ManagerName = string.Empty;
            EmergencyContact = string.Empty;
            EmergencyPhone = string.Empty;
            HouseholdAddress = string.Empty;
            CurrentAddress = string.Empty;
            Education = string.Empty;
            GraduateSchool = string.Empty;
            Major = string.Empty;
            GraduateDate = string.Empty;
            CreateTime = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDate { get; set; } = string.Empty;

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 转正日期
        /// </summary>
        public string? RegularDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public string? LeaveDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string? HouseholdAddress { get; set; }

        /// <summary>
        /// 现居住地址
        /// </summary>
        public string? CurrentAddress { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string? Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string? GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public string? GraduateDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 员工模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeTemplateDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            EnglishName = string.Empty;
            Gender = string.Empty;
            BirthDate = string.Empty;
            IdCard = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            DepartmentName = string.Empty;
            PositionName = string.Empty;
            EmployeeType = string.Empty;
            HireDate = string.Empty;
            RegularDate = string.Empty;
            LeaveDate = string.Empty;
            Status = string.Empty;
            WorkLocation = string.Empty;
            ManagerName = string.Empty;
            EmergencyContact = string.Empty;
            EmergencyPhone = string.Empty;
            HouseholdAddress = string.Empty;
            CurrentAddress = string.Empty;
            Education = string.Empty;
            GraduateSchool = string.Empty;
            Major = string.Empty;
            GraduateDate = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 英文姓名
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDate { get; set; } = string.Empty;

        /// <summary>
        /// 身份证号
        /// </summary>
        public string? IdCard { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; } = string.Empty;

        /// <summary>
        /// 员工类型
        /// </summary>
        public string EmployeeType { get; set; } = string.Empty;

        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; } = string.Empty;

        /// <summary>
        /// 转正日期
        /// </summary>
        public string? RegularDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public string? LeaveDate { get; set; }

        /// <summary>
        /// 员工状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 工作地点
        /// </summary>
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 直属上级姓名
        /// </summary>
        public string? ManagerName { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string? HouseholdAddress { get; set; }

        /// <summary>
        /// 现居住地址
        /// </summary>
        public string? CurrentAddress { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string? Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string? GraduateSchool { get; set; }

        /// <summary>
        /// 专业
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public string? GraduateDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工删除DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public class TaktEmployeeStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeStatusDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工状态(1=在职 2=试用期 3=离职 4=停薪留职 5=退休)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


