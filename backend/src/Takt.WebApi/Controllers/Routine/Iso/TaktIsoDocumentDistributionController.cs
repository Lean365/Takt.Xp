//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocumentDistributionController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文档分发记录控制器
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Application.Services.Routine.Iso;
using Microsoft.AspNetCore.Mvc;

namespace Takt.WebApi.Controllers.Routine.Iso
{
    /// <summary>
    /// ISO文档分发记录控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    [Route("api/[controller]", Name = "ISO文档分发记录")]
    [ApiController]
    [ApiModule("routine", "ISO文档分发管理")]
    public class TaktIsoDocumentDistributionController : TaktBaseController
    {
        private readonly ITaktIsoDocumentDistributionService _distributionService;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentDistributionController(
            ITaktIsoDocumentDistributionService distributionService,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _distributionService = distributionService;
        }

        /// <summary>
        /// 获取分发记录分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分发记录分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:iso:distribution:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktIsoDocumentDistributionQueryDto query)
        {
            var result = await _distributionService.GetListAsync(query);
            return Success(result, _localization.L("IsoDocumentDistribution.List.Success"));
        }

        /// <summary>
        /// 获取分发记录详情
        /// </summary>
        /// <param name="id">分发记录ID</param>
        /// <returns>分发记录详情</returns>
        [HttpGet("{id}")]
        [TaktPerm("routine:iso:distribution:query")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _distributionService.GetByIdAsync(id);
            return Success(result, _localization.L("IsoDocumentDistribution.Get.Success"));
        }

