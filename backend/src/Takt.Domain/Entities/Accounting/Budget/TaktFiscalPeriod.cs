#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFiscalPeriod.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 财政期间实体类 (基于SAP FI财政年度变式)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 财政期间实体类 (基于SAP FI财政年度变式)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 财政年度变式
    /// </remarks>
    [SugarTable("Takt_accounting_budget_fiscal_period", "财政期间")]
    [SugarIndex("ix_fiscal_period_code", nameof(CompanyCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, nameof(FiscalPeriod), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktFiscalPeriod : TaktBaseEntity
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
        /// 期间名称
        /// </summary>
        [SugarColumn(ColumnName = "period_name", ColumnDescription = "期间名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PeriodName { get; set; } = string.Empty;

        /// <summary>
        /// 期间简称
        /// </summary>
        [SugarColumn(ColumnName = "period_short_name", ColumnDescription = "期间简称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PeriodShortName { get; set; }

        /// <summary>
        /// 期间类型(1=月度 2=季度 3=半年度 4=年度)
        /// </summary>
        [SugarColumn(ColumnName = "period_type", ColumnDescription = "期间类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PeriodType { get; set; } = 1;

        /// <summary>
        /// 期间开始日期
        /// </summary>
        [SugarColumn(ColumnName = "period_start_date", ColumnDescription = "期间开始日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PeriodStartDate { get; set; }

        /// <summary>
        /// 期间结束日期
        /// </summary>
        [SugarColumn(ColumnName = "period_end_date", ColumnDescription = "期间结束日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime PeriodEndDate { get; set; }

        /// <summary>
        /// 是否允许过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_posting", ColumnDescription = "是否允许过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许预算调整
        /// </summary>
        [SugarColumn(ColumnName = "allow_budget_adjustment", ColumnDescription = "是否允许预算调整", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowBudgetAdjustment { get; set; } = 1;

        /// <summary>
        /// 是否允许预算结转
        /// </summary>
        [SugarColumn(ColumnName = "allow_budget_carry_forward", ColumnDescription = "是否允许预算结转", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowBudgetCarryForward { get; set; } = 1;

        /// <summary>
        /// 期间状态(0=未开始 1=进行中 2=已关闭 3=已锁定)
        /// </summary>
        [SugarColumn(ColumnName = "period_status", ColumnDescription = "期间状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PeriodStatus { get; set; } = 0;

        /// <summary>
        /// 期间描述
        /// </summary>
        [SugarColumn(ColumnName = "period_description", ColumnDescription = "期间描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PeriodDescription { get; set; }

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



