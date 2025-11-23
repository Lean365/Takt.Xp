//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRoutineMenu.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 日常事务菜单数据初始化类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 日常事务菜单数据初始化类
/// </summary>
public class TaktDbSeedRoutineMenu
{
  /// <summary>
  /// 获取日常事务二级目录列表
  /// </summary>
  public static List<TaktMenu> GetRoutineSecondLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "新闻管理", TransKey = "menu.routine.news._self", ParentId = parentId, OrderNum = 1, Path = "news", Component = "", MenuType = 0, Perms = "", Icon = "GlobalOutlined", Remark = "新闻管理目录" },
        new TaktMenu { MenuName = "日程管理", TransKey = "menu.routine.schedule._self", ParentId = parentId, OrderNum = 2, Path = "schedule", Component = "", MenuType = 0, Perms = "", Icon = "CalendarOutlined", Remark = "日程管理目录" },
        new TaktMenu { MenuName = "用车管理", TransKey = "menu.routine.vehicle._self", ParentId = parentId, OrderNum = 3, Path = "vehicle", Component = "", MenuType = 0, Perms = "", Icon = "CarOutlined", Remark = "用车管理目录" },
        new TaktMenu { MenuName = "邮件管理", TransKey = "menu.routine.email._self", ParentId = parentId, OrderNum = 4, Path = "email", Component = "", MenuType = 0, Perms = "", Icon = "MailOutlined", Remark = "邮件管理目录" },
        new TaktMenu { MenuName = "会议管理", TransKey = "menu.routine.meeting._self", ParentId = parentId, OrderNum = 5, Path = "meeting", Component = "", MenuType = 0, Perms = "", Icon = "VideoCameraOutlined", Remark = "会议管理目录" },
        new TaktMenu { MenuName = "公告通知", TransKey = "menu.routine.notice._self", ParentId = parentId, OrderNum = 6, Path = "notice", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "公告通知目录" },
        new TaktMenu { MenuName = "文件管理", TransKey = "menu.routine.document._self", ParentId = parentId, OrderNum = 7, Path = "document", Component = "", MenuType = 0, Perms = "", Icon = "FolderOutlined", Remark = "文件管理目录" },
        new TaktMenu { MenuName = "合同管理", TransKey = "menu.routine.contract._self", ParentId = parentId, OrderNum = 8, Path = "contract", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "合同管理目录" },
        new TaktMenu { MenuName = "项目管理", TransKey = "menu.routine.project._self", ParentId = parentId, OrderNum = 9, Path = "project", Component = "", MenuType = 0, Perms = "", Icon = "ProjectOutlined", Remark = "项目管理目录" },
        new TaktMenu { MenuName = "任务管理", TransKey = "menu.routine.quartz._self", ParentId = parentId, OrderNum = 10, Path = "quartz", Component = "", MenuType = 0, Perms = "", Icon = "ScheduleOutlined", Remark = "任务管理目录" },
        new TaktMenu { MenuName = "办公用品", TransKey = "menu.routine.officesupplies._self", ParentId = parentId, OrderNum = 11, Path = "officesupplies", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品管理目录" },
        new TaktMenu { MenuName = "图书管理", TransKey = "menu.routine.Library._self", ParentId = parentId, OrderNum = 12, Path = "Library", Component = "", MenuType = 0, Perms = "", Icon = "BookOutlined", Remark = "图书管理目录" },
        new TaktMenu { MenuName = "医务管理", TransKey = "menu.routine.medical._self", ParentId = parentId, OrderNum = 13, Path = "medical", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "医务管理目录" }
    };

  /// <summary>
  /// 获取日常事务二级菜单项列表
  /// </summary>
  public static List<TaktMenu> GetRoutineSecondMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "编码规则", TransKey = "menu.routine.numberrule", ParentId = parentId, OrderNum = 1, Path = "numberrule", Component = "routine/numberrule/index", MenuType = 1, Perms = "routine:numberrule:list", Icon = "CodeOutlined", Remark = "编码规则菜单" },
        new TaktMenu { MenuName = "通用设置", TransKey = "menu.routine.settings", ParentId = parentId, OrderNum = 2, Path = "settings", Component = "routine/settings/index", MenuType = 1, Perms = "routine:settings:list", Icon = "ToolOutlined", Remark = "通用设置菜单" },
        new TaktMenu { MenuName = "语言管理", TransKey = "menu.routine.i18n", ParentId = parentId, OrderNum = 3, Path = "i18n", Component = "routine/i18n/index", MenuType = 1, Perms = "routine:i18n:list", Icon = "TranslationOutlined", Remark = "语言管理菜单" },
        new TaktMenu { MenuName = "字典管理", TransKey = "menu.routine.dict", ParentId = parentId, OrderNum = 4, Path = "dict", Component = "routine/dict/index", MenuType = 1, Perms = "routine:dict:list", Icon = "BookOutlined", Remark = "字典管理菜单" },
        new TaktMenu { MenuName = "在线用户", TransKey = "menu.routine.online", ParentId = parentId, OrderNum = 5, Path = "online", Component = "routine/online/index", MenuType = 1, Perms = "routine:online:list", Icon = "TeamOutlined", Remark = "在线用户菜单" },
        new TaktMenu { MenuName = "在线消息", TransKey = "menu.routine.message", ParentId = parentId, OrderNum = 6, Path = "message", Component = "routine/message/index", MenuType = 1, Perms = "routine:message:list", Icon = "MessageOutlined", Remark = "在线消息菜单" }
    };

  /// <summary>
  /// 获取新闻管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineNewsThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "热点新闻", TransKey = "menu.routine.news.hot", ParentId = parentId, OrderNum = 1, Path = "hot", Component = "routine/news/hot/index", MenuType = 1, Perms = "routine:news:hot:list", Icon = "FireOutlined", Remark = "热点新闻管理" },
        new TaktMenu { MenuName = "评论管理", TransKey = "menu.routine.news.comment", ParentId = parentId, OrderNum = 2, Path = "comment", Component = "routine/news/comment/index", MenuType = 1, Perms = "routine:news:comment:list", Icon = "MessageOutlined", Remark = "评论管理" },
        new TaktMenu { MenuName = "点赞管理", TransKey = "menu.routine.news.like", ParentId = parentId, OrderNum = 3, Path = "like", Component = "routine/news/like/index", MenuType = 1, Perms = "routine:news:like:list", Icon = "LikeOutlined", Remark = "新闻点赞管理" }
    };

  /// <summary>
  /// 获取日程管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineScheduleThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "我的日程", TransKey = "menu.routine.schedule.myschedule", ParentId = parentId, OrderNum = 1, Path = "myschedule", Component = "routine/schedule/myschedule/index", MenuType = 1, Perms = "routine:schedule:myschedule:list", Icon = "UserOutlined", Remark = "我的日程" },
        new TaktMenu { MenuName = "团队日程", TransKey = "menu.routine.schedule.team", ParentId = parentId, OrderNum = 2, Path = "team", Component = "routine/schedule/team/index", MenuType = 1, Perms = "routine:schedule:team:list", Icon = "TeamOutlined", Remark = "团队日程" },
        new TaktMenu { MenuName = "日程看板", TransKey = "menu.routine.schedule.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/schedule/dashboard/index", MenuType = 1, Perms = "routine:schedule:dashboard:list", Icon = "DashboardOutlined", Remark = "日程看板" }
    };

  /// <summary>
  /// 获取用车管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineCarThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "用车调度", TransKey = "menu.routine.vehicle.dispatch", ParentId = parentId, OrderNum = 1, Path = "dispatch", Component = "routine/vehicle/dispatch/index", MenuType = 1, Perms = "routine:vehicle:dispatch:list", Icon = "ScheduleOutlined", Remark = "用车调度" },
        new TaktMenu { MenuName = "车辆信息", TransKey = "menu.routine.vehicle.info", ParentId = parentId, OrderNum = 2, Path = "info", Component = "routine/vehicle/info/index", MenuType = 1, Perms = "routine:vehicle:info:list", Icon = "IdcardOutlined", Remark = "车辆信息" },
        new TaktMenu { MenuName = "驾驶员信息", TransKey = "menu.routine.vehicle.driver", ParentId = parentId, OrderNum = 3, Path = "driver", Component = "routine/vehicle/driver/index", MenuType = 1, Perms = "routine:vehicle:driver:list", Icon = "UserOutlined", Remark = "驾驶员信息" },
        new TaktMenu { MenuName = "维保信息", TransKey = "menu.routine.vehicle.maintenance", ParentId = parentId, OrderNum = 4, Path = "maintenance", Component = "routine/vehicle/maintenance/index", MenuType = 1, Perms = "routine:vehicle:maintenance:list", Icon = "ToolOutlined", Remark = "维保信息" }
    };

  /// <summary>
  /// 获取邮件管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineEmailThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "收件箱", TransKey = "menu.routine.email.inbox", ParentId = parentId, OrderNum = 1, Path = "inbox", Component = "routine/email/inbox/index", MenuType = 1, Perms = "routine:email:inbox:list", Icon = "InboxOutlined", Remark = "收件箱" },
        new TaktMenu { MenuName = "草稿箱", TransKey = "menu.routine.email.drafts", ParentId = parentId, OrderNum = 2, Path = "drafts", Component = "routine/email/drafts/index", MenuType = 1, Perms = "routine:email:drafts:list", Icon = "EditOutlined", Remark = "草稿箱" },
        new TaktMenu { MenuName = "已发送", TransKey = "menu.routine.email.sent", ParentId = parentId, OrderNum = 3, Path = "sent", Component = "routine/email/sent/index", MenuType = 1, Perms = "routine:email:sent:list", Icon = "SendOutlined", Remark = "已发送邮件" },
        new TaktMenu { MenuName = "垃圾箱", TransKey = "menu.routine.email.trash", ParentId = parentId, OrderNum = 4, Path = "trash", Component = "routine/email/trash/index", MenuType = 1, Perms = "routine:email:trash:list", Icon = "DeleteOutlined", Remark = "垃圾箱" },
        new TaktMenu { MenuName = "模板", TransKey = "menu.routine.email.template", ParentId = parentId, OrderNum = 5, Path = "template", Component = "routine/email/template/index", MenuType = 1, Perms = "routine:email:template:list", Icon = "FileTextOutlined", Remark = "邮件模板" }
    };

  /// <summary>
  /// 获取会议管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineMeetingThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "我的会议", TransKey = "menu.routine.meeting.mymeeting", ParentId = parentId, OrderNum = 1, Path = "mymeeting", Component = "routine/meeting/mymeeting/index", MenuType = 1, Perms = "routine:meeting:mymeeting:list", Icon = "UserOutlined", Remark = "我的会议" },
        new TaktMenu { MenuName = "预约申请", TransKey = "menu.routine.meeting.booking", ParentId = parentId, OrderNum = 2, Path = "booking", Component = "routine/meeting/booking/index", MenuType = 1, Perms = "routine:meeting:booking:list", Icon = "PlusSquareOutlined", Remark = "会议预约申请" },
        new TaktMenu { MenuName = "会议看板", TransKey = "menu.routine.meeting.dashboard", ParentId = parentId, OrderNum = 3, Path = "dashboard", Component = "routine/meeting/dashboard/index", MenuType = 1, Perms = "routine:meeting:dashboard:list", Icon = "DashboardOutlined", Remark = "会议看板" },
        new TaktMenu { MenuName = "会议室", TransKey = "menu.routine.meeting.room", ParentId = parentId, OrderNum = 4, Path = "room", Component = "routine/meeting/room/index", MenuType = 1, Perms = "routine:meeting:room:list", Icon = "HomeOutlined", Remark = "会议室管理" }
    };

  /// <summary>
  /// 获取公告通知三级目录列表
  /// </summary>
  public static List<TaktMenu> GetRoutineNoticeThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "消息", TransKey = "menu.routine.notice.message._self", ParentId = parentId, OrderNum = 1, Path = "message", Component = "", MenuType = 0, Perms = "", Icon = "MessageOutlined", Remark = "消息目录" },
        new TaktMenu { MenuName = "公告", TransKey = "menu.routine.notice.announcement._self", ParentId = parentId, OrderNum = 2, Path = "announcement", Component = "", MenuType = 0, Perms = "", Icon = "SoundOutlined", Remark = "公告目录" },
        new TaktMenu { MenuName = "通知", TransKey = "menu.routine.notice.notification._self", ParentId = parentId, OrderNum = 3, Path = "notification", Component = "", MenuType = 0, Perms = "", Icon = "NotificationOutlined", Remark = "通知目录" }
    };





  /// <summary>
  /// 获取文件管理三级目录列表
  /// </summary>
  public static List<TaktMenu> GetRoutineFileThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "规章制度", TransKey = "menu.routine.document.regulation._self", ParentId = parentId, OrderNum = 2, Path = "regulation", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "规章制度目录" },
        new TaktMenu { MenuName = "ISO文件", TransKey = "menu.routine.document.iso._self", ParentId = parentId, OrderNum = 4, Path = "iso", Component = "", MenuType = 0, Perms = "", Icon = "SafetyCertificateOutlined", Remark = "ISO文件目录" },
        new TaktMenu { MenuName = "公文文件", TransKey = "menu.routine.document.official._self", ParentId = parentId, OrderNum = 5, Path = "official", Component = "", MenuType = 0, Perms = "", Icon = "FileProtectOutlined", Remark = "公文文件目录" }
    };

  /// <summary>
  /// 获取文件管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineFileThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "日常文件", TransKey = "menu.routine.document.file._self", ParentId = parentId, OrderNum = 1, Path = "file", Component = "routine/document/file/index", MenuType = 1, Perms = "routine:document:file:list", Icon = "FileTextOutlined", Remark = "日常文件" },
        new TaktMenu { MenuName = "法律法规", TransKey = "menu.routine.document.law._self", ParentId = parentId, OrderNum = 2, Path = "law", Component = "routine/document/law/index", MenuType = 1, Perms = "routine:document:law:list", Icon = "FileProtectOutlined", Remark = "法律法规" }
    };

  /// <summary>
  /// 获取办公用品管理三级目录列表
  /// </summary>
  public static List<TaktMenu> GetRoutineOfficeSuppliesThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "库存", TransKey = "menu.routine.officesupplies.inventory._self", ParentId = parentId, OrderNum = 1, Path = "inventory", Component = "", MenuType = 0, Perms = "", Icon = "InboxOutlined", Remark = "办公用品库存管理目录" },
        new TaktMenu { MenuName = "领用", TransKey = "menu.routine.officesupplies.usage._self", ParentId = parentId, OrderNum = 2, Path = "usage", Component = "", MenuType = 0, Perms = "", Icon = "ExportOutlined", Remark = "办公用品领用管理目录" }
    };

  /// <summary>
  /// 获取图书管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineBookThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "图书借还", TransKey = "menu.routine.Library.borrow", ParentId = parentId, OrderNum = 1, Path = "borrow", Component = "routine/Library/borrow/index", MenuType = 1, Perms = "routine:Library:borrow:list", Icon = "SwapOutlined", Remark = "图书借还管理" },
        new TaktMenu { MenuName = "图书编目", TransKey = "menu.routine.Library.catalog", ParentId = parentId, OrderNum = 2, Path = "catalog", Component = "routine/Library/catalog/index", MenuType = 1, Perms = "routine:Library:catalog:list", Icon = "FileTextOutlined", Remark = "图书编目管理" },
        new TaktMenu { MenuName = "读者管理", TransKey = "menu.routine.Library.reader", ParentId = parentId, OrderNum = 3, Path = "reader", Component = "routine/Library/reader/index", MenuType = 1, Perms = "routine:Library:reader:list", Icon = "UserOutlined", Remark = "读者管理" },
        new TaktMenu { MenuName = "库存管理", TransKey = "menu.routine.Library.inventory", ParentId = parentId, OrderNum = 4, Path = "inventory", Component = "routine/Library/inventory/index", MenuType = 1, Perms = "routine:Library:inventory:list", Icon = "InboxOutlined", Remark = "库存管理" },
        new TaktMenu { MenuName = "罚款管理", TransKey = "menu.routine.Library.fine", ParentId = parentId, OrderNum = 5, Path = "fine", Component = "routine/Library/fine/index", MenuType = 1, Perms = "routine:Library:fine:list", Icon = "MoneyCollectOutlined", Remark = "罚款管理" }
    };

  /// <summary>
  /// 获取医务管理三级目录列表
  /// </summary>
  public static List<TaktMenu> GetRoutineMedicalThirdLevelMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "药品", TransKey = "menu.routine.medical.medicine._self", ParentId = parentId, OrderNum = 1, Path = "medicine", Component = "", MenuType = 0, Perms = "", Icon = "MedicineBoxOutlined", Remark = "药品管理目录" },
        new TaktMenu { MenuName = "领用", TransKey = "menu.routine.medical.usage._self", ParentId = parentId, OrderNum = 2, Path = "usage", Component = "", MenuType = 0, Perms = "", Icon = "ExportOutlined", Remark = "医务领用管理目录" }
    };


  /// <summary>
  /// 获取消息四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineMessageFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "我的消息", TransKey = "menu.routine.notice.message.mymessages", ParentId = parentId, OrderNum = 1, Path = "mymessages", Component = "routine/notice/message/mymessages/index", MenuType = 1, Perms = "routine:notice:message:mymessages:list", Icon = "UserOutlined", Remark = "我的消息" },
        new TaktMenu { MenuName = "消息列表", TransKey = "menu.routine.notice.message.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/notice/message/list/index", MenuType = 1, Perms = "routine:notice:message:list:list", Icon = "BarsOutlined", Remark = "消息列表" }
    };

  /// <summary>
  /// 获取公告四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineAnnouncementFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "公告管理", TransKey = "menu.routine.notice.announcement.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/notice/announcement/manage/index", MenuType = 1, Perms = "routine:notice:announcement:manage:list", Icon = "FileTextOutlined", Remark = "公告管理" },
        new TaktMenu { MenuName = "公告收发", TransKey = "menu.routine.notice.announcement.sendreceive", ParentId = parentId, OrderNum = 2, Path = "sendreceive", Component = "routine/notice/announcement/sendreceive/index", MenuType = 1, Perms = "routine:notice:announcement:sendreceive:list", Icon = "SendOutlined", Remark = "公告收发" }
    };

  /// <summary>
  /// 获取通知四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineNotificationFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "通知管理", TransKey = "menu.routine.notice.notification.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/notice/notification/manage/index", MenuType = 1, Perms = "routine:notice:notification:manage:list", Icon = "FileTextOutlined", Remark = "通知管理" },
        new TaktMenu { MenuName = "通知收发", TransKey = "menu.routine.notice.notification.sendreceive", ParentId = parentId, OrderNum = 2, Path = "sendreceive", Component = "routine/notice/notification/sendreceive/index", MenuType = 1, Perms = "routine:notice:notification:sendreceive:list", Icon = "SendOutlined", Remark = "通知收发" }
    };



  /// <summary>
  /// 获取日常文件四级子菜单列表
  /// </summary>


  /// <summary>
  /// 获取ISO文件四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineIsoFileFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "ISO管理", TransKey = "menu.routine.document.iso.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/iso/manage/index", MenuType = 1, Perms = "routine:document:iso:manage:list", Icon = "FileTextOutlined", Remark = "ISO文件管理" },
        new TaktMenu { MenuName = "ISO控制", TransKey = "menu.routine.document.iso.control", ParentId = parentId, OrderNum = 2, Path = "control", Component = "routine/document/iso/control/index", MenuType = 1, Perms = "routine:document:iso:control:list", Icon = "ControlOutlined", Remark = "ISO文件控制" }
    };



  /// <summary>
  /// 获取规章制度四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineRegulationFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "制度管理", TransKey = "menu.routine.document.regulation.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/regulation/manage/index", MenuType = 1, Perms = "routine:document:regulation:manage:list", Icon = "FileTextOutlined", Remark = "规章制度管理" },
        new TaktMenu { MenuName = "制度控制", TransKey = "menu.routine.document.regulation.control", ParentId = parentId, OrderNum = 2, Path = "control", Component = "routine/document/regulation/control/index", MenuType = 1, Perms = "routine:document:regulation:control:list", Icon = "ControlOutlined", Remark = "规章制度控制" }
    };

  /// <summary>
  /// 获取公文文件四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineDocumentFileFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "公文管理", TransKey = "menu.routine.document.official.manage", ParentId = parentId, OrderNum = 1, Path = "manage", Component = "routine/document/official/manage/index", MenuType = 1, Perms = "routine:document:official:manage:list", Icon = "FileTextOutlined", Remark = "公文文件管理" },
        new TaktMenu { MenuName = "公文发布", TransKey = "menu.routine.document.official.issuance", ParentId = parentId, OrderNum = 2, Path = "issuance", Component = "routine/document/official/issuance/index", MenuType = 1, Perms = "routine:document:official:issuance:list", Icon = "SendOutlined", Remark = "公文文件发布" }
    };

  /// <summary>
  /// 获取办公用品库存四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineOfficeSuppliesInventoryFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "请购", TransKey = "menu.routine.officesupplies.inventory.requisition", ParentId = parentId, OrderNum = 1, Path = "requisition", Component = "routine/officesupplies/inventory/requisition/index", MenuType = 1, Perms = "routine:officesupplies:inventory:requisition:list", Icon = "FileAddOutlined", Remark = "办公用品请购管理" },
        new TaktMenu { MenuName = "入库", TransKey = "menu.routine.officesupplies.inventory.inbound", ParentId = parentId, OrderNum = 2, Path = "inbound", Component = "routine/officesupplies/inventory/inbound/index", MenuType = 1, Perms = "routine:officesupplies:inventory:inbound:list", Icon = "InboxOutlined", Remark = "办公用品入库管理" },
        new TaktMenu { MenuName = "盘点", TransKey = "menu.routine.officesupplies.inventory.stocktaking", ParentId = parentId, OrderNum = 3, Path = "stocktaking", Component = "routine/officesupplies/inventory/stocktaking/index", MenuType = 1, Perms = "routine:officesupplies:inventory:stocktaking:list", Icon = "ReconciliationOutlined", Remark = "办公用品盘点管理" }
    };

  /// <summary>
  /// 获取办公用品领用四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineOfficeSuppliesUsageFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "申请", TransKey = "menu.routine.officesupplies.usage.apply", ParentId = parentId, OrderNum = 1, Path = "apply", Component = "routine/officesupplies/usage/apply/index", MenuType = 1, Perms = "routine:officesupplies:usage:apply:list", Icon = "FileAddOutlined", Remark = "办公用品申请管理" },
        new TaktMenu { MenuName = "列表", TransKey = "menu.routine.officesupplies.usage.list", ParentId = parentId, OrderNum = 2, Path = "list", Component = "routine/officesupplies/usage/list/index", MenuType = 1, Perms = "routine:officesupplies:usage:list:list", Icon = "BarsOutlined", Remark = "办公用品领用列表" }
    };

  /// <summary>
  /// 获取药品管理四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineMedicineFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "请购", TransKey = "menu.routine.medical.medicine.requisition", ParentId = parentId, OrderNum = 1, Path = "requisition", Component = "routine/medical/medicine/requisition/index", MenuType = 1, Perms = "routine:medical:medicine:requisition:list", Icon = "FileAddOutlined", Remark = "药品请购管理" },
        new TaktMenu { MenuName = "入库", TransKey = "menu.routine.medical.medicine.inbound", ParentId = parentId, OrderNum = 2, Path = "inbound", Component = "routine/medical/medicine/inbound/index", MenuType = 1, Perms = "routine:medical:medicine:inbound:list", Icon = "InboxOutlined", Remark = "药品入库管理" },
        new TaktMenu { MenuName = "清单", TransKey = "menu.routine.medical.medicine.list", ParentId = parentId, OrderNum = 3, Path = "list", Component = "routine/medical/medicine/list/index", MenuType = 1, Perms = "routine:medical:medicine:list:list", Icon = "BarsOutlined", Remark = "药品清单管理" },
        new TaktMenu { MenuName = "盘点", TransKey = "menu.routine.medical.medicine.stocktaking", ParentId = parentId, OrderNum = 4, Path = "stocktaking", Component = "routine/medical/medicine/stocktaking/index", MenuType = 1, Perms = "routine:medical:medicine:stocktaking:list", Icon = "ReconciliationOutlined", Remark = "药品盘点管理" }
    };

  /// <summary>
  /// 获取医务领用四级子菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineMedicalUsageFourthMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "档案", TransKey = "menu.routine.medical.usage.archive", ParentId = parentId, OrderNum = 1, Path = "archive", Component = "routine/medical/usage/archive/index", MenuType = 1, Perms = "routine:medical:usage:archive:list", Icon = "FileTextOutlined", Remark = "医务档案管理" },
        new TaktMenu { MenuName = "领药", TransKey = "menu.routine.medical.usage.receive", ParentId = parentId, OrderNum = 2, Path = "receive", Component = "routine/medical/usage/receive/index", MenuType = 1, Perms = "routine:medical:usage:receive:list", Icon = "ExportOutlined", Remark = "医务领药管理" },
        new TaktMenu { MenuName = "费用", TransKey = "menu.routine.medical.usage.cost", ParentId = parentId, OrderNum = 3, Path = "cost", Component = "routine/medical/usage/cost/index", MenuType = 1, Perms = "routine:medical:usage:cost:list", Icon = "MoneyCollectOutlined", Remark = "医务费用管理" }
    };

  // ==================== 合同管理菜单 ====================

  /// <summary>
  /// 获取合同管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineContractThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "合同模板", TransKey = "menu.routine.contract.template", ParentId = parentId, OrderNum = 1, Path = "template", Component = "routine/contract/template/index", MenuType = 1, Perms = "routine:contract:template:list", Icon = "FileTextOutlined", Remark = "合同模板目录" },
        new TaktMenu { MenuName = "合同起草", TransKey = "menu.routine.contract.draft", ParentId = parentId, OrderNum = 2, Path = "draft", Component = "routine/contract/draft/index", MenuType = 1, Perms = "routine:contract:draft:list", Icon = "EditOutlined", Remark = "合同起草目录" },
        new TaktMenu { MenuName = "合同审批", TransKey = "menu.routine.contract.approval", ParentId = parentId, OrderNum = 3, Path = "approval", Component = "routine/contract/approval/index", MenuType = 1, Perms = "routine:contract:approval:list", Icon = "CheckSquareOutlined", Remark = "合同审批目录" },
        new TaktMenu { MenuName = "合同执行", TransKey = "menu.routine.contract.execution", ParentId = parentId, OrderNum = 4, Path = "execution", Component = "routine/contract/execution/index", MenuType = 1, Perms = "routine:contract:execution:list", Icon = "PlayCircleOutlined", Remark = "合同执行目录" },
        new TaktMenu { MenuName = "合同归档", TransKey = "menu.routine.contract.archive", ParentId = parentId, OrderNum = 5, Path = "archive", Component = "routine/contract/archive/index", MenuType = 1, Perms = "routine:contract:archive:list", Icon = "InboxOutlined", Remark = "合同归档目录" }
    };



  // ==================== 项目管理菜单 ====================

  /// <summary>
  /// 获取项目管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineProjectThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "项目信息", TransKey = "menu.routine.project.info", ParentId = parentId, OrderNum = 1, Path = "info", Component = "routine/project/info/index", MenuType = 1, Perms = "routine:project:info:list", Icon = "ProjectOutlined", Remark = "项目信息目录" },
        new TaktMenu { MenuName = "项目计划", TransKey = "menu.routine.project.plan", ParentId = parentId, OrderNum = 2, Path = "plan", Component = "routine/project/plan/index", MenuType = 1, Perms = "routine:project:plan:list", Icon = "CalendarOutlined", Remark = "项目计划目录" },
        new TaktMenu { MenuName = "项目任务", TransKey = "menu.routine.project.task", ParentId = parentId, OrderNum = 3, Path = "task", Component = "routine/project/task/index", MenuType = 1, Perms = "routine:project:task:list", Icon = "CheckSquareOutlined", Remark = "项目任务目录" },
        new TaktMenu { MenuName = "项目资源", TransKey = "menu.routine.project.resource", ParentId = parentId, OrderNum = 4, Path = "resource", Component = "routine/project/resource/index", MenuType = 1, Perms = "routine:project:resource:list", Icon = "TeamOutlined", Remark = "项目资源目录" },
        new TaktMenu { MenuName = "项目监控", TransKey = "menu.routine.project.monitor", ParentId = parentId, OrderNum = 5, Path = "monitor", Component = "routine/project/monitor/index", MenuType = 1, Perms = "routine:project:monitor:list", Icon = "DashboardOutlined", Remark = "项目监控目录" }
    };



  // ==================== 任务管理菜单 ====================

  /// <summary>
  /// 获取任务管理三级菜单列表
  /// </summary>
  public static List<TaktMenu> GetRoutineQuartzThirdMenus(long parentId) => new List<TaktMenu> {
        new TaktMenu { MenuName = "定时任务", TransKey = "menu.routine.quartz.job", ParentId = parentId, OrderNum = 1, Path = "job", Component = "routine/quartz/job/index", MenuType = 1, Perms = "routine:quartz:job:list", Icon = "ClockCircleOutlined", Remark = "定时任务目录" },
        new TaktMenu { MenuName = "任务调度", TransKey = "menu.routine.quartz.schedule", ParentId = parentId, OrderNum = 2, Path = "schedule", Component = "routine/quartz/schedule/index", MenuType = 1, Perms = "routine:quartz:schedule:list", Icon = "ScheduleOutlined", Remark = "任务调度目录" }
    };



}