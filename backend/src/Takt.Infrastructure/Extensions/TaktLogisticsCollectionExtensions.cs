//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogisticsCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2025-01-11
// 版本号 : V0.0.1
// 描述    : 物流服务集合扩展
//===================================================================

using Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy;
using Takt.Application.Services.Logistics.Manufacturing.Master;
using Takt.Application.Services.Logistics.Material.Master;
using Microsoft.Extensions.DependencyInjection;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 物流服务集合扩展
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册物流相关的所有服务，包括：
    /// 1. 制造服务 - 生产制造相关
    /// 2. 装配服务 - 装配生产相关
    /// 3. 输出服务 - 生产输出相关
    /// 4. 主数据服务 - 生产主数据管理
    /// </remarks>
    public static class TaktLogisticsCollectionExtensions
    {
        /// <summary>
        /// 添加物流服务
        /// </summary>
        /// <remarks>
        /// 注册物流相关的所有服务，包括：
        /// 1. 制造服务 - 生产制造相关
        /// 2. 装配服务 - 装配生产相关
        /// 3. 输出服务 - 生产输出相关
        /// 4. 主数据服务 - 生产主数据管理
        /// 5. 物料主数据服务 - 物料主数据管理
        /// 6. 人员作业率服务 - 人员作业率管理
        /// 7. 作业率服务 - 作业率管理
        /// 8. 模型目标服务 - 模型目标管理
        /// 9. 设备作业率服务 - 设备作业率管理
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddLogisticsServices(this IServiceCollection services)
        {
            // 制造装配输出服务
            services.AddScoped<ITaktAssyOutputService, TaktAssyOutputService>();
            services.AddScoped<ITaktAssyOutputDetailService, TaktAssyOutputDetailService>();

            // 制造主数据服务
            services.AddScoped<ITaktProdOrderService, TaktProdOrderService>();
            services.AddScoped<ITaktStandardTimeService, TaktStandardTimeService>();
            services.AddScoped<ITaktPersonnelOperationRateService, TaktPersonnelOperationRateService>();
            services.AddScoped<ITaktOperationRateService, TaktOperationRateService>();
            services.AddScoped<ITaktModelDestinationService, TaktModelDestinationService>();
            services.AddScoped<ITaktEquipmentOperationRateService, TaktEquipmentOperationRateService>();

            // 物料主数据服务
            services.AddScoped<ITaktPlantService, TaktPlantService>();
            services.AddScoped<ITaktProdMaterialService, TaktProdMaterialService>();

            return services;
        }
    }
}


