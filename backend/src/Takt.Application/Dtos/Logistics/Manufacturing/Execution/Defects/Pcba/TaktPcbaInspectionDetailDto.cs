//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPcbaInspectionDetailDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: PCBA检验明细表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Defects.Pcba
{
    /// <summary>
    /// PCBA检验明细表基础DTO
    /// </summary>
    public class TaktPcbaInspectionDetailDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionStandard = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionDetailId { get; set; }

        /// <summary>
        /// PCBA检验主表ID
        /// </summary>
        public long PcbaInspectionId { get; set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; } = string.Empty;

        /// <summary>
        /// 检验标准
        /// </summary>
        public string? InspectionStandard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; } = string.Empty;

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
        /// 检验备注
        /// </summary>
        public string? InspectionRemark { get; set; }

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
    /// PCBA检验明细表查询DTO
    /// </summary>
    public class TaktPcbaInspectionDetailQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailQueryDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// PCBA检验主表ID
        /// </summary>
        public long? PcbaInspectionId { get; set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string? InspectionItemCode { get; set; }

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string? InspectionItemName { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string? InspectionResult { get; set; }

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }
    }

    /// <summary>
    /// PCBA检验明细表创建DTO
    /// </summary>
    public class TaktPcbaInspectionDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailCreateDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionStandard = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// PCBA检验主表ID
        /// </summary>
        public long PcbaInspectionId { get; set; }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; } = string.Empty;

        /// <summary>
        /// 检验标准
        /// </summary>
        public string? InspectionStandard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; } = string.Empty;

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
        /// 检验备注
        /// </summary>
        public string? InspectionRemark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验明细表更新DTO
    /// </summary>
    public class TaktPcbaInspectionDetailUpdateDto : TaktPcbaInspectionDetailCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionDetailId { get; set; }
    }

    /// <summary>
    /// PCBA检验明细表导入DTO
    /// </summary>
    public class TaktPcbaInspectionDetailImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailImportDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionStandard = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; } = string.Empty;

        /// <summary>
        /// 检验标准
        /// </summary>
        public string? InspectionStandard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; } = string.Empty;

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
        /// 检验备注
        /// </summary>
        public string? InspectionRemark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验明细表导出DTO
    /// </summary>
    public class TaktPcbaInspectionDetailExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailExportDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; } = string.Empty;

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; } = string.Empty;

        /// <summary>
        /// 不良代码
        /// </summary>
        public string? DefectCode { get; set; }

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
    }

    /// <summary>
    /// PCBA检验明细表模板DTO
    /// </summary>
    public class TaktPcbaInspectionDetailTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailTemplateDto()
        {
            InspectionItemCode = string.Empty;
            InspectionItemName = string.Empty;
            InspectionStandard = string.Empty;
            InspectionResult = string.Empty;
            DefectCode = string.Empty;
            DefectName = string.Empty;
            DefectDescription = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 检验项目代码
        /// </summary>
        public string InspectionItemCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验项目名称
        /// </summary>
        public string InspectionItemName { get; set; } = string.Empty;

        /// <summary>
        /// 检验标准
        /// </summary>
        public string? InspectionStandard { get; set; }

        /// <summary>
        /// 检验结果
        /// </summary>
        public string InspectionResult { get; set; } = string.Empty;

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
        /// 检验备注
        /// </summary>
        public string? InspectionRemark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验明细表状态更新DTO
    /// </summary>
    public class TaktPcbaInspectionDetailStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDetailStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionDetailId { get; set; }

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


