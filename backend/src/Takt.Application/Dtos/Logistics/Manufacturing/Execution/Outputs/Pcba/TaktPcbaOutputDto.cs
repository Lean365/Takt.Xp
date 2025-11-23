//===================================================================
// 项目名: Takt.Application
// 文件名: TaktPcbaOutputDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: PCBA产出主表数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Execution.Outputs.Pcba
{
    /// <summary>
    /// PCBA产出主表基础DTO
    /// </summary>
    public class TaktPcbaOutputDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            BatchNo = string.Empty;
            MaterialCode = string.Empty;
            PcbaOutputDetails = new List<TaktPcbaOutputDetailDto>();
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaOutputId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// </summary>
        public string ProdCategory { get; set; } = string.Empty;

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
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

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
        /// 组立明细列表
        /// </summary>
        public List<TaktPcbaOutputDetailDto>? PcbaOutputDetails { get; set; }
    }

    /// <summary>
    /// PCBA产出主表查询DTO
    /// </summary>
    public class TaktPcbaOutputQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputQueryDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 生产类别
        /// </summary>
        public string? ProdCategory { get; set; }

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
    }

    /// <summary>
    /// PCBA产出主表创建DTO
    /// </summary>
    public class TaktPcbaOutputCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputCreateDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            BatchNo = string.Empty;
            MaterialCode = string.Empty;
            PcbaOutputDetails = new List<TaktPcbaOutputDetailDto>();
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// </summary>
        public string ProdCategory { get; set; } = string.Empty;

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
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktPcbaOutputDetailDto>? PcbaOutputDetails { get; set; }
    }

    /// <summary>
    /// PCBA产出主表更新DTO
    /// </summary>
    public class TaktPcbaOutputUpdateDto : TaktPcbaOutputCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaOutputId { get; set; }
    }

    /// <summary>
    /// PCBA产出主表导入DTO
    /// </summary>
    public class TaktPcbaOutputImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputImportDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            BatchNo = string.Empty;
            MaterialCode = string.Empty;
            PcbaOutputDetails = new List<TaktPcbaOutputDetailDto>();
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// </summary>
        public string ProdCategory { get; set; } = string.Empty;

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
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktPcbaOutputDetailDto>? PcbaOutputDetails { get; set; }
    }

    /// <summary>
    /// PCBA产出主表导出DTO
    /// </summary>
    public class TaktPcbaOutputExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputExportDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            PcbaOutputDetails = new List<TaktPcbaOutputDetailDto>();
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// </summary>
        public string ProdCategory { get; set; } = string.Empty;

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktPcbaOutputDetailDto>? PcbaOutputDetails { get; set; }
    }

    /// <summary>
    /// PCBA产出主表模板DTO
    /// </summary>
    public class TaktPcbaOutputTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputTemplateDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            BatchNo = string.Empty;
            MaterialCode = string.Empty;
            PcbaOutputDetails = new List<TaktPcbaOutputDetailDto>();
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// </summary>
        public string ProdCategory { get; set; } = string.Empty;

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
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准点数
        /// </summary>
        public int StdShorts { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktPcbaOutputDetailDto>? PcbaOutputDetails { get; set; }
    }

    /// <summary>
    /// PCBA产出主表状态更新DTO
    /// </summary>
    public class TaktPcbaOutputStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPcbaOutputStatusDto()
        {
        }

        /// <summary>
        /// 主键ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long PcbaOutputId { get; set; }

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


