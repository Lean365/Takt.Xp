//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedLogisticsMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 后勤管理菜单数据初始化类
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedLogisticsMenu.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 后勤管理菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 后勤管理菜单数据初始化类
/// </summary>
public class TaktDbSeedLogisticsMenu
{
    /// <summary>
    /// 获取后勤管理二级目录列表
    /// </summary>
    public static List<TaktMenu> GetLogisticsSecondLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "物料管理", TransKey = "menu.logistics.material._self", ParentId = parentId, OrderNum = 1, Path = "material", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "物料管理目录" },
        new TaktMenu { MenuName = "生产管理", TransKey = "menu.logistics.manufacturing._self", ParentId = parentId, OrderNum = 2, Path = "manufacturing", Component = "", MenuType = 0, Perms = "", Icon = "ExperimentOutlined", Remark = "生产管理目录" },
        new TaktMenu { MenuName = "质量管理", TransKey = "menu.logistics.quality._self", ParentId = parentId, OrderNum = 3, Path = "quality", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "质量管理目录" },
        new TaktMenu { MenuName = "销售管理", TransKey = "menu.logistics.sales._self", ParentId = parentId, OrderNum = 4, Path = "sales", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingCartOutlined", Remark = "销售管理目录" },
        new TaktMenu { MenuName = "设备管理", TransKey = "menu.logistics.equipment._self", ParentId = parentId, OrderNum = 5, Path = "equipment", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "设备管理目录" },
        new TaktMenu { MenuName = "客服管理", TransKey = "menu.logistics.service._self", ParentId = parentId, OrderNum = 6, Path = "service", Component = "", MenuType = 0, Perms = "", Icon = "CustomerServiceOutlined", Remark = "客服管理目录" }
    };
    /// <summary>
    /// 获取物料管理三级目录列表
    /// </summary>
    public static List<TaktMenu> GetMaterialThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "物料数据", TransKey = "menu.logistics.material.master._self", ParentId = parentId, OrderNum = 1, Path = "master", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "物料数据目录" },
        new TaktMenu { MenuName = "采购管理", TransKey = "menu.logistics.material.purchase._self", ParentId = parentId, OrderNum = 2, Path = "purchase", Component = "", MenuType = 0, Perms = "", Icon = "ShoppingOutlined", Remark = "采购管理目录" },
        new TaktMenu { MenuName = "样品管理", TransKey = "menu.logistics.material.sample._self", ParentId = parentId, OrderNum = 3, Path = "sample", Component = "", MenuType = 0, Perms = "", Icon = "ExperimentOutlined", Remark = "样品管理目录" },
        new TaktMenu { MenuName = "图纸管理", TransKey = "menu.logistics.material.drawing._self", ParentId = parentId, OrderNum = 4, Path = "drawing", Component = "", MenuType = 0, Perms = "", Icon = "FileImageOutlined", Remark = "图纸管理目录" },
        new TaktMenu { MenuName = "客供品管理", TransKey = "menu.logistics.material.csm._self", ParentId = parentId, OrderNum = 5, Path = "csm", Component = "", MenuType = 0, Perms = "", Icon = "UserOutlined", Remark = "客供品管理目录" }
    };

    /// <summary>
    /// 获取生产管理三级目录列表
    /// </summary>
    public static List<TaktMenu> GetProductionThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "主数据", TransKey = "menu.logistics.manufacturing.master._self", ParentId = parentId, OrderNum = 1, Path = "master", Component = "", MenuType = 0, Perms = "", Icon = "DatabaseOutlined", Remark = "生产管理主数据目录" },
        new TaktMenu { MenuName = "设计变更", TransKey = "menu.logistics.manufacturing.change._self", ParentId = parentId, OrderNum = 2, Path = "change", Component = "", MenuType = 0, Perms = "", Icon = "SwapOutlined", Remark = "设计变更管理目录" },
        new TaktMenu { MenuName = "执行管理", TransKey = "menu.logistics.manufacturing.execution._self", ParentId = parentId, OrderNum = 3, Path = "execution", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "执行管理目录" },
        new TaktMenu { MenuName = "SOP管理", TransKey = "menu.logistics.manufacturing.sop._self", ParentId = parentId, OrderNum = 4, Path = "sop", Component = "", MenuType = 0, Perms = "", Icon = "FileTextOutlined", Remark = "SOP管理目录" },
        new TaktMenu { MenuName = "技联管理", TransKey = "menu.logistics.manufacturing.techcontact._self", ParentId = parentId, OrderNum = 5, Path = "techcontact", Component = "", MenuType = 0, Perms = "", Icon = "MessageOutlined", Remark = "技联管理目录" }
    };

    /// <summary>
    /// 获取生产管理主数据四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionMasterFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "物料清单", TransKey = "menu.logistics.manufacturing.master.bom", ParentId = parentId, OrderNum = 1, Path = "bom", Component = "logistics/manufacturing/master/bom/index", MenuType = 1, Perms = "logistics:manufacturing:master:bom:list", Icon = "UnorderedListOutlined", Remark = "物料清单管理" },
        new TaktMenu { MenuName = "工作中心", TransKey = "menu.logistics.manufacturing.master.workcenter", ParentId = parentId, OrderNum = 2, Path = "workcenter", Component = "logistics/manufacturing/master/workcenter/index", MenuType = 1, Perms = "logistics:manufacturing:master:workcenter:list", Icon = "ClusterOutlined", Remark = "工作中心管理" },
        new TaktMenu { MenuName = "生产订单", TransKey = "menu.logistics.manufacturing.master.order", ParentId = parentId, OrderNum = 3, Path = "order", Component = "logistics/manufacturing/master/order/index", MenuType = 1, Perms = "logistics:manufacturing:master:order:list", Icon = "FileTextOutlined", Remark = "生产订单管理" },
        new TaktMenu { MenuName = "生产工时", TransKey = "menu.logistics.manufacturing.master.worktime", ParentId = parentId, OrderNum = 4, Path = "worktime", Component = "logistics/manufacturing/master/worktime/index", MenuType = 1, Perms = "logistics:manufacturing:master:worktime:list", Icon = "FieldTimeOutlined", Remark = "生产工时管理" },
        new TaktMenu { MenuName = "看板", TransKey = "menu.logistics.manufacturing.master.kanban", ParentId = parentId, OrderNum = 5, Path = "kanban", Component = "logistics/manufacturing/master/kanban/index", MenuType = 1, Perms = "logistics:manufacturing:master:kanban:list", Icon = "DashboardOutlined", Remark = "看板管理" }
    };



    /// <summary>
    /// 获取销售管理三级菜单列表
    /// </summary>
    public static List<TaktMenu> GetSalesThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "顾客", TransKey = "menu.logistics.sales.customer", ParentId = parentId, OrderNum = 1, Path = "customer", Component = "logistics/sales/customer/index", MenuType = 1, Perms = "logistics:sales:customer:list", Icon = "UserOutlined", Remark = "顾客管理" },
        new TaktMenu { MenuName = "客户", TransKey = "menu.logistics.sales.client", ParentId = parentId, OrderNum = 2, Path = "client", Component = "logistics/sales/client/index", MenuType = 1, Perms = "logistics:sales:client:list", Icon = "TeamOutlined", Remark = "客户管理" },
        new TaktMenu { MenuName = "销售价格", TransKey = "menu.logistics.sales.price", ParentId = parentId, OrderNum = 3, Path = "price", Component = "logistics/sales/price/index", MenuType = 1, Perms = "logistics:sales:price:list", Icon = "MoneyCollectOutlined", Remark = "销售价格管理" },
        new TaktMenu { MenuName = "销售订单", TransKey = "menu.logistics.sales.order", ParentId = parentId, OrderNum = 4, Path = "order", Component = "logistics/sales/order/index", MenuType = 1, Perms = "logistics:sales:order:list", Icon = "ShoppingCartOutlined", Remark = "销售订单管理" }
    };

    /// <summary>
    /// 获取质量管理三级目录列表
    /// </summary>
    public static List<TaktMenu> GetQualityThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "主数据", TransKey = "menu.logistics.quality.master._self", ParentId = parentId, OrderNum = 1, Path = "master", Component = "", MenuType = 0, Perms = "", Icon = "DatabaseOutlined", Remark = "质量管理主数据目录" },
        new TaktMenu { MenuName = "质量检验", TransKey = "menu.logistics.quality.inspection._self", ParentId = parentId, OrderNum = 2, Path = "inspection", Component = "", MenuType = 0, Perms = "", Icon = "SearchOutlined", Remark = "质量检验管理目录" },
        new TaktMenu { MenuName = "质量追溯", TransKey = "menu.logistics.quality.trace._self", ParentId = parentId, OrderNum = 3, Path = "trace", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "质量追溯管理目录" },
        new TaktMenu { MenuName = "质量成本", TransKey = "menu.logistics.quality.cost._self", ParentId = parentId, OrderNum = 4, Path = "cost", Component = "", MenuType = 0, Perms = "", Icon = "MoneyCollectOutlined", Remark = "质量成本管理目录" },
        new TaktMenu { MenuName = "检验计划", TransKey = "menu.logistics.quality.plan._self", ParentId = parentId, OrderNum = 5, Path = "plan", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "检验计划管理目录" }
    };

    /// <summary>
    /// 获取质量管理主数据四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetQualityMasterFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "检验项目", TransKey = "menu.logistics.quality.master.item", ParentId = parentId, OrderNum = 1, Path = "item", Component = "logistics/quality/master/item/index", MenuType = 1, Perms = "logistics:quality:master:item:list", Icon = "CheckSquareOutlined", Remark = "检验项目管理" },
        new TaktMenu { MenuName = "检验方法", TransKey = "menu.logistics.quality.master.method", ParentId = parentId, OrderNum = 2, Path = "method", Component = "logistics/quality/master/method/index", MenuType = 1, Perms = "logistics:quality:master:method:list", Icon = "ExperimentOutlined", Remark = "检验方法管理" },
        new TaktMenu { MenuName = "抽样方案", TransKey = "menu.logistics.quality.master.sampling", ParentId = parentId, OrderNum = 3, Path = "sampling", Component = "logistics/quality/master/sampling/index", MenuType = 1, Perms = "logistics:quality:master:sampling:list", Icon = "ClusterOutlined", Remark = "抽样方案管理" },
        new TaktMenu { MenuName = "缺陷分级", TransKey = "menu.logistics.quality.master.defect", ParentId = parentId, OrderNum = 4, Path = "defect", Component = "logistics/quality/master/defect/index", MenuType = 1, Perms = "logistics:quality:master:defect:list", Icon = "WarningOutlined", Remark = "缺陷分级管理" },
        new TaktMenu { MenuName = "判定规则", TransKey = "menu.logistics.quality.master.rule", ParentId = parentId, OrderNum = 5, Path = "rule", Component = "logistics/quality/master/rule/index", MenuType = 1, Perms = "logistics:quality:master:rule:list", Icon = "FileTextOutlined", Remark = "判定规则管理" },
        new TaktMenu { MenuName = "品管类别", TransKey = "menu.logistics.quality.master.category", ParentId = parentId, OrderNum = 6, Path = "category", Component = "logistics/quality/master/category/index", MenuType = 1, Perms = "logistics:quality:master:category:list", Icon = "TagsOutlined", Remark = "品管类别管理" }
    };

    /// <summary>
    /// 获取质量检验四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetQualityInspectionFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "收货检验", TransKey = "menu.logistics.quality.inspection.receiving", ParentId = parentId, OrderNum = 1, Path = "receiving", Component = "logistics/quality/inspection/receiving/index", MenuType = 1, Perms = "logistics:quality:inspection:receiving:list", Icon = "InboxOutlined", Remark = "收货检验管理" },
        new TaktMenu { MenuName = "过程检验", TransKey = "menu.logistics.quality.inspection.process", ParentId = parentId, OrderNum = 2, Path = "process", Component = "logistics/quality/inspection/process/index", MenuType = 1, Perms = "logistics:quality:inspection:process:list", Icon = "ExperimentOutlined", Remark = "过程检验管理" },
        new TaktMenu { MenuName = "入库检验", TransKey = "menu.logistics.quality.inspection.storage", ParentId = parentId, OrderNum = 3, Path = "storage", Component = "logistics/quality/inspection/storage/index", MenuType = 1, Perms = "logistics:quality:inspection:storage:list", Icon = "DatabaseOutlined", Remark = "入库检验管理" },
        new TaktMenu { MenuName = "退货检验", TransKey = "menu.logistics.quality.inspection.return", ParentId = parentId, OrderNum = 4, Path = "return", Component = "logistics/quality/inspection/return/index", MenuType = 1, Perms = "logistics:quality:inspection:return:list", Icon = "RollbackOutlined", Remark = "退货检验管理" }
    };

    /// <summary>
    /// 获取质量追溯四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetQualityTraceFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "批次追溯", TransKey = "menu.logistics.quality.trace.batch", ParentId = parentId, OrderNum = 1, Path = "batch", Component = "logistics/quality/trace/batch/index", MenuType = 1, Perms = "logistics:quality:trace:batch:list", Icon = "ClusterOutlined", Remark = "批次追溯管理" },
        new TaktMenu { MenuName = "纠正预防", TransKey = "menu.logistics.quality.trace.corrective", ParentId = parentId, OrderNum = 2, Path = "corrective", Component = "logistics/quality/trace/corrective/index", MenuType = 1, Perms = "logistics:quality:trace:corrective:list", Icon = "ToolOutlined", Remark = "纠正预防管理" },
        new TaktMenu { MenuName = "不合格通知", TransKey = "menu.logistics.quality.trace.notification", ParentId = parentId, OrderNum = 3, Path = "notification", Component = "logistics/quality/trace/notification/index", MenuType = 1, Perms = "logistics:quality:trace:notification:list", Icon = "NotificationOutlined", Remark = "不合格通知管理" }
    };

    /// <summary>
    /// 获取质量成本四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetQualityCostFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "品质业务", TransKey = "menu.logistics.quality.cost.business", ParentId = parentId, OrderNum = 1, Path = "business", Component = "logistics/quality/cost/business/index", MenuType = 1, Perms = "logistics:quality:cost:business:list", Icon = "SafetyCertificateOutlined", Remark = "品质业务管理" },
        new TaktMenu { MenuName = "返工业务", TransKey = "menu.logistics.quality.cost.rework", ParentId = parentId, OrderNum = 2, Path = "rework", Component = "logistics/quality/cost/rework/index", MenuType = 1, Perms = "logistics:quality:cost:rework:list", Icon = "ReloadOutlined", Remark = "返工业务管理" },
        new TaktMenu { MenuName = "废弃事故", TransKey = "menu.logistics.quality.cost.scrap", ParentId = parentId, OrderNum = 3, Path = "scrap", Component = "logistics/quality/cost/scrap/index", MenuType = 1, Perms = "logistics:quality:cost:scrap:list", Icon = "DeleteOutlined", Remark = "废弃事故管理" }
    };

    /// <summary>
    /// 获取检验计划四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetQualityPlanFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "供应商评估", TransKey = "menu.logistics.quality.plan.supplier", ParentId = parentId, OrderNum = 1, Path = "supplier", Component = "logistics/quality/plan/supplier/index", MenuType = 1, Perms = "logistics:quality:plan:supplier:list", Icon = "ShopOutlined", Remark = "供应商评估管理" },
        new TaktMenu { MenuName = "客户调查", TransKey = "menu.logistics.quality.plan.customer", ParentId = parentId, OrderNum = 2, Path = "customer", Component = "logistics/quality/plan/customer/index", MenuType = 1, Perms = "logistics:quality:plan:customer:list", Icon = "UserOutlined", Remark = "客户调查管理" }
    };

    /// <summary>
    /// 获取设备管理三级目录列表
    /// </summary>
    public static List<TaktMenu> GetEquipmentThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "设备数据", TransKey = "menu.logistics.equipment.master._self", ParentId = parentId, OrderNum = 1, Path = "master", Component = "", MenuType = 0, Perms = "", Icon = "DatabaseOutlined", Remark = "设备数据目录" },
        new TaktMenu { MenuName = "维保管理", TransKey = "menu.logistics.equipment.maintenance._self", ParentId = parentId, OrderNum = 2, Path = "maintenance", Component = "", MenuType = 0, Perms = "", Icon = "ToolOutlined", Remark = "设备维保管理目录" }
    };

    /// <summary>
    /// 获取客服管理三级目录列表
    /// </summary>
    public static List<TaktMenu> GetServiceThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "客户服务", TransKey = "menu.logistics.service.cs._self", ParentId = parentId, OrderNum = 1, Path = "service", Component = "", MenuType = 0, Perms = "", Icon = "CustomerServiceOutlined", Remark = "客户服务目录" },
        new TaktMenu { MenuName = "客诉管理", TransKey = "menu.logistics.service.cc._self", ParentId = parentId, OrderNum = 2, Path = "complaint", Component = "", MenuType = 0, Perms = "", Icon = "WarningOutlined", Remark = "客诉管理目录" }
    };


    /// <summary>
    /// 获取物料管理主数据四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetMaterialMasterFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "工厂信息", TransKey = "menu.logistics.material.master.plant", ParentId = parentId, OrderNum = 1, Path = "plant", Component = "logistics/material/master/plant/index", MenuType = 1, Perms = "logistics:material:master:plant:list", Icon = "ClusterOutlined", Remark = "工厂信息管理" },
        new TaktMenu { MenuName = "通用物料", TransKey = "menu.logistics.material.master.genmat", ParentId = parentId, OrderNum = 2, Path = "genmat", Component = "logistics/material/master/genmat/index", MenuType = 1, Perms = "logistics:material:master:genmat:list", Icon = "InfoCircleOutlined", Remark = "通用物料管理" },
        new TaktMenu { MenuName = "工厂物料", TransKey = "menu.logistics.material.master.ftymat", ParentId = parentId, OrderNum = 3, Path = "ftymat", Component = "logistics/material/master/ftymat/index", MenuType = 1, Perms = "logistics:material:master:ftymat:list", Icon = "ClusterOutlined", Remark = "工厂物料管理" },
        new TaktMenu { MenuName = "生产物料", TransKey = "menu.logistics.material.master.prodmat", ParentId = parentId, OrderNum = 3, Path = "prodmat", Component = "logistics/material/master/prodmat/index", MenuType = 1, Perms = "logistics:material:master:prodmat:list", Icon = "ClusterOutlined", Remark = "生产物料管理" }
    };

    /// <summary>
    /// 获取采购目录四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetMaterialPurchaseFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "卖方", TransKey = "menu.logistics.material.purchase.vendor", ParentId = parentId, OrderNum = 1, Path = "vendor", Component = "logistics/material/purchase/vendor/index", MenuType = 1, Perms = "logistics:material:purchase:vendor:list", Icon = "ShopOutlined", Remark = "卖方管理" },
        new TaktMenu { MenuName = "供应商", TransKey = "menu.logistics.material.purchase.supplier", ParentId = parentId, OrderNum = 2, Path = "supplier", Component = "logistics/material/purchase/supplier/index", MenuType = 1, Perms = "logistics:material:purchase:supplier:list", Icon = "ShopOutlined", Remark = "供应商管理" },
        new TaktMenu { MenuName = "采购价格", TransKey = "menu.logistics.material.purchase.price", ParentId = parentId, OrderNum = 3, Path = "price", Component = "logistics/material/purchase/price/index", MenuType = 1, Perms = "logistics:material:purchase:price:list", Icon = "MoneyCollectOutlined", Remark = "采购价格管理" },
        new TaktMenu { MenuName = "采购申请", TransKey = "menu.logistics.material.purchase.requisition", ParentId = parentId, OrderNum = 4, Path = "requisition", Component = "logistics/material/purchase/requisition/index", MenuType = 1, Perms = "logistics:material:purchase:requisition:list", Icon = "FileAddOutlined", Remark = "采购申请管理" },
        new TaktMenu { MenuName = "采购订单", TransKey = "menu.logistics.material.purchase.order", ParentId = parentId, OrderNum = 5, Path = "order", Component = "logistics/material/purchase/order/index", MenuType = 1, Perms = "logistics:material:purchase:order:list", Icon = "ShoppingOutlined", Remark = "采购订单管理" }
    };

    /// <summary>
    /// 获取样品管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetMaterialSampleFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "料件样品", TransKey = "menu.logistics.material.sample.component", ParentId = parentId, OrderNum = 1, Path = "component", Component = "logistics/material/sample/component/index", MenuType = 1, Perms = "logistics:material:sample:component:list", Icon = "ExperimentOutlined", Remark = "料件样品管理" },
        new TaktMenu { MenuName = "产品样品", TransKey = "menu.logistics.material.sample.product", ParentId = parentId, OrderNum = 2, Path = "product", Component = "logistics/material/sample/product/index", MenuType = 1, Perms = "logistics:material:sample:product:list", Icon = "ExperimentOutlined", Remark = "产品样品管理" }
    };

    /// <summary>
    /// 获取图纸管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetMaterialDrawingFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "设计图纸", TransKey = "menu.logistics.material.drawing.design", ParentId = parentId, OrderNum = 1, Path = "design", Component = "logistics/material/drawing/design/index", MenuType = 1, Perms = "logistics:material:drawing:design:list", Icon = "FileImageOutlined", Remark = "设计图纸管理" },
        new TaktMenu { MenuName = "工程图纸", TransKey = "menu.logistics.material.drawing.engineering", ParentId = parentId, OrderNum = 2, Path = "engineering", Component = "logistics/material/drawing/engineering/index", MenuType = 1, Perms = "logistics:material:drawing:engineering:list", Icon = "FileImageOutlined", Remark = "工程图纸管理" },
        new TaktMenu { MenuName = "Gerber文件", TransKey = "menu.logistics.material.drawing.gerber", ParentId = parentId, OrderNum = 3, Path = "gerber", Component = "logistics/material/drawing/gerber/index", MenuType = 1, Perms = "logistics:material:drawing:gerber:list", Icon = "FileImageOutlined", Remark = "Gerber文件管理" },
        new TaktMenu { MenuName = "坐标文件", TransKey = "menu.logistics.material.drawing.coordinate", ParentId = parentId, OrderNum = 4, Path = "coordinate", Component = "logistics/material/drawing/coordinate/index", MenuType = 1, Perms = "logistics:material:drawing:coordinate:list", Icon = "FileImageOutlined", Remark = "坐标文件管理" },
        new TaktMenu { MenuName = "装配图纸", TransKey = "menu.logistics.material.drawing.assembly", ParentId = parentId, OrderNum = 5, Path = "assembly", Component = "logistics/material/drawing/assembly/index", MenuType = 1, Perms = "logistics:material:drawing:assembly:list", Icon = "FileImageOutlined", Remark = "装配图纸管理" },
        new TaktMenu { MenuName = "结构文件", TransKey = "menu.logistics.material.drawing.structure", ParentId = parentId, OrderNum = 6, Path = "structure", Component = "logistics/material/drawing/structure/index", MenuType = 1, Perms = "logistics:material:drawing:structure:list", Icon = "FileImageOutlined", Remark = "结构文件管理" },
        new TaktMenu { MenuName = "阻抗文件", TransKey = "menu.logistics.material.drawing.impedance", ParentId = parentId, OrderNum = 7, Path = "impedance", Component = "logistics/material/drawing/impedance/index", MenuType = 1, Perms = "logistics:material:drawing:impedance:list", Icon = "FileImageOutlined", Remark = "阻抗文件管理" },
        new TaktMenu { MenuName = "工艺流程", TransKey = "menu.logistics.material.drawing.process", ParentId = parentId, OrderNum = 8, Path = "process", Component = "logistics/material/drawing/process/index", MenuType = 1, Perms = "logistics:material:drawing:process:list", Icon = "FileImageOutlined", Remark = "工艺流程管理" }
    };

    /// <summary>
    /// 获取客供品管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetMaterialCustomerFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "材料", TransKey = "menu.logistics.material.csm.raw", ParentId = parentId, OrderNum = 1, Path = "raw", Component = "logistics/material/csm/raw/index", MenuType = 1, Perms = "logistics:material:csm:raw:list", Icon = "ClusterOutlined", Remark = "客供材料管理" },
        new TaktMenu { MenuName = "成品", TransKey = "menu.logistics.material.csm.good", ParentId = parentId, OrderNum = 2, Path = "good", Component = "logistics/material/csm/good/index", MenuType = 1, Perms = "logistics:material:csm:good:list", Icon = "ClusterOutlined", Remark = "客供产品管理" }
    };

    /// <summary>
    /// 获取设计变更四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionChangeFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "设变实施", TransKey = "menu.logistics.manufacturing.change.implementation", ParentId = parentId, OrderNum = 1, Path = "implementation", Component = "logistics/manufacturing/change/implementation/index", MenuType = 1, Perms = "logistics:manufacturing:change:implementation:list", Icon = "CheckCircleOutlined", Remark = "设变实施管理" },
        new TaktMenu { MenuName = "投入批次", TransKey = "menu.logistics.manufacturing.change.batch", ParentId = parentId, OrderNum = 2, Path = "batch", Component = "logistics/manufacturing/change/batch/index", MenuType = 1, Perms = "logistics:manufacturing:change:batch:list", Icon = "InboxOutlined", Remark = "投入批次管理" },
        new TaktMenu { MenuName = "物料确认", TransKey = "menu.logistics.manufacturing.change.material", ParentId = parentId, OrderNum = 3, Path = "material", Component = "logistics/manufacturing/change/material/index", MenuType = 1, Perms = "logistics:manufacturing:change:material:list", Icon = "CheckSquareOutlined", Remark = "物料确认管理" },
        new TaktMenu { MenuName = "设变查询", TransKey = "menu.logistics.manufacturing.change.query", ParentId = parentId, OrderNum = 4, Path = "query", Component = "logistics/manufacturing/change/query/index", MenuType = 1, Perms = "logistics:manufacturing:change:query:list", Icon = "SearchOutlined", Remark = "设变查询管理" },
        new TaktMenu { MenuName = "旧品管制", TransKey = "menu.logistics.manufacturing.change.oldproduct", ParentId = parentId, OrderNum = 5, Path = "oldproduct", Component = "logistics/manufacturing/change/oldproduct/index", MenuType = 1, Perms = "logistics:manufacturing:change:oldproduct:list", Icon = "StopOutlined", Remark = "旧品管制管理" },
        new TaktMenu { MenuName = "SOP确认", TransKey = "menu.logistics.manufacturing.change.sop", ParentId = parentId, OrderNum = 6, Path = "sop", Component = "logistics/manufacturing/change/sop/index", MenuType = 1, Perms = "logistics:manufacturing:change:sop:list", Icon = "FileTextOutlined", Remark = "SOP确认管理" },
        new TaktMenu { MenuName = "技术联络", TransKey = "menu.logistics.manufacturing.change.techcontact", ParentId = parentId, OrderNum = 7, Path = "techcontact", Component = "logistics/manufacturing/change/techcontact/index", MenuType = 1, Perms = "logistics:manufacturing:change:techcontact:list", Icon = "MessageOutlined", Remark = "技术联络管理" }
    };

    /// <summary>
    /// 获取设计变更四级目录列表
    /// </summary>
    public static List<TaktMenu> GetProductionChangeFourthLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "设变录入", TransKey = "menu.logistics.manufacturing.change.input._self", ParentId = parentId, OrderNum = 1, Path = "input", Component = "", MenuType = 0, Perms = "", Icon = "EditOutlined", Remark = "设变录入目录" }
    };

    /// <summary>
    /// 获取执行管理四级目录列表
    /// </summary>
    public static List<TaktMenu> GetProductionExecutionFourthLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "产出", TransKey = "menu.logistics.manufacturing.execution.output._self", ParentId = parentId, OrderNum = 1, Path = "output", Component = "", MenuType = 0, Perms = "", Icon = "ClusterOutlined", Remark = "产出管理目录" },
        new TaktMenu { MenuName = "不良", TransKey = "menu.logistics.manufacturing.execution.defect._self", ParentId = parentId, OrderNum = 2, Path = "defect", Component = "", MenuType = 0, Perms = "", Icon = "WarningOutlined", Remark = "不良管理目录" },
        new TaktMenu { MenuName = "工数", TransKey = "menu.logistics.manufacturing.execution.worktime._self", ParentId = parentId, OrderNum = 3, Path = "worktime", Component = "", MenuType = 0, Perms = "", Icon = "FieldTimeOutlined", Remark = "工数管理目录" }
    };

    /// <summary>
    /// 获取产出管理五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionOutputFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制一课", TransKey = "menu.logistics.manufacturing.execution.output.workshop1", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "logistics/manufacturing/execution/output/workshop1/index", MenuType = 1, Perms = "logistics:manufacturing:execution:output:workshop1:list", Icon = "ClusterOutlined", Remark = "制一课产出管理" },
        new TaktMenu { MenuName = "制二课", TransKey = "menu.logistics.manufacturing.execution.output.workshop2", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "logistics/manufacturing/execution/output/workshop2/index", MenuType = 1, Perms = "logistics:manufacturing:execution:output:workshop2:list", Icon = "ClusterOutlined", Remark = "制二课产出管理" }
    };

    /// <summary>
    /// 获取不良管理五级目录列表
    /// </summary>
    public static List<TaktMenu> GetProductionDefectFifthLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制二课", TransKey = "menu.logistics.manufacturing.execution.defect.workshop2._self", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "", MenuType = 0, Perms = "", Icon = "WarningOutlined", Remark = "制二课不良管理目录" }
    };

    /// <summary>
    /// 获取制二课不良管理六级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionDefectWorkshop2SixthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "检查", TransKey = "menu.logistics.manufacturing.execution.defect.workshop2.inspection", ParentId = parentId, OrderNum = 1, Path = "inspection", Component = "logistics/manufacturing/execution/defect/workshop2/inspection/index", MenuType = 1, Perms = "logistics:manufacturing:execution:defect:workshop2:inspection:list", Icon = "SearchOutlined", Remark = "制二课不良检查管理" },
        new TaktMenu { MenuName = "修理", TransKey = "menu.logistics.manufacturing.execution.defect.workshop2.repair", ParentId = parentId, OrderNum = 2, Path = "repair", Component = "logistics/manufacturing/execution/defect/workshop2/repair/index", MenuType = 1, Perms = "logistics:manufacturing:execution:defect:workshop2:repair:list", Icon = "ToolOutlined", Remark = "制二课不良修理管理" }
    };

    /// <summary>
    /// 获取不良管理五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionDefectFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制一课", TransKey = "menu.logistics.manufacturing.execution.defect.workshop1", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "logistics/manufacturing/execution/defect/workshop1/index", MenuType = 1, Perms = "logistics:manufacturing:execution:defect:workshop1:list", Icon = "WarningOutlined", Remark = "制一课不良管理" }
    };

    /// <summary>
    /// 获取工数管理五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionWorktimeFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制一课", TransKey = "menu.logistics.manufacturing.execution.worktime.workshop1", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "logistics/manufacturing/execution/worktime/workshop1/index", MenuType = 1, Perms = "logistics:manufacturing:execution:worktime:workshop1:list", Icon = "FieldTimeOutlined", Remark = "制一课工数管理" },
        new TaktMenu { MenuName = "制二课", TransKey = "menu.logistics.manufacturing.execution.worktime.workshop2", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "logistics/manufacturing/execution/worktime/workshop2/index", MenuType = 1, Perms = "logistics:manufacturing:execution:worktime:workshop2:list", Icon = "FieldTimeOutlined", Remark = "制二课工数管理" }
    };


    /// <summary>
    /// 获取设变录入五级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionChangeInputFifthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "技术课", TransKey = "menu.logistics.manufacturing.change.input.gijutsu", ParentId = parentId, OrderNum = 1, Path = "gijutsu", Component = "logistics/manufacturing/change/input/gijutsu/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:gijutsu:list", Icon = "ToolOutlined", Remark = "技术课设变录入" },
        new TaktMenu { MenuName = "生管课", TransKey = "menu.logistics.manufacturing.change.input.seikan", ParentId = parentId, OrderNum = 2, Path = "seikan", Component = "logistics/manufacturing/change/input/seikan/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:seikan:list", Icon = "ClusterOutlined", Remark = "生管课设变录入" },
        new TaktMenu { MenuName = "采购课", TransKey = "menu.logistics.manufacturing.change.input.koubai", ParentId = parentId, OrderNum = 3, Path = "koubai", Component = "logistics/manufacturing/change/input/koubai/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:koubai:list", Icon = "ShoppingOutlined", Remark = "采购课设变录入" },
        new TaktMenu { MenuName = "受检课", TransKey = "menu.logistics.manufacturing.change.input.uketsuke", ParentId = parentId, OrderNum = 4, Path = "uketsuke", Component = "logistics/manufacturing/change/input/uketsuke/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:uketsuke:list", Icon = "CheckSquareOutlined", Remark = "受检课设变录入" },
        new TaktMenu { MenuName = "部管课", TransKey = "menu.logistics.manufacturing.change.input.bukan", ParentId = parentId, OrderNum = 5, Path = "bukan", Component = "logistics/manufacturing/change/input/bukan/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:bukan:list", Icon = "ApartmentOutlined", Remark = "部管课设变录入" },
        new TaktMenu { MenuName = "制二课", TransKey = "menu.logistics.manufacturing.change.input.seizou2", ParentId = parentId, OrderNum = 6, Path = "seizou2", Component = "logistics/manufacturing/change/input/seizou2/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:seizou2:list", Icon = "ClusterOutlined", Remark = "制二课设变录入" },
        new TaktMenu { MenuName = "制一课", TransKey = "menu.logistics.manufacturing.change.input.seizou1", ParentId = parentId, OrderNum = 7, Path = "seizou1", Component = "logistics/manufacturing/change/input/seizou1/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:seizou1:list", Icon = "ClusterOutlined", Remark = "制一课设变录入" },
        new TaktMenu { MenuName = "品管课", TransKey = "menu.logistics.manufacturing.change.input.hinkan", ParentId = parentId, OrderNum = 8, Path = "hinkan", Component = "logistics/manufacturing/change/input/hinkan/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:hinkan:list", Icon = "SafetyCertificateOutlined", Remark = "品管课设变录入" },
        new TaktMenu { MenuName = "制技课", TransKey = "menu.logistics.manufacturing.change.input.seizougijutsu", ParentId = parentId, OrderNum = 9, Path = "seizougijutsu", Component = "logistics/manufacturing/change/input/seizougijutsu/index", MenuType = 1, Perms = "logistics:manufacturing:change:input:seizougijutsu:list", Icon = "ToolOutlined", Remark = "制技课设变录入" }
    };

    // ==================== 设备管理菜单 ====================

    /// <summary>
    /// 获取设备主数据四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetEquipmentMasterFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "设备清单", TransKey = "menu.logistics.equipment.master.list", ParentId = parentId, OrderNum = 1, Path = "inventory", Component = "logistics/equipment/master/inventory/index", MenuType = 1, Perms = "logistics:equipment:master:list:list", Icon = "DatabaseOutlined", Remark = "设备清单管理" },
        new TaktMenu { MenuName = "功能位置", TransKey = "menu.logistics.equipment.master.location", ParentId = parentId, OrderNum = 2, Path = "location", Component = "logistics/equipment/master/location/index", MenuType = 1, Perms = "logistics:equipment:master:location:list", Icon = "EnvironmentOutlined", Remark = "功能位置管理" },
        new TaktMenu { MenuName = "物料关联", TransKey = "menu.logistics.equipment.master.material", ParentId = parentId, OrderNum = 3, Path = "material", Component = "logistics/equipment/master/material/index", MenuType = 1, Perms = "logistics:equipment:master:material:list", Icon = "LinkOutlined", Remark = "物料关联管理" }
    };

    /// <summary>
    /// 获取设备维保管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetEquipmentMaintenanceFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "维保工单", TransKey = "menu.logistics.equipment.maintenance.workorder", ParentId = parentId, OrderNum = 1, Path = "workorder", Component = "logistics/equipment/maintenance/workorder/index", MenuType = 1, Perms = "logistics:equipment:maintenance:workorder:list", Icon = "FileTextOutlined", Remark = "维保工单管理" },
        new TaktMenu { MenuName = "分配", TransKey = "menu.logistics.equipment.maintenance.assign", ParentId = parentId, OrderNum = 2, Path = "assign", Component = "logistics/equipment/maintenance/assign/index", MenuType = 1, Perms = "logistics:equipment:maintenance:assign:list", Icon = "UserAddOutlined", Remark = "维保工单分配" },
        new TaktMenu { MenuName = "执行", TransKey = "menu.logistics.equipment.maintenance.execute", ParentId = parentId, OrderNum = 3, Path = "execute", Component = "logistics/equipment/maintenance/execute/index", MenuType = 1, Perms = "logistics:equipment:maintenance:execute:list", Icon = "PlayCircleOutlined", Remark = "维保工单执行" }
    };

    // ==================== SOP管理菜单 ====================

    /// <summary>
    /// 获取SOP管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionSopFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制一课", TransKey = "menu.logistics.manufacturing.sop.workshop1", ParentId = parentId, OrderNum = 1, Path = "workshop1", Component = "logistics/manufacturing/sop/workshop1/index", MenuType = 1, Perms = "logistics:manufacturing:sop:workshop1:list", Icon = "ClusterOutlined", Remark = "制一课SOP管理" },
        new TaktMenu { MenuName = "制二课", TransKey = "menu.logistics.manufacturing.sop.workshop2", ParentId = parentId, OrderNum = 2, Path = "workshop2", Component = "logistics/manufacturing/sop/workshop2/index", MenuType = 1, Perms = "logistics:manufacturing:sop:workshop2:list", Icon = "ClusterOutlined", Remark = "制二课SOP管理" }
    };

    // ==================== 技联管理菜单 ====================

    /// <summary>
    /// 获取技联管理四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetProductionTechcontactFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "EPP联络", TransKey = "menu.logistics.manufacturing.techcontact.epp", ParentId = parentId, OrderNum = 1, Path = "epp", Component = "logistics/manufacturing/techcontact/epp/index", MenuType = 1, Perms = "logistics:manufacturing:techcontact:epp:list", Icon = "MessageOutlined", Remark = "EPP联络管理" },
        new TaktMenu { MenuName = "工程设变", TransKey = "menu.logistics.manufacturing.techcontact.engineering", ParentId = parentId, OrderNum = 2, Path = "engineering", Component = "logistics/manufacturing/techcontact/engineering/index", MenuType = 1, Perms = "logistics:manufacturing:techcontact:engineering:list", Icon = "SwapOutlined", Remark = "工程设变管理" },
        new TaktMenu { MenuName = "外部联络", TransKey = "menu.logistics.manufacturing.techcontact.external", ParentId = parentId, OrderNum = 3, Path = "external", Component = "logistics/manufacturing/techcontact/external/index", MenuType = 1, Perms = "logistics:manufacturing:techcontact:external:list", Icon = "GlobalOutlined", Remark = "外部联络管理" }
    };

    // ==================== 服务管理菜单 ====================

    /// <summary>
    /// 获取客户服务四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetServiceCustomerServiceFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "服务项目", TransKey = "menu.logistics.service.cs.item", ParentId = parentId, OrderNum = 1, Path = "item", Component = "logistics/service/service/item/index", MenuType = 1, Perms = "logistics:service:cs:item:list", Icon = "AppstoreOutlined", Remark = "服务项目管理" },
        new TaktMenu { MenuName = "服务合同", TransKey = "menu.logistics.service.cs.contract", ParentId = parentId, OrderNum = 2, Path = "contract", Component = "logistics/service/service/contract/index", MenuType = 1, Perms = "logistics:service:cs:contract:list", Icon = "FileTextOutlined", Remark = "服务合同管理" },
        new TaktMenu { MenuName = "服务请求", TransKey = "menu.logistics.service.cs.request", ParentId = parentId, OrderNum = 3, Path = "request", Component = "logistics/service/service/request/index", MenuType = 1, Perms = "logistics:service:cs:request:list", Icon = "QuestionCircleOutlined", Remark = "服务请求管理" },
        new TaktMenu { MenuName = "服务工单", TransKey = "menu.logistics.service.cs.workorder", ParentId = parentId, OrderNum = 4, Path = "workorder", Component = "logistics/service/service/workorder/index", MenuType = 1, Perms = "logistics:service:cs:workorder:list", Icon = "FileTextOutlined", Remark = "服务工单管理" },
        new TaktMenu { MenuName = "工时记录", TransKey = "menu.logistics.service.cs.timesheet", ParentId = parentId, OrderNum = 5, Path = "timesheet", Component = "logistics/service/service/timesheet/index", MenuType = 1, Perms = "logistics:service:cs:timesheet:list", Icon = "FieldTimeOutlined", Remark = "工时记录管理" },
        new TaktMenu { MenuName = "物料消耗", TransKey = "menu.logistics.service.cs.consumption", ParentId = parentId, OrderNum = 6, Path = "consumption", Component = "logistics/service/service/consumption/index", MenuType = 1, Perms = "logistics:service:cs:consumption:list", Icon = "InboxOutlined", Remark = "物料消耗管理" },
        new TaktMenu { MenuName = "外协服务", TransKey = "menu.logistics.service.cs.outsourcing", ParentId = parentId, OrderNum = 7, Path = "outsourcing", Component = "logistics/service/service/outsourcing/index", MenuType = 1, Perms = "logistics:service:cs:outsourcing:list", Icon = "TeamOutlined", Remark = "外协服务管理" }
    };

    // ==================== 客诉管理菜单 ====================

    /// <summary>
    /// 获取客户投诉四级菜单列表
    /// </summary>
    public static List<TaktMenu> GetServiceCustomerComplaintFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "质量通知单", TransKey = "menu.logistics.service.cc.notice", ParentId = parentId, OrderNum = 1, Path = "notice", Component = "logistics/service/complaint/notice/index", MenuType = 1, Perms = "logistics:service:cc:notice:list", Icon = "NotificationOutlined", Remark = "质量通知单管理" },
        new TaktMenu { MenuName = "客户主数据标记", TransKey = "menu.logistics.service.cc.mark", ParentId = parentId, OrderNum = 2, Path = "mark", Component = "logistics/service/complaint/mark/index", MenuType = 1, Perms = "logistics:service:cc:mark:list", Icon = "TagOutlined", Remark = "客户主数据标记管理" },
        new TaktMenu { MenuName = "原因分析", TransKey = "menu.logistics.service.cc.analysis", ParentId = parentId, OrderNum = 3, Path = "analysis", Component = "logistics/service/complaint/analysis/index", MenuType = 1, Perms = "logistics:service:cc:analysis:list", Icon = "FundOutlined", Remark = "原因分析管理" },
        new TaktMenu { MenuName = "纠正措施", TransKey = "menu.logistics.service.cc.corrective", ParentId = parentId, OrderNum = 4, Path = "corrective", Component = "logistics/service/complaint/corrective/index", MenuType = 1, Perms = "logistics:service:cc:corrective:list", Icon = "ToolOutlined", Remark = "纠正措施管理" },
        new TaktMenu { MenuName = "退换货执行", TransKey = "menu.logistics.service.cc.return", ParentId = parentId, OrderNum = 5, Path = "return", Component = "logistics/service/complaint/return/index", MenuType = 1, Perms = "logistics:service:cc:return:list", Icon = "RollbackOutlined", Remark = "退换货执行管理" }
    };


}

