# 控制器规范

## 1. 目录规范

- 所有控制器必须放在WebApi层下的Controllers目录中
- 按业务模块分子目录,例如:
  - `Takt.WebApi.Controllers.Identity` (身份认证模块)
- `Takt.WebApi.Controllers.System` (系统管理模块)
- `Takt.WebApi.Controllers.Monitor` (监控模块)

## 2. 命名规范

- 控制器类必须以`Takt`开头,以`Controller`结尾
- 必须继承自`TaktBaseController`
- 必须标注`[ApiController]`特性
- 必须标注`[Route("api/Takt/[controller]")]`特性
- 必须标注`[TaktAuth]`特性进行身份认证
- 必须标注`[TaktLog]`特性记录操作日志

## 3. 标准示例：用户控制器

```csharp
namespace Takt.WebApi.Controllers.Identity
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [Route("api/Takt/user")]
    [TaktAuth]  // 权限验证
    [TaktLog]   // 操作日志
    public class TaktUserController : TaktBaseController
    {
        private readonly ITaktUserService _userService;

        public TaktUserController(ITaktUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        [TaktPermission("system:user:list")]     // 用户列表权限
        [HttpGet("list")]
        public async Task<TaktApiResult<TaktPagedResult<TaktUserDto>>> GetUserList([FromQuery] TaktUserQueryDto query)
        {
            var result = await _userService.GetUserListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        [TaktPermission("system:user:query")]    // 用户查询权限
        [HttpGet("{id}")]
        public async Task<TaktApiResult<TaktUserDto>> GetUser(long id)
        {
            var result = await _userService.GetUserAsync(id);
            return Success(result);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        [TaktPermission("system:user:add")]      // 用户新增权限
        [HttpPost]
        public async Task<TaktApiResult<long>> CreateUser([FromBody] TaktUserCreateDto input)
        {
            var result = await _userService.CreateUserAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        [TaktPermission("system:user:edit")]     // 用户编辑权限
        [HttpPut]
        public async Task<TaktApiResult> UpdateUser([FromBody] TaktUserUpdateDto input)
        {
            var result = await _userService.UpdateUserAsync(input);
            return result ? Success() : Error();
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        [TaktPermission("system:user:delete")]   // 用户删除权限
        [HttpDelete("{id}")]
        public async Task<TaktApiResult> DeleteUser(long id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return result ? Success() : Error();
        }

        /// <summary>
        /// 导入用户
        /// </summary>
        [TaktPermission("system:user:import")]   // 用户导入权限
        [HttpPost("import")]
        public async Task<TaktApiResult<string>> ImportUsers(IFormFile file)
        {
            // 1.读取Excel
            var users = await ExcelHelper.ImportAsync<TaktUserImportDto>(file);

            // 2.导入数据
            var result = await _userService.ImportUsersAsync(users);
            return Success(result);
        }

        /// <summary>
        /// 导出用户
        /// </summary>
        [TaktPermission("system:user:export")]   // 用户导出权限
        [HttpGet("export")]
        public async Task<IActionResult> ExportUsers([FromQuery] TaktUserQueryDto query)
        {
            // 1.查询数据
            var users = await _userService.ExportUsersAsync(query);

            // 2.导出Excel
            var bytes = await ExcelHelper.ExportAsync(users);
            return File(bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "用户列表.xlsx");
        }
    }
}
```

## 4. 规范说明

1. 基础规范
   - 控制器必须继承TaktBaseController
   - 必须标注ApiController特性
   - 必须标注Route特性
   - 必须标注TaktAuth特性进行身份认证
   - 必须标注TaktLog特性记录操作日志
   - 必须有完整的XML注释

2. 路由规范
   - 基础路由: api/Takt/[controller]
   - GET方法: 
     - 列表: list
     - 详情: {id}
   - POST方法: 空
   - PUT方法: 空
   - DELETE方法: {id}
   - 特殊操作: [HttpPost("action")]

3. 参数规范
   - 查询参数: [FromQuery]
   - 路径参数: 直接使用
   - 请求体: [FromBody]
   - 文件: IFormFile

4. 返回值规范
   - 统一使用TaktApiResult封装
   - 分页数据用TaktPagedResult
   - 文件返回用File方法
   - 统一使用Success/Error方法

5. 异常处理
   - 全局异常过滤
   - 业务异常抛出TaktException
   - 参数验证异常处理
   - 返回统一错误格式

6. 权限控制
   - 控制器级别使用[TaktAuth]进行身份认证
   - 方法级别使用[TaktPermission]进行权限验证
   - 权限编码格式: 模块:实体:操作
   - 常见操作权限:list、query、add、edit、delete、import、export等
   - 记录操作日志
   - 数据权限过滤

7. 日志记录
   - 控制器级别使用[TaktLog]记录操作日志
   - 自动记录请求参数、返回结果、执行时间等
   - 异常情况自动记录异常信息
   - 支持忽略指定方法的日志记录[TaktLog(false)]

8. 文件处理
   - 文件大小限制
   - 文件类型验证
   - 统一保存路径
   - 防止重名

9. 安全规范
   - 参数验证
   - 防止SQL注入
   - 防止XSS攻击
   - 敏感数据加密
