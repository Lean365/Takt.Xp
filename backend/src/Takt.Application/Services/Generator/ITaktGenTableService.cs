#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGenTableService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表服务接口
//===================================================================

using Takt.Application.Dtos.Generator;
using Takt.Shared.Models;

namespace Takt.Application.Services.Generator;

/// <summary>
/// 代码生成表服务接口
/// </summary>
public interface ITaktGenTableService
{
    #region 基础操作

    /// <summary>
    /// 根据ID获取表信息
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>表信息</returns>
    Task<TaktGenTableDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取分页表列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktGenTableDto>> GetListAsync(TaktGenTableQueryDto input);

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段列表</returns>
    Task<List<TaktGenColumnDto>> GetColumnListAsync(long tableId);

    /// <summary>
    /// 创建表信息
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    Task<TaktGenTableDto> CreateAsync(TaktGenTableCreateDto input);

    /// <summary>
    /// 更新表信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表信息</returns>
    Task<TaktGenTableDto> UpdateAsync(TaktGenTableUpdateDto input);

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteAsync(long id);

    #endregion

    #region 表操作

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表列表</returns>
    Task<List<TaktGenTableDto>> ImportTablesAsync(TaktGenTableImportDto input);

    /// <summary>
    /// 导入表及其所有字段信息
    /// </summary>
    /// <param name="databaseName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <returns>是否成功</returns>
    Task<bool> ImportTableAndColumnsAsync(string databaseName, string tableName);

    /// <summary>
    /// 导出表
    /// </summary>
    /// <returns>导出的表列表</returns>
    Task<List<TaktGenTableExportDto>> ExportTablesAsync();

    /// <summary>
    /// 获取数据库列表
    /// </summary>
    /// <returns>数据库列表</returns>
    Task<List<string>> GetDatabaseListByDbAsync();

    /// <summary>
    /// 获取表列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <returns>表列表</returns>
    Task<List<TaktGenTableInfoDto>> GetTableListByDbAsync(string databaseName);

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    Task<List<TaktGenTableColumnInfoDto>> GetTableColumnListByDbAsync(string databaseName, string tableName);

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否同步成功</returns>
    Task<bool> SyncTableAsync(long id);

    #endregion
}


