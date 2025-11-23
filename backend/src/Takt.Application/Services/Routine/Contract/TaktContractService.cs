//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktContractService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 合同服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine.Contract;
using Takt.Application.Dtos.Routine.Contract;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Utils;
using Takt.Domain.Utils;
using Takt.Shared.Constants;

namespace Takt.Application.Services.Routine.Contract
{
    /// <summary>
    /// 合同服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktContractService : TaktBaseService, ITaktContractService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取合同仓储
        /// </summary>
        private ITaktRepository<TaktContract> ContractRepository => _repositoryFactory.GetBusinessRepository<TaktContract>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktContractService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取合同分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktContractDto>> GetListAsync(TaktContractQueryDto query)
        {
            _logger.Info("开始查询合同列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await ContractRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new TaktPagedResult<TaktContractDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktContractDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取合同详情
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>返回合同详情</returns>
        public async Task<TaktContractDto> GetByIdAsync(long contractId)
        {
            var contract = await ContractRepository.GetByIdAsync(contractId);
            if (contract == null)
                throw new TaktException(L("Contract.NotFound", contractId));

            return contract.Adapt<TaktContractDto>();
        }

        /// <summary>
        /// 创建合同
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回合同ID</returns>
        public async Task<long> CreateAsync(TaktContractCreateDto input)
        {
            var contract = input.Adapt<TaktContract>();
            var result = await ContractRepository.CreateAsync(contract);
            return result;
        }

        /// <summary>
        /// 更新合同
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktContractUpdateDto input)
        {
            var contract = await ContractRepository.GetByIdAsync(input.ContractId);
            if (contract == null)
                throw new TaktException(L("Contract.NotFound", input.ContractId));

            input.Adapt(contract);
            var result = await ContractRepository.UpdateAsync(contract);
            return result > 0;
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="contractId">合同ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long contractId)
        {
            var contract = await ContractRepository.GetByIdAsync(contractId);
            if (contract == null)
                throw new TaktException(L("Contract.NotFound", contractId));

            var result = await ContractRepository.DeleteAsync(contract);
            return result > 0;
        }

        /// <summary>
        /// 批量删除合同
        /// </summary>
        /// <param name="contractIds">合同ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] contractIds)
        {
            if (contractIds == null || contractIds.Length == 0)
                throw new TaktException(L("Contract.SelectToDelete"));

            var contracts = await ContractRepository.GetListAsync(x => contractIds.Contains(x.Id));
            if (!contracts.Any())
                throw new TaktException(L("Contract.NotFound"));

            var result = await ContractRepository.DeleteAsync(contracts);
            return result > 0;
        }

        /// <summary>
        /// 导入合同数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "合同信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktContractImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var contract = dto.Adapt<TaktContract>();
                    await ContractRepository.CreateAsync(contract);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Contract.ImportFailed", dto.ContractName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出合同数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktContractQueryDto query, string sheetName = "合同信息")
        {
            var predicate = QueryExpression(query);

            var contracts = await ContractRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = contracts.Adapt<List<TaktContractExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "合同信息")
        {
            var template = new List<TaktContractTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktContract, bool>> QueryExpression(TaktContractQueryDto query)
        {
            var exp = Expressionable.Create<TaktContract>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ContractCode))
                    exp = exp.And(x => x.ContractCode.Contains(query.ContractCode));

                if (!string.IsNullOrEmpty(query.ContractName))
                    exp = exp.And(x => x.ContractName.Contains(query.ContractName));

                if (query.ContractType.HasValue)
                    exp = exp.And(x => x.ContractType == query.ContractType.Value);

                if (!string.IsNullOrEmpty(query.ContractCategory))
                    exp = exp.And(x => x.ContractCategory.Contains(query.ContractCategory));

                if (!string.IsNullOrEmpty(query.ContractParty))
                    exp = exp.And(x => x.ContractParty.Contains(query.ContractParty));

                if (query.ContractStatus.HasValue)
                    exp = exp.And(x => x.ContractStatus == query.ContractStatus.Value);

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.CreateTime <= query.EndTime.Value);
            }

            return exp.ToExpression();
        }
    }
} 




