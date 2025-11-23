//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTemplateEngine.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 模板引擎实现，用于代码生成
//===================================================================

using Takt.Application.Services.Generator.CodeGenerator.Models;
using Scriban;
using Scriban.Runtime;

namespace Takt.Application.Services.Generator.CodeGenerator.Templates;

/// <summary>
/// 模板引擎实现
/// 负责模板的编译、渲染和格式化
/// </summary>
public class TaktTemplateEngine : ITaktTemplateEngine
{
    /// <summary>
    /// 模板缓存字典，用于存储已编译的模板
    /// </summary>
    private readonly Dictionary<string, Template> _templateCache;

    /// <summary>
    /// 代码生成配置
    /// </summary>
    private readonly TaktCodeGenerationConfig _config;

    /// <summary>
    /// 日志记录器
    /// </summary>
    private readonly ITaktLogger _logger;

    /// <summary>
    /// 模板目录
    /// </summary>
    private readonly string _templateRoot;

    /// <summary>
    /// 模板类别与路径映射表
    /// </summary>
    private Dictionary<string, Dictionary<string, string>> _templatePathMap;

    /// <summary>
    /// 初始化模板引擎
    /// </summary>
    /// <param name="config">代码生成配置</param>
    /// <param name="logger">日志记录器</param>
    public TaktTemplateEngine(TaktCodeGenerationConfig config, ITaktLogger logger)
    {
        _templateCache = new Dictionary<string, Template>();
        _config = config;
        _logger = logger;
        _templateRoot = Path.Combine(_config.TemplatePaths.WebHostEnvironment.WebRootPath, "Generator");
        InitializeTemplatePathMap();
    }

    /// <summary>
    /// 初始化模板路径映射表
    /// </summary>
    private void InitializeTemplatePathMap()
    {
        _templatePathMap = new Dictionary<string, Dictionary<string, string>>();

        // 获取所有模板文件
        var templateFiles = Directory.GetFiles(_templateRoot, "*.scriban", SearchOption.AllDirectories);

        // 按类别分组
        foreach (var file in templateFiles)
        {
            var relativePath = Path.GetRelativePath(_templateRoot, file).Replace("\\", "/");

            // 判断模板类别
            string category = "";
            if (relativePath.Contains("/Crud/")) category = "crud";
            else if (relativePath.Contains("/MasterDetail/")) category = "sub";
            else if (relativePath.Contains("/Tree/")) category = "tree";
            else continue;

            // 判断模板类型
            string type = "";
            if (relativePath.Contains("/Controllers/")) type = "controller";
            else if (relativePath.Contains("/Entities/")) type = "entity";
            else if (relativePath.Contains("/Dtos/")) type = "dto";
            else if (relativePath.Contains("/Services/") && relativePath.Contains("IService")) type = "iservice";
            else if (relativePath.Contains("/Services/")) type = "service";
            else if (relativePath.Contains("/views/index")) type = "vue";
            else if (relativePath.Contains("/api/")) type = "api";
            else if (relativePath.Contains("/types/")) type = "types";
            else if (relativePath.Contains("/locales/")) type = "locales";
            else if (relativePath.Contains("/components/Detail")) type = "detail";
            else if (relativePath.Contains("/components/Form")) type = "form";
            else continue;

            // 添加到映射表
            if (!_templatePathMap.ContainsKey(category))
            {
                _templatePathMap[category] = new Dictionary<string, string>();
            }
            _templatePathMap[category][type] = relativePath;
        }

        _logger.Debug($"模板路径映射表初始化完成，共 {_templatePathMap.Count} 个类别");
        foreach (var category in _templatePathMap)
        {
            _logger.Debug($"类别 {category.Key} 包含 {category.Value.Count} 个模板");
            foreach (var template in category.Value)
            {
                _logger.Debug($"  - {template.Key}: {template.Value}");
            }
        }
    }

