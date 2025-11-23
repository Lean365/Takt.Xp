//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentRevisionController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档修订记录控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO文档修订记录控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档修订记录")]
    [ApiController]
    [ApiModule("routine", "ISO文档修订管理")]
    public class TaktIsoDocumentRevisionController : TaktBaseController
    {
        private readonly ITaktIsoDocumentRevisionService _revisionService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentRevisionController(
            ITaktIsoDocumentRevisionService revisionService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _revisionService = revisionService;
        }

        /// <summary>
        /// 获取修订记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>修订记录分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:revision:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoDocumentRevisionQueryDto query)
        {
            var result = await _revisionService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocumentRevision.List.Success"));
        }

        /// <summary>
        /// 获取修订记录详情
        /// </summary>
        /// <param name="id">修订记录ID</param>
        /// <returns>修订记录详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _revisionService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocumentRevision.Get.Success"));
        }

        /// <summary>
        /// 创建修订记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>修订记录ID</returns>
        [HttpPost]
        [TaktLog("创建ISO文档修订记录")]
        [TaktPerm("routine:iso:revision:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoDocumentRevisionCreateDto input)
        {
            var result = await _revisionService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocumentRevision.Insert.Success"));
        }

        /// <summary>
        /// 更新修订记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO文档修订记录")]
        [TaktPerm("routine:iso:revision:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoDocumentRevisionUpdateDto input)
        {
            var result = await _revisionService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocumentRevision.Update.Success"));
        }

        /// <summary>
        /// 删除修订记录
        /// </summary>
        /// <param name="id">修订记录ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO文档修订记录")]
        [TaktPerm("routine:iso:revision:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _revisionService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocumentRevision.Delete.Success"));
        }

        /// <summary>
        /// 批量删除修订记录
        /// </summary>
        /// <param name="ids">修订记录ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:revision:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _revisionService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocumentRevision.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取修订记录选项列表
        /// </summary>
        /// <returns>修订记录选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _revisionService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据ISO文档ID获取修订记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>修订记录列表</returns>
        [HttpGet("by-document/{documentId}")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetByDocumentIdAsync(long documentId)
        {
            var result = await _revisionService.GetByDocumentIdAsync(documentId);
            return Success(result);
        }

        /// <summary>
        /// 提交修订申请
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="version">版本号</param>
        /// <param name="revisionContent">修订内容</param>
        /// <param name="revisionReason">修订原因</param>
        /// <returns>是否成功</returns>
        [HttpPost("submit")]
        [TaktLog("提交ISO文档修订申请")]
        [TaktPerm("routine:iso:revision:create")]
        public async Task<IActionResult> SubmitRevisionAsync(long documentId, string version, string revisionContent, string revisionReason)
        {
            var result = await _revisionService.SubmitRevisionAsync(documentId, version, revisionContent, revisionReason);
            return Success(result, _localization.L("IsoDocumentRevision.Submit.Success"));
        }

        /// <summary>
        /// 审批修订
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{revisionRecordId}/approve")]
        [TaktLog("审批ISO文档修订")]
        [TaktPerm("routine:iso:revision:update")]
        public async Task<IActionResult> ApproveRevisionAsync(long revisionRecordId, [FromQuery] int approvalResult, [FromQuery] string? approvalComment)
        {
            var result = await _revisionService.ApproveRevisionAsync(revisionRecordId, approvalResult, approvalComment);
            return Success(result, _localization.L("IsoDocumentRevision.Approve.Success"));
        }

        /// <summary>
        /// 切换当前版本
        /// </summary>
        /// <param name="revisionRecordId">修订记录ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{revisionRecordId}/switch-version")]
        [TaktLog("切换ISO文档当前版本")]
        [TaktPerm("routine:iso:revision:update")]
        public async Task<IActionResult> SwitchCurrentVersionAsync(long revisionRecordId)
        {
            var result = await _revisionService.SwitchCurrentVersionAsync(revisionRecordId);
            return Success(result, _localization.L("IsoDocumentRevision.SwitchVersion.Success"));
        }

        /// <summary>
        /// 获取待审批的修订申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        [HttpGet("pending-approval")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetPendingApprovalListAsync()
        {
            var result = await _revisionService.GetPendingApprovalListAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取我的修订申请列表
        /// </summary>
        /// <param name="reviserId">修订人ID</param>
        /// <returns>我的申请列表</returns>
        [HttpGet("my-applications/{reviserId}")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetMyApplicationsAsync(long reviserId)
        {
            var result = await _revisionService.GetMyApplicationsAsync(reviserId);
            return Success(result);
        }

        /// <summary>
        /// 获取修订统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:revision:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _revisionService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




