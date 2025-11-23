//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGeneralSettingsService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置服务接口
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGeneralSettingsService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置服务接口
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGeneralSettingsService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置服务接口
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktGeneralSettingsService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Settings
{
    /// <summary>
    /// 通用设置服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface ITaktGeneralSettingsService
    {
        /// <summary>
        /// 获取通用设置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>通用设置分页列表</returns>
        Task<TaktPagedResult<TaktGeneralSettingsDto>> GetListAsync(TaktGeneralSettingsQueryDto query);

        /// <summary>
        /// 获取通用设置详情
        /// </summary>
        /// <param name="id">设置ID</param>
        /// <returns>通用设置详情</returns>
        Task<TaktGeneralSettingsDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建通用设置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>设置ID</returns>
        Task<long> CreateAsync(TaktGeneralSettingsCreateDto input);

        /// <summary>
        /// 更新通用设置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktGeneralSettingsUpdateDto input);

        /// <summary>
        /// 删除通用设置
        /// </summary>
        /// <param name="id">设置ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除通用设置
        /// </summary>
        /// <param name="settingsIds">设置ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] settingsIds);

        /// <summary>
        /// 更新通用设置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktGeneralSettingsStatusDto input);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 导入通用设置数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出通用设置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktGeneralSettingsQueryDto query, string sheetName);
    }
}



