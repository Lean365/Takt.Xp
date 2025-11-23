#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktEquipmentOperationRateDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 设备稼动率数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 设备稼动率基础DTO
    /// </summary>
    public class TaktEquipmentOperationRateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            EquipmentName = string.Empty;
            WorkCenter = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// 设备稼动率ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long EquipmentOperationRateId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal PlannedRunTime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal ActualRunTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        public decimal OperationRate { get; set; } = 0;

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
    /// 设备稼动率查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateQueryDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string? EquipmentCode { get; set; }

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
    /// 设备稼动率创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateCreateDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            EquipmentName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal PlannedRunTime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal ActualRunTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        public decimal OperationRate { get; set; } = 0;

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
    /// 设备稼动率更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateUpdateDto : TaktEquipmentOperationRateCreateDto
    {
        /// <summary>
        /// 设备稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long EquipmentOperationRateId { get; set; }
    }

    /// <summary>
    /// 设备稼动率状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateStatusDto
    {
        /// <summary>
        /// 设备稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long EquipmentOperationRateId { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 设备稼动率导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateImportDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            EquipmentName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal PlannedRunTime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal ActualRunTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        public decimal OperationRate { get; set; } = 0;

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
    /// 设备稼动率导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateExportDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            EquipmentName = string.Empty;
            WorkCenter = string.Empty;
            StatusText = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal PlannedRunTime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal ActualRunTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        public decimal OperationRate { get; set; } = 0;

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
    /// 设备稼动率模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktEquipmentOperationRateTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktEquipmentOperationRateTemplateDto()
        {
            PlantCode = string.Empty;
            EquipmentCode = string.Empty;
            EquipmentName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备编码
        /// </summary>
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 计划运行时间(小时)
        /// </summary>
        public decimal PlannedRunTime { get; set; } = 0;

        /// <summary>
        /// 实际运行时间(小时)
        /// </summary>
        public decimal ActualRunTime { get; set; } = 0;

        /// <summary>
        /// 停机时间(小时)
        /// </summary>
        public decimal Downtime { get; set; } = 0;

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        public decimal OperationRate { get; set; } = 0;

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



