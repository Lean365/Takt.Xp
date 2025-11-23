//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPerformanceDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 绩效数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.HumanResource.Performance
{
    /// <summary>
    /// 绩效基础DTO
    /// </summary>
    public class TaktPerformanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceDto()
        {
            PerformanceType = string.Empty;
            Evaluator = string.Empty;
            Comment = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 绩效ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 绩效类型
        /// </summary>
        public string PerformanceType { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// 工作质量评分
        /// </summary>
        public decimal QualityScore { get; set; }

        /// <summary>
        /// 工作效率评分
        /// </summary>
        public decimal EfficiencyScore { get; set; }

        /// <summary>
        /// 团队合作评分
        /// </summary>
        public decimal TeamworkScore { get; set; }

        /// <summary>
        /// 创新能力评分
        /// </summary>
        public decimal InnovationScore { get; set; }

        /// <summary>
        /// 学习能力评分
        /// </summary>
        public decimal LearningScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 绩效等级(A=优秀 B=良好 C=合格 D=不合格)
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; set; } = string.Empty;

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime EvaluationTime { get; set; }

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 绩效状态(1=草稿 2=已提交 3=已评估 4=已确认)
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
    /// 绩效查询DTO
    /// </summary>
    public class TaktPerformanceQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceQueryDto()
        {
            PerformanceType = string.Empty;
            Evaluator = string.Empty;
            Comment = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 绩效类型
        /// </summary>
        public string? PerformanceType { get; set; }

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public DateTime? PeriodStart { get; set; }

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public DateTime? PeriodEnd { get; set; }

        /// <summary>
        /// 绩效等级
        /// </summary>
        public string? Grade { get; set; }

        /// <summary>
        /// 评估人
        /// </summary>
        public string? Evaluator { get; set; }

        /// <summary>
        /// 绩效状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }
    }

    /// <summary>
    /// 绩效创建DTO
    /// </summary>
    public class TaktPerformanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceCreateDto()
        {
            PerformanceType = string.Empty;
            Evaluator = string.Empty;
            Comment = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 绩效类型
        /// </summary>
        public string PerformanceType { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// 工作质量评分
        /// </summary>
        public decimal QualityScore { get; set; }

        /// <summary>
        /// 工作效率评分
        /// </summary>
        public decimal EfficiencyScore { get; set; }

        /// <summary>
        /// 团队合作评分
        /// </summary>
        public decimal TeamworkScore { get; set; }

        /// <summary>
        /// 创新能力评分
        /// </summary>
        public decimal InnovationScore { get; set; }

        /// <summary>
        /// 学习能力评分
        /// </summary>
        public decimal LearningScore { get; set; }

        /// <summary>
        /// 综合评分
        /// </summary>
        public decimal TotalScore { get; set; }

        /// <summary>
        /// 绩效等级(A=优秀 B=良好 C=合格 D=不合格)
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; set; } = string.Empty;

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime EvaluationTime { get; set; }

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 绩效状态(1=草稿 2=已提交 3=已评估 4=已确认)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 绩效更新DTO
    /// </summary>
    public class TaktPerformanceUpdateDto : TaktPerformanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceUpdateDto() : base()
        {
        }

        /// <summary>
        /// 绩效ID
        /// </summary>
        [AdaptMember("Id")]
        public long PerformanceId { get; set; }
    }

    /// <summary>
    /// 绩效删除DTO
    /// </summary>
    public class TaktPerformanceDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 绩效ID
        /// </summary>
        [AdaptMember("Id")]
        public long PerformanceId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 绩效导入DTO
    /// </summary>
    public class TaktPerformanceImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceImportDto()
        {
            EmployeeNo = string.Empty;
            PerformanceType = string.Empty;
            PeriodStart = string.Empty;
            PeriodEnd = string.Empty;
            QualityScore = string.Empty;
            EfficiencyScore = string.Empty;
            TeamworkScore = string.Empty;
            InnovationScore = string.Empty;
            LearningScore = string.Empty;
            TotalScore = string.Empty;
            Grade = string.Empty;
            Evaluator = string.Empty;
            EvaluationTime = string.Empty;
            Comment = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 绩效类型
        /// </summary>
        public string PerformanceType { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public string PeriodStart { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public string PeriodEnd { get; set; } = string.Empty;

        /// <summary>
        /// 工作质量评分
        /// </summary>
        public string QualityScore { get; set; } = string.Empty;

        /// <summary>
        /// 工作效率评分
        /// </summary>
        public string EfficiencyScore { get; set; } = string.Empty;

        /// <summary>
        /// 团队合作评分
        /// </summary>
        public string TeamworkScore { get; set; } = string.Empty;

        /// <summary>
        /// 创新能力评分
        /// </summary>
        public string InnovationScore { get; set; } = string.Empty;

        /// <summary>
        /// 学习能力评分
        /// </summary>
        public string LearningScore { get; set; } = string.Empty;

        /// <summary>
        /// 综合评分
        /// </summary>
        public string TotalScore { get; set; } = string.Empty;

        /// <summary>
        /// 绩效等级
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; set; } = string.Empty;

        /// <summary>
        /// 评估时间
        /// </summary>
        public string EvaluationTime { get; set; } = string.Empty;

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 绩效状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 绩效导出DTO
    /// </summary>
    public class TaktPerformanceExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            PerformanceType = string.Empty;
            PeriodStart = string.Empty;
            PeriodEnd = string.Empty;
            QualityScore = string.Empty;
            EfficiencyScore = string.Empty;
            TeamworkScore = string.Empty;
            InnovationScore = string.Empty;
            LearningScore = string.Empty;
            TotalScore = string.Empty;
            Grade = string.Empty;
            Evaluator = string.Empty;
            EvaluationTime = string.Empty;
            Comment = string.Empty;
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
        /// 绩效类型
        /// </summary>
        public string PerformanceType { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public string PeriodStart { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public string PeriodEnd { get; set; } = string.Empty;

        /// <summary>
        /// 工作质量评分
        /// </summary>
        public string QualityScore { get; set; } = string.Empty;

        /// <summary>
        /// 工作效率评分
        /// </summary>
        public string EfficiencyScore { get; set; } = string.Empty;

        /// <summary>
        /// 团队合作评分
        /// </summary>
        public string TeamworkScore { get; set; } = string.Empty;

        /// <summary>
        /// 创新能力评分
        /// </summary>
        public string InnovationScore { get; set; } = string.Empty;

        /// <summary>
        /// 学习能力评分
        /// </summary>
        public string LearningScore { get; set; } = string.Empty;

        /// <summary>
        /// 综合评分
        /// </summary>
        public string TotalScore { get; set; } = string.Empty;

        /// <summary>
        /// 绩效等级
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; set; } = string.Empty;

        /// <summary>
        /// 评估时间
        /// </summary>
        public string EvaluationTime { get; set; } = string.Empty;

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 绩效状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 绩效模板DTO
    /// </summary>
    public class TaktPerformanceTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPerformanceTemplateDto()
        {
            EmployeeNo = string.Empty;
            PerformanceType = string.Empty;
            PeriodStart = string.Empty;
            PeriodEnd = string.Empty;
            QualityScore = string.Empty;
            EfficiencyScore = string.Empty;
            TeamworkScore = string.Empty;
            InnovationScore = string.Empty;
            LearningScore = string.Empty;
            TotalScore = string.Empty;
            Grade = string.Empty;
            Evaluator = string.Empty;
            EvaluationTime = string.Empty;
            Comment = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 绩效类型
        /// </summary>
        public string PerformanceType { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期开始时间
        /// </summary>
        public string PeriodStart { get; set; } = string.Empty;

        /// <summary>
        /// 评估周期结束时间
        /// </summary>
        public string PeriodEnd { get; set; } = string.Empty;

        /// <summary>
        /// 工作质量评分
        /// </summary>
        public string QualityScore { get; set; } = string.Empty;

        /// <summary>
        /// 工作效率评分
        /// </summary>
        public string EfficiencyScore { get; set; } = string.Empty;

        /// <summary>
        /// 团队合作评分
        /// </summary>
        public string TeamworkScore { get; set; } = string.Empty;

        /// <summary>
        /// 创新能力评分
        /// </summary>
        public string InnovationScore { get; set; } = string.Empty;

        /// <summary>
        /// 学习能力评分
        /// </summary>
        public string LearningScore { get; set; } = string.Empty;

        /// <summary>
        /// 综合评分
        /// </summary>
        public string TotalScore { get; set; } = string.Empty;

        /// <summary>
        /// 绩效等级
        /// </summary>
        public string Grade { get; set; } = string.Empty;

        /// <summary>
        /// 评估人
        /// </summary>
        public string Evaluator { get; set; } = string.Empty;

        /// <summary>
        /// 评估时间
        /// </summary>
        public string EvaluationTime { get; set; } = string.Empty;

        /// <summary>
        /// 评估意见
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        /// 绩效状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 


