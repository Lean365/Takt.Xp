//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktDriverDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 驾驶员数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Vehicle
{
    /// <summary>
    /// 驾驶员基础DTO
    /// </summary>
    public class TaktDriverDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long DriverId { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string LicenseNo { get; set; }

        /// <summary>
        /// 驾驶证类型
        /// </summary>
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态
        /// </summary>
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        public int LicensePoints { get; set; }

        /// <summary>
        /// 驾驶证图片
        /// </summary>
        public string? LicenseImages { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验
        /// </summary>
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分
        /// </summary>
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分
        /// </summary>
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分
        /// </summary>
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机
        /// </summary>
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆
        /// </summary>
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆
        /// </summary>
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车
        /// </summary>
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间
        /// </summary>
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 健康证图片
        /// </summary>
        public string? HealthCertImages { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        public string? SkillCertificates { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

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
        /// 是否删除
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
    }

    /// <summary>
    /// 驾驶员查询DTO
    /// </summary>
    public class TaktDriverQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverQueryDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
            Department = string.Empty;
        }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string? EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string? EmployeeNo { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string? LicenseNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 驾驶证类型
        /// </summary>
        public int? LicenseType { get; set; }

        /// <summary>
        /// 驾驶证状态
        /// </summary>
        public int? LicenseStatus { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Gender { get; set; }

        /// <summary>
        /// 是否专职司机
        /// </summary>
        public int? IsFullTime { get; set; }
    }

    /// <summary>
    /// 驾驶员创建DTO
    /// </summary>
    public class TaktDriverCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverCreateDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string LicenseNo { get; set; }

        /// <summary>
        /// 驾驶证类型
        /// </summary>
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态
        /// </summary>
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        public int LicensePoints { get; set; }

        /// <summary>
        /// 驾驶证图片
        /// </summary>
        public string? LicenseImages { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验
        /// </summary>
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分
        /// </summary>
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分
        /// </summary>
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分
        /// </summary>
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机
        /// </summary>
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆
        /// </summary>
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆
        /// </summary>
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车
        /// </summary>
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间
        /// </summary>
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 健康证图片
        /// </summary>
        public string? HealthCertImages { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        public string? SkillCertificates { get; set; }
    }

    /// <summary>
    /// 驾驶员更新DTO
    /// </summary>
    public class TaktDriverUpdateDto : TaktDriverCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long DriverId { get; set; }
    }

    /// <summary>
    /// 驾驶员删除DTO
    /// </summary>
    public class TaktDriverDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long DriverId { get; set; }
    }

    /// <summary>
    /// 驾驶员批量删除DTO
    /// </summary>
    public class TaktDriverBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> DriverIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 驾驶员导入DTO
    /// </summary>
    public class TaktDriverImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverImportDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string LicenseNo { get; set; }

        /// <summary>
        /// 驾驶证类型
        /// </summary>
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态
        /// </summary>
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        public int LicensePoints { get; set; }

        /// <summary>
        /// 驾驶证图片
        /// </summary>
        public string? LicenseImages { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验
        /// </summary>
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分
        /// </summary>
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分
        /// </summary>
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分
        /// </summary>
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机
        /// </summary>
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆
        /// </summary>
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆
        /// </summary>
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车
        /// </summary>
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间
        /// </summary>
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 健康证图片
        /// </summary>
        public string? HealthCertImages { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        public string? SkillCertificates { get; set; }
    }

    /// <summary>
    /// 驾驶员导出DTO
    /// </summary>
    public class TaktDriverExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverExportDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string LicenseNo { get; set; }

        /// <summary>
        /// 驾驶证类型
        /// </summary>
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态
        /// </summary>
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        public int LicensePoints { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验
        /// </summary>
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分
        /// </summary>
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分
        /// </summary>
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分
        /// </summary>
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机
        /// </summary>
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆
        /// </summary>
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆
        /// </summary>
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车
        /// </summary>
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间
        /// </summary>
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        public string? SkillCertificates { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 驾驶员导入模板DTO
    /// </summary>
    public class TaktDriverTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDriverTemplateDto()
        {
            EmployeeName = string.Empty;
            EmployeeNo = string.Empty;
            LicenseNo = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        public string EmployeeNo { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// 驾驶员状态(0=在职 1=离职 2=休假 3=停职 4=其他)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 驾驶证号码
        /// </summary>
        public string LicenseNo { get; set; }

        /// <summary>
        /// 驾驶证类型(0=A1 1=A2 2=A3 3=B1 4=B2 5=C1 6=C2 7=D 8=E 9=F 10=M 11=N 12=P)
        /// </summary>
        public int LicenseType { get; set; }

        /// <summary>
        /// 驾驶证发证机关
        /// </summary>
        public string? LicenseAuthority { get; set; }

        /// <summary>
        /// 驾驶证发证日期
        /// </summary>
        public DateTime? LicenseIssueDate { get; set; }

        /// <summary>
        /// 驾驶证有效期
        /// </summary>
        public DateTime LicenseExpiryDate { get; set; }

        /// <summary>
        /// 驾驶证状态(0=正常 1=即将到期 2=已过期 3=被吊销 4=被注销)
        /// </summary>
        public int LicenseStatus { get; set; }

        /// <summary>
        /// 驾驶证扣分
        /// </summary>
        public int LicensePoints { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string? IdCardNo { get; set; }

        /// <summary>
        /// 性别(0=男 1=女)
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int? Age { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public string? EmergencyContact { get; set; }

        /// <summary>
        /// 紧急联系电话
        /// </summary>
        public string? EmergencyPhone { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string? HomeAddress { get; set; }

        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? HireDate { get; set; }

        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? ResignDate { get; set; }

        /// <summary>
        /// 驾龄（年）
        /// </summary>
        public int? DrivingYears { get; set; }

        /// <summary>
        /// 驾驶经验(0=新手 1=一般 2=熟练 3=专家)
        /// </summary>
        public int DrivingExperience { get; set; }

        /// <summary>
        /// 可驾驶车型
        /// </summary>
        public string? DrivableVehicles { get; set; }

        /// <summary>
        /// 驾驶技能评分
        /// </summary>
        public decimal? DrivingSkillScore { get; set; }

        /// <summary>
        /// 安全驾驶评分
        /// </summary>
        public decimal? SafetyScore { get; set; }

        /// <summary>
        /// 服务态度评分
        /// </summary>
        public decimal? ServiceScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal? OverallScore { get; set; }

        /// <summary>
        /// 事故记录次数
        /// </summary>
        public int AccidentCount { get; set; }

        /// <summary>
        /// 违章记录次数
        /// </summary>
        public int ViolationCount { get; set; }

        /// <summary>
        /// 投诉记录次数
        /// </summary>
        public int ComplaintCount { get; set; }

        /// <summary>
        /// 表扬记录次数
        /// </summary>
        public int PraiseCount { get; set; }

        /// <summary>
        /// 是否专职司机(0=否 1=是)
        /// </summary>
        public int IsFullTime { get; set; }

        /// <summary>
        /// 是否可驾驶危险品车辆(0=否 1=是)
        /// </summary>
        public int CanDriveHazardous { get; set; }

        /// <summary>
        /// 是否可驾驶大型车辆(0=否 1=是)
        /// </summary>
        public int CanDriveLarge { get; set; }

        /// <summary>
        /// 是否可驾驶客车(0=否 1=是)
        /// </summary>
        public int CanDrivePassenger { get; set; }

        /// <summary>
        /// 工作区域
        /// </summary>
        public string? WorkArea { get; set; }

        /// <summary>
        /// 工作时间(0=白班 1=夜班 2=全天 3=灵活)
        /// </summary>
        public int WorkSchedule { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BaseSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal? PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal? Allowance { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal? TotalSalary { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// 开户银行
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// 账户持有人
        /// </summary>
        public string? AccountHolder { get; set; }

        /// <summary>
        /// 健康证号码
        /// </summary>
        public string? HealthCertNo { get; set; }

        /// <summary>
        /// 健康证有效期
        /// </summary>
        public DateTime? HealthCertExpiry { get; set; }

        /// <summary>
        /// 培训证书
        /// </summary>
        public string? TrainingCertificates { get; set; }

        /// <summary>
        /// 技能证书
        /// </summary>
        public string? SkillCertificates { get; set; }
    }
} 


