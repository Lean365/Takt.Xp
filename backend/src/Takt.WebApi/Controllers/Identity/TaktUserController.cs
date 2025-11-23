//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktUserController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;

namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    [Route("api/[controller]", Name = "用户")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class TaktUserController : TaktBaseController
    {
        private readonly ITaktUserService _userService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService">用户服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktUserController(
            ITaktUserService userService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("identity:user:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktUserQueryDto query)
        {
            var result = await _userService.GetListAsync(query);
            return Success(result, _localization.L("User.List.Success"));
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户详情</returns>
        [HttpGet("{userId}")]
        [TaktPerm("identity:user:query")]
        public async Task<IActionResult> GetByIdAsync(long userId)
        {
            var result = await _userService.GetByIdAsync(userId);
            return Success(result, _localization.L("User.Get.Success"));
        }

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("identity:user:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _userService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用户ID</returns>
        [HttpPost]
        [TaktLog("创建用户")]
        [TaktPerm("identity:user:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktUserCreateDto input)
        {
            var result = await _userService.CreateAsync(input);
            return Success(result, _localization.L("User.Insert.Success"));
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新用户")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktUserUpdateDto input)
        {
            var result = await _userService.UpdateAsync(input);
            return Success(result, _localization.L("User.Update.Success"));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{userId}")]
        [TaktLog("删除用户")]
        [TaktPerm("identity:user:delete")]
        public async Task<IActionResult> DeleteAsync(long userId)
        {
            try
            {
                var result = await _userService.DeleteAsync(userId);
                return Success(result, _localization.L("User.Delete.Success"));
            }
            catch (TaktException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktLog("批量删除用户")]
        [TaktPerm("identity:user:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] userIds)
        {
            var result = await _userService.BatchDeleteAsync(userIds);
            return Success(result, _localization.L("User.BatchDelete.Success"));
        }

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/status")]
        [TaktLog("更新用户状态")]
        [TaktPerm("identity:user:update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long userId, [FromQuery] int status)
        {
            var input = new TaktUserStatusDto
            {
                UserId = userId,
                UserStatus = status
            };
            var result = await _userService.UpdateStatusAsync(input);
            return Success(result, _localization.L("User.Status.Success"));
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("reset-password")]
        [TaktLog("重置用户密码")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] TaktUserResetPwdDto input)
        {
            var result = await _userService.ResetPasswordAsync(input);
            return Success(result, _localization.L("User.ResetPassword.Success"));
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("change-password")]
        [TaktLog("修改用户密码")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] TaktUserChangePwdDto input)
        {
            var result = await _userService.ChangePasswordAsync(input);
            return Success(result, _localization.L("User.ChangePassword.Success"));
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/unlock")]
        [TaktLog("解锁用户")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> UnlockUserAsync(long userId)
        {
            var input = new TaktUserUnlockDto { UserId = userId };
            var result = await _userService.UnlockUserAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新用户个人信息
        /// </summary>
        /// <param name="input">个人信息更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("profile")]
        [TaktLog("更新用户个人信息")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> UpdateProfileAsync([FromBody] TaktUserProfileUpdateDto input)
        {
            var result = await _userService.UpdateProfileAsync(input);
            return Success(result, _localization.L("User.Profile.Update.Success"));
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input">头像更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("avatar")]
        [TaktLog("更新用户头像")]
        [TaktPerm("identity:user:update")]
        public async Task<IActionResult> UpdateAvatarAsync([FromBody] TaktUserAvatarUpdateDto input)
        {
            var result = await _userService.UpdateAvatarAsync(input);
            return Success(result, _localization.L("User.Avatar.Update.Success"));
        }

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户角色列表</returns>
        [HttpGet("{userId}/roles")]
        [TaktPerm("identity:user:query")]
        public async Task<IActionResult> GetUserRolesAsync(long userId)
        {
            var result = await _userService.GetUserRoleIdsAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户部门列表</returns>
        [HttpGet("{userId}/depts")]
        [TaktPerm("identity:user:query")]
        public async Task<IActionResult> GetUserDeptsAsync(long userId)
        {
            var result = await _userService.GetUserDeptIdsAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户岗位列表</returns>
        [HttpGet("{userId}/posts")]
        [TaktPerm("identity:user:query")]
        public async Task<IActionResult> GetUserPostsAsync(long userId)
        {
            var result = await _userService.GetUserPostIdsAsync(userId);
            return Success(result);
        }


        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/roles")]
        [TaktPerm("identity:user:allocate")]
        public async Task<IActionResult> AssignUserRolesAsync(long userId, [FromBody] long[] roleIds)
        {
            var result = await _userService.AssignUserRolesAsync(userId, roleIds);
            return Success(result);
        }

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/depts")]
        [TaktPerm("identity:user:allocate")]
        public async Task<IActionResult> AssignUserDeptsAsync(long userId, [FromBody] long[] deptIds)
        {
            var result = await _userService.AssignUserDeptsAsync(userId, deptIds);
            return Success(result);
        }

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="postIds">岗位ID列表</param>
        /// <returns>是否成功</returns>
        [HttpPut("{userId}/posts")]
        [TaktPerm("identity:user:allocate")]
        public async Task<IActionResult> AssignUserPostsAsync(long userId, [FromBody] long[] postIds)
        {
            var result = await _userService.AssignUserPostsAsync(userId, postIds);
            return Success(result);
        }


        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("identity:user:export")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _userService.GetTemplateAsync(sheetName, fileName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入用户")]
        [TaktPerm("identity:user:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string? sheetName = null)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _userService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("User.Import.Success"));
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("identity:user:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktUserQueryDto query, [FromQuery] string? sheetName = null, [FromQuery] string? fileName = null)
        {
            var result = await _userService.ExportAsync(query, sheetName, fileName);
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



