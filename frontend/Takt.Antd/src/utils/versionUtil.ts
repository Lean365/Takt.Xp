//===================================================================
// 项目名 : Lean.Takt
// 文件名 : version.ts
// 创建者 : Claude
// 创建时间: 2024-01-16
// 版本号 : v1.0.0
// 描述    : 版本信息工具函数
//===================================================================

/**
 * 获取应用版本信息
 */
export function getAppVersion(): string {
  try {
    // 方法1: 从环境变量获取（推荐）
    if (import.meta.env.VITE_APP_VERSION) {
      return import.meta.env.VITE_APP_VERSION
    }
    
    // 方法2: 从全局变量获取
    if (typeof window !== 'undefined' && (window as any).__APP_VERSION__) {
      return (window as any).__APP_VERSION__
    }
    
    // 方法3: 从package.json获取（需要构建时注入）
    if (import.meta.env.VITE_PACKAGE_VERSION) {
      return import.meta.env.VITE_PACKAGE_VERSION
    }
    
    // 默认版本
    return '0.0.0'
  } catch (error) {
    console.warn('[版本工具] 无法获取版本信息:', error)
    return '0.0.0'
  }
}

/**
 * 获取应用名称
 */
export function getAppName(): string {
  try {
    // 从环境变量获取
    if (import.meta.env.VITE_APP_NAME) {
      return import.meta.env.VITE_APP_NAME
    }
    
    // 从全局变量获取
    if (typeof window !== 'undefined' && (window as any).__APP_NAME__) {
      return (window as any).__APP_NAME__
    }
    
    // 默认名称
    return 'Lean.Takt'
  } catch (error) {
    console.warn('[版本工具] 无法获取应用名称:', error)
    return 'Lean.Takt'
  }
}

/**
 * 获取构建时间
 */
export function getBuildTime(): string {
  try {
    // 从环境变量获取
    if (import.meta.env.VITE_BUILD_TIME) {
      return import.meta.env.VITE_BUILD_TIME
    }
    
    // 从全局变量获取
    if (typeof window !== 'undefined' && (window as any).__BUILD_TIME__) {
      return (window as any).__BUILD_TIME__
    }
    
    // 默认时间
    return new Date().toISOString()
  } catch (error) {
    console.warn('[版本工具] 无法获取构建时间:', error)
    return new Date().toISOString()
  }
}

/**
 * 获取完整的版本信息
 */
export function getVersionInfo() {
  return {
    version: getAppVersion(),
    name: getAppName(),
    buildTime: getBuildTime()
  }
} 
