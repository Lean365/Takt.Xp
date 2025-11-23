//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTranslationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 翻译服务实现类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.I18n
{
    /// <summary>
    /// 翻译服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktTranslationService : TaktBaseService, ITaktTranslationService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取翻译仓储
        /// </summary>
        private ITaktRepository<TaktTranslation> TranslationRepository => _repositoryFactory.GetBusinessRepository<TaktTranslation>();

        /// <summary>
        /// 获取语言仓储
        /// </summary>
        private ITaktRepository<TaktLanguage> LanguageRepository => _repositoryFactory.GetBusinessRepository<TaktLanguage>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktTranslationService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取翻译分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>翻译分页列表</returns>
        public async Task<TaktPagedResult<TaktTranslationDto>> GetListAsync(TaktTranslationQueryDto query)
        {
            query ??= new TaktTranslationQueryDto();

            var result = await TranslationRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktTranslationDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktTranslationDto>>()
            };
        }

        /// <summary>
        /// 获取翻译详情
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>翻译详情</returns>
        public async Task<TaktTranslationDto> GetByIdAsync(long TranslationId)
        {
            var translation = await TranslationRepository.GetByIdAsync(TranslationId);
            return translation == null ? throw new TaktException(L("Core.Translation.NotFound", TranslationId)) : translation.Adapt<TaktTranslationDto>();
        }

        /// <summary>
        /// 创建翻译
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>翻译ID</returns>
        public async Task<long> CreateAsync(TaktTranslationCreateDto input)
        {
            // 验证语言是否存在
            var language = await LanguageRepository.GetFirstAsync(x => x.LangCode == input.LangCode);
            if (language == null)
                throw new TaktException($"语言代码 {input.LangCode} 不存在");

            // 验证字段组合是否已存在
            var fieldValues = new Dictionary<string, string>
            {
                { "LangCode", input.LangCode },
                { "TransKey", input.TransKey }
            };
            await TaktValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

            var translation = input.Adapt<TaktTranslation>();

            return await TranslationRepository.CreateAsync(translation) > 0 ? translation.Id : throw new TaktException(L("Core.Translation.CreateFailed"));
        }

        /// <summary>
        /// 更新翻译
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktTranslationUpdateDto input)
        {
            var translation = await TranslationRepository.GetByIdAsync(input.TranslationId)
                ?? throw new TaktException(L("Core.Translation.NotFound", input.TranslationId));

            // 验证语言是否存在
            var language = await LanguageRepository.GetFirstAsync(x => x.LangCode == input.LangCode);
            if (language == null)
                throw new TaktException($"语言代码 {input.LangCode} 不存在");

            // 验证字段组合是否已存在
            if (translation.LangCode != input.LangCode || translation.TransKey != input.TransKey)
            {
                var fieldValues = new Dictionary<string, string>
                {
                    { "LangCode", input.LangCode },
                    { "TransKey", input.TransKey }
                };
                await TaktValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues, input.TranslationId);
            }

            input.Adapt(translation);
            return await TranslationRepository.UpdateAsync(translation) > 0;
        }

        /// <summary>
        /// 删除翻译
        /// </summary>
        /// <param name="TranslationId">翻译ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long TranslationId)
        {
            var translation = await TranslationRepository.GetByIdAsync(TranslationId)
                ?? throw new TaktException(L("Core.Translation.NotFound", TranslationId));

            return await TranslationRepository.DeleteAsync(TranslationId) > 0;
        }

        /// <summary>
        /// 批量删除翻译
        /// </summary>
        /// <param name="TranslationIds">翻译ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] TranslationIds)
        {
            if (TranslationIds == null || TranslationIds.Length == 0) return false;
            return await TranslationRepository.DeleteRangeAsync(TranslationIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入翻译数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var translations = await TaktExcelHelper.ImportAsync<TaktTranslationDto>(fileStream, sheetName);
            if (translations == null || !translations.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var translation in translations)
            {
                try
                {
                    // 验证语言是否存在
                    var language = await LanguageRepository.GetFirstAsync(x => x.LangCode == translation.LangCode);
                    if (language == null)
                    {
                        _logger.Error($"语言代码 {translation.LangCode} 不存在");
                        fail++;
                        continue;
                    }

                    // 验证字段组合是否已存在
                    var fieldValues = new Dictionary<string, string>
                    {
                        { "LangCode", translation.LangCode },
                        { "TransKey", translation.TransKey }
                    };
                    await TaktValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

                    // 创建新记录
                    await TranslationRepository.CreateAsync(translation.Adapt<TaktTranslation>());
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.Translation.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出翻译数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktTranslationQueryDto query, string sheetName)
        {
            var list = await TranslationRepository.GetListAsync(QueryExpression(query));
            return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktTranslationDto>>(), sheetName, L("Core.Translation.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktTranslationTemplateDto>(sheetName);
        }

        /// <summary>
        /// 获取指定语言的翻译值
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transKey">翻译键</param>
        /// <returns>翻译值</returns>
        public async Task<string> GetTransValueAsync(string langCode, string transKey)
        {
            var translation = await TranslationRepository.GetFirstAsync(x => x.LangCode == langCode && x.TransKey == transKey);
            return translation?.TransValue ?? string.Empty;
        }

        /// <summary>
        /// 获取指定类型的翻译列表
        /// </summary>
        /// <param name="langCode">语言代码</param>
        /// <param name="transType">翻译类型，0表示获取所有翻译</param>
        /// <returns>翻译列表</returns>
        public async Task<List<TaktTranslationDto>> GetListByModuleAsync(string langCode, int transType)
        {
            var list = await TranslationRepository.GetListAsync(x => x.LangCode == langCode && (transType == 0 || x.TransType == transType));
            return list.Adapt<List<TaktTranslationDto>>();
        }

        /// <summary>
        /// 获取转置后的翻译数据(分页)
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转置后的翻译数据,包含分页信息</returns>
        public async Task<TaktPagedResult<TaktTransposedDto>> GetTransposedDataAsync(TaktTransposedQueryDto query)
        {
            // 1. 获取所有启用的语言代码
            var languages = await LanguageRepository.GetListAsync(x => x.I18nStatus == 0);
            var langCodes = languages.Select(x => x.LangCode).ToList();
            _logger.Info($"获取到的语言代码列表: {string.Join(", ", langCodes)}");

            // 2. 构建查询条件
            var exp = Expressionable.Create<TaktTranslation>()
                .AndIF(!string.IsNullOrEmpty(query.TransKey), x => x.TransKey!.Contains(query.TransKey))
                .AndIF(!string.IsNullOrEmpty(query.TransValue), x => x.TransValue!.Contains(query.TransValue))
                .AndIF(query.TransType != -1, x => x.TransType == query.TransType)
                .ToExpression();

            // 3. 获取所有翻译数据
            var allTranslations = await TranslationRepository.GetListAsync(exp);
            _logger.Info($"查询到的翻译数据数量: {allTranslations.Count}");

            // 4. 按翻译键分组并去重
            var groupedTranslations = allTranslations
                .GroupBy(x => x.TransKey)
                .Select(g => g.First())
                .OrderBy(x => x.TransKey)
                .ToList();

            var totalCount = groupedTranslations.Count;
            _logger.Info($"翻译键数量: {totalCount}");

            // 5. 分页处理
            var pagedTranslations = groupedTranslations
                .Skip((query.PageIndex - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToList();

            // 6. 获取这些翻译键对应的所有翻译数据
            var transKeys = pagedTranslations.Select(x => x.TransKey).ToList();
            var transKeyTranslations = allTranslations.Where(x => transKeys.Contains(x.TransKey)).ToList();
            _logger.Info($"获取到的翻译数据数量: {transKeyTranslations.Count}");

            // 7. 转置数据
            var transposedRows = new List<TaktTransposedDto>();
            foreach (var translation in pagedTranslations)
            {
                var dto = new TaktTransposedDto
                {
                    TransKey = translation.TransKey,
                    TransType = translation.TransType,
                    OrderNum = translation.OrderNum,
                    Remark = translation.Remark,
                    CreateBy = translation.CreateBy,
                    CreateTime = translation.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    UpdateBy = translation.UpdateBy,
                    UpdateTime = translation.UpdateTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                    Translations = new Dictionary<string, TaktTranslationLangDto>()
                };

                // 为每个语言代码添加翻译值
                foreach (var langCode in langCodes)
                {
                    var langTranslation = transKeyTranslations.FirstOrDefault(x => x.TransKey == translation.TransKey && x.LangCode == langCode);
                    dto.Translations[langCode] = new TaktTranslationLangDto
                    {
                        TranslationId = langTranslation?.Id ?? 0,
                        LangCode = langCode,
                        TransValue = langTranslation?.TransValue ?? string.Empty,
                    };
                }

                transposedRows.Add(dto);
            }

            return new TaktPagedResult<TaktTransposedDto>
            {
                TotalNum = totalCount,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = transposedRows
            };
        }

        /// <summary>
        /// 获取转置后的翻译详情
        /// </summary>
        /// <param name="transKey">翻译键</param>
        /// <returns>转置后的翻译详情</returns>
        public async Task<TaktTransposedDto> GetTransposedDetailAsync(string transKey)
        {
            if (string.IsNullOrEmpty(transKey))
            {
                throw new TaktException(L("Core.Translation.TransKeyRequired"));
            }

            // 1. 获取所有启用的语言代码
            var languages = await LanguageRepository.GetListAsync(x => x.I18nStatus == 0);
            var langCodes = languages.Select(x => x.LangCode).ToList();

            // 2. 获取指定翻译键的所有翻译
            var translations = await TranslationRepository.GetListAsync(x => x.TransKey == transKey);
            if (!translations.Any())
            {
                throw new TaktException(L("Core.Translation.NotFound", transKey));
            }

            // 3. 构建转置详情
            var firstTranslation = translations.First();
            var detail = new TaktTransposedDto
            {
                TransKey = transKey,
                TransType = firstTranslation.TransType,
                OrderNum = firstTranslation.OrderNum,
                Remark = firstTranslation.Remark,
                CreateBy = firstTranslation.CreateBy,
                CreateTime = firstTranslation.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                UpdateBy = firstTranslation.UpdateBy,
                UpdateTime = firstTranslation.UpdateTime?.ToString("yyyy-MM-dd HH:mm:ss"),
                Translations = new Dictionary<string, TaktTranslationLangDto>()
            };

            // 4. 添加各语言的翻译信息
            foreach (var langCode in langCodes)
            {
                var translation = translations.FirstOrDefault(x => x.LangCode == langCode);
                detail.Translations[langCode] = new TaktTranslationLangDto
                {
                    TranslationId = translation?.Id ?? 0,
                    LangCode = langCode,
                    TransValue = translation?.TransValue ?? string.Empty,
                };
            }

            return detail;
        }

        /// <summary>
        /// 创建转置翻译数据
        /// </summary>
        /// <param name="input">转置数据创建对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> CreateTransposedAsync(TaktTransposedDto input)
        {
            if (input == null || string.IsNullOrEmpty(input.TransKey))
            {
                throw new TaktException(L("Core.Translation.InvalidInput"));
            }

            try
            {
                // 获取所有启用的语言
                var languages = await LanguageRepository.GetListAsync(x => x.I18nStatus == 0);
                var langCodes = languages.Select(x => x.LangCode).ToList();

                // 验证每个语言的翻译数据
                foreach (var langCode in langCodes)
                {
                    if (input.Translations.TryGetValue(langCode, out var translation))
                    {
                        // 验证字段组合是否已存在
                        var fieldValues = new Dictionary<string, string>
                        {
                            { "LangCode", langCode },
                            { "TransKey", input.TransKey }
                        };
                        await TaktValidateUtils.ValidateFieldsExistsAsync(TranslationRepository, fieldValues);

                        // 创建翻译记录，不管 TransValue 是否有值
                        var translationEntity = new TaktTranslation
                        {
                            LangCode = langCode,
                            TransKey = input.TransKey,
                            TransValue = translation.TransValue ?? string.Empty,
                            TransType = input.TransType,
                            Remark = input.Remark,
                            CreateBy = _currentUser.UserName,
                            CreateTime = DateTime.Now
                        };

                        await TranslationRepository.CreateAsync(translationEntity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"创建转置翻译数据失败: {ex.Message}", ex);
                throw new TaktException(L("Core.Translation.CreateFailed"));
            }
        }

        /// <summary>
        /// 更新转置翻译数据
        /// </summary>
        /// <param name="input">转置数据更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateTransposedAsync(TaktTransposedDto input)
        {
            if (input == null || string.IsNullOrEmpty(input.TransKey))
            {
                throw new TaktException(L("Core.Translation.InvalidInput"));
            }

            try
            {
                // 获取所有启用的语言
                var languages = await LanguageRepository.GetListAsync(x => x.I18nStatus == 0);

                // 处理每个语言的翻译
                foreach (var language in languages)
                {
                    var langCode = language.LangCode;
                    if (input.Translations.TryGetValue(langCode, out var translation))
                    {
                        // 根据 LangCode 和 TransKey 查询已存在的翻译记录
                        var existingTranslation = await TranslationRepository.GetFirstAsync(x =>
                            x.LangCode == langCode && x.TransKey == input.TransKey);

                        if (existingTranslation != null)
                        {
                            // 更新已存在的记录
                            existingTranslation.TransValue = translation.TransValue ?? string.Empty;
                            existingTranslation.Remark = input.Remark;
                            existingTranslation.UpdateBy = _currentUser.UserName;
                            existingTranslation.UpdateTime = DateTime.Now;
                            await TranslationRepository.UpdateAsync(existingTranslation);
                        }
                        else
                        {
                            // 创建不存在的记录
                            var newTranslation = new TaktTranslation
                            {
                                LangCode = langCode,
                                TransKey = input.TransKey,
                                TransValue = translation.TransValue ?? string.Empty,
                                TransType = input.TransType,
                                Remark = input.Remark,
                                CreateBy = _currentUser.UserName,
                                CreateTime = DateTime.Now
                            };

                            await TranslationRepository.CreateAsync(newTranslation);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新转置翻译数据失败: {ex.Message}", ex);
                throw new TaktException(L("Core.Translation.UpdateFailed"));
            }
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktTranslation, bool>> QueryExpression(TaktTranslationQueryDto query)
        {
            return Expressionable.Create<TaktTranslation>()
                .AndIF(!string.IsNullOrEmpty(query.LangCode), x => x.LangCode == query.LangCode)
                .AndIF(!string.IsNullOrEmpty(query.TransKey), x => x.TransKey.Contains(query.TransKey))
                .AndIF(!string.IsNullOrEmpty(query.TransValue), x => x.TransValue.Contains(query.TransValue))
                .AndIF(query.TransType.HasValue && query.TransType != -1, x => x.TransType == query.TransType)
                .ToExpression();
        }
    }
}



