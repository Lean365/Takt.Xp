#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginLogEntity.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-17 17:00
// 版本号 : V.0.0.1
// 描述    : 登录日志实体
//===================================================================

using Takt.Shared.Enums;
using Takt.Domain.Entities.Logging;

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// 登录日志实体
    /// </summary>
    [SugarTable("Takt_audit_login_log", "登录日志表")]
    public class TaktLoginLog : TaktBaseEntity
    {
        #region 基础信息

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserName { get; set; } = string.Empty;

        #endregion

        #region 登录信息

        /// <summary>
        /// 登录类型（0:其他，1:用户密码登录，2:短信登录，3:邮件登录，4:微信登录，5:QQ登录，6:钉钉登录，7:企业微信登录，8:飞书登录，9:支付宝登录，等等）
        /// </summary>
        [SugarColumn(ColumnName = "login_type", ColumnDescription = "登录类型", ColumnDataType = "int", IsNullable = false)]
        public TaktLoginType LoginType { get; set; }

        /// <summary>
        /// 登录状态（0:成功，1:失败）
        /// </summary>
        [SugarColumn(ColumnName = "login_status", ColumnDescription = "登录状态", ColumnDataType = "int", IsNullable = false)]
        public TaktLoginStatus LoginStatus { get; set; }

        /// <summary>
        /// 登录来源
        /// </summary>
        [SugarColumn(ColumnName = "login_source", ColumnDescription = "登录来源", ColumnDataType = "int", IsNullable = false)]
        public int LoginSource { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        [SugarColumn(ColumnName = "login_time", ColumnDescription = "登录时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime LoginTime { get; set; } = DateTime.Now;

        #endregion

        #region 位置信息

        /// <summary>
        /// 登录IP
        /// </summary>
        [SugarColumn(ColumnName = "login_ip", ColumnDescription = "登录IP", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LoginIp { get; set; } = string.Empty;

        /// <summary>
        /// 登录位置
        /// </summary>
        [SugarColumn(ColumnName = "login_location", ColumnDescription = "登录位置", Length = 150, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string LoginLocation { get; set; } = string.Empty;

        #endregion

        #region 设备和环境信息

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "device_id", ColumnDescription = "设备ID", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceId { get; set; }

        /// <summary>
        /// 设备类型（PC、Android、iOS、MacOS、Linux、其他）
        /// </summary>
        [SugarColumn(ColumnName = "device_type", ColumnDescription = "设备类型", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DeviceType { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = -1, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string UserAgent { get; set; } = string.Empty;

        #endregion

        #region 其他信息

        /// <summary>
        /// 消息
        /// </summary>
        [SugarColumn(ColumnName = "login_message", ColumnDescription = "消息", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LoginMessage { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        /// 登录设备日志
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DeviceId))]
        public virtual TaktDeviceLog? DeviceLog { get; set; }

        #endregion
    }
}





