#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyOutputDetailService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立明细服务实现
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;
using Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Assy;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立明细服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputDetailService : TaktBaseService, ITaktAssyOutputDetailService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取组立明细仓储
        /// </summary>
        private ITaktRepository<TaktAssyOutputDetail> AssyMpOutputDetailRepository => _repositoryFactory.GetBusinessRepository<TaktAssyOutputDetail>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAssyOutputDetailService(
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
        /// 获取组立明细分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktAssyOutputDetailDto>> GetListAsync(TaktAssyOutputDetailQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await AssyMpOutputDetailRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktAssyOutputDetailDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktAssyOutputDetailDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取组立明细详情
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>返回组立明细详情</returns>
        public async Task<TaktAssyOutputDetailDto> GetByIdAsync(long assyMpOutputDetailId)
        {
            var assyMpOutputDetail = await AssyMpOutputDetailRepository.GetByIdAsync(assyMpOutputDetailId);
            if (assyMpOutputDetail == null)
                throw new TaktException(L("AssyMpOutputDetail.NotFound", assyMpOutputDetailId));

            return assyMpOutputDetail.Adapt<TaktAssyOutputDetailDto>();
        }

        /// <summary>
        /// 根据组立日报ID获取明细列表
        /// </summary>
        /// <param name="assyOutputId">组立日报ID</param>
        /// <returns>组立明细列表</returns>
        public async Task<List<TaktAssyOutputDetailDto>> GetByAssyMpOutputIdAsync(long assyOutputId)
        {
            var details = await AssyMpOutputDetailRepository.GetListAsync(x => x.AssyOutputId == assyOutputId);
            return details.Adapt<List<TaktAssyOutputDetailDto>>();
        }

        /// <summary>
        /// 创建组立明细
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回组立明细ID</returns>
        public async Task<long> CreateAsync(TaktAssyOutputDetailCreateDto input)
        {
            var assyMpOutputDetail = input.Adapt<TaktAssyOutputDetail>();
            var result = await AssyMpOutputDetailRepository.CreateAsync(assyMpOutputDetail);
            return result;
        }

        /// <summary>
        /// 批量创建组立明细
        /// </summary>
        /// <param name="inputs">创建对象列表</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> CreateRangeAsync(List<TaktAssyOutputDetailCreateDto> inputs)
        {
            if (inputs == null || !inputs.Any())
                return true;

            var details = inputs.Select(input => input.Adapt<TaktAssyOutputDetail>()).ToList();
            var result = await AssyMpOutputDetailRepository.CreateRangeAsync(details);
            return result > 0;
        }

        /// <summary>
        /// 更新组立明细
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktAssyOutputDetailUpdateDto input)
        {
            var assyMpOutputDetail = await AssyMpOutputDetailRepository.GetByIdAsync(input.AssyMpOutputDetailId);
            if (assyMpOutputDetail == null)
                throw new TaktException(L("AssyMpOutputDetail.NotFound", input.AssyMpOutputDetailId));

            input.Adapt(assyMpOutputDetail);
            var result = await AssyMpOutputDetailRepository.UpdateAsync(assyMpOutputDetail);
            return result > 0;
        }

        /// <summary>
        /// 删除组立明细
        /// </summary>
        /// <param name="assyMpOutputDetailId">组立明细ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long assyMpOutputDetailId)
        {
            var assyMpOutputDetail = await AssyMpOutputDetailRepository.GetByIdAsync(assyMpOutputDetailId);
            if (assyMpOutputDetail == null)
                throw new TaktException(L("AssyMpOutputDetail.NotFound", assyMpOutputDetailId));

            var result = await AssyMpOutputDetailRepository.DeleteAsync(assyMpOutputDetail);
            return result > 0;
        }

        /// <summary>
        /// 根据组立日报ID删除明细
        /// </summary>
        /// <param name="assyOutputId">组立日报ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteByAssyOutputIdAsync(long assyOutputId)
        {
            var result = await AssyMpOutputDetailRepository.DeleteAsync(x => x.AssyOutputId == assyOutputId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除组立明细
        /// </summary>
        /// <param name="assyOutputDetailIds">组立明细ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] assyOutputDetailIds)
        {
            if (assyOutputDetailIds == null || assyOutputDetailIds.Length == 0)
                throw new TaktException(L("AssyMpOutputDetail.SelectToDelete"));

            var assyMpOutputDetails = await AssyMpOutputDetailRepository.GetListAsync(x => assyOutputDetailIds.Contains(x.Id));
            if (!assyMpOutputDetails.Any())
                throw new TaktException(L("AssyMpOutputDetail.NotFound"));

            var result = await AssyMpOutputDetailRepository.DeleteAsync(assyMpOutputDetails);
            return result > 0;
        }

        /// <summary>
        /// 导入组立明细数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "组立明细信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktAssyOutputDetailImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var assyMpOutputDetail = dto.Adapt<TaktAssyOutputDetail>();
                    await AssyMpOutputDetailRepository.CreateAsync(assyMpOutputDetail);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("AssyMpOutputDetail.ImportFailed", dto.TimePeriod), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出组立明细数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktAssyOutputDetailQueryDto query, string sheetName = "组立明细信息")
        {
            var predicate = QueryExpression(query);

            var assyMpOutputDetails = await AssyMpOutputDetailRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = assyMpOutputDetails.Adapt<List<TaktAssyOutputDetailExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "组立明细信息")
        {
            var template = new List<TaktAssyOutputDetailTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktAssyOutputDetail, bool>> QueryExpression(TaktAssyOutputDetailQueryDto query)
        {
            var exp = Expressionable.Create<TaktAssyOutputDetail>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.TimePeriod))
                    exp = exp.And(x => x.TimePeriod.Contains(query.TimePeriod));
            }

            return exp.ToExpression();
        }
    }
}



