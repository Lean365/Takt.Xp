#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktOperationRateDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 稼动率数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 稼动率基础DTO
    /// </summary>
    public class TaktOperationRateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// 稼动率ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long OperationRateId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划工作时间(小时)
        /// </summary>
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(小时)
        /// </summary>
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 设备稼动率(%)
        /// </summary>
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        public decimal PersonnelOperationRate { get; set; } = 0;

        /// <summary>
        /// 综合稼动率(%)
        /// </summary>
        public decimal OverallOperationRate { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 稼动率查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateQueryDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// 稼动率创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateCreateDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划工作时间(小时)
        /// </summary>
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(小时)
        /// </summary>
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 设备稼动率(%)
        /// </summary>
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        public decimal PersonnelOperationRate { get; set; } = 0;

        /// <summary>
        /// 综合稼动率(%)
        /// </summary>
        public decimal OverallOperationRate { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 稼动率更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateUpdateDto : TaktOperationRateCreateDto
    {
        /// <summary>
        /// 稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long OperationRateId { get; set; }
    }

    /// <summary>
    /// 稼动率状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateStatusDto
    {
        /// <summary>
        /// 稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long OperationRateId { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 稼动率导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateImportDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划工作时间(小时)
        /// </summary>
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(小时)
        /// </summary>
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 设备稼动率(%)
        /// </summary>
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        public decimal PersonnelOperationRate { get; set; } = 0;

        /// <summary>
        /// 综合稼动率(%)
        /// </summary>
        public decimal OverallOperationRate { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 稼动率导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateExportDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
            StatusText = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划工作时间(小时)
        /// </summary>
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(小时)
        /// </summary>
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 设备稼动率(%)
        /// </summary>
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        public decimal PersonnelOperationRate { get; set; } = 0;

        /// <summary>
        /// 综合稼动率(%)
        /// </summary>
        public decimal OverallOperationRate { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusText { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 稼动率模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktOperationRateTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktOperationRateTemplateDto()
        {
            PlantCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划工作时间(小时)
        /// </summary>
        public decimal PlannedWorkTime { get; set; } = 0;

        /// <summary>
        /// 实际工作时间(小时)
        /// </summary>
        public decimal ActualWorkTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 设备稼动率(%)
        /// </summary>
        public decimal EquipmentOperationRate { get; set; } = 0;

        /// <summary>
        /// 人员稼动率(%)
        /// </summary>
        public decimal PersonnelOperationRate { get; set; } = 0;

        /// <summary>
        /// 综合稼动率(%)
        /// </summary>
        public decimal OverallOperationRate { get; set; } = 0;

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}



