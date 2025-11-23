# QRCoder和短信帮助类使用指南

## 概述

本文档介绍如何使用 `TaktQrCodeHelper` 和 `TaktSmsHelper` 帮助类来生成二维码和处理短信验证码。

## QRCoder帮助类使用

### 1. 基本二维码生成

```csharp
// 生成PNG格式的二维码
string qrCodePng = TaktQrCodeHelper.GenerateQrCodePng("https://example.com");

// 生成JPEG格式的二维码
string qrCodeJpeg = TaktQrCodeHelper.GenerateQrCodeJpeg("https://example.com", quality: 90);

// 生成SVG格式的二维码
string qrCodeSvg = TaktQrCodeHelper.GenerateQrCodeSvg("https://example.com");
```

### 2. 自定义二维码样式

```csharp
// 自定义颜色和大小
string customQrCode = TaktQrCodeHelper.GenerateQrCodePng(
    content: "https://example.com",
    pixelsPerModule: 25,
    eccLevel: QRCoder.QRCodeGenerator.ECCLevel.H,
    darkColor: Color.FromArgb(0, 123, 255),  // 蓝色
    lightColor: Color.FromArgb(248, 249, 250) // 浅灰色
);
```

### 3. 带Logo的二维码

```csharp
// 生成带Logo的二维码
string qrCodeWithLogo = TaktQrCodeHelper.GenerateQrCodeWithLogo(
    content: "https://example.com",
    logoPath: "path/to/logo.png",
    logoSizePercent: 15
);
```

### 4. 彩色二维码

```csharp
// 生成彩色二维码
string colorQrCode = TaktQrCodeHelper.GenerateColorQrCode(
    content: "https://example.com",
    darkColor: Color.FromArgb(255, 0, 0),    // 红色
    lightColor: Color.FromArgb(255, 255, 0)  // 黄色
);
```

### 5. 保存到文件

```csharp
// 保存二维码到文件
TaktQrCodeHelper.GenerateQrCodeToFile(
    content: "https://example.com",
    filePath: "qrcode.png",
    format: ImageFormat.Png
);
```

### 6. 获取二维码信息

```csharp
// 获取二维码详细信息
var qrCodeInfo = TaktQrCodeHelper.GetQrCodeInfo("https://example.com");
Console.WriteLine($"版本: {qrCodeInfo.Version}");
Console.WriteLine($"模块数: {qrCodeInfo.ModuleCount}");
Console.WriteLine($"数据容量: {qrCodeInfo.DataCapacity}");
```

### 7. 验证二维码内容

```csharp
// 验证二维码内容是否有效
bool isValid = TaktQrCodeHelper.IsValidQrCodeContent("https://example.com");
```

## 短信帮助类使用

### 1. 生成验证码

```csharp
// 生成6位数字验证码
string numericCode = TaktSmsHelper.GenerateNumericCode();

// 生成8位数字验证码
string longCode = TaktSmsHelper.GenerateNumericCode(8);

// 生成字母数字混合验证码
string alphanumericCode = TaktSmsHelper.GenerateAlphanumericCode(
    length: 8,
    includeUppercase: true,
    includeLowercase: false,
    includeNumbers: true
);

// 生成加密验证码
string secureCode = TaktSmsHelper.GenerateSecureCode();
```

### 2. 验证手机号和验证码

```csharp
// 验证手机号格式
bool isValidPhone = TaktSmsHelper.IsValidPhoneNumber("13800138000");

// 验证验证码格式
bool isValidCode = TaktSmsHelper.IsValidCode("123456");
```

### 3. 验证码哈希处理

```csharp
// 生成盐值
string salt = TaktSmsHelper.GenerateSalt();

// 生成验证码哈希
string codeHash = TaktSmsHelper.GenerateCodeHash("123456", salt);

// 验证验证码哈希
bool isValidHash = TaktSmsHelper.VerifyCodeHash("123456", salt, codeHash);
```

### 4. 频率限制检查

```csharp
// 检查发送频率限制
var frequencyResult = TaktSmsHelper.CheckFrequencyLimit(
    phone: "13800138000",
    currentTime: DateTime.Now,
    lastSendTime: DateTime.Now.AddMinutes(-2),
    hourlyCount: 5,
    dailyCount: 20,
    hourlyLimit: 10,
    dailyLimit: 50,
    minIntervalSeconds: 60
);

if (frequencyResult.CanSend)
{
    Console.WriteLine("可以发送验证码");
}
else
{
    Console.WriteLine($"不能发送: {frequencyResult.Reason}");
}
```

### 5. 短信内容生成

```csharp
// 生成短信内容
string smsContent = TaktSmsHelper.GenerateSmsContent(
    template: "您的验证码是：{code}，有效期{expiration}分钟。【{company}】",
    code: "123456",
    expirationMinutes: 5,
    companyName: "Takt.Xp"
);
```

### 6. 缓存键生成

