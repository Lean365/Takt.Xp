#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Change
{
    /// <summary>
    /// 设变执行跟踪实体（只保留部门、批次、实施、说明）
    /// </summary>
    [SugarTable("Takt_logistics_bom_change_execution_track", "设变执行跟踪")]
    public class TaktChangeExecutionTrack : TaktBaseEntity
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        [SugarColumn(ColumnName = "change_number", ColumnDescription = "设变号码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeNumber { get; set; } = string.Empty;


        /// <summary>
        /// 部门确认日期
        /// </summary>
        [SugarColumn(ColumnName = "department_confirm_date", ColumnDescription = "部门确认日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [SugarColumn(ColumnName = "department", ColumnDescription = "部门", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        [SugarColumn(ColumnName = "planned_batch", ColumnDescription = "预定批次", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        [SugarColumn(ColumnName = "old_product_process", ColumnDescription = "旧品处理", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        [SugarColumn(ColumnName = "purchase_order", ColumnDescription = "采购订单", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        [SugarColumn(ColumnName = "iqc_inspection_number", ColumnDescription = "IQC检验单号", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        [SugarColumn(ColumnName = "delivery_batch", ColumnDescription = "出库批次", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        [SugarColumn(ColumnName = "delivery_order", ColumnDescription = "出库单号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        [SugarColumn(ColumnName = "prod_order", ColumnDescription = "生产工单", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        [SugarColumn(ColumnName = "prod_team", ColumnDescription = "生产班组", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        [SugarColumn(ColumnName = "actual_batch", ColumnDescription = "实施批次", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        [SugarColumn(ColumnName = "qa_verification_batch", ColumnDescription = "QA验证批次", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        [SugarColumn(ColumnName = "action_description", ColumnDescription = "实施说明", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ActionDescription { get; set; }

        /// <summary>
        /// 重要备注
        /// </summary>
        [SugarColumn(ColumnName = "important_memo", ColumnDescription = "重要备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ImportantMemo { get; set; }

    }
} 
