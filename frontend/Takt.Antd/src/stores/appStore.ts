import { defineStore } from 'pinia'
import { ref } from 'vue'
import i18n from '@/locales'
import { nextTick } from 'vue'

// 支持的语言列表
export const SUPPORTED_LOCALES = [
  'zh-CN',  // 简体中文
  'zh-TW',  // 繁体中文
  'en-US',  // 英语
  'ja-JP',  // 日语
  'ko-KR',  // 韩语
  'fr-FR',  // 法语
  'es-ES',  // 西班牙语
  'ru-RU',  // 俄语
  'ar-SA'   // 阿拉伯语
] as const

export type SupportedLocale = typeof SUPPORTED_LOCALES[number]

// 默认语言
export const DEFAULT_LOCALE: SupportedLocale = 'zh-CN'

export const useAppStore = defineStore('app', () => {
  // 状态
  const language = ref<SupportedLocale>(localStorage.getItem('language') as SupportedLocale || DEFAULT_LOCALE)

  // 初始化函数
  const initialize = async () => {
    const savedLanguage = localStorage.getItem('language')
    if (savedLanguage && SUPPORTED_LOCALES.includes(savedLanguage as SupportedLocale)) {
      await setLocale(savedLanguage as SupportedLocale)
    } else {
      // 如果没有保存的语言或语言无效，使用默认语言
      await setLocale(DEFAULT_LOCALE)
    }
  }

  // 设置语言
  const setLocale = async (locale: SupportedLocale) => {
    try {
      // 验证语言代码
      if (!SUPPORTED_LOCALES.includes(locale)) {
        throw new Error(`不支持的语言: ${locale}`)
      }

      // 更新 store 中的语言
      language.value = locale
      localStorage.setItem('language', locale)
      
      // 更新 i18n 语言
      i18n.global.locale.value = locale
      
      // 等待下一个 tick，确保语言切换生效
      await nextTick()
      
      // 更新文档方向（对于阿拉伯语等从右到左的语言）
      document.dir = locale === 'ar-SA' ? 'rtl' : 'ltr'
      document.documentElement.setAttribute('dir', document.dir)
      document.documentElement.setAttribute('lang', locale)
      
      // 触发自定义事件，通知其他组件语言已更改
      window.dispatchEvent(new Event('languagechange'))

      return true
    } catch (error) {
      console.error('Failed to change language:', error)
      throw error
    }
  }

  // 获取当前语言
  const getCurrentLocale = (): SupportedLocale => {
    return language.value
  }

  // 检查是否支持某个语言
  const isLocaleSupported = (locale: string): boolean => {
    return SUPPORTED_LOCALES.includes(locale as SupportedLocale)
  }

  return {
    // 状态
    language,
    // 方法
    initialize,
    setLocale,
    getCurrentLocale,
    isLocaleSupported,
    // 常量
    SUPPORTED_LOCALES
  }
})