        /// <summary>
        /// 创建分发记录
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>分发记录ID</returns>
        [HttpPost]
        [TaktLog("创建ISO文档分发记录")]
        [TaktPerm("routine:iso:distribution:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktIsoDocumentDistributionCreateDto input)
        {
            var result = await _distributionService.CreateAsync(input);
            return Success(result, _localization.L("IsoDocumentDistribution.Insert.Success"));
        }

        /// <summary>
        /// 更新分发记录
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktLog("更新ISO文档分发记录")]
        [TaktPerm("routine:iso:distribution:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktIsoDocumentDistributionUpdateDto input)
        {
            var result = await _distributionService.UpdateAsync(input);
            return Success(result, _localization.L("IsoDocumentDistribution.Update.Success"));
        }

        /// <summary>
        /// 删除分发记录
        /// </summary>
        /// <param name="id">分发记录ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{id}")]
        [TaktLog("删除ISO文档分发记录")]
        [TaktPerm("routine:iso:distribution:delete")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _distributionService.DeleteAsync(id);
            return Success(result, _localization.L("IsoDocumentDistribution.Delete.Success"));
        }

        /// <summary>
        /// 批量删除分发记录
        /// </summary>
        /// <param name="ids">分发记录ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:iso:distribution:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] ids)
        {
            var result = await _distributionService.BatchDeleteAsync(ids);
            return Success(result, _localization.L("IsoDocumentDistribution.BatchDelete.Success"));
        }

        /// <summary>
        /// 获取分发记录选项列表
        /// </summary>
        /// <returns>分发记录选项列表</returns>
        [HttpGet("options")]
        [TaktPerm("routine:iso:distribution:query")]
        public async Task<IActionResult> GetOptionsAsync()
        {
            var result = await _distributionService.GetOptionsAsync();
            return Success(result);
        }

        /// <summary>
        /// 根据ISO文档ID获取分发记录列表
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <returns>分发记录列表</returns>
        [HttpGet("by-document/{documentId}")]
        [TaktPerm("routine:iso:distribution:query")]
        public async Task<IActionResult> GetByDocumentIdAsync(long documentId)
        {
            var result = await _distributionService.GetByDocumentIdAsync(documentId);
            return Success(result);
        }

        /// <summary>
        /// 批量创建分发记录
        /// </summary>
        /// <param name="documentId">ISO文档ID</param>
        /// <param name="documentVersion">文档版本号</param>
        /// <param name="userIds">用户ID列表</param>
        /// <param name="distributionType">分发对象类型</param>
        /// <param name="distributionMethod">分发方式</param>
        /// <param name="expireDate">过期日期</param>
        /// <param name="isForced">是否强制分发</param>
        /// <returns>创建结果</returns>
        [HttpPost("batch-create")]
        [TaktLog("批量创建ISO文档分发记录")]
        [TaktPerm("routine:iso:distribution:create")]
        public async Task<IActionResult> BatchCreateAsync(long documentId, string documentVersion, [FromBody] long[] userIds, [FromQuery] int distributionType, [FromQuery] int distributionMethod, [FromQuery] DateTime? expireDate, [FromQuery] bool isForced)
        {
            var result = await _distributionService.BatchCreateAsync(documentId, documentVersion, userIds, distributionType, distributionMethod, expireDate, isForced);
            return Success(result, _localization.L("IsoDocumentDistribution.BatchCreate.Success"));
        }

        /// <summary>
        /// 接收文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="receiveNote">接收说明</param>
        /// <returns>是否成功</returns>
        [HttpPut("{distributionRecordId}/receive")]
        [TaktLog("接收ISO文档分发")]
        [TaktPerm("routine:iso:distribution:update")]
        public async Task<IActionResult> ReceiveAsync(long distributionRecordId, [FromQuery] string? receiveNote)
        {
            var result = await _distributionService.ReceiveAsync(distributionRecordId, receiveNote);
            return Success(result, _localization.L("IsoDocumentDistribution.Receive.Success"));
        }

        /// <summary>
        /// 确认文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{distributionRecordId}/confirm")]
        [TaktLog("确认ISO文档分发")]
        [TaktPerm("routine:iso:distribution:update")]
        public async Task<IActionResult> ConfirmAsync(long distributionRecordId)
        {
            var result = await _distributionService.ConfirmAsync(distributionRecordId);
            return Success(result, _localization.L("IsoDocumentDistribution.Confirm.Success"));
        }

        /// <summary>
        /// 拒绝文档
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <param name="rejectReason">拒绝原因</param>
        /// <returns>是否成功</returns>
        [HttpPut("{distributionRecordId}/reject")]
        [TaktLog("拒绝ISO文档分发")]
        [TaktPerm("routine:iso:distribution:update")]
        public async Task<IActionResult> RejectAsync(long distributionRecordId, [FromQuery] string rejectReason)
        {
            var result = await _distributionService.RejectAsync(distributionRecordId, rejectReason);
            return Success(result, _localization.L("IsoDocumentDistribution.Reject.Success"));
        }

        /// <summary>
        /// 标记为已读
        /// </summary>
        /// <param name="distributionRecordId">分发记录ID</param>
        /// <returns>是否成功</returns>
        [HttpPut("{distributionRecordId}/mark-read")]
        [TaktLog("标记ISO文档分发为已读")]
        [TaktPerm("routine:iso:distribution:update")]
        public async Task<IActionResult> MarkAsReadAsync(long distributionRecordId)
        {
            var result = await _distributionService.MarkAsReadAsync(distributionRecordId);
            return Success(result, _localization.L("IsoDocumentDistribution.MarkRead.Success"));
        }

        /// <summary>
        /// 获取用户未读分发记录数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>未读数量</returns>
        [HttpGet("unread-count/{userId}")]
        [TaktPerm("routine:iso:distribution:query")]
        public async Task<IActionResult> GetUnreadCountAsync(long userId)
        {
            var result = await _distributionService.GetUnreadCountAsync(userId);
            return Success(result);
        }

        /// <summary>
        /// 获取分发记录统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        [HttpGet("statistics")]
        [TaktPerm("routine:iso:distribution:query")]
        public async Task<IActionResult> GetStatisticsAsync()
        {
            var result = await _distributionService.GetStatisticsAsync();
            return Success(result);
        }
    }
}




