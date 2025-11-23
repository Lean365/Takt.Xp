# 仓储层规范

## 1. 命名规范

- 仓储接口必须以`ITakt`开头,以`Repository`结尾
- 仓储实现类必须以`Takt`开头,以`Repository`结尾
- 必须放在正确的命名空间下:
  - 接口: `Takt.Domain.Repositories`
- 实现: `Takt.Infrastructure.Repositories`

## 2. 通用仓储接口

```csharp
//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : ITaktRepository.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:50
// 版本号 : V.0.0.1
// 描述    : 通用仓储接口
//===================================================================

namespace Takt.Domain.Repositories
{
    /// <summary>
    /// 通用仓储接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public interface ITaktRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        ISqlSugarClient SqlSugarClient { get; }

        /// <summary>
        /// 获取SimpleClient对象
        /// </summary>
        SimpleClient<TEntity> SimpleClient { get; }

        #region 查询操作

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>实体</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync();

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体列表</returns>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果</returns>
        Task<(List<TEntity> list, long total)> GetPagedListAsync(int pageIndex, int pageSize);

        /// <summary>
        /// 条件分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <returns>分页结果</returns>
        Task<(List<TEntity> list, long total)> GetPagedListAsync(Expression<Func<TEntity, bool>> condition, int pageIndex, int pageSize);

        #endregion

        #region 新增操作

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> InsertRangeAsync(List<TEntity> entities);

        #endregion

        #region 更新操作

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> UpdateRangeAsync(List<TEntity> entities);

        #endregion

        #region 删除操作

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteAsync(object id);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteAsync(TEntity entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键列表</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteRangeAsync(List<object> ids);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>影响行数</returns>
        Task<int> DeleteRangeAsync(List<TEntity> entities);

        #endregion
    }
}
```

## 3. 通用仓储实现

```csharp
//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktRepository.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:50
// 版本号 : V.0.0.1
// 描述    : 通用仓储实现
//===================================================================

namespace Takt.Infrastructure.Repositories
{
    /// <summary>
    /// 通用仓储实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktRepository<TEntity> : ITaktRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// SqlSugar客户端
        /// </summary>
        private readonly ISqlSugarClient _db;

        /// <summary>
        /// SimpleClient对象
        /// </summary>
        private readonly SimpleClient<TEntity> _simpleClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">SqlSugar客户端</param>
        public TaktRepository(ISqlSugarClient db)
        {
            _db = db;
            _simpleClient = new SimpleClient<TEntity>(db);
        }

        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        public ISqlSugarClient SqlSugarClient => _db;

        /// <summary>
        /// 获取SimpleClient对象
        /// </summary>
        public SimpleClient<TEntity> SimpleClient => _simpleClient;

        #region 查询操作

        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await _simpleClient.GetByIdAsync(id);
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetListAsync()
        {
            return await _simpleClient.GetListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await _simpleClient.GetListAsync(condition);
        }

        /// <inheritdoc/>
        public virtual async Task<(List<TEntity> list, long total)> GetPagedListAsync(int pageIndex, int pageSize)
        {
            RefAsync<int> totalCount = 0;
            var list = await _simpleClient.AsQueryable()
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return (list, totalCount);
        }

        /// <inheritdoc/>
        public virtual async Task<(List<TEntity> list, long total)> GetPagedListAsync(Expression<Func<TEntity, bool>> condition, int pageIndex, int pageSize)
        {
            RefAsync<int> totalCount = 0;
            var list = await _simpleClient.AsQueryable()
                .Where(condition)
                .ToPageListAsync(pageIndex, pageSize, totalCount);
            return (list, totalCount);
        }

        #endregion

        #region 新增操作

        /// <inheritdoc/>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            return await _simpleClient.InsertAsync(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<int> InsertRangeAsync(List<TEntity> entities)
        {
            return await _simpleClient.InsertRangeAsync(entities);
        }

        #endregion

        #region 更新操作

        /// <inheritdoc/>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            return await _simpleClient.UpdateAsync(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            return await _simpleClient.UpdateRangeAsync(entities);
        }

        #endregion

        #region 删除操作

        /// <inheritdoc/>
        public virtual async Task<int> DeleteAsync(object id)
        {
            return await _simpleClient.DeleteByIdAsync(id);
        }

        /// <inheritdoc/>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            return await _simpleClient.DeleteAsync(entity);
        }

        /// <inheritdoc/>
        public virtual async Task<int> DeleteRangeAsync(List<object> ids)
        {
            return await _simpleClient.DeleteByIdsAsync(ids);
        }

        /// <inheritdoc/>
        public virtual async Task<int> DeleteRangeAsync(List<TEntity> entities)
        {
            return await _simpleClient.DeleteAsync(entities);
        }

        #endregion
    }
}
```

## 4. 使用说明

1. 基本用法
   - 直接使用通用仓储接口和实现类
   - 支持基础的CRUD操作
   - 支持分页查询
   - 支持条件查询
   - 支持批量操作
   - **重要**: 不需要为每个实体单独实现仓储类,直接使用通用仓储即可

2. 扩展用法
   - 可以使用`SqlSugarClient`进行复杂查询
   - 可以使用`SimpleClient`进行简单操作
   - 对于特殊查询需求,在应用服务层通过注入`ITaktRepository<T>`来实现
   - 禁止创建单独的仓储实现类

3. 注意事项
   - 所有方法都是异步的,返回Task
   - 查询方法支持表达式目录树作为条件
   - 批量操作建议使用Range方法
   - 分页查询会返回总记录数
   - 可以覆写虚方法实现特定逻辑
   - 通用仓储已经满足99%的数据访问需求
   - 特殊查询应在应用服务层实现,而不是创建新的仓储类

4. 依赖注入
   ```csharp
   // 在Startup.cs中只需注册一次通用仓储
   services.AddScoped(typeof(ITaktRepository<>), typeof(TaktRepository<>));
   ```

5. 使用示例
   ```csharp
   // 在应用服务中注入并使用
   public class TaktUserService
   {
       private readonly ITaktRepository<TaktUser> _userRepository;

       public TaktUserService(ITaktRepository<TaktUser> userRepository)
       {
           _userRepository = userRepository;
       }

       // 使用通用仓储方法
       public async Task<TaktUser> GetUserAsync(long id)
       {
           return await _userRepository.GetByIdAsync(id);
       }

       // 特殊查询直接使用SqlSugarClient
       public async Task<List<TaktUser>> GetActiveUsersAsync()
       {
           return await _userRepository.SqlSugarClient.Queryable<TaktUser>()
               .Where(u => u.Status == CommonStatus.Enable)
               .ToListAsync();
       }
   }
   ``` 



