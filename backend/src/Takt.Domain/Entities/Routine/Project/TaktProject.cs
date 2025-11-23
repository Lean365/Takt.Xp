#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProject.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目的基本信息，包括项目分类、状态管理等
    /// </remarks>
    [SugarTable("Takt_routine_project", "项目表")]
    [SugarIndex("ix_project_code", nameof(ProjectCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_status", nameof(ProjectStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_type", nameof(ProjectType), OrderByType.Asc, false)]
    [SugarIndex("ix_project_category", nameof(ProjectCategory), OrderByType.Asc, false)]
    public class TaktProject : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目编号</summary>
        [SugarColumn(ColumnName = "project_code", ColumnDescription = "项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectCode { get; set; } = string.Empty;

        /// <summary>项目名称</summary>
        [SugarColumn(ColumnName = "project_name", ColumnDescription = "项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>项目类型(1=研发项目 2=实施项目 3=维护项目 4=咨询项目 5=培训项目 6=其他项目)</summary>
        [SugarColumn(ColumnName = "project_type", ColumnDescription = "项目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectType { get; set; } = 1;

        /// <summary>项目分类</summary>
        [SugarColumn(ColumnName = "project_category", ColumnDescription = "项目分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectCategory { get; set; }

        /// <summary>项目描述</summary>
        [SugarColumn(ColumnName = "project_description", ColumnDescription = "项目描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectDescription { get; set; }

        /// <summary>项目目标</summary>
        [SugarColumn(ColumnName = "project_objectives", ColumnDescription = "项目目标", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectObjectives { get; set; }

        /// <summary>项目范围</summary>
        [SugarColumn(ColumnName = "project_scope", ColumnDescription = "项目范围", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectScope { get; set; }

        /// <summary>项目预算</summary>
        [SugarColumn(ColumnName = "project_budget", ColumnDescription = "项目预算", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? ProjectBudget { get; set; }

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>项目开始日期</summary>
        [SugarColumn(ColumnName = "project_start_date", ColumnDescription = "项目开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>项目结束日期</summary>
        [SugarColumn(ColumnName = "project_end_date", ColumnDescription = "项目结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>项目期限(天)</summary>
        [SugarColumn(ColumnName = "project_duration", ColumnDescription = "项目期限(天)", ColumnDataType = "int", IsNullable = true)]
        public int? ProjectDuration { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际结束日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>项目进度(%)</summary>
        [SugarColumn(ColumnName = "project_progress", ColumnDescription = "项目进度(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProjectProgress { get; set; } = 0;

        /// <summary>项目优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "project_priority", ColumnDescription = "项目优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ProjectPriority { get; set; } = 2;

        /// <summary>项目规模(1=小型 2=中型 3=大型 4=特大型)</summary>
        [SugarColumn(ColumnName = "project_scale", ColumnDescription = "项目规模", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ProjectScale { get; set; } = 2;

        /// <summary>项目复杂度(1=简单 2=一般 3=复杂 4=极复杂)</summary>
        [SugarColumn(ColumnName = "project_complexity", ColumnDescription = "项目复杂度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ProjectComplexity { get; set; } = 2;

        /// <summary>项目状态(0=规划中 1=已启动 2=进行中 3=已完成 4=已暂停 5=已终止 6=已关闭)</summary>
        [SugarColumn(ColumnName = "project_status", ColumnDescription = "项目状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectStatus { get; set; } = 0;

        /// <summary>项目阶段(1=启动阶段 2=规划阶段 3=执行阶段 4=监控阶段 5=收尾阶段)</summary>
        [SugarColumn(ColumnName = "project_phase", ColumnDescription = "项目阶段", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectPhase { get; set; } = 1;

        /// <summary>项目发起人</summary>
        [SugarColumn(ColumnName = "project_sponsor", ColumnDescription = "项目发起人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectSponsor { get; set; }

        /// <summary>项目发起人电话</summary>
        [SugarColumn(ColumnName = "project_sponsor_phone", ColumnDescription = "项目发起人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>项目发起人邮箱</summary>
        [SugarColumn(ColumnName = "project_sponsor_email", ColumnDescription = "项目发起人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>项目经理</summary>
        [SugarColumn(ColumnName = "project_manager", ColumnDescription = "项目经理", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectManager { get; set; }

        /// <summary>项目经理电话</summary>
        [SugarColumn(ColumnName = "project_manager_phone", ColumnDescription = "项目经理电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectManagerPhone { get; set; }

        /// <summary>项目经理邮箱</summary>
        [SugarColumn(ColumnName = "project_manager_email", ColumnDescription = "项目经理邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectManagerEmail { get; set; }

        /// <summary>项目部门</summary>
        [SugarColumn(ColumnName = "project_department", ColumnDescription = "项目部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectDepartment { get; set; }

        /// <summary>项目团队</summary>
        [SugarColumn(ColumnName = "project_team", ColumnDescription = "项目团队", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectTeam { get; set; }

        /// <summary>客户名称</summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>客户联系人</summary>
        [SugarColumn(ColumnName = "customer_contact", ColumnDescription = "客户联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerContact { get; set; }

        /// <summary>客户联系电话</summary>
        [SugarColumn(ColumnName = "customer_phone", ColumnDescription = "客户联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerPhone { get; set; }

        /// <summary>客户联系邮箱</summary>
        [SugarColumn(ColumnName = "customer_email", ColumnDescription = "客户联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerEmail { get; set; }

        /// <summary>项目地点</summary>
        [SugarColumn(ColumnName = "project_location", ColumnDescription = "项目地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectLocation { get; set; }

        /// <summary>项目关键词</summary>
        [SugarColumn(ColumnName = "project_keywords", ColumnDescription = "项目关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectKeywords { get; set; }

        /// <summary>相关项目</summary>
        [SugarColumn(ColumnName = "related_projects", ColumnDescription = "相关项目", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedProjects { get; set; }

        /// <summary>相关合同</summary>
        [SugarColumn(ColumnName = "related_contracts", ColumnDescription = "相关合同", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedContracts { get; set; }

        /// <summary>项目备注</summary>
        [SugarColumn(ColumnName = "project_remark", ColumnDescription = "项目备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProjectRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



