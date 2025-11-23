//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedHrmCoordinator.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : HRM种子数据协调器
//===================================================================

using Takt.Domain.Entities.HumanResource.Organization;
using Takt.Domain.Entities.HumanResource.Employee;
using Takt.Domain.Repositories;

namespace Takt.Infrastructure.Data.Seeds.Biz.Hrm;

/// <summary>
/// HRM种子数据协调器
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-19
/// 功能说明:
/// 1. 统一管理HRM相关种子数据的初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktDbSeedHrmCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktDepartment> DepartmentRepository => _repositoryFactory.GetBusinessRepository<TaktDepartment>();
    private ITaktRepository<TaktPosition> PositionRepository => _repositoryFactory.GetBusinessRepository<TaktPosition>();
    private ITaktRepository<TaktEmployee> EmployeeRepository => _repositoryFactory.GetBusinessRepository<TaktEmployee>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public TaktDbSeedHrmCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有HRM种子数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<HrmSeedResult> InitializeAllHrmDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有HRM种子数据...");

            var result = new HrmSeedResult();

            // 1. 初始化部门数据
            var departmentResult = await InitializeDepartmentAsync();
            result.DepartmentResult = departmentResult;

            // 2. 初始化职位数据
            var positionResult = await InitializePositionAsync();
            result.PositionResult = positionResult;

            // 3. 初始化员工数据
            var employeeResult = await InitializeEmployeeAsync();
            result.EmployeeResult = employeeResult;

            _logger.Info($"HRM种子数据初始化完成！部门: {departmentResult.insertCount + departmentResult.updateCount} 条, 职位: {positionResult.insertCount + positionResult.updateCount} 条, 员工: {employeeResult.insertCount + employeeResult.updateCount} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化HRM种子数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化部门数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeDepartmentAsync()
    {
        var seed = new TaktDbSeedDepartment();
        var data = seed.GetDefaultDepartments();
        return await InitializeDepartmentsAsync(data, "部门");
    }

    /// <summary>
    /// 初始化职位数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializePositionAsync()
    {
        var seed = new TaktDbSeedPosition();
        var data = seed.GetDefaultPositions();
        return await InitializePositionsAsync(data, "职位");
    }

    /// <summary>
    /// 初始化员工数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeEmployeeAsync()
    {
        var seed = new TaktDbSeedEmployee();
        var data = seed.GetDefaultEmployees();
        return await InitializeEmployeesAsync(data, "员工");
    }

    /// <summary>
    /// 批量初始化部门数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeDepartmentsAsync(List<TaktDepartment> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await DepartmentRepository.GetFirstAsync(x => x.DeptCode == item.DeptCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await DepartmentRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.DeptCode}' 创建成功");
            }
            else
            {
                existing.DeptName = item.DeptName;
                existing.EnglishName = item.EnglishName;
                existing.ParentId = item.ParentId;
                existing.DeptLevel = item.DeptLevel;
                existing.DeptType = item.DeptType;
                existing.OrderNum = item.OrderNum;
                existing.Phone = item.Phone;
                existing.Email = item.Email;
                existing.Status = item.Status;
                existing.Description = item.Description;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await DepartmentRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.DeptCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化职位数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializePositionsAsync(List<TaktPosition> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await PositionRepository.GetFirstAsync(x => x.PosCode == item.PosCode);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await PositionRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.PosCode}' 创建成功");
            }
            else
            {
                existing.PosName = item.PosName;
                existing.EnglishName = item.EnglishName;
                existing.CategoryId = item.CategoryId;
                existing.PosLevel = item.PosLevel;
                existing.PosSequence = item.PosSequence;
                existing.Status = item.Status;
                existing.OrderNum = item.OrderNum;
                existing.Description = item.Description;
                existing.Responsibilities = item.Responsibilities;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await PositionRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.PosCode}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 批量初始化员工数据
    /// </summary>
    /// <param name="data">数据列表</param>
    /// <param name="dataType">数据类型描述</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeEmployeesAsync(List<TaktEmployee> data, string dataType)
    {
        int insertCount = 0, updateCount = 0;

        foreach (var item in data)
        {
            var existing = await EmployeeRepository.GetFirstAsync(x => x.EmployeeNo == item.EmployeeNo);

            if (existing == null)
            {
                item.CreateBy = "Takt365";
                item.CreateTime = DateTime.Now;
                item.UpdateBy = "Takt365";
                item.UpdateTime = DateTime.Now;

                await EmployeeRepository.CreateAsync(item);
                insertCount++;
                _logger.Info($"[创建] {dataType} '{item.EmployeeNo}' 创建成功");
            }
            else
            {
                existing.EmployeeName = item.EmployeeName;
                existing.EnglishName = item.EnglishName;
                existing.Gender = item.Gender;
                existing.BirthDate = item.BirthDate;
                existing.Mobile = item.Mobile;
                existing.Email = item.Email;
                existing.DepartmentId = item.DepartmentId;
                existing.PositionId = item.PositionId;
                existing.EmployeeType = item.EmployeeType;
                existing.HireDate = item.HireDate;
                existing.RegularDate = item.RegularDate;
                existing.WorkLocation = item.WorkLocation;
                existing.Status = item.Status;
                existing.UpdateBy = "Takt365";
                existing.UpdateTime = DateTime.Now;
                await EmployeeRepository.UpdateAsync(existing);
                updateCount++;
                _logger.Info($"[更新] {dataType} '{item.EmployeeNo}' 更新成功");
            }
        }

        _logger.Info($"{dataType}数据初始化完成 - 插入: {insertCount}, 更新: {updateCount}");
        return (insertCount, updateCount);
    }
}

/// <summary>
/// HRM种子数据结果
/// </summary>
public class HrmSeedResult
{
    /// <summary>
    /// 部门结果
    /// </summary>
    public (int insertCount, int updateCount) DepartmentResult { get; set; }
    
    /// <summary>
    /// 职位结果
    /// </summary>
    public (int insertCount, int updateCount) PositionResult { get; set; }
    
    /// <summary>
    /// 员工结果
    /// </summary>
    public (int insertCount, int updateCount) EmployeeResult { get; set; }

    /// <summary>
    /// 获取总插入数量
    /// </summary>
    /// <returns>总插入数量</returns>
    public int GetTotalInsertCount() => 
        DepartmentResult.insertCount + PositionResult.insertCount + EmployeeResult.insertCount;

    /// <summary>
    /// 获取总更新数量
    /// </summary>
    /// <returns>总更新数量</returns>
    public int GetTotalUpdateCount() => 
        DepartmentResult.updateCount + PositionResult.updateCount + EmployeeResult.updateCount;
}


