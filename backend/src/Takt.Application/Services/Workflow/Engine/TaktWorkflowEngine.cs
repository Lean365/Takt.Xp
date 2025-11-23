#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎实现 - 基于简化后的实体结构
//===================================================================

using Takt.Application.Services.Workflow.Engine.Expressions;
using Takt.Application.Services.Workflow.Engine.Runtime;
using Takt.Application.Services.Workflow.Engine.Resolvers;
using Takt.Domain.IServices.SignalR;
using Takt.Domain.Models.Workflow;
using Takt.Shared.Constants;
using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Workflow.Engine
{
  /// <summary>
  /// 工作流引擎实现 - 基于OpenAuth.Net标准
  /// </summary>
  /// <remarks>
  /// 创建者: Claude
  /// 创建时间: 2024-12-01
  /// 功能说明:
  /// 1. 支持BPMN2.0标准的工作流定义和执行
  /// 2. 支持无业务关联流程（请假、报销等）
  /// 3. 支持有业务关联流程（采购、销售等）
  /// 4. 支持三种表单类型：动态表单、URL表单、自定义表单
  /// 5. 支持工作流审批和流转
  /// 6. 实现工作流变量的管理
  /// 7. 提供工作流状态查询和历史记录
  /// 基于OpenAuth.Net标准实现
  /// </remarks>
  public class TaktWorkflowEngine : ITaktWorkflowEngine
  {

    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    /// <summary>
    /// 数据库上下文
    /// </summary>
    private readonly ITaktDbContext _dbContext;

    /// <summary>
    /// 日志
    /// </summary>
    private readonly ITaktLogger _logger;

    /// <summary>
    /// 当前用户
    /// </summary>
    private readonly ITaktCurrentUser _currentUser;

    /// <summary>
    /// 表达式引擎
    /// </summary>
    private readonly ITaktExpressionEngine _expressionEngine;

    /// <summary>
    /// SignalR通知服务
    /// </summary>
    private readonly ITaktSignalRNotificationService _signalRNotificationService;

    /// <summary>
    /// 审批人解析器
    /// </summary>
    private readonly ITaktApproverResolver _approverResolver;

    /// <summary>
    /// 获取实例仓储
    /// </summary>
    private ITaktRepository<TaktInstance> InstanceRepository => _repositoryFactory.GetWorkflowRepository<TaktInstance>();

    /// <summary>
    /// 获取方案仓储
    /// </summary>
    private ITaktRepository<TaktScheme> SchemeRepository => _repositoryFactory.GetWorkflowRepository<TaktScheme>();

    /// <summary>
    /// 获取操作记录仓储
    /// </summary>
    private ITaktRepository<TaktInstanceOper> OperRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceOper>();

    /// <summary>
    /// 获取流转历史仓储
    /// </summary>
    private ITaktRepository<TaktInstanceTrans> TransRepository => _repositoryFactory.GetWorkflowRepository<TaktInstanceTrans>();



    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="expressionEngine">表达式引擎</param>
    /// <param name="signalRNotificationService">SignalR通知服务</param>
    /// <param name="approverResolver">审批人解析器</param>
    public TaktWorkflowEngine(
        ITaktDbContext dbContext,
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        ITaktCurrentUser currentUser,
        ITaktExpressionEngine expressionEngine,
        ITaktSignalRNotificationService signalRNotificationService,
        ITaktApproverResolver approverResolver)
    {
      _dbContext = dbContext;
      _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
      _logger = logger;
      _currentUser = currentUser;
      _expressionEngine = expressionEngine ?? throw new ArgumentNullException(nameof(expressionEngine));
      _signalRNotificationService = signalRNotificationService ?? throw new ArgumentNullException(nameof(signalRNotificationService));
      _approverResolver = approverResolver ?? throw new ArgumentNullException(nameof(approverResolver));
    }

    /// <summary>
    /// 启动工作流实例
    /// </summary>
    /// <param name="dto">工作流启动信息</param>
    /// <returns>新创建的工作流实例ID</returns>
    /// <exception cref="InvalidOperationException">当工作流方案不存在、未发布或配置无效时抛出</exception>
    public async Task<long> StartAsync(TaktWorkflowStartDto dto)
    {
      _dbContext.BeginTran();
      try
      {
        // 获取工作流方案
        var scheme = await SchemeRepository.GetByIdAsync(dto.SchemeId);
        if (scheme == null)
        {
          throw new InvalidOperationException("工作流方案不存在");
        }

        if (scheme.Status != 1) // 1 表示已发布
        {
          throw new InvalidOperationException("工作流方案未发布，无法启动");
        }

        // 解析工作流配置
        var workflowConfig = JsonConvert.DeserializeObject<TaktWorkflowConfigModel>(scheme.SchemeConfig);
        if (workflowConfig == null || !workflowConfig.Nodes.Any())
        {
          throw new InvalidOperationException("工作流配置无效");
        }

        // 获取开始节点
        var startNode = workflowConfig.Nodes.FirstOrDefault(x => x.Type == "start");
        if (startNode == null)
        {
          throw new InvalidOperationException("工作流配置中未找到开始节点");
        }

        // 创建工作流实例
        var instance = new TaktInstance
        {
          SchemeId = dto.SchemeId,
          InstanceTitle = dto.InstanceTitle,
          BusinessKey = dto.BusinessKey ?? Guid.NewGuid().ToString(),
          InitiatorId = dto.InitiatorId,
          CurrentNodeId = startNode.Id,
          CurrentNodeName = startNode.Name,
          Status = 1, // 1 表示运行中
          StartTime = DateTime.Now,
          Variables = dto.Variables,
          FormData = dto.Variables != null ? JsonConvert.SerializeObject(dto.Variables) : "{}",
          SchemeConfig = scheme.SchemeConfig // 保存配置以便后续使用
        };

        await InstanceRepository.CreateAsync(instance);

        // 使用 TaktFlowRuntime 初始化流程
        var flowRuntime = new TaktFlowRuntime(instance, workflowConfig);

        // 获取下一个节点的审批人（如果存在）
        string? nextMakerList = null;
        if (flowRuntime.NextNode != null && flowRuntime.NextNodeId != "-1")
        {
          try
          {
            nextMakerList = await flowRuntime.GetNodeMarkersAsync(
              flowRuntime.NextNode,
              _approverResolver,
              dto.InitiatorId);
          }
          catch (Exception ex)
          {
            _logger.Warn($"获取下一个节点审批人失败: {ex.Message}");
          }
        }

        // 注意：TaktInstance 没有 MakerList 属性，审批人信息存储在 TaktApprover 表中
        // 如果需要，可以在这里创建 TaktApprover 记录

        // 记录工作流启动历史
        await CreateTransRecordAsync(instance.Id, "", startNode.Id, startNode.Name, 1, "工作流启动");

        // 记录开始节点操作
        await CreateOperRecordAsync(instance.Id, startNode.Id, startNode.Name, 1, dto.InitiatorId, "工作流启动", null);

        _dbContext.CommitTran();
        _logger.Info($"工作流实例启动成功，ID: {instance.Id}, 方案: {scheme.SchemeName}");

        // 发送工作流启动通知
        await _signalRNotificationService.SendWorkflowStartedAsync(
            instance.Id,
            instance.InstanceTitle,
            _currentUser.UserName,
            new List<long> { dto.InitiatorId });

        return instance.Id;
      }
      catch (Exception ex)
      {
        _dbContext.RollbackTran();
        _logger.Error($"工作流实例启动失败: {ex.Message}", ex);
        throw;
      }
    }

    /// <summary>
    /// 暂停工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="reason">暂停原因</param>
    /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许暂停时抛出</exception>
    public async Task SuspendAsync(long instanceId, string reason = "手动暂停")
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      if (instance.Status != 1) // 1 表示运行中
      {
        throw new InvalidOperationException("只有运行中的工作流实例才能暂停");
      }

      instance.Status = 3; // 3 表示已暂停
      await InstanceRepository.UpdateAsync(instance);

      // 记录工作流暂停历史
      await CreateTransRecordAsync(instanceId, instance.CurrentNodeId ?? "", "", "", 3, reason);
      await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 6, _currentUser.UserId, reason, null);
    }

    /// <summary>
    /// 恢复工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许恢复时抛出</exception>
    public async Task ResumeAsync(long instanceId)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      if (instance.Status != 3) // 3 表示已暂停
      {
        throw new InvalidOperationException("只有已暂停的工作流实例才能恢复");
      }

      instance.Status = 1; // 1 表示运行中
      await InstanceRepository.UpdateAsync(instance);

      // 记录工作流恢复历史
      await CreateTransRecordAsync(instanceId, "", instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 1, "工作流恢复");
      await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 7, _currentUser.UserId, "工作流恢复", null);
    }

    /// <summary>
    /// 终止工作流实例
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="reason">终止原因</param>
    /// <exception cref="InvalidOperationException">当工作流实例不存在或状态不允许终止时抛出</exception>
    public async Task TerminateAsync(long instanceId, string reason)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      if (instance.Status != 1 && instance.Status != 3) // 1 表示运行中, 3 表示已暂停
      {
        throw new InvalidOperationException("只有运行中或已暂停的工作流实例才能终止");
      }

      instance.Status = 4; // 4 表示已终止
      instance.EndTime = DateTime.Now;
      await InstanceRepository.UpdateAsync(instance);

      // 记录工作流终止历史
      await CreateTransRecordAsync(instanceId, instance.CurrentNodeId ?? "", "", "", 4, reason);
      await CreateOperRecordAsync(instanceId, instance.CurrentNodeId ?? "", instance.CurrentNodeName ?? "", 8, _currentUser.UserId, reason, null);
    }

    /// <summary>
    /// 工作流审批 - 支持OpenAuth.Net标准的审批操作
    /// </summary>
    /// <param name="dto">审批信息</param>
    /// <returns>审批是否成功</returns>
    /// <exception cref="InvalidOperationException">当工作流实例不存在、状态不允许审批或操作类型不支持时抛出</exception>
    public async Task<bool> ApproveAsync(TaktWorkflowApproveDto dto)
    {
      _dbContext.BeginTran();
      try
      {
        var instance = await InstanceRepository.GetByIdAsync(dto.InstanceId);
        if (instance == null)
        {
          throw new InvalidOperationException("工作流实例不存在");
        }

        if (instance.Status != 1) // 1 表示运行中
        {
          throw new InvalidOperationException("只有运行中的工作流实例才能审批");
        }

        if (instance.CurrentNodeId != dto.NodeId)
        {
          throw new InvalidOperationException("当前节点不匹配");
        }

        // 获取工作流方案
        var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
        if (scheme == null)
        {
          throw new InvalidOperationException("工作流方案不存在");
        }

        // 解析工作流配置
        var workflowConfig = JsonConvert.DeserializeObject<TaktWorkflowConfigModel>(scheme.SchemeConfig);
        if (workflowConfig == null)
        {
          throw new InvalidOperationException("工作流配置无效");
        }

        // 如果提供了表单数据，更新实例的表单数据
        if (!string.IsNullOrEmpty(dto.FormData))
        {
          instance.FormData = dto.FormData;
        }

        // 记录操作
        await CreateOperRecordAsync(dto.InstanceId, dto.NodeId, instance.CurrentNodeName ?? "", dto.OperType, _currentUser.UserId, dto.OperOpinion, dto.OperData);

        // 根据操作类型处理 - 支持OpenAuth.Net标准的审批操作
        switch (dto.OperType)
        {
          case 1: // 同意
            await ProcessApproveAsync(instance, workflowConfig, dto);
            break;
          case 2: // 拒绝
            await ProcessRejectAsync(instance, dto);
            break;
          case 3: // 退回
            await ProcessReturnAsync(instance, workflowConfig, dto);
            break;
          case 4: // 转办
            await ProcessTransferAsync(instance, dto);
            break;
          case 5: // 委托
            await ProcessDelegateAsync(instance, dto);
            break;
          case 6: // 加签
            await ProcessAddSignAsync(instance, dto);
            break;
          case 7: // 知会
            await ProcessNotifyAsync(instance, dto);
            break;
          case 8: // 撤回
            await ProcessWithdrawAsync(instance, dto);
            break;
          default:
            throw new InvalidOperationException("不支持的操作类型");
        }

        _dbContext.CommitTran();
        return true;
      }
      catch (Exception ex)
      {
        _dbContext.RollbackTran();
        _logger.Error($"工作流审批失败: {ex.Message}", ex);
        throw;
      }
    }

    /// <summary>
    /// 获取工作流实例状态
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>工作流实例状态信息</returns>
    /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
    public async Task<TaktInstanceStatusDto> GetStatusAsync(long instanceId)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      return new TaktInstanceStatusDto
      {
        InstanceId = instance.Id,
        Status = instance.Status
      };
    }

    /// <summary>
    /// 获取可用的流转选项
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>可用的流转选项列表</returns>
    /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
    public async Task<List<TaktInstanceTransDto>> GetAvailableTransitionsAsync(long instanceId)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      if (instance.Status != 1) // 1 表示运行中
      {
        return new List<TaktInstanceTransDto>();
      }

      // 使用 TaktFlowRuntime 获取可用转换
      var flowRuntime = new TaktFlowRuntime(instance);
      var transitions = new List<TaktInstanceTransDto>();

      if (!string.IsNullOrEmpty(instance.CurrentNodeId) &&
          flowRuntime.FromNodeLines.ContainsKey(instance.CurrentNodeId))
      {
        foreach (var line in flowRuntime.FromNodeLines[instance.CurrentNodeId])
        {
          var targetNode = flowRuntime.Nodes[line.To];
          transitions.Add(new TaktInstanceTransDto
          {
            Id = 0, // 新创建的转换，ID为0
            InstanceId = instance.Id,
            StartNodeId = instance.CurrentNodeId ?? "",
            StartNodeName = instance.CurrentNodeName ?? "",
            ToNodeId = targetNode.Id,
            ToNodeName = targetNode.Name,
            TransTime = DateTime.Now,
            Status = 0 // 草稿状态
          });
        }
      }

      return transitions;
    }

    /// <summary>
    /// 获取当前节点信息
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>当前节点信息，如果不存在则返回null</returns>
    public async Task<TaktNodeDto?> GetCurrentNodeAsync(long instanceId)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null || string.IsNullOrEmpty(instance.CurrentNodeId))
      {
        return null;
      }

      // 使用 TaktFlowRuntime 获取当前节点信息
      var flowRuntime = new TaktFlowRuntime(instance);
      var currentNode = flowRuntime.CurrentNode;

      // 从 SetInfo 中提取配置信息
      string? nodeConfig = null;
      int approverType = 1;
      string? approverConfig = null;

      if (currentNode.SetInfo != null)
      {
        nodeConfig = JsonConvert.SerializeObject(currentNode.SetInfo);
        // 如果 SetInfo 是 SetInfo 对象，可以进一步解析
        // 这里简化处理，直接序列化
      }

      return new TaktNodeDto
      {
        NodeId = currentNode.Id,
        NodeName = currentNode.Name,
        NodeType = currentNode.Type,
        NodeConfig = nodeConfig,
        ApproverType = approverType,
        ApproverConfig = approverConfig
      };
    }

    /// <summary>
    /// 获取工作流变量
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>工作流变量字典</returns>
    /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
    public async Task<Dictionary<string, object>> GetVariablesAsync(long instanceId)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      if (string.IsNullOrEmpty(instance.Variables))
      {
        return new Dictionary<string, object>();
      }

      try
      {
        return JsonConvert.DeserializeObject<Dictionary<string, object>>(instance.Variables) ?? new Dictionary<string, object>();
      }
      catch
      {
        return new Dictionary<string, object>();
      }
    }

    /// <summary>
    /// 设置工作流变量
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="variables">变量字典</param>
    /// <exception cref="InvalidOperationException">当工作流实例不存在时抛出</exception>
    public async Task SetVariablesAsync(long instanceId, Dictionary<string, object> variables)
    {
      var instance = await InstanceRepository.GetByIdAsync(instanceId);
      if (instance == null)
      {
        throw new InvalidOperationException("工作流实例不存在");
      }

      instance.Variables = JsonConvert.SerializeObject(variables);
      await InstanceRepository.UpdateAsync(instance);
    }

    /// <summary>
    /// 获取工作流历史记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>流转历史记录列表</returns>
    public async Task<List<TaktInstanceTransDto>> GetHistoryAsync(long instanceId)
    {
      var list = await TransRepository.GetListAsync(x => x.InstanceId == instanceId);
      return list.Adapt<List<TaktInstanceTransDto>>();
    }

    /// <summary>
    /// 获取工作流操作记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <returns>操作记录列表</returns>
    public async Task<List<TaktInstanceOperDto>> GetOperationsAsync(long instanceId)
    {
      var list = await OperRepository.GetListAsync(x => x.InstanceId == instanceId);
      return list.Adapt<List<TaktInstanceOperDto>>();
    }

    /// <summary>
    /// 获取转换历史列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>转换历史列表</returns>
    public async Task<TaktPagedResult<TaktInstanceTransDto>> GetTransitionListAsync(TaktInstanceTransQueryDto query)
    {
      var list = await TransRepository.GetListAsync(x =>
          (query.InstanceId == null || x.InstanceId == query.InstanceId) &&
          (string.IsNullOrEmpty(query.StartNodeId) || x.StartNodeId.Contains(query.StartNodeId)) &&
          (string.IsNullOrEmpty(query.ToNodeId) || x.ToNodeId.Contains(query.ToNodeId)) &&
          (query.Status == null || x.Status == query.Status)
      );

      var total = list.Count;
      var pagedList = list.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize).ToList();

      var dtoList = pagedList.Adapt<List<TaktInstanceTransDto>>();

      return new TaktPagedResult<TaktInstanceTransDto>
      {
        TotalNum = total,
        Rows = dtoList
      };
    }

    /// <summary>
    /// 获取转换历史详情
    /// </summary>
    /// <param name="transitionId">转换ID</param>
    /// <returns>转换历史详情</returns>
    public async Task<TaktInstanceTransDto?> GetTransitionAsync(string transitionId)
    {
      var trans = await TransRepository.GetFirstAsync(x => x.Id.ToString() == transitionId);
      return trans?.Adapt<TaktInstanceTransDto>();
    }

    /// <summary>
    /// 使用条件表达式引擎评估条件
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <param name="variables">工作流变量</param>
    /// <returns>评估结果</returns>
    public async Task<bool> EvaluateConditionAsync(string expression, Dictionary<string, object> variables)
    {
      if (string.IsNullOrWhiteSpace(expression))
        return true;

      // 使用注入的表达式引擎
      return await _expressionEngine.EvaluateAsync(expression, variables);
    }

    #region 私有方法

    /// <summary>
    /// 处理同意操作 - 支持OpenAuth.Net标准，使用 TaktFlowRuntime
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="workflowConfig">工作流配置</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessApproveAsync(TaktInstance instance, TaktWorkflowConfigModel workflowConfig, TaktWorkflowApproveDto dto)
    {
      // 创建运行时引擎
      var flowRuntime = new TaktFlowRuntime(instance, workflowConfig);

      // 创建审批标签
      var tag = new Tag
      {
        Taged = (int)TagState.Ok,
        UserId = _currentUser.UserId.ToString(),
        UserName = _currentUser.UserName,
        Description = dto.OperOpinion ?? "",
        TagedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
      };

      // 根据当前节点类型处理
      string currentNodeType = flowRuntime.GetCurrentNodeType();
      string? nextNodeId = null;
      string? nextNodeName = null;

      if (currentNodeType == "branch")
      {
        // 网关开始节点
        string gatewayResult = flowRuntime.VerifyGatewayStart(tag);

        if (gatewayResult == TagState.No.ToString("D"))
        {
          // 网关不通过，终止流程
          instance.Status = 4; // 4 表示已终止
          instance.EndTime = DateTime.Now;
          await InstanceRepository.UpdateAsync(instance);
          await CreateTransRecordAsync(instance.Id, dto.NodeId, "", "", 4, "网关审批不通过");
          return;
        }
        else if (gatewayResult == "1")
        {
          // 网关仍在处理中
          // 保存配置并更新实例
          instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());
          await InstanceRepository.UpdateAsync(instance);
          await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 1, "网关审批中");
          return;
        }
        else
        {
          // 网关通过，获取下一个节点
          nextNodeId = gatewayResult;
          nextNodeName = flowRuntime.GetNodeType(nextNodeId) == "end"
            ? "结束"
            : flowRuntime.Nodes[nextNodeId].Name;
        }
      }
      else if (currentNodeType == "parallel")
      {
        // 会签节点
        // 如果节点需要运行时指定执行者，使用 DTO 中的 NodeDesignates
        string[]? parallelRuntimeDesignates = null;
        if (!string.IsNullOrEmpty(dto.NodeDesignateType) && dto.NodeDesignates != null && dto.NodeDesignates.Length > 0)
        {
          parallelRuntimeDesignates = dto.NodeDesignates;
        }

        string makerList = await flowRuntime.GetMultiInstanceNodeMakersAsync(
          instance.CurrentNodeId ?? "",
          _approverResolver,
          instance.InitiatorId,
          parallelRuntimeDesignates);

        // 标记当前节点
        flowRuntime.MakeTagNode(instance.CurrentNodeId ?? "", tag);

        // 会签节点需要等待所有审批人完成，这里暂时不流转
        // 保存配置
        instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());
        // 注意：审批人信息存储在 TaktApprover 表中，不存储在 TaktInstance 中
        await InstanceRepository.UpdateAsync(instance);
        await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 1, "会签审批中");
        return;
      }
      else
      {
        // 普通审批节点
        flowRuntime.MakeTagNode(instance.CurrentNodeId ?? "", tag);

        // 判断流程是否完成
        if (flowRuntime.IsFinish())
        {
          // 流程完成
          instance.Status = 2; // 2 表示已完成
          instance.EndTime = DateTime.Now;
          instance.CurrentNodeId = null;
          instance.CurrentNodeName = null;
          instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());
          await InstanceRepository.UpdateAsync(instance);

          await CreateTransRecordAsync(instance.Id, dto.NodeId, "", "", 2, "工作流完成");

          // 发送工作流完成通知
          await _signalRNotificationService.SendWorkflowCompletedAsync(
              instance.Id,
              instance.InstanceTitle,
              _currentUser.UserName,
              "已完成",
              new List<long> { instance.InitiatorId });
          return;
        }

        // 获取下一个节点
        try
        {
          var nextNode = flowRuntime.GetNextNode();
          nextNodeId = nextNode.Id;
          nextNodeName = nextNode.Name;

          // 更新运行时状态（PreviousNodeId 是只读的，通过更新实例来保存）
          flowRuntime.CurrentNodeId = nextNodeId;
        }
        catch (InvalidOperationException)
        {
          // 没有下一个节点，流程完成
          instance.Status = 2;
          instance.EndTime = DateTime.Now;
          instance.CurrentNodeId = null;
          instance.CurrentNodeName = null;
          instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());
          await InstanceRepository.UpdateAsync(instance);
          await CreateTransRecordAsync(instance.Id, dto.NodeId, "", "", 2, "工作流完成");
          return;
        }
      }

      // 更新实例状态
      instance.CurrentNodeId = nextNodeId;
      instance.CurrentNodeName = nextNodeName;
      instance.PreviousNodeId = dto.NodeId; // 保存上一个节点ID
      // 保存上一个节点名称
      if (!string.IsNullOrEmpty(dto.NodeId) && flowRuntime.Nodes.ContainsKey(dto.NodeId))
      {
        instance.PreviousNodeName = flowRuntime.Nodes[dto.NodeId].Name;
      }
      instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());

      // 如果下一个节点需要运行时指定执行者，使用 DTO 中的 NodeDesignates
      string[]? runtimeDesignates = null;
      if (!string.IsNullOrEmpty(dto.NodeDesignateType) && dto.NodeDesignates != null && dto.NodeDesignates.Length > 0)
      {
        runtimeDesignates = dto.NodeDesignates;
      }

      // 获取下一个节点的审批人（如果节点需要运行时指定执行者，使用 runtimeDesignates）
      if (!string.IsNullOrEmpty(nextNodeId))
      {
        try
        {
          var nextNode = flowRuntime.Nodes[nextNodeId];
          string nextMakerList = await flowRuntime.GetNodeMarkersAsync(
            nextNode,
            _approverResolver,
            instance.InitiatorId,
            runtimeDesignates);
          // 注意：审批人信息存储在 TaktApprover 表中，不存储在 TaktInstance 中
          // 如果需要，可以在这里创建 TaktApprover 记录
        }
        catch (Exception ex)
        {
          _logger.Warn($"获取下一个节点审批人失败: {ex.Message}");
        }
      }

      await InstanceRepository.UpdateAsync(instance);

      // 记录流转历史
      await CreateTransRecordAsync(instance.Id, dto.NodeId, nextNodeId ?? "", nextNodeName ?? "", 1, "节点流转");

      // 如果是结束节点，完成工作流
      if (!string.IsNullOrEmpty(nextNodeId) && flowRuntime.GetNodeType(nextNodeId) == "end")
      {
        instance.Status = 2; // 2 表示已完成
        instance.EndTime = DateTime.Now;
        await InstanceRepository.UpdateAsync(instance);

        // 发送工作流完成通知
        await _signalRNotificationService.SendWorkflowCompletedAsync(
            instance.Id,
            instance.InstanceTitle,
            _currentUser.UserName,
            "已完成",
            new List<long> { instance.InitiatorId });
      }
    }

    /// <summary>
    /// 处理拒绝操作
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessRejectAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      instance.Status = 4; // 4 表示已终止
      instance.EndTime = DateTime.Now;
      await InstanceRepository.UpdateAsync(instance);

      await CreateTransRecordAsync(instance.Id, dto.NodeId, "", "", 4, "工作流被拒绝");
    }

    /// <summary>
    /// 处理退回操作 - 使用 TaktFlowRuntime
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="workflowConfig">工作流配置</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessReturnAsync(TaktInstance instance, TaktWorkflowConfigModel workflowConfig, TaktWorkflowApproveDto dto)
    {
      // 创建运行时引擎
      var flowRuntime = new TaktFlowRuntime(instance, workflowConfig);

      // 创建驳回标签
      var tag = new Tag
      {
        Taged = (int)TagState.Reject,
        UserId = _currentUser.UserId.ToString(),
        UserName = _currentUser.UserName,
        Description = dto.OperOpinion ?? "节点退回",
        TagedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
      };

      // 使用 TaktFlowRuntime 的 RejectNode 方法处理退回
      // 优先使用 DTO 中的字段，如果没有则从 OperData 中解析
      string? rejectNodeId = dto.NodeRejectStep;
      string? rejectType = dto.NodeRejectType;

      // 如果 DTO 中没有，尝试从 OperData 中解析退回信息（兼容旧版本）
      if (string.IsNullOrEmpty(rejectNodeId) && !string.IsNullOrEmpty(dto.OperData))
      {
        try
        {
          var operDataJson = JsonConvert.DeserializeObject<JObject>(dto.OperData);
          rejectNodeId = operDataJson?["rejectNodeId"]?.ToString();
          rejectType = operDataJson?["rejectType"]?.ToString();
        }
        catch
        {
          // 解析失败，使用默认值
        }
      }

      flowRuntime.RejectNode(rejectNodeId, rejectType, tag);

      // 更新实例状态
      instance.CurrentNodeId = flowRuntime.CurrentNodeId;
      instance.CurrentNodeName = flowRuntime.Nodes[flowRuntime.CurrentNodeId].Name;
      instance.PreviousNodeId = flowRuntime.PreviousNodeId;
      // 保存上一个节点名称
      if (!string.IsNullOrEmpty(flowRuntime.PreviousNodeId) && flowRuntime.Nodes.ContainsKey(flowRuntime.PreviousNodeId))
      {
        instance.PreviousNodeName = flowRuntime.Nodes[flowRuntime.PreviousNodeId].Name;
      }
      instance.Status = 4; // 4 表示已驳回（需要申请者重新提交表单）
      instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());

      // 获取退回目标节点的审批人
      if (!string.IsNullOrEmpty(flowRuntime.CurrentNodeId))
      {
        try
        {
          var rejectNode = flowRuntime.Nodes[flowRuntime.CurrentNodeId];
          string makerList = await flowRuntime.GetNodeMarkersAsync(
            rejectNode,
            _approverResolver,
            instance.InitiatorId);
          // 注意：审批人信息存储在 TaktApprover 表中
        }
        catch (Exception ex)
        {
          _logger.Warn($"获取退回节点审批人失败: {ex.Message}");
        }
      }

      await InstanceRepository.UpdateAsync(instance);

      // 记录流转历史
      await CreateTransRecordAsync(
        instance.Id,
        dto.NodeId,
        flowRuntime.CurrentNodeId,
        instance.CurrentNodeName ?? "",
        3,
        $"节点退回: {dto.OperOpinion ?? ""}");
    }

    /// <summary>
    /// 处理转办操作
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessTransferAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      // 转办操作，当前节点保持不变，但操作人变为目标用户
      // 这里需要根据具体业务逻辑实现
      await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 5, $"转办给用户{dto.TargetUserId}");
    }

    /// <summary>
    /// 处理委托操作
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessDelegateAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      // 委托操作，当前节点保持不变，但操作人变为目标用户
      // 这里需要根据具体业务逻辑实现
      await CreateTransRecordAsync(instance.Id, dto.NodeId, dto.NodeId, instance.CurrentNodeName ?? "", 6, $"委托给用户{dto.TargetUserId}");
    }

    /// <summary>
    /// 创建流转历史记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="fromNodeId">源节点ID</param>
    /// <param name="toNodeId">目标节点ID</param>
    /// <param name="toNodeName">目标节点名称</param>
    /// <param name="transType">流转类型</param>
    /// <param name="remark">备注</param>
    /// <returns>异步任务</returns>
    private async Task CreateTransRecordAsync(long instanceId, string fromNodeId, string toNodeId, string toNodeName, int transType, string remark)
    {
      var trans = new TaktInstanceTrans
      {
        InstanceId = instanceId,
        StartNodeId = fromNodeId,
        StartNodeType = 1, // 默认类型
        StartNodeName = fromNodeId, // 简化处理
        ToNodeId = toNodeId,
        ToNodeType = 1, // 默认类型
        ToNodeName = toNodeName,
        Status = string.IsNullOrEmpty(toNodeId) ? 2 : 1, // 如果没有目标节点，表示完成(2)，否则运行中(1)
        TransTime = DateTime.Now
      };

      await TransRepository.CreateAsync(trans);
    }

    /// <summary>
    /// 创建操作记录
    /// </summary>
    /// <param name="instanceId">工作流实例ID</param>
    /// <param name="nodeId">节点ID</param>
    /// <param name="nodeName">节点名称</param>
    /// <param name="operType">操作类型</param>
    /// <param name="operatorId">操作人ID</param>
    /// <param name="operOpinion">操作意见</param>
    /// <param name="operData">操作数据</param>
    /// <returns>异步任务</returns>
    private async Task CreateOperRecordAsync(long instanceId, string nodeId, string nodeName, int operType, long operatorId, string operOpinion, string? operData)
    {
      var oper = new TaktInstanceOper
      {
        InstanceId = instanceId,
        NodeId = nodeId,
        NodeName = nodeName,
        OperType = operType,
        OperatorId = operatorId,
        OperatorName = _currentUser.UserName,
        OperOpinion = operOpinion,
        OperData = operData
      };

      await OperRepository.CreateAsync(oper);
    }

    /// <summary>
    /// 拒绝工作流
    /// </summary>
    /// <param name="dto">拒绝参数</param>
    /// <returns>拒绝结果</returns>
    public async Task<bool> RejectAsync(TaktWorkflowRejectDto dto)
    {
      _dbContext.BeginTran();
      try
      {
        var instance = await InstanceRepository.GetByIdAsync(dto.InstanceId);
        if (instance == null)
        {
          throw new InvalidOperationException("工作流实例不存在");
        }

        if (instance.Status != 1) // 1 表示运行中
        {
          throw new InvalidOperationException("只有运行中的工作流实例才能拒绝");
        }

        if (instance.CurrentNodeId != dto.NodeId)
        {
          throw new InvalidOperationException("当前节点不匹配");
        }

        // 更新实例状态为已终止
        instance.Status = 4; // 4 表示已终止
        instance.EndTime = DateTime.Now;
        await InstanceRepository.UpdateAsync(instance);

        // 记录操作
        await CreateOperRecordAsync(dto.InstanceId, dto.NodeId, instance.CurrentNodeName ?? "", 2, _currentUser.UserId, dto.RejectReason, dto.RejectData);

        // 记录流转历史
        await CreateTransRecordAsync(dto.InstanceId, dto.NodeId, "", "", 4, dto.RejectReason);

        _dbContext.CommitTran();
        return true;
      }
      catch (Exception ex)
      {
        _dbContext.RollbackTran();
        _logger.Error($"工作流拒绝失败: {ex.Message}", ex);
        throw;
      }
    }

    /// <summary>
    /// 手动触发节点转换
    /// </summary>
    /// <param name="dto">转换参数</param>
    /// <returns>转换结果</returns>
    public async Task<bool> TransitAsync(TaktWorkflowTransitDto dto)
    {
      _dbContext.BeginTran();
      try
      {
        var instance = await InstanceRepository.GetByIdAsync(dto.InstanceId);
        if (instance == null)
        {
          throw new InvalidOperationException("工作流实例不存在");
        }

        if (instance.Status != 1) // 1 表示运行中
        {
          throw new InvalidOperationException("只有运行中的工作流实例才能转换");
        }

        if (instance.CurrentNodeId != dto.FromNodeId)
        {
          throw new InvalidOperationException("源节点不匹配");
        }

        // 获取工作流方案
        var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
        if (scheme == null)
        {
          throw new InvalidOperationException("工作流方案不存在");
        }

        // 解析工作流配置
        var workflowConfig = JsonConvert.DeserializeObject<TaktWorkflowConfigModel>(scheme.SchemeConfig);
        if (workflowConfig == null)
        {
          throw new InvalidOperationException("工作流配置无效");
        }

        // 验证目标节点是否存在
        var targetNode = workflowConfig.Nodes.FirstOrDefault(n => n.Id == dto.ToNodeId);
        if (targetNode == null)
        {
          throw new InvalidOperationException("目标节点不存在");
        }

        // 更新实例状态
        instance.CurrentNodeId = dto.ToNodeId;
        instance.CurrentNodeName = targetNode.Name;
        await InstanceRepository.UpdateAsync(instance);

        // 记录操作
        await CreateOperRecordAsync(dto.InstanceId, dto.FromNodeId, instance.CurrentNodeName ?? "", 1, _currentUser.UserId, dto.TransitReason, dto.TransitData);

        // 记录流转历史
        await CreateTransRecordAsync(dto.InstanceId, dto.FromNodeId, dto.ToNodeId, targetNode.Name, 1, dto.TransitReason);

        _dbContext.CommitTran();
        return true;
      }
      catch (Exception ex)
      {
        _dbContext.RollbackTran();
        _logger.Error($"工作流转换失败: {ex.Message}", ex);
        throw;
      }
    }

    /// <summary>
    /// 评估条件表达式
    /// </summary>
    /// <param name="dto">条件评估参数</param>
    /// <returns>评估结果</returns>
    public async Task<TaktConditionEvaluateResultDto> EvaluateConditionAsync(TaktConditionEvaluateDto dto)
    {
      try
      {
        var result = await _expressionEngine.EvaluateAsync(dto.Expression, dto.Variables);
        return new TaktConditionEvaluateResultDto
        {
          Expression = dto.Expression,
          Result = result,
          ErrorMessage = string.Empty
        };
      }
      catch (Exception ex)
      {
        return new TaktConditionEvaluateResultDto
        {
          Expression = dto.Expression,
          Result = false,
          ErrorMessage = ex.Message
        };
      }
    }

    /// <summary>
    /// 获取工作流实例待办任务
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>待办任务列表</returns>
    public async Task<List<TaktWorkflowTaskDto>> GetTodoTasksAsync(long userId, TaktWorkflowTodoQueryDto query)
    {
      // 这里需要根据具体业务逻辑实现待办任务查询
      // 简化实现：返回当前用户相关的运行中工作流实例
      var instances = await InstanceRepository.GetListAsync(x =>
          x.Status == 1 && // 运行中
          (string.IsNullOrEmpty(query.Keyword) || x.InstanceTitle.Contains(query.Keyword)) &&
          (string.IsNullOrEmpty(query.SchemeId) || x.SchemeId.ToString() == query.SchemeId) &&
          (string.IsNullOrEmpty(query.NodeId) || x.CurrentNodeId == query.NodeId) &&
          (query.StartTime == null || x.StartTime >= query.StartTime) &&
          (query.EndTime == null || x.StartTime <= query.EndTime)
      );

      var result = new List<TaktWorkflowTaskDto>();
      foreach (var instance in instances)
      {
        result.Add(new TaktWorkflowTaskDto
        {
          TaskId = instance.Id,
          InstanceId = instance.Id.ToString(),
          InstanceName = instance.InstanceTitle,
          SchemeId = instance.SchemeId.ToString(),
          SchemeName = "", // 需要从方案表获取
          NodeId = instance.CurrentNodeId ?? "",
          NodeName = instance.CurrentNodeName ?? "",
          NodeType = "", // 需要从配置获取
          StartUserName = "", // 需要从用户表获取
          CurrentUserName = "", // 需要从用户表获取
          Status = instance.Status.ToString(),
          CreateTime = instance.StartTime ?? DateTime.Now,
          UpdateTime = instance.UpdateTime ?? DateTime.Now
        });
      }

      return result;
    }

    /// <summary>
    /// 获取工作流实例已办任务
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="query">查询条件</param>
    /// <returns>已办任务列表</returns>
    public async Task<List<TaktWorkflowTaskDto>> GetDoneTasksAsync(long userId, TaktWorkflowDoneQueryDto query)
    {
      // 这里需要根据具体业务逻辑实现已办任务查询
      // 简化实现：返回当前用户操作过的已完成或已终止的工作流实例
      var operations = await OperRepository.GetListAsync(x =>
          x.OperatorId == userId &&
          (string.IsNullOrEmpty(query.Keyword) || (x.NodeName ?? "").Contains(query.Keyword)) &&
          (string.IsNullOrEmpty(query.SchemeId) || x.InstanceId.ToString() == query.SchemeId) &&
          (string.IsNullOrEmpty(query.NodeId) || x.NodeId == query.NodeId) &&
          (string.IsNullOrEmpty(query.OperType) || x.OperType.ToString() == query.OperType)
      );

      var result = new List<TaktWorkflowTaskDto>();
      foreach (var oper in operations)
      {
        var instance = await InstanceRepository.GetByIdAsync(oper.InstanceId);
        if (instance != null)
        {
          result.Add(new TaktWorkflowTaskDto
          {
            TaskId = instance.Id,
            InstanceId = instance.Id.ToString(),
            InstanceName = instance.InstanceTitle,
            SchemeId = instance.SchemeId.ToString(),
            SchemeName = "", // 需要从方案表获取
            NodeId = oper.NodeId ?? "",
            NodeName = oper.NodeName ?? "",
            NodeType = "", // 需要从配置获取
            StartUserName = "", // 需要从用户表获取
            CurrentUserName = oper.OperatorName,
            Status = instance.Status.ToString(),
            CreateTime = instance.StartTime ?? DateTime.Now,
            UpdateTime = oper.CreateTime
          });
        }
      }

      return result;
    }

    /// <summary>
    /// 处理加签操作 - OpenAuth.Net标准
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessAddSignAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      // 加签操作：在当前节点添加额外的审批人
      // 这里需要根据具体的业务需求来实现
      _logger.Info($"处理加签操作，实例ID: {instance.Id}, 节点ID: {dto.NodeId}");

      // 记录加签操作
      await CreateOperRecordAsync(instance.Id, dto.NodeId, instance.CurrentNodeName ?? "", 6, _currentUser.UserId, dto.OperOpinion, dto.OperData);
    }

    /// <summary>
    /// 处理知会操作 - OpenAuth.Net标准
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessNotifyAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      // 知会操作：通知相关人员，但不影响流程流转
      _logger.Info($"处理知会操作，实例ID: {instance.Id}, 节点ID: {dto.NodeId}");

      // 记录知会操作
      await CreateOperRecordAsync(instance.Id, dto.NodeId, instance.CurrentNodeName ?? "", 7, _currentUser.UserId, dto.OperOpinion, dto.OperData);

      // 知会操作不影响流程状态，只是记录操作
    }

    /// <summary>
    /// 处理撤回操作 - OpenAuth.Net标准
    /// </summary>
    /// <param name="instance">工作流实例</param>
    /// <param name="dto">审批信息</param>
    /// <returns>异步任务</returns>
    private async Task ProcessWithdrawAsync(TaktInstance instance, TaktWorkflowApproveDto dto)
    {
      // 撤回操作：将流程退回到发起人 - 使用 TaktFlowRuntime
      _logger.Info($"处理撤回操作，实例ID: {instance.Id}, 节点ID: {dto.NodeId}");

      // 只有发起人才能撤回
      if (instance.InitiatorId != _currentUser.UserId)
      {
        throw new InvalidOperationException("只有发起人才能撤回流程");
      }

      // 获取工作流方案
      var scheme = await SchemeRepository.GetByIdAsync(instance.SchemeId);
      if (scheme == null)
      {
        throw new InvalidOperationException("工作流方案不存在");
      }

      // 解析工作流配置
      var workflowConfig = JsonConvert.DeserializeObject<TaktWorkflowConfigModel>(scheme.SchemeConfig);
      if (workflowConfig == null)
      {
        throw new InvalidOperationException("工作流配置无效");
      }

      // 创建运行时引擎
      var flowRuntime = new TaktFlowRuntime(instance, workflowConfig);

      // 使用 TaktFlowRuntime 的 ReCall 方法撤回流程
      flowRuntime.ReCall();

      // 更新实例状态
      instance.Status = 0; // 0 表示草稿（撤回后回到草稿状态）
      instance.CurrentNodeId = flowRuntime.CurrentNodeId;
      instance.CurrentNodeName = flowRuntime.Nodes[flowRuntime.CurrentNodeId].Name;
      instance.PreviousNodeId = flowRuntime.PreviousNodeId;
      // 保存上一个节点名称
      if (!string.IsNullOrEmpty(flowRuntime.PreviousNodeId) && flowRuntime.Nodes.ContainsKey(flowRuntime.PreviousNodeId))
      {
        instance.PreviousNodeName = flowRuntime.Nodes[flowRuntime.PreviousNodeId].Name;
      }
      instance.SchemeConfig = JsonConvert.SerializeObject(flowRuntime.ToSchemeObj());

      // 获取开始节点的审批人（通常是发起人）
      try
      {
        var startNode = flowRuntime.Nodes[flowRuntime.StartNodeId];
        string makerList = await flowRuntime.GetNodeMarkersAsync(
          startNode,
          _approverResolver,
          instance.InitiatorId);
        // 注意：审批人信息存储在 TaktApprover 表中
      }
      catch (Exception ex)
      {
        _logger.Warn($"获取开始节点审批人失败: {ex.Message}");
      }

      await InstanceRepository.UpdateAsync(instance);

      // 记录撤回操作
      await CreateOperRecordAsync(instance.Id, dto.NodeId, instance.CurrentNodeName ?? "", 8, _currentUser.UserId, dto.OperOpinion, dto.OperData);
      await CreateTransRecordAsync(instance.Id, dto.NodeId, flowRuntime.CurrentNodeId, instance.CurrentNodeName ?? "", 5, $"流程撤回: {dto.OperOpinion ?? ""}");
    }

    #endregion

  }
}

