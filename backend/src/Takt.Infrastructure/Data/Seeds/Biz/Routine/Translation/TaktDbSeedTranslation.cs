//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedTranslation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 翻译数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 翻译数据提供类
/// </summary>
public class TaktDbSeedTranslation
{
    /// <summary>
    /// 获取基础翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<TaktTranslation> GetDefaultTranslations()
    {
        return new List<TaktTranslation>
        {
            // 通用操作消息 - 根据实际代码中使用的翻译键
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.AddSuccess", TransValue = "添加成功", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.AddFailed", TransValue = "添加失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.UpdateSuccess", TransValue = "更新成功", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.UpdateFailed", TransValue = "更新失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.DeleteSuccess", TransValue = "删除成功", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.DeleteFailed", TransValue = "删除失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.NotExists", TransValue = "不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.BatchDeleteIdsRequired", TransValue = "批量删除ID不能为空", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.SomeRecordsNotFound", TransValue = "部分记录不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.FileRequired", TransValue = "文件不能为空", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.ImportFailed", TransValue = "导入失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.ExportFailed", TransValue = "导出失败", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Common.GetTemplateFailed", TransValue = "获取模板失败", TransType = 1,  },

            new TaktTranslation { LangCode = "en-US", TransKey = "Common.AddSuccess", TransValue = "Add successful", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.AddFailed", TransValue = "Add failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.UpdateSuccess", TransValue = "Update successful", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.UpdateFailed", TransValue = "Update failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.DeleteSuccess", TransValue = "Delete successful", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.DeleteFailed", TransValue = "Delete failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.NotExists", TransValue = "Not exists", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.BatchDeleteIdsRequired", TransValue = "Batch delete IDs required", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.SomeRecordsNotFound", TransValue = "Some records not found", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.FileRequired", TransValue = "File required", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.ImportFailed", TransValue = "Import failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.ExportFailed", TransValue = "Export failed", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Common.GetTemplateFailed", TransValue = "Get template failed", TransType = 1,  },

            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.AddSuccess", TransValue = "追加成功", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.AddFailed", TransValue = "追加失敗", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.UpdateSuccess", TransValue = "更新成功", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.UpdateFailed", TransValue = "更新失敗", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.DeleteSuccess", TransValue = "削除成功", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.DeleteFailed", TransValue = "削除失敗", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.NotExists", TransValue = "存在しません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.BatchDeleteIdsRequired", TransValue = "バッチ削除IDが必要です", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.SomeRecordsNotFound", TransValue = "一部のレコードが見つかりません", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.FileRequired", TransValue = "ファイルが必要です", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.ImportFailed", TransValue = "インポートに失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.ExportFailed", TransValue = "エクスポートに失敗しました", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Common.GetTemplateFailed", TransValue = "テンプレートの取得に失敗しました", TransType = 1,  }
        };
    }
}

