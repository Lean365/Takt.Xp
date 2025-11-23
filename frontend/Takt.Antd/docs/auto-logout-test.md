# 自动登出功能测试说明

## 功能概述

本系统实现了完整的自动登出和Token刷新机制：

### 1. 30分钟无操作自动退出
- 用户30分钟内无任何操作时自动登出
- 显示提示消息："由于长时间未操作，您已被自动登出"
- 自动跳转到登录页面

### 2. 有操作时自动刷新Token
- 检测到用户活动时，检查Token是否即将过期（5分钟内）
- 如果Token即将过期，自动刷新获取新Token
- 静默刷新，不影响用户体验
- 如果刷新Token已过期，执行登出操作

## 测试方法

### 方法一：使用测试组件（推荐）
1. 在开发环境下，页面右上角会显示"自动登出功能测试"组件
2. 组件实时显示：
   - 自动登出状态
   - Token信息
   - 剩余时间
3. 提供测试按钮：
   - 触发用户活动
   - 重置定时器
   - 检查并刷新Token
   - 测试自动刷新

### 方法二：使用控制台调试函数
在浏览器控制台中输入以下命令：

```javascript
// 获取自动登出状态
window.TaktAutoLogout.getStatus()

// 获取Token详细信息
window.TaktAutoLogout.getTokenInfo()

// 手动触发用户活动
window.TaktAutoLogout.triggerActivity()

// 手动重置定时器
window.TaktAutoLogout.resetTimer()

// 手动检查并刷新Token
window.TaktAutoLogout.checkAndRefreshToken()

// 测试自动刷新功能
window.TaktAutoLogout.testAutoRefresh()
```

## 监听的事件类型

系统监听以下用户活动事件：
- 鼠标事件：mousemove, mousedown, mouseup, click
- 键盘事件：keypress, keydown, keyup
- 滚动事件：scroll
- 触摸事件：touchstart, touchmove, touchend
- 焦点事件：focus, blur
- 表单事件：input, change, submit
- 拖拽事件：dragstart, drop
- 页面可见性：visibilitychange
- 窗口焦点：focus, blur

## 配置参数

```javascript
// Token过期时间（30分钟）
const INACTIVITY_TIMEOUT = 30 * 60 * 1000

// Token刷新阈值（5分钟内过期时刷新）
const TOKEN_REFRESH_THRESHOLD = 5 * 60 * 1000

// 刷新令牌过期时间（7天）
const REFRESH_TOKEN_EXPIRE_TIME = 7 * 24 * 60 * 60 * 1000
```

## 测试场景

### 场景1：正常使用
1. 登录系统
2. 正常操作（点击、输入、滚动等）
3. 观察Token是否自动刷新
4. 验证30分钟无操作后是否自动登出

### 场景2：Token即将过期
1. 登录系统
2. 等待Token接近过期时间（5分钟内）
3. 触发用户活动
4. 观察Token是否自动刷新

### 场景3：刷新Token过期
1. 登录系统
2. 等待刷新Token过期（7天）
3. 触发用户活动
4. 观察是否执行登出操作

### 场景4：页面切换
1. 登录系统
2. 切换到其他标签页
3. 切换回应用页面
4. 观察是否触发活动检测

## 注意事项

1. **仅在开发环境显示测试组件**：生产环境不会显示测试界面
2. **静默刷新**：Token自动刷新时不显示提示消息，避免干扰用户
3. **错误处理**：Token刷新失败时会显示错误消息并执行登出
4. **状态检查**：系统会检查用户登录状态和页面类型，避免在登录页面启动定时器
5. **资源清理**：页面卸载时会自动清理定时器和事件监听器

## 日志输出

系统会在控制台输出详细的调试信息：
- `[自动登出]` 前缀的日志用于自动登出功能
- `[Auth]` 前缀的日志用于Token相关操作
- 包含详细的状态信息和操作结果

## 故障排除

### 问题1：自动登出不工作
- 检查用户是否已登录
- 检查是否在登录页面
- 查看控制台是否有错误信息

### 问题2：Token不自动刷新
- 检查Token是否即将过期（5分钟内）
- 检查刷新Token是否已过期
- 查看网络请求是否正常

### 问题3：测试组件不显示
- 确认是否在开发环境
- 检查浏览器控制台是否有错误
- 确认组件是否正确导入

## 技术实现

### 核心文件
- `frontend/src/utils/autoLogout.ts` - 自动登出核心逻辑
- `frontend/src/stores/user.ts` - 用户状态管理
- `frontend/src/utils/request.ts` - HTTP请求拦截器
- `frontend/src/components/Business/TaktAutoLogoutTest/index.vue` - 测试组件

### 关键函数
- `initAutoLogout()` - 初始化自动登出功能
- `handleUserActivity()` - 处理用户活动
- `autoRefreshToken()` - 自动刷新Token
- `resetTimer()` - 重置定时器
- `clearAutoLogout()` - 清理自动登出功能 
