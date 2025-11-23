//===================================================================
// 项目名 : Lean.Takt
// 文件名 : TaktDbSeedDept.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 部门数据初始化类 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 部门数据初始化类
/// </summary>
public class TaktDbSeedDeptCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 日志记录器
    /// </summary>
    private readonly ITaktLogger _logger;

    /// <summary>
    /// 部门仓储
    /// </summary>
    private ITaktRepository<TaktDept> DeptRepository => _repositoryFactory.GetAuthRepository<TaktDept>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志记录器</param>
    public TaktDbSeedDeptCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化部门数据
    /// </summary>
    public async Task<(int, int)> InitializeDeptAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 第一步：创建总公司（根节点）
        var (headquarters, isHeadquartersInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "总公司",
            ParentId = 0, // 根节点保持为0
            OrderNum = 1,
            Leader = "董事长",
            Phone = "13800138000",
            Email = "chairman@takt.cn",
            DeptStatus = 0,
            Remark = "Headquarters;本社"
        });
        if (isHeadquartersInsert) insertCount++; else updateCount++;

        // 第二步：创建分公司（上级：总公司）
        var (branch, isBranchInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "分公司",
            ParentId = headquarters.Id,
            OrderNum = 1,
            Leader = "分公司社长",
            Phone = "13800138001",
            Email = "president@takt.cn",
            DeptStatus = 0,
            Remark = "Branch Company;支社"
        });
        if (isBranchInsert) insertCount++; else updateCount++;

        // 第三步：创建总经办（上级：分公司）
        var (gmOffice, isGmOfficeInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "总经办",
            ParentId = branch.Id,
            OrderNum = 1,
            Leader = "总经理",
            Phone = "13800138002",
            Email = "gm@takt.cn",
            DeptStatus = 0,
            Remark = "General Manager Office;社長室"
        });
        if (isGmOfficeInsert) insertCount++; else updateCount++;

        // 第四步：创建总务部（上级：总经办）
        var (gaOffice, isGaOfficeInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "总务部",
            ParentId = gmOffice.Id,
            OrderNum = 1,
            Leader = "总务部长",
            Phone = "13800138003",
            Email = "ga@takt.cn",
            DeptStatus = 0,
            Remark = "General Affairs Department;総務部"
        });
        if (isGaOfficeInsert) insertCount++; else updateCount++;

        // 第五步：创建财务部（上级：总经办）
        var (financeOffice, isFinanceOfficeInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "财务部",
            ParentId = gmOffice.Id,
            OrderNum = 2,
            Leader = "财务部长",
            Phone = "13800138004",
            Email = "finance@takt.cn",
            DeptStatus = 0,
            Remark = "Finance Department;財務部"
        });
        if (isFinanceOfficeInsert) insertCount++; else updateCount++;

        // 第六步：创建IT部（上级：总经办）
        var (itOffice, isItOfficeInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "IT部",
            ParentId = gmOffice.Id,
            OrderNum = 3,
            Leader = "IT部长",
            Phone = "13800138005",
            Email = "it@takt.cn",
            DeptStatus = 0,
            Remark = "IT Department;IT部"
        });
        if (isItOfficeInsert) insertCount++; else updateCount++;

        // 第七步：创建事业推进本部（上级：总经办）
        var (businessDivision, isBusinessDivisionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "事业推进本部",
            ParentId = gmOffice.Id,
            OrderNum = 4,
            Leader = "事业推进本部长",
            Phone = "13800138006",
            Email = "business@takt.cn",
            DeptStatus = 0,
            Remark = "Business Promotion Division;事業推進本部"
        });
        if (isBusinessDivisionInsert) insertCount++; else updateCount++;

        // 第八步：创建管理部（上级：事业推进本部）
        var (adminDept, isAdminDeptInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "管理部",
            ParentId = businessDivision.Id,
            OrderNum = 1,
            Leader = "管理部长",
            Phone = "13800138007",
            Email = "admin@takt.cn",
            DeptStatus = 0,
            Remark = "Administration Department;管理部"
        });
        if (isAdminDeptInsert) insertCount++; else updateCount++;

        // 第九步：创建生管课（上级：管理部）
        var (ppcSection, isPpcSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "生管课",
            ParentId = adminDept.Id,
            OrderNum = 1,
            Leader = "生管课长",
            Phone = "13800138008",
            Email = "ppc@takt.cn",
            DeptStatus = 0,
            Remark = "Production Control Section;生産管理課"
        });
        if (isPpcSectionInsert) insertCount++; else updateCount++;

        // 第十步：创建部管课（上级：管理部）
        var (deptMgrSection, isDeptMgrSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "部管课",
            ParentId = adminDept.Id,
            OrderNum = 2,
            Leader = "部管课长",
            Phone = "13800138009",
            Email = "deptmgr@takt.cn",
            DeptStatus = 0,
            Remark = "Department Management Section;部門管理課"
        });
        if (isDeptMgrSectionInsert) insertCount++; else updateCount++;

        // 第十一步：创建报关课（上级：管理部）
        var (customsSection, isCustomsSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "报关课",
            ParentId = adminDept.Id,
            OrderNum = 3,
            Leader = "报关课长",
            Phone = "13800138010",
            Email = "customs@takt.cn",
            DeptStatus = 0,
            Remark = "Customs Declaration Section;通関課"
        });
        if (isCustomsSectionInsert) insertCount++; else updateCount++;

        // 第十二步：创建资材部（上级：事业推进本部）
        var (materialDept, isMaterialDeptInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "资材部",
            ParentId = businessDivision.Id,
            OrderNum = 2,
            Leader = "资材部长",
            Phone = "13800138011",
            Email = "material@takt.cn",
            DeptStatus = 0,
            Remark = "Material Department;資材部"
        });
        if (isMaterialDeptInsert) insertCount++; else updateCount++;

        // 第十三步：创建采购课（上级：资材部）
        var (purchaseSection, isPurchaseSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "采购课",
            ParentId = materialDept.Id,
            OrderNum = 1,
            Leader = "采购课长",
            Phone = "13800138012",
            Email = "purchase@takt.cn",
            DeptStatus = 0,
            Remark = "Purchasing Section;購買課"
        });
        if (isPurchaseSectionInsert) insertCount++; else updateCount++;

        // 第十四步：创建生产改善推进本部（上级：总经办）
        var (productionDivision, isProductionDivisionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "生产改善推进本部",
            ParentId = gmOffice.Id,
            OrderNum = 5,
            Leader = "生产改善推进本部长",
            Phone = "13800138013",
            Email = "production@takt.cn",
            DeptStatus = 0,
            Remark = "Production Improvement Division;生産改善推進本部"
        });
        if (isProductionDivisionInsert) insertCount++; else updateCount++;

        // 第十五步：创建技术部（上级：生产改善推进本部）
        var (techDept, isTechDeptInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "技术部",
            ParentId = productionDivision.Id,
            OrderNum = 1,
            Leader = "技术部长",
            Phone = "13800138014",
            Email = "tech@takt.cn",
            DeptStatus = 0,
            Remark = "Technical Department;技術部"
        });
        if (isTechDeptInsert) insertCount++; else updateCount++;

        // 第十六步：创建技术课（上级：技术部）
        var (techSection, isTechSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "技术课",
            ParentId = techDept.Id,
            OrderNum = 1,
            Leader = "技术课长",
            Phone = "13800138015",
            Email = "engineering@takt.cn",
            DeptStatus = 0,
            Remark = "Engineering Section;技術課"
        });
        if (isTechSectionInsert) insertCount++; else updateCount++;

        // 第十七步：创建制造部（上级：生产改善推进本部）
        var (manufacturingDept, isManufacturingDeptInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "制造部",
            ParentId = productionDivision.Id,
            OrderNum = 2,
            Leader = "制造部长",
            Phone = "13800138016",
            Email = "manufacturing@takt.cn",
            DeptStatus = 0,
            Remark = "Manufacturing Department;製造部"
        });
        if (isManufacturingDeptInsert) insertCount++; else updateCount++;

        // 第十八步：创建制一课（上级：制造部）
        var (mfg1Section, isMfg1SectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "制一课",
            ParentId = manufacturingDept.Id,
            OrderNum = 1,
            Leader = "制一课长",
            Phone = "13800138017",
            Email = "mfg1@takt.cn",
            DeptStatus = 0,
            Remark = "Manufacturing Section 1;製造1課"
        });
        if (isMfg1SectionInsert) insertCount++; else updateCount++;

        // 第十九步：创建制二课（上级：制造部）
        var (mfg2Section, isMfg2SectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "制二课",
            ParentId = manufacturingDept.Id,
            OrderNum = 2,
            Leader = "制二课长",
            Phone = "13800138018",
            Email = "mfg2@takt.cn",
            DeptStatus = 0,
            Remark = "Manufacturing Section 2;製造2課"
        });
        if (isMfg2SectionInsert) insertCount++; else updateCount++;

        // 第二十步：创建制技课（上级：制造部）
        var (mfgTechSection, isMfgTechSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "制技课",
            ParentId = manufacturingDept.Id,
            OrderNum = 3,
            Leader = "制技课长",
            Phone = "13800138019",
            Email = "mfgtech@takt.cn",
            DeptStatus = 0,
            Remark = "Manufacturing Technology Section;製造技術課"
        });
        if (isMfgTechSectionInsert) insertCount++; else updateCount++;

        // 第二十一步：创建品管部（上级：生产改善推进本部）
        var (qaDept, isQaDeptInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "品管部",
            ParentId = productionDivision.Id,
            OrderNum = 3,
            Leader = "品管部长",
            Phone = "13800138020",
            Email = "qa@takt.cn",
            DeptStatus = 0,
            Remark = "Quality Assurance Department;品質管理部"
        });
        if (isQaDeptInsert) insertCount++; else updateCount++;

        // 第二十二步：创建受检课（上级：品管部）
        var (inspectionSection, isInspectionSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "受检课",
            ParentId = qaDept.Id,
            OrderNum = 1,
            Leader = "受检课长",
            Phone = "13800138021",
            Email = "inspection@takt.cn",
            DeptStatus = 0,
            Remark = "Inspection Section;検査課"
        });
        if (isInspectionSectionInsert) insertCount++; else updateCount++;

        // 第二十三步：创建品管课（上级：品管部）
        var (qcSection, isQcSectionInsert) = await GetOrCreateDeptAsync(new TaktDept
        {
            DeptName = "品管课",
            ParentId = qaDept.Id,
            OrderNum = 2,
            Leader = "品管课长",
            Phone = "13800138022",
            Email = "qc@takt.cn",
            DeptStatus = 0,
            Remark = "Quality Control Section;品質管理課"
        });
        if (isQcSectionInsert) insertCount++; else updateCount++;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 获取或创建部门
    /// </summary>
    /// <param name="dept">部门信息</param>
    /// <returns>部门实体</returns>
    private async Task<(TaktDept dept, bool isInsert)> GetOrCreateDeptAsync(TaktDept dept)
    {
        var existingDept = await DeptRepository.GetFirstAsync(d => d.DeptName == dept.DeptName);
        if (existingDept == null)
        {
            dept.CreateBy = "Takt365";
            dept.CreateTime = DateTime.Now;
            dept.UpdateBy = "Takt365";
            dept.UpdateTime = DateTime.Now;
            await DeptRepository.CreateAsync(dept);
            _logger.Info($"[创建] 部门 '{dept.DeptName}' 创建成功，ID: {dept.Id}");
            return (dept, true);
        }
        else
        {
            existingDept.DeptName = dept.DeptName;
            existingDept.ParentId = dept.ParentId;
            existingDept.OrderNum = dept.OrderNum;
            existingDept.Leader = dept.Leader;
            existingDept.Phone = dept.Phone;
            existingDept.Email = dept.Email;
            existingDept.DeptStatus = dept.DeptStatus;
            existingDept.UpdateBy = "Takt365";
            existingDept.UpdateTime = DateTime.Now;

            await DeptRepository.UpdateAsync(existingDept);
            _logger.Info($"[更新] 部门 '{existingDept.DeptName}' 更新成功，ID: {existingDept.Id}");
            return (existingDept, false);
        }
    }
}
