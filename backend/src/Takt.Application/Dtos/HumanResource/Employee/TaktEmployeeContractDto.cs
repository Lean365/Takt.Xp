//===================================================================
// 项目名: Takt.Application
// 文件名: TaktEmployeeContractDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 员工合同数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.HumanResource.Employee
{
    /// <summary>
    /// 员工合同基础DTO
    /// </summary>
    public class TaktEmployeeContractDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
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
    /// 员工合同查询DTO
    /// </summary>
    public class TaktEmployeeContractQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractQueryDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string? ContractNo { get; set; }

        /// <summary>
        /// 合同类型
        /// </summary>
        public string? ContractType { get; set; }

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 员工合同创建DTO
    /// </summary>
    public class TaktEmployeeContractCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractCreateDto()
        {
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同更新DTO
    /// </summary>
    public class TaktEmployeeContractUpdateDto : TaktEmployeeContractCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractUpdateDto() : base()
        {
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }
    }

    /// <summary>
    /// 员工合同删除DTO
    /// </summary>
    public class TaktEmployeeContractDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 合同ID
        /// </summary>
        [AdaptMember("Id")]
        public long ContractId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同导入DTO
    /// </summary>
    public class TaktEmployeeContractImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractImportDto()
        {
            EmployeeNo = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            ContractFile = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public DateTime? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public DateTime? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同文件
        /// </summary>
        public string? ContractFile { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 员工合同导出DTO
    /// </summary>
    public class TaktEmployeeContractExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            ProbationStartDate = string.Empty;
            ProbationEndDate = string.Empty;
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
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public string? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public string? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 员工合同模板DTO
    /// </summary>
    public class TaktEmployeeContractTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEmployeeContractTemplateDto()
        {
            EmployeeNo = string.Empty;
            ContractNo = string.Empty;
            ContractType = string.Empty;
            StartDate = string.Empty;
            EndDate = string.Empty;
            ProbationStartDate = string.Empty;
            ProbationEndDate = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNo { get; set; } = string.Empty;

        /// <summary>
        /// 合同类型
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// 合同开始日期
        /// </summary>
        public string StartDate { get; set; } = string.Empty;

        /// <summary>
        /// 合同结束日期
        /// </summary>
        public string EndDate { get; set; } = string.Empty;

        /// <summary>
        /// 试用期开始日期
        /// </summary>
        public string? ProbationStartDate { get; set; }

        /// <summary>
        /// 试用期结束日期
        /// </summary>
        public string? ProbationEndDate { get; set; }

        /// <summary>
        /// 合同状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


