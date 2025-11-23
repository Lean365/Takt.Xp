//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAssyOutputDetailDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 组立明细数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立明细基础DTO（与TaktAssyOutputDetail实体字段严格对应）
    /// </summary>
    public class TaktAssyOutputDetailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailDto()
        {
            TimePeriod = string.Empty;
            DowntimeReason = string.Empty;
            DowntimeDescription = string.Empty;
            UnachievedReason = string.Empty;
            UnachievedDescription = string.Empty;
        }

        /// <summary>
        /// 组立明细ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyMpOutputDetailId { get; set; }

        /// <summary>
        /// 组立日报ID
        /// </summary>
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        public decimal AchievementRate { get; set; } = 0;
    }

    /// <summary>
    /// 组立明细查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailQueryDto()
        {
            TimePeriod = string.Empty;
        }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string? TimePeriod { get; set; }
    }

    /// <summary>
    /// 组立明细创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailCreateDto()
        {
            TimePeriod = string.Empty;
            DowntimeReason = string.Empty;
            DowntimeDescription = string.Empty;
            UnachievedReason = string.Empty;
            UnachievedDescription = string.Empty;
        }

        /// <summary>
        /// 组立日报ID
        /// </summary>
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        public decimal AchievementRate { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string UpdateBy { get; set; } = string.Empty;

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime DeleteTime { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string DeleteBy { get; set; } = string.Empty;        
    }

    /// <summary>
    /// 组立明细更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailUpdateDto : TaktAssyOutputDetailCreateDto
    {
        /// <summary>
        /// 组立明细ID
        /// </summary>
        [AdaptMember("Id")]
        public long AssyMpOutputDetailId { get; set; }
    }

    /// <summary>
    /// 组立明细导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailImportDto()
        {
            TimePeriod = string.Empty;
            DowntimeReason = string.Empty;
            DowntimeDescription = string.Empty;
            UnachievedReason = string.Empty;
            UnachievedDescription = string.Empty;
        }

        /// <summary>
        /// 组立日报ID
        /// </summary>
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        public decimal AchievementRate { get; set; } = 0;
    }

    /// <summary>
    /// 组立明细导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailExportDto()
        {
            TimePeriod = string.Empty;
            DowntimeReason = string.Empty;
            DowntimeDescription = string.Empty;
            UnachievedReason = string.Empty;
            UnachievedDescription = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 组立日报ID
        /// </summary>
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        public decimal AchievementRate { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 组立明细导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDetailTemplateDto()
        {
            TimePeriod = string.Empty;
            DowntimeReason = string.Empty;
            DowntimeDescription = string.Empty;
            UnachievedReason = string.Empty;
            UnachievedDescription = string.Empty;
        }

        /// <summary>
        /// 组立日报ID
        /// </summary>
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 生产时段
        /// </summary>
        public string TimePeriod { get; set; } = string.Empty;

        /// <summary>
        /// 实际生产数量
        /// </summary>
        public decimal ProdActualQty { get; set; } = 0;

        /// <summary>
        /// 停线时间(分钟)
        /// </summary>
        public int DowntimeMinutes { get; set; } = 0;

        /// <summary>
        /// 停线原因
        /// </summary>
        public string? DowntimeReason { get; set; }

        /// <summary>
        /// 停线说明
        /// </summary>
        public string? DowntimeDescription { get; set; }

        /// <summary>
        /// 未达成原因
        /// </summary>
        public string? UnachievedReason { get; set; }

        /// <summary>
        /// 未达成说明
        /// </summary>
        public string? UnachievedDescription { get; set; }

        /// <summary>
        /// 投入工时(分钟)
        /// </summary>
        public decimal InputMinutes { get; set; } = 0;

        /// <summary>
        /// 生产工时(分钟)
        /// </summary>
        public decimal ProdMinutes { get; set; } = 0;

        /// <summary>
        /// 实际工时(分钟)
        /// </summary>
        public decimal ActualMinutes { get; set; } = 0;

        /// <summary>
        /// 达成率(%)
        /// </summary>
        public decimal AchievementRate { get; set; } = 0;
    }
}



