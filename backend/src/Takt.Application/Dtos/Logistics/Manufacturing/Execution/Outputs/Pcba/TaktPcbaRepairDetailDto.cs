//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPcbaRepairDetailDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: PCBA维修明细表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Outputs.Pcba
{
    /// <summary>
    /// PCBA维修明细表基础DTO
    /// </summary>
    public class TaktPcbaRepairDetailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailDto()
        {
            RepairItem = string.Empty;
            RepairMethod = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairDetailId { get; set; }

        /// <summary>
        /// PCBA维修主表ID
        /// </summary>
        public long PcbaRepairId { get; set; }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string RepairItem { get; set; } = string.Empty;

        /// <summary>
        /// 维修方法
        /// </summary>
        public string? RepairMethod { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修工时
        /// </summary>
        public decimal RepairHours { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

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
    /// PCBA维修明细表查询DTO
    /// </summary>
    public class TaktPcbaRepairDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailQueryDto()
        {
            RepairItem = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// PCBA维修主表ID
        /// </summary>
        public long? PcbaRepairId { get; set; }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string? RepairItem { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string? RepairResult { get; set; }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }
    }

    /// <summary>
    /// PCBA维修明细表创建DTO
    /// </summary>
    public class TaktPcbaRepairDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailCreateDto()
        {
            RepairItem = string.Empty;
            RepairMethod = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// PCBA维修主表ID
        /// </summary>
        public long PcbaRepairId { get; set; }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string RepairItem { get; set; } = string.Empty;

        /// <summary>
        /// 维修方法
        /// </summary>
        public string? RepairMethod { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修工时
        /// </summary>
        public decimal RepairHours { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修明细表更新DTO
    /// </summary>
    public class TaktPcbaRepairDetailUpdateDto : TaktPcbaRepairDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairDetailId { get; set; }
    }

    /// <summary>
    /// PCBA维修明细表导入DTO
    /// </summary>
    public class TaktPcbaRepairDetailImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailImportDto()
        {
            RepairItem = string.Empty;
            RepairMethod = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string RepairItem { get; set; } = string.Empty;

        /// <summary>
        /// 维修方法
        /// </summary>
        public string? RepairMethod { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修工时
        /// </summary>
        public decimal RepairHours { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修明细表导出DTO
    /// </summary>
    public class TaktPcbaRepairDetailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailExportDto()
        {
            RepairItem = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string RepairItem { get; set; } = string.Empty;

        /// <summary>
        /// 维修方法
        /// </summary>
        public string? RepairMethod { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修工时
        /// </summary>
        public decimal RepairHours { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;
    }

    /// <summary>
    /// PCBA维修明细表模板DTO
    /// </summary>
    public class TaktPcbaRepairDetailTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailTemplateDto()
        {
            RepairItem = string.Empty;
            RepairMethod = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 维修项目
        /// </summary>
        public string RepairItem { get; set; } = string.Empty;

        /// <summary>
        /// 维修方法
        /// </summary>
        public string? RepairMethod { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修成功数量
        /// </summary>
        public decimal SuccessQty { get; set; } = 0;

        /// <summary>
        /// 维修失败数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修工时
        /// </summary>
        public decimal RepairHours { get; set; } = 0;

        /// <summary>
        /// 维修成本
        /// </summary>
        public decimal RepairCost { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA维修明细表状态更新DTO
    /// </summary>
    public class TaktPcbaRepairDetailStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaRepairDetailStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaRepairDetailId { get; set; }

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


