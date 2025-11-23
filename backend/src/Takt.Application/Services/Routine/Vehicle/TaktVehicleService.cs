//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktVehicleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 用车服务实现
//===================================================================

using Microsoft.AspNetCore.Http;
using Takt.Domain.IServices.SignalR;
using Takt.Shared.Models;
using Takt.Shared.Enums;

namespace Takt.Application.Services.Routine.Vehicle
{
    /// <summary>
    /// 用车服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktVehicleService : TaktBaseService, ITaktVehicleService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取车辆仓储
        /// </summary>
        private ITaktRepository<TaktVehicle> VehicleRepository => _repositoryFactory.GetBusinessRepository<TaktVehicle>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktVehicleService(
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
        /// 获取用车分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktVehicleDto>> GetListAsync(TaktVehicleQueryDto query)
        {
            _logger.Info("开始查询用车列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await VehicleRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new TaktPagedResult<TaktVehicleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktVehicleDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取用车详情
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>返回用车详情</returns>
        public async Task<TaktVehicleDto> GetByIdAsync(long vehicleId)
        {
            var vehicle = await VehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
                throw new TaktException(L("Vehicle.NotFound", vehicleId));

            return vehicle.Adapt<TaktVehicleDto>();
        }

        /// <summary>
        /// 创建用车
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回用车ID</returns>
        public async Task<long> CreateAsync(TaktVehicleCreateDto input)
        {
            var vehicle = input.Adapt<TaktVehicle>();
            var result = await VehicleRepository.CreateAsync(vehicle);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Vehicle.Created"),
                    Content = L("Vehicle.CreatedContent", vehicle.PlateNumber),
                    Timestamp = DateTime.Now,
                    Data = vehicle
                });
            }
            
            return result;
        }

        /// <summary>
        /// 更新用车
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktVehicleUpdateDto input)
        {
            var vehicle = await VehicleRepository.GetByIdAsync(input.VehicleId);
            if (vehicle == null)
                throw new TaktException(L("Vehicle.NotFound", input.VehicleId));

            input.Adapt(vehicle);
            var result = await VehicleRepository.UpdateAsync(vehicle);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Vehicle.Updated"),
                    Content = L("Vehicle.UpdatedContent", vehicle.PlateNumber),
                    Timestamp = DateTime.Now,
                    Data = vehicle
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除用车
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long vehicleId)
        {
            var vehicle = await VehicleRepository.GetByIdAsync(vehicleId);
            if (vehicle == null)
                throw new TaktException(L("Vehicle.NotFound", vehicleId));

            var result = await VehicleRepository.DeleteAsync(vehicle);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Vehicle.Deleted"),
                    Content = L("Vehicle.DeletedContent", vehicle.PlateNumber),
                    Timestamp = DateTime.Now,
                    Data = vehicle
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除用车
        /// </summary>
        /// <param name="vehicleIds">用车ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] vehicleIds)
        {
            if (vehicleIds == null || vehicleIds.Length == 0)
                throw new TaktException(L("Vehicle.SelectToDelete"));

            var vehicles = await VehicleRepository.GetListAsync(x => vehicleIds.Contains(x.Id));
            if (!vehicles.Any())
                throw new TaktException(L("Vehicle.NotFound"));

            var result = await VehicleRepository.DeleteAsync(vehicles);
            return result > 0;
        }

        /// <summary>
        /// 导入用车数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "用车信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktVehicleImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var vehicle = dto.Adapt<TaktVehicle>();
                    await VehicleRepository.CreateAsync(vehicle);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Vehicle.ImportFailed", dto.PlateNumber), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出用车数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktVehicleQueryDto query, string sheetName = "用车信息")
        {
            var predicate = QueryExpression(query);

            var vehicles = await VehicleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = vehicles.Adapt<List<TaktVehicleExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "用车信息")
        {
            var template = new List<TaktVehicleTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktVehicle, bool>> QueryExpression(TaktVehicleQueryDto query)
        {
            var exp = Expressionable.Create<TaktVehicle>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlateNumber))
                    exp = exp.And(x => x.PlateNumber.Contains(query.PlateNumber));

                if (query.VehicleType.HasValue)
                    exp = exp.And(x => x.VehicleType == query.VehicleType.Value);

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (!string.IsNullOrEmpty(query.Brand))
                    exp = exp.And(x => x.Brand.Contains(query.Brand));

                if (!string.IsNullOrEmpty(query.Model))
                    exp = exp.And(x => x.Model.Contains(query.Model));

                if (!string.IsNullOrEmpty(query.Color))
                    exp = exp.And(x => x.Color.Contains(query.Color));


            }

            return exp.ToExpression();
        }
    }
}




