#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFundBudget.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 资金预算实体类 (基于SAP FI资金预算管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 资金预算实体类 (基于SAP FI资金预算管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 资金预算管理
    /// </remarks>
    [SugarTable("Takt_accounting_budget_fund_budget", "资金预算表")]
    [SugarIndex("ix_fund_budget_code", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, nameof(GlAccountCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, nameof(FiscalPeriod), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktFundBudget : TaktBaseEntity
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
        /// 银行编码
        /// </summary>
        [SugarColumn(ColumnName = "bank_code", ColumnDescription = "银行编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankCode { get; set; }

        /// <summary>
        /// 银行账户编码
        /// </summary>
        [SugarColumn(ColumnName = "bank_account_code", ColumnDescription = "银行账户编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccountCode { get; set; }

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
        /// 预算类型(1=收入预算 2=支出预算 3=净现金流预算)
        /// </summary>
        [SugarColumn(ColumnName = "budget_type", ColumnDescription = "预算类型", ColumnDataType = "int", IsNullable = false)]
        public int BudgetType { get; set; }

        /// <summary>
        /// 预算版本
        /// </summary>
        [SugarColumn(ColumnName = "budget_version", ColumnDescription = "预算版本", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetVersion { get; set; } = string.Empty;

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
        /// 已用预算金额
        /// </summary>
        [SugarColumn(ColumnName = "used_budget_amount", ColumnDescription = "已用预算金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UsedBudgetAmount { get; set; } = 0;

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
        /// 期初余额
        /// </summary>
        [SugarColumn(ColumnName = "opening_balance", ColumnDescription = "期初余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OpeningBalance { get; set; } = 0;

        /// <summary>
        /// 期末余额
        /// </summary>
        [SugarColumn(ColumnName = "closing_balance", ColumnDescription = "期末余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ClosingBalance { get; set; } = 0;

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



