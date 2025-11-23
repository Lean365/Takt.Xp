# 实体规范

## 1. 目录规范

- 所有实体必须放在Domain层下的Entities目录中
- 按业务模块分子目录,例如:
  - `Takt.Domain.Entities.Identity` (身份认证模块)
- `Takt.Domain.Entities.System` (系统管理模块)
- `Takt.Domain.Entities.Monitor` (监控模块)

## 2. 命名规范

- 所有实体类必须以`Takt`开头
- 必须继承自`TaktBaseEntity`
- 类名采用PascalCase命名法
- 属性名采用PascalCase命名法
- 数据库表名采用小写下划线命名法,必须以`Takt_`开头

## 3. 文件结构规范

```csharp
//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktXxx.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:50
// 版本号 : V.0.0.1
// 描述    : XXX实体类
//===================================================================

namespace Takt.Domain.Entities
{
    /// <summary>
    /// XXX实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    [SugarTable("Takt_xxx", "XXX表")]
    public class TaktXxx : TaktBaseEntity
    {
        // 属性定义
    }
}
```

## 4. 标准示例：用户实体

```csharp
[SugarTable("Takt_user", "用户表")]
[SugarIndex("ix_user_name", nameof(UserName), OrderByType.Asc, true)]
public class TaktUser : TaktBaseEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string NickName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string EnglishName { get; set; }

    /// <summary>
    /// 用户类型（0系统用户 1普通用户）
    /// </summary>
    [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型（0系统用户 1普通用户）", ColumnDataType = "tinyint", IsNullable = false)]
    public YesNo UserType { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Password { get; set; }

    /// <summary>
    /// 密码盐值
    /// </summary>
    [SugarColumn(ColumnName = "salt", ColumnDescription = "密码盐值", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Salt { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(ColumnName = "phone_number", ColumnDescription = "手机号码", Length = 11, ColumnDataType = "nvarchar", IsNullable = true)]
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string Email { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", ColumnDataType = "tinyint", IsNullable = false)]
    public Gender Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
    public string Avatar { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    public long DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string DeptName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "tinyint", IsNullable = false)]
    public CommonStatus Status { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnName = "tenant_id", ColumnDescription = "租户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long TenantId { get; set; }

    /// <summary>
    /// 用户角色关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserRole.UserId))]
    public List<TaktUserRole> UserRoles { get; set; }

    /// <summary>
    /// 用户部门关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserDept.UserId))]
    public List<TaktUserDept> UserDepts { get; set; }

    /// <summary>
    /// 用户岗位关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserPost.UserId))]
    public List<TaktUserPost> UserPosts { get; set; }
}
```

## 5. 规范说明

1. 表名规范
   - 必须以`Takt_`开头
   - 使用小写下划线命名法
   - 必须添加`SugarTable`特性,指定表名和中文说明

2. 索引规范
   - 使用`SugarIndex`特性定义索引
   - 索引名称必须以`ix_`开头
   - 使用`nameof`引用属性名

3. 列名规范
   - 使用小写下划线命名法
   - 必须添加`SugarColumn`特性
   - 必须指定列名、说明、数据类型
   - 必须指定是否可空
   - 字符串类型必须指定长度

4. 数据类型规范
   - 字符串类型:
     - `nvarchar`: 可变长度Unicode字符串(默认)
     - `varchar`: 可变长度非Unicode字符串
     - `text`: 长文本
     - `longtext`: 超长文本
   - 整数类型:
     - `int`: 标准整数(默认)
     - `smallint`: 小范围整数
     - `bigint`: 大整数,用于主键和大数值
     - `tinyint`: 微小整数(0-255)
   - 浮点数类型:
     - `decimal`: 精确小数(默认),统一使用18位5位小数 decimal(18,5)
     - `numeric`: 精确数值
     - `float`: 近似小数
     - `real`: 单精度浮点数
     - `money`: 货币
     - `smallmoney`: 小额货币
   - 日期时间类型:
     - `datetime`: 日期时间(默认)
     - `date`: 仅日期
     - `datetime2`: 高精度日期时间
     - `smalldatetime`: 小范围日期时间
     - `timestamp`: 时间戳
   - 布尔类型:
     - 统一使用`int`类型,值为0或1
   - 枚举类型:
     - 统一使用`int`类型
   - GUID类型:
     - `uniqueidentifier`: 全局唯一标识符

注意:
- 除非有特殊需求,建议使用默认推荐类型
- 字符串类型必须指定长度(text/longtext除外)
- 主键统一使用bigint
- 金额相关字段使用decimal(18,5)
- 布尔值和枚举值统一使用int
- GUID字段使用uniqueidentifier

5. 导航属性规范
   - 使用`Navigate`特性定义关联关系
   - 指定导航类型和关联字段
   - 一对多关系使用`List<T>`
   - 多对一关系使用单个实体

6. 注释规范
   - 必须包含类注释,说明用途
   - 必须包含属性注释,说明含义
   - 对于枚举值,在注释中说明每个值的含义

7. 基础字段
   - 必须继承`TaktBaseEntity`获取基础字段
   - 包含Id、创建人、创建时间、修改人、修改时间、删除人、删除时间、是否删除、备注等字段

8. 安全规范
   - 密码等敏感字段必须加密存储
   - 必须使用盐值加密
   - 敏感信息展示时需要脱敏处理 



