//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRepositoryFactory.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 仓储工厂实现类 - 支持多库模式
//===================================================================

using Takt.Shared.Options;
using Takt.Infrastructure.Data.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Repositories;

/// <summary>
/// 仓储工厂实现类
/// 支持多库模式，为不同数据库提供专门的仓储获取方法
/// </summary>
public class TaktRepositoryFactory : ITaktRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider">服务提供器</param>
    public TaktRepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 获取认证数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>认证数据库仓储</returns>
    public ITaktRepository<TEntity> GetAuthRepository<TEntity>() where TEntity : class, new()
    {
        var identityDbContext = _serviceProvider.GetRequiredService<TaktIdentityDBContext>();
        var currentUser = _serviceProvider.GetRequiredService<ITaktCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<ITaktLogger>();
        var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var dbOptions = _serviceProvider.GetRequiredService<IOptions<TaktDbOptions>>();

        return new TaktRepository<TEntity>(identityDbContext.Client, currentUser, logger, httpContextAccessor, dbOptions);
    }

    /// <summary>
    /// 获取业务数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>业务数据库仓储</returns>
    public ITaktRepository<TEntity> GetBusinessRepository<TEntity>() where TEntity : class, new()
    {
        var businessDbContext = _serviceProvider.GetRequiredService<TaktBusinessDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<ITaktCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<ITaktLogger>();
        var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var dbOptions = _serviceProvider.GetRequiredService<IOptions<TaktDbOptions>>();

        // 添加诊断日志：显示 SqlSugarScope 实例的哈希码
        //logger.Info($"[仓储工厂] 获取业务仓储: 实体类型={typeof(TEntity).Name}");
        //logger.Info($"[仓储工厂] 业务数据库上下文哈希码: {businessDbContext.GetHashCode()}");
        //logger.Info($"[仓储工厂] 业务数据库 SqlSugarScope 哈希码: {businessDbContext.Client.GetHashCode()}");

        return new TaktRepository<TEntity>(businessDbContext.Client, currentUser, logger, httpContextAccessor, dbOptions);
    }

    /// <summary>
    /// 获取工作流数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>工作流数据库仓储</returns>
    public ITaktRepository<TEntity> GetWorkflowRepository<TEntity>() where TEntity : class, new()
    {
        var workflowDbContext = _serviceProvider.GetRequiredService<TaktWorkflowDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<ITaktCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<ITaktLogger>();
        var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var dbOptions = _serviceProvider.GetRequiredService<IOptions<TaktDbOptions>>();

        return new TaktRepository<TEntity>(workflowDbContext.Client, currentUser, logger, httpContextAccessor, dbOptions);
    }

    /// <summary>
    /// 获取代码生成数据库仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <returns>代码生成数据库仓储</returns>
    public ITaktRepository<TEntity> GetGeneratorRepository<TEntity>() where TEntity : class, new()
    {
        var generatorDbContext = _serviceProvider.GetRequiredService<TaktGeneratorDbContext>();
        var currentUser = _serviceProvider.GetRequiredService<ITaktCurrentUser>();
        var logger = _serviceProvider.GetRequiredService<ITaktLogger>();
        var httpContextAccessor = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
        var dbOptions = _serviceProvider.GetRequiredService<IOptions<TaktDbOptions>>();

        return new TaktRepository<TEntity>(generatorDbContext.Client, currentUser, logger, httpContextAccessor, dbOptions);
    }
}




