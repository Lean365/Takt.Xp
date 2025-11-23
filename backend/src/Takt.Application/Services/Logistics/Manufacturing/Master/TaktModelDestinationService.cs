#nullable enable

//===================================================================
// 项目名: Takt.Application
// 文件名: TaktModelDestinationService.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号: V0.0.1
// 描述: 机种仕向服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 机种仕向服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktModelDestinationService : TaktBaseService, ITaktModelDestinationService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取机种仕向仓储
        /// </summary>
        private ITaktRepository<TaktModelDestination> ModelDestinationRepository => _repositoryFactory.GetBusinessRepository<TaktModelDestination>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktModelDestinationService(
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
        /// 获取机种仕向分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktModelDestinationDto>> GetListAsync(TaktModelDestinationQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await ModelDestinationRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktModelDestinationDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktModelDestinationDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取机种仕向详情
        /// </summary>
        /// <param name="id">机种仕向ID</param>
        /// <returns>返回机种仕向详情</returns>
        public async Task<TaktModelDestinationDto> GetByIdAsync(long id)
        {
            var modelDestination = await ModelDestinationRepository.GetByIdAsync(id);
            if (modelDestination == null)
                throw new TaktException(L("ModelDestination.NotFound", id));

            return modelDestination.Adapt<TaktModelDestinationDto>();
        }

        /// <summary>
        /// 创建机种仕向
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回机种仕向ID</returns>
        public async Task<long> CreateAsync(TaktModelDestinationCreateDto input)
        {
            var modelDestination = input.Adapt<TaktModelDestination>();
            modelDestination.CreateTime = DateTime.Now;
            modelDestination.CreateBy = _currentUser.UserName ?? "System";

            var result = await ModelDestinationRepository.CreateAsync(modelDestination);
            return result;
        }

        /// <summary>
        /// 更新机种仕向
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktModelDestinationUpdateDto input)
        {
            var modelDestination = await ModelDestinationRepository.GetByIdAsync(input.ModelDestinationId);
            if (modelDestination == null)
                throw new TaktException(L("ModelDestination.NotFound", input.ModelDestinationId));

            input.Adapt(modelDestination);
            modelDestination.UpdateTime = DateTime.Now;
            modelDestination.UpdateBy = _currentUser.UserName ?? "System";

            var result = await ModelDestinationRepository.UpdateAsync(modelDestination);
            return result > 0;
        }

        /// <summary>
        /// 删除机种仕向
        /// </summary>
        /// <param name="id">机种仕向ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var modelDestination = await ModelDestinationRepository.GetByIdAsync(id);
            if (modelDestination == null)
                throw new TaktException(L("ModelDestination.NotFound", id));

            var result = await ModelDestinationRepository.DeleteAsync(modelDestination);
            return result > 0;
        }

        /// <summary>
        /// 批量删除机种仕向
        /// </summary>
        /// <param name="ids">机种仕向ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("ModelDestination.SelectToDelete"));

            var modelDestinations = await ModelDestinationRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!modelDestinations.Any())
                throw new TaktException(L("ModelDestination.NotFound"));

            var result = await ModelDestinationRepository.DeleteAsync(modelDestinations);
            return result > 0;
        }

        /// <summary>
        /// 导入机种仕向数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "机种仕向信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktModelDestinationImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var modelDestination = dto.Adapt<TaktModelDestination>();
                    modelDestination.CreateTime = DateTime.Now;
                    modelDestination.CreateBy = _currentUser.UserName ?? "System";

                    await ModelDestinationRepository.CreateAsync(modelDestination);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("ModelDestination.ImportFailed", dto.ModelCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出机种仕向数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktModelDestinationQueryDto query, string sheetName = "机种仕向信息")
        {
            var list = await ModelDestinationRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktModelDestinationExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportList, sheetName, "机种仕向数据");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "机种仕向信息")
        {
            var template = new List<TaktModelDestinationTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktModelDestination, bool>> QueryExpression(TaktModelDestinationQueryDto query)
        {
            var exp = Expressionable.Create<TaktModelDestination>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.MaterialCode))
                    exp = exp.And(x => x.MaterialCode.Contains(query.MaterialCode));

                if (!string.IsNullOrEmpty(query.ModelCode))
                    exp = exp.And(x => x.ModelCode.Contains(query.ModelCode));

                if (!string.IsNullOrEmpty(query.ModelName))
                    exp = exp.And(x => x.ModelName.Contains(query.ModelName));

                if (!string.IsNullOrEmpty(query.DestinationCode))
                    exp = exp.And(x => x.DestinationCode.Contains(query.DestinationCode));

                if (!string.IsNullOrEmpty(query.DestinationName))
                    exp = exp.And(x => x.DestinationName.Contains(query.DestinationName));

                if (query.DestinationType.HasValue)
                    exp = exp.And(x => x.DestinationType == query.DestinationType.Value);
            }

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取仕向地类型文本
        /// </summary>
        /// <param name="destinationType">仕向地类型</param>
        /// <returns>仕向地类型文本</returns>
        private string GetDestinationTypeText(int destinationType)
        {
            return destinationType switch
            {
                1 => "日本",
                2 => "美国",
                3 => "澳洲",
                4 => "德国",
                5 => "加拿大",
                6 => "国内",
                7 => "其他",
                _ => "未知"
            };
        }

        /// <summary>
        /// 根据物料编码获取机种编码
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>机种编码列表</returns>
        public async Task<List<string>> GetModelCodesByMaterialAsync(string materialCode)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new TaktException("物料编码不能为空");

            var modelDestinations = await ModelDestinationRepository.GetListAsync(x => x.MaterialCode == materialCode);
            return modelDestinations.Select(x => x.ModelCode).Distinct().ToList();
        }

        /// <summary>
        /// 根据物料编码获取仕向编码
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>仕向编码列表</returns>
        public async Task<List<string>> GetDestinationCodesByMaterialAsync(string materialCode)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new TaktException("物料编码不能为空");

            var modelDestinations = await ModelDestinationRepository.GetListAsync(x => x.MaterialCode == materialCode);
            return modelDestinations.Select(x => x.DestinationCode).Distinct().ToList();
        }

        /// <summary>
        /// 根据物料编码获取机种仕向信息
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>机种仕向信息列表</returns>
        public async Task<List<TaktModelDestinationDto>> GetModelDestinationsByMaterialAsync(string materialCode)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new TaktException("物料编码不能为空");

            var modelDestinations = await ModelDestinationRepository.GetListAsync(x => x.MaterialCode == materialCode);
            return modelDestinations.Adapt<List<TaktModelDestinationDto>>();
        }
    }
}



