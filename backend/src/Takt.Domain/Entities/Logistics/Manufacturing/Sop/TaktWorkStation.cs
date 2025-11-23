#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkStation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 工作站/工位表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Sop
{
    /// <summary>
    /// 工作站/工位表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 描述: 管理生产线上的工作站信息，支持电子看板配置
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_sop_work_station", "工作站表")]
    [SugarIndex("ix_station_id", nameof(StationId), OrderByType.Asc, true)]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_line_code", nameof(LineCode), OrderByType.Asc, false)]
    public class TaktWorkStation : TaktBaseEntity
    {
        /// <summary>
        /// 工位ID
        /// </summary>
        [SugarColumn(ColumnName = "station_id", ColumnDescription = "工位ID", Length = 15, ColumnDataType = "varchar", IsNullable = false, IsPrimaryKey = true)]
        public string StationId { get; set; } = string.Empty;

        /// <summary>
        /// 工位名称
        /// </summary>
        [SugarColumn(ColumnName = "station_name", ColumnDescription = "工位名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string StationName { get; set; } = string.Empty;

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "varchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 产线代码
        /// </summary>
        [SugarColumn(ColumnName = "line_code", ColumnDescription = "产线代码", Length = 20, ColumnDataType = "varchar", IsNullable = false)]
        public string LineCode { get; set; } = string.Empty;

        /// <summary>
        /// 工位类型(1=装配 2=检测 3=包装 4=其他)
        /// </summary>
        [SugarColumn(ColumnName = "station_type", ColumnDescription = "工位类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int StationType { get; set; } = 1;

        /// <summary>
        /// 电子看板IP地址
        /// </summary>
        [SugarColumn(ColumnName = "display_ip", ColumnDescription = "电子看板IP地址", Length = 15, ColumnDataType = "varchar", IsNullable = true)]
        public string? DisplayIp { get; set; }

        /// <summary>
        /// 看板屏幕尺寸
        /// </summary>
        [SugarColumn(ColumnName = "screen_size", ColumnDescription = "看板屏幕尺寸", Length = 20, ColumnDataType = "varchar", IsNullable = true)]
        public string? ScreenSize { get; set; }

        /// <summary>
        /// 看板分辨率
        /// </summary>
        [SugarColumn(ColumnName = "screen_resolution", ColumnDescription = "看板分辨率", Length = 20, ColumnDataType = "varchar", IsNullable = true)]
        public string? ScreenResolution { get; set; }

        /// <summary>
        /// 工位负责人
        /// </summary>
        [SugarColumn(ColumnName = "station_manager", ColumnDescription = "工位负责人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StationManager { get; set; }

        /// <summary>
        /// 状态(0=草稿 1=发布)
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int Status { get; set; } = 1;

        // ==================== 导航属性 ====================

        /// <summary>
        /// 关联的SOP工序列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaktSopProcess>? SopProcesses { get; set; }
    }
}




