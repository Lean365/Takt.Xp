#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBank.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 银行实体类 (基于SAP FI银行主数据)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Financial
{
    /// <summary>
    /// 银行实体类 (基于SAP FI银行主数据)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 银行主数据
    /// </remarks>
    [SugarTable("Takt_accounting_financial_bank", "银行信息")]
    [SugarIndex("ix_bank_code", nameof(BankCode), OrderByType.Asc, true)]
    [SugarIndex("ix_swift_code", nameof(SwiftCode), OrderByType.Asc, false)]
    public class TaktBank : TaktBaseEntity
    {
        /// <summary>
        /// 银行国家代码
        /// </summary>
        [SugarColumn(ColumnName = "bank_country", ColumnDescription = "银行国家代码", Length = 3, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BankCountry { get; set; } = string.Empty;

        /// <summary>
        /// 银行编码
        /// </summary>
        [SugarColumn(ColumnName = "bank_code", ColumnDescription = "银行编码", Length = 15, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BankCode { get; set; } = string.Empty;

        /// <summary>
        /// 银行名称
        /// </summary>
        [SugarColumn(ColumnName = "bank_name", ColumnDescription = "银行名称", Length = 60, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BankName { get; set; } = string.Empty;

        /// <summary>
        /// 银行简称
        /// </summary>
        [SugarColumn(ColumnName = "bank_short_name", ColumnDescription = "银行简称", Length = 25, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankShortName { get; set; }

        /// <summary>
        /// 银行街道地址
        /// </summary>
        [SugarColumn(ColumnName = "bank_street", ColumnDescription = "银行街道地址", Length = 60, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankStreet { get; set; }

        /// <summary>
        /// 银行城市
        /// </summary>
        [SugarColumn(ColumnName = "bank_city", ColumnDescription = "银行城市", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankCity { get; set; }

        /// <summary>
        /// 银行地区/州
        /// </summary>
        [SugarColumn(ColumnName = "bank_region", ColumnDescription = "银行地区/州", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankRegion { get; set; }

        /// <summary>
        /// 银行邮政编码
        /// </summary>
        [SugarColumn(ColumnName = "bank_postal_code", ColumnDescription = "银行邮政编码", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankPostalCode { get; set; }

        /// <summary>
        /// 银行电话
        /// </summary>
        [SugarColumn(ColumnName = "bank_phone", ColumnDescription = "银行电话", Length = 16, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankPhone { get; set; }

        /// <summary>
        /// 银行传真
        /// </summary>
        [SugarColumn(ColumnName = "bank_fax", ColumnDescription = "银行传真", Length = 16, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankFax { get; set; }

        /// <summary>
        /// 银行电传
        /// </summary>
        [SugarColumn(ColumnName = "bank_telex", ColumnDescription = "银行电传", Length = 16, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankTelex { get; set; }

        /// <summary>
        /// SWIFT代码
        /// </summary>
        [SugarColumn(ColumnName = "swift_code", ColumnDescription = "SWIFT代码", Length = 11, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SwiftCode { get; set; }

        /// <summary>
        /// 银行网络
        /// </summary>
        [SugarColumn(ColumnName = "bank_network", ColumnDescription = "银行网络", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankNetwork { get; set; }

        /// <summary>
        /// 银行组
        /// </summary>
        [SugarColumn(ColumnName = "bank_group", ColumnDescription = "银行组", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankGroup { get; set; }

        /// <summary>
        /// 银行控制键
        /// </summary>
        [SugarColumn(ColumnName = "bank_control_key", ColumnDescription = "银行控制键", Length = 2, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankControlKey { get; set; }

        /// <summary>
        /// 银行账户类型
        /// </summary>
        [SugarColumn(ColumnName = "bank_account_type", ColumnDescription = "银行账户类型", Length = 1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccountType { get; set; }

        /// <summary>
        /// 银行账户组
        /// </summary>
        [SugarColumn(ColumnName = "bank_account_group", ColumnDescription = "银行账户组", Length = 4, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankAccountGroup { get; set; }

        /// <summary>
        /// 银行账户货币
        /// </summary>
        [SugarColumn(ColumnName = "bank_currency", ColumnDescription = "银行账户货币", Length = 3, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BankCurrency { get; set; }

        /// <summary>
        /// 银行账户限制
        /// </summary>
        [SugarColumn(ColumnName = "bank_limit", ColumnDescription = "银行账户限制", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? BankLimit { get; set; }

        /// <summary>
        /// 银行账户余额
        /// </summary>
        [SugarColumn(ColumnName = "bank_balance", ColumnDescription = "银行账户余额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? BankBalance { get; set; }

        /// <summary>
        /// 银行账户透支限额
        /// </summary>
        [SugarColumn(ColumnName = "bank_overdraft_limit", ColumnDescription = "银行账户透支限额", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? BankOverdraftLimit { get; set; }

        /// <summary>
        /// 银行账户利息率
        /// </summary>
        [SugarColumn(ColumnName = "bank_interest_rate", ColumnDescription = "银行账户利息率", ColumnDataType = "decimal(5,4)", IsNullable = true)]
        public decimal? BankInterestRate { get; set; }

        /// <summary>
        /// 银行账户手续费
        /// </summary>
        [SugarColumn(ColumnName = "bank_fee", ColumnDescription = "银行账户手续费", ColumnDataType = "decimal(10,2)", IsNullable = true)]
        public decimal? BankFee { get; set; }

        /// <summary>
        /// 银行账户开户日期
        /// </summary>
        [SugarColumn(ColumnName = "bank_open_date", ColumnDescription = "银行账户开户日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BankOpenDate { get; set; }

        /// <summary>
        /// 银行账户关闭日期
        /// </summary>
        [SugarColumn(ColumnName = "bank_close_date", ColumnDescription = "银行账户关闭日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? BankCloseDate { get; set; }

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



