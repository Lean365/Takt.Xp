#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStaffBudget.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 人员预算实体类 (基于SAP FI人员预算管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 人员预算实体类 (基于SAP FI人员预算管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 人员预算管理
    /// </remarks>
    [SugarTable("Takt_accounting_budget_staff_budget", "人员预算表")]
    [SugarIndex("ix_staff_budget_code", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, nameof(DepartmentCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, nameof(FiscalPeriod), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktStaffBudget : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门编码
        /// </summary>
        [SugarColumn(ColumnName = "department_code", ColumnDescription = "部门编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DepartmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "department_name", ColumnDescription = "部门名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 岗位编码
        /// </summary>
        [SugarColumn(ColumnName = "position_code", ColumnDescription = "岗位编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PositionCode { get; set; }

        /// <summary>
        /// 岗位名称
        /// </summary>
        [SugarColumn(ColumnName = "position_name", ColumnDescription = "岗位名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PositionName { get; set; }

        /// <summary>
        /// 员工编码
        /// </summary>
        [SugarColumn(ColumnName = "employee_code", ColumnDescription = "员工编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// 总账科目编码
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_code", ColumnDescription = "总账科目编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GlAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 预算科目编码
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_code", ColumnDescription = "预算科目编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetAccountCode { get; set; }

        /// <summary>
        /// 预算科目关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CompanyCode), nameof(BudgetAccountCode))]
        public TaktBudgetAccount? BudgetAccount { get; set; }

        /// <summary>
        /// 财政年度
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year", ColumnDescription = "财政年度", ColumnDataType = "int", IsNullable = false)]
        public int FiscalYear { get; set; }

        /// <summary>
        /// 财政期间
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_period", ColumnDescription = "财政期间", ColumnDataType = "int", IsNullable = false)]
        public int FiscalPeriod { get; set; }

        /// <summary>
        /// 预算类型(1=人员编制预算 2=薪资预算 3=培训预算 4=福利预算)
        /// </summary>
        [SugarColumn(ColumnName = "budget_type", ColumnDescription = "预算类型", ColumnDataType = "int", IsNullable = false)]
        public int BudgetType { get; set; }

        /// <summary>
        /// 预算版本
        /// </summary>
        [SugarColumn(ColumnName = "budget_version", ColumnDescription = "预算版本", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetVersion { get; set; } = string.Empty;

        /// <summary>
        /// 预算人数
        /// </summary>
        [SugarColumn(ColumnName = "budget_headcount", ColumnDescription = "预算人数", ColumnDataType = "int", IsNullable = false)]
        public int BudgetHeadcount { get; set; }

        /// <summary>
        /// 预算人均薪资
        /// </summary>
        [SugarColumn(ColumnName = "budget_average_salary", ColumnDescription = "预算人均薪资", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal BudgetAverageSalary { get; set; }

        /// <summary>
        /// 预算金额
        /// </summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false)]
        public decimal BudgetAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 已用预算人数
        /// </summary>
        [SugarColumn(ColumnName = "used_budget_headcount", ColumnDescription = "已用预算人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UsedBudgetHeadcount { get; set; } = 0;

        /// <summary>
        /// 已用预算金额
        /// </summary>
        [SugarColumn(ColumnName = "used_budget_amount", ColumnDescription = "已用预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UsedBudgetAmount { get; set; } = 0;

        /// <summary>
        /// 剩余预算人数
        /// </summary>
        [SugarColumn(ColumnName = "remaining_budget_headcount", ColumnDescription = "剩余预算人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RemainingBudgetHeadcount { get; set; } = 0;

        /// <summary>
        /// 剩余预算金额
        /// </summary>
        [SugarColumn(ColumnName = "remaining_budget_amount", ColumnDescription = "剩余预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RemainingBudgetAmount { get; set; } = 0;

        /// <summary>
        /// 预算使用率
        /// </summary>
        [SugarColumn(ColumnName = "budget_usage_rate", ColumnDescription = "预算使用率", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal BudgetUsageRate { get; set; } = 0;

        /// <summary>
        /// 预算调整金额
        /// </summary>
        [SugarColumn(ColumnName = "budget_adjustment_amount", ColumnDescription = "预算调整金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal BudgetAdjustmentAmount { get; set; } = 0;

        /// <summary>
        /// 预算结转金额
        /// </summary>
        [SugarColumn(ColumnName = "budget_carry_forward_amount", ColumnDescription = "预算结转金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal BudgetCarryForwardAmount { get; set; } = 0;

        /// <summary>
        /// 人员编制上限
        /// </summary>
        [SugarColumn(ColumnName = "headcount_limit", ColumnDescription = "人员编制上限", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int HeadcountLimit { get; set; } = 0;

        /// <summary>
        /// 人员编制下限
        /// </summary>
        [SugarColumn(ColumnName = "headcount_minimum", ColumnDescription = "人员编制下限", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int HeadcountMinimum { get; set; } = 0;

        /// <summary>
        /// 薪资等级
        /// </summary>
        [SugarColumn(ColumnName = "salary_grade", ColumnDescription = "薪资等级", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SalaryGrade { get; set; }

        /// <summary>
        /// 预算状态(0=草稿 1=已提交 2=已审批 3=已发布 4=已关闭)
        /// </summary>
        [SugarColumn(ColumnName = "budget_status", ColumnDescription = "预算状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BudgetStatus { get; set; } = 0;

        /// <summary>
        /// 预算描述
        /// </summary>
        [SugarColumn(ColumnName = "budget_description", ColumnDescription = "预算描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetDescription { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



