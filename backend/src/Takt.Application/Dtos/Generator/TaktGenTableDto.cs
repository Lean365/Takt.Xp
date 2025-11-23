#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenTableDto.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表DTO
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Generator;

/// <summary>
/// 代码生成表DTO
/// </summary>
public class TaktGenTableDto
{
    #region 基本信息

    /// <summary>
    /// 主键
    /// </summary>
    [AdaptMember("Id")]
    public long GenTableId { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 是否数据表
    /// </summary>
    public int IsDataTable { get; set; } = 1;

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段
    /// </summary>
    public string TreeCode { get; set; } = string.Empty;

    /// <summary>
    /// 树名称字段
    /// </summary>
    public string TreeName { get; set; } = string.Empty;

    /// <summary>
    /// 树父编码字段
    /// </summary>
    public string TreeParentCode { get; set; } = string.Empty;

    #endregion

    #region 类型信息
    /// <summary>
    /// 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用TaktGenTemplate数据表中的模板）
    /// </summary>
    public string TplType { get; set; } = "0";


    /// <summary>
    /// 使用的模板（crud单表操作 tree树表操作 sub主子表操作）
    /// </summary>
    public string TplCategory { get; set; } = "crud";

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string EntityNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体类名
    /// </summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象命名空间
    /// </summary>
    public string DtoNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 对象类型
    /// </summary>
    public string DtoType { get; set; } = string.Empty;

    /// <summary>
    /// 对象类名
    /// </summary>
    public string DtoClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string ServiceNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string IServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类名称
    /// </summary>
    public string ServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string IRepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string RepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string IRepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string RepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string ControllerNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string ControllerClassName { get; set; } = string.Empty;

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string GenModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string GenFunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    #endregion

    #region 生成选项

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

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）
    /// </summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 自动生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

    /// <summary>
    /// 自动生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

    /// <summary>
    /// 前端模板 1、element ui 2、element plus
    /// </summary>
    public int FrontTpl { get; set; } = 2;

    /// <summary>
    /// 前端样式 12,24
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否已生成代码（0：未生成，1：已生成）
    /// </summary>
    public int IsGenCode { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0未删除 1已删除）
    /// </summary>
    public int IsDeleted { get; set; }

    /// <summary>
    /// 删除者
    /// </summary>
    public string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeleteTime { get; set; }

    #endregion

    #region 系统信息



    /// <summary>
    /// 列信息
    /// </summary>
    public List<TaktGenColumnDto> Columns { get; set; } = new();

    /// <summary>
    /// 子表信息
    /// </summary>
    public TaktGenTableDto? SubTable { get; set; }

    #endregion
}

/// <summary>
/// 代码生成表查询DTO
/// </summary>
public class TaktGenTableQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 表名
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// 表描述
    /// </summary>
    public string? TableComment { get; set; }
}

/// <summary>
/// 代码生成表创建DTO
/// </summary>
public class TaktGenTableCreateDto
{
    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 是否数据表
    /// </summary>
    public int IsDataTable { get; set; } = 1;

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段
    /// </summary>
    public string TreeCode { get; set; } = string.Empty;

    /// <summary>
    /// 树名称字段
    /// </summary>
    public string TreeName { get; set; } = string.Empty;

    /// <summary>
    /// 树父编码字段
    /// </summary>
    public string TreeParentCode { get; set; } = string.Empty;

    #endregion

    #region 类型信息

    /// <summary>
    /// 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用TaktGenTemplate数据表中的模板）
    /// </summary>
    public string TplType { get; set; } = "0";

    /// <summary>
    /// 使用的模板（crud单表操作 tree树表操作 sub主子表操作）
    /// </summary>
    public string TplCategory { get; set; } = "crud";

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string EntityNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体类名
    /// </summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象命名空间
    /// </summary>
    public string DtoNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 对象类型
    /// </summary>
    public string DtoType { get; set; } = string.Empty;

