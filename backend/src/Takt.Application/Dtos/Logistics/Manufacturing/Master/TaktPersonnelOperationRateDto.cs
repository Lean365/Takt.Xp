#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPersonnelOperationRateDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 人员稼动率数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 人员稼动率基础DTO
    /// </summary>
    public class TaktPersonnelOperationRateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            PersonnelName = string.Empty;
            WorkCenter = string.Empty;
            CreateBy = string.Empty;
            UpdateBy = string.Empty;
        }

        /// <summary>
        /// 人员稼动率ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PersonnelOperationRateId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员编码
        /// </summary>
        public string PersonnelCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonnelName { get; set; } = string.Empty;

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
        /// 休息时间(小时)
        /// </summary>
        public decimal RestTime { get; set; } = 0;

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
    /// 人员稼动率查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateQueryDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 人员编码
        /// </summary>
        public string? PersonnelCode { get; set; }

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
    /// 人员稼动率创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateCreateDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            PersonnelName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员编码
        /// </summary>
        public string PersonnelCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonnelName { get; set; } = string.Empty;

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
        /// 休息时间(小时)
        /// </summary>
        public decimal RestTime { get; set; } = 0;

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
    /// 人员稼动率更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateUpdateDto : TaktPersonnelOperationRateCreateDto
    {
        /// <summary>
        /// 人员稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long PersonnelOperationRateId { get; set; }
    }

    /// <summary>
    /// 人员稼动率状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateStatusDto
    {
        /// <summary>
        /// 人员稼动率ID
        /// </summary>
        [AdaptMember("Id")]
        public long PersonnelOperationRateId { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 人员稼动率导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateImportDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            PersonnelName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员编码
        /// </summary>
        public string PersonnelCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonnelName { get; set; } = string.Empty;

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
        /// 休息时间(小时)
        /// </summary>
        public decimal RestTime { get; set; } = 0;

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
    /// 人员稼动率导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateExportDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            PersonnelName = string.Empty;
            WorkCenter = string.Empty;
            StatusText = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员编码
        /// </summary>
        public string PersonnelCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonnelName { get; set; } = string.Empty;

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
        /// 休息时间(小时)
        /// </summary>
        public decimal RestTime { get; set; } = 0;

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
    /// 人员稼动率模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktPersonnelOperationRateTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPersonnelOperationRateTemplateDto()
        {
            PlantCode = string.Empty;
            PersonnelCode = string.Empty;
            PersonnelName = string.Empty;
            WorkCenter = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员编码
        /// </summary>
        public string PersonnelCode { get; set; } = string.Empty;

        /// <summary>
        /// 人员姓名
        /// </summary>
        public string PersonnelName { get; set; } = string.Empty;

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
        /// 休息时间(小时)
        /// </summary>
        public decimal RestTime { get; set; } = 0;

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



