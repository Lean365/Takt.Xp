#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceOrderChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 服务工单变更记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务工单变更记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录服务工单的所有变更历史，包括状态变更、内容修改等
    /// </remarks>
    [SugarTable("Takt_logistics_service_order_change_log", "服务工单变更记录表")]
    [SugarIndex("ix_service_order_change_log", nameof(ServiceOrderId), OrderByType.Asc, nameof(ChangeTime), OrderByType.Desc, false)]
    [SugarIndex("ix_change_type", nameof(ChangeType), OrderByType.Asc, false)]
    public class TaktServiceOrderChangeLog : TaktBaseEntity
    {
        /// <summary>服务工单ID</summary>
        [SugarColumn(ColumnName = "service_order_id", ColumnDescription = "服务工单ID", ColumnDataType = "bigint", IsNullable = false)]
        public long ServiceOrderId { get; set; }

        /// <summary>变更类型(1=创建 2=修改 3=状态变更 4=分配 5=完成 6=验收 7=关闭 8=取消)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>变更前状态</summary>
        [SugarColumn(ColumnName = "before_status", ColumnDescription = "变更前状态", ColumnDataType = "int", IsNullable = true)]
        public int? BeforeStatus { get; set; }

        /// <summary>变更后状态</summary>
        [SugarColumn(ColumnName = "after_status", ColumnDescription = "变更后状态", ColumnDataType = "int", IsNullable = true)]
        public int? AfterStatus { get; set; }

        /// <summary>变更字段</summary>
        [SugarColumn(ColumnName = "change_field", ColumnDescription = "变更字段", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeField { get; set; }

        /// <summary>变更前值</summary>
        [SugarColumn(ColumnName = "before_value", ColumnDescription = "变更前值", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeValue { get; set; }

        /// <summary>变更后值</summary>
        [SugarColumn(ColumnName = "after_value", ColumnDescription = "变更后值", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterValue { get; set; }

        /// <summary>变更描述</summary>
        [SugarColumn(ColumnName = "change_description", ColumnDescription = "变更描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeDescription { get; set; }

        /// <summary>变更人</summary>
        [SugarColumn(ColumnName = "change_user", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangeUser { get; set; } = string.Empty;

        /// <summary>变更时间</summary>
        [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeTime { get; set; } = DateTime.Now;

        /// <summary>变更IP</summary>
        [SugarColumn(ColumnName = "change_ip", ColumnDescription = "变更IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeIp { get; set; }

        /// <summary>变更备注</summary>
        [SugarColumn(ColumnName = "change_remark", ColumnDescription = "变更备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 



