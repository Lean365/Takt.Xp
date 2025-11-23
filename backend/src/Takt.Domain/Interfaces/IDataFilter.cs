
//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 数据过滤
// 命名空间 : Takt.Domain.Interfaces
// 文件名 : IDataFilter.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 数据过滤器接口
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System;
using System.Linq.Expressions;

namespace Takt.Domain.Interfaces
{
  /// <summary>
  /// 数据过滤器接口
  /// </summary>
  /// <typeparam name="TEntity">实体类型</typeparam>
  public interface IDataFilter<TEntity> where TEntity : class
  {
    /// <summary>
    /// 获取过滤器是否启用
    /// </summary>
    bool IsEnabled { get; }

    /// <summary>
    /// 获取过滤表达式
    /// </summary>
    /// <returns>过滤表达式</returns>
    Expression<Func<TEntity, bool>> GetFilter();
  }
}
