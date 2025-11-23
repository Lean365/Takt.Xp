//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedGeneratorDictType.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成器字典类型种子数据提供类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 代码生成相关字典类型种子数据提供类
/// </summary>
public class TaktDbSeedGeneratorDictType
{
    /// <summary>
    /// 获取代码生成相关字典类型数据
    /// </summary>
    /// <returns>字典类型数据列表</returns>
    public List<TaktDictType> GetGeneratorDictTypes()
    {
        return new List<TaktDictType>
        {
            new TaktDictType { DictName = "数据库类型", DictType = "gen_db_type", OrderNum = 0, DictStatus=0, Remark = "代码生成数据库类型" },
            new TaktDictType { DictName = "生成代码类型", DictType = "gen_code_type", OrderNum = 1, DictStatus=0, Remark = "代码生成类型（0：前端代码，1：后端代码，2：SQL代码，3：其他）" },
            new TaktDictType { DictName = "生成模板分类", DictType = "gen_template_category", OrderNum = 2, DictStatus=0, Remark = "代码生成模板分类（1：实体类，2：数据访问层，3：服务层，4：控制器，5：API，6：类型，7：视图，8：翻译，9：其他）" },
            new TaktDictType { DictName = "模板类型", DictType = "gen_tpl_type", OrderNum = 3, DictStatus=0, Remark = "代码生成模板类型（0使用wwwroot/Generator/*.scriban模板 1使用TaktGenTemplate数据表中的模板）" },
            new TaktDictType { DictName = "生成模板", DictType = "gen_template_type", OrderNum = 4, DictStatus=0, Remark = "代码生成模板类型" },
            new TaktDictType { DictName = "前端模板", DictType = "gen_frontend_type", OrderNum = 5, DictStatus=0, Remark = "前端模板类型" },
            new TaktDictType { DictName = "生成模块", DictType = "gen_module_name", OrderNum = 6, DictStatus=0, Remark = "代码生成模块名称" },
            new TaktDictType { DictName = "前端布局", DictType = "gen_frontend_style", OrderNum = 7, DictStatus=0, Remark = "前端页面布局" },
            new TaktDictType { DictName = "按钮样式", DictType = "gen_button_style", OrderNum = 8, DictStatus=0, Remark = "按钮样式类型" },
            new TaktDictType { DictName = "生成方式", DictType = "gen_method", OrderNum = 9, DictStatus=0, Remark = "代码生成方式" },
            new TaktDictType { DictName = "生成功能", DictType = "gen_function", OrderNum = 10, DictStatus=0, Remark = "代码生成功能" },
            new TaktDictType { DictName = "树表配置", DictType = "gen_tree_config", OrderNum = 11, DictStatus=0, Remark = "树表配置" },
            new TaktDictType { DictName = "主子表配置", DictType = "gen_sub_config", OrderNum = 12, DictStatus=0, Remark = "主子表配置" },
            new TaktDictType { DictName = "表前缀类型", DictType = "gen_table_prefix", OrderNum = 13, DictStatus=0, Remark = "数据表前缀类型字典" },
            new TaktDictType { DictName = "项目名称", DictType = "gen_project_name", OrderNum = 14, DictStatus=0, Remark = "项目名称" },
            new TaktDictType { DictName = "显示类型", DictType = "gen_display_type", OrderNum = 15, DictStatus=0, Remark = "字段显示类型（文本框、下拉框、日期框等）" },
            new TaktDictType { DictName = "查询类型", DictType = "gen_query_type", OrderNum = 16, DictStatus=0, Remark = "字段查询类型（等于、模糊、范围等）" },
            new TaktDictType { DictName = "数据库字段类型", DictType = "gen_db_column_type", OrderNum = 17, DictStatus=0, Remark = "数据库字段类型" },
            new TaktDictType { DictName = "C#字段类型", DictType = "gen_cs_column_type", OrderNum = 18, DictStatus=0, Remark = "C#字段类型" },
            new TaktDictType { DictName = "编程语言类型", DictType = "gen_programming_language", OrderNum = 19, DictStatus=0, Remark = "代码生成支持的编程语言类型" },
            new TaktDictType { DictName = "前端框架类型", DictType = "gen_frontend_framework", OrderNum = 20, DictStatus=0, Remark = "代码生成支持的前端框架类型" },
            new TaktDictType { DictName = "后端框架类型", DictType = "gen_backend_framework", OrderNum = 21, DictStatus=0, Remark = "代码生成支持的后端框架类型" },
            new TaktDictType { DictName = "ORM框架类型", DictType = "gen_orm_framework", OrderNum = 22, DictStatus=0, Remark = "代码生成支持的ORM框架类型" }
        };
    }
}


