#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGenTemplateService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成模板服务接口
//===================================================================

namespace Takt.Application.Services.Generator;

/// <summary>
/// 代码生成模板服务接口
/// 用于管理代码生成的模板信息
/// </summary>
public interface ITaktGenTemplateService
{
    #region 基础操作

    /// <summary>
    /// 根据ID获取模板信息
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>模板信息</returns>
    Task<TaktGenTemplateDto?> GetByIdAsync(long id);

    /// <summary>
    /// 获取分页模板列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    Task<TaktPagedResult<TaktGenTemplateDto>> GetListAsync(TaktGenTemplateQueryDto input);

    /// <summary>
    /// 创建模板信息
    /// </summary>
    /// <param name="input">模板信息</param>
    /// <returns>创建结果</returns>
    Task<TaktGenTemplateDto> CreateAsync(TaktGenTemplateCreateDto input);

    /// <summary>
    /// 更新模板信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的模板信息</returns>
    Task<TaktGenTemplateDto> UpdateAsync(TaktGenTemplateUpdateDto input);

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>是否删除成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除模板
    /// </summary>
    /// <param name="ids">模板ID集合</param>
    /// <returns>是否删除成功</returns>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 更新模板状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(TaktGenTemplateStatusDto input);

    #endregion 基础操作

    #region 模板操作

    /// <summary>
    /// 导入模板
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入成功和失败的数量</returns>
    Task<(int success, int fail)> ImportTemplatesAsync(Stream fileStream, string sheetName = "GenTemplate");

    /// <summary>
    /// 导出模板
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    Task<(string fileName, byte[] content)> ExportTemplatesAsync(TaktGenTemplateQueryDto query, string sheetName = "GenTemplate");

    /// <summary>
    /// 获取模板文件
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件字节数组</returns>
    Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "GenTemplate");

    #endregion 模板操作
}

