#nullable enable

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceOrder.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 服务工单实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务工单实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 专门用于服务业务，包括软件服务、培训服务、咨询服务、技术支持等
    /// </remarks>
    [SugarTable("Takt_logistics_service_order", "服务工单表")]
    [SugarIndex("ix_service_order_code", nameof(ServiceOrderCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_service_order", nameof(CompanyCode), OrderByType.Asc, nameof(ServiceOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_customer_service_order", nameof(CustomerCode), OrderByType.Asc, nameof(ServiceOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_service_order_status", nameof(ServiceOrderStatus), OrderByType.Asc, false)]
    public class TaktServiceOrder : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>服务工单编号</summary>
        [SugarColumn(ColumnName = "service_order_code", ColumnDescription = "服务工单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ServiceOrderCode { get; set; } = string.Empty;

        /// <summary>服务工单标题</summary>
        [SugarColumn(ColumnName = "service_order_title", ColumnDescription = "服务工单标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ServiceOrderTitle { get; set; } = string.Empty;

        /// <summary>服务工单描述</summary>
        [SugarColumn(ColumnName = "service_order_description", ColumnDescription = "服务工单描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceOrderDescription { get; set; }

        // -------------------- 软件服务相关信息 --------------------

        /// <summary>软件编号</summary>
        [SugarColumn(ColumnName = "software_code", ColumnDescription = "软件编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareCode { get; set; }

        /// <summary>软件名称</summary>
        [SugarColumn(ColumnName = "software_name", ColumnDescription = "软件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareName { get; set; }

        /// <summary>软件简称</summary>
        [SugarColumn(ColumnName = "software_short_name", ColumnDescription = "软件简称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareShortName { get; set; }

        /// <summary>软件版本</summary>
        [SugarColumn(ColumnName = "software_version", ColumnDescription = "软件版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareVersion { get; set; }

        /// <summary>软件开发商</summary>
        [SugarColumn(ColumnName = "software_developer", ColumnDescription = "软件开发商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareDeveloper { get; set; }

        /// <summary>软件服务商</summary>
        [SugarColumn(ColumnName = "software_service_provider", ColumnDescription = "软件服务商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareServiceProvider { get; set; }

        /// <summary>软件许可</summary>
        [SugarColumn(ColumnName = "software_license", ColumnDescription = "软件许可", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SoftwareLicense { get; set; }

        /// <summary>许可到期</summary>
        [SugarColumn(ColumnName = "license_expire_date", ColumnDescription = "许可到期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LicenseExpireDate { get; set; }

        /// <summary>服务许可</summary>
        [SugarColumn(ColumnName = "service_license", ColumnDescription = "服务许可", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceLicense { get; set; }

        /// <summary>服务到期</summary>
        [SugarColumn(ColumnName = "service_expire_date", ColumnDescription = "服务到期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ServiceExpireDate { get; set; }

        /// <summary>服务类型(1=开发服务 2=维护服务 3=培训服务 4=咨询服务 5=技术支持 6=定制开发 7=系统集成 8=数据迁移 9=软件升级 10=故障处理)</summary>
        [SugarColumn(ColumnName = "service_type", ColumnDescription = "服务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ServiceType { get; set; } = 2;

        /// <summary>服务分类</summary>
        [SugarColumn(ColumnName = "service_category", ColumnDescription = "服务分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceCategory { get; set; }

        /// <summary>服务级别(1=基础服务 2=标准服务 3=高级服务 4=专业服务 5=定制服务)</summary>
        [SugarColumn(ColumnName = "service_level", ColumnDescription = "服务级别", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ServiceLevel { get; set; } = 2;

        /// <summary>服务描述</summary>
        [SugarColumn(ColumnName = "service_description", ColumnDescription = "服务描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceDescription { get; set; }

        /// <summary>服务内容</summary>
        [SugarColumn(ColumnName = "service_content", ColumnDescription = "服务内容", Length = 2000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceContent { get; set; }

        /// <summary>服务范围</summary>
        [SugarColumn(ColumnName = "service_scope", ColumnDescription = "服务范围", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceScope { get; set; }

        /// <summary>服务标准</summary>
        [SugarColumn(ColumnName = "service_standard", ColumnDescription = "服务标准", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceStandard { get; set; }

        /// <summary>响应时间(小时)</summary>
        [SugarColumn(ColumnName = "response_time_hours", ColumnDescription = "响应时间(小时)", ColumnDataType = "int", IsNullable = false, DefaultValue = "24")]
        public int ResponseTimeHours { get; set; } = 24;

        /// <summary>解决时间(小时)</summary>
        [SugarColumn(ColumnName = "resolution_time_hours", ColumnDescription = "解决时间(小时)", ColumnDataType = "int", IsNullable = false, DefaultValue = "72")]
        public int ResolutionTimeHours { get; set; } = 72;

        /// <summary>服务时间</summary>
        [SugarColumn(ColumnName = "service_time", ColumnDescription = "服务时间", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceTime { get; set; }

        /// <summary>服务价格</summary>
        [SugarColumn(ColumnName = "service_price", ColumnDescription = "服务价格", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ServicePrice { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>计费方式(1=按次收费 2=按月收费 3=按年收费 4=按项目收费 5=免费)</summary>
        [SugarColumn(ColumnName = "billing_method", ColumnDescription = "计费方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int BillingMethod { get; set; } = 2;

        /// <summary>服务周期(天)</summary>
        [SugarColumn(ColumnName = "service_cycle_days", ColumnDescription = "服务周期(天)", ColumnDataType = "int", IsNullable = false, DefaultValue = "30")]
        public int ServiceCycleDays { get; set; } = 30;

        /// <summary>服务开始日期</summary>
        [SugarColumn(ColumnName = "service_start_date", ColumnDescription = "服务开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ServiceStartDate { get; set; }

        /// <summary>服务结束日期</summary>
        [SugarColumn(ColumnName = "service_end_date", ColumnDescription = "服务结束日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ServiceEndDate { get; set; }

        /// <summary>联系人</summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>联系电话</summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>联系邮箱</summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>服务地址</summary>
        [SugarColumn(ColumnName = "service_address", ColumnDescription = "服务地址", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceAddress { get; set; }

        /// <summary>服务团队</summary>
        [SugarColumn(ColumnName = "service_team", ColumnDescription = "服务团队", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceTeam { get; set; }

        /// <summary>服务工具</summary>
        [SugarColumn(ColumnName = "service_tools", ColumnDescription = "服务工具", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceTools { get; set; }

        /// <summary>服务文档</summary>
        [SugarColumn(ColumnName = "service_document", ColumnDescription = "服务文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceDocument { get; set; }

        /// <summary>服务协议</summary>
        [SugarColumn(ColumnName = "service_agreement", ColumnDescription = "服务协议", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceAgreement { get; set; }

        /// <summary>服务质量评分</summary>
        [SugarColumn(ColumnName = "service_quality_score", ColumnDescription = "服务质量评分", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? ServiceQualityScore { get; set; }

        /// <summary>客户满意度</summary>
        [SugarColumn(ColumnName = "customer_satisfaction", ColumnDescription = "客户满意度", ColumnDataType = "decimal(3,1)", IsNullable = true)]
        public decimal? CustomerSatisfaction { get; set; }

        /// <summary>服务工单状态(0=草稿 1=已提交 2=已分配 3=处理中 4=已完成 5=已验收 6=已关闭 7=已取消)</summary>
        [SugarColumn(ColumnName = "service_order_status", ColumnDescription = "服务工单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ServiceOrderStatus { get; set; } = 0;

        /// <summary>优先级(1=低 2=中 3=高 4=紧急)</summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>申请日期</summary>
        [SugarColumn(ColumnName = "application_date", ColumnDescription = "申请日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ApplicationDate { get; set; } = DateTime.Now;

        /// <summary>申请人</summary>
        [SugarColumn(ColumnName = "applicant", ColumnDescription = "申请人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string Applicant { get; set; } = string.Empty;

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

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "service_order_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ServiceOrderRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



