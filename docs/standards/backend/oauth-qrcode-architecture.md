# OAuth第三方登录与二维码登录架构设计

## 概述

本文档说明OAuth第三方登录与二维码登录的正确架构设计，避免概念混淆。

## 两种登录方式的区别

### 1. OAuth第三方登录（完全独立）

**定义**：用户通过第三方平台（如GitHub、Google、微信、支付宝等）进行授权登录

**流程**：
```
用户点击"GitHub登录" → 跳转到GitHub授权页面 → 用户授权 → 返回应用并登录
```

**特点**：
- 直接跳转到第三方平台
- 不涉及二维码
- 每个提供商独立配置
- 支持多种第三方平台

**支持的提供商**：
- GitHub
- Google
- Facebook
- Twitter
- QQ
- 微信
- 支付宝
- Gitee

### 2. 二维码登录（仅支持微信和支付宝扫码）

**定义**：PC端生成二维码，用户用手机扫描完成登录

**流程**：
```
PC端生成二维码 → 手机扫描 → 手机确认 → PC端自动登录
```

**特点**：
- 涉及二维码生成和扫描
- 仅支持微信和支付宝扫码
- 需要手机APP配合

## 架构设计

### 1. 服务分离

```csharp
// OAuth第三方登录服务（完全独立）
public interface ITaktOAuthService
{
    Task<List<TaktOAuthProviderDto>> GetProvidersAsync();
    Task<string> GetAuthorizationUrlAsync(string provider, string? redirectUri = null);
    Task<TaktLoginResultDto> HandleCallbackAsync(string provider, string code, string state);
    // ... 其他OAuth相关方法
}

// 二维码登录服务（仅支持微信和支付宝扫码）
public interface ITaktQrCodeService
{
    Task<TaktGenerateQrCodeResponse> GenerateQrCodeAsync(TaktGenerateQrCodeRequest request);
    Task<TaktCheckQrCodeStatusResponse> CheckQrCodeStatusAsync(TaktCheckQrCodeStatusRequest request);
    // ... 其他二维码相关方法
}
```

### 2. 配置分离

```json
{
  "TaktOAuth": {
    "Enabled": true,
    "GitHub": {
      "Enabled": true,
      "ClientId": "your-github-client-id",
      "ClientSecret": "your-github-client-secret"
    },
    "Google": {
      "Enabled": true,
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret"
    },
    "WeChat": {
      "Enabled": true,
      "AppId": "your-wechat-app-id",
      "AppSecret": "your-wechat-app-secret"
    },
    "Alipay": {
      "Enabled": true,
      "AppId": "your-alipay-app-id",
      "PrivateKey": "your-alipay-private-key"
    }
  },
  "TaktQrCode": {
    "Enabled": true,
    "EnableWeChatLogin": true,    // 仅支持微信扫码
    "EnableAlipayLogin": true,    // 仅支持支付宝扫码
    "ExpirationMinutes": 5
  }
}
```

### 3. 控制器分离

```csharp
// OAuth第三方登录控制器
[ApiController]
[Route("api/[controller]")]
public class TaktOAuthController : TaktBaseController
{
    [HttpGet("providers")]
    public async Task<IActionResult> GetProviders()
    {
        // 获取支持的第三方登录提供商
    }

    [HttpGet("authorize/{provider}")]
    public async Task<IActionResult> Authorize(string provider)
    {
        // 开始OAuth授权流程
    }

    [HttpGet("callback/{provider}")]
    public async Task<IActionResult> Callback(string provider, string code, string state)
    {
        // 处理OAuth回调
    }
}

// 二维码登录控制器
[ApiController]
[Route("api/[controller]")]
public class TaktQrCodeAuthController : TaktBaseController
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateQrCode([FromBody] TaktGenerateQrCodeRequest request)
    {
        // 生成二维码（支持普通二维码和微信/支付宝扫码二维码）
    }

    [HttpPost("check-status")]
    public async Task<IActionResult> CheckQrCodeStatus([FromBody] TaktCheckQrCodeStatusRequest request)
    {
        // 检查二维码状态
    }
}
```

## 二维码登录的两种模式

### 1. 普通二维码登录

```csharp
case TaktQrCodeType.Login:
case TaktQrCodeType.BindDevice:
case TaktQrCodeType.Authorize:
    // 生成应用内部的二维码
    qrCodeContent = GenerateQrCodeContent(qrCodeId, request.QrCodeType);
    qrCodeImage = GenerateQrCodeImage(qrCodeContent);
    break;
```

**二维码内容**：`https://your-app.com/qr?id=abc123&type=1`

**使用场景**：
- 手机APP扫描二维码
- 在手机APP中确认登录
- PC端自动登录

### 2. 微信/支付宝扫码登录

```csharp
case TaktQrCodeType.WeChatLogin:
    // 生成微信授权二维码
    qrCodeContent = TaktQrCodeHelper.GenerateWeChatQrCodeUrl(...);
    qrCodeImage = TaktQrCodeHelper.GenerateWeChatQrCodeImage(...);
    break;

case TaktQrCodeType.AlipayLogin:
    // 生成支付宝授权二维码
    qrCodeContent = TaktQrCodeHelper.GenerateAlipayQrCodeUrl(...);
    qrCodeImage = TaktQrCodeHelper.GenerateAlipayQrCodeImage(...);
    break;
```

**二维码内容**：微信/支付宝的授权URL

**使用场景**：
- 用户用微信/支付宝扫描二维码
- 在微信/支付宝中授权
- PC端自动登录

## 配置说明

### OAuth配置（TaktOAuthOptions）

```csharp
public class TaktOAuthOptions
{
    public bool Enabled { get; set; } = true;
    public GitHubOptions GitHub { get; set; } = new();
    public GoogleOptions Google { get; set; } = new();
    public WeChatOptions WeChat { get; set; } = new();
    public AlipayOptions Alipay { get; set; } = new();
    // ... 其他提供商
}
```

### 二维码配置（TaktQrCodeOptions）

```csharp
public class TaktQrCodeOptions
{
    public int ExpirationMinutes { get; set; } = 5;
    public bool EnableWeChatLogin { get; set; } = false;  // 仅支持微信扫码
    public bool EnableAlipayLogin { get; set; } = false;  // 仅支持支付宝扫码
}
```

## 使用场景

### 1. 用户想要用GitHub登录
- 使用OAuth服务
- 点击"GitHub登录"按钮
- 跳转到GitHub授权页面
- 授权后返回应用登录

### 2. 用户想要用微信扫码登录
- 使用二维码服务
- 生成微信授权二维码
- 用微信扫描二维码
- 在微信中授权
- PC端自动登录

### 3. 用户想要用手机APP扫码登录
- 使用二维码服务
- 生成普通二维码
- 用手机APP扫描
- 在手机APP中确认
- PC端自动登录

## 总结

1. **OAuth第三方登录**：完全独立的功能，支持多种第三方平台，直接跳转授权
2. **二维码登录**：仅支持微信和支付宝扫码，生成授权二维码供手机扫描
3. **两者分离**：不同的服务、不同的配置、不同的控制器
4. **避免混淆**：不要将OAuth的授权URL作为普通二维码的内容

这样的设计确保了：
- 功能清晰分离
- 配置独立管理
- 易于维护和扩展
- 避免概念混淆 
