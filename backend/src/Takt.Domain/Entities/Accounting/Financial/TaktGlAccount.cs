#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGlAccount.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 总账科目主数据实体类 (基于SAP FI科目主数据SKA1/SKAT)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Accounting.Financial
{
    /// <summary>
    /// 总账科目主数据实体类 (基于SAP FI科目主数据SKA1/SKAT)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP FI-FI 科目主数据 (SKA1/SKAT)
    /// </remarks>
    [SugarTable("Takt_accounting_financial_gl_account", "总账科目主数据表")]
    [SugarIndex("ix_gl_account_code", nameof(GlAccountCode), OrderByType.Asc, true)]
    [SugarIndex("ix_accounts", nameof(AccountsCode), OrderByType.Asc, false)]
    public class TaktGlAccount : TaktBaseEntity
    {
        /// <summary>
        /// 科目表编码
        /// </summary>
        [SugarColumn(ColumnName = "accounts_code", ColumnDescription = "科目表编码", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AccountsCode { get; set; } = string.Empty;



        /// <summary>
        /// 总账科目编码
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_code", ColumnDescription = "总账科目编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GlAccountCode { get; set; } = string.Empty;

        /// <summary>
        /// 总账科目名称
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_name", ColumnDescription = "总账科目名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string GlAccountName { get; set; } = string.Empty;

        /// <summary>
        /// 总账科目简称
        /// </summary>
        [SugarColumn(ColumnName = "gl_account_short_name", ColumnDescription = "总账科目简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? GlAccountShortName { get; set; }

        /// <summary>
        /// 科目类型(1=资产 2=负债 3=权益 4=成本 5=损益)
        /// </summary>
        [SugarColumn(ColumnName = "account_type", ColumnDescription = "科目类型", ColumnDataType = "int", IsNullable = false)]
        public int AccountType { get; set; }

        /// <summary>
        /// 科目组
        /// </summary>
        [SugarColumn(ColumnName = "account_group", ColumnDescription = "科目组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccountGroup { get; set; }

        /// <summary>
        /// 科目级别
        /// </summary>
        [SugarColumn(ColumnName = "account_level", ColumnDescription = "科目级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AccountLevel { get; set; } = 1;

        /// <summary>
        /// 上级科目ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "上级科目ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// 上级科目
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktGlAccount? Parent { get; set; }

        /// <summary>
        /// 科目路径
        /// </summary>
        [SugarColumn(ColumnName = "account_path", ColumnDescription = "科目路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccountPath { get; set; }

        /// <summary>
        /// 科目性质(1=资产类 2=负债类 3=权益类 4=成本类 5=损益类)
        /// </summary>
        [SugarColumn(ColumnName = "account_nature", ColumnDescription = "科目性质", ColumnDataType = "int", IsNullable = false)]
        public int AccountNature { get; set; }

        /// <summary>
        /// 科目用途(1=总账 2=明细账 3=辅助账 4=备查账)
        /// </summary>
        [SugarColumn(ColumnName = "account_purpose", ColumnDescription = "科目用途", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AccountPurpose { get; set; } = 1;

        /// <summary>
        /// 科目描述
        /// </summary>
        [SugarColumn(ColumnName = "account_description", ColumnDescription = "科目描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccountDescription { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = false, DefaultValue = "9999-12-31")]
        public DateTime? ExpiryDate { get; set; }  = new DateTime(9999, 12, 31);

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



