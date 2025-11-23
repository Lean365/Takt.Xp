//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCompanyController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-09 16:30
// 版本号 : V0.0.1
// 描述   : 公司代码控制器
//===================================================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Application.Services.Accounting.Financial;

namespace Takt.WebApi.Controllers.Accounting.Financial
{
    /// <summary>
    /// 公司代码控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-09
    /// </remarks>
    [Route("api/[controller]", Name = "公司代码管理")]
    [ApiController]
    [ApiModule("accounting", "财务会计")]
    public class TaktCompanyController : TaktBaseController
    {
        private readonly ITaktCompanyService _companyService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="companyService">公司代码服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktCompanyController(
            ITaktCompanyService companyService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _companyService = companyService;
        }

        /// <summary>
        /// 获取公司代码分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>公司代码分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("accounting:financial:company:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktCompanyQueryDto query)
        {
            var result = await _companyService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取公司代码详情
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>公司代码详情</returns>
        [HttpGet("{companyId}")]
        [TaktPerm("accounting:financial:company:query")]
        public async Task<IActionResult> GetByIdAsync(long companyId)
        {
            var result = await _companyService.GetByIdAsync(companyId);
            return Success(result);
        }

        /// <summary>
        /// 获取公司代码选项列表
        /// </summary>
        /// <returns>公司代码选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("accounting:financial:company:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _companyService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 创建公司代码
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>公司代码ID</returns>
        [HttpPost]
        [TaktLog("创建公司代码")]
        [TaktPerm("accounting:financial:company:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktCompanyCreateDto input)
        {
            var result = await _companyService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新公司代码
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新公司代码")]
        [TaktPerm("accounting:financial:company:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktCompanyUpdateDto input)
        {
            var result = await _companyService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除公司代码
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{companyId}")]
        [TaktLog("删除公司代码")]
        [TaktPerm("accounting:financial:company:delete")]
        public async Task<IActionResult> DeleteAsync(long companyId)
        {
            var result = await _companyService.DeleteAsync(companyId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除公司代码
        /// </summary>
        /// <param name="companyIds">公司代码ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktLog("批量删除公司代码")]
        [TaktPerm("accounting:financial:company:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] companyIds)
        {
            var result = await _companyService.BatchDeleteAsync(companyIds);
            return Success(result);
        }

        /// <summary>
        /// 更新公司代码状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新公司代码状态")]
        [TaktPerm("accounting:financial:company:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktCompanyStatusDto input)
        {
            var result = await _companyService.UpdateStatusAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 获取公司代码导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("accounting:financial:company:export")]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "Company")
        {
            var result = await _companyService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 导入公司代码数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktLog("导入公司代码")]
        [TaktPerm("accounting:financial:company:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "Company")
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _companyService.ImportAsync(stream, sheetName);
            return Success(new { success, fail }, _localization.L("Company.Import.Success"));
        }

        /// <summary>
        /// 导出公司代码数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("accounting:financial:company:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktCompanyQueryDto query, [FromQuery] string sheetName = "Company")
        {
            var result = await _companyService.ExportAsync(query, sheetName);
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




