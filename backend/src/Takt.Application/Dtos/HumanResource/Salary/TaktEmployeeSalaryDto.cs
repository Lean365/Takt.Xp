//===================================================================
// 项目名: Takt.Application
// 文件名: TaktEmployeeSalaryDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 员工薪资数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.HumanResource.Salary
{
    /// <summary>
    /// 员工薪资基础DTO
    /// </summary>
    public class TaktEmployeeSalaryDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 薪资ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 薪资状态
        /// </summary>
        public int Status { get; set; }

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
    }

    /// <summary>
    /// 员工薪资查询DTO
    /// </summary>
    public class TaktEmployeeSalaryQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryQueryDto()
        {
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 生效日期开始
        /// </summary>
        public DateTime? EffectiveDateStart { get; set; }

        /// <summary>
        /// 生效日期结束
        /// </summary>
        public DateTime? EffectiveDateEnd { get; set; }

        /// <summary>
        /// 薪资状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 员工薪资创建DTO
    /// </summary>
    public class TaktEmployeeSalaryCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryCreateDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal BasicSalary { get; set; }

        /// <summary>
        /// 岗位工资
        /// </summary>
        public decimal PositionSalary { get; set; }

        /// <summary>
        /// 绩效工资
        /// </summary>
        public decimal PerformanceSalary { get; set; }

        /// <summary>
        /// 津贴
        /// </summary>
        public decimal Allowance { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 总工资
        /// </summary>
        public decimal TotalSalary { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 薪资状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工薪资更新DTO
    /// </summary>
    public class TaktEmployeeSalaryUpdateDto : TaktEmployeeSalaryCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryUpdateDto() : base()
        {
        }

        /// <summary>
        /// 薪资ID
        /// </summary>
        [AdaptMember("Id")]
        public long SalaryId { get; set; }
    }

    /// <summary>
    /// 员工薪资删除DTO
    /// </summary>
    public class TaktEmployeeSalaryDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 薪资ID
        /// </summary>
        [AdaptMember("Id")]
        public long SalaryId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工薪资导入DTO
    /// </summary>
    public class TaktEmployeeSalaryImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryImportDto()
        {
            EmployeeNo = string.Empty;
            BasicSalary = string.Empty;
            PositionSalary = string.Empty;
            PerformanceSalary = string.Empty;
            Allowance = string.Empty;
            Bonus = string.Empty;
            TotalSalary = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 基本工资
        /// </summary>
        public string BasicSalary { get; set; } = string.Empty;

        /// <summary>
        /// 岗位工资
        /// </summary>
        public string PositionSalary { get; set; } = string.Empty;

        /// <summary>
        /// 绩效工资
        /// </summary>
        public string PerformanceSalary { get; set; } = string.Empty;

        /// <summary>
        /// 津贴
        /// </summary>
        public string Allowance { get; set; } = string.Empty;

        /// <summary>
        /// 奖金
        /// </summary>
        public string Bonus { get; set; } = string.Empty;

        /// <summary>
        /// 总工资
        /// </summary>
        public string TotalSalary { get; set; } = string.Empty;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 薪资状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工薪资导出DTO
    /// </summary>
    public class TaktEmployeeSalaryExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            BasicSalary = string.Empty;
            PositionSalary = string.Empty;
            PerformanceSalary = string.Empty;
            Allowance = string.Empty;
            Bonus = string.Empty;
            TotalSalary = string.Empty;
            EffectiveDate = string.Empty;
            Status = string.Empty;
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
        /// 基本工资
        /// </summary>
        public string BasicSalary { get; set; } = string.Empty;

        /// <summary>
        /// 岗位工资
        /// </summary>
        public string PositionSalary { get; set; } = string.Empty;

        /// <summary>
        /// 绩效工资
        /// </summary>
        public string PerformanceSalary { get; set; } = string.Empty;

        /// <summary>
        /// 津贴
        /// </summary>
        public string Allowance { get; set; } = string.Empty;

        /// <summary>
        /// 奖金
        /// </summary>
        public string Bonus { get; set; } = string.Empty;

        /// <summary>
        /// 总工资
        /// </summary>
        public string TotalSalary { get; set; } = string.Empty;

        /// <summary>
        /// 生效日期
        /// </summary>
        public string EffectiveDate { get; set; } = string.Empty;

        /// <summary>
        /// 薪资状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 员工薪资模板DTO
    /// </summary>
    public class TaktEmployeeSalaryTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeSalaryTemplateDto()
        {
            EmployeeNo = string.Empty;
            BasicSalary = string.Empty;
            PositionSalary = string.Empty;
            PerformanceSalary = string.Empty;
            Allowance = string.Empty;
            Bonus = string.Empty;
            TotalSalary = string.Empty;
            EffectiveDate = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 基本工资
        /// </summary>
        public string BasicSalary { get; set; } = string.Empty;

        /// <summary>
        /// 岗位工资
        /// </summary>
        public string PositionSalary { get; set; } = string.Empty;

        /// <summary>
        /// 绩效工资
        /// </summary>
        public string PerformanceSalary { get; set; } = string.Empty;

        /// <summary>
        /// 津贴
        /// </summary>
        public string Allowance { get; set; } = string.Empty;

        /// <summary>
        /// 奖金
        /// </summary>
        public string Bonus { get; set; } = string.Empty;

        /// <summary>
        /// 总工资
        /// </summary>
        public string TotalSalary { get; set; } = string.Empty;

        /// <summary>
        /// 生效日期
        /// </summary>
        public string EffectiveDate { get; set; } = string.Empty;

        /// <summary>
        /// 薪资状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


