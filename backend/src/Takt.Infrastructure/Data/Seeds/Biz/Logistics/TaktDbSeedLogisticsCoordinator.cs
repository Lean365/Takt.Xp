//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedLogisticsCoordinator.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 后勤制造种子数据协调器
//===================================================================

using Takt.Domain.Entities.Logistics.Material.Master;

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 后勤制造种子数据协调器
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-19
/// 功能说明:
/// 1. 统一管理后勤制造相关种子数据的初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktDbSeedLogisticsCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktOperationRate> OperationRateRepository => _repositoryFactory.GetBusinessRepository<TaktOperationRate>();
    private ITaktRepository<TaktProdOrder> ProdOrderRepository => _repositoryFactory.GetBusinessRepository<TaktProdOrder>();
    private ITaktRepository<TaktStandardTime> StandardTimeRepository => _repositoryFactory.GetBusinessRepository<TaktStandardTime>();
    private ITaktRepository<TaktModelDestination> ModelDestinationRepository => _repositoryFactory.GetBusinessRepository<TaktModelDestination>();
    private ITaktRepository<TaktProdMaterial> ProdMaterialRepository => _repositoryFactory.GetBusinessRepository<TaktProdMaterial>();
    private ITaktRepository<TaktPlant> PlantRepository => _repositoryFactory.GetBusinessRepository<TaktPlant>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public TaktDbSeedLogisticsCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有后勤制造种子数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<LogisticsSeedResult> InitializeAllLogisticsDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有后勤制造种子数据...");

            var result = new LogisticsSeedResult();

            // 1. 初始化标准稼动率数据
            var operationRateResult = await InitializeOperationRateAsync();
            result.OperationRateResult = operationRateResult;

            // 2. 初始化生产工单数据
            var prodOrderResult = await InitializeProdOrderAsync();
            result.ProdOrderResult = prodOrderResult;

            // 3. 初始化标准工时数据
            var standardTimeResult = await InitializeStandardTimeAsync();
            result.StandardTimeResult = standardTimeResult;

            // 4. 初始化机种仕向数据
            var modelDestinationResult = await InitializeModelDestinationAsync();
            result.ModelDestinationResult = modelDestinationResult;

            // 5. 初始化生产物料数据
            var mpMaterialResult = await InitializeMpMaterialAsync();
            result.MpMaterialResult = mpMaterialResult;

            // 6. 初始化工厂信息数据
            var plantResult = await InitializePlantAsync();
            result.PlantResult = plantResult;

            _logger.Info($"后勤制造种子数据初始化完成！标准稼动率: {operationRateResult.insertCount + operationRateResult.updateCount} 条, 生产工单: {prodOrderResult.insertCount + prodOrderResult.updateCount} 条, 标准工时: {standardTimeResult.insertCount + standardTimeResult.updateCount} 条, 机种仕向: {modelDestinationResult.insertCount + modelDestinationResult.updateCount} 条, 生产物料: {mpMaterialResult.insertCount + mpMaterialResult.updateCount} 条, 工厂信息: {plantResult.insertCount + plantResult.updateCount} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化后勤制造种子数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化标准稼动率数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeOperationRateAsync()
    {
        var seed = new TaktDbSeedOperationRate();
        var data = seed.GetDefaultOperationRates();
        return await InitializeOperationRatesAsync(data, "标准稼动率");
    }

    /// <summary>
    /// 初始化生产工单数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeProdOrderAsync()
    {
        var seed = new TaktDbSeedProdOrder();
        var data = seed.GetDefaultProdOrders();
        return await InitializeProdOrdersAsync(data, "生产工单");
    }

    /// <summary>
    /// 初始化标准工时数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeStandardTimeAsync()
    {
        var seed = new TaktDbSeedStandardTime();
        var data = seed.GetDefaultStandardTimes();
        return await InitializeStandardTimesAsync(data, "标准工时");
    }

    /// <summary>
    /// 初始化机种仕向数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeModelDestinationAsync()
    {
        var seed = new TaktDbSeedModelDestination();
        var data = seed.GetDefaultModelDestinations();
        return await InitializeModelDestinationsAsync(data, "机种仕向");
    }

    /// <summary>
    /// 初始化生产物料数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeMpMaterialAsync()
    {
        var seed = new TaktDbSeedProdMaterial();
        var data = seed.GetDefaultMpMaterials();
        return await InitializeMpMaterialsAsync(data, "生产物料");
    }

    /// <summary>
    /// 初始化工厂信息数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializePlantAsync()
    {
        var seed = new TaktDbSeedPlant();
        var data = seed.GetDefaultPlants();
        return await InitializePlantsAsync(data, "工厂信息");
    }

    /// <summary>
    /// 批量初始化标准稼动率数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeOperationRatesAsync(List<TaktOperationRate> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await OperationRateRepository.GetFirstAsync(x =>
                x.PlantCode == item.PlantCode &&
                x.FinancialYear == item.FinancialYear &&
                x.OperationType == item.OperationType);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await OperationRateRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.PlantCode}-{item.FinancialYear}-{item.OperationType}' 创建成功");
            }
            else
            {
                existing.PlantCode = item.PlantCode;
                existing.FinancialYear = item.FinancialYear;
                existing.OperationType = item.OperationType;
                existing.OperationRate = item.OperationRate;
                existing.EffectiveDate = item.EffectiveDate;
                existing.ExpiryDate = item.ExpiryDate;
                existing.Status = item.Status;
                existing.Remark = item.Remark;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await OperationRateRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.PlantCode}-{item.FinancialYear}-{item.OperationType}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化生产工单数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeProdOrdersAsync(List<TaktProdOrder> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ProdOrderRepository.GetFirstAsync(x => x.ProdOrderCode == item.ProdOrderCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ProdOrderRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.ProdOrderCode}' 创建成功");
            }
            else
            {
                existing.PlantCode = item.PlantCode;
                existing.ProdOrderType = item.ProdOrderType;
                existing.ProdOrderCode = item.ProdOrderCode;
                existing.MaterialCode = item.MaterialCode;
                existing.ProdOrderQty = item.ProdOrderQty;
                existing.ProducedQty = item.ProducedQty;
                existing.UnitOfMeasure = item.UnitOfMeasure;
                existing.Priority = item.Priority;
                existing.WorkCenter = item.WorkCenter;
                existing.ProdLine = item.ProdLine;
                existing.ProdBatch = item.ProdBatch;
                existing.Status = item.Status;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ProdOrderRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.ProdOrderCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化标准工时数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeStandardTimesAsync(List<TaktStandardTime> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await StandardTimeRepository.GetFirstAsync(x =>
                x.MaterialCode == item.MaterialCode &&
                x.WorkCenter == item.WorkCenter);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await StandardTimeRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.MaterialCode}-{item.WorkCenter}' 创建成功");
            }
            else
            {
                existing.PlantCode = item.PlantCode;
                existing.MaterialCode = item.MaterialCode;
                existing.WorkCenter = item.WorkCenter;
                existing.OperationDesc = item.OperationDesc;
                existing.StandardMinutes = item.StandardMinutes;
                existing.TimeUnit = item.TimeUnit;
                existing.StandardShorts = item.StandardShorts;
                existing.PointsUnit = item.PointsUnit;
                existing.PointsToMinutesRate = item.PointsToMinutesRate;
                existing.ConvertedMinutes = item.ConvertedMinutes;
                existing.EffectiveDate = item.EffectiveDate;
                existing.ExpiryDate = item.ExpiryDate;
                existing.Approver = item.Approver;
                existing.ApprovalDate = item.ApprovalDate;
                existing.Status = item.Status;
                existing.Remark = item.Remark;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await StandardTimeRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.MaterialCode}-{item.WorkCenter}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化机种仕向数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeModelDestinationsAsync(List<TaktModelDestination> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ModelDestinationRepository.GetFirstAsync(x =>
                x.MaterialCode == item.MaterialCode &&
                x.DestinationCode == item.DestinationCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ModelDestinationRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.MaterialCode}-{item.DestinationCode}' 创建成功");
            }
            else
            {
                existing.PlantCode = item.PlantCode;
                existing.MaterialCode = item.MaterialCode;
                existing.ModelCode = item.ModelCode;
                existing.ModelName = item.ModelName;
                existing.DestinationCode = item.DestinationCode;
                existing.DestinationName = item.DestinationName;
                existing.DestinationType = item.DestinationType;
                existing.ProductSpecification = item.ProductSpecification;
                existing.Remark = item.Remark;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ModelDestinationRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.MaterialCode}-{item.DestinationCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化生产物料数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeMpMaterialsAsync(List<TaktProdMaterial> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ProdMaterialRepository.GetFirstAsync(x =>
                x.PlantCode == item.PlantCode &&
                x.MaterialCode == item.MaterialCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ProdMaterialRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.MaterialCode}' 创建成功");
            }
            else
            {
                existing.PlantCode = item.PlantCode;
                existing.MaterialCode = item.MaterialCode;
                existing.MaterialDescription = item.MaterialDescription;
                existing.MovingAveragePrice = item.MovingAveragePrice;
                existing.PriceUnit = item.PriceUnit;
                existing.ValuationClass = item.ValuationClass;
                existing.ProfitCenter = item.ProfitCenter;
                existing.MinimumOrderQuantity = item.MinimumOrderQuantity;
                existing.RoundingValue = item.RoundingValue;
                existing.PlannedDeliveryTime = item.PlannedDeliveryTime;
                existing.StorageBin = item.StorageBin;
                existing.PurchaseGroup = item.PurchaseGroup;
                existing.ExternalProcurementStorageLocation = item.ExternalProcurementStorageLocation;
                existing.IndustrySector = item.IndustrySector;
                existing.BaseUnitOfMeasure = item.BaseUnitOfMeasure;
                existing.MaterialType = item.MaterialType;
                existing.ProductHierarchy = item.ProductHierarchy;
                existing.MaterialGroup = item.MaterialGroup;
                existing.ProcurementType = item.ProcurementType;
                existing.SpecialProcurementType = item.SpecialProcurementType;
                existing.IsBulkMaterial = item.IsBulkMaterial;
                existing.InHouseProductionDays = item.InHouseProductionDays;
                existing.PostToInspectionStock = item.PostToInspectionStock;
                existing.VarianceCode = item.VarianceCode;
                existing.IsBatchManaged = item.IsBatchManaged;
                existing.ManufacturerPartNumber = item.ManufacturerPartNumber;
                existing.Manufacturer = item.Manufacturer;
                existing.Currency = item.Currency;
                existing.PriceControl = item.PriceControl;
                existing.ProductionStorageLocation = item.ProductionStorageLocation;
                existing.CrossPlantMaterialStatus = item.CrossPlantMaterialStatus;
                existing.InventoryQuantity = item.InventoryQuantity;
                existing.Status = item.Status;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ProdMaterialRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.MaterialCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化工厂信息数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializePlantsAsync(List<TaktPlant> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await PlantRepository.GetFirstAsync(x => x.PlantCode == item.PlantCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await PlantRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.PlantCode}' 创建成功");
            }
            else
            {
                existing.PlantName = item.PlantName;
                existing.PlantShortName = item.PlantShortName;
                existing.PlantType = item.PlantType;
                existing.Country = item.Country;
                existing.Province = item.Province;
                existing.City = item.City;
                existing.PlantAddress = item.PlantAddress;
                existing.ContactPerson = item.ContactPerson;
                existing.Phone = item.Phone;
                existing.Email = item.Email;
                existing.Status = item.Status;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await PlantRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.PlantCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
}

/// <summary>
/// 后勤制造种子数据结果
/// </summary>
public class LogisticsSeedResult
{
    /// <summary>
    /// 标准稼动率结果
    /// </summary>
    public (int insertCount, int updateCount) OperationRateResult { get; set; }

    /// <summary>
    /// 生产工单结果
    /// </summary>
    public (int insertCount, int updateCount) ProdOrderResult { get; set; }

    /// <summary>
    /// 标准工时结果
    /// </summary>
    public (int insertCount, int updateCount) StandardTimeResult { get; set; }

    /// <summary>
    /// 机种仕向结果
    /// </summary>
    public (int insertCount, int updateCount) ModelDestinationResult { get; set; }

    /// <summary>
    /// 生产物料结果
    /// </summary>
    public (int insertCount, int updateCount) MpMaterialResult { get; set; }

    /// <summary>
    /// 工厂信息结果
    /// </summary>
    public (int insertCount, int updateCount) PlantResult { get; set; }

    /// <summary>
    /// 获取总插入数量
    /// </summary>
    /// <returns>总插入数量</returns>
    public int GetTotalInsertCount() =>
        OperationRateResult.insertCount + ProdOrderResult.insertCount +
        StandardTimeResult.insertCount + ModelDestinationResult.insertCount + MpMaterialResult.insertCount + PlantResult.insertCount;

    /// <summary>
    /// 获取总更新数量
    /// </summary>
    /// <returns>总更新数量</returns>
    public int GetTotalUpdateCount() =>
        OperationRateResult.updateCount + ProdOrderResult.updateCount +
        StandardTimeResult.updateCount + ModelDestinationResult.updateCount + MpMaterialResult.updateCount + PlantResult.updateCount;
}


