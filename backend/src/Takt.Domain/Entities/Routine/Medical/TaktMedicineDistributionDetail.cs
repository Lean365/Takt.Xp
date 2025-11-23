#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMedicineDistributionDetail.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 药品发放明细表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Medical
{
    /// <summary>
    /// 药品发放明细表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录药品发放的详细药品信息，包括药品、数量、单价等
    /// </remarks>
    [SugarTable("Takt_routine_medical_medicine_distribution_detail", "药品发放明细")]
    [SugarIndex("ix_medical_medicine_distribution_detail_distribution", nameof(DistributionId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_detail_medicine", nameof(MedicineId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_detail_category", nameof(CategoryId), OrderByType.Asc, false)]
    public class TaktMedicineDistributionDetail : TaktBaseEntity
    {
        /// <summary>发放主表ID</summary>
        [SugarColumn(ColumnName = "distribution_id", ColumnDescription = "发放主表ID", ColumnDataType = "bigint", IsNullable = false)]
        public long DistributionId { get; set; }

        /// <summary>药品ID</summary>
        [SugarColumn(ColumnName = "medicine_id", ColumnDescription = "药品ID", ColumnDataType = "bigint", IsNullable = false)]
        public long MedicineId { get; set; }

        /// <summary>药品分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "药品分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

        /// <summary>药品分类名称</summary>
        [SugarColumn(ColumnName = "category_name", ColumnDescription = "药品分类名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CategoryName { get; set; } = string.Empty;

        /// <summary>药品名称</summary>
        [SugarColumn(ColumnName = "medicine_name", ColumnDescription = "药品名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MedicineName { get; set; } = string.Empty;

        /// <summary>药品规格</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "药品规格", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>生产厂家</summary>
        [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "生产厂家", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Manufacturer { get; set; }

        /// <summary>批准文号</summary>
        [SugarColumn(ColumnName = "approval_number", ColumnDescription = "批准文号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalNumber { get; set; }

        /// <summary>批号</summary>
        [SugarColumn(ColumnName = "batch_number", ColumnDescription = "批号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BatchNumber { get; set; }

        /// <summary>生产日期</summary>
        [SugarColumn(ColumnName = "production_date", ColumnDescription = "生产日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ProductionDate { get; set; }

        /// <summary>有效期</summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "有效期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Unit { get; set; } = string.Empty;

        /// <summary>发放数量</summary>
        [SugarColumn(ColumnName = "distribution_quantity", ColumnDescription = "发放数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DistributionQuantity { get; set; } = 0;

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>总金额</summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>是否处方药(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_prescription", ColumnDescription = "是否处方药", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPrescription { get; set; } = 0;

        /// <summary>用药说明</summary>
        [SugarColumn(ColumnName = "usage_instructions", ColumnDescription = "用药说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UsageInstructions { get; set; }

        /// <summary>用法用量</summary>
        [SugarColumn(ColumnName = "dosage", ColumnDescription = "用法用量", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Dosage { get; set; }

        /// <summary>服用时间</summary>
        [SugarColumn(ColumnName = "taking_time", ColumnDescription = "服用时间", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TakingTime { get; set; }

        /// <summary>服用天数</summary>
        [SugarColumn(ColumnName = "taking_days", ColumnDescription = "服用天数", ColumnDataType = "int", IsNullable = true)]
        public int? TakingDays { get; set; }

        /// <summary>注意事项</summary>
        [SugarColumn(ColumnName = "precautions", ColumnDescription = "注意事项", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Precautions { get; set; }

        /// <summary>是否收费(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_charged", ColumnDescription = "是否收费", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCharged { get; set; } = 0;

        /// <summary>收费金额</summary>
        [SugarColumn(ColumnName = "charge_amount", ColumnDescription = "收费金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ChargeAmount { get; set; } = 0;



        /// <summary>
        /// 导航属性：关联的药品发放主表
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(DistributionId))]
        public TaktMedicineDistribution? Distribution { get; set; }

        /// <summary>
        /// 导航属性：关联的药品信息
        /// </summary>
        [Navigate(NavigateType.ManyToOne, nameof(MedicineId))]
        public TaktMedicine? Medicine { get; set; }
    }
} 