    /// <summary>
    /// 对象类名
    /// </summary>
    public string DtoClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string ServiceNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string IServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类名称
    /// </summary>
    public string ServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string IRepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string RepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string IRepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string RepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string ControllerNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string ControllerClassName { get; set; } = string.Empty;

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string GenModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string GenFunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    #endregion

    #region 生成选项

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

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）
    /// </summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 自动生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

    /// <summary>
    /// 自动生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

    /// <summary>
    /// 前端模板 1、element ui 2、element plus
    /// </summary>
    public int FrontTpl { get; set; } = 2;

    /// <summary>
    /// 前端样式 12,24
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否已生成代码（0：未生成，1：已生成）
    /// </summary>
    public int IsGenCode { get; set; } = 0;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    #endregion

    /// <summary>
    /// 列信息
    /// </summary>
    public List<TaktGenColumnDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表更新DTO
/// </summary>
public class TaktGenTableUpdateDto : TaktGenTableCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "主键ID不能为空")]
    [AdaptMember("Id")]
    public long GenTableId { get; set; }
}

/// <summary>
/// 代码生成表导入DTO
/// </summary>
public class TaktGenTableImportDto
{
    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 是否数据表
    /// </summary>
    public int IsDataTable { get; set; } = 1;

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段
    /// </summary>
    public string TreeCode { get; set; } = string.Empty;

    /// <summary>
    /// 树名称字段
    /// </summary>
    public string TreeName { get; set; } = string.Empty;

    /// <summary>
    /// 树父编码字段
    /// </summary>
    public string TreeParentCode { get; set; } = string.Empty;

    #endregion

    #region 类型信息

    /// <summary>
    /// 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用TaktGenTemplate数据表中的模板）
    /// </summary>
    public string TplType { get; set; } = "0";

    /// <summary>
    /// 使用的模板（crud单表操作 tree树表操作 sub主子表操作）
    /// </summary>
    public string TplCategory { get; set; } = "crud";

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string EntityNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体类名
    /// </summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象命名空间
    /// </summary>
    public string DtoNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 对象类型
    /// </summary>
    public string DtoType { get; set; } = string.Empty;

    /// <summary>
    /// 对象类名
    /// </summary>
    public string DtoClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string ServiceNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string IServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类名称
    /// </summary>
    public string ServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string IRepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string RepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string IRepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string RepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string ControllerNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string ControllerClassName { get; set; } = string.Empty;

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string GenModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string GenFunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    #endregion

    #region 生成选项

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

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）
    /// </summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 自动生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

    /// <summary>
    /// 自动生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

    /// <summary>
    /// 前端模板 1、element ui 2、element plus
    /// </summary>
    public int FrontTpl { get; set; } = 2;

    /// <summary>
    /// 前端样式 12,24
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否已生成代码（0：未生成，1：已生成）
    /// </summary>
    public int IsGenCode { get; set; } = 0;

    #endregion

    /// <summary>
    /// 列信息
    /// </summary>
    public List<TaktGenColumnDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表模板DTO
/// </summary>
public class TaktGenTableTemplateDto
{

}

/// <summary>
/// 代码生成表导出DTO
/// </summary>
public class TaktGenTableExportDto
{
    #region 基本信息

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; } = string.Empty;

    /// <summary>
    /// 是否数据表
    /// </summary>
    public int IsDataTable { get; set; } = 1;

    /// <summary>
    /// 关联父表名
    /// </summary>
    public string? SubTableName { get; set; }

    /// <summary>
    /// 本表关联父表的外键名
    /// </summary>
    public string? SubTableFkName { get; set; }

    /// <summary>
    /// 树编码字段
    /// </summary>
    public string TreeCode { get; set; } = string.Empty;

    /// <summary>
    /// 树名称字段
    /// </summary>
    public string TreeName { get; set; } = string.Empty;

    /// <summary>
    /// 树父编码字段
    /// </summary>
    public string TreeParentCode { get; set; } = string.Empty;

    #endregion

    #region 类型信息

