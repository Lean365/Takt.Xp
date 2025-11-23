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

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Defects.Pcba
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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairDescription = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
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
        /// 维修项目代码
        /// </summary>
        public string RepairItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string RepairItemName { get; set; } = string.Empty;

        /// <summary>
        /// 维修描述
        /// </summary>
        public string? RepairDescription { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良名称
        /// </summary>
        public string? DefectName { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修金额
        /// </summary>
        public decimal RepairAmount { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修备注
        /// </summary>
        public string? RepairRemark { get; set; }

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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// PCBA维修主表ID
        /// </summary>
        public long? PcbaRepairId { get; set; }

        /// <summary>
        /// 维修项目代码
        /// </summary>
        public string? RepairItemCode { get; set; }

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string? RepairItemName { get; set; }

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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairDescription = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// PCBA维修主表ID
        /// </summary>
        public long PcbaRepairId { get; set; }

        /// <summary>
        /// 维修项目代码
        /// </summary>
        public string RepairItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string RepairItemName { get; set; } = string.Empty;

        /// <summary>
        /// 维修描述
        /// </summary>
        public string? RepairDescription { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良名称
        /// </summary>
        public string? DefectName { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修金额
        /// </summary>
        public decimal RepairAmount { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修备注
        /// </summary>
        public string? RepairRemark { get; set; }

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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairDescription = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 维修项目代码
        /// </summary>
        public string RepairItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string RepairItemName { get; set; } = string.Empty;

        /// <summary>
        /// 维修描述
        /// </summary>
        public string? RepairDescription { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良名称
        /// </summary>
        public string? DefectName { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修金额
        /// </summary>
        public decimal RepairAmount { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修备注
        /// </summary>
        public string? RepairRemark { get; set; }

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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 维修项目代码
        /// </summary>
        public string RepairItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string RepairItemName { get; set; } = string.Empty;

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修金额
        /// </summary>
        public decimal RepairAmount { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }
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
            RepairItemCode = string.Empty;
            RepairItemName = string.Empty;
            RepairDescription = string.Empty;
            RepairResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            RepairPerson = string.Empty;
        }

        /// <summary>
        /// 维修项目代码
        /// </summary>
        public string RepairItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 维修项目名称
        /// </summary>
        public string RepairItemName { get; set; } = string.Empty;

        /// <summary>
        /// 维修描述
        /// </summary>
        public string? RepairDescription { get; set; }

        /// <summary>
        /// 维修结果
        /// </summary>
        public string RepairResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 不良名称
        /// </summary>
        public string? DefectName { get; set; }

        /// <summary>
        /// 不良描述
        /// </summary>
        public string? DefectDescription { get; set; }

        /// <summary>
        /// 维修数量
        /// </summary>
        public decimal RepairQty { get; set; } = 0;

        /// <summary>
        /// 维修金额
        /// </summary>
        public decimal RepairAmount { get; set; } = 0;

        /// <summary>
        /// 维修人员
        /// </summary>
        public string? RepairPerson { get; set; }

        /// <summary>
        /// 维修时间
        /// </summary>
        public DateTime? RepairTime { get; set; }

        /// <summary>
        /// 维修备注
        /// </summary>
        public string? RepairRemark { get; set; }

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


