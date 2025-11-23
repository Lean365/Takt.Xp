# 后端多语言开发规范

## 1. 全局异常处理实现

### 异常过滤器
```csharp
public class TaktGlobalExceptionFilter : IExceptionFilter
{
    private readonly ITaktI18nService _i18nService;

    public TaktGlobalExceptionFilter(ITaktI18nService i18nService)
    {
        _i18nService = i18nService;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        // 获取当前语言
        var langCode = Thread.CurrentThread.CurrentUICulture.Name;
        
        if (context.Exception is BusinessException businessException)
        {
            // 业务异常转换为多语言
            var message = await _i18nService.GetTranslationAsync(businessException.MessageKey, langCode);
            context.Result = new JsonResult(new { 
                code = businessException.Code, 
                message = message 
            });
        }
        else if (context.Exception is ValidationException validationException)
        {
            // 验证异常转换为多语言
            var errors = await TranslateValidationErrors(validationException.Errors, langCode);
            context.Result = new JsonResult(new { 
                code = 400, 
                message = "validation.error", 
                errors = errors 
            });
        }
    }
}
```

## 2. 业务异常多语言实现

### 自定义异常基类
```csharp
public abstract class TaktException : Exception
{
    public string MessageKey { get; }
    public object[] MessageParams { get; }

    protected TaktException(string messageKey, params object[] messageParams)
    {
        MessageKey = messageKey;
        MessageParams = messageParams;
    }
}

public class BusinessException : TaktException
{
    public int Code { get; }

    public BusinessException(string messageKey, int code = 400, params object[] messageParams) 
        : base(messageKey, messageParams)
    {
        Code = code;
    }
}
```

### 使用示例
```csharp
// 抛出业务异常
throw new BusinessException("user.not.found", 404, userId);

// 对应的翻译文件
{
    "user.not.found": "用户 {0} 不存在"
}
```

## 3. 验证异常多语言实现

### FluentValidation配置
```csharp
public class UserValidator : AbstractValidator<UserDto>
{
    private readonly ITaktI18nService _i18nService;

    public UserValidator(ITaktI18nService i18nService)
    {
        _i18nService = i18nService;

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(async _ => await _i18nService.GetTranslationAsync("validation.user.name.required"))
            .MaximumLength(50)
            .WithMessage(async _ => await _i18nService.GetTranslationAsync("validation.user.name.maxlength"));

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage(async _ => await _i18nService.GetTranslationAsync("validation.user.email.invalid"));
    }
}
```

## 4. 响应消息多语言实现

### 统一响应处理
```csharp
public class TaktResult<T>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static async Task<TaktResult<T>> SuccessAsync(
        ITaktI18nService i18nService,
        T data = default,
        string messageKey = "common.success")
    {
        return new TaktResult<T>
        {
            Code = 200,
            Message = await i18nService.GetTranslationAsync(messageKey),
            Data = data
        };
    }

    public static async Task<TaktResult<T>> ErrorAsync(
        ITaktI18nService i18nService,
        string messageKey,
        int code = 400)
    {
        return new TaktResult<T>
        {
            Code = code,
            Message = await i18nService.GetTranslationAsync(messageKey)
        };
    }
}
```

### 控制器基类
```csharp
public abstract class TaktBaseController : ControllerBase
{
    protected readonly ITaktI18nService _i18nService;

    protected TaktBaseController(ITaktI18nService i18nService)
    {
        _i18nService = i18nService;
    }

    protected async Task<IActionResult> SuccessAsync<T>(T data = default, string messageKey = "common.success")
    {
        return Ok(await TaktResult<T>.SuccessAsync(_i18nService, data, messageKey));
    }

    protected async Task<IActionResult> ErrorAsync(string messageKey, int code = 400)
    {
        return BadRequest(await TaktResult<object>.ErrorAsync(_i18nService, messageKey, code));
    }
}
```

## 5. 中间件中的语言切换

