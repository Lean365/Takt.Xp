#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNumberingRules.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 规则实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Numbering
{
    /// <summary>
    /// 规则实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录各种业务单据的编号规则配置，包括规则名称、格式设置、序列号管理等
    /// </remarks>
    [SugarTable("Takt_routine_number_rule", "单号规则")]
    [SugarIndex("ix_number_rule_code", nameof(NumberRuleCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_number_rule", nameof(CompanyCode), OrderByType.Asc, nameof(NumberRuleCode), OrderByType.Asc, false)]
    [SugarIndex("ix_number_rule_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_number_rule_type", nameof(NumberRuleType), OrderByType.Asc, false)]
    public class TaktNumberingRules : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>部门代码</summary>
        [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeptCode { get; set; }

        /// <summary>规则编号</summary>
        [SugarColumn(ColumnName = "number_rule_code", ColumnDescription = "规则编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NumberRuleCode { get; set; } = string.Empty;

        /// <summary>规则编号简称</summary>
        [SugarColumn(ColumnName = "number_rule_short_code", ColumnDescription = "规则编号简称", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NumberRuleShortCode { get; set; } = string.Empty;

        /// <summary>规则名称</summary>
        [SugarColumn(ColumnName = "number_rule_name", ColumnDescription = "规则名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NumberRuleName { get; set; } = string.Empty;

        /// <summary>单据类型(1=日常事务 2=人力资源 3=财务核算 4=后勤管理 5=低代码 6=工作流 7=其他)</summary>
        [SugarColumn(ColumnName = "number_rule_type", ColumnDescription = "单据类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int NumberRuleType { get; set; } = 1;

        /// <summary>规则描述</summary>
        [SugarColumn(ColumnName = "number_rule_description", ColumnDescription = "规则描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NumberRuleDescription { get; set; }

        /// <summary>前缀(用于编号格式中的前缀部分，如"第"字)</summary>
        [SugarColumn(ColumnName = "number_prefix", ColumnDescription = "前缀", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NumberPrefix { get; set; }

        /// <summary>后缀（用于编号格式中的后缀部分，如"号"字）</summary>
        [SugarColumn(ColumnName = "number_suffix", ColumnDescription = "后缀", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NumberSuffix { get; set; }

        /// <summary>修订号(用于标识文档的修订版本，自然数序号，如1、2、3等)</summary>
        [SugarColumn(ColumnName = "revision_number", ColumnDescription = "修订号", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int RevisionNumber { get; set; } = 1;

        /// <summary>日期格式</summary>
        [SugarColumn(ColumnName = "date_format", ColumnDescription = "日期格式", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "yyyyMMdd")]
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>序列号长度</summary>
        [SugarColumn(ColumnName = "sequence_length", ColumnDescription = "序列号长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "4")]
        public int SequenceLength { get; set; } = 4;

        /// <summary>序列号起始值</summary>
        [SugarColumn(ColumnName = "sequence_start", ColumnDescription = "序列号起始值", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SequenceStart { get; set; } = 1;

        /// <summary>序列号步长</summary>
        [SugarColumn(ColumnName = "sequence_step", ColumnDescription = "序列号步长", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SequenceStep { get; set; } = 1;

        /// <summary>当前序列号</summary>
        [SugarColumn(ColumnName = "current_sequence", ColumnDescription = "当前序列号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CurrentSequence { get; set; } = 0;

        /// <summary>序列号重置规则(1=不重置 2=按年重置 3=按月重置 4=按日重置)</summary>
        [SugarColumn(ColumnName = "sequence_reset_rule", ColumnDescription = "序列号重置规则", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SequenceResetRule { get; set; } = 1;

        /// <summary>最后重置日期</summary>
        [SugarColumn(ColumnName = "last_reset_date", ColumnDescription = "最后重置日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastResetDate { get; set; }

        /// <summary>格式模板</summary>
        [SugarColumn(ColumnName = "number_format_template", ColumnDescription = "格式模板", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}")]
        public string NumberFormatTemplate { get; set; } = "{PREFIX}{DATE}{SEQUENCE}{SUFFIX}";

        /// <summary>示例</summary>
        [SugarColumn(ColumnName = "number_example", ColumnDescription = "示例", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NumberExample { get; set; }

        /// <summary>是否包含公司代码(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_company_code", ColumnDescription = "是否包含公司代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeCompanyCode { get; set; } = 0;

        /// <summary>是否包含部门代码(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_department_code", ColumnDescription = "是否包含部门代码", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeDepartmentCode { get; set; } = 0;

        /// <summary>是否包含修订号(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_revision_number", ColumnDescription = "是否包含修订号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeRevisionNumber { get; set; } = 0;

        /// <summary>是否包含年份(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_year", ColumnDescription = "是否包含年份", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IncludeYear { get; set; } = 1;

        /// <summary>是否包含月份(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_month", ColumnDescription = "是否包含月份", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IncludeMonth { get; set; } = 1;

        /// <summary>是否包含日期(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_day", ColumnDescription = "是否包含日期", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IncludeDay { get; set; } = 1;

        /// <summary>是否包含小时(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_hour", ColumnDescription = "是否包含小时", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeHour { get; set; } = 0;

        /// <summary>是否包含分钟(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_minute", ColumnDescription = "是否包含分钟", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeMinute { get; set; } = 0;

        /// <summary>是否包含秒(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_second", ColumnDescription = "是否包含秒", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeSecond { get; set; } = 0;

        /// <summary>是否包含毫秒(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_millisecond", ColumnDescription = "是否包含毫秒", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeMillisecond { get; set; } = 0;

        /// <summary>是否包含随机数(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_random", ColumnDescription = "是否包含随机数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeRandom { get; set; } = 0;

        /// <summary>随机数长度</summary>
        [SugarColumn(ColumnName = "random_length", ColumnDescription = "随机数长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int RandomLength { get; set; } = 2;

        /// <summary>是否包含校验位(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "include_check_digit", ColumnDescription = "是否包含校验位", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IncludeCheckDigit { get; set; } = 0;

        /// <summary>校验位算法(1=简单求和 2=加权求和 3=模运算)</summary>
        [SugarColumn(ColumnName = "check_digit_algorithm", ColumnDescription = "校验位算法", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CheckDigitAlgorithm { get; set; } = 1;

        /// <summary>是否允许重复(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "allow_duplicate", ColumnDescription = "是否允许重复", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AllowDuplicate { get; set; } = 0;

        /// <summary>重复检查范围(1=全局 2=按年 3=按月 4=按日)</summary>
        [SugarColumn(ColumnName = "duplicate_check_scope", ColumnDescription = "重复检查范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DuplicateCheckScope { get; set; } = 1;

        /// <summary>最后使用时间</summary>
        [SugarColumn(ColumnName = "last_used_time", ColumnDescription = "最后使用时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastUsedTime { get; set; }

        /// <summary>使用次数</summary>
        [SugarColumn(ColumnName = "usage_count", ColumnDescription = "使用次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UsageCount { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>规则状态(0=正常 1=停用 2=维护中)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
}



