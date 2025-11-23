//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeviceLogDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:30
// 版本号 : V0.0.1
// 描述   : 设备日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Logging
{
    /// <summary>
    /// 设备日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktDeviceLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDeviceLogDto()
        {
            DeviceId = string.Empty;
            DeviceToken = string.Empty;
            ProviderKey = string.Empty;
            ProviderDisplayName = string.Empty;
            TimeZone = string.Empty;
            Language = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long DeviceLogId { get; set; }

        #region 基础信息

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        public string DeviceId { get; set; }

        #endregion

        #region 设备信息

        /// <summary>
        /// 设备令牌
        /// </summary>
        public string? DeviceToken { get; set; }

        #endregion

        #region 登录信息

        /// <summary>
        /// 登录类型（0普通 1短信 2邮箱 3微信 4QQ 5GitHub）
        /// </summary>
        public int LoginType { get; set; }

        /// <summary>
        /// 登录来源（0Web 1App 2小程序 3其他）
        /// </summary>
        public int LoginSource { get; set; }

        /// <summary>
        /// 登录提供者（0系统 1微信 2钉钉 3企业微信）
        /// </summary>
        public int LoginProvider { get; set; }

        /// <summary>
        /// 提供者Key
        /// </summary>
        public string ProviderKey { get; set; }

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        public string ProviderDisplayName { get; set; }

        #endregion

        #region 环境信息

        /// <summary>
        /// 网络类型（WIFI、移动数据、电信数据、联通数据、其他）
        /// </summary>
        public string NetworkType { get; set; } = "WIFI";

        /// <summary>
        /// 时区
        /// </summary>
        public string TimeZone { get; set; } = "Asia/Shanghai";

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; } = "zh-CN";

        /// <summary>
        /// 是否VPN(否、是)
        /// </summary>
        public string IsVpn { get; set; } = "否";

        /// <summary>
        /// 是否代理（非代理、代理）
        /// </summary>
        public string IsProxy { get; set; } = "非代理";

        #endregion

        #region 首次登录信息

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 首次登录IP
        /// </summary>
        public string? FirstLoginIp { get; set; }

        /// <summary>
        /// 首次登录地点
        /// </summary>
        public string? FirstLoginLocation { get; set; }

        /// <summary>
        /// 首次登录设备ID
        /// </summary>
        public string? FirstLoginDeviceId { get; set; }

        /// <summary>
        /// 首次登录设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        public string? FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 首次登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他）
        /// </summary>
        public string? FirstLoginBrowser { get; set; }

        /// <summary>
        /// 首次登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        public string? FirstLoginOs { get; set; }

        #endregion

        #region 最后登录信息

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地点
        /// </summary>
        public string? LastLoginLocation { get; set; }

        /// <summary>
        /// 最后登录设备ID
        /// </summary>
        public string? LastLoginDeviceId { get; set; }

        /// <summary>
        /// 最后登录设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        public string? LastLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录浏览器类型（Chrome、Firefox、Edge、Safari、IE、其他）
        /// </summary>
        public string? LastLoginBrowser { get; set; }

        /// <summary>
        /// 最后登录操作系统类型（Windows、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        public string? LastLoginOs { get; set; }

        #endregion

        #region 在线状态信息

        /// <summary>
        /// 最后在线时间
        /// </summary>
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段(JSON格式,例如:["09:00-12:00","14:00-18:00"])
        /// </summary>
        public string? TodayOnlinePeriods { get; set; }

        #endregion

        #region 统计信息

        /// <summary>
        /// 总登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 连续登录天数
        /// </summary>
        public int ContinuousLoginDays { get; set; }

        #endregion

        #region 状态信息

        /// <summary>
        /// 设备状态（0正常 1停用）
        /// </summary>
        public int DeviceStatus { get; set; }

        #endregion

        #region 基础字段

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

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
        /// 是否删除（0未删除 1已删除）
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

        #endregion
    }

    /// <summary>
    /// 设备日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktDeviceLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDeviceLogQueryDto()
        {
            FirstLoginDeviceType = string.Empty;
            LastLoginDeviceType = string.Empty;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// 首次登录设备类型
        /// </summary>
        public string FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录设备类型
        /// </summary>
        public string LastLoginDeviceType { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public int? LoginType { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public int? LoginSource { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? DeviceStatus { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 设备日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktDeviceLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDeviceLogExportDto()
        {
            DeviceId = string.Empty;
            DeviceToken = string.Empty;
            LoginType = string.Empty;
            LoginSource = string.Empty;
            LoginProvider = string.Empty;
            ProviderDisplayName = string.Empty;
            NetworkType = string.Empty;
            TimeZone = string.Empty;
            Language = string.Empty;
            IsVpn = string.Empty;
            IsProxy = string.Empty;
            FirstLoginIp = string.Empty;
            FirstLoginLocation = string.Empty;
            LastLoginIp = string.Empty;
            LastLoginLocation = string.Empty;
            TodayOnlinePeriods = string.Empty;
            DeviceStatus = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 设备标识
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 首次登录设备类型
        /// </summary>
        public string FirstLoginDeviceType { get; set; }

        /// <summary>
        /// 最后登录设备类型
        /// </summary>
        public string LastLoginDeviceType { get; set; }

        /// <summary>
        /// 设备令牌
        /// </summary>
        public string DeviceToken { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public string LoginSource { get; set; }

        /// <summary>
        /// 登录提供者
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 提供者显示名称
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// 网络类型
        /// </summary>
        public string NetworkType { get; set; }

        /// <summary>
        /// 时区
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 是否VPN
        /// </summary>
        public string IsVpn { get; set; }

        /// <summary>
        /// 是否代理
        /// </summary>
        public string IsProxy { get; set; }

        /// <summary>
        /// 首次登录时间
        /// </summary>
        public DateTime? FirstLoginTime { get; set; }

        /// <summary>
        /// 首次登录IP
        /// </summary>
        public string FirstLoginIp { get; set; }

        /// <summary>
        /// 首次登录地点
        /// </summary>
        public string FirstLoginLocation { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录地点
        /// </summary>
        public string LastLoginLocation { get; set; }

        /// <summary>
        /// 最后在线时间
        /// </summary>
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 最后离线时间
        /// </summary>
        public DateTime? LastOfflineTime { get; set; }

        /// <summary>
        /// 今日在线时段
        /// </summary>
        public string TodayOnlinePeriods { get; set; }

        /// <summary>
        /// 总登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 连续登录天数
        /// </summary>
        public int ContinuousLoginDays { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string DeviceStatus { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}