### 语言中间件
```csharp
public class TaktLanguageMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITaktI18nService _i18nService;

    public TaktLanguageMiddleware(RequestDelegate next, ITaktI18nService i18nService)
    {
        _next = next;
        _i18nService = i18nService;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. 从请求头获取语言
        var langCode = context.Request.Headers["Accept-Language"].FirstOrDefault();

        // 2. 从Cookie获取语言
        if (string.IsNullOrEmpty(langCode))
        {
            langCode = context.Request.Cookies["Language"];
        }

        // 3. 使用默认语言
        if (string.IsNullOrEmpty(langCode))
        {
            langCode = await _i18nService.GetDefaultLanguageCode();
        }

        // 设置当前线程的语言文化
        var culture = new CultureInfo(langCode);
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;

        await _next(context);
    }
}
```

## 6. 使用示例

### 在控制器中
```csharp
[ApiController]
[Route("api/[controller]")]
public class UserController : TaktBaseController
{
    private readonly IUserService _userService;

    public UserController(ITaktI18nService i18nService, IUserService userService) 
        : base(i18nService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        try
        {
            var user = await _userService.CreateAsync(dto);
            return await SuccessAsync(user, "user.create.success");
        }
        catch (BusinessException ex)
        {
            return await ErrorAsync(ex.MessageKey, ex.Code);
        }
    }
}
```

### 在服务层中
```csharp
public class UserService : IUserService
{
    private readonly ITaktI18nService _i18nService;

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        if (await IsUserNameExists(dto.UserName))
        {
            throw new BusinessException("user.name.duplicate");
        }

        // 创建用户逻辑...
    }
}
```

## 7. 添加新语言步骤

### 第一步：前端配置
1. 在 `frontend/src/stores/app.ts` 中的 `SUPPORTED_LOCALES` 数组中添加新语言代码
2. 确保语言代码符合 IETF 语言标签标准（如：zh-CN、en-US、ja-JP 等）

### 第二步：后端配置
1. 在 `backend/src/Takt.Xp.Infrastructure/Services/Local/TaktLocalizationService.cs` 中添加新语言支持
2. 确保新语言代码与前端配置保持一致

### 第三步：添加翻译文件
1. 后端翻译（必须）
   - 在 `TaktTranslation` 表中添加新语言的翻译数据
   - 复制现有语言的翻译记录
   - 修改 `LangCode` 为新语言代码
   - 翻译 `TranslationValue` 字段内容
   - 确保 `TranslationKey` 与现有语言保持一致

2. 前端翻译（二选一）
   - 方式一：在 `frontend/src/locales` 目录下创建静态翻译文件
     - 文件名格式：`{语言代码}.ts`（如：de-DE.ts）
     - 复制现有语言文件作为模板
     - 翻译所有键值对
   - 方式二：在 `TaktTranslation` 表中添加前端翻译数据
     - 与后端翻译使用相同的表
     - 通过 `Module` 字段区分前后端翻译
     - 确保翻译键值对完整

### 第四步：数据库配置
1. 在语言表（TaktLanguage）中添加新语言记录
2. 设置正确的语言代码、名称、图标和状态

### 第五步：测试验证
1. 重启应用使配置生效
2. 测试新语言切换功能
3. 验证所有翻译是否正确显示
4. 检查特殊字符和格式是否正确

### 注意事项
1. 添加新语言前，确保已完成所有翻译文件的准备工作
2. 对于从右到左（RTL）的语言（如阿拉伯语），需要特别处理文本方向
3. 建议先在测试环境验证新语言支持
4. 确保新添加的语言代码在所有配置中保持一致
5. 添加新语言后，需要更新相关文档
6. 确保 `TaktTranslation` 表中的翻译数据完整，不要遗漏任何键值
7. 如果选择使用 `TaktTranslation` 表存储前端翻译，需要确保前端正确配置了动态加载机制


