//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFinanceMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务核算菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFinanceMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 财务核算菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 财务核算菜单数据初始化类
/// </summary>
public class TaktDbSeedFinanceMenu
{
    /// <summary>
    /// 获取财务核算二级目录列表
    /// </summary>
    public static List<TaktMenu> GetFinanceSecondLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "管理会计", TransKey = "menu.accounting.financial._self", ParentId = parentId, OrderNum = 1, Path = "financial", Component = "", MenuType = 0, Perms = "", Icon = "FundOutlined", Remark = "管理会计子目录" },
        new TaktMenu { MenuName = "控制会计", TransKey = "menu.accounting.controlling._self", ParentId = parentId, OrderNum = 2, Path = "controlling", Component = "", MenuType = 0, Perms = "", Icon = "ControlOutlined", Remark = "控制会计子目录" },
        new TaktMenu { MenuName = "全面预算", TransKey = "menu.accounting.budget._self", ParentId = parentId, OrderNum = 3, Path = "budget", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "全面预算子目录" }
    };

    /// <summary>
    /// 获取管理会计三级菜单列表
    /// </summary>
    public static List<TaktMenu> GetFinanceManagementThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "公司信息", TransKey = "menu.accounting.financial.company", ParentId = parentId, OrderNum = 1, Path = "company", Component = "accounting/financial/company/index", MenuType = 1, Perms = "accounting:financial:company:list", Icon = "BankOutlined", Remark = "公司信息管理" },
        new TaktMenu { MenuName = "银行信息", TransKey = "menu.accounting.financial.bank", ParentId = parentId, OrderNum = 2, Path = "bank", Component = "accounting/financial/bank/index", MenuType = 1, Perms = "accounting:financial:bank:list", Icon = "BankOutlined", Remark = "银行信息管理" },
        new TaktMenu { MenuName = "会计科目", TransKey = "menu.accounting.financial.account", ParentId = parentId, OrderNum = 3, Path = "account", Component = "accounting/financial/account/index", MenuType = 1, Perms = "accounting:financial:account:list", Icon = "BookOutlined", Remark = "会计科目管理" },
        new TaktMenu { MenuName = "公司科目", TransKey = "menu.accounting.financial.companyaccount", ParentId = parentId, OrderNum = 4, Path = "companyaccount", Component = "accounting/financial/companyaccount/index", MenuType = 1, Perms = "accounting:financial:companyaccount:list", Icon = "BankOutlined", Remark = "公司科目管理" },
        new TaktMenu { MenuName = "固定资产", TransKey = "menu.accounting.financial.fixedasset", ParentId = parentId, OrderNum = 5, Path = "fixedasset", Component = "accounting/financial/fixedasset/index", MenuType = 1, Perms = "accounting:financial:fixedasset:list", Icon = "SafetyCertificateOutlined", Remark = "固定资产管理" },
        new TaktMenu { MenuName = "总账", TransKey = "menu.accounting.financial.ledger", ParentId = parentId, OrderNum = 6, Path = "ledger", Component = "accounting/financial/ledger/index", MenuType = 1, Perms = "accounting:financial:ledger:list", Icon = "BookOutlined", Remark = "总账管理" },
        new TaktMenu { MenuName = "应付", TransKey = "menu.accounting.financial.payable", ParentId = parentId, OrderNum = 7, Path = "payable", Component = "accounting/financial/payable/index", MenuType = 1, Perms = "accounting:financial:payable:list", Icon = "TagOutlined", Remark = "应付管理" }
    };

    /// <summary>
    /// 获取控制会计三级菜单列表
    /// </summary>
    public static List<TaktMenu> GetFinanceControlThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "利润中心", TransKey = "menu.accounting.controlling.profitcenter", ParentId = parentId, OrderNum = 1, Path = "profitcenter", Component = "accounting/controlling/profitcenter/index", MenuType = 1, Perms = "accounting:controlling:profitcenter:list", Icon = "RiseOutlined", Remark = "利润中心管理" },
        new TaktMenu { MenuName = "成本中心", TransKey = "menu.accounting.controlling.costcenter", ParentId = parentId, OrderNum = 2, Path = "costcenter", Component = "accounting/controlling/costcenter/index", MenuType = 1, Perms = "accounting:controlling:costcenter:list", Icon = "ClusterOutlined", Remark = "成本中心管理" },
        new TaktMenu { MenuName = "成本要素", TransKey = "menu.accounting.controlling.costelement", ParentId = parentId, OrderNum = 3, Path = "costelement", Component = "accounting/controlling/costelement/index", MenuType = 1, Perms = "accounting:controlling:costelement:list", Icon = "FundOutlined", Remark = "成本要素管理" }
    };

    /// <summary>
    /// 获取全面预算三级目录列表
    /// </summary>
    public static List<TaktMenu> GetBudgetThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "预算编制", TransKey = "menu.accounting.budget.formulation._self", ParentId = parentId, OrderNum = 1, Path = "formulation", Component = "", MenuType = 0, Perms = "", Icon = "FormOutlined", Remark = "预算编制目录" },
        new TaktMenu { MenuName = "预算控制", TransKey = "menu.accounting.budget.control._self", ParentId = parentId, OrderNum = 2, Path = "control", Component = "", MenuType = 0, Perms = "", Icon = "ControlOutlined", Remark = "预算控制目录" }
    };

    /// <summary>
    /// 获取预算编制四级目录列表
    /// </summary>
    public static List<TaktMenu> GetFormulationFourthLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "销售预算", TransKey = "menu.accounting.budget.formulation.sales._self", ParentId = parentId, OrderNum = 1, Path = "sales", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingCartOutlined", Remark = "销售预算目录" },
        new TaktMenu { MenuName = "生产预算", TransKey = "menu.accounting.budget.formulation.production._self", ParentId = parentId, OrderNum = 2, Path = "production", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "生产预算目录" },
        new TaktMenu { MenuName = "成本预算", TransKey = "menu.accounting.budget.formulation.cost._self", ParentId = parentId, OrderNum = 3, Path = "cost", Component = "", MenuType = 0, Perms = "", Icon = "AccountBookOutlined", Remark = "成本预算目录" },
        new TaktMenu { MenuName = "费用预算", TransKey = "menu.accounting.budget.formulation.expense._self", ParentId = parentId, OrderNum = 4, Path = "expense", Component = "", MenuType = 0, Perms = "", Icon = "MoneyCollectOutlined", Remark = "费用预算目录" },
        new TaktMenu { MenuName = "财务预算", TransKey = "menu.accounting.budget.formulation.financial._self", ParentId = parentId, OrderNum = 5, Path = "financial", Component = "", MenuType = 0, Perms = "", Icon = "AccountBookOutlined", Remark = "财务预算目录" }
    };

    /// <summary>
    /// 获取预算控制四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetBudgetControlFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "预算看板", TransKey = "menu.accounting.budget.control.dashboard", ParentId = parentId, OrderNum = 1, Path = "dashboard", Component = "accounting/budget/control/dashboard/index", MenuType = 1, Perms = "accounting:budget:control:dashboard:list", Icon = "DashboardOutlined", Remark = "预算看板" },
        new TaktMenu { MenuName = "预算审批", TransKey = "menu.accounting.budget.control.approval", ParentId = parentId, OrderNum = 2, Path = "approval", Component = "accounting/budget/control/approval/index", MenuType = 1, Perms = "accounting:budget:control:approval:list", Icon = "CheckSquareOutlined", Remark = "预算审批" }
    };

    /// <summary>
    /// 获取销售预算五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetSalesBudgetFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "销售成本", TransKey = "menu.accounting.budget.formulation.sales.cost", ParentId = parentId, OrderNum = 1, Path = "cost", Component = "accounting/budget/formulation/sales/cost/index", MenuType = 1, Perms = "accounting:budget:formulation:sales:cost:list", Icon = "MoneyCollectOutlined", Remark = "销售成本预算" },
        new TaktMenu { MenuName = "销售滚动", TransKey = "menu.accounting.budget.formulation.sales.rolling", ParentId = parentId, OrderNum = 2, Path = "rolling", Component = "accounting/budget/formulation/sales/rolling/index", MenuType = 1, Perms = "accounting:budget:formulation:sales:rolling:list", Icon = "InteractionOutlined", Remark = "销售滚动预算" }
    };

    /// <summary>
    /// 获取生产预算五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionBudgetFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "生产辅料", TransKey = "menu.accounting.budget.formulation.production.auxiliary", ParentId = parentId, OrderNum = 1, Path = "auxiliary", Component = "accounting/budget/formulation/production/auxiliary/index", MenuType = 1, Perms = "accounting:budget:formulation:production:auxiliary:list", Icon = "ToolOutlined", Remark = "生产辅料预算" },
        new TaktMenu { MenuName = "生产人工", TransKey = "menu.accounting.budget.formulation.production.labor", ParentId = parentId, OrderNum = 2, Path = "labor", Component = "accounting/budget/formulation/production/labor/index", MenuType = 1, Perms = "accounting:budget:formulation:production:labor:list", Icon = "TeamOutlined", Remark = "生产人工预算" },
        new TaktMenu { MenuName = "生产制造", TransKey = "menu.accounting.budget.formulation.production.manufacturing", ParentId = parentId, OrderNum = 3, Path = "manufacturing", Component = "accounting/budget/formulation/production/manufacturing/index", MenuType = 1, Perms = "accounting:budget:formulation:production:manufacturing:list", Icon = "BuildOutlined", Remark = "生产制造预算" }
    };

    /// <summary>
    /// 获取成本预算五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetCostBudgetFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "直接材料", TransKey = "menu.accounting.budget.formulation.cost.directmaterial", ParentId = parentId, OrderNum = 1, Path = "directmaterial", Component = "accounting/budget/formulation/cost/directmaterial/index", MenuType = 1, Perms = "accounting:budget:formulation:cost:directmaterial:list", Icon = "InboxOutlined", Remark = "直接材料成本预算" },
        new TaktMenu { MenuName = "直接人工", TransKey = "menu.accounting.budget.formulation.cost.directlabor", ParentId = parentId, OrderNum = 2, Path = "directlabor", Component = "accounting/budget/formulation/cost/directlabor/index", MenuType = 1, Perms = "accounting:budget:formulation:cost:directlabor:list", Icon = "TeamOutlined", Remark = "直接人工成本预算" },
        new TaktMenu { MenuName = "制造费用", TransKey = "menu.accounting.budget.formulation.cost.manufacturing", ParentId = parentId, OrderNum = 3, Path = "manufacturing", Component = "accounting/budget/formulation/cost/manufacturing/index", MenuType = 1, Perms = "accounting:budget:formulation:cost:manufacturing:list", Icon = "BuildOutlined", Remark = "制造费用预算" }
    };

    /// <summary>
    /// 获取费用预算五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetExpenseBudgetFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "销售费用", TransKey = "menu.accounting.budget.formulation.expense.sales", ParentId = parentId, OrderNum = 1, Path = "sales", Component = "accounting/budget/formulation/expense/sales/index", MenuType = 1, Perms = "accounting:budget:formulation:expense:sales:list", Icon = "ShoppingCartOutlined", Remark = "销售费用预算" },
        new TaktMenu { MenuName = "管理费用", TransKey = "menu.accounting.budget.formulation.expense.manage", ParentId = parentId, OrderNum = 2, Path = "manage", Component = "accounting/budget/formulation/expense/manage/index", MenuType = 1, Perms = "accounting:budget:formulation:expense:manage:list", Icon = "SettingOutlined", Remark = "管理费用预算" },
        new TaktMenu { MenuName = "财务费用", TransKey = "menu.accounting.budget.formulation.expense.financial", ParentId = parentId, OrderNum = 3, Path = "financial", Component = "accounting/budget/formulation/expense/financial/index", MenuType = 1, Perms = "accounting:budget:formulation:expense:financial:list", Icon = "AccountBookOutlined", Remark = "财务费用预算" }
    };

    /// <summary>
    /// 获取财务预算五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetFinancialBudgetFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "现金流量", TransKey = "menu.accounting.budget.formulation.financial.cashflow", ParentId = parentId, OrderNum = 1, Path = "cashflow", Component = "accounting/budget/formulation/financial/cashflow/index", MenuType = 1, Perms = "accounting:budget:formulation:financial:cashflow:list", Icon = "MoneyCollectOutlined", Remark = "现金流量预算" },
        new TaktMenu { MenuName = "资产负债表", TransKey = "menu.accounting.budget.formulation.financial.balancesheet", ParentId = parentId, OrderNum = 2, Path = "balancesheet", Component = "accounting/budget/formulation/financial/balancesheet/index", MenuType = 1, Perms = "accounting:budget:formulation:financial:balancesheet:list", Icon = "AccountBookOutlined", Remark = "资产负债表预算" },
        new TaktMenu { MenuName = "利润表", TransKey = "menu.accounting.budget.formulation.financial.income", ParentId = parentId, OrderNum = 3, Path = "income", Component = "accounting/budget/formulation/financial/income/index", MenuType = 1, Perms = "accounting:budget:formulation:financial:income:list", Icon = "RiseOutlined", Remark = "利润表预算" }
    };
} 

