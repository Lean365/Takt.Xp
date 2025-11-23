//===================================================================
// 项目名: Takt.Application
// 文件名: TaktTrainingRecordDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 培训记录数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.HumanResource.Training
{
    /// <summary>
    /// 培训记录基础DTO
    /// </summary>
    public class TaktTrainingRecordDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordDto()
        {
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            Trainer = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 培训记录ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 培训时长(小时)
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Trainer { get; set; } = string.Empty;

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 培训状态(1=计划中 2=进行中 3=已完成 4=已取消)
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
    /// 培训记录查询DTO
    /// </summary>
    public class TaktTrainingRecordQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordQueryDto()
        {
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            Trainer = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 培训名称
        /// </summary>
        public string? TrainingName { get; set; }

        /// <summary>
        /// 培训类型
        /// </summary>
        public string? TrainingType { get; set; }

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string? Trainer { get; set; }

        /// <summary>
        /// 培训状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// 培训记录创建DTO
    /// </summary>
    public class TaktTrainingRecordCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordCreateDto()
        {
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            Trainer = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 培训时长(小时)
        /// </summary>
        public decimal Duration { get; set; }

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Trainer { get; set; } = string.Empty;

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public decimal? Score { get; set; }

        /// <summary>
        /// 培训状态(1=计划中 2=进行中 3=已完成 4=已取消)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 培训记录更新DTO
    /// </summary>
    public class TaktTrainingRecordUpdateDto : TaktTrainingRecordCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordUpdateDto() : base()
        {
        }

        /// <summary>
        /// 培训记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long TrainingRecordId { get; set; }
    }

    /// <summary>
    /// 培训记录删除DTO
    /// </summary>
    public class TaktTrainingRecordDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 培训记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long TrainingRecordId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 培训记录导入DTO
    /// </summary>
    public class TaktTrainingRecordImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordImportDto()
        {
            EmployeeNo = string.Empty;
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            Duration = string.Empty;
            Trainer = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            Score = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训时长(小时)
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Trainer { get; set; } = string.Empty;

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public string? Score { get; set; }

        /// <summary>
        /// 培训状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 培训记录导出DTO
    /// </summary>
    public class TaktTrainingRecordExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            Duration = string.Empty;
            Trainer = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            Score = string.Empty;
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
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训时长(小时)
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Trainer { get; set; } = string.Empty;

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public string? Score { get; set; }

        /// <summary>
        /// 培训状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 培训记录模板DTO
    /// </summary>
    public class TaktTrainingRecordTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktTrainingRecordTemplateDto()
        {
            EmployeeNo = string.Empty;
            TrainingName = string.Empty;
            TrainingType = string.Empty;
            StartTime = string.Empty;
            EndTime = string.Empty;
            Duration = string.Empty;
            Trainer = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
            Score = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 培训名称
        /// </summary>
        public string TrainingName { get; set; } = string.Empty;

        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingType { get; set; } = string.Empty;

        /// <summary>
        /// 培训开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训结束时间
        /// </summary>
        public string EndTime { get; set; } = string.Empty;

        /// <summary>
        /// 培训时长(小时)
        /// </summary>
        public string Duration { get; set; } = string.Empty;

        /// <summary>
        /// 培训讲师
        /// </summary>
        public string Trainer { get; set; } = string.Empty;

        /// <summary>
        /// 培训地点
        /// </summary>
        public string? Location { get; set; }

        /// <summary>
        /// 培训描述
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 培训成绩
        /// </summary>
        public string? Score { get; set; }

        /// <summary>
        /// 培训状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


