//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceOperService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例操作记录服务实现
//===================================================================

using Takt.Application.Dtos.Workflow;
using Takt.Domain.Entities.Workflow;
using Takt.Domain.Repositories;
using Takt.Domain.IServices;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using Mapster;
using Takt.Shared.Exceptions;

namespace Takt.Application.Services.Workflow;

/// <summary>
/// 工作流实例操作记录服务实现
/// </summary>
public class TaktInstanceOperService : TaktBaseService, ITaktInstanceOperService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流实例操作记录仓储
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
    public TaktInstanceOperService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取操作记录列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页操作记录列表</returns>
    public async Task<TaktPagedResult<TaktInstanceOperDto>> GetListAsync(TaktInstanceOperQueryDto query)
    {
        try
        {
            var result = await OperRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceOperDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceOperDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("InstanceOper.GetListFailed"), ex);
            throw new TaktException(L("InstanceOper.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>操作记录信息，如果不存在则返回null</returns>
    public async Task<TaktInstanceOperDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<TaktInstanceOperDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建工作流审批操作
    /// </summary>
    /// <param name="dto">审批操作信息</param>
    /// <returns>新创建的操作记录ID</returns>
    public async Task<long> CreateApproveAsync(TaktInstanceApproveDto dto)
    {
        try
        {
            // 使用Adapt进行对象映射
            var entity = dto.Adapt<TaktInstanceOper>();
            
            // 设置当前用户信息
            entity.OperatorId = _currentUser.UserId;
            entity.OperatorName = _currentUser.UserName;
            
            // 如果没有节点名称，使用节点ID作为默认名称
            if (string.IsNullOrEmpty(entity.NodeName))
            {
                entity.NodeName = dto.NodeId;
            }
            
            // 确保操作类型正确映射
            entity.OperType = dto.OperType;

            var id = await OperRepository.CreateAsync(entity);
            _logger.Info($"创建工作流审批操作成功，ID: {id}, 实例ID: {dto.InstanceId}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建工作流审批操作失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 创建操作记录
    /// </summary>
    /// <param name="dto">操作记录创建信息</param>
    /// <returns>新创建的操作记录ID</returns>
    public async Task<long> CreateAsync(TaktInstanceOperCreateDto dto)
    {
        try
        {
            var entity = dto.Adapt<TaktInstanceOper>();

            var id = await OperRepository.CreateAsync(entity);
            _logger.Info($"创建操作记录成功，ID: {id}, 实例ID: {dto.InstanceId}");
            return id;
        }
        catch (Exception ex)
        {
            _logger.Error($"创建操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 删除操作记录
    /// </summary>
    /// <param name="id">操作记录ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await OperRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除操作记录成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除操作记录失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除实例操作
    /// </summary>
    /// <param name="ids">实例操作ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await OperRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await OperRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 获取我的操作记录列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页操作记录列表</returns>
    public async Task<TaktPagedResult<TaktInstanceOperDto>> GetMyOperationsAsync(long userId, TaktInstanceOperQueryDto query)
    {
        try
        {
            var expression = Expressionable.Create<TaktInstanceOper>()
                .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
                .AndIF(!string.IsNullOrEmpty(query.NodeId), x => x.NodeId.Contains(query.NodeId))
                .AndIF(!string.IsNullOrEmpty(query.NodeName), x => x.NodeName.Contains(query.NodeName))
                .AndIF(query.OperType.HasValue, x => x.OperType == query.OperType.Value)
                .AndIF(query.OperatorId.HasValue, x => x.OperatorId == query.OperatorId.Value)
                .AndIF(!string.IsNullOrEmpty(query.OperatorName), x => x.OperatorName.Contains(query.OperatorName))
                .And(x => x.OperatorId == userId)
                .ToExpression();

            var result = await OperRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceOperDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceOperDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取我的操作记录列表失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 导出操作记录数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktInstanceOperQueryDto query, string? sheetName, string? fileName)
    {
        var actualSheetName = sheetName ?? "InstanceOper";
        var actualFileName = fileName ?? "操作记录数据";
        var list = await OperRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<TaktInstanceOperExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<TaktInstanceOper, bool>> QueryExpression(TaktInstanceOperQueryDto query)
    {
        return Expressionable.Create<TaktInstanceOper>()
            .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
            .AndIF(!string.IsNullOrEmpty(query.NodeId), x => x.NodeId.Contains(query.NodeId))
            .AndIF(!string.IsNullOrEmpty(query.NodeName), x => x.NodeName.Contains(query.NodeName))
            .AndIF(query.OperType.HasValue && query.OperType.Value != -1, x => x.OperType == query.OperType.Value)
            .AndIF(query.OperatorId.HasValue, x => x.OperatorId == query.OperatorId.Value)
            .AndIF(!string.IsNullOrEmpty(query.OperatorName), x => x.OperatorName.Contains(query.OperatorName))
            .ToExpression();
    }
} 


