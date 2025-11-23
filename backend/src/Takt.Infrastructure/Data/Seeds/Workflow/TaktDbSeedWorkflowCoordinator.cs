//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedWorkflowCoordinator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 工作流种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Workflow;
using Takt.Domain.Entities.HumanResource.Leave;
using Takt.Domain.Entities.Accounting.Controlling;

namespace Takt.Infrastructure.Data.Seeds.Workflow;

/// <summary>
/// 工作流种子数据协调器
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 基于简化工作流实体架构
/// </remarks>
public class TaktDbSeedWorkflowCoordinator
{
    // 统一状态常量 - 所有工作流实体使用相同的状态值
    private const int STATUS_DRAFT = 0;                  // 草稿
    private const int STATUS_RUNNING = 1;                // 运行中/已发布
    private const int STATUS_COMPLETED = 2;              // 已完成
    private const int STATUS_DISABLED = 3;               // 已停用

    // 工作流实例状态常量（保持向后兼容）
    private const int INSTANCE_STATUS_DRAFT = STATUS_DRAFT;
    private const int INSTANCE_STATUS_RUNNING = STATUS_RUNNING;
    private const int INSTANCE_STATUS_COMPLETED = STATUS_COMPLETED;
    private const int INSTANCE_STATUS_DISABLED = STATUS_DISABLED;

    // 表单状态常量（保持向后兼容）
    private const int FORM_STATUS_DRAFT = STATUS_DRAFT;
    private const int FORM_STATUS_PUBLISHED = STATUS_RUNNING;
    private const int FORM_STATUS_COMPLETED = STATUS_COMPLETED;
    private const int FORM_STATUS_DISABLED = STATUS_DISABLED;

    // 流程定义状态常量（保持向后兼容）
    private const int SCHEME_STATUS_DRAFT = STATUS_DRAFT;
    private const int SCHEME_STATUS_PUBLISHED = STATUS_RUNNING;
    private const int SCHEME_STATUS_COMPLETED = STATUS_COMPLETED;
    private const int SCHEME_STATUS_DISABLED = STATUS_DISABLED;

    // 操作类型常量
    private const int OPERATION_TYPE_SUBMIT = 1;         // 提交
    private const int OPERATION_TYPE_APPROVE = 2;        // 审批
    private const int OPERATION_TYPE_REJECT = 3;         // 驳回
    private const int OPERATION_TYPE_TRANSFER = 4;       // 转办
    private const int OPERATION_TYPE_TERMINATE = 5;      // 终止
    private const int OPERATION_TYPE_WITHDRAW = 6;       // 撤回

    // 流转类型常量
    private const int TRANSITION_TYPE_ENTER = 1;         // 进入
    private const int TRANSITION_TYPE_LEAVE = 2;         // 离开
    private const int TRANSITION_TYPE_EXECUTE = 3;       // 执行

    // 流转结果常量
    private const int TRANSITION_RESULT_SUCCESS = 1;     // 成功
    private const int TRANSITION_RESULT_FAILED = 2;      // 失败
    private const int TRANSITION_RESULT_SKIPPED = 3;     // 跳过

    // 节点类型常量
    private const int NODE_TYPE_START = 1;               // 开始节点
    private const int NODE_TYPE_USER_TASK = 2;           // 用户任务
    private const int NODE_TYPE_GATEWAY = 3;             // 网关
    private const int NODE_TYPE_END = 4;                 // 结束节点

