#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProfitCenter.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 利润中心实体类 (基于SAP CO利润中心管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Controlling
{
    /// <summary>
    /// 利润中心实体类 (基于SAP CO利润中心管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP CO-PCA 利润中心会计
    /// </remarks>
    [SugarTable("Takt_accounting_controlling_profit_center", "利润中心表")]
    [SugarIndex("ix_profit_center_code", nameof(ProfitCenterCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktProfitCenter : TaktBaseEntity
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
        /// 利润中心编码
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_code", ColumnDescription = "利润中心编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProfitCenterCode { get; set; } = string.Empty;

        /// <summary>
        /// 利润中心名称
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_name", ColumnDescription = "利润中心名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProfitCenterName { get; set; } = string.Empty;

        /// <summary>
        /// 利润中心简称
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_short_name", ColumnDescription = "利润中心简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenterShortName { get; set; }

        /// <summary>
        /// 利润中心类别
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_category", ColumnDescription = "利润中心类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProfitCenterCategory { get; set; } = 0;

        /// <summary>
        /// 上级利润中心ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级利润中心ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级利润中心
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktProfitCenter? Parent { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [SugarColumn(ColumnName = "manager_id", ColumnDescription = "负责人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ManagerId { get; set; }

        /// <summary>
        /// 负责人编码
        /// </summary>
        [SugarColumn(ColumnName = "manager_code", ColumnDescription = "负责人编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagerCode { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [SugarColumn(ColumnName = "manager_name", ColumnDescription = "负责人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ManagerName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DeptId { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptCode { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptName { get; set; }

        /// <summary>
        /// 成本中心ID
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_id", ColumnDescription = "成本中心ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? CostCenterId { get; set; }

        /// <summary>
        /// 成本中心编码
        /// </summary>
        [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CostCenterCode { get; set; }

        /// <summary>
        /// 成本中心关联
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(CostCenterCode))]
        public TaktCostCenter? CostCenter { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 利润中心描述
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_description", ColumnDescription = "利润中心描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenterDescription { get; set; }

        /// <summary>
        /// 利润中心地址
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_address", ColumnDescription = "利润中心地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenterAddress { get; set; }

        /// <summary>
        /// 利润中心电话
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_phone", ColumnDescription = "利润中心电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenterPhone { get; set; }

        /// <summary>
        /// 利润中心邮箱
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_email", ColumnDescription = "利润中心邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenterEmail { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = false, DefaultValue = "9999-12-31")]
        public DateTime? ExpiryDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>
        /// 是否允许过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_posting", ColumnDescription = "是否允许过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许手工过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_manual_posting", ColumnDescription = "是否允许手工过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowManualPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许自动过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_auto_posting", ColumnDescription = "是否允许自动过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowAutoPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许跨年度过账
        /// </summary>
        [SugarColumn(ColumnName = "allow_cross_year_posting", ColumnDescription = "是否允许跨年度过账", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AllowCrossYearPosting { get; set; } = 1;

        /// <summary>
        /// 是否允许负余额
        /// </summary>
        [SugarColumn(ColumnName = "allow_negative_balance", ColumnDescription = "是否允许负余额", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AllowNegativeBalance { get; set; } = 0;

        /// <summary>
        /// 是否明细账
        /// </summary>
        [SugarColumn(ColumnName = "is_detail_account", ColumnDescription = "是否明细账", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsDetailAccount { get; set; } = 0;

        /// <summary>
        /// 是否辅助核算
        /// </summary>
        [SugarColumn(ColumnName = "is_auxiliary_account", ColumnDescription = "是否辅助核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsAuxiliaryAccount { get; set; } = 0;

        /// <summary>
        /// 是否外币核算
        /// </summary>
        [SugarColumn(ColumnName = "is_foreign_currency", ColumnDescription = "是否外币核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsForeignCurrency { get; set; } = 0;

        /// <summary>
        /// 是否数量核算
        /// </summary>
        [SugarColumn(ColumnName = "is_quantity_account", ColumnDescription = "是否数量核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsQuantityAccount { get; set; } = 0;

        /// <summary>
        /// 利润中心余额
        /// </summary>
        [SugarColumn(ColumnName = "profit_center_balance", ColumnDescription = "利润中心余额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProfitCenterBalance { get; set; } = 0;

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
        /// 本年累计借方
        /// </summary>
        [SugarColumn(ColumnName = "year_debit_total", ColumnDescription = "本年累计借方", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal YearDebitTotal { get; set; } = 0;

        /// <summary>
        /// 本年累计贷方
        /// </summary>
        [SugarColumn(ColumnName = "year_credit_total", ColumnDescription = "本年累计贷方", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal YearCreditTotal { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
} 



