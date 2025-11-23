#nullable enable

using Takt.Shared.Models;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeviceLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 14:30
// 版本号 : V0.0.1
// 描述    : 设备日志实体（合并登录设备日志和登录环境日志）
//===================================================================

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// 设备日志实体
    /// 合并了登录设备日志和登录环境日志的所有字段
    /// </summary>
    [SugarTable("Takt_audit_device_log", "设备日志")]
    [SugarIndex("ix_device_user", nameof(UserId), OrderByType.Asc, nameof(LastLoginDeviceId), OrderByType.Asc, false)]
    public class TaktDeviceLog : TaktBaseEntity
    {
        #region 基础信息

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }



        #endregion

        #region 设备信息

        /// <summary>
        /// 设备令牌
        /// </summary>
        [SugarColumn(ColumnName = "device_token", ColumnDescription = "设备令牌", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceToken { get; set; }

        /// <summary>
        /// 设备信息
        /// </summary>
        [SugarColumn(ColumnName = "device_info", ColumnDescription = "设备信息", Length = -1, ColumnDataType = "nvarchar", IsNullable = true, IsJson = true)]
        public string? DeviceInfo { get; set; }

        #endregion

        #region 登录信息

        /// <summary>
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginType { get; set; } = 0;

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        [SugarColumn(ColumnName = "login_source", ColumnDescription = "登录来源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginSource { get; set; } = 0;

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        [SugarColumn(ColumnName = "login_provider", ColumnDescription = "登录提供者", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginProvider { get; set; } = 0;

        /// <summary>
        /// 提供者Key
        /// </summary>
        [SugarColumn(ColumnName = "provider_key", ColumnDescription = "提供者Key", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ProviderKey { get; set; } = string.Empty;

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        [SugarColumn(ColumnName = "provider_display_name", ColumnDescription = "提供者名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string ProviderDisplayName { get; set; } = string.Empty;

        #endregion

        #region 环境信息

        /// <summary>
        /// 网络类型（WIFI、移动数据、电信数据、联通数据、其他）
        /// </summary>
        [SugarColumn(ColumnName = "network_type", ColumnDescription = "网络类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "WIFI")]
        public string NetworkType { get; set; } = "WIFI";

        /// <summary>
        /// 时区
        /// </summary>
        [SugarColumn(ColumnName = "time_zone", ColumnDescription = "时区", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "Asia/Shanghai")]
        public string TimeZone { get; set; } = "Asia/Shanghai";

        /// <summary>
        /// 语言
        /// </summary>
        [SugarColumn(ColumnName = "language", ColumnDescription = "语言", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "zh-CN")]
        public string Language { get; set; } = "zh-CN";

        /// <summary>
        /// 是否VPN(否、是)
        /// </summary>
        [SugarColumn(ColumnName = "is_vpn", ColumnDescription = "是否VPN", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "否")]
        public string IsVpn { get; set; } = "否";

        /// <summary>
        /// 是否代理（非代理、代理）
        /// </summary>
        [SugarColumn(ColumnName = "is_proxy", ColumnDescription = "是否代理", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "非代理")]
        public string IsProxy { get; set; } = "非代理";

        #endregion

        #region 首次登录信息

        /// <summary>
        /// 首次登录时间
        /// </summary>
        [SugarColumn(ColumnName = "first_login_time", ColumnDescription = "首次登录时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 首次登录IP
        /// </summary>
        [SugarColumn(ColumnName = "first_login_ip", ColumnDescription = "首次登录IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginIp { get; set; }

        /// <summary>
        /// 首次登录地点
        /// </summary>
        [SugarColumn(ColumnName = "first_login_location", ColumnDescription = "首次登录地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginLocation { get; set; }

        /// <summary>
        /// 首次登录设备ID
        /// </summary>
        [SugarColumn(ColumnName = "first_login_device_id", ColumnDescription = "首次登录设备ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginDeviceId { get; set; }

        /// <summary>
        /// 首次登录设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_device_type", ColumnDescription = "首次登录设备类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_browser", ColumnDescription = "首次登录浏览器类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        [SugarColumn(ColumnName = "first_login_os", ColumnDescription = "首次登录操作系统类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FirstLoginOs { get; set; }

        #endregion

        #region 最后登录信息

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [SugarColumn(ColumnName = "last_login_time", ColumnDescription = "最后登录时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [SugarColumn(ColumnName = "last_login_ip", ColumnDescription = "最后登录IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地点
        /// </summary>
        [SugarColumn(ColumnName = "last_login_location", ColumnDescription = "最后登录地点", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginLocation { get; set; }

        /// <summary>
        /// 最后登录设备ID
        /// </summary>
        [SugarColumn(ColumnName = "last_login_device_id", ColumnDescription = "最后登录设备ID", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginDeviceId { get; set; }

        /// <summary>
        /// 最后登录设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_device_type", ColumnDescription = "最后登录设备类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_browser", ColumnDescription = "最后登录浏览器类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        [SugarColumn(ColumnName = "last_login_os", ColumnDescription = "最后登录操作系统类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LastLoginOs { get; set; }

        #endregion

        #region 在线状态信息

        /// <summary>
        /// 最后在线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_online_time", ColumnDescription = "最后在线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_offline_time", ColumnDescription = "最后离线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        [SugarColumn(ColumnName = "today_online_periods", ColumnDescription = "今日在线时段", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TodayOnlinePeriods { get; set; }

        #endregion

        #region 统计信息

        /// <summary>
        /// 总登录次数
        /// </summary>
        [SugarColumn(ColumnName = "login_count", ColumnDescription = "总登录次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int LoginCount { get; set; } = 0;

        /// <summary>
        /// 连续登录天数
        /// </summary>
        [SugarColumn(ColumnName = "continuous_login_days", ColumnDescription = "连续登录天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ContinuousLoginDays { get; set; } = 0;

        #endregion

        #region 状态信息

        /// <summary>
        /// 设备状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "device_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DeviceStatus { get; set; } = 0;

        #endregion
    }
}






