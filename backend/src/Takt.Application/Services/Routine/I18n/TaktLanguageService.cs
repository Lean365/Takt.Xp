#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLanguageService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 语言服务实现类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.I18n
{
    /// <summary>
    /// 语言服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktLanguageService : TaktBaseService, ITaktLanguageService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取语言仓储
        /// </summary>
        private ITaktRepository<TaktLanguage> LanguageRepository => _repositoryFactory.GetBusinessRepository<TaktLanguage>();

        /// <summary>
        /// 获取翻译仓储
        /// </summary>
        private ITaktRepository<TaktTranslation> TranslationRepository => _repositoryFactory.GetBusinessRepository<TaktTranslation>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktLanguageService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取语言分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>语言分页列表</returns>
        public async Task<TaktPagedResult<TaktLanguageDto>> GetListAsync(TaktLanguageQueryDto? query)
        {
            query ??= new TaktLanguageQueryDto();

            var result = await LanguageRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktLanguageDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktLanguageDto>>()
            };
        }

        /// <summary>
        /// 获取语言详情
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>语言详情</returns>
        public async Task<TaktLanguageDto> GetByIdAsync(long LanguageId)
        {
            var language = await LanguageRepository.GetByIdAsync(LanguageId);
            if (language == null)
                throw new TaktException(L("Core.Language.NotFound", LanguageId));

            var languageDto = language.Adapt<TaktLanguageDto>();

            // 加载关联的翻译数据
            var translations = await TranslationRepository.GetListAsync(x => x.LangCode == language.LangCode);
            languageDto.TranslationList = translations.Adapt<List<TaktTranslationDto>>();

            return languageDto;
        }

        /// <summary>
        /// 获取语言选项列表
        /// </summary>
        /// <returns>语言选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var list = await LanguageRepository.GetListAsync(x => x.I18nStatus == 0 && x.IsDeleted == 0);
            return list.Select(lang => new TaktSelectOption
            {
                DictLabel = lang.LangName,
                DictValue = lang.LangCode,
                ExtLabel = lang.LangIcon,
                ExtValue = lang.OrderNum,
                TransKey = null,
                CssClass = 1,
                ListClass = 1,
                OrderNum = lang.OrderNum
            }).ToList();
        }

        /// <summary>
        /// 创建语言
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>语言ID</returns>
        public async Task<long> CreateAsync(TaktLanguageCreateDto input)
        {
            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangCode", input.LangCode);
            await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangName", input.LangName);
            await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangIcon", input.LangIcon);

            var language = input.Adapt<TaktLanguage>();
            var languageId = await LanguageRepository.CreateAsync(language);

            if (languageId > 0 && input.TranslationList != null && input.TranslationList.Any())
            {
                // 创建关联的翻译数据
                foreach (var translationDto in input.TranslationList)
                {
                    var translation = translationDto.Adapt<TaktTranslation>();
                    translation.LangCode = language.LangCode; // 确保使用正确的语言代码
                    await TranslationRepository.CreateAsync(translation);
                }
            }

            return languageId > 0 ? language.Id : throw new TaktException(L("Core.Language.CreateFailed"));
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktLanguageUpdateDto input)
        {
            var language = await LanguageRepository.GetByIdAsync(input.LanguageId)
                ?? throw new TaktException(L("Core.Language.NotFound", input.LanguageId));

            // 验证字段是否已存在
            if (language.LangCode != input.LangCode)
                await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangCode", input.LangCode, input.LanguageId);
            if (language.LangName != input.LangName)
                await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangName", input.LangName, input.LanguageId);
            if (language.LangIcon != input.LangIcon)
                await TaktValidateUtils.ValidateFieldExistsAsync(LanguageRepository, "LangIcon", input.LangIcon, input.LanguageId);

            input.Adapt(language);
            var updateResult = await LanguageRepository.UpdateAsync(language);

            if (updateResult > 0 && input.TranslationList != null)
            {
                // 删除原有的翻译数据
                await TranslationRepository.DeleteAsync(x => x.LangCode == language.LangCode);

                // 创建新的翻译数据
                if (input.TranslationList.Any())
                {
                    foreach (var translationDto in input.TranslationList)
                    {
                        var translation = translationDto.Adapt<TaktTranslation>();
                        translation.LangCode = language.LangCode; // 确保使用正确的语言代码
                        await TranslationRepository.CreateAsync(translation);
                    }
                }
            }

            return updateResult > 0;
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="LanguageId">语言ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long LanguageId)
        {
            var language = await LanguageRepository.GetByIdAsync(LanguageId)
                ?? throw new TaktException($"语言不存在: {LanguageId}");

            // 先删除关联的翻译数据
            await TranslationRepository.DeleteAsync(x => x.LangCode == language.LangCode);

            // 再删除语言数据
            return await LanguageRepository.DeleteAsync(LanguageId) > 0;
        }

        /// <summary>
        /// 批量删除语言
        /// </summary>
        /// <param name="LanguageIds">语言ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] LanguageIds)
        {
            return await LanguageRepository.DeleteRangeAsync(LanguageIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 更新语言状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktLanguageStatusDto input)
        {
            var language = await LanguageRepository.GetByIdAsync(input.LanguageId)
                ?? throw new TaktException(L("Core.Language.NotFound", input.LanguageId));

            language.I18nStatus = input.I18nStatus;
            return await LanguageRepository.UpdateAsync(language) > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktLanguageTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入语言数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var languages = await TaktExcelHelper.ImportAsync<TaktLanguageDto>(fileStream, sheetName);
            if (languages == null || !languages.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var language in languages)
            {
                try
                {
                    // 检查是否已存在相同的LangCode
                    var existingLanguage = await LanguageRepository.GetFirstAsync(x => x.LangCode == language.LangCode);
                    if (existingLanguage != null)
                    {
                        // 更新现有记录
                        language.Adapt(existingLanguage);
                        await LanguageRepository.UpdateAsync(existingLanguage);
                    }
                    else
                    {
                        // 创建新记录
                        await LanguageRepository.CreateAsync(language.Adapt<TaktLanguage>());
                    }
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.Language.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出语言数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktLanguageQueryDto query, string sheetName)
        {
            var list = await LanguageRepository.GetListAsync(QueryExpression(query));
            var (fileName, content) = await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktLanguageDto>>(), sheetName, L("Core.Language.ExportTitle"));
            return (fileName, content);
        }



        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktLanguage, bool>> QueryExpression(TaktLanguageQueryDto query)
        {
            return Expressionable.Create<TaktLanguage>()
                .AndIF(!string.IsNullOrEmpty(query.LangCode), x => x.LangCode.Contains(query.LangCode!))
                .AndIF(!string.IsNullOrEmpty(query.LangName), x => x.LangName.Contains(query.LangName!))
                .AndIF(query.I18nStatus != -1, x => x.I18nStatus == query.I18nStatus)
                .AndIF(query.IsDefault != -1, x => x.IsDefault == query.IsDefault)
                .AndIF(query.IsBuiltin != -1, x => x.IsBuiltin == query.IsBuiltin)
                .ToExpression();
        }
    }
}



