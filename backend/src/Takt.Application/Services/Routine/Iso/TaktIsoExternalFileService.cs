#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoExternalFileService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : ISO外来文件控制服务实现
//===================================================================

using Takt.Application.Dtos.Routine.Iso;
using Takt.Domain.Entities.Routine.Iso;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Models;
using Takt.Shared.Helpers;
using Takt.Domain.Utils;
using Takt.Shared.Enums;
using Takt.Shared.Exceptions;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using System.Linq.Expressions;

namespace Takt.Application.Services.Routine.Iso
{
    /// <summary>
    /// ISO外来文件控制服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktIsoExternalFileService : TaktBaseService, ITaktIsoExternalFileService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktIsoExternalFileService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取ISO外来文件控制仓储
        /// </summary>
        private ITaktRepository<TaktIsoExternalFile> IsoExternalFileRepository => _repositoryFactory.GetBusinessRepository<TaktIsoExternalFile>();

        /// <summary>
        /// 获取外来文件控制分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>外来文件控制分页列表</returns>
        public async Task<TaktPagedResult<TaktIsoExternalFileDto>> GetListAsync(TaktIsoExternalFileQueryDto query)
        {
            query ??= new TaktIsoExternalFileQueryDto();

            _logger.Info($"查询外来文件控制列表，参数：FileCode={query.FileCode}, FileName={query.FileName}, FileSource={query.FileSource}, Status={query.Status}");

            var result = await IsoExternalFileRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            _logger.Info($"查询外来文件控制列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktIsoExternalFileDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktIsoExternalFileDto>>()
            };
        }

