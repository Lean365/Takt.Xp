//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedStandardTime.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 标准工时数据提供类
//===================================================================

using Takt.Domain.Entities.Logistics.Manufacturing;

namespace Takt.Infrastructure.Data.Seeds.Biz.Logistics;

/// <summary>
/// 标准工时数据提供类
/// </summary>
public class TaktDbSeedStandardTime
{
    /// <summary>
    /// 获取默认标准工时数据
    /// </summary>
    /// <returns>标准工时数据列表</returns>
    public List<TaktStandardTime> GetDefaultStandardTimes()
    {
        return new List<TaktStandardTime>
        {
            new TaktStandardTime { PlantCode = "C100", MaterialCode = "1921062000", WorkCenter = "ZBSZ", OperationDesc = "制一课组立班BS", StandardMinutes = 119.49M, TimeUnit = "MIN", StandardShorts = 0, PointsUnit = "SHORT", PointsToMinutesRate = 0.1m, ConvertedMinutes = 119.49M, EffectiveDate = new DateTime(2025, 8, 1), ExpiryDate = new DateTime(9999, 12, 31), Approver = "admin", ApprovalDate = new DateTime(2025, 8, 1), Status = 0, Remark = "标准组立工时" },
            new TaktStandardTime { PlantCode = "C100", MaterialCode = "1921062000", WorkCenter = "ZBSZ", OperationDesc = "制一课组立班BS", StandardMinutes = 119.49M, TimeUnit = "MIN", StandardShorts = 0, PointsUnit = "SHORT", PointsToMinutesRate = 0.1m, ConvertedMinutes = 119.49M, EffectiveDate = new DateTime(2025, 8, 1), ExpiryDate = new DateTime(9999, 12, 31), Approver = "admin", ApprovalDate = new DateTime(2025, 8, 1), Status = 0, Remark = "标准组立工时" },
            new TaktStandardTime { PlantCode = "C100", MaterialCode = "1921062001", WorkCenter = "ZBSZ", OperationDesc = "制一课组立班BS", StandardMinutes = 119.49M, TimeUnit = "MIN", StandardShorts = 0, PointsUnit = "SHORT", PointsToMinutesRate = 0.1m, ConvertedMinutes = 119.49M, EffectiveDate = new DateTime(2025, 8, 1), ExpiryDate = new DateTime(9999, 12, 31), Approver = "admin", ApprovalDate = new DateTime(2025, 8, 1), Status = 0, Remark = "标准组立工时" },
            new TaktStandardTime { PlantCode = "C100", MaterialCode = "1921062001", WorkCenter = "ZBSZ", OperationDesc = "制一课组立班BS", StandardMinutes = 119.49M, TimeUnit = "MIN", StandardShorts = 0, PointsUnit = "SHORT", PointsToMinutesRate = 0.1m, ConvertedMinutes = 119.49M, EffectiveDate = new DateTime(2025, 8, 1), ExpiryDate = new DateTime(9999, 12, 31), Approver = "admin", ApprovalDate = new DateTime(2025, 8, 1), Status = 0, Remark = "标准组立工时" },
            new TaktStandardTime { PlantCode = "C100", MaterialCode = "1921062020", WorkCenter = "ZBSZ", OperationDesc = "制一课组立班BS", StandardMinutes = 119.49M, TimeUnit = "MIN", StandardShorts = 0, PointsUnit = "SHORT", PointsToMinutesRate = 0.1m, ConvertedMinutes = 119.49M, EffectiveDate = new DateTime(2025, 8, 1), ExpiryDate = new DateTime(9999, 12, 31), Approver = "admin", ApprovalDate = new DateTime(2025, 8, 1), Status = 0, Remark = "标准组立工时" }
        };
    }
}


