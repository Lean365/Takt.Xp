# 异常处理规范

## 1. 异常类型

1. 基础异常类
   ```csharp
   public class TaktException : Exception
   {
       public string Code { get; }
       
       public TaktException(string message) : base(message)
       {
           Code = TaktConstants.ErrorCodes.ServerError;
       }
       
       public TaktException(string code, string message) : base(message)
       {
           Code = code;
       }
   }
   ```

2. 业务异常类
   ```csharp
   public class TaktBusinessException : TaktException
   {
       public TaktBusinessException(string message) 
           : base(TaktConstants.ErrorCodes.BusinessError, message)
       {
       }
   }
   ```

3. 验证异常类
   ```csharp
   public class TaktValidationException : TaktException
   {
       public TaktValidationException(string message) 
           : base(TaktConstants.ErrorCodes.ValidationFailed, message)
       {
       }
   }
   ```

## 2. 错误代码

```csharp
public static class ErrorCodes
{
    public const string Unauthorized = "401";       // 未授权
    public const string Forbidden = "403";          // 禁止访问
    public const string NotFound = "404";           // 资源未找到
    public const string ValidationFailed = "400";   // 数据验证失败
    public const string BusinessError = "500";      // 业务错误
    public const string ServerError = "500";        // 服务器错误
}
```

## 3. 异常处理中间件

```csharp
public class TaktExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITaktLogger _logger;

    public TaktExceptionMiddleware(RequestDelegate next, ITaktLogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            TaktException ex => new TaktApiResult 
            { 
                Code = ex.Code,
                Msg = ex.Message 
            },
            _ => new TaktApiResult 
            { 
                Code = ErrorCodes.ServerError,
                Msg = "服务器内部错误" 
            }
        };

        // 记录日志
        _logger.Error(exception, "请求处理异常");

        // 返回响应
        context.Response.StatusCode = int.Parse(response.Code);
        await context.Response.WriteAsJsonAsync(response);
    }
}
```

## 4. 异常处理规范

1. 异常抛出规范
   - 业务逻辑异常抛出TaktBusinessException
   - 参数验证异常抛出TaktValidationException
   - 权限验证异常抛出TaktUnauthorizedException
   - 资源未找到异常抛出TaktNotFoundException
   - 其他异常抛出TaktException

2. 异常捕获规范
   - 只捕获预期的异常
   - 避免捕获基类Exception
   - 避免空catch块
   - 正确处理或重新抛出异常

3. 异常日志规范
   - 记录完整的异常信息
   - 记录异常发生的上下文
   - 记录用户信息和请求信息
   - 区分异常级别
   - 使用结构化日志

4. 异常返回规范
   - 统一使用TaktApiResult返回
   - 包含错误代码和错误消息
   - 隐藏敏感的异常信息
   - 提供友好的错误提示

## 5. 使用示例

1. 业务逻辑中抛出异常
   ```csharp
   public async Task<TaktUserDto> GetUserAsync(long id)
   {
       var user = await _userRepository.GetByIdAsync(id);
       if (user == null)
       {
           throw new TaktNotFoundException($"用户[{id}]不存在");
       }
       return user.Adapt<TaktUserDto>();
   }
   ```

2. 参数验证抛出异常
   ```csharp
   public async Task<long> CreateUserAsync(TaktUserCreateDto input)
   {
       if (string.IsNullOrEmpty(input.UserName))
       {
           throw new TaktValidationException("用户名不能为空");
       }
       // ...
   }
   ```

3. 权限验证抛出异常
   ```csharp
   public async Task DeleteUserAsync(long id)
   {
       if (!await _permissionChecker.HasPermissionAsync("system:user:delete"))
       {
           throw new TaktUnauthorizedException("没有删除用户的权限");
       }
       // ...
   }
   ```

## 6. 最佳实践

1. 异常粒度
   - 异常应该足够具体
   - 避免过于笼统的异常
   - 合理划分异常类型

2. 异常信息
   - 提供清晰的错误消息
   - 包含必要的上下文信息
   - 避免暴露敏感信息

3. 异常性能
   - 避免使用异常控制流程
   - 合理使用异常捕获
   - 避免频繁抛出异常

4. 异常处理层次
   - 在合适的层次处理异常
   - 避免异常处理跨越层次
   - 保持异常处理的一致性 
