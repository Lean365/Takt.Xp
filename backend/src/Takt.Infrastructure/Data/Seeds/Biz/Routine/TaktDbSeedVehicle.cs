//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedVehicle.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 车辆管理种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.Vehicle;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 车辆管理种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供车辆管理的默认种子数据
/// 2. 包含一条示例车辆记录、车辆预约记录和驾驶员记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedVehicle
{
    /// <summary>
    /// 获取默认车辆数据
    /// </summary>
    /// <returns>车辆数据列表</returns>
    public List<TaktVehicle> GetDefaultVehicles()
    {
        return new List<TaktVehicle>
        {
            new TaktVehicle
            {
                PlateNumber = "京A12345",
                VehicleType = 0, // 0-轿车
                Status = 0, // 0-空闲
                Brand = "丰田",
                Model = "凯美瑞",
                Color = "白色",
                SeatCount = 5,
                PurchaseDate = DateTime.Parse("2023-01-15"),
                PurchasePrice = 250000.00m,
                CurrentMileage = 15000.00m,
                InsuranceExpiryDate = DateTime.Parse("2024-12-31"),
                InspectionExpiryDate = DateTime.Parse("2024-06-30"),
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认车辆预约数据
    /// </summary>
    /// <returns>车辆预约数据列表</returns>
    public List<TaktVehicleBooking> GetDefaultVehicleBookings()
    {
        return new List<TaktVehicleBooking>
        {
            new TaktVehicleBooking
            {
                VehicleId = 1, // 关联到第一个车辆
                UserId = 1958104363528491008, // admin用户ID
                BookingTitle = "公务出差用车",
                BookingContent = "前往客户公司进行商务洽谈，需要用车2天",
                StartTime = DateTime.Now.AddDays(1).AddHours(9), // 明天上午9点
                EndTime = DateTime.Now.AddDays(2).AddHours(18), // 后天下午6点
                Status = 1, // 1-已批准
                BookingType = 0, // 0-公务用车
                Purpose = "商务洽谈",
                Destination = "北京市朝阳区客户公司",
                EstimatedMileage = 120.00m,
                PassengerCount = 3,
                Passengers = "张三,李四,王五",
                ContactPerson = "张三",
                ContactPhone = "13800138000",
                NeedDriver = 1, // 1-需要司机
                DriverId = 1, // 司机ID (关联到第一个驾驶员)
                ApproverId = 1958104363528491008, // 审批人ID (admin用户)
                ApprovalTime = DateTime.Now,
                ApprovalRemarks = "同意用车申请",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }

    /// <summary>
    /// 获取默认驾驶员数据
    /// </summary>
    /// <returns>驾驶员数据列表</returns>
    public List<TaktDriver> GetDefaultDrivers()
    {
        return new List<TaktDriver>
        {
            new TaktDriver
            {
                EmployeeId = 1958104363528491008, // admin用户ID
                Status = 0, // 0-在职
                LicenseNo = "110101199001011234",
                LicenseType = 5, // 5-C1
                LicenseStatus = 0, // 0-正常
                LicensePoints = 0, // 驾驶证扣分
                DrivingYears = 8, // 驾龄8年
                DrivingExperience = 2, // 2-熟练
                DrivableVehicles = "小型汽车、小型自动挡汽车、低速载货汽车、三轮汽车",
                IsFullTime = 1, // 1-专职司机
                WorkArea = "北京市区及周边",
                WorkSchedule = 0, // 0-白班
                Remarks = "经验丰富的专职司机，服务态度良好",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}


