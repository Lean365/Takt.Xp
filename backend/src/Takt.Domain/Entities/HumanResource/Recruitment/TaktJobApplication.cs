// -----------------------------------------------------------------------------
// <copyright file="TaktJobApplication.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>JobApplication 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

namespace Takt.Domain.Entities.HumanResource.Recruitment
{
    /// <summary>
    /// 招聘申请实体
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 存储应聘者申请信息
    /// 2. 支持申请状态跟踪
    /// 3. 关联职位发布和应聘者信息
    /// </remarks>
    [SugarTable("takt_humanresource_job_application", "招聘申请表")]
    [SugarIndex("ix_job_application_code", nameof(ApplicationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_job_application_posting", nameof(JobPostingId), OrderByType.Asc)]
    [SugarIndex("ix_job_application_applicant", nameof(ApplicantId), OrderByType.Asc)]
    [SugarIndex("ix_job_application_status", nameof(ApplicationStatus), OrderByType.Asc)]
    [SugarIndex("ix_job_application_date", nameof(ApplicationDate), OrderByType.Desc)]
    public class TaktJobApplication : TaktBaseEntity
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        [SugarColumn(ColumnName = "application_code", ColumnDescription = "申请编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string ApplicationCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位发布ID
        /// </summary>
        [SugarColumn(ColumnName = "job_posting_id", ColumnDescription = "职位发布ID", ColumnDataType = "bigint", IsNullable = false)]
        public long JobPostingId { get; set; }

        /// <summary>
        /// 应聘者ID
        /// </summary>
        [SugarColumn(ColumnName = "applicant_id", ColumnDescription = "应聘者ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ApplicantId { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// 申请状态(1=已申请 2=简历筛选 3=初试 4=复试 5=终试 6=背景调查 7=录用 8=拒绝 9=取消)
        /// </summary>
        [SugarColumn(ColumnName = "application_status", ColumnDescription = "申请状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ApplicationStatus { get; set; }

        /// <summary>
        /// 简历来源(1=内部推荐 2=外部招聘 3=猎头推荐 4=校园招聘 5=社会招聘 6=其他)
        /// </summary>
        [SugarColumn(ColumnName = "resume_source", ColumnDescription = "简历来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ResumeSource { get; set; }

        /// <summary>
        /// 推荐人ID
        /// </summary>
        [SugarColumn(ColumnName = "referrer_id", ColumnDescription = "推荐人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ReferrerId { get; set; }

        /// <summary>
        /// 期望薪资
        /// </summary>
        [SugarColumn(ColumnName = "expected_salary", ColumnDescription = "期望薪资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ExpectedSalary { get; set; }

        /// <summary>
        /// 最快到岗时间
        /// </summary>
        [SugarColumn(ColumnName = "earliest_start_date", ColumnDescription = "最快到岗时间", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EarliestStartDate { get; set; }

        /// <summary>
        /// 应聘者姓名
        /// </summary>
        [SugarColumn(ColumnName = "applicant_name", ColumnDescription = "应聘者姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
        public string ApplicantName { get; set; } = string.Empty;

        /// <summary>
        /// 应聘者电话
        /// </summary>
        [SugarColumn(ColumnName = "applicant_phone", ColumnDescription = "应聘者电话", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string ApplicantPhone { get; set; } = string.Empty;

        /// <summary>
        /// 应聘者邮箱
        /// </summary>
        [SugarColumn(ColumnName = "applicant_email", ColumnDescription = "应聘者邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? ApplicantEmail { get; set; }

        /// <summary>
        /// 应聘者性别(1=男 2=女)
        /// </summary>
        [SugarColumn(ColumnName = "applicant_gender", ColumnDescription = "应聘者性别", ColumnDataType = "int", IsNullable = true)]
        public int? ApplicantGender { get; set; }

        /// <summary>
        /// 应聘者年龄
        /// </summary>
        [SugarColumn(ColumnName = "applicant_age", ColumnDescription = "应聘者年龄", ColumnDataType = "int", IsNullable = true)]
        public int? ApplicantAge { get; set; }

        /// <summary>
        /// 应聘者学历(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        [SugarColumn(ColumnName = "applicant_education", ColumnDescription = "应聘者学历", ColumnDataType = "int", IsNullable = true)]
        public int? ApplicantEducation { get; set; }

        /// <summary>
        /// 应聘者工作经验(年)
        /// </summary>
        [SugarColumn(ColumnName = "applicant_experience", ColumnDescription = "应聘者工作经验", ColumnDataType = "int", IsNullable = true)]
        public int? ApplicantExperience { get; set; }

        /// <summary>
        /// 应聘者毕业院校
        /// </summary>
        [SugarColumn(ColumnName = "applicant_school", ColumnDescription = "应聘者毕业院校", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? ApplicantSchool { get; set; }

        /// <summary>
        /// 应聘者专业
        /// </summary>
        [SugarColumn(ColumnName = "applicant_major", ColumnDescription = "应聘者专业", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? ApplicantMajor { get; set; }

        /// <summary>
        /// 应聘者现居地址
        /// </summary>
        [SugarColumn(ColumnName = "applicant_address", ColumnDescription = "应聘者现居地址", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string? ApplicantAddress { get; set; }

        /// <summary>
        /// 简历附件路径
        /// </summary>
        [SugarColumn(ColumnName = "resume_path", ColumnDescription = "简历附件路径", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? ResumePath { get; set; }

        /// <summary>
        /// 简历评分
        /// </summary>
        [SugarColumn(ColumnName = "resume_score", ColumnDescription = "简历评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? ResumeScore { get; set; }

        /// <summary>
        /// 初试时间
        /// </summary>
        [SugarColumn(ColumnName = "first_interview_time", ColumnDescription = "初试时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FirstInterviewTime { get; set; }

        /// <summary>
        /// 初试地点
        /// </summary>
        [SugarColumn(ColumnName = "first_interview_location", ColumnDescription = "初试地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? FirstInterviewLocation { get; set; }

        /// <summary>
        /// 初试面试官
        /// </summary>
        [SugarColumn(ColumnName = "first_interviewer", ColumnDescription = "初试面试官", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? FirstInterviewer { get; set; }

        /// <summary>
        /// 初试结果(1=通过 2=不通过 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "first_interview_result", ColumnDescription = "初试结果", ColumnDataType = "int", IsNullable = true)]
        public int? FirstInterviewResult { get; set; }

        /// <summary>
        /// 初试评分
        /// </summary>
        [SugarColumn(ColumnName = "first_interview_score", ColumnDescription = "初试评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? FirstInterviewScore { get; set; }

        /// <summary>
        /// 初试评价
        /// </summary>
        [SugarColumn(ColumnName = "first_interview_comment", ColumnDescription = "初试评价", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? FirstInterviewComment { get; set; }

        /// <summary>
        /// 复试时间
        /// </summary>
        [SugarColumn(ColumnName = "second_interview_time", ColumnDescription = "复试时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? SecondInterviewTime { get; set; }

        /// <summary>
        /// 复试地点
        /// </summary>
        [SugarColumn(ColumnName = "second_interview_location", ColumnDescription = "复试地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? SecondInterviewLocation { get; set; }

        /// <summary>
        /// 复试面试官
        /// </summary>
        [SugarColumn(ColumnName = "second_interviewer", ColumnDescription = "复试面试官", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? SecondInterviewer { get; set; }

        /// <summary>
        /// 复试结果(1=通过 2=不通过 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "second_interview_result", ColumnDescription = "复试结果", ColumnDataType = "int", IsNullable = true)]
        public int? SecondInterviewResult { get; set; }

        /// <summary>
        /// 复试评分
        /// </summary>
        [SugarColumn(ColumnName = "second_interview_score", ColumnDescription = "复试评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? SecondInterviewScore { get; set; }

        /// <summary>
        /// 复试评价
        /// </summary>
        [SugarColumn(ColumnName = "second_interview_comment", ColumnDescription = "复试评价", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? SecondInterviewComment { get; set; }

        /// <summary>
        /// 终试时间
        /// </summary>
        [SugarColumn(ColumnName = "final_interview_time", ColumnDescription = "终试时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FinalInterviewTime { get; set; }

        /// <summary>
        /// 终试地点
        /// </summary>
        [SugarColumn(ColumnName = "final_interview_location", ColumnDescription = "终试地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? FinalInterviewLocation { get; set; }

        /// <summary>
        /// 终试面试官
        /// </summary>
        [SugarColumn(ColumnName = "final_interviewer", ColumnDescription = "终试面试官", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
        public string? FinalInterviewer { get; set; }

        /// <summary>
        /// 终试结果(1=通过 2=不通过 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "final_interview_result", ColumnDescription = "终试结果", ColumnDataType = "int", IsNullable = true)]
        public int? FinalInterviewResult { get; set; }

        /// <summary>
        /// 终试评分
        /// </summary>
        [SugarColumn(ColumnName = "final_interview_score", ColumnDescription = "终试评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? FinalInterviewScore { get; set; }

        /// <summary>
        /// 终试评价
        /// </summary>
        [SugarColumn(ColumnName = "final_interview_comment", ColumnDescription = "终试评价", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? FinalInterviewComment { get; set; }

        /// <summary>
        /// 背景调查结果(1=通过 2=不通过 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "background_check_result", ColumnDescription = "背景调查结果", ColumnDataType = "int", IsNullable = true)]
        public int? BackgroundCheckResult { get; set; }

        /// <summary>
        /// 背景调查评价
        /// </summary>
        [SugarColumn(ColumnName = "background_check_comment", ColumnDescription = "背景调查评价", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? BackgroundCheckComment { get; set; }

        /// <summary>
        /// 录用薪资
        /// </summary>
        [SugarColumn(ColumnName = "offer_salary", ColumnDescription = "录用薪资", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? OfferSalary { get; set; }

        /// <summary>
        /// 录用通知时间
        /// </summary>
        [SugarColumn(ColumnName = "offer_time", ColumnDescription = "录用通知时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OfferTime { get; set; }

        /// <summary>
        /// 录用回复时间
        /// </summary>
        [SugarColumn(ColumnName = "offer_response_time", ColumnDescription = "录用回复时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OfferResponseTime { get; set; }

        /// <summary>
        /// 录用回复结果(1=接受 2=拒绝 3=待定)
        /// </summary>
        [SugarColumn(ColumnName = "offer_response", ColumnDescription = "录用回复结果", ColumnDataType = "int", IsNullable = true)]
        public int? OfferResponse { get; set; }

        /// <summary>
        /// 拒绝原因
        /// </summary>
        [SugarColumn(ColumnName = "rejection_reason", ColumnDescription = "拒绝原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? RejectionReason { get; set; }


        /// <summary>
        /// 职位发布信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(JobPostingId))]
        public virtual TaktJobPosting? JobPosting { get; set; }

        /// <summary>
        /// 应聘者信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ApplicantId))]
        public virtual Employee.TaktEmployee? Applicant { get; set; }

        /// <summary>
        /// 推荐人信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ReferrerId))]
        public virtual Employee.TaktEmployee? Referrer { get; set; }
    }
}




