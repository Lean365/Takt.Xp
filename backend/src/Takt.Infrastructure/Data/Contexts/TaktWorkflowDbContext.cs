//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowDbContext.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 工作流数据库上下文
//===================================================================

using Takt.Shared.Options;
using Takt.Shared.Utils;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 工作流数据库上下文类
    /// 负责管理工作流相关的数据库操作
    /// </summary>
    public class TaktWorkflowDbContext : TaktDbContext
    {
        /// <summary>
        /// 数据库配置选项
        /// </summary>
        private readonly TaktDbOptions _dbOptions;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        private readonly ITaktCurrentUser _currentUser;



        /// <summary>
        /// 构造函数（用于多库AOP配置）
        /// </summary>
        /// <param name="client">SqlSugarScopeProvider实例</param>
        /// <param name="dbOptions">数据库配置选项</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="currentUser">当前用户信息</param>
        /// <param name="serviceProvider">服务提供器</param>
        /// <param name="configuration">配置接口</param>
        public TaktWorkflowDbContext(SqlSugar.SqlSugarScopeProvider client, IOptions<TaktDbOptions> dbOptions, ITaktLogger logger, ITaktCurrentUser currentUser, IServiceProvider serviceProvider, IConfiguration configuration)
            : base(new SqlSugarScope(client.CurrentConnectionConfig), logger)
        {
            _dbOptions = dbOptions.Value;
            _currentUser = currentUser;

            // AOP配置已由TaktServiceCollectionExtensions统一管理
        }







        /// <summary>
        /// 初始化数据库
        /// 包括创建数据库、创建表、更新表结构等操作
        /// </summary>
        public override async Task InitializeAsync()
        {
            try
            {
                // _logger.Info("开始初始化工作流数据库");
                await InitializeWorkflowDatabaseAsync();

                // AOP配置已由TaktServiceCollectionExtensions统一管理
            }
            catch (Exception ex)
            {
                var maskedMessage1 = TaktMaskUtils.MaskDatabaseLog("初始化工作流数据库失败");
                _logger.Error(maskedMessage1, ex);
                throw;
            }
        }

        /// <summary>
        /// 初始化工作流数据库
        /// </summary>
        private async Task InitializeWorkflowDatabaseAsync()
        {
            try
            {
                //_logger.Info("开始初始化工作流数据库");

                // 1.创建数据库(如果不存在)
                if (string.IsNullOrEmpty(Client.Ado.Connection.ConnectionString))
                {
                    throw new InvalidOperationException("数据库连接字符串不能为空");
                }

                var maskedMessage2 = TaktMaskUtils.MaskDatabaseLog("正在检查/创建工作流数据库...");
                _logger.Info(maskedMessage2);

                // 设置超时时间
                var timeout = TimeSpan.FromSeconds(30);
                using var cts = new CancellationTokenSource(timeout);

                try
                {
                    await Task.Run(() => Client.DbMaintenance.CreateDatabase(), cts.Token);
                    var maskedMessage3 = TaktMaskUtils.MaskDatabaseLog("工作流数据库检查/创建成功");
                    _logger.Info(maskedMessage3);
                }
                catch (OperationCanceledException)
                {
                    var maskedMessage4 = TaktMaskUtils.MaskDatabaseLog("工作流数据库操作超时（30秒），请检查网络连接和数据库服务器状态");
                    _logger.Error(maskedMessage4);
                    throw new TimeoutException("工作流数据库操作超时，请检查网络连接和数据库服务器状态");
                }

                // 2.获取所有工作流相关实体类型
                var entityTypes = GetWorkflowEntityTypes();

                // 3.创建/更新表
                await CreateOrUpdateTablesAsync(entityTypes, "工作流数据库", Client);
            }
            catch (Exception ex)
            {
                var maskedMessage5 = TaktMaskUtils.MaskDatabaseLog("初始化工作流数据库失败");
                _logger.Error(maskedMessage5, ex);
                throw;
            }
        }

        /// <summary>
        /// 获取所有工作流相关实体类型
        /// </summary>
        private Type[] GetWorkflowEntityTypes()
        {
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.FullName != null && a.FullName.StartsWith("Takt."))
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.IsPublic &&
                           t.Namespace != null &&
                           t.Namespace.StartsWith("Takt.Domain.Entities.Workflow") &&
                           t.BaseType == typeof(TaktBaseEntity))
                .ToArray();

            var maskedMessage6 = TaktMaskUtils.MaskDatabaseLog($"工作流数据库找到 {entityTypes.Length} 个实体类型: {string.Join(", ", entityTypes.Select(t => t.Name))}");
            _logger.Info(maskedMessage6);
            
            return entityTypes;
        }

        /// <summary>
        /// 创建或更新表
        /// </summary>
        private async Task CreateOrUpdateTablesAsync(Type[] entityTypes, string databaseName, ISqlSugarClient? client = null)
        {
            var dbClient = client ?? Client;
            
            // 对实体类型进行排序，确保表创建顺序的一致性
            // 工作流数据库优先级：按实体名称排序
            var sortedEntityTypes = entityTypes
                .OrderBy(t => t.Name)        // 按实体名称排序
                .ToArray();
            
            var maskedMessage7 = TaktMaskUtils.MaskDatabaseLog($"开始处理 {databaseName} 的 {sortedEntityTypes.Length} 个实体类型（已按名称排序）");
            _logger.Info(maskedMessage7);

            foreach (var entityType in sortedEntityTypes)
            {
                try
                {
                    var tableName = dbClient.EntityMaintenance.GetTableName(entityType);
                    var entityInfo = dbClient.EntityMaintenance.GetEntityInfo(entityType);

                    var maskedMessage8 = TaktMaskUtils.MaskDatabaseLog($"处理实体类型: {entityType.Name} -> 表名: {tableName}");
                    _logger.Info(maskedMessage8);

                    // 检查表是否存在
                    var isTableExists = await Task.Run(() => dbClient.DbMaintenance.IsAnyTable(tableName));
                    if (!isTableExists)
                    {
                        var maskedMessage = TaktMaskUtils.MaskDatabaseLog($"[{databaseName}] 新建表 {tableName}");
                        _logger.Info(maskedMessage);
                        await Task.Run(() => dbClient.CodeFirst.InitTables(entityType));
                        continue;
                    }

                    // 获取数据库中的列信息
                    var dbColumns = await Task.Run(() => dbClient.DbMaintenance.GetColumnInfosByTableName(tableName));
                    // 获取实体中的列信息
                    var entityColumns = entityInfo.Columns;

                    // 比较列差异
                    var shouldUpdate = ShouldUpdateTable(dbColumns, entityColumns);

                    // 更新表结构
                    if (shouldUpdate == true) // 需要更新
                    {
                        var maskedMessage = TaktMaskUtils.MaskDatabaseLog($"[{databaseName}] 更新表 {tableName}");
                        _logger.Info(maskedMessage);
                        await Task.Run(() => dbClient.CodeFirst.InitTables(entityType));
                    }
                    else if (shouldUpdate == null) // 需要删除表重新创建
                    {
                        var maskedMessage = TaktMaskUtils.MaskDatabaseLog($"[{databaseName}] 删除表 {tableName} 并重新创建");
                        _logger.Error(maskedMessage); // 红色日志
                        
                        // 先删除表
                        await Task.Run(() => dbClient.DbMaintenance.DropTable(tableName));
                        
                        // 等待一小段时间确保删除操作完成
                        await Task.Delay(100);
                        
                        // 使用新的事务重新创建表
                        await Task.Run(() => dbClient.CodeFirst.InitTables(entityType));
                    }
                    else // 无需更新
                    {
                        var maskedMessage9 = TaktMaskUtils.MaskDatabaseLog($"表 {tableName} 无需更新");
                        _logger.Info(maskedMessage9);
                    }
                }
                catch (Exception ex)
                {
                    var maskedMessage10 = TaktMaskUtils.MaskDatabaseLog($"处理表 {entityType.Name} 时发生错误: {ex.Message}");
                    _logger.Error(maskedMessage10);
                    throw;
                }
            }
        }

        /// <summary>
        /// 判断是否需要更新表结构
        /// </summary>
        /// <returns>true: 需要更新, false: 无需更新, null: 需要删除表重新创建</returns>
        private bool? ShouldUpdateTable(List<DbColumnInfo> dbColumns, List<EntityColumnInfo> entityColumns)
        {
            // 获取实体中实际需要创建数据库列的属性（排除忽略的属性和导航属性）
            var actualEntityColumns = entityColumns.Where(c => !c.IsIgnore && !IsNavigationProperty(c)).ToList();
            
            // 计算基类列数量（TaktBaseEntity的列数）
            var baseEntityColumnCount = 25; // TaktBaseEntity包含25个基础字段
            
            // 计算实际业务列数量（排除基类列）
            var businessColumnCount = actualEntityColumns.Count - baseEntityColumnCount;
            
            // 如果实际列数量不匹配，需要更新
            if (dbColumns.Count != actualEntityColumns.Count)
            {
                var maskedMessage11 = TaktMaskUtils.MaskDatabaseLog($"表列数量分析:");
                _logger.Info(maskedMessage11);
                var maskedMessage12 = TaktMaskUtils.MaskDatabaseLog($"  数据库列数量: {dbColumns.Count}");
                _logger.Info(maskedMessage12);
                var maskedMessage13 = TaktMaskUtils.MaskDatabaseLog($"  实体总列数量: {entityColumns.Count}");
                _logger.Info(maskedMessage13);
                var maskedMessage14 = TaktMaskUtils.MaskDatabaseLog($"  实体实际列数量: {actualEntityColumns.Count}");
                _logger.Info(maskedMessage14);
                var maskedMessage15 = TaktMaskUtils.MaskDatabaseLog($"  基类列数量: {baseEntityColumnCount}");
                _logger.Info(maskedMessage15);
                var maskedMessage16 = TaktMaskUtils.MaskDatabaseLog($"  业务列数量: {businessColumnCount}");
                _logger.Info(maskedMessage16);
                var maskedMessage17 = TaktMaskUtils.MaskDatabaseLog($"  数据库列: {string.Join(", ", dbColumns.Select(c => c.DbColumnName))}");
                _logger.Info(maskedMessage17);
                var maskedMessage18 = TaktMaskUtils.MaskDatabaseLog($"  实体实际列: {string.Join(", ", actualEntityColumns.Select(c => c.DbColumnName))}");
                _logger.Info(maskedMessage18);
                return true;
            }

            // 检查每个实体列是否在数据库中存在且属性匹配
            foreach (var entityColumn in actualEntityColumns)
            {
                var dbColumn = dbColumns.FirstOrDefault(x =>
                    x.DbColumnName.Equals(entityColumn.DbColumnName, StringComparison.OrdinalIgnoreCase));

                if (dbColumn == null)
                {
                    var maskedMessage19 = TaktMaskUtils.MaskDatabaseLog($"数据库缺少列: {entityColumn.DbColumnName}");
                    _logger.Info(maskedMessage19);
                    return true; // 实体中有列但数据库中没有
                }

                // 检查列属性是否匹配 - 如果不匹配，需要删除表重新创建
                if (entityColumn.IsNullable != dbColumn.IsNullable)
                {
                    var maskedMessage20 = TaktMaskUtils.MaskDatabaseLog($"列可空性不匹配: {entityColumn.DbColumnName}, 数据库: {dbColumn.IsNullable}, 实体: {entityColumn.IsNullable} - 需要删除表重新创建");
                    _logger.Info(maskedMessage20);
                    return null; // 需要删除表重新创建
                }

                // 检查列长度是否匹配 - 需要特殊处理 nvarchar(MAX) 的情况
                if (entityColumn.Length > 0 && entityColumn.Length != dbColumn.Length)
                {
                    // 特殊处理：如果数据库列是 nvarchar(MAX) 且实体列长度合理，则认为是匹配的
                    if (dbColumn.Length == -1 && entityColumn.Length > 0 && entityColumn.Length <= 8000)
                    {
                        var maskedMessage21 = TaktMaskUtils.MaskDatabaseLog($"列长度兼容: {entityColumn.DbColumnName}, 数据库: nvarchar(MAX), 实体: nvarchar({entityColumn.Length}) - 长度兼容，无需重新创建");
                        _logger.Info(maskedMessage21);
                        continue; // 继续检查下一个列
                    }
                    
                    var maskedMessage22 = TaktMaskUtils.MaskDatabaseLog($"列长度不匹配: {entityColumn.DbColumnName}, 数据库: {dbColumn.Length}, 实体: {entityColumn.Length} - 需要删除表重新创建");
                    _logger.Info(maskedMessage22);
                    return null; // 需要删除表重新创建
                }
            }

            return false; // 所有列都匹配
        }

        /// <summary>
        /// 判断是否为导航属性
        /// </summary>
        /// <param name="entityColumn">实体列信息</param>
        /// <returns>true: 是导航属性, false: 不是导航属性</returns>
        private bool IsNavigationProperty(EntityColumnInfo entityColumn)
        {
            try
            {
                // 检查属性是否有 Navigate 特性
                var propertyInfo = entityColumn.PropertyInfo;
                if (propertyInfo != null)
                {
                    var navigateAttribute = propertyInfo.GetCustomAttributes(typeof(Navigate), true).FirstOrDefault();
                    return navigateAttribute != null;
                }
                return false;
            }
            catch
            {
                // 如果无法判断，默认不是导航属性
                return false;
            }
        }
    }
}





