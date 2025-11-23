//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRoutineSeedTranslation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 日常事务本地化资源数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 日常事务本地化资源数据提供类
/// </summary>
public class TaktRoutineSeedTranslation
{
    /// <summary>
    /// 获取日常事务翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<TaktTranslation> GetRoutineTranslations()
    {
        return new List<TaktTranslation>
        {
            // 日常事务基础操作
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.NotFound", TransValue = "日常事务记录不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.NotFound", TransValue = "Routine record not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.NotFound", TransValue = "日常業務記録が存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.CreateFailed", TransValue = "创建日常事务记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.CreateFailed", TransValue = "Failed to create routine record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.CreateFailed", TransValue = "日常業務記録の作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.UpdateFailed", TransValue = "更新日常事务记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.UpdateFailed", TransValue = "Failed to update routine record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.UpdateFailed", TransValue = "日常業務記録の更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.DeleteFailed", TransValue = "删除日常事务记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.DeleteFailed", TransValue = "Failed to delete routine record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.DeleteFailed", TransValue = "日常業務記録の削除に失敗しました", TransType = 1,  },

            // 日程安排相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.NotFound", TransValue = "日程安排不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.NotFound", TransValue = "Schedule not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.NotFound", TransValue = "スケジュールが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.CreateFailed", TransValue = "创建日程安排失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.CreateFailed", TransValue = "Failed to create schedule", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.CreateFailed", TransValue = "スケジュールの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "更新日程安排失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "Failed to update schedule", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.UpdateFailed", TransValue = "スケジュールの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "删除日程安排失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "Failed to delete schedule", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Schedule.DeleteFailed", TransValue = "スケジュールの削除に失敗しました", TransType = 1,  },

            // 待办事项相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.NotFound", TransValue = "待办事项不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Todo.NotFound", TransValue = "Todo item not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.NotFound", TransValue = "ToDo項目が存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.CreateFailed", TransValue = "创建待办事项失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Todo.CreateFailed", TransValue = "Failed to create todo item", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.CreateFailed", TransValue = "ToDo項目の作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.UpdateFailed", TransValue = "更新待办事项失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Todo.UpdateFailed", TransValue = "Failed to update todo item", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.UpdateFailed", TransValue = "ToDo項目の更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Todo.DeleteFailed", TransValue = "删除待办事项失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Todo.DeleteFailed", TransValue = "Failed to delete todo item", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Todo.DeleteFailed", TransValue = "ToDo項目の削除に失敗しました", TransType = 1,  },

            // 会议管理相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.NotFound", TransValue = "会议记录不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.NotFound", TransValue = "Meeting record not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.NotFound", TransValue = "会議記録が存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.CreateFailed", TransValue = "创建会议记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.CreateFailed", TransValue = "Failed to create meeting record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.CreateFailed", TransValue = "会議記録の作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "更新会议记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "Failed to update meeting record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.UpdateFailed", TransValue = "会議記録の更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "删除会议记录失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "Failed to delete meeting record", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Meeting.DeleteFailed", TransValue = "会議記録の削除に失敗しました", TransType = 1,  },

            // 公告通知相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.NotFound", TransValue = "公告通知不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Notice.NotFound", TransValue = "Notice not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.NotFound", TransValue = "お知らせが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.CreateFailed", TransValue = "创建公告通知失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Notice.CreateFailed", TransValue = "Failed to create notice", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.CreateFailed", TransValue = "お知らせの作成に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.UpdateFailed", TransValue = "更新公告通知失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Notice.UpdateFailed", TransValue = "Failed to update notice", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.UpdateFailed", TransValue = "お知らせの更新に失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.Notice.DeleteFailed", TransValue = "删除公告通知失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.Notice.DeleteFailed", TransValue = "Failed to delete notice", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.Notice.DeleteFailed", TransValue = "お知らせの削除に失敗しました", TransType = 1,  },

            // 文件管理相关
            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.File.NotFound", TransValue = "文件不存在", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.File.NotFound", TransValue = "File not found", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.File.NotFound", TransValue = "ファイルが存在しません", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.File.UploadFailed", TransValue = "文件上传失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.File.UploadFailed", TransValue = "Failed to upload file", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.File.UploadFailed", TransValue = "ファイルのアップロードに失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.File.DownloadFailed", TransValue = "文件下载失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.File.DownloadFailed", TransValue = "Failed to download file", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.File.DownloadFailed", TransValue = "ファイルのダウンロードに失敗しました", TransType = 1,  },

            new TaktTranslation { LangCode = "zh-CN", TransKey = "Routine.File.DeleteFailed", TransValue = "文件删除失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "Routine.File.DeleteFailed", TransValue = "Failed to delete file", TransType = 1,  },
            new TaktTranslation { LangCode = "ja-JP", TransKey = "Routine.File.DeleteFailed", TransValue = "ファイルの削除に失敗しました", TransType = 1,  }
        };
    }
}




