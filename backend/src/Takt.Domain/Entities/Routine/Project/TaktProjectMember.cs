#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectMember.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目成员实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目成员实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目成员的相关信息，包括成员信息、角色、权限等
    /// </remarks>
    [SugarTable("Takt_routine_project_member", "项目成员表")]
    [SugarIndex("ix_project_member_code", nameof(ProjectMemberCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_member", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectMemberCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_member_status", nameof(ProjectMemberStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_member_role", nameof(ProjectMemberRole), OrderByType.Asc, false)]
    public class TaktProjectMember : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目成员编号</summary>
        [SugarColumn(ColumnName = "project_member_code", ColumnDescription = "项目成员编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectMemberCode { get; set; } = string.Empty;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>成员姓名</summary>
        [SugarColumn(ColumnName = "member_name", ColumnDescription = "成员姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MemberName { get; set; } = string.Empty;

        /// <summary>成员编号</summary>
        [SugarColumn(ColumnName = "member_code", ColumnDescription = "成员编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MemberCode { get; set; } = string.Empty;

        /// <summary>成员类型(1=内部员工 2=外部顾问 3=合作伙伴 4=客户代表 5=其他)</summary>
        [SugarColumn(ColumnName = "member_type", ColumnDescription = "成员类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MemberType { get; set; } = 1;

        /// <summary>项目角色(1=项目经理 2=技术负责人 3=开发工程师 4=测试工程师 5=产品经理 6=业务分析师 7=质量工程师 8=其他)</summary>
        [SugarColumn(ColumnName = "project_member_role", ColumnDescription = "项目角色", ColumnDataType = "int", IsNullable = false, DefaultValue = "8")]
        public int ProjectMemberRole { get; set; } = 8;

        /// <summary>角色描述</summary>
        [SugarColumn(ColumnName = "role_description", ColumnDescription = "角色描述", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RoleDescription { get; set; }

        /// <summary>成员部门</summary>
        [SugarColumn(ColumnName = "member_department", ColumnDescription = "成员部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MemberDepartment { get; set; }

        /// <summary>成员岗位</summary>
        [SugarColumn(ColumnName = "member_position", ColumnDescription = "成员岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MemberPosition { get; set; }

        /// <summary>成员级别(1=初级 2=中级 3=高级 4=专家 5=资深专家)</summary>
        [SugarColumn(ColumnName = "member_level", ColumnDescription = "成员级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int MemberLevel { get; set; } = 2;

        /// <summary>成员技能</summary>
        [SugarColumn(ColumnName = "member_skills", ColumnDescription = "成员技能", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MemberSkills { get; set; }

        /// <summary>成员经验(年)</summary>
        [SugarColumn(ColumnName = "member_experience", ColumnDescription = "成员经验(年)", ColumnDataType = "int", IsNullable = true)]
        public int? MemberExperience { get; set; }

        /// <summary>联系电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>联系邮箱</summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>微信号</summary>
        [SugarColumn(ColumnName = "wechat_id", ColumnDescription = "微信号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WechatId { get; set; }

        /// <summary>QQ号</summary>
        [SugarColumn(ColumnName = "qq_number", ColumnDescription = "QQ号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QqNumber { get; set; }

        /// <summary>加入项目日期</summary>
        [SugarColumn(ColumnName = "join_project_date", ColumnDescription = "加入项目日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? JoinProjectDate { get; set; }

        /// <summary>计划离开日期</summary>
        [SugarColumn(ColumnName = "planned_leave_date", ColumnDescription = "计划离开日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlannedLeaveDate { get; set; }

        /// <summary>实际离开日期</summary>
        [SugarColumn(ColumnName = "actual_leave_date", ColumnDescription = "实际离开日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualLeaveDate { get; set; }

        /// <summary>工作时间(小时/天)</summary>
        [SugarColumn(ColumnName = "work_hours_per_day", ColumnDescription = "工作时间(小时/天)", ColumnDataType = "decimal(4,2)", IsNullable = false, DefaultValue = "8")]
        public decimal WorkHoursPerDay { get; set; } = 8;

        /// <summary>工作天数(天/周)</summary>
        [SugarColumn(ColumnName = "work_days_per_week", ColumnDescription = "工作天数(天/周)", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
        public int WorkDaysPerWeek { get; set; } = 5;

        /// <summary>分配比例(%)</summary>
        [SugarColumn(ColumnName = "allocation_ratio", ColumnDescription = "分配比例(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal AllocationRatio { get; set; } = 100;

        /// <summary>计划工时(小时)</summary>
        [SugarColumn(ColumnName = "planned_work_hours", ColumnDescription = "计划工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? PlannedWorkHours { get; set; }

        /// <summary>实际工时(小时)</summary>
        [SugarColumn(ColumnName = "actual_work_hours", ColumnDescription = "实际工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? ActualWorkHours { get; set; }

        /// <summary>剩余工时(小时)</summary>
        [SugarColumn(ColumnName = "remaining_work_hours", ColumnDescription = "剩余工时(小时)", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? RemainingWorkHours { get; set; }

        /// <summary>计划成本</summary>
        [SugarColumn(ColumnName = "planned_cost", ColumnDescription = "计划成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? PlannedCost { get; set; }

        /// <summary>实际成本</summary>
        [SugarColumn(ColumnName = "actual_cost", ColumnDescription = "实际成本", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ActualCost { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>时薪</summary>
        [SugarColumn(ColumnName = "hourly_rate", ColumnDescription = "时薪", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? HourlyRate { get; set; }

        /// <summary>日薪</summary>
        [SugarColumn(ColumnName = "daily_rate", ColumnDescription = "日薪", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? DailyRate { get; set; }

        /// <summary>月薪</summary>
        [SugarColumn(ColumnName = "monthly_salary", ColumnDescription = "月薪", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? MonthlySalary { get; set; }

        /// <summary>项目权限</summary>
        [SugarColumn(ColumnName = "project_permissions", ColumnDescription = "项目权限", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectPermissions { get; set; }

        /// <summary>数据权限</summary>
        [SugarColumn(ColumnName = "data_permissions", ColumnDescription = "数据权限", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DataPermissions { get; set; }

        /// <summary>功能权限</summary>
        [SugarColumn(ColumnName = "function_permissions", ColumnDescription = "功能权限", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FunctionPermissions { get; set; }

        /// <summary>是否项目经理(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_project_manager", ColumnDescription = "是否项目经理", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsProjectManager { get; set; } = 0;

        /// <summary>是否核心成员(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_core_member", ColumnDescription = "是否核心成员", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCoreMember { get; set; } = 0;

        /// <summary>是否全职(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_full_time", ColumnDescription = "是否全职", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsFullTime { get; set; } = 1;

        /// <summary>是否远程工作(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_remote_work", ColumnDescription = "是否远程工作", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRemoteWork { get; set; } = 0;

        /// <summary>工作地点</summary>
        [SugarColumn(ColumnName = "work_location", ColumnDescription = "工作地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkLocation { get; set; }

        /// <summary>汇报对象</summary>
        [SugarColumn(ColumnName = "reporting_to", ColumnDescription = "汇报对象", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReportingTo { get; set; }

        /// <summary>下属成员</summary>
        [SugarColumn(ColumnName = "subordinate_members", ColumnDescription = "下属成员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SubordinateMembers { get; set; }

        /// <summary>协作成员</summary>
        [SugarColumn(ColumnName = "collaboration_members", ColumnDescription = "协作成员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CollaborationMembers { get; set; }

        /// <summary>负责的任务</summary>
        [SugarColumn(ColumnName = "responsible_tasks", ColumnDescription = "负责的任务", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ResponsibleTasks { get; set; }

        /// <summary>参与的任务</summary>
        [SugarColumn(ColumnName = "participating_tasks", ColumnDescription = "参与的任务", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParticipatingTasks { get; set; }

        /// <summary>工作表现评价</summary>
        [SugarColumn(ColumnName = "performance_evaluation", ColumnDescription = "工作表现评价", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PerformanceEvaluation { get; set; }

        /// <summary>工作表现等级(1=优秀 2=良好 3=合格 4=不合格)</summary>
        [SugarColumn(ColumnName = "performance_grade", ColumnDescription = "工作表现等级", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
        public int PerformanceGrade { get; set; } = 3;

        /// <summary>项目成员状态(0=计划中 1=已加入 2=工作中 3=已离开 4=已移除)</summary>
        [SugarColumn(ColumnName = "project_member_status", ColumnDescription = "项目成员状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectMemberStatus { get; set; } = 0;

        /// <summary>成员备注</summary>
        [SugarColumn(ColumnName = "member_remark", ColumnDescription = "成员备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MemberRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



