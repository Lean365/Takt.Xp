// ===================================================================
// 项目名称: Lean.Takt
// 文件名称: iconColor.ts 
// 创建日期: 2024-03-20
// 描述: 图标随机颜色Vue指令
// ===================================================================

import type { Directive, DirectiveBinding, App } from 'vue'
import { 
  setIconRandomColor, 
  setIconThemeColor, 
  setIconGradientColor, 
  setIconAnimation, 
  setIconSize, 
  setIconStatus,
  clearIconStyles,
  generateRandomColor
} from '@/utils/iconColorUtil'

/**
 * 图标随机颜色指令
 * 用法: v-icon-color="'random'" 或 v-icon-color="{ type: 'theme', value: 'primary' }"
 */
export const iconColor: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    const value = binding.value
    
    if (typeof value === 'string') {
      // 简单用法：v-icon-color="'random'"
      if (value === 'random') {
        setIconRandomColor(el)
      } else {
        // 主题色用法：v-icon-color="'primary'"
        setIconThemeColor(el, value as any)
      }
    } else if (typeof value === 'object' && value !== null) {
      // 对象用法：v-icon-color="{ type: 'random', seed: 'custom-seed' }"
      const { type, seed, animation, size, status } = value
      
      switch (type) {
        case 'random':
          setIconRandomColor(el, seed)
          break
        case 'theme':
          setIconThemeColor(el, value.value)
          break
        case 'gradient':
          setIconGradientColor(el, value.value)
          break
        case 'animation':
          setIconAnimation(el, animation)
          break
        case 'size':
          setIconSize(el, size)
          break
        case 'status':
          setIconStatus(el, status)
          break
        default:
          console.warn(`[图标指令] 未知的类型: ${type}`)
      }
    }
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    // 清除之前的样式
    clearIconStyles(el)
    
    // 重新应用新样式
    const value = binding.value
    
    if (typeof value === 'string') {
      if (value === 'random') {
        setIconRandomColor(el)
      } else {
        setIconThemeColor(el, value as any)
      }
    } else if (typeof value === 'object' && value !== null) {
      const { type, seed, animation, size, status } = value
      
      switch (type) {
        case 'random':
          setIconRandomColor(el, seed)
          break
        case 'theme':
          setIconThemeColor(el, value.value)
          break
        case 'gradient':
          setIconGradientColor(el, value.value)
          break
        case 'animation':
          setIconAnimation(el, animation)
          break
        case 'size':
          setIconSize(el, size)
          break
        case 'status':
          setIconStatus(el, status)
          break
      }
    }
  },
  
  unmounted(el: HTMLElement) {
    // 组件卸载时清除样式
    clearIconStyles(el)
  }
}

/**
 * 图标随机颜色指令（简化版）
 * 用法: v-icon-random="'custom-seed'"
 */
export const iconRandom: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    const seed = binding.value || el.textContent || el.className || 'default'
    setIconRandomColor(el, seed)
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    clearIconStyles(el)
    const seed = binding.value || el.textContent || el.className || 'default'
    setIconRandomColor(el, seed)
  },
  
  unmounted(el: HTMLElement) {
    clearIconStyles(el)
  }
}

/**
 * 图标主题色指令
 * 用法: v-icon-theme="'primary'"
 */
export const iconTheme: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    setIconThemeColor(el, binding.value)
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    clearIconStyles(el)
    setIconThemeColor(el, binding.value)
  },
  
  unmounted(el: HTMLElement) {
    clearIconStyles(el)
  }
}

/**
 * 图标动画指令
 * 用法: v-icon-animation="'pulse'"
 */
export const iconAnimation: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    setIconAnimation(el, binding.value)
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    // 移除之前的动画类
    el.classList.remove('Takt-icon-pulse', 'Takt-icon-spin', 'Takt-icon-bounce', 'Takt-icon-shake')
    setIconAnimation(el, binding.value)
  },
  
  unmounted(el: HTMLElement) {
    el.classList.remove('Takt-icon-pulse', 'Takt-icon-spin', 'Takt-icon-bounce', 'Takt-icon-shake')
  }
}

/**
 * 图标尺寸指令
 * 用法: v-icon-size="'lg'"
 */
export const iconSize: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    setIconSize(el, binding.value)
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    // 移除之前的尺寸类
    el.classList.remove('Takt-icon-xs', 'Takt-icon-sm', 'Takt-icon-md', 'Takt-icon-lg', 'Takt-icon-xl', 'Takt-icon-xxl')
    setIconSize(el, binding.value)
  },
  
  unmounted(el: HTMLElement) {
    el.classList.remove('Takt-icon-xs', 'Takt-icon-sm', 'Takt-icon-md', 'Takt-icon-lg', 'Takt-icon-xl', 'Takt-icon-xxl')
  }
}

/**
 * 图标状态指令
 * 用法: v-icon-status="'loading'"
 */
export const iconStatus: Directive = {
  mounted(el: HTMLElement, binding: DirectiveBinding) {
    setIconStatus(el, binding.value)
  },
  
  updated(el: HTMLElement, binding: DirectiveBinding) {
    // 移除之前的状态类
    el.classList.remove('Takt-icon-disabled', 'Takt-icon-loading', 'Takt-icon-selected', 'Takt-icon-highlight')
    setIconStatus(el, binding.value)
  },
  
  unmounted(el: HTMLElement) {
    el.classList.remove('Takt-icon-disabled', 'Takt-icon-loading', 'Takt-icon-selected', 'Takt-icon-highlight')
  }
}

/**
 * 注册所有图标颜色指令
 * @param app Vue应用实例
 */
export function setupIconColor(app: App) {
  app.directive('icon-color', iconColor)
  app.directive('icon-random', iconRandom)
  app.directive('icon-theme', iconTheme)
  app.directive('icon-animation', iconAnimation)
  app.directive('icon-size', iconSize)
  app.directive('icon-status', iconStatus)
  
  console.log('[图标指令] 图标颜色指令注册完成')
}

// 默认导出主要指令
export default iconColor 
