//===================================================================
// 项目名: Takt.Application
// 文件名: TaktStandardTimeDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 标准工时数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时基础DTO（与TaktStandardTime实体字段严格对应）
    /// </summary>
    public class TaktStandardTimeDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
            ChangeLogs = new List<TaktStandardTimeChangeLogDto>();
        }

        /// <summary>
        /// 标准工时ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long StandardTimeId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 标准工时变更记录集合 (子表)
        /// </summary>
        public List<TaktStandardTimeChangeLogDto>? ChangeLogs { get; set; }
    }

    /// <summary>
    /// 标准工时查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeQueryDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            Approver = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
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
    /// 标准工时创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeCreateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
            ChangeLogs = new List<TaktStandardTimeChangeLogCreateDto>();
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 标准工时变更记录集合
        /// </summary>
        public List<TaktStandardTimeChangeLogCreateDto>? ChangeLogs { get; set; }
    }

    /// <summary>
    /// 标准工时更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeUpdateDto : TaktStandardTimeCreateDto
    {
        /// <summary>
        /// 标准工时ID
        /// </summary>
        [AdaptMember("Id")]
        public long StandardTimeId { get; set; }
    }

    /// <summary>
    /// 标准工时导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeImportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 标准工时导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeExportDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 标准工时导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeTemplateDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string WorkCenter { get; set; } = string.Empty;

        /// <summary>
        /// 工序描述
        /// </summary>
        public string? OperationDesc { get; set; }

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Approver { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ApprovalDate { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 标准工时状态DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeStatusDto()
        {
        }

        /// <summary>
        /// 标准工时ID
        /// </summary>
        [AdaptMember("Id")]
        public long Id { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 标准工时和点数选项DTO
    /// </summary>
    public class TaktStandardTimeAndShortsOptionDto
    {
        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StandardMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;
    }

}

