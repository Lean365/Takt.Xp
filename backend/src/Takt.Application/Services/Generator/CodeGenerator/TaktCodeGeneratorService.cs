using Takt.Application.Services.Generator.CodeGenerator.Models;
using Takt.Application.Services.Generator.CodeGenerator.Templates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO.Compression;

namespace Takt.Application.Services.Generator.CodeGenerator;

/// <summary>
/// 代码生成服务实现
/// </summary>
public class TaktCodeGeneratorService : ITaktCodeGeneratorService
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktTemplateEngine _templateEngine;
    private readonly ITaktLogger _logger;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private ITaktRepository<TaktGenTable> TableRepository => _repositoryFactory.GetAuthRepository<TaktGenTable>();
    private ITaktRepository<TaktGenColumn> ColumnRepository => _repositoryFactory.GetAuthRepository<TaktGenColumn>();
    private ITaktRepository<TaktGenConfig> ConfigRepository => _repositoryFactory.GetAuthRepository<TaktGenConfig>();
    private ITaktRepository<TaktGenTemplate> TemplateRepository => _repositoryFactory.GetAuthRepository<TaktGenTemplate>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktCodeGeneratorService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktTemplateEngine templateEngine,
        ITaktLogger logger,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _templateEngine = templateEngine;
        _logger = logger;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    public async Task<bool> GenerateCodeAsync(TaktGenTable table)
    {
        try
        {
            _logger.Info($"开始生成代码,表名:{table.TableName}");

            // 获取表信息
            var genTable = await TableRepository.GetFirstAsync(x => x.TableName == table.TableName && x.IsGenCode == 0);
            if (genTable == null)
            {
                _logger.Error($"未找到表 {table.TableName} 的生成信息");
                throw new TaktException($"未找到表 {table.TableName} 的生成信息，请先在代码生成表中设置该表的信息");
            }

            // 获取列信息
            var columns = await ColumnRepository.GetListAsync(x => x.TableId == genTable.Id);
            if (columns == null || !columns.Any())
            {
                _logger.Error($"未找到表 {table.TableName} 的列信息");
                throw new TaktException($"未找到表 {table.TableName} 的列信息，请先设置表的列信息");
            }

            // 获取生成配置
            var config = await ConfigRepository.GetFirstAsync(x => x.Status == 0);
            if (config == null)
            {
                _logger.Error("未找到可用的代码生成配置");
                throw new TaktException("未找到可用的代码生成配置，请先配置代码生成参数");
            }

            // 获取模板列表
            List<TaktGenTemplate> templates;
            if (config.GenTplType == 0)
            {
                // 使用 wwwroot/Generator/*.scriban 模板
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Generator");
                if (!Directory.Exists(templatePath))
                {
                    _logger.Error($"模板目录不存在: {templatePath}");
                    throw new TaktException("模板目录不存在，请确保 wwwroot/Generator 目录存在");
                }

                var templateFiles = Directory.GetFiles(templatePath, "*.scriban", SearchOption.AllDirectories);
                if (!templateFiles.Any())
                {
                    _logger.Error("未找到可用的代码生成模板文件");
                    throw new TaktException("未找到可用的代码生成模板文件，请确保 wwwroot/Generator 目录下有 .scriban 文件");
                }

                templates = templateFiles.Select(file => new TaktGenTemplate
                {
                    TemplateName = Path.GetFileNameWithoutExtension(file),
                    FileName = Path.GetFileName(file),
                    TemplateContent = File.ReadAllText(file),
                    TemplateLanguage = GetTemplateLanguage(file),
                    Status = 0
                }).ToList();
            }
            else
            {
                // 使用 TaktGenTemplate 表中的模板
                templates = await TemplateRepository.GetListAsync(x => x.Status == 0);
                if (templates == null || !templates.Any())
                {
                    _logger.Error("未找到可用的代码生成模板");
                    throw new TaktException("未找到可用的代码生成模板，请先配置代码生成模板");
                }
            }

            // 准备生成模型
            var model = new TaktGeneratorModel
            {
                Table = genTable,
                Template = templates.First(), // 设置默认模板
                Config = new TaktCodeGenerationConfig(_webHostEnvironment)
                {
                    Author = config.Author,
                    ModuleName = config.ModuleName,
                    ProjectName = config.ProjectName,
                    BaseNamespace = config.ProjectName,
                    GenPath = config.GenPath,
                    Templates = templates.ToList()
                },
                Columns = columns.ToList(), // 添加列信息
            };

            // 创建生成选项
            var options = new Models.TaktGeneratorOptions
            {
                Author = config.Author,
                ModuleName = config.ModuleName,
                ProjectName = config.ProjectName,
                BaseNamespace = config.ProjectName,
                GenPath = config.GenPath,
                GenerateController = true,
                GenerateService = true,
                GenerateRepository = true,
                GenerateEntity = true,
                GenerateDto = true,
                GenerateFrontend = true,
                GenerateApiDoc = true,
                IsTree = genTable.TplCategory == "tree",
                IsMasterDetail = genTable.TplCategory == "sub",
                GenFunction = genTable.GenFunction,
                IsSqlDiff = genTable.IsSqlDiff,
                IsSnowflakeId = genTable.IsSnowflakeId,
                IsRepository = genTable.IsRepository
            };

            // 解析选项配置
            if (!string.IsNullOrEmpty(config.Options))
            {
                var optionsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(config.Options);
                if (optionsDict != null)
                {
                    // 设置生成选项
                    if (optionsDict.ContainsKey("isTree"))
                    {
                        options.IsTree = Convert.ToBoolean(optionsDict["isTree"]);
                    }
                    if (optionsDict.ContainsKey("isMasterDetail"))
                    {
                        options.IsMasterDetail = Convert.ToBoolean(optionsDict["isMasterDetail"]);
                    }
                    if (optionsDict.ContainsKey("generateController"))
                    {
                        options.GenerateController = Convert.ToBoolean(optionsDict["generateController"]);
                    }
                    if (optionsDict.ContainsKey("generateService"))
                    {
                        options.GenerateService = Convert.ToBoolean(optionsDict["generateService"]);
                    }
                    if (optionsDict.ContainsKey("generateRepository"))
                    {
                        options.GenerateRepository = Convert.ToBoolean(optionsDict["generateRepository"]);
                    }
                    if (optionsDict.ContainsKey("generateEntity"))
                    {
                        options.GenerateEntity = Convert.ToBoolean(optionsDict["generateEntity"]);
                    }
                    if (optionsDict.ContainsKey("generateDto"))
                    {
                        options.GenerateDto = Convert.ToBoolean(optionsDict["generateDto"]);
                    }
                    if (optionsDict.ContainsKey("generateFrontend"))
                    {
                        options.GenerateFrontend = Convert.ToBoolean(optionsDict["generateFrontend"]);
                    }
                    if (optionsDict.ContainsKey("generateApiDoc"))
                    {
                        options.GenerateApiDoc = Convert.ToBoolean(optionsDict["generateApiDoc"]);
                    }
                    // 设置新增的生成选项
                    if (optionsDict.ContainsKey("genFunction"))
                    {
                        options.GenFunction = optionsDict["genFunction"]?.ToString() ?? "1,2,3,4,5,6,7";
                    }
                    if (optionsDict.ContainsKey("isSqlDiff"))
                    {
                        options.IsSqlDiff = Convert.ToInt32(optionsDict["isSqlDiff"]);
                    }
                    if (optionsDict.ContainsKey("isSnowflakeId"))
                    {
                        options.IsSnowflakeId = Convert.ToInt32(optionsDict["isSnowflakeId"]);
                    }
                    if (optionsDict.ContainsKey("isRepository"))
                    {
                        options.IsRepository = Convert.ToInt32(optionsDict["isRepository"]);
                    }
                }
            }

            model.Options = options;

            // 生成代码
            var result = await GenerateCodeInternalAsync(model);
            if (!result)
            {
                _logger.Error($"代码生成失败,表名:{table.TableName}");
                throw new TaktException($"代码生成失败，请检查配置和模板是否正确");
            }

            _logger.Info($"代码生成成功,表名:{table.TableName}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码生成失败,表名:{table.TableName},错误:{ex.Message}");
            throw new TaktException($"代码生成失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 预览代码
    /// </summary>
    public async Task<Dictionary<string, string>> PreviewCodeAsync(TaktGenTable table)
    {
        try
        {
            _logger.Info($"开始预览代码,表名:{table.TableName}");

            // 获取表信息
            var genTable = await TableRepository.GetFirstAsync(x => x.TableName == table.TableName && x.IsGenCode == 0);
            if (genTable == null)
            {
                _logger.Error($"未找到表 {table.TableName} 的生成信息");
                throw new TaktException($"未找到表 {table.TableName} 的生成信息，请先在代码生成表中设置该表的信息");
            }

            // 获取列信息
            var columns = await ColumnRepository.GetListAsync(x => x.TableId == genTable.Id);
            if (columns == null || !columns.Any())
            {
                _logger.Error($"未找到表 {table.TableName} 的列信息");
                throw new TaktException($"未找到表 {table.TableName} 的列信息，请先设置表的列信息");
            }

            // 获取生成配置
            var config = await ConfigRepository.GetFirstAsync(x => x.Status == 0);
            if (config == null)
            {
                _logger.Error("未找到可用的代码生成配置");
                throw new TaktException("未找到可用的代码生成配置，请先配置代码生成参数");
            }

            // 获取模板列表
            List<TaktGenTemplate> templates;
            if (config.GenTplType == 0)
            {
                // 使用 wwwroot/Generator/*.scriban 模板
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Generator");
                if (!Directory.Exists(templatePath))
                {
                    _logger.Error($"模板目录不存在: {templatePath}");
                    throw new TaktException("模板目录不存在，请确保 wwwroot/Generator 目录存在");
                }

                var templateFiles = Directory.GetFiles(templatePath, "*.scriban", SearchOption.AllDirectories);
                if (!templateFiles.Any())
                {
                    _logger.Error("未找到可用的代码生成模板文件");
                    throw new TaktException("未找到可用的代码生成模板文件，请确保 wwwroot/Generator 目录下有 .scriban 文件");
                }

                templates = templateFiles.Select(file => new TaktGenTemplate
                {
                    TemplateName = Path.GetFileNameWithoutExtension(file),
                    FileName = Path.GetFileName(file),
                    TemplateContent = File.ReadAllText(file),
                    TemplateLanguage = GetTemplateLanguage(file),
                    Status = 0
                }).ToList();
            }
            else
            {
                // 使用 TaktGenTemplate 表中的模板
                templates = await TemplateRepository.GetListAsync(x => x.Status == 0);
                if (templates == null || !templates.Any())
                {
                    _logger.Error("未找到可用的代码生成模板");
                    throw new TaktException("未找到可用的代码生成模板，请先配置代码生成模板");
                }
            }

            // 准备生成模型
            var model = new TaktGeneratorModel
            {
                Table = genTable,
                Template = templates.First(), // 设置默认模板
                Config = new TaktCodeGenerationConfig(_webHostEnvironment)
                {
                    Author = config.Author,
                    ModuleName = config.ModuleName,
                    ProjectName = config.ProjectName,
                    BaseNamespace = config.ProjectName,
                    GenPath = config.GenPath,
                    Templates = templates.ToList()
                },
                Columns = columns.ToList(), // 添加列信息
            };

            // 创建生成选项
            var options = new Models.TaktGeneratorOptions
            {
                Author = config.Author,
                ModuleName = config.ModuleName,
                ProjectName = config.ProjectName,
                BaseNamespace = config.ProjectName,
                GenPath = config.GenPath,
                GenerateController = true,
                GenerateService = true,
                GenerateRepository = true,
                GenerateEntity = true,
                GenerateDto = true,
                GenerateFrontend = true,
                GenerateApiDoc = true,
                IsTree = genTable.TplCategory == "tree",
                IsMasterDetail = genTable.TplCategory == "sub"
            };

            // 解析选项配置
            if (!string.IsNullOrEmpty(config.Options))
            {
                var optionsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(config.Options);
                if (optionsDict != null)
                {
                    // 设置生成选项
                    if (optionsDict.ContainsKey("isTree"))
                    {
                        options.IsTree = Convert.ToBoolean(optionsDict["isTree"]);
                    }
                    if (optionsDict.ContainsKey("isMasterDetail"))
                    {
                        options.IsMasterDetail = Convert.ToBoolean(optionsDict["isMasterDetail"]);
                    }
                    if (optionsDict.ContainsKey("generateController"))
                    {
                        options.GenerateController = Convert.ToBoolean(optionsDict["generateController"]);
                    }
                    if (optionsDict.ContainsKey("generateService"))
                    {
                        options.GenerateService = Convert.ToBoolean(optionsDict["generateService"]);
                    }
                    if (optionsDict.ContainsKey("generateRepository"))
                    {
                        options.GenerateRepository = Convert.ToBoolean(optionsDict["generateRepository"]);
                    }
                    if (optionsDict.ContainsKey("generateEntity"))
                    {
                        options.GenerateEntity = Convert.ToBoolean(optionsDict["generateEntity"]);
                    }
                    if (optionsDict.ContainsKey("generateDto"))
                    {
                        options.GenerateDto = Convert.ToBoolean(optionsDict["generateDto"]);
                    }
                    if (optionsDict.ContainsKey("generateFrontend"))
                    {
                        options.GenerateFrontend = Convert.ToBoolean(optionsDict["generateFrontend"]);
                    }
                    if (optionsDict.ContainsKey("generateApiDoc"))
                    {
                        options.GenerateApiDoc = Convert.ToBoolean(optionsDict["generateApiDoc"]);
                    }
                }
            }

            model.Options = options;

            var result = new Dictionary<string, string>();

            // 预览代码
            foreach (var template in templates)
            {
                _logger.Debug($"开始预览模板:{template.TemplateName}");

                model.Template = template; // 动态设置当前模板

                // 渲染模板
                var content = await _templateEngine.RenderAsync(template.TemplateName, model);
                result[template.FileName] = content;

                _logger.Info($"模板预览成功:{template.TemplateName}");
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码预览失败,表名:{table.TableName},错误:{ex.Message}");
            throw new TaktException($"代码预览失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 下载代码
    /// </summary>
    public async Task<byte[]> DownloadCodeAsync(TaktGenTable table)
    {
        try
        {
            _logger.Info($"开始下载代码,表名:{table.TableName}");

            // 获取表信息
            var genTable = await TableRepository.GetFirstAsync(x => x.TableName == table.TableName && x.IsGenCode == 0);
            if (genTable == null)
            {
                _logger.Error($"未找到表 {table.TableName} 的生成信息");
                throw new TaktException($"未找到表 {table.TableName} 的生成信息，请先在代码生成表中设置该表的信息");
            }

            // 获取列信息
            var columns = await ColumnRepository.GetListAsync(x => x.TableId == genTable.Id);
            if (columns == null || !columns.Any())
            {
                _logger.Error($"未找到表 {table.TableName} 的列信息");
                throw new TaktException($"未找到表 {table.TableName} 的列信息，请先设置表的列信息");
            }

            // 获取生成配置
            var config = await ConfigRepository.GetFirstAsync(x => x.Status == 0);
            if (config == null)
            {
                _logger.Error("未找到可用的代码生成配置");
                throw new TaktException("未找到可用的代码生成配置，请先配置代码生成参数");
            }

            // 获取模板列表
            List<TaktGenTemplate> templates;
            if (config.GenTplType == 0)
            {
                // 使用 wwwroot/Generator/*.scriban 模板
                var templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "Generator");
                if (!Directory.Exists(templatePath))
                {
                    _logger.Error($"模板目录不存在: {templatePath}");
                    throw new TaktException("模板目录不存在，请确保 wwwroot/Generator 目录存在");
                }

                var templateFiles = Directory.GetFiles(templatePath, "*.scriban", SearchOption.AllDirectories);
                if (!templateFiles.Any())
                {
                    _logger.Error("未找到可用的代码生成模板文件");
                    throw new TaktException("未找到可用的代码生成模板文件，请确保 wwwroot/Generator 目录下有 .scriban 文件");
                }

                templates = templateFiles.Select(file => new TaktGenTemplate
                {
                    TemplateName = Path.GetFileNameWithoutExtension(file),
                    FileName = Path.GetFileName(file),
                    TemplateContent = File.ReadAllText(file),
                    TemplateLanguage = GetTemplateLanguage(file),
                    Status = 0
                }).ToList();
            }
            else
            {
                // 使用 TaktGenTemplate 表中的模板
                templates = await TemplateRepository.GetListAsync(x => x.Status == 0);
                if (templates == null || !templates.Any())
                {
                    _logger.Error("未找到可用的代码生成模板");
                    throw new TaktException("未找到可用的代码生成模板，请先配置代码生成模板");
                }
            }

            // 准备生成模型
            var model = new TaktGeneratorModel
            {
                Table = genTable,
                Template = templates.First(), // 设置默认模板
                Config = new TaktCodeGenerationConfig(_webHostEnvironment)
                {
                    Author = config.Author,
                    ModuleName = config.ModuleName,
                    ProjectName = config.ProjectName,
                    BaseNamespace = config.ProjectName,
                    GenPath = config.GenPath,
                    Templates = templates.ToList()
                },
                Columns = columns.ToList(), // 添加列信息
            };

            // 创建生成选项
            var options = new Models.TaktGeneratorOptions
            {
                Author = config.Author,
                ModuleName = config.ModuleName,
                ProjectName = config.ProjectName,
                BaseNamespace = config.ProjectName,
                GenPath = config.GenPath,
                GenerateController = true,
                GenerateService = true,
                GenerateRepository = true,
                GenerateEntity = true,
                GenerateDto = true,
                GenerateFrontend = true,
                GenerateApiDoc = true,
                IsTree = genTable.TplCategory == "tree",
                IsMasterDetail = genTable.TplCategory == "sub"
            };

            // 解析选项配置
            if (!string.IsNullOrEmpty(config.Options))
            {
                var optionsDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(config.Options);
                if (optionsDict != null)
                {
                    // 设置生成选项
                    if (optionsDict.ContainsKey("isTree"))
                    {
                        options.IsTree = Convert.ToBoolean(optionsDict["isTree"]);
                    }
                    if (optionsDict.ContainsKey("isMasterDetail"))
                    {
                        options.IsMasterDetail = Convert.ToBoolean(optionsDict["isMasterDetail"]);
                    }
                    if (optionsDict.ContainsKey("generateController"))
                    {
                        options.GenerateController = Convert.ToBoolean(optionsDict["generateController"]);
                    }
                    if (optionsDict.ContainsKey("generateService"))
                    {
                        options.GenerateService = Convert.ToBoolean(optionsDict["generateService"]);
                    }
                    if (optionsDict.ContainsKey("generateRepository"))
                    {
                        options.GenerateRepository = Convert.ToBoolean(optionsDict["generateRepository"]);
                    }
                    if (optionsDict.ContainsKey("generateEntity"))
                    {
                        options.GenerateEntity = Convert.ToBoolean(optionsDict["generateEntity"]);
                    }
                    if (optionsDict.ContainsKey("generateDto"))
                    {
                        options.GenerateDto = Convert.ToBoolean(optionsDict["generateDto"]);
                    }
                    if (optionsDict.ContainsKey("generateFrontend"))
                    {
                        options.GenerateFrontend = Convert.ToBoolean(optionsDict["generateFrontend"]);
                    }
                    if (optionsDict.ContainsKey("generateApiDoc"))
                    {
                        options.GenerateApiDoc = Convert.ToBoolean(optionsDict["generateApiDoc"]);
                    }
                }
            }

            model.Options = options;

            // 创建ZIP文件
            using var ms = new MemoryStream();
            using (var archive = new ZipArchive(ms, ZipArchiveMode.Create))
            {
                foreach (var template in templates)
                {
                    _logger.Debug($"开始生成模板:{template.TemplateName}");

                    model.Template = template; // 动态设置当前模板

                    // 渲染模板
                    var content = await _templateEngine.RenderAsync(template.TemplateName, model);

                    // 获取文件路径信息
                    var (directory, fileName) = GetFilePathInfo(template.TemplateName, model);
                    var filePath = Path.Combine(directory, fileName);

                    _logger.Info($"生成文件路径: {filePath}");

                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                        _logger.Info($"创建目录: {directory}");
                    }

                    var entry = archive.CreateEntry(filePath);
                    using var entryStream = entry.Open();
                    using var writer = new StreamWriter(entryStream);
                    await writer.WriteAsync(content);
                }
            }

            var zipBytes = ms.ToArray();
            _logger.Info($"代码下载成功,ZIP文件大小:{zipBytes.Length}字节");
            return zipBytes;
        }
        catch (Exception ex)
        {
            _logger.Error($"代码下载失败,表名:{table.TableName},错误:{ex.Message}");
            throw new TaktException($"代码下载失败：{ex.Message}");
        }
    }

    /// <summary>
    /// 生成代码内部实现
    /// </summary>
    private async Task<bool> GenerateCodeInternalAsync(TaktGeneratorModel model)
    {
        try
        {
            _logger.Info($"GenPath: {model.Config.GenPath}");
            _logger.Info($"TplType: {model.Table.TplType}, TplCategory: {model.Table.TplCategory}");

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
            model.Columns = model.Columns.Where(c => !baseEntityFields.Contains(c.ColumnName.ToLower())).ToList();

            // 只保留一套模板过滤逻辑
            var templates = model.Config.Templates;
            if (model.Table.TplType == "1")
            {
                // 数据库模板，根据模板类型和类别过滤
                templates = templates.Where(t =>
                    t.TemplateCodeType == (model.Table.TplType == "1" ? 1 : 0) && // 1: 后端代码, 0: 前端代码
                    t.TemplateCategory.ToString() == model.Table.TplCategory &&   // 模板类别
                    t.TemplateOrmType == 1 &&                                     // Entity Framework Core
                    t.Status == 0                                                 // 正常状态
                ).ToList();
            }

            if (!templates.Any())
            {
                _logger.Error($"未找到可用的模板，模板类型: {model.Table.TplType}, 模板类别: {model.Table.TplCategory}");
                throw new TaktException($"未找到可用的模板，请检查模板配置");
            }

            // 定义后端生成顺序
            var backendOrder = new Dictionary<string, int>
            {
                { "Entity", 1 },      // 实体
                { "Dto", 2 },         // 对象
                { "IService", 3 },    // 服务接口
                { "Service", 4 },     // 服务实现
                { "Controller", 5 }   // 控制器
            };

            // 定义前端生成顺序
            var frontendOrder = new Dictionary<string, int>
            {
                { "types", 1 },       // 类型定义
                { "api", 2 },         // API接口
                { "vue", 3 },         // 页面组件
                { "locales", 4 }      // 语言包
            };

            // 按顺序生成后端代码
            var backendTemplates = templates.Where(t => !t.TemplateName.Contains("vue") &&
                                                      !t.TemplateName.Contains("api") &&
                                                      !t.TemplateName.Contains("types") &&
                                                      !t.TemplateName.Contains("locales"))
                                          .OrderBy(t => backendOrder.GetValueOrDefault(t.TemplateName, 999));

            foreach (var template in backendTemplates)
            {
                _logger.Debug($"开始生成后端模板:{template.TemplateName}");
                model.Template = template;

                // 渲染模板
                var content = await _templateEngine.RenderAsync(template.TemplateName, model);

                // 获取文件路径信息
                var (directory, fileName) = GetFilePathInfo(template.TemplateName, model);
                var filePath = Path.Combine(directory, fileName);

                _logger.Info($"生成后端文件路径: {filePath}");

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    _logger.Info($"创建目录: {directory}");
                }

                await File.WriteAllTextAsync(filePath, content);
                _logger.Info($"后端模板生成成功:{template.TemplateName},文件路径:{filePath}");
            }

            // 按顺序生成前端代码
            var frontendTemplates = templates.Where(t => t.TemplateName.Contains("vue") ||
                                                       t.TemplateName.Contains("api") ||
                                                       t.TemplateName.Contains("types") ||
                                                       t.TemplateName.Contains("locales"))
                                           .OrderBy(t => frontendOrder.GetValueOrDefault(t.TemplateName.ToLower(), 999));

            foreach (var template in frontendTemplates)
            {
                _logger.Debug($"开始生成前端模板:{template.TemplateName}");
                model.Template = template;

                // 渲染模板
                var content = await _templateEngine.RenderAsync(template.TemplateName, model);

                // 获取文件路径信息
                var (directory, fileName) = GetFilePathInfo(template.TemplateName, model);
                var filePath = Path.Combine(directory, fileName);

                _logger.Info($"生成前端文件路径: {filePath}");

                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    _logger.Info($"创建目录: {directory}");
                }

                await File.WriteAllTextAsync(filePath, content);
                _logger.Info($"前端模板生成成功:{template.TemplateName},文件路径:{filePath}");
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"生成代码时发生错误: {ex.Message}, 堆栈: {ex.StackTrace}");
            return false;
        }
    }

    /// <summary>
    /// 获取文件路径信息
    /// </summary>
    private (string Directory, string FileName) GetFilePathInfo(string templateFilePath, TaktGeneratorModel model)
    {
        // 输出日志，便于排查
        _logger.Info($"GetFilePathInfo: templateFilePath={templateFilePath}");

        // 获取模板名称（不含扩展名）
        var templateName = Path.GetFileNameWithoutExtension(templateFilePath);

        // 判断类型
        string type = model.Table.TplCategory?.ToLower() ?? "crud";

        // 判断是前端还是后端
        bool isBackend = true;  // 默认后端
        bool isFrontend = false;

        // 根据模板名称判断类型
        if (templateName.Contains("vue") || templateName.Contains("api") ||
            templateName.Contains("types") || templateName.Contains("locales"))
        {
            isBackend = false;
            isFrontend = true;
        }

        // 取模块名
        var TransType = model.Table.GenModuleName?.ToLower() ?? "common";
        var genPath = model.Table.GenPath;

        // 拼接输出目录
        string directory;
        if (isBackend)
        {
            directory = Path.Combine(genPath, "backend", type, TransType);
        }
        else
        {
            directory = Path.Combine(genPath, "frontend", type, TransType);
        }

        // 根据模板名称生成文件名
        string outputFileName;
        if (templateName.Contains("Entity"))
        {
            outputFileName = $"{model.Table.EntityClassName}.cs";
        }
        else if (templateName.Contains("Dto"))
        {
            outputFileName = $"{model.Table.DtoClassName}.cs";
        }
        else if (templateName.Contains("IService"))
        {
            outputFileName = $"{model.Table.IServiceClassName}.cs";
        }
        else if (templateName.Contains("Service"))
        {
            outputFileName = $"{model.Table.ServiceClassName}.cs";
        }
        else if (templateName.Contains("Controller"))
        {
            outputFileName = $"{model.Table.ControllerClassName}.cs";
        }
        else if (templateName.Contains("types"))
        {
            outputFileName = $"{model.Table.EntityClassName.ToLower()}.types.ts";
        }
        else if (templateName.Contains("api"))
        {
            outputFileName = $"{model.Table.EntityClassName.ToLower()}.ts";
        }
        else if (templateName.Contains("vue"))
        {
            outputFileName = $"{model.Table.EntityClassName.ToLower()}.vue";
        }
        else if (templateName.Contains("locales"))
        {
            outputFileName = "zh-CN.ts";
        }
        else
        {
            outputFileName = $"{model.Table.EntityClassName}{templateName}.cs";
        }

        _logger.Info($"生成文件路径: directory={directory}, fileName={outputFileName}");
        return (directory, outputFileName);
    }

    /// <summary>
    /// 根据文件扩展名获取模板语言类型
    /// </summary>
    private int GetTemplateLanguage(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLower();
        return extension switch
        {
            ".cs.scriban" => 1,    // C#
            ".ts.scriban" => 2,    // TypeScript
            ".js.scriban" => 3,    // JavaScript
            ".java.scriban" => 4,  // Java
            ".vue.scriban" => 5,   // Vue
            ".html.scriban" => 6,  // HTML
            ".css.scriban" => 7,   // CSS
            ".sql.scriban" => 8,   // SQL
            _ => 1                 // 默认C#
        };
    }
}
