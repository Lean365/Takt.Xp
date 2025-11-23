//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedAccountingCoordinator.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : 会计种子数据协调器
//===================================================================

using Takt.Domain.Entities.Accounting.Controlling;
using Takt.Domain.Entities.Accounting.Financial;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 会计种子数据协调器
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-19
/// 功能说明:
/// 1. 统一管理会计相关种子数据的初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktDbSeedAccountingCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktCompany> CompanyRepository => _repositoryFactory.GetBusinessRepository<TaktCompany>();
    private ITaktRepository<TaktProfitCenter> ProfitCenterRepository => _repositoryFactory.GetBusinessRepository<TaktProfitCenter>();
    private ITaktRepository<TaktCostCenter> CostCenterRepository => _repositoryFactory.GetBusinessRepository<TaktCostCenter>();
    private ITaktRepository<TaktCostElement> CostElementRepository => _repositoryFactory.GetBusinessRepository<TaktCostElement>();
    private ITaktRepository<TaktExpense> ExpenseRepository => _repositoryFactory.GetBusinessRepository<TaktExpense>();
    private ITaktRepository<TaktExpenseDraft> ExpenseDraftRepository => _repositoryFactory.GetBusinessRepository<TaktExpenseDraft>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public TaktDbSeedAccountingCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有会计种子数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<AccountingSeedResult> InitializeAllAccountingDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有会计种子数据...");

            var result = new AccountingSeedResult();

            // 1. 初始化公司信息数据
            var companyResult = await InitializeCompanyAsync();
            result.CompanyResult = companyResult;

            // 2. 初始化利润中心数据
            var profitCenterResult = await InitializeProfitCenterAsync();
            result.ProfitCenterResult = profitCenterResult;

            // 3. 初始化成本中心数据
            var costCenterResult = await InitializeCostCenterAsync();
            result.CostCenterResult = costCenterResult;

            // 4. 初始化成本要素数据
            var costElementResult = await InitializeCostElementAsync();
            result.CostElementResult = costElementResult;

            // 5. 初始化费用报销记录数据
            var expenseResult = await InitializeExpenseAsync();
            result.ExpenseResult = expenseResult;

            // 6. 初始化费用签拟单数据
            var expenseDraftResult = await InitializeExpenseDraftAsync();
            result.ExpenseDraftResult = expenseDraftResult;

            _logger.Info($"会计种子数据初始化完成！公司信息: {companyResult.insertCount + companyResult.updateCount} 条, 利润中心: {profitCenterResult.insertCount + profitCenterResult.updateCount} 条, 成本中心: {costCenterResult.insertCount + costCenterResult.updateCount} 条, 成本要素: {costElementResult.insertCount + costElementResult.updateCount} 条, 费用报销: {expenseResult.insertCount + expenseResult.updateCount} 条, 费用签拟单: {expenseDraftResult.insertCount + expenseDraftResult.updateCount} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化会计种子数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化公司信息数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCompanyAsync()
    {
        var seed = new TaktDbSeedCompany();
        var data = seed.GetDefaultCompanies();
        return await InitializeCompaniesAsync(data, "公司信息");
    }

    /// <summary>
    /// 初始化利润中心数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeProfitCenterAsync()
    {
        var seed = new TaktDbSeedProfitCenter();
        var data = seed.GetDefaultProfitCenters();
        return await InitializeProfitCentersAsync(data, "利润中心");
    }

    /// <summary>
    /// 初始化成本中心数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCostCenterAsync()
    {
        var seed = new TaktDbSeedCostCenter();
        var data = seed.GetDefaultCostCenters();
        return await InitializeCostCentersAsync(data, "成本中心");
    }

    /// <summary>
    /// 初始化成本要素数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCostElementAsync()
    {
        var seed = new TaktDbSeedCostElement();
        var data = seed.GetDefaultCostElements();
        return await InitializeCostElementsAsync(data, "成本要素");
    }

    /// <summary>
    /// 初始化费用报销记录数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeExpenseAsync()
    {
        var seed = new TaktDbSeedExpense();
        var data = seed.GetDefaultExpenses();
        return await InitializeExpensesAsync(data, "费用报销记录");
    }

    /// <summary>
    /// 初始化费用签拟单数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeExpenseDraftAsync()
    {
        var seed = new TaktDbSeedExpense();
        var data = seed.GetDefaultExpenseDrafts();
        return await InitializeExpenseDraftsAsync(data, "费用签拟单");
    }

    /// <summary>
    /// 批量初始化公司信息数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCompaniesAsync(List<TaktCompany> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await CompanyRepository.GetFirstAsync(x => x.CompanyCode == item.CompanyCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await CompanyRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.CompanyCode}' 创建成功");
            }
            else
            {
                existing.CompanyName = item.CompanyName;
                existing.CompanyShortName = item.CompanyShortName;
                existing.CompanyCity = item.CompanyCity;
                existing.CompanyCountry = item.CompanyCountry;
                existing.CompanyCurrency = item.CompanyCurrency;
                existing.CompanyIndustryType = item.CompanyIndustryType;
                existing.CompanyNature = item.CompanyNature;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await CompanyRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.CompanyCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化利润中心数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeProfitCentersAsync(List<TaktProfitCenter> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ProfitCenterRepository.GetFirstAsync(x => x.ProfitCenterCode == item.ProfitCenterCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ProfitCenterRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.ProfitCenterCode}' 创建成功");
            }
            else
            {
                existing.ProfitCenterName = item.ProfitCenterName;
                existing.CompanyCode = item.CompanyCode;
                existing.PlantCode = item.PlantCode;
                existing.ProfitCenterCategory = item.ProfitCenterCategory;
                existing.ManagerName = item.ManagerName;
                existing.DeptName = item.DeptName;
                existing.ProfitCenterDescription = item.ProfitCenterDescription;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ProfitCenterRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.ProfitCenterCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化成本中心数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCostCentersAsync(List<TaktCostCenter> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await CostCenterRepository.GetFirstAsync(x => x.CostCenterCode == item.CostCenterCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await CostCenterRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.CostCenterCode}' 创建成功");
            }
            else
            {
                existing.CostCenterName = item.CostCenterName;
                existing.CompanyCode = item.CompanyCode;
                existing.PlantCode = item.PlantCode;
                existing.CostCenterCategory = item.CostCenterCategory;
                existing.ManagerName = item.ManagerName;
                existing.DeptName = item.DeptName;
                existing.CostCenterDescription = item.CostCenterDescription;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await CostCenterRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.CostCenterCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化成本要素数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeCostElementsAsync(List<TaktCostElement> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await CostElementRepository.GetFirstAsync(x => x.CostElementCode == item.CostElementCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await CostElementRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.CostElementCode}' 创建成功");
            }
            else
            {
                existing.CostElementName = item.CostElementName;
                existing.CompanyCode = item.CompanyCode;
                existing.PlantCode = item.PlantCode;
                existing.CostElementType = item.CostElementType;
                existing.CostElementCategory = item.CostElementCategory;
                existing.Unit = item.Unit;
                existing.CostElementDescription = item.CostElementDescription;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await CostElementRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.CostElementCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化费用报销记录数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeExpensesAsync(List<TaktExpense> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ExpenseRepository.GetFirstAsync(x => x.ExpenseNo == item.ExpenseNo);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ExpenseRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.ExpenseNo}' 创建成功");
            }
            else
            {
                existing.CompanyCode = item.CompanyCode;
                existing.PlantCode = item.PlantCode;
                existing.EmployeeId = item.EmployeeId;
                existing.ExpenseTypeId = item.ExpenseTypeId;
                existing.TotalAmount = item.TotalAmount;
                existing.ExpenseDate = item.ExpenseDate;
                existing.ExpenseReason = item.ExpenseReason;
                existing.PaymentMethod = item.PaymentMethod;
                existing.Status = item.Status;
                existing.ApproverId = item.ApproverId;
                existing.ApproveTime = item.ApproveTime;
                existing.ApproveComment = item.ApproveComment;
                existing.AttachmentCount = item.AttachmentCount;
                existing.Notes = item.Notes;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ExpenseRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.ExpenseNo}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化费用签拟单数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeExpenseDraftsAsync(List<TaktExpenseDraft> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await ExpenseDraftRepository.GetFirstAsync(x => x.DocNo == item.DocNo);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await ExpenseDraftRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.DocNo}' 创建成功");
            }
            else
            {
                existing.CompanyCode = item.CompanyCode;
                existing.PlantCode = item.PlantCode;
                existing.ApplicantId = item.ApplicantId;
                existing.ApplicantCode = item.ApplicantCode;
                existing.ApplicantName = item.ApplicantName;
                existing.DepartmentId = item.DepartmentId;
                existing.DepartmentCode = item.DepartmentCode;
                existing.DepartmentName = item.DepartmentName;
                existing.ApplyDate = item.ApplyDate;
                existing.ExpenseType = item.ExpenseType;
                existing.ExpenseSubType = item.ExpenseSubType;
                existing.ExpenseAmount = item.ExpenseAmount;
                existing.Currency = item.Currency;
                existing.ExchangeRate = item.ExchangeRate;
                existing.LocalAmount = item.LocalAmount;
                existing.Purpose = item.Purpose;
                existing.CostCenterId = item.CostCenterId;
                existing.CostCenterCode = item.CostCenterCode;
                existing.CostCenterName = item.CostCenterName;
                existing.CostElementId = item.CostElementId;
                existing.CostElementCode = item.CostElementCode;
                existing.CostElementName = item.CostElementName;
                existing.BudgetType = item.BudgetType;
                existing.BudgetNo = item.BudgetNo;
                existing.BudgetId = item.BudgetId;
                existing.BudgetAmount = item.BudgetAmount;
                existing.UsedBudgetAmount = item.UsedBudgetAmount;
                existing.RemainingBudgetAmount = item.RemainingBudgetAmount;
                existing.SupplierCode = item.SupplierCode;
                existing.SupplierName = item.SupplierName;
                existing.ContractNumber = item.ContractNumber;
                existing.InvoiceNumber = item.InvoiceNumber;
                existing.ExpectedPaymentDate = item.ExpectedPaymentDate;
                existing.ActualPaymentDate = item.ActualPaymentDate;
                existing.PaymentStatus = item.PaymentStatus;
                existing.ApprovalStatus = item.ApprovalStatus;
                existing.OrderNum = item.OrderNum;
                existing.Status = item.Status;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await ExpenseDraftRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.DocNo}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
}

/// <summary>
/// 会计种子数据结果
/// </summary>
public class AccountingSeedResult
{
    /// <summary>
    /// 公司信息结果
    /// </summary>
    public (int insertCount, int updateCount) CompanyResult { get; set; }

    /// <summary>
    /// 利润中心结果
    /// </summary>
    public (int insertCount, int updateCount) ProfitCenterResult { get; set; }

    /// <summary>
    /// 成本中心结果
    /// </summary>
    public (int insertCount, int updateCount) CostCenterResult { get; set; }

    /// <summary>
    /// 成本要素结果
    /// </summary>
    public (int insertCount, int updateCount) CostElementResult { get; set; }

    /// <summary>
    /// 费用报销记录结果
    /// </summary>
    public (int insertCount, int updateCount) ExpenseResult { get; set; }

    /// <summary>
    /// 费用签拟单结果
    /// </summary>
    public (int insertCount, int updateCount) ExpenseDraftResult { get; set; }

    /// <summary>
    /// 获取总插入数量
    /// </summary>
    /// <returns>总插入数量</returns>
    public int GetTotalInsertCount() =>
        CompanyResult.insertCount + ProfitCenterResult.insertCount +
        CostCenterResult.insertCount + CostElementResult.insertCount +
        ExpenseResult.insertCount + ExpenseDraftResult.insertCount;

    /// <summary>
    /// 获取总更新数量
    /// </summary>
    /// <returns>总更新数量</returns>
    public int GetTotalUpdateCount() =>
        CompanyResult.updateCount + ProfitCenterResult.updateCount +
        CostCenterResult.updateCount + CostElementResult.updateCount +
        ExpenseResult.updateCount + ExpenseDraftResult.updateCount;
}