    /// <summary>
    /// 模板类型（0使用wwwroot/Generator/*.scriban模板 1使用TaktGenTemplate数据表中的模板）
    /// </summary>
    public string TplType { get; set; } = "0";

    /// <summary>
    /// 使用的模板（crud单表操作 tree树表操作 sub主子表操作）
    /// </summary>
    public string TplCategory { get; set; } = "crud";

    /// <summary>
    /// 基本命名空间前缀
    /// </summary>
    public string BaseNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体命名空间
    /// </summary>
    public string EntityNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 实体类名
    /// </summary>
    public string EntityClassName { get; set; } = string.Empty;

    /// <summary>
    /// 对象命名空间
    /// </summary>
    public string DtoNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 对象类型
    /// </summary>
    public string DtoType { get; set; } = string.Empty;

    /// <summary>
    /// 对象类名
    /// </summary>
    public string DtoClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务命名空间
    /// </summary>
    public string ServiceNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 服务接口类名称
    /// </summary>
    public string IServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类名称
    /// </summary>
    public string ServiceClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口命名空间
    /// </summary>
    public string IRepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储命名空间
    /// </summary>
    public string RepositoryNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 仓储接口类名称
    /// </summary>
    public string IRepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 仓储类名称
    /// </summary>
    public string RepositoryClassName { get; set; } = string.Empty;

    /// <summary>
    /// 控制器命名空间
    /// </summary>
    public string ControllerNamespace { get; set; } = string.Empty;

    /// <summary>
    /// 控制器类名称
    /// </summary>
    public string ControllerClassName { get; set; } = string.Empty;

    #endregion

    #region 生成配置信息

    /// <summary>
    /// 生成模块名
    /// </summary>
    public string GenModuleName { get; set; } = string.Empty;

    /// <summary>
    /// 生成业务名
    /// </summary>
    public string GenBusinessName { get; set; } = string.Empty;

    /// <summary>
    /// 生成功能名
    /// </summary>
    public string GenFunctionName { get; set; } = string.Empty;

    /// <summary>
    /// 生成作者名
    /// </summary>
    public string GenAuthor { get; set; } = string.Empty;

    #endregion

    #region 生成选项

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

    /// <summary>
    /// 生成代码方式（0zip压缩包 1自定义路径）
    /// </summary>
    public int GenMethod { get; set; } = 0;

    /// <summary>
    /// 代码生成存放位置
    /// </summary>
    public string GenPath { get; set; } = "/";

    /// <summary>
    /// 上级菜单ID
    /// </summary>
    public long ParentMenuId { get; set; }

    /// <summary>
    /// 排序类型
    /// </summary>
    public string SortType { get; set; } = "asc";

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; } = string.Empty;

    /// <summary>
    /// 权限前缀
    /// </summary>
    public string PermsPrefix { get; set; } = string.Empty;

    /// <summary>
    /// 自动生成菜单
    /// </summary>
    public int IsGenMenu { get; set; }

    /// <summary>
    /// 自动生成翻译
    /// </summary>
    public int IsGenTranslation { get; set; }

    /// <summary>
    /// 前端模板 1、element ui 2、element plus
    /// </summary>
    public int FrontTpl { get; set; } = 2;

    /// <summary>
    /// 前端样式 12,24
    /// </summary>
    public int FrontStyle { get; set; } = 24;

    /// <summary>
    /// 操作按钮样式
    /// </summary>
    public int BtnStyle { get; set; } = 1;

    /// <summary>
    /// 是否已生成代码（0：未生成，1：已生成）
    /// </summary>
    public int IsGenCode { get; set; } = 0;

    #endregion

    /// <summary>
    /// 列信息
    /// </summary>
    public List<TaktGenColumnDto> Columns { get; set; } = new();
}

/// <summary>
/// 代码生成表信息DTO
/// </summary>
public class TaktGenTableInfoDto
{
    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 表描述
    /// </summary>
    public string TableComment { get; set; } = string.Empty;
}


