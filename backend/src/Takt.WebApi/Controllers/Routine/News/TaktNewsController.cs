#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsController.cs
// 创建者 : Takt(Claude AI)
// 创建时间:2024-12-01 10:00// 版本号 : V1.0
// 描述   : 新闻控制器
//===================================================================

namespace Takt.WebApi.Controllers.Routine.News
{
    /// <summary>
    /// 新闻控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024
    /// </remarks>
    [Route("api/[controller]", Name = "新闻")]
    [ApiController]
    [ApiModule("routine", "新闻管理")]
    public class TaktNewsController : TaktBaseController
    {
        private readonly ITaktNewsService _newsService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="newsService">新闻服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsController(
            ITaktNewsService newsService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// 获取新闻分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:news:hot:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktNewsQueryDto query)
        {
            var result = await _newsService.GetListAsync(query);
            return Success(result, _localization.L("News.List.Success"));
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>新闻详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _newsService.GetByIdAsync(id);
            return Success(result, _localization.L("News.Get.Success"));
        }

        /// <summary>
        /// 创建新闻
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>新闻ID</returns>
        [HttpPost]
        [TaktLog("创建新闻")]
        [TaktPerm("routine:news:hot:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktNewsCreateDto input)
        {
            var result = await _newsService.CreateAsync(input);
            return Success(result, _localization.L("News.Insert.Success"));
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新新闻")]
        [TaktPerm("routine:news:hot:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktNewsUpdateDto input)
        {
            var result = await _newsService.UpdateAsync(input);
            return Success(result, _localization.L("News.Update.Success"));
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除新闻")]
        [TaktPerm("routine:news:hot:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _newsService.DeleteAsync(id);
            return Success(result, _localization.L("News.Delete.Success"));
        }

        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="ids">新闻ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:news:hot:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _newsService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("News.BatchDelete.Success"));
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新新闻状态")]
        [TaktPerm("routine:news:hot:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktNewsStatusDto input)
        {
            var result = await _newsService.UpdateStatusAsync(input);
            return Success(result, _localization.L("News.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取热门新闻
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门新闻列表</returns>
        [HttpGet("hot")]
        [TaktPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetHotNewsAsync([FromQuery] int count = 10)
        {
            var result = await _newsService.GetHotNewsAsync(count);
            return Success(result, _localization.L("News.Hot.Success"));
        }

        /// <summary>
        /// 获取推荐新闻
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐新闻列表</returns>
        [HttpGet("recommended")]
        [TaktPerm("routine:news:hot:query")]
        public async Task<IActionResult> GetRecommendedNewsAsync([FromQuery] int count = 10)
        {
            var result = await _newsService.GetRecommendNewsAsync(count);
            return Success(result, _localization.L("News.Recommended.Success"));
        }

        /// <summary>
        /// 搜索新闻
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        [HttpGet("search")]
        [TaktPerm("routine:news:hot:query")]
        public async Task<IActionResult> SearchNewsAsync([FromQuery] string keyword, [FromQuery] int count = 20)
        {
            var result = await _newsService.SearchNewsAsync(keyword, count);
            return Success(result, _localization.L("News.Search.Success"));
        }

        /// <summary>
        /// 导入新闻数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("routine:news:hot:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _newsService.ImportAsync(stream, "TaktNews");
            return Success(new { success, fail }, _localization.L("News.Import.Success"));
        }

        /// <summary>
        /// 导出新闻数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:news:hot:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktNewsQueryDto query)
        {
            var result = await _newsService.ExportAsync(query, "TaktNews");
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:news:hot:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _newsService.GetTemplateAsync("TaktNews");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
}



