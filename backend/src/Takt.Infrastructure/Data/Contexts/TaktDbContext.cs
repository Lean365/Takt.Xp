#nullable disable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbContextBase.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 数据库上下文基础抽象类
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.Data;
using Takt.Domain.Entities.Logging;
using Takt.Domain.Interfaces;
using Microsoft.Extensions.Options;
using SqlSugar;
using System.Data;

namespace Takt.Infrastructure.Data.Contexts
{
    /// <summary>
    /// 数据库上下文基础抽象类
    /// 包含所有数据库上下文的共同功能
    /// </summary>
    public abstract class TaktDbContext : ITaktDbContext
    {
        /// <summary>
        /// SqlSugar数据库客户端实例
        /// </summary>
        protected readonly SqlSugarScope _client;

        /// <summary>
        /// 日志记录器
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="client">SqlSugar数据库客户端</param>
        /// <param name="logger">日志记录器</param>
        protected TaktDbContext(SqlSugarScope client, ITaktLogger logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 获取数据库客户端实例
        /// </summary>
        public SqlSugarScope Client => _client;

        /// <summary>
        /// 获取Ado对象
        /// </summary>
        public IAdo Ado => _client.Ado;

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        public virtual void BeginTran()
        {
            try
            {
                _client.Ado.BeginTran();
            }
            catch (Exception ex)
            {
                _logger.Error("开启事务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        public virtual void CommitTran()
        {
            try
            {
                _client.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _logger.Error("提交事务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        public virtual void RollbackTran()
        {
            _client.RollbackTran();
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns>数据库连接对象</returns>
        public virtual IDbConnection GetConnection()
        {
            return _client.Ado.Connection;
        }

        /// <summary>
        /// 初始化数据库
        /// 包括创建数据库、创建表、更新表结构等操作
        /// </summary>
        public abstract Task InitializeAsync();

        /// <summary>
        /// 创建数据库（如果不存在）
        /// </summary>
        protected async Task CreateDatabaseIfNotExistsAsync()
        {
            if (string.IsNullOrEmpty(_client.Ado.Connection.ConnectionString))
            {
                throw new InvalidOperationException("数据库连接字符串不能为空");
            }

            await Task.Run(() => _client.DbMaintenance.CreateDatabase());
        }
    }
}






