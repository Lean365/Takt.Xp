//===================================================================
// 项目名 : Takt.WebApi
// 文件名 : TaktPlantController.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 工厂控制器
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
    /// 工厂控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaktPlantController : TaktBaseController
    {
        private readonly ITaktPlantService _plantService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="plantService">工厂服务</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktPlantController(
            ITaktPlantService plantService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _plantService = plantService ?? throw new ArgumentNullException(nameof(plantService));
        }

        /// <summary>
        /// 获取工厂分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        [HttpGet("list")]
        [TaktPerm("logistics:material:master:plant:list")]
        public async Task<TaktApiResult<TaktPagedResult<TaktPlantDto>>> GetListAsync([FromQuery] TaktPlantQueryDto query)
        {
            try
            {
                var result = await _plantService.GetListAsync(query);
                return TaktApiResult<TaktPagedResult<TaktPlantDto>>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktPagedResult<TaktPlantDto>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据ID获取工厂详情
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回工厂详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("logistics:material:master:plant:list")]
        public async Task<TaktApiResult<TaktPlantDto>> GetByIdAsync(long id)
        {
            try
            {
                var result = await _plantService.GetByIdAsync(id);
                return TaktApiResult<TaktPlantDto>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktPlantDto>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据工厂编码获取工厂详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <returns>返回工厂详情</returns>
        [HttpGet("code/{plantCode}")]
        [TaktPerm("logistics:material:master:plant:list")]
        public async Task<TaktApiResult<TaktPlantDto>> GetByPlantCodeAsync(string plantCode)
        {
            try
            {
                var result = await _plantService.GetByPlantCodeAsync(plantCode);
                return TaktApiResult<TaktPlantDto>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<TaktPlantDto>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 创建工厂
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回创建结果</returns>
        [HttpPost]
        [TaktPerm("logistics:material:master:plant:create")]
        public async Task<TaktApiResult<long>> CreateAsync([FromBody] TaktPlantCreateDto input)
        {
            try
            {
                var result = await _plantService.CreateAsync(input);
                return TaktApiResult<long>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<long>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 更新工厂
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回更新结果</returns>
        [HttpPut]
        [TaktPerm("logistics:material:master:plant:edit")]
        public async Task<TaktApiResult<bool>> UpdateAsync([FromBody] TaktPlantUpdateDto input)
        {
            try
            {
                var result = await _plantService.UpdateAsync(input);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 删除工厂
        /// </summary>
        /// <param name="id">工厂ID</param>
        /// <returns>返回删除结果</returns>
        [HttpDelete("{id}")]
        [TaktPerm("logistics:material:master:plant:delete")]
        public async Task<TaktApiResult<bool>> DeleteAsync(long id)
        {
            try
            {
                var result = await _plantService.DeleteAsync(id);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 批量删除工厂
        /// </summary>
        /// <param name="ids">工厂ID集合</param>
        /// <returns>返回删除结果</returns>
        [HttpDelete("batch")]
        [TaktPerm("logistics:material:master:plant:delete")]
        public async Task<TaktApiResult<bool>> BatchDeleteAsync([FromBody] long[] ids)
        {
            try
            {
                var result = await _plantService.BatchDeleteAsync(ids);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 更新工厂状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回更新结果</returns>
        [HttpPut("status")]
        [TaktPerm("logistics:material:master:plant:edit")]
        public async Task<TaktApiResult<bool>> UpdateStatusAsync([FromBody] TaktPlantStatusDto input)
        {
            try
            {
                var result = await _plantService.UpdateStatusAsync(input);
                return TaktApiResult<bool>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<bool>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 导入工厂数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("logistics:material:master:plant:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var (success, fail) = await _plantService.ImportAsync(stream, "工厂信息");
            return Success(new { success, fail }, _localization.L("Plant.Import.Success"));
        }

        /// <summary>
        /// 导出工厂数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>Excel文件或zip文件</returns>
        [HttpGet("export")]
        [TaktPerm("logistics:material:master:plant:export")]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktPlantQueryDto query)
        {
            var result = await _plantService.ExportAsync(query, "Plant");
            var contentType = result.fileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase)
                ? "application/zip"
                : "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            // 只在 filename* 用 UTF-8 编码，filename 用 ASCII
            var safeFileName = System.Text.Encoding.ASCII.GetString(System.Text.Encoding.ASCII.GetBytes(result.fileName));
            Response.Headers["Content-Disposition"] = $"attachment; filename*=UTF-8''{Uri.EscapeDataString(result.fileName)}";
            return File(result.content, contentType, result.fileName);
        }

        /// <summary>
        /// 获取工厂导入模板
        /// </summary>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("logistics:material:master:plant:export")]
        public async Task<IActionResult> GetTemplateAsync()
        {
            var result = await _plantService.GetTemplateAsync("工厂信息");
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取工厂选项列表
        /// </summary>
        /// <returns>返回工厂选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("logistics:material:master:plant:list")]
        public async Task<TaktApiResult<List<TaktSelectOption>>> GetOptionsAsync()
        {
            try
            {
                var result = await _plantService.GetOptionsAsync();
                return TaktApiResult<List<TaktSelectOption>>.Success(result);
            }
            catch (Exception ex)
            {
                return TaktApiResult<List<TaktSelectOption>>.Error(ex.Message);
            }
        }


    }
}


