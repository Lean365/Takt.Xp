#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceNotification.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 服务通知实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务通知实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_notification", "服务通知表")]
    [SugarIndex("ix_service_notification_code", nameof(NotificationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_notification", nameof(CompanyCode), OrderByType.Asc, nameof(NotificationCode), OrderByType.Asc, false)]
    [SugarIndex("ix_equipment_notification", nameof(EquipmentCode), OrderByType.Asc, false)]
    public class TaktServiceNotification : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>服务通知编号</summary>
        [SugarColumn(ColumnName = "notification_code", ColumnDescription = "服务通知编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NotificationCode { get; set; } = string.Empty;

        /// <summary>通知类型(1=故障通知 2=维护通知 3=检查通知 4=投诉通知 5=建议通知)</summary>
        [SugarColumn(ColumnName = "notification_type", ColumnDescription = "通知类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int NotificationType { get; set; } = 1;

        /// <summary>设备编号</summary>
        [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentCode { get; set; }

        /// <summary>设备名称</summary>
        [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentName { get; set; }

        /// <summary>设备位置</summary>
        [SugarColumn(ColumnName = "equipment_location", ColumnDescription = "设备位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EquipmentLocation { get; set; }

        /// <summary>通知标题</summary>
        [SugarColumn(ColumnName = "notification_title", ColumnDescription = "通知标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string NotificationTitle { get; set; } = string.Empty;

        /// <summary>通知描述</summary>
        [SugarColumn(ColumnName = "notification_description", ColumnDescription = "通知描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NotificationDescription { get; set; }

        /// <summary>故障现象</summary>
        [SugarColumn(ColumnName = "fault_phenomenon", ColumnDescription = "故障现象", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaultPhenomenon { get; set; }

        /// <summary>故障原因</summary>
        [SugarColumn(ColumnName = "fault_reason", ColumnDescription = "故障原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FaultReason { get; set; }

        /// <summary>影响程度(1=轻微 2=一般 3=严重 4=紧急)</summary>
        [SugarColumn(ColumnName = "impact_level", ColumnDescription = "影响程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImpactLevel { get; set; } = 2;

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>通知人</summary>
        [SugarColumn(ColumnName = "notifier", ColumnDescription = "通知人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Notifier { get; set; } = string.Empty;

        /// <summary>通知人电话</summary>
        [SugarColumn(ColumnName = "notifier_phone", ColumnDescription = "通知人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NotifierPhone { get; set; }

        /// <summary>通知人邮箱</summary>
        [SugarColumn(ColumnName = "notifier_email", ColumnDescription = "通知人邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NotifierEmail { get; set; }

        /// <summary>通知日期</summary>
        [SugarColumn(ColumnName = "notification_date", ColumnDescription = "通知日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime NotificationDate { get; set; } = DateTime.Now;

        /// <summary>要求处理日期</summary>
        [SugarColumn(ColumnName = "required_processing_date", ColumnDescription = "要求处理日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? RequiredProcessingDate { get; set; }

        /// <summary>实际处理日期</summary>
        [SugarColumn(ColumnName = "actual_processing_date", ColumnDescription = "实际处理日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ActualProcessingDate { get; set; }

        /// <summary>处理人</summary>
        [SugarColumn(ColumnName = "processor", ColumnDescription = "处理人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Processor { get; set; }

        /// <summary>处理人电话</summary>
        [SugarColumn(ColumnName = "processor_phone", ColumnDescription = "处理人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessorPhone { get; set; }

        /// <summary>处理方案</summary>
        [SugarColumn(ColumnName = "processing_solution", ColumnDescription = "处理方案", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessingSolution { get; set; }

        /// <summary>处理结果</summary>
        [SugarColumn(ColumnName = "processing_result", ColumnDescription = "处理结果", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProcessingResult { get; set; }

        /// <summary>处理费用</summary>
        [SugarColumn(ColumnName = "processing_cost", ColumnDescription = "处理费用", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ProcessingCost { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>通知状态(0=草稿 1=已提交 2=已分配 3=处理中 4=已完成 5=已关闭 6=已取消)</summary>
        [SugarColumn(ColumnName = "notification_status", ColumnDescription = "通知状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NotificationStatus { get; set; } = 0;

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

        /// <summary>是否紧急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_urgent", ColumnDescription = "是否紧急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsUrgent { get; set; } = 0;

        /// <summary>是否加急(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_rush", ColumnDescription = "是否加急", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRush { get; set; } = 0;

        /// <summary>相关图片</summary>
        [SugarColumn(ColumnName = "related_images", ColumnDescription = "相关图片", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedImages { get; set; }

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "notification_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? NotificationRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



