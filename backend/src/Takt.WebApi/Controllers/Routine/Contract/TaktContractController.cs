//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 合同控制器
//===================================================================

using Takt.Application.Dtos.Routine;
using Takt.Application.Services.Routine;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 合同控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "合同管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktContractController : TaktBaseController
    {
        private readonly ITaktContractService _contractService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="contractService">合同服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktContractController(
            ITaktContractService contractService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _contractService = contractService;
        }

        /// <summary>
        /// 获取合同分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>合同分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:contract:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktContractQueryDto query)
        {
            var result = await _contractService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>合同详情</returns>
        [HttpGet("{contractId}")]
        public async Task<IActionResult> GetByIdAsync(long contractId)
        {
            var result = await _contractService.GetByIdAsync(contractId);
            return Success(result);
        }

        /// <summary>
        /// 创建合同
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>合同ID</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] TaktContractCreateDto input)
        {
            var result = await _contractService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktContractUpdateDto input)
        {
            var result = await _contractService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{contractId}")]
        public async Task<IActionResult> DeleteAsync(long contractId)
        {
            var result = await _contractService.DeleteAsync(contractId);
            return Success(result);
        }

        /// <summary>
        /// 批量删除合同
        /// </summary>
        /// <param name="contractIds">合同ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] contractIds)
        {
            var result = await _contractService.BatchDeleteAsync(contractIds);
            return Success(result);
        }

        /// <summary>
        /// 导入合同数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "合同信息")
        {
            using var stream = file.OpenReadStream();
            var result = await _contractService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出合同数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktContractQueryDto query, [FromQuery] string sheetName = "合同信息")
        {
            var result = await _contractService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "合同信息")
        {
            var result = await _contractService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }
    }
} 




