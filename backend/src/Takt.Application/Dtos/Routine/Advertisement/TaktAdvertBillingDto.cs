#nullable enable

//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktAdvertBillingDto.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V1.0.0
// 描述   : 广告计费数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Advertisement
{
    /// <summary>
    /// 广告计费基础DTO（与TaktAdvertBilling实体字段严格对应）
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    public class TaktAdvertBillingDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        #region 关联信息

        /// <summary>
        /// 广告ID
        /// </summary>
        public long AdvertId { get; set; }

        /// <summary>
        /// 广告标题（冗余字段，便于查询）
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        #endregion

        #region 计费基础信息

        /// <summary>
        /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
        /// </summary>
        public int BillingType { get; set; }

        /// <summary>
        /// 计费单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BudgetAmount { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal SpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal RemainingAmount { get; set; } = 0;

        #endregion

        #region 计费统计

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ImpressionCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; } = 0;

        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount { get; set; } = 0;

        /// <summary>
        /// 千次展示数（CPM计算用）
        /// </summary>
        public int CpmCount { get; set; } = 0;

        #endregion

        #region 计费状态

        /// <summary>
        /// 计费状态 (0: 未开始, 1: 计费中, 2: 已暂停, 3: 已结束, 4: 已取消)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }

        /// <summary>
        /// 最后计费时间
        /// </summary>
        public DateTime? LastBillingTime { get; set; }

        #endregion

        #region 计费规则

        /// <summary>
        /// 每日预算限制（元）
        /// </summary>
        public decimal DailyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 每小时预算限制（元）
        /// </summary>
        public decimal HourlyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 最低出价（元）
        /// </summary>
        public decimal MinBid { get; set; } = 0;

        /// <summary>
        /// 最高出价（元）
        /// </summary>
        public decimal MaxBid { get; set; } = 0;

        #endregion

        #region 计费明细

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        public decimal TodaySpent { get; set; } = 0;

        /// <summary>
        /// 昨日消耗金额（元）
        /// </summary>
        public decimal YesterdaySpent { get; set; } = 0;

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        public decimal MonthlySpent { get; set; } = 0;

        /// <summary>
        /// 上月消耗金额（元）
        /// </summary>
        public decimal LastMonthSpent { get; set; } = 0;

        #endregion

        #region 计费控制

        /// <summary>
        /// 是否自动续费 (0: 否, 1: 是)
        /// </summary>
        public int AutoRenewal { get; set; } = 0;

        /// <summary>
        /// 续费金额（元）
        /// </summary>
        public decimal RenewalAmount { get; set; } = 0;

        /// <summary>
        /// 续费触发阈值（元）
        /// </summary>
        public decimal RenewalThreshold { get; set; } = 0;

        /// <summary>
        /// 是否启用预算预警 (0: 否, 1: 是)
        /// </summary>
        public int BudgetAlertEnabled { get; set; } = 0;

        /// <summary>
        /// 预算预警阈值（百分比）
        /// </summary>
        public int BudgetAlertThreshold { get; set; } = 80;

        #endregion

        #region 计费审核

        /// <summary>
        /// 计费审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int AuditStatus { get; set; } = 0;

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        #endregion

        #region 扩展属性

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }

        #endregion

        #region 基础字段

        /// <summary>
        /// 自定义字段1
        /// </summary>
        public string? Udf01 { get; set; }

        /// <summary>
        /// 自定义字段2
        /// </summary>
        public string? Udf02 { get; set; }

        /// <summary>
        /// 自定义字段3
        /// </summary>
        public string? Udf03 { get; set; }

        /// <summary>
        /// 自定义字段4
        /// </summary>
        public string? Udf04 { get; set; }

        /// <summary>
        /// 自定义字段5
        /// </summary>
        public string? Udf05 { get; set; }

        /// <summary>
        /// 自定义字段6
        /// </summary>
        public string? Udf06 { get; set; }

        /// <summary>
        /// 自定义数值1
        /// </summary>
        public decimal? Udf51 { get; set; } = 0;

        /// <summary>
        /// 自定义数值2
        /// </summary>
        public decimal? Udf52 { get; set; } = 0;

        /// <summary>
        /// 自定义数值3
        /// </summary>
        public decimal? Udf53 { get; set; } = 0;

        /// <summary>
        /// 自定义数值4
        /// </summary>
        public int? Udf54 { get; set; } = 0;

        /// <summary>
        /// 自定义数值5
        /// </summary>
        public int? Udf55 { get; set; } = 0;

        /// <summary>
        /// 自定义数值6
        /// </summary>
        public int? Udf56 { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 关联的广告实体
        /// </summary>
        public TaktAdvertDto? Advert { get; set; }

        #endregion
    }

    /// <summary>
    /// 广告计费查询DTO
    /// </summary>
    public class TaktAdvertBillingQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingQueryDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string? CompanyCode { get; set; }

        /// <summary>
        /// 广告ID
        /// </summary>
        public long? AdvertId { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public string? AdvertTitle { get; set; }

        /// <summary>
        /// 计费方式
        /// </summary>
        public int? BillingType { get; set; }

        /// <summary>
        /// 计费状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int? AuditStatus { get; set; }

        /// <summary>
        /// 是否自动续费
        /// </summary>
        public int? AutoRenewal { get; set; }

        /// <summary>
        /// 是否启用预算预警
        /// </summary>
        public int? BudgetAlertEnabled { get; set; }

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }
    }

    /// <summary>
    /// 广告计费创建DTO
    /// </summary>
    public class TaktAdvertBillingCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingCreateDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
        }

    /// <summary>公司代码</summary>
    public string CompanyCode { get; set; } = string.Empty;
    #region 关联信息
    /// <summary>
    /// 广告ID
    /// </summary>
    public long AdvertId { get; set; }
    /// <summary>
    /// 广告标题（冗余字段，便于查询）
    /// </summary>
    public string AdvertTitle { get; set; } = string.Empty;
    #endregion
    #region 计费基础信息
    /// <summary>
    /// 计费方式 (1: CPM按千次展示, 2: CPC按点击, 3: CPA按行动, 4: 固定价格)
    /// </summary>
    public int BillingType { get; set; }
    /// <summary>
    /// 计费单价（元）
    /// </summary>
    public decimal UnitPrice { get; set; } = 0;
    /// <summary>
    /// 预算金额（元）
    /// </summary>
    public decimal BudgetAmount { get; set; } = 0;
    /// <summary>
    /// 已消耗金额（元）
    /// </summary>
    public decimal SpentAmount { get; set; } = 0;
    /// <summary>
    /// 剩余金额（元）
    /// </summary>
    public decimal RemainingAmount { get; set; } = 0;
    #endregion
    #region 计费统计
    /// <summary>
    /// 展示次数
    /// </summary>
    public int ImpressionCount { get; set; } = 0;
    /// <summary>
    /// 点击次数
    /// </summary>
    public int ClickCount { get; set; } = 0;
    /// <summary>
    /// 行动次数
    /// </summary>
    public int ActionCount { get; set; } = 0;
    /// <summary>
    /// 千次展示数（CPM计算用）
    /// </summary>
    public int CpmCount { get; set; } = 0;
    #endregion
    #region 计费状态
    /// <summary>
    /// 计费状态 (0: 未开始, 1: 计费中, 2: 已暂停, 3: 已结束, 4: 已取消)
    /// </summary>
    public int Status { get; set; } = 0;
    /// <summary>
    /// 计费开始时间
    /// </summary>
    public DateTime? BillingStartTime { get; set; }
    /// <summary>
    /// 计费结束时间
    /// </summary>
    public DateTime? BillingEndTime { get; set; }
    /// <summary>
    /// 最后计费时间
    /// </summary>
    public DateTime? LastBillingTime { get; set; }
    #endregion
    #region 计费规则
    /// <summary>
    /// 每日预算限制（元）
    /// </summary>
    public decimal DailyBudgetLimit { get; set; } = 0;
    /// <summary>
    /// 每小时预算限制（元）
    /// </summary>
    public decimal HourlyBudgetLimit { get; set; } = 0;
    /// <summary>
    /// 最低出价（元）
    /// </summary>
    public decimal MinBid { get; set; } = 0;
    /// <summary>
    /// 最高出价（元）
    /// </summary>
    public decimal MaxBid { get; set; } = 0;
    #endregion
    #region 计费明细
    /// <summary>
    /// 今日消耗金额（元）
    /// </summary>
    public decimal TodaySpent { get; set; } = 0;
    /// <summary>
    /// 昨日消耗金额（元）
    /// </summary>
    public decimal YesterdaySpent { get; set; } = 0;
    /// <summary>
    /// 本月消耗金额（元）
    /// </summary>
    public decimal MonthlySpent { get; set; } = 0;
    /// <summary>
    /// 上月消耗金额（元）
    /// </summary>
    public decimal LastMonthSpent { get; set; } = 0;
    #endregion
    #region 计费控制
    /// <summary>
    /// 是否自动续费 (0: 否, 1: 是)
    /// </summary>
    public int AutoRenewal { get; set; } = 0;
    /// <summary>
    /// 续费金额（元）
    /// </summary>
    public decimal RenewalAmount { get; set; } = 0;
    /// <summary>
    /// 续费触发阈值（元）
    /// </summary>
    public decimal RenewalThreshold { get; set; } = 0;
    /// <summary>
    /// 是否启用预算预警 (0: 否, 1: 是)
    /// </summary>
    public int BudgetAlertEnabled { get; set; } = 0;
    /// <summary>
    /// 预算预警阈值（百分比）
    /// </summary>
    public int BudgetAlertThreshold { get; set; } = 80;
    #endregion
    #region 计费审核
    /// <summary>
    /// 计费审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
    /// </summary>
    public int AuditStatus { get; set; } = 0;
    /// <summary>
    /// 审核意见
    /// </summary>
    public string? AuditComments { get; set; }
    /// <summary>
    /// 审核人
    /// </summary>
    public string? AuditedBy { get; set; }
    /// <summary>
    /// 审核时间
    /// </summary>
    public DateTime? AuditedTime { get; set; }
    #endregion
    #region 扩展属性
    /// <summary>
    /// 扩展属性JSON
    /// </summary>
    public string? ExtProperties { get; set; }
    #endregion
    #region 基础字段
    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
    #endregion
    }

    /// <summary>
    /// 广告计费更新DTO
    /// </summary>
    public class TaktAdvertBillingUpdateDto : TaktAdvertBillingCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }
    }

    /// <summary>
    /// 广告计费导出DTO
    /// </summary>
    public class TaktAdvertBillingExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingExportDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            BillingType = string.Empty;
            Status = string.Empty;
            AuditStatus = string.Empty;
            AutoRenewal = string.Empty;
            BudgetAlertEnabled = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告ID
        /// </summary>
        public long AdvertId { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 计费方式
        /// </summary>
        public string BillingType { get; set; } = string.Empty;

        /// <summary>
        /// 计费单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BudgetAmount { get; set; }

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal SpentAmount { get; set; }

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal RemainingAmount { get; set; }

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ImpressionCount { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount { get; set; }

        /// <summary>
        /// 千次展示数
        /// </summary>
        public int CpmCount { get; set; }

        /// <summary>
        /// 计费状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }

        /// <summary>
        /// 最后计费时间
        /// </summary>
        public DateTime? LastBillingTime { get; set; }

        /// <summary>
        /// 每日预算限制（元）
        /// </summary>
        public decimal DailyBudgetLimit { get; set; }

        /// <summary>
        /// 每小时预算限制（元）
        /// </summary>
        public decimal HourlyBudgetLimit { get; set; }

        /// <summary>
        /// 最低出价（元）
        /// </summary>
        public decimal MinBid { get; set; }

        /// <summary>
        /// 最高出价（元）
        /// </summary>
        public decimal MaxBid { get; set; }

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        public decimal TodaySpent { get; set; }

        /// <summary>
        /// 昨日消耗金额（元）
        /// </summary>
        public decimal YesterdaySpent { get; set; }

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        public decimal MonthlySpent { get; set; }

        /// <summary>
        /// 上月消耗金额（元）
        /// </summary>
        public decimal LastMonthSpent { get; set; }

        /// <summary>
        /// 是否自动续费
        /// </summary>
        public string AutoRenewal { get; set; } = string.Empty;

        /// <summary>
        /// 续费金额（元）
        /// </summary>
        public decimal RenewalAmount { get; set; }

        /// <summary>
        /// 续费触发阈值（元）
        /// </summary>
        public decimal RenewalThreshold { get; set; }

        /// <summary>
        /// 是否启用预算预警
        /// </summary>
        public string BudgetAlertEnabled { get; set; } = string.Empty;

        /// <summary>
        /// 预算预警阈值（百分比）
        /// </summary>
        public int BudgetAlertThreshold { get; set; }

        /// <summary>
        /// 计费审核状态
        /// </summary>
        public string AuditStatus { get; set; } = string.Empty;

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }
    }

    /// <summary>
    /// 广告计费模板DTO
    /// </summary>
    public class TaktAdvertBillingTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingTemplateDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告ID
        /// </summary>
        public string AdvertId { get; set; } = string.Empty;

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 计费方式
        /// </summary>
        public string BillingType { get; set; } = string.Empty;

        /// <summary>
        /// 计费单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BudgetAmount { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal SpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal RemainingAmount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ImpressionCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; } = 0;

        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount { get; set; } = 0;

        /// <summary>
        /// 千次展示数
        /// </summary>
        public int CpmCount { get; set; } = 0;

        /// <summary>
        /// 计费状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }

        /// <summary>
        /// 最后计费时间
        /// </summary>
        public DateTime? LastBillingTime { get; set; }

        /// <summary>
        /// 每日预算限制（元）
        /// </summary>
        public decimal DailyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 每小时预算限制（元）
        /// </summary>
        public decimal HourlyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 最低出价（元）
        /// </summary>
        public decimal MinBid { get; set; } = 0;

        /// <summary>
        /// 最高出价（元）
        /// </summary>
        public decimal MaxBid { get; set; } = 0;

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        public decimal TodaySpent { get; set; } = 0;

        /// <summary>
        /// 昨日消耗金额（元）
        /// </summary>
        public decimal YesterdaySpent { get; set; } = 0;

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        public decimal MonthlySpent { get; set; } = 0;

        /// <summary>
        /// 上月消耗金额（元）
        /// </summary>
        public decimal LastMonthSpent { get; set; } = 0;

        /// <summary>
        /// 是否自动续费
        /// </summary>
        public int AutoRenewal { get; set; } = 0;

        /// <summary>
        /// 续费金额（元）
        /// </summary>
        public decimal RenewalAmount { get; set; } = 0;

        /// <summary>
        /// 续费触发阈值（元）
        /// </summary>
        public decimal RenewalThreshold { get; set; } = 0;

        /// <summary>
        /// 是否启用预算预警
        /// </summary>
        public int BudgetAlertEnabled { get; set; } = 0;

        /// <summary>
        /// 预算预警阈值（百分比）
        /// </summary>
        public int BudgetAlertThreshold { get; set; } = 80;

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }
    }

    /// <summary>
    /// 广告计费导入DTO
    /// </summary>
    public class TaktAdvertBillingImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingImportDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告ID
        /// </summary>
        public long AdvertId { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 计费方式
        /// </summary>
        public int BillingType { get; set; }

        /// <summary>
        /// 计费单价（元）
        /// </summary>
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>
        /// 预算金额（元）
        /// </summary>
        public decimal BudgetAmount { get; set; } = 0;

        /// <summary>
        /// 已消耗金额（元）
        /// </summary>
        public decimal SpentAmount { get; set; } = 0;

        /// <summary>
        /// 剩余金额（元）
        /// </summary>
        public decimal RemainingAmount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ImpressionCount { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; } = 0;

        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount { get; set; } = 0;

        /// <summary>
        /// 千次展示数
        /// </summary>
        public int CpmCount { get; set; } = 0;

        /// <summary>
        /// 计费状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }

        /// <summary>
        /// 最后计费时间
        /// </summary>
        public DateTime? LastBillingTime { get; set; }

        /// <summary>
        /// 每日预算限制（元）
        /// </summary>
        public decimal DailyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 每小时预算限制（元）
        /// </summary>
        public decimal HourlyBudgetLimit { get; set; } = 0;

        /// <summary>
        /// 最低出价（元）
        /// </summary>
        public decimal MinBid { get; set; } = 0;

        /// <summary>
        /// 最高出价（元）
        /// </summary>
        public decimal MaxBid { get; set; } = 0;

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        public decimal TodaySpent { get; set; } = 0;

        /// <summary>
        /// 昨日消耗金额（元）
        /// </summary>
        public decimal YesterdaySpent { get; set; } = 0;

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        public decimal MonthlySpent { get; set; } = 0;

        /// <summary>
        /// 上月消耗金额（元）
        /// </summary>
        public decimal LastMonthSpent { get; set; } = 0;

        /// <summary>
        /// 是否自动续费
        /// </summary>
        public int AutoRenewal { get; set; } = 0;

        /// <summary>
        /// 续费金额（元）
        /// </summary>
        public decimal RenewalAmount { get; set; } = 0;

        /// <summary>
        /// 续费触发阈值（元）
        /// </summary>
        public decimal RenewalThreshold { get; set; } = 0;

        /// <summary>
        /// 是否启用预算预警
        /// </summary>
        public int BudgetAlertEnabled { get; set; } = 0;

        /// <summary>
        /// 预算预警阈值（百分比）
        /// </summary>
        public int BudgetAlertThreshold { get; set; } = 80;

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 广告计费状态DTO
    /// </summary>
    public class TaktAdvertBillingStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 计费状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 广告计费审核DTO
    /// </summary>
    public class TaktAdvertBillingAuditDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingAuditDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? AuditComments { get; set; }
    }

    /// <summary>
    /// 广告计费开始DTO
    /// </summary>
    public class TaktAdvertBillingStartDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingStartDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 计费开始时间
        /// </summary>
        public DateTime? BillingStartTime { get; set; }
    }

    /// <summary>
    /// 广告计费结束DTO
    /// </summary>
    public class TaktAdvertBillingEndDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingEndDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 计费结束时间
        /// </summary>
        public DateTime? BillingEndTime { get; set; }
    }

    /// <summary>
    /// 广告计费统计DTO
    /// </summary>
    public class TaktAdvertBillingStatsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingStatsDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long BillingId { get; set; }

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ImpressionCount { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 行动次数
        /// </summary>
        public int ActionCount { get; set; }

        /// <summary>
        /// 千次展示数
        /// </summary>
        public int CpmCount { get; set; }

        /// <summary>
        /// 今日消耗金额（元）
        /// </summary>
        public decimal TodaySpent { get; set; }

        /// <summary>
        /// 本月消耗金额（元）
        /// </summary>
        public decimal MonthlySpent { get; set; }
    }

    /// <summary>
    /// 广告计费统计汇总DTO
    /// </summary>
    public class TaktAdvertBillingStatisticsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingStatisticsDto()
        {
        }

        /// <summary>
        /// 总计费数量
        /// </summary>
        public int TotalBillingCount { get; set; }

        /// <summary>
        /// 计费中数量
        /// </summary>
        public int ActiveBillingCount { get; set; }

        /// <summary>
        /// 已暂停数量
        /// </summary>
        public int PausedBillingCount { get; set; }

        /// <summary>
        /// 已结束数量
        /// </summary>
        public int EndedBillingCount { get; set; }

        /// <summary>
        /// 已取消数量
        /// </summary>
        public int CancelledBillingCount { get; set; }

        /// <summary>
        /// 待审核数量
        /// </summary>
        public int PendingAuditCount { get; set; }

        /// <summary>
        /// 审核通过数量
        /// </summary>
        public int AuditPassedCount { get; set; }

        /// <summary>
        /// 审核拒绝数量
        /// </summary>
        public int AuditRejectedCount { get; set; }

        /// <summary>
        /// 自动续费数量
        /// </summary>
        public int AutoRenewalCount { get; set; }

        /// <summary>
        /// 启用预算预警数量
        /// </summary>
        public int BudgetAlertEnabledCount { get; set; }

        /// <summary>
        /// 总预算金额（元）
        /// </summary>
        public decimal TotalBudgetAmount { get; set; }

        /// <summary>
        /// 总已消耗金额（元）
        /// </summary>
        public decimal TotalSpentAmount { get; set; }

        /// <summary>
        /// 总剩余金额（元）
        /// </summary>
        public decimal TotalRemainingAmount { get; set; }

        /// <summary>
        /// 总展示次数
        /// </summary>
        public int TotalImpressionCount { get; set; }

        /// <summary>
        /// 总点击次数
        /// </summary>
        public int TotalClickCount { get; set; }

        /// <summary>
        /// 总行动次数
        /// </summary>
        public int TotalActionCount { get; set; }

        /// <summary>
        /// 总千次展示数
        /// </summary>
        public int TotalCpmCount { get; set; }

        /// <summary>
        /// 今日总消耗金额（元）
        /// </summary>
        public decimal TotalTodaySpent { get; set; }

        /// <summary>
        /// 本月总消耗金额（元）
        /// </summary>
        public decimal TotalMonthlySpent { get; set; }
    }
}
