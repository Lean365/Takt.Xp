#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPersonnelOperationRateService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 人员稼动率服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 人员稼动率服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateService : TaktBaseService, ITaktPersonnelOperationRateService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取人员稼动率仓储
        /// </summary>
        private ITaktRepository<TaktPersonnelOperationRate> PersonnelOperationRateRepository => _repositoryFactory.GetBusinessRepository<TaktPersonnelOperationRate>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktPersonnelOperationRateService(
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
        /// 获取人员稼动率分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktPersonnelOperationRateDto>> GetListAsync(TaktPersonnelOperationRateQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await PersonnelOperationRateRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktPersonnelOperationRateDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktPersonnelOperationRateDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取人员稼动率详情
        /// </summary>
        /// <param name="id">人员稼动率ID</param>
        /// <returns>返回人员稼动率详情</returns>
        public async Task<TaktPersonnelOperationRateDto> GetByIdAsync(long id)
        {
            var personnelOperationRate = await PersonnelOperationRateRepository.GetByIdAsync(id);
            if (personnelOperationRate == null)
                throw new TaktException(L("PersonnelOperationRate.NotFound", id));

            return personnelOperationRate.Adapt<TaktPersonnelOperationRateDto>();
        }

        /// <summary>
        /// 创建人员稼动率
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回人员稼动率ID</returns>
        public async Task<long> CreateAsync(TaktPersonnelOperationRateCreateDto input)
        {
            var personnelOperationRate = input.Adapt<TaktPersonnelOperationRate>();
            personnelOperationRate.CreateTime = DateTime.Now;
            personnelOperationRate.CreateBy = _currentUser.UserName ?? "System";

            var result = await PersonnelOperationRateRepository.CreateAsync(personnelOperationRate);
            return result;
        }

        /// <summary>
        /// 更新人员稼动率
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktPersonnelOperationRateUpdateDto input)
        {
            var personnelOperationRate = await PersonnelOperationRateRepository.GetByIdAsync(input.PersonnelOperationRateId);
            if (personnelOperationRate == null)
                throw new TaktException(L("PersonnelOperationRate.NotFound", input.PersonnelOperationRateId));

            input.Adapt(personnelOperationRate);
            personnelOperationRate.UpdateTime = DateTime.Now;
            personnelOperationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await PersonnelOperationRateRepository.UpdateAsync(personnelOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 删除人员稼动率
        /// </summary>
        /// <param name="id">人员稼动率ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var personnelOperationRate = await PersonnelOperationRateRepository.GetByIdAsync(id);
            if (personnelOperationRate == null)
                throw new TaktException(L("PersonnelOperationRate.NotFound", id));

            var result = await PersonnelOperationRateRepository.DeleteAsync(personnelOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 批量删除人员稼动率
        /// </summary>
        /// <param name="ids">人员稼动率ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("PersonnelOperationRate.SelectToDelete"));

            var personnelOperationRates = await PersonnelOperationRateRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!personnelOperationRates.Any())
                throw new TaktException(L("PersonnelOperationRate.NotFound"));

            var result = await PersonnelOperationRateRepository.DeleteAsync(personnelOperationRates);
            return result > 0;
        }

        /// <summary>
        /// 更新人员稼动率状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktPersonnelOperationRateStatusDto input)
        {
            var personnelOperationRate = await PersonnelOperationRateRepository.GetByIdAsync(input.PersonnelOperationRateId);
            if (personnelOperationRate == null)
                throw new TaktException(L("PersonnelOperationRate.NotFound", input.PersonnelOperationRateId));

            personnelOperationRate.Status = input.Status;
            personnelOperationRate.UpdateTime = DateTime.Now;
            personnelOperationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await PersonnelOperationRateRepository.UpdateAsync(personnelOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 导入人员稼动率数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "人员稼动率信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktPersonnelOperationRateImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var personnelOperationRate = dto.Adapt<TaktPersonnelOperationRate>();
                    personnelOperationRate.CreateTime = DateTime.Now;
                    personnelOperationRate.CreateBy = _currentUser.UserName ?? "System";

                    await PersonnelOperationRateRepository.CreateAsync(personnelOperationRate);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("PersonnelOperationRate.ImportFailed", dto.PersonnelCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出人员稼动率数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktPersonnelOperationRateQueryDto query, string sheetName = "人员稼动率信息")
        {
            var predicate = QueryExpression(query);

            var personnelOperationRates = await PersonnelOperationRateRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = personnelOperationRates.Adapt<List<TaktPersonnelOperationRateExportDto>>();
            
            // 处理导出DTO中的特殊字段
            for (int i = 0; i < exportDtos.Count; i++)
            {
                exportDtos[i].StatusText = personnelOperationRates[i].Status == 0 ? "正常" : "停用";
            }

            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "人员稼动率信息")
        {
            var template = new List<TaktPersonnelOperationRateTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktPersonnelOperationRate, bool>> QueryExpression(TaktPersonnelOperationRateQueryDto query)
        {
            var exp = Expressionable.Create<TaktPersonnelOperationRate>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.PersonnelCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PersonnelCode));

                if (!string.IsNullOrEmpty(query.WorkCenter))
                    exp = exp.And(x => x.PlantCode.Contains(query.WorkCenter));

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (query.StartDate.HasValue)
                    exp = exp.And(x => x.StartDate >= query.StartDate.Value);

                if (query.EndDate.HasValue)
                    exp = exp.And(x => x.EndDate <= query.EndDate.Value);
            }

            return exp.ToExpression();
        }
    }
}



