#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : WorkflowConfigModel.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流配置模型
//===================================================================

namespace Takt.Domain.Models.Workflow
{
  /// <summary>
  /// 工作流配置模型
  /// </summary>
  public class TaktWorkflowConfigModel
  {
    /// <summary>
    /// 流程标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 版本号
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// 初始化编号
    /// </summary>
    public int InitNum { get; set; }

    /// <summary>
    /// 节点列表
    /// </summary>
    public List<TaktWorkflowNodeModel> Nodes { get; set; } = new();

    /// <summary>
    /// 边列表（连线）
    /// </summary>
    public List<TaktWorkflowEdgeModel> Edges { get; set; } = new();
  }

    /// <summary>
    /// 工作流节点模型（用于JSON序列化）
    /// </summary>
    public class TaktWorkflowNodeModel
  {
    /// <summary>
    /// 节点ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 节点类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 节点位置
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// 节点位置
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// 节点宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 节点高度
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 节点配置（JSON对象，需要转换为 SetInfo）
    /// </summary>
    public object? SetInfo { get; set; }
  }

    /// <summary>
    /// 工作流边模型（用于JSON序列化）
    /// </summary>
    public class TaktWorkflowEdgeModel
  {
    /// <summary>
    /// 边ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 源节点ID
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string Target { get; set; } = string.Empty;

    /// <summary>
    /// 边标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 边类型
    /// </summary>
    public string Type { get; set; } = "manual";

    /// <summary>
    /// 转换条件（需要转换为 DataCompare 列表）
    /// </summary>
    public object? Condition { get; set; }

        /// <summary>
        /// 条件比较列表（兼容 OpenAuth.Net 格式）
        /// </summary>
        public List<TaktDataCompare>? Compares { get; set; }
  }
}

