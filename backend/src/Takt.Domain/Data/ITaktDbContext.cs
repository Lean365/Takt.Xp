//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktDbContext.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-23 12:00
// 版本号 : V0.0.1
// 描述   : 数据库上下文接口
//===================================================================

using SqlSugar;
using System.Data;
using System.Threading.Tasks;

namespace Takt.Domain.Data;

/// <summary>
/// 数据库上下文接口
/// </summary>
public interface ITaktDbContext
{
    /// <summary>
    /// 获取SqlSugar客户端
    /// </summary>
    SqlSugarScope Client { get; }

    /// <summary>
    /// 获取Ado对象
    /// </summary>
    IAdo Ado { get; }

    /// <summary>
    /// 开启事务
    /// </summary>
    void BeginTran();

    /// <summary>
    /// 提交事务
    /// </summary>
    void CommitTran();

    /// <summary>
    /// 回滚事务
    /// </summary>
    void RollbackTran();

    /// <summary>
    /// 获取数据库连接
    /// </summary>
    /// <returns>数据库连接</returns>
    IDbConnection GetConnection();


} 



