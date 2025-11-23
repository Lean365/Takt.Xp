#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktOperationRateService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 稼动率服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 稼动率服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateService : TaktBaseService, ITaktOperationRateService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取稼动率仓储
        /// </summary>
        private ITaktRepository<TaktOperationRate> OperationRateRepository => _repositoryFactory.GetBusinessRepository<TaktOperationRate>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktOperationRateService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取稼动率分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktOperationRateDto>> GetListAsync(TaktOperationRateQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await OperationRateRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktOperationRateDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktOperationRateDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取稼动率详情
        /// </summary>
        /// <param name="id">稼动率ID</param>
        /// <returns>返回稼动率详情</returns>
        public async Task<TaktOperationRateDto> GetByIdAsync(long id)
        {
            var operationRate = await OperationRateRepository.GetByIdAsync(id);
            if (operationRate == null)
                throw new TaktException(L("OperationRate.NotFound", id));

            return operationRate.Adapt<TaktOperationRateDto>();
        }

        /// <summary>
        /// 创建稼动率
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回稼动率ID</returns>
        public async Task<long> CreateAsync(TaktOperationRateCreateDto input)
        {
            var operationRate = input.Adapt<TaktOperationRate>();
            operationRate.CreateTime = DateTime.Now;
            operationRate.CreateBy = _currentUser.UserName ?? "System";

            var result = await OperationRateRepository.CreateAsync(operationRate);
            return result;
        }

        /// <summary>
        /// 更新稼动率
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktOperationRateUpdateDto input)
        {
            var operationRate = await OperationRateRepository.GetByIdAsync(input.OperationRateId);
            if (operationRate == null)
                throw new TaktException(L("OperationRate.NotFound", input.OperationRateId));

            input.Adapt(operationRate);
            operationRate.UpdateTime = DateTime.Now;
            operationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await OperationRateRepository.UpdateAsync(operationRate);
            return result > 0;
        }

        /// <summary>
        /// 删除稼动率
        /// </summary>
        /// <param name="id">稼动率ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var operationRate = await OperationRateRepository.GetByIdAsync(id);
            if (operationRate == null)
                throw new TaktException(L("OperationRate.NotFound", id));

            var result = await OperationRateRepository.DeleteAsync(operationRate);
            return result > 0;
        }

        /// <summary>
        /// 批量删除稼动率
        /// </summary>
        /// <param name="ids">稼动率ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("OperationRate.SelectToDelete"));

            var operationRates = await OperationRateRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!operationRates.Any())
                throw new TaktException(L("OperationRate.NotFound"));

            var result = await OperationRateRepository.DeleteAsync(operationRates);
            return result > 0;
        }

        /// <summary>
        /// 更新稼动率状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktOperationRateStatusDto input)
        {
            var operationRate = await OperationRateRepository.GetByIdAsync(input.OperationRateId);
            if (operationRate == null)
                throw new TaktException(L("OperationRate.NotFound", input.OperationRateId));

            operationRate.Status = input.Status;
            operationRate.UpdateTime = DateTime.Now;
            operationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await OperationRateRepository.UpdateAsync(operationRate);
            return result > 0;
        }

        /// <summary>
        /// 导入稼动率数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "稼动率信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktOperationRateImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var operationRate = dto.Adapt<TaktOperationRate>();
                    operationRate.CreateTime = DateTime.Now;
                    operationRate.CreateBy = _currentUser.UserName ?? "System";

                    await OperationRateRepository.CreateAsync(operationRate);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("OperationRate.ImportFailed", dto.WorkCenter), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出稼动率数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktOperationRateQueryDto query, string sheetName = "稼动率信息")
        {
            var predicate = QueryExpression(query);

            var operationRates = await OperationRateRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = operationRates.Adapt<List<TaktOperationRateExportDto>>();
            
            // 处理导出DTO中的特殊字段
            for (int i = 0; i < exportDtos.Count; i++)
            {
                exportDtos[i].StatusText = operationRates[i].Status == 0 ? "正常" : "停用";
            }

            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取稼动率选项列表
        /// </summary>
        /// <returns>稼动率选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var operationRates = await OperationRateRepository.AsQueryable()
                .Where(r => r.Status == 0)  // 只获取正常状态的稼动率
                .OrderBy(r => r.Id)
                .Select(r => new TaktSelectOption
                {
                    DictLabel = $"{r.PlantCode}-{r.OperationType}",
                    DictValue = r.Id,
                    ExtLabel = $"稼动率: {r.OperationRate}%",
                    ExtValue = r.OperationRate,
                    OrderNum = (int)r.Id
                })
                .ToListAsync();
            return operationRates;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "稼动率信息")
        {
            var template = new List<TaktOperationRateTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktOperationRate, bool>> QueryExpression(TaktOperationRateQueryDto query)
        {
            var exp = Expressionable.Create<TaktOperationRate>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.WorkCenter))
                    exp = exp.And(x => x.OperationType.ToString().Contains(query.WorkCenter));

                // Status: -1 表示显示全部
                if (query.Status.HasValue && query.Status.Value != -1)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (query.StartDate.HasValue)
                    exp = exp.And(x => x.EffectiveDate >= query.StartDate.Value);

                if (query.EndDate.HasValue)
                    exp = exp.And(x => x.EffectiveDate <= query.EndDate.Value);
            }

            return exp.ToExpression();
        }
    }
}



