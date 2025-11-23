# Takt.Xp 配置文件说明

本文档详细说明了 Takt.Xp 项目的配置文件结构和各项配置的含义。

## 配置文件结构

项目包含以下配置文件：
- `appsettings.json` - 生产环境配置
- `appsettings.Development.json` - 开发环境配置

## 主要配置项

### 1. 短信认证配置 (SmsAuth)

```json
{
  "SmsAuth": {
    "Enabled": true,
    "Provider": "Aliyun",
    "AccessKeyId": "your-sms-access-key-id",
    "AccessKeySecret": "your-sms-access-key-secret",
    "SignName": "极限节拍",
    "TemplateCode": "SMS_123456789",
    "ExpirationMinutes": 5,
    "MaxSendCountPerHour": 10,
    "EnableCaptcha": true,
    "CaptchaRequiredAttempts": 3,
    "EnableFrequencyLimit": true,
    "EnableIpLimit": true,
    "MaxSendCountPerIpPerHour": 20
  }
}
```

**配置说明：**
- `Enabled`: 是否启用短信登录功能
- `Provider`: 短信服务提供商（如：Aliyun、Tencent等）
- `AccessKeyId`: 短信服务访问密钥ID
- `AccessKeySecret`: 短信服务访问密钥Secret
- `SignName`: 短信签名
- `TemplateCode`: 短信模板代码
- `ExpirationMinutes`: 验证码过期时间（分钟）
- `MaxSendCountPerHour`: 每小时最大发送次数
- `EnableCaptcha`: 是否启用图形验证码
- `CaptchaRequiredAttempts`: 需要图形验证码的尝试次数
- `EnableFrequencyLimit`: 是否启用频率限制
- `EnableIpLimit`: 是否启用IP限制
- `MaxSendCountPerIpPerHour`: 每个IP每小时最大发送次数

**对应的配置类：** `TaktSmsAuthOptions`

### 2. 二维码认证配置 (QrCodeAuth)

```json
{
  "QrCodeAuth": {
    "Enabled": true,
    "ExpirationMinutes": 5,
    "PixelsPerModule": 20,
    "EccLevel": "M",
    "BaseUrl": "https://localhost:7249/qr",
    "EnableThirdPartyLogin": true,
    "EnableWeChatLogin": true,
    "EnableAlipayLogin": true,
    "EnableDeviceTracking": true,
    "MaxQrCodePerUser": 5,
    "AutoCleanupEnabled": true,
    "CleanupIntervalMinutes": 10
  }
}
```

**配置说明：**
- `Enabled`: 是否启用二维码登录功能
- `ExpirationMinutes`: 二维码过期时间（分钟）
- `PixelsPerModule`: 二维码图片像素大小
- `EccLevel`: 二维码纠错级别（L=7%, M=15%, Q=25%, H=30%）
- `BaseUrl`: 二维码基础URL
- `EnableThirdPartyLogin`: 是否启用第三方扫码登录
- `EnableWeChatLogin`: 是否启用微信扫码登录
- `EnableAlipayLogin`: 是否启用支付宝扫码登录
- `EnableDeviceTracking`: 是否启用设备跟踪
- `MaxQrCodePerUser`: 每个用户最大二维码数量
- `AutoCleanupEnabled`: 是否启用自动清理
- `CleanupIntervalMinutes`: 清理间隔时间（分钟）

**对应的配置类：** `TaktQrCodeAuthOptions`

### 3. OAuth第三方登录配置 (OAuth)

