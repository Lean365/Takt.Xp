#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBudgetAccount.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 预算科目实体类 (基于SAP FI预算科目管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Budget
{
    /// <summary>
    /// 预算科目实体类 (基于SAP FI预算科目管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 预算科目管理
    /// </remarks>
    [SugarTable("Takt_accounting_budget_budget_account", "预算科目表")]
    [SugarIndex("ix_budget_account_code", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, nameof(GlAccountCode), OrderByType.Asc, nameof(BudgetType), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktBudgetAccount : TaktBaseEntity
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
        /// 公司科目关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CompanyCode), nameof(GlAccountCode))]
        public Financial.TaktCcAccount? CcAccount { get; set; }

        /// <summary>
        /// 预算类型(1=成本预算 2=费用预算 3=收入预算 4=投资预算 5=资金预算 6=销售预算 7=人员预算 8=模具预算)
        /// </summary>
        [SugarColumn(ColumnName = "budget_type", ColumnDescription = "预算类型", ColumnDataType = "int", IsNullable = false)]
        public int BudgetType { get; set; }

        /// <summary>
        /// 预算科目编码
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_code", ColumnDescription = "预算科目编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 预算科目名称
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_name", ColumnDescription = "预算科目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetAccountName { get; set; } = string.Empty;

        /// <summary>
        /// 预算科目类别(1=收入类 2=成本类 3=费用类 4=投资类 5=资金类)
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_category", ColumnDescription = "预算科目类别", ColumnDataType = "int", IsNullable = false)]
        public int BudgetAccountCategory { get; set; }

        /// <summary>
        /// 预算科目级别(1=一级 2=二级 3=三级 4=四级 5=五级)
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_level", ColumnDescription = "预算科目级别", ColumnDataType = "int", IsNullable = false)]
        public int BudgetAccountLevel { get; set; }

        /// <summary>
        /// 上级预算科目编码
        /// </summary>
        [SugarColumn(ColumnName = "parent_budget_account_code", ColumnDescription = "上级预算科目编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ParentBudgetAccountCode { get; set; }

        /// <summary>
        /// 上级预算科目
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentBudgetAccountCode))]
        public TaktBudgetAccount? ParentBudgetAccount { get; set; }

        /// <summary>
        /// 预算科目路径
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_path", ColumnDescription = "预算科目路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BudgetAccountPath { get; set; } = string.Empty;

        /// <summary>
        /// 是否末级科目
        /// </summary>
        [SugarColumn(ColumnName = "is_leaf_account", ColumnDescription = "是否末级科目", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsLeafAccount { get; set; } = 1;

        /// <summary>
        /// 是否启用预算控制
        /// </summary>
        [SugarColumn(ColumnName = "enable_budget_control", ColumnDescription = "是否启用预算控制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int EnableBudgetControl { get; set; } = 1;

        /// <summary>
        /// 预算控制类型(1=严格控制 2=预警控制 3=不控制)
        /// </summary>
        [SugarColumn(ColumnName = "budget_control_type", ColumnDescription = "预算控制类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int BudgetControlType { get; set; } = 2;

        /// <summary>
        /// 预算预警阈值(%)
        /// </summary>
        [SugarColumn(ColumnName = "budget_warning_threshold", ColumnDescription = "预算预警阈值", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "80")]
        public decimal BudgetWarningThreshold { get; set; } = 80;

        /// <summary>
        /// 预算超支阈值(%)
        /// </summary>
        [SugarColumn(ColumnName = "budget_overrun_threshold", ColumnDescription = "预算超支阈值", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "100")]
        public decimal BudgetOverrunThreshold { get; set; } = 100;

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
        /// 是否允许负预算
        /// </summary>
        [SugarColumn(ColumnName = "allow_negative_budget", ColumnDescription = "是否允许负预算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AllowNegativeBudget { get; set; } = 0;

        /// <summary>
        /// 预算科目属性(1=经营性 2=投资性 3=筹资性 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_property", ColumnDescription = "预算科目属性", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int BudgetAccountProperty { get; set; } = 1;

        /// <summary>
        /// 预算科目用途(1=日常经营 2=项目投资 3=资本支出 4=研发投入 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_purpose", ColumnDescription = "预算科目用途", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int BudgetAccountPurpose { get; set; } = 1;

        /// <summary>
        /// 预算科目描述
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_description", ColumnDescription = "预算科目描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetAccountDescription { get; set; }

        /// <summary>
        /// 预算科目备注
        /// </summary>
        [SugarColumn(ColumnName = "budget_account_remark", ColumnDescription = "预算科目备注", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BudgetAccountRemark { get; set; }

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



