#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktEquipmentOperationRateService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 设备稼动率服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 设备稼动率服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateService : TaktBaseService, ITaktEquipmentOperationRateService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取设备稼动率仓储
        /// </summary>
        private ITaktRepository<TaktEquipmentOperationRate> EquipmentOperationRateRepository => _repositoryFactory.GetBusinessRepository<TaktEquipmentOperationRate>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktEquipmentOperationRateService(
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
        /// 获取设备稼动率分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktEquipmentOperationRateDto>> GetListAsync(TaktEquipmentOperationRateQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await EquipmentOperationRateRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktEquipmentOperationRateDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktEquipmentOperationRateDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取设备稼动率详情
        /// </summary>
        /// <param name="id">设备稼动率ID</param>
        /// <returns>返回设备稼动率详情</returns>
        public async Task<TaktEquipmentOperationRateDto> GetByIdAsync(long id)
        {
            var equipmentOperationRate = await EquipmentOperationRateRepository.GetByIdAsync(id);
            if (equipmentOperationRate == null)
                throw new TaktException(L("EquipmentOperationRate.NotFound", id));

            return equipmentOperationRate.Adapt<TaktEquipmentOperationRateDto>();
        }

        /// <summary>
        /// 创建设备稼动率
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回设备稼动率ID</returns>
        public async Task<long> CreateAsync(TaktEquipmentOperationRateCreateDto input)
        {
            var equipmentOperationRate = input.Adapt<TaktEquipmentOperationRate>();
            equipmentOperationRate.CreateTime = DateTime.Now;
            equipmentOperationRate.CreateBy = _currentUser.UserName ?? "System";

            var result = await EquipmentOperationRateRepository.CreateAsync(equipmentOperationRate);
            return result;
        }

        /// <summary>
        /// 更新设备稼动率
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktEquipmentOperationRateUpdateDto input)
        {
            var equipmentOperationRate = await EquipmentOperationRateRepository.GetByIdAsync(input.EquipmentOperationRateId);
            if (equipmentOperationRate == null)
                throw new TaktException(L("EquipmentOperationRate.NotFound", input.EquipmentOperationRateId));

            input.Adapt(equipmentOperationRate);
            equipmentOperationRate.UpdateTime = DateTime.Now;
            equipmentOperationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await EquipmentOperationRateRepository.UpdateAsync(equipmentOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 删除设备稼动率
        /// </summary>
        /// <param name="id">设备稼动率ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var equipmentOperationRate = await EquipmentOperationRateRepository.GetByIdAsync(id);
            if (equipmentOperationRate == null)
                throw new TaktException(L("EquipmentOperationRate.NotFound", id));

            var result = await EquipmentOperationRateRepository.DeleteAsync(equipmentOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 批量删除设备稼动率
        /// </summary>
        /// <param name="ids">设备稼动率ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("EquipmentOperationRate.SelectToDelete"));

            var equipmentOperationRates = await EquipmentOperationRateRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!equipmentOperationRates.Any())
                throw new TaktException(L("EquipmentOperationRate.NotFound"));

            var result = await EquipmentOperationRateRepository.DeleteAsync(equipmentOperationRates);
            return result > 0;
        }

        /// <summary>
        /// 更新设备稼动率状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktEquipmentOperationRateStatusDto input)
        {
            var equipmentOperationRate = await EquipmentOperationRateRepository.GetByIdAsync(input.EquipmentOperationRateId);
            if (equipmentOperationRate == null)
                throw new TaktException(L("EquipmentOperationRate.NotFound", input.EquipmentOperationRateId));

            equipmentOperationRate.Status = input.Status;
            equipmentOperationRate.UpdateTime = DateTime.Now;
            equipmentOperationRate.UpdateBy = _currentUser.UserName ?? "System";

            var result = await EquipmentOperationRateRepository.UpdateAsync(equipmentOperationRate);
            return result > 0;
        }

        /// <summary>
        /// 导入设备稼动率数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "设备稼动率信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktEquipmentOperationRateImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var equipmentOperationRate = dto.Adapt<TaktEquipmentOperationRate>();
                    equipmentOperationRate.CreateTime = DateTime.Now;
                    equipmentOperationRate.CreateBy = _currentUser.UserName ?? "System";

                    await EquipmentOperationRateRepository.CreateAsync(equipmentOperationRate);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("EquipmentOperationRate.ImportFailed", dto.EquipmentCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出设备稼动率数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktEquipmentOperationRateQueryDto query, string sheetName = "设备稼动率信息")
        {
            var predicate = QueryExpression(query);

            var equipmentOperationRates = await EquipmentOperationRateRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = equipmentOperationRates.Adapt<List<TaktEquipmentOperationRateExportDto>>();
            
            // 处理导出DTO中的特殊字段
            for (int i = 0; i < exportDtos.Count; i++)
            {
                exportDtos[i].StatusText = equipmentOperationRates[i].Status == 0 ? "正常" : "停用";
            }

            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "设备稼动率信息")
        {
            var template = new List<TaktEquipmentOperationRateTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktEquipmentOperationRate, bool>> QueryExpression(TaktEquipmentOperationRateQueryDto query)
        {
            var exp = Expressionable.Create<TaktEquipmentOperationRate>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.EquipmentCode))
                    exp = exp.And(x => x.EquipmentCode.Contains(query.EquipmentCode));

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