```json
{
  "OAuth": {
    "Enabled": true,
    "DefaultRedirectUri": "https://localhost:7249/oauth/callback",
    "SessionTimeoutMinutes": 30,
    "GitHub": {
      "Enabled": false,
      "ClientId": "your-github-client-id",
      "ClientSecret": "your-github-client-secret",
      "RedirectUri": "https://localhost:7249/oauth/github/callback",
      "Scope": "read:user,user:email",
      "AuthorizationUrl": "https://github.com/login/oauth/authorize",
      "TokenUrl": "https://github.com/login/oauth/access_token",
      "UserInfoUrl": "https://api.github.com/user"
    },
    "Google": {
      "Enabled": false,
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret",
      "RedirectUri": "https://localhost:7249/oauth/google/callback",
      "Scope": "openid email profile",
      "AuthorizationUrl": "https://accounts.google.com/o/oauth2/v2/auth",
      "TokenUrl": "https://oauth2.googleapis.com/token",
      "UserInfoUrl": "https://www.googleapis.com/oauth2/v2/userinfo"
    },
    "Facebook": {
      "Enabled": false,
      "AppId": "your-facebook-app-id",
      "AppSecret": "your-facebook-app-secret",
      "RedirectUri": "https://localhost:7249/oauth/facebook/callback",
      "Scope": "email,public_profile",
      "AuthorizationUrl": "https://www.facebook.com/v12.0/dialog/oauth",
      "TokenUrl": "https://graph.facebook.com/v12.0/oauth/access_token",
      "UserInfoUrl": "https://graph.facebook.com/me"
    },
    "Twitter": {
      "Enabled": false,
      "ApiKey": "your-twitter-api-key",
      "ApiKeySecret": "your-twitter-api-secret",
      "RedirectUri": "https://localhost:7249/oauth/twitter/callback",
      "Scope": "tweet.read users.read offline.access",
      "AuthorizationUrl": "https://twitter.com/i/oauth2/authorize",
      "TokenUrl": "https://api.twitter.com/2/oauth2/token",
      "UserInfoUrl": "https://api.twitter.com/2/users/me"
    },
    "QQ": {
      "Enabled": false,
      "AppId": "your-qq-app-id",
      "AppKey": "your-qq-app-key",
      "RedirectUri": "https://localhost:7249/oauth/qq/callback",
      "Scope": "get_user_info",
      "AuthorizationUrl": "https://graph.qq.com/oauth2.0/authorize",
      "TokenUrl": "https://graph.qq.com/oauth2.0/token",
      "UserInfoUrl": "https://graph.qq.com/user/get_user_info"
    },
    "WeChat": {
      "Enabled": true,
      "AppId": "your-wechat-app-id",
      "AppSecret": "your-wechat-app-secret",
      "RedirectUri": "https://localhost:7249/oauth/wechat/callback",
      "Scope": "snsapi_login",
      "AuthorizationUrl": "https://open.weixin.qq.com/connect/qrconnect",
      "TokenUrl": "https://api.weixin.qq.com/sns/oauth2/access_token",
      "UserInfoUrl": "https://api.weixin.qq.com/sns/userinfo"
    },
    "Alipay": {
      "Enabled": true,
      "AppId": "your-alipay-app-id",
      "PrivateKey": "your-alipay-private-key",
      "PublicKey": "your-alipay-public-key",
      "RedirectUri": "https://localhost:7249/oauth/alipay/callback",
      "Scope": "auth_user",
      "AuthorizationUrl": "https://openauth.alipay.com/oauth2/publicAppAuthorize.htm",
      "TokenUrl": "https://openapi.alipay.com/gateway.do",
      "UserInfoUrl": "https://openapi.alipay.com/gateway.do"
    },
    "Gitee": {
      "Enabled": false,
      "ClientId": "your-gitee-client-id",
      "ClientSecret": "your-gitee-client-secret",
      "RedirectUri": "https://localhost:7249/oauth/gitee/callback",
      "Scope": "user_info emails",
      "AuthorizationUrl": "https://gitee.com/oauth/authorize",
      "TokenUrl": "https://gitee.com/oauth/token",
      "UserInfoUrl": "https://gitee.com/api/v5/user"
    }
  }
}
```

**配置说明：**
- `Enabled`: 是否启用OAuth登录功能
- `DefaultRedirectUri`: 默认回调地址
- `SessionTimeoutMinutes`: 会话超时时间（分钟）
- 各第三方登录提供商配置：
  - `Enabled`: 是否启用该提供商
  - `ClientId/AppId`: 客户端ID或应用ID
  - `ClientSecret/AppSecret`: 客户端密钥或应用密钥
  - `RedirectUri`: 回调地址
  - `Scope`: 授权范围
  - `AuthorizationUrl`: 授权URL
  - `TokenUrl`: 令牌URL
  - `UserInfoUrl`: 用户信息URL

**对应的配置类：** `TaktOAuthOptions`

## 环境变量配置

### 生产环境配置

在生产环境中，建议使用环境变量来配置敏感信息：

