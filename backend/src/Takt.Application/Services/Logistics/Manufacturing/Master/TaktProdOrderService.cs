//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProdOrderService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 生产工单服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderService : TaktBaseService, ITaktProdOrderService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取生产工单仓储
        /// </summary>
        private ITaktRepository<TaktProdOrder> ProdOrderRepository => _repositoryFactory.GetBusinessRepository<TaktProdOrder>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProdOrderService(
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
        /// 获取生产工单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktProdOrderDto>> GetListAsync(TaktProdOrderQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await ProdOrderRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktProdOrderDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktProdOrderDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取生产工单详情
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>返回生产工单详情</returns>
        public async Task<TaktProdOrderDto> GetByIdAsync(long prodOrderId)
        {
            var prodOrder = await ProdOrderRepository.GetByIdAsync(prodOrderId);
            if (prodOrder == null)
                throw new TaktException(L("ProdOrder.NotFound", prodOrderId));

            return prodOrder.Adapt<TaktProdOrderDto>();
        }

        /// <summary>
        /// 创建生产工单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回生产工单ID</returns>
        public async Task<long> CreateAsync(TaktProdOrderCreateDto input)
        {
            var prodOrder = input.Adapt<TaktProdOrder>();
            var result = await ProdOrderRepository.CreateAsync(prodOrder);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    result,
                    1, // 新增
                    "创建生产工单",
                    null,
                    null,
                    null,
                    null,
                    null);
            }

            return result;
        }

        /// <summary>
        /// 更新生产工单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktProdOrderUpdateDto input)
        {
            var prodOrder = await ProdOrderRepository.GetByIdAsync(input.ProdOrderId);
            if (prodOrder == null)
                throw new TaktException(L("ProdOrder.NotFound", input.ProdOrderId));

            // 保存更新前的值用于变更记录
            var beforeValue = JsonConvert.SerializeObject(prodOrder);

            input.Adapt(prodOrder);
            var result = await ProdOrderRepository.UpdateAsync(prodOrder);

            // 创建变更记录
            if (result > 0)
            {
                var afterValue = JsonConvert.SerializeObject(prodOrder);
                await CreateChangeLogAsync(
                    input.ProdOrderId,
                    2, // 修改
                    "更新生产工单",
                    beforeValue,
                    afterValue,
                    null,
                    null,
                    null);
            }

            return result > 0;
        }

        /// <summary>
        /// 删除生产工单
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long prodOrderId)
        {
            var prodOrder = await ProdOrderRepository.GetByIdAsync(prodOrderId);
            if (prodOrder == null)
                throw new TaktException(L("ProdOrder.NotFound", prodOrderId));

            // 保存删除前的值用于变更记录
            var beforeValue = JsonConvert.SerializeObject(prodOrder);

            var result = await ProdOrderRepository.DeleteAsync(prodOrder);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    prodOrderId,
                    3, // 删除
                    "删除生产工单",
                    beforeValue,
                    null,
                    null,
                    null,
                    null);
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除生产工单
        /// </summary>
        /// <param name="ids">生产工单ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("ProdOrder.SelectToDelete"));

            var prodOrders = await ProdOrderRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!prodOrders.Any())
                throw new TaktException(L("ProdOrder.NotFound"));

            var result = await ProdOrderRepository.DeleteAsync(prodOrders);
            return result > 0;
        }

        /// <summary>
        /// 导入生产工单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "生产工单信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktProdOrderImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var prodOrder = dto.Adapt<TaktProdOrder>();
                    await ProdOrderRepository.CreateAsync(prodOrder);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("ProdOrder.ImportFailed", dto.ProdOrderCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出生产工单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktProdOrderQueryDto query, string sheetName = "生产工单信息")
        {
            var predicate = QueryExpression(query);

            var prodOrders = await ProdOrderRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = prodOrders.Adapt<List<TaktProdOrderExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "生产工单信息")
        {
            var template = new List<TaktProdOrderTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 更新生产工单状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktProdOrderStatusDto input)
        {
            var prodOrder = await ProdOrderRepository.GetByIdAsync(input.ProdOrderId);
            if (prodOrder == null)
                throw new TaktException(L("ProdOrder.NotFound", input.ProdOrderId));

            var oldStatus = prodOrder.Status;
            prodOrder.Status = input.Status;
            var result = await ProdOrderRepository.UpdateAsync(prodOrder);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    input.ProdOrderId,
                    4, // 状态变更
                    $"状态变更: {oldStatus} -> {input.Status}",
                    null,
                    null,
                    "Status",
                    oldStatus.ToString(),
                    input.Status.ToString());
            }

            return result > 0;
        }

        /// <summary>
        /// 获取生产工单选项列表
        /// </summary>
        /// <returns>生产工单选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var prodOrders = await ProdOrderRepository.GetListAsync(x => x.Status == 0);
            return prodOrders.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.ProdOrderCode} - {x.MaterialCode}",
                DictValue = x.ProdOrderCode,
                ExtLabel = x.MaterialCode,
                ExtValue = x.ProdOrderQty,
                TransKey = null,
                CssClass = 1,
                ListClass = 1,
                OrderNum = (int)x.Id,
                Status = x.Status
            }).ToList();
        }





        /// <summary>
        /// 更新生产工单数量
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <param name="producedQuantity">已生产数量</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateProducedQuantityAsync(long prodOrderId, decimal producedQuantity)
        {
            var prodOrder = await ProdOrderRepository.GetByIdAsync(prodOrderId);
            if (prodOrder == null)
                throw new TaktException(L("ProdOrder.NotFound", prodOrderId));

            var oldQuantity = prodOrder.ProducedQty;
            prodOrder.ProducedQty = producedQuantity;
            var result = await ProdOrderRepository.UpdateAsync(prodOrder);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    prodOrderId,
                    5, // 数量调整
                    $"已生产数量调整: {oldQuantity} -> {producedQuantity}",
                    null,
                    null,
                    "ProducedQuantity",
                    oldQuantity.ToString(),
                    producedQuantity.ToString());
            }

            return result > 0;
        }



        /// <summary>
        /// 获取生产工单变更记录
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>变更记录列表</returns>
        public async Task<List<TaktProdOrderChangeLogDto>> GetChangeLogsAsync(long prodOrderId)
        {
            // 获取生产工单变更记录仓储
            var changeLogRepository = _repositoryFactory.GetBusinessRepository<TaktProdOrderChangeLog>();

            // 查询指定生产工单的变更记录，按变更日期降序排列
            var changeLogs = await changeLogRepository.GetListAsync(
                x => x.ProdOrderId == prodOrderId && x.Status == 1);

            // 按变更日期降序排序
            changeLogs = changeLogs.OrderByDescending(x => x.ChangeDate).ToList();

            // 转换为DTO并返回
            return changeLogs.Adapt<List<TaktProdOrderChangeLogDto>>();
        }



        /// <summary>
        /// 创建生产工单变更记录
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="changeReason">变更原因</param>
        /// <param name="beforeValue">变更前值</param>
        /// <param name="afterValue">变更后值</param>
        /// <param name="changeField">变更字段</param>
        /// <param name="beforeFieldValue">变更前字段值</param>
        /// <param name="afterFieldValue">变更后字段值</param>
        /// <returns>变更记录ID</returns>
        private async Task<long> CreateChangeLogAsync(
            long prodOrderId,
            int changeType,
            string? changeReason = null,
            string? beforeValue = null,
            string? afterValue = null,
            string? changeField = null,
            string? beforeFieldValue = null,
            string? afterFieldValue = null)
        {
            try
            {
                // 获取生产工单信息
                var prodOrder = await ProdOrderRepository.GetByIdAsync(prodOrderId);
                if (prodOrder == null)
                    return 0;

                // 获取变更记录仓储
                var changeLogRepository = _repositoryFactory.GetBusinessRepository<TaktProdOrderChangeLog>();

                // 创建变更记录
                var changeLog = new TaktProdOrderChangeLog
                {
                    ProdOrderId = prodOrderId,
                    PlantCode = prodOrder.PlantCode,
                    ProdOrderType = prodOrder.ProdOrderType,
                    ProdOrderCode = prodOrder.ProdOrderCode,
                    MaterialCode = prodOrder.MaterialCode,
                    ProdOrderQty = prodOrder.ProdOrderQty,
                    ProducedQty = prodOrder.ProducedQty,
                    UnitOfMeasure = prodOrder.UnitOfMeasure,
                    ActualStartDate = prodOrder.ActualStartDate,
                    ActualEndDate = prodOrder.ActualEndDate,
                    Priority = prodOrder.Priority,
                    WorkCenter = prodOrder.WorkCenter ?? string.Empty,
                    ProdLine = prodOrder.ProdLine ?? string.Empty,
                    ProdBatch = prodOrder.ProdBatch ?? string.Empty,
                    SerialNo = prodOrder.SerialNo ?? string.Empty,
                    RoutingCode = prodOrder.RoutingCode ?? string.Empty,
                    ChangeType = changeType,
                    ChangeDate = DateTime.Now,
                    ChangeUser = _currentUser.UserName ?? "Takt365",
                    ChangeReason = changeReason,
                    BeforeValue = beforeValue,
                    AfterValue = afterValue,
                    ChangeField = changeField,
                    BeforeFieldValue = beforeFieldValue,
                    AfterFieldValue = afterFieldValue,
                    Status = 1
                };

                // 保存变更记录
                var result = await changeLogRepository.CreateAsync(changeLog);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"创建生产工单变更记录失败: ProdOrderId={prodOrderId}, ChangeType={changeType}", ex);
                return 0;
            }
        }



        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktProdOrder, bool>> QueryExpression(TaktProdOrderQueryDto query)
        {
            var exp = Expressionable.Create<TaktProdOrder>();

            if (query != null)
            {
                // PlantCode: -1 表示显示全部
                if (!string.IsNullOrEmpty(query.PlantCode) && query.PlantCode != "-1")
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.ProdOrderCode))
                    exp = exp.And(x => x.ProdOrderCode.Contains(query.ProdOrderCode));

                if (!string.IsNullOrEmpty(query.MaterialCode))
                    exp = exp.And(x => x.MaterialCode.Contains(query.MaterialCode));

                // ProdOrderType: -1 表示显示全部
                if (!string.IsNullOrEmpty(query.ProdOrderType) && query.ProdOrderType != "-1")
                    exp = exp.And(x => x.ProdOrderType.Contains(query.ProdOrderType));

                if (!string.IsNullOrEmpty(query.ProdBatch))
                    exp = exp.And(x => x.ProdBatch.Contains(query.ProdBatch));

                if (!string.IsNullOrEmpty(query.SerialNo))
                    exp = exp.And(x => x.SerialNo.Contains(query.SerialNo));

                // Status: -1 表示显示全部
                if (query.Status.HasValue && query.Status.Value != -1)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (query.StartDate.HasValue)
                    exp = exp.And(x => x.ActualStartDate >= query.StartDate.Value);

                if (query.EndDate.HasValue)
                    exp = exp.And(x => x.ActualEndDate <= query.EndDate.Value);
            }

            return exp.ToExpression();
        }
    }
}




