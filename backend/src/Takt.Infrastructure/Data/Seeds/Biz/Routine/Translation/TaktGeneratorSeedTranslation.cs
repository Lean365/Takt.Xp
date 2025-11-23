//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneratorSeedTranslation.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-02-18 11:00
// 版本号 : V0.0.1
// 描述   : 代码生成器本地化资源数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.I18n;

namespace Takt.Infrastructure.Data.Seeds.Biz.Translation;

/// <summary>
/// 代码生成器本地化资源数据提供类
/// </summary>
public class TaktGeneratorSeedTranslation
{
    /// <summary>
    /// 获取代码生成器翻译数据
    /// </summary>
    /// <returns>翻译数据列表</returns>
    public List<TaktTranslation> GetGeneratorTranslations()
    {
        return new List<TaktTranslation>
        {
            // 代码生成器模块
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.module.name", TransValue = "代码生成器", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.module.name", TransValue = "Code Generator", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.module.description", TransValue = "快速生成代码的工具", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.module.description", TransValue = "A tool for quickly generating code", TransType = 1,  },

            // 代码生成器页面
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.page.title", TransValue = "代码生成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.page.title", TransValue = "Code Generation", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.page.description", TransValue = "选择模板并生成代码", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.page.description", TransValue = "Select template and generate code", TransType = 1,  },

            // 代码生成器操作
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.action.generate", TransValue = "生成代码", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.action.generate", TransValue = "Generate Code", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.action.preview", TransValue = "预览代码", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.action.preview", TransValue = "Preview Code", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.action.download", TransValue = "下载代码", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.action.download", TransValue = "Download Code", TransType = 1,  },

            // 代码生成器模板
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.template.entity", TransValue = "实体类模板", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.template.entity", TransValue = "Entity Template", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.template.repository", TransValue = "仓储类模板", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.template.repository", TransValue = "Repository Template", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.template.service", TransValue = "服务类模板", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.template.service", TransValue = "Service Template", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.template.controller", TransValue = "控制器模板", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.template.controller", TransValue = "Controller Template", TransType = 1,  },

            // 代码生成器配置
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.config.namespace", TransValue = "命名空间", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.config.namespace", TransValue = "Namespace", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.config.author", TransValue = "作者", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.config.author", TransValue = "Author", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.config.date", TransValue = "创建日期", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.config.date", TransValue = "Create Date", TransType = 1,  },

            // 代码生成器状态
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.status.generating", TransValue = "正在生成", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.status.generating", TransValue = "Generating", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.status.success", TransValue = "生成成功", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.status.success", TransValue = "Generation Successful", TransType = 1,  },
            new TaktTranslation { LangCode = "zh-CN", TransKey = "generator.status.failed", TransValue = "生成失败", TransType = 1,  },
            new TaktTranslation { LangCode = "en-US", TransKey = "generator.status.failed", TransValue = "Generation Failed", TransType = 1,  }
        };
    }
}



