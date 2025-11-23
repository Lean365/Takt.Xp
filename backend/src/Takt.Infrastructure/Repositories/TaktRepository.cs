//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRepository.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 14:20
// 版本号 : V0.0.1
// 描述    : SqlSugar仓储实现
//===================================================================

using Takt.Shared.Models;
using Takt.Shared.Options;
using Takt.Domain.Entities.Identity;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Takt.Infrastructure.Repositories
{
    /// <summary>
    /// SqlSugar通用仓储实现
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktRepository<TEntity> : ITaktRepository<TEntity> where TEntity : class, new()
    {
        private readonly SqlSugarScope _db;
        private readonly SimpleClient<TEntity> _entities;
        private readonly ITaktCurrentUser _currentUser;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<TaktDbOptions> _dbOptions;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 动态获取当前用户信息
        /// 优先使用HTTP上下文中的用户信息，如果不可用则使用注入的实例
        /// </summary>
        /// <param name="fallbackUserName">备用用户名，如果HTTP上下文和注入实例都不可用则使用此值</param>
        /// <returns>当前用户名</returns>
        private string GetCurrentUserName(string? fallbackUserName = null)
        {
            try
            {
                // 尝试从HTTP上下文动态获取当前用户信息
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext != null)
                {
                    var currentUser = httpContext.RequestServices?.GetService<ITaktCurrentUser>();
                    if (currentUser != null)
                    {
                        var userName = currentUser.UserName;
                        if (!string.IsNullOrEmpty(userName) && userName != "Takt365")
                        {
                            _logger.Debug($"[GetCurrentUserName] 从HTTP上下文获取到用户名: {userName}");
                            return userName;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Warn($"[GetCurrentUserName] 从HTTP上下文获取用户信息失败: {ex.Message}");
            }

            // 如果无法从HTTP上下文获取，使用注入的实例
            var injectedUserName = _currentUser.UserName ?? "Takt365";
            var finalUserName = fallbackUserName ?? injectedUserName;
            _logger.Debug($"[GetCurrentUserName] 使用注入实例的用户名: {injectedUserName}, 最终用户名: {finalUserName}");
            return finalUserName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="db">SqlSugar客户端</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="dbOptions">数据库配置选项</param>
        public TaktRepository(SqlSugarScope db, ITaktCurrentUser currentUser,
            ITaktLogger logger, IHttpContextAccessor httpContextAccessor, IOptions<TaktDbOptions> dbOptions)
        {
            _db = db;
            _entities = _db.GetSimpleClient<TEntity>();
            _currentUser = currentUser;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _dbOptions = dbOptions;

            // 添加诊断日志：显示接收到的 SqlSugarScope 实例信息
            //_logger.Info($"[仓储] 创建仓储实例: 实体类型={typeof(TEntity).Name}");
            //_logger.Info($"[仓储] 接收到的 SqlSugarScope 哈希码: {_db.GetHashCode()}");

            //_logger.Info($"[仓储] SqlSugarScope 数据库名称: {_db.Ado.Connection.Database}");
        }

        /// <summary>
        /// 获取SqlSugar客户端
        /// </summary>
        public ISqlSugarClient SqlSugarClient => _db;

        /// <summary>
        /// 获取SimpleClient对象
        /// </summary>
        public SimpleClient<TEntity> SimpleClient => _entities;

        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <returns>返回ISugarQueryable查询对象</returns>
        public ISugarQueryable<TEntity> AsQueryable()
        {
            return _db.Queryable<TEntity>();
        }

        #region 查询操作

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>返回实体对象,如果未找到返回null</returns>
        public async Task<TEntity?> GetByIdAsync(object id)
        {
            var query = _db.Queryable<TEntity>();

            // 非管理员需要过滤已删除记录
            if (_currentUser.UserType != 2 && typeof(TEntity).IsSubclassOf(typeof(TaktBaseEntity)))
            {
                query = query.Where("is_deleted = 0");
            }

            return await query.InSingleAsync(id);
        }

        /// <summary>
        /// 获取第一个符合条件的实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>返回实体对象,如果未找到返回null</returns>
        public async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> condition)
        {
            var query = _db.Queryable<TEntity>();

            // 非管理员需要过滤已删除记录
            if (_currentUser.UserType != 2 && typeof(TEntity).IsSubclassOf(typeof(TaktBaseEntity)))
            {
                query = query.Where("is_deleted = 0");
            }

            return await query.Where(condition).FirstAsync();
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>实体列表</returns>
        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? condition = null)
        {
            var query = _db.Queryable<TEntity>();

            // 非管理员需要过滤已删除记录
            if (_currentUser.UserType != 2 && typeof(TEntity).IsSubclassOf(typeof(TaktBaseEntity)))
            {
                query = query.Where("is_deleted = 0");
            }

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderByExpression">排序表达式</param>
        /// <param name="orderByType">排序类型</param>
        /// <returns>分页结果</returns>
        public async Task<TaktPagedResult<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? condition = null,
            int pageIndex = 1,
            int pageSize = 20,
            Expression<Func<TEntity, object>>? orderByExpression = null,
            OrderByType orderByType = OrderByType.Desc)
        {
            var query = _db.Queryable<TEntity>();

            // 非管理员需要过滤已删除记录
            if (_currentUser.UserType != 2 && typeof(TEntity).IsSubclassOf(typeof(TaktBaseEntity)))
            {
                query = query.Where("is_deleted = 0");
            }

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (orderByExpression != null)
            {
                query = orderByType == OrderByType.Asc
                    ? query.OrderBy(orderByExpression)
                    : query.OrderBy(orderByExpression, OrderByType.Desc);
            }

            var total = await query.CountAsync();
            var list = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new TaktPagedResult<TEntity>
            {
                Rows = list,
                TotalNum = total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// 获取分页列表(支持多个排序条件)
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="orderByExpressions">排序表达式列表</param>
        /// <returns>实体列表和总记录数</returns>
        public async Task<TaktPagedResult<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>>? condition = null,
            int pageIndex = 1,
            int pageSize = 20,
            List<(Expression<Func<TEntity, object>> Expression, OrderByType Type)>? orderByExpressions = null)
        {
            var query = _db.Queryable<TEntity>();

            // 添加查询条件
            if (condition != null)
            {
                query = query.Where(condition);
            }

            // 添加排序条件
            if (orderByExpressions != null && orderByExpressions.Count > 0)
            {
                var isFirst = true;
                foreach (var (expression, type) in orderByExpressions)
                {
                    if (isFirst)
                    {
                        query = type == OrderByType.Asc
                            ? query.OrderBy(expression)
                            : query.OrderBy(expression, OrderByType.Desc);
                        isFirst = false;
                    }
                    else
                    {
                        query = type == OrderByType.Asc
                            ? query.OrderByIF(true, expression, OrderByType.Asc)
                            : query.OrderByIF(true, expression, OrderByType.Desc);
                    }
                }
            }

            // 获取总数
            var total = await query.CountAsync();

            // 分页查询
            var list = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new TaktPagedResult<TEntity>
            {
                Rows = list,
                TotalNum = total,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        /// <summary>
        /// 获取符合条件的实体数量（异步）
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>数量</returns>
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? condition = null)
        {
            var query = _db.Queryable<TEntity>();

            // 非管理员需要过滤已删除记录
            if (_currentUser.UserType != 2 && typeof(TEntity).IsSubclassOf(typeof(TaktBaseEntity)))
            {
                query = query.Where("is_deleted = 0");
            }

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.CountAsync();
        }

        #endregion

        #region 新增操作

        /// <summary>
        /// 新增单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> CreateAsync(TEntity entity)
        {
            return await CreateAsync(entity, null);
        }

        /// <summary>
        /// 新增实体（指定用户名）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="userName">用户名</param>
        /// <returns>影响行数</returns>
        public async Task<int> CreateAsync(TEntity entity, string? userName)
        {
            if (entity is TaktBaseEntity baseEntity)
            {
                baseEntity.CreateTime = DateTime.Now;
                if (string.IsNullOrEmpty(baseEntity.CreateBy))
                {
                    baseEntity.CreateBy = GetCurrentUserName(userName);
                }
                _logger.Info($"[CreateAsync] 实体类型: {typeof(TEntity).Name}, CreateBy: '{baseEntity.CreateBy}'");
            }

            // 检查是否启用差异日志
            var insertable = _db.Insertable(entity);
            if (_dbOptions.Value.SqlDiffLog)
            {
                insertable.EnableDiffLogEvent();
            }

            int result;

            // 检查是否启用雪花ID
            if (_dbOptions.Value.SnowflakeId.Enable)
            {
                // 使用雪花ID
                var snowflakeId = insertable.ExecuteReturnSnowflakeId();
                result = snowflakeId > 0 ? 1 : 0;

                // 将生成的雪花ID赋值给实体对象
                if (snowflakeId > 0)
                {
                    var idProperty = typeof(TEntity).GetProperty("Id");
                    if (idProperty != null && idProperty.CanWrite)
                    {
                        idProperty.SetValue(entity, snowflakeId);
                        _logger.Debug($"[CreateAsync] 实体类型: {typeof(TEntity).Name}, 生成的雪花ID: {snowflakeId}");
                    }
                }
            }
            else
            {
                // 使用自增ID
                result = await insertable.ExecuteCommandAsync();

                // 如果成功插入，获取生成的ID并赋值给实体对象
                if (result > 0)
                {
                    try
                    {
                        // 使用反射设置Id属性
                        var idProperty = typeof(TEntity).GetProperty("Id");
                        if (idProperty != null && idProperty.CanWrite)
                        {
                            // 尝试从数据库获取生成的ID
                            var generatedId = await insertable.ExecuteReturnIdentityAsync();
                            if (generatedId > 0)
                            {
                                idProperty.SetValue(entity, generatedId);
                                _logger.Debug($"[CreateAsync] 实体类型: {typeof(TEntity).Name}, 生成的自增ID: {generatedId}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"[CreateAsync] 获取生成的ID失败: {ex.Message}");
                        // 不抛出异常，避免影响主业务流程
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 批量新增实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> CreateRangeAsync(List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
                return 0;

            var now = DateTime.Now;
            foreach (var entity in entities)
            {
                if (entity is TaktBaseEntity baseEntity)
                {
                    baseEntity.CreateTime = now;
                    if (string.IsNullOrEmpty(baseEntity.CreateBy))
                    {
                        baseEntity.CreateBy = GetCurrentUserName();
                    }
                }
            }

            // 检查是否启用差异日志
            var insertable = _db.Insertable(entities);
            if (_dbOptions.Value.SqlDiffLog)
            {
                insertable.EnableDiffLogEvent();
            }

            int result;

            // 检查是否启用雪花ID
            if (_dbOptions.Value.SnowflakeId.Enable)
            {
                // 使用雪花ID批量插入
                var snowflakeIds = insertable.ExecuteReturnSnowflakeIdList();
                result = snowflakeIds.Count;

                // 将生成的雪花ID赋值给实体对象
                if (result > 0)
                {
                    for (int i = 0; i < entities.Count && i < snowflakeIds.Count; i++)
                    {
                        var idProperty = typeof(TEntity).GetProperty("Id");
                        if (idProperty != null && idProperty.CanWrite)
                        {
                            idProperty.SetValue(entities[i], snowflakeIds[i]);
                        }
                    }
                    _logger.Debug($"[CreateRangeAsync] 批量插入 {result} 个实体，使用雪花ID");
                }
            }
            else
            {
                // 使用自增ID批量插入
                result = await insertable.ExecuteCommandAsync();
                _logger.Debug($"[CreateRangeAsync] 批量插入 {result} 个实体，使用自增ID");
            }

            return result;
        }

        #endregion

        #region 更新操作

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            return await UpdateAsync(entity, null);
        }

        /// <summary>
        /// 更新实体（指定用户名）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="userName">用户名</param>
        /// <returns>影响行数</returns>
        public async Task<int> UpdateAsync(TEntity entity, string? userName)
        {
            try
            {
                _logger.Info($"[UpdateAsync] 开始更新实体: 类型={typeof(TEntity).Name}");

                // 自动设置审计字段
                if (entity is TaktBaseEntity baseEntity)
                {
                    baseEntity.UpdateTime = DateTime.Now;
                    if (string.IsNullOrEmpty(baseEntity.UpdateBy))
                    {
                        baseEntity.UpdateBy = GetCurrentUserName(userName);
                    }
                }

                // 检查是否启用差异日志
                var updateable = _db.Updateable(entity);
                if (_dbOptions.Value.SqlDiffLog)
                {
                    updateable.EnableDiffLogEvent();
                }
                var result = await updateable.ExecuteCommandAsync();

                _logger.Info($"[UpdateAsync] 更新完成，影响行数: {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"[UpdateAsync] 更新实体失败: 类型={typeof(TEntity).Name}", ex);
                throw;
            }
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> UpdateRangeAsync(List<TEntity> entities)
        {
            var now = DateTime.Now;
            var userName = GetCurrentUserName();

            foreach (var entity in entities)
            {
                if (entity is TaktBaseEntity baseEntity)
                {
                    baseEntity.UpdateTime = now;
                    if (string.IsNullOrEmpty(baseEntity.UpdateBy))
                    {
                        baseEntity.UpdateBy = userName;
                    }
                }
            }

            // 超大数据量时使用分批更新
            if (entities.Count > 50000)
            {
                var total = 0;
                // 每批10000条
                for (int i = 0; i < entities.Count; i += 10000)
                {
                    var batch = entities.Skip(i).Take(10000).ToList();
                    var batchUpdateable = _db.Updateable(batch);
                    if (_dbOptions.Value.SqlDiffLog)
                    {
                        batchUpdateable.EnableDiffLogEvent();
                    }
                    total += await batchUpdateable.ExecuteCommandAsync();
                }
                return total;
            }

            // 普通批量更新使用PageSize分批
            var updateable = _db.Updateable(entities);
            if (_dbOptions.Value.SqlDiffLog)
            {
                updateable.EnableDiffLogEvent();
            }
            return await updateable.PageSize(10000).ExecuteCommandAsync();
        }

        #endregion

        #region 删除操作

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return 0;
            return await DeleteAsync(entity);
        }

        /// <summary>
        /// 删除单个实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            // 检查是否有Status字段
            var statusProperty = typeof(TEntity).GetProperty("Status");
            if (statusProperty != null)
            {
                // 更新Status为1
                statusProperty.SetValue(entity, 1);
                var updateByProperty = typeof(TEntity).GetProperty("UpdateBy");
                var updateTimeProperty = typeof(TEntity).GetProperty("UpdateTime");
                if (updateByProperty != null && string.IsNullOrEmpty(updateByProperty.GetValue(entity)?.ToString()))
                {
                    updateByProperty.SetValue(entity, GetCurrentUserName());
                }
                if (updateTimeProperty != null)
                {
                    updateTimeProperty.SetValue(entity, DateTime.Now);
                }
            }

            // 设置删除标记
            if (entity is TaktBaseEntity baseEntity)
            {
                baseEntity.IsDeleted = 1;
                baseEntity.DeleteTime = DateTime.Now;
                if (string.IsNullOrEmpty(baseEntity.DeleteBy))
                {
                    baseEntity.DeleteBy = GetCurrentUserName();
                }
            }

            // 检查是否启用差异日志
            var updateable = _db.Updateable(entity);
            if (_dbOptions.Value.SqlDiffLog)
            {
                updateable.EnableDiffLogEvent();
            }
            return await updateable.ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="condition">删除条件</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> condition)
        {
            var now = DateTime.Now;
            var userName = GetCurrentUserName();

            // 获取符合条件的实体
            var entities = await GetListAsync(condition);
            if (!entities.Any()) return 0;

            // 检查是否有Status字段
            var statusProperty = typeof(TEntity).GetProperty("Status");
            var updateByProperty = typeof(TEntity).GetProperty("UpdateBy");
            var updateTimeProperty = typeof(TEntity).GetProperty("UpdateTime");

            foreach (var entity in entities)
            {
                // 更新Status为1
                if (statusProperty != null)
                {
                    statusProperty.SetValue(entity, 1);
                    if (updateByProperty != null && string.IsNullOrEmpty(updateByProperty.GetValue(entity)?.ToString()))
                    {
                        updateByProperty.SetValue(entity, userName);
                    }
                    if (updateTimeProperty != null)
                    {
                        updateTimeProperty.SetValue(entity, now);
                    }
                }

                // 设置删除标记
                if (entity is TaktBaseEntity baseEntity)
                {
                    baseEntity.IsDeleted = 1;
                    baseEntity.DeleteTime = now;
                    if (string.IsNullOrEmpty(baseEntity.DeleteBy))
                    {
                        baseEntity.DeleteBy = userName;
                    }
                }
            }

            // 检查是否启用差异日志
            var updateable = _db.Updateable(entities);
            if (_dbOptions.Value.SqlDiffLog)
            {
                updateable.EnableDiffLogEvent();
            }
            return await updateable.ExecuteCommandAsync();
        }

        /// <summary>
        /// 根据主键批量删除
        /// </summary>
        /// <param name="ids">主键值列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteRangeAsync(List<object> ids)
        {
            var entities = new List<TEntity>();
            foreach (var id in ids)
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    entities.Add(entity);
                }
            }

            if (!entities.Any()) return 0;
            return await DeleteRangeAsync(entities);
        }

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>返回受影响的行数</returns>
        public async Task<int> DeleteRangeAsync(List<TEntity> entities)
        {
            var now = DateTime.Now;
            var userName = GetCurrentUserName();

            // 检查是否有Status字段
            var statusProperty = typeof(TEntity).GetProperty("Status");
            var updateByProperty = typeof(TEntity).GetProperty("UpdateBy");
            var updateTimeProperty = typeof(TEntity).GetProperty("UpdateTime");

            foreach (var entity in entities)
            {
                // 更新Status为1
                if (statusProperty != null)
                {
                    statusProperty.SetValue(entity, 1);
                    if (updateByProperty != null && string.IsNullOrEmpty(updateByProperty.GetValue(entity)?.ToString()))
                    {
                        updateByProperty.SetValue(entity, userName);
                    }
                    if (updateTimeProperty != null)
                    {
                        updateTimeProperty.SetValue(entity, now);
                    }
                }

                // 设置删除标记
                if (entity is TaktBaseEntity baseEntity)
                {
                    baseEntity.IsDeleted = 1;
                    baseEntity.DeleteTime = now;
                    if (string.IsNullOrEmpty(baseEntity.DeleteBy))
                    {
                        baseEntity.DeleteBy = userName;
                    }
                }
            }
            // 检查是否启用差异日志
            var updateable = _db.Updateable(entities);
            if (_dbOptions.Value.SqlDiffLog)
            {
                updateable.EnableDiffLogEvent();
            }
            return await updateable.ExecuteCommandAsync();
        }

        #endregion

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        public virtual async Task<List<string>> GetUserRolesAsync(long userId)
        {
            var roles = await _db.Queryable<TaktUserRole>()
                .LeftJoin<TaktRole>((ur, r) => ur.RoleId == r.Id)
                .Where(ur => ur.UserId == userId)
                .Select((ur, r) => r.RoleKey)
                .ToListAsync() ?? new List<string>();

            return roles.Where(r => !string.IsNullOrEmpty(r)).ToList();
        }

        /// <summary>
        /// 获取用户权限列表
        /// </summary>
        public virtual async Task<List<string>> GetUserPermissionsAsync(long userId)
        {
            _logger.Info("[权限仓储] 开始查询用户权限: UserId={UserId}", userId);

            try
            {
                // 直接执行查询（暂时移除缓存逻辑，因为cache_table不存在）
                var permissionStrings = await _db.Queryable<TaktUserRole>()
                    .LeftJoin<TaktRole>((ur, r) => ur.RoleId == r.Id)
                    .LeftJoin<TaktRoleMenu>((ur, r, rm) => r.Id == rm.RoleId)
                    .LeftJoin<TaktMenu>((ur, r, rm, m) => rm.MenuId == m.Id)
                    .Where(ur => ur.UserId == userId)
                    .Select((ur, r, rm, m) => new { Perms = m.Perms })
                    .MergeTable()
                    .Where(x => x.Perms != null && x.Perms != string.Empty)
                    .Select(x => x.Perms)
                    .ToListAsync() ?? new List<string>();

                var permissions = permissionStrings
                    .SelectMany(perms => perms.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    .Distinct()
                    .ToList();

                _logger.Info("[权限仓储] 用户权限查询完成: UserId={UserId}, 原始权限字符串数量={RawCount}, 解析后权限数量={FinalCount}",
                    userId, permissionStrings.Count, permissions.Count);

                if (permissions.Count == 0)
                {
                    _logger.Warn("[权限仓储] 用户 {UserId} 没有找到任何权限", userId);
                }
                else
                {
                    _logger.Debug("[权限仓储] 用户 {UserId} 权限列表: {Permissions}",
                        userId, string.Join(", ", permissions.Take(20))); // 只显示前20个权限
                }

                return permissions;
            }
            catch (Exception ex)
            {
                _logger.Error("[权限仓储] 查询用户权限失败: UserId={UserId}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        public virtual async Task<List<long>> GetUserPostsAsync(long userId)
        {
            _logger.Info("[岗位仓储] 开始查询用户岗位: UserId={UserId}", userId);

            var postIds = await _db.Queryable<TaktUserPost>()
                .Where(up => up.UserId == userId && up.IsDeleted == 0)
                .Select(up => up.PostId)
                .ToListAsync() ?? new List<long>();

            return postIds;
        }

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        public virtual async Task<List<long>> GetUserDeptsAsync(long userId)
        {
            _logger.Info("[部门仓储] 开始查询用户部门: UserId={UserId}", userId);

            var deptIds = await _db.Queryable<TaktUserDept>()
                .Where(ud => ud.UserId == userId && ud.IsDeleted == 0)
                .Select(ud => ud.DeptId)
                .ToListAsync() ?? new List<long>();

            return deptIds;
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        public virtual async Task<List<long>> GetRoleMenusAsync(long roleId)
        {
            _logger.Info("[菜单仓储] 开始查询角色菜单: RoleId={RoleId}", roleId);

            var menuIds = await _db.Queryable<TaktRoleMenu>()
                .Where(rm => rm.RoleId == roleId && rm.IsDeleted == 0)
                .Select(rm => rm.MenuId)
                .ToListAsync() ?? new List<long>();

            return menuIds;
        }

    }
}




