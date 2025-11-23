#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktComplaint.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 核心客诉主表实体类 (基于SAP QM质量管理)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Complaint
{
    /// <summary>
    /// 核心客诉主表实体类 (基于SAP QM质量管理)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: SAP QM 质量管理模块
    /// </remarks>
    [SugarTable("Takt_logistics_complaint", "核心客诉")]
    [SugarIndex("ix_complaint_code", nameof(ComplaintCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_plant", nameof(CompanyCode), OrderByType.Asc, nameof(PlantCode), OrderByType.Asc, false)]
    public class TaktComplaint : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 客诉编号
        /// </summary>
        [SugarColumn(ColumnName = "complaint_code", ColumnDescription = "客诉编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComplaintCode { get; set; } = string.Empty;

        /// <summary>
        /// 客诉类型(1=产品质量 2=服务质量 3=交付问题 4=包装问题 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "complaint_type", ColumnDescription = "客诉类型", ColumnDataType = "int", IsNullable = false)]
        public int ComplaintType { get; set; }

        /// <summary>
        /// 客诉类型名称
        /// </summary>
        [SugarColumn(ColumnName = "complaint_type_name", ColumnDescription = "客诉类型名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComplaintTypeName { get; set; }

        /// <summary>
        /// 客诉级别(1=轻微 2=一般 3=严重 4=重大)
        /// </summary>
        [SugarColumn(ColumnName = "complaint_level", ColumnDescription = "客诉级别", ColumnDataType = "int", IsNullable = false)]
        public int ComplaintLevel { get; set; }

        /// <summary>
        /// 客诉级别名称
        /// </summary>
        [SugarColumn(ColumnName = "complaint_level_name", ColumnDescription = "客诉级别名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComplaintLevelName { get; set; }

        /// <summary>
        /// 客诉标题
        /// </summary>
        [SugarColumn(ColumnName = "complaint_title", ColumnDescription = "客诉标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ComplaintTitle { get; set; } = string.Empty;

        /// <summary>
        /// 客诉描述
        /// </summary>
        [SugarColumn(ColumnName = "complaint_description", ColumnDescription = "客诉描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComplaintDescription { get; set; }

        /// <summary>
        /// 客户编码
        /// </summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerCode { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        [SugarColumn(ColumnName = "customer_name", ColumnDescription = "客户名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [SugarColumn(ColumnName = "contact_person", ColumnDescription = "联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactPhone { get; set; }

        /// <summary>
        /// 联系邮箱
        /// </summary>
        [SugarColumn(ColumnName = "contact_email", ColumnDescription = "联系邮箱", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContactEmail { get; set; }

        /// <summary>
        /// 客诉发生日期
        /// </summary>
        [SugarColumn(ColumnName = "complaint_date", ColumnDescription = "客诉发生日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ComplaintDate { get; set; }

        /// <summary>
        /// 客诉接收日期
        /// </summary>
        [SugarColumn(ColumnName = "receive_date", ColumnDescription = "客诉接收日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ReceiveDate { get; set; }

        /// <summary>
        /// 客诉处理截止日期
        /// </summary>
        [SugarColumn(ColumnName = "deadline_date", ColumnDescription = "客诉处理截止日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? DeadlineDate { get; set; }

        /// <summary>
        /// 客诉状态(0=待处理 1=处理中 2=已完成 3=已关闭 4=已取消)
        /// </summary>
        [SugarColumn(ColumnName = "complaint_status", ColumnDescription = "客诉状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ComplaintStatus { get; set; } = 0;

        /// <summary>
        /// 客诉状态名称
        /// </summary>
        [SugarColumn(ColumnName = "complaint_status_name", ColumnDescription = "客诉状态名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComplaintStatusName { get; set; }

        /// <summary>
        /// 处理优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "处理优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 处理优先级名称
        /// </summary>
        [SugarColumn(ColumnName = "priority_name", ColumnDescription = "处理优先级名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? PriorityName { get; set; }

        /// <summary>
        /// 客诉来源(1=电话 2=邮件 3=现场 4=系统 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "complaint_source", ColumnDescription = "客诉来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ComplaintSource { get; set; } = 1;

        /// <summary>
        /// 客诉来源名称
        /// </summary>
        [SugarColumn(ColumnName = "complaint_source_name", ColumnDescription = "客诉来源名称", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ComplaintSourceName { get; set; }

        /// <summary>
        /// 是否重复客诉
        /// </summary>
        [SugarColumn(ColumnName = "is_repeat", ColumnDescription = "是否重复客诉", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsRepeat { get; set; } = 0;

        /// <summary>
        /// 原客诉编号
        /// </summary>
        [SugarColumn(ColumnName = "original_complaint_code", ColumnDescription = "原客诉编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OriginalComplaintCode { get; set; }

        /// <summary>
        /// 客诉金额
        /// </summary>
        [SugarColumn(ColumnName = "complaint_amount", ColumnDescription = "客诉金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ComplaintAmount { get; set; } = 0;

        /// <summary>
        /// 币种
        /// </summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; }
    }
} 