    /// <summary>
    /// 渲染模板
    /// </summary>
    /// <param name="templateName">模板名称</param>
    /// <param name="model">生成模型</param>
    /// <returns>渲染后的代码</returns>
    /// <exception cref="ArgumentNullException">模板或模型为空时抛出</exception>
    /// <exception cref="ArgumentException">模型中的Table或Template为空时抛出</exception>
    /// <exception cref="TaktTemplateException">模板渲染失败时抛出</exception>
    public async Task<string> RenderAsync(string templateName, TaktGeneratorModel model)
    {
        if (string.IsNullOrEmpty(templateName))
            throw new ArgumentNullException(nameof(templateName));
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        if (model.Table == null)
            throw new ArgumentException("模型中的Table属性不能为null", nameof(model));
        if (model.Template == null)
            throw new ArgumentException("模型中的Template属性不能为null", nameof(model));

        _logger.Debug($"开始渲染模板,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},Dto对象:{model.Table.DtoType}");
        try
        {
            // 获取模板内容
            var template = GetTemplateContent(templateName, model.Table.TplCategory, model);
            _logger.Debug($"获取模板内容成功,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},模板长度:{template.Length}");

            // 获取或编译模板
            var compiledTemplate = GetOrCompileTemplate(template);
            _logger.Debug($"模板编译成功,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},模板哈希值:{ComputeHash(template)}");

            // 创建模板上下文
            var context = CreateTemplateContext(model);
            _logger.Debug($"创建模板上下文成功,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName}");

            // 渲染模板
            var result = await compiledTemplate.RenderAsync(context);
            if (compiledTemplate.HasErrors)
            {
                var errors = string.Join("\n", compiledTemplate.Messages.Select(m => m.Message));
                _logger.Error($"模板渲染失败,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},错误信息:\n{errors}");
                throw new TaktTemplateException($"模板渲染失败,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName}:\n{errors}");
            }
            _logger.Debug($"模板渲染成功,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},结果长度:{result.Length}");

            // 格式化代码
            var language = GetLanguageType(model.Template.TemplateLanguage);
            var formattedResult = FormatCode(result, language);
            _logger.Info($"模板渲染和格式化完成,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},语言:{language},结果长度:{formattedResult.Length}");
            return formattedResult;
        }
        catch (Exception ex)
        {
            _logger.Error($"模板渲染失败,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName},错误:{ex.Message},堆栈:{ex.StackTrace}");
            throw new TaktTemplateException($"模板渲染失败,模板名称:{templateName},模板类别:{model.Table.TplCategory},表名:{model.Table.TableName}", ex);
        }
    }

    /// <summary>
    /// 渲染多个模板
    /// </summary>
    /// <param name="templates">模板字典，key为模板名称，value为模板内容</param>
    /// <param name="model">生成模型</param>
    /// <returns>渲染后的代码字典</returns>
    public async Task<Dictionary<string, string>> RenderManyAsync(Dictionary<string, string> templates, TaktGeneratorModel model)
    {
        _logger.Debug($"开始批量渲染模板,模板数量:{templates.Count},表名:{model.Table.TableName}");
        var results = new Dictionary<string, string>();
        try
        {
            foreach (var (key, template) in templates)
            {
                _logger.Debug($"开始渲染模板:{key}");
                results[key] = await RenderAsync(key, model);
                _logger.Debug($"模板:{key}渲染完成");
            }
            _logger.Info($"批量渲染模板完成,成功数量:{results.Count}");
            return results;
        }
        catch (Exception ex)
        {
            _logger.Error($"批量渲染模板失败,表名:{model.Table.TableName}", ex);
            throw;
        }
    }

    /// <summary>
    /// 获取或编译模板
    /// </summary>
    /// <param name="template">模板内容</param>
    /// <returns>编译后的模板</returns>
    private Template GetOrCompileTemplate(string template)
    {
        var templateHash = ComputeHash(template);
        _logger.Debug($"获取或编译模板,模板哈希值:{templateHash}");

        if (!_templateCache.TryGetValue(templateHash, out var compiledTemplate))
        {
            _logger.Debug("模板未缓存,开始编译");
            try
            {
                compiledTemplate = Template.Parse(template);
                if (compiledTemplate.HasErrors)
                {
                    var errors = string.Join("\n", compiledTemplate.Messages.Select(m => m.Message));
                    _logger.Error($"模板解析错误:\n{errors}");
                    throw new TaktTemplateException($"模板解析错误:\n{errors}");
                }
                _templateCache[templateHash] = compiledTemplate;
                _logger.Debug("模板编译完成并已缓存");
            }
            catch (Exception ex)
            {
                _logger.Error($"模板编译失败: {ex.Message}\n模板内容:\n{template}", ex);
                throw new TaktTemplateException($"模板编译失败: {ex.Message}", ex);
            }
        }
        else
        {
            _logger.Debug("使用缓存的模板");
        }

        return compiledTemplate;
    }

