#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktReworkAssemblyCost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 组立Assy成本实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Quality.Cost
{
    /// <summary>
    /// 组立Assy成本实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录组立装配相关的选别改修成本，包括不良内容、选别改修费用、顾客费用请求等
    /// </remarks>
    [SugarTable("Takt_logistics_quality_business_rework_assembly_cost", "组立Assy成本表")]
    [SugarIndex("ix_quality_business_assembly_cost", nameof(QualityBusinessId), OrderByType.Asc, false)]
    public class TaktReworkAssemblyCost : TaktBaseEntity
    {
        /// <summary>品质业务ID</summary>
        [SugarColumn(ColumnName = "quality_business_id", ColumnDescription = "品质业务ID", ColumnDataType = "bigint", IsNullable = false)]
        public long QualityBusinessId { get; set; }

        /// <summary>直接人员赁率</summary>
        [SugarColumn(ColumnName = "direct_personnel_rate", ColumnDescription = "直接人员赁率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DirectPersonnelRate { get; set; } = 0;

        /// <summary>间接人员赁率</summary>
        [SugarColumn(ColumnName = "indirect_personnel_rate", ColumnDescription = "间接人员赁率", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "0")]
        public decimal IndirectPersonnelRate { get; set; } = 0;

        /// <summary>直接人员参加人数</summary>
        [SugarColumn(ColumnName = "direct_personnel_count", ColumnDescription = "直接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DirectPersonnelCount { get; set; } = 0;

        /// <summary>间接人员参加人数</summary>
        [SugarColumn(ColumnName = "indirect_personnel_count", ColumnDescription = "间接人员参加人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IndirectPersonnelCount { get; set; } = 0;

        /// <summary>不良内容</summary>
        [SugarColumn(ColumnName = "defect_content", ColumnDescription = "不良内容", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DefectContent { get; set; } = string.Empty;

        /// <summary>选别・改修费用</summary>
        [SugarColumn(ColumnName = "sorting_repair_cost", ColumnDescription = "选别・改修费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal SortingRepairCost { get; set; } = 0;

        /// <summary>选别・改修时间(分)</summary>
        [SugarColumn(ColumnName = "sorting_repair_time", ColumnDescription = "选别・改修时间(分)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int SortingRepairTime { get; set; } = 0;

        /// <summary>再检查时间(分)</summary>
        [SugarColumn(ColumnName = "recheck_time", ColumnDescription = "再检查时间(分)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int RecheckTime { get; set; } = 0;

        /// <summary>交通费、旅费</summary>
        [SugarColumn(ColumnName = "travel_expenses", ColumnDescription = "交通费、旅费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TravelExpenses { get; set; } = 0;

        /// <summary>仓库管理费</summary>
        [SugarColumn(ColumnName = "warehouse_management_cost", ColumnDescription = "仓库管理费", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal WarehouseManagementCost { get; set; } = 0;

        /// <summary>选别・改修其他费用</summary>
        [SugarColumn(ColumnName = "sorting_repair_other_cost", ColumnDescription = "选别・改修其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal SortingRepairOtherCost { get; set; } = 0;

        /// <summary>选别・改修备注</summary>
        [SugarColumn(ColumnName = "sorting_repair_remark", ColumnDescription = "选别・改修备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SortingRepairRemark { get; set; }

        /// <summary>向顾客的费用请求</summary>
        [SugarColumn(ColumnName = "customer_cost_request", ColumnDescription = "向顾客的费用请求", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CustomerCostRequest { get; set; } = 0;

        /// <summary>顾客名</summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "顾客名", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>Debit Note No</summary>
        [SugarColumn(ColumnName = "debit_note_no", ColumnDescription = "Debit Note No", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DebitNoteNo { get; set; }

        /// <summary>其他费用</summary>
        [SugarColumn(ColumnName = "other_expenses", ColumnDescription = "其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherExpenses { get; set; } = 0;

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



