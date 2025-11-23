//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFormService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 表单服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 表单服务实现
/// </summary>
public class TaktFormService : TaktBaseService, ITaktFormService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    private ITaktRepository<TaktForm> FormRepository => _repositoryFactory.GetWorkflowRepository<TaktForm>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public TaktFormService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取表单定义列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页表单定义列表</returns>
    public async Task<TaktPagedResult<TaktFormDto>> GetListAsync(TaktFormQueryDto query)
    {
        try
        {
            var result = await FormRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktFormDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("Form.GetListFailed"), ex);
            throw new TaktException(L("Form.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>表单定义信息，如果不存在则返回null</returns>
    public async Task<TaktFormDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<TaktFormDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 根据键获取表单定义
    /// </summary>
    /// <param name="formKey">表单键</param>
    /// <returns>表单定义信息，如果不存在则返回null</returns>
    public async Task<TaktFormDto?> GetByKeyAsync(string formKey)
    {
        try
        {
            var entity = await FormRepository.GetFirstAsync(x => x.FormKey == formKey);
            if (entity == null)
                return null;

            return entity.Adapt<TaktFormDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据键获取表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建表单定义
    /// </summary>
    /// <param name="dto">表单定义创建信息</param>
    /// <returns>新创建的表单定义ID</returns>
    public async Task<long> CreateAsync(TaktFormCreateDto dto)
    {
        try
        {
            // 校验表单键是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormKey", dto.FormKey);
            // 校验表单名称是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", dto.FormName);

            var entity = dto.Adapt<TaktForm>();

            var id = await FormRepository.CreateAsync(entity);
            _logger.Info($"创建表单定义成功，ID: {id}, 键: {dto.FormKey}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 更新表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="dto">表单定义更新信息</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateAsync(long id, TaktFormUpdateDto dto)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            // 验证字段是否已存在（排除当前表单）
            if (entity.FormKey != dto.FormKey)
                await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormKey", dto.FormKey, id);
            if (entity.FormName != dto.FormName)
                await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", dto.FormName, id);

            dto.Adapt(entity);

            var result = await FormRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新表单定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除表单定义
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await FormRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除表单定义成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除表单定义
    /// </summary>
    /// <param name="ids">表单定义ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await FormRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 更新表单状态
    /// </summary>
    /// <param name="id">表单定义ID</param>
    /// <param name="status">新状态值</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, int status)
    {
        try
        {
            var entity = await FormRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Status = status;

            var result = await FormRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新表单状态成功，ID: {id}, 状态: {status}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新表单状态失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取当前用户的表单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页表单定义列表</returns>
    public async Task<TaktPagedResult<TaktFormDto>> GetCurrentAsync(long userId, TaktFormQueryDto query)
    {
        try
        {
            var expression = Expressionable.Create<TaktForm>()
                .AndIF(!string.IsNullOrEmpty(query.FormKey), x => x.FormKey.Contains(query.FormKey))
                .AndIF(!string.IsNullOrEmpty(query.FormName), x => x.FormName.Contains(query.FormName))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .And(x => x.CreateBy == _currentUser.UserName)
                .ToExpression();

            var result = await FormRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktFormDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktFormDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的表单列表失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取表单定义选项列表
    /// </summary>
    /// <returns>表单定义选项列表</returns>
    public async Task<List<TaktSelectOption>> GetOptionsAsync()
    {
        var forms = await FormRepository.AsQueryable()
            .Where(f => f.Status == 0 && f.IsDeleted == 0)  // 只获取正常状态且未删除的表单定义
            .OrderBy(f => f.FormName)
            .Select(f => new TaktSelectOption
            {
                DictLabel = f.FormName,
                DictValue = f.Id,
                ExtLabel = f.FormKey,
                ExtValue = f.Notes,
                OrderNum = 0,
                Status = f.Status
            })
            .ToListAsync();
        return forms;
    }

    /// <summary>
    /// 克隆表单定义
    /// </summary>
    /// <param name="dto">表单定义克隆信息</param>
    /// <returns>新表单定义ID</returns>
    public async Task<long> CloneAsync(TaktFormCreateDto dto)
    {
        try
        {
            // 校验表单键是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormKey", dto.FormKey);
            // 校验表单名称是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", dto.FormName);

            var entity = dto.Adapt<TaktForm>();

            var id = await FormRepository.CreateAsync(entity);
            _logger.Info($"克隆表单定义成功，ID: {id}, 键: {dto.FormKey}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"克隆表单定义失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel模板文件</returns>
    public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
    {
        var actualSheetName = sheetName ?? "Form";
        var actualFileName = fileName ?? "FormTemplate";
        return await TaktExcelHelper.GenerateTemplateAsync<TaktFormTemplateDto>(actualSheetName, actualFileName);
    }

    /// <summary>
    /// 导入表单数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
    {
        try
        {
            var actualSheetName = sheetName ?? "Form";
            var forms = await TaktExcelHelper.ImportAsync<TaktFormImportDto>(fileStream, actualSheetName);
            if (!forms.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var form in forms)
            {
                try
                {
                    if (string.IsNullOrEmpty(form.FormKey))
                    {
                        _logger.Warn("导入表单失败: 表单键不能为空");
                        fail++;
                        continue;
                    }

                    if (string.IsNullOrEmpty(form.FormName))
                    {
                        _logger.Warn("导入表单失败: 表单名称不能为空");
                        fail++;
                        continue;
                    }

                    // 校验表单键是否已存在
                    await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormKey", form.FormKey);
                    // 校验表单名称是否已存在
                    await TaktValidateUtils.ValidateFieldExistsAsync(FormRepository, "FormName", form.FormName);

                    var entity = form.Adapt<TaktForm>();
                    entity.CreateTime = DateTime.Now;
                    entity.CreateBy = _currentUser.UserName;
                    entity.Status = 0;

                    var result = await FormRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入表单失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入表单数据失败", ex);
            throw new TaktException($"导入表单数据失败: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// 导出表单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktFormQueryDto query, string? sheetName, string? fileName)
    {
        var list = await FormRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<TaktFormExportDto>>();
        
        // 使用传入的参数，如果为空则使用默认值
        var actualSheetName = sheetName ?? "Form";
        var actualFileName = fileName ?? "FormData";
        
        return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<TaktForm, bool>> QueryExpression(TaktFormQueryDto query)
    {
        return Expressionable.Create<TaktForm>()
            .AndIF(!string.IsNullOrEmpty(query.FormKey), x => x.FormKey.Contains(query.FormKey))
            .AndIF(!string.IsNullOrEmpty(query.FormName), x => x.FormName.Contains(query.FormName))
            .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .ToExpression();
    }
}

