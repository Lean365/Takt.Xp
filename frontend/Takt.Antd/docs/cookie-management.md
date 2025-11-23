# Cookie管理配置说明

## 概述

本项目使用`js-cookie`库来管理认证令牌和用户会话信息，替代了之前的`localStorage`存储方式。

## 主要特性

### 1. 安全性增强
- 使用`secure: true`确保仅HTTPS传输
- 使用`sameSite: 'strict'`防止CSRF攻击
- 支持HttpOnly cookie（后端设置）

### 2. 过期时间管理
- **Access Token**: 30分钟过期（短期）
- **Refresh Token**: 7天过期（长期）
- **CSRF Token**: 由后端控制过期时间
- **登录时间**: 7天过期
- **登录失败次数**: 1天过期

### 3. 统一的配置管理
所有cookie配置集中在`src/utils/cookieConfig.ts`文件中管理。

## 配置选项

### 长期Cookie配置（7天）
```typescript
export const LONG_COOKIE_OPTIONS = {
  expires: 7,
  secure: true,
  sameSite: 'strict',
  path: '/'
}
```

### 短期Cookie配置（30分钟）
```typescript
export const SHORT_COOKIE_OPTIONS = {
  expires: 1/48, // 30分钟
  secure: true,
  sameSite: 'strict',
  path: '/'
}
```

### 临时Cookie配置（1天）
```typescript
export const TEMP_COOKIE_OPTIONS = {
  expires: 1,
  secure: true,
  sameSite: 'strict',
  path: '/'
}
```

## 使用方式

### 1. 导入配置
```typescript
import { COOKIE_KEYS, cookieUtils, SHORT_COOKIE_OPTIONS } from '@/utils/cookieConfig'
```

### 2. 设置Cookie
```typescript
// 使用默认配置
cookieUtils.set('key', 'value')

// 使用自定义配置
cookieUtils.set('key', 'value', SHORT_COOKIE_OPTIONS)
```

### 3. 获取Cookie
```typescript
const value = cookieUtils.get('key')
```

### 4. 移除Cookie
```typescript
cookieUtils.remove('key')
```

### 5. 检查Cookie是否存在
```typescript
const exists = cookieUtils.exists('key')
```

## Cookie键名常量

```typescript
export const COOKIE_KEYS = {
  ACCESS_TOKEN: 'access_token',      // 访问令牌
  REFRESH_TOKEN: 'refresh_token',    // 刷新令牌
  CSRF_TOKEN: 'XSRF-TOKEN',         // CSRF令牌
  LAST_LOGIN_TIME: 'lastLoginTime', // 最后登录时间
  LOGIN_FAIL_COUNT: 'loginFailCount' // 登录失败次数
} as const
```

## 错误处理

所有cookie操作都包含try-catch错误处理，失败时会：
1. 记录错误日志
2. 返回适当的默认值
3. 不会中断应用程序流程

## 注意事项

1. **HTTPS要求**: 生产环境必须使用HTTPS，因为`secure: true`选项
2. **浏览器兼容性**: 确保目标浏览器支持`sameSite: 'strict'`
3. **跨域限制**: `sameSite: 'strict'`可能影响跨域请求
4. **存储限制**: Cookie有大小限制（通常4KB），不适合存储大量数据

## 迁移说明

从localStorage迁移到cookie的主要变化：

1. **认证令牌**: 从localStorage迁移到cookie
2. **会话信息**: 从localStorage迁移到cookie
3. **CSRF令牌**: 使用js-cookie替代原生document.cookie操作
4. **错误处理**: 增强了错误处理和日志记录

## 相关文件

- `src/utils/cookieConfig.ts` - Cookie配置和工具函数
- `src/utils/auth.ts` - 认证令牌管理
- `src/utils/request.ts` - HTTP请求拦截器
- `src/stores/userStore.ts` - 用户状态管理
