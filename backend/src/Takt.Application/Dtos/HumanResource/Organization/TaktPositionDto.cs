//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPositionDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 职位数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.HumanResource.Organization
{
    /// <summary>
    /// 职位基础DTO
    /// </summary>
    public class TaktPositionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            Description = string.Empty;
            Responsibilities = string.Empty;
            Requirements = string.Empty;
        }

        /// <summary>
        /// 职位ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string PosCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 职位级别(1=初级 2=中级 3=高级 4=专家 5=资深专家)
        /// </summary>
        public int PosLevel { get; set; }

        /// <summary>
        /// 职位序列(1=管理序列 2=技术序列 3=专业序列 4=操作序列)
        /// </summary>
        public int PosSequence { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位职责
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// 任职要求
        /// </summary>
        public string? Requirements { get; set; }

        /// <summary>
        /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        public int? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验(年)
        /// </summary>
        public int? MinExperience { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        public decimal? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        public decimal? SalaryMax { get; set; }

        /// <summary>
        /// 职位人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 职位状态(0=正常 1=停用)
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

        /// <summary>
        /// 职位员工
        /// </summary>
        public List<Employee.TaktEmployeeDto>? Employees { get; set; }
    }

    /// <summary>
    /// 职位查询DTO
    /// </summary>
    public class TaktPositionQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionQueryDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string? PosCode { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string? PosName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别ID
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// 职位级别
        /// </summary>
        public int? PosLevel { get; set; }

        /// <summary>
        /// 职位序列
        /// </summary>
        public int? PosSequence { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 最低学历要求
        /// </summary>
        public int? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验
        /// </summary>
        public int? MinExperience { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 职位创建DTO
    /// </summary>
    public class TaktPositionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionCreateDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            Description = string.Empty;
            Responsibilities = string.Empty;
            Requirements = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string PosCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 职位级别(1=初级 2=中级 3=高级 4=专家 5=资深专家)
        /// </summary>
        public int PosLevel { get; set; }

        /// <summary>
        /// 职位序列(1=管理序列 2=技术序列 3=专业序列 4=操作序列)
        /// </summary>
        public int PosSequence { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位职责
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// 任职要求
        /// </summary>
        public string? Requirements { get; set; }

        /// <summary>
        /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        public int? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验(年)
        /// </summary>
        public int? MinExperience { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        public decimal? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        public decimal? SalaryMax { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 职位状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 职位更新DTO
    /// </summary>
    public class TaktPositionUpdateDto : TaktPositionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionUpdateDto() : base()
        {
        }

        /// <summary>
        /// 职位ID
        /// </summary>
        [AdaptMember("Id")]
        public long PositionId { get; set; }
    }

    /// <summary>
    /// 职位删除DTO
    /// </summary>
    public class TaktPositionDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 职位ID
        /// </summary>
        [AdaptMember("Id")]
        public long PositionId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 职位导入DTO
    /// </summary>
    public class TaktPositionImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionImportDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            CategoryName = string.Empty;
            PosLevel = string.Empty;
            PosSequence = string.Empty;
            Description = string.Empty;
            Responsibilities = string.Empty;
            Requirements = string.Empty;
            MinEducation = string.Empty;
            MinExperience = string.Empty;
            SalaryMin = string.Empty;
            SalaryMax = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string PosCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别名称
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 职位级别
        /// </summary>
        public string PosLevel { get; set; } = string.Empty;

        /// <summary>
        /// 职位序列
        /// </summary>
        public string PosSequence { get; set; } = string.Empty;

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位职责
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// 任职要求
        /// </summary>
        public string? Requirements { get; set; }

        /// <summary>
        /// 最低学历要求
        /// </summary>
        public string? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验
        /// </summary>
        public string? MinExperience { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        public string? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        public string? SalaryMax { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 职位导出DTO
    /// </summary>
    public class TaktPositionExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionExportDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            CategoryName = string.Empty;
            PosLevel = string.Empty;
            PosSequence = string.Empty;
            Description = string.Empty;
            Responsibilities = string.Empty;
            Requirements = string.Empty;
            MinEducation = string.Empty;
            MinExperience = string.Empty;
            SalaryMin = string.Empty;
            SalaryMax = string.Empty;
            Status = string.Empty;
            CreateTime = string.Empty;
        }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string PosCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别名称
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 职位级别
        /// </summary>
        public string PosLevel { get; set; } = string.Empty;

        /// <summary>
        /// 职位序列
        /// </summary>
        public string PosSequence { get; set; } = string.Empty;

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位职责
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// 任职要求
        /// </summary>
        public string? Requirements { get; set; }

        /// <summary>
        /// 最低学历要求
        /// </summary>
        public string? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验
        /// </summary>
        public string? MinExperience { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        public string? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        public string? SalaryMax { get; set; }

        /// <summary>
        /// 职位人数
        /// </summary>
        public int EmployeeCount { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 职位模板DTO
    /// </summary>
    public class TaktPositionTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPositionTemplateDto()
        {
            PosCode = string.Empty;
            PosName = string.Empty;
            EnglishName = string.Empty;
            CategoryName = string.Empty;
            PosLevel = string.Empty;
            PosSequence = string.Empty;
            Description = string.Empty;
            Responsibilities = string.Empty;
            Requirements = string.Empty;
            MinEducation = string.Empty;
            MinExperience = string.Empty;
            SalaryMin = string.Empty;
            SalaryMax = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string PosCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PosName { get; set; } = string.Empty;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string? EnglishName { get; set; }

        /// <summary>
        /// 职位类别名称
        /// </summary>
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>
        /// 职位级别
        /// </summary>
        public string PosLevel { get; set; } = string.Empty;

        /// <summary>
        /// 职位序列
        /// </summary>
        public string PosSequence { get; set; } = string.Empty;

        /// <summary>
        /// 职位描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 职位职责
        /// </summary>
        public string? Responsibilities { get; set; }

        /// <summary>
        /// 任职要求
        /// </summary>
        public string? Requirements { get; set; }

        /// <summary>
        /// 最低学历要求
        /// </summary>
        public string? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验
        /// </summary>
        public string? MinExperience { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        public string? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        public string? SalaryMax { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 职位状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


