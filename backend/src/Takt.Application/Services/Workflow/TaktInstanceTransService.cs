//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktInstanceTransService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流实例流转历史服务实现
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
/// 工作流实例流转历史服务实现
/// </summary>
public class TaktInstanceTransService : TaktBaseService, ITaktInstanceTransService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 工作流实例流转历史仓储
    /// </summary>
    private ITaktRepository<TaktInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceTrans>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localizationService">本地化服务</param>
    public TaktInstanceTransService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localizationService) : base(logger, httpContextAccessor, currentUser, localizationService)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    /// <summary>
    /// 获取流转历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页流转历史列表</returns>
    public async Task<TaktPagedResult<TaktInstanceTransDto>> GetListAsync(TaktInstanceTransQueryDto query)
    {
        try
        {
            var result = await TransRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceTransDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceTransDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error(L("InstanceTrans.GetListFailed"), ex);
            throw new TaktException(L("InstanceTrans.GetListFailed"));
        }
    }

    /// <summary>
    /// 根据ID获取流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>流转历史信息，如果不存在则返回null</returns>
    public async Task<TaktInstanceTransDto?> GetByIdAsync(long id)
    {
        try
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return null;

            return entity.Adapt<TaktInstanceTransDto>();
        }
        catch (Exception ex)
        {
            _logger.Error($"根据ID获取流转历史失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 删除流转历史
    /// </summary>
    /// <param name="id">流转历史ID</param>
    /// <returns>删除是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return false;

            var result = await TransRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除流转历史成功，ID: {id}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            _logger.Error($"删除流转历史失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 批量删除实例流转
    /// </summary>
    /// <param name="ids">实例流转ID数组</param>
    /// <returns>是否全部成功</returns>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
            return false;
        foreach (var id in ids)
        {
            var entity = await TransRepository.GetByIdAsync(id);
            if (entity == null)
                return false;
            var result = await TransRepository.DeleteAsync(id);
            if (result <= 0)
                return false;
        }
        return true;
    }

    /// <summary>
    /// 获取工作流实例的流转历史
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转历史列表</returns>
    public async Task<List<TaktInstanceTransDto>> GetByInstanceIdAsync(long instanceId)
    {
        try
        {
            var list = await TransRepository.GetListAsync(x => x.InstanceId == instanceId);
            return list.Adapt<List<TaktInstanceTransDto>>();
        }
        catch (Exception ex)
        {
            _logger.Error($"获取工作流实例流转历史失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取工作流实例的流转历史分页列表
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktInstanceTransDto>> GetByInstanceIdPagedAsync(long instanceId, TaktInstanceTransQueryDto query)
    {
        try
        {
            var expression = Expressionable.Create<TaktInstanceTrans>()
                .And(x => x.InstanceId == instanceId)
                .AndIF(!string.IsNullOrEmpty(query.StartNodeId), x => x.StartNodeId.Contains(query.StartNodeId))
                .AndIF(!string.IsNullOrEmpty(query.StartNodeName), x => x.StartNodeName.Contains(query.StartNodeName))
                .AndIF(query.StartNodeType.HasValue, x => x.StartNodeType == query.StartNodeType.Value)
                .AndIF(!string.IsNullOrEmpty(query.ToNodeId), x => x.ToNodeId.Contains(query.ToNodeId))
                .AndIF(!string.IsNullOrEmpty(query.ToNodeName), x => x.ToNodeName.Contains(query.ToNodeName))
                .AndIF(query.ToNodeType.HasValue, x => x.ToNodeType == query.ToNodeType.Value)
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(!string.IsNullOrEmpty(query.CreateBy), x => x.CreateBy == query.CreateBy)
                .AndIF(query.TransTimeStart.HasValue, x => x.TransTime >= query.TransTimeStart.Value)
                .AndIF(query.TransTimeEnd.HasValue, x => x.TransTime <= query.TransTimeEnd.Value)
                .ToExpression();

            var result = await TransRepository.GetPagedListAsync(
                expression,
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktInstanceTransDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktInstanceTransDto>>()
            };
        }
        catch (Exception ex)
        {
            _logger.Error($"获取工作流实例流转历史分页列表失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 导出流转历史数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="fileName">文件名</param>
    /// <returns>Excel文件</returns>
    public async Task<(string fileName, byte[] content)> ExportAsync(TaktInstanceTransQueryDto query, string? sheetName, string? fileName)
    {
        var actualSheetName = sheetName ?? "InstanceTrans";
        var actualFileName = fileName ?? "流转历史数据";
        var list = await TransRepository.GetListAsync(QueryExpression(query));
        var exportList = list.Adapt<List<TaktInstanceTransExportDto>>();
        return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
    }

    /// <summary>
    /// 构建查询表达式
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>查询表达式</returns>
    private Expression<Func<TaktInstanceTrans, bool>> QueryExpression(TaktInstanceTransQueryDto query)
    {
        return Expressionable.Create<TaktInstanceTrans>()
            .AndIF(query.InstanceId.HasValue, x => x.InstanceId == query.InstanceId.Value)
            .AndIF(!string.IsNullOrEmpty(query.StartNodeId), x => x.StartNodeId.Contains(query.StartNodeId))
            .AndIF(!string.IsNullOrEmpty(query.StartNodeName), x => x.StartNodeName.Contains(query.StartNodeName))
            .AndIF(query.StartNodeType.HasValue, x => x.StartNodeType == query.StartNodeType.Value)
            .AndIF(!string.IsNullOrEmpty(query.ToNodeId), x => x.ToNodeId.Contains(query.ToNodeId))
            .AndIF(!string.IsNullOrEmpty(query.ToNodeName), x => x.ToNodeName.Contains(query.ToNodeName))
            .AndIF(query.ToNodeType.HasValue, x => x.ToNodeType == query.ToNodeType.Value)
            .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .AndIF(!string.IsNullOrEmpty(query.CreateBy), x => x.CreateBy == query.CreateBy)
            .AndIF(query.TransTimeStart.HasValue, x => x.TransTime >= query.TransTimeStart.Value)
            .AndIF(query.TransTimeEnd.HasValue, x => x.TransTime <= query.TransTimeEnd.Value)
            .ToExpression();
    }
} 


