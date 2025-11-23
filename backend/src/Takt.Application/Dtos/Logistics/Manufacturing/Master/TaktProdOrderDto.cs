#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktProdOrderDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 生产工单数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单基础DTO（与TaktProdOrder实体字段严格对应）
    /// </summary>
    public class TaktProdOrderDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            UnitOfMeasure = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            RoutingCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
        }

        /// <summary>
        /// 生产工单ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long ProdOrderId { get; set; }

        // 业务字段 - 按照TaktProdOrder.cs的顺序
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 生产工单变更记录集合
        /// </summary>
        public List<TaktProdOrderChangeLogDto>? ChangeLogs { get; set; }
    }

    /// <summary>
    /// 生产工单查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderQueryDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string? PlantCode { get; set; }

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string? ProdOrderType { get; set; }

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string? ProdOrderCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string? MaterialCode { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

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
    /// 生产工单创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderCreateDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            UnitOfMeasure = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            RoutingCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 生产工单更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderUpdateDto : TaktProdOrderCreateDto
    {
        /// <summary>
        /// 生产工单ID
        /// </summary>
        [AdaptMember("Id")]
        public long ProdOrderId { get; set; }
    }

    /// <summary>
    /// 生产工单导入DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderImportDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            UnitOfMeasure = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            RoutingCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 生产工单导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderExportDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            UnitOfMeasure = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            RoutingCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string? RoutingCode { get; set; }

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
    /// 生产工单模板DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderTemplateDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            UnitOfMeasure = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            RoutingCode = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
        }

        /// <summary>
        /// 工厂代码
        /// </summary>
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 生产工单状态更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderStatusDto
    {
        /// <summary>
        /// 生产工单ID
        /// </summary>
        [AdaptMember("Id")]
        public long ProdOrderId { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用)
        /// </summary>
        public int Status { get; set; }
    }


}


