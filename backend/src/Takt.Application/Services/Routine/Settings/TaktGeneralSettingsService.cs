#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneralSettingsService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 通用设置服务实现 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Routine.Settings;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.Settings
{
    /// <summary>
    /// 通用设置服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class TaktGeneralSettingsService : TaktBaseService, ITaktGeneralSettingsService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取设置仓储
        /// </summary>
        private ITaktRepository<TaktGeneralSettings> GeneralSettingsRepository => _repositoryFactory.GetBusinessRepository<TaktGeneralSettings>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktGeneralSettingsService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取系统设置分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>系统设置分页列表</returns>
        public async Task<TaktPagedResult<TaktGeneralSettingsDto>> GetListAsync(TaktGeneralSettingsQueryDto? query)
        {
            query ??= new TaktGeneralSettingsQueryDto();

            _logger.Info($"查询系统设置列表，参数：SettingsName={query.SettingsName}, SettingsKey={query.SettingsKey}, SettingsValue={query.SettingsValue}, IsBuiltin={query.IsBuiltin}, Status={query.Status}, IsEncrypted={query.IsEncrypted}");

            var result = await GeneralSettingsRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info($"查询系统设置列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktGeneralSettingsDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktGeneralSettingsDto>>()
            };
        }

        /// <summary>
        /// 获取系统设置详情
        /// </summary>
        /// <param name="settingsId">设置ID</param>
        /// <returns>系统设置详情</returns>
        public async Task<TaktGeneralSettingsDto> GetByIdAsync(long settingsId)
        {
            var settings = await GeneralSettingsRepository.GetByIdAsync(settingsId);
            return settings == null ? throw new TaktException(L("Core.Config.NotFound", settingsId)) : settings.Adapt<TaktGeneralSettingsDto>();
        }

        /// <summary>
        /// 创建系统设置
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>设置ID</returns>
        public async Task<long> CreateAsync(TaktGeneralSettingsCreateDto input)
        {
            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsName", input.SettingsName);
            await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsKey", input.SettingsKey);
            await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsValue", input.SettingsValue);

            var settings = input.Adapt<TaktGeneralSettings>();
            return await GeneralSettingsRepository.CreateAsync(settings) > 0 ? settings.Id : throw new TaktException(L("Core.Config.CreateFailed"));
        }

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktGeneralSettingsUpdateDto input)
        {
            var settings = await GeneralSettingsRepository.GetByIdAsync(input.SettingsId)
                ?? throw new TaktException(L("Core.Config.NotFound", input.SettingsId));

            // 验证字段是否已存在
            if (settings.SettingsName != input.SettingsName)
                await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsName", input.SettingsName, input.SettingsId);
            if (settings.SettingsKey != input.SettingsKey)
                await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsKey", input.SettingsKey, input.SettingsId);
            if (settings.SettingsValue != input.SettingsValue)
                await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsValue", input.SettingsValue, input.SettingsId);

            input.Adapt(settings);
            return await GeneralSettingsRepository.UpdateAsync(settings) > 0;
        }

        /// <summary>
        /// 删除系统设置
        /// </summary>
        /// <param name="settingsId">设置ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long settingsId)
        {
            var settings = await GeneralSettingsRepository.GetByIdAsync(settingsId)
                ?? throw new TaktException(L("Core.Config.NotFound", settingsId));

            if (settings.IsBuiltin == 1)
                throw new TaktException(L("Core.Config.CannotDeleteBuiltin"));

            return await GeneralSettingsRepository.DeleteAsync(settingsId) > 0;
        }

        /// <summary>
        /// 批量删除系统设置
        /// </summary>
        /// <param name="settingsIds">设置ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] settingsIds)
        {
            if (settingsIds == null || settingsIds.Length == 0) return false;
            return await GeneralSettingsRepository.DeleteRangeAsync(settingsIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 更新设置状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktGeneralSettingsStatusDto input)
        {
            var settings = await GeneralSettingsRepository.GetByIdAsync(input.SettingsId)
                ?? throw new TaktException(L("Core.Config.NotFound", input.SettingsId));

            settings.Status = input.Status;
            return await GeneralSettingsRepository.UpdateAsync(settings) > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktGeneralSettingsImportDto>(sheetName);
        }

        /// <summary>
        /// 导入系统设置数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            _logger.Info($"开始导入系统设置数据，工作表名称：{sheetName}");

            var settingss = await TaktExcelHelper.ImportAsync<TaktGeneralSettingsImportDto>(fileStream, sheetName);
            if (settingss == null || !settingss.Any())
            {
                _logger.Warn("导入的Excel文件中没有找到任何设置数据");
                return (0, 0);
            }

            _logger.Info($"成功从Excel文件中读取到 {settingss.Count()} 条设置数据");

            var (success, fail) = (0, 0);
            foreach (var settings in settingss)
            {
                try
                {
                    _logger.Info($"正在处理设置：{settings.SettingsName} (Key: {settings.SettingsKey})");

                    // 验证字段是否已存在
                    await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsName", settings.SettingsName);
                    await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsKey", settings.SettingsKey);
                    await TaktValidateUtils.ValidateFieldExistsAsync(GeneralSettingsRepository, "SettingsValue", settings.SettingsValue);

                    var entity = settings.Adapt<TaktGeneralSettings>();
                    await GeneralSettingsRepository.CreateAsync(entity);
                    success++;
                    _logger.Info($"成功导入设置：{settings.SettingsName}");
                }
                catch (Exception ex)
                {
                    fail++;
                    _logger.Error($"导入设置失败：{settings.SettingsName}, 错误：{ex.Message}");
                }
            }

            _logger.Info($"设置导入完成，成功：{success}，失败：{fail}");
            return (success, fail);
        }

        /// <summary>
        /// 导出系统设置数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出结果</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktGeneralSettingsQueryDto query, string sheetName = "GeneralSettings")
        {
            var list = await GeneralSettingsRepository.GetListAsync(QueryExpression(query));
            return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktGeneralSettingsExportDto>>(), sheetName, L("Core.Config.ExportTitle"));
        }


        /// <summary>
        /// 根据设置键获取设置值
        /// </summary>
        /// <param name="settingsKey">设置键</param>
        /// <returns>设置值</returns>
        public async Task<string?> GetSettingsValueAsync(string settingsKey)
        {
            var settings = await GeneralSettingsRepository.GetFirstAsync(x => x.SettingsKey == settingsKey && x.Status == 0);
            return settings?.SettingsValue;
        }

        /// <summary>
        /// 设置设置值
        /// </summary>
        /// <param name="settingsKey">设置键</param>
        /// <param name="settingsValue">设置值</param>
        /// <returns>是否成功</returns>
        public async Task SetSettingsValueAsync(string settingsKey, string settingsValue)
        {
            var settings = await GeneralSettingsRepository.GetFirstAsync(x => x.SettingsKey == settingsKey);
            if (settings == null)
            {
                settings = new TaktGeneralSettings
                {
                    SettingsKey = settingsKey,
                    SettingsValue = settingsValue,
                    SettingsName = settingsKey,
                    Status = 0,
                    IsBuiltin = 0,
                    IsEncrypted = 0,
                    OrderNum = 0
                };
                await GeneralSettingsRepository.CreateAsync(settings);
            }
            else
            {
                settings.SettingsValue = settingsValue;
                await GeneralSettingsRepository.UpdateAsync(settings);
            }
        }

        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <returns>设置字典</returns>
        public async Task<Dictionary<string, string>> GetAllConfigsAsync()
        {
            var settingss = await GeneralSettingsRepository.GetListAsync(x => x.Status == 0);
            return settingss.Where(x => x.SettingsKey != null && x.SettingsValue != null)
                .ToDictionary(x => x.SettingsKey!, x => x.SettingsValue!);
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktGeneralSettings, bool>> QueryExpression(TaktGeneralSettingsQueryDto query)
        {
            return Expressionable.Create<TaktGeneralSettings>()
                .AndIF(!string.IsNullOrEmpty(query.SettingsName), x => x.SettingsName!.Contains(query.SettingsName!))
                .AndIF(!string.IsNullOrEmpty(query.SettingsKey), x => x.SettingsKey!.Contains(query.SettingsKey!))
                .AndIF(!string.IsNullOrEmpty(query.SettingsValue), x => x.SettingsValue!.Contains(query.SettingsValue!))
                .AndIF(query.IsBuiltin != -1, x => x.IsBuiltin == query.IsBuiltin)
                .AndIF(query.Status != -1, x => x.Status == query.Status)
                .AndIF(query.IsEncrypted != -1, x => x.IsEncrypted == query.IsEncrypted)
                .ToExpression();
        }
    }
}



