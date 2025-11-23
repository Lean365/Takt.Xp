namespace Takt.Application.Services.Generator.CodeGenerator.Models;

/// <summary>
/// 代码生成模型
/// </summary>
public class TaktGeneratorModel
{
    /// <summary>
    /// 表信息
    /// </summary>
    public TaktGenTable? Table { get; set; }

    /// <summary>
    /// 生成配置
    /// </summary>
    public TaktCodeGenerationConfig? Config { get; set; }

    /// <summary>
    /// 生成选项
    /// </summary>
    public TaktGeneratorOptions? Options { get; set; }

    /// <summary>
    /// 模板信息
    /// </summary>
    public TaktGenTemplate? Template { get; set; }

    /// <summary>
    /// 列信息
    /// </summary>
    public List<TaktGenColumn>? Columns { get; set; }

    /// <summary>
    /// DTO类型
    /// </summary>
    public string? DtoType { get; set; }
}

/// <summary>
/// 代码生成选项
/// </summary>
public class TaktGeneratorOptions
{
    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// 模块名称
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    /// 项目名称
    /// </summary>
    public string? ProjectName { get; set; }

    /// <summary>
    /// 基础命名空间
    /// </summary>
    public string? BaseNamespace { get; set; }

    /// <summary>
    /// 生成路径
    /// </summary>
    public string? GenPath { get; set; }

    /// <summary>
    /// 是否生成控制器
    /// </summary>
    public bool GenerateController { get; set; } = true;

    /// <summary>
    /// 是否生成服务
    /// </summary>
    public bool GenerateService { get; set; } = true;

    /// <summary>
    /// 是否生成仓储
    /// </summary>
    public bool GenerateRepository { get; set; } = true;

    /// <summary>
    /// 是否生成实体
    /// </summary>
    public bool GenerateEntity { get; set; } = true;

    /// <summary>
    /// 是否生成DTO
    /// </summary>
    public bool GenerateDto { get; set; } = true;

    /// <summary>
    /// 是否生成前端
    /// </summary>
    public bool GenerateFrontend { get; set; } = true;

    /// <summary>
    /// 是否生成API文档
    /// </summary>
    public bool GenerateApiDoc { get; set; } = true;

    #region 树表选项

    /// <summary>
    /// 是否为树表
    /// </summary>
    public bool IsTree { get; set; } = false;

    /// <summary>
    /// 树编码字段
    /// </summary>
    public string TreeCode { get; set; } = "id";

    /// <summary>
    /// 树父编码字段
    /// </summary>
    public string TreeParentCode { get; set; } = "parent_id";

    /// <summary>
    /// 树名称字段
    /// </summary>
    public string TreeName { get; set; } = "name";

    #endregion

    #region 主子表选项

    /// <summary>
    /// 是否为主子表
    /// </summary>
    public bool IsMasterDetail { get; set; } = false;

    /// <summary>
    /// 主表名
    /// </summary>
    public string MasterTable { get; set; } = string.Empty;

    /// <summary>
    /// 子表名
    /// </summary>
    public string DetailTable { get; set; } = string.Empty;

    /// <summary>
    /// 主表主键
    /// </summary>
    public string MasterTableKey { get; set; } = "id";

    /// <summary>
    /// 子表外键
    /// </summary>
    public string DetailTableKey { get; set; } = string.Empty;

    #endregion

    #region 生成功能选项

    /// <summary>
    /// 生成功能（查询，新增，更新，删除，模板，导入，导出的按钮编号）
    /// </summary>
    public string GenFunction { get; set; } = "1,2,3,4,5,6,7";

    /// <summary>
    /// 是否启用SQL差异
    /// </summary>
    public int IsSqlDiff { get; set; } = 1;

    /// <summary>
    /// 是否使用雪花id
    /// </summary>
    public int IsSnowflakeId { get; set; } = 1;

    /// <summary>
    /// 是否生成仓储层
    /// </summary>
    public int IsRepository { get; set; } = 1;

    #endregion
}
