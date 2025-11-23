//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRoutineCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2025-01-11
// 版本号 : V0.0.1
// 描述    : 常规业务服务集合扩展
//===================================================================


using Takt.Application.Services.Routine.Advertisement;
using Takt.Application.Services.Routine.Book;
using Takt.Application.Services.Routine.DataDictionary;
using Takt.Application.Services.Routine.Document;
using Takt.Application.Services.Routine.Document.Regulations;
using Takt.Application.Services.Routine.I18n;
using Takt.Application.Services.Routine.Iso;
using Takt.Application.Services.Routine.Numbering;
using Takt.Application.Services.Routine.Settings;
using Takt.Shared.Options;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 常规业务服务集合扩展
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册常规业务相关的所有服务，包括：
    /// 1. 核心服务 - 系统配置、字典、语言、翻译等
    /// 2. 任务服务 - 定时任务和日志
    /// 3. 邮件服务 - 邮件发送和模板
    /// 4. 文件服务 - 文件上传和管理
    /// 5. 通知服务 - 系统通知管理
    /// 6. 合同服务 - 合同管理
    /// 7. 会议服务 - 会议管理
    /// 8. 项目服务 - 项目管理
    /// 9. 日程服务 - 日程管理
    /// 10. 用车服务 - 用车管理
    /// 11. 新闻服务 - 新闻管理
    /// 12. 广告服务 - 广告管理
    /// </remarks>
    public static class TaktRoutineCollectionExtensions
    {
        /// <summary>
        /// 添加常规业务服务
        /// </summary>
        /// <remarks>
        /// 注册常规业务相关的所有服务，包括：
        /// 1. 核心服务 - 系统配置、字典、语言、翻译等
        /// 2. 任务服务 - 定时任务和日志
        /// 3. 邮件服务 - 邮件发送和模板
        /// 4. 文件服务 - 文件上传和管理
        /// 5. 通知服务 - 系统通知管理
        /// 6. 合同服务 - 合同管理
        /// 7. 会议服务 - 会议管理
        /// 8. 项目服务 - 项目管理
        /// 9. 日程服务 - 日程管理
        /// 10. 用车服务 - 用车管理
        /// 11. 新闻服务 - 新闻管理
        /// 12. 广告服务 - 广告管理
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddRoutineServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 核心服务
            services.AddScoped<ITaktGeneralSettingsService, TaktGeneralSettingsService>();              // 系统配置
            services.AddScoped<ITaktDictDataService, TaktDictDataService>();          // 字典数据服务
            services.AddScoped<ITaktDictTypeService, TaktDictTypeService>();          // 字典类型服务
            services.AddScoped<ITaktLanguageService, TaktLanguageService>();          // 语言服务
            services.AddScoped<ITaktTranslationService, TaktTranslationService>();    // 翻译服务
            services.AddScoped<ITaktNumberingRulesService, TaktNumberingRulesService>();      // 单号规则服务
            services.AddScoped<ITaktNumberGeneratorService, TaktNumberGeneratorService>(); // 单号生成服务

            // 任务相关服务
            services.AddScoped<ITaktQuartzService, TaktQuartzService>();              // 定时任务服务

            // 邮件相关服务
            services.AddScoped<ITaktMailService, TaktMailService>();                  // 邮件服务
            services.AddScoped<ITaktMailTplService, TaktMailTplService>();            // 邮件模板服务

            // 文件管理服务
            services.AddScoped<ITaktFileService, TaktFileService>();                  // 文件服务
            services.AddScoped<ITaktIsoDocumentService, TaktIsoDocumentService>();    // ISO文档服务
            services.AddScoped<ITaktIsoDocumentAccessService, TaktIsoDocumentAccessService>();        // ISO文档查阅服务
            services.AddScoped<ITaktIsoDocumentApprovalService, TaktIsoDocumentApprovalService>();    // ISO文档审批服务
            services.AddScoped<ITaktIsoDocumentDistributionService, TaktIsoDocumentDistributionService>(); // ISO文档分发服务
            services.AddScoped<ITaktIsoDocumentObsoleteService, TaktIsoDocumentObsoleteService>();    // ISO文档作废服务
            services.AddScoped<ITaktIsoDocumentRevisionService, TaktIsoDocumentRevisionService>();    // ISO文档修订服务
            services.AddScoped<ITaktIsoExternalFileService, TaktIsoExternalFileService>();            // ISO外来文件服务
            services.AddScoped<ITaktRegulationService, TaktRegulationService>();      // 法规服务
            services.AddScoped<ITaktLawService, TaktLawService>();                    // 法律文件服务
            services.AddScoped<ITaktOfficialDocumentService, TaktOfficialDocumentService>(); // 公文服务

            // 通知服务
            services.AddScoped<ITaktNoticeService, TaktNoticeService>();              // 通知服务

            // 合同管理服务
            services.AddScoped<ITaktContractService, TaktContractService>();          // 合同服务

            // 会议管理服务
            services.AddScoped<ITaktMeetingService, TaktMeetingService>();            // 会议服务

            // 项目管理服务
            services.AddScoped<ITaktProjectService, TaktProjectService>();            // 项目服务

            // 日程管理服务
            services.AddScoped<ITaktScheduleService, TaktScheduleService>();          // 日程服务
            services.AddScoped<ITaktTeamScheduleService, TaktTeamScheduleService>();  // 团队日程服务

            // 用车管理服务
            services.AddScoped<ITaktVehicleService, TaktVehicleService>();            // 用车服务

            // 新闻相关服务
            services.AddScoped<ITaktNewsService, TaktNewsService>();                  // 新闻服务
            services.AddScoped<ITaktNewsTopicService, TaktNewsTopicService>();        // 新闻话题服务
            services.AddScoped<ITaktNewsTopicRelationService, TaktNewsTopicRelationService>(); // 新闻话题关系服务
            services.AddScoped<ITaktNewsCommentService, TaktNewsCommentService>();     // 新闻评论服务
            services.AddScoped<ITaktNewsLikeService, TaktNewsLikeService>();          // 新闻点赞服务
            services.AddScoped<ITaktNewsTopicParticipantService, TaktNewsTopicParticipantService>(); // 新闻话题参与者服务

            // 图书管理服务
            services.AddScoped<ITaktBookService, TaktBookService>();                  // 图书服务

            // 广告管理服务
            services.AddScoped<ITaktAdvertService, TaktAdvertService>();              // 广告服务
            services.AddScoped<ITaktAdvertBillingService, TaktAdvertBillingService>(); // 广告计费服务

            // 配置选项注册
            services.Configure<TaktCommentOptions>(configuration.GetSection("Comment"));

            return services;
        }
    }
}



