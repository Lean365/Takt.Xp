#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPltMaterial.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 工厂级物料主数据实体类 (基于SAP MM物料管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Material.Master
{
    /// <summary>
    /// 工厂级物料主数据实体类 (基于SAP MM物料管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP MM 物料管理模块 - 工厂级视图
    /// </remarks>
    [SugarTable("Takt_logistics_material_plt", "工厂物料")]
    [SugarIndex("ix_plt_material", nameof(PlantCode), OrderByType.Asc, nameof(MaterialCode), OrderByType.Asc, true)]
    [SugarIndex("ix_material_code", nameof(MaterialCode), OrderByType.Asc, false)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktPlantMaterial : TaktBaseEntity
    {
        /// <summary>
        /// 工厂
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        // ==================== 基础信息 ====================

        /// <summary>
        /// 物料号
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料号", Length = 40, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 维护状态
        /// </summary>
        [SugarColumn(ColumnName = "maintenance_status", ColumnDescription = "维护状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MaintenanceStatus { get; set; } = 1;

        /// <summary>
        /// 在工厂级标记要删除的物料
        /// </summary>
        [SugarColumn(ColumnName = "marked_for_deletion_at_plant_level", ColumnDescription = "在工厂级标记要删除的物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MarkedForDeletionAtPlantLevel { get; set; } = 0;

        /// <summary>
        /// 评估类别
        /// </summary>
        [SugarColumn(ColumnName = "valuation_category", ColumnDescription = "评估类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ValuationCategory { get; set; }

        /// <summary>
        /// 批量管理标识(内部)
        /// </summary>
        [SugarColumn(ColumnName = "batch_management_indicator_internal", ColumnDescription = "批量管理标识(内部)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BatchManagementIndicatorInternal { get; set; } = 0;

        /// <summary>
        /// 工厂特定的物料状态
        /// </summary>
        [SugarColumn(ColumnName = "plant_specific_material_status", ColumnDescription = "工厂特定的物料状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantSpecificMaterialStatus { get; set; }

        /// <summary>
        /// 工厂特定物料状态有效的起始日期
        /// </summary>
        [SugarColumn(ColumnName = "plant_specific_status_valid_from", ColumnDescription = "工厂特定物料状态有效的起始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PlantSpecificStatusValidFrom { get; set; }

        /// <summary>
        /// ABC标识
        /// </summary>
        [SugarColumn(ColumnName = "abc_indicator", ColumnDescription = "ABC标识", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AbcIndicator { get; set; }

        /// <summary>
        /// 标志：关键部件
        /// </summary>
        [SugarColumn(ColumnName = "critical_part_indicator", ColumnDescription = "标志：关键部件", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CriticalPartIndicator { get; set; } = 0;

        // ==================== 采购组织 ====================

        /// <summary>
        /// 采购组
        /// </summary>
        [SugarColumn(ColumnName = "purchasing_group", ColumnDescription = "采购组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchasingGroup { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        [SugarColumn(ColumnName = "issue_unit", ColumnDescription = "发货单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueUnit { get; set; }

        // ==================== MRP 参数 ====================

        /// <summary>
        /// 物料: MRP 参数文件
        /// </summary>
        [SugarColumn(ColumnName = "material_mrp_profile", ColumnDescription = "物料: MRP 参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialMrpProfile { get; set; }

        /// <summary>
        /// 物料需求计划类型
        /// </summary>
        [SugarColumn(ColumnName = "mrp_type", ColumnDescription = "物料需求计划类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpType { get; set; }

        /// <summary>
        /// MRP 控制者（物料计划人）
        /// </summary>
        [SugarColumn(ColumnName = "mrp_controller", ColumnDescription = "MRP 控制者（物料计划人）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpController { get; set; }

        /// <summary>
        /// 标识: MRP控制者是买方(未激活的)
        /// </summary>
        [SugarColumn(ColumnName = "mrp_controller_is_buyer_inactive", ColumnDescription = "标识: MRP控制者是买方(未激活的)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MrpControllerIsBuyerInactive { get; set; } = 0;

        /// <summary>
        /// 计划的天数内交货
        /// </summary>
        [SugarColumn(ColumnName = "planned_delivery_time_days", ColumnDescription = "计划的天数内交货", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal PlannedDeliveryTimeDays { get; set; } = 0;

        /// <summary>
        /// 以天计的收货处理时间
        /// </summary>
        [SugarColumn(ColumnName = "goods_receipt_processing_time_days", ColumnDescription = "以天计的收货处理时间", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal GoodsReceiptProcessingTimeDays { get; set; } = 0;

        /// <summary>
        /// 期间标识
        /// </summary>
        [SugarColumn(ColumnName = "period_indicator", ColumnDescription = "期间标识", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PeriodIndicator { get; set; }

        /// <summary>
        /// 装配报废百分比
        /// </summary>
        [SugarColumn(ColumnName = "assembly_scrap_percentage", ColumnDescription = "装配报废百分比", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal AssemblyScrapPercentage { get; set; } = 0;

        /// <summary>
        /// 批量 (物料计划)
        /// </summary>
        [SugarColumn(ColumnName = "lot_size_material_planning", ColumnDescription = "批量 (物料计划)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal LotSizeMaterialPlanning { get; set; } = 0;

        /// <summary>
        /// 采购类型
        /// </summary>
        [SugarColumn(ColumnName = "procurement_type", ColumnDescription = "采购类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcurementType { get; set; }

        /// <summary>
        /// 特殊采购类型
        /// </summary>
        [SugarColumn(ColumnName = "special_procurement_type", ColumnDescription = "特殊采购类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialProcurementType { get; set; }

        /// <summary>
        /// 再订货点
        /// </summary>
        [SugarColumn(ColumnName = "reorder_point", ColumnDescription = "再订货点", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ReorderPoint { get; set; } = 0;

        /// <summary>
        /// 安全库存
        /// </summary>
        [SugarColumn(ColumnName = "safety_stock", ColumnDescription = "安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal SafetyStock { get; set; } = 0;

        /// <summary>
        /// 最小批量
        /// </summary>
        [SugarColumn(ColumnName = "minimum_lot_size", ColumnDescription = "最小批量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinimumLotSize { get; set; } = 0;

        /// <summary>
        /// 最大批量大小
        /// </summary>
        [SugarColumn(ColumnName = "maximum_lot_size", ColumnDescription = "最大批量大小", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaximumLotSize { get; set; } = 0;

        /// <summary>
        /// 固定批量大小
        /// </summary>
        [SugarColumn(ColumnName = "fixed_lot_size", ColumnDescription = "固定批量大小", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal FixedLotSize { get; set; } = 0;

        /// <summary>
        /// 采购订单数量的舍入值
        /// </summary>
        [SugarColumn(ColumnName = "purchase_order_quantity_rounding_value", ColumnDescription = "采购订单数量的舍入值", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal PurchaseOrderQuantityRoundingValue { get; set; } = 0;

        /// <summary>
        /// 最大库存水平
        /// </summary>
        [SugarColumn(ColumnName = "maximum_stock_level", ColumnDescription = "最大库存水平", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MaximumStockLevel { get; set; } = 0;

        /// <summary>
        /// 订购成本
        /// </summary>
        [SugarColumn(ColumnName = "ordering_costs", ColumnDescription = "订购成本", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OrderingCosts { get; set; } = 0;

        /// <summary>
        /// 对于独立和集中需求的相关需求标识
        /// </summary>
        [SugarColumn(ColumnName = "dependent_requirements_indicator_independent_collective", ColumnDescription = "对于独立和集中需求的相关需求标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DependentRequirementsIndicatorIndependentCollective { get; set; } = 0;

        /// <summary>
        /// 库存成本标识
        /// </summary>
        [SugarColumn(ColumnName = "storage_costs_indicator", ColumnDescription = "库存成本标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StorageCostsIndicator { get; set; } = 0;

        /// <summary>
        /// 选择可替换物料单的方法
        /// </summary>
        [SugarColumn(ColumnName = "alternative_bom_selection_method", ColumnDescription = "选择可替换物料单的方法", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AlternativeBomSelectionMethod { get; set; }

        /// <summary>
        /// 中止指示符
        /// </summary>
        [SugarColumn(ColumnName = "discontinuation_indicator", ColumnDescription = "中止指示符", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DiscontinuationIndicator { get; set; } = 0;

        /// <summary>
        /// 中断日期
        /// </summary>
        [SugarColumn(ColumnName = "discontinuation_date", ColumnDescription = "中断日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DiscontinuationDate { get; set; }

        /// <summary>
        /// 后续物料
        /// </summary>
        [SugarColumn(ColumnName = "follow_up_material", ColumnDescription = "后续物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FollowUpMaterial { get; set; }

        /// <summary>
        /// 需求分组指示符
        /// </summary>
        [SugarColumn(ColumnName = "requirements_grouping_indicator", ColumnDescription = "需求分组指示符", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RequirementsGroupingIndicator { get; set; }

        /// <summary>
        /// 综合MRP标识
        /// </summary>
        [SugarColumn(ColumnName = "mixed_mrp_indicator", ColumnDescription = "综合MRP标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MixedMrpIndicator { get; set; } = 0;

        /// <summary>
        /// 浮动的计划边际码
        /// </summary>
        [SugarColumn(ColumnName = "floating_schedule_margin_key", ColumnDescription = "浮动的计划边际码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FloatingScheduleMarginKey { get; set; }

        /// <summary>
        /// 标识: 计划订单的自动修正
        /// </summary>
        [SugarColumn(ColumnName = "automatic_planned_order_adjustment", ColumnDescription = "标识: 计划订单的自动修正", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AutomaticPlannedOrderAdjustment { get; set; } = 0;

        /// <summary>
        /// 用于生产订单的批准标识
        /// </summary>
        [SugarColumn(ColumnName = "production_order_approval_indicator", ColumnDescription = "用于生产订单的批准标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProductionOrderApprovalIndicator { get; set; } = 0;

        /// <summary>
        /// 标识：反冲
        /// </summary>
        [SugarColumn(ColumnName = "backflush_indicator", ColumnDescription = "标识：反冲", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BackflushIndicator { get; set; } = 0;

        // ==================== 生产参数 ====================

        /// <summary>
        /// 生产管理员
        /// </summary>
        [SugarColumn(ColumnName = "production_supervisor", ColumnDescription = "生产管理员", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionSupervisor { get; set; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [SugarColumn(ColumnName = "processing_time", ColumnDescription = "处理时间", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessingTime { get; set; } = 0;

        /// <summary>
        /// 建立和拆卸时间
        /// </summary>
        [SugarColumn(ColumnName = "setup_and_teardown_time", ColumnDescription = "建立和拆卸时间", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal SetupAndTeardownTime { get; set; } = 0;

        /// <summary>
        /// 工序间时间
        /// </summary>
        [SugarColumn(ColumnName = "interoperation_time", ColumnDescription = "工序间时间", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal InteroperationTime { get; set; } = 0;

        /// <summary>
        /// 基准数量
        /// </summary>
        [SugarColumn(ColumnName = "base_quantity", ColumnDescription = "基准数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal BaseQuantity { get; set; } = 0;

        /// <summary>
        /// 厂内生产时间
        /// </summary>
        [SugarColumn(ColumnName = "in_house_production_time", ColumnDescription = "厂内生产时间", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal InHouseProductionTime { get; set; } = 0;

        /// <summary>
        /// 最大存储期间
        /// </summary>
        [SugarColumn(ColumnName = "maximum_storage_period", ColumnDescription = "最大存储期间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaximumStoragePeriod { get; set; } = 0;

        /// <summary>
        /// 最大库存期间单位
        /// </summary>
        [SugarColumn(ColumnName = "maximum_storage_period_unit", ColumnDescription = "最大库存期间单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaximumStoragePeriodUnit { get; set; }

        /// <summary>
        /// 标识: 从生产区的库存提取
        /// </summary>
        [SugarColumn(ColumnName = "withdrawal_from_production_bin_indicator", ColumnDescription = "标识: 从生产区的库存提取", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int WithdrawalFromProductionBinIndicator { get; set; } = 0;

        /// <summary>
        /// 标识: 在初步计划中包括的物料
        /// </summary>
        [SugarColumn(ColumnName = "material_included_in_rough_cut_planning", ColumnDescription = "标识: 在初步计划中包括的物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialIncludedInRoughCutPlanning { get; set; } = 0;

        /// <summary>
        /// 超量交货容差限制
        /// </summary>
        [SugarColumn(ColumnName = "overdelivery_tolerance_limit", ColumnDescription = "超量交货容差限制", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OverdeliveryToleranceLimit { get; set; } = 0;

        /// <summary>
        /// 标识：允许未限制的过量交货
        /// </summary>
        [SugarColumn(ColumnName = "unlimited_overdelivery_allowed", ColumnDescription = "标识：允许未限制的过量交货", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UnlimitedOverdeliveryAllowed { get; set; } = 0;

        /// <summary>
        /// 不足交货容差限制
        /// </summary>
        [SugarColumn(ColumnName = "underdelivery_tolerance_limit", ColumnDescription = "不足交货容差限制", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnderdeliveryToleranceLimit { get; set; } = 0;

        /// <summary>
        /// 总计补货提前时间(按工作日)
        /// </summary>
        [SugarColumn(ColumnName = "total_replenishment_lead_time_workdays", ColumnDescription = "总计补货提前时间(按工作日)", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalReplenishmentLeadTimeWorkdays { get; set; } = 0;

        /// <summary>
        /// 替换部件
        /// </summary>
        [SugarColumn(ColumnName = "replacement_part", ColumnDescription = "替换部件", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReplacementPart { get; set; }

        /// <summary>
        /// 用百分比表示的成本的附加因子
        /// </summary>
        [SugarColumn(ColumnName = "surcharge_factor_for_costs_percentage", ColumnDescription = "用百分比表示的成本的附加因子", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal SurchargeFactorForCostsPercentage { get; set; } = 0;

        /// <summary>
        /// 生产状态
        /// </summary>
        [SugarColumn(ColumnName = "production_status", ColumnDescription = "生产状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionStatus { get; set; }

        // ==================== 质量管理 ====================

        /// <summary>
        /// 过帐到检验库存
        /// </summary>
        [SugarColumn(ColumnName = "post_to_inspection_stock", ColumnDescription = "过帐到检验库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PostToInspectionStock { get; set; } = 0;

        /// <summary>
        /// 质量检查的样本(在%中)(取消激活)
        /// </summary>
        [SugarColumn(ColumnName = "quality_inspection_sample_percentage_inactive", ColumnDescription = "质量检查的样本(在%中)(取消激活)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal QualityInspectionSamplePercentageInactive { get; set; } = 0;

        /// <summary>
        /// 隔离期(未激活)
        /// </summary>
        [SugarColumn(ColumnName = "quarantine_period_inactive", ColumnDescription = "隔离期(未激活)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int QuarantinePeriodInactive { get; set; } = 0;

        /// <summary>
        /// 采购中质量管理的控制码
        /// </summary>
        [SugarColumn(ColumnName = "qm_control_key_procurement", ColumnDescription = "采购中质量管理的控制码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QmControlKeyProcurement { get; set; }

        /// <summary>
        /// 平均检查持续期间(未激活的的)
        /// </summary>
        [SugarColumn(ColumnName = "average_inspection_duration_inactive", ColumnDescription = "平均检查持续期间(未激活的的)", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal AverageInspectionDurationInactive { get; set; } = 0;

        /// <summary>
        /// 检查计划的标识(未激活)
        /// </summary>
        [SugarColumn(ColumnName = "inspection_setup_indicator_inactive", ColumnDescription = "检查计划的标识(未激活)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int InspectionSetupIndicatorInactive { get; set; } = 0;

        /// <summary>
        /// 凭证需求标识
        /// </summary>
        [SugarColumn(ColumnName = "documentation_required_indicator", ColumnDescription = "凭证需求标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DocumentationRequiredIndicator { get; set; } = 0;

        /// <summary>
        /// 活动性物质内容(未激活的)
        /// </summary>
        [SugarColumn(ColumnName = "active_substance_content_inactive", ColumnDescription = "活动性物质内容(未激活的)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ActiveSubstanceContentInactive { get; set; } = 0;

        /// <summary>
        /// 循环检查间隔
        /// </summary>
        [SugarColumn(ColumnName = "recurring_inspection_interval", ColumnDescription = "循环检查间隔", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RecurringInspectionInterval { get; set; } = 0;

        /// <summary>
        /// 根据检验抽样检查的日期(取消激活)
        /// </summary>
        [SugarColumn(ColumnName = "sample_inspection_date_inactive", ColumnDescription = "根据检验抽样检查的日期(取消激活)", ColumnDataType = "date", IsNullable = true)]
        public DateTime? SampleInspectionDateInactive { get; set; }

        // ==================== 库存管理 ====================

        /// <summary>
        /// 中转库存（工厂到工厂）
        /// </summary>
        [SugarColumn(ColumnName = "stock_in_transit_plant_to_plant", ColumnDescription = "中转库存（工厂到工厂）", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal StockInTransitPlantToPlant { get; set; } = 0;

        /// <summary>
        /// 装载组
        /// </summary>
        [SugarColumn(ColumnName = "loading_group", ColumnDescription = "装载组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoadingGroup { get; set; }

        /// <summary>
        /// 批次管理需求的标识
        /// </summary>
        [SugarColumn(ColumnName = "batch_management_requirement_indicator", ColumnDescription = "批次管理需求的标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BatchManagementRequirementIndicator { get; set; } = 0;

        /// <summary>
        /// 配额分配使用
        /// </summary>
        [SugarColumn(ColumnName = "quota_arrangement_usage", ColumnDescription = "配额分配使用", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QuotaArrangementUsage { get; set; }

        /// <summary>
        /// 服务水平
        /// </summary>
        [SugarColumn(ColumnName = "service_level", ColumnDescription = "服务水平", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ServiceLevel { get; set; } = 0;

        /// <summary>
        /// 分割标识
        /// </summary>
        [SugarColumn(ColumnName = "splitting_indicator", ColumnDescription = "分割标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SplittingIndicator { get; set; } = 0;

        /// <summary>
        /// 计划版本
        /// </summary>
        [SugarColumn(ColumnName = "plan_version", ColumnDescription = "计划版本", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanVersion { get; set; }

        /// <summary>
        /// 对象类型
        /// </summary>
        [SugarColumn(ColumnName = "object_type", ColumnDescription = "对象类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ObjectType { get; set; }

        /// <summary>
        /// 对象标识
        /// </summary>
        [SugarColumn(ColumnName = "object_id", ColumnDescription = "对象标识", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ObjectId { get; set; }

        /// <summary>
        /// 可用性检查的检查组
        /// </summary>
        [SugarColumn(ColumnName = "checking_group_availability_check", ColumnDescription = "可用性检查的检查组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CheckingGroupAvailabilityCheck { get; set; }

        /// <summary>
        /// 会计年度变式
        /// </summary>
        [SugarColumn(ColumnName = "fiscal_year_variant", ColumnDescription = "会计年度变式", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FiscalYearVariant { get; set; }

        /// <summary>
        /// 标识: 考虑修正因子
        /// </summary>
        [SugarColumn(ColumnName = "correction_factor_indicator", ColumnDescription = "标识: 考虑修正因子", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CorrectionFactorIndicator { get; set; } = 0;

        /// <summary>
        /// 装运建立时间
        /// </summary>
        [SugarColumn(ColumnName = "shipping_setup_time", ColumnDescription = "装运建立时间", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ShippingSetupTime { get; set; } = 0;

        /// <summary>
        /// 在装运中有关能力计划的基准数量
        /// </summary>
        [SugarColumn(ColumnName = "base_quantity_capacity_planning_shipping", ColumnDescription = "在装运中有关能力计划的基准数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal BaseQuantityCapacityPlanningShipping { get; set; } = 0;

        /// <summary>
        /// 处理时间: 装运
        /// </summary>
        [SugarColumn(ColumnName = "processing_time_shipping", ColumnDescription = "处理时间: 装运", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessingTimeShipping { get; set; } = 0;

        /// <summary>
        /// 取消激活的
        /// </summary>
        [SugarColumn(ColumnName = "deactivated", ColumnDescription = "取消激活的", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Deactivated { get; set; } = 0;

        /// <summary>
        /// 货源
        /// </summary>
        [SugarColumn(ColumnName = "source_of_supply", ColumnDescription = "货源", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SourceOfSupply { get; set; }

        /// <summary>
        /// 标识: "允许自动采购订单"
        /// </summary>
        [SugarColumn(ColumnName = "automatic_purchase_order_allowed", ColumnDescription = "标识: \"允许自动采购订单\"", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AutomaticPurchaseOrderAllowed { get; set; } = 0;

        /// <summary>
        /// 标识: 源清单要求
        /// </summary>
        [SugarColumn(ColumnName = "source_list_requirement", ColumnDescription = "标识: 源清单要求", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SourceListRequirement { get; set; } = 0;

        /// <summary>
        /// 外贸的商品代码和进口代码
        /// </summary>
        [SugarColumn(ColumnName = "commodity_code_import_code_foreign_trade", ColumnDescription = "外贸的商品代码和进口代码", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CommodityCodeImportCodeForeignTrade { get; set; }

        /// <summary>
        /// 物料原产地国家
        /// </summary>
        [SugarColumn(ColumnName = "country_of_origin", ColumnDescription = "物料原产地国家", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CountryOfOrigin { get; set; }

        /// <summary>
        /// 物料原产地（非特惠货源）
        /// </summary>
        [SugarColumn(ColumnName = "region_of_origin_non_preferential", ColumnDescription = "物料原产地（非特惠货源）", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RegionOfOriginNonPreferential { get; set; }

        /// <summary>
        /// 商品代码的计量单位(外贸)
        /// </summary>
        [SugarColumn(ColumnName = "commodity_code_unit_of_measure_foreign_trade", ColumnDescription = "商品代码的计量单位(外贸)", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CommodityCodeUnitOfMeasureForeignTrade { get; set; }

        /// <summary>
        /// 出口/进口物料组
        /// </summary>
        [SugarColumn(ColumnName = "export_import_material_group", ColumnDescription = "出口/进口物料组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExportImportMaterialGroup { get; set; }

        // ==================== 成本核算 ====================

        /// <summary>
        /// 利润中心
        /// </summary>
        [SugarColumn(ColumnName = "profit_center", ColumnDescription = "利润中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProfitCenter { get; set; }

        /// <summary>
        /// 在途库存
        /// </summary>
        [SugarColumn(ColumnName = "stock_in_transit", ColumnDescription = "在途库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal StockInTransit { get; set; } = 0;

        // ==================== 生产计划 ====================

        /// <summary>
        /// PPC 计划日历
        /// </summary>
        [SugarColumn(ColumnName = "ppc_planning_calendar", ColumnDescription = "PPC 计划日历", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PpcPlanningCalendar { get; set; }

        /// <summary>
        /// 标识: 允许的重复制造
        /// </summary>
        [SugarColumn(ColumnName = "repetitive_manufacturing_allowed", ColumnDescription = "标识: 允许的重复制造", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RepetitiveManufacturingAllowed { get; set; } = 0;

        /// <summary>
        /// 计划的时界
        /// </summary>
        [SugarColumn(ColumnName = "planning_time_fence", ColumnDescription = "计划的时界", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int PlanningTimeFence { get; set; } = 0;

        /// <summary>
        /// 消耗模式
        /// </summary>
        [SugarColumn(ColumnName = "consumption_mode", ColumnDescription = "消耗模式", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConsumptionMode { get; set; }

        /// <summary>
        /// 消耗期间:逆向
        /// </summary>
        [SugarColumn(ColumnName = "consumption_period_backward", ColumnDescription = "消耗期间:逆向", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConsumptionPeriodBackward { get; set; } = 0;

        /// <summary>
        /// 消耗时期-向前
        /// </summary>
        [SugarColumn(ColumnName = "consumption_period_forward", ColumnDescription = "消耗时期-向前", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConsumptionPeriodForward { get; set; } = 0;

        /// <summary>
        /// 版本标识符
        /// </summary>
        [SugarColumn(ColumnName = "version_identifier", ColumnDescription = "版本标识符", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VersionIdentifier { get; set; }

        /// <summary>
        /// 可选的 BOM
        /// </summary>
        [SugarColumn(ColumnName = "alternative_bom", ColumnDescription = "可选的 BOM", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AlternativeBom { get; set; }

        /// <summary>
        /// BOM 用途
        /// </summary>
        [SugarColumn(ColumnName = "bom_usage", ColumnDescription = "BOM 用途", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BomUsage { get; set; }

        /// <summary>
        /// 任务清单组码
        /// </summary>
        [SugarColumn(ColumnName = "task_list_group", ColumnDescription = "任务清单组码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskListGroup { get; set; }

        /// <summary>
        /// 组计数器
        /// </summary>
        [SugarColumn(ColumnName = "group_counter", ColumnDescription = "组计数器", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int GroupCounter { get; set; } = 0;

        /// <summary>
        /// 批量产品成本核算
        /// </summary>
        [SugarColumn(ColumnName = "lot_size_costing", ColumnDescription = "批量产品成本核算", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal LotSizeCosting { get; set; } = 0;

        /// <summary>
        /// 成本核算的特殊采购类型
        /// </summary>
        [SugarColumn(ColumnName = "special_procurement_type_costing", ColumnDescription = "成本核算的特殊采购类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SpecialProcurementTypeCosting { get; set; }

        /// <summary>
        /// 生产单位
        /// </summary>
        [SugarColumn(ColumnName = "production_unit", ColumnDescription = "生产单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionUnit { get; set; }

        /// <summary>
        /// 发货库存地点
        /// </summary>
        [SugarColumn(ColumnName = "issue_storage_location", ColumnDescription = "发货库存地点", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssueStorageLocation { get; set; }

        /// <summary>
        /// 物料需求计划组
        /// </summary>
        [SugarColumn(ColumnName = "mrp_group", ColumnDescription = "物料需求计划组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MrpGroup { get; set; }

        /// <summary>
        /// 部件废品百分数
        /// </summary>
        [SugarColumn(ColumnName = "component_scrap_percentage", ColumnDescription = "部件废品百分数", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ComponentScrapPercentage { get; set; } = 0;

        /// <summary>
        /// 证书类型
        /// </summary>
        [SugarColumn(ColumnName = "certificate_type", ColumnDescription = "证书类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CertificateType { get; set; }

        /// <summary>
        /// 物料/工厂的检验设置存在
        /// </summary>
        [SugarColumn(ColumnName = "inspection_setup_exists_material_plant", ColumnDescription = "物料/工厂的检验设置存在", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int InspectionSetupExistsMaterialPlant { get; set; } = 0;

        /// <summary>
        /// 间隔时间（多个计划订单分阶段生产时，计划订单之间的间隔时间)
        /// </summary>
        [SugarColumn(ColumnName = "interval_time_staged_production", ColumnDescription = "间隔时间（多个计划订单分阶段生产时，计划订单之间的间隔时间)", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal IntervalTimeStagedProduction { get; set; } = 0;

        /// <summary>
        /// 供货天数参数文件
        /// </summary>
        [SugarColumn(ColumnName = "supply_days_profile", ColumnDescription = "供货天数参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SupplyDaysProfile { get; set; }

        /// <summary>
        /// 连接到SOP上的CO/PA局部字段名
        /// </summary>
        [SugarColumn(ColumnName = "copa_local_field_name_connected_to_sop", ColumnDescription = "连接到SOP上的CO/PA局部字段名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CopaLocalFieldNameConnectedToSop { get; set; }

        /// <summary>
        /// 周期盘点的盘点标识
        /// </summary>
        [SugarColumn(ColumnName = "cycle_counting_indicator", ColumnDescription = "周期盘点的盘点标识", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CycleCountingIndicator { get; set; }

        /// <summary>
        /// 差异码
        /// </summary>
        [SugarColumn(ColumnName = "variance_key", ColumnDescription = "差异码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VarianceKey { get; set; }

        /// <summary>
        /// 序列号参数文件
        /// </summary>
        [SugarColumn(ColumnName = "serial_number_profile", ColumnDescription = "序列号参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNumberProfile { get; set; }

        /// <summary>
        /// 内部对象号
        /// </summary>
        [SugarColumn(ColumnName = "internal_object_number", ColumnDescription = "内部对象号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalObjectNumber { get; set; }

        /// <summary>
        /// 可配置的物料
        /// </summary>
        [SugarColumn(ColumnName = "configurable_material", ColumnDescription = "可配置的物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConfigurableMaterial { get; set; } = 0;

        /// <summary>
        /// 重复生产参数文件
        /// </summary>
        [SugarColumn(ColumnName = "repetitive_manufacturing_profile", ColumnDescription = "重复生产参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepetitiveManufacturingProfile { get; set; }

        /// <summary>
        /// 工厂中允许负库存
        /// </summary>
        [SugarColumn(ColumnName = "negative_stocks_allowed_in_plant", ColumnDescription = "工厂中允许负库存", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NegativeStocksAllowedInPlant { get; set; } = 0;

        /// <summary>
        /// 要求的供应商质量管理系统
        /// </summary>
        [SugarColumn(ColumnName = "required_qm_system_supplier", ColumnDescription = "要求的供应商质量管理系统", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RequiredQmSystemSupplier { get; set; }

        /// <summary>
        /// 计划周期
        /// </summary>
        [SugarColumn(ColumnName = "planning_cycle", ColumnDescription = "计划周期", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanningCycle { get; set; }

        /// <summary>
        /// 舍入参数文件
        /// </summary>
        [SugarColumn(ColumnName = "rounding_profile", ColumnDescription = "舍入参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RoundingProfile { get; set; }

        /// <summary>
        /// 消耗的参考物料
        /// </summary>
        [SugarColumn(ColumnName = "consumption_reference_material", ColumnDescription = "消耗的参考物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConsumptionReferenceMaterial { get; set; }

        /// <summary>
        /// 消耗的参考工厂
        /// </summary>
        [SugarColumn(ColumnName = "consumption_reference_plant", ColumnDescription = "消耗的参考工厂", Length = 8, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ConsumptionReferencePlant { get; set; }

        /// <summary>
        /// 到消耗物料将被复制的日期
        /// </summary>
        [SugarColumn(ColumnName = "date_consumption_material_copied", ColumnDescription = "到消耗物料将被复制的日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DateConsumptionMaterialCopied { get; set; }

        /// <summary>
        /// 消耗的参考物料的乘数
        /// </summary>
        [SugarColumn(ColumnName = "consumption_reference_material_multiplier", ColumnDescription = "消耗的参考物料的乘数", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ConsumptionReferenceMaterialMultiplier { get; set; } = 0;

        /// <summary>
        /// 自动重置预测模式
        /// </summary>
        [SugarColumn(ColumnName = "automatic_reset_forecast_model", ColumnDescription = "自动重置预测模式", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AutomaticResetForecastModel { get; set; } = 0;

        /// <summary>
        /// 进/出口中优惠指示符
        /// </summary>
        [SugarColumn(ColumnName = "preference_indicator_import_export", ColumnDescription = "进/出口中优惠指示符", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PreferenceIndicatorImportExport { get; set; }

        // ==================== 贸易和法律 ====================

        /// <summary>
        /// 免税证明：法律控制指示符
        /// </summary>
        [SugarColumn(ColumnName = "tax_exemption_certificate_legal_control_indicator", ColumnDescription = "免税证明：法律控制指示符", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TaxExemptionCertificateLegalControlIndicator { get; set; } = 0;

        /// <summary>
        /// 法律控制的免税证书编号
        /// </summary>
        [SugarColumn(ColumnName = "legal_control_tax_exemption_certificate_number", ColumnDescription = "法律控制的免税证书编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LegalControlTaxExemptionCertificateNumber { get; set; }

        /// <summary>
        /// 免税证明：免税证书的发放日
        /// </summary>
        [SugarColumn(ColumnName = "tax_exemption_certificate_issue_date", ColumnDescription = "免税证明：免税证书的发放日", ColumnDataType = "date", IsNullable = true)]
        public DateTime? TaxExemptionCertificateIssueDate { get; set; }

        /// <summary>
        /// 标识：存在供应商申报
        /// </summary>
        [SugarColumn(ColumnName = "vendor_declaration_exists", ColumnDescription = "标识：存在供应商申报", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int VendorDeclarationExists { get; set; } = 0;

        /// <summary>
        /// 供应商申报的有效日期
        /// </summary>
        [SugarColumn(ColumnName = "vendor_declaration_valid_from", ColumnDescription = "供应商申报的有效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? VendorDeclarationValidFrom { get; set; }

        /// <summary>
        /// 指示符：军用物资
        /// </summary>
        [SugarColumn(ColumnName = "military_goods_indicator", ColumnDescription = "指示符：军用物资", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MilitaryGoodsIndicator { get; set; } = 0;

        /// <summary>
        /// IS－R服务级别
        /// </summary>
        [SugarColumn(ColumnName = "is_r_service_level", ColumnDescription = "IS－R服务级别", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IsRServiceLevel { get; set; }

        /// <summary>
        /// 指示符 : 物料是联产品
        /// </summary>
        [SugarColumn(ColumnName = "material_is_co_product_indicator", ColumnDescription = "指示符 : 物料是联产品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaterialIsCoProductIndicator { get; set; } = 0;

        /// <summary>
        /// 计划策略组
        /// </summary>
        [SugarColumn(ColumnName = "planning_strategy_group", ColumnDescription = "计划策略组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlanningStrategyGroup { get; set; }

        /// <summary>
        /// 计划中的可配置物料的内部对象号
        /// </summary>
        [SugarColumn(ColumnName = "internal_object_number_configurable_material_planning", ColumnDescription = "计划中的可配置物料的内部对象号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InternalObjectNumberConfigurableMaterialPlanning { get; set; }

        /// <summary>
        /// 外部采购的缺省仓储位置
        /// </summary>
        [SugarColumn(ColumnName = "default_storage_location_external_procurement", ColumnDescription = "外部采购的缺省仓储位置", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DefaultStorageLocationExternalProcurement { get; set; }

        /// <summary>
        /// 标识：散装物料
        /// </summary>
        [SugarColumn(ColumnName = "bulk_material_indicator", ColumnDescription = "标识：散装物料", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BulkMaterialIndicator { get; set; } = 0;

        /// <summary>
        /// 周期标识被固定
        /// </summary>
        [SugarColumn(ColumnName = "cycle_indicator_fixed", ColumnDescription = "周期标识被固定", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CycleIndicatorFixed { get; set; } = 0;

        /// <summary>
        /// 库存确定组
        /// </summary>
        [SugarColumn(ColumnName = "stock_determination_group", ColumnDescription = "库存确定组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StockDeterminationGroup { get; set; }

        /// <summary>
        /// QM 中活动的物料授权组
        /// </summary>
        [SugarColumn(ColumnName = "material_authorization_group_active_qm", ColumnDescription = "QM 中活动的物料授权组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialAuthorizationGroupActiveQm { get; set; }

        /// <summary>
        /// 计划独立需求的调整期间
        /// </summary>
        [SugarColumn(ColumnName = "adjustment_period_planned_independent_requirements", ColumnDescription = "计划独立需求的调整期间", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AdjustmentPeriodPlannedIndependentRequirements { get; set; } = 0;

        /// <summary>
        /// 任务清单类型
        /// </summary>
        [SugarColumn(ColumnName = "task_list_type", ColumnDescription = "任务清单类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TaskListType { get; set; }

        // ==================== 特殊指示符和标识 ====================

        /// <summary>
        /// 安全时间标识（含或不含安全时间）
        /// </summary>
        [SugarColumn(ColumnName = "safety_time_indicator", ColumnDescription = "安全时间标识（含或不含安全时间）", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SafetyTimeIndicator { get; set; } = 0;

        /// <summary>
        /// 安全时间（按工作日计算）
        /// </summary>
        [SugarColumn(ColumnName = "safety_time_workdays", ColumnDescription = "安全时间（按工作日计算）", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal SafetyTimeWorkdays { get; set; } = 0;

        /// <summary>
        /// 活动控制：计划订单处理
        /// </summary>
        [SugarColumn(ColumnName = "active_control_planned_order_processing", ColumnDescription = "活动控制：计划订单处理", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ActiveControlPlannedOrderProcessing { get; set; }

        /// <summary>
        /// 在生产/处理订单中批量输入的确定
        /// </summary>
        [SugarColumn(ColumnName = "lot_entry_determination_production_processing_order", ColumnDescription = "在生产/处理订单中批量输入的确定", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LotEntryDeterminationProductionProcessingOrder { get; set; }

        /// <summary>
        /// 计量单位组
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure_group", ColumnDescription = "计量单位组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UnitOfMeasureGroup { get; set; }

        /// <summary>
        /// 物料运输组
        /// </summary>
        [SugarColumn(ColumnName = "material_transportation_group", ColumnDescription = "物料运输组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialTransportationGroup { get; set; }

        /// <summary>
        /// VO 物料的库存转移销售值（工厂到工厂）
        /// </summary>
        [SugarColumn(ColumnName = "vo_material_stock_transfer_sales_value_plant_to_plant", ColumnDescription = "VO 物料的库存转移销售值（工厂到工厂）", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal VoMaterialStockTransferSalesValuePlantToPlant { get; set; } = 0;

        /// <summary>
        /// 仅记值物料的销售价格计的运输值
        /// </summary>
        [SugarColumn(ColumnName = "transportation_value_only_goods_sales_price", ColumnDescription = "仅记值物料的销售价格计的运输值", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TransportationValueOnlyGoodsSalesPrice { get; set; } = 0;

        /// <summary>
        /// 指示符: 平滑促销消耗
        /// </summary>
        [SugarColumn(ColumnName = "smooth_promotional_consumption_indicator", ColumnDescription = "指示符: 平滑促销消耗", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SmoothPromotionalConsumptionIndicator { get; set; } = 0;

        /// <summary>
        /// 将进行成本核算的生产版本
        /// </summary>
        [SugarColumn(ColumnName = "production_version_to_be_costed", ColumnDescription = "将进行成本核算的生产版本", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProductionVersionToBeCosted { get; set; }

        /// <summary>
        /// 固定价格联产品
        /// </summary>
        [SugarColumn(ColumnName = "fixed_price_co_product", ColumnDescription = "固定价格联产品", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int FixedPriceCoProduct { get; set; } = 0;

        /// <summary>
        /// 用于计算工作负荷的后勤处理组
        /// </summary>
        [SugarColumn(ColumnName = "logistics_handling_group_workload_calculation", ColumnDescription = "用于计算工作负荷的后勤处理组", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LogisticsHandlingGroupWorkloadCalculation { get; set; }

        /// <summary>
        /// 工厂物料分销参数文件
        /// </summary>
        [SugarColumn(ColumnName = "plant_material_distribution_profile", ColumnDescription = "工厂物料分销参数文件", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlantMaterialDistributionProfile { get; set; }

        /// <summary>
        /// 有约束的虚拟库存
        /// </summary>
        [SugarColumn(ColumnName = "restricted_virtual_stock", ColumnDescription = "有约束的虚拟库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal RestrictedVirtualStock { get; set; } = 0;

        /// <summary>
        /// 连接空缺库存的销售价
        /// </summary>
        [SugarColumn(ColumnName = "sales_price_connecting_shortage_stock", ColumnDescription = "连接空缺库存的销售价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal SalesPriceConnectingShortageStock { get; set; } = 0;

        /// <summary>
        /// 物料: CFOP类别
        /// </summary>
        [SugarColumn(ColumnName = "material_cfop_category", ColumnDescription = "物料: CFOP类别", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaterialCfopCategory { get; set; }

        /// <summary>
        /// 最小安全库存
        /// </summary>
        [SugarColumn(ColumnName = "minimum_safety_stock", ColumnDescription = "最小安全库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal MinimumSafetyStock { get; set; } = 0;

        /// <summary>
        /// 无成本核算
        /// </summary>
        [SugarColumn(ColumnName = "no_costing", ColumnDescription = "无成本核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NoCosting { get; set; } = 0;

        /// <summary>
        /// 库存入库和库存出库的策略
        /// </summary>
        [SugarColumn(ColumnName = "stock_in_stock_out_strategy", ColumnDescription = "库存入库和库存出库的策略", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StockInStockOutStrategy { get; set; }

        /// <summary>
        /// 初始批次管理的标识
        /// </summary>
        [SugarColumn(ColumnName = "initial_batch_management_indicator", ColumnDescription = "初始批次管理的标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int InitialBatchManagementIndicator { get; set; } = 0;

        /// <summary>
        /// 初始批量的参考物料
        /// </summary>
        [SugarColumn(ColumnName = "initial_lot_reference_material", ColumnDescription = "初始批量的参考物料", Length = 40, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InitialLotReferenceMaterial { get; set; }

        /// <summary>
        /// 评估的收货锁定库存
        /// </summary>
        [SugarColumn(ColumnName = "valuated_goods_receipt_blocked_stock", ColumnDescription = "评估的收货锁定库存", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ValuatedGoodsReceiptBlockedStock { get; set; } = 0;

        /// <summary>
        /// 分段策略
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_strategy", ColumnDescription = "分段策略", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStrategy { get; set; }

        /// <summary>
        /// 分段状态
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_status", ColumnDescription = "分段状态", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStatus { get; set; }

        /// <summary>
        /// 分段策略范围
        /// </summary>
        [SugarColumn(ColumnName = "segmentation_strategy_scope", ColumnDescription = "分段策略范围", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SegmentationStrategyScope { get; set; }

        /// <summary>
        /// 根据交货日期或段排序库存
        /// </summary>
        [SugarColumn(ColumnName = "sort_stock_by_delivery_date_or_segment", ColumnDescription = "根据交货日期或段排序库存", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SortStockByDeliveryDateOrSegment { get; set; }

        /// <summary>
        /// 消耗优先级
        /// </summary>
        [SugarColumn(ColumnName = "consumption_priority", ColumnDescription = "消耗优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ConsumptionPriority { get; set; } = 0;

        /// <summary>
        /// 离散批次编号
        /// </summary>
        [SugarColumn(ColumnName = "discrete_batch_number", ColumnDescription = "离散批次编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DiscreteBatchNumber { get; set; }

        /// <summary>
        /// 库存保护标识
        /// </summary>
        [SugarColumn(ColumnName = "stock_protection_indicator", ColumnDescription = "库存保护标识", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int StockProtectionIndicator { get; set; } = 0;

        /// <summary>
        /// 订单分配运行
        /// </summary>
        [SugarColumn(ColumnName = "order_allocation_run", ColumnDescription = "订单分配运行", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderAllocationRun { get; set; } = 0;

        /// <summary>
        /// 按天数发货处理时间
        /// </summary>
        [SugarColumn(ColumnName = "shipping_processing_time_days", ColumnDescription = "按天数发货处理时间", ColumnDataType = "decimal(5,1)", IsNullable = false, DefaultValue = "0")]
        public decimal ShippingProcessingTimeDays { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
}




