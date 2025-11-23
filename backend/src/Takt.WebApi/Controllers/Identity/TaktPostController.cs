//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPostController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位控制器
//===================================================================

using Takt.Application.Dtos.Identity;
using Takt.Application.Services.Identity;

namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 岗位控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    [Route("api/[controller]", Name = "岗位")]
    [ApiController]
    [ApiModule("identity", "身份认证")]
    public class TaktPostController : TaktBaseController
    {
        private readonly ITaktPostService _postService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="postService">岗位服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktPostController(
            ITaktPostService postService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _postService = postService;
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>岗位分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("identity:post:query")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktPostQueryDto query)
        {
            var result = await _postService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>岗位详情</returns>
        [HttpGet("{postId}")]
        [TaktPerm("identity:post:query")]
        public async Task<IActionResult> GetByIdAsync(long postId)
        {
            var result = await _postService.GetByIdAsync(postId);
            return Success(result);
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>岗位ID</returns>
        [HttpPost]
        [TaktPerm("identity:post:create")]
        [TaktLog("创建岗位")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktPostCreateDto input)
        {
            var result = await _postService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("identity:post:update")]
        [TaktLog("更新岗位")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktPostUpdateDto input)
        {
            var result = await _postService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{postId}")]
        [TaktPerm("identity:post:delete")]
        [TaktLog("删除岗位")]
        public async Task<IActionResult> DeleteAsync(long postId)
        {
            var result = await _postService.DeleteAsync(postId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        /// <param name="postIds">岗位ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("identity:post:delete")]
        [TaktLog("批量删除岗位")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] postIds)
        {
            var result = await _postService.BatchDeleteAsync(postIds);
            return Success(result);
        }

        /// <summary>
        /// 更新岗位状态
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{postId}/status")]
        [TaktPerm("identity:post:update")]
        [TaktLog("更新岗位状态")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatusAsync(long postId, [FromQuery] int status)
        {
            var input = new TaktPostStatusDto
            {
                PostId = postId,
                PostStatus = status
            };
            var result = await _postService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取岗位选项列表
        /// </summary>
        /// <returns>岗位选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("identity:post:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var posts = await _postService.GetOptionsAsync();
            return Success(posts);
        }

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>用户岗位列表</returns>
        [HttpGet("{postId}/users")]
        [TaktPerm("identity:post:query")]
        public async Task<IActionResult> GetUserPostsAsync(long postId)
        {
            var result = await _postService.GetUserPostsAsync(postId);
            return Success(result);
        }

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        [HttpPost("{postId}/users")]
        [TaktPerm("identity:post:allocate")]
        [TaktLog("分配用户岗位")]
        public async Task<IActionResult> AssignUserPostsAsync(long postId, [FromBody] long[] userIds)
        {
            var result = await _postService.AssignUserPostsAsync(postId, userIds);
            return Success(result);
        }

        /// <summary>
        /// 导入岗位数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("identity:post:import")]
        [TaktLog("导入岗位数据")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Post")
        {
            using var stream = file.OpenReadStream();
            var result = await _postService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("identity:post:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktPostQueryDto query, [FromQuery] string sheetName = "Post")
        {
            var result = await _postService.ExportAsync(query, sheetName);
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
        [TaktPerm("identity:post:export")]
        [TaktLog("获取岗位导入模板")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Post")
        {
            var result = await _postService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



