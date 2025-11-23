//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentAccessController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档查阅记录控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO文档查阅记录控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档查阅记录")]
    [ApiController]
    [ApiModule("routine", "ISO文档查阅管理")]
    public class TaktIsoDocumentAccessController : TaktBaseController
    {
        private readonly ITaktIsoDocumentAccessService _accessService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentAccessController(
            ITaktIsoDocumentAccessService accessService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _accessService = accessService;
        }

        /// <summary>
        /// 获取查阅记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查阅记录分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:access:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoDocumentAccessQueryDto query)
        {
            var result = await _accessService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocumentAccess.List.Success"));
        }

        /// <summary>
        /// 获取查阅记录详情
        /// </summary>
        /// <param name="id">查阅记录ID</param>
        /// <returns>查阅记录详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _accessService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocumentAccess.Get.Success"));
        }

        /// <summary>
        /// 创建查阅记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>查阅记录ID</returns>
        [HttpPost]
        [TaktLog("创建ISO文档查阅记录")]
        [TaktPerm("routine:iso:access:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoDocumentAccessCreateDto input)
        {
            var result = await _accessService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocumentAccess.Insert.Success"));
        }

        /// <summary>
        /// 更新查阅记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO文档查阅记录")]
        [TaktPerm("routine:iso:access:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoDocumentAccessUpdateDto input)
        {
            var result = await _accessService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocumentAccess.Update.Success"));
        }

        /// <summary>
        /// 删除查阅记录
        /// </summary>
        /// <param name="id">查阅记录ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO文档查阅记录")]
        [TaktPerm("routine:iso:access:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _accessService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocumentAccess.Delete.Success"));
        }

        /// <summary>
        /// 批量删除查阅记录
        /// </summary>
        /// <param name="ids">查阅记录ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:access:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _accessService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocumentAccess.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取查阅记录选项列表
        /// </summary>
        /// <returns>查阅记录选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _accessService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据ISO文档ID获取查阅记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>查阅记录列表</returns>
        [HttpGet("by-document/{documentId}")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetByDocumentIdAsync(long documentId)
        {
            var result = await _accessService.GetByDocumentIdAsync(documentId);
            return Success(result);
        }

        /// <summary>
        /// 记录文件查阅
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="accessType">查阅类型</param>
        /// <param name="accessPurpose">查阅目的</param>
        /// <returns>查阅记录ID</returns>
        [HttpPost("record")]
        [TaktLog("记录ISO文档查阅")]
        [TaktPerm("routine:iso:access:create")]
        public async Task<IActionResult> RecordAccessAsync(long documentId, string documentVersion, int accessType, string? accessPurpose)
        {
            var result = await _accessService.RecordAccessAsync(documentId, documentVersion, accessType, accessPurpose);
            return Success(result, _localization.L("IsoDocumentAccess.Record.Success"));
        }

        /// <summary>
        /// 更新查阅时长
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="duration">查阅时长（秒）</param>
        /// <returns>是否成功</returns>
        [HttpPut("{accessRecordId}/duration")]
        [TaktLog("更新ISO文档查阅时长")]
        [TaktPerm("routine:iso:access:update")]
        public async Task<IActionResult> UpdateAccessDurationAsync(long accessRecordId, int duration)
        {
            var result = await _accessService.UpdateAccessDurationAsync(accessRecordId, duration);
            return Success(result, _localization.L("IsoDocumentAccess.Duration.Success"));
        }

        /// <summary>
        /// 确认查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="confirmComment">确认意见</param>
        /// <returns>是否成功</returns>
        [HttpPut("{accessRecordId}/confirm")]
        [TaktLog("确认ISO文档查阅")]
        [TaktPerm("routine:iso:access:update")]
        public async Task<IActionResult> ConfirmAccessAsync(long accessRecordId, string? confirmComment)
        {
            var result = await _accessService.ConfirmAccessAsync(accessRecordId, confirmComment);
            return Success(result, _localization.L("IsoDocumentAccess.Confirm.Success"));
        }

        /// <summary>
        /// 评价查阅
        /// </summary>
        /// <param name="accessRecordId">查阅记录ID</param>
        /// <param name="rating">评价等级</param>
        /// <param name="ratingComment">评价内容</param>
        /// <returns>是否成功</returns>
        [HttpPut("{accessRecordId}/rate")]
        [TaktLog("评价ISO文档查阅")]
        [TaktPerm("routine:iso:access:update")]
        public async Task<IActionResult> RateAccessAsync(long accessRecordId, int rating, string? ratingComment)
        {
            var result = await _accessService.RateAccessAsync(accessRecordId, rating, ratingComment);
            return Success(result, _localization.L("IsoDocumentAccess.Rate.Success"));
        }

        /// <summary>
        /// 获取用户查阅统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        [HttpGet("user-statistics/{userId}")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetUserAccessStatisticsAsync(long userId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _accessService.GetUserAccessStatisticsAsync(userId, startDate, endDate);
            return Success(result);
        }

        /// <summary>
        /// 获取文档查阅统计
        /// </summary>
        /// <param name="documentId">文档ID</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>查阅统计信息</returns>
        [HttpGet("document-statistics/{documentId}")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetDocumentAccessStatisticsAsync(long documentId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var result = await _accessService.GetDocumentAccessStatisticsAsync(documentId, startDate, endDate);
            return Success(result);
        }

        /// <summary>
        /// 获取热门文档列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>热门文档列表</returns>
        [HttpGet("popular-documents")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetPopularDocumentsAsync([FromQuery] int topCount = 10, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var result = await _accessService.GetPopularDocumentsAsync(topCount, startDate, endDate);
            return Success(result);
        }

        /// <summary>
        /// 获取活跃用户列表
        /// </summary>
        /// <param name="topCount">返回数量</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns>活跃用户列表</returns>
        [HttpGet("active-users")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetActiveUsersAsync([FromQuery] int topCount = 10, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            var result = await _accessService.GetActiveUsersAsync(topCount, startDate, endDate);
            return Success(result);
        }

        /// <summary>
        /// 获取查阅记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:access:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _accessService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




