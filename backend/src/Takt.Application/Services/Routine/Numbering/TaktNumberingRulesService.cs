//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNumberingRulesService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 单号规则服务实现
//===================================================================

using Takt.Application.Dtos.Routine.Numbering;
using Takt.Shared.Models;
using Takt.Shared.Utils;
using Takt.Domain.Entities.Routine.Numbering;
using Microsoft.AspNetCore.Http;
using Dm.util;

namespace Takt.Application.Services.Routine.Numbering
{
    /// <summary>
    /// 单号规则服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktNumberingRulesService : TaktBaseService, ITaktNumberingRulesService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktNumberingRulesService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            ITaktLocalizationService localization) 
            : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取单号规则仓储
        /// </summary>
        private ITaktRepository<TaktNumberingRules> NumberRuleRepository => 
            _repositoryFactory.GetBusinessRepository<TaktNumberingRules>();

        /// <summary>
        /// 获取单号规则分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktNumberingRulesDto>> GetListAsync(TaktNumberingRulesQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await NumberRuleRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktNumberingRulesDto>
            {
                Rows = result.Rows.Adapt<List<TaktNumberingRulesDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取单号规则详情
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>返回单号规则详情</returns>
        public async Task<TaktNumberingRulesDto> GetByIdAsync(long numberRuleId)
        {
            var numberRule = await NumberRuleRepository.GetByIdAsync(numberRuleId);
            if (numberRule == null)
                throw new TaktException(L("NumberRule.NotFound"));

            return numberRule.Adapt<TaktNumberingRulesDto>();
        }

        /// <summary>
        /// 获取单号规则选项列表
        /// </summary>
        /// <returns>返回选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var numberRules = await NumberRuleRepository.AsQueryable()
                .Where(x => x.Status == 0 && x.IsDeleted == 0)
                .OrderBy(x => x.OrderNum)
                .Select(x => new { x.Id, x.NumberRuleName, x.NumberRuleCode })
                .ToListAsync();

            return numberRules.Select(x => new TaktSelectOption
            {
                DictValue = x.Id.ToString(),
                DictLabel = $"{x.NumberRuleName}({x.NumberRuleCode})"
            }).ToList();
        }

