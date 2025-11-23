#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGenConfigService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成配置服务接口
//===================================================================

namespace Takt.Application.Services.Generator;
using Takt.Application.Dtos.Generator;

/// <summary>
/// 代码生成配置服务接口
/// 用于管理代码生成的基础配置信息
/// </summary>
public interface ITaktGenConfigService
{
    #region 基础操作

    /// <summary>
    /// 根据ID获取配置信息
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>配置信息</returns>
    Task<TaktGenConfigDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取分页配置列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktGenConfigDto>> GetListAsync(TaktGenConfigQueryDto input);

    /// <summary>
    /// 创建配置信息
    /// </summary>
    /// <param name="input">配置信息</param>
    /// <returns>创建结果</returns>
    Task<TaktGenConfigDto> CreateAsync(TaktGenConfigCreateDto input);

    /// <summary>
    /// 更新配置信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的配置信息</returns>
    Task<TaktGenConfigDto> UpdateAsync(TaktGenConfigUpdateDto input);

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>是否删除成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    #endregion 基础操作

    #region 配置操作

    /// <summary>
    /// 导入配置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入成功和失败的数量</returns>
    Task<(int success, int fail)> ImportConfigsAsync(Stream fileStream, string sheetName = "GenConfig");

    /// <summary>
    /// 导出配置
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportConfigsAsync(TaktGenConfigQueryDto query, string sheetName = "GenConfig");

    /// <summary>
    /// 获取配置模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "GenConfig");

    /// <summary>
    /// 更新生成配置状态
    /// </summary>
    /// <param name="input">状态更新参数</param>
    /// <returns>是否更新成功</returns>
    Task<bool> UpdateStatusAsync(TaktGenConfigStatusDto input);    

    /// <summary>
    /// 获取生成配置选项列表（用于下拉选择）
    /// </summary>
    /// <returns>生成配置选项列表</returns>
    Task<List<TaktSelectOption>> GetOptionsAsync();



    #endregion 配置操作
}

