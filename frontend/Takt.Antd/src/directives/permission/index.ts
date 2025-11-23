//===================================================================
// 项目名 : Lean.Takt
// 文件名 : index.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 权限验证指令
//===================================================================

import type { App, Directive } from 'vue'
import { useUserStore } from '@/stores/userStore'
import { message } from 'ant-design-vue'

/**
 * 权限验证指令
 * v-hasPermi="['identity:config:list']"
 */
export const hasPermi: Directive = {
  mounted(el: HTMLElement, binding) {
    const { value } = binding
    const permissions = useUserStore().permissions
    
    if (value && value instanceof Array && value.length > 0) {
      const hasPermission = value.some(permission => {
        const has = permissions.includes(permission)
        return has
      })
      
      if (!hasPermission) {
        // 立即弹出权限不足错误消息框
        const missingPermissions = value.filter(permission => !permissions.includes(permission))
        const errorMessage = `权限不足！缺少以下权限：${missingPermissions.join(', ')}`
        
        // 使用 Ant Design Vue 的 message.error 弹出错误消息框
        message.error({
          content: errorMessage,
          duration: 5
        })
        
        // 禁用元素并添加视觉提示
        if (el instanceof HTMLButtonElement || el instanceof HTMLInputElement || el instanceof HTMLSelectElement) {
          el.disabled = true
          el.style.opacity = '0.5'
          el.style.cursor = 'not-allowed'
        }
        
        // 添加权限不足的提示标签
        const tooltip = document.createElement('div')
        tooltip.style.cssText = `
          position: absolute;
          top: -25px;
          left: 0;
          background: #ff4d4f;
          color: white;
          padding: 2px 6px;
          border-radius: 3px;
          font-size: 11px;
          white-space: nowrap;
          z-index: 9999;
          pointer-events: none;
        `
        tooltip.textContent = '权限不足'
        
        // 设置元素为相对定位以便显示提示
        if (getComputedStyle(el).position === 'static') {
          el.style.position = 'relative'
        }
        
        // 添加提示元素
        el.appendChild(tooltip)
        
        // 鼠标悬停时显示详细权限信息
        el.addEventListener('mouseenter', () => {
          tooltip.textContent = `缺少权限：${missingPermissions.join(', ')}`
          tooltip.style.display = 'block'
        })
        
        el.addEventListener('mouseleave', () => {
          tooltip.style.display = 'none'
        })
        
        // 点击时再次弹出权限错误消息框
        el.addEventListener('click', (e) => {
          e.preventDefault()
          e.stopPropagation()
          message.error({
            content: `权限不足！缺少以下权限：${missingPermissions.join(', ')}`,
            duration: 5
          })
        })
      }
    } else {
      throw new Error('请设置操作权限标签值')
    }
  }
}

/**
 * 角色验证指令
 * v-hasRole="['admin','editor']"
 */
export const hasRole: Directive = {
  mounted(el: HTMLElement, binding) {
    const { value } = binding
    const roles = useUserStore().roles
    
    if (value && value instanceof Array && value.length > 0) {
      const hasRole = roles.some(role => {
        return value.includes(role)
      })
      
      if (!hasRole) {
        // 立即弹出角色不足错误消息框
        const missingRoles = value.filter(role => !roles.includes(role))
        const errorMessage = `角色不足！缺少以下角色：${missingRoles.join(', ')}`
        
        // 使用 Ant Design Vue 的 message.error 弹出错误消息框
        message.error({
          content: errorMessage,
          duration: 5
        })
        
        // 禁用元素并添加视觉提示
        if (el instanceof HTMLButtonElement || el instanceof HTMLInputElement || el instanceof HTMLSelectElement) {
          el.disabled = true
          el.style.opacity = '0.5'
          el.style.cursor = 'not-allowed'
        }
        
        // 添加角色不足的提示标签
        const tooltip = document.createElement('div')
        tooltip.style.cssText = `
          position: absolute;
          top: -25px;
          left: 0;
          background: #ff4d4f;
          color: white;
          padding: 2px 6px;
          border-radius: 3px;
          font-size: 11px;
          white-space: nowrap;
          z-index: 9999;
          pointer-events: none;
        `
        tooltip.textContent = '角色不足'
        
        // 设置元素为相对定位以便显示提示
        if (getComputedStyle(el).position === 'static') {
          el.style.position = 'relative'
        }
        
        // 添加提示元素
        el.appendChild(tooltip)
        
        // 鼠标悬停时显示详细角色信息
        el.addEventListener('mouseenter', () => {
          tooltip.textContent = `缺少角色：${missingRoles.join(', ')}`
          tooltip.style.display = 'block'
        })
        
        el.addEventListener('mouseleave', () => {
          tooltip.style.display = 'none'
        })
        
        // 点击时再次弹出角色错误消息框
        el.addEventListener('click', (e) => {
          e.preventDefault()
          e.stopPropagation()
          message.error({
            content: `角色不足！缺少以下角色：${missingRoles.join(', ')}`,
            duration: 5
          })
        })
      }
    } else {
      throw new Error('请设置角色标签值')
    }
  }
}

// 注册所有指令
export function setupPermission(app: App) {
  app.directive('hasPermi', hasPermi)
  app.directive('hasRole', hasRole)
} 
