//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktAdvertBillingService.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 广告计费服务实现类
//===================================================================

#nullable enable

using Takt.Application.Dtos.Routine.Advertisement;
using Takt.Domain.Entities.Routine.Advert;
using Microsoft.AspNetCore.Http;
using Mapster;
using SqlSugar;
using Takt.Shared.Models;
using Takt.Shared.Exceptions;
using System.Linq.Expressions;
using Takt.Domain.IServices.Security;
using Takt.Shared.Utils;
using Takt.Shared.Helpers;
using Takt.Shared.Extensions;

namespace Takt.Application.Services.Routine.Advertisement
{
    /// <summary>
    /// 广告计费服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-19
    /// </remarks>
    public class TaktAdvertBillingService : TaktBaseService, ITaktAdvertBillingService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertBillingService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取广告计费仓储
        /// </summary>
        private ITaktRepository<TaktAdvertBilling> AdvertBillingRepository => _repositoryFactory.GetBusinessRepository<TaktAdvertBilling>();

        /// <summary>
        /// 获取广告计费分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktAdvertBillingDto>> GetListAsync(TaktAdvertBillingQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await AdvertBillingRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            return new TaktPagedResult<TaktAdvertBillingDto>
            {
                Rows = result.Rows.Adapt<List<TaktAdvertBillingDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取广告计费详情
        /// </summary>
        /// <param name="billingId">广告计费ID</param>
        /// <returns>广告计费详情</returns>
        public async Task<TaktAdvertBillingDto> GetByIdAsync(long billingId)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(billingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            return entity.Adapt<TaktAdvertBillingDto>();
        }

        /// <summary>
        /// 创建广告计费
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>广告计费ID</returns>
        public async Task<long> CreateAsync(TaktAdvertBillingCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (input.AdvertId <= 0)
                throw new TaktException(L("AdvertBilling.AdvertId.Required"));

            // 验证广告是否存在
            await TaktValidateUtils.ValidateFieldExistsAsync(AdvertBillingRepository, "AdvertId", input.AdvertId.ToString());

            // 创建广告计费
            var entity = input.Adapt<TaktAdvertBilling>();
            entity.Status = 0; // 默认未开始
            entity.AuditStatus = 0; // 默认待审核
            entity.CreateBy = _currentUser.UserName;
            entity.CreateTime = DateTime.Now;

            var result = await AdvertBillingRepository.CreateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("AdvertBilling.Create.Failed"));

            return entity.Id;
        }

