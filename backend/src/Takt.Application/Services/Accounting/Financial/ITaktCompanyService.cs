//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktCompanyService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-09 16:30
// 版本号 : V0.0.1
// 描述   : 公司代码服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Takt.Shared.Models;
using Takt.Application.Dtos.Accounting.Financial;

namespace Takt.Application.Services.Accounting.Financial
{
    /// <summary>
    /// 公司代码服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-09
    /// </remarks>
    public interface ITaktCompanyService
    {
        /// <summary>
        /// 获取公司代码分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>公司代码分页列表</returns>
        Task<TaktPagedResult<TaktCompanyDto>> GetListAsync(TaktCompanyQueryDto query);

        /// <summary>
        /// 获取公司代码详情
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>公司代码详情</returns>
        Task<TaktCompanyDto> GetByIdAsync(long companyId);

        /// <summary>
        /// 获取公司代码选项列表
        /// </summary>
        /// <returns>公司代码选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 创建公司代码
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>公司代码ID</returns>
        Task<long> CreateAsync(TaktCompanyCreateDto input);

        /// <summary>
        /// 更新公司代码
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktCompanyUpdateDto input);

        /// <summary>
        /// 删除公司代码
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long companyId);

        /// <summary>
        /// 批量删除公司代码
        /// </summary>
        /// <param name="companyIds">公司代码ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] companyIds);

        /// <summary>
        /// 更新公司代码状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktCompanyStatusDto input);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 导入公司代码数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出公司代码数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktCompanyQueryDto query, string sheetName);
    }
}





