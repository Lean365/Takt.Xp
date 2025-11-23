#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktProdOrderChangeLogDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 生产工单变更记录数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单变更记录基础DTO（与TaktProdOrderChangeLog实体字段严格对应）
    /// </summary>
    public class TaktProdOrderChangeLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderChangeLogDto()
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
            ChangeUser = string.Empty;
            ChangeReason = string.Empty;
            BeforeValue = string.Empty;
            AfterValue = string.Empty;
            ChangeField = string.Empty;
            BeforeFieldValue = string.Empty;
            AfterFieldValue = string.Empty;
        }

        /// <summary>
        /// 变更记录ID（适配字段）
        /// </summary>
        [AdaptMember("Id")]
        public long ChangeLogId { get; set; }

        /// <summary>
        /// 生产工单ID
        /// </summary>
        public long ProdOrderId { get; set; }

        // 业务字段 - 按照TaktProdOrderChangeLog.cs的顺序
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
        /// 生产工单优先级
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
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        // 变更记录特有字段
        /// <summary>
        /// 变更类型(1=创建 2=修改 3=状态变更 4=分配 5=完成 6=验收 7=关闭 8=取消)
        /// </summary>
        public int ChangeType { get; set; }

        /// <summary>
        /// 变更日期
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变更用户
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
        /// 变更字段
        /// </summary>
        public string? ChangeField { get; set; }

        /// <summary>
        /// 变更前字段值
        /// </summary>
        public string? BeforeFieldValue { get; set; }

        /// <summary>
        /// 变更后字段值
        /// </summary>
        public string? AfterFieldValue { get; set; }
    }

    /// <summary>
    /// 生产工单变更记录查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderChangeLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderChangeLogQueryDto()
        {
            PlantCode = string.Empty;
            ProdOrderType = string.Empty;
            ProdOrderCode = string.Empty;
            MaterialCode = string.Empty;
            WorkCenter = string.Empty;
            ProdLine = string.Empty;
            ProdBatch = string.Empty;
            SerialNo = string.Empty;
            RoutingCode = string.Empty;
            ChangeUser = string.Empty;
            ChangeReason = string.Empty;
            ChangeField = string.Empty;
        }

        /// <summary>
        /// 生产工单ID
        /// </summary>
        public long? ProdOrderId { get; set; }

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
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 变更类型
        /// </summary>
        public int? ChangeType { get; set; }

        /// <summary>
        /// 变更用户
        /// </summary>
        public string? ChangeUser { get; set; }

        /// <summary>
        /// 变更原因
        /// </summary>
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 变更字段
        /// </summary>
        public string? ChangeField { get; set; }

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
    /// 生产工单变更记录创建DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderChangeLogCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProdOrderChangeLogCreateDto()
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
            ChangeUser = string.Empty;
            ChangeReason = string.Empty;
            BeforeValue = string.Empty;
            AfterValue = string.Empty;
            ChangeField = string.Empty;
            BeforeFieldValue = string.Empty;
            AfterFieldValue = string.Empty;
        }

        /// <summary>
        /// 生产工单ID
        /// </summary>
        public long ProdOrderId { get; set; }

        // 业务字段 - 按照TaktProdOrderChangeLog.cs的顺序
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
        /// 生产工单优先级
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
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        // 变更记录特有字段
        /// <summary>
        /// 变更类型(1=创建 2=修改 3=状态变更 4=分配 5=完成 6=验收 7=关闭 8=取消)
        /// </summary>
        public int ChangeType { get; set; }

        /// <summary>
        /// 变更日期
        /// </summary>
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变更用户
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
        /// 变更字段
        /// </summary>
        public string? ChangeField { get; set; }

        /// <summary>
        /// 变更前字段值
        /// </summary>
        public string? BeforeFieldValue { get; set; }

        /// <summary>
        /// 变更后字段值
        /// </summary>
        public string? AfterFieldValue { get; set; }
    }

    /// <summary>
    /// 生产工单变更记录更新DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProdOrderChangeLogUpdateDto : TaktProdOrderChangeLogCreateDto
    {
        /// <summary>
        /// 变更记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long ChangeLogId { get; set; }
    }
}


