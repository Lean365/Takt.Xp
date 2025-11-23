//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNumberingRulesService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 单号规则服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Numbering
{
    /// <summary>
    /// 单号规则服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktNumberingRulesService
    {
        /// <summary>
        /// 获取单号规则分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>单号规则分页列表</returns>
        Task<TaktPagedResult<TaktNumberingRulesDto>> GetListAsync(TaktNumberingRulesQueryDto query);

        /// <summary>
        /// 获取单号规则详情
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>单号规则详情</returns>
        Task<TaktNumberingRulesDto> GetByIdAsync(long numberRuleId);

        /// <summary>
        /// 获取单号规则选项列表
        /// </summary>
        /// <returns>单号规则选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 创建单号规则
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>单号规则ID</returns>
        Task<long> CreateAsync(TaktNumberingRulesCreateDto input);

        /// <summary>
        /// 更新单号规则
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNumberingRulesUpdateDto input);

        /// <summary>
        /// 删除单号规则
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long numberRuleId);

        /// <summary>
        /// 批量删除单号规则
        /// </summary>
        /// <param name="numberRuleIds">单号规则ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] numberRuleIds);

        /// <summary>
        /// 更新单号规则状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktNumberingRulesStatusDto input);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 导入单号规则数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出单号规则数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktNumberingRulesQueryDto query, string sheetName);
    }
}



