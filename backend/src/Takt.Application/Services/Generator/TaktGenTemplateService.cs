#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenTemplateService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成模板服务实现
//===================================================================

namespace Takt.Application.Services.Generator;
using Takt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Linq.Expressions;

/// <summary>
/// 代码生成模板服务实现
/// </summary>
public class TaktGenTemplateService : TaktBaseService, ITaktGenTemplateService
{
    /// <summary>
    /// 工厂仓储
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private ITaktRepository<TaktGenTemplate> TemplateRepository => _repositoryFactory.GetGeneratorRepository<TaktGenTemplate>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">模板仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktGenTemplateService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    #region 基础操作

    /// <summary>
    /// 获取分页模板列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTemplateDto>> GetListAsync(TaktGenTemplateQueryDto input)
    {
        // 使用独立的查询表达式方法
        var exp = QueryExpression(input);

        // 执行分页查询
        var result = await TemplateRepository.GetPagedListAsync(
            exp,
            input.PageIndex,
            input.PageSize,
            x => x.Id,
            OrderByType.Asc);

        // 返回分页结果
        return new TaktPagedResult<TaktGenTemplateDto>
        {
            Rows = result.Rows.Adapt<List<TaktGenTemplateDto>>(),
            TotalNum = result.TotalNum,
            PageIndex = result.PageIndex,
            PageSize = result.PageSize
        };
    }

    /// <summary>
    /// 根据ID获取模板信息
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>模板信息</returns>
    public async Task<TaktGenTemplateDto?> GetByIdAsync(long id)
    {
        var template = await TemplateRepository.GetByIdAsync(id);
        if (template == null)
        {
            return null;
        }

        return template.Adapt<TaktGenTemplateDto>();
    }

    /// <summary>
    /// 创建模板信息
    /// </summary>
    /// <param name="input">模板信息</param>
    /// <returns>创建结果</returns>
    public async Task<TaktGenTemplateDto> CreateAsync(TaktGenTemplateCreateDto input)
    {
        // 验证字段是否已存在
        await TaktValidateUtils.ValidateFieldExistsAsync(TemplateRepository, "TemplateName", input.TemplateName);

        var template = input.Adapt<TaktGenTemplate>();
        var result = await TemplateRepository.CreateAsync(template);
        if (result <= 0)
        {
            throw new TaktException(L("GenTemplate.CreateFailed"));
        }

        return template.Adapt<TaktGenTemplateDto>();
    }

    /// <summary>
    /// 更新模板信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的模板信息</returns>
    public async Task<TaktGenTemplateDto> UpdateAsync(TaktGenTemplateUpdateDto input)
    {
        var template = await TemplateRepository.GetByIdAsync(input.GenTemplateId)
            ?? throw new TaktException(L("GenTemplate.NotFound", input.GenTemplateId));

        // 验证字段是否已存在
        if (template.TemplateName != input.TemplateName)
        {
            await TaktValidateUtils.ValidateFieldExistsAsync(TemplateRepository, "TemplateName", input.TemplateName, input.GenTemplateId);
        }

        input.Adapt(template);
        var result = await TemplateRepository.UpdateAsync(template);
        if (result <= 0)
        {
            throw new TaktException(L("GenTemplate.UpdateFailed"));
        }

        return template.Adapt<TaktGenTemplateDto>();
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        var template = await TemplateRepository.GetByIdAsync(id);
        if (template == null)
        {
            throw new TaktException($"模板[{id}]不存在");
        }

        return await TemplateRepository.DeleteAsync(template) > 0;
    }

    /// <summary>
    /// 批量删除模板
    /// </summary>
    /// <param name="ids">模板ID集合</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || !ids.Any())
            throw new TaktException(L("GenTemplate.BatchDelete.Empty"));

        return await TemplateRepository.DeleteRangeAsync(ids.Cast<object>().ToList()) > 0;
    }

    /// <summary>
    /// 更新模板状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(TaktGenTemplateStatusDto input)
    {
        var template = await TemplateRepository.GetByIdAsync(input.GenTemplateId)
            ?? throw new TaktException(L("GenTemplate.NotFound", input.GenTemplateId));

        template.Status = input.Status;
        template.UpdateBy = _currentUser.UserName;
        template.UpdateTime = DateTime.Now;

        return await TemplateRepository.UpdateAsync(template) > 0;
    }

    #endregion 基础操作

    #region 模板操作

    /// <summary>
    /// 导入模板
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>返回导入成功和失败的数量</returns>
    public async Task<(int success, int fail)> ImportTemplatesAsync(Stream fileStream, string sheetName = "GenTemplate")
    {
        try
        {
            var templates = await TaktExcelHelper.ImportAsync<TaktGenTemplate>(fileStream, sheetName);
            if (templates == null || !templates.Any())
            {
                return (0, 0);
            }

            var success = 0;
            var fail = 0;

            foreach (var template in templates)
            {
                try
                {
                    // 验证模板名称是否已存在
                    var existTemplate = await TemplateRepository.GetFirstAsync(x => x.TemplateName == template.TemplateName);
                    if (existTemplate != null)
                    {
                        fail++;
                        continue;
                    }

                    template.CreateBy = _currentUser.UserName;
                    template.CreateTime = DateTime.Now;
                    template.UpdateBy = _currentUser.UserName;
                    template.UpdateTime = DateTime.Now;

                    var result = await TemplateRepository.CreateAsync(template);
                    if (result > 0)
                    {
                        success++;
                    }
                    else
                    {
                        fail++;
                    }
                }
                catch (Exception)
                {
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error(L("GenTemplate.Import.Failed"), ex);
            throw new TaktException(L("GenTemplate.Import.Failed"), ex);
        }
    }

    /// <summary>
    /// 导出模板
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public async Task<(string fileName, byte[] content)> ExportTemplatesAsync(TaktGenTemplateQueryDto query, string sheetName = "GenTemplate")
    {
        try
        {
            var data = await GetListAsync(new TaktGenTemplateQueryDto());
            return await TaktExcelHelper.ExportAsync(data.Rows, sheetName);
        }
        catch (Exception ex)
        {
            _logger.Error(L("GenTemplate.Export.Failed"), ex);
            throw new TaktException(L("GenTemplate.Export.Failed"), ex);
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "GenTemplate")
    {
        return await TaktExcelHelper.GenerateTemplateAsync<TaktGenTemplate>(sheetName);
    }

    #endregion 模板操作

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<TaktGenTemplate, bool>> QueryExpression(TaktGenTemplateQueryDto query)
    {
        return Expressionable.Create<TaktGenTemplate>()
            .AndIF(!string.IsNullOrEmpty(query.TemplateName), x => x.TemplateName.Contains(query.TemplateName!))
            .AndIF(query.TemplateCodeType.HasValue, x => x.TemplateCodeType == query.TemplateCodeType.Value)
            .AndIF(query.TemplateOrmType.HasValue, x => x.TemplateOrmType == query.TemplateOrmType.Value)
            .AndIF(query.TemplateCategory.HasValue, x => x.TemplateCategory == query.TemplateCategory.Value)
            .AndIF(query.Status.HasValue && query.Status.Value != -1, x => x.Status == query.Status.Value)
            .ToExpression();
    }
}

