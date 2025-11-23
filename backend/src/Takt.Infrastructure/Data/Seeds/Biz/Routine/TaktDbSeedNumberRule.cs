//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedNumberRule.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 编号规则种子数据提供类
//===================================================================
using Takt.Domain.Entities.Routine.Numbering;
namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 编号规则种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供公告、通知、规章制度等业务的编号规则配置
/// 2. 包含多种常用的编号格式模板
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedNumberRule
{
    /// <summary>
    /// 获取默认编号规则数据
    /// </summary>
    /// <returns>编号规则数据列表</returns>
    public List<TaktNumberingRules> GetDefaultNumberRules()
    {
        return new List<TaktNumberingRules>
        {
            // 公告编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "ADM", // 行政部
                NumberRuleCode = "NOTICE",
                NumberRuleShortCode = "NOTI",
                NumberRuleName = "公告编号规则",
                NumberRuleType = 1, // 1=日常事务
                NumberRuleDescription = "用于生成系统公告的编号，格式：DTA-部门-NOTICE-年份第XXX号",
                NumberPrefix = "第",
                NumberSuffix = "号",
                RevisionNumber = 1,
                DateFormat = "yyyy",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 2, // 2=按年重置
                NumberFormatTemplate = "{COMPANY}-{DEPARTMENT}-{RULE_SHORT}-{DATE}-{PREFIX}{SEQUENCE}{SUFFIX}",
                NumberExample = "DTA-ADM-NOTI-2024-第001号",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 0,
                IncludeDay = 0,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 1,
                Status = 0, // 0=正常

            },

            // 通知编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "ADM", // 行政部
                NumberRuleCode = "NOTIFICATION",
                NumberRuleShortCode = "NOTF",
                NumberRuleName = "通知编号规则",
                NumberRuleType = 1, // 1=日常事务
                NumberRuleDescription = "用于生成系统通知的编号，格式：DTA-部门-NOTIFICATION-年份第XXX号",
                NumberPrefix = "第",
                NumberSuffix = "号",
                RevisionNumber = 1,
                DateFormat = "yyyy",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 2, // 2=按年重置
                NumberFormatTemplate = "{COMPANY}-{DEPARTMENT}-{RULE_SHORT}-{DATE}-{PREFIX}{SEQUENCE}{SUFFIX}",
                NumberExample = "DTA-ADM-NOTF-2024-第001号",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 0,
                IncludeDay = 0,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 2,
                Status = 0, // 0=正常

            },

            // 规章制度编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "ADM", // 行政部
                NumberRuleCode = "REGULATION",
                NumberRuleShortCode = "REGS",
                NumberRuleName = "规章制度编号规则",
                NumberRuleType = 1, // 1=日常事务
                NumberRuleDescription = "用于生成规章制度的编号，格式：DTA-部门-REGULATION-年份第XXX号",
                NumberPrefix = "第",
                NumberSuffix = "号",
                RevisionNumber = 1,
                DateFormat = "yyyy",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 2, // 2=按年重置
                NumberFormatTemplate = "{COMPANY}-{DEPARTMENT}-{RULE_SHORT}-{DATE}-{PREFIX}{SEQUENCE}{SUFFIX}",
                NumberExample = "DTA-ADM-REGS-2024-第001号",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 0,
                IncludeDay = 0,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 3,
                Status = 0, // 0=正常

            },

            // 会议纪要编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "ADM", // 行政部
                NumberRuleCode = "MEETING_MINUTES",
                NumberRuleShortCode = "MEET",
                NumberRuleName = "会议纪要编号规则",
                NumberRuleType = 1, // 1=日常事务
                NumberRuleDescription = "用于生成会议纪要的编号，格式：DTA-部门-MEETING-年份第XXX号",
                NumberPrefix = "第",
                NumberSuffix = "号",
                RevisionNumber = 1,
                DateFormat = "yyyy",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 2, // 2=按年重置
                NumberFormatTemplate = "{COMPANY}-{DEPARTMENT}-{RULE_SHORT}-{DATE}-{PREFIX}{SEQUENCE}{SUFFIX}",
                NumberExample = "DTA-ADM-MEET-2024-第001号",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 0,
                IncludeDay = 0,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 4,
                Status = 0 // 0=正常
            },

            // 文件编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "ADM", // 行政部
                NumberRuleCode = "DOCUMENT",
                NumberRuleShortCode = "DOCU",
                NumberRuleName = "文件编号规则",
                NumberRuleType = 1, // 1=日常事务
                NumberRuleDescription = "用于生成各类文件的编号，格式：[公司代码]-[体系代码]-[文件层级代码]-[部门代码]-[主题序列号]-[版本号/修订号]。体系代码：Q-质量管理体系(ISO 9001)，E-环境管理体系(ISO 14001)，S-职业健康安全管理体系(ISO 45001)，I-信息安全管理体系(ISO 27001)等",
                NumberPrefix = "",
                NumberSuffix = "",
                RevisionNumber = 1,
                DateFormat = "yyyy",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 1, // 1=不重置
                NumberFormatTemplate = "{COMPANY}-{SYSTEM}-{LEVEL}-{DEPARTMENT}-{RULE_SHORT}-{SEQUENCE}-{REVISION}:{DATE}",
                NumberExample = "DTA-Q-03-ADM-DOCU-001-1:2023",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 1,
                IncludeYear = 1,
                IncludeMonth = 0,
                IncludeDay = 0,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 5,
                Status = 0 // 0=正常
            },

            // 工作流编号规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "IT", // 信息技术部
                NumberRuleCode = "WORKFLOW",
                NumberRuleShortCode = "WORK",
                NumberRuleName = "工作流编号规则",
                NumberRuleType = 6, // 6=工作流
                NumberRuleDescription = "用于生成工作流实例的编号，格式：WF+年月日+序号",
                NumberPrefix = "WF",
                NumberSuffix = "",
                RevisionNumber = 1,
                DateFormat = "yyyyMMdd",
                SequenceLength = 4,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 4, // 4=按日重置
                NumberFormatTemplate = "{PREFIX}{DATE}{SEQUENCE}",
                NumberExample = "WF20241201001",
                IncludeCompanyCode = 0,
                IncludeDepartmentCode = 0,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 1,
                IncludeDay = 1,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 6,
                Status = 0 // 0=正常
            },

            // 请假编码规则
            new TaktNumberingRules
            {
                CompanyCode = "DTA",
                DeptCode = "HR", // 人力资源部
                NumberRuleCode = "LEAVE_APPLICATION",
                NumberRuleShortCode = "LEAV",
                NumberRuleName = "请假编码规则",
                NumberRuleType = 2, // 2=人力资源
                NumberRuleDescription = "用于生成请假申请的编号，格式：DTA-HR-LEAVE-年月日+序号",
                NumberPrefix = "LEAVE",
                NumberSuffix = "",
                RevisionNumber = 1,
                DateFormat = "yyyyMMdd",
                SequenceLength = 3,
                SequenceStart = 1,
                SequenceStep = 1,
                CurrentSequence = 0,
                SequenceResetRule = 4, // 4=按日重置
                NumberFormatTemplate = "{COMPANY}-{DEPARTMENT}-{PREFIX}-{DATE}{SEQUENCE}",
                NumberExample = "DTA-HR-LEAVE-20241201001",
                IncludeCompanyCode = 1,
                IncludeDepartmentCode = 1,
                IncludeRevisionNumber = 0,
                IncludeYear = 1,
                IncludeMonth = 1,
                IncludeDay = 1,
                IncludeHour = 0,
                IncludeMinute = 0,
                IncludeSecond = 0,
                IncludeMillisecond = 0,
                IncludeRandom = 0,
                RandomLength = 2,
                IncludeCheckDigit = 0,
                CheckDigitAlgorithm = 1,
                AllowDuplicate = 0,
                DuplicateCheckScope = 1,
                OrderNum = 7,
                Status = 0 // 0=正常
            }
        };
    }
}