        /// <summary>
        /// 更新广告计费
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktAdvertBillingUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId)
                ?? throw new TaktException(L("AdvertBilling.NotFound"));

            // 更新字段
            input.Adapt(entity);
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("AdvertBilling.Update.Failed"));

            return true;
        }

        /// <summary>
        /// 删除广告计费
        /// </summary>
        /// <param name="billingId">广告计费ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long billingId)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(billingId)
                ?? throw new TaktException(L("AdvertBilling.NotFound"));

            // 软删除
            entity.IsDeleted = 1;
            entity.DeleteBy = _currentUser.UserName;
            entity.DeleteTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            if (result <= 0)
                throw new TaktException(L("AdvertBilling.Delete.Failed"));

            return true;
        }

        /// <summary>
        /// 批量删除广告计费
        /// </summary>
        /// <param name="ids">广告计费ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("AdvertBilling.SelectRequired"));

            var entities = await AdvertBillingRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!entities.Any())
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            foreach (var entity in entities)
            {
                entity.IsDeleted = 1;
                entity.DeleteBy = _currentUser.UserName;
                entity.DeleteTime = DateTime.Now;
            }

            var result = await AdvertBillingRepository.UpdateRangeAsync(entities);
            if (result <= 0)
                throw new TaktException(L("AdvertBilling.BatchDelete.Failed"));

            return true;
        }

        /// <summary>
        /// 获取广告计费导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
        {
            var actualSheetName = sheetName ?? "AdvertBilling";
            var actualFileName = fileName ?? "AdvertBillingTemplate";
            return await TaktExcelHelper.GenerateTemplateAsync<TaktAdvertBillingTemplateDto>(actualSheetName, actualFileName);
        }

        /// <summary>
        /// 导入广告计费数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
        {
            try
            {
                var actualSheetName = sheetName ?? "AdvertBilling";
                var data = await TaktExcelHelper.ImportAsync<TaktAdvertBillingImportDto>(fileStream, actualSheetName);
                if (!data.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var item in data)
                {
                    try
                    {
                        if (item.AdvertId <= 0)
                        {
                            _logger.Warn("导入广告计费数据失败: 广告ID不能为空或无效");
                            fail++;
                            continue;
                        }

                        // 校验广告是否存在
                        try
                        {
                            await TaktValidateUtils.ValidateFieldExistsAsync(AdvertBillingRepository, "AdvertId", item.AdvertId.ToString());
                        }
                        catch
                        {
                            _logger.Warn("导入广告计费数据失败: 广告ID {AdvertId} 的计费信息已存在", item.AdvertId);
                            fail++;
                            continue;
                        }

                        var entity = item.Adapt<TaktAdvertBilling>();
                        entity.CreateBy = _currentUser.UserName;
                        entity.CreateTime = DateTime.Now;
                        entity.Status = 0; // 默认未开始
                        entity.AuditStatus = 0; // 默认待审核

                        var result = await AdvertBillingRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("导入广告计费数据失败: {Error}", ex.Message);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入广告计费数据异常: {Error}", ex.Message);
                throw new TaktException(L("AdvertBilling.Import.Error", ex.Message));
            }
        }

        /// <summary>
        /// 导出广告计费数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktAdvertBillingQueryDto query, string? sheetName, string? fileName)
        {
            var list = await AdvertBillingRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktAdvertBillingExportDto>>();
            
            // 使用传入的参数，如果为空则使用默认值
            var actualSheetName = sheetName ?? "AdvertBilling";
            var actualFileName = fileName ?? $"AdvertBilling_{DateTime.Now:yyyyMMddHHmmss}";

            return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
        }

        /// <summary>
        /// 更新广告计费状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktAdvertBillingStatusDto input)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            entity.Status = input.Status;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 审核广告计费
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AuditAsync(TaktAdvertBillingAuditDto input)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            entity.AuditStatus = input.AuditStatus;
            entity.AuditComments = input.AuditComments;
            entity.AuditedBy = _currentUser.UserName;
            entity.AuditedTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 启动广告计费
        /// </summary>
        /// <param name="input">启动对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> StartAsync(TaktAdvertBillingStartDto input)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            entity.Status = 1; // 计费中
            entity.BillingStartTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 结束广告计费
        /// </summary>
        /// <param name="input">结束对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> EndAsync(TaktAdvertBillingEndDto input)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            entity.Status = 3; // 已结束
            entity.BillingEndTime = DateTime.Now;
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 更新广告计费统计信息
        /// </summary>
        /// <param name="input">统计信息对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatsAsync(TaktAdvertBillingStatsDto input)
        {
            var entity = await AdvertBillingRepository.GetByIdAsync(input.BillingId);
            if (entity == null)
            {
                throw new TaktException(L("AdvertBilling.NotFound"));
            }

            input.Adapt(entity);
            entity.UpdateBy = _currentUser.UserName;
            entity.UpdateTime = DateTime.Now;

            var result = await AdvertBillingRepository.UpdateAsync(entity);
            return result > 0;
        }

        /// <summary>
        /// 获取广告计费统计信息
        /// </summary>
        /// <returns>广告计费统计信息</returns>
        public async Task<TaktAdvertBillingStatisticsDto> GetStatisticsAsync()
        {
            var allBillings = await AdvertBillingRepository.GetListAsync(x => x.IsDeleted == 0);
            var activeBillings = allBillings.Where(x => x.Status == 1).ToList();

            return new TaktAdvertBillingStatisticsDto
            {
                TotalBillingCount = allBillings.Count,
                ActiveBillingCount = activeBillings.Count,
                TotalBudgetAmount = allBillings.Sum(x => x.BudgetAmount),
                TotalSpentAmount = allBillings.Sum(x => x.SpentAmount),
                TotalRemainingAmount = allBillings.Sum(x => x.BudgetAmount) - allBillings.Sum(x => x.SpentAmount)
            };
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private static Expression<Func<TaktAdvertBilling, bool>> QueryExpression(TaktAdvertBillingQueryDto query)
        {
            return Expressionable.Create<TaktAdvertBilling>()
                .AndIF(!string.IsNullOrEmpty(query?.CompanyCode), x => x.CompanyCode == query!.CompanyCode!)
                .AndIF(query?.AdvertId.HasValue == true && query.AdvertId!.Value > 0, x => x.AdvertId == query!.AdvertId!.Value)
                .AndIF(!string.IsNullOrEmpty(query?.AdvertTitle), x => x.AdvertTitle.Contains(query!.AdvertTitle!))
                .AndIF(query?.BillingType.HasValue == true && query.BillingType!.Value > 0, x => x.BillingType == query!.BillingType!.Value)
                .AndIF(query?.Status.HasValue == true, x => x.Status == query!.Status!.Value)
                .AndIF(query?.AuditStatus.HasValue == true, x => x.AuditStatus == query!.AuditStatus!.Value)
                .AndIF(query?.AutoRenewal.HasValue == true, x => x.AutoRenewal == query!.AutoRenewal!.Value)
                .AndIF(query?.BudgetAlertEnabled.HasValue == true, x => x.BudgetAlertEnabled == query!.BudgetAlertEnabled!.Value)
                .And(x => x.IsDeleted == 0) // 默认只查询未删除的数据
                .ToExpression();
        }
    }
}



