# 服务层规范

## 1. 目录规范

- 所有服务必须放在Application层下的Services目录中
- 按业务模块分子目录，例如:
  - `Takt.Application.Services.Identity` (身份认证模块)
- `Takt.Application.Services.System` (系统管理模块)
- `Takt.Application.Services.Monitor` (监控模块)

## 2. 命名规范

- 接口必须以`ITakt`开头，以`Service`结尾
- 实现类必须以`Takt`开头，以`Service`结尾
- 接口和实现类放在同一个命名空间下

## 3. 标准示例：用户服务

```csharp
namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface ITaktUserService
    {
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户列表</returns>
        Task<TaktPagedResult<TaktUserDto>> GetUserListAsync(TaktUserQueryDto query);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>用户详情</returns>
        Task<TaktUserDto> GetUserAsync(long id);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建信息</param>
        /// <returns>用户ID</returns>
        Task<long> CreateUserAsync(TaktUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateUserAsync(TaktUserUpdateDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteUserAsync(long id);

        /// <summary>
        /// 导入用户
        /// </summary>
        /// <param name="users">用户列表</param>
        /// <returns>导入结果</returns>
        Task<string> ImportUsersAsync(List<TaktUserImportDto> users);

        /// <summary>
        /// 导出用户
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>导出数据</returns>
        Task<List<TaktUserExportDto>> ExportUsersAsync(TaktUserQueryDto query);
    }

    /// <summary>
    /// 用户服务实现
    /// </summary>
    public class TaktUserService : ITaktUserService
    {
        private readonly ITaktRepository<TaktUser> _userRepository;

        public TaktUserService(ITaktRepository<TaktUser> userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        public async Task<TaktPagedResult<TaktUserDto>> GetUserListAsync(TaktUserQueryDto query)
        {
            // 1.构建查询条件
            var predicate = Expressionable.Create<TaktUser>();
            
            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(u => u.UserName.Contains(query.UserName));
                
            if (!string.IsNullOrEmpty(query.PhoneNumber))
                predicate.And(u => u.PhoneNumber == query.PhoneNumber);
                
            if (query.Status.HasValue)
                predicate.And(u => u.Status == query.Status.Value);

            // 2.查询数据
            var users = await _userRepository.GetPagedListAsync(
                predicate.ToExpression(),
                query.PageIndex,
                query.PageSize);

            // 3.转换并返回
            var dtos = users.Rows.Adapt<List<TaktUserDto>>();
            return new TaktPagedResult<TaktUserDto>
            {
                TotalNum = users.TotalNum,
                PageIndex = users.PageIndex,
                PageSize = users.PageSize,
                Rows = dtos
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        public async Task<TaktUserDto> GetUserAsync(long id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user.Adapt<TaktUserDto>();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        public async Task<long> CreateUserAsync(TaktUserCreateDto input)
        {
            // 1.检查用户名是否存在
            var exists = await _userRepository.IsAnyAsync(u => u.UserName == input.UserName);
            if (exists)
                throw new TaktException("用户名已存在");

            // 2.创建用户实体
            var user = input.Adapt<TaktUser>();
            user.Salt = Guid.NewGuid().ToString("N").Substring(0, 6);
            user.Password = HashHelper.Md5Hash(input.Password + user.Salt);

            // 3.保存用户
            await _userRepository.InsertAsync(user);
            return user.Id;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        public async Task<bool> UpdateUserAsync(TaktUserUpdateDto input)
        {
            // 1.获取用户
            var user = await _userRepository.GetByIdAsync(input.Id);
            if (user == null)
                throw new TaktException("用户不存在");

            // 2.更新用户
            input.Adapt(user);
            return await _userRepository.UpdateAsync(user);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        public async Task<bool> DeleteUserAsync(long id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 导入用户
        /// </summary>
        public async Task<string> ImportUsersAsync(List<TaktUserImportDto> users)
        {
            if (!users.Any())
                return "导入数据为空";

            // 1.转换为实体
            var entities = users.Adapt<List<TaktUser>>();
            foreach (var user in entities)
            {
                user.Salt = Guid.NewGuid().ToString("N").Substring(0, 6);
                user.Password = HashHelper.Md5Hash("123456" + user.Salt);
            }

            // 2.批量插入
            await _userRepository.InsertRangeAsync(entities);
            return $"成功导入{users.Count}条数据";
        }

        /// <summary>
        /// 导出用户
        /// </summary>
        public async Task<List<TaktUserExportDto>> ExportUsersAsync(TaktUserQueryDto query)
        {
            // 1.查询数据
            var predicate = Expressionable.Create<TaktUser>();
            if (!string.IsNullOrEmpty(query.UserName))
                predicate.And(u => u.UserName.Contains(query.UserName));
            if (!string.IsNullOrEmpty(query.PhoneNumber))
                predicate.And(u => u.PhoneNumber == query.PhoneNumber);
            if (query.Status.HasValue)
                predicate.And(u => u.Status == query.Status.Value);

            var users = await _userRepository.GetListAsync(predicate.ToExpression());

            // 2.转换并返回
            return users.Adapt<List<TaktUserExportDto>>();
        }
    }
}
```

## 4. 规范说明

1. 基础规范
   - 接口和实现类必须有完整的XML注释
   - 所有方法必须有注释说明
   - 方法命名要清晰表达功能
   - 统一使用异步方法

2. 接口设计
   - 接口方法要职责单一
   - 参数使用DTO对象
   - 返回值使用DTO对象
   - 分页查询继承基类
   - 导入导出单独方法

3. 实现规范
   - 构造函数注入依赖
   - 使用通用仓储接口ITaktRepository<T>访问数据
   - 使用Mapster进行对象映射
   - 统一异常处理
   - 参数校验完整

4. 业务规范
   - 单个方法逻辑清晰
   - 复杂业务拆分子方法
   - 重复代码提取公共方法
   - 避免循环依赖
   - 避免过度封装

5. 事务规范
   - 需要事务的方法标注特性
   - 事务范围要合理
   - 避免长事务
   - 避免嵌套事务

6. 缓存规范
   - 合理使用缓存
   - 缓存key要规范
   - 及时清理缓存
   - 防止缓存穿透

7. 日志规范
   - 记录关键操作日志
   - 异常要详细记录
   - 敏感信息脱敏
   - 统一日志格式

8. 安全规范
   - 密码加密存储
   - 敏感数据加密
   - 参数防注入
   - 权限验证 

