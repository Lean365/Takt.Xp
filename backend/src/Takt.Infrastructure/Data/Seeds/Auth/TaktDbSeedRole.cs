//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRole.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 角色数据提供类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 角色数据提供类
/// </summary>
public class TaktDbSeedRole
{
    /// <summary>
    /// 获取默认角色数据
    /// </summary>
    /// <returns>角色数据列表</returns>
    public List<TaktRole> GetDefaultRoles()
    {
        return new List<TaktRole>
        {
            new TaktRole { RoleKey = "system_admin", RoleName = "系统管理员", OrderNum = 1, RoleStatus=0, Remark = "System Administrator;システム管理者" },
            new TaktRole { RoleKey = "security_admin", RoleName = "安全管理员", OrderNum = 2, RoleStatus=0, Remark = "Security Administrator;セキュリティ管理者" },
            new TaktRole { RoleKey = "biz_manager", RoleName = "业务管理员", OrderNum = 3, RoleStatus=0, Remark = "Business Manager;業務管理者" },
            new TaktRole { RoleKey = "hr_manager", RoleName = "人事管理员", OrderNum = 4, RoleStatus=0, Remark = "HR Manager;人事管理者" },
            new TaktRole { RoleKey = "fin_manager", RoleName = "财务管理员", OrderNum = 5, RoleStatus=0, Remark = "Finance Manager;財務管理者" },
            new TaktRole { RoleKey = "prod_manager", RoleName = "生产管理员", OrderNum = 6, RoleStatus=0, Remark = "Production Manager;生産管理者" },
            new TaktRole { RoleKey = "qc_manager", RoleName = "品质管理员", OrderNum = 7, RoleStatus=0, Remark = "Quality Control Manager;品質管理者" },
            new TaktRole { RoleKey = "wh_manager", RoleName = "仓库管理员", OrderNum = 8, RoleStatus=0, Remark = "Warehouse Manager;倉庫管理者" },
            new TaktRole { RoleKey = "inv_manager", RoleName = "库存管理员", OrderNum = 9, RoleStatus=0, Remark = "Inventory Manager;在庫管理者" },
            new TaktRole { RoleKey = "purchase_manager", RoleName = "采购管理员", OrderNum = 10, RoleStatus=0, Remark = "Purchase Manager;購買管理者" },
            new TaktRole { RoleKey = "supplier_manager", RoleName = "供应商管理员", OrderNum = 11, RoleStatus=0, Remark = "Supplier Manager;仕入先管理者" },
            new TaktRole { RoleKey = "sales_manager", RoleName = "销售管理员", OrderNum = 12, RoleStatus=0, Remark = "Sales Manager;営業管理者" },
            new TaktRole { RoleKey = "customer_manager", RoleName = "客户管理员", OrderNum = 13, RoleStatus=0, Remark = "Customer Manager;顧客管理者" },
            new TaktRole { RoleKey = "master_manager", RoleName = "主数据管理员", OrderNum = 14, RoleStatus=0, Remark = "Master Data Manager;マスタ管理者" },
            new TaktRole { RoleKey = "code_manager", RoleName = "编码管理员", OrderNum = 15, RoleStatus=0, Remark = "Code Manager;コード管理者" },
            new TaktRole { RoleKey = "general_user", RoleName = "一般用户", OrderNum = 16, RoleStatus=0, Remark = "General User;一般ユーザー" },
            new TaktRole { RoleKey = "guest", RoleName = "访客", OrderNum = 17, RoleStatus=0, Remark = "Guest User;ゲストユーザー" }
        };
    }
}