    // 流转状态常量

    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktForm> FormRepository => _repositoryFactory.GetWorkflowRepository<TaktForm>();
    private ITaktRepository<TaktScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<TaktScheme>();
    private ITaktRepository<TaktInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<TaktInstance>();
    private ITaktRepository<TaktInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceTrans>();
    private ITaktRepository<TaktInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceOper>();
    private ITaktRepository<TaktLeave> LeaveRepository => _repositoryFactory.GetBusinessRepository<TaktLeave>();
    private ITaktRepository<TaktExpense> ExpenseRepository => _repositoryFactory.GetBusinessRepository<TaktExpense>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDbSeedWorkflowCoordinator(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有工作流全链路数据
    /// </summary>
    public async Task InitializeAllWorkflowsAsync()
    {
        try
        {
            _logger.Info("开始初始化所有工作流全链路数据...");

            // 使用字典缓存ID，类似菜单种子的方法
            var workflowNameToId = new Dictionary<string, long>();

            // 1. 初始化请假流程（只创建表单和流程定义，不创建实例）
            await InitializeLeaveWorkflowDefinitionsAsync(workflowNameToId);

            // 2. 初始化费用报销流程（只创建表单和流程定义，不创建实例）
            await InitializeExpenseWorkflowDefinitionsAsync(workflowNameToId);

            // 3. 创建具体的演示实例
            await CreateTwoWorkflowInstancesAsync(workflowNameToId);

            _logger.Info("所有工作流全链路数据初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化所有工作流失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化员工请假流程定义（只创建表单和流程定义）
    /// </summary>
    public async Task InitializeLeaveWorkflowDefinitionsAsync(Dictionary<string, long> workflowNameToId)
    {
        try
        {
            _logger.Info("开始初始化员工请假流程定义...");

            // 1. 插入请假申请表单定义
            var formId = await InsertLeaveFormDefinitionAsync();
            workflowNameToId["leave_form"] = formId;
            _logger.Info($"请假申请表单定义创建完成，FormId: {formId}");

            // 2. 插入请假流程定义
            var schemeId = await InsertLeaveSchemeAsync(formId);
            workflowNameToId["leave_workflow"] = schemeId;
            _logger.Info($"请假流程定义创建完成，SchemeId: {schemeId}");

            _logger.Info("员工请假流程定义初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化员工请假流程定义失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 初始化费用报销流程定义（只创建表单和流程定义）
    /// </summary>
    public async Task InitializeExpenseWorkflowDefinitionsAsync(Dictionary<string, long> workflowNameToId)
    {
        try
        {
            _logger.Info("开始初始化费用报销流程定义...");

            // 1. 插入费用报销表单定义
            var formId = await InsertExpenseFormDefinitionAsync();
            workflowNameToId["expense_form"] = formId;
            _logger.Info($"费用报销表单定义创建完成，FormId: {formId}");

            // 2. 插入费用报销流程定义
            var schemeId = await InsertExpenseSchemeAsync(formId);
            workflowNameToId["expense_workflow"] = schemeId;
            _logger.Info($"费用报销流程定义创建完成，SchemeId: {schemeId}");

            _logger.Info("费用报销流程定义初始化完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化费用报销流程定义失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 插入请假申请表单定义
    /// </summary>
    private async Task<long> InsertLeaveFormDefinitionAsync()
    {
        var form = new TaktForm
        {
            FormKey = "leave_form",
            FormName = "请假申请表单",
            FormCategory = 1, // 人事类
            FormType = 1, // 请假申请
            Version = "1.0",
            FormConfig = @"{
                ""rule"": [
                    {
                        ""type"": ""input"",
                        ""field"": ""LeaveNo"",
                        ""title"": ""请假编号"",
                        ""props"": {
                            ""type"": ""text"",
                            ""placeholder"": ""请输入请假编号"",
                            ""maxLength"": 20
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入请假编号"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""EmployeeId"",
                        ""title"": ""员工"",
                        ""props"": {
                            ""placeholder"": ""请选择员工"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择员工"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""LeaveTypeId"",
                        ""title"": ""请假类型"",
                        ""props"": {
                            ""placeholder"": ""请选择请假类型"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择请假类型"" }
                        ]
                    },
                    {
                        ""type"": ""datePicker"",
                        ""field"": ""StartTime"",
                        ""title"": ""请假开始时间"",
                        ""props"": {
                            ""type"": ""datetime"",
                            ""placeholder"": ""请选择开始时间"",
                            ""showTime"": true,
                            ""format"": ""YYYY-MM-DD HH:mm:ss""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择开始时间"" }
                        ]
                    },
                    {
                        ""type"": ""datePicker"",
                        ""field"": ""EndTime"",
                        ""title"": ""请假结束时间"",
                        ""props"": {
                            ""type"": ""datetime"",
                            ""placeholder"": ""请选择结束时间"",
                            ""showTime"": true,
                            ""format"": ""YYYY-MM-DD HH:mm:ss""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择结束时间"" }
                        ]
                    },
                    {
                        ""type"": ""number"",
                        ""field"": ""LeaveDays"",
                        ""title"": ""请假天数"",
                        ""props"": {
                            ""placeholder"": ""请输入请假天数"",
                            ""min"": 0,
                            ""max"": 365,
                            ""precision"": 2,
                            ""step"": 0.5
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入请假天数"" },
                            { ""type"": ""number"", ""min"": 0, ""message"": ""请假天数不能小于0"" }
                        ]
                    },
                    {
                        ""type"": ""textarea"",
                        ""field"": ""LeaveReason"",
                        ""title"": ""请假原因"",
                        ""props"": {
                            ""placeholder"": ""请输入请假原因"",
                            ""rows"": 4,
                            ""maxLength"": 500,
                            ""showCount"": true
                        },
                        ""validate"": [
                            { ""max"": 500, ""message"": ""请假原因不能超过500个字符"" }
                        ]
                    }
                ],
                ""option"": {
                    ""submitText"": ""提交申请"",
                    ""resetText"": ""重置表单"",
                    ""labelWidth"": 120,
                    ""labelPosition"": ""right"",
                    ""size"": ""default"",
                    ""disabled"": false,
                    ""hideRequiredMark"": false,
                    ""showMessage"": true,
                    ""inlineMessage"": false,
                    ""statusIcon"": false,
                    ""validateOnRuleChange"": true
                }
            }",
            BusinessTableName = "Takt_leave",
            Status = FORM_STATUS_PUBLISHED, // 已发布
            Notes = "员工请假申请表单，字段与TaktLeave实体完全一致，用于工作流审批后自动创建请假记录",
            CreateBy = "Takt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Takt365",
            UpdateTime = DateTime.Now
        };

        var existingForm = await FormRepository.GetFirstAsync(f => f.FormKey == form.FormKey);
        if (existingForm != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有请假表单定义，FormId: {existingForm.Id}");
            existingForm.FormName = form.FormName;
            existingForm.FormConfig = form.FormConfig;
            existingForm.Status = form.Status;
            existingForm.Notes = form.Notes;
            existingForm.UpdateBy = form.UpdateBy;
            existingForm.UpdateTime = form.UpdateTime;
            await FormRepository.UpdateAsync(existingForm);
            _logger.Info($"请假申请表单定义已更新: FormKey={form.FormKey}, FormId={existingForm.Id}");
            return existingForm.Id;
        }

        // 不存在就新增
        _logger.Info($"[DEBUG] 创建新的请假表单定义，FormKey: {form.FormKey}");
        await FormRepository.CreateAsync(form);
        _logger.Info($"请假申请表单定义已创建: FormKey={form.FormKey}, FormId={form.Id}");
        return form.Id;
    }

    /// <summary>
    /// 插入费用报销表单定义
    /// </summary>
    private async Task<long> InsertExpenseFormDefinitionAsync()
    {
        var form = new TaktForm
        {
            FormKey = "expense_form",
            FormName = "费用报销表单",
            FormCategory = 2, // 财务类
            FormType = 2, // 报销申请
            Version = "1.0",
            FormConfig = @"{
                ""rule"": [
                    {
                        ""type"": ""input"",
                        ""field"": ""ExpenseNo"",
                        ""title"": ""报销单号"",
                        ""props"": {
                            ""type"": ""text"",
                            ""placeholder"": ""请输入报销单号"",
                            ""maxLength"": 20
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入报销单号"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""EmployeeId"",
                        ""title"": ""申请人"",
                        ""props"": {
                            ""placeholder"": ""请选择申请人"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择申请人"" }
                        ]
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""ExpenseTypeId"",
                        ""title"": ""报销类型"",
                        ""props"": {
                            ""placeholder"": ""请选择报销类型"",
                            ""showSearch"": true,
                            ""filterOption"": true
                        },
                        ""options"": [
                            { ""label"": ""差旅费"", ""value"": 1 },
                            { ""label"": ""办公用品"", ""value"": 2 },
                            { ""label"": ""交通费"", ""value"": 3 },
                            { ""label"": ""通讯费"", ""value"": 4 },
                            { ""label"": ""其他"", ""value"": 5 }
                        ],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择报销类型"" }
                        ]
                    },
                    {
                        ""type"": ""number"",
                        ""field"": ""TotalAmount"",
                        ""title"": ""报销总金额"",
                        ""props"": {
                            ""placeholder"": ""请输入报销总金额"",
                            ""min"": 0,
                            ""max"": 999999.99,
                            ""precision"": 2,
                            ""step"": 0.01,
                            ""formatter"": ""value => `¥ ${value}`"",
                            ""parser"": ""value => value.replace('¥ ', '')""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入报销总金额"" },
                            { ""type"": ""number"", ""min"": 0.01, ""message"": ""报销金额必须大于0"" }
                        ]
                    },
                    {
                        ""type"": ""datePicker"",
                        ""field"": ""ExpenseDate"",
                        ""title"": ""报销日期"",
                        ""props"": {
                            ""type"": ""date"",
                            ""placeholder"": ""请选择报销日期"",
                            ""format"": ""YYYY-MM-DD""
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择报销日期"" }
                        ]
                    },
                    {
                        ""type"": ""textarea"",
                        ""field"": ""ExpenseReason"",
                        ""title"": ""报销事由"",
                        ""props"": {
                            ""placeholder"": ""请输入报销事由"",
                            ""rows"": 4,
                            ""maxLength"": 500,
                            ""showCount"": true
                        },
                        ""validate"": [
                            { ""required"": true, ""message"": ""请输入报销事由"" },
                            { ""max"": 500, ""message"": ""报销事由不能超过500个字符"" }
                        ]
                    },
                    {
                        ""type"": ""upload"",
                        ""field"": ""Attachments"",
                        ""title"": ""附件"",
                        ""props"": {
                            ""name"": ""file"",
                            ""multiple"": true,
                            ""accept"": "".jpg,.jpeg,.png,.pdf,.doc,.docx"",
                            ""listType"": ""text"",
                            ""maxCount"": 10
                        }
                    },
                    {
                        ""type"": ""select"",
                        ""field"": ""PaymentMethod"",
                        ""title"": ""付款方式"",
                        ""props"": {
                            ""placeholder"": ""请选择付款方式"",
                            ""showSearch"": true
                        },
                        ""options"": [
                            { ""label"": ""银行转账"", ""value"": 1 },
                            { ""label"": ""现金"", ""value"": 2 },
                            { ""label"": ""支票"", ""value"": 3 }
                        ],
                        ""validate"": [
                            { ""required"": true, ""message"": ""请选择付款方式"" }
                        ]
                    }
                ],
                ""option"": {
                    ""submitText"": ""提交报销"",
                    ""resetText"": ""重置表单"",
                    ""labelWidth"": 120,
                    ""labelPosition"": ""right"",
                    ""size"": ""default"",
                    ""disabled"": false,
                    ""hideRequiredMark"": false,
                    ""showMessage"": true,
                    ""inlineMessage"": false,
                    ""statusIcon"": false,
                    ""validateOnRuleChange"": true
                }
            }",
            BusinessTableName = "Takt_expense",
            Status = FORM_STATUS_PUBLISHED, // 已发布
            Notes = "费用报销申请表单，包含报销类型、金额、事由、附件等完整信息，用于工作流审批",
            CreateBy = "admin",
            CreateTime = DateTime.Now,
            UpdateBy = "admin",
            UpdateTime = DateTime.Now
        };

        var existingForm = await FormRepository.GetFirstAsync(f => f.FormKey == form.FormKey);
        if (existingForm != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有费用报销表单定义，FormId: {existingForm.Id}");
            existingForm.FormName = form.FormName;
            existingForm.FormConfig = form.FormConfig;
            existingForm.Status = form.Status;
            existingForm.Notes = form.Notes;
            existingForm.UpdateBy = form.UpdateBy;
            existingForm.UpdateTime = form.UpdateTime;
            await FormRepository.UpdateAsync(existingForm);
            _logger.Info($"费用报销表单定义已更新: FormKey={form.FormKey}, FormId={existingForm.Id}");
            return existingForm.Id;
        }

        // 不存在就新增
        _logger.Info($"[DEBUG] 创建新的费用报销表单定义，FormKey: {form.FormKey}");
        await FormRepository.CreateAsync(form);
        _logger.Info($"费用报销表单定义已创建: FormKey={form.FormKey}, FormId={form.Id}");
        return form.Id;
    }

    /// <summary>
    /// 插入请假流程定义
    /// </summary>
    private async Task<long> InsertLeaveSchemeAsync(long formId)
    {
        _logger.Info($"[DEBUG] InsertLeaveSchemeAsync 接收到的 formId: {formId}");
        
        var scheme = new TaktScheme
        {
            SchemeKey = "leave_workflow",
            SchemeName = "员工请假流程",
            Version = "1.0",
            FormId = formId,
            SchemeConfig = @"{
                ""nodes"": [
                    {""id"": ""start"", ""type"": ""start"", ""x"": 100, ""y"": 100, ""data"": {""nodeName"": ""开始""}},
                    {""id"": ""apply"", ""type"": ""task"", ""x"": 250, ""y"": 100, ""data"": {""nodeName"": ""员工申请""}},
                    {""id"": ""teamLeader"", ""type"": ""task"", ""x"": 400, ""y"": 100, ""data"": {""nodeName"": ""班长审批""}},
                    {""id"": ""sectionChief"", ""type"": ""task"", ""x"": 550, ""y"": 100, ""data"": {""nodeName"": ""课长审批""}},
                    {""id"": ""manager"", ""type"": ""task"", ""x"": 700, ""y"": 100, ""data"": {""nodeName"": ""经理审批""}},
                    {""id"": ""generalManager"", ""type"": ""task"", ""x"": 850, ""y"": 100, ""data"": {""nodeName"": ""总经理审批""}},
                    {""id"": ""end"", ""type"": ""end"", ""x"": 1000, ""y"": 100, ""data"": {""nodeName"": ""结束""}}
                ],
                ""edges"": [
                    {""id"": ""edge1"", ""source"": ""start"", ""target"": ""apply"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge2"", ""source"": ""apply"", ""target"": ""teamLeader"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge3"", ""source"": ""teamLeader"", ""target"": ""sectionChief"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge4"", ""source"": ""sectionChief"", ""target"": ""manager"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge5"", ""source"": ""manager"", ""target"": ""generalManager"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge6"", ""source"": ""generalManager"", ""target"": ""end"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""}
                ]
            }",
            Status = SCHEME_STATUS_PUBLISHED,
            Notes = "员工请假审批流程定义 - 根据请假类型决定审批路径（事假/病假需要总经理审批，年假/调休直接总务处理）",
            CreateBy = "Takt365",
            CreateTime = DateTime.Now,
            UpdateBy = "Takt365",
            UpdateTime = DateTime.Now
        };

        var existingScheme = await SchemeRepository.GetFirstAsync(d => d.SchemeKey == scheme.SchemeKey);
        if (existingScheme != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有请假流程定义，当前FormId: {existingScheme.FormId}, 新的FormId: {scheme.FormId}");
            existingScheme.SchemeName = scheme.SchemeName;
            existingScheme.FormId = scheme.FormId; // 确保FormId也被更新
            existingScheme.SchemeConfig = scheme.SchemeConfig;
            existingScheme.Status = scheme.Status;
            existingScheme.Notes = scheme.Notes;
            existingScheme.UpdateBy = scheme.UpdateBy;
            existingScheme.UpdateTime = scheme.UpdateTime;
            await SchemeRepository.UpdateAsync(existingScheme);
            _logger.Info($"请假流程定义已更新: SchemeKey={scheme.SchemeKey}, FormId={existingScheme.FormId}");
            return existingScheme.Id;
        }

        // 不存在就新增
        _logger.Info($"[DEBUG] 创建新的请假流程定义，FormId: {scheme.FormId}");
        await SchemeRepository.CreateAsync(scheme);
        _logger.Info($"请假流程定义已创建: SchemeKey={scheme.SchemeKey}, SchemeId={scheme.Id}, FormId={scheme.FormId}");
        return scheme.Id;
    }

    /// <summary>
    /// 插入费用报销流程定义
    /// </summary>
    private async Task<long> InsertExpenseSchemeAsync(long formId)
    {
        _logger.Info($"[DEBUG] InsertExpenseSchemeAsync 接收到的 formId: {formId}");
        
        var scheme = new TaktScheme
        {
            SchemeKey = "expense_workflow",
            SchemeName = "费用报销流程",
            Version = "1.0",
            FormId = formId,
            SchemeConfig = @"{
                ""nodes"": [
                    {""id"": ""start"", ""type"": ""start"", ""x"": 100, ""y"": 100, ""data"": {""nodeName"": ""开始""}},
                    {""id"": ""apply"", ""type"": ""task"", ""x"": 250, ""y"": 100, ""data"": {""nodeName"": ""员工申请""}},
                    {""id"": ""sectionChief"", ""type"": ""task"", ""x"": 400, ""y"": 100, ""data"": {""nodeName"": ""课长审批""}},
                    {""id"": ""manager"", ""type"": ""task"", ""x"": 550, ""y"": 100, ""data"": {""nodeName"": ""经理审批""}},
                    {""id"": ""generalManager"", ""type"": ""task"", ""x"": 700, ""y"": 100, ""data"": {""nodeName"": ""总经理审批""}},
                    {""id"": ""end"", ""type"": ""end"", ""x"": 850, ""y"": 100, ""data"": {""nodeName"": ""结束""}}
                ],
                ""edges"": [
                    {""id"": ""edge1"", ""source"": ""start"", ""target"": ""apply"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge2"", ""source"": ""apply"", ""target"": ""sectionChief"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge3"", ""source"": ""sectionChief"", ""target"": ""manager"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge4"", ""source"": ""manager"", ""target"": ""generalManager"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""},
                    {""id"": ""edge5"", ""source"": ""generalManager"", ""target"": ""end"", ""sourceAnchor"": ""right"", ""targetAnchor"": ""left""}
                ]
            }",
            Status = SCHEME_STATUS_PUBLISHED,
            Notes = "费用报销审批流程定义 - 课长审批 → 经理审批 → 总经理审批",
            CreateBy = "admin",
            CreateTime = DateTime.Now,
            UpdateBy = "admin",
            UpdateTime = DateTime.Now
        };

        var existingScheme = await SchemeRepository.GetFirstAsync(d => d.SchemeKey == scheme.SchemeKey);
        if (existingScheme != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有费用报销流程定义，当前FormId: {existingScheme.FormId}, 新的FormId: {scheme.FormId}");
            existingScheme.SchemeName = scheme.SchemeName;
            existingScheme.FormId = scheme.FormId; // 确保FormId也被更新
            existingScheme.SchemeConfig = scheme.SchemeConfig;
            existingScheme.Status = scheme.Status;
            existingScheme.Notes = scheme.Notes;
            existingScheme.UpdateBy = scheme.UpdateBy;
            existingScheme.UpdateTime = scheme.UpdateTime;
            await SchemeRepository.UpdateAsync(existingScheme);
            _logger.Info($"费用报销流程定义已更新: SchemeKey={scheme.SchemeKey}, FormId={existingScheme.FormId}");
            return existingScheme.Id;
        }

        // 不存在就新增
        _logger.Info($"[DEBUG] 创建新的费用报销流程定义，FormId: {scheme.FormId}");
        await SchemeRepository.CreateAsync(scheme);
        _logger.Info($"费用报销流程定义已创建: SchemeKey={scheme.SchemeKey}, SchemeId={scheme.Id}, FormId={scheme.FormId}");
        return scheme.Id;
    }



    /// <summary>
    /// 更新请假流程实例的表单数据
    /// </summary>
    private async Task InsertLeaveFormInstanceAsync(long instanceId, long leaveId, long formId)
    {
        var instance = await InstanceRepository.GetFirstAsync(x => x.Id == instanceId);
        if (instance != null)
        {
            // 更新实例的表单相关字段
            instance.FormId = formId; // 使用传入的表单定义ID
            instance.FormType = 0; // 动态表单
            instance.FormData = $@"{{
                ""LeaveId"": {leaveId},
                ""LeaveNo"": ""LEAVE20240120001"",
                ""EmployeeId"": 0,
                ""LeaveTypeId"": 1,
                ""StartTime"": ""2024-01-20 09:00:00"",
                ""EndTime"": ""2024-01-22 18:00:00"",
                ""LeaveDays"": 2.5,
                ""LeaveReason"": ""感冒发烧，需要休息调养""
            }}";
            instance.UpdateBy = "Takt365";
            instance.UpdateTime = DateTime.Now.AddDays(-2);

            await InstanceRepository.UpdateAsync(instance);
            _logger.Info($"请假流程实例表单数据已更新: InstanceId={instanceId}");
        }
    }