```bash
# 短信服务配置
export SMS_AUTH__ACCESS_KEY_ID="your-sms-access-key-id"
export SMS_AUTH__ACCESS_KEY_SECRET="your-sms-access-key-secret"
export SMS_AUTH__SIGN_NAME="极限节拍"
export SMS_AUTH__TEMPLATE_CODE="SMS_123456789"

# OAuth配置
export OAUTH__WECHAT__APP_ID="your-wechat-app-id"
export OAUTH__WECHAT__APP_SECRET="your-wechat-app-secret"
export OAUTH__ALIPAY__APP_ID="your-alipay-app-id"
export OAUTH__ALIPAY__PRIVATE_KEY="your-alipay-private-key"
export OAUTH__ALIPAY__PUBLIC_KEY="your-alipay-public-key"
```

### 开发环境配置

在开发环境中，可以直接在 `appsettings.Development.json` 中配置：

```json
{
  "SmsAuth": {
    "Enabled": true,
    "Provider": "Aliyun",
    "AccessKeyId": "dev-sms-access-key-id",
    "AccessKeySecret": "dev-sms-access-key-secret",
    "SignName": "极限节拍",
    "TemplateCode": "SMS_123456789"
  },
  "QrCodeAuth": {
    "Enabled": true,
    "ExpirationMinutes": 5,
    "BaseUrl": "https://localhost:7249/qr"
  },
  "OAuth": {
    "Enabled": true,
    "WeChat": {
      "Enabled": true,
      "AppId": "dev-wechat-app-id",
      "AppSecret": "dev-wechat-app-secret"
    },
    "Alipay": {
      "Enabled": true,
      "AppId": "dev-alipay-app-id",
      "PrivateKey": "dev-alipay-private-key",
      "PublicKey": "dev-alipay-public-key"
    }
  }
}
```

## 配置验证

系统启动时会验证以下配置：

1. **短信配置验证**：
   - 检查 `SmsAuth.Enabled` 为 true 时，必须配置 `AccessKeyId` 和 `AccessKeySecret`
   - 验证 `SignName` 和 `TemplateCode` 不能为空

2. **二维码配置验证**：
   - 检查 `QrCodeAuth.Enabled` 为 true 时，必须配置 `BaseUrl`
   - 验证 `ExpirationMinutes` 在合理范围内（1-60分钟）

3. **OAuth配置验证**：
   - 检查 `OAuth.Enabled` 为 true 时，至少有一个提供商启用
   - 验证启用的提供商必须配置完整的认证信息

## 安全建议

1. **敏感信息保护**：
   - 不要在代码中硬编码敏感信息
   - 使用环境变量或密钥管理服务
   - 定期轮换密钥

2. **权限控制**：
   - 为不同环境配置不同的权限
   - 限制API访问频率
   - 启用IP白名单

3. **监控和日志**：
   - 记录所有认证尝试
   - 监控异常登录行为
   - 设置告警机制

## 故障排除

### 常见问题

1. **短信发送失败**：
   - 检查 `AccessKeyId` 和 `AccessKeySecret` 是否正确
   - 验证 `SignName` 是否已审核通过
   - 确认 `TemplateCode` 是否有效

2. **二维码生成失败**：
   - 检查 `BaseUrl` 是否可访问
   - 验证 `ExpirationMinutes` 配置是否合理
   - 确认二维码服务是否正常

3. **OAuth登录失败**：
   - 检查提供商配置是否完整
   - 验证 `RedirectUri` 是否在提供商后台注册
   - 确认 `Scope` 权限是否足够

### 调试方法

1. **启用详细日志**：
```json
{
  "Logging": {
    "LogLevel": {
      "Takt.Application.Services.Identity": "Debug",
"Takt.Infrastructure.Security": "Debug"
    }
  }
}
```

2. **检查配置加载**：
```csharp
// 在服务中注入配置并输出
var smsOptions = serviceProvider.GetRequiredService<IOptions<TaktSmsAuthOptions>>();
var qrCodeOptions = serviceProvider.GetRequiredService<IOptions<TaktQrCodeAuthOptions>>();
var oauthOptions = serviceProvider.GetRequiredService<IOptions<TaktOAuthOptions>>();
```

## 总结

本文档详细说明了 Takt.Xp 项目的认证配置结构。通过合理配置 `SmsAuth`、`QrCodeAuth` 和 `OAuth` 选项，可以实现完整的多种登录方式支持，包括：

- 传统用户名密码登录
- 短信验证码登录
- 二维码扫码登录
- 第三方OAuth登录

配置采用分层结构，支持环境变量覆盖，确保安全性和灵活性。建议在生产环境中使用环境变量配置敏感信息，并定期审查和更新配置。 

