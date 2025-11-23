#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExternalEquipment.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 外部设备实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 外部设备实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_equipment", "设备表")]
    [SugarIndex("ix_equipment_code", nameof(EquipmentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_customer_equipment", nameof(CustomerCode), OrderByType.Asc, nameof(EquipmentCode), OrderByType.Asc, false)]
    [SugarIndex("ix_equipment_type", nameof(EquipmentType), OrderByType.Asc, false)]
    public class TaktEquipment : TaktBaseEntity
    {
        /// <summary>客户代码</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户代码", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CustomerCode { get; set; } = string.Empty;

        /// <summary>设备编号</summary>
        [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentCode { get; set; } = string.Empty;

        /// <summary>设备名称</summary>
        [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentName { get; set; } = string.Empty;

        /// <summary>设备类型</summary>
        [SugarColumn(ColumnName = "equipment_type", ColumnDescription = "设备类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentType { get; set; } = string.Empty;

        /// <summary>设备状态</summary>
        [SugarColumn(ColumnName = "equipment_status", ColumnDescription = "设备状态", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentStatus { get; set; } = string.Empty;

        /// <summary>设备位置</summary>
        [SugarColumn(ColumnName = "equipment_location", ColumnDescription = "设备位置", Length = 255, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentLocation { get; set; } = string.Empty;

        /// <summary>设备描述</summary>
        [SugarColumn(ColumnName = "equipment_description", ColumnDescription = "设备描述", Length = 255, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EquipmentDescription { get; set; } = string.Empty;

        /// <summary>
        /// 外部设备配件导航属性（一对多）
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktEquipmentAccessory.EquipmentId))]
        public List<TaktEquipmentAccessory>? EquipmentAccessories { get; set; }
    }
} 



