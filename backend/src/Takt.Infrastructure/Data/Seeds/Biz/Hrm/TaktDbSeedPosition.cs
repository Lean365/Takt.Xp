//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedPosition.cs
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : V0.0.1
// 描述   : HRM职位数据提供类
//===================================================================

using Takt.Domain.Entities.HumanResource.Organization;

namespace Takt.Infrastructure.Data.Seeds.Biz.Hrm;

/// <summary>
/// HRM职位数据提供类
/// </summary>
public class TaktDbSeedPosition
{
    /// <summary>
    /// 获取默认职位数据
    /// </summary>
    /// <returns>职位数据列表</returns>
    public List<TaktPosition> GetDefaultPositions()
    {
        return new List<TaktPosition>
        {
            // 管理职通道 - 管理职
            new TaktPosition { PosCode = "BUCHO", PosName = "部长", EnglishName = "Bucho", CategoryId = 1, PosLevel = 5, PosSequence = 1, Status = 1, OrderNum = 1, Description = "部门最高负责人", Responsibilities = "负责部门整体战略规划、目标制定、资源配置和团队管理" },
            new TaktPosition { PosCode = "JICHO", PosName = "次长", EnglishName = "Jicho", CategoryId = 1, PosLevel = 4, PosSequence = 1, Status = 1, OrderNum = 2, Description = "部门副部长", Responsibilities = "协助部长管理部门事务，负责具体业务执行和团队协调" },
            new TaktPosition { PosCode = "KACHO", PosName = "课长", EnglishName = "Kacho", CategoryId = 1, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 3, Description = "科室负责人", Responsibilities = "负责科室日常事务、业务规划和团队指导" },
            new TaktPosition { PosCode = "KAKARI-CHO", PosName = "系长", EnglishName = "Kakari-cho", CategoryId = 1, PosLevel = 2, PosSequence = 1, Status = 1, OrderNum = 4, Description = "团队领导者", Responsibilities = "负责团队日常运作、任务分配和成员指导" },
            new TaktPosition { PosCode = "HAN-CHO", PosName = "班长", EnglishName = "Han-cho", CategoryId = 1, PosLevel = 1, PosSequence = 1, Status = 1, OrderNum = 5, Description = "一线主管", Responsibilities = "负责生产线管理、质量控制和安全监督" },
            
            // 专业职通道 - 专业职
            new TaktPosition { PosCode = "SENIOR-SPEC", PosName = "高级专家/主幹", EnglishName = "Senior Specialist/Core Staff", CategoryId = 2, PosLevel = 4, PosSequence = 2, Status = 1, OrderNum = 6, Description = "高级专家/主幹", Responsibilities = "负责专业技术指导、复杂问题解决和技术创新" },
            new TaktPosition { PosCode = "SPECIALIST", PosName = "专家/主事", EnglishName = "Specialist/Coordinator", CategoryId = 2, PosLevel = 3, PosSequence = 2, Status = 1, OrderNum = 7, Description = "专家/主事", Responsibilities = "负责专业技术工作、项目协调和技术支持" },
            new TaktPosition { PosCode = "SHUNIN", PosName = "主任", EnglishName = "Shunin", CategoryId = 2, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 8, Description = "资深专业岗", Responsibilities = "负责专业领域工作、技术指导和经验传承" },
            new TaktPosition { PosCode = "TANTO", PosName = "一般担当", EnglishName = "Tanto", CategoryId = 2, PosLevel = 1, PosSequence = 2, Status = 1, OrderNum = 9, Description = "专员/科员", Responsibilities = "负责具体业务执行、数据分析和基础工作" },
            
            // 生产制造相关职位
            new TaktPosition { PosCode = "PROD-MGR", PosName = "生产经理", EnglishName = "Production Manager", CategoryId = 3, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 10, Description = "生产经理", Responsibilities = "负责生产计划制定、生产进度控制和产能管理" },
            new TaktPosition { PosCode = "LINE-LEADER", PosName = "线长", EnglishName = "Line Leader", CategoryId = 3, PosLevel = 2, PosSequence = 1, Status = 1, OrderNum = 11, Description = "生产线长", Responsibilities = "负责生产线管理、人员调度和质量控制" },
            new TaktPosition { PosCode = "OPERATOR", PosName = "操作员", EnglishName = "Operator", CategoryId = 3, PosLevel = 1, PosSequence = 2, Status = 1, OrderNum = 12, Description = "生产操作员", Responsibilities = "负责设备操作、产品生产和基础维护" },
            new TaktPosition { PosCode = "TECHNICIAN", PosName = "技术员", EnglishName = "Technician", CategoryId = 3, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 13, Description = "生产技术员", Responsibilities = "负责设备维护、工艺调试和技术支持" },
            
            // 品质管理相关职位
            new TaktPosition { PosCode = "QA-MGR", PosName = "品质经理", EnglishName = "Quality Manager", CategoryId = 4, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 14, Description = "品质经理", Responsibilities = "负责品质体系建立、品质标准制定和品质改进" },
            new TaktPosition { PosCode = "QA-ENGINEER", PosName = "品质工程师", EnglishName = "Quality Engineer", CategoryId = 4, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 15, Description = "品质工程师", Responsibilities = "负责品质分析、问题解决和品质改进" },
            new TaktPosition { PosCode = "QC-INSPECTOR", PosName = "检验员", EnglishName = "QC Inspector", CategoryId = 4, PosLevel = 1, PosSequence = 2, Status = 1, OrderNum = 16, Description = "品质检验员", Responsibilities = "负责产品检验、数据记录和异常报告" },
            
            // 技术开发相关职位
            new TaktPosition { PosCode = "R&D-MGR", PosName = "研发经理", EnglishName = "R&D Manager", CategoryId = 5, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 17, Description = "研发经理", Responsibilities = "负责研发项目管理、技术规划和团队协调" },
            new TaktPosition { PosCode = "R&D-ENGINEER", PosName = "研发工程师", EnglishName = "R&D Engineer", CategoryId = 5, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 18, Description = "研发工程师", Responsibilities = "负责产品设计、技术开发和测试验证" },
            new TaktPosition { PosCode = "PROCESS-ENGINEER", PosName = "工艺工程师", EnglishName = "Process Engineer", CategoryId = 5, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 19, Description = "工艺工程师", Responsibilities = "负责工艺流程设计、工艺改进和工艺标准化" },
            
            // 采购管理相关职位
            new TaktPosition { PosCode = "PURCHASING-MGR", PosName = "采购经理", EnglishName = "Purchasing Manager", CategoryId = 6, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 20, Description = "采购经理", Responsibilities = "负责采购策略制定、供应商管理和成本控制" },
            new TaktPosition { PosCode = "PURCHASING-SPEC", PosName = "采购专员", EnglishName = "Purchasing Specialist", CategoryId = 6, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 21, Description = "采购专员", Responsibilities = "负责采购执行、供应商沟通和订单管理" },
            new TaktPosition { PosCode = "MATERIALS-MGR", PosName = "资材经理", EnglishName = "Materials Manager", CategoryId = 6, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 22, Description = "资材经理", Responsibilities = "负责资材管理、库存控制和物流协调" },
            
            // 销售营业相关职位
            new TaktPosition { PosCode = "SALES-MGR", PosName = "营业经理", EnglishName = "Sales Manager", CategoryId = 7, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 23, Description = "营业经理", Responsibilities = "负责销售策略制定、客户关系管理和销售目标达成" },
            new TaktPosition { PosCode = "SALES-REP", PosName = "营业代表", EnglishName = "Sales Representative", CategoryId = 7, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 24, Description = "营业代表", Responsibilities = "负责客户开发、订单获取和客户服务" },
            
            // 管理支持相关职位
            new TaktPosition { PosCode = "HR-MGR", PosName = "人事经理", EnglishName = "HR Manager", CategoryId = 8, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 25, Description = "人事经理", Responsibilities = "负责人力资源规划、招聘管理和员工发展" },
            new TaktPosition { PosCode = "HR-SPEC", PosName = "人事专员", EnglishName = "HR Specialist", CategoryId = 8, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 26, Description = "人事专员", Responsibilities = "负责人事事务处理、员工服务和基础管理" },
            new TaktPosition { PosCode = "FINANCE-MGR", PosName = "财务经理", EnglishName = "Finance Manager", CategoryId = 8, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 27, Description = "财务经理", Responsibilities = "负责财务管理、成本控制和财务分析" },
            new TaktPosition { PosCode = "ACCOUNTANT", PosName = "会计", EnglishName = "Accountant", CategoryId = 8, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 28, Description = "会计", Responsibilities = "负责会计核算、账务处理和财务报告" },
            new TaktPosition { PosCode = "IT-MGR", PosName = "IT经理", EnglishName = "IT Manager", CategoryId = 8, PosLevel = 3, PosSequence = 1, Status = 1, OrderNum = 29, Description = "IT经理", Responsibilities = "负责IT系统规划、系统维护和技术支持" },
            new TaktPosition { PosCode = "IT-SPEC", PosName = "IT专员", EnglishName = "IT Specialist", CategoryId = 8, PosLevel = 2, PosSequence = 2, Status = 1, OrderNum = 30, Description = "IT专员", Responsibilities = "负责系统维护、用户支持和基础开发" }
        };
    }
}




