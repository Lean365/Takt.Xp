//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowSeedTranslation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 工作流本地化资源数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 工作流本地化资源数据提供类
/// </summary>
public class TaktWorkflowSeedTranslation
{
    /// <summary>
    /// 获取工作流翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<TaktTranslation> GetWorkflowTranslations()
    {
        return new List<TaktTranslation>
        {
            // 工作流相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.NotFound", TransValue = "工作流不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.NotFound", TransValue = "Workflow not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.NotFound", TransValue = "ワークフローが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.CreateFailed", TransValue = "创建工作流失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.CreateFailed", TransValue = "Failed to create workflow", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.CreateFailed", TransValue = "ワークフローの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.UpdateFailed", TransValue = "更新工作流失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.UpdateFailed", TransValue = "Failed to update workflow", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.UpdateFailed", TransValue = "ワークフローの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.DeleteFailed", TransValue = "删除工作流失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.DeleteFailed", TransValue = "Failed to delete workflow", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.DeleteFailed", TransValue = "ワークフローの削除に失敗しました", TransType = 1,  },

            // 工作流节点相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.NotFound", TransValue = "工作流节点不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Node.NotFound", TransValue = "Workflow node not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.NotFound", TransValue = "ワークフローノードが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.CreateFailed", TransValue = "创建工作流节点失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Node.CreateFailed", TransValue = "Failed to create workflow node", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.CreateFailed", TransValue = "ワークフローノードの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.UpdateFailed", TransValue = "更新工作流节点失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Node.UpdateFailed", TransValue = "Failed to update workflow node", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.UpdateFailed", TransValue = "ワークフローノードの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Node.DeleteFailed", TransValue = "删除工作流节点失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Node.DeleteFailed", TransValue = "Failed to delete workflow node", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Node.DeleteFailed", TransValue = "ワークフローノードの削除に失敗しました", TransType = 1,  },

            // 工作流连线相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.NotFound", TransValue = "工作流连线不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Link.NotFound", TransValue = "Workflow link not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.NotFound", TransValue = "ワークフローリンクが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.CreateFailed", TransValue = "创建工作流连线失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Link.CreateFailed", TransValue = "Failed to create workflow link", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.CreateFailed", TransValue = "ワークフローリンクの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.UpdateFailed", TransValue = "更新工作流连线失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Link.UpdateFailed", TransValue = "Failed to update workflow link", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.UpdateFailed", TransValue = "ワークフローリンクの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Link.DeleteFailed", TransValue = "删除工作流连线失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Link.DeleteFailed", TransValue = "Failed to delete workflow link", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Link.DeleteFailed", TransValue = "ワークフローリンクの削除に失敗しました", TransType = 1,  },

            // 工作流实例相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.NotFound", TransValue = "工作流实例不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.NotFound", TransValue = "Workflow instance not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.NotFound", TransValue = "ワークフローインスタンスが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.CreateFailed", TransValue = "创建工作流实例失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.CreateFailed", TransValue = "Failed to create workflow instance", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.CreateFailed", TransValue = "ワークフローインスタンスの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "更新工作流实例失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "Failed to update workflow instance", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.UpdateFailed", TransValue = "ワークフローインスタンスの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "删除工作流实例失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "Failed to delete workflow instance", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Instance.DeleteFailed", TransValue = "ワークフローインスタンスの削除に失敗しました", TransType = 1,  },

            // 工作流任务相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.NotFound", TransValue = "工作流任务不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Task.NotFound", TransValue = "Workflow task not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.NotFound", TransValue = "ワークフロータスクが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.CreateFailed", TransValue = "创建工作流任务失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Task.CreateFailed", TransValue = "Failed to create workflow task", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.CreateFailed", TransValue = "ワークフロータスクの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.UpdateFailed", TransValue = "更新工作流任务失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Task.UpdateFailed", TransValue = "Failed to update workflow task", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.UpdateFailed", TransValue = "ワークフロータスクの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Task.DeleteFailed", TransValue = "删除工作流任务失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Task.DeleteFailed", TransValue = "Failed to delete workflow task", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Task.DeleteFailed", TransValue = "ワークフロータスクの削除に失敗しました", TransType = 1,  },

            // 工作流日志相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.NotFound", TransValue = "工作流日志不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Log.NotFound", TransValue = "Workflow log not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.NotFound", TransValue = "ワークフローログが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.CreateFailed", TransValue = "创建工作流日志失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Log.CreateFailed", TransValue = "Failed to create workflow log", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.CreateFailed", TransValue = "ワークフローログの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.UpdateFailed", TransValue = "更新工作流日志失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Log.UpdateFailed", TransValue = "Failed to update workflow log", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.UpdateFailed", TransValue = "ワークフローログの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Workflow.Log.DeleteFailed", TransValue = "删除工作流日志失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Workflow.Log.DeleteFailed", TransValue = "Failed to delete workflow log", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Workflow.Log.DeleteFailed", TransValue = "ワークフローログの削除に失敗しました", TransType = 1,  },

            // 工作流定义相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.NotFound", TransValue = "工作流定义不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.NotFound", TransValue = "Workflow definition not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.NotFound", TransValue = "ワークフロー定義が存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.NameExists", TransValue = "工作流定义名称已存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.NameExists", TransValue = "Workflow definition name already exists", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.NameExists", TransValue = "ワークフロー定義名が既に存在します", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.Create.Failed", TransValue = "创建工作流定义失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.Create.Failed", TransValue = "Failed to create workflow definition", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.Create.Failed", TransValue = "ワークフロー定義の作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.Update.Failed", TransValue = "更新工作流定义失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.Update.Failed", TransValue = "Failed to update workflow definition", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.Update.Failed", TransValue = "ワークフロー定義の更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.Delete.Failed", TransValue = "删除工作流定义失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.Delete.Failed", TransValue = "Failed to delete workflow definition", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.Delete.Failed", TransValue = "ワークフロー定義の削除に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.CannotDeleteActive", TransValue = "无法删除已发布的工作流定义", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.CannotDeleteActive", TransValue = "Cannot delete published workflow definition", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.CannotDeleteActive", TransValue = "公開済みのワークフロー定義は削除できません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.BatchDelete.Failed", TransValue = "批量删除工作流定义失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.BatchDelete.Failed", TransValue = "Failed to batch delete workflow definitions", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.BatchDelete.Failed", TransValue = "ワークフロー定義の一括削除に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.UpdateStatus.Failed", TransValue = "更新工作流定义状态失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.UpdateStatus.Failed", TransValue = "Failed to update workflow definition status", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.UpdateStatus.Failed", TransValue = "ワークフロー定義の状態更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.UpgradeVersion.Failed", TransValue = "升级工作流定义版本失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.UpgradeVersion.Failed", TransValue = "Failed to upgrade workflow definition version", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.UpgradeVersion.Failed", TransValue = "ワークフロー定義のバージョンアップグレードに失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.GetOptions.Success", TransValue = "获取工作流定义选项成功，共{0}条", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.GetOptions.Success", TransValue = "Successfully retrieved {0} workflow definition options", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.GetOptions.Success", TransValue = "ワークフロー定義オプションの取得に成功しました。{0}件", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "WorkflowDefinition.GetOptions.Failed", TransValue = "获取工作流定义选项失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "WorkflowDefinition.GetOptions.Failed", TransValue = "Failed to get workflow definition options", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "WorkflowDefinition.GetOptions.Failed", TransValue = "ワークフロー定義オプションの取得に失敗しました", TransType = 1,  }
        };
    }
}



