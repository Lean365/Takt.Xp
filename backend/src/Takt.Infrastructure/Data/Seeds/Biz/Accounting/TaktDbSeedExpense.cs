//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedExpense.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 费用管理种子数据提供类
//===================================================================

using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Infrastructure.Data.Seeds.Biz.Accounting;

/// <summary>
/// 费用管理种子数据提供类
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 提供费用管理的默认种子数据
/// 2. 包含示例费用报销记录和费用签拟单记录
/// 3. 用于系统初始化和测试
/// </remarks>
public class TaktDbSeedExpense
{
    /// <summary>
    /// 获取默认费用报销记录数据
    /// </summary>
    /// <returns>费用报销记录数据列表</returns>
    public List<TaktExpense> GetDefaultExpenses()
    {
        return new List<TaktExpense>
        {
            new TaktExpense
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                ExpenseNo = "EXP20241201001",
                EmployeeId = 1958104363528491008, // admin用户ID
                ExpenseTypeId = 1, // 差旅费
                TotalAmount = 2500.00m,
                ExpenseDate = DateTime.Now.AddDays(-5),
                ExpenseReason = "前往上海参加客户会议，包括交通费、住宿费、餐费等",
                PaymentMethod = 2, // 2=银行转账
                Status = 2, // 2=已批准
                ApproverId = 1958104363528491008, // admin用户ID
                ApproveTime = DateTime.Now.AddDays(-3),
                ApproveComment = "费用合理，同意报销",
                AttachmentCount = 3,
                Notes = "包含发票、行程单等附件",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-5),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-3)
            },
            new TaktExpense
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                ExpenseNo = "EXP20241201002",
                EmployeeId = 1958104363528491008, // admin用户ID
                ExpenseTypeId = 2, // 办公用品费
                TotalAmount = 800.00m,
                ExpenseDate = DateTime.Now.AddDays(-3),
                ExpenseReason = "购买办公用品，包括文具、打印纸、文件夹等",
                PaymentMethod = 1, // 1=现金
                Status = 1, // 1=待审批
                ApproverId = null,
                ApproveTime = null,
                ApproveComment = null,
                AttachmentCount = 1,
                Notes = "办公用品采购清单",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-3)
            },
            new TaktExpense
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                ExpenseNo = "EXP20241201003",
                EmployeeId = 1958104363528491008, // admin用户ID
                ExpenseTypeId = 3, // 培训费
                TotalAmount = 1500.00m,
                ExpenseDate = DateTime.Now.AddDays(-7),
                ExpenseReason = "参加技术培训课程，提升专业技能",
                PaymentMethod = 3, // 3=信用卡
                Status = 3, // 3=已拒绝
                ApproverId = 1958104363528491008, // admin用户ID
                ApproveTime = DateTime.Now.AddDays(-5),
                ApproveComment = "培训费用超出预算，建议选择其他培训方式",
                AttachmentCount = 2,
                Notes = "培训费用申请",
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-7),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-5)
            }
        };
    }

    /// <summary>
    /// 获取默认费用签拟单数据
    /// </summary>
    /// <returns>费用签拟单数据列表</returns>
    public List<TaktExpenseDraft> GetDefaultExpenseDrafts()
    {
        return new List<TaktExpenseDraft>
        {
            new TaktExpenseDraft
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                DocNo = "ED20241201001",
                ApplicantId = 1958104363528491008, // admin用户ID
                ApplicantCode = "EMP001",
                ApplicantName = "admin",
                DepartmentId = 1,
                DepartmentCode = "DEPT001",
                DepartmentName = "技术部",
                ApplyDate = DateTime.Now.AddDays(-2),
                ExpenseType = "差旅费",
                ExpenseSubType = "交通费",
                ExpenseAmount = 1800.00m,
                Currency = "CNY",
                ExchangeRate = 1.0000m,
                LocalAmount = 1800.00m,
                Purpose = "前往北京参加技术交流会议",
                CostCenterId = 1,
                CostCenterCode = "CC001",
                CostCenterName = "技术成本中心",
                CostElementId = 1,
                CostElementCode = "CE001",
                CostElementName = "差旅费成本要素",
                BudgetType = "年度预算",
                BudgetNo = "BUD2024001",
                BudgetId = 1,
                BudgetAmount = 50000.00m,
                UsedBudgetAmount = 15000.00m,
                RemainingBudgetAmount = 35000.00m,
                SupplierCode = "SUP001",
                SupplierName = "北京交通服务公司",
                ContractNumber = "CON2024001",
                InvoiceNumber = "INV20241201001",
                ExpectedPaymentDate = DateTime.Now.AddDays(7),
                ActualPaymentDate = null,
                PaymentStatus = 0, // 0=未付款
                ApprovalStatus = 1, // 1=已提交
                OrderNum = 1,
                Status = 0, // 0=正常
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new TaktExpenseDraft
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                DocNo = "ED20241201002",
                ApplicantId = 1958104363528491008, // admin用户ID
                ApplicantCode = "EMP001",
                ApplicantName = "admin",
                DepartmentId = 1,
                DepartmentCode = "DEPT001",
                DepartmentName = "技术部",
                ApplyDate = DateTime.Now.AddDays(-1),
                ExpenseType = "办公费",
                ExpenseSubType = "办公用品",
                ExpenseAmount = 2200.00m,
                Currency = "CNY",
                ExchangeRate = 1.0000m,
                LocalAmount = 2200.00m,
                Purpose = "购买办公用品，包括文具、打印纸等",
                CostCenterId = 1,
                CostCenterCode = "CC001",
                CostCenterName = "技术成本中心",
                CostElementId = 2,
                CostElementCode = "CE002",
                CostElementName = "办公费成本要素",
                BudgetType = "月度预算",
                BudgetNo = "BUD20241201",
                BudgetId = 2,
                BudgetAmount = 10000.00m,
                UsedBudgetAmount = 3000.00m,
                RemainingBudgetAmount = 7000.00m,
                SupplierCode = "SUP002",
                SupplierName = "办公用品供应商",
                ContractNumber = null,
                InvoiceNumber = "INV20241201002",
                ExpectedPaymentDate = DateTime.Now.AddDays(5),
                ActualPaymentDate = null,
                PaymentStatus = 0, // 0=未付款
                ApprovalStatus = 0, // 0=草稿
                OrderNum = 2,
                Status = 0, // 0=正常
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-1),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-1)
            },
            new TaktExpenseDraft
            {
                CompanyCode = "1000",
                PlantCode = "1000",
                DocNo = "ED20241201003",
                ApplicantId = 1958104363528491008, // admin用户ID
                ApplicantCode = "EMP001",
                ApplicantName = "admin",
                DepartmentId = 1,
                DepartmentCode = "DEPT001",
                DepartmentName = "技术部",
                ApplyDate = DateTime.Now.AddDays(-3),
                ExpenseType = "培训费",
                ExpenseSubType = "技术培训",
                ExpenseAmount = 3500.00m,
                Currency = "CNY",
                ExchangeRate = 1.0000m,
                LocalAmount = 3500.00m,
                Purpose = "参加云计算技术培训，提升团队技术水平",
                CostCenterId = 1,
                CostCenterCode = "CC001",
                CostCenterName = "技术成本中心",
                CostElementId = 3,
                CostElementCode = "CE003",
                CostElementName = "培训费成本要素",
                BudgetType = "季度预算",
                BudgetNo = "BUD2024Q401",
                BudgetId = 3,
                BudgetAmount = 20000.00m,
                UsedBudgetAmount = 8000.00m,
                RemainingBudgetAmount = 12000.00m,
                SupplierCode = "SUP003",
                SupplierName = "技术培训中心",
                ContractNumber = "CON2024002",
                InvoiceNumber = "INV20241201003",
                ExpectedPaymentDate = DateTime.Now.AddDays(3),
                ActualPaymentDate = DateTime.Now.AddDays(-1),
                PaymentStatus = 1, // 1=已付款
                ApprovalStatus = 2, // 2=已审批
                OrderNum = 3,
                Status = 0, // 0=正常
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-3),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-1)
            }
        };
    }
}


