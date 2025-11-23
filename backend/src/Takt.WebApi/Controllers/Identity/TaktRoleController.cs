//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRoleController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;


namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "角色")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class TaktRoleController : TaktBaseController
    {
        private readonly ITaktRoleService _roleService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="roleService">角色服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktRoleController(
            ITaktRoleService roleService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>角色分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("identity:role:query")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktRoleQueryDto query)
        {
            var result = await _roleService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色详情</returns>
        [HttpGet("{roleId}")]
        [TaktPerm("identity:role:query")]
        public async Task<IActionResult> GetByIdAsync(long roleId)
        {
            var result = await _roleService.GetByIdAsync(roleId);
            return Success(result);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        [HttpPost]
        [TaktPerm("identity:role:create")]
        [TaktLog("创建角色")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktRoleCreateDto input)
        {
            var result = await _roleService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("identity:role:update")]
        [TaktLog("更新角色")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktRoleUpdateDto input)
        {
            var result = await _roleService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{roleId}")]
        [TaktPerm("identity:role:delete")]
        [TaktLog("删除角色")]
        public async Task<IActionResult> DeleteAsync(long roleId)
        {
            var result = await _roleService.DeleteAsync(roleId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("identity:role:delete")]
        [TaktLog("批量删除角色")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] roleIds)
        {
            var result = await _roleService.BatchDeleteAsync(roleIds);
            return Success(result);
        }


        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{roleId}/status")]
        [TaktPerm("identity:role:update")]
        [TaktLog("更新角色状态")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long roleId, [FromQuery] int status)
        {
            var input = new TaktRoleStatusDto
            {
                RoleId = roleId,
                RoleStatus = status
            };
            var result = await _roleService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取角色选项列表
        /// </summary>
        /// <returns>角色选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("identity:role:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _roleService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取角色部门列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色部门列表</returns>
        [HttpGet("{roleId}/depts")]
        [TaktPerm("identity:role:query")]
        public async Task<IActionResult> GetRoleDeptsAsync(long roleId)
        {
            var result = await _roleService.GetRoleDeptIdsAsync(roleId);
            return Success(result);
        }

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色菜单列表</returns>
        [HttpGet("{roleId}/menus")]
        [TaktPerm("identity:role:query")]
        public async Task<IActionResult> GetRoleMenusAsync(long roleId)
        {
            var menus = await _roleService.GetRoleMenuIdsAsync(roleId);
            return Success(menus);
        }

        /// <summary>
        /// 分配角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuIds">菜单ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{roleId}/menus")]
        [TaktPerm("identity:role:allocate")]
        [TaktLog("分配角色菜单")]
        public async Task<IActionResult> AssignRoleMenusAsync(long roleId, [FromBody] long[] menuIds)
        {
            var result = await _roleService.AssignRoleMenusAsync(roleId, menuIds);
            return Success(result);
        }

        /// <summary>
        /// 分配角色用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{roleId}/users")]
        [TaktPerm("identity:role:allocate")]
        [TaktLog("分配角色用户")]
        public async Task<IActionResult> AssignRoleUsersAsync(long roleId, [FromBody] long[] userIds)
        {
            var result = await _roleService.AssignRoleUsersAsync(roleId, userIds);
            return Success(result);
        }

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{roleId}/depts")]
        [TaktPerm("identity:role:allocate")]
        [TaktLog("分配角色部门")]
        public async Task<IActionResult> AssignRoleDeptsAsync(long roleId, [FromBody] long[] deptIds)
        {
            var result = await _roleService.AssignRoleDeptsAsync(roleId, deptIds);
            return Success(result);
        }

        /// <summary>
        /// 获取角色导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("identity:role:export")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Role")
        {
            var result = await _roleService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入角色数据")]
        [TaktPerm("identity:role:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Role")
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _roleService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Role.Import.Success"));
        }

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("identity:role:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktRoleQueryDto query, [FromQuery] string sheetName = "Role")
        {
            var result = await _roleService.ExportAsync(query, sheetName);
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

    }
}



