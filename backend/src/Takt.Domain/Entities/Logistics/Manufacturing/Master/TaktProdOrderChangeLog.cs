#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProdOrderChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 生产工单变更记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单变更记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录生产工单的所有变更历史，包括状态变更、内容修改等
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_master_prodorder_changelog", "生产工单变更记录表")]
    [SugarIndex("ix_prod_order_id", nameof(ProdOrderId), OrderByType.Asc, false)]
    [SugarIndex("ix_change_date", nameof(ChangeDate), OrderByType.Desc, false)]
    [SugarIndex("ix_change_type", nameof(ChangeType), OrderByType.Asc, false)]
    public class TaktProdOrderChangeLog : TaktBaseEntity
    {
        /// <summary>
        /// 生产工单ID
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_id", ColumnDescription = "生产工单ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ProdOrderId { get; set; }

        // 业务字段 - 按照TaktProdOrder.cs的顺序
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单类型
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_type", ColumnDescription = "生产工单类型", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderType { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单号
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProdOrderCode { get; set; } = string.Empty;

        /// <summary>
        /// 物料编码
        /// </summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>
        /// 生产工单数量
        /// </summary>
        [SugarColumn(ColumnName = "prod_order_qty", ColumnDescription = "生产工单数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProdOrderQty { get; set; } = 0;

        /// <summary>
        /// 已生产数量
        /// </summary>
        [SugarColumn(ColumnName = "produced_qty", ColumnDescription = "已生产数量", ColumnDataType = "decimal(18,3)", IsNullable = false, DefaultValue = "0")]
        public decimal ProducedQty { get; set; } = 0;

        /// <summary>
        /// 计量单位
        /// </summary>
        [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string UnitOfMeasure { get; set; } = string.Empty;

        /// <summary>
        /// 实际开始日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际完成日期
        /// </summary>
        [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int Priority { get; set; } = 2;

        /// <summary>
        /// 工作中心
        /// </summary>
        [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? WorkCenter { get; set; }

        /// <summary>
        /// 生产线
        /// </summary>
        [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdLine { get; set; }

        /// <summary>
        /// 生产批次
        /// </summary>
        [SugarColumn(ColumnName = "prod_batch", ColumnDescription = "生产批次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ProdBatch { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SerialNo { get; set; }

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        [SugarColumn(ColumnName = "routing_code", ColumnDescription = "工艺路线编码", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RoutingCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        // 变更记录特有字段
        /// <summary>
        /// 变更类型(1=创建 2=修改 3=状态变更 4=分配 5=完成 6=验收 7=关闭 8=取消)
        /// </summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>
        /// 变更日期
        /// </summary>
        [SugarColumn(ColumnName = "change_date", ColumnDescription = "变更日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeDate { get; set; }

        /// <summary>
        /// 变更用户
        /// </summary>
        [SugarColumn(ColumnName = "change_user", ColumnDescription = "变更用户", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeUser { get; set; } = string.Empty;

        /// <summary>
        /// 变更原因
        /// </summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>
        /// 变更前值(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "before_value", ColumnDescription = "变更前值", ColumnDataType = "nvarchar(max)", IsNullable = true)]
        public string? BeforeValue { get; set; }

        /// <summary>
        /// 变更后值(JSON格式)
        /// </summary>
        [SugarColumn(ColumnName = "after_value", ColumnDescription = "变更后值", ColumnDataType = "nvarchar(max)", IsNullable = true)]
        public string? AfterValue { get; set; }

        /// <summary>
        /// 变更字段
        /// </summary>
        [SugarColumn(ColumnName = "change_field", ColumnDescription = "变更字段", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeField { get; set; }

        /// <summary>
        /// 变更前字段值
        /// </summary>
        [SugarColumn(ColumnName = "before_field_value", ColumnDescription = "变更前字段值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeFieldValue { get; set; }

        /// <summary>
        /// 变更后字段值
        /// </summary>
        [SugarColumn(ColumnName = "after_field_value", ColumnDescription = "变更后字段值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterFieldValue { get; set; }
    }
}




