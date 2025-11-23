//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktLanguageService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 语言服务接口
//===================================================================

using Takt.Application.Dtos.Routine.I18n;

namespace Takt.Application.Services.Routine.I18n
{
    /// <summary>
    /// 语言服务接口
    /// </summary>
    public interface ITaktLanguageService
    {
        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        Task<TaktPagedResult<TaktLanguageDto>> GetListAsync(TaktLanguageQueryDto query);

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>语言详情</returns>
        Task<TaktLanguageDto> GetByIdAsync(long LanguageId);

        /// <summary>
        /// 获取语言选项列表
        /// </summary>
        /// <returns>语言选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        Task<long> CreateAsync(TaktLanguageCreateDto input);

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktLanguageUpdateDto input);

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long LanguageId);

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="LanguageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] LanguageIds);

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktLanguageStatusDto input);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktLanguageQueryDto query, string sheetName);

    }
}



