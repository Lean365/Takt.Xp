//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktMeetingRoom.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 会议室管理实体
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Metting
{
    /// <summary>
    /// 会议室管理实体
    /// </summary>
    [SugarTable("Takt_routine_meeting_room", "会议室管理")]
    [SugarIndex("index_room_name", nameof(RoomName), OrderByType.Asc)]
    [SugarIndex("index_room_location", nameof(Location), OrderByType.Asc)]
    [SugarIndex("index_room_status", nameof(Status), OrderByType.Asc)]
    public class TaktMeetingRoom : TaktBaseEntity
    {
        /// <summary>
        /// 会议室名称
        /// </summary>
        [SugarColumn(ColumnName = "room_name", ColumnDescription = "会议室名称", Length = 100, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string RoomName { get; set; } = string.Empty;

        /// <summary>
        /// 会议室编号
        /// </summary>
        [SugarColumn(ColumnName = "room_code", ColumnDescription = "会议室编号", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string RoomCode { get; set; } = string.Empty;

        /// <summary>
        /// 会议室位置
        /// </summary>
        [SugarColumn(ColumnName = "location", ColumnDescription = "会议室位置", Length = 200, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Location { get; set; }

        /// <summary>
        /// 会议室类型（0：小型会议室，1：中型会议室，2：大型会议室，3：培训室，4：视频会议室）
        /// </summary>
        [SugarColumn(ColumnName = "room_type", ColumnDescription = "会议室类型", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int RoomType { get; set; }

        /// <summary>
        /// 容纳人数
        /// </summary>
        [SugarColumn(ColumnName = "capacity", ColumnDescription = "容纳人数", IsNullable = false, DefaultValue = "10", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Capacity { get; set; }

        /// <summary>
        /// 会议室状态（0：正常，1：维护中，2：停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "会议室状态", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int Status { get; set; }

        /// <summary>
        /// 是否支持视频会议（0：否，1：是）
        /// </summary>
        [SugarColumn(ColumnName = "support_video", ColumnDescription = "是否支持视频会议", IsNullable = false, DefaultValue = "0", ColumnDataType = "int", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public int SupportVideo { get; set; }

        /// <summary>
        /// 设备配置
        /// </summary>
        [SugarColumn(ColumnName = "equipment_config", ColumnDescription = "设备配置", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? EquipmentConfig { get; set; }

        /// <summary>
        /// 使用说明
        /// </summary>
        [SugarColumn(ColumnName = "usage_instructions", ColumnDescription = "使用说明", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? UsageInstructions { get; set; }

        /// <summary>
        /// 预订规则
        /// </summary>
        [SugarColumn(ColumnName = "booking_rules", ColumnDescription = "预订规则", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? BookingRules { get; set; }

        /// <summary>
        /// 开放时间
        /// </summary>
        [SugarColumn(ColumnName = "open_time", ColumnDescription = "开放时间", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? OpenTime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        [SugarColumn(ColumnName = "close_time", ColumnDescription = "关闭时间", Length = 100, IsNullable = true, DefaultValue = "", ColumnDataType = "nvarchar", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? CloseTime { get; set; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [SugarColumn(ColumnName = "manager_id", ColumnDescription = "负责人ID", IsNullable = true, ColumnDataType = "bigint", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public long? ManagerId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "remarks", ColumnDescription = "备注", IsNullable = true, DefaultValue = "", ColumnDataType = "text", IsOnlyIgnoreUpdate = false, IsOnlyIgnoreInsert = false)]
        public string? Remarks { get; set; }
    }
} 