        /// <summary>
        /// 获取外来文件控制详情
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>外来文件控制详情</returns>
        public async Task<TaktIsoExternalFileDto> GetByIdAsync(long externalFileControlId)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId);
            return externalFile == null ? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId)) : externalFile.Adapt<TaktIsoExternalFileDto>();
        }

        /// <summary>
        /// 创建外来文件控制
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>外来文件控制ID</returns>
        public async Task<long> CreateAsync(TaktIsoExternalFileCreateDto input)
        {
            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(IsoExternalFileRepository, "FileCode", input.FileCode);

            var externalFile = input.Adapt<TaktIsoExternalFile>();
            externalFile.ReceiveDate = DateTime.Now;
            externalFile.ReceiverBy = _currentUser.UserName ?? string.Empty;
            externalFile.Status = 0; // 待审核

            var result = await IsoExternalFileRepository.CreateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoExternalFile.Created"),
                    Content = L("Routine.IsoExternalFile.CreatedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });

                return externalFile.Id;
            }

            throw new TaktException(L("Routine.IsoExternalFile.CreateFailed"));
        }

        /// <summary>
        /// 更新外来文件控制
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktIsoExternalFileUpdateDto input)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(input.ExternalFileId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", input.ExternalFileId));

            // 验证字段是否已存在
            if (externalFile.FileCode != input.FileCode)
                await TaktValidateUtils.ValidateFieldExistsAsync(IsoExternalFileRepository, "FileCode", input.FileCode, input.ExternalFileId);

            input.Adapt(externalFile);
            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Updated"),
                    Content = L("Routine.IsoExternalFile.UpdatedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 删除外来文件控制
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long externalFileControlId)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            var result = await IsoExternalFileRepository.DeleteAsync(externalFileControlId);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Deleted"),
                    Content = L("Routine.IsoExternalFile.DeletedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除外来文件控制
        /// </summary>
        /// <param name="externalFileControlIds">外来文件控制ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] externalFileControlIds)
        {
            if (externalFileControlIds == null || externalFileControlIds.Length == 0) return false;

            // 获取要删除的文件信息用于通知
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => externalFileControlIds.Contains(x.Id));

            var result = await IsoExternalFileRepository.DeleteRangeAsync(externalFileControlIds.Cast<object>().ToList());

            if (result > 0 && externalFiles.Any())
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.BatchDeleted"),
                    Content = L("Routine.IsoExternalFile.BatchDeletedContent", externalFiles.Count),
                    Timestamp = DateTime.Now,
                    Data = externalFiles
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 导出外来文件控制数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktIsoExternalFileQueryDto query, string? sheetName, string? fileName)
        {
            var externalFiles = await IsoExternalFileRepository.GetListAsync(QueryExpression(query));
            var exportData = externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
            var actualSheetName = sheetName ?? "外来文件控制";
            var actualFileName = fileName ?? "外来文件控制数据";
            var result = await TaktExcelHelper.ExportAsync(exportData, actualSheetName, actualFileName);
            return (result.fileName, result.content);
        }

        /// <summary>
        /// 获取外来文件控制选项列表
        /// </summary>
        /// <returns>外来文件控制选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => x.Status == 2); // 已批准
            return externalFiles.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.FileName} ({x.FileCode})",
                DictValue = x.Id,
                ExtLabel = x.FileCode,
                ExtValue = x.FileVersion,
                OrderNum = 0,
                Status = x.Status
            }).ToList();
        }

        /// <summary>
        /// 审核外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="reviewComment">审核意见</param>
        /// <param name="isApproved">是否通过</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ReviewAsync(long externalFileControlId, string? reviewComment, bool isApproved)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.ReviewDate = DateTime.Now;
            externalFile.ReviewerBy = _currentUser.UserName ?? string.Empty;
            externalFile.ReviewComment = reviewComment;
            externalFile.Status = isApproved ? 1 : 3; // 已审核或已拒绝

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Reviewed"),
                    Content = L("Routine.IsoExternalFile.ReviewedContent", externalFile.FileName, isApproved ? "通过" : "拒绝"),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 批准外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="approvalComment">批准意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveAsync(long externalFileControlId, string? approvalComment)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.ApprovalDate = DateTime.Now;
            externalFile.ApproverBy = _currentUser.UserName ?? string.Empty;
            externalFile.ApprovalComment = approvalComment;
            externalFile.Status = 2; // 已批准
            externalFile.EffectiveDate = DateTime.Now;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Approved"),
                    Content = L("Routine.IsoExternalFile.ApprovedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 拒绝外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="rejectionReason">拒绝原因</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RejectAsync(long externalFileControlId, string rejectionReason)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.Status = 3; // 已拒绝
            externalFile.ReviewComment = rejectionReason; // 使用审核意见字段存储拒绝原因
            externalFile.ReviewDate = DateTime.Now;
            externalFile.ReviewerBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Rejected"),
                    Content = L("Routine.IsoExternalFile.RejectedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 分发外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="distributionScope">分发范围</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DistributeAsync(long externalFileControlId, string distributionScope)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.DistributionScope = distributionScope;
            externalFile.IsDistributed = true;
            externalFile.DistributionDate = DateTime.Now;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.Distributed"),
                    Content = L("Routine.IsoExternalFile.DistributedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 通知相关人员
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> NotifyStakeholdersAsync(long externalFileControlId)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.IsNotified = true;
            externalFile.NotifyDate = DateTime.Now;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoExternalFile.Notified"),
                    Content = L("Routine.IsoExternalFile.NotifiedContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 更新外来文件
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="newVersion">新版本号</param>
        /// <param name="newFilePath">新文件路径</param>
        /// <param name="newFileSize">新文件大小</param>
        /// <param name="newFileMd5">新文件MD5</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateFileAsync(long externalFileControlId, string newVersion, string newFilePath, long newFileSize, string? newFileMd5)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.FileVersion = newVersion;
            externalFile.FilePath = newFilePath;
            externalFile.FileSize = newFileSize;
            externalFile.FileMd5 = newFileMd5;
            externalFile.UpdateTime = DateTime.Now;
            externalFile.UpdateBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.FileUpdated"),
                    Content = L("Routine.IsoExternalFile.FileUpdatedContent", externalFile.FileName, newVersion),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 检查文件更新
        /// </summary>
        /// <returns>需要更新的文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> CheckFileUpdatesAsync()
        {
            var today = DateTime.Today;
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => 
                x.NextUpdateDate.HasValue && 
                x.NextUpdateDate.Value.Date <= today && 
                x.Status == 2); // 已批准

            return externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
        }

        /// <summary>
        /// 获取待审核文件列表
        /// </summary>
        /// <returns>待审核文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetPendingReviewFilesAsync()
        {
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => x.Status == 0); // 待审核
            return externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
        }

        /// <summary>
        /// 获取待批准文件列表
        /// </summary>
        /// <returns>待批准文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetPendingApprovalFilesAsync()
        {
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => x.Status == 1); // 已审核
            return externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
        }

        /// <summary>
        /// 获取即将失效文件列表
        /// </summary>
        /// <param name="days">提前天数</param>
        /// <returns>即将失效文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetExpiringFilesAsync(int days = 30)
        {
            var expiryDate = DateTime.Today.AddDays(days);
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => 
                x.ExpiryDate.HasValue && 
                x.ExpiryDate.Value.Date <= expiryDate && 
                x.Status == 2); // 已批准

            return externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
        }

        /// <summary>
        /// 根据文件代码获取外来文件
        /// </summary>
        /// <param name="fileCode">文件代码</param>
        /// <returns>外来文件详情</returns>
        public async Task<TaktIsoExternalFileDto> GetByFileCodeAsync(string fileCode)
        {
            var externalFile = await IsoExternalFileRepository.GetFirstAsync(x => x.FileCode == fileCode);
            return externalFile == null ? throw new TaktException(L("Routine.IsoExternalFile.NotFoundByCode", fileCode)) : externalFile.Adapt<TaktIsoExternalFileDto>();
        }

        /// <summary>
        /// 提交审核
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="reviewComment">审核意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SubmitReviewAsync(long externalFileControlId, string? reviewComment)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.Status = 0; // 待审核
            externalFile.ReviewComment = reviewComment;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentNotification,
                    Title = L("Routine.IsoExternalFile.SubmittedForReview"),
                    Content = L("Routine.IsoExternalFile.SubmittedForReviewContent", externalFile.FileName),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 更新文件状态
        /// </summary>
        /// <param name="externalFileControlId">外来文件控制ID</param>
        /// <param name="status">状态</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(long externalFileControlId, int status)
        {
            var externalFile = await IsoExternalFileRepository.GetByIdAsync(externalFileControlId)
                ?? throw new TaktException(L("Routine.IsoExternalFile.NotFound", externalFileControlId));

            externalFile.Status = status;
            externalFile.UpdateTime = DateTime.Now;
            externalFile.UpdateBy = _currentUser.UserName ?? string.Empty;

            var result = await IsoExternalFileRepository.UpdateAsync(externalFile);

            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceivePersonalNotice(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.IsoDocumentStatusUpdate,
                    Title = L("Routine.IsoExternalFile.StatusUpdated"),
                    Content = L("Routine.IsoExternalFile.StatusUpdatedContent", externalFile.FileName, status),
                    Timestamp = DateTime.Now,
                    Data = externalFile
                });
            }

            return result > 0;
        }

        /// <summary>
        /// 获取待审核文件列表
        /// </summary>
        /// <returns>待审核文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetPendingReviewListAsync()
        {
            return await GetPendingReviewFilesAsync();
        }

        /// <summary>
        /// 获取待审批文件列表
        /// </summary>
        /// <returns>待审批文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetPendingApprovalListAsync()
        {
            return await GetPendingApprovalFilesAsync();
        }

        /// <summary>
        /// 获取我的外来文件列表
        /// </summary>
        /// <param name="receiverId">接收人ID</param>
        /// <returns>我的外来文件列表</returns>
        public async Task<List<TaktIsoExternalFileDto>> GetMyFilesAsync(long receiverId)
        {
            var externalFiles = await IsoExternalFileRepository.GetListAsync(x => x.ReceiverBy == _currentUser.UserName);
            return externalFiles.Adapt<List<TaktIsoExternalFileDto>>();
        }

        /// <summary>
        /// 获取外来文件统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetStatisticsAsync()
        {
            var totalCount = await IsoExternalFileRepository.GetCountAsync();
            var pendingReviewCount = await IsoExternalFileRepository.GetCountAsync(x => x.Status == 0);
            var approvedCount = await IsoExternalFileRepository.GetCountAsync(x => x.Status == 2);
            var rejectedCount = await IsoExternalFileRepository.GetCountAsync(x => x.Status == 3);
            var distributedCount = await IsoExternalFileRepository.GetCountAsync(x => x.IsDistributed);
            var expiringCount = await IsoExternalFileRepository.GetCountAsync(x => 
                x.ExpiryDate.HasValue && 
                x.ExpiryDate.Value.Date <= DateTime.Today.AddDays(30) && 
                x.Status == 2);

            return new
            {
                TotalCount = totalCount,
                PendingReviewCount = pendingReviewCount,
                ApprovedCount = approvedCount,
                RejectedCount = rejectedCount,
                DistributedCount = distributedCount,
                ExpiringCount = expiringCount
            };
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktIsoExternalFile, bool>> QueryExpression(TaktIsoExternalFileQueryDto query)
        {
            return Expressionable.Create<TaktIsoExternalFile>()
                .AndIF(!string.IsNullOrEmpty(query.FileCode), x => x.FileCode!.Contains(query.FileCode!))
                .AndIF(!string.IsNullOrEmpty(query.FileName), x => x.FileName!.Contains(query.FileName!))
                .AndIF(!string.IsNullOrEmpty(query.FileVersion), x => x.FileVersion!.Contains(query.FileVersion!))
                .AndIF(query.FileSource.HasValue, x => x.FileSource == query.FileSource!.Value)
                .AndIF(!string.IsNullOrEmpty(query.SourceOrganization), x => x.SourceOrganization!.Contains(query.SourceOrganization!))
                .AndIF(query.FileType.HasValue, x => x.FileType == query.FileType!.Value)
                .AndIF(!string.IsNullOrEmpty(query.FileCategory), x => x.FileCategory!.Contains(query.FileCategory!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ReceiverBy), x => x.ReceiverBy!.Contains(query.ReceiverBy!))
                .AndIF(!string.IsNullOrEmpty(query.ReviewerBy), x => x.ReviewerBy!.Contains(query.ReviewerBy!))
                .AndIF(!string.IsNullOrEmpty(query.ApproverBy), x => x.ApproverBy!.Contains(query.ApproverBy!))
                .AndIF(query.StartReceiveDate.HasValue, x => x.ReceiveDate >= query.StartReceiveDate!.Value)
                .AndIF(query.EndReceiveDate.HasValue, x => x.ReceiveDate <= query.EndReceiveDate!.Value)
                .AndIF(query.StartReviewDate.HasValue, x => x.ReviewDate >= query.StartReviewDate!.Value)
                .AndIF(query.EndReviewDate.HasValue, x => x.ReviewDate <= query.EndReviewDate!.Value)
                .AndIF(query.StartApprovalDate.HasValue, x => x.ApprovalDate >= query.StartApprovalDate!.Value)
                .AndIF(query.EndApprovalDate.HasValue, x => x.ApprovalDate <= query.EndApprovalDate!.Value)
                .AndIF(query.StartEffectiveDate.HasValue, x => x.EffectiveDate >= query.StartEffectiveDate!.Value)
                .AndIF(query.EndEffectiveDate.HasValue, x => x.EffectiveDate <= query.EndEffectiveDate!.Value)
                .AndIF(query.StartExpiryDate.HasValue, x => x.ExpiryDate >= query.StartExpiryDate!.Value)
                .AndIF(query.EndExpiryDate.HasValue, x => x.ExpiryDate <= query.EndExpiryDate!.Value)
                .ToExpression();
        }
    }
}





