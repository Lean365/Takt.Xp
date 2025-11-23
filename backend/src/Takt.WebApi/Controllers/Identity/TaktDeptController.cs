//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeptController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;

namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 部门控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "部门")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class TaktDeptController : TaktBaseController
    {
        private readonly ITaktDeptService _deptService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deptService">部门服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktDeptController(
            ITaktDeptService deptService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _deptService = deptService;
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>部门分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("identity:dept:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktDeptQueryDto query)
        {
            var result = await _deptService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        /// <returns>返回树形部门列表</returns>
        [HttpGet("tree")]
        [ProducesResponseType(typeof(TaktApiResult<List<TaktDeptDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTreeAsync([FromQuery] TaktDeptQueryDto query)
        {
            var depts = await _deptService.GetTreeAsync(query);
            return Success(depts);
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>部门详情</returns>
        [HttpGet("{deptId}")]
        [TaktPerm("identity:dept:query")]
        public async Task<IActionResult> GetByIdAsync(long deptId)
        {
            var result = await _deptService.GetByIdAsync(deptId);
            return Success(result);
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>部门ID</returns>
        [HttpPost]
        [TaktPerm("identity:dept:create")]
        [TaktLog("创建部门")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktDeptCreateDto input)
        {
            var result = await _deptService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("identity:dept:update")]
        [TaktLog("更新部门")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktDeptUpdateDto input)
        {
            var result = await _deptService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{deptId}")]
        [TaktPerm("identity:dept:delete")]
        [TaktLog("删除部门")]
        public async Task<IActionResult> DeleteAsync(long deptId)
        {
            var result = await _deptService.DeleteAsync(deptId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        /// <param name="deptIds">部门ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("identity:dept:delete")]
        [TaktLog("批量删除部门")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] deptIds)
        {
            var result = await _deptService.BatchDeleteAsync(deptIds);
            return Success(result);
        }

        /// <summary>
        /// 更新部门状态
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{deptId}/status")]
        [TaktPerm("identity:dept:update")]
        [TaktLog("更新部门状态")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long deptId, [FromQuery] int status)
        {
            var result = await _deptService.UpdateStatusAsync(deptId, status);
            return Success(result);
        }

        /// <summary>
        /// 获取部门选项列表
        /// </summary>
        /// <returns>部门选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("identity:dept:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _deptService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取树形部门选项列表
        /// </summary>
        /// <returns>树形部门选项列表</returns>
        [HttpGet("tree-options")]
        [TaktPerm("identity:dept:query")]
        public async Task<IActionResult> GetTreeOptionsAsync()
        {
            var result = await _deptService.GetTreeOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>用户部门列表</returns>
        [HttpGet("{deptId}/users")]
        [TaktPerm("identity:dept:query")]
        public async Task<IActionResult> GetUserDeptsAsync(long deptId)
        {
            var result = await _deptService.GetUserDeptsAsync(deptId);
            return Success(result);
        }

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>是否分配成功</returns>
        [HttpPost("{deptId}/users")]
        [TaktPerm("identity:dept:update")]
        [TaktLog("分配用户部门")]
        public async Task<IActionResult> AssignUserDeptsAsync(long deptId, [FromBody] long[] userIds)
        {
            var result = await _deptService.AssignUserDeptsAsync(deptId, userIds);
            return Success(result);
        }

        /// <summary>
        /// 获取角色部门列表
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="query">查询条件</param>
        /// <returns>角色部门分页列表</returns>
        [HttpGet("{deptId}/roles")]
        [TaktPerm("identity:dept:query")]
        public async Task<IActionResult> GetRoleDeptsAsync(long deptId, [FromQuery] TaktRoleQueryDto query)
        {
            var result = await _deptService.GetRoleDeptsAsync(deptId, query);
            return Success(result);
        }

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleIds">角色ID数组</param>
        /// <returns>是否分配成功</returns>
        [HttpPost("{deptId}/roles")]
        [TaktPerm("identity:dept:update")]
        [TaktLog("分配角色部门")]
        public async Task<IActionResult> AssignRoleDeptsAsync(long deptId, [FromBody] long[] roleIds)
        {
            var result = await _deptService.AssignRoleDeptsAsync(deptId, roleIds);
            return Success(result);
        }

        /// <summary>
        /// 导入部门数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("identity:dept:import")]
        [TaktLog("导入部门数据")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Dept")
        {
            if (file == null || file.Length == 0)
                return Error("请选择要导入的文件");

            using var stream = file.OpenReadStream();
            var (success, fail) = await _deptService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Dept.Import.Success"));
        }

        /// <summary>
        /// 导出部门数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("identity:dept:export")]
        [TaktLog("导出部门数据")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktDeptQueryDto query, [FromQuery] string sheetName = "Dept")
        {
            var result = await _deptService.ExportAsync(query, sheetName);
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
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("identity:dept:export")]
        [TaktLog("获取部门导入模板")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Dept")
        {
            var result = await _deptService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



