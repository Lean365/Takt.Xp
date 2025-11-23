using Takt.Infrastructure.Data.Seeds;
using Takt.Infrastructure.Data.Seeds.Auth;
using Takt.Infrastructure.Data.Seeds.Biz;
using Takt.Infrastructure.Data.Seeds.Biz.Dict;
using Takt.Infrastructure.Data.Seeds.Biz.Logistics;
using Takt.Infrastructure.Data.Seeds.Biz.Accounting;
using Takt.Infrastructure.Data.Seeds.Biz.Hrm;
using Takt.Infrastructure.Data.Seeds.Biz.Translation;
using Takt.Infrastructure.Data.Seeds.Generator;
using Takt.Infrastructure.Data.Seeds.Workflow;
using Takt.Infrastructure.Data.Seeds.Biz.Routine;

namespace Takt.Infrastructure.Extensions;

/// <summary>
/// 种子数据服务集合扩展
/// </summary>
public static class TaktSeedsCollectionExtensions
{
    /// <summary>
    /// 添加种子数据服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddTaktSeeds(this IServiceCollection services)
    {
        // 注册协调器服务
        services.AddScoped<TaktDbSeedWorkflowCoordinator>();
        services.AddScoped<TaktDbSeedTranslationCoordinator>();
        services.AddScoped<TaktDbSeedDictCoordinator>();
        services.AddScoped<TaktDbSeedLogisticsCoordinator>();
        services.AddScoped<TaktDbSeedAccountingCoordinator>();
        services.AddScoped<TaktDbSeedHrmCoordinator>();
        services.AddScoped<TaktDbSeedIdentityCoordinator>();
        
        // 注册缺失的协调器服务
        services.AddScoped<TaktDbSeedMenuCoordinator>();
        services.AddScoped<TaktDbSeedGeneratorCoordinator>();
        services.AddScoped<TaktDbSeedRoutineCoordinator>();
        services.AddScoped<TaktDbSeedDeptCoordinator>();
        services.AddScoped<TaktDbSeedRelationCoordinator>();

        // 注册多库种子数据服务
        services.AddScoped<TaktIdentityDBSeed>();
        services.AddScoped<TaktGeneratorDbSeed>();
        services.AddScoped<TaktWorkflowDbSeed>();
        services.AddScoped<TaktBusinessDbSeed>();

        return services;
    }
}
