#nullable enable

using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom.Change
{
    /// <summary>
    /// 设变执行跟踪DTO
    /// </summary>
    public class TaktChangeExecutionTrackDto : TaktPagedQuery
    {
        /// <summary>
        /// 设变执行跟踪ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionTrackId { get; set; } = string.Empty;

        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门确认日期
        /// </summary>
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        public string? ImplementationNote { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪查询DTO
    /// </summary>
    public class TaktChangeExecutionTrackQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string? ChangeNumber { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string? Department { get; set; }

        /// <summary>
        /// 部门确认日期开始
        /// </summary>
        public DateTime? DepartmentConfirmDateStart { get; set; }

        /// <summary>
        /// 部门确认日期结束
        /// </summary>
        public DateTime? DepartmentConfirmDateEnd { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪创建DTO
    /// </summary>
    public class TaktChangeExecutionTrackCreateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门确认日期
        /// </summary>
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        public string? ImplementationNote { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪更新DTO
    /// </summary>
    public class TaktChangeExecutionTrackUpdateDto : TaktChangeExecutionTrackCreateDto
    {
        /// <summary>
        /// 设变执行跟踪ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionTrackId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 设变执行跟踪导入DTO
    /// </summary>
    public class TaktChangeExecutionTrackImportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门确认日期
        /// </summary>
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        public string? ImplementationNote { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪导出DTO
    /// </summary>
    public class TaktChangeExecutionTrackExportDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门确认日期
        /// </summary>
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        public string? ImplementationNote { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪模板DTO
    /// </summary>
    public class TaktChangeExecutionTrackTemplateDto
    {
        /// <summary>
        /// 设变号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 部门确认日期
        /// </summary>
        public DateTime? DepartmentConfirmDate { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; } = string.Empty;

        /// <summary>
        /// 预定批次
        /// </summary>
        public string? PlannedBatch { get; set; }

        /// <summary>
        /// 旧品处理
        /// </summary>
        public string? OldProductProcess { get; set; }

        /// <summary>
        /// 供应商
        /// </summary>
        public string? Supplier { get; set; }

        /// <summary>
        /// 采购订单
        /// </summary>
        public string? PurchaseOrder { get; set; }

        /// <summary>
        /// IQC检验单号
        /// </summary>
        public string? IqcInspectionNumber { get; set; }

        /// <summary>
        /// 出库批次
        /// </summary>
        public string? DeliveryBatch { get; set; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string? DeliveryOrder { get; set; }

        /// <summary>
        /// 生产工单
        /// </summary>
        public string? ProdOrder { get; set; }

        /// <summary>
        /// 生产班组
        /// </summary>
        public string? ProdTeam { get; set; }

        /// <summary>
        /// 实施批次
        /// </summary>
        public string? ActualBatch { get; set; }

        /// <summary>
        /// QA验证批次
        /// </summary>
        public string? QaVerificationBatch { get; set; }

        /// <summary>
        /// 实施说明
        /// </summary>
        public string? ImplementationNote { get; set; }
    }

    /// <summary>
    /// 设变执行跟踪状态DTO
    /// </summary>
    public class TaktChangeExecutionTrackStatusDto
    {
        /// <summary>
        /// 设变执行跟踪ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionTrackId { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}


