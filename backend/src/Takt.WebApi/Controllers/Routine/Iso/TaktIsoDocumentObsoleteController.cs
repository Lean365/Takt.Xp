//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentObsoleteController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档作废记录控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO文档作废记录控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档作废记录")]
    [ApiController]
    [ApiModule("routine", "ISO文档作废管理")]
    public class TaktIsoDocumentObsoleteController : TaktBaseController
    {
        private readonly ITaktIsoDocumentObsoleteService _obsoleteService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentObsoleteController(
            ITaktIsoDocumentObsoleteService obsoleteService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _obsoleteService = obsoleteService;
        }

        /// <summary>
        /// 获取作废记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>作废记录分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:obsolete:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoDocumentObsoleteQueryDto query)
        {
            var result = await _obsoleteService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocumentObsolete.List.Success"));
        }

        /// <summary>
        /// 获取作废记录详情
        /// </summary>
        /// <param name="id">作废记录ID</param>
        /// <returns>作废记录详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _obsoleteService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocumentObsolete.Get.Success"));
        }

        /// <summary>
        /// 创建作废申请
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>作废记录ID</returns>
        [HttpPost]
        [TaktLog("创建ISO文档作废申请")]
        [TaktPerm("routine:iso:obsolete:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoDocumentObsoleteCreateDto input)
        {
            var result = await _obsoleteService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocumentObsolete.Insert.Success"));
        }

        /// <summary>
        /// 更新作废记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO文档作废记录")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoDocumentObsoleteUpdateDto input)
        {
            var result = await _obsoleteService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocumentObsolete.Update.Success"));
        }

        /// <summary>
        /// 删除作废记录
        /// </summary>
        /// <param name="id">作废记录ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO文档作废记录")]
        [TaktPerm("routine:iso:obsolete:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _obsoleteService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocumentObsolete.Delete.Success"));
        }

        /// <summary>
        /// 批量删除作废记录
        /// </summary>
        /// <param name="ids">作废记录ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:obsolete:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _obsoleteService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocumentObsolete.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取作废记录选项列表
        /// </summary>
        /// <returns>作废记录选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _obsoleteService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据ISO文档ID获取作废记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>作废记录列表</returns>
        [HttpGet("by-document/{documentId}")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetByDocumentIdAsync(long documentId)
        {
            var result = await _obsoleteService.GetByDocumentIdAsync(documentId);
            return Success(result);
        }

        /// <summary>
        /// 提交作废申请
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="obsoleteType">作废类型</param>
        /// <param name="obsoleteReason">作废原因</param>
        /// <param name="replacementDocumentId">替代文档ID</param>
        /// <param name="replacementDocumentVersion">替代文档版本号</param>
        /// <returns>是否成功</returns>
        [HttpPost("submit-application")]
        [TaktLog("提交ISO文档作废申请")]
        [TaktPerm("routine:iso:obsolete:create")]
        public async Task<IActionResult> SubmitApplicationAsync(long documentId, string documentVersion, int obsoleteType, string obsoleteReason, [FromQuery] long? replacementDocumentId, [FromQuery] string? replacementDocumentVersion)
        {
            var result = await _obsoleteService.SubmitApplicationAsync(documentId, documentVersion, obsoleteType, obsoleteReason, replacementDocumentId, replacementDocumentVersion);
            return Success(result, _localization.L("IsoDocumentObsolete.Application.Success"));
        }

        /// <summary>
        /// 审批作废申请
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="approvalResult">审批结果</param>
        /// <param name="approvalComment">审批意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{obsoleteRecordId}/approve")]
        [TaktLog("审批ISO文档作废申请")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> ApproveApplicationAsync(long obsoleteRecordId, [FromQuery] int approvalResult, [FromQuery] string? approvalComment)
        {
            var result = await _obsoleteService.ApproveApplicationAsync(obsoleteRecordId, approvalResult, approvalComment);
            return Success(result, _localization.L("IsoDocumentObsolete.Approval.Success"));
        }

        /// <summary>
        /// 执行作废
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="obsoleteMethod">作废方式</param>
        /// <param name="obsoleteNote">作废说明</param>
        /// <returns>是否成功</returns>
        [HttpPut("{obsoleteRecordId}/execute")]
        [TaktLog("执行ISO文档作废")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> ExecuteObsoleteAsync(long obsoleteRecordId, [FromQuery] int obsoleteMethod, [FromQuery] string? obsoleteNote)
        {
            var result = await _obsoleteService.ExecuteObsoleteAsync(obsoleteRecordId, obsoleteMethod, obsoleteNote);
            return Success(result, _localization.L("IsoDocumentObsolete.Execute.Success"));
        }

        /// <summary>
        /// 回收作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="recycleMethod">回收方式</param>
        /// <param name="recycleNote">回收说明</param>
        /// <returns>是否成功</returns>
        [HttpPut("{obsoleteRecordId}/recycle")]
        [TaktLog("回收ISO文档")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> RecycleObsoleteAsync(long obsoleteRecordId, [FromQuery] int recycleMethod, [FromQuery] string? recycleNote)
        {
            var result = await _obsoleteService.RecycleObsoleteAsync(obsoleteRecordId, recycleMethod, recycleNote);
            return Success(result, _localization.L("IsoDocumentObsolete.Recycle.Success"));
        }

        /// <summary>
        /// 销毁作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="destroyMethod">销毁方式</param>
        /// <param name="destroyNote">销毁说明</param>
        /// <returns>是否成功</returns>
        [HttpPut("{obsoleteRecordId}/destroy")]
        [TaktLog("销毁ISO文档")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> DestroyObsoleteAsync(long obsoleteRecordId, [FromQuery] int destroyMethod, [FromQuery] string? destroyNote)
        {
            var result = await _obsoleteService.DestroyObsoleteAsync(obsoleteRecordId, destroyMethod, destroyNote);
            return Success(result, _localization.L("IsoDocumentObsolete.Destroy.Success"));
        }

        /// <summary>
        /// 归档作废文档
        /// </summary>
        /// <param name="obsoleteRecordId">作废记录ID</param>
        /// <param name="archiveLocation">归档位置</param>
        /// <param name="archiveNote">归档说明</param>
        /// <returns>是否成功</returns>
        [HttpPut("{obsoleteRecordId}/archive")]
        [TaktLog("归档ISO文档")]
        [TaktPerm("routine:iso:obsolete:update")]
        public async Task<IActionResult> ArchiveObsoleteAsync(long obsoleteRecordId, [FromQuery] string archiveLocation, [FromQuery] string? archiveNote)
        {
            var result = await _obsoleteService.ArchiveObsoleteAsync(obsoleteRecordId, archiveLocation, archiveNote);
            return Success(result, _localization.L("IsoDocumentObsolete.Archive.Success"));
        }

        /// <summary>
        /// 获取待审批的作废申请列表
        /// </summary>
        /// <returns>待审批列表</returns>
        [HttpGet("pending-approval")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetPendingApprovalListAsync()
        {
            var result = await _obsoleteService.GetPendingApprovalListAsync();
            return Success(result);
        }

        /// <summary>
        /// 获取我的作废申请列表
        /// </summary>
        /// <param name="applicantId">申请人ID</param>
        /// <returns>我的申请列表</returns>
        [HttpGet("my-applications/{applicantId}")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetMyApplicationsAsync(long applicantId)
        {
            var result = await _obsoleteService.GetMyApplicationsAsync(applicantId);
            return Success(result);
        }

        /// <summary>
        /// 获取作废统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:obsolete:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _obsoleteService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




