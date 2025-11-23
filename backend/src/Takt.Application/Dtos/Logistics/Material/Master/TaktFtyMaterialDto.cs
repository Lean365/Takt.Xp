//===================================================================
// 项目名: Takt.Application
// 文件名: TaktFtyMaterialDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工厂数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Material.Master
{

    /// <summary>
    /// 工厂物料基础DTO
    /// </summary>
    public class TaktFtyMaterialDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFtyMaterialDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ValuationCategory = string.Empty;
            PlantSpecificMaterialStatus = string.Empty;
            AbcIndicator = string.Empty;
            PurchasingGroup = string.Empty;
            IssueUnit = string.Empty;
            MaterialMrpProfile = string.Empty;
            MrpType = string.Empty;
            MrpController = string.Empty;
            PeriodIndicator = string.Empty;
            ProcurementType = string.Empty;
            SpecialProcurementType = string.Empty;
            AlternativeBomSelectionMethod = string.Empty;
            FollowUpMaterial = string.Empty;
            RequirementsGroupingIndicator = string.Empty;
            FloatingScheduleMarginKey = string.Empty;
            ProductionSupervisor = string.Empty;
            MaximumStoragePeriodUnit = string.Empty;
            ReplacementPart = string.Empty;
            ProductionStatus = string.Empty;
            QmControlKeyProcurement = string.Empty;
            LoadingGroup = string.Empty;
            QuotaArrangementUsage = string.Empty;
            PlanVersion = string.Empty;
            ObjectType = string.Empty;
            ObjectId = string.Empty;
            CheckingGroupAvailabilityCheck = string.Empty;
            FiscalYearVariant = string.Empty;
            SourceOfSupply = string.Empty;
            CommodityCodeImportCodeForeignTrade = string.Empty;
            CountryOfOrigin = string.Empty;
            RegionOfOriginNonPreferential = string.Empty;
            CommodityCodeUnitOfMeasureForeignTrade = string.Empty;
            ExportImportMaterialGroup = string.Empty;
            ProfitCenter = string.Empty;
            PpcPlanningCalendar = string.Empty;
            ConsumptionMode = string.Empty;
            VersionIdentifier = string.Empty;
            AlternativeBom = string.Empty;
            BomUsage = string.Empty;
            TaskListGroup = string.Empty;
            SpecialProcurementTypeCosting = string.Empty;
            ProductionUnit = string.Empty;
            IssueStorageLocation = string.Empty;
            MrpGroup = string.Empty;
            CertificateType = string.Empty;
            SupplyDaysProfile = string.Empty;
            CopaLocalFieldNameConnectedToSop = string.Empty;
            CycleCountingIndicator = string.Empty;
            VarianceKey = string.Empty;
            SerialNumberProfile = string.Empty;
            InternalObjectNumber = string.Empty;
            RepetitiveManufacturingProfile = string.Empty;
            RequiredQmSystemSupplier = string.Empty;
            PlanningCycle = string.Empty;
            RoundingProfile = string.Empty;
            ConsumptionReferenceMaterial = string.Empty;
            ConsumptionReferencePlant = string.Empty;
            PreferenceIndicatorImportExport = string.Empty;
            LegalControlTaxExemptionCertificateNumber = string.Empty;
            IsRServiceLevel = string.Empty;
            PlanningStrategyGroup = string.Empty;
            InternalObjectNumberConfigurableMaterialPlanning = string.Empty;
            DefaultStorageLocationExternalProcurement = string.Empty;
            StockDeterminationGroup = string.Empty;
            MaterialAuthorizationGroupActiveQm = string.Empty;
            TaskListType = string.Empty;
            ActiveControlPlannedOrderProcessing = string.Empty;
            LotEntryDeterminationProductionProcessingOrder = string.Empty;
            UnitOfMeasureGroup = string.Empty;
            MaterialTransportationGroup = string.Empty;
            ProductionVersionToBeCosted = string.Empty;
            LogisticsHandlingGroupWorkloadCalculation = string.Empty;
            PlantMaterialDistributionProfile = string.Empty;
            MaterialCfopCategory = string.Empty;
            StockInStockOutStrategy = string.Empty;
            InitialLotReferenceMaterial = string.Empty;
            SegmentationStrategy = string.Empty;
            SegmentationStatus = string.Empty;
            SegmentationStrategyScope = string.Empty;
            SortStockByDeliveryDateOrSegment = string.Empty;
            DiscreteBatchNumber = string.Empty;


        }
        
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long FtyMaterialId { get; set; }

        /// <summary>
        /// 工厂
        /// </summary>
        public string PlantCode { get; set; } 
        // ==================== 基础信息 ====================
        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } 
        /// <summary>
        /// 维护状态
        /// </summary>
        public int MaintenanceStatus { get; set; } 
        /// <summary>
        /// 在工厂级标记要删除的物料
        /// </summary>
        public int MarkedForDeletionAtPlantLevel { get; set; } 
        /// <summary>
        /// 评估类别
        /// </summary>
        public string? ValuationCategory { get; set; }
        /// <summary>
        /// 批量管理标识(内部)
        /// </summary>
        public int BatchManagementIndicatorInternal { get; set; } 
        /// <summary>
        /// 工厂特定的物料状态
        /// </summary>
        public string? PlantSpecificMaterialStatus { get; set; }
        /// <summary>
        /// 工厂特定物料状态有效的起始日期
        /// </summary>
        public DateTime? PlantSpecificStatusValidFrom { get; set; }
        /// <summary>
        /// ABC标识
        /// </summary>
        public string? AbcIndicator { get; set; }
        /// <summary>
        /// 标志：关键部件
        /// </summary>
        public int CriticalPartIndicator { get; set; } 
        // ==================== 采购组织 ====================
        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchasingGroup { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string? IssueUnit { get; set; }
        // ==================== MRP 参数 ====================
        /// <summary>
        /// 物料: MRP 参数文件
        /// </summary>
        public string? MaterialMrpProfile { get; set; }
        /// <summary>
        /// 物料需求计划类型
        /// </summary>
        public string? MrpType { get; set; }
        /// <summary>
        /// MRP 控制者（物料计划人）
        /// </summary>
        public string? MrpController { get; set; }
        /// <summary>
        /// 标识: MRP控制者是买方(未激活的)
        /// </summary>
        public int MrpControllerIsBuyerInactive { get; set; } 
        /// <summary>
        /// 计划的天数内交货
        /// </summary>
        public decimal PlannedDeliveryTimeDays { get; set; } 
        /// <summary>
        /// 以天计的收货处理时间
        /// </summary>
        public decimal GoodsReceiptProcessingTimeDays { get; set; } 
        /// <summary>
        /// 期间标识
        /// </summary>
        public string? PeriodIndicator { get; set; }
        /// <summary>
        /// 装配报废百分比
        /// </summary>
        public decimal AssemblyScrapPercentage { get; set; } 
        /// <summary>
        /// 批量 (物料计划)
        /// </summary>
        public decimal LotSizeMaterialPlanning { get; set; } 
        /// <summary>
        /// 采购类型
        /// </summary>
        public string? ProcurementType { get; set; }
        /// <summary>
        /// 特殊采购类型
        /// </summary>
        public string? SpecialProcurementType { get; set; }
        /// <summary>
        /// 再订货点
        /// </summary>
        public decimal ReorderPoint { get; set; } 
        /// <summary>
        /// 安全库存
        /// </summary>
        public decimal SafetyStock { get; set; } 
        /// <summary>
        /// 最小批量
        /// </summary>
        public decimal MinimumLotSize { get; set; } 
        /// <summary>
        /// 最大批量大小
        /// </summary>
        public decimal MaximumLotSize { get; set; } 
        /// <summary>
        /// 固定批量大小
        /// </summary>
        public decimal FixedLotSize { get; set; } 
        /// <summary>
        /// 采购订单数量的舍入值
        /// </summary>
        public decimal PurchaseOrderQuantityRoundingValue { get; set; } 
        /// <summary>
        /// 最大库存水平
        /// </summary>
        public decimal MaximumStockLevel { get; set; } 
        /// <summary>
        /// 订购成本
        /// </summary>
        public decimal OrderingCosts { get; set; } 
        /// <summary>
        /// 对于独立和集中需求的相关需求标识
        /// </summary>
        public int DependentRequirementsIndicatorIndependentCollective { get; set; } 
        /// <summary>
        /// 库存成本标识
        /// </summary>
        public int StorageCostsIndicator { get; set; } 
        /// <summary>
        /// 选择可替换物料单的方法
        /// </summary>
        public string? AlternativeBomSelectionMethod { get; set; }
        /// <summary>
        /// 中止指示符
        /// </summary>
        public int DiscontinuationIndicator { get; set; } 
        /// <summary>
        /// 中断日期
        /// </summary>
        public DateTime? DiscontinuationDate { get; set; }
        /// <summary>
        /// 后续物料
        /// </summary>
        public string? FollowUpMaterial { get; set; }
        /// <summary>
        /// 需求分组指示符
        /// </summary>
        public string? RequirementsGroupingIndicator { get; set; }
        /// <summary>
        /// 综合MRP标识
        /// </summary>
        public int MixedMrpIndicator { get; set; } 
        /// <summary>
        /// 浮动的计划边际码
        /// </summary>
        public string? FloatingScheduleMarginKey { get; set; }
        /// <summary>
        /// 标识: 计划订单的自动修正
        /// </summary>
        public int AutomaticPlannedOrderAdjustment { get; set; } 
        /// <summary>
        /// 用于生产订单的批准标识
        /// </summary>
        public int ProductionOrderApprovalIndicator { get; set; } 
        /// <summary>
        /// 标识：反冲
        /// </summary>
        public int BackflushIndicator { get; set; } 
        // ==================== 生产参数 ====================
        /// <summary>
        /// 生产管理员
        /// </summary>
        public string? ProductionSupervisor { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public decimal ProcessingTime { get; set; } 
        /// <summary>
        /// 建立和拆卸时间
        /// </summary>
        public decimal SetupAndTeardownTime { get; set; } 
        /// <summary>
        /// 工序间时间
        /// </summary>
        public decimal InteroperationTime { get; set; } 
        /// <summary>
        /// 基准数量
        /// </summary>
        public decimal BaseQuantity { get; set; } 
        /// <summary>
        /// 厂内生产时间
        /// </summary>
        public decimal InHouseProductionTime { get; set; } 
        /// <summary>
        /// 最大存储期间
        /// </summary>
        public int MaximumStoragePeriod { get; set; } 
        /// <summary>
        /// 最大库存期间单位
        /// </summary>
        public string? MaximumStoragePeriodUnit { get; set; }
        /// <summary>
        /// 标识: 从生产区的库存提取
        /// </summary>
        public int WithdrawalFromProductionBinIndicator { get; set; } 
        /// <summary>
        /// 标识: 在初步计划中包括的物料
        /// </summary>
        public int MaterialIncludedInRoughCutPlanning { get; set; } 
        /// <summary>
        /// 超量交货容差限制
        /// </summary>
        public decimal OverdeliveryToleranceLimit { get; set; } 
        /// <summary>
        /// 标识：允许未限制的过量交货
        /// </summary>
        public int UnlimitedOverdeliveryAllowed { get; set; } 
        /// <summary>
        /// 不足交货容差限制
        /// </summary>
        public decimal UnderdeliveryToleranceLimit { get; set; } 
        /// <summary>
        /// 总计补货提前时间(按工作日)
        /// </summary>
        public decimal TotalReplenishmentLeadTimeWorkdays { get; set; } 
        /// <summary>
        /// 替换部件
        /// </summary>
        public string? ReplacementPart { get; set; }
        /// <summary>
        /// 用百分比表示的成本的附加因子
        /// </summary>
        public decimal SurchargeFactorForCostsPercentage { get; set; } 
        /// <summary>
        /// 生产状态
        /// </summary>
        public string? ProductionStatus { get; set; }
        // ==================== 质量管理 ====================
        /// <summary>
        /// 过帐到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } 
        /// <summary>
        /// 质量检查的样本(在%中)(取消激活)
        /// </summary>
        public decimal QualityInspectionSamplePercentageInactive { get; set; } 
        /// <summary>
        /// 隔离期(未激活)
        /// </summary>
        public int QuarantinePeriodInactive { get; set; } 
        /// <summary>
        /// 采购中质量管理的控制码
        /// </summary>
        public string? QmControlKeyProcurement { get; set; }
        /// <summary>
        /// 平均检查持续期间(未激活的的)
        /// </summary>
        public decimal AverageInspectionDurationInactive { get; set; } 
        /// <summary>
        /// 检查计划的标识(未激活)
        /// </summary>
        public int InspectionSetupIndicatorInactive { get; set; } 
        /// <summary>
        /// 凭证需求标识
        /// </summary>
        public int DocumentationRequiredIndicator { get; set; } 
        /// <summary>
        /// 活动性物质内容(未激活的)
        /// </summary>
        public decimal ActiveSubstanceContentInactive { get; set; } 
        /// <summary>
        /// 循环检查间隔
        /// </summary>
        public int RecurringInspectionInterval { get; set; } 
        /// <summary>
        /// 根据检验抽样检查的日期(取消激活)
        /// </summary>
        public DateTime? SampleInspectionDateInactive { get; set; }
        // ==================== 库存管理 ====================
        /// <summary>
        /// 中转库存（工厂到工厂）
        /// </summary>
        public decimal StockInTransitPlantToPlant { get; set; } 
        /// <summary>
        /// 装载组
        /// </summary>
        public string? LoadingGroup { get; set; }
        /// <summary>
        /// 批次管理需求的标识
        /// </summary>
        public int BatchManagementRequirementIndicator { get; set; } 
        /// <summary>
        /// 配额分配使用
        /// </summary>
        public string? QuotaArrangementUsage { get; set; }
        /// <summary>
        /// 服务水平
        /// </summary>
        public decimal ServiceLevel { get; set; } 
        /// <summary>
        /// 分割标识
        /// </summary>
        public int SplittingIndicator { get; set; } 
        /// <summary>
        /// 计划版本
        /// </summary>
        public string? PlanVersion { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string? ObjectType { get; set; }
        /// <summary>
        /// 对象标识
        /// </summary>
        public string? ObjectId { get; set; }
        /// <summary>
        /// 可用性检查的检查组
        /// </summary>
        public string? CheckingGroupAvailabilityCheck { get; set; }
        /// <summary>
        /// 会计年度变式
        /// </summary>
        public string? FiscalYearVariant { get; set; }
        /// <summary>
        /// 标识: 考虑修正因子
        /// </summary>
        public int CorrectionFactorIndicator { get; set; } 
        /// <summary>
        /// 装运建立时间
        /// </summary>
        public decimal ShippingSetupTime { get; set; } 
        /// <summary>
        /// 在装运中有关能力计划的基准数量
        /// </summary>
        public decimal BaseQuantityCapacityPlanningShipping { get; set; } 
        /// <summary>
        /// 处理时间: 装运
        /// </summary>
        public decimal ProcessingTimeShipping { get; set; } 
        /// <summary>
        /// 取消激活的
        /// </summary>
        public int Deactivated { get; set; } 
        /// <summary>
        /// 货源
        /// </summary>
        public string? SourceOfSupply { get; set; }
        /// <summary>
        /// 标识: "允许自动采购订单"
        /// </summary>
        public int AutomaticPurchaseOrderAllowed { get; set; } 
        /// <summary>
        /// 标识: 源清单要求
        /// </summary>
        public int SourceListRequirement { get; set; } 
        /// <summary>
        /// 外贸的商品代码和进口代码
        /// </summary>
        public string? CommodityCodeImportCodeForeignTrade { get; set; }
        /// <summary>
        /// 物料原产地国家
        /// </summary>
        public string? CountryOfOrigin { get; set; }
        /// <summary>
        /// 物料原产地（非特惠货源）
        /// </summary>
        public string? RegionOfOriginNonPreferential { get; set; }
        /// <summary>
        /// 商品代码的计量单位(外贸)
        /// </summary>
        public string? CommodityCodeUnitOfMeasureForeignTrade { get; set; }
        /// <summary>
        /// 出口/进口物料组
        /// </summary>
        public string? ExportImportMaterialGroup { get; set; }
        // ==================== 成本核算 ====================
        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }
        /// <summary>
        /// 在途库存
        /// </summary>
        public decimal StockInTransit { get; set; } 
        // ==================== 生产计划 ====================
        /// <summary>
        /// PPC 计划日历
        /// </summary>
        public string? PpcPlanningCalendar { get; set; }
        /// <summary>
        /// 标识: 允许的重复制造
        /// </summary>
        public int RepetitiveManufacturingAllowed { get; set; } 
        /// <summary>
        /// 计划的时界
        /// </summary>
        public int PlanningTimeFence { get; set; } 
        /// <summary>
        /// 消耗模式
        /// </summary>
        public string? ConsumptionMode { get; set; }
        /// <summary>
        /// 消耗期间:逆向
        /// </summary>
        public int ConsumptionPeriodBackward { get; set; } 
        /// <summary>
        /// 消耗时期-向前
        /// </summary>
        public int ConsumptionPeriodForward { get; set; } 
        /// <summary>
        /// 版本标识符
        /// </summary>
        public string? VersionIdentifier { get; set; }
        /// <summary>
        /// 可选的 BOM
        /// </summary>
        public string? AlternativeBom { get; set; }
        /// <summary>
        /// BOM 用途
        /// </summary>
        public string? BomUsage { get; set; }
        /// <summary>
        /// 任务清单组码
        /// </summary>
        public string? TaskListGroup { get; set; }
        /// <summary>
        /// 组计数器
        /// </summary>
        public int GroupCounter { get; set; } 
        /// <summary>
        /// 批量产品成本核算
        /// </summary>
        public decimal LotSizeCosting { get; set; } 
        /// <summary>
        /// 成本核算的特殊采购类型
        /// </summary>
        public string? SpecialProcurementTypeCosting { get; set; }
        /// <summary>
        /// 生产单位
        /// </summary>
        public string? ProductionUnit { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string? IssueStorageLocation { get; set; }
        /// <summary>
        /// 物料需求计划组
        /// </summary>
        public string? MrpGroup { get; set; }
        /// <summary>
        /// 部件废品百分数
        /// </summary>
        public decimal ComponentScrapPercentage { get; set; } 
        /// <summary>
        /// 证书类型
        /// </summary>
        public string? CertificateType { get; set; }
        /// <summary>
        /// 物料/工厂的检验设置存在
        /// </summary>
        public int InspectionSetupExistsMaterialPlant { get; set; } 
        /// <summary>
        /// 间隔时间（多个计划订单分阶段生产时，计划订单之间的间隔时间)
        /// </summary>
        public decimal IntervalTimeStagedProduction { get; set; } 
        /// <summary>
        /// 供货天数参数文件
        /// </summary>
        public string? SupplyDaysProfile { get; set; }
        /// <summary>
        /// 连接到SOP上的CO/PA局部字段名
        /// </summary>
        public string? CopaLocalFieldNameConnectedToSop { get; set; }
        /// <summary>
        /// 周期盘点的盘点标识
        /// </summary>
        public string? CycleCountingIndicator { get; set; }
        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceKey { get; set; }
        /// <summary>
        /// 序列号参数文件
        /// </summary>
        public string? SerialNumberProfile { get; set; }
        /// <summary>
        /// 内部对象号
        /// </summary>
        public string? InternalObjectNumber { get; set; }
        /// <summary>
        /// 可配置的物料
        /// </summary>
        public int ConfigurableMaterial { get; set; } 
        /// <summary>
        /// 重复生产参数文件
        /// </summary>
        public string? RepetitiveManufacturingProfile { get; set; }
        /// <summary>
        /// 工厂中允许负库存
        /// </summary>
        public int NegativeStocksAllowedInPlant { get; set; } 
        /// <summary>
        /// 要求的供应商质量管理系统
        /// </summary>
        public string? RequiredQmSystemSupplier { get; set; }
        /// <summary>
        /// 计划周期
        /// </summary>
        public string? PlanningCycle { get; set; }
        /// <summary>
        /// 舍入参数文件
        /// </summary>
        public string? RoundingProfile { get; set; }
        /// <summary>
        /// 消耗的参考物料
        /// </summary>
        public string? ConsumptionReferenceMaterial { get; set; }
        /// <summary>
        /// 消耗的参考工厂
        /// </summary>
        public string? ConsumptionReferencePlant { get; set; }
        /// <summary>
        /// 到消耗物料将被复制的日期
        /// </summary>
        public DateTime? DateConsumptionMaterialCopied { get; set; }
        /// <summary>
        /// 消耗的参考物料的乘数
        /// </summary>
        public decimal ConsumptionReferenceMaterialMultiplier { get; set; } 
        /// <summary>
        /// 自动重置预测模式
        /// </summary>
        public int AutomaticResetForecastModel { get; set; } 
        /// <summary>
        /// 进/出口中优惠指示符
        /// </summary>
        public string? PreferenceIndicatorImportExport { get; set; }
        // ==================== 贸易和法律 ====================
        /// <summary>
        /// 免税证明：法律控制指示符
        /// </summary>
        public int TaxExemptionCertificateLegalControlIndicator { get; set; } 
        /// <summary>
        /// 法律控制的免税证书编号
        /// </summary>
        public string? LegalControlTaxExemptionCertificateNumber { get; set; }
        /// <summary>
        /// 免税证明：免税证书的发放日
        /// </summary>
        public DateTime? TaxExemptionCertificateIssueDate { get; set; }
        /// <summary>
        /// 标识：存在供应商申报
        /// </summary>
        public int VendorDeclarationExists { get; set; } 
        /// <summary>
        /// 供应商申报的有效日期
        /// </summary>
        public DateTime? VendorDeclarationValidFrom { get; set; }
        /// <summary>
        /// 指示符：军用物资
        /// </summary>
        public int MilitaryGoodsIndicator { get; set; } 
        /// <summary>
        /// IS－R服务级别
        /// </summary>
        public string? IsRServiceLevel { get; set; }
        /// <summary>
        /// 指示符 : 物料是联产品
        /// </summary>
        public int MaterialIsCoProductIndicator { get; set; } 
        /// <summary>
        /// 计划策略组
        /// </summary>
        public string? PlanningStrategyGroup { get; set; }
        /// <summary>
        /// 计划中的可配置物料的内部对象号
        /// </summary>
        public string? InternalObjectNumberConfigurableMaterialPlanning { get; set; }
        /// <summary>
        /// 外部采购的缺省仓储位置
        /// </summary>
        public string? DefaultStorageLocationExternalProcurement { get; set; }
        /// <summary>
        /// 标识：散装物料
        /// </summary>
        public int BulkMaterialIndicator { get; set; } 
        /// <summary>
        /// 周期标识被固定
        /// </summary>
        public int CycleIndicatorFixed { get; set; } 
        /// <summary>
        /// 库存确定组
        /// </summary>
        public string? StockDeterminationGroup { get; set; }
        /// <summary>
        /// QM 中活动的物料授权组
        /// </summary>
        public string? MaterialAuthorizationGroupActiveQm { get; set; }
        /// <summary>
        /// 计划独立需求的调整期间
        /// </summary>
        public int AdjustmentPeriodPlannedIndependentRequirements { get; set; } 
        /// <summary>
        /// 任务清单类型
        /// </summary>
        public string? TaskListType { get; set; }
        // ==================== 特殊指示符和标识 ====================
        /// <summary>
        /// 安全时间标识（含或不含安全时间）
        /// </summary>
        public int SafetyTimeIndicator { get; set; } 
        /// <summary>
        /// 安全时间（按工作日计算）
        /// </summary>
        public decimal SafetyTimeWorkdays { get; set; } 
        /// <summary>
        /// 活动控制：计划订单处理
        /// </summary>
        public string? ActiveControlPlannedOrderProcessing { get; set; }
        /// <summary>
        /// 在生产/处理订单中批量输入的确定
        /// </summary>
        public string? LotEntryDeterminationProductionProcessingOrder { get; set; }
        /// <summary>
        /// 计量单位组
        /// </summary>
        public string? UnitOfMeasureGroup { get; set; }
        /// <summary>
        /// 物料运输组
        /// </summary>
        public string? MaterialTransportationGroup { get; set; }
        /// <summary>
        /// VO 物料的库存转移销售值（工厂到工厂）
        /// </summary>
        public decimal VoMaterialStockTransferSalesValuePlantToPlant { get; set; } 
        /// <summary>
        /// 仅记值物料的销售价格计的运输值
        /// </summary>
        public decimal TransportationValueOnlyGoodsSalesPrice { get; set; } 
        /// <summary>
        /// 指示符: 平滑促销消耗
        /// </summary>
        public int SmoothPromotionalConsumptionIndicator { get; set; } 
        /// <summary>
        /// 将进行成本核算的生产版本
        /// </summary>
        public string? ProductionVersionToBeCosted { get; set; }
        /// <summary>
        /// 固定价格联产品
        /// </summary>
        public int FixedPriceCoProduct { get; set; } 
        /// <summary>
        /// 用于计算工作负荷的后勤处理组
        /// </summary>
        public string? LogisticsHandlingGroupWorkloadCalculation { get; set; }
        /// <summary>
        /// 工厂物料分销参数文件
        /// </summary>
        public string? PlantMaterialDistributionProfile { get; set; }
        /// <summary>
        /// 有约束的虚拟库存
        /// </summary>
        public decimal RestrictedVirtualStock { get; set; } 
        /// <summary>
        /// 连接空缺库存的销售价
        /// </summary>
        public decimal SalesPriceConnectingShortageStock { get; set; } 
        /// <summary>
        /// 物料: CFOP类别
        /// </summary>
        public string? MaterialCfopCategory { get; set; }
        /// <summary>
        /// 最小安全库存
        /// </summary>
        public decimal MinimumSafetyStock { get; set; } 
        /// <summary>
        /// 无成本核算
        /// </summary>
        public int NoCosting { get; set; } 
        /// <summary>
        /// 库存入库和库存出库的策略
        /// </summary>
        public string? StockInStockOutStrategy { get; set; }
        /// <summary>
        /// 初始批次管理的标识
        /// </summary>
        public int InitialBatchManagementIndicator { get; set; } 
        /// <summary>
        /// 初始批量的参考物料
        /// </summary>
        public string? InitialLotReferenceMaterial { get; set; }
        /// <summary>
        /// 评估的收货锁定库存
        /// </summary>
        public decimal ValuatedGoodsReceiptBlockedStock { get; set; } 
        /// <summary>
        /// 分段策略
        /// </summary>
        public string? SegmentationStrategy { get; set; }
        /// <summary>
        /// 分段状态
        /// </summary>
        public string? SegmentationStatus { get; set; }
        /// <summary>
        /// 分段策略范围
        /// </summary>
        public string? SegmentationStrategyScope { get; set; }
        /// <summary>
        /// 根据交货日期或段排序库存
        /// </summary>
        public string? SortStockByDeliveryDateOrSegment { get; set; }
        /// <summary>
        /// 消耗优先级
        /// </summary>
        public int ConsumptionPriority { get; set; } 
        /// <summary>
        /// 离散批次编号
        /// </summary>
        public string? DiscreteBatchNumber { get; set; }
        /// <summary>
        /// 库存保护标识
        /// </summary>
        public int StockProtectionIndicator { get; set; } 
        /// <summary>
        /// 订单分配运行
        /// </summary>
        public int OrderAllocationRun { get; set; } 
        /// <summary>
        /// 按天数发货处理时间
        /// </summary>
        public decimal ShippingProcessingTimeDays { get; set; } 
        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

    }
    /// <summary>
    /// 工厂物料基础DTO
    /// </summary>
    public class TaktFtyMaterialQueryDto:TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFtyMaterialQueryDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ValuationCategory = string.Empty;
            AbcIndicator = string.Empty;
            PurchasingGroup = string.Empty;
            MrpType = string.Empty;
            ProcurementType = string.Empty;
            ProductionStatus = string.Empty;
            SourceOfSupply = string.Empty;
            CountryOfOrigin = string.Empty;

        }
        /// <summary>
        /// 工厂
        /// </summary>
        public string PlantCode { get; set; } 
        // ==================== 基础信息 ====================
        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } 
        /// <summary>
        /// 在工厂级标记要删除的物料
        /// </summary>
        public int MarkedForDeletionAtPlantLevel { get; set; } 
        /// <summary>
        /// 评估类别
        /// </summary>
        public string? ValuationCategory { get; set; }

        /// <summary>
        /// ABC标识
        /// </summary>
        public string? AbcIndicator { get; set; }

        // ==================== 采购组织 ====================
        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchasingGroup { get; set; }

        // ==================== MRP 参数 ====================

        /// <summary>
        /// 物料需求计划类型
        /// </summary>
        public string? MrpType { get; set; }
        /// <summary>
        /// MRP 控制者（物料计划人）
        /// </summary>
        public string? MrpController { get; set; }

        /// <summary>
        /// 计划的天数内交货
        /// </summary>
        public decimal PlannedDeliveryTimeDays { get; set; } 

        /// <summary>
        /// 采购类型
        /// </summary>
        public string? ProcurementType { get; set; }
        /// <summary>
        /// 特殊采购类型
        /// </summary>
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 安全库存
        /// </summary>
        public decimal SafetyStock { get; set; } 
        /// <summary>
        /// 最小批量
        /// </summary>
        public decimal MinimumLotSize { get; set; } 
        /// <summary>
        /// 最大批量大小
        /// </summary>
        public decimal MaximumLotSize { get; set; } 
        /// <summary>
        /// 固定批量大小
        /// </summary>
        public decimal FixedLotSize { get; set; } 
        /// <summary>
        /// 采购订单数量的舍入值
        /// </summary>
        public decimal PurchaseOrderQuantityRoundingValue { get; set; } 

        /// <summary>
        /// 库存成本标识
        /// </summary>
        public int StorageCostsIndicator { get; set; } 

        // ==================== 生产参数 ====================
        /// <summary>
        /// 生产管理员
        /// </summary>
        public string? ProductionSupervisor { get; set; }

        /// <summary>
        /// 厂内生产时间
        /// </summary>
        public decimal InHouseProductionTime { get; set; } 

        /// <summary>
        /// 生产状态
        /// </summary>
        public string? ProductionStatus { get; set; }
        // ==================== 质量管理 ====================
        /// <summary>
        /// 过帐到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } 
        
        // ==================== 库存管理 ====================
        /// <summary>
        /// 中转库存（工厂到工厂）
        /// </summary>
        public decimal StockInTransitPlantToPlant { get; set; } 
        /// <summary>
        /// 装载组
        /// </summary>
        public string? LoadingGroup { get; set; }
        /// <summary>
        /// 批次管理需求的标识
        /// </summary>
        public int BatchManagementRequirementIndicator { get; set; } 
        
        /// <summary>
        /// 货源
        /// </summary>
        public string? SourceOfSupply { get; set; }
        /// <summary>
        /// 标识: "允许自动采购订单"
        /// </summary>
        public int AutomaticPurchaseOrderAllowed { get; set; } 
        /// <summary>
        /// 标识: 源清单要求
        /// </summary>
        public int SourceListRequirement { get; set; }         
        /// <summary>
        /// 物料原产地国家
        /// </summary>
        public string? CountryOfOrigin { get; set; }
        /// <summary>
        /// 物料原产地（非特惠货源）
        /// </summary>
        public string? RegionOfOriginNonPreferential { get; set; }

        // ==================== 成本核算 ====================
        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }

        // ==================== 生产计划 ====================
        
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string? IssueStorageLocation { get; set; }
        /// <summary>
        /// 物料需求计划组
        /// </summary>
        public string? MrpGroup { get; set; }
        
        /// <summary>
        /// 周期盘点的盘点标识
        /// </summary>
        public string? CycleCountingIndicator { get; set; }
        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceKey { get; set; }
        
        /// <summary>
        /// 外部采购的缺省仓储位置
        /// </summary>
        public string? DefaultStorageLocationExternalProcurement { get; set; }
        /// <summary>
        /// 标识：散装物料
        /// </summary>
        public int BulkMaterialIndicator { get; set; }         
        
        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

    }

    /// <summary>
    /// 工厂物料创建DTO
    /// </summary>
    public class TaktFtyMaterialCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFtyMaterialCreateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            ValuationCategory = string.Empty;
            PlantSpecificMaterialStatus = string.Empty;
            AbcIndicator = string.Empty;
            PurchasingGroup = string.Empty;
            IssueUnit = string.Empty;
            MaterialMrpProfile = string.Empty;
            MrpType = string.Empty;
            MrpController = string.Empty;
            PeriodIndicator = string.Empty;
            ProcurementType = string.Empty;
            SpecialProcurementType = string.Empty;
            AlternativeBomSelectionMethod = string.Empty;
            FollowUpMaterial = string.Empty;
            RequirementsGroupingIndicator = string.Empty;
            FloatingScheduleMarginKey = string.Empty;
            ProductionSupervisor = string.Empty;
            MaximumStoragePeriodUnit = string.Empty;
            ReplacementPart = string.Empty;
            ProductionStatus = string.Empty;
            QmControlKeyProcurement = string.Empty;
            LoadingGroup = string.Empty;
            QuotaArrangementUsage = string.Empty;
            PlanVersion = string.Empty;
            ObjectType = string.Empty;
            ObjectId = string.Empty;
            CheckingGroupAvailabilityCheck = string.Empty;
            FiscalYearVariant = string.Empty;
            SourceOfSupply = string.Empty;
            CommodityCodeImportCodeForeignTrade = string.Empty;
            CountryOfOrigin = string.Empty;
            RegionOfOriginNonPreferential = string.Empty;
            CommodityCodeUnitOfMeasureForeignTrade = string.Empty;
            ExportImportMaterialGroup = string.Empty;
            ProfitCenter = string.Empty;
            PpcPlanningCalendar = string.Empty;
            ConsumptionMode = string.Empty;
            VersionIdentifier = string.Empty;
            AlternativeBom = string.Empty;
            BomUsage = string.Empty;
            TaskListGroup = string.Empty;
            SpecialProcurementTypeCosting = string.Empty;
            ProductionUnit = string.Empty;
            IssueStorageLocation = string.Empty;
            MrpGroup = string.Empty;
            CertificateType = string.Empty;
            SupplyDaysProfile = string.Empty;
            CopaLocalFieldNameConnectedToSop = string.Empty;
            CycleCountingIndicator = string.Empty;
            VarianceKey = string.Empty;
            SerialNumberProfile = string.Empty;
            InternalObjectNumber = string.Empty;
            RepetitiveManufacturingProfile = string.Empty;
            RequiredQmSystemSupplier = string.Empty;
            PlanningCycle = string.Empty;
            RoundingProfile = string.Empty;
            ConsumptionReferenceMaterial = string.Empty;
            ConsumptionReferencePlant = string.Empty;
            PreferenceIndicatorImportExport = string.Empty;
            LegalControlTaxExemptionCertificateNumber = string.Empty;
            IsRServiceLevel = string.Empty;
            PlanningStrategyGroup = string.Empty;
            InternalObjectNumberConfigurableMaterialPlanning = string.Empty;
            DefaultStorageLocationExternalProcurement = string.Empty;
            StockDeterminationGroup = string.Empty;
            MaterialAuthorizationGroupActiveQm = string.Empty;
            TaskListType = string.Empty;
            ActiveControlPlannedOrderProcessing = string.Empty;
            LotEntryDeterminationProductionProcessingOrder = string.Empty;
            UnitOfMeasureGroup = string.Empty;
            MaterialTransportationGroup = string.Empty;
            ProductionVersionToBeCosted = string.Empty;
            LogisticsHandlingGroupWorkloadCalculation = string.Empty;
            PlantMaterialDistributionProfile = string.Empty;
            MaterialCfopCategory = string.Empty;
            StockInStockOutStrategy = string.Empty;
            InitialLotReferenceMaterial = string.Empty;
            SegmentationStrategy = string.Empty;
            SegmentationStatus = string.Empty;
            SegmentationStrategyScope = string.Empty;
            SortStockByDeliveryDateOrSegment = string.Empty;
            DiscreteBatchNumber = string.Empty;


        }
        /// <summary>
        /// 工厂
        /// </summary>
        public string PlantCode { get; set; } 
        // ==================== 基础信息 ====================
        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterialCode { get; set; } 
        /// <summary>
        /// 维护状态
        /// </summary>
        public int MaintenanceStatus { get; set; } 
        /// <summary>
        /// 在工厂级标记要删除的物料
        /// </summary>
        public int MarkedForDeletionAtPlantLevel { get; set; } 
        /// <summary>
        /// 评估类别
        /// </summary>
        public string? ValuationCategory { get; set; }
        /// <summary>
        /// 批量管理标识(内部)
        /// </summary>
        public int BatchManagementIndicatorInternal { get; set; } 
        /// <summary>
        /// 工厂特定的物料状态
        /// </summary>
        public string? PlantSpecificMaterialStatus { get; set; }
        /// <summary>
        /// 工厂特定物料状态有效的起始日期
        /// </summary>
        public DateTime? PlantSpecificStatusValidFrom { get; set; }
        /// <summary>
        /// ABC标识
        /// </summary>
        public string? AbcIndicator { get; set; }
        /// <summary>
        /// 标志：关键部件
        /// </summary>
        public int CriticalPartIndicator { get; set; } 
        // ==================== 采购组织 ====================
        /// <summary>
        /// 采购组
        /// </summary>
        public string? PurchasingGroup { get; set; }
        /// <summary>
        /// 发货单位
        /// </summary>
        public string? IssueUnit { get; set; }
        // ==================== MRP 参数 ====================
        /// <summary>
        /// 物料: MRP 参数文件
        /// </summary>
        public string? MaterialMrpProfile { get; set; }
        /// <summary>
        /// 物料需求计划类型
        /// </summary>
        public string? MrpType { get; set; }
        /// <summary>
        /// MRP 控制者（物料计划人）
        /// </summary>
        public string? MrpController { get; set; }
        /// <summary>
        /// 标识: MRP控制者是买方(未激活的)
        /// </summary>
        public int MrpControllerIsBuyerInactive { get; set; } 
        /// <summary>
        /// 计划的天数内交货
        /// </summary>
        public decimal PlannedDeliveryTimeDays { get; set; } 
        /// <summary>
        /// 以天计的收货处理时间
        /// </summary>
        public decimal GoodsReceiptProcessingTimeDays { get; set; } 
        /// <summary>
        /// 期间标识
        /// </summary>
        public string? PeriodIndicator { get; set; }
        /// <summary>
        /// 装配报废百分比
        /// </summary>
        public decimal AssemblyScrapPercentage { get; set; } 
        /// <summary>
        /// 批量 (物料计划)
        /// </summary>
        public decimal LotSizeMaterialPlanning { get; set; } 
        /// <summary>
        /// 采购类型
        /// </summary>
        public string? ProcurementType { get; set; }
        /// <summary>
        /// 特殊采购类型
        /// </summary>
        public string? SpecialProcurementType { get; set; }
        /// <summary>
        /// 再订货点
        /// </summary>
        public decimal ReorderPoint { get; set; } 
        /// <summary>
        /// 安全库存
        /// </summary>
        public decimal SafetyStock { get; set; } 
        /// <summary>
        /// 最小批量
        /// </summary>
        public decimal MinimumLotSize { get; set; } 
        /// <summary>
        /// 最大批量大小
        /// </summary>
        public decimal MaximumLotSize { get; set; } 
        /// <summary>
        /// 固定批量大小
        /// </summary>
        public decimal FixedLotSize { get; set; } 
        /// <summary>
        /// 采购订单数量的舍入值
        /// </summary>
        public decimal PurchaseOrderQuantityRoundingValue { get; set; } 
        /// <summary>
        /// 最大库存水平
        /// </summary>
        public decimal MaximumStockLevel { get; set; } 
        /// <summary>
        /// 订购成本
        /// </summary>
        public decimal OrderingCosts { get; set; } 
        /// <summary>
        /// 对于独立和集中需求的相关需求标识
        /// </summary>
        public int DependentRequirementsIndicatorIndependentCollective { get; set; } 
        /// <summary>
        /// 库存成本标识
        /// </summary>
        public int StorageCostsIndicator { get; set; } 
        /// <summary>
        /// 选择可替换物料单的方法
        /// </summary>
        public string? AlternativeBomSelectionMethod { get; set; }
        /// <summary>
        /// 中止指示符
        /// </summary>
        public int DiscontinuationIndicator { get; set; } 
        /// <summary>
        /// 中断日期
        /// </summary>
        public DateTime? DiscontinuationDate { get; set; }
        /// <summary>
        /// 后续物料
        /// </summary>
        public string? FollowUpMaterial { get; set; }
        /// <summary>
        /// 需求分组指示符
        /// </summary>
        public string? RequirementsGroupingIndicator { get; set; }
        /// <summary>
        /// 综合MRP标识
        /// </summary>
        public int MixedMrpIndicator { get; set; } 
        /// <summary>
        /// 浮动的计划边际码
        /// </summary>
        public string? FloatingScheduleMarginKey { get; set; }
        /// <summary>
        /// 标识: 计划订单的自动修正
        /// </summary>
        public int AutomaticPlannedOrderAdjustment { get; set; } 
        /// <summary>
        /// 用于生产订单的批准标识
        /// </summary>
        public int ProductionOrderApprovalIndicator { get; set; } 
        /// <summary>
        /// 标识：反冲
        /// </summary>
        public int BackflushIndicator { get; set; } 
        // ==================== 生产参数 ====================
        /// <summary>
        /// 生产管理员
        /// </summary>
        public string? ProductionSupervisor { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        public decimal ProcessingTime { get; set; } 
        /// <summary>
        /// 建立和拆卸时间
        /// </summary>
        public decimal SetupAndTeardownTime { get; set; } 
        /// <summary>
        /// 工序间时间
        /// </summary>
        public decimal InteroperationTime { get; set; } 
        /// <summary>
        /// 基准数量
        /// </summary>
        public decimal BaseQuantity { get; set; } 
        /// <summary>
        /// 厂内生产时间
        /// </summary>
        public decimal InHouseProductionTime { get; set; } 
        /// <summary>
        /// 最大存储期间
        /// </summary>
        public int MaximumStoragePeriod { get; set; } 
        /// <summary>
        /// 最大库存期间单位
        /// </summary>
        public string? MaximumStoragePeriodUnit { get; set; }
        /// <summary>
        /// 标识: 从生产区的库存提取
        /// </summary>
        public int WithdrawalFromProductionBinIndicator { get; set; } 
        /// <summary>
        /// 标识: 在初步计划中包括的物料
        /// </summary>
        public int MaterialIncludedInRoughCutPlanning { get; set; } 
        /// <summary>
        /// 超量交货容差限制
        /// </summary>
        public decimal OverdeliveryToleranceLimit { get; set; } 
        /// <summary>
        /// 标识：允许未限制的过量交货
        /// </summary>
        public int UnlimitedOverdeliveryAllowed { get; set; } 
        /// <summary>
        /// 不足交货容差限制
        /// </summary>
        public decimal UnderdeliveryToleranceLimit { get; set; } 
        /// <summary>
        /// 总计补货提前时间(按工作日)
        /// </summary>
        public decimal TotalReplenishmentLeadTimeWorkdays { get; set; } 
        /// <summary>
        /// 替换部件
        /// </summary>
        public string? ReplacementPart { get; set; }
        /// <summary>
        /// 用百分比表示的成本的附加因子
        /// </summary>
        public decimal SurchargeFactorForCostsPercentage { get; set; } 
        /// <summary>
        /// 生产状态
        /// </summary>
        public string? ProductionStatus { get; set; }
        // ==================== 质量管理 ====================
        /// <summary>
        /// 过帐到检验库存
        /// </summary>
        public int PostToInspectionStock { get; set; } 
        /// <summary>
        /// 质量检查的样本(在%中)(取消激活)
        /// </summary>
        public decimal QualityInspectionSamplePercentageInactive { get; set; } 
        /// <summary>
        /// 隔离期(未激活)
        /// </summary>
        public int QuarantinePeriodInactive { get; set; } 
        /// <summary>
        /// 采购中质量管理的控制码
        /// </summary>
        public string? QmControlKeyProcurement { get; set; }
        /// <summary>
        /// 平均检查持续期间(未激活的的)
        /// </summary>
        public decimal AverageInspectionDurationInactive { get; set; } 
        /// <summary>
        /// 检查计划的标识(未激活)
        /// </summary>
        public int InspectionSetupIndicatorInactive { get; set; } 
        /// <summary>
        /// 凭证需求标识
        /// </summary>
        public int DocumentationRequiredIndicator { get; set; } 
        /// <summary>
        /// 活动性物质内容(未激活的)
        /// </summary>
        public decimal ActiveSubstanceContentInactive { get; set; } 
        /// <summary>
        /// 循环检查间隔
        /// </summary>
        public int RecurringInspectionInterval { get; set; } 
        /// <summary>
        /// 根据检验抽样检查的日期(取消激活)
        /// </summary>
        public DateTime? SampleInspectionDateInactive { get; set; }
        // ==================== 库存管理 ====================
        /// <summary>
        /// 中转库存（工厂到工厂）
        /// </summary>
        public decimal StockInTransitPlantToPlant { get; set; } 
        /// <summary>
        /// 装载组
        /// </summary>
        public string? LoadingGroup { get; set; }
        /// <summary>
        /// 批次管理需求的标识
        /// </summary>
        public int BatchManagementRequirementIndicator { get; set; } 
        /// <summary>
        /// 配额分配使用
        /// </summary>
        public string? QuotaArrangementUsage { get; set; }
        /// <summary>
        /// 服务水平
        /// </summary>
        public decimal ServiceLevel { get; set; } 
        /// <summary>
        /// 分割标识
        /// </summary>
        public int SplittingIndicator { get; set; } 
        /// <summary>
        /// 计划版本
        /// </summary>
        public string? PlanVersion { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>
        public string? ObjectType { get; set; }
        /// <summary>
        /// 对象标识
        /// </summary>
        public string? ObjectId { get; set; }
        /// <summary>
        /// 可用性检查的检查组
        /// </summary>
        public string? CheckingGroupAvailabilityCheck { get; set; }
        /// <summary>
        /// 会计年度变式
        /// </summary>
        public string? FiscalYearVariant { get; set; }
        /// <summary>
        /// 标识: 考虑修正因子
        /// </summary>
        public int CorrectionFactorIndicator { get; set; } 
        /// <summary>
        /// 装运建立时间
        /// </summary>
        public decimal ShippingSetupTime { get; set; } 
        /// <summary>
        /// 在装运中有关能力计划的基准数量
        /// </summary>
        public decimal BaseQuantityCapacityPlanningShipping { get; set; } 
        /// <summary>
        /// 处理时间: 装运
        /// </summary>
        public decimal ProcessingTimeShipping { get; set; } 
        /// <summary>
        /// 取消激活的
        /// </summary>
        public int Deactivated { get; set; } 
        /// <summary>
        /// 货源
        /// </summary>
        public string? SourceOfSupply { get; set; }
        /// <summary>
        /// 标识: "允许自动采购订单"
        /// </summary>
        public int AutomaticPurchaseOrderAllowed { get; set; } 
        /// <summary>
        /// 标识: 源清单要求
        /// </summary>
        public int SourceListRequirement { get; set; } 
        /// <summary>
        /// 外贸的商品代码和进口代码
        /// </summary>
        public string? CommodityCodeImportCodeForeignTrade { get; set; }
        /// <summary>
        /// 物料原产地国家
        /// </summary>
        public string? CountryOfOrigin { get; set; }
        /// <summary>
        /// 物料原产地（非特惠货源）
        /// </summary>
        public string? RegionOfOriginNonPreferential { get; set; }
        /// <summary>
        /// 商品代码的计量单位(外贸)
        /// </summary>
        public string? CommodityCodeUnitOfMeasureForeignTrade { get; set; }
        /// <summary>
        /// 出口/进口物料组
        /// </summary>
        public string? ExportImportMaterialGroup { get; set; }
        // ==================== 成本核算 ====================
        /// <summary>
        /// 利润中心
        /// </summary>
        public string? ProfitCenter { get; set; }
        /// <summary>
        /// 在途库存
        /// </summary>
        public decimal StockInTransit { get; set; } 
        // ==================== 生产计划 ====================
        /// <summary>
        /// PPC 计划日历
        /// </summary>
        public string? PpcPlanningCalendar { get; set; }
        /// <summary>
        /// 标识: 允许的重复制造
        /// </summary>
        public int RepetitiveManufacturingAllowed { get; set; } 
        /// <summary>
        /// 计划的时界
        /// </summary>
        public int PlanningTimeFence { get; set; } 
        /// <summary>
        /// 消耗模式
        /// </summary>
        public string? ConsumptionMode { get; set; }
        /// <summary>
        /// 消耗期间:逆向
        /// </summary>
        public int ConsumptionPeriodBackward { get; set; } 
        /// <summary>
        /// 消耗时期-向前
        /// </summary>
        public int ConsumptionPeriodForward { get; set; } 
        /// <summary>
        /// 版本标识符
        /// </summary>
        public string? VersionIdentifier { get; set; }
        /// <summary>
        /// 可选的 BOM
        /// </summary>
        public string? AlternativeBom { get; set; }
        /// <summary>
        /// BOM 用途
        /// </summary>
        public string? BomUsage { get; set; }
        /// <summary>
        /// 任务清单组码
        /// </summary>
        public string? TaskListGroup { get; set; }
        /// <summary>
        /// 组计数器
        /// </summary>
        public int GroupCounter { get; set; } 
        /// <summary>
        /// 批量产品成本核算
        /// </summary>
        public decimal LotSizeCosting { get; set; } 
        /// <summary>
        /// 成本核算的特殊采购类型
        /// </summary>
        public string? SpecialProcurementTypeCosting { get; set; }
        /// <summary>
        /// 生产单位
        /// </summary>
        public string? ProductionUnit { get; set; }
        /// <summary>
        /// 发货库存地点
        /// </summary>
        public string? IssueStorageLocation { get; set; }
        /// <summary>
        /// 物料需求计划组
        /// </summary>
        public string? MrpGroup { get; set; }
        /// <summary>
        /// 部件废品百分数
        /// </summary>
        public decimal ComponentScrapPercentage { get; set; } 
        /// <summary>
        /// 证书类型
        /// </summary>
        public string? CertificateType { get; set; }
        /// <summary>
        /// 物料/工厂的检验设置存在
        /// </summary>
        public int InspectionSetupExistsMaterialPlant { get; set; } 
        /// <summary>
        /// 间隔时间（多个计划订单分阶段生产时，计划订单之间的间隔时间)
        /// </summary>
        public decimal IntervalTimeStagedProduction { get; set; } 
        /// <summary>
        /// 供货天数参数文件
        /// </summary>
        public string? SupplyDaysProfile { get; set; }
        /// <summary>
        /// 连接到SOP上的CO/PA局部字段名
        /// </summary>
        public string? CopaLocalFieldNameConnectedToSop { get; set; }
        /// <summary>
        /// 周期盘点的盘点标识
        /// </summary>
        public string? CycleCountingIndicator { get; set; }
        /// <summary>
        /// 差异码
        /// </summary>
        public string? VarianceKey { get; set; }
        /// <summary>
        /// 序列号参数文件
        /// </summary>
        public string? SerialNumberProfile { get; set; }
        /// <summary>
        /// 内部对象号
        /// </summary>
        public string? InternalObjectNumber { get; set; }
        /// <summary>
        /// 可配置的物料
        /// </summary>
        public int ConfigurableMaterial { get; set; } 
        /// <summary>
        /// 重复生产参数文件
        /// </summary>
        public string? RepetitiveManufacturingProfile { get; set; }
        /// <summary>
        /// 工厂中允许负库存
        /// </summary>
        public int NegativeStocksAllowedInPlant { get; set; } 
        /// <summary>
        /// 要求的供应商质量管理系统
        /// </summary>
        public string? RequiredQmSystemSupplier { get; set; }
        /// <summary>
        /// 计划周期
        /// </summary>
        public string? PlanningCycle { get; set; }
        /// <summary>
        /// 舍入参数文件
        /// </summary>
        public string? RoundingProfile { get; set; }
        /// <summary>
        /// 消耗的参考物料
        /// </summary>
        public string? ConsumptionReferenceMaterial { get; set; }
        /// <summary>
        /// 消耗的参考工厂
        /// </summary>
        public string? ConsumptionReferencePlant { get; set; }
        /// <summary>
        /// 到消耗物料将被复制的日期
        /// </summary>
        public DateTime? DateConsumptionMaterialCopied { get; set; }
        /// <summary>
        /// 消耗的参考物料的乘数
        /// </summary>
        public decimal ConsumptionReferenceMaterialMultiplier { get; set; } 
        /// <summary>
        /// 自动重置预测模式
        /// </summary>
        public int AutomaticResetForecastModel { get; set; } 
        /// <summary>
        /// 进/出口中优惠指示符
        /// </summary>
        public string? PreferenceIndicatorImportExport { get; set; }
        // ==================== 贸易和法律 ====================
        /// <summary>
        /// 免税证明：法律控制指示符
        /// </summary>
        public int TaxExemptionCertificateLegalControlIndicator { get; set; } 
        /// <summary>
        /// 法律控制的免税证书编号
        /// </summary>
        public string? LegalControlTaxExemptionCertificateNumber { get; set; }
        /// <summary>
        /// 免税证明：免税证书的发放日
        /// </summary>
        public DateTime? TaxExemptionCertificateIssueDate { get; set; }
        /// <summary>
        /// 标识：存在供应商申报
        /// </summary>
        public int VendorDeclarationExists { get; set; } 
        /// <summary>
        /// 供应商申报的有效日期
        /// </summary>
        public DateTime? VendorDeclarationValidFrom { get; set; }
        /// <summary>
        /// 指示符：军用物资
        /// </summary>
        public int MilitaryGoodsIndicator { get; set; } 
        /// <summary>
        /// IS－R服务级别
        /// </summary>
        public string? IsRServiceLevel { get; set; }
        /// <summary>
        /// 指示符 : 物料是联产品
        /// </summary>
        public int MaterialIsCoProductIndicator { get; set; } 
        /// <summary>
        /// 计划策略组
        /// </summary>
        public string? PlanningStrategyGroup { get; set; }
        /// <summary>
        /// 计划中的可配置物料的内部对象号
        /// </summary>
        public string? InternalObjectNumberConfigurableMaterialPlanning { get; set; }
        /// <summary>
        /// 外部采购的缺省仓储位置
        /// </summary>
        public string? DefaultStorageLocationExternalProcurement { get; set; }
        /// <summary>
        /// 标识：散装物料
        /// </summary>
        public int BulkMaterialIndicator { get; set; } 
        /// <summary>
        /// 周期标识被固定
        /// </summary>
        public int CycleIndicatorFixed { get; set; } 
        /// <summary>
        /// 库存确定组
        /// </summary>
        public string? StockDeterminationGroup { get; set; }
        /// <summary>
        /// QM 中活动的物料授权组
        /// </summary>
        public string? MaterialAuthorizationGroupActiveQm { get; set; }
        /// <summary>
        /// 计划独立需求的调整期间
        /// </summary>
        public int AdjustmentPeriodPlannedIndependentRequirements { get; set; } 
        /// <summary>
        /// 任务清单类型
        /// </summary>
        public string? TaskListType { get; set; }
        // ==================== 特殊指示符和标识 ====================
        /// <summary>
        /// 安全时间标识（含或不含安全时间）
        /// </summary>
        public int SafetyTimeIndicator { get; set; } 
        /// <summary>
        /// 安全时间（按工作日计算）
        /// </summary>
        public decimal SafetyTimeWorkdays { get; set; } 
        /// <summary>
        /// 活动控制：计划订单处理
        /// </summary>
        public string? ActiveControlPlannedOrderProcessing { get; set; }
        /// <summary>
        /// 在生产/处理订单中批量输入的确定
        /// </summary>
        public string? LotEntryDeterminationProductionProcessingOrder { get; set; }
        /// <summary>
        /// 计量单位组
        /// </summary>
        public string? UnitOfMeasureGroup { get; set; }
        /// <summary>
        /// 物料运输组
        /// </summary>
        public string? MaterialTransportationGroup { get; set; }
        /// <summary>
        /// VO 物料的库存转移销售值（工厂到工厂）
        /// </summary>
        public decimal VoMaterialStockTransferSalesValuePlantToPlant { get; set; } 
        /// <summary>
        /// 仅记值物料的销售价格计的运输值
        /// </summary>
        public decimal TransportationValueOnlyGoodsSalesPrice { get; set; } 
        /// <summary>
        /// 指示符: 平滑促销消耗
        /// </summary>
        public int SmoothPromotionalConsumptionIndicator { get; set; } 
        /// <summary>
        /// 将进行成本核算的生产版本
        /// </summary>
        public string? ProductionVersionToBeCosted { get; set; }
        /// <summary>
        /// 固定价格联产品
        /// </summary>
        public int FixedPriceCoProduct { get; set; } 
        /// <summary>
        /// 用于计算工作负荷的后勤处理组
        /// </summary>
        public string? LogisticsHandlingGroupWorkloadCalculation { get; set; }
        /// <summary>
        /// 工厂物料分销参数文件
        /// </summary>
        public string? PlantMaterialDistributionProfile { get; set; }
        /// <summary>
        /// 有约束的虚拟库存
        /// </summary>
        public decimal RestrictedVirtualStock { get; set; } 
        /// <summary>
        /// 连接空缺库存的销售价
        /// </summary>
        public decimal SalesPriceConnectingShortageStock { get; set; } 
        /// <summary>
        /// 物料: CFOP类别
        /// </summary>
        public string? MaterialCfopCategory { get; set; }
        /// <summary>
        /// 最小安全库存
        /// </summary>
        public decimal MinimumSafetyStock { get; set; } 
        /// <summary>
        /// 无成本核算
        /// </summary>
        public int NoCosting { get; set; } 
        /// <summary>
        /// 库存入库和库存出库的策略
        /// </summary>
        public string? StockInStockOutStrategy { get; set; }
        /// <summary>
        /// 初始批次管理的标识
        /// </summary>
        public int InitialBatchManagementIndicator { get; set; } 
        /// <summary>
        /// 初始批量的参考物料
        /// </summary>
        public string? InitialLotReferenceMaterial { get; set; }
        /// <summary>
        /// 评估的收货锁定库存
        /// </summary>
        public decimal ValuatedGoodsReceiptBlockedStock { get; set; } 
        /// <summary>
        /// 分段策略
        /// </summary>
        public string? SegmentationStrategy { get; set; }
        /// <summary>
        /// 分段状态
        /// </summary>
        public string? SegmentationStatus { get; set; }
        /// <summary>
        /// 分段策略范围
        /// </summary>
        public string? SegmentationStrategyScope { get; set; }
        /// <summary>
        /// 根据交货日期或段排序库存
        /// </summary>
        public string? SortStockByDeliveryDateOrSegment { get; set; }
        /// <summary>
        /// 消耗优先级
        /// </summary>
        public int ConsumptionPriority { get; set; } 
        /// <summary>
        /// 离散批次编号
        /// </summary>
        public string? DiscreteBatchNumber { get; set; }
        /// <summary>
        /// 库存保护标识
        /// </summary>
        public int StockProtectionIndicator { get; set; } 
        /// <summary>
        /// 订单分配运行
        /// </summary>
        public int OrderAllocationRun { get; set; } 
        /// <summary>
        /// 按天数发货处理时间
        /// </summary>
        public decimal ShippingProcessingTimeDays { get; set; } 
        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }

    }
    /// <summary>
    /// 工厂物料更新DTO
    /// </summary>
    public class TaktFtyMaterialUpdateDto:TaktFtyMaterialCreateDto
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long FtyMaterialId { get; set; }

    }
}

