//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktContractService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 合同服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Takt.Shared.Models;
using Takt.Application.Dtos.Routine.Contract;

namespace Takt.Application.Services.Routine.Contract
{
    /// <summary>
    /// 合同服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktContractService
    {
        /// <summary>
        /// 获取合同分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>合同分页列表</returns>
        Task<TaktPagedResult<TaktContractDto>> GetListAsync(TaktContractQueryDto query);

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>合同详情</returns>
        Task<TaktContractDto> GetByIdAsync(long contractId);

        /// <summary>
        /// 创建合同
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>合同ID</returns>
        Task<long> CreateAsync(TaktContractCreateDto input);

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktContractUpdateDto input);

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long contractId);

        /// <summary>
        /// 批量删除合同
        /// </summary>
        /// <param name="contractIds">合同ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] contractIds);

        /// <summary>
        /// 导入合同数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "合同信息");

        /// <summary>
        /// 导出合同数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktContractQueryDto query, string sheetName = "合同信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "合同信息");
    }
} 




