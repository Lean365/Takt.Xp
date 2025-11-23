//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktDictTypeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 字典类型服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Takt.Shared.Models;
using Takt.Shared.Enums;
using System.IO;
using Takt.Application.Dtos.Routine.DataDictionary;

namespace Takt.Application.Services.Routine.DataDictionary
{
    /// <summary>
    /// 字典类型服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface ITaktDictTypeService
    {
        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        Task<TaktPagedResult<TaktDictTypeDto>> GetListAsync(TaktDictTypeQueryDto query);

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="id">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        Task<TaktDictTypeDto> GetByIdAsync(long id);

        /// <summary>
        /// 根据字典类型获取详情
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns>字典类型详情</returns>
        Task<TaktDictTypeDto> GetByTypeAsync(string type);

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        Task<long> CreateAsync(TaktDictTypeCreateDto input);

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktDictTypeUpdateDto input);

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="id">字典类型ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] dictTypeIds);

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktDictTypeQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktDictTypeStatusDto input);

        /// <summary>
        /// 执行字典SQL脚本
        /// </summary>
        /// <param name="sqlScript">SQL脚本</param>
        /// <returns>字典数据列表</returns>
        Task<List<TaktDictDataDto>> ExecuteDictSqlAsync(string sqlScript);
    }
} 




