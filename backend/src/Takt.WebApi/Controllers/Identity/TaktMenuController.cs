//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMenuController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;

namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "菜单")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class TaktMenuController : TaktBaseController
    {
        private readonly ITaktMenuService _menuService;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="menuService">菜单服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">用户上下文</param>
        /// <param name="localization">本地化服务</param>
        public TaktMenuController(
            ITaktMenuService menuService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _menuService = menuService;

        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>菜单分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("identity:menu:query")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktMenuQueryDto query)
        {
            var result = await _menuService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>菜单详情</returns>
        [HttpGet("{menuId}")]
        [TaktPerm("identity:menu:query")]
        public async Task<IActionResult> GetByIdAsync(long menuId)
        {
            var result = await _menuService.GetByIdAsync(menuId);
            return Success(result);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>菜单ID</returns>
        [HttpPost]
        [TaktPerm("identity:menu:create")]
        [TaktLog("创建菜单")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktMenuCreateDto input)
        {
            var result = await _menuService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("identity:menu:update")]
        [TaktLog("更新菜单")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktMenuUpdateDto input)
        {
            var result = await _menuService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{menuId}")]
        [TaktPerm("identity:menu:delete")]
        [TaktLog("删除菜单")]
        public async Task<IActionResult> DeleteAsync(long menuId)
        {
            var result = await _menuService.DeleteAsync(menuId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("identity:menu:delete")]
        [TaktLog("批量删除菜单")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] menuIds)
        {
            var result = await _menuService.BatchDeleteAsync(menuIds);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{menuId}/status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [TaktPerm("identity:menu:update")]
        [TaktLog("更新菜单状态")]
        public async Task<IActionResult> UpdateStatusAsync(long menuId, [FromQuery] int status)
        {
            var input = new TaktMenuStatusDto
            {
                MenuId = menuId,
                MenuStatus = status
            };
            var result = await _menuService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新菜单排序
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <param name="orderNum">显示顺序</param>
        /// <returns>是否成功</returns>
        [HttpPut("{menuId}/order")]
        [TaktPerm("identity:menu:update")]
        [TaktLog("更新菜单排序")]
        public async Task<IActionResult> UpdateOrderAsync(long menuId, [FromQuery] int orderNum)
        {
            var input = new TaktMenuOrderDto
            {
                MenuId = menuId,
                OrderNum = orderNum
            };
            var result = await _menuService.UpdateOrderAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回树形菜单列表</returns>
        [HttpGet("tree")]
        [TaktPerm("identity:menu:query")]
        [ProducesResponseType(typeof(TaktApiResult<List<TaktMenuDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] TaktMenuQueryDto query)
        {
            var menus = await _menuService.GetTreeAsync(query);
            return Success(menus);
        }

        /// <summary>
        /// 获取当前用户的菜单树
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>当前用户的菜单树</returns>
        [HttpGet("current-user/{userId}")]
        public async Task<IActionResult> GetCurrentUserMenusAsync(long userId)
        {
            var result = await _menuService.GetCurrentUserMenusAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取菜单选项列表
        /// </summary>
        /// <returns>菜单选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("identity:menu:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _menuService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取树形菜单选项列表
        /// </summary>
        /// <returns>树形菜单选项列表</returns>
        [HttpGet("tree-options")]
        [TaktPerm("identity:menu:query")]
        public async Task<IActionResult> GetTreeOptionsAsync()
        {
            var result = await _menuService.GetTreeOptionsAsync();
            return Success(result);
        }
        /// <summary>
        /// 导入菜单数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("identity:menu:import")]
        [TaktLog("导入菜单数据")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Menu")
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _menuService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Menu.Import.Success"));
        }

        /// <summary>
        /// 导出菜单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("identity:menu:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktMenuQueryDto query, [FromQuery] string sheetName = "Menu")
        {
            var result = await _menuService.ExportAsync(query, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入模板Excel文件</returns>
        [HttpGet("template")]
        [TaktPerm("identity:menu:query")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Menu")
        {
            var result = await _menuService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

    }
}



