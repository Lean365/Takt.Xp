//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedProdMaterial.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 生产级物料主数据种子数据
//===================================================================

using Takt.Domain.Entities.Logistics.Material.Master;

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 生产级物料主数据种子数据
/// </summary>
public class TaktDbSeedProdMaterial
{
    /// <summary>
    /// 获取默认生产物料数据
    /// </summary>
    /// <returns>生产物料数据列表</returns>
    public List<TaktProdMaterial> GetDefaultMpMaterials()
    {
        return new List<TaktProdMaterial>
        {
            new TaktProdMaterial 
            { 
                PlantCode = "C100", 
                MaterialCode = "1921062000", 
                MaterialDescription = "UR-4MD VIDEO REC", 
                MovingAveragePrice = 1250.00m, 
                PriceUnit = "PCS", 
                ValuationClass = "3000", 
                ProfitCenter = "PC001", 
                MinimumOrderQuantity = 100, 
                RoundingValue = 1, 
                PlannedDeliveryTime = 5.0m, 
                StorageBin = "A-01-01", 
                PurchaseGroup = "PG01", 
                ExternalProcurementStorageLocation = "EXT001", 
                IndustrySector = "IS01", 
                BaseUnitOfMeasure = "PCS", 
                MaterialType = "FERT", 
                ProductHierarchy = "PH001", 
                MaterialGroup = "MG01", 
                ProcurementType = 1, 
                SpecialProcurementType = "SPT01", 
                IsBulkMaterial = 0, 
                InHouseProductionDays = 3.0m, 
                PostToInspectionStock = 0, 
                VarianceCode = "VC001", 
                IsBatchManaged = 1, 
                ManufacturerPartNumber = "MPN001", 
                Manufacturer = "Manufacturer A", 
                Currency = "CNY", 
                PriceControl = "S", 
                ProductionStorageLocation = "PROD001", 
                CrossPlantMaterialStatus = 1, 
                InventoryQuantity = 500, 
                Status = 1 
            },
            new TaktProdMaterial 
            { 
                PlantCode = "C100", 
                MaterialCode = "1921062001", 
                MaterialDescription = "UR-4MD VIDEO REC BSC", 
                MovingAveragePrice = 1350.00m, 
                PriceUnit = "PCS", 
                ValuationClass = "3000", 
                ProfitCenter = "PC001", 
                MinimumOrderQuantity = 50, 
                RoundingValue = 1, 
                PlannedDeliveryTime = 4.0m, 
                StorageBin = "A-01-02", 
                PurchaseGroup = "PG01", 
                ExternalProcurementStorageLocation = "EXT001", 
                IndustrySector = "IS01", 
                BaseUnitOfMeasure = "PCS", 
                MaterialType = "FERT", 
                ProductHierarchy = "PH001", 
                MaterialGroup = "MG01", 
                ProcurementType = 1, 
                SpecialProcurementType = "SPT01", 
                IsBulkMaterial = 0, 
                InHouseProductionDays = 2.5m, 
                PostToInspectionStock = 0, 
                VarianceCode = "VC001", 
                IsBatchManaged = 1, 
                ManufacturerPartNumber = "MPN002", 
                Manufacturer = "Manufacturer A", 
                Currency = "CNY", 
                PriceControl = "S", 
                ProductionStorageLocation = "PROD001", 
                CrossPlantMaterialStatus = 1, 
                InventoryQuantity = 300, 
                Status = 1 
            },
            new TaktProdMaterial 
            { 
                PlantCode = "C100", 
                MaterialCode = "1921062020", 
                MaterialDescription = "UR-4MD VIDEO REC 500GB", 
                MovingAveragePrice = 1450.00m, 
                PriceUnit = "PCS", 
                ValuationClass = "3000", 
                ProfitCenter = "PC001", 
                MinimumOrderQuantity = 200, 
                RoundingValue = 1, 
                PlannedDeliveryTime = 6.0m, 
                StorageBin = "A-01-03", 
                PurchaseGroup = "PG01", 
                ExternalProcurementStorageLocation = "EXT001", 
                IndustrySector = "IS01", 
                BaseUnitOfMeasure = "PCS", 
                MaterialType = "FERT", 
                ProductHierarchy = "PH001", 
                MaterialGroup = "MG01", 
                ProcurementType = 1, 
                SpecialProcurementType = "SPT01", 
                IsBulkMaterial = 0, 
                InHouseProductionDays = 4.0m, 
                PostToInspectionStock = 0, 
                VarianceCode = "VC001", 
                IsBatchManaged = 1, 
                ManufacturerPartNumber = "MPN003", 
                Manufacturer = "Manufacturer A", 
                Currency = "CNY", 
                PriceControl = "S", 
                ProductionStorageLocation = "PROD001", 
                CrossPlantMaterialStatus = 1, 
                InventoryQuantity = 800, 
                Status = 1 
            },
            new TaktProdMaterial 
            { 
                PlantCode = "C100", 
                MaterialCode = "1921062022", 
                MaterialDescription = "UR-4MD VIDEO REC FF", 
                MovingAveragePrice = 1550.00m, 
                PriceUnit = "PCS", 
                ValuationClass = "3000", 
                ProfitCenter = "PC001", 
                MinimumOrderQuantity = 150, 
                RoundingValue = 1, 
                PlannedDeliveryTime = 5.5m, 
                StorageBin = "A-01-04", 
                PurchaseGroup = "PG01", 
                ExternalProcurementStorageLocation = "EXT001", 
                IndustrySector = "IS01", 
                BaseUnitOfMeasure = "PCS", 
                MaterialType = "FERT", 
                ProductHierarchy = "PH001", 
                MaterialGroup = "MG01", 
                ProcurementType = 1, 
                SpecialProcurementType = "SPT01", 
                IsBulkMaterial = 0, 
                InHouseProductionDays = 3.5m, 
                PostToInspectionStock = 0, 
                VarianceCode = "VC001", 
                IsBatchManaged = 1, 
                ManufacturerPartNumber = "MPN004", 
                Manufacturer = "Manufacturer A", 
                Currency = "CNY", 
                PriceControl = "S", 
                ProductionStorageLocation = "PROD001", 
                CrossPlantMaterialStatus = 1, 
                InventoryQuantity = 600, 
                Status = 1 
            },
            new TaktProdMaterial 
            { 
                PlantCode = "C100", 
                MaterialCode = "1921062030", 
                MaterialDescription = "UR-4MD VIDEO REC JPN", 
                MovingAveragePrice = 1650.00m, 
                PriceUnit = "PCS", 
                ValuationClass = "3000", 
                ProfitCenter = "PC001", 
                MinimumOrderQuantity = 100, 
                RoundingValue = 1, 
                PlannedDeliveryTime = 7.0m, 
                StorageBin = "A-01-05", 
                PurchaseGroup = "PG01", 
                ExternalProcurementStorageLocation = "EXT001", 
                IndustrySector = "IS01", 
                BaseUnitOfMeasure = "PCS", 
                MaterialType = "FERT", 
                ProductHierarchy = "PH001", 
                MaterialGroup = "MG01", 
                ProcurementType = 1, 
                SpecialProcurementType = "SPT01", 
                IsBulkMaterial = 0, 
                InHouseProductionDays = 5.0m, 
                PostToInspectionStock = 0, 
                VarianceCode = "VC001", 
                IsBatchManaged = 1, 
                ManufacturerPartNumber = "MPN005", 
                Manufacturer = "Manufacturer A", 
                Currency = "CNY", 
                PriceControl = "S", 
                ProductionStorageLocation = "PROD001", 
                CrossPlantMaterialStatus = 1, 
                InventoryQuantity = 400, 
                Status = 1 
            }
        };
    }
}


