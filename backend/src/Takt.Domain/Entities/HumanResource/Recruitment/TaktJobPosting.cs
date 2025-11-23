﻿// -----------------------------------------------------------------------------
// <copyright file="TaktJobPosting.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>JobPosting 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Recruitment
{
    /// <summary>
    /// 职位发布实体
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-23
    /// 功能说明:
    /// 1. 存储职位发布信息
    /// 2. 支持招聘需求管理
    /// 3. 关联职位和部门信息
    /// </remarks>
    [SugarTable("takt_humanresource_job_posting", "职位发布表")]
    [SugarIndex("ix_job_posting_code", nameof(PostingCode), OrderByType.Asc, true)]
    [SugarIndex("ix_job_posting_position", nameof(PositionId), OrderByType.Asc)]
    [SugarIndex("ix_job_posting_department", nameof(DepartmentId), OrderByType.Asc)]
    [SugarIndex("ix_job_posting_status", nameof(Status), OrderByType.Asc)]
    public class TaktJobPosting : TaktBaseEntity
    {
        /// <summary>
        /// 发布编号
        /// </summary>
        [SugarColumn(ColumnName = "posting_code", ColumnDescription = "发布编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string PostingCode { get; set; } = string.Empty;

        /// <summary>
        /// 职位标题
        /// </summary>
        [SugarColumn(ColumnName = "job_title", ColumnDescription = "职位标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string JobTitle { get; set; } = string.Empty;

        /// <summary>
        /// 职位ID
        /// </summary>
        [SugarColumn(ColumnName = "position_id", ColumnDescription = "职位ID", ColumnDataType = "bigint", IsNullable = false)]
        public long PositionId { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "department_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DepartmentId { get; set; }

        /// <summary>
        /// 招聘人数
        /// </summary>
        [SugarColumn(ColumnName = "headcount", ColumnDescription = "招聘人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int Headcount { get; set; }

        /// <summary>
        /// 已招聘人数
        /// </summary>
        [SugarColumn(ColumnName = "hired_count", ColumnDescription = "已招聘人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int HiredCount { get; set; }

        /// <summary>
        /// 工作地点
        /// </summary>
        [SugarColumn(ColumnName = "work_location", ColumnDescription = "工作地点", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? WorkLocation { get; set; }

        /// <summary>
        /// 薪资范围下限
        /// </summary>
        [SugarColumn(ColumnName = "salary_min", ColumnDescription = "薪资范围下限", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? SalaryMin { get; set; }

        /// <summary>
        /// 薪资范围上限
        /// </summary>
        [SugarColumn(ColumnName = "salary_max", ColumnDescription = "薪资范围上限", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? SalaryMax { get; set; }

        /// <summary>
        /// 职位描述
        /// </summary>
        [SugarColumn(ColumnName = "job_description", ColumnDescription = "职位描述", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string? JobDescription { get; set; }

        /// <summary>
        /// 职位要求
        /// </summary>
        [SugarColumn(ColumnName = "job_requirements", ColumnDescription = "职位要求", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string? JobRequirements { get; set; }

        /// <summary>
        /// 最低学历要求(1=高中 2=大专 3=本科 4=硕士 5=博士)
        /// </summary>
        [SugarColumn(ColumnName = "min_education", ColumnDescription = "最低学历要求", ColumnDataType = "int", IsNullable = true)]
        public int? MinEducation { get; set; }

        /// <summary>
        /// 最低工作经验(年)
        /// </summary>
        [SugarColumn(ColumnName = "min_experience", ColumnDescription = "最低工作经验", ColumnDataType = "int", IsNullable = true)]
        public int? MinExperience { get; set; }

        /// <summary>
        /// 发布状态(1=草稿 2=已发布 3=暂停 4=已关闭)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "发布状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int Status { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        [SugarColumn(ColumnName = "publish_time", ColumnDescription = "发布时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>
        [SugarColumn(ColumnName = "deadline", ColumnDescription = "截止时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// 招聘负责人ID
        /// </summary>
        [SugarColumn(ColumnName = "recruiter_id", ColumnDescription = "招聘负责人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? RecruiterId { get; set; }

        /// <summary>
        /// 职位信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(PositionId))]
        public virtual Organization.TaktPosition? Position { get; set; }

        /// <summary>
        /// 部门信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DepartmentId))]
        public virtual Organization.TaktDepartment? Department { get; set; }

        /// <summary>
        /// 招聘负责人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(RecruiterId))]
        public virtual Employee.TaktEmployee? Recruiter { get; set; }

        /// <summary>
        /// 应聘者列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktJobApplication.JobPostingId))]
        public virtual List<TaktJobApplication>? Applications { get; set; }
    }
}


