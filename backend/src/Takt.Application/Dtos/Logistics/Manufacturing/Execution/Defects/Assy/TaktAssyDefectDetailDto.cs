//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAssyDefectDetailDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 装配不良明细表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Defects.Assy
{
    /// <summary>
    /// 装配不良明细表基础DTO
    /// </summary>
    public class TaktAssyDefectDetailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            DefectPosition = string.Empty;
            DefectReason = string.Empty;
            DefectSolution = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectDetailId { get; set; }

        /// <summary>
        /// 装配不良主表ID
        /// </summary>
        public long AssyDefectId { get; set; }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string DefectCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良名称
        /// </summary>
        public string DefectName { get; set; } = string.Empty;

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良解决方案
        /// </summary>
        public string? DefectSolution { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良明细表查询DTO
    /// </summary>
    public class TaktAssyDefectDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailQueryDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectPosition = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 装配不良主表ID
        /// </summary>
        public long? AssyDefectId { get; set; }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良名称
        /// </summary>
        public string? DefectName { get; set; }

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int? RepairStatus { get; set; }
    }

    /// <summary>
    /// 装配不良明细表创建DTO
    /// </summary>
    public class TaktAssyDefectDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailCreateDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            DefectPosition = string.Empty;
            DefectReason = string.Empty;
            DefectSolution = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 装配不良主表ID
        /// </summary>
        public long AssyDefectId { get; set; }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string DefectCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良名称
        /// </summary>
        public string DefectName { get; set; } = string.Empty;

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良解决方案
        /// </summary>
        public string? DefectSolution { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良明细表更新DTO
    /// </summary>
    public class TaktAssyDefectDetailUpdateDto : TaktAssyDefectDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectDetailId { get; set; }
    }

    /// <summary>
    /// 装配不良明细表导入DTO
    /// </summary>
    public class TaktAssyDefectDetailImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailImportDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            DefectPosition = string.Empty;
            DefectReason = string.Empty;
            DefectSolution = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string DefectCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良名称
        /// </summary>
        public string DefectName { get; set; } = string.Empty;

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良解决方案
        /// </summary>
        public string? DefectSolution { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良明细表导出DTO
    /// </summary>
    public class TaktAssyDefectDetailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailExportDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectPosition = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string DefectCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良名称
        /// </summary>
        public string DefectName { get; set; } = string.Empty;

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; } = 0;
    }

    /// <summary>
    /// 装配不良明细表模板DTO
    /// </summary>
    public class TaktAssyDefectDetailTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailTemplateDto()
        {
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            DefectPosition = string.Empty;
            DefectReason = string.Empty;
            DefectSolution = string.Empty;
            Inspector = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string DefectCode { get; set; } = string.Empty;

        /// <summary>
        /// 不良名称
        /// </summary>
        public string DefectName { get; set; } = string.Empty;

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 不良位置
        /// </summary>
        public string? DefectPosition { get; set; }

        /// <summary>
        /// 不良原因
        /// </summary>
        public string? DefectReason { get; set; }

        /// <summary>
        /// 不良解决方案
        /// </summary>
        public string? DefectSolution { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        public decimal DefectQty { get; set; } = 0;

        /// <summary>
        /// 不良金额
        /// </summary>
        public decimal DefectAmount { get; set; } = 0;

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }

        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? InspectionTime { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修状态
        /// </summary>
        public int RepairStatus { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 装配不良明细表状态更新DTO
    /// </summary>
    public class TaktAssyDefectDetailStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyDefectDetailStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyDefectDetailId { get; set; }

        /// <summary>
        /// 状态（0停用 1正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}


