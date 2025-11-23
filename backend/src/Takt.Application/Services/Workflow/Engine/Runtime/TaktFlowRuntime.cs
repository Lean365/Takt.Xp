#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFlowRuntime.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流运行时引擎 - 基于 OpenAuth.Net FlowRuntime
//===================================================================

using Takt.Domain.Entities.Workflow;
using Takt.Domain.Models.Workflow;
using Takt.Shared.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Takt.Application.Services.Workflow.Engine.Runtime
{
    /// <summary>
    /// 工作流运行时引擎
    /// 一个正在运行中的流程实例
    /// </summary>
    public class TaktFlowRuntime
    {
        private readonly TaktInstance _instance;
        private readonly Dictionary<string, TaktFlowNode> _nodes;
        private readonly List<TaktFlowLine> _lines;
        private readonly Dictionary<string, List<TaktFlowLine>> _fromNodeLines;
        private readonly Dictionary<string, List<TaktFlowLine>> _toNodeLines;

        /// <summary>
        /// 流程标题
        /// </summary>
        private string Title { get; set; } = string.Empty;

        /// <summary>
        /// 初始化编号
        /// </summary>
        private int InitNum { get; set; }

        /// <summary>
        /// 开始节点ID
        /// </summary>
        public string StartNodeId { get; private set; } = string.Empty;

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string CurrentNodeId { get; set; }

        /// <summary>
        /// 当前节点对象
        /// </summary>
        public TaktFlowNode CurrentNode => _nodes[CurrentNodeId];

        /// <summary>
        /// 下一个节点ID
        /// </summary>
        public string? NextNodeId { get; private set; }

        /// <summary>
        /// 下一个节点对象
        /// </summary>
        public TaktFlowNode? NextNode => NextNodeId != null && NextNodeId != "-1" ? _nodes[NextNodeId] : null;

        /// <summary>
        /// 上一个节点ID
        /// </summary>
        public string? PreviousNodeId { get; private set; }

        /// <summary>
        /// 表单数据
        /// </summary>
        public string FormData { get; private set; } = string.Empty;

        /// <summary>
        /// 流程实例ID
        /// </summary>
        public long InstanceId => _instance.Id;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instance">工作流实例</param>
        /// <param name="config">工作流配置（可选，如果为空则从实例的 SchemeConfig 解析）</param>
        public TaktFlowRuntime(TaktInstance instance, TaktWorkflowConfigModel? config = null)
        {
            _instance = instance ?? throw new ArgumentNullException(nameof(instance));

            // 如果未提供配置，从实例的 SchemeConfig 解析
            if (config == null)
            {
                if (string.IsNullOrEmpty(instance.SchemeConfig))
                {
                    throw new InvalidOperationException("工作流实例的配置为空");
                }

                config = JsonConvert.DeserializeObject<TaktWorkflowConfigModel>(instance.SchemeConfig);
                if (config == null)
                {
                    throw new InvalidOperationException("工作流配置解析失败");
                }
            }

            // 初始化节点和连线
            _nodes = new Dictionary<string, TaktFlowNode>();
            _lines = new List<TaktFlowLine>();
            _fromNodeLines = new Dictionary<string, List<TaktFlowLine>>();
            _toNodeLines = new Dictionary<string, List<TaktFlowLine>>();

            InitNodes(config);
            InitLines(config);

            // 设置流程基本信息
            Title = config.Title;
            InitNum = config.InitNum;
            FormData = instance.FormData ?? "{}";

            // 设置当前节点
            CurrentNodeId = instance.CurrentNodeId ?? StartNodeId;
            PreviousNodeId = instance.PreviousNodeId;

            // 网关开始节点和流程结束节点没有下一步
            if (GetCurrentNodeType() == TaktWorkflowConstants.NODE_TYPE_BRANCH ||
                GetCurrentNodeType() == TaktWorkflowConstants.NODE_TYPE_END)
            {
                NextNodeId = "-1";
            }
            else
            {
                NextNodeId = GetNextNodeId();
            }
        }

        #region 初始化方法

        /// <summary>
        /// 初始化节点
        /// </summary>
        private void InitNodes(TaktWorkflowConfigModel config)
        {
            foreach (var nodeModel in config.Nodes)
            {
                var node = new TaktFlowNode
                {
                    Id = nodeModel.Id,
                    Name = nodeModel.Name,
                    Type = nodeModel.Type,
                    Left = nodeModel.Left,
                    Top = nodeModel.Top,
                    Width = nodeModel.Width,
                    Height = nodeModel.Height
                };

                // 转换 SetInfo
                if (nodeModel.SetInfo != null)
                {
                    try
                    {
                        var setInfoJson = nodeModel.SetInfo is JObject jObj 
                            ? jObj 
                            : JObject.FromObject(nodeModel.SetInfo);
                        
                        node.SetInfo = setInfoJson.ToObject<SetInfo>();
                    }
                    catch
                    {
                        // 如果转换失败，创建空的 SetInfo
                        node.SetInfo = new SetInfo();
                    }
                }
                else
                {
                    node.SetInfo = new SetInfo();
                }

                if (!_nodes.ContainsKey(node.Id))
                {
                    _nodes.Add(node.Id, node);
                }

                // 记录开始节点
                if (node.Type == TaktWorkflowConstants.NODE_TYPE_START)
                {
                    StartNodeId = node.Id;
                }
            }
        }

        /// <summary>
        /// 初始化连线
        /// </summary>
        private void InitLines(TaktWorkflowConfigModel config)
        {
            foreach (var edgeModel in config.Edges)
            {
                var line = new TaktFlowLine
                {
                    Id = edgeModel.Id,
                    From = edgeModel.Source,
                    To = edgeModel.Target,
                    Label = edgeModel.Label ?? string.Empty,
                    Type = edgeModel.Type,
                    Name = edgeModel.Label ?? string.Empty
                };

                // 转换条件
                if (edgeModel.Compares != null && edgeModel.Compares.Any())
                {
                    line.Compares = edgeModel.Compares;
                }
                else if (edgeModel.Condition != null)
                {
                    // 尝试从 Condition 对象中解析条件
                    try
                    {
                        var conditionJson = edgeModel.Condition is JObject jObj
                            ? jObj
                            : JObject.FromObject(edgeModel.Condition);

                        if (conditionJson["compares"] != null)
                        {
                            line.Compares = conditionJson["compares"].ToObject<List<TaktDataCompare>>();
                        }
                    }
                    catch
                    {
                        // 转换失败，不设置条件
                    }
                }

                _lines.Add(line);

                // 建立从节点到连线的映射
                if (!_fromNodeLines.ContainsKey(line.From))
                {
                    _fromNodeLines.Add(line.From, new List<TaktFlowLine>());
                }
                _fromNodeLines[line.From].Add(line);

                // 建立到节点到连线的映射
                if (!_toNodeLines.ContainsKey(line.To))
                {
                    _toNodeLines.Add(line.To, new List<TaktFlowLine>());
                }
                _toNodeLines[line.To].Add(line);
            }
        }

        #endregion

        #region 核心方法

        /// <summary>
        /// 判断流程是否完成
        /// </summary>
        public bool IsFinish()
        {
            return GetNextNodeType() == TaktWorkflowConstants.NODE_TYPE_END;
        }

        /// <summary>
        /// 获取下一个节点
        /// </summary>
        public TaktFlowNode GetNextNode(string? nodeId = null)
        {
            var nextId = GetNextNodeId(nodeId);
            if (string.IsNullOrEmpty(nextId) || nextId == "-1")
            {
                throw new InvalidOperationException("无法找到下一个节点");
            }
            return _nodes[nextId];
        }

        /// <summary>
        /// 获取下一个节点ID
        /// </summary>
        private string GetNextNodeId(string? nodeId = null)
        {
            var currentNodeId = nodeId ?? CurrentNodeId;
            if (!_fromNodeLines.ContainsKey(currentNodeId) || !_fromNodeLines[currentNodeId].Any())
            {
                throw new InvalidOperationException("无法寻找到下一个节点");
            }

            var lines = _fromNodeLines[currentNodeId];

            // URL表单暂时不支持条件判断
            if (_instance.FormType == 2) // URL表单
            {
                return lines[0].To;
            }

            // 如果没有表单数据，返回第一条连线
            if (string.IsNullOrEmpty(FormData) || FormData == "{}")
            {
                return lines[0].To;
            }

            // 解析表单数据
            var formDataJson = JObject.Parse(FormData);

            // 遍历所有连线，找到满足条件的连线
            foreach (var line in lines)
            {
                if (line.Compares != null && line.Compares.Any() && line.Compare(formDataJson))
                {
                    return line.To;
                }
            }

            // 如果没有满足条件的连线，返回第一条
            return lines[0].To;
        }

        /// <summary>
        /// 获取实例接下来运行的节点类型
        /// </summary>
        public string GetNextNodeType()
        {
            if (NextNodeId != null && NextNodeId != "-1")
            {
                return GetNodeType(NextNodeId);
            }
            return TaktWorkflowConstants.NODE_TYPE_END;
        }

        /// <summary>
        /// 获取节点类型
        /// </summary>
        public string GetNodeType(string nodeId)
        {
            if (!_nodes.ContainsKey(nodeId))
            {
                throw new InvalidOperationException($"节点 {nodeId} 不存在");
            }
            return _nodes[nodeId].Type;
        }

        /// <summary>
        /// 获取当前节点类型
        /// </summary>
        public string GetCurrentNodeType()
        {
            return GetNodeType(CurrentNodeId);
        }

        /// <summary>
        /// 获取上一个节点
        /// </summary>
        public TaktFlowNode GetPreviousNode(string? nodeId = null)
        {
            var currentNodeId = nodeId ?? CurrentNodeId;
            if (!_toNodeLines.ContainsKey(currentNodeId) || !_toNodeLines[currentNodeId].Any())
            {
                throw new InvalidOperationException("无法找到上一个节点");
            }

            var previousNodeId = _toNodeLines[currentNodeId][0].From;
            return _nodes[previousNodeId];
        }

        /// <summary>
        /// 标记节点状态
        /// </summary>
        public void MakeTagNode(string nodeId, Tag tag)
        {
            if (!_nodes.ContainsKey(nodeId))
            {
                throw new InvalidOperationException($"节点 {nodeId} 不存在");
            }

            var node = _nodes[nodeId];
            if (node.SetInfo == null)
            {
                node.SetInfo = new SetInfo();
            }

            node.SetInfo.Taged = tag.Taged;
            node.SetInfo.UserId = tag.UserId;
            node.SetInfo.UserName = tag.UserName;
            node.SetInfo.Description = tag.Description;
            node.SetInfo.TagedTime = tag.TagedTime;
        }

        /// <summary>
        /// 转换为方案对象（用于保存到数据库）
        /// </summary>
        public object ToSchemeObj()
        {
            return new
            {
                title = Title,
                initNum = InitNum,
                lines = _lines.Select(l => new
                {
                    id = l.Id,
                    from = l.From,
                    to = l.To,
                    label = l.Label,
                    type = l.Type,
                    name = l.Name,
                    compares = l.Compares
                }),
                nodes = _nodes.Values.Select(n => new
                {
                    id = n.Id,
                    name = n.Name,
                    type = n.Type,
                    left = n.Left,
                    top = n.Top,
                    width = n.Width,
                    height = n.Height,
                    setInfo = n.SetInfo != null ? new
                    {
                        taged = n.SetInfo.Taged,
                        userId = n.SetInfo.UserId,
                        userName = n.SetInfo.UserName,
                        description = n.SetInfo.Description,
                        tagedTime = n.SetInfo.TagedTime,
                        nodeDesignate = n.SetInfo.NodeDesignate,
                        nodeDesignateData = n.SetInfo.NodeDesignateData != null ? new
                        {
                            datas = n.SetInfo.NodeDesignateData.Datas
                        } : null,
                        nodeConfluenceType = n.SetInfo.NodeConfluenceType,
                        confluenceOk = n.SetInfo.ConfluenceOk,
                        confluenceNo = n.SetInfo.ConfluenceNo,
                        nodeRejectType = n.SetInfo.NodeRejectType,
                        canWriteFormItemIds = n.SetInfo.CanWriteFormItemIds
                    } : null
                }),
                areas = new string[0]
            };
        }

        /// <summary>
        /// 审批网关开始节点
        /// </summary>
        /// <param name="tag">审批标签</param>
        /// <returns>
        /// -1: 不通过
        /// 1: 等待（网关未结束）
        /// 其他: 网关结束后的下一个节点ID
        /// </returns>
        public string VerifyGatewayStart(Tag tag)
        {
            // 审批网关时的【当前节点】一直是网关开始节点
            MakeTagNode(CurrentNodeId, tag);

            string canCheckId = ""; // 寻找当前登录用户可审核的节点Id
            foreach (string fromForkStartNodeId in FromNodeLines[CurrentNodeId].Select(u => u.To))
            {
                var fromForkStartNode = _nodes[fromForkStartNodeId]; // 与网关开始节点直接连接的节点
                canCheckId = GetOneForkLineCanCheckNodeId(fromForkStartNode, tag);
                if (!string.IsNullOrEmpty(canCheckId))
                    break;
            }

            if (string.IsNullOrEmpty(canCheckId))
            {
                throw new InvalidOperationException("审核异常,找不到审核节点");
            }

            // 记录审批操作
            var content = $"{tag.UserName}-{DateTime.Now:yyyy-MM-dd HH:mm}审批了【{_nodes[canCheckId].Name}】" +
                $"结果：{(tag.Taged == (int)TagState.Ok ? "同意" : "不同意")}，备注：{tag.Description}";
            // SaveOperationHis(content); // 需要在外部调用

            MakeTagNode(canCheckId, tag); // 标记审核节点状态

            var forkNode = _nodes[CurrentNodeId]; // 网关开始节点
            TaktFlowNode nextNode = GetNextNode(canCheckId); // 获取当前处理的下一个节点

            int forkNumber = FromNodeLines[CurrentNodeId].Count; // 直接与网关节点连接的点，即网关分支数目
            string gatewayResult = string.Empty; // 记录网关审批的结果,为空表示仍然在网关内部处理

            if (forkNode.SetInfo?.NodeConfluenceType == TaktWorkflowConstants.CONFLUENCE_TYPE_ONE) // 有一个步骤通过即可
            {
                if (tag.Taged == (int)TagState.Ok)
                {
                    if (nextNode.Type == TaktWorkflowConstants.NODE_TYPE_JOIN) // 下一个节点是网关结束，则该线路结束
                    {
                        gatewayResult = GetNextNodeId(nextNode.Id);
                    }
                }
                else if (tag.Taged == (int)TagState.No)
                {
                    if (forkNode.SetInfo.ConfluenceNo == null)
                    {
                        forkNode.SetInfo.ConfluenceNo = 1;
                    }
                    else if (forkNode.SetInfo.ConfluenceNo == (forkNumber - 1))
                    {
                        gatewayResult = TagState.No.ToString("D");
                    }
                    else
                    {
                        bool isFirst = true; // 是不是从网关开始到现在第一个
                        var preNode = GetPreviousNode(canCheckId);
                        while (preNode.Id != forkNode.Id) // 反向一直到网关开始节点
                        {
                            if (preNode.SetInfo != null && preNode.SetInfo.Taged == (int)TagState.No)
                            {
                                isFirst = false;
                                break;
                            }
                            preNode = GetPreviousNode(preNode.Id);
                        }

                        if (isFirst)
                        {
                            forkNode.SetInfo.ConfluenceNo = (forkNode.SetInfo.ConfluenceNo ?? 0) + 1;
                        }
                    }
                }
            }
            else // 所有步骤通过
            {
                if (tag.Taged == (int)TagState.No) // 只要有一个不同意，那么流程就结束
                {
                    gatewayResult = TagState.No.ToString("D");
                }
                else if (tag.Taged == (int)TagState.Ok)
                {
                    if (nextNode.Type == TaktWorkflowConstants.NODE_TYPE_JOIN) // 这种模式下只有坚持到【网关结束】节点之前才有意义
                    {
                        if (forkNode.SetInfo?.ConfluenceOk == null)
                        {
                            forkNode.SetInfo ??= new SetInfo();
                            forkNode.SetInfo.ConfluenceOk = 1;
                        }
                        else if (forkNode.SetInfo.ConfluenceOk == (forkNumber - 1)) // 网关成功
                        {
                            gatewayResult = GetNextNodeId(nextNode.Id);
                        }
                        else
                        {
                            forkNode.SetInfo.ConfluenceOk = (forkNode.SetInfo.ConfluenceOk ?? 0) + 1;
                        }
                    }
                }
            }

            if (gatewayResult == TagState.No.ToString("D"))
            {
                tag.Taged = (int)TagState.No;
                MakeTagNode(nextNode.Id, tag);
            }
            else if (!string.IsNullOrEmpty(gatewayResult)) // 网关结束，标记合流节点
            {
                tag.Taged = (int)TagState.Ok;
                MakeTagNode(nextNode.Id, tag);
                NextNodeId = gatewayResult;
            }
            else
            {
                NextNodeId = nextNode.Id;
            }

            return gatewayResult;
        }

        /// <summary>
        /// 网关时，获取一条网关分支上面是否有用户可审核的节点
        /// </summary>
        private string GetOneForkLineCanCheckNodeId(TaktFlowNode fromForkStartNode, Tag tag)
        {
            string canCheckId = "";
            var node = fromForkStartNode;
            do // 沿一条分支线路执行，直到遇到网关结束节点
            {
                // 这里需要调用审批人解析器来判断当前用户是否可以审核该节点
                // 简化处理：如果节点未审批且当前用户ID在审批人列表中
                if (node.SetInfo?.Taged == null)
                {
                    // 这里需要根据节点的审批人配置来判断
                    // 暂时简化处理
                    canCheckId = node.Id;
                    break;
                }

                node = GetNextNode(node.Id);
            } while (node.Type != TaktWorkflowConstants.NODE_TYPE_JOIN);

            return canCheckId;
        }

        /// <summary>
        /// 获取网关开始节点的所有可执行者
        /// </summary>
        public string GetForkNodeMakers(string forkNodeId)
        {
            string makerList = "";
            foreach (string fromForkStartNodeId in FromNodeLines[forkNodeId].Select(u => u.To))
            {
                var fromForkStartNode = _nodes[fromForkStartNodeId]; // 与网关开始节点直接连接的节点
                if (!string.IsNullOrEmpty(makerList))
                {
                    makerList += ",";
                }
                makerList += GetOneForkLineMakers(fromForkStartNode);
            }
            return makerList;
        }

        /// <summary>
        /// 获取网关一条线上的审核者,该审核者应该是已审核过的节点的下一个人
        /// </summary>
        private string GetOneForkLineMakers(TaktFlowNode fromForkStartNode)
        {
            string markers = "";
            var node = fromForkStartNode;
            do // 沿一条分支线路执行，直到遇到第一个没有审核的节点
            {
                if (node.SetInfo != null && node.SetInfo.Taged != null)
                {
                    if (node.Type != TaktWorkflowConstants.NODE_TYPE_BRANCH && 
                        node.SetInfo.Taged != (int)TagState.Ok) // 如果节点是不同意或驳回，则不用再找了
                    {
                        break;
                    }
                    node = GetNextNode(node.Id); // 下一个节点
                    continue;
                }

                // 这里需要调用审批人解析器获取节点的审批人
                // 暂时返回空，需要在外部调用时传入审批人解析器
                break;
            } while (node.Type != TaktWorkflowConstants.NODE_TYPE_JOIN);

            return markers;
        }

        /// <summary>
        /// 计算多实例、会签节点的执行人
        /// </summary>
        /// <param name="nodeId">会签节点ID</param>
        /// <param name="approverResolver">审批人解析器</param>
        /// <param name="instanceCreateUserId">流程实例创建人ID</param>
        /// <param name="runtimeDesignates">运行时指定的审批人</param>
        /// <returns>执行人ID列表（逗号分隔的字符串）</returns>
        public async Task<string> GetMultiInstanceNodeMakersAsync(
            string nodeId,
            Resolvers.ITaktApproverResolver approverResolver,
            long instanceCreateUserId,
            string[]? runtimeDesignates = null)
        {
            if (GetNodeType(nodeId) != TaktWorkflowConstants.NODE_TYPE_PARALLEL)
            {
                throw new InvalidOperationException("当前节点不是会签节点，请联系管理员");
            }

            var node = _nodes[nodeId];
            string[] makerList = Array.Empty<string>();

            // 通过审批人解析器获取会签人员列表
            var makerListStr = await approverResolver.ResolveApproversByNodeAsync(
                node, instanceCreateUserId, runtimeDesignates);

            if (makerListStr == "1")
            {
                throw new InvalidOperationException("会签节点不能设置为所有人，请重新设计流程模板");
            }

            makerList = makerListStr.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (!makerList.Any())
            {
                throw new InvalidOperationException("会签节点没有找到审批人，请检查流程设计");
            }

            // 将所有的会签人员写入到 TaktApprover 表（需要在外部实现）
            // 这里返回会签人员列表，由调用方负责写入数据库

            // 如果是顺序执行，取第一个人
            if (node.SetInfo?.NodeConfluenceType == TaktWorkflowConstants.APPROVE_TYPE_SEQUENTIAL)
            {
                return makerList[0];
            }

            // 否则并行且/并行或都是返回所有加签人
            return string.Join(",", makerList);
        }

        /// <summary>
        /// 获取节点审批人（用于一般节点）
        /// </summary>
        /// <param name="node">流程节点</param>
        /// <param name="approverResolver">审批人解析器</param>
        /// <param name="instanceCreateUserId">流程实例创建人ID</param>
        /// <param name="runtimeDesignates">运行时指定的审批人</param>
        /// <returns>审批人ID列表（逗号分隔的字符串，或 "1" 表示所有人）</returns>
        public async Task<string> GetNodeMarkersAsync(
            TaktFlowNode node,
            Resolvers.ITaktApproverResolver approverResolver,
            long instanceCreateUserId,
            string[]? runtimeDesignates = null)
        {
            return await approverResolver.ResolveApproversByNodeAsync(
                node, instanceCreateUserId, runtimeDesignates);
        }

        /// <summary>
        /// 驳回节点
        /// </summary>
        /// <param name="rejectNodeId">驳回到的节点ID（如果为空，则根据 NodeRejectType 计算）</param>
        /// <param name="rejectType">驳回类型（如果为空，则使用节点的配置）</param>
        /// <param name="tag">驳回标签</param>
        public void RejectNode(string? rejectNodeId, string? rejectType, Tag tag)
        {
            // 默认驳回到指定节点
            string rejectNode = rejectNodeId ?? string.Empty;

            // 如果不是指定到节点
            if (string.IsNullOrEmpty(rejectNode))
            {
                var currentNode = _nodes[CurrentNodeId];
                if (currentNode.SetInfo != null && string.IsNullOrEmpty(rejectType))
                {
                    rejectType = currentNode.SetInfo.NodeRejectType;
                }

                if (rejectType == TaktWorkflowConstants.REJECT_TYPE_PREVIOUS) // 前一步
                {
                    rejectNode = PreviousNodeId ?? StartNodeId;
                }
                else if (rejectType == TaktWorkflowConstants.REJECT_TYPE_FIRST) // 第一步
                {
                    rejectNode = GetNextNodeId(StartNodeId);
                }
            }

            MakeTagNode(CurrentNodeId, tag);

            if (!string.IsNullOrEmpty(rejectNode))
            {
                PreviousNodeId = CurrentNodeId;
                CurrentNodeId = rejectNode;
                NextNodeId = GetNextNodeId(rejectNode);
            }
        }

        /// <summary>
        /// 撤销流程，清空所有节点
        /// </summary>
        public void ReCall()
        {
            foreach (var item in _nodes.Values)
            {
                item.SetInfo = null;
            }

            PreviousNodeId = CurrentNodeId;
            CurrentNodeId = StartNodeId;
            NextNodeId = GetNextNodeId(StartNodeId);
        }

        /// <summary>
        /// 撤销当前节点的审批
        /// </summary>
        public void UndoVerification()
        {
            // 已结束的流程不能撤销
            // 这个检查应该在外部进行

            if (PreviousNodeId == StartNodeId)
            {
                throw new InvalidOperationException("没有任何审批，不能撤销!你可以删除或召回这个流程");
            }

            // 恢复到上一个节点
            CurrentNodeId = PreviousNodeId ?? StartNodeId;
            PreviousNodeId = GetPreviousNode(CurrentNodeId).Id;
            NextNodeId = GetNextNodeId(CurrentNodeId);

            // 清除当前节点的审批状态
            var currentNode = _nodes[CurrentNodeId];
            if (currentNode.SetInfo != null)
            {
                currentNode.SetInfo.Taged = null;
                currentNode.SetInfo.UserId = string.Empty;
                currentNode.SetInfo.UserName = string.Empty;
                currentNode.SetInfo.Description = string.Empty;
                currentNode.SetInfo.TagedTime = string.Empty;
            }
        }

        #endregion

        #region 属性访问

        /// <summary>
        /// 获取所有节点
        /// </summary>
        public Dictionary<string, TaktFlowNode> Nodes => _nodes;

        /// <summary>
        /// 获取所有连线
        /// </summary>
        public List<TaktFlowLine> Lines => _lines;

        /// <summary>
        /// 获取从节点发出的连线
        /// </summary>
        public Dictionary<string, List<TaktFlowLine>> FromNodeLines => _fromNodeLines;

        /// <summary>
        /// 获取到达节点的连线
        /// </summary>
        public Dictionary<string, List<TaktFlowLine>> ToNodeLines => _toNodeLines;

        #endregion
    }
}

