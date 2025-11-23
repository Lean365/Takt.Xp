#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProdOrderController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 生产工单控制器
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Application.Services.Logistics.Manufacturing.Master;

namespace Takt.WebApi.Controllers.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 生产工单控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "生产工单")]
    [ApiController]
    [ApiModule("logistics", "后勤管理")]
    public class TaktProdOrderController : TaktBaseController
    {
        private readonly ITaktProdOrderService _prodOrderService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prodOrderService">生产工单服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProdOrderController(
            ITaktProdOrderService prodOrderService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _prodOrderService = prodOrderService;
        }

        /// <summary>
        /// 获取生产工单分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>生产工单分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:manufacturing:master:order:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktProdOrderQueryDto query)
        {
            var result = await _prodOrderService.GetListAsync(query);
            return Success(result, _localization.L("ProdOrder.List.Success"));
        }

        /// <summary>
        /// 获取生产工单详情
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>生产工单详情</returns>
        [HttpGet("{prodOrderId}")]
        [TaktPerm("logistics:manufacturing:master:order:query")]
        public async Task<IActionResult> GetByIdAsync(long prodOrderId)
        {
            var result = await _prodOrderService.GetByIdAsync(prodOrderId);
            return Success(result, _localization.L("ProdOrder.Get.Success"));
        }

        /// <summary>
        /// 创建生产工单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>生产工单ID</returns>
        [HttpPost]
        [TaktLog("创建生产工单")]
        [TaktPerm("logistics:manufacturing:master:order:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktProdOrderCreateDto input)
        {
            var result = await _prodOrderService.CreateAsync(input);
            return Success(result, _localization.L("ProdOrder.Insert.Success"));
        }

        /// <summary>
        /// 更新生产工单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新生产工单")]
        [TaktPerm("logistics:manufacturing:master:order:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktProdOrderUpdateDto input)
        {
            var result = await _prodOrderService.UpdateAsync(input);
            return Success(result, _localization.L("ProdOrder.Update.Success"));
        }

        /// <summary>
        /// 删除生产工单
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{prodOrderId}")]
        [TaktLog("删除生产工单")]
        [TaktPerm("logistics:manufacturing:master:order:delete")]
        public async Task<IActionResult> DeleteAsync(long prodOrderId)
        {
            try
            {
                var result = await _prodOrderService.DeleteAsync(prodOrderId);
                return Success(result, _localization.L("ProdOrder.Delete.Success"));
            }
            catch (TaktException ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除生产工单
        /// </summary>
        /// <param name="prodOrderIds">生产工单ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:manufacturing:master:order:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] prodOrderIds)
        {
            var result = await _prodOrderService.BatchDeleteAsync(prodOrderIds);
            return Success(result, _localization.L("ProdOrder.BatchDelete.Success"));
        }

        /// <summary>
        /// 导入生产工单数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:manufacturing:master:order:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _prodOrderService.ImportAsync(stream, "生产工单信息");
            return Success(new { success, fail }, _localization.L("ProdOrder.Import.Success"));
        }

        /// <summary>
        /// 导出生产工单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:manufacturing:master:order:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktProdOrderQueryDto query)
        {
            var result = await _prodOrderService.ExportAsync(query, "ProdOrder");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取生产工单导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:manufacturing:master:order:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _prodOrderService.GetTemplateAsync("生产工单信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 更新生产工单状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut("status")]
        [TaktLog("更新生产工单状态")]
        [TaktPerm("logistics:manufacturing:master:order:update")]
        public async Task<IActionResult> UpdateStatusAsync([FromBody] TaktProdOrderStatusDto input)
        {
            var result = await _prodOrderService.UpdateStatusAsync(input);
            return Success(result, _localization.L("ProdOrder.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取生产工单选项列表
        /// </summary>
        /// <returns>生产工单选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("logistics:manufacturing:master:order:list")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _prodOrderService.GetOptionsAsync();
            return Success(result, _localization.L("ProdOrder.Options.Success"));
        }





        /// <summary>
        /// 更新生产工单数量
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <param name="producedQuantity">已生产数量</param>
        /// <returns>是否成功</returns>
        [HttpPut("{prodOrderId}/quantity")]
        [TaktLog("更新生产工单数量")]
        [TaktPerm("logistics:manufacturing:master:order:update")]
        public async Task<IActionResult> UpdateProducedQuantityAsync(long prodOrderId, [FromBody] decimal producedQuantity)
        {
            var result = await _prodOrderService.UpdateProducedQuantityAsync(prodOrderId, producedQuantity);
            return Success(result, _localization.L("ProdOrder.QuantityUpdate.Success"));
        }

        /// <summary>
        /// 更新生产工单状态
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <param name="prodOrderStatus">生产工单状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{prodOrderId}/status")]
        [TaktLog("更新生产工单状态")]
        [TaktPerm("logistics:manufacturing:master:order:update")]
        public async Task<IActionResult> UpdateProdOrderStatusAsync(long prodOrderId, [FromBody] int prodOrderStatus)
        {
            var input = new TaktProdOrderStatusDto { ProdOrderId = prodOrderId, Status = prodOrderStatus };
            var result = await _prodOrderService.UpdateStatusAsync(input);
            return Success(result, _localization.L("ProdOrder.StatusUpdate.Success"));
        }

        /// <summary>
        /// 获取生产工单变更记录
        /// </summary>
        /// <param name="prodOrderId">生产工单ID</param>
        /// <returns>变更记录列表</returns>
        [HttpGet("{prodOrderId}/changelogs")]
        [TaktPerm("logistics:manufacturing:master:order:query")]
        public async Task<IActionResult> GetChangeLogsAsync(long prodOrderId)
        {
            var result = await _prodOrderService.GetChangeLogsAsync(prodOrderId);
            return Success(result, _localization.L("ProdOrder.ChangeLogs.Success"));
        }
    }
}




