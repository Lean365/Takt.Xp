//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedAuditMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 审计日志菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedAuditMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 审计日志菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 审计日志菜单数据初始化类
/// </summary>
public class TaktDbSeedAuditMenu
{
    /// <summary>
    /// 获取审计日志子菜单列表
    /// </summary>
    public static List<TaktMenu> GetAuditThirdMenus(long parentId)
    {
        return new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "操作日志",
                TransKey = "menu.Logging.operlog",
                ParentId = parentId,
                OrderNum = 1,
                Path = "operlog",
                Component = "audit/operlog/index",
                MenuType = 1,
                Perms = "logging:operlog:list",
                Icon = "HistoryOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "登录日志",
                TransKey = "menu.Logging.loginlog",
                ParentId = parentId,
                OrderNum = 2,
                Path = "loginlog",
                Component = "audit/loginlog/index",
                MenuType = 1,
                Perms = "logging:loginlog:list",
                Icon = "LoginOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "设备日志",
                TransKey = "menu.Logging.devicelog",
                ParentId = parentId,
                OrderNum = 3,
                Path = "devicelog",
                Component = "audit/devicelog/index",
                MenuType = 1,
                Perms = "logging:devicelog:list",
                Icon = "MonitorOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "差异日志",
                TransKey = "menu.Logging.difflog",
                ParentId = parentId,
                OrderNum = 4,
                Path = "difflog",
                Component = "audit/difflog/index",
                MenuType = 1,
                Perms = "logging:difflog:list",
                Icon = "DiffOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "异常日志",
                TransKey = "menu.Logging.exceptionlog",
                ParentId = parentId,
                OrderNum = 5,
                Path = "exceptionlog",
                Component = "audit/exceptionlog/index",
                MenuType = 1,
                Perms = "logging:exceptionlog:list",
                Icon = "ExceptionOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
             new TaktMenu
            {
                MenuName = "任务日志",
                TransKey = "menu.Logging.quartzlog",
                ParentId = parentId,
                OrderNum = 6,
                Path = "quartzlog",
                Component = "audit/quartzlog/index",
                MenuType = 1,
                Perms = "logging:quartzlog:list",
                Icon = "ExceptionOutlined",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "服务器监控",
                TransKey = "menu.Logging.server",
                ParentId = parentId,
                OrderNum = 7,
                Path = "server",
                Component = "audit/server/index",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 1,
                Visible = 0,
                MenuStatus = 0,
                Perms = "logging:server:list",
                Icon = "DashboardOutlined",
                Remark = "服务器监控菜单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now
            }
        };
    }
}



