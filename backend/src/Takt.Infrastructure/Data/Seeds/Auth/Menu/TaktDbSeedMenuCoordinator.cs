//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedMenuCoordinator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 菜单数据初始化主类 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Identity;
using Takt.Infrastructure.Data.Seeds.Biz;
using System.Linq.Expressions;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 菜单数据初始化主类
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
/// </remarks>
public class TaktDbSeedMenuCoordinator
{
  /// <summary>
  /// 仓储工厂
  /// </summary>
  protected readonly ITaktRepositoryFactory _repositoryFactory;
  private readonly ITaktLogger _logger;

  private ITaktRepository<TaktMenu> MenuRepository => _repositoryFactory.GetAuthRepository<TaktMenu>();

  /// <summary>
  /// 构造函数
  /// </summary>
  /// <param name="repositoryFactory">仓储工厂</param>
  /// <param name="logger">日志记录器</param>
  public TaktDbSeedMenuCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
  {
    _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
  }

  /// <summary>
  /// 初始化菜单数据
  /// </summary>
  public async Task<(int, int)> InitializeMenuAsync()
  {
    var menuNameToId = new Dictionary<string, long>();

    _logger.Info("开始初始化菜单数据...");

    // 1. 初始化顶级菜单
    var (topInsertCount, topUpdateCount) = await InitializeTopMenusAsync(menuNameToId);

    // 2. 初始化二级目录
    var (secondLevelInsertCount, secondLevelUpdateCount) = await InitializeSecondLevelMenusAsync(menuNameToId);

    // 3. 初始化二级子菜单
    var (secondInsertCount, secondUpdateCount) = await InitializeSecondMenusAsync(menuNameToId);

    // 4. 初始化三级目录
    var (thirdLevelInsertCount, thirdLevelUpdateCount) = await InitializeThirdLevelMenusAsync(menuNameToId);

    // 5. 初始化三级子菜单
    var (thirdSubInsertCount, thirdSubUpdateCount) = await InitializeThirdMenusAsync(menuNameToId);

    // 6. 初始化四级目录
    var (fourthLevelInsertCount, fourthLevelUpdateCount) = await InitializeFourthLevelMenusAsync(menuNameToId);

    // 7. 初始化四级子菜单
    var (fourthSubInsertCount, fourthSubUpdateCount) = await InitializeFourthMenusAsync(menuNameToId);

    // 8. 初始化五级目录
    var (fifthLevelInsertCount, fifthLevelUpdateCount) = await InitializeFifthLevelMenusAsync(menuNameToId);

    // 9. 初始化五级子菜单
    var (fifthSubInsertCount, fifthSubUpdateCount) = await InitializeFifthMenusAsync(menuNameToId);

    // 10. 初始化六级菜单
    var (sixthInsertCount, sixthUpdateCount) = await InitializeSixthMenusAsync(menuNameToId);

    // 11. 初始化按钮
    var (btnInsertCount, btnUpdateCount) = await InitializeButtonsAsync(menuNameToId);

    var totalInsertCount = topInsertCount + secondLevelInsertCount + secondInsertCount + thirdLevelInsertCount + thirdSubInsertCount + fourthLevelInsertCount + fourthSubInsertCount + fifthLevelInsertCount + fifthSubInsertCount + sixthInsertCount + btnInsertCount;
    var totalUpdateCount = topUpdateCount + secondLevelUpdateCount + secondUpdateCount + thirdLevelUpdateCount + thirdSubUpdateCount + fourthLevelUpdateCount + fourthSubUpdateCount + fifthLevelUpdateCount + fifthSubUpdateCount + sixthUpdateCount + btnUpdateCount;

    _logger.Info($"菜单数据初始化完成。新增: {totalInsertCount}, 更新: {totalUpdateCount}");

    return (totalInsertCount, totalUpdateCount);
  }

  /// <summary>
  /// 初始化顶级菜单
  /// </summary>
  private async Task<(int, int)> InitializeTopMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;

    var topMenus = new List<TaktMenu>
        {
            new TaktMenu
            {
                MenuName = "仪表盘",
                TransKey = "menu.dashboard._self",
                ParentId = 0,
                OrderNum = 1,
                Path = "dashboard",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "DashboardOutlined",
                Remark = "仪表盘目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "日常事务",
                TransKey = "menu.routine._self",
                ParentId = 0,
                OrderNum = 2,
                Path = "routine",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "FileTextOutlined",
                Remark = "日常事务目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "财务核算",
                TransKey = "menu.accounting._self",
                ParentId = 0,
                OrderNum = 3,
                Path = "accounting",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "AccountBookOutlined",
                Remark = "财务核算目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "后勤管理",
                TransKey = "menu.logistics._self",
                ParentId = 0,
                OrderNum = 4,
                Path = "logistics",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "ClusterOutlined",
                Remark = "后勤管理目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "人力资源",
                TransKey = "menu.HumanResource._self",
                ParentId = 0,
                OrderNum = 5,
                Path = "hrm",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "TeamOutlined",
                Remark = "人力资源目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "工作流程",
                TransKey = "menu.workflow._self",
                ParentId = 0,
                OrderNum = 6,
                Path = "workflow",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "DeploymentUnitOutlined",
                Remark = "工作流程目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "身份认证",
                TransKey = "menu.identity._self",
                ParentId = 0,
                OrderNum = 7,
                Path = "identity",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "UserOutlined",
                Remark = "身份认证目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },

            new TaktMenu
            {
                MenuName = "代码生成",
                TransKey = "menu.generator._self",
                ParentId = 0,
                OrderNum = 8,
                Path = "generator",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "CodeOutlined",
                Remark = "代码生成目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "审批日志",
                TransKey = "menu.Logging._self",
                ParentId = 0,
                OrderNum = 9,
                Path = "audit",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "AuditOutlined",
                Remark = "审批日志目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "广告管理",
                TransKey = "menu.advertisement._self",
                ParentId = 0,
                OrderNum = 10,
                Path = "advertisement",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "PictureOutlined",
                Remark = "广告管理目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "组件",
                TransKey = "menu.component._self",
                ParentId = 0,
                OrderNum = 11,
                Path = "component",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "AppstoreOutlined",
                Remark = "组件目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            },
            new TaktMenu
            {
                MenuName = "关于",
                TransKey = "menu.about._self",
                ParentId = 0,
                OrderNum = 13,
                Path = "about",
                Component = "",
                QueryParams = null,
                IsExternal = 0,
                IsCache = 0,
                MenuType = 0,
                Visible = 0,
                MenuStatus = 0,
                Perms = "",
                Icon = "InfoCircleOutlined",
                Remark = "关于目录",
                CreateBy = "Takt(Claude AI)",
                CreateTime = DateTime.Now,
                UpdateBy = "Takt(Claude AI)",
                UpdateTime = DateTime.Now
            }
        };

    foreach (var menu in topMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, true);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化二级目录
  /// </summary>
  private async Task<(int, int)> InitializeSecondLevelMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;

    // 1. 仪表盘二级目录
    var dashboardMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "仪表盘" && m.ParentId == 0);
    if (dashboardMenu != null)
    {
      // 初始化仪表盘的二级目录
      var dashboardSubMenus = TaktDbSeedDashboardMenu.GetDashboardSecondMenus(dashboardMenu.Id);
      foreach (var menu in dashboardSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;

        if (isNew) insertCount++;
        else updateCount++;
      }
    }

    // 2. 日常事务二级目录
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 初始化日常事务的二级目录（MenuType = 0）
      var routineSubMenus = TaktDbSeedRoutineMenu.GetRoutineSecondLevelMenus(routineMenu.Id);
      foreach (var menu in routineSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;

        if (isNew) insertCount++;
        else updateCount++;
      }
    }

