//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAdvertService.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 广告服务实现类
//===================================================================

#nullable enable

using Takt.Application.Dtos.Routine.Advertisement;
using Takt.Domain.Entities.Routine.Advert;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 广告服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktAdvertService : TaktBaseService, ITaktAdvertService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取广告仓储
        /// </summary>
        private ITaktRepository<TaktAdvert> AdvertRepository => _repositoryFactory.GetBusinessRepository<TaktAdvert>();

        /// <summary>
        /// 获取广告分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktAdvertDto>> GetListAsync(TaktAdvertQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await AdvertRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            return new TaktPagedResult<TaktAdvertDto>
            {
                Rows = result.Rows.Adapt<List<TaktAdvertDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取广告详情
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>广告详情</returns>
        public async Task<TaktAdvertDto> GetByIdAsync(long advertId)
        {
            var entity = await AdvertRepository.GetByIdAsync(advertId);
            if (entity == null)
            {
                throw new TaktException(L("Advert.NotFound"));
            }

            return entity.Adapt<TaktAdvertDto>();
        }

        /// <summary>
        /// 创建广告
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>广告ID</returns>
        public async Task<long> CreateAsync(TaktAdvertCreateDto input)
        {
            var entity = input.Adapt<TaktAdvert>();

            // 设置基础字段
            entity.CreateBy = _currentUser.UserName;
            entity.CreateTime = DateTime.Now;
            entity.Status = 0; // 草稿状态
            entity.AuditStatus = 0; // 待审核状态

            var result = await AdvertRepository.CreateAsync(entity);
            return result;
        }

        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktAdvertUpdateDto input)
        {
            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId);
            if (entity == null)
            {
                throw new TaktException(L("Advert.NotFound"));
            }

            // 更新字段
            input.Adapt(entity);
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="advertId">广告ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long advertId)
        {
            var entity = await AdvertRepository.GetByIdAsync(advertId);
            if (entity == null)
            {
                throw new TaktException(L("Advert.NotFound"));
            }

            // 软删除
            entity.IsDeleted = 1;
            entity.DeleteBy = _currentUser.UserName;
            entity.DeleteTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 批量删除广告
        /// </summary>
        /// <param name="ids">广告ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            var entities = await AdvertRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!entities.Any())
            {
                throw new TaktException(L("Advert.NotFound"));
            }

            foreach (var entity in entities)
            {
                entity.IsDeleted = 1;
                entity.DeleteBy = _currentUser.UserName;
                entity.DeleteTime = DateTime.Now;
            }

            var result = await AdvertRepository.UpdateRangeAsync(entities);
            return result > 0;
        }

        // TODO: 其他方法待实现
        /// <summary>
        /// 导入广告数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
        {
            try
            {
                var actualSheetName = sheetName ?? "Advert";
                var adverts = await TaktExcelHelper.ImportAsync<TaktAdvertImportDto>(fileStream, actualSheetName);
                if (!adverts.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var advert in adverts)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(advert.AdvertTitle))
                        {
                            _logger.Warn("导入广告失败: 广告标题不能为空");
                            fail++;
                            continue;
                        }

                        // 校验广告标题是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(AdvertRepository, "AdvertTitle", advert.AdvertTitle);

                        var entity = advert.Adapt<TaktAdvert>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.Status = 0; // 草稿状态
                        entity.AuditStatus = 0; // 待审核状态

                        var result = await AdvertRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("导入广告失败: {Error}", ex.Message);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入广告异常: {Error}", ex.Message);
                throw new TaktException(L("Advert.Import.Error", ex.Message));
            }
        }

        /// <summary>
        /// 导出广告数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktAdvertQueryDto query, string? sheetName, string? fileName)
        {
            var list = await AdvertRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktAdvertExportDto>>();

            // 使用传入的参数，如果为空则使用默认值
            var actualSheetName = sheetName ?? "Advert";
            var actualFileName = fileName ?? "AdvertData";

            return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
        }

        /// <summary>
        /// 获取广告导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
        {
            var actualSheetName = sheetName ?? "Advert";
            var actualFileName = fileName ?? "AdvertTemplate";
            return await TaktExcelHelper.GenerateTemplateAsync<TaktAdvertTemplateDto>(actualSheetName, actualFileName);
        }

        /// <summary>
        /// 更新广告状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktAdvertStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId)
                ?? throw new TaktException(L("Advert.NotFound"));

            entity.Status = input.Status;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("Advert.Update.Failed"));

            return true;
        }

        /// <summary>
        /// 审核广告
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AuditAsync(TaktAdvertAuditDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId)
                ?? throw new TaktException(L("Advert.NotFound"));

            entity.AuditStatus = input.AuditStatus;
            entity.AuditComments = input.AuditComments;
            entity.AuditedBy = _currentUser.UserName;
            entity.AuditedTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("Advert.Logging.Failed"));

            return true;
        }

        /// <summary>
        /// 发布广告
        /// </summary>
        /// <param name="input">发布对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> PublishAsync(TaktAdvertPublishDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId)
                ?? throw new TaktException(L("Advert.NotFound"));

            entity.Status = 1; // 已发布
            entity.PublishTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("Advert.Publish.Failed"));

            return true;
        }

        /// <summary>
        /// 下线广告
        /// </summary>
        /// <param name="input">下线对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> OfflineAsync(TaktAdvertOfflineDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId)
                ?? throw new TaktException(L("Advert.NotFound"));

            entity.Status = 2; // 已下线
            entity.OfflineTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("Advert.Offline.Failed"));

            return true;
        }

        /// <summary>
        /// 更新广告统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatsAsync(TaktAdvertStatsDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertRepository.GetByIdAsync(input.AdvertId)
                ?? throw new TaktException(L("Advert.NotFound"));

            input.Adapt(entity);
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("Advert.UpdateStats.Failed"));

            return true;
        }

        /// <summary>
        /// 获取广告统计信息
        /// </summary>
        /// <returns>广告统计信息</returns>
        public async Task<TaktAdvertStatisticsDto> GetStatisticsAsync()
        {
            var allAdverts = await AdvertRepository.GetListAsync(x => x.IsDeleted == 0);
            var publishedAdverts = allAdverts.Where(x => x.Status == 1).ToList();
            var draftAdverts = allAdverts.Where(x => x.Status == 0).ToList();
            var offlineAdverts = allAdverts.Where(x => x.Status == 2).ToList();

            return new TaktAdvertStatisticsDto
            {
                TotalCount = allAdverts.Count,
                PublishedCount = publishedAdverts.Count,
                DraftCount = draftAdverts.Count,
                OfflineCount = offlineAdverts.Count,
                PendingAuditCount = allAdverts.Count(x => x.AuditStatus == 0),
                AuditPassedCount = allAdverts.Count(x => x.AuditStatus == 1),
                AuditRejectedCount = allAdverts.Count(x => x.AuditStatus == 2)
            };
        }

        /// <summary>
        /// 获取广告模板列表
        /// </summary>
        /// <returns>广告模板列表</returns>
        public async Task<List<TaktAdvertTemplateDto>> GetTemplateListAsync()
        {
            // 这里应该从模板表获取，暂时返回空列表
            return new List<TaktAdvertTemplateDto>();
        }

        public Task<long> CreateFromTemplateAsync(long templateId) => throw new NotImplementedException();

        public Task<long> CopyAsync(long advertId) => throw new NotImplementedException();

        public Task<List<TaktAdvertDto>> GetFrontendListAsync(string position, int limit = 10) => throw new NotImplementedException();

        public Task<bool> RecordClickAsync(long advertId) => throw new NotImplementedException();

        public Task<bool> RecordShowAsync(long advertId) => throw new NotImplementedException();

        public Task<bool> RecordCloseAsync(long advertId) => throw new NotImplementedException();

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktAdvert, bool>> QueryExpression(TaktAdvertQueryDto query)
        {
            var exp = Expressionable.Create<TaktAdvert>();

            // 基础条件
            exp = exp.And(x => x.IsDeleted == 0);

            // 公司代码
            if (!string.IsNullOrEmpty(query.CompanyCode))
            {
                exp = exp.And(x => x.CompanyCode == query.CompanyCode);
            }

            // 广告标题
            if (!string.IsNullOrEmpty(query.AdvertTitle))
            {
                exp = exp.And(x => x.AdvertTitle.Contains(query.AdvertTitle));
            }

            // 广告副标题
            if (!string.IsNullOrEmpty(query.AdvertSubtitle))
            {
                exp = exp.And(x => x.AdvertSubtitle != null && x.AdvertSubtitle.Contains(query.AdvertSubtitle));
            }

            // 广告类型
            if (query.AdvertType.HasValue)
            {
                exp = exp.And(x => x.AdvertType == query.AdvertType.Value);
            }

            // 广告位置
            if (!string.IsNullOrEmpty(query.AdvertPosition))
            {
                exp = exp.And(x => x.AdvertPosition == query.AdvertPosition);
            }

            // 广告状态
            if (query.Status.HasValue)
            {
                exp = exp.And(x => x.Status == query.Status.Value);
            }

            // 广告审核状态
            if (query.AuditStatus.HasValue)
            {
                exp = exp.And(x => x.AuditStatus == query.AuditStatus.Value);
            }

            // 是否置顶
            if (query.IsTop.HasValue)
            {
                exp = exp.And(x => x.IsTop == query.IsTop.Value);
            }

            // 是否推荐
            if (query.IsRecommend.HasValue)
            {
                exp = exp.And(x => x.IsRecommend == query.IsRecommend.Value);
            }

            // 是否热门
            if (query.IsHot.HasValue)
            {
                exp = exp.And(x => x.IsHot == query.IsHot.Value);
            }

            // 开始时间
            if (query.StartTime.HasValue)
            {
                exp = exp.And(x => x.StartTime >= query.StartTime.Value);
            }

            // 结束时间
            if (query.EndTime.HasValue)
            {
                exp = exp.And(x => x.EndTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
}