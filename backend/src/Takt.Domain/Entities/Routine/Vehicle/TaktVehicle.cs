//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktVehicle.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 用车管理实体
//===================================================================

using Takt.Domain.Entities.Identity;
using SqlSugar;

namespace Takt.Domain.Entities.Routine.Vehicle
{
    /// <summary>
    /// 用车管理实体
    /// </summary>
    [SugarTable("Takt_routine_vehicle", "用车管理")]
    [SugarIndex("index_vehicle_plate", nameof(PlateNumber), OrderByType.Asc)]
    [SugarIndex("index_vehicle_type", nameof(VehicleType), OrderByType.Asc)]
    [SugarIndex("index_vehicle_status", nameof(Status), OrderByType.Asc)]
    public class TaktVehicle : TaktBaseEntity
    {
        /// <summary>
        /// 车牌号
        /// </summary>
        [SugarColumn(ColumnName = "plate_number", ColumnDescription = "车牌号", Length = 20, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string PlateNumber { get; set; } = string.Empty;

        /// <summary>
        /// 车辆类型（0：轿车，1：SUV，2：商务车，3：面包车）
        /// </summary>
        [SugarColumn(ColumnName = "vehicle_type", ColumnDescription = "车辆类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态（0：空闲，1：使用中，2：维修中，3：已报废）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "车辆状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        [SugarColumn(ColumnName = "brand", ColumnDescription = "品牌", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        [SugarColumn(ColumnName = "model", ColumnDescription = "型号", Length = 50, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [SugarColumn(ColumnName = "color", ColumnDescription = "颜色", Length = 20, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        [SugarColumn(ColumnName = "seat_count", ColumnDescription = "座位数", IsNullable = false, DefaultValue = "5", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        [SugarColumn(ColumnName = "purchase_date", ColumnDescription = "购买日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        [SugarColumn(ColumnName = "purchase_price", ColumnDescription = "购买价格", IsNullable = true, ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        [SugarColumn(ColumnName = "current_mileage", ColumnDescription = "当前里程数", IsNullable = false, DefaultValue = "0", ColumnDataType = "decimal(18,2)", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        [SugarColumn(ColumnName = "insurance_expiry_date", ColumnDescription = "保险到期日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        [SugarColumn(ColumnName = "inspection_expiry_date", ColumnDescription = "年检到期日期", IsNullable = true, ColumnDataType = "datetime", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public DateTime? InspectionExpiryDate { get; set; }

    }
}


