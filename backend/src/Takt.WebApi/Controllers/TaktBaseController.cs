//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktBaseController.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 通用基础控制器
//===================================================================

using Microsoft.AspNetCore.Mvc;
using Takt.Shared.Models;
using Takt.Domain.IServices.Extensions;

namespace Takt.WebApi.Controllers
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public abstract class TaktBaseController : ControllerBase
    {
        /// <summary>
        /// 本地化服务
        /// </summary>
        protected readonly ITaktLocalizationService _localization;

        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 当前用户服务
        /// </summary>
        protected readonly ITaktCurrentUser _currentUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        protected TaktBaseController(ITaktLogger logger, ITaktCurrentUser currentUser,  ITaktLocalizationService localization)
        {
            _logger = logger;
            _currentUser = currentUser;
            _localization = localization;
        }

        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="message">消息</param>
        /// <returns>操作结果</returns>
        protected IActionResult Success(object? data = null, string message = "Common.OperationSuccess")
        {
            return new JsonResult(TaktApiResult.Success(data, _localization.L(message)));
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="code">错误代码</param>
        /// <returns>操作结果</returns>
        protected IActionResult Error(string message = "Common.OperationFailed", int code = 400)
        {
            return new JsonResult(TaktApiResult.Error(_localization.L(message), code));
        }
    }
} 




