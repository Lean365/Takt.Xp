//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktVehicleExpense.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 车辆费用管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 车辆费用管理实体
    /// </summary>
    [SugarTable("Takt_routine_vehicle_expense", "车辆费用管理")]
    [SugarIndex("index_expense_vehicle_id", nameof(VehicleId), OrderByType.Asc)]
    [SugarIndex("index_expense_type", nameof(ExpenseType), OrderByType.Asc)]
    [SugarIndex("index_expense_date", nameof(ExpenseDate), OrderByType.Asc)]
    [SugarIndex("index_expense_status", nameof(Status), OrderByType.Asc)]
    public class TaktVehicleExpense : TaktBaseEntity
    {
        /// <summary>
        /// 车辆ID
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_id", ColumnDescription = "车辆ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long VehicleId { get; set; }

        /// <summary>
        /// 费用类型（0：加油费，1：维修费，2：停车费，3：保险费，4：年检费，5：其他费用）
        /// </summary>
        [SugarColumn(ColumnName = "expense_type", ColumnDescription = "费用类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int ExpenseType { get; set; }

        /// <summary>
        /// 费用标题
        /// </summary>
        [SugarColumn(ColumnName = "expense_title", ColumnDescription = "费用标题", Length = 200, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string ExpenseTitle { get; set; } = string.Empty;

        /// <summary>
        /// 费用描述
        /// </summary>
        [SugarColumn(ColumnName = "expense_description", ColumnDescription = "费用描述", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ExpenseDescription { get; set; }

        /// <summary>
        /// 费用日期
        /// </summary>
        [SugarColumn(ColumnName = "expense_date", ColumnDescription = "费用日期", IsNullable = false, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime ExpenseDate { get; set; }

        /// <summary>
        /// 费用金额
        /// </summary>
        [SugarColumn(ColumnName = "expense_amount", ColumnDescription = "费用金额", IsNullable = false, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal ExpenseAmount { get; set; }

        /// <summary>
        /// 费用状态（0：待报销，1：已报销，2：已拒绝）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "费用状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 经办人ID
        /// </summary>
        [SugarColumn(ColumnName = "operator_id", ColumnDescription = "经办人ID", IsNullable = false, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long OperatorId { get; set; }

        /// <summary>
        /// 供应商/服务商
        /// </summary>
        [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商/服务商", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Supplier { get; set; }

        /// <summary>
        /// 发票号码
        /// </summary>
        [SugarColumn(ColumnName = "invoice_no", ColumnDescription = "发票号码", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? InvoiceNo { get; set; }

        /// <summary>
        /// 报销人ID
        /// </summary>
        [SugarColumn(ColumnName = "reimburser_id", ColumnDescription = "报销人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ReimburserId { get; set; }

        /// <summary>
        /// 报销时间
        /// </summary>
        [SugarColumn(ColumnName = "reimbursement_time", ColumnDescription = "报销时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ReimbursementTime { get; set; }

        /// <summary>
        /// 审批人ID
        /// </summary>
        [SugarColumn(ColumnName = "approver_id", ColumnDescription = "审批人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ApproverId { get; set; }

        /// <summary>
        /// 审批时间
        /// </summary>
        [SugarColumn(ColumnName = "approval_time", ColumnDescription = "审批时间", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? ApprovalTime { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? ApprovalRemarks { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }

        /// <summary>
        /// 导航属性：车辆
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(VehicleId))]
        public TaktVehicle? Vehicle { get; set; }

        /// <summary>
        /// 导航属性：经办人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(OperatorId))]
        public TaktUser? Operator { get; set; }

        /// <summary>
        /// 导航属性：报销人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ReimburserId))]
        public TaktUser? Reimburser { get; set; }

        /// <summary>
        /// 导航属性：审批人
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(ApproverId))]
        public TaktUser? Approver { get; set; }
    }
} 


