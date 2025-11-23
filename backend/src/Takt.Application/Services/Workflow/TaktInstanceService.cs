//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例服务实现
//===================================================================


using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流实例服务实现
/// </summary>
public class TaktInstanceService : TaktBaseService, ITaktInstanceService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流实例仓储
    /// </summary>
    private ITaktRepository<TaktInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<TaktInstance>();

    /// <summary>
    /// 工作流实例转换仓储
    /// </summary>
    private ITaktRepository<TaktInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceTrans>();

    /// <summary>
    /// 工作流实例操作仓储
    /// </summary>
    private ITaktRepository<TaktInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceOper>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public TaktInstanceService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取工作流实例列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页工作流实例列表</returns>
    public async Task<TaktPagedResult<TaktInstanceDto>> GetListAsync(TaktInstanceQueryDto query)
    {
        try
        {
            var result = await InstanceRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取工作流实例列表失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 根据ID获取工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>工作流实例信息，如果不存在则返回null</returns>
    public async Task<TaktInstanceDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<TaktInstanceDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 根据业务键获取工作流实例
    /// </summary>
    /// <param name="businessKey">业务键</param>
    /// <returns>工作流实例信息，如果不存在则返回null</returns>
    public async Task<TaktInstanceDto?> GetByBusinessKeyAsync(string businessKey)
    {
        try
        {
            var entity = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == businessKey);
            if (entity == null)
                return null;

            return entity.Adapt<TaktInstanceDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据业务键获取工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建工作流实例
    /// </summary>
    /// <param name="dto">工作流实例创建信息</param>
    /// <returns>新创建的工作流实例ID</returns>
    public async Task<long> CreateAsync(TaktInstanceCreateDto dto)
    {
        try
        {
            // 检查业务键是否已存在
            var existing = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == dto.BusinessKey);
            if (existing != null)
            {
                throw new InvalidOperationException($"业务键 '{dto.BusinessKey}' 已存在");
            }

            var entity = dto.Adapt<TaktInstance>();
            entity.StartTime = DateTime.Now;

            var id = await InstanceRepository.CreateAsync(entity);
            _logger.Info($"创建工作流实例成功，ID: {id}, 业务键: {dto.BusinessKey}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 更新工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="dto">工作流实例更新信息</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateAsync(long id, TaktInstanceUpdateDto dto)
    {
        try
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            dto.Adapt(entity);

            var result = await InstanceRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新工作流实例成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除工作流实例
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await InstanceRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除工作流实例成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除工作流实例
    /// </summary>
    /// <param name="ids">工作流实例ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await InstanceRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 更新工作流实例状态
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="status">新状态值</param>
    /// <param name="reason">状态变更原因</param>
    /// <returns>更新是否成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, int status, string? reason = null)
    {
        try
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Status = status;

            // 如果状态为已完成，设置结束时间
            if (status == 2) // 已完成
            {
                entity.EndTime = DateTime.Now;
            }

            var result = await InstanceRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"更新工作流实例状态成功，ID: {id}, 状态: {status}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"更新工作流实例状态失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 设置工作流实例变量
    /// </summary>
    /// <param name="id">工作流实例ID</param>
    /// <param name="variables">变量字典</param>
    /// <returns>设置是否成功</returns>
    public async Task<bool> SetVariablesAsync(long id, Dictionary<string, object> variables)
    {
        try
        {
            var entity = await InstanceRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.Variables = JsonConvert.SerializeObject(variables);

            var result = await InstanceRepository.UpdateAsync(entity);
            if (result > 0)
            {
                _logger.Info($"设置工作流实例变量成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"设置工作流实例变量失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取我的工作流实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页工作流实例列表</returns>
    public async Task<TaktPagedResult<TaktInstanceDto>> GetMyInstancesAsync(long userId, TaktInstanceQueryDto query)
    {
        try
        {
            var expression = QueryExpression(query, x => x.InitiatorId == userId);

            var result = await InstanceRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的工作流实例列表失败: {ex.Message}", ex);
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
        var actualSheetName = sheetName ?? "Instance";
        var actualFileName = fileName ?? "工作流实例模板";
        return await TaktExcelHelper.GenerateTemplateAsync<TaktInstanceTemplateDto>(actualSheetName, actualFileName);
    }

    /// <summary>
    /// 导入工作流实例数据
    /// </summary>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入结果</returns>
    public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
    {
        try
        {
            var actualSheetName = sheetName ?? "Instance";
            var instances = await TaktExcelHelper.ImportAsync<TaktInstanceImportDto>(fileStream, actualSheetName);
            if (!instances.Any())
                return (0, 0);

            int success = 0, fail = 0;

            foreach (var instance in instances)
            {
                try
                {
                    if (string.IsNullOrEmpty(instance.BusinessKey))
                    {
                        _logger.Warn("导入工作流实例失败: 业务键不能为空");
                        fail++;
                        continue;
                    }

                    // 校验业务键是否已存在
                    await TaktValidateUtils.ValidateFieldExistsAsync(InstanceRepository, "BusinessKey", instance.BusinessKey);

                    var entity = instance.Adapt<TaktInstance>();

                    var result = await InstanceRepository.CreateAsync(entity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入工作流实例失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }
        catch (Exception ex)
        {
            _logger.Error("导入工作流实例数据失败", ex);
            throw new TaktException("导入工作流实例数据失败");
        }
    }

    /// <summary>
    /// 导出工作流实例数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktInstanceQueryDto query, string? sheetName, string? fileName)
    {
        var actualSheetName = sheetName ?? "Instance";
        var actualFileName = fileName ?? "工作流实例数据";
        var list = await InstanceRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<TaktInstanceExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="additionalCondition">附加查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<TaktInstance, bool>> QueryExpression(TaktInstanceQueryDto query, Expression<Func<TaktInstance, bool>>? additionalCondition = null)
    {
        var expression = Expressionable.Create<TaktInstance>()
            .AndIF(!string.IsNullOrEmpty(query.InstanceTitle), x => x.InstanceTitle.Contains(query.InstanceTitle))
            .AndIF(!string.IsNullOrEmpty(query.BusinessKey), x => x.BusinessKey.Contains(query.BusinessKey))
            .AndIF(query.Status.HasValue && query.Status.Value != -1, x => x.Status == query.Status.Value)
            .AndIF(query.Priority.HasValue && query.Priority.Value != -1, x => x.Priority == query.Priority.Value)
            .AndIF(query.Urgency.HasValue && query.Urgency.Value != -1, x => x.Urgency == query.Urgency.Value)
            .AndIF(query.StartTime.HasValue, x => x.StartTime == query.StartTime.Value)
            .AndIF(query.EndTime.HasValue, x => x.EndTime == query.EndTime.Value);

        if (additionalCondition != null)
        {
            expression = expression.And(additionalCondition);
        }

        return expression.ToExpression();
    }
}

