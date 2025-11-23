# Request Utils 使用说明

## 日志分级系统

### 日志级别

```typescript
const log = {
  debug: import.meta.env.DEV ? console.log : () => {},  // 开发环境显示，生产环境隐藏
  warn: console.warn,                                   // 警告信息
  error: console.error,                                 // 错误信息
  info: console.info                                    // 一般信息
};
```

### 使用示例

```typescript
import { request } from '@/utils/request'

// 使用日志分级
log.debug('[Request] 请求信息:', config);
log.warn('[CSRF] 未找到CSRF令牌，但后端会自动生成');
log.error('[Response] 服务器错误:', errorMessage);
log.info('[Auth] 用户登录成功');
```

## 类型扩展

### 新增配置选项

```typescript
interface AxiosRequestConfig {
  skipAuth?: boolean;      // 跳过 Token 校验
}
```

### 使用示例

#### 跳过认证

```typescript
// 跳过 Token 校验的请求（如登录接口）
const loginResponse = await request({
  url: '/api/auth/login',
  method: 'POST',
  data: { username, password },
  skipAuth: true  // 跳过认证
});
```

## 最佳实践

1. **日志使用**：
   - `debug`: 用于开发调试信息
   - `warn`: 用于警告信息，不影响功能
   - `error`: 用于错误信息
   - `info`: 用于一般信息

2. **配置选项**：
   - `skipAuth`: 仅在不需要认证的接口上使用

3. **环境适配**：
   - 开发环境：显示所有日志
   - 生产环境：隐藏 debug 日志，减少控制台输出

4. **字段名处理**：
   - 后端已统一处理 JSON 序列化为 camelCase
   - 前端不再进行字段名转换，直接使用后端返回的数据
