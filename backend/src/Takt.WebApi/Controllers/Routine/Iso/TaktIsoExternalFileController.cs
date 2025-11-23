//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoExternalFileController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO外来文件控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO外来文件控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO外来文件")]
    [ApiController]
    [ApiModule("routine", "ISO外来文件管理")]
    public class TaktIsoExternalFileController : TaktBaseController
    {
        private readonly ITaktIsoExternalFileService _externalFileService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoExternalFileController(
            ITaktIsoExternalFileService externalFileService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _externalFileService = externalFileService;
        }

        /// <summary>
        /// 获取外来文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>外来文件分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:external-file:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoExternalFileQueryDto query)
        {
            var result = await _externalFileService.GetListAsync(query);
            return Success(result, _localization.L("IsoExternalFile.List.Success"));
        }

        /// <summary>
        /// 获取外来文件详情
        /// </summary>
        /// <param name="id">外来文件ID</param>
        /// <returns>外来文件详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _externalFileService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoExternalFile.Get.Success"));
        }

        /// <summary>
        /// 创建外来文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>外来文件ID</returns>
        [HttpPost]
        [TaktLog("创建ISO外来文件")]
        [TaktPerm("routine:iso:external-file:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoExternalFileCreateDto input)
        {
            var result = await _externalFileService.CreateAsync(input);
            return Success(result, _localization.L("IsoExternalFile.Insert.Success"));
        }

        /// <summary>
        /// 更新外来文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO外来文件")]
        [TaktPerm("routine:iso:external-file:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoExternalFileUpdateDto input)
        {
            var result = await _externalFileService.UpdateAsync(input);
            return Success(result, _localization.L("IsoExternalFile.Update.Success"));
        }

        /// <summary>
        /// 删除外来文件
        /// </summary>
        /// <param name="id">外来文件ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO外来文件")]
        [TaktPerm("routine:iso:external-file:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _externalFileService.DeleteAsync(id);
            return Success(result, _localization.L("IsoExternalFile.Delete.Success"));
        }

        /// <summary>
        /// 批量删除外来文件
        /// </summary>
        /// <param name="ids">外来文件ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:external-file:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _externalFileService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoExternalFile.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取外来文件选项列表
        /// </summary>
        /// <returns>外来文件选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _externalFileService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据文件代码获取外来文件
        /// </summary>
        /// <param name="fileCode">文件代码</param>
        /// <returns>外来文件详情</returns>
        [HttpGet("by-code/{fileCode}")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetByFileCodeAsync(string fileCode)
        {
            var result = await _externalFileService.GetByFileCodeAsync(fileCode);
            return Success(result);
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="externalFileId">外来文件ID</param>
        /// <param name="reviewComment">审核意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{externalFileId}/review")]
        [TaktLog("提交ISO外来文件审核")]
        [TaktPerm("routine:iso:external-file:update")]
        public async Task<IActionResult> SubmitReviewAsync(long externalFileId, [FromQuery] string? reviewComment)
        {
            var result = await _externalFileService.SubmitReviewAsync(externalFileId, reviewComment);
            return Success(result, _localization.L("IsoExternalFile.Review.Success"));
        }

        /// <summary>
        /// 审批外来文件
        /// </summary>
        /// <param name="externalFileId">外来文件ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{externalFileId}/approve")]
        [TaktLog("审批ISO外来文件")]
        [TaktPerm("routine:iso:external-file:update")]
        public async Task<IActionResult> ApproveAsync(long externalFileId, [FromQuery] int approvalResult, [FromQuery] string? approvalComment)
        {
            var result = await _externalFileService.ApproveAsync(externalFileId, approvalComment);
            return Success(result, _localization.L("IsoExternalFile.Approve.Success"));
        }

        /// <summary>
        /// 分发外来文件
        /// </summary>
        /// <param name="externalFileId">外来文件ID</param>
        /// <param name="distributionScope">分发范围</param>
        /// <returns>是否成功</returns>
        [HttpPut("{externalFileId}/distribute")]
        [TaktLog("分发ISO外来文件")]
        [TaktPerm("routine:iso:external-file:update")]
        public async Task<IActionResult> DistributeAsync(long externalFileId, [FromQuery] string distributionScope)
        {
            var result = await _externalFileService.DistributeAsync(externalFileId, distributionScope);
            return Success(result, _localization.L("IsoExternalFile.Distribute.Success"));
        }

        /// <summary>
        /// 更新文件状态
        /// </summary>
        /// <param name="externalFileId">外来文件ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        [HttpPut("{externalFileId}/status")]
        [TaktLog("更新ISO外来文件状态")]
        [TaktPerm("routine:iso:external-file:update")]
        public async Task<IActionResult> UpdateStatusAsync(long externalFileId, [FromQuery] int status)
        {
            var result = await _externalFileService.UpdateStatusAsync(externalFileId, status);
            return Success(result, _localization.L("IsoExternalFile.Status.Success"));
        }

        /// <summary>
        /// 获取待审核的外来文件列表
        /// </summary>
        /// <returns>待审核列表</returns>
        [HttpGet("pending-review")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetPendingReviewListAsync()
        {
            var result = await _externalFileService.GetPendingReviewListAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取待审批的外来文件列表
        /// </summary>
        /// <returns>待审批列表</returns>
        [HttpGet("pending-approval")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetPendingApprovalListAsync()
        {
            var result = await _externalFileService.GetPendingApprovalListAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取我的外来文件列表
        /// </summary>
        /// <param name="receiverId">接收人ID</param>
        /// <returns>我的外来文件列表</returns>
        [HttpGet("my-files/{receiverId}")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetMyFilesAsync(long receiverId)
        {
            var result = await _externalFileService.GetMyFilesAsync(receiverId);
            return Success(result);
        }

        /// <summary>
        /// 获取外来文件统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:external-file:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _externalFileService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