    /// <summary>
    /// 更新费用报销流程实例的表单数据
    /// </summary>
    private async Task InsertExpenseFormInstanceAsync(long instanceId, long expenseId, long formId)
    {
        var instance = await InstanceRepository.GetFirstAsync(x => x.Id == instanceId);
        if (instance != null)
        {
            // 更新实例的表单相关字段
            instance.FormId = formId; // 使用传入的表单定义ID
            instance.FormType = 0; // 动态表单
            instance.FormData = $@"{{
                ""ExpenseId"": {expenseId},
                ""ExpenseNo"": ""EXP20240320001"",
                ""EmployeeId"": 0,
                ""ExpenseTypeId"": 1,
                ""TotalAmount"": 2500.00,
                ""ExpenseDate"": ""2024-03-20"",
                ""ExpenseReason"": ""出差北京参加技术会议，包含交通费、住宿费、餐费等"",
                ""PaymentMethod"": 1,
                ""Attachments"": [
                    {{""name"": ""机票发票.jpg"", ""url"": ""/uploads/expense/20240320/机票发票.jpg""}},
                    {{""name"": ""酒店发票.pdf"", ""url"": ""/uploads/expense/20240320/酒店发票.pdf""}},
                    {{""name"": ""餐费发票.jpg"", ""url"": ""/uploads/expense/20240320/餐费发票.jpg""}}
                ]
            }}";
            instance.UpdateBy = "admin";
            instance.UpdateTime = DateTime.Now.AddHours(-1);