```csharp
// 生成验证码缓存键
string codeCacheKey = TaktSmsHelper.GenerateCacheKey("13800138000", "login");

// 生成发送次数缓存键
string countCacheKey = TaktSmsHelper.GenerateCountCacheKey("13800138000", "login", "hourly");
```

### 7. 运营商信息查询

```csharp
// 获取运营商信息
var carrierInfo = TaktSmsHelper.GetCarrierInfo("13800138000");
if (carrierInfo.IsValid)
{
    Console.WriteLine($"运营商: {carrierInfo.Carrier}");
    Console.WriteLine($"号段: {carrierInfo.Prefix}");
}
```

### 8. 测试验证码生成

```csharp
// 生成测试验证码（仅用于开发环境）
string testCode = TaktSmsHelper.GenerateTestCode("13800138000");
// 同一手机号总是生成相同的测试码
```

## 在服务中使用

### 短信服务中使用帮助类

```csharp
public class TaktSmsService
{
    public async Task<TaktSendSmsCodeResponse> SendVerificationCodeAsync(TaktSendSmsCodeRequest request)
    {
        // 验证手机号
        if (!TaktSmsHelper.IsValidPhoneNumber(request.Phone))
        {
            return new TaktSendSmsCodeResponse
            {
                Success = false,
                Message = "无效的手机号格式"
            };
        }

        // 生成验证码
        string verificationCode = TaktSmsHelper.GenerateSecureCode();

        // 生成缓存键
        string cacheKey = TaktSmsHelper.GenerateCacheKey(request.Phone, request.CodeType.ToString());

        // 生成短信内容
        string smsContent = TaktSmsHelper.GenerateSmsContent(
            template: null, // 使用默认模板
            code: verificationCode,
            expirationMinutes: 5
        );

        // 存储到缓存
        await _cache.SetAsync(cacheKey, verificationCode, TimeSpan.FromMinutes(5));

        // 发送短信
        await SendSmsAsync(request.Phone, smsContent);

        return new TaktSendSmsCodeResponse
        {
            Success = true,
            Message = "验证码发送成功"
        };
    }
}
```

### 二维码服务中使用帮助类

```csharp
public class TaktQrCodeService
{
    public async Task<TaktGenerateQrCodeResponse> GenerateQrCodeAsync(TaktGenerateQrCodeRequest request)
    {
        // 生成二维码内容
        string qrCodeContent = GenerateQrCodeContent(qrCodeId, request.QrCodeType);

        // 验证内容
        if (!TaktQrCodeHelper.IsValidQrCodeContent(qrCodeContent))
        {
            throw new ArgumentException("无效的二维码内容");
        }

        // 生成二维码图片
        string qrCodeImage = TaktQrCodeHelper.GenerateQrCodePng(
            content: qrCodeContent,
            pixelsPerModule: 20,
            eccLevel: QRCoder.QRCodeGenerator.ECCLevel.M
        );

        // 获取二维码信息
        var qrCodeInfo = TaktQrCodeHelper.GetQrCodeInfo(qrCodeContent);

        return new TaktGenerateQrCodeResponse
        {
            QrCodeId = qrCodeId,
            QrCodeContent = qrCodeContent,
            QrCodeImage = qrCodeImage,
            ExpiresIn = 300,
            CreateTime = DateTime.Now
        };
    }
}
```

## 最佳实践

### 1. 二维码生成最佳实践

- 使用适当的纠错级别（ECC Level）
  - L: 7% 纠错能力，适合大尺寸二维码
  - M: 15% 纠错能力，推荐使用
  - Q: 25% 纠错能力，适合小尺寸二维码
  - H: 30% 纠错能力，适合带Logo的二维码

- 选择合适的像素密度
  - 20-25 pixels/module: 适合网页显示
  - 10-15 pixels/module: 适合移动设备
  - 30+ pixels/module: 适合打印

### 2. 短信验证码最佳实践

- 使用加密验证码生成器
- 实现频率限制防止滥用
- 设置合理的有效期
- 记录发送日志
- 支持多种验证码类型

### 3. 安全考虑

- 验证码不要明文存储
- 使用HTTPS传输
- 实现防重放攻击机制
- 定期清理过期数据
- 监控异常发送行为

## 错误处理

```csharp
try
{
    // 生成二维码
    string qrCode = TaktQrCodeHelper.GenerateQrCodePng(content);
}
catch (ArgumentException ex)
{
    // 处理参数错误
    _logger.Error("二维码生成参数错误: {Error}", ex.Message);
}
catch (Exception ex)
{
    // 处理其他错误
    _logger.Error("二维码生成失败: {Error}", ex.Message);
}
```

## 性能优化

1. **缓存二维码**: 对于相同内容的二维码，可以缓存生成结果
2. **异步处理**: 短信发送使用异步方法
3. **批量处理**: 大量二维码生成时考虑批量处理
4. **资源释放**: 及时释放图片资源

## 总结

`TaktQrCodeHelper` 和 `TaktSmsHelper` 提供了完整的二维码生成和短信验证码处理功能，遵循最佳实践和安全标准，可以满足各种业务需求。 

