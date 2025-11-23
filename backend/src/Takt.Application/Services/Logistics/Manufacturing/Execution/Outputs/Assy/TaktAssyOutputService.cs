//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAssyOutputService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 组立日报服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Outputs.Assy;
using Takt.Domain.Entities.Logistics.Manufacturing.Outputs.Assy;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Outputs.Assy
{
    /// <summary>
    /// 组立日报服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktAssyOutputService : TaktBaseService, ITaktAssyOutputService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取组立日报仓储
        /// </summary>
        private ITaktRepository<TaktAssyOutput> AssyMpOutputRepository => _repositoryFactory.GetBusinessRepository<TaktAssyOutput>();



        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktAssyOutputService(
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
        /// 获取组立日报分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktAssyOutputDto>> GetListAsync(TaktAssyOutputQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await AssyMpOutputRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktAssyOutputDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktAssyOutputDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取组立日报详情
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>返回组立日报详情</returns>
        public async Task<TaktAssyOutputDto> GetByIdAsync(long assyMpOutputId)
        {
            var assyMpOutput = await AssyMpOutputRepository.GetByIdAsync(assyMpOutputId);
            if (assyMpOutput == null)
                throw new TaktException(L("AssyMpOutput.NotFound", assyMpOutputId));

            return assyMpOutput.Adapt<TaktAssyOutputDto>();
        }

        /// <summary>
        /// 创建组立日报
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回组立日报ID</returns>
        public async Task<long> CreateAsync(TaktAssyOutputCreateDto input)
        {
            var assyMpOutput = input.Adapt<TaktAssyOutput>();
            var result = await AssyMpOutputRepository.CreateAsync(assyMpOutput);
            return result;
        }

        /// <summary>
        /// 更新组立日报
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktAssyOutputUpdateDto input)
        {
            var assyMpOutput = await AssyMpOutputRepository.GetByIdAsync(input.AssyMpOutputId);
            if (assyMpOutput == null)
                throw new TaktException(L("AssyMpOutput.NotFound", input.AssyMpOutputId));

            input.Adapt(assyMpOutput);
            var result = await AssyMpOutputRepository.UpdateAsync(assyMpOutput);
            return result > 0;
        }

        /// <summary>
        /// 删除组立日报
        /// </summary>
        /// <param name="assyMpOutputId">组立日报ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long assyMpOutputId)
        {
            var assyMpOutput = await AssyMpOutputRepository.GetByIdAsync(assyMpOutputId);
            if (assyMpOutput == null)
                throw new TaktException(L("AssyMpOutput.NotFound", assyMpOutputId));

            var result = await AssyMpOutputRepository.DeleteAsync(assyMpOutput);
            return result > 0;
        }

        /// <summary>
        /// 批量删除组立日报
        /// </summary>
        /// <param name="ids">组立日报ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("AssyMpOutput.SelectToDelete"));

            var assyMpOutputs = await AssyMpOutputRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!assyMpOutputs.Any())
                throw new TaktException(L("AssyMpOutput.NotFound"));

            var result = await AssyMpOutputRepository.DeleteAsync(assyMpOutputs);
            return result > 0;
        }

        /// <summary>
        /// 导入组立日报数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "组立日报信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktAssyOutputImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var assyMpOutput = dto.Adapt<TaktAssyOutput>();
                    await AssyMpOutputRepository.CreateAsync(assyMpOutput);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("AssyMpOutput.ImportFailed", dto.ProdOrderCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出组立日报数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktAssyOutputQueryDto query, string sheetName = "组立日报信息")
        {
            var predicate = QueryExpression(query);

            var assyMpOutputs = await AssyMpOutputRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = assyMpOutputs.Adapt<List<TaktAssyOutputExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "组立日报信息")
        {
            var template = new List<TaktAssyOutputTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktAssyOutput, bool>> QueryExpression(TaktAssyOutputQueryDto query)
        {
            var exp = Expressionable.Create<TaktAssyOutput>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.ProdCategory))
                    exp = exp.And(x => x.ProdCategory.Contains(query.ProdCategory));

                if (!string.IsNullOrEmpty(query.ProdLine))
                    exp = exp.And(x => x.ProdLine.Contains(query.ProdLine));

                if (!string.IsNullOrEmpty(query.ProdOrderCode))
                    exp = exp.And(x => x.ProdOrderCode.Contains(query.ProdOrderCode));

                if (!string.IsNullOrEmpty(query.ModelCode))
                    exp = exp.And(x => x.ModelCode.Contains(query.ModelCode));

                if (!string.IsNullOrEmpty(query.MaterialCode))
                    exp = exp.And(x => x.MaterialCode.Contains(query.MaterialCode));

                if (query.StartProdDate.HasValue)
                    exp = exp.And(x => x.ProdDate >= query.StartProdDate.Value);

                if (query.EndProdDate.HasValue)
                    exp = exp.And(x => x.ProdDate <= query.EndProdDate.Value);
            }

            return exp.ToExpression();
        }
    }
}



