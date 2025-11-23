//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktVehicleDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 用车数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Vehicle
{
    /// <summary>
    /// 用车基础DTO
    /// </summary>
    public class TaktVehicleDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long VehicleId { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        public DateTime? InspectionExpiryDate { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 用车查询DTO
    /// </summary>
    public class TaktVehicleQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleQueryDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string? PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int? VehicleType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }
    }

    /// <summary>
    /// 用车创建DTO
    /// </summary>
    public class TaktVehicleCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleCreateDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        public DateTime? InspectionExpiryDate { get; set; }
    }

    /// <summary>
    /// 用车更新DTO
    /// </summary>
    public class TaktVehicleUpdateDto : TaktVehicleCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long VehicleId { get; set; }
    }

    /// <summary>
    /// 用车删除DTO
    /// </summary>
    public class TaktVehicleDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long VehicleId { get; set; }
    }

    /// <summary>
    /// 用车批量删除DTO
    /// </summary>
    public class TaktVehicleBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> VehicleIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 用车导入DTO
    /// </summary>
    public class TaktVehicleImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleImportDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        public DateTime? InspectionExpiryDate { get; set; }
    }

    /// <summary>
    /// 用车导出DTO
    /// </summary>
    public class TaktVehicleExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleExportDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        public DateTime? InspectionExpiryDate { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 用车导入模板DTO
    /// </summary>
    public class TaktVehicleTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktVehicleTemplateDto()
        {
            PlateNumber = string.Empty;
            Brand = string.Empty;
            Model = string.Empty;
            Color = string.Empty;
        }

        /// <summary>
        /// 车牌号
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// 车辆类型(0=轿车 1=SUV 2=商务车 3=面包车)
        /// </summary>
        public int VehicleType { get; set; }

        /// <summary>
        /// 车辆状态(0=空闲 1=使用中 2=维修中 3=已报废)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string? Brand { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string? Model { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatCount { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 购买价格
        /// </summary>
        public decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 当前里程数
        /// </summary>
        public decimal CurrentMileage { get; set; }

        /// <summary>
        /// 保险到期日期
        /// </summary>
        public DateTime? InsuranceExpiryDate { get; set; }

        /// <summary>
        /// 年检到期日期
        /// </summary>
        public DateTime? InspectionExpiryDate { get; set; }
    }
} 


