//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktPlantService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 工厂服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Material.Master;
using Takt.Application.Services.Logistics.Material.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Material.Master;
using Takt.Domain.IServices.Extensions;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Linq.Expressions;

namespace Takt.Application.Services.Logistics.Material.Master
{
    /// <summary>
    /// 工厂服务实现
    /// </summary>
    public class TaktPlantService : TaktBaseService, ITaktPlantService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取工厂仓储
        /// </summary>
        private ITaktRepository<TaktPlant> PlantRepository => _repositoryFactory.GetBusinessRepository<TaktPlant>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktPlantService(
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
        /// 获取工厂分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktPlantDto>> GetListAsync(TaktPlantQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await PlantRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            var dtoResult = new TaktPagedResult<TaktPlantDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktPlantDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取工厂详情
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回工厂详情</returns>
        public async Task<TaktPlantDto> GetByIdAsync(long id)
        {
            var plant = await PlantRepository.GetByIdAsync(id);
            if (plant == null)
                throw new TaktException(L("Plant.NotFound", id));

            return plant.Adapt<TaktPlantDto>();
        }

        /// <summary>
        /// 根据工厂编码获取工厂详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <returns>返回工厂详情</returns>
        public async Task<TaktPlantDto> GetByPlantCodeAsync(string plantCode)
        {
            if (string.IsNullOrEmpty(plantCode))
                throw new TaktException(L("Plant.PlantCodeRequired"));

            var plant = await PlantRepository.GetFirstAsync(x => x.PlantCode == plantCode);
            if (plant == null)
                throw new TaktException(L("Plant.NotFoundByCode", plantCode));

            return plant.Adapt<TaktPlantDto>();
        }

        /// <summary>
        /// 创建工厂
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回工厂ID</returns>
        public async Task<long> CreateAsync(TaktPlantCreateDto input)
        {
            // 验证工厂编码是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(PlantRepository, "PlantCode", input.PlantCode);

            var plant = input.Adapt<TaktPlant>();
            var result = await PlantRepository.CreateAsync(plant);
            return result;
        }

        /// <summary>
        /// 更新工厂
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktPlantUpdateDto input)
        {
            var plant = await PlantRepository.GetByIdAsync(input.PlantId);
            if (plant == null)
                throw new TaktException(L("Plant.NotFound", input.PlantId));

            // 验证工厂编码是否已存在（排除当前记录）
            if (plant.PlantCode != input.PlantCode)
                await TaktValidateUtils.ValidateFieldExistsAsync(PlantRepository, "PlantCode", input.PlantCode, input.PlantId);

            input.Adapt(plant);
            var result = await PlantRepository.UpdateAsync(plant);
            return result > 0;
        }

        /// <summary>
        /// 删除工厂
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var plant = await PlantRepository.GetByIdAsync(id);
            if (plant == null)
                throw new TaktException(L("Plant.NotFound", id));

            var result = await PlantRepository.DeleteAsync(plant);
            return result > 0;
        }

        /// <summary>
        /// 批量删除工厂
        /// </summary>
        /// <param name="ids">工厂ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("Plant.SelectToDelete"));

            var plants = await PlantRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!plants.Any())
                throw new TaktException(L("Plant.NotFound"));

            var result = await PlantRepository.DeleteAsync(plants);
            return result > 0;
        }

        /// <summary>
        /// 更新工厂状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktPlantStatusDto input)
        {
            var plant = await PlantRepository.GetByIdAsync(input.PlantId);
            if (plant == null)
                throw new TaktException(L("Plant.NotFound", input.PlantId));

            plant.Status = input.Status;
            var result = await PlantRepository.UpdateAsync(plant);
            return result > 0;
        }

        /// <summary>
        /// 导入工厂数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "工厂信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktPlantImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var plant = dto.Adapt<TaktPlant>();
                    await PlantRepository.CreateAsync(plant);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Plant.ImportFailed", dto.PlantCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出工厂数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktPlantQueryDto query, string sheetName = "工厂信息")
        {
            var predicate = QueryExpression(query);

            var plants = await PlantRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.OrderNum)
                .ToListAsync();

            var exportDtos = plants.Adapt<List<TaktPlantExportDto>>();
            
            // 处理导出DTO中的特殊字段
            for (int i = 0; i < exportDtos.Count; i++)
            {
                exportDtos[i].StatusText = plants[i].Status == 0 ? "停用" : "正常";
            }

            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "工厂信息")
        {
            var template = new List<TaktPlantTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 获取工厂选项列表
        /// </summary>
        /// <returns>工厂选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var plants = await PlantRepository.AsQueryable()
                .Where(r => r.Status == 1)  // 只获取正常状态的工厂
                .OrderBy(r => r.OrderNum)
                .Select(r => new TaktSelectOption
                {
                    DictLabel = r.PlantName,
                    DictValue = r.PlantCode,
                    ExtLabel = r.PlantShortName ?? r.PlantName,
                    ExtValue = r.Id,
                    OrderNum = r.OrderNum
                })
                .ToListAsync();
            return plants;
        }



        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktPlant, bool>> QueryExpression(TaktPlantQueryDto query)
        {
            var exp = Expressionable.Create<TaktPlant>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.PlantName))
                    exp = exp.And(x => x.PlantName.Contains(query.PlantName));

                if (!string.IsNullOrEmpty(query.PlantShortName))
                    exp = exp.And(x => x.PlantShortName.Contains(query.PlantShortName));

                // PlantType: -1 表示显示全部
                if (query.PlantType.HasValue && query.PlantType.Value != -1)
                    exp = exp.And(x => x.PlantType == query.PlantType.Value);

                if (!string.IsNullOrEmpty(query.PlantTypeName))
                    exp = exp.And(x => x.PlantTypeName.Contains(query.PlantTypeName));

                if (!string.IsNullOrEmpty(query.City))
                    exp = exp.And(x => x.City.Contains(query.City));

                if (!string.IsNullOrEmpty(query.Province))
                    exp = exp.And(x => x.Province.Contains(query.Province));

                if (!string.IsNullOrEmpty(query.Country))
                    exp = exp.And(x => x.Country.Contains(query.Country));

                // Status: -1 表示显示全部
                if (query.Status.HasValue && query.Status.Value != -1)
                    exp = exp.And(x => x.Status == query.Status.Value);
            }

            return exp.ToExpression();
        }
    }
}


