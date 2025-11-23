#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktQualityBusinessIqcCost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 品质业务IQC成本子表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 品质业务IQC成本子表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录品质业务IQC（受入检查）相关的成本明细，包括受入检查、检定、校正等业务费用
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business_iqc_cost", "品质业务IQC成本子表")]
    [SugarIndex("ix_quality_business_iqc_cost", nameof(QualityBusinessId), OrderByType.Asc, nameof(CostDate), OrderByType.Desc, false)]
    public class TaktQualityBusinessIqcCost : TaktBaseEntity
    {
        /// <summary>品质业务ID</summary>
        [SugarColumn(ColumnName = "quality_business_id", ColumnDescription = "品质业务ID", ColumnDataType = "bigint", IsNullable = false)]
        public long QualityBusinessId { get; set; }

        /// <summary>成本日期</summary>
        [SugarColumn(ColumnName = "cost_date", ColumnDescription = "成本日期", ColumnDataType = "date", IsNullable = false)]
        public DateTime CostDate { get; set; } = DateTime.Today;

        /// <summary>直接人员费率</summary>
        [SugarColumn(ColumnName = "direct_personnel_rate", ColumnDescription = "直接人员费率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DirectPersonnelRate { get; set; } = 0;

        /// <summary>受入检查业务费用</summary>
        [SugarColumn(ColumnName = "incoming_inspection_cost", ColumnDescription = "受入检查业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal IncomingInspectionCost { get; set; } = 0;

        /// <summary>检查时间(分)</summary>
        [SugarColumn(ColumnName = "inspection_time_minutes", ColumnDescription = "检查时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionTimeMinutes { get; set; } = 0;

        /// <summary>交通费、旅费</summary>
        [SugarColumn(ColumnName = "transportation_travel_cost", ColumnDescription = "交通费、旅费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TransportationTravelCost { get; set; } = 0;

        /// <summary>检查其他费用</summary>
        [SugarColumn(ColumnName = "inspection_other_cost", ColumnDescription = "检查其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InspectionOtherCost { get; set; } = 0;

        /// <summary>检查备注</summary>
        [SugarColumn(ColumnName = "inspection_remark", ColumnDescription = "检查备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InspectionRemark { get; set; }

        /// <summary>初期检定・定期检定业务费用</summary>
        [SugarColumn(ColumnName = "initial_periodic_verification_cost", ColumnDescription = "初期检定・定期检定业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InitialPeriodicVerificationCost { get; set; } = 0;

        /// <summary>检定作业时间(分)</summary>
        [SugarColumn(ColumnName = "verification_work_time_minutes", ColumnDescription = "检定作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal VerificationWorkTimeMinutes { get; set; } = 0;

        /// <summary>检定其他费用</summary>
        [SugarColumn(ColumnName = "verification_other_cost", ColumnDescription = "检定其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal VerificationOtherCost { get; set; } = 0;

        /// <summary>检定备注</summary>
        [SugarColumn(ColumnName = "verification_remark", ColumnDescription = "检定备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VerificationRemark { get; set; }

        /// <summary>测定器校正业务费用</summary>
        [SugarColumn(ColumnName = "measuring_instrument_calibration_cost", ColumnDescription = "测定器校正业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal MeasuringInstrumentCalibrationCost { get; set; } = 0;

        /// <summary>校正作业时间(分)</summary>
        [SugarColumn(ColumnName = "calibration_work_time_minutes", ColumnDescription = "校正作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CalibrationWorkTimeMinutes { get; set; } = 0;

        /// <summary>外部委托费、运搬费</summary>
        [SugarColumn(ColumnName = "external_commission_transport_cost", ColumnDescription = "外部委托费、运搬费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ExternalCommissionTransportCost { get; set; } = 0;

        /// <summary>校正其他费用</summary>
        [SugarColumn(ColumnName = "calibration_other_cost", ColumnDescription = "校正其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CalibrationOtherCost { get; set; } = 0;

        /// <summary>校正备注</summary>
        [SugarColumn(ColumnName = "calibration_remark", ColumnDescription = "校正备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CalibrationRemark { get; set; }

        /// <summary>其他通常业务费用</summary>
        [SugarColumn(ColumnName = "other_normal_business_cost", ColumnDescription = "其他通常业务费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherNormalBusinessCost { get; set; } = 0;

        /// <summary>通常业务作业时间(分)</summary>
        [SugarColumn(ColumnName = "normal_business_work_time_minutes", ColumnDescription = "通常业务作业时间(分)", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NormalBusinessWorkTimeMinutes { get; set; } = 0;

        /// <summary>通常业务其他费用</summary>
        [SugarColumn(ColumnName = "normal_business_other_cost", ColumnDescription = "通常业务其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal NormalBusinessOtherCost { get; set; } = 0;

        /// <summary>通常业务其他备注</summary>
        [SugarColumn(ColumnName = "normal_business_other_remark", ColumnDescription = "通常业务其他备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NormalBusinessOtherRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



