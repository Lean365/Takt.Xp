//===================================================================
// 项目名: Takt.Application
// 文件名: TaktStandardTimeChangeLogDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 标准工时变更记录数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时变更记录基础DTO（与TaktStandardTimeChangeLog实体字段严格对应）
    /// </summary>
    public class TaktStandardTimeChangeLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeChangeLogDto()
        {
            PlantCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            TimeUnit = "MIN";
            PointsUnit = "SHORT";
            ChangeUser = string.Empty;
        }

        /// <summary>
        /// 标准工时变更记录ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long StandardTimeChangeLogId { get; set; }

        /// <summary>
        /// 标准工时ID
        /// </summary>
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
        /// 标准点数
        /// </summary>
        public int StandardShorts { get; set; } = 0;

        /// <summary>
        /// 点数转分钟汇率(1点数=多少分钟)
        /// </summary>
        public decimal PointsToMinutesRate { get; set; } = 1;

        /// <summary>
        /// 转换后标准工时(分钟)
        /// </summary>
        public decimal ConvertedMinutes { get; set; } = 0;

        /// <summary>
        /// 工时单位
        /// </summary>
        public string TimeUnit { get; set; } = "MIN";

        /// <summary>
        /// 点数单位
        /// </summary>
        public string PointsUnit { get; set; } = "SHORT";

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 变更类型(1=新增 2=修改 3=删除 4=审核 5=停用 6=启用)
        /// </summary>
        public int ChangeType { get; set; } = 1;

        /// <summary>
        /// 变更日期
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变更人
        /// </summary>
        public string ChangeUser { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 变更前值(JSON格式)
        /// </summary>
        public string? BeforeValue { get; set; }

        /// <summary>
        /// 变更后值(JSON格式)
        /// </summary>
        public string? AfterValue { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 1;
    }

    /// <summary>
    /// 标准工时变更记录查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeChangeLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeChangeLogQueryDto()
        {
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            ChangeUser = string.Empty;
        }

        /// <summary>
        /// 标准工时ID
        /// </summary>
        public long? StandardTimeId { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 变更类型(1=新增 2=修改 3=删除 4=审核 5=停用 6=启用)
        /// </summary>
        public int? ChangeType { get; set; }

        /// <summary>
        /// 变更人
        /// </summary>
        public string? ChangeUser { get; set; }

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
    /// 标准工时变更记录创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeChangeLogCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktStandardTimeChangeLogCreateDto()
        {
            ChangeUser = string.Empty;
        }

        /// <summary>
        /// 标准工时ID
        /// </summary>
        public long StandardTimeId { get; set; }

        /// <summary>
        /// 变更类型(1=新增 2=修改 3=删除 4=审核 5=停用 6=启用)
        /// </summary>
        public int ChangeType { get; set; } = 1;

        /// <summary>
        /// 变更日期
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 变更人
        /// </summary>
        public string ChangeUser { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 变更前值(JSON格式)
        /// </summary>
        public string? BeforeValue { get; set; }

        /// <summary>
        /// 变更后值(JSON格式)
        /// </summary>
        public string? AfterValue { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 1;
    }
}

