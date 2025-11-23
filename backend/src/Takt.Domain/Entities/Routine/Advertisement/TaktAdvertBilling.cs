#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAdvertBilling.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 广告计费实体
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Advert
{
    /// <summary>
    /// 广告计费实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    [SugarTable("Takt_routine_advert_billing", "广告计费")]
    [SugarIndex("ix_billing_advert_id", nameof(AdvertId), OrderByType.Asc)]
    [SugarIndex("ix_billing_status", nameof(Status), OrderByType.Asc)]
    [SugarIndex("ix_billing_type", nameof(BillingType), OrderByType.Asc)]
    public class TaktAdvertBilling : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        #region 关联信息

        /// <summary>
        /// 广告ID
        /// </summary>
        [SugarColumn(ColumnName = "advert_id", ColumnDescription = "广告ID", ColumnDataType = "bigint", IsNullable = false)]
        public long AdvertId { get; set; }

        /// <summary>
        /// 广告标题（冗余字段，便于查询）
        /// </summary>
        [SugarColumn(ColumnName = "advert_title", ColumnDescription = "广告标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string AdvertTitle { get; set; } = string.Empty;

        #endregion 关联信息

        #region 计费基础信息

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        [SugarColumn(ColumnName = "billing_type", ColumnDescription = "计费方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BillingType { get; set; }

        /// <summary>
        /// 计费单价（元）
        /// </summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "计费单价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "budget_amount", ColumnDescription = "预算金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal BudgetAmount { get; set; }

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "spent_amount", ColumnDescription = "已消耗金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal SpentAmount { get; set; }

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "remaining_amount", ColumnDescription = "剩余金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RemainingAmount { get; set; }

        #endregion 计费基础信息

        #region 计费统计

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "impression_count", ColumnDescription = "展示次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ImpressionCount { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "click_count", ColumnDescription = "点击次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ClickCount { get; set; }

        /// <summary>
        /// 行动次数
        /// </summary>
        [SugarColumn(ColumnName = "action_count", ColumnDescription = "行动次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ActionCount { get; set; }

        /// <summary>
        /// 千次展示数（CPM计算用）
        /// </summary>
        [SugarColumn(ColumnName = "cpm_count", ColumnDescription = "千次展示数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CpmCount { get; set; }

        #endregion 计费统计

        #region 计费状态

        /// <summary>
        /// 计费状态 (0: 未开始, 1: 计费中, 2: 已暂停, 3: 已结束, 4: 已取消)
        /// </summary>
        [SugarColumn(ColumnName = "billing_status", ColumnDescription = "计费状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; }

        /// <summary>
        /// 计费开始时间
        /// </summary>
        [SugarColumn(ColumnName = "billing_start_time", ColumnDescription = "计费开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        [SugarColumn(ColumnName = "billing_end_time", ColumnDescription = "计费结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? BillingEndTime { get; set; }

        /// <summary>
        /// 最后计费时间
        /// </summary>
        [SugarColumn(ColumnName = "last_billing_time", ColumnDescription = "最后计费时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastBillingTime { get; set; }

        #endregion 计费状态

        #region 计费规则

        /// <summary>
        /// 每日预算限制（元）
        /// </summary>
        [SugarColumn(ColumnName = "daily_budget_limit", ColumnDescription = "每日预算限制", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal DailyBudgetLimit { get; set; }

        /// <summary>
        /// 每小时预算限制（元）
        /// </summary>
        [SugarColumn(ColumnName = "hourly_budget_limit", ColumnDescription = "每小时预算限制", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal HourlyBudgetLimit { get; set; }

        /// <summary>
        /// 最低出价（元）
        /// </summary>
        [SugarColumn(ColumnName = "min_bid", ColumnDescription = "最低出价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MinBid { get; set; }

        /// <summary>
        /// 最高出价（元）
        /// </summary>
        [SugarColumn(ColumnName = "max_bid", ColumnDescription = "最高出价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MaxBid { get; set; }

        #endregion 计费规则

        #region 计费明细

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "today_spent", ColumnDescription = "今日消耗金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal TodaySpent { get; set; }

        /// <summary>
        /// 昨日消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "yesterday_spent", ColumnDescription = "昨日消耗金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal YesterdaySpent { get; set; }

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "monthly_spent", ColumnDescription = "本月消耗金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal MonthlySpent { get; set; }

        /// <summary>
        /// 上月消耗金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "last_month_spent", ColumnDescription = "上月消耗金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal LastMonthSpent { get; set; }

        #endregion 计费明细

        #region 计费控制

        /// <summary>
        /// 是否自动续费 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "auto_renewal", ColumnDescription = "是否自动续费", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AutoRenewal { get; set; }

        /// <summary>
        /// 续费金额（元）
        /// </summary>
        [SugarColumn(ColumnName = "renewal_amount", ColumnDescription = "续费金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RenewalAmount { get; set; }

        /// <summary>
        /// 续费触发阈值（元）
        /// </summary>
        [SugarColumn(ColumnName = "renewal_threshold", ColumnDescription = "续费触发阈值", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
        public decimal RenewalThreshold { get; set; }

        /// <summary>
        /// 是否启用预算预警 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "budget_alert_enabled", ColumnDescription = "是否启用预算预警", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BudgetAlertEnabled { get; set; }

        /// <summary>
        /// 预算预警阈值（百分比）
        /// </summary>
        [SugarColumn(ColumnName = "budget_alert_threshold", ColumnDescription = "预算预警阈值", ColumnDataType = "int", IsNullable = false, DefaultValue = "80")]
        public int BudgetAlertThreshold { get; set; }

        #endregion 计费控制

        #region 计费审核

        /// <summary>
        /// 计费审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "audit_status", ColumnDescription = "计费审核状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        [SugarColumn(ColumnName = "audit_comments", ColumnDescription = "审核意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        [SugarColumn(ColumnName = "audited_by", ColumnDescription = "审核人", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [SugarColumn(ColumnName = "audited_time", ColumnDescription = "审核时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? AuditedTime { get; set; }

        #endregion 计费审核

        #region 扩展属性

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        [SugarColumn(ColumnName = "ext_properties", ColumnDescription = "扩展属性JSON", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string? ExtProperties { get; set; }

        #endregion 扩展属性

        #region 导航属性

        /// <summary>
        /// 关联的广告实体
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TaktAdvert? Advert { get; set; }

        #endregion 导航属性
    }
}