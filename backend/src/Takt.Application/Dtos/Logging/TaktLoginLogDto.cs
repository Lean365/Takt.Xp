//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginLogDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 登录日志数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;
using Takt.Domain.Entities.Logging;

namespace Takt.Application.Dtos.Logging
{
    /// <summary>
    /// 登录日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktLoginLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLoginLogDto()
        {
            UserName = string.Empty;
            LoginIp = string.Empty;
            LoginLocation = string.Empty;
            UserAgent = string.Empty;
            LoginMessage = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long LoginLogId { get; set; }

        #region 基础信息

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        #endregion

        #region 登录信息

        /// <summary>
        /// 登录类型
        /// </summary>
        public TaktLoginType LoginType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public TaktLoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        public int LoginSource { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        #endregion

        #region 位置信息

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 登录位置
        /// </summary>
        public string LoginLocation { get; set; }

        #endregion

        #region 设备和环境信息

        /// <summary>
        /// 设备ID
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// 设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        public string? DeviceType { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        #endregion

        #region 其他信息

        /// <summary>
        /// 消息
        /// </summary>
        public string? LoginMessage { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 登录设备日志
        /// </summary>
        public TaktDeviceLogDto? DeviceLog { get; set; }

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
    /// 登录日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktLoginLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLoginLogQueryDto()
        {
            UserName = string.Empty;
            DeviceType = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public TaktLoginStatus? LoginStatus { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public TaktLoginType? LoginType { get; set; }

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
    /// 登录日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktLoginLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLoginLogExportDto()
        {
            UserName = string.Empty;
            LoginIp = string.Empty;
            LoginLocation = string.Empty;
            UserAgent = string.Empty;
            LoginMessage = string.Empty;
            CreateTime = DateTime.Now;
            DeviceName = string.Empty;
            OsVersion = string.Empty;
            BrowserVersion = string.Empty;
            LoginType = string.Empty;
            LoginStatus = string.Empty;
            DeviceType = string.Empty;
            OsType = string.Empty;
            BrowserType = string.Empty;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIp { get; set; }

        /// <summary>
        /// 登录位置
        /// </summary>
        public string LoginLocation { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// 登录类型
        /// </summary>
        public string LoginType { get; set; }

        /// <summary>
        /// 登录状态
        /// </summary>
        public string LoginStatus { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public string DeviceType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 操作系统类型
        /// </summary>
        public string OsType { get; set; }

        /// <summary>
        /// 操作系统版本
        /// </summary>
        public string OsVersion { get; set; }

        /// <summary>
        /// 浏览器类型
        /// </summary>
        public string BrowserType { get; set; }

        /// <summary>
        /// 浏览器版本
        /// </summary>
        public string BrowserVersion { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string? LoginMessage { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}