    /// <summary>
    /// 创建模板上下文
    /// </summary>
    /// <param name="model">生成模型</param>
    /// <returns>模板上下文</returns>
    private TemplateContext CreateTemplateContext(TaktGeneratorModel model)
    {
        var context = new TemplateContext();
        var scriptObject = new ScriptObject();

        // 添加基本变量
        scriptObject["base_namespace"] = model.Table.BaseNamespace;
        scriptObject["module_name"] = model.Table.GenModuleName;
        scriptObject["author"] = model.Table.GenAuthor;
        scriptObject["date"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        scriptObject["className"] = ToPascalCase(model.Table.TableName);
        scriptObject["table_name"] = model.Table.TableName;
        scriptObject["table_comment"] = model.Table.TableComment ?? model.Table.TableName;

        // 添加表信息
        var tableObject = new ScriptObject();
        tableObject["database_name"] = model.Table.DatabaseName;
        tableObject["table_name"] = model.Table.TableName;
        tableObject["comment"] = model.Table.TableComment;
        tableObject["is_data_table"] = model.Table.IsDataTable;
        tableObject["module_name"] = model.Table.GenModuleName;
        tableObject["module_comment"] = model.Table.TableComment;
        tableObject["sub_table_name"] = model.Table.SubTableName;
        tableObject["sub_table_fk_name"] = model.Table.SubTableFkName;
        tableObject["tree_code"] = model.Table.TreeCode;
        tableObject["tree_name"] = model.Table.TreeName;
        tableObject["tree_parent_code"] = model.Table.TreeParentCode;
        tableObject["tpl_type"] = model.Table.TplType;
        tableObject["tpl_category"] = model.Table.TplCategory;
        tableObject["base_namespace"] = model.Table.BaseNamespace;
        tableObject["entity_namespace"] = model.Table.EntityNamespace;
        tableObject["entity_class_name"] = model.Table.EntityClassName;
        tableObject["dto_namespace"] = model.Table.DtoNamespace;
        tableObject["dto_class_name"] = model.Table.DtoClassName;
        tableObject["dto_type"] = model.Table.DtoType?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        _logger.Debug($"设置DTO类型: {model.Table.DtoType}, 表名: {model.Table.TableName}, 类型: {model.Table.DtoType?.GetType().FullName ?? "null"}");
        tableObject["service_namespace"] = model.Table.ServiceNamespace;
        tableObject["iservice_class_name"] = model.Table.IServiceClassName;
        tableObject["service_class_name"] = model.Table.ServiceClassName;
        tableObject["irepository_namespace"] = model.Table.IRepositoryNamespace;
        tableObject["irepository_class_name"] = model.Table.IRepositoryClassName;
        tableObject["repository_namespace"] = model.Table.RepositoryNamespace;
        tableObject["repository_class_name"] = model.Table.RepositoryClassName;
        tableObject["controller_namespace"] = model.Table.ControllerNamespace;
        tableObject["controller_class_name"] = model.Table.ControllerClassName;
        tableObject["business_name"] = model.Table.GenBusinessName;
        tableObject["function_name"] = model.Table.GenFunctionName;
        tableObject["author"] = model.Table.GenAuthor;
        tableObject["gen_method"] = model.Table.GenMethod;
        tableObject["gen_path"] = model.Table.GenPath;
        tableObject["parent_menu_id"] = model.Table.ParentMenuId;
        tableObject["generate_menu"] = model.Table.IsGenMenu;
        tableObject["generate_translation"] = model.Table.IsGenTranslation;
        tableObject["sort_type"] = model.Table.SortType;
        tableObject["sort_field"] = model.Table.SortField;
        tableObject["perms_prefix"] = model.Table.PermsPrefix;
        tableObject["front_tpl"] = model.Table.FrontTpl;
        tableObject["front_style"] = model.Table.FrontStyle;
        tableObject["btn_style"] = model.Table.BtnStyle;
        tableObject["status"] = model.Table.IsGenCode;
        scriptObject["table"] = tableObject;

        // 添加列信息
        if (model.Columns == null || !model.Columns.Any())
        {
            _logger.Error($"表 {model.Table.TableName} 的列信息为空");
            throw new TaktTemplateException($"表 {model.Table.TableName} 的列信息为空");
        }

        // 定义需要排除的基类字段
        var baseEntityFields = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "id",
            "create_by",
            "create_time",
            "update_by",
            "update_time",
            "is_deleted",
            "delete_by",
            "delete_time",
            "remark"
        };

        // 过滤掉基类字段
        var filteredColumns = model.Columns.Where(c => !baseEntityFields.Contains(c.ColumnName.ToLower())).ToList();

        var columns = filteredColumns.Select(c => new
        {
            column_name = c.ColumnName,
            column_comment = c.ColumnComment,
            property_name = ToPascalCase(c.ColumnName),
            csharp_type = c.DataType,
            data_type = c.PropertyName,
            csharp_field = c.ColumnDataType,
            csharp_length = c.Length,
            csharp_decimal_digits = c.DecimalDigits,
            is_increment = c.IsIncrement == 1,
            is_primary_key = c.IsPrimaryKey == 1,
            is_required = c.IsRequired == 1,
            is_insert = c.IsCreate == 0,
            is_edit = c.IsUpdate == 0,
            is_list = c.IsList == 1,
            is_query = c.IsQuery == 1,
            query_type = c.QueryType,
            is_sort = c.IsSort == 1,
            is_export = c.IsExport == 1,
            display_type = c.DisplayType,
            dict_type = c.DictType,
            order_num = c.OrderNum,
            required_str = c.RequiredStr,
            sort_str = c.SortStr,
            csharp_field_fl = c.CsharpFieldFl,
            disabled_str = c.DisabledStr
        }).ToList();

        tableObject["columns"] = new ScriptArray(columns);

        // 添加辅助函数
        scriptObject.Import(typeof(string));
        scriptObject.Import(typeof(DateTime));
        scriptObject.Import(typeof(Math));
        scriptObject.Import(typeof(System.Text.StringBuilder));
        scriptObject.Import(typeof(System.Collections.Generic.List<>));
        scriptObject.Import(typeof(System.Linq.Enumerable));

        // 添加自定义辅助函数
        var functions = new ScriptObject();
        functions.Import("pascal_case", (string s) => ToPascalCase(s));
        functions.Import("camel_case", (string s) => ToCamelCase(s));
        functions.Import("snake_case", (string s) => ToSnakeCase(s));
        functions.Import("kebab_case", (string s) => ToKebabCase(s));
        functions.Import("plural", (string s) => ToPlural(s));
        functions.Import("singular", (string s) => ToSingular(s));
        functions.Import("is_nullable_type", (string s) => IsNullableType(s));
        functions.Import("get_default_value", (string s) => GetDefaultValue(s));
        functions.Import("generate_xml_summary", (string s) => GenerateXmlSummary(s));
        functions.Import("generate_jsdoc", (string s) => GenerateJsDoc(s));
        functions.Import("generate_javadoc", (string s) => GenerateJavaDoc(s));
        functions.Import("to_string", (object o) => o?.ToString() ?? string.Empty);
        functions.Import("to_csharp_type", (string s) => DbTypeToCSharpType(s));

        // 添加字符串处理函数
        functions.Import("to_lower", (string s) => s?.ToLower() ?? string.Empty);
        functions.Import("to_upper", (string s) => s?.ToUpper() ?? string.Empty);
        functions.Import("capitalize", (string s) => s?.Length > 0 ? char.ToUpper(s[0]) + s.Substring(1).ToLower() : string.Empty);
        functions.Import("join", (string separator, IEnumerable<object> values) => string.Join(separator, values));
        functions.Import("split", (string s, string separator) => s?.Split(separator) ?? Array.Empty<string>());
        functions.Import("trim", (string s) => s?.Trim() ?? string.Empty);
        functions.Import("trim_start", (string s) => s?.TrimStart() ?? string.Empty);
        functions.Import("trim_end", (string s) => s?.TrimEnd() ?? string.Empty);
        functions.Import("replace", (string s, string oldValue, string newValue) => s?.Replace(oldValue, newValue) ?? string.Empty);
        functions.Import("contains", (string s, string value) => s?.Contains(value) ?? false);
        functions.Import("starts_with", (string s, string value) => s?.StartsWith(value) ?? false);
        functions.Import("ends_with", (string s, string value) => s?.EndsWith(value) ?? false);
        functions.Import("substring", (string s, int startIndex, int length) => s?.Substring(startIndex, length) ?? string.Empty);
        functions.Import("pad_left", (string s, int totalWidth, char paddingChar) => s?.PadLeft(totalWidth, paddingChar) ?? string.Empty);
        functions.Import("pad_right", (string s, int totalWidth, char paddingChar) => s?.PadRight(totalWidth, paddingChar) ?? string.Empty);
        functions.Import("is_null_or_empty", (string s) => string.IsNullOrEmpty(s));
        functions.Import("is_null_or_white_space", (string s) => string.IsNullOrWhiteSpace(s));

        // 添加全局函数
        scriptObject.Import(functions);
        scriptObject["string"] = functions; // 添加 string 命名空间

        // 添加自定义函数
        scriptObject["to_pascal_case"] = new Func<string, string>(ToPascalCase);
        scriptObject["to_camel_case"] = new Func<string, string>(ToCamelCase);
        scriptObject["to_snake_case"] = new Func<string, string>(ToSnakeCase);
        scriptObject["to_kebab_case"] = new Func<string, string>(ToKebabCase);

        context.PushGlobal(scriptObject);
        return context;
    }