            await InstanceRepository.UpdateAsync(instance);
            _logger.Info($"费用报销流程实例表单数据已更新: InstanceId={instanceId}");
        }
    }

    /// <summary>
    /// 插入请假流转历史
    /// </summary>
    private async Task InsertLeaveTransitionsAsync(long instanceId)
    {
        _logger.Info($"[DEBUG] InsertLeaveTransitionsAsync 接收到的 instanceId: {instanceId}");
        
        var transitions = new[]
        {
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "start",
                StartNodeType = NODE_TYPE_START,
                StartNodeName = "开始",
                ToNodeId = "apply",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "员工申请",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-2),
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "apply",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "员工申请",
                ToNodeId = "deptManager",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "部门经理审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "deptManager",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "部门经理审批",
                ToNodeId = "condition",
                ToNodeType = NODE_TYPE_GATEWAY,
                ToNodeName = "请假类型判断",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-2).AddMinutes(3),
                CreateBy = "Takt365",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(3),
                UpdateBy = "Takt365",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(3)
            }
        };

        foreach (var transition in transitions)
        {
            _logger.Info($"[DEBUG] 查询请假流转记录: StartNodeName={transition.StartNodeName}, ToNodeName={transition.ToNodeName}");
            var existing = await TransRepository.GetFirstAsync(x =>
                x.StartNodeName == transition.StartNodeName &&
                x.ToNodeName == transition.ToNodeName);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有请假流转记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {transition.InstanceId}");
                existing.Status = transition.Status;
                existing.UpdateBy = transition.UpdateBy;
                existing.UpdateTime = transition.UpdateTime;
                await TransRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的请假流转记录，InstanceId: {transition.InstanceId}");
                await TransRepository.CreateAsync(transition);
            }
        }
        _logger.Info($"请假流转历史处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 插入费用报销流转历史
    /// </summary>
    private async Task InsertExpenseTransitionsAsync(long instanceId)
    {
        _logger.Info($"[DEBUG] InsertExpenseTransitionsAsync 接收到的 instanceId: {instanceId}");
        
        var transitions = new[]
        {
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "start",
                StartNodeType = NODE_TYPE_START,
                StartNodeName = "开始",
                ToNodeId = "apply",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "员工申请",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddHours(-1),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddHours(-1),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddHours(-1)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "apply",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "员工申请",
                ToNodeId = "sectionChief",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "课长审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddHours(-1).AddMinutes(5),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddHours(-1).AddMinutes(5),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddHours(-1).AddMinutes(5)
            }
        };

        foreach (var transition in transitions)
        {
            _logger.Info($"[DEBUG] 查询费用报销流转记录: StartNodeName={transition.StartNodeName}, ToNodeName={transition.ToNodeName}");
            var existing = await TransRepository.GetFirstAsync(x =>
                x.StartNodeName == transition.StartNodeName &&
                x.ToNodeName == transition.ToNodeName);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有费用报销流转记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {transition.InstanceId}");
                existing.Status = transition.Status;
                existing.UpdateBy = transition.UpdateBy;
                existing.UpdateTime = transition.UpdateTime;
                await TransRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的费用报销流转记录，InstanceId: {transition.InstanceId}");
                await TransRepository.CreateAsync(transition);
            }
        }
        _logger.Info($"费用报销流转历史处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 插入请假操作记录
    /// </summary>
    private async Task InsertLeaveOperationsAsync(long instanceId, long leaveId)
    {
        _logger.Info($"[DEBUG] InsertLeaveOperationsAsync 接收到的 instanceId: {instanceId}, leaveId: {leaveId}");
        
        var operations = new[]
        {
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "apply",
                NodeName = "员工申请",
                OperType = OPERATION_TYPE_SUBMIT,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "王五",
                OperOpinion = "请假事由：感冒发烧，需要休息调养",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""LeaveNo\"":\""LEAVE20240120001\"",\""EmployeeId\"":0,\""LeaveTypeId\"":1,\""StartTime\"":\""2024-01-20 09:00:00\"",\""EndTime\"":\""2024-01-22 18:00:00\"",\""LeaveDays\"":2.5,\""LeaveReason\"":\""感冒发烧，需要休息调养\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-2).AddMinutes(2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-2).AddMinutes(2)
            }
        };

        foreach (var operation in operations)
        {
            _logger.Info($"[DEBUG] 查询请假操作记录: NodeName={operation.NodeName}, OperatorName={operation.OperatorName}, OperType={operation.OperType}");
            var existing = await OperRepository.GetFirstAsync(x =>
                x.NodeName == operation.NodeName &&
                x.OperatorName == operation.OperatorName &&
                x.OperType == operation.OperType);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有请假操作记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {operation.InstanceId}");
                existing.NodeId = operation.NodeId;
                existing.NodeName = operation.NodeName;
                existing.OperType = operation.OperType;
                existing.OperatorId = operation.OperatorId;
                existing.OperatorName = operation.OperatorName;
                existing.OperOpinion = operation.OperOpinion;
                existing.OperData = operation.OperData;
                existing.Status = operation.Status;
                existing.UpdateBy = operation.UpdateBy;
                existing.UpdateTime = operation.UpdateTime;
                await OperRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的请假操作记录，InstanceId: {operation.InstanceId}");
                await OperRepository.CreateAsync(operation);
            }
        }
        _logger.Info($"请假操作记录处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 插入费用报销操作记录
    /// </summary>
    private async Task InsertExpenseOperationsAsync(long instanceId, long expenseId)
    {
        _logger.Info($"[DEBUG] InsertExpenseOperationsAsync 接收到的 instanceId: {instanceId}, expenseId: {expenseId}");
        
        var operations = new[]
        {
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "apply",
                NodeName = "员工申请",
                OperType = OPERATION_TYPE_SUBMIT,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "李四",
                OperOpinion = "出差北京参加技术会议，费用明细：机票1200元，酒店800元，餐费500元，总计2500元",
                OperData = $@"{{\""ExpenseId\"":{expenseId},\""ExpenseNo\"":\""EXP20240320002\"",\""EmployeeId\"":0,\""ExpenseTypeId\"":1,\""TotalAmount\"":2500.00,\""ExpenseDate\"":\""2024-03-20\"",\""ExpenseReason\"":\""出差北京参加技术会议，包含交通费、住宿费、餐费等\"",\""PaymentMethod\"":1}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddHours(-2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddHours(-2)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "sectionChief",
                NodeName = "课长审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "张课长",
                OperOpinion = "费用合理，同意报销",
                OperData = $@"{{\""ExpenseId\"":{expenseId},\""ApprovalResult\"":\""approved\"",\""ApprovalComment\"":\""费用合理，同意报销\"",\""ApprovalTime\"":\""{DateTime.Now.AddHours(-1):yyyy-MM-dd HH:mm:ss}\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddHours(-1),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddHours(-1)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "manager",
                NodeName = "经理审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "王经理",
                OperOpinion = "审批通过，可以报销",
                OperData = $@"{{\""ExpenseId\"":{expenseId},\""ApprovalResult\"":\""approved\"",\""ApprovalComment\"":\""审批通过，可以报销\"",\""ApprovalTime\"":\""{DateTime.Now.AddMinutes(-30):yyyy-MM-dd HH:mm:ss}\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddMinutes(-30),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddMinutes(-30)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "generalManager",
                NodeName = "总经理审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "李总经理",
                OperOpinion = "最终审批通过",
                OperData = $@"{{\""ExpenseId\"":{expenseId},\""ApprovalResult\"":\""approved\"",\""ApprovalComment\"":\""最终审批通过\"",\""ApprovalTime\"":\""{DateTime.Now.AddMinutes(-10):yyyy-MM-dd HH:mm:ss}\""}}",
                Status = STATUS_COMPLETED,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddMinutes(-10),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddMinutes(-10)
            }
        };

        foreach (var operation in operations)
        {
            _logger.Info($"[DEBUG] 查询费用报销操作记录: NodeName={operation.NodeName}, OperatorName={operation.OperatorName}, OperType={operation.OperType}");
            var existing = await OperRepository.GetFirstAsync(x =>
                x.NodeName == operation.NodeName &&
                x.OperatorName == operation.OperatorName &&
                x.OperType == operation.OperType);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有费用报销操作记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {operation.InstanceId}");
                existing.NodeId = operation.NodeId;
                existing.NodeName = operation.NodeName;
                existing.OperType = operation.OperType;
                existing.OperatorId = operation.OperatorId;
                existing.OperatorName = operation.OperatorName;
                existing.OperOpinion = operation.OperOpinion;
                existing.OperData = operation.OperData;
                existing.Status = operation.Status;
                existing.UpdateBy = operation.UpdateBy;
                existing.UpdateTime = operation.UpdateTime;
                await OperRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的费用报销操作记录，InstanceId: {operation.InstanceId}");
                await OperRepository.CreateAsync(operation);
            }
        }
        _logger.Info($"费用报销操作记录处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 创建两个流程实例用于演示（王五请假完整流程，费用报销启动状态）
    /// </summary>
    public async Task CreateTwoWorkflowInstancesAsync(Dictionary<string, long> workflowNameToId)
    {
        try
        {
            _logger.Info("开始创建两个工作流实例用于演示...");

            // 从字典中获取请假流程定义ID
            if (!workflowNameToId.TryGetValue("leave_workflow", out var leaveSchemeId))
            {
                _logger.Warn("未找到请假流程定义ID，请先运行 InitializeLeaveWorkflowDefinitionsAsync");
                return;
            }

            // 从字典中获取费用报销流程定义ID
            if (!workflowNameToId.TryGetValue("expense_workflow", out var expenseSchemeId))
            {
                _logger.Warn("未找到费用报销流程定义ID，请先运行 InitializeExpenseWorkflowDefinitionsAsync");
                return;
            }

            // 创建两个实例：王五请假完整流程，费用报销启动状态
            var leaveFormId = workflowNameToId["leave_form"];
            var expenseFormId = workflowNameToId["expense_form"];
            await CreateWangWuLeaveInstanceAsync(leaveSchemeId, leaveFormId);
            await CreateExpenseInstanceInRunningStatusAsync(expenseSchemeId, expenseFormId);

            _logger.Info("两个工作流实例创建完成！");
        }
        catch (Exception ex)
        {
            _logger.Error($"创建两个工作流实例失败: {ex.Message}", ex);
            throw;
        }
    }





    /// <summary>
    /// 创建费用报销运行中状态的实例
    /// </summary>
    private async Task CreateExpenseInstanceInRunningStatusAsync(long schemeId, long formId)
    {
        // 1. 先创建费用报销记录（运行中状态）
        var expenseRecord = new TaktExpense
        {
            CompanyCode = "1000",
            PlantCode = "1000",
            ExpenseNo = "EXP20240320002",
            EmployeeId = 0, // 让数据库自动生成雪花ID
            ExpenseTypeId = 1,
            TotalAmount = 2500.00m,
            ExpenseDate = new DateTime(2024, 3, 20),
            ExpenseReason = "出差北京参加技术会议，包含交通费、住宿费、餐费等",
            PaymentMethod = 1, // 现金
            Status = 1, // 待审批（业务状态，非工作流状态）
            AttachmentCount = 3,
            Notes = "包含机票、酒店、餐费发票",
            CreateBy = "admin",
            CreateTime = DateTime.Now.AddHours(-2),
            UpdateBy = "admin",
            UpdateTime = DateTime.Now.AddHours(-2)
        };

        var existingExpense = await ExpenseRepository.GetFirstAsync(x => x.ExpenseNo == expenseRecord.ExpenseNo);
        long expenseId;
        if (existingExpense != null)
        {
            // 存在就更新
            existingExpense.EmployeeId = expenseRecord.EmployeeId;
            existingExpense.ExpenseTypeId = expenseRecord.ExpenseTypeId;
            existingExpense.TotalAmount = expenseRecord.TotalAmount;
            existingExpense.ExpenseDate = expenseRecord.ExpenseDate;
            existingExpense.ExpenseReason = expenseRecord.ExpenseReason;
            existingExpense.PaymentMethod = expenseRecord.PaymentMethod;
            existingExpense.Status = expenseRecord.Status;
            existingExpense.AttachmentCount = expenseRecord.AttachmentCount;
            existingExpense.Notes = expenseRecord.Notes;
            existingExpense.UpdateBy = expenseRecord.UpdateBy;
            existingExpense.UpdateTime = expenseRecord.UpdateTime;
            await ExpenseRepository.UpdateAsync(existingExpense);
            expenseId = existingExpense.Id;
            _logger.Info($"费用报销运行中记录已更新: ExpenseNo={expenseRecord.ExpenseNo}, ExpenseId={expenseId}");
        }
        else
        {
            // 不存在就新增
            await ExpenseRepository.CreateAsync(expenseRecord);
            expenseId = expenseRecord.Id;
            _logger.Info($"费用报销运行中记录已创建: ExpenseNo={expenseRecord.ExpenseNo}, ExpenseId={expenseId}");
        }

        // 2. 创建工作流实例，引用费用报销记录ID
        var instance = new TaktInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "李四的费用报销申请（执行中）",
            BusinessKey = "EXPENSE-2024-002",
            Status = INSTANCE_STATUS_RUNNING,
            StartTime = DateTime.Now.AddHours(-2),
            EndTime = null,
            InitiatorId = 0, // 让数据库自动生成雪花ID
            CurrentNodeId = "sectionChief",
            CurrentNodeName = "课长审批",
            Variables = $@"{{\""ExpenseId\"":{expenseId},\""ExpenseNo\"":\""EXP20240320002\"",\""EmployeeId\"":0,\""ExpenseTypeId\"":1,\""TotalAmount\"":2500.00,\""ExpenseDate\"":\""2024-03-20\"",\""ExpenseReason\"":\""出差北京参加技术会议，包含交通费、住宿费、餐费等\"",\""PaymentMethod\"":1,\""Status\"":1}}",
            CreateBy = "admin",
            CreateTime = DateTime.Now.AddHours(-2),
            UpdateBy = "admin",
            UpdateTime = DateTime.Now.AddHours(-2)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        long instanceId;
        if (existingInstance != null)
        {
            // 存在就更新
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.Status = instance.Status;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            instanceId = existingInstance.Id;
            _logger.Info($"费用报销运行中状态实例已更新: {instance.BusinessKey}, InstanceId={instanceId}");
        }
        else
        {
            // 不存在就新增
            await InstanceRepository.CreateAsync(instance);
            instanceId = instance.Id;
            _logger.Info($"费用报销运行中状态实例已创建: {instance.BusinessKey}, InstanceId={instanceId}");
        }

        // 使用获取到的实例ID创建相关数据（无论实例是新创建还是已存在）
        await InsertExpenseFormInstanceAsync(instanceId, expenseId, formId);
        await InsertExpenseTransitionsAsync(instanceId);
        await InsertExpenseOperationsAsync(instanceId, expenseId);
    }

    /// <summary>
    /// 创建王五的完整请假流程（从开始到结束）
    /// </summary>
    private async Task CreateWangWuLeaveInstanceAsync(long schemeId, long formId)
    {
        _logger.Info($"[DEBUG] CreateWangWuLeaveInstanceAsync 接收到的 schemeId: {schemeId}");
        
        // 1. 先创建王五的请假记录（已完成状态）
        var leaveRecord = new TaktLeave
        {
            CompanyCode = "1000",
            PlantCode = "1000",
            LeaveNo = "LEAVE20240320001",
            EmployeeId = 0, // 让数据库自动生成雪花ID
            LeaveTypeId = 1, // 病假
            StartTime = new DateTime(2024, 3, 20, 9, 0, 0),
            EndTime = new DateTime(2024, 3, 22, 18, 0, 0),
            LeaveDays = 2.5m,
            LeaveReason = "感冒发烧，需要休息调养",
            Status = 2, // 已批准（业务状态，非工作流状态）
            ApproverId = 0, // 让数据库自动生成雪花ID
            ApproveTime = DateTime.Now.AddDays(-1),
            ApproveComment = "同意请假申请，注意休息",
            CreateBy = "admin",
            CreateTime = DateTime.Now.AddDays(-3),
            UpdateBy = "admin",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingLeave = await LeaveRepository.GetFirstAsync(x => x.LeaveNo == leaveRecord.LeaveNo);
        long leaveId;
        if (existingLeave != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有王五请假记录，当前LeaveId: {existingLeave.Id}");
            existingLeave.CompanyCode = leaveRecord.CompanyCode;
            existingLeave.PlantCode = leaveRecord.PlantCode;
            existingLeave.EmployeeId = leaveRecord.EmployeeId;
            existingLeave.LeaveTypeId = leaveRecord.LeaveTypeId;
            existingLeave.StartTime = leaveRecord.StartTime;
            existingLeave.EndTime = leaveRecord.EndTime;
            existingLeave.LeaveDays = leaveRecord.LeaveDays;
            existingLeave.LeaveReason = leaveRecord.LeaveReason;
            existingLeave.Status = leaveRecord.Status;
            existingLeave.ApproverId = leaveRecord.ApproverId;
            existingLeave.ApproveTime = leaveRecord.ApproveTime;
            existingLeave.ApproveComment = leaveRecord.ApproveComment;
            existingLeave.UpdateBy = leaveRecord.UpdateBy;
            existingLeave.UpdateTime = leaveRecord.UpdateTime;
            await LeaveRepository.UpdateAsync(existingLeave);
            leaveId = existingLeave.Id;
            _logger.Info($"王五请假记录已更新: LeaveNo={leaveRecord.LeaveNo}, LeaveId={leaveId}");
        }
        else
        {
            // 不存在就新增
            await LeaveRepository.CreateAsync(leaveRecord);
            leaveId = leaveRecord.Id;
            _logger.Info($"王五请假记录已创建: LeaveNo={leaveRecord.LeaveNo}, LeaveId={leaveId}");
        }

        // 2. 创建工作流实例（已完成状态）
        var instance = new TaktInstance
        {
            SchemeId = schemeId,
            InstanceTitle = "王五的请假申请（已完成）",
            BusinessKey = "LEAVE-WANGWU-2024-001",
            Status = INSTANCE_STATUS_COMPLETED,
            StartTime = DateTime.Now.AddDays(-3),
            EndTime = DateTime.Now.AddDays(-1),
            InitiatorId = 0, // 让数据库自动生成雪花ID
            CurrentNodeId = "end",
            CurrentNodeName = "结束",
            Variables = $@"{{
                ""LeaveId"": {leaveId},
                ""LeaveNo"": ""LEAVE20240320001"",
                ""EmployeeId"": 0,
                ""LeaveTypeId"": 1,
                ""StartTime"": ""2024-03-20 09:00:00"",
                ""EndTime"": ""2024-03-22 18:00:00"",
                ""LeaveDays"": 2.5,
                ""LeaveReason"": ""感冒发烧，需要休息调养"",
                ""Status"": 2,
                ""ApproveComment"": ""同意请假申请，注意休息""
            }}",
            CreateBy = "admin",
            CreateTime = DateTime.Now.AddDays(-3),
            UpdateBy = "admin",
            UpdateTime = DateTime.Now.AddDays(-1)
        };

        var existingInstance = await InstanceRepository.GetFirstAsync(x => x.BusinessKey == instance.BusinessKey);
        long instanceId;
        if (existingInstance != null)
        {
            // 存在就更新
            _logger.Info($"[DEBUG] 找到现有王五请假流程实例，当前InstanceId: {existingInstance.Id}, 当前SchemeId: {existingInstance.SchemeId}, 新的SchemeId: {instance.SchemeId}");
            existingInstance.InstanceTitle = instance.InstanceTitle;
            existingInstance.SchemeId = instance.SchemeId; // 确保SchemeId也被更新
            existingInstance.Status = instance.Status;
            existingInstance.StartTime = instance.StartTime;
            existingInstance.EndTime = instance.EndTime;
            existingInstance.CurrentNodeId = instance.CurrentNodeId;
            existingInstance.CurrentNodeName = instance.CurrentNodeName;
            existingInstance.Variables = instance.Variables;
            existingInstance.UpdateBy = instance.UpdateBy;
            existingInstance.UpdateTime = instance.UpdateTime;
            await InstanceRepository.UpdateAsync(existingInstance);
            instanceId = existingInstance.Id;
            _logger.Info($"王五请假流程实例已更新: BusinessKey={instance.BusinessKey}, InstanceId={instanceId}, SchemeId={existingInstance.SchemeId}");
        }
        else
        {
            // 不存在就新增
            _logger.Info($"[DEBUG] 创建新的王五请假流程实例，SchemeId: {instance.SchemeId}");
            await InstanceRepository.CreateAsync(instance);
            instanceId = instance.Id;
            _logger.Info($"王五请假流程实例已创建: BusinessKey={instance.BusinessKey}, InstanceId={instanceId}, SchemeId={instance.SchemeId}");
        }

        // 3. 创建完整的流转历史（从开始到结束）
        await CreateWangWuLeaveTransitionsAsync(instanceId);
        
        // 4. 创建完整的操作记录
        await CreateWangWuLeaveOperationsAsync(instanceId, leaveId);
        
        // 5. 创建表单实例
        await InsertLeaveFormInstanceAsync(instanceId, leaveId, formId);
    }

    /// <summary>
    /// 创建王五请假流程的完整流转历史
    /// </summary>
    private async Task CreateWangWuLeaveTransitionsAsync(long instanceId)
    {
        _logger.Info($"[DEBUG] CreateWangWuLeaveTransitionsAsync 接收到的 instanceId: {instanceId}");
        
        var transitions = new[]
        {
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "start",
                StartNodeType = NODE_TYPE_START,
                StartNodeName = "开始",
                ToNodeId = "apply",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "员工申请",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-3),
            CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-3),
            UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-3)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "apply",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "员工申请",
                ToNodeId = "teamLeader",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "班长审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-3).AddMinutes(5),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(5),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(5)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "teamLeader",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "班长审批",
                ToNodeId = "sectionChief",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "课长审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-3).AddMinutes(10),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(10),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(10)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "sectionChief",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "课长审批",
                ToNodeId = "manager",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "经理审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-2),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "manager",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "经理审批",
                ToNodeId = "generalManager",
                ToNodeType = NODE_TYPE_USER_TASK,
                ToNodeName = "总经理审批",
                Status = STATUS_DRAFT,
                TransTime = DateTime.Now.AddDays(-1).AddHours(-2),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-1).AddHours(-2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-1).AddHours(-2)
            },
            new TaktInstanceTrans
            {
                InstanceId = instanceId,
                StartNodeId = "generalManager",
                StartNodeType = NODE_TYPE_USER_TASK,
                StartNodeName = "总经理审批",
                ToNodeId = "end",
                ToNodeType = NODE_TYPE_END,
                ToNodeName = "结束",
                Status = STATUS_RUNNING,
                TransTime = DateTime.Now.AddDays(-1),
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-1),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-1)
            }
        };

        foreach (var transition in transitions)
        {
            _logger.Info($"[DEBUG] 查询王五请假流转记录: StartNodeName={transition.StartNodeName}, ToNodeName={transition.ToNodeName}");
            var existing = await TransRepository.GetFirstAsync(x =>
                x.StartNodeName == transition.StartNodeName &&
                x.ToNodeName == transition.ToNodeName);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有王五请假流转记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {transition.InstanceId}");
                existing.StartNodeId = transition.StartNodeId;
                existing.StartNodeType = transition.StartNodeType;
                existing.StartNodeName = transition.StartNodeName;
                existing.ToNodeId = transition.ToNodeId;
                existing.ToNodeType = transition.ToNodeType;
                existing.ToNodeName = transition.ToNodeName;
                existing.Status = transition.Status;
                existing.TransTime = transition.TransTime;
                existing.UpdateBy = transition.UpdateBy;
                existing.UpdateTime = transition.UpdateTime;
                await TransRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的王五请假流转记录，InstanceId: {transition.InstanceId}");
                await TransRepository.CreateAsync(transition);
            }
        }
        _logger.Info($"王五请假流转历史处理完成: InstanceId={instanceId}");
    }

    /// <summary>
    /// 创建王五请假流程的完整操作记录
    /// </summary>
    private async Task CreateWangWuLeaveOperationsAsync(long instanceId, long leaveId)
    {
        _logger.Info($"[DEBUG] CreateWangWuLeaveOperationsAsync 接收到的 instanceId: {instanceId}, leaveId: {leaveId}");
        
        var operations = new[]
        {
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "apply",
                NodeName = "员工申请",
                OperType = OPERATION_TYPE_SUBMIT,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "王五",
                OperOpinion = "请假事由：感冒发烧，需要休息调养",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""LeaveNo\"":\""LEAVE20240320001\"",\""EmployeeId\"":0,\""LeaveTypeId\"":1,\""StartTime\"":\""2024-03-20 09:00:00\"",\""EndTime\"":\""2024-03-22 18:00:00\"",\""LeaveDays\"":2.5,\""LeaveReason\"":\""感冒发烧，需要休息调养\""}}",
                Status = STATUS_RUNNING,
            CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(5),
            UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(5)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "teamLeader",
                NodeName = "班长审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "班长",
                OperOpinion = "同意请假申请，注意休息",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""ApproveResult\"":\""approved\"",\""ApproveComment\"":\""同意请假申请，注意休息\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-3).AddMinutes(10),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-3).AddMinutes(10)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "sectionChief",
                NodeName = "课长审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "课长",
                OperOpinion = "同意请假申请，注意身体",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""ApproveResult\"":\""approved\"",\""ApproveComment\"":\""同意请假申请，注意身体\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-2)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "manager",
                NodeName = "经理审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "经理",
                OperOpinion = "同意请假申请，早日康复",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""ApproveResult\"":\""approved\"",\""ApproveComment\"":\""同意请假申请，早日康复\""}}",
                Status = STATUS_RUNNING,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-1).AddHours(-2),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-1).AddHours(-2)
            },
            new TaktInstanceOper
            {
                InstanceId = instanceId,
                NodeId = "generalManager",
                NodeName = "总经理审批",
                OperType = OPERATION_TYPE_APPROVE,
                OperatorId = 0, // 让数据库自动生成雪花ID
                OperatorName = "总经理",
                OperOpinion = "最终批准，流程结束",
                OperData = $@"{{\""LeaveId\"":{leaveId},\""ApproveResult\"":\""approved\"",\""ApproveComment\"":\""最终批准，流程结束\""}}",
                Status = STATUS_COMPLETED,
                CreateBy = "admin",
                CreateTime = DateTime.Now.AddDays(-1),
                UpdateBy = "admin",
                UpdateTime = DateTime.Now.AddDays(-1)
            }
        };

        foreach (var operation in operations)
        {
            _logger.Info($"[DEBUG] 查询王五请假操作记录: NodeName={operation.NodeName}, OperatorName={operation.OperatorName}, OperType={operation.OperType}");
            var existing = await OperRepository.GetFirstAsync(x =>
                x.NodeName == operation.NodeName &&
                x.OperatorName == operation.OperatorName &&
                x.OperType == operation.OperType);

            if (existing != null)
            {
                // 存在就更新
                _logger.Info($"[DEBUG] 找到现有王五请假操作记录，当前InstanceId: {existing.InstanceId}, 新的InstanceId: {operation.InstanceId}");
                existing.NodeId = operation.NodeId;
                existing.NodeName = operation.NodeName;
                existing.OperType = operation.OperType;
                existing.OperatorId = operation.OperatorId;
                existing.OperatorName = operation.OperatorName;
                existing.OperOpinion = operation.OperOpinion;
                existing.OperData = operation.OperData;
                existing.Status = operation.Status;
                existing.UpdateBy = operation.UpdateBy;
                existing.UpdateTime = operation.UpdateTime;
                await OperRepository.UpdateAsync(existing);
            }
            else
            {
                // 不存在就新增
                _logger.Info($"[DEBUG] 创建新的王五请假操作记录，InstanceId: {operation.InstanceId}");
                await OperRepository.CreateAsync(operation);
            }
        }
        _logger.Info($"王五请假操作记录处理完成: InstanceId={instanceId}");
    }

}