    // 3. 财务核算二级目录
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      // 初始化管理会计、控制会计、全面预算目录
      var financeSubMenus = TaktDbSeedFinanceMenu.GetFinanceSecondLevelMenus(financeMenu.Id);
      foreach (var menu in financeSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;

        if (isNew) insertCount++;
        else updateCount++;
      }
    }

    // 4. 后勤管理二级目录
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      // 初始化后勤管理的二级目录（MenuType = 0）
      var logisticsSubMenus = TaktDbSeedLogisticsMenu.GetLogisticsSecondLevelMenus(logisticsMenu.Id);
      foreach (var menu in logisticsSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 5. 工作流程二级目录
    var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
    if (workflowMenu != null)
    {
      var workflowSubMenus = TaktDbSeedWorkflowMenu.GetWorkflowSecondLevelMenus(workflowMenu.Id);
      foreach (var menu in workflowSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 6. 人力资源二级目录
    var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
    if (hrmMenu != null)
    {
      var hrmSubMenus = TaktDbSeedHrmMenu.GetHrmSecondMenus(hrmMenu.Id);
      foreach (var menu in hrmSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 7. 身份认证二级目录
    var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
    if (identityMenu != null)
    {
      var identitySubMenus = TaktDbSeedIdentityMenu.GetIdentityThirdMenus(identityMenu.Id);
      foreach (var menu in identitySubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 8. 代码生成二级目录
    var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
    if (generatorMenu != null)
    {
      var generatorSubMenus = TaktDbSeedGeneratorMenu.GetGeneratorThirdMenus(generatorMenu.Id);
      foreach (var menu in generatorSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 9. 审批日志二级目录
    var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审批日志" && m.ParentId == 0);
    if (auditMenu != null)
    {
      var auditSubMenus = TaktDbSeedAuditMenu.GetAuditThirdMenus(auditMenu.Id);
      foreach (var menu in auditSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }


    // 10. 广告管理二级目录
    var advertisementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "广告管理" && m.ParentId == 0);
    if (advertisementMenu != null)
    {
      var advertisementSubMenus = TaktDbSeedAdvertisementMenu.GetAdvertisementSecondMenus(advertisementMenu.Id);
      foreach (var menu in advertisementSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 12. 组件二级目录
    var componentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "组件" && m.ParentId == 0);
    if (componentMenu != null)
    {
      var componentSubMenus = TaktDbSeedComponentMenu.GetComponentSecondMenus(componentMenu.Id);
      foreach (var menu in componentSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    // 13. 关于二级目录
    var aboutMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "关于" && m.ParentId == 0);
    if (aboutMenu != null)
    {
      var aboutSubMenus = TaktDbSeedAboutMenu.GetAboutSecondlMenus(aboutMenu.Id);
      foreach (var menu in aboutSubMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化二级子菜单
  /// </summary>
  private async Task<(int, int)> InitializeSecondMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var subMenus = new List<TaktMenu>();

    // 1. 日常事务二级子菜单
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 日常事务的二级子菜单处理
      // 这里可以添加日常事务的二级子菜单逻辑
    }

    // 2. 财务核算二级子菜单
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      // 财务核算的二级子菜单处理
      // 这里可以添加财务核算的二级子菜单逻辑
    }

    // 3. 后勤管理二级子菜单
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      // 后勤管理的二级子菜单处理
      // 这里可以添加后勤管理的二级子菜单逻辑
    }

    // 4. 工作流程二级子菜单
    var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
    if (workflowMenu != null)
    {
      // 工作流程的二级子菜单处理
      // 这里可以添加工作流程的二级子菜单逻辑
    }

    // 5. 人力资源二级子菜单
    var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
    if (hrmMenu != null)
    {
      // 人力资源的二级子菜单处理
      // 这里可以添加人力资源的二级子菜单逻辑
    }

    // 6. 身份认证二级子菜单
    var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
    if (identityMenu != null)
    {
      // 身份认证的二级子菜单处理
      // 这里可以添加身份认证的二级子菜单逻辑
    }

    // 7. 代码生成二级子菜单
    var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
    if (generatorMenu != null)
    {
      // 代码生成的二级子菜单处理
      // 这里可以添加代码生成的二级子菜单逻辑
    }

    // 8. 审批日志二级子菜单
    var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审批日志" && m.ParentId == 0);
    if (auditMenu != null)
    {
      // 审批日志的二级子菜单处理
      // 这里可以添加审批日志的二级子菜单逻辑
    }

    // 9. 实时通信二级子菜单
    var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
    if (realtimeMenu != null)
    {
      // 实时通信的二级子菜单处理
      // 这里可以添加实时通信的二级子菜单逻辑
    }

    // 处理所有二级子菜单
    foreach (var menu in subMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化三级目录
  /// </summary>
  private async Task<(int, int)> InitializeThirdLevelMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var thirdLevelMenus = new List<TaktMenu>();

    // 1. 日常事务下的三级目录
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 新闻管理下的三级菜单
      var newsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "新闻管理" && m.ParentId == routineMenu.Id);
      if (newsMenu != null)
      {
        var newsSubMenus = TaktDbSeedRoutineMenu.GetRoutineNewsThirdMenus(newsMenu.Id);
        thirdLevelMenus.AddRange(newsSubMenus);
      }

      // 办公用品下的三级目录
      var officeSuppliesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
      if (officeSuppliesMenu != null)
      {
        var officeSuppliesSubMenus = TaktDbSeedRoutineMenu.GetRoutineOfficeSuppliesThirdLevelMenus(officeSuppliesMenu.Id);
        thirdLevelMenus.AddRange(officeSuppliesSubMenus);
      }

      // 文件管理下的三级目录
      var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
      if (fileMenu != null)
      {
        var fileSubMenus = TaktDbSeedRoutineMenu.GetRoutineFileThirdLevelMenus(fileMenu.Id);
        thirdLevelMenus.AddRange(fileSubMenus);
      }



      // 公告通知下的三级目录
      var noticeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
      if (noticeMenu != null)
      {
        var noticeSubMenus = TaktDbSeedRoutineMenu.GetRoutineNoticeThirdLevelMenus(noticeMenu.Id);
        thirdLevelMenus.AddRange(noticeSubMenus);
      }

      // 医务管理下的三级目录
      var medicalMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
      if (medicalMenu != null)
      {
        var medicalSubMenus = TaktDbSeedRoutineMenu.GetRoutineMedicalThirdLevelMenus(medicalMenu.Id);
        thirdLevelMenus.AddRange(medicalSubMenus);
      }



      // 图书管理下的三级目录
      var bookMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "图书管理" && m.ParentId == routineMenu.Id);
      if (bookMenu != null)
      {
        var bookSubMenus = TaktDbSeedRoutineMenu.GetRoutineBookThirdLevelMenus(bookMenu.Id);
        thirdLevelMenus.AddRange(bookSubMenus);
      }
    }

    // 2. 财务核算下的三级目录
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      // 管理会计下的三级目录
      var managementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == financeMenu.Id);
      if (managementMenu != null)
      {
        var managementSubMenus = TaktDbSeedFinanceMenu.GetFinanceManagementThirdMenus(managementMenu.Id);
        thirdLevelMenus.AddRange(managementSubMenus);
      }

      // 控制会计下的三级目录
      var controlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == financeMenu.Id);
      if (controlMenu != null)
      {
        var controlSubMenus = TaktDbSeedFinanceMenu.GetFinanceControlThirdMenus(controlMenu.Id);
        thirdLevelMenus.AddRange(controlSubMenus);
      }

      // 全面预算下的三级目录
      var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
      if (budgetMenu != null)
      {
        var budgetSubMenus = TaktDbSeedFinanceMenu.GetBudgetThirdLevelMenus(budgetMenu.Id);
        thirdLevelMenus.AddRange(budgetSubMenus);
      }
    }
    else
    {
      _logger.Info("未找到财务核算目录");
    }

    // 3. 后勤管理下的三级目录
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      // 物料管理下的三级目录
      var materialMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenu.Id);
      if (materialMenu != null)
      {
        var materialSubMenus = TaktDbSeedLogisticsMenu.GetMaterialThirdLevelMenus(materialMenu.Id);
        thirdLevelMenus.AddRange(materialSubMenus);
      }

      // 生产管理下的三级目录
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
      if (productionMenu != null)
      {
        var productionSubMenus = TaktDbSeedLogisticsMenu.GetProductionThirdLevelMenus(productionMenu.Id);
        thirdLevelMenus.AddRange(productionSubMenus);
      }

      // 质量管理下的三级目录
      var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
      if (qualityMenu != null)
      {
        var qualitySubMenus = TaktDbSeedLogisticsMenu.GetQualityThirdLevelMenus(qualityMenu.Id);
        thirdLevelMenus.AddRange(qualitySubMenus);
      }

      // 销售管理下的三级目录
      var salesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
      if (salesMenu != null)
      {
        var salesSubMenus = TaktDbSeedLogisticsMenu.GetSalesThirdMenus(salesMenu.Id);
        thirdLevelMenus.AddRange(salesSubMenus);
      }

      // 设备管理下的三级目录
      var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
      if (equipmentMenu != null)
      {
        var equipmentSubMenus = TaktDbSeedLogisticsMenu.GetEquipmentThirdLevelMenus(equipmentMenu.Id);
        thirdLevelMenus.AddRange(equipmentSubMenus);
      }

      // 客服管理下的三级目录
      var serviceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
      if (serviceMenu != null)
      {
        var serviceSubMenus = TaktDbSeedLogisticsMenu.GetServiceThirdLevelMenus(serviceMenu.Id);
        thirdLevelMenus.AddRange(serviceSubMenus);
      }

      // 项目管理下的三级目录 - 已移除，项目管理功能已整合到其他模块
    }

    // 4. 工作流程下的三级目录
    var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
    if (workflowMenu != null)
    {
      // 工作流程的三级目录处理
      // 这里可以添加工作流程的三级目录逻辑
    }

    // 5. 人力资源下的三级目录
    var hrmMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "人力资源" && m.ParentId == 0);
    if (hrmMenu != null)
    {
      // 考勤管理下的三级目录
      var attendanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "考勤管理" && m.ParentId == hrmMenu.Id);
      if (attendanceMenu != null)
      {
        var attendanceSubMenus = TaktDbSeedHrmMenu.GetHrmAttendanceThirdMenus(attendanceMenu.Id);
        thirdLevelMenus.AddRange(attendanceSubMenus);
      }

      // 福利管理下的三级目录
      var benefitMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "福利管理" && m.ParentId == hrmMenu.Id);
      if (benefitMenu != null)
      {
        var benefitSubMenus = TaktDbSeedHrmMenu.GetHrmBenefitThirdMenus(benefitMenu.Id);
        thirdLevelMenus.AddRange(benefitSubMenus);
      }

      // 员工管理下的三级目录
      var employeeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "员工管理" && m.ParentId == hrmMenu.Id);
      if (employeeMenu != null)
      {
        var employeeSubMenus = TaktDbSeedHrmMenu.GetHrmEmployeeThirdMenus(employeeMenu.Id);
        thirdLevelMenus.AddRange(employeeSubMenus);
      }

      // 请假管理下的三级目录
      var leaveMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "请假管理" && m.ParentId == hrmMenu.Id);
      if (leaveMenu != null)
      {
        var leaveSubMenus = TaktDbSeedHrmMenu.GetHrmLeaveThirdMenus(leaveMenu.Id);
        thirdLevelMenus.AddRange(leaveSubMenus);
      }

      // 组织管理下的三级目录
      var organizationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "组织管理" && m.ParentId == hrmMenu.Id);
      if (organizationMenu != null)
      {
        var organizationSubMenus = TaktDbSeedHrmMenu.GetHrmOrganizationThirdMenus(organizationMenu.Id);
        thirdLevelMenus.AddRange(organizationSubMenus);
      }

      // 绩效管理下的三级目录
      var performanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "绩效管理" && m.ParentId == hrmMenu.Id);
      if (performanceMenu != null)
      {
        var performanceSubMenus = TaktDbSeedHrmMenu.GetHrmPerformanceThirdMenus(performanceMenu.Id);
        thirdLevelMenus.AddRange(performanceSubMenus);
      }

      // 招聘管理下的三级目录
      var recruitmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "招聘管理" && m.ParentId == hrmMenu.Id);
      if (recruitmentMenu != null)
      {
        var recruitmentSubMenus = TaktDbSeedHrmMenu.GetHrmRecruitmentThirdMenus(recruitmentMenu.Id);
        thirdLevelMenus.AddRange(recruitmentSubMenus);
      }

      // 薪酬管理下的三级目录
      var salaryMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "薪酬管理" && m.ParentId == hrmMenu.Id);
      if (salaryMenu != null)
      {
        var salarySubMenus = TaktDbSeedHrmMenu.GetHrmSalaryThirdMenus(salaryMenu.Id);
        thirdLevelMenus.AddRange(salarySubMenus);
      }

      // 培训管理下的三级目录
      var trainingMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "培训管理" && m.ParentId == hrmMenu.Id);
      if (trainingMenu != null)
      {
        var trainingSubMenus = TaktDbSeedHrmMenu.GetHrmTrainingThirdMenus(trainingMenu.Id);
        thirdLevelMenus.AddRange(trainingSubMenus);
      }

      // 报表统计下的三级目录
      var reportMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "报表统计" && m.ParentId == hrmMenu.Id);
      if (reportMenu != null)
      {
        var reportSubMenus = TaktDbSeedHrmMenu.GetHrmReportThirdMenus(reportMenu.Id);
        thirdLevelMenus.AddRange(reportSubMenus);
      }
    }

    // 6. 身份认证下的三级目录
    var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
    if (identityMenu != null)
    {
      // 身份认证的三级目录处理
      // 这里可以添加身份认证的三级目录逻辑
    }

    // 7. 代码生成下的三级目录
    var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
    if (generatorMenu != null)
    {
      // 代码生成的三级目录处理
      // 这里可以添加代码生成的三级目录逻辑
    }

    // 8. 审批日志下的三级目录
    var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审批日志" && m.ParentId == 0);
    if (auditMenu != null)
    {
      // 审批日志的三级目录处理
      // 这里可以添加审批日志的三级目录逻辑
    }

    // 9. 实时通信下的三级目录
    var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
    if (realtimeMenu != null)
    {
      // 实时通信的三级目录处理
      // 这里可以添加实时通信的三级目录逻辑
    }



    // 处理所有三级目录菜单
    foreach (var menu in thirdLevelMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化三级子菜单
  /// </summary>
  private async Task<(int, int)> InitializeThirdMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;

    // 1. 日常事务下的二级菜单项和三级子菜单
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 1. 日常事务二级菜单项（基础服务和内部沟通功能）
      var routineSecondMenus = TaktDbSeedRoutineMenu.GetRoutineSecondMenus(routineMenu.Id);
      foreach (var menu in routineSecondMenus)
      {
        menu.CreateBy = "Takt(Claude AI)";
        menu.CreateTime = DateTime.Now;
        menu.UpdateBy = "Takt(Claude AI)";
        menu.UpdateTime = DateTime.Now;
        var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
        menuNameToId[menu.MenuName] = savedMenu.Id;
        if (isNew) insertCount++; else updateCount++;
      }

      // 2. 日程管理下的三级菜单
      var scheduleMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日程管理" && m.ParentId == routineMenu.Id);
      if (scheduleMenu != null)
      {
        var scheduleSubMenus = TaktDbSeedRoutineMenu.GetRoutineScheduleThirdMenus(scheduleMenu.Id);
        foreach (var menu in scheduleSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 3. 用车管理下的三级菜单
      var carMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "用车管理" && m.ParentId == routineMenu.Id);
      if (carMenu != null)
      {
        var carSubMenus = TaktDbSeedRoutineMenu.GetRoutineCarThirdMenus(carMenu.Id);
        foreach (var menu in carSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }



      // 4. 邮件管理下的三级菜单
      var emailMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "邮件管理" && m.ParentId == routineMenu.Id);
      if (emailMenu != null)
      {
        var emailSubMenus = TaktDbSeedRoutineMenu.GetRoutineEmailThirdMenus(emailMenu.Id);
        foreach (var menu in emailSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 5. 会议管理下的三级菜单
      var meetingMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "会议管理" && m.ParentId == routineMenu.Id);
      if (meetingMenu != null)
      {
        var meetingSubMenus = TaktDbSeedRoutineMenu.GetRoutineMeetingThirdMenus(meetingMenu.Id);
        foreach (var menu in meetingSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 6. 合同管理下的三级菜单
      var contractMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "合同管理" && m.ParentId == routineMenu.Id);
      if (contractMenu != null)
      {
        var contractSubMenus = TaktDbSeedRoutineMenu.GetRoutineContractThirdMenus(contractMenu.Id);
        foreach (var menu in contractSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 7. 项目管理下的三级菜单
      var projectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "项目管理" && m.ParentId == routineMenu.Id);
      if (projectMenu != null)
      {
        var projectSubMenus = TaktDbSeedRoutineMenu.GetRoutineProjectThirdMenus(projectMenu.Id);
        foreach (var menu in projectSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 8. 任务管理下的三级菜单
      var quartzMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "任务管理" && m.ParentId == routineMenu.Id);
      if (quartzMenu != null)
      {
        var quartzSubMenus = TaktDbSeedRoutineMenu.GetRoutineQuartzThirdMenus(quartzMenu.Id);
        foreach (var menu in quartzSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }
    }

    // 2. 财务核算下的三级子菜单
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      // 管理会计下的三级菜单
      var managementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "管理会计" && m.ParentId == financeMenu.Id);
      if (managementMenu != null)
      {
        var managementSubMenus = TaktDbSeedFinanceMenu.GetFinanceManagementThirdMenus(managementMenu.Id);
        foreach (var menu in managementSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 控制会计下的三级菜单
      var controlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "控制会计" && m.ParentId == financeMenu.Id);
      if (controlMenu != null)
      {
        var controlSubMenus = TaktDbSeedFinanceMenu.GetFinanceControlThirdMenus(controlMenu.Id);
        foreach (var menu in controlSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }
    }

    // 3. 后勤管理下的三级子菜单
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      // 生产管理下的三级菜单
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
      if (productionMenu != null)
      {
        var productionSubMenus = TaktDbSeedLogisticsMenu.GetProductionThirdLevelMenus(productionMenu.Id);
        foreach (var menu in productionSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 销售管理下的三级菜单
      var salesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售管理" && m.ParentId == logisticsMenu.Id);
      if (salesMenu != null)
      {
        var salesSubMenus = TaktDbSeedLogisticsMenu.GetSalesThirdMenus(salesMenu.Id);
        foreach (var menu in salesSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 质量管理下的三级菜单
      var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenu.Id);
      if (qualityMenu != null)
      {
        var qualitySubMenus = TaktDbSeedLogisticsMenu.GetQualityThirdLevelMenus(qualityMenu.Id);
        foreach (var menu in qualitySubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 设备管理下的三级目录
      var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenu.Id);
      if (equipmentMenu != null)
      {
        var equipmentSubMenus = TaktDbSeedLogisticsMenu.GetEquipmentThirdLevelMenus(equipmentMenu.Id);
        foreach (var menu in equipmentSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 文件管理下的三级菜单
      var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
      if (fileMenu != null)
      {
        var fileSubMenus = TaktDbSeedRoutineMenu.GetRoutineFileThirdMenus(fileMenu.Id);
        foreach (var menu in fileSubMenus)
        {
          menu.CreateBy = "Takt(Claude AI)";
          menu.CreateTime = DateTime.Now;
          menu.UpdateBy = "Takt(Claude AI)";
          menu.UpdateTime = DateTime.Now;
          var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
          menuNameToId[menu.MenuName] = savedMenu.Id;
          if (isNew) insertCount++; else updateCount++;
        }
      }

      // 项目管理下的三级菜单 - 已移除，项目管理功能已整合到其他模块
    }



    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化四级子菜单
  /// </summary>
  private async Task<(int, int)> InitializeFourthMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var fourthLevelMenus = new List<TaktMenu>();

    // 1. 日常事务下的四级子菜单
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 图书管理已改为三级菜单结构，不再需要四级子菜单

      // 办公用品下的四级子菜单
      var officeSuppliesMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "办公用品" && m.ParentId == routineMenu.Id);
      if (officeSuppliesMenu != null)
      {
        // 办公用品库存下的四级子菜单
        var officeSuppliesInventoryMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "库存" && m.ParentId == officeSuppliesMenu.Id);
        if (officeSuppliesInventoryMenu != null)
        {
          var officeSuppliesInventorySubMenus = TaktDbSeedRoutineMenu.GetRoutineOfficeSuppliesInventoryFourthMenus(officeSuppliesInventoryMenu.Id);
          fourthLevelMenus.AddRange(officeSuppliesInventorySubMenus);
        }

        // 办公用品领用下的四级子菜单
        var officeSuppliesUsageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == officeSuppliesMenu.Id);
        if (officeSuppliesUsageMenu != null)
        {
          var officeSuppliesUsageSubMenus = TaktDbSeedRoutineMenu.GetRoutineOfficeSuppliesUsageFourthMenus(officeSuppliesUsageMenu.Id);
          fourthLevelMenus.AddRange(officeSuppliesUsageSubMenus);
        }
      }

      // 文件管理下的四级子菜单
      var fileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
      if (fileMenu != null)
      {
        // 规章制度下的四级子菜单
        var regulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "规章制度" && m.ParentId == fileMenu.Id);
        if (regulationMenu != null)
        {
          var regulationSubMenus = TaktDbSeedRoutineMenu.GetRoutineRegulationFourthMenus(regulationMenu.Id);
          fourthLevelMenus.AddRange(regulationSubMenus);
        }

        // ISO文件下的四级子菜单
        var isoFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "ISO文件" && m.ParentId == fileMenu.Id);
        if (isoFileMenu != null)
        {
          var isoFileSubMenus = TaktDbSeedRoutineMenu.GetRoutineIsoFileFourthMenus(isoFileMenu.Id);
          fourthLevelMenus.AddRange(isoFileSubMenus);
        }

        // 公文文件下的四级子菜单
        var documentFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公文文件" && m.ParentId == fileMenu.Id);
        if (documentFileMenu != null)
        {
          var documentFileSubMenus = TaktDbSeedRoutineMenu.GetRoutineDocumentFileFourthMenus(documentFileMenu.Id);
          fourthLevelMenus.AddRange(documentFileSubMenus);
        }
      }



      // 公告通知下的四级子菜单
      var noticeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告通知" && m.ParentId == routineMenu.Id);
      if (noticeMenu != null)
      {
        // 消息下的四级子菜单
        var messageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "消息" && m.ParentId == noticeMenu.Id);
        if (messageMenu != null)
        {
          var messageSubMenus = TaktDbSeedRoutineMenu.GetRoutineMessageFourthMenus(messageMenu.Id);
          fourthLevelMenus.AddRange(messageSubMenus);
        }

        // 公告下的四级子菜单
        var announcementMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公告" && m.ParentId == noticeMenu.Id);
        if (announcementMenu != null)
        {
          var announcementSubMenus = TaktDbSeedRoutineMenu.GetRoutineAnnouncementFourthMenus(announcementMenu.Id);
          fourthLevelMenus.AddRange(announcementSubMenus);
        }

        // 通知下的四级子菜单
        var notificationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "通知" && m.ParentId == noticeMenu.Id);
        if (notificationMenu != null)
        {
          var notificationSubMenus = TaktDbSeedRoutineMenu.GetRoutineNotificationFourthMenus(notificationMenu.Id);
          fourthLevelMenus.AddRange(notificationSubMenus);
        }
      }

      // 医务管理下的四级子菜单
      var medicalMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "医务管理" && m.ParentId == routineMenu.Id);
      if (medicalMenu != null)
      {
        // 药品下的四级子菜单
        var medicineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "药品" && m.ParentId == medicalMenu.Id);
        if (medicineMenu != null)
        {
          var medicineSubMenus = TaktDbSeedRoutineMenu.GetRoutineMedicineFourthMenus(medicineMenu.Id);
          fourthLevelMenus.AddRange(medicineSubMenus);
        }

        // 医务领用下的四级子菜单
        var medicalUsageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "领用" && m.ParentId == medicalMenu.Id);
        if (medicalUsageMenu != null)
        {
          var medicalUsageSubMenus = TaktDbSeedRoutineMenu.GetRoutineMedicalUsageFourthMenus(medicalUsageMenu.Id);
          fourthLevelMenus.AddRange(medicalUsageSubMenus);
        }
      }



      // 文件管理下的四级子菜单 - 新增菜单
      var fileMenuForFourth = await MenuRepository.GetFirstAsync(m => m.MenuName == "文件管理" && m.ParentId == routineMenu.Id);
      if (fileMenuForFourth != null)
      {
        // 规章制度下的四级子菜单
        var regulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "规章制度" && m.ParentId == fileMenuForFourth.Id);
        if (regulationMenu != null)
        {
          var regulationSubMenus = TaktDbSeedRoutineMenu.GetRoutineRegulationFourthMenus(regulationMenu.Id);
          fourthLevelMenus.AddRange(regulationSubMenus);
        }

        // ISO文件下的四级子菜单
        var isoFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "ISO文件" && m.ParentId == fileMenuForFourth.Id);
        if (isoFileMenu != null)
        {
          var isoFileSubMenus = TaktDbSeedRoutineMenu.GetRoutineIsoFileFourthMenus(isoFileMenu.Id);
          fourthLevelMenus.AddRange(isoFileSubMenus);
        }

        // 公文文件下的四级子菜单
        var documentFileMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "公文文件" && m.ParentId == fileMenuForFourth.Id);
        if (documentFileMenu != null)
        {
          var documentFileSubMenus = TaktDbSeedRoutineMenu.GetRoutineDocumentFileFourthMenus(documentFileMenu.Id);
          fourthLevelMenus.AddRange(documentFileSubMenus);
        }
      }








    }

    // 2. 财务核算下的四级子菜单
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      // 全面预算
      var financeBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
      if (financeBudgetMenu != null)
      {
        // 预算编制下的四级目录
        var financeBudgetFormulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == financeBudgetMenu.Id);
        if (financeBudgetFormulationMenu != null)
        {
          var formulationMenus = TaktDbSeedFinanceMenu.GetFormulationFourthLevelMenus(financeBudgetFormulationMenu.Id);
          fourthLevelMenus.AddRange(formulationMenus);
        }

        // 预算控制下的四级菜单
        var financeBudgetControlMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算控制" && m.ParentId == financeBudgetMenu.Id);
        if (financeBudgetControlMenu != null)
        {
          var budgetControlMenus = TaktDbSeedFinanceMenu.GetBudgetControlFourthMenus(financeBudgetControlMenu.Id);
          fourthLevelMenus.AddRange(budgetControlMenus);
        }
      }
    }

    // 3. 后勤管理下的四级子菜单
    var logisticsMenuForFourth = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenuForFourth != null)
    {
      // 物料管理下的四级子菜单
      var materialMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料管理" && m.ParentId == logisticsMenuForFourth.Id);
      if (materialMenu != null)
      {
        // 物料数据下的四级子菜单
        var materialSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "物料数据" && m.ParentId == materialMenu.Id);
        if (materialSubMenu != null)
        {
          var materialMaterialSubMenus = TaktDbSeedLogisticsMenu.GetMaterialMasterFourthMenus(materialSubMenu.Id);
          fourthLevelMenus.AddRange(materialMaterialSubMenus);
        }

        // 采购管理下的四级子菜单
        var purchaseSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "采购管理" && m.ParentId == materialMenu.Id);
        if (purchaseSubMenu != null)
        {
          var materialPurchaseSubMenus = TaktDbSeedLogisticsMenu.GetMaterialPurchaseFourthMenus(purchaseSubMenu.Id);
          fourthLevelMenus.AddRange(materialPurchaseSubMenus);
        }

        // 样品管理下的四级子菜单
        var sampleSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "样品管理" && m.ParentId == materialMenu.Id);
        if (sampleSubMenu != null)
        {
          var materialSampleSubMenus = TaktDbSeedLogisticsMenu.GetMaterialSampleFourthMenus(sampleSubMenu.Id);
          fourthLevelMenus.AddRange(materialSampleSubMenus);
        }

        // 图纸管理下的四级子菜单
        var drawingSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "图纸管理" && m.ParentId == materialMenu.Id);
        if (drawingSubMenu != null)
        {
          var materialDrawingSubMenus = TaktDbSeedLogisticsMenu.GetMaterialDrawingFourthMenus(drawingSubMenu.Id);
          fourthLevelMenus.AddRange(materialDrawingSubMenus);
        }

        // 客供品管理下的四级子菜单
        var customerSubMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客供品管理" && m.ParentId == materialMenu.Id);
        if (customerSubMenu != null)
        {
          var materialCustomerSubMenus = TaktDbSeedLogisticsMenu.GetMaterialCustomerFourthMenus(customerSubMenu.Id);
          fourthLevelMenus.AddRange(materialCustomerSubMenus);
        }
      }

      // 生产管理下的四级子菜单
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenuForFourth.Id);
      if (productionMenu != null)
      {
        // 主数据下的四级子菜单
        var productionBasicMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "主数据" && m.ParentId == productionMenu.Id);
        if (productionBasicMenu != null)
        {
          var productionBasicSubMenus = TaktDbSeedLogisticsMenu.GetProductionMasterFourthMenus(productionBasicMenu.Id);
          fourthLevelMenus.AddRange(productionBasicSubMenus);
        }

        // 设计变更下的四级子菜单
        var productionChangeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
        if (productionChangeMenu != null)
        {
          var productionChangeSubMenus = TaktDbSeedLogisticsMenu.GetProductionChangeFourthMenus(productionChangeMenu.Id);
          fourthLevelMenus.AddRange(productionChangeSubMenus);

          // 设变录入下的四级子菜单
          var productionChangeInputMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == productionChangeMenu.Id);
          if (productionChangeInputMenu != null)
          {
            var productionChangeInputSubMenus = TaktDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(productionChangeInputMenu.Id);
            fourthLevelMenus.AddRange(productionChangeInputSubMenus);
          }
        }

        // SOP管理下的四级子菜单
        var productionSopMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "SOP管理" && m.ParentId == productionMenu.Id);
        if (productionSopMenu != null)
        {
          var productionSopSubMenus = TaktDbSeedLogisticsMenu.GetProductionSopFourthMenus(productionSopMenu.Id);
          fourthLevelMenus.AddRange(productionSopSubMenus);
        }

        // 技联管理下的四级子菜单
        var productionTechcontactMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "技联管理" && m.ParentId == productionMenu.Id);
        if (productionTechcontactMenu != null)
        {
          var productionTechcontactSubMenus = TaktDbSeedLogisticsMenu.GetProductionTechcontactFourthMenus(productionTechcontactMenu.Id);
          fourthLevelMenus.AddRange(productionTechcontactSubMenus);
        }
      }

      // 质量管理下的四级子菜单
      var qualityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量管理" && m.ParentId == logisticsMenuForFourth.Id);
      if (qualityMenu != null)
      {
        // 主数据下的四级子菜单
        var qualityBasicMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "主数据" && m.ParentId == qualityMenu.Id);
        if (qualityBasicMenu != null)
        {
          var qualityBasicSubMenus = TaktDbSeedLogisticsMenu.GetQualityMasterFourthMenus(qualityBasicMenu.Id);
          fourthLevelMenus.AddRange(qualityBasicSubMenus);
        }

        // 质量检验下的四级子菜单
        var qualityInspectionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量检验" && m.ParentId == qualityMenu.Id);
        if (qualityInspectionMenu != null)
        {
          var qualityInspectionSubMenus = TaktDbSeedLogisticsMenu.GetQualityInspectionFourthMenus(qualityInspectionMenu.Id);
          fourthLevelMenus.AddRange(qualityInspectionSubMenus);
        }

        // 质量追溯下的四级子菜单
        var qualityTraceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量追溯" && m.ParentId == qualityMenu.Id);
        if (qualityTraceMenu != null)
        {
          var qualityTraceSubMenus = TaktDbSeedLogisticsMenu.GetQualityTraceFourthMenus(qualityTraceMenu.Id);
          fourthLevelMenus.AddRange(qualityTraceSubMenus);
        }

        // 质量成本下的四级子菜单
        var qualityCostMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "质量成本" && m.ParentId == qualityMenu.Id);
        if (qualityCostMenu != null)
        {
          var qualityCostSubMenus = TaktDbSeedLogisticsMenu.GetQualityCostFourthMenus(qualityCostMenu.Id);
          fourthLevelMenus.AddRange(qualityCostSubMenus);
        }

        // 检验计划下的四级子菜单
        var qualityPlanMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "检验计划" && m.ParentId == qualityMenu.Id);
        if (qualityPlanMenu != null)
        {
          var qualityPlanSubMenus = TaktDbSeedLogisticsMenu.GetQualityPlanFourthMenus(qualityPlanMenu.Id);
          fourthLevelMenus.AddRange(qualityPlanSubMenus);
        }
      }



      // 设备管理下的四级子菜单
      var equipmentMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备管理" && m.ParentId == logisticsMenuForFourth.Id);
      if (equipmentMenu != null)
      {
        // 设备数据下的四级子菜单
        var equipmentMasterMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设备数据" && m.ParentId == equipmentMenu.Id);
        if (equipmentMasterMenu != null)
        {
          var equipmentMasterSubMenus = TaktDbSeedLogisticsMenu.GetEquipmentMasterFourthMenus(equipmentMasterMenu.Id);
          fourthLevelMenus.AddRange(equipmentMasterSubMenus);
        }

        // 维保管理下的四级子菜单
        var equipmentMaintenanceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "维保管理" && m.ParentId == equipmentMenu.Id);
        if (equipmentMaintenanceMenu != null)
        {
          var equipmentMaintenanceSubMenus = TaktDbSeedLogisticsMenu.GetEquipmentMaintenanceFourthMenus(equipmentMaintenanceMenu.Id);
          fourthLevelMenus.AddRange(equipmentMaintenanceSubMenus);
        }
      }
    }

    // 4. 工作流程下的四级子菜单
    var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
    if (workflowMenu != null)
    {
      // 流程引擎下的四级子菜单
      var engineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "流程引擎" && m.ParentId == workflowMenu.Id);
      if (engineMenu != null)
      {
        var engineSubMenus = TaktDbSeedWorkflowMenu.GetWorkflowEngineThirdMenus(engineMenu.Id);
        fourthLevelMenus.AddRange(engineSubMenus);
      }

      // 流程管理下的四级子菜单
      var manageMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "流程管理" && m.ParentId == workflowMenu.Id);
      if (manageMenu != null)
      {
        var manageSubMenus = TaktDbSeedWorkflowMenu.GetWorkflowManageThirdMenus(manageMenu.Id);
        fourthLevelMenus.AddRange(manageSubMenus);
      }
    }

    // 处理所有四级子菜单
    foreach (var menu in fourthLevelMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化四级目录
  /// </summary>
  private async Task<(int, int)> InitializeFourthLevelMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var fourthLevelMenus = new List<TaktMenu>();

    // 1. 日常事务下的四级目录
    var routineMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "日常事务" && m.ParentId == 0);
    if (routineMenu != null)
    {
      // 日常事务的四级目录处理
      // 这里可以添加日常事务的四级目录逻辑
    }

    // 2. 财务核算下的四级目录
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
      if (budgetMenu != null)
      {
        var formulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
        if (formulationMenu != null)
        {
          var formulationSubMenus = TaktDbSeedFinanceMenu.GetFormulationFourthLevelMenus(formulationMenu.Id);
          fourthLevelMenus.AddRange(formulationSubMenus);
        }
      }
    }
    else
    {
      _logger.Info("未找到财务核算目录");
    }

    // 3. 后勤管理下的四级目录
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      // 生产管理
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
      if (productionMenu != null)
      {
        // 设计变更下的四级目录
        var changeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
        if (changeMenu != null)
        {
          var changeSubMenus = TaktDbSeedLogisticsMenu.GetProductionChangeFourthLevelMenus(changeMenu.Id);
          fourthLevelMenus.AddRange(changeSubMenus);
        }

        // 执行管理下的四级目录
        var executionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "执行管理" && m.ParentId == productionMenu.Id);
        if (executionMenu != null)
        {
          var executionSubMenus = TaktDbSeedLogisticsMenu.GetProductionExecutionFourthLevelMenus(executionMenu.Id);
          fourthLevelMenus.AddRange(executionSubMenus);
        }

        // SOP管理下的四级目录
        var sopMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "SOP管理" && m.ParentId == productionMenu.Id);
        if (sopMenu != null)
        {
          var sopSubMenus = TaktDbSeedLogisticsMenu.GetProductionSopFourthMenus(sopMenu.Id);
          fourthLevelMenus.AddRange(sopSubMenus);
        }

        // 技联管理下的四级目录
        var techcontactMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "技联管理" && m.ParentId == productionMenu.Id);
        if (techcontactMenu != null)
        {
          var techcontactSubMenus = TaktDbSeedLogisticsMenu.GetProductionTechcontactFourthMenus(techcontactMenu.Id);
          fourthLevelMenus.AddRange(techcontactSubMenus);
        }
      }

      // 客服管理
      var serviceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客服管理" && m.ParentId == logisticsMenu.Id);
      if (serviceMenu != null)
      {
        // 服务管理下的四级目录
        var serviceServiceMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客户服务" && m.ParentId == serviceMenu.Id);
        if (serviceServiceMenu != null)
        {
          var serviceServiceSubMenus = TaktDbSeedLogisticsMenu.GetServiceCustomerServiceFourthMenus(serviceServiceMenu.Id);
          fourthLevelMenus.AddRange(serviceServiceSubMenus);
        }

        // 客诉管理下的四级目录
        var serviceComplaintMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "客诉管理" && m.ParentId == serviceMenu.Id);
        if (serviceComplaintMenu != null)
        {
          var serviceComplaintSubMenus = TaktDbSeedLogisticsMenu.GetServiceCustomerComplaintFourthMenus(serviceComplaintMenu.Id);
          fourthLevelMenus.AddRange(serviceComplaintSubMenus);
        }
      }
    }

    // 4. 工作流程下的四级目录
    var workflowMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工作流程" && m.ParentId == 0);
    if (workflowMenu != null)
    {
      // 工作流程的四级目录处理
      // 这里可以添加工作流程的四级目录逻辑
    }

    // 5. 身份认证下的四级目录
    var identityMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "身份认证" && m.ParentId == 0);
    if (identityMenu != null)
    {
      // 身份认证的四级目录处理
      // 这里可以添加身份认证的四级目录逻辑
    }

    // 6. 代码生成下的四级目录
    var generatorMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "代码生成" && m.ParentId == 0);
    if (generatorMenu != null)
    {
      // 代码生成的四级目录处理
      // 这里可以添加代码生成的四级目录逻辑
    }

    // 7. 审批日志下的四级目录
    var auditMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "审批日志" && m.ParentId == 0);
    if (auditMenu != null)
    {
      // 审批日志的四级目录处理
      // 这里可以添加审批日志的四级目录逻辑
    }

    // 8. 实时通信下的四级目录
    var realtimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "实时通信" && m.ParentId == 0);
    if (realtimeMenu != null)
    {
      // 实时通信的四级目录处理
      // 这里可以添加实时通信的四级目录逻辑
    }

    // 处理所有四级目录菜单
    foreach (var menu in fourthLevelMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化五级目录
  /// </summary>
  private async Task<(int, int)> InitializeFifthLevelMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var fifthLevelMenus = new List<TaktMenu>();

    // 财务核算模块没有五级目录，此方法留空或移除相关逻辑

    // 处理所有五级目录菜单
    foreach (var menu in fifthLevelMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化五级子菜单
  /// </summary>
  private async Task<(int, int)> InitializeFifthMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var fifthMenus = new List<TaktMenu>();

    // 财务核算
    var financeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务核算" && m.ParentId == 0);
    if (financeMenu != null)
    {
      var budgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "全面预算" && m.ParentId == financeMenu.Id);
      if (budgetMenu != null)
      {
        var formulationMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "预算编制" && m.ParentId == budgetMenu.Id);
        if (formulationMenu != null)
        {
          // 销售预算下的五级菜单
          var salesBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "销售预算" && m.ParentId == formulationMenu.Id);
          if (salesBudgetMenu != null)
          {
            var salesBudgetSubMenus = TaktDbSeedFinanceMenu.GetSalesBudgetFifthMenus(salesBudgetMenu.Id);
            fifthMenus.AddRange(salesBudgetSubMenus);
          }

          // 生产预算下的五级菜单
          var productionBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产预算" && m.ParentId == formulationMenu.Id);
          if (productionBudgetMenu != null)
          {
            var productionBudgetSubMenus = TaktDbSeedFinanceMenu.GetProductionBudgetFifthMenus(productionBudgetMenu.Id);
            fifthMenus.AddRange(productionBudgetSubMenus);
          }

          // 成本预算下的五级菜单
          var costBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "成本预算" && m.ParentId == formulationMenu.Id);
          if (costBudgetMenu != null)
          {
            var costBudgetSubMenus = TaktDbSeedFinanceMenu.GetCostBudgetFifthMenus(costBudgetMenu.Id);
            fifthMenus.AddRange(costBudgetSubMenus);
          }

          // 费用预算下的五级菜单
          var expenseBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "费用预算" && m.ParentId == formulationMenu.Id);
          if (expenseBudgetMenu != null)
          {
            var expenseBudgetSubMenus = TaktDbSeedFinanceMenu.GetExpenseBudgetFifthMenus(expenseBudgetMenu.Id);
            fifthMenus.AddRange(expenseBudgetSubMenus);
          }

          // 财务预算下的五级菜单
          var financialBudgetMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "财务预算" && m.ParentId == formulationMenu.Id);
          if (financialBudgetMenu != null)
          {
            var financialBudgetSubMenus = TaktDbSeedFinanceMenu.GetFinancialBudgetFifthMenus(financialBudgetMenu.Id);
            fifthMenus.AddRange(financialBudgetSubMenus);
          }
        }
      }
    }

    // 后勤管理
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
      if (productionMenu != null)
      {
        // 设计变更 -> 设变录入
        var changeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设计变更" && m.ParentId == productionMenu.Id);
        if (changeMenu != null)
        {
          var inputMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "设变录入" && m.ParentId == changeMenu.Id);
          if (inputMenu != null)
          {
            var inputSubMenus = TaktDbSeedLogisticsMenu.GetProductionChangeInputFifthMenus(inputMenu.Id);
            fifthMenus.AddRange(inputSubMenus);
          }
        }

        // 执行管理 -> 产出
        var executionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "执行管理" && m.ParentId == productionMenu.Id);
        if (executionMenu != null)
        {
          var outputMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "产出" && m.ParentId == executionMenu.Id);
          if (outputMenu != null)
          {
            var outputSubMenus = TaktDbSeedLogisticsMenu.GetProductionOutputFifthMenus(outputMenu.Id);
            fifthMenus.AddRange(outputSubMenus);
          }

          // 执行管理 -> 不良
          var defectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "不良" && m.ParentId == executionMenu.Id);
          if (defectMenu != null)
          {
            var defectSubMenus = TaktDbSeedLogisticsMenu.GetProductionDefectFifthLevelMenus(defectMenu.Id);
            fifthMenus.AddRange(defectSubMenus);

            var defectMenuItems = TaktDbSeedLogisticsMenu.GetProductionDefectFifthMenus(defectMenu.Id);
            fifthMenus.AddRange(defectMenuItems);
          }

          // 执行管理 -> 工数
          var worktimeMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "工数" && m.ParentId == executionMenu.Id);
          if (worktimeMenu != null)
          {
            var worktimeSubMenus = TaktDbSeedLogisticsMenu.GetProductionWorktimeFifthMenus(worktimeMenu.Id);
            fifthMenus.AddRange(worktimeSubMenus);
          }
        }
      }
    }



    // 处理所有五级菜单
    foreach (var menu in fifthMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化六级菜单
  /// </summary>
  private async Task<(int, int)> InitializeSixthMenusAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;
    var sixthMenus = new List<TaktMenu>();

    // 后勤管理
    var logisticsMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "后勤管理" && m.ParentId == 0);
    if (logisticsMenu != null)
    {
      var productionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "生产管理" && m.ParentId == logisticsMenu.Id);
      if (productionMenu != null)
      {
        // 执行管理 -> 不良 -> 制二
        var executionMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "执行管理" && m.ParentId == productionMenu.Id);
        if (executionMenu != null)
        {
          var defectMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == "不良" && m.ParentId == executionMenu.Id);
          if (defectMenu != null)
          {
            var workshop2Menu = await MenuRepository.GetFirstAsync(m => m.MenuName == "制二" && m.ParentId == defectMenu.Id);
            if (workshop2Menu != null)
            {
              var workshop2SubMenus = TaktDbSeedLogisticsMenu.GetProductionDefectWorkshop2SixthMenus(workshop2Menu.Id);
              sixthMenus.AddRange(workshop2SubMenus);
            }
          }
        }
      }
    }

    // 处理所有六级菜单
    foreach (var menu in sixthMenus)
    {
      menu.CreateBy = "Takt(Claude AI)";
      menu.CreateTime = DateTime.Now;
      menu.UpdateBy = "Takt(Claude AI)";
      menu.UpdateTime = DateTime.Now;
      var (isNew, savedMenu) = await CreateOrUpdateMenuAsync(menu, false);
      menuNameToId[menu.MenuName] = savedMenu.Id;

      if (isNew) insertCount++;
      else updateCount++;
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 初始化按钮
  /// </summary>
  private async Task<(int, int)> InitializeButtonsAsync(Dictionary<string, long> menuNameToId)
  {
    int insertCount = 0;
    int updateCount = 0;

    // 获取所有菜单类型的菜单
    var menus = await MenuRepository.GetListAsync(m => m.MenuType == 1);

    foreach (var menu in menus)
    {
      if (!string.IsNullOrEmpty(menu.Perms))
      {
        var modulePrefix = menu.Perms.Split(':')[0];
        var buttons = GetMenuButtons(menu, modulePrefix);

        foreach (var button in buttons)
        {
          button.ParentId = menu.Id; // 使用数据库中的菜单ID
          button.CreateBy = "Takt(Claude AI)";
          button.CreateTime = DateTime.Now;
          button.UpdateBy = "Takt(Claude AI)";
          button.UpdateTime = DateTime.Now;
          var (isNew, savedButton) = await CreateOrUpdateMenuAsync(button, false);

          if (isNew) insertCount++;
          else updateCount++;

          _logger.Info($"[{(isNew ? "创建" : "更新")}] 按钮 '{button.MenuName}' (ParentId: {button.ParentId}) {(isNew ? "创建" : "更新")}成功");
        }
      }
    }

    return (insertCount, updateCount);
  }

  /// <summary>
  /// 创建或更新菜单
  /// </summary>
  private async Task<(bool isNew, TaktMenu menu)> CreateOrUpdateMenuAsync(TaktMenu menu, bool isTopLevel)
  {
    Expression<Func<TaktMenu, bool>> query;
    if (isTopLevel)
    {
      query = m => m.MenuName == menu.MenuName && m.ParentId == 0;
    }
    else
    {
      query = m => m.MenuName == menu.MenuName && m.ParentId == menu.ParentId;
    }

    var existingMenu = await MenuRepository.GetFirstAsync(query);

    if (existingMenu == null)
    {
      await MenuRepository.CreateAsync(menu);
      _logger.Info($"[创建] {(isTopLevel ? "顶级" : "")}菜单 '{menu.MenuName}' (ParentId: {menu.ParentId}) 创建成功");

      // 创建后重新查询以获取正确的ID
      var createdMenu = await MenuRepository.GetFirstAsync(m => m.MenuName == menu.MenuName && m.ParentId == menu.ParentId);
      return (true, createdMenu ?? menu);
    }

    // 更新菜单属性，保持ParentId不变
    existingMenu.MenuName = menu.MenuName;
    existingMenu.TransKey = menu.TransKey;
    existingMenu.OrderNum = menu.OrderNum;
    existingMenu.Path = menu.Path;
    existingMenu.Component = menu.Component;
    existingMenu.QueryParams = menu.QueryParams;
    existingMenu.IsExternal = menu.IsExternal;
    existingMenu.IsCache = menu.IsCache;
    // 修复：只有当新的MenuType是目录时，才强制更新
    if (menu.MenuType == 0)
    {
      existingMenu.MenuType = 0;
    }
    existingMenu.Visible = menu.Visible;
    existingMenu.MenuStatus = menu.MenuStatus;
    existingMenu.Perms = menu.Perms;
    existingMenu.Icon = menu.Icon;

    existingMenu.Remark = menu.Remark;
    existingMenu.UpdateBy = "Takt(Claude AI)";
    existingMenu.UpdateTime = DateTime.Now;

    await MenuRepository.UpdateAsync(existingMenu);
    _logger.Info($"[更新] {(isTopLevel ? "顶级" : "")}菜单 '{existingMenu.MenuName}' (ParentId: {existingMenu.ParentId}) 更新成功");
    return (false, existingMenu);
  }

  /// <summary>
  /// 获取菜单按钮列表
  /// </summary>
  private List<TaktMenu> GetMenuButtons(TaktMenu menu, string modulePrefix)
  {
    var buttons = new List<TaktMenu>();

    // 通用按钮
    var buttonNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销" };
    var buttonPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke" };

    // 认证通用按钮
    var buttonIdNames = new[] { "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "授权", "分配", "重置密码", "变更密码", "清空", "截断" };
    var buttonIdPerms = new[] { "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "authorize", "allocate", "resetpwd", "changepwd", "empty", "truncate" };

    // 代码生成按钮
    var buttonGenNames = new[] { "查询", "新增", "修改", "删除", "生成", "预览", "下载", "同步", "导入", "导出", "模板", "字段", "表", "数据库", "初始化", "清空", "截断" };
    var buttonGenPerms = new[] { "query", "create", "update", "delete", "generate", "preview", "download", "sync", "import", "export", "template", "columns", "tables", "databases", "initialize", "empty", "truncate" };

    // 工作流按钮 - 按实体分类，排除重复
    var buttonFlowNames = new[] {
            // 通用操作按钮
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "复制", "克隆",
            
            // 流程通用操作
            "暂停", "恢复", "提交", "撤回", "转办", "委托", "退回", "催办", "加签", "减签", "进度", "历史",
            
            // TaktScheme 流程定义专用操作
            "发布", "停用", "启用", "版本", "设计", "配置", "验证",
            
            // TaktInstance 流程实例专用操作  
            "启动", "终止",
            
            // TaktForm 表单操作
            "字段管理", "权限设置", "数据源配置", "主题设置", "表单数据",
            
            // TaktInstanceTrans 流转历史管理
            "流转归档", "流转清理"
        };
    var buttonFlowPerms = new[] {
            // 通用操作权限
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "copy", "clone",
            
            // 流程通用权限
            "suspend", "resume", "submit", "withdraw", "transfer", "delegate", "return", "urge", "addsign", "subsign", "progress", "history",
            
            // TaktScheme 流程定义专用权限
            "publish", "disable", "enable", "version", "design", "config", "validate",
            
            // TaktInstance 流程实例专用权限
            "start", "terminate",
            
            // TaktForm 表单权限
            "field", "permission", "datasource", "theme", "data",
            
            // TaktInstanceTrans 流转历史权限
            "archive", "clean"
        };

    // 日常事务通用按钮 - 适用于所有日常事务模块
    var buttonRoutineNames = new[] {
            // 基础操作按钮
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销",
            
            // 克隆操作按钮
            "克隆", "复制",
            
            // 文档操作按钮
            "保存草稿", "删除草稿", "发送", "撤回", "转发", "回复", "已读", "未读", "传阅", "签收", "催办", "确认",
            
            // 社交互动按钮
            "点赞", "取消点赞", "收藏", "取消收藏", "分享", "取消分享", "评论", "取消评论", "举报", "取消举报", "关注", "取消关注",
            
            // 文件操作按钮
            "上传", "下载", "归档", "销毁", "版本",
            
            // 系统操作按钮
            "运行", "停止", "重启", "刷新", "重置", "清空"
        };
    var buttonRoutinePerms = new[] {
            // 基础操作权限
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke",
            
            // 克隆操作权限
            "clone", "copy",
            
            // 文档操作权限
            "draft", "deletedraft", "send", "withdraw", "forward", "reply", "read", "unread", "circulate", "sign", "urge", "confirm",
            
            // 社交互动权限
            "like", "unlike", "favorite", "unfavorite", "share", "unshare", "comment", "uncomment", "flagging", "unflagging", "follow", "unfollow",
            
            // 文件操作权限
            "upload", "download", "archive", "destroy", "version",
            
            // 系统操作权限
            "run", "stop", "restart", "refresh", "reset", "empty"
        };

    // 人力资源通用按钮 - 适用于所有人力资源模块
    var buttonHrmNames = new[] {
            // 基础操作按钮
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算"
        };
    var buttonHrmPerms = new[] {
            // 基础操作权限
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate"
        };

    // 会计通用按钮 - 适用于所有会计模块
    var buttonAccountingNames = new[] {
            // 基础操作按钮
            "查询", "新增", "修改", "删除", "详情", "预览", "打印", "导入", "导出", "模板", "审批", "撤销", "核算",
            "记账", "结账", "对账", "支付", "折旧", "报销", "冲销", "计提", "账期", "结转", "作废"
        };
    var buttonAccountingPerms = new[] {
            // 基础操作权限
            "query", "create", "update", "delete", "detail", "preview", "print", "import", "export", "template", "approve", "revoke", "calculate",
            "book", "closing", "reconcile", "payment", "depreciation", "reimburse", "reversal", "accrual", "period", "carryforward", "cancel"
        };

    // 从菜单的权限标识中获取菜单标识
    // 使用Range语法截取第一个冒号和最后一个冒号之间的字符串
    var menuPerm = string.Empty;
    if (!string.IsNullOrEmpty(menu.Perms))
    {
      var parts = menu.Perms.Split(':');
      if (parts.Length > 2)
      {
        // 截取第一个冒号之后到最后一个冒号之前的部分
        menuPerm = string.Join(":", parts[1..^1]).ToLower();
      }
      else if (parts.Length == 2)
      {
        // 只有一个冒号的情况，取第一部分
        menuPerm = parts[0].ToLower();
      }
    }

    string[] names;
    string[] perms;

    // 根据模块前缀选择对应的按钮配置
    switch (modulePrefix.ToLower())
    {
      case "identity":
        names = buttonIdNames;
        perms = buttonIdPerms;
        break;

      case "generator":
        names = buttonGenNames;
        perms = buttonGenPerms;
        break;

      case "workflow":
        names = buttonFlowNames;
        perms = buttonFlowPerms;
        break;

      case "routine":
        names = buttonRoutineNames;
        perms = buttonRoutinePerms;
        break;

      case "hrm":
        names = buttonHrmNames;
        perms = buttonHrmPerms;
        break;

      case "accounting":
        names = buttonAccountingNames;
        perms = buttonAccountingPerms;
        break;

      default:
        names = buttonNames;
        perms = buttonPerms;
        break;
    }

    for (int i = 0; i < names.Length; i++)
    {
      buttons.Add(new TaktMenu
      {
        MenuName = names[i],
        TransKey = "button." + perms[i],
        ParentId = menu.Id,
        OrderNum = i + 1,
        Path = string.Empty,
        Component = string.Empty,
        MenuType = 2,
        Perms = $"{modulePrefix.ToLower()}:{menuPerm}:{perms[i].ToLower()}",
        Icon = string.Empty,

        CreateBy = "Takt(Claude AI)",
        CreateTime = DateTime.Now,
        UpdateBy = "Takt(Claude AI)",
        UpdateTime = DateTime.Now,
        Remark = $"{menu.MenuName}{names[i]}按钮"
      });
    }

    return buttons;
  }


}

