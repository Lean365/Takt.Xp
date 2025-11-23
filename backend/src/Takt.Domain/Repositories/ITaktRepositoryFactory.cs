//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 仓储
// 命名空间 : Takt.Domain.Repositories
// 文件名 : ITaktRepositoryFactory.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 仓储工厂接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.Repositories;

/// <summary>
/// 仓储工厂接口
/// 支持多库模式，为不同数据库提供专门的仓储获取方法
/// </summary>
public interface ITaktRepositoryFactory
{
  /// <summary>
  /// 获取认证数据库仓储
  /// </summary>
  /// <typeparam name="TEntity">实体类型</typeparam>
  /// <returns>认证数据库仓储</returns>
  ITaktRepository<TEntity> GetAuthRepository<TEntity>() where TEntity : class, new();

  /// <summary>
  /// 获取业务数据库仓储
  /// </summary>
  /// <typeparam name="TEntity">实体类型</typeparam>
  /// <returns>业务数据库仓储</returns>
  ITaktRepository<TEntity> GetBusinessRepository<TEntity>() where TEntity : class, new();

  /// <summary>
  /// 获取工作流数据库仓储
  /// </summary>
  /// <typeparam name="TEntity">实体类型</typeparam>
  /// <returns>工作流数据库仓储</returns>
  ITaktRepository<TEntity> GetWorkflowRepository<TEntity>() where TEntity : class, new();

  /// <summary>
  /// 获取代码生成数据库仓储
  /// </summary>
  /// <typeparam name="TEntity">实体类型</typeparam>
  /// <returns>代码生成数据库仓储</returns>
  ITaktRepository<TEntity> GetGeneratorRepository<TEntity>() where TEntity : class, new();
}



