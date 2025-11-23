#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMaintenanceOrder.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 维修工单实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 维修工单实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 专门用于设备维修业务，包括预防性维护、故障维修、紧急维修等
    /// </remarks>
    [SugarTable("Takt_logistics_maintenance_order", "维修工单表")]
    [SugarIndex("ix_maintenance_order_code", nameof(MaintenanceOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_maintenance_order", nameof(CompanyCode), OrderByType.Asc, nameof(MaintenanceOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_equipment_maintenance_order", nameof(EquipmentCode), OrderByType.Asc, false)]
    [SugarIndex("ix_maintenance_order_status", nameof(MaintenanceOrderStatus), OrderByType.Asc, false)]
    public class TaktMaintenanceOrder : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>维修工单编号</summary>
        [SugarColumn(ColumnName = "maintenance_order_code", ColumnDescription = "维修工单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaintenanceOrderCode { get; set; } = string.Empty;

        /// <summary>维修工单类型(1=预防性维护 2=故障维修 3=紧急维修 4=定期检查 5=设备改造 6=设备升级)</summary>
        [SugarColumn(ColumnName = "maintenance_order_type", ColumnDescription = "维修工单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int MaintenanceOrderType { get; set; } = 1;

        /// <summary>设备编号</summary>
        [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>设备名称</summary>
        [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentName { get; set; }

        /// <summary>设备型号</summary>
        [SugarColumn(ColumnName = "equipment_model", ColumnDescription = "设备型号", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentModel { get; set; }

        /// <summary>设备位置</summary>
        [SugarColumn(ColumnName = "equipment_location", ColumnDescription = "设备位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentLocation { get; set; }

        /// <summary>设备分类</summary>
        [SugarColumn(ColumnName = "equipment_category", ColumnDescription = "设备分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentCategory { get; set; }

        /// <summary>维修工单标题</summary>
        [SugarColumn(ColumnName = "maintenance_order_title", ColumnDescription = "维修工单标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaintenanceOrderTitle { get; set; } = string.Empty;

        /// <summary>维修工单描述</summary>
        [SugarColumn(ColumnName = "maintenance_order_description", ColumnDescription = "维修工单描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaintenanceOrderDescription { get; set; }

        /// <summary>故障现象</summary>
        [SugarColumn(ColumnName = "fault_phenomenon", ColumnDescription = "故障现象", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaultPhenomenon { get; set; }

        /// <summary>故障原因</summary>
        [SugarColumn(ColumnName = "fault_reason", ColumnDescription = "故障原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaultReason { get; set; }

        /// <summary>维修方案</summary>
        [SugarColumn(ColumnName = "repair_solution", ColumnDescription = "维修方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairSolution { get; set; }

        /// <summary>维修结果</summary>
        [SugarColumn(ColumnName = "repair_result", ColumnDescription = "维修结果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairResult { get; set; }

        /// <summary>维修建议</summary>
        [SugarColumn(ColumnName = "repair_suggestion", ColumnDescription = "维修建议", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairSuggestion { get; set; }

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>影响程度(1=轻微 2=一般 3=严重 4=紧急)</summary>
        [SugarColumn(ColumnName = "impact_level", ColumnDescription = "影响程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImpactLevel { get; set; } = 2;

        /// <summary>申请日期</summary>
        [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        /// <summary>申请人</summary>
        [SugarColumn(ColumnName = "applicant", ColumnDescription = "申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Applicant { get; set; } = string.Empty;

        /// <summary>申请人电话</summary>
        [SugarColumn(ColumnName = "applicant_phone", ColumnDescription = "申请人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApplicantPhone { get; set; }

        /// <summary>申请人邮箱</summary>
        [SugarColumn(ColumnName = "applicant_email", ColumnDescription = "申请人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApplicantEmail { get; set; }

        /// <summary>计划开始日期</summary>
        [SugarColumn(ColumnName = "planned_start_date", ColumnDescription = "计划开始日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PlannedStartDate { get; set; }

        /// <summary>计划完成日期</summary>
        [SugarColumn(ColumnName = "planned_end_date", ColumnDescription = "计划完成日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PlannedEndDate { get; set; }

        /// <summary>实际开始日期</summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>实际完成日期</summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>维修人员</summary>
        [SugarColumn(ColumnName = "repair_person", ColumnDescription = "维修人员", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairPerson { get; set; }

        /// <summary>维修人员电话</summary>
        [SugarColumn(ColumnName = "repair_person_phone", ColumnDescription = "维修人员电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairPersonPhone { get; set; }

        /// <summary>维修人员邮箱</summary>
        [SugarColumn(ColumnName = "repair_person_email", ColumnDescription = "维修人员邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairPersonEmail { get; set; }

        /// <summary>外部维修商</summary>
        [SugarColumn(ColumnName = "external_repair_vendor", ColumnDescription = "外部维修商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalRepairVendor { get; set; }

        /// <summary>外部维修商联系人</summary>
        [SugarColumn(ColumnName = "external_repair_contact", ColumnDescription = "外部维修商联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalRepairContact { get; set; }

        /// <summary>外部维修商电话</summary>
        [SugarColumn(ColumnName = "external_repair_phone", ColumnDescription = "外部维修商电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExternalRepairPhone { get; set; }

        /// <summary>维修工时</summary>
        [SugarColumn(ColumnName = "repair_hours", ColumnDescription = "维修工时", ColumnDataType = "decimal(8,2)", IsNullable = false, DefaultValue = "0")]
        public decimal RepairHours { get; set; } = 0;

        /// <summary>人工费用</summary>
        [SugarColumn(ColumnName = "labor_cost", ColumnDescription = "人工费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal LaborCost { get; set; } = 0;

        /// <summary>材料费用</summary>
        [SugarColumn(ColumnName = "material_cost", ColumnDescription = "材料费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal MaterialCost { get; set; } = 0;

        /// <summary>其他费用</summary>
        [SugarColumn(ColumnName = "other_cost", ColumnDescription = "其他费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OtherCost { get; set; } = 0;

        /// <summary>总费用</summary>
        [SugarColumn(ColumnName = "total_cost", ColumnDescription = "总费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalCost { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>维修工单状态(0=草稿 1=已提交 2=已分配 3=维修中 4=已完成 5=已验收 6=已关闭 7=已取消)</summary>
        [SugarColumn(ColumnName = "maintenance_order_status", ColumnDescription = "维修工单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int MaintenanceOrderStatus { get; set; } = 0;

        /// <summary>审批状态(0=未审批 1=已审批 2=已拒绝)</summary>
        [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审批状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ApprovalStatus { get; set; } = 0;

        /// <summary>审批人</summary>
        [SugarColumn(ColumnName = "approver", ColumnDescription = "审批人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Approver { get; set; }

        /// <summary>审批日期</summary>
        [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审批日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ApprovalDate { get; set; }

        /// <summary>审批意见</summary>
        [SugarColumn(ColumnName = "approval_remarks", ColumnDescription = "审批意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ApprovalRemarks { get; set; }

        /// <summary>验收人</summary>
        [SugarColumn(ColumnName = "acceptor", ColumnDescription = "验收人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Acceptor { get; set; }

        /// <summary>验收日期</summary>
        [SugarColumn(ColumnName = "acceptance_date", ColumnDescription = "验收日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? AcceptanceDate { get; set; }

        /// <summary>验收意见</summary>
        [SugarColumn(ColumnName = "acceptance_remarks", ColumnDescription = "验收意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AcceptanceRemarks { get; set; }

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>是否加急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_rush", ColumnDescription = "是否加急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRush { get; set; } = 0;

        /// <summary>维修图片</summary>
        [SugarColumn(ColumnName = "repair_images", ColumnDescription = "维修图片", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairImages { get; set; }

        /// <summary>维修文档</summary>
        [SugarColumn(ColumnName = "repair_documents", ColumnDescription = "维修文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RepairDocuments { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "maintenance_order_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? MaintenanceOrderRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



