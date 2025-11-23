//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPcbaInspectionDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: PCBA检验主表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Defects.Pcba
{
    /// <summary>
    /// PCBA检验主表基础DTO
    /// </summary>
    public class TaktPcbaInspectionDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            InspectionMethod = string.Empty;
            Inspector = string.Empty;
            PcbaInspectionDetails = new List<TaktPcbaInspectionDetailDto>();
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string InspectionType { get; set; } = string.Empty;

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; set; } = string.Empty;

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = string.Empty;

        /// <summary>
        /// 检验开始时间
        /// </summary>
        public DateTime? InspectionStartTime { get; set; }

        /// <summary>
        /// 检验结束时间
        /// </summary>
        public DateTime? InspectionEndTime { get; set; }

        /// <summary>
        /// 检验数量
        /// </summary>
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassQty { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal PassRate { get; set; } = 0;

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

        /// <summary>
        /// PCBA检验明细列表
        /// </summary>
        public List<TaktPcbaInspectionDetailDto> PcbaInspectionDetails { get; set; }
    }

    /// <summary>
    /// PCBA检验主表查询DTO
    /// </summary>
    public class TaktPcbaInspectionQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionQueryDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 生产日期开始
        /// </summary>
        public DateTime? ProdDateStart { get; set; }

        /// <summary>
        /// 生产日期结束
        /// </summary>
        public DateTime? ProdDateEnd { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public int? ShiftNo { get; set; }

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string? ProdOrderCode { get; set; }

        /// <summary>
        /// 机种
        /// </summary>
        public string? ModelCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string? InspectionType { get; set; }

        /// <summary>
        /// 检验员
        /// </summary>
        public string? Inspector { get; set; }
    }

    /// <summary>
    /// PCBA检验主表创建DTO
    /// </summary>
    public class TaktPcbaInspectionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionCreateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            InspectionMethod = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string InspectionType { get; set; } = string.Empty;

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; set; } = string.Empty;

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = string.Empty;

        /// <summary>
        /// 检验开始时间
        /// </summary>
        public DateTime? InspectionStartTime { get; set; }

        /// <summary>
        /// 检验结束时间
        /// </summary>
        public DateTime? InspectionEndTime { get; set; }

        /// <summary>
        /// 检验数量
        /// </summary>
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassQty { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal PassRate { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验主表更新DTO
    /// </summary>
    public class TaktPcbaInspectionUpdateDto : TaktPcbaInspectionCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionId { get; set; }
    }

    /// <summary>
    /// PCBA检验主表导入DTO
    /// </summary>
    public class TaktPcbaInspectionImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionImportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            InspectionMethod = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string InspectionType { get; set; } = string.Empty;

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; set; } = string.Empty;

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = string.Empty;

        /// <summary>
        /// 检验开始时间
        /// </summary>
        public DateTime? InspectionStartTime { get; set; }

        /// <summary>
        /// 检验结束时间
        /// </summary>
        public DateTime? InspectionEndTime { get; set; }

        /// <summary>
        /// 检验数量
        /// </summary>
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassQty { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal PassRate { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验主表导出DTO
    /// </summary>
    public class TaktPcbaInspectionExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionExportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验类型
        /// </summary>
        public string InspectionType { get; set; } = string.Empty;

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = string.Empty;

        /// <summary>
        /// 检验数量
        /// </summary>
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassQty { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal PassRate { get; set; } = 0;
    }

    /// <summary>
    /// PCBA检验主表模板DTO
    /// </summary>
    public class TaktPcbaInspectionTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionTemplateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            InspectionType = string.Empty;
            InspectionMethod = string.Empty;
            Inspector = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string ProdLine { get; set; } = string.Empty;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 机种
        /// </summary>
        public string ModelCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 检验类型
        /// </summary>
        public string InspectionType { get; set; } = string.Empty;

        /// <summary>
        /// 检验方法
        /// </summary>
        public string InspectionMethod { get; set; } = string.Empty;

        /// <summary>
        /// 检验员
        /// </summary>
        public string Inspector { get; set; } = string.Empty;

        /// <summary>
        /// 检验开始时间
        /// </summary>
        public DateTime? InspectionStartTime { get; set; }

        /// <summary>
        /// 检验结束时间
        /// </summary>
        public DateTime? InspectionEndTime { get; set; }

        /// <summary>
        /// 检验数量
        /// </summary>
        public decimal InspectionQty { get; set; } = 0;

        /// <summary>
        /// 合格数量
        /// </summary>
        public decimal PassQty { get; set; } = 0;

        /// <summary>
        /// 不合格数量
        /// </summary>
        public decimal FailQty { get; set; } = 0;

        /// <summary>
        /// 合格率
        /// </summary>
        public decimal PassRate { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// PCBA检验主表状态更新DTO
    /// </summary>
    public class TaktPcbaInspectionStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaInspectionStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaInspectionId { get; set; }

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


