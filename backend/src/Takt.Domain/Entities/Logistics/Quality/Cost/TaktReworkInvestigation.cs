#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktReworkInvestigation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 返工调查实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 返工调查实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录返工调查相关的基础信息，包括机种、Lot、人员赁率、检讨调查试验等
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business_rework_investigation", "返工调查表")]
    [SugarIndex("ix_quality_business_rework_investigation", nameof(QualityBusinessId), OrderByType.Asc, false)]
    public class TaktReworkInvestigation : TaktBaseEntity
    {
        /// <summary>品质业务ID</summary>
        [SugarColumn(ColumnName = "quality_business_id", ColumnDescription = "品质业务ID", ColumnDataType = "bigint", IsNullable = false)]
        public long QualityBusinessId { get; set; }

        /// <summary>机种</summary>
        [SugarColumn(ColumnName = "model_type", ColumnDescription = "机种", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ModelType { get; set; } = string.Empty;

        /// <summary>Lot</summary>
        [SugarColumn(ColumnName = "lot_number", ColumnDescription = "Lot", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string LotNumber { get; set; } = string.Empty;

        /// <summary>直接人员赁率</summary>
        [SugarColumn(ColumnName = "direct_personnel_rate", ColumnDescription = "直接人员赁率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DirectPersonnelRate { get; set; } = 0;

        /// <summary>间接人员赁率</summary>
        [SugarColumn(ColumnName = "indirect_personnel_rate", ColumnDescription = "间接人员赁率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal IndirectPersonnelRate { get; set; } = 0;

        /// <summary>检讨・调查・试验内容</summary>
        [SugarColumn(ColumnName = "investigation_content", ColumnDescription = "检讨・调查・试验内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? InvestigationContent { get; set; }

        /// <summary>检讨・调查・试验费用</summary>
        [SugarColumn(ColumnName = "investigation_cost", ColumnDescription = "检讨・调查・试验费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal InvestigationCost { get; set; } = 0;

        /// <summary>检讨会使用时间(分)</summary>
        [SugarColumn(ColumnName = "review_meeting_time", ColumnDescription = "检讨会使用时间(分)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReviewMeetingTime { get; set; } = 0;

        /// <summary>直接人员参加人数</summary>
        [SugarColumn(ColumnName = "direct_personnel_count", ColumnDescription = "直接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DirectPersonnelCount { get; set; } = 0;

        /// <summary>间接人员参加人数</summary>
        [SugarColumn(ColumnName = "indirect_personnel_count", ColumnDescription = "间接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IndirectPersonnelCount { get; set; } = 0;

        /// <summary>调查评价试验工作时间(分)</summary>
        [SugarColumn(ColumnName = "evaluation_test_time", ColumnDescription = "调查评价试验工作时间(分)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int EvaluationTestTime { get; set; } = 0;

        /// <summary>交通费、旅费</summary>
        [SugarColumn(ColumnName = "travel_expenses", ColumnDescription = "交通费、旅费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TravelExpenses { get; set; } = 0;

        /// <summary>其他费用</summary>
        [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherExpenses { get; set; } = 0;

        /// <summary>其他作业時間(分)</summary>
        [SugarColumn(ColumnName = "other_work_time", ColumnDescription = "其他作业時間(分)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OtherWorkTime { get; set; } = 0;

        /// <summary>其他设备购入费,工程费,搬运费</summary>
        [SugarColumn(ColumnName = "equipment_engineering_cost", ColumnDescription = "其他设备购入费,工程费,搬运费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal EquipmentEngineeringCost { get; set; } = 0;

        /// <summary>需要不良改修对应</summary>
        [SugarColumn(ColumnName = "need_defect_repair", ColumnDescription = "需要不良改修对应", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NeedDefectRepair { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



