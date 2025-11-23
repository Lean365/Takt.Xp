#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExternalEquipmentAccessory.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 外部设备配件实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 外部设备配件实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_equipment_accessory", "设备配件表")]
    [SugarIndex("ix_accessory_code", nameof(AccessoryCode), OrderByType.Asc, true)]
    [SugarIndex("ix_customer_accessory", nameof(CustomerCode), OrderByType.Asc, nameof(AccessoryCode), OrderByType.Asc, false)]
    [SugarIndex("ix_equipment_id", nameof(EquipmentId), OrderByType.Asc, false)]
    public class TaktEquipmentAccessory : TaktBaseEntity
    {
        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>外部设备ID</summary>
        [SugarColumn(ColumnName = "external_equipment_id", ColumnDescription = "外部设备ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EquipmentId { get; set; }

        /// <summary>配件编号</summary>
        [SugarColumn(ColumnName = "accessory_code", ColumnDescription = "配件编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AccessoryCode { get; set; } = string.Empty;

        /// <summary>配件名称</summary>
        [SugarColumn(ColumnName = "accessory_name", ColumnDescription = "配件名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string AccessoryName { get; set; } = string.Empty;

        /// <summary>配件类型(1=机械配件 2=电气配件 3=液压配件 4=气动配件 5=其他)</summary>
        [SugarColumn(ColumnName = "accessory_type", ColumnDescription = "配件类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AccessoryType { get; set; } = 1;

        /// <summary>规格型号</summary>
        [SugarColumn(ColumnName = "specification", ColumnDescription = "规格型号", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Specification { get; set; }

        /// <summary>制造商</summary>
        [SugarColumn(ColumnName = "manufacturer", ColumnDescription = "制造商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Manufacturer { get; set; }

        /// <summary>供应商</summary>
        [SugarColumn(ColumnName = "supplier", ColumnDescription = "供应商", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Supplier { get; set; }

        /// <summary>数量</summary>
        [SugarColumn(ColumnName = "quantity", ColumnDescription = "数量", ColumnDataType = "decimal(10,2)", IsNullable = false, DefaultValue = "1")]
        public decimal Quantity { get; set; } = 1;

        /// <summary>单位</summary>
        [SugarColumn(ColumnName = "unit", ColumnDescription = "单位", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "PCS")]
        public string Unit { get; set; } = "PCS";

        /// <summary>单价</summary>
        [SugarColumn(ColumnName = "unit_price", ColumnDescription = "单价", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal UnitPrice { get; set; } = 0;

        /// <summary>总价值</summary>
        [SugarColumn(ColumnName = "total_value", ColumnDescription = "总价值", ColumnDataType = "decimal(15,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalValue { get; set; } = 0;

        /// <summary>币种(CNY=人民币 USD=美元 EUR=欧元)</summary>
        [SugarColumn(ColumnName = "currency", ColumnDescription = "币种", Length = 3, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
        public string Currency { get; set; } = "CNY";

        /// <summary>配件状态(0=停用 1=正常 2=损坏 3=丢失 4=维修中)</summary>
        [SugarColumn(ColumnName = "accessory_status", ColumnDescription = "配件状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int AccessoryStatus { get; set; } = 1;

        /// <summary>安装日期</summary>
        [SugarColumn(ColumnName = "installation_date", ColumnDescription = "安装日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? InstallationDate { get; set; }

        /// <summary>更换日期</summary>
        [SugarColumn(ColumnName = "replacement_date", ColumnDescription = "更换日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ReplacementDate { get; set; }

        /// <summary>使用寿命(小时)</summary>
        [SugarColumn(ColumnName = "service_life_hours", ColumnDescription = "使用寿命(小时)", ColumnDataType = "int", IsNullable = true)]
        public int? ServiceLifeHours { get; set; }

        /// <summary>已使用时间(小时)</summary>
        [SugarColumn(ColumnName = "used_hours", ColumnDescription = "已使用时间(小时)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int UsedHours { get; set; } = 0;

        /// <summary>维护周期(天)</summary>
        [SugarColumn(ColumnName = "maintenance_cycle", ColumnDescription = "维护周期(天)", ColumnDataType = "int", IsNullable = true)]
        public int? MaintenanceCycle { get; set; }

        /// <summary>上次维护日期</summary>
        [SugarColumn(ColumnName = "last_maintenance_date", ColumnDescription = "上次维护日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? LastMaintenanceDate { get; set; }

        /// <summary>下次维护日期</summary>
        [SugarColumn(ColumnName = "next_maintenance_date", ColumnDescription = "下次维护日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? NextMaintenanceDate { get; set; }

        /// <summary>技术参数</summary>
        [SugarColumn(ColumnName = "technical_parameters", ColumnDescription = "技术参数", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TechnicalParameters { get; set; }

        /// <summary>配件图片</summary>
        [SugarColumn(ColumnName = "accessory_image", ColumnDescription = "配件图片", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccessoryImage { get; set; }

        /// <summary>配件文档</summary>
        [SugarColumn(ColumnName = "accessory_document", ColumnDescription = "配件文档", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccessoryDocument { get; set; }

         /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

    }
} 