        /// <summary>
        /// 创建单号规则
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回新创建的单号规则ID</returns>
        public async Task<long> CreateAsync(TaktNumberingRulesCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.NumberRuleName))
                throw new TaktException(L("NumberRule.Name.Required"));

            if (string.IsNullOrEmpty(input.NumberRuleCode))
                throw new TaktException(L("NumberRule.Code.Required"));

            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldsExistsAsync(NumberRuleRepository, 
                new Dictionary<string, string> 
                { 
                    { "NumberRuleCode", input.NumberRuleCode }, 
                    { "NumberRuleName", input.NumberRuleName } 
                });

            var numberRule = input.Adapt<TaktNumberingRules>();
            numberRule.CreateBy = _currentUser.UserName;
            numberRule.CreateTime = DateTime.Now;

            var result = await NumberRuleRepository.CreateAsync(numberRule);
            if (result <= 0)
                throw new TaktException(L("Common.AddFailed"));

            return numberRule.Id;
        }

        /// <summary>
        /// 更新单号规则
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNumberingRulesUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var numberRule = await NumberRuleRepository.GetByIdAsync(input.NumberRuleId);
            if (numberRule == null)
                throw new TaktException(L("NumberRule.NotFound"));

            // 验证字段是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldsExistsAsync(NumberRuleRepository, 
                new Dictionary<string, string> 
                { 
                    { "NumberRuleCode", input.NumberRuleCode }, 
                    { "NumberRuleName", input.NumberRuleName } 
                }, 
                input.NumberRuleId);

            input.Adapt(numberRule);
            numberRule.UpdateBy = _currentUser.UserName;
            numberRule.UpdateTime = DateTime.Now;

            var result = await NumberRuleRepository.UpdateAsync(numberRule);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 删除单号规则
        /// </summary>
        /// <param name="numberRuleId">单号规则ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long numberRuleId)
        {
            var numberRule = await NumberRuleRepository.GetByIdAsync(numberRuleId);
            if (numberRule == null)
                throw new TaktException(L("NumberRule.NotFound"));

            numberRule.IsDeleted = 1;
            numberRule.DeleteBy = _currentUser.UserName;
            numberRule.DeleteTime = DateTime.Now;

            var result = await NumberRuleRepository.UpdateAsync(numberRule);
            if (result <= 0)
                throw new TaktException(L("Common.DeleteFailed"));

            return true;
        }

        /// <summary>
        /// 批量删除单号规则
        /// </summary>
        /// <param name="numberRuleIds">单号规则ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] numberRuleIds)
        {
            var numberRules = await NumberRuleRepository.AsQueryable()
                .Where(x => numberRuleIds.Contains(x.Id))
                .ToListAsync();

            if (!numberRules.Any())
                return false;

            foreach (var numberRule in numberRules)
            {
                numberRule.IsDeleted = 1;
                numberRule.DeleteBy = _currentUser.UserName;
                numberRule.DeleteTime = DateTime.Now;
            }

            var result = await NumberRuleRepository.UpdateRangeAsync(numberRules);
            if (result <= 0)
                throw new TaktException(L("Common.BatchDeleteFailed"));

            return true;
        }

        /// <summary>
        /// 更新单号规则状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktNumberingRulesStatusDto input)
        {
            var numberRule = await NumberRuleRepository.GetByIdAsync(input.NumberRuleId);
            if (numberRule == null)
                throw new TaktException(L("NumberRule.NotFound"));

            numberRule.Status = input.Status;
            numberRule.UpdateBy = _currentUser.UserName;
            numberRule.UpdateTime = DateTime.Now;

            var result = await NumberRuleRepository.UpdateAsync(numberRule);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            var templateDtos = new List<TaktNumberingRulesImportDto>();
            return await TaktExcelHelper.ExportAsync(templateDtos, sheetName);
        }

        /// <summary>
        /// 导入单号规则数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktNumberingRulesImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    if (string.IsNullOrEmpty(dto.NumberRuleName))
                        throw new TaktException(L("NumberRule.Name.Required"));

                    if (string.IsNullOrEmpty(dto.NumberRuleCode))
                        throw new TaktException(L("NumberRule.Code.Required"));

                    // 验证字段是否已存在
                    await TaktValidateUtils.ValidateFieldsExistsAsync(NumberRuleRepository, 
                        new Dictionary<string, string> 
                        { 
                            { "NumberRuleCode", dto.NumberRuleCode }, 
                            { "NumberRuleName", dto.NumberRuleName } 
                        });

                    var numberRule = dto.Adapt<TaktNumberingRules>();
                    numberRule.CreateBy = _currentUser.UserName;
                    numberRule.CreateTime = DateTime.Now;

                    await NumberRuleRepository.CreateAsync(numberRule);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("NumberRule.ImportFailed", dto.NumberRuleName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出单号规则数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktNumberingRulesQueryDto query, string sheetName)
        {
            var predicate = QueryExpression(query);

            var numberRules = await NumberRuleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.OrderNum)
                .ToListAsync();

            var exportDtos = numberRules.Adapt<List<TaktNumberingRulesExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktNumberingRules, bool>> QueryExpression(TaktNumberingRulesQueryDto query)
        {
            return x => x.IsDeleted == 0
                && (string.IsNullOrEmpty(query.NumberRuleName) || x.NumberRuleName.Contains(query.NumberRuleName))
                && (string.IsNullOrEmpty(query.NumberRuleCode) || x.NumberRuleCode.Contains(query.NumberRuleCode))
                && (query.NumberRuleType == -1 || x.NumberRuleType == query.NumberRuleType)
                && (query.Status == -1 || x.Status == query.Status);
        }
    }
}





