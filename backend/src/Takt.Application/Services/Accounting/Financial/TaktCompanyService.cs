//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktCompanyService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-09 16:30
// 版本号 : V0.0.1
// 描述   : 公司代码服务实现
//===================================================================

using Takt.Application.Dtos.Accounting.Financial;
using Takt.Domain.Entities.Accounting.Financial;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Accounting.Financial
{
    /// <summary>
    /// 公司代码服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-09
    /// </remarks>
    public class TaktCompanyService : TaktBaseService, ITaktCompanyService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取公司代码仓储
        /// </summary>
        private ITaktRepository<TaktCompany> CompanyRepository => _repositoryFactory.GetBusinessRepository<TaktCompany>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktCompanyService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取公司代码分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktCompanyDto>> GetListAsync(TaktCompanyQueryDto query)
        {
            _logger.Info("开始查询公司代码列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await CompanyRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.OrderNum,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            var dtoResult = new TaktPagedResult<TaktCompanyDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktCompanyDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取公司代码详情
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>返回公司代码详情</returns>
        public async Task<TaktCompanyDto> GetByIdAsync(long companyId)
        {
            var company = await CompanyRepository.GetByIdAsync(companyId);
            if (company == null)
                throw new TaktException(L("Company.NotFound", companyId));

            return company.Adapt<TaktCompanyDto>();
        }

        /// <summary>
        /// 获取公司代码选项列表
        /// </summary>
        /// <returns>公司代码选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var companies = await CompanyRepository.AsQueryable()
                .Where(c => c.Status == 0 && c.IsDeleted == 0)  // 只获取正常状态且未删除的公司
                .OrderBy(c => c.CompanyCode)
                .Select(c => new TaktSelectOption
                {
                    DictLabel = c.CompanyName,
                    DictValue = c.CompanyCode,
                    ExtLabel = c.CompanyShortName,
                    ExtValue = c.CompanyTaxNumber,
                    OrderNum = c.OrderNum,
                    Status = c.Status
                })
                .ToListAsync();
            return companies;
        }

        /// <summary>
        /// 创建公司代码
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回公司代码ID</returns>
        public async Task<long> CreateAsync(TaktCompanyCreateDto input)
        {
            _logger.Info("开始创建公司代码，输入参数：{@Input}", input);

            // 检查公司代码是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyCode", input.CompanyCode);

            // 检查公司名称是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyName", input.CompanyName);

            // 检查公司简称是否已存在（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyShortName))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyShortName", input.CompanyShortName);
            }
            // 检查公司营业执照号是否已存在（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyBusinessLicense))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyBusinessLicense", input.CompanyBusinessLicense);
            }
            // 检查公司税号是否已存在（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyTaxNumber))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyTaxNumber", input.CompanyTaxNumber);
            }
            // 检查公司组织机构代码是否已存在（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyOrganizationCode))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyOrganizationCode", input.CompanyOrganizationCode);
            }
            // 检查公司统一社会信用代码是否已存在（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyUnifiedCreditCode))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyUnifiedCreditCode", input.CompanyUnifiedCreditCode);
            }

            var company = input.Adapt<TaktCompany>();
            company.CreateBy = _currentUser.UserName;
            company.CreateTime = DateTime.Now;

            var result = await CompanyRepository.CreateAsync(company);
            _logger.Info("公司代码创建成功，ID：{CompanyId}", result);

            return result;
        }

        /// <summary>
        /// 更新公司代码
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktCompanyUpdateDto input)
        {
            _logger.Info("开始更新公司代码，输入参数：{@Input}", input);

            var company = await CompanyRepository.GetByIdAsync(input.CompanyId);
            if (company == null)
                throw new TaktException(L("Company.NotFound", input.CompanyId));

            // 检查公司代码是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyCode", input.CompanyCode, input.CompanyId);

            // 检查公司名称是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyName", input.CompanyName, input.CompanyId);

            // 检查公司简称是否已被其他记录使用（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyShortName))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyShortName", input.CompanyShortName, input.CompanyId);
            }
            // 检查公司营业执照号是否已被其他记录使用（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyBusinessLicense))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyBusinessLicense", input.CompanyBusinessLicense, input.CompanyId);
            }
            // 检查公司税号是否已被其他记录使用（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyTaxNumber))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyTaxNumber", input.CompanyTaxNumber, input.CompanyId);
            }
            // 检查公司组织机构代码是否已被其他记录使用（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyOrganizationCode))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyOrganizationCode", input.CompanyOrganizationCode, input.CompanyId);
            }
            // 检查公司统一社会信用代码是否已被其他记录使用（如果不为空）
            if (!string.IsNullOrEmpty(input.CompanyUnifiedCreditCode))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyUnifiedCreditCode", input.CompanyUnifiedCreditCode, input.CompanyId);
            }

            input.Adapt(company);
            company.UpdateBy = _currentUser.UserName;
            company.UpdateTime = DateTime.Now;

            var result = await CompanyRepository.UpdateAsync(company);
            _logger.Info("公司代码更新结果：{Result}", result);

            return result > 0;
        }

        /// <summary>
        /// 删除公司代码
        /// </summary>
        /// <param name="companyId">公司代码ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long companyId)
        {
            _logger.Info("开始删除公司代码，ID：{CompanyId}", companyId);

            var company = await CompanyRepository.GetByIdAsync(companyId);
            if (company == null)
                throw new TaktException(L("Company.NotFound", companyId));

            company.DeleteBy = _currentUser.UserName;
            company.DeleteTime = DateTime.Now;
            company.IsDeleted = 1;

            var result = await CompanyRepository.UpdateAsync(company);
            _logger.Info("公司代码删除结果：{Result}", result);

            return result > 0;
        }

        /// <summary>
        /// 批量删除公司代码
        /// </summary>
        /// <param name="companyIds">公司代码ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] companyIds)
        {
            _logger.Info("开始批量删除公司代码，ID集合：{@CompanyIds}", companyIds);

            var companies = await CompanyRepository.GetListAsync(x => companyIds.Contains(x.Id));
            if (!companies.Any())
                throw new TaktException(L("Company.NotFound"));

            foreach (var company in companies)
            {
                company.DeleteBy = _currentUser.UserName;
                company.DeleteTime = DateTime.Now;
                company.IsDeleted = 1;
            }

            var result = await CompanyRepository.UpdateRangeAsync(companies);
            _logger.Info("公司代码批量删除结果：{Result}", result);

            return result > 0;
        }

        /// <summary>
        /// 更新公司代码状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktCompanyStatusDto input)
        {
            _logger.Info("开始更新公司代码状态，输入参数：{@Input}", input);

            var company = await CompanyRepository.GetByIdAsync(input.CompanyId);
            if (company == null)
                throw new TaktException(L("Company.NotFound", input.CompanyId));

            company.Status = input.Status;
            company.UpdateBy = _currentUser.UserName;
            company.UpdateTime = DateTime.Now;

            var result = await CompanyRepository.UpdateAsync(company);
            _logger.Info("公司代码状态更新完成，结果：{Result}", result > 0);

            return result > 0;
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            _logger.Info("开始获取公司代码导入模板，工作表：{SheetName}", sheetName);

            return await TaktExcelHelper.GenerateTemplateAsync<TaktCompanyTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入公司代码数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            _logger.Info("开始导入公司代码数据，工作表：{SheetName}", sheetName);

            try
            {
                var companies = await TaktExcelHelper.ImportAsync<TaktCompanyImportDto>(fileStream, sheetName);
                if (!companies.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var company in companies)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(company.CompanyCode))
                        {
                            _logger.Warn("导入公司代码失败: 公司代码不能为空");
                            fail++;
                            continue;
                        }

                        if (string.IsNullOrEmpty(company.CompanyName))
                        {
                            _logger.Warn("导入公司代码失败: 公司名称不能为空");
                            fail++;
                            continue;
                        }

                        // 校验公司代码是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyCode", company.CompanyCode);
                        // 校验公司名称是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyName", company.CompanyName);
                        // 校验公司简称是否已存在（如果不为空）
                        if (!string.IsNullOrEmpty(company.CompanyShortName))
                        {
                            await TaktValidateUtils.ValidateFieldExistsAsync(CompanyRepository, "CompanyShortName", company.CompanyShortName);
                        }

                        var entity = company.Adapt<TaktCompany>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.Status = 0;

                        var result = await CompanyRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入公司代码失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入公司代码数据失败", ex);
                throw new TaktException("导入公司代码数据失败");
            }
        }

        /// <summary>
        /// 导出公司代码数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktCompanyQueryDto query, string sheetName)
        {
            _logger.Info("开始导出公司代码数据，查询条件：{@Query}", query);

            var list = await CompanyRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktCompanyExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportList, sheetName, "Company");
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回查询表达式</returns>
        private Expression<Func<TaktCompany, bool>> QueryExpression(TaktCompanyQueryDto query)
        {
            return Expressionable.Create<TaktCompany>()
                .And(x => x.IsDeleted == 0)
                .AndIF(!string.IsNullOrEmpty(query?.CompanyCode), x => x.CompanyCode.Contains(query!.CompanyCode!))
                .AndIF(!string.IsNullOrEmpty(query?.CompanyName), x => x.CompanyName.Contains(query!.CompanyName!))
                .AndIF(!string.IsNullOrEmpty(query?.CompanyShortName), x => x.CompanyShortName != null && x.CompanyShortName.Contains(query!.CompanyShortName!))
                .AndIF(!string.IsNullOrEmpty(query?.CompanyCity), x => x.CompanyCity != null && x.CompanyCity.Contains(query!.CompanyCity!))
                .AndIF(!string.IsNullOrEmpty(query?.CompanyRegion), x => x.CompanyRegion != null && x.CompanyRegion.Contains(query!.CompanyRegion!))
                .AndIF(!string.IsNullOrEmpty(query?.CompanyCountry), x => x.CompanyCountry != null && x.CompanyCountry.Contains(query!.CompanyCountry!))
                .AndIF(query?.CompanyIndustryType.HasValue == true && query.CompanyIndustryType!.Value != -1, x => x.CompanyIndustryType == query!.CompanyIndustryType!.Value)
                .AndIF(query?.CompanyNature.HasValue == true && query.CompanyNature!.Value != -1, x => x.CompanyNature == query!.CompanyNature!.Value)
                .AndIF(query?.CompanyCurrency.HasValue == true && query.CompanyCurrency!.Value != -1, x => x.CompanyCurrency == query!.CompanyCurrency!.Value)
                .AndIF(query?.CompanyLanguage.HasValue == true && query.CompanyLanguage!.Value != -1, x => x.CompanyLanguage == query!.CompanyLanguage!.Value)
                .AndIF(query?.Status.HasValue == true && query.Status!.Value != -1, x => x.Status == query!.Status!.Value)
                .AndIF(query?.EstablishmentDateStart.HasValue == true, x => x.EstablishmentDate != null && x.EstablishmentDate >= query!.EstablishmentDateStart!.Value)
                .AndIF(query?.EstablishmentDateEnd.HasValue == true, x => x.EstablishmentDate != null && x.EstablishmentDate <= query!.EstablishmentDateEnd!.Value)
                .ToExpression();
        }
    }
}




