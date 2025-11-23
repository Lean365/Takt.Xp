//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 项目控制器
//===================================================================

using Takt.Application.Dtos.Routine;
using Takt.Application.Services.Routine;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 项目控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "项目管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktProjectController : TaktBaseController
    {
        private readonly ITaktProjectService _projectService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="projectService">项目服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProjectController(
            ITaktProjectService projectService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>项目分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:project:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktProjectQueryDto query)
        {
            var result = await _projectService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>项目详情</returns>
        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetByIdAsync(long projectId)
        {
            var result = await _projectService.GetByIdAsync(projectId);
            return Success(result);
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>项目ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaktProjectCreateDto input)
        {
            var result = await _projectService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktProjectUpdateDto input)
        {
            var result = await _projectService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{projectId}")]
        public async Task<IActionResult> DeleteAsync(long projectId)
        {
            var result = await _projectService.DeleteAsync(projectId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="projectIds">项目ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] projectIds)
        {
            var result = await _projectService.BatchDeleteAsync(projectIds);
            return Success(result);
        }

        /// <summary>
        /// 导入项目数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "项目信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _projectService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktProjectQueryDto query, [FromQuery] string sheetName = "项目信息")
        {
            var result = await _projectService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "项目信息")
        {
            var result = await _projectService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 




