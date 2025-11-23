//===================================================================
// 项目名 : Takt.WebApi
// 文件名 : TaktProdMaterialController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 生产物料控制器
//===================================================================

using Takt.Application.Dtos.Logistics.Material.Master;
using Takt.Application.Services.Logistics.Material.Master;
using Takt.Shared.Models;
using Takt.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Logistics.Material.Master
{
    /// <summary>
    /// 生产物料控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaktProdMaterialController : TaktBaseController
    {
        private readonly ITaktProdMaterialService _prodMaterialService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="prodMaterialService">生产物料服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProdMaterialController(
            ITaktProdMaterialService prodMaterialService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _prodMaterialService = prodMaterialService ?? throw new ArgumentNullException(nameof(prodMaterialService));
        }

        /// <summary>
        /// 获取生产物料分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:material:master:prod:list")]
        public async Task<TaktApiResult<TaktPagedResult<TaktProdMaterialDto>>> GetListAsync([FromQuery] TaktProdMaterialQueryDto query)
        {
            try
            {
                var result = await _prodMaterialService.GetListAsync(query);
                return TaktApiResult<TaktPagedResult<TaktProdMaterialDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktPagedResult<TaktProdMaterialDto>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取生产物料详情
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回生产物料详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("logistics:material:master:prod:list")]
        public async Task<TaktApiResult<TaktProdMaterialDto>> GetByIdAsync(long id)
        {
            try
            {
                var result = await _prodMaterialService.GetByIdAsync(id);
                return TaktApiResult<TaktProdMaterialDto>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktProdMaterialDto>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据工厂编码和物料编码获取生产物料详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="materialCode">物料编码</param>
        /// <returns>返回生产物料详情</returns>
        [HttpGet("code/{plantCode}/{materialCode}")]
        [TaktPerm("logistics:material:master:prod:list")]
        public async Task<TaktApiResult<TaktProdMaterialDto>> GetByPlantAndMaterialCodeAsync(string plantCode, string materialCode)
        {
            try
            {
                var result = await _prodMaterialService.GetByPlantAndMaterialCodeAsync(plantCode, materialCode);
                return TaktApiResult<TaktProdMaterialDto>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktProdMaterialDto>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 创建生产物料
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回创建结果</returns>
        [HttpPost]
        [TaktPerm("logistics:material:master:prod:create")]
        public async Task<TaktApiResult<long>> CreateAsync([FromBody] TaktProdMaterialCreateDto input)
        {
            try
            {
                var result = await _prodMaterialService.CreateAsync(input);
                return TaktApiResult<long>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<long>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 更新生产物料
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回更新结果</returns>
        [HttpPut]
        [TaktPerm("logistics:material:master:prod:update")]
        public async Task<TaktApiResult<bool>> UpdateAsync([FromBody] TaktProdMaterialUpdateDto input)
        {
            try
            {
                var result = await _prodMaterialService.UpdateAsync(input);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 删除生产物料
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回删除结果</returns>
        [HttpDelete("{id}")]
        [TaktPerm("logistics:material:master:prod:delete")]
        public async Task<TaktApiResult<bool>> DeleteAsync(long id)
        {
            try
            {
                var result = await _prodMaterialService.DeleteAsync(id);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除生产物料
        /// </summary>
        /// <param name="ids">生产物料ID集合</param>
        /// <returns>返回删除结果</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:material:master:prod:delete")]
        public async Task<TaktApiResult<bool>> BatchDeleteAsync([FromBody] long[] ids)
        {
            try
            {
                var result = await _prodMaterialService.BatchDeleteAsync(ids);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 更新生产物料状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回更新结果</returns>
        [HttpPut("status")]
        [TaktPerm("logistics:material:master:prod:update")]
        public async Task<TaktApiResult<bool>> UpdateStatusAsync([FromBody] TaktProdMaterialStatusDto input)
        {
            try
            {
                var result = await _prodMaterialService.UpdateStatusAsync(input);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 导入生产物料数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:material:master:prod:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _prodMaterialService.ImportAsync(stream, "生产物料信息");
            return Success(new { success, fail }, _localization.L("ProdMaterial.Import.Success"));
        }

        /// <summary>
        /// 导出生产物料数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:material:master:prod:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktProdMaterialQueryDto query)
        {
            var result = await _prodMaterialService.ExportAsync(query, "ProdMaterial");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取生产物料导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:material:master:prod:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _prodMaterialService.GetTemplateAsync("生产物料信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取生产物料选项列表
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("logistics:material:master:prod:list")]
        public async Task<TaktApiResult<List<TaktProdMaterialDto>>> GetOptionsAsync(string plantCode, string? keyword = null)
        {
            try
            {
                var result = await _prodMaterialService.GetOptionsAsync(plantCode, keyword);
                return TaktApiResult<List<TaktProdMaterialDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<List<TaktProdMaterialDto>>.Error(ex.Message);
            }
        }

    }
}


