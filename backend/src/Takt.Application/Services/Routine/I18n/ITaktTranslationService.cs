//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktTranslationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务接口
//===================================================================

namespace Takt.Application.Services.Routine.I18n
{
    /// <summary>
    /// 翻译服务接口
    /// </summary>
    public interface ITaktTranslationService
    {
        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        Task<TaktPagedResult<TaktTranslationDto>> GetListAsync(TaktTranslationQueryDto query);

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>翻译详情</returns>
        Task<TaktTranslationDto> GetByIdAsync(long TranslationId);

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        Task<long> CreateAsync(TaktTranslationCreateDto input);

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktTranslationUpdateDto input);

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long TranslationId);

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="TranslationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] TranslationIds);

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "翻译信息");

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktTranslationQueryDto query, string sheetName = "翻译信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "翻译信息");

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        Task<string> GetTransValueAsync(string langCode, string transKey);

        /// <summary>
        /// 获取指定类型的翻译列表
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transType">翻译类型，0表示获取所有翻译</param>
        /// <returns>翻译列表</returns>
        Task<List<TaktTranslationDto>> GetListByModuleAsync(string langCode, int transType);

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据,包含分页信息</returns>
        Task<TaktPagedResult<TaktTransposedDto>> GetTransposedDataAsync(TaktTransposedQueryDto query);

        /// <summary>
        /// 获取转置后的翻译详情
        /// </summary>
        /// <param name="transKey">翻译键</param>
        /// <returns>转置后的翻译详情</returns>
        Task<TaktTransposedDto> GetTransposedDetailAsync(string transKey);

        /// <summary>
        /// 创建转置翻译数据
        /// </summary>
        /// <param name="input">转置数据创建对象</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateTransposedAsync(TaktTransposedDto input);

        /// <summary>
        /// 更新转置翻译数据
        /// </summary>
        /// <param name="input">转置数据更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateTransposedAsync(TaktTransposedDto input);
    }
}