    private string GetCSharpType(string sqlType)
    {
        return sqlType.ToLower() switch
        {
            "int" => "int",
            "bigint" => "long",
            "decimal" => "decimal",
            "float" => "float",
            "double" => "double",
            "bit" => "bool",
            "datetime" => "DateTime",
            "date" => "DateTime",
            "time" => "TimeSpan",
            "char" => "string",
            "varchar" => "string",
            "nvarchar" => "string",
            "text" => "string",
            "ntext" => "string",
            "uniqueidentifier" => "Guid",
            _ => "string"
        };
    }

    /// <summary>
    /// 计算模板哈希值
    /// </summary>
    /// <param name="template">模板内容</param>
    /// <returns>哈希值</returns>
    private string ComputeHash(string template)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(template);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// 获取语言类型
    /// </summary>
    /// <param name="templateLanguage">模板语言类型</param>
    /// <returns>语言类型字符串</returns>
    private string GetLanguageType(int templateLanguage)
    {
        return templateLanguage switch
        {
            1 => "csharp",
            2 => "typescript",
            3 => "javascript",
            4 => "java",
            5 => "vue",
            6 => "html",
            7 => "css",
            8 => "sql",
            _ => "csharp"
        };
    }

    #region 字符串转换辅助方法

    /// <summary>
    /// 转换为Pascal命名法
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Pascal命名法字符串</returns>
    private string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
    }

    /// <summary>
    /// 转换为Camel命名法
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Camel命名法字符串</returns>
    private string ToCamelCase(string input)
    {
        var pascal = ToPascalCase(input);
        return pascal.Length > 0 ? char.ToLower(pascal[0]) + pascal.Substring(1) : pascal;
    }

    /// <summary>
    /// 转换为Snake命名法
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Snake命名法字符串</returns>
    private string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        return string.Concat(input.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    /// <summary>
    /// 转换为Kebab命名法
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Kebab命名法字符串</returns>
    private string ToKebabCase(string input)
    {
        return ToSnakeCase(input).Replace('_', '-');
    }

    #endregion 字符串转换辅助方法

    #region 类型转换函数

    /// <summary>
    /// 数据库类型转换为C#类型
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    /// <returns>C#类型</returns>
    private string DbTypeToCSharpType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "int",
            "bigint" => "long",
            "varchar" => "string",
            "nvarchar" => "string",
            "datetime" => "DateTime",
            "bit" => "bool",
            "decimal" => "decimal",
            "float" => "float",
            "double" => "double",
            _ => "object"
        };
    }

    /// <summary>
    /// 数据库类型转换为TypeScript类型
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    /// <returns>TypeScript类型</returns>
    private string DbTypeToTypeScriptType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "number",
            "bigint" => "number",
            "varchar" => "string",
            "nvarchar" => "string",
            "datetime" => "Date",
            "bit" => "boolean",
            "decimal" => "number",
            "float" => "number",
            "double" => "number",
            _ => "any"
        };
    }

    /// <summary>
    /// 数据库类型转换为Java类型
    /// </summary>
    /// <param name="dbType">数据库类型</param>
    /// <returns>Java类型</returns>
    private string DbTypeToJavaType(string dbType)
    {
        return dbType.ToLower() switch
        {
            "int" => "Integer",
            "bigint" => "Long",
            "varchar" => "String",
            "nvarchar" => "String",
            "datetime" => "Date",
            "bit" => "Boolean",
            "decimal" => "BigDecimal",
            "float" => "Float",
            "double" => "Double",
            _ => "Object"
        };
    }

    #endregion 类型转换函数

    #region 注释生成函数

    /// <summary>
    /// 生成XML注释
    /// </summary>
    /// <param name="description">描述文本</param>
    /// <returns>XML注释</returns>
    private string GenerateXmlSummary(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/// <summary>
/// {description}
/// </summary>";
    }

    /// <summary>
    /// 生成JSDoc注释
    /// </summary>
    /// <param name="description">描述文本</param>
    /// <returns>JSDoc注释</returns>
    private string GenerateJsDoc(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/**
 * {description}
 */";
    }

    /// <summary>
    /// 生成JavaDoc注释
    /// </summary>
    /// <param name="description">描述文本</param>
    /// <returns>JavaDoc注释</returns>
    private string GenerateJavaDoc(string description)
    {
        if (string.IsNullOrEmpty(description)) return string.Empty;
        return $@"/**
 * {description}
 */";
    }

    #endregion 注释生成函数

    #region 代码生成辅助函数

    /// <summary>
    /// 转换为复数形式
    /// </summary>
    /// <param name="word">单词</param>
    /// <returns>复数形式</returns>
    private string ToPlural(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return word.EndsWith("y") ? word[..^1] + "ies" :
               word.EndsWith("s") ? word :
               word + "s";
    }

    /// <summary>
    /// 转换为单数形式
    /// </summary>
    /// <param name="word">单词</param>
    /// <returns>单数形式</returns>
    private string ToSingular(string word)
    {
        if (string.IsNullOrEmpty(word)) return word;
        return word.EndsWith("ies") ? word[..^3] + "y" :
               word.EndsWith("s") ? word[..^1] :
               word;
    }

    /// <summary>
    /// 判断是否为可空类型
    /// </summary>
    /// <param name="type">类型名称</param>
    /// <returns>是否为可空类型</returns>
    private bool IsNullableType(string type)
    {
        return type.EndsWith("?") || type == "string";
    }

    /// <summary>
    /// 获取默认值
    /// </summary>
    /// <param name="type">类型名称</param>
    /// <returns>默认值字符串</returns>
    private string GetDefaultValue(string type)
    {
        return type.ToLower() switch
        {
            "int" => "0",
            "long" => "0L",
            "decimal" => "0M",
            "float" => "0F",
            "double" => "0D",
            "bool" => "false",
            "datetime" => "DateTime.MinValue",
            "string" => "string.Empty",
            _ => "null"
        };
    }

    #endregion 代码生成辅助函数

    #region 代码格式化

    /// <summary>
    /// 格式化代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <param name="language">语言类型</param>
    /// <returns>格式化后的代码</returns>
    private string FormatCode(string code, string language)
    {
        _logger.Debug($"开始格式化代码,语言:{language},代码长度:{code.Length}");
        try
        {
            var formattedCode = language.ToLower() switch
            {
                "csharp" => FormatCSharpCode(code),
                "typescript" => FormatTypeScriptCode(code),
                "javascript" => FormatJavaScriptCode(code),
                "java" => FormatJavaCode(code),
                "vue" => FormatVueCode(code),
                "html" => FormatHtmlCode(code),
                "css" => FormatCssCode(code),
                "sql" => FormatSqlCode(code),
                _ => code
            };

            _logger.Debug($"代码格式化完成,结果长度:{formattedCode.Length}");
            return formattedCode;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码格式化失败,语言:{language}", ex);
            return code;
        }
    }

    /// <summary>
    /// 格式化C#代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatCSharpCode(string code)
    {
        // 基本的C#代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 4)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("["))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 格式化TypeScript代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatTypeScriptCode(string code)
    {
        // TypeScript代码格式化
        return FormatJavaScriptCode(code); // TypeScript使用与JavaScript相同的格式化规则
    }

    /// <summary>
    /// 格式化JavaScript代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatJavaScriptCode(string code)
    {
        // JavaScript代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("["))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 格式化Java代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatJavaCode(string code)
    {
        // Java代码格式化
        return FormatCSharpCode(code); // Java使用与C#相同的格式化规则
    }

    /// <summary>
    /// 格式化Vue代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatVueCode(string code)
    {
        // Vue代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();
        var inScript = false;
        var inStyle = false;
        var inTemplate = false;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 检测区域
            if (trimmedLine.StartsWith("<script"))
            {
                inScript = true;
                inStyle = false;
                inTemplate = false;
            }
            else if (trimmedLine.StartsWith("<style"))
            {
                inScript = false;
                inStyle = true;
                inTemplate = false;
            }
            else if (trimmedLine.StartsWith("<template"))
            {
                inScript = false;
                inStyle = false;
                inTemplate = true;
            }

            // 根据不同区域使用不同的缩进规则
            if (inScript || inStyle)
            {
                // 减少缩进级别
                if (trimmedLine.StartsWith("}") || trimmedLine.StartsWith("]") || trimmedLine.StartsWith("</"))
                {
                    indentLevel = Math.Max(0, indentLevel - 1);
                }

                // 添加当前行
                if (!string.IsNullOrWhiteSpace(trimmedLine))
                {
                    result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
                }
                else
                {
                    result.AppendLine();
                }

                // 增加缩进级别
                if (trimmedLine.EndsWith("{") || trimmedLine.EndsWith("[") || trimmedLine.EndsWith(">"))
                {
                    indentLevel++;
                }
            }
            else
            {
                // template区域使用2空格缩进
                if (trimmedLine.StartsWith("</"))
                {
                    indentLevel = Math.Max(0, indentLevel - 1);
                }

                if (!string.IsNullOrWhiteSpace(trimmedLine))
                {
                    result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
                }
                else
                {
                    result.AppendLine();
                }

                if (trimmedLine.EndsWith(">") && !trimmedLine.StartsWith("</") && !trimmedLine.Contains("/>"))
                {
                    indentLevel++;
                }
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 格式化HTML代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatHtmlCode(string code)
    {
        // HTML代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("</"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith(">") && !trimmedLine.StartsWith("</") && !trimmedLine.Contains("/>"))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 格式化CSS代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatCssCode(string code)
    {
        // CSS代码格式化
        var lines = code.Split('\n');
        var indentLevel = 0;
        var result = new System.Text.StringBuilder();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            // 减少缩进级别
            if (trimmedLine.StartsWith("}"))
            {
                indentLevel = Math.Max(0, indentLevel - 1);
            }

            // 添加当前行
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                result.AppendLine($"{new string(' ', indentLevel * 2)}{trimmedLine}");
            }
            else
            {
                result.AppendLine();
            }

            // 增加缩进级别
            if (trimmedLine.EndsWith("{"))
            {
                indentLevel++;
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// 格式化SQL代码
    /// </summary>
    /// <param name="code">代码内容</param>
    /// <returns>格式化后的代码</returns>
    private string FormatSqlCode(string code)
    {
        // SQL代码格式化
        var lines = code.Split('\n');
        var result = new System.Text.StringBuilder();
        var keywords = new[] { "SELECT", "FROM", "WHERE", "GROUP BY", "ORDER BY", "HAVING", "JOIN", "LEFT JOIN", "RIGHT JOIN", "INNER JOIN", "UPDATE", "DELETE", "INSERT", "VALUES" };

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();

            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                // 主要关键字新起一行
                var isKeywordLine = keywords.Any(k => trimmedLine.ToUpper().StartsWith(k));
                if (isKeywordLine)
                {
                    result.AppendLine();
                    result.AppendLine(trimmedLine);
                }
                else
                {
                    result.AppendLine($"    {trimmedLine}");
                }
            }
            else
            {
                result.AppendLine();
            }
        }

        return result.ToString();
    }

    #endregion 代码格式化

    /// <summary>
    /// 获取模板内容
    /// </summary>
    /// <param name="templateName">模板名称</param>
    /// <param name="tplCategory">模板类别</param>
    /// <param name="model">生成模型</param>
    /// <returns>模板内容</returns>
    private string GetTemplateContent(string templateName, string tplCategory, TaktGeneratorModel model)
    {
        if (_config.TemplatePaths == null)
            throw new TaktTemplateException("模板路径配置为空");
        if (_config.TemplatePaths.WebHostEnvironment == null)
            throw new TaktTemplateException("WebHostEnvironment 未配置");

        string tplType = model.Table.TplType?.Trim() ?? "0";
        string category = tplCategory?.Trim().ToLower() ?? "crud";
        string name = templateName?.Trim().ToLower();
        _logger.Debug($"获取模板内容,模板类型:{tplType},模板类别:{category},模板名称:{name},表名:{model.Table.TableName}");

        if (tplType == "1")
        {
            var template = _config.Templates?.FirstOrDefault(t =>
                t.TemplateName.Equals(templateName, StringComparison.OrdinalIgnoreCase) &&
                t.TemplateCategory.ToString().Equals(tplCategory, StringComparison.OrdinalIgnoreCase));
            if (template == null)
            {
                _logger.Error($"未找到数据库模板,模板名称:{templateName},模板类别:{tplCategory},表名:{model.Table.TableName}");
                throw new TaktTemplateException($"未找到数据库模板: {templateName}, 类别: {tplCategory}");
            }
            _logger.Debug($"从数据库获取模板成功,模板名称:{templateName},模板类别:{tplCategory},表名:{model.Table.TableName}");
            return template.TemplateContent;
        }
        else
        {
            // 对于 locales 模板，使用文件名作为键
            if (name == "locales")
            {
                name = "zh-CN"; // 默认使用中文模板
            }

            if (!_templatePathMap.TryGetValue(category, out var nameMap) || !nameMap.TryGetValue(name, out var relPath))
            {
                _logger.Error($"模板映射表查找失败,模板类别:{category},模板名称:{name},表名:{model.Table.TableName}");
                _logger.Error($"当前模板映射表内容: {string.Join(", ", _templatePathMap.Select(kv => $"{kv.Key}: [{string.Join(", ", kv.Value.Select(t => $"{t.Key}={t.Value}"))}]"))}");
                throw new TaktTemplateException($"不支持的模板类别或名称: {category}/{name}");
            }

            var templatePath = Path.Combine(_templateRoot, relPath);
            _logger.Debug($"模板文件路径: {templatePath},表名:{model.Table.TableName}");

            if (!File.Exists(templatePath))
            {
                _logger.Error($"模板文件不存在,路径:{templatePath},表名:{model.Table.TableName}");
                throw new TaktTemplateException($"模板文件不存在: {templatePath}");
            }

            var content = File.ReadAllText(templatePath);
            if (string.IsNullOrEmpty(content))
            {
                _logger.Error($"模板内容为空,路径:{templatePath},表名:{model.Table.TableName}");
                throw new TaktTemplateException($"模板内容为空: {templatePath}");
            }

            if (!content.Contains("{{") || !content.Contains("}}"))
            {
                _logger.Error($"模板内容格式不正确,路径:{templatePath},表名:{model.Table.TableName}");
                throw new TaktTemplateException($"模板内容格式不正确: {templatePath}");
            }

            _logger.Debug($"从文件系统获取模板成功,路径:{templatePath},表名:{model.Table.TableName},内容长度:{content.Length}");
            return content;
        }
    }
}

/// <summary>
/// 模板异常
/// </summary>
public class TaktTemplateException : Exception
{
    /// <summary>
    /// 初始化模板异常
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public TaktTemplateException(string message, Exception innerException = null)
        : base(message, innerException)
    {
    }
}



