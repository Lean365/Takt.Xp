//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAccountingCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2025-01-11
// 版本号 : V0.0.1
// 描述    : 会计服务集合扩展
//===================================================================

using Microsoft.Extensions.DependencyInjection;
using Takt.Application.Services.Accounting.Financial;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 会计服务集合扩展
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册会计相关的所有服务，包括：
    /// 1. 财务会计 - 总账、应收应付、固定资产等
    /// 2. 管理会计 - 成本中心、利润中心、成本要素等
    /// 3. 税务管理 - 税务代码、税务报表、税务支付等
    /// 4. 预算管理 - 预算编制、预算控制、预算审批等
    /// 5. 财务报表 - 资产负债表、利润表、现金流量表等
    /// </remarks>
    public static class TaktAccountingCollectionExtensions
    {
        /// <summary>
        /// 添加会计服务
        /// </summary>
        /// <remarks>
        /// 注册会计相关的所有服务，包括：
        /// 1. 财务会计服务 - 总账、应收应付、固定资产等
        /// 2. 管理会计服务 - 成本中心、利润中心、成本要素等
        /// 3. 税务管理服务 - 税务代码、税务报表、税务支付等
        /// 4. 预算管理服务 - 预算编制、预算控制、预算审批等
        /// 5. 财务报表服务 - 资产负债表、利润表、现金流量表等
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAccountingServices(this IServiceCollection services)
        {
            // 财务会计服务
            services.AddScoped<ITaktCompanyService, TaktCompanyService>();
            // services.AddScoped<ITaktGlAccountService, TaktGlAccountService>();
            // services.AddScoped<ITaktAccountsReceivableService, TaktAccountsReceivableService>();
            // services.AddScoped<ITaktAccountsPayableService, TaktAccountsPayableService>();
            // services.AddScoped<ITaktFixedAssetService, TaktFixedAssetService>();

            // 管理会计服务
            // services.AddScoped<ITaktCostCenterService, TaktCostCenterService>();
            // services.AddScoped<ITaktProfitCenterService, TaktProfitCenterService>();
            // services.AddScoped<ITaktCostElementService, TaktCostElementService>();

            // 税务管理服务
            // services.AddScoped<ITaktTaxService, TaktTaxService>();
            // services.AddScoped<ITaktTaxReportingService, TaktTaxReportingService>();

            // 预算管理服务
            // services.AddScoped<ITaktBudgetService, TaktBudgetService>();
            // services.AddScoped<ITaktBudgetControlService, TaktBudgetControlService>();

            // 财务报表服务
            // services.AddScoped<ITaktFinancialReportingService, TaktFinancialReportingService>();

            return services;
        }
    }
}


