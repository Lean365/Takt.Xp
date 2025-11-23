# DTO规范

## 1. 目录规范

- 所有DTO必须放在Application层下的Dtos目录中
- 按业务模块分子目录,例如:
  - `Takt.Application.Dtos.Identity` (身份认证模块)
- `Takt.Application.Dtos.System` (系统管理模块)
- `Takt.Application.Dtos.Monitor` (监控模块)

## 2. 命名规范

- 所有DTO类必须以`Takt`开头,以`Dto`结尾
- 按用途分类命名:
  - 基础DTO: `TaktXxxDto`
  - 查询DTO: `TaktXxxQueryDto`
  - 创建DTO: `TaktXxxCreateDto`
  - 更新DTO: `TaktXxxUpdateDto`
  - 删除DTO: `TaktXxxDeleteDto`
  - 导入DTO: `TaktXxxImportDto`
  - 导出DTO: `TaktXxxExportDto`
  - 模板DTO: `TaktXxxTemplateDto`

## 3. 文件结构规范

```csharp
//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktXxxDto.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:50
// 版本号 : V1.0.0
// 描述    : XXX数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// XXX数据传输对象
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktXxxDto
    {
        // 属性定义
    }
}
```

## 4. 标准示例：用户DTO

```csharp
namespace Takt.Application.Dtos.Identity
{
    /// <summary>
    /// 用户基础DTO
    /// </summary>
    public class TaktUserDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public YesNo UserType { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CommonStatus Status { get; set; }
    }

    /// <summary>
    /// 用户查询DTO
    /// </summary>
    public class TaktUserQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public CommonStatus? Status { get; set; }
    }

    /// <summary>
    /// 用户创建DTO
    /// </summary>
    public class TaktUserCreateDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }
    }

    /// <summary>
    /// 用户更新DTO
    /// </summary>
    public class TaktUserUpdateDto
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        public long DeptId { get; set; }

        /// <summary>
        /// 角色ID列表
        /// </summary>
        public List<long> RoleIds { get; set; }

        /// <summary>
        /// 岗位ID列表
        /// </summary>
        public List<long> PostIds { get; set; }
    }

    /// <summary>
    /// 用户导入DTO
    /// </summary>
    public class TaktUserImportDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0男 1女 2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表(逗号分隔)
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表(逗号分隔)
        /// </summary>
        public string PostNames { get; set; }
    }

    /// <summary>
    /// 用户导出DTO
    /// </summary>
    public class TaktUserExportDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string GenderName { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表
        /// </summary>
        public string PostNames { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 用户导入模板DTO
    /// </summary>
    public class TaktUserTemplateDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 性别(0男 1女 2未知)
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 角色名称列表(多个逗号分隔)
        /// </summary>
        public string RoleNames { get; set; }

        /// <summary>
        /// 岗位名称列表(多个逗号分隔)
        /// </summary>
        public string PostNames { get; set; }
    }
}
```

## 5. 规范说明

1. 基础规范
   - 所有DTO类必须有完整的XML注释
   - 所有属性必须有注释说明
   - 枚举类型必须说明每个值的含义
   - 不要在DTO中包含业务逻辑

2. 构造函数规范
   - DTO类允许包含构造函数
   - 必须提供无参构造函数
   - 可以提供带参构造函数用于快速初始化
   - 构造函数中只允许对本类属性赋值
   - 不允许在构造函数中调用外部服务
   - 不允许在构造函数中包含业务逻辑

3. 属性规范
   - 属性名采用PascalCase命名法
   - 属性类型要与实体对应
   - 可以省略不需要的字段
   - 可以添加展示需要的字段
   - 敏感字段不要返回(如密码)

4. 分类规范
   - 基础DTO: 用于返回数据,包含基本字段
   - 查询DTO: 继承分页基类,包含查询条件
   - 创建DTO: 包含创建实体需要的字段
   - 更新DTO: 包含更新实体需要的字段
   - 导入DTO: 用于Excel导入,字段类型简单化
   - 导出DTO: 用于Excel导出,转换枚举显示值
   - 模板DTO: 用于生成导入模板,包含示例值

5. 验证和映射规范
   - 验证规则直接在DTO类中使用特性标注
   - 必填字段使用[Required]特性
   - 长度限制使用[MaxLength]等特性
   - 格式验证使用[RegularExpression]等特性
   - 使用Mapster自动映射同名属性
   - 只有特殊情况(属性名不同或需要转换)时才需要配置映射
   - 示例：
     ```csharp
     public class TaktUserDto
     {
         [Required(ErrorMessage = "用户名不能为空")]
         [MaxLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
         public string UserName { get; set; }

         [Required(ErrorMessage = "昵称不能为空")]
         [MaxLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
         public string NickName { get; set; }

         // 只有特殊映射时才需要配置，比如：
         [AdaptMember("Gender")]
         public string GenderName { get; set; }
     }
     ```

6. 安全规范
   - 密码等敏感字段不返回
   - 用户信息脱敏处理
   - 权限字段做好控制 



