#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenConfigService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 生成配置服务实现类
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Generator;

/// <summary>
/// 生成配置服务实现类
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-01-22
/// </remarks>
public class TaktGenConfigService : TaktBaseService, ITaktGenConfigService
{
    /// <summary>
    /// 工厂仓储
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private ITaktRepository<TaktGenConfig> ConfigRepository => _repositoryFactory.GetGeneratorRepository<TaktGenConfig>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">配置仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktGenConfigService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取配置分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>配置分页列表</returns>
    public async Task<TaktPagedResult<TaktGenConfigDto>> GetListAsync(TaktGenConfigQueryDto query)
    {
        query ??= new TaktGenConfigQueryDto();

        var result = await ConfigRepository.GetPagedListAsync(
            QueryExpression(query),
            query.PageIndex,
            query.PageSize,
            x => x.Id,
            OrderByType.Asc);

        return new TaktPagedResult<TaktGenConfigDto>
        {
            TotalNum = result.TotalNum,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize,
            Rows = result.Rows.Adapt<List<TaktGenConfigDto>>()
        };
    }

    /// <summary>
    /// 获取配置详情
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>配置详情</returns>
    public async Task<TaktGenConfigDto?> GetByIdAsync(long id)
    {
        var config = await ConfigRepository.GetByIdAsync(id);
        return config?.Adapt<TaktGenConfigDto>();
    }

    /// <summary>
    /// 创建配置
    /// </summary>
    /// <param name="input">创建对象</param>
    /// <returns>配置信息</returns>
    public async Task<TaktGenConfigDto> CreateAsync(TaktGenConfigCreateDto input)
    {
        // 验证字段是否已存在
        await TaktValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "GenConfigName", input.GenConfigName);

        var config = input.Adapt<TaktGenConfig>();
        var result = await ConfigRepository.CreateAsync(config);
        if (result <= 0)
            throw new TaktException(L("Generator.Config.CreateFailed"));

        return config.Adapt<TaktGenConfigDto>();
    }

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="input">更新对象</param>
    /// <returns>更新后的配置信息</returns>
    public async Task<TaktGenConfigDto> UpdateAsync(TaktGenConfigUpdateDto input)
    {
        var config = await ConfigRepository.GetByIdAsync(input.GenConfigId)
            ?? throw new TaktException(L("Generator.Config.NotFound", input.GenConfigId));

        // 验证字段是否已存在
        if (config.GenConfigName != input.GenConfigName)
            await TaktValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "GenConfigName", input.GenConfigName, input.GenConfigId);

        input.Adapt(config);
        var result = await ConfigRepository.UpdateAsync(config);
        if (result <= 0)
            throw new TaktException(L("Generator.Config.UpdateFailed"));

        return config.Adapt<TaktGenConfigDto>();
    }

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var config = await ConfigRepository.GetByIdAsync(id)
            ?? throw new TaktException(L("Generator.Config.NotFound", id));

        return await ConfigRepository.DeleteAsync(id) > 0;
    }

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0) return false;
        return await ConfigRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 更新生成配置状态
    /// </summary>
    /// <param name="input">状态更新参数</param>
    /// <returns>是否更新成功</returns>
    public async Task<bool> UpdateStatusAsync(TaktGenConfigStatusDto input)
    {
        var config = await ConfigRepository.GetByIdAsync(input.GenConfigId)
            ?? throw new TaktException(L("Generator.Config.NotFound", input.GenConfigId));
        config.Status = input.Status;
        var result = await ConfigRepository.UpdateAsync(config);
        return result > 0;
    }

    /// <summary>
    /// 导入配置
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportConfigsAsync(Stream fileStream, string sheetName = "TaktGenConfig")
    {
        var configs = await TaktExcelHelper.ImportAsync<TaktGenConfigDto>(fileStream, sheetName);
        if (configs == null || !configs.Any()) return (0, 0);

        var (success, fail) = (0, 0);
        foreach (var config in configs)
        {
            try
            {
                // 验证字段是否已存在
                await TaktValidateUtils.ValidateFieldExistsAsync(ConfigRepository, "GenConfigName", config.GenConfigName);

                await ConfigRepository.CreateAsync(config.Adapt<TaktGenConfig>());
                success++;
            }
            catch
            {
                fail++;
            }
        }

        return (success, fail);
    }

    /// <summary>
    /// 导出配置
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导出的Excel文件内容</returns>
    public async Task<(string fileName, byte[] content)> ExportConfigsAsync(TaktGenConfigQueryDto query, string sheetName = "Config")
    {
        try
        {
            var list = await ConfigRepository.GetListAsync(QueryExpression(query));
            return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktGenConfigDto>>(), sheetName, L("Generator.Config.ExportTitle"));
        }
        catch (Exception ex)
        {
            _logger.Error(L("Generator.Config.ExportFailed", ex.Message), ex);
            throw new TaktException(L("Generator.Config.ExportFailed"));
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "GenConfig")
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktGenConfigDto>(sheetName);
    }


    /// <summary>
    /// 获取生成配置选项列表（用于下拉选择）
    /// </summary>
    /// <returns>生成配置选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var configs = await ConfigRepository.GetListAsync(_ => true);
        return configs.Select(c => new TaktSelectOption
        {
            DictLabel = c.GenConfigName,
            DictValue = c.Id
        }).ToList();
    }

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<TaktGenConfig, bool>> QueryExpression(TaktGenConfigQueryDto query)
    {
        return Expressionable.Create<TaktGenConfig>()
            .AndIF(!string.IsNullOrEmpty(query.GenConfigName), x => x.GenConfigName.Contains(query.GenConfigName!))
            .AndIF(!string.IsNullOrEmpty(query.ProjectName), x => x.ProjectName.Contains(query.ProjectName!))
            .AndIF(!string.IsNullOrEmpty(query.ModuleName), x => x.ModuleName.Contains(query.ModuleName!))
            .AndIF(!string.IsNullOrEmpty(query.BusinessName), x => x.BusinessName.Contains(query.BusinessName!))
            .AndIF(!string.IsNullOrEmpty(query.FunctionName), x => x.FunctionName.Contains(query.FunctionName!))
            .AndIF(query.GenMethod.HasValue && query.GenMethod.Value != -1, x => x.GenMethod == query.GenMethod.Value)
            .AndIF(query.Status.HasValue && query.Status.Value != -1, x => x.Status == query.Status.Value)
            .ToExpression();
    }

}




