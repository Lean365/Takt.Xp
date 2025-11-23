//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedDepartment.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : HRM部门数据提供类
//===================================================================

using Takt.Domain.Entities.HumanResource.Organization;

namespace Takt.Infrastructure.Data.Seeds.Biz.Hrm;

/// <summary>
/// HRM部门数据提供类
/// </summary>
public class TaktDbSeedDepartment
{
    /// <summary>
    /// 获取默认部门数据
    /// </summary>
    /// <returns>部门数据列表</returns>
    public List<TaktDepartment> GetDefaultDepartments()
    {
        return new List<TaktDepartment>
        {
            // 一级部门 - 总经理/工厂长
            new TaktDepartment { DeptCode = "GM", DeptName = "总经理/工厂长", EnglishName = "General Manager/Factory Director", ParentId = null, DeptLevel = 1, DeptType = 1, OrderNum = 1, Phone = "400-888-0001", Email = "gm@Takt.com", Status = 1, Description = "代表取締役，负责整体战略规划和管理" },
            
            // 二级部门 - 六大本部
            new TaktDepartment { DeptCode = "PROD-HQ", DeptName = "生产本部", EnglishName = "Production Headquarters", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 1, Phone = "0755-88888801", Email = "prod-hq@Takt.com", Status = 1, Description = "生产本部，负责生产制造管理" },
            new TaktDepartment { DeptCode = "QA-DEPT", DeptName = "品质保证部", EnglishName = "Quality Assurance Department", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 2, Phone = "0755-88888802", Email = "qa-dept@Takt.com", Status = 1, Description = "品质保证部，负责质量管理和品质保证" },
            new TaktDepartment { DeptCode = "TECH-DEPT", DeptName = "生产技术部", EnglishName = "Production Technology Department", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 3, Phone = "0755-88888803", Email = "tech-dept@Takt.com", Status = 1, Description = "生产技术部，负责生产技术研发和工艺改进" },
            new TaktDepartment { DeptCode = "PUR-DEPT", DeptName = "采购/资材部", EnglishName = "Purchasing/Materials Department", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 4, Phone = "0755-88888804", Email = "pur-dept@Takt.com", Status = 1, Description = "采购/资材部，负责物料采购和资材管理" },
            new TaktDepartment { DeptCode = "MGMT-HQ", DeptName = "管理本部", EnglishName = "Management Headquarters", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 5, Phone = "0755-88888805", Email = "mgmt-hq@Takt.com", Status = 1, Description = "管理本部，负责公司管理和支持职能" },
            new TaktDepartment { DeptCode = "SALES-DEPT", DeptName = "营业部", EnglishName = "Sales Department", ParentId = 1, DeptLevel = 2, DeptType = 2, OrderNum = 6, Phone = "0755-88888806", Email = "sales-dept@Takt.com", Status = 1, Description = "营业部，负责公司产品销售和客户关系管理" },
            
            // 三级部门 - 生产本部下级
            new TaktDepartment { DeptCode = "PROD-HQ-PLAN", DeptName = "生产管理课", EnglishName = "Production Management Section", ParentId = 2, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888811", Email = "prod-plan@Takt.com", Status = 1, Description = "生产管理课，负责计划与物料控制" },
            new TaktDepartment { DeptCode = "PROD-HQ-MFG", DeptName = "制造课", EnglishName = "Manufacturing Section", ParentId = 2, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888812", Email = "prod-mfg@Takt.com", Status = 1, Description = "制造课，负责执行生产与5S管理" },
            new TaktDepartment { DeptCode = "PROD-HQ-EQUIP", DeptName = "设备保全课", EnglishName = "Equipment Maintenance Section", ParentId = 2, DeptLevel = 3, DeptType = 3, OrderNum = 3, Phone = "0755-88888813", Email = "prod-equip@Takt.com", Status = 1, Description = "设备保全课，负责设备维护与TPM" },
            
            // 三级部门 - 品质保证部下级
            new TaktDepartment { DeptCode = "QA-DEPT-MGMT", DeptName = "品质管理课", EnglishName = "Quality Management Section", ParentId = 3, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888821", Email = "qa-mgmt@Takt.com", Status = 1, Description = "品质管理课，负责IQC/IPQC/FQC" },
            new TaktDepartment { DeptCode = "QA-DEPT-ASS", DeptName = "品质保证课", EnglishName = "Quality Assurance Section", ParentId = 3, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888822", Email = "qa-ass@Takt.com", Status = 1, Description = "品质保证课，负责体系与客户对应" },
            
            // 三级部门 - 生产技术部下级
            new TaktDepartment { DeptCode = "TECH-DEPT-PROC", DeptName = "工艺课", EnglishName = "Process Section", ParentId = 4, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888831", Email = "tech-proc@Takt.com", Status = 1, Description = "工艺课，负责工艺流程与改善" },
            new TaktDepartment { DeptCode = "TECH-DEPT-DEV", DeptName = "开发设计课", EnglishName = "Development and Design Section", ParentId = 4, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888832", Email = "tech-dev@Takt.com", Status = 1, Description = "开发设计课，负责产品与模具设计" },
            
            // 三级部门 - 采购/资材部下级
            new TaktDepartment { DeptCode = "PUR-DEPT-PUR", DeptName = "采购课", EnglishName = "Purchasing Section", ParentId = 5, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888841", Email = "pur-purchasing@Takt.com", Status = 1, Description = "采购课，负责供应商管理" },
            new TaktDepartment { DeptCode = "PUR-DEPT-MAT", DeptName = "资材课", EnglishName = "Materials Section", ParentId = 5, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888842", Email = "pur-materials@Takt.com", Status = 1, Description = "资材课，负责仓库与物流管理" },
            
            // 三级部门 - 管理本部下级
            new TaktDepartment { DeptCode = "MGMT-HQ-HR", DeptName = "总务人事课", EnglishName = "General Affairs and HR Section", ParentId = 6, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888851", Email = "mgmt-hr@Takt.com", Status = 1, Description = "总务人事课，负责人力资源与行政" },
            new TaktDepartment { DeptCode = "MGMT-HQ-FIN", DeptName = "财务课", EnglishName = "Finance Section", ParentId = 6, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888852", Email = "mgmt-fin@Takt.com", Status = 1, Description = "财务课，负责会计与成本控制" },
            new TaktDepartment { DeptCode = "MGMT-HQ-IT", DeptName = "IT课", EnglishName = "IT Section", ParentId = 6, DeptLevel = 3, DeptType = 3, OrderNum = 3, Phone = "0755-88888853", Email = "mgmt-it@Takt.com", Status = 1, Description = "IT课，负责系统维护" },
            
            // 三级部门 - 营业部下级
            new TaktDepartment { DeptCode = "SALES-DEPT-DOM", DeptName = "国内营业课", EnglishName = "Domestic Sales Section", ParentId = 7, DeptLevel = 3, DeptType = 3, OrderNum = 1, Phone = "0755-88888861", Email = "sales-dom@Takt.com", Status = 1, Description = "国内营业课，负责国内销售业务" },
            new TaktDepartment { DeptCode = "SALES-DEPT-INT", DeptName = "海外营业课", EnglishName = "International Sales Section", ParentId = 7, DeptLevel = 3, DeptType = 3, OrderNum = 2, Phone = "0755-88888862", Email = "sales-int@Takt.com", Status = 1, Description = "海外营业课，负责国际销售业务" }
        };
    }
}



