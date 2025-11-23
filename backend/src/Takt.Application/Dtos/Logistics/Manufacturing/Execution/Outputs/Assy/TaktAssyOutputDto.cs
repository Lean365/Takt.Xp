//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAssyOutputDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 组立日报数据传输对象
//===================================================================

using System;
using System.Collections.Generic;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立日报基础DTO（与TaktAssyOutput实体字段严格对应）
    /// </summary>
    public class TaktAssyOutputDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
            AssyMpOutputDetails = new List<TaktAssyOutputDetailDto>();
        }

        /// <summary>
        /// 组立日报ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long AssyMpOutputId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产类别
        /// RD: 研发  EVT: 工程验证测试  DVT: 设计验证测试  EPP: 工程试产  PP: 试产  FPP: 正式生产  MP: 大规模生产  RPR: 维修生产  RWR: 返工生产
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
        /// 直接人员
        /// </summary>
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        public int IndirectLabor { get; set; } = 0;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 状态
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
        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktAssyOutputDetailDto>? AssyMpOutputDetails { get; set; }
    }

    /// <summary>
    /// 组立日报查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputQueryDto()
        {
            PlantCode = string.Empty;
            ProdCategory = string.Empty;
            ProdLine = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
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
        /// 生产日期范围-开始日期
        /// </summary>
        public DateTime? StartProdDate { get; set; }

        /// <summary>
        /// 生产日期范围-结束日期
        /// </summary>
        public DateTime? EndProdDate { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 批次
        /// </summary>
        public string? BatchNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }


    }

    /// <summary>
    /// 组立日报创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputCreateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
            AssyMpOutputDetails = new List<TaktAssyOutputDetailCreateDto>();
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
        /// 直接人员
        /// </summary>
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        public int IndirectLabor { get; set; } = 0;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 组立明细列表
        /// </summary>
        public List<TaktAssyOutputDetailCreateDto>? AssyMpOutputDetails { get; set; }
    }

    /// <summary>
    /// 组立日报更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputUpdateDto : TaktAssyOutputCreateDto
    {
        /// <summary>
        /// 组立日报ID
        /// </summary>
        [AdaptMember("Id")]
        public long AssyMpOutputId { get; set; }
    }

    /// <summary>
    /// 组立日报导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputImportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
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
        /// 直接人员
        /// </summary>
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        public int IndirectLabor { get; set; } = 0;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 组立日报导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputExportDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
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
        /// 直接人员
        /// </summary>
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        public int IndirectLabor { get; set; } = 0;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 组立日报导入模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAssyOutputTemplateDto()
        {
            PlantCode = string.Empty;
            ProdLine = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            ModelCode = string.Empty;
            MaterialCode = string.Empty;
            BatchNo = string.Empty;
            Remark = string.Empty;
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
        /// 直接人员
        /// </summary>
        public int DirectLabor { get; set; } = 0;

        /// <summary>
        /// 间接人员
        /// </summary>
        public int IndirectLabor { get; set; } = 0;

        /// <summary>
        /// 班次(1=早班 2=中班 3=晚班)
        /// </summary>
        public int ShiftNo { get; set; } = 1;

        /// <summary>
        /// 生产订单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

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
        /// 订单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 标准工时(分钟)
        /// </summary>
        public decimal StdMinutes { get; set; } = 0;

        /// <summary>
        /// 标准产能
        /// </summary>
        public decimal StdCapacity { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
}



