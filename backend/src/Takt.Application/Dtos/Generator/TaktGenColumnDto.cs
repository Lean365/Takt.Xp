#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenColumnDto.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成列数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Generator;

/// <summary>
/// 代码生成列数据传输对象
/// </summary>
public class TaktGenColumnDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenColumnDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        PropertyName = string.Empty;
        DataType = string.Empty;
        ColumnDataType = string.Empty;
        QueryType = "EQ";
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 列ID
    /// </summary>
    [AdaptMember("Id")]
    public long GenColumnId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列注释不能为空")]
    [StringLength(200, ErrorMessage = "列注释长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string PropertyName { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string DataType { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int? Length { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int? DecimalDigits { get; set; }

    /// <summary>
    /// C#字段名（首字母小写）
    /// </summary>
    [Required(ErrorMessage = "C#字段名不能为空")]
    [StringLength(50, ErrorMessage = "C#字段名长度不能超过50个字符")]
    public string ColumnDataType { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public int IsCreate { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public int IsUpdate { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

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
}

/// <summary>
/// 代码生成列查询DTO
/// </summary>
public class TaktGenColumnQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 表ID
    /// </summary>
    public long? TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string? ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [StringLength(200, ErrorMessage = "列注释长度不能超过200个字符")]
    public string? ColumnComment { get; set; }
}

/// <summary>
/// 代码生成列创建DTO
/// </summary>
public class TaktGenColumnCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenColumnCreateDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        PropertyName = string.Empty;
        DataType = string.Empty;
        ColumnDataType = string.Empty;
        QueryType = "EQ";
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 表ID
    /// </summary>
    [Required(ErrorMessage = "表ID不能为空")]
    public long TableId { get; set; }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列注释不能为空")]
    [StringLength(200, ErrorMessage = "列注释长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string PropertyName { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string DataType { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int? Length { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int? DecimalDigits { get; set; }

    /// <summary>
    /// C#字段名（首字母小写）
    /// </summary>
    [Required(ErrorMessage = "C#字段名不能为空")]
    [StringLength(50, ErrorMessage = "C#字段名长度不能超过50个字符")]
    public string ColumnDataType { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public int IsCreate { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public int IsUpdate { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(200, ErrorMessage = "备注长度不能超过200个字符")]
    public string? Remark { get; set; }


}

/// <summary>
/// 代码生成列更新DTO
/// </summary>
public class TaktGenColumnUpdateDto : TaktGenColumnCreateDto
{
    /// <summary>
    /// 列ID
    /// </summary>
    [Required(ErrorMessage = "列ID不能为空")]
    [AdaptMember("Id")]
    public long GenColumnId { get; set; }
}

/// <summary>
/// 代码生成列导入DTO
/// </summary>
public class TaktGenColumnImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenColumnImportDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        PropertyName = string.Empty;
        DataType = string.Empty;
        ColumnDataType = string.Empty;
        QueryType = "EQ";
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 列名
    /// </summary>
    [Required(ErrorMessage = "列名不能为空")]
    [StringLength(50, ErrorMessage = "列名长度不能超过50个字符")]
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    [Required(ErrorMessage = "列注释不能为空")]
    [StringLength(200, ErrorMessage = "列注释长度不能超过200个字符")]
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    [Required(ErrorMessage = "数据库列类型不能为空")]
    [StringLength(50, ErrorMessage = "数据库列类型长度不能超过50个字符")]
    public string PropertyName { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    [Required(ErrorMessage = "C#类型不能为空")]
    [StringLength(50, ErrorMessage = "C#类型长度不能超过50个字符")]
    public string DataType { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int? Length { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int? DecimalDigits { get; set; }

    /// <summary>
    /// C#字段名（首字母小写）
    /// </summary>
    [Required(ErrorMessage = "C#字段名不能为空")]
    [StringLength(50, ErrorMessage = "C#字段名长度不能超过50个字符")]
    public string ColumnDataType { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public int IsCreate { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public int IsUpdate { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    [StringLength(50, ErrorMessage = "查询方式长度不能超过50个字符")]
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    [StringLength(50, ErrorMessage = "显示类型长度不能超过50个字符")]
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    [StringLength(200, ErrorMessage = "字典类型长度不能超过200个字符")]
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 代码生成列导出DTO
/// </summary>
public class TaktGenColumnExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktGenColumnExportDto()
    {
        ColumnName = string.Empty;
        ColumnComment = string.Empty;
        PropertyName = string.Empty;
        DataType = string.Empty;
        ColumnDataType = string.Empty;
        QueryType = "EQ";
        DisplayType = string.Empty;
        DictType = string.Empty;
    }

    /// <summary>
    /// 列名
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    /// 列注释
    /// </summary>
    public string ColumnComment { get; set; }

    /// <summary>
    /// 数据库列类型
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// C#类型
    /// </summary>
    public string DataType { get; set; }

    /// <summary>
    /// C#长度（字符串长度、数值类型的整数位数）
    /// </summary>
    public int? Length { get; set; }

    /// <summary>
    /// C#小数位数（decimal等数值类型的小数位数）
    /// </summary>
    public int? DecimalDigits { get; set; }

    /// <summary>
    /// C#字段名（首字母小写）
    /// </summary>
    public string ColumnDataType { get; set; }

    /// <summary>
    /// 是否自增（1是）
    /// </summary>
    public int IsIncrement { get; set; }

    /// <summary>
    /// 是否主键（1是）
    /// </summary>
    public int IsPrimaryKey { get; set; }

    /// <summary>
    /// 是否必填（1是）
    /// </summary>
    public int IsRequired { get; set; }

    /// <summary>
    /// 是否为新增字段（1是）
    /// </summary>
    public int IsCreate { get; set; }

    /// <summary>
    /// 是否编辑字段（1是）
    /// </summary>
    public int IsUpdate { get; set; }

    /// <summary>
    /// 是否列表字段（1是）
    /// </summary>
    public int IsList { get; set; }

    /// <summary>
    /// 是否查询字段（1是）
    /// </summary>
    public int IsQuery { get; set; }

    /// <summary>
    /// 查询方式（等于、不等于、大于、小于、范围）
    /// </summary>
    public string QueryType { get; set; }

    /// <summary>
    /// 是否排序字段（1是）
    /// </summary>
    public int IsSort { get; set; }

    /// <summary>
    /// 是否导出字段（1是）
    /// </summary>
    public int IsExport { get; set; }

    /// <summary>
    /// 显示类型（文本框、文本域、下拉框、复选框、单选框、日期控件）
    /// </summary>
    public string DisplayType { get; set; }

    /// <summary>
    /// 字典类型（用于下拉框、单选框、复选框等需要数据字典的字段）
    /// </summary>
    public string DictType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int OrderNum { get; set; }
}

/// <summary>
/// 数据库表列信息DTO
/// </summary>
public class TaktGenTableColumnInfoDto
{
    /// <summary>
    /// 列名
    /// </summary>
    public string DbColumnName { get; set; } = string.Empty;

    /// <summary>
    /// 数据类型
    /// </summary>
    public string DataType { get; set; } = string.Empty;

    /// <summary>
    /// 长度
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 小数位数
    /// </summary>
    public int DecimalDigits { get; set; }

    /// <summary>
    /// 精度
    /// </summary>
    public int Scale { get; set; }

    /// <summary>
    /// 是否可空
    /// </summary>
    public bool IsNullable { get; set; }

    /// <summary>
    /// 是否自增
    /// </summary>
    public bool IsIdentity { get; set; }

    /// <summary>
    /// 是否主键
    /// </summary>
    public bool IsPrimarykey { get; set; }

    /// <summary>
    /// 默认值
    /// </summary>
    public string DefaultValue { get; set; } = string.Empty;

    /// <summary>
    /// 列描述
    /// </summary>
    public string ColumnDescription { get; set; } = string.Empty;

    /// <summary>
    /// Oracle类型
    /// </summary>
    public string OracleDataType { get; set; } = string.Empty;

    /// <summary>
    /// 属性名
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// 属性类型
    /// </summary>
    public string PropertyType { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// 是否数组
    /// </summary>
    public bool IsArray { get; set; }

    /// <summary>
    /// 是否Json
    /// </summary>
    public bool IsJson { get; set; }

    /// <summary>
    /// 建表字段排序
    /// </summary>
    public int CreateTableFieldSort { get; set; }

    /// <summary>
    /// 插入SQL
    /// </summary>
    public string InsertSql { get; set; } = string.Empty;

    /// <summary>
    /// 更新SQL
    /// </summary>
    public string UpdateSql { get; set; } = string.Empty;
}

