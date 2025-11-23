#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSalesOrderChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 销售订单变更记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售订单变更记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_order_change_log", "销售订单变更记录表")]
    [SugarIndex("ix_sales_order_id", nameof(SalesOrderId), OrderByType.Asc, false)]
    [SugarIndex("ix_sales_order_code", nameof(SalesOrderCode), OrderByType.Asc, false)]
    [SugarIndex("ix_change_date", nameof(ChangeDate), OrderByType.Desc, false)]
    public class TaktSalesOrderChangeLog : TaktBaseEntity
    {
        /// <summary>销售订单ID</summary>
        [SugarColumn(ColumnName = "sales_order_id", ColumnDescription = "销售订单ID", ColumnDataType = "bigint", IsNullable = false)]
        public long SalesOrderId { get; set; }

        /// <summary>销售订单编号</summary>
        [SugarColumn(ColumnName = "sales_order_code", ColumnDescription = "销售订单编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string SalesOrderCode { get; set; } = string.Empty;

        /// <summary>变更类型(1=创建 2=修改 3=状态变更 4=审批 5=取消 6=删除)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>变更前状态</summary>
        [SugarColumn(ColumnName = "before_status", ColumnDescription = "变更前状态", ColumnDataType = "int", IsNullable = true)]
        public int? BeforeStatus { get; set; }

        /// <summary>变更后状态</summary>
        [SugarColumn(ColumnName = "after_status", ColumnDescription = "变更后状态", ColumnDataType = "int", IsNullable = true)]
        public int? AfterStatus { get; set; }

        /// <summary>变更字段名</summary>
        [SugarColumn(ColumnName = "changed_field", ColumnDescription = "变更字段名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangedField { get; set; }

        /// <summary>变更前值</summary>
        [SugarColumn(ColumnName = "before_value", ColumnDescription = "变更前值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeValue { get; set; }

        /// <summary>变更后值</summary>
        [SugarColumn(ColumnName = "after_value", ColumnDescription = "变更后值", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterValue { get; set; }

        /// <summary>变更日期</summary>
        [SugarColumn(ColumnName = "change_date", ColumnDescription = "变更日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeDate { get; set; } = DateTime.Now;

        /// <summary>变更人</summary>
        [SugarColumn(ColumnName = "changed_by", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>变更原因</summary>
        [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeReason { get; set; }

        /// <summary>变更备注</summary>
        [SugarColumn(ColumnName = "change_remark", ColumnDescription = "变更备注", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeRemark { get; set; }

        /// <summary>IP地址</summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IpAddress { get; set; }

        /// <summary>用户代理</summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UserAgent { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 销售订单主表
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(SalesOrderId))]
        public TaktSalesOrder? SalesOrder { get; set; }
    }
} 



