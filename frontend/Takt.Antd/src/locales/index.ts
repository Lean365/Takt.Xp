import { createI18n } from 'vue-i18n'
import { getListByModuleAsync } from '@/api/routine/i18n/translation'
import { watch } from 'vue'

// 支持的语言列表
const supportedLocales = [
  'en-US', 'zh-CN', 'ar-SA', 'es-ES', 'fr-FR', 
  'ja-JP', 'ko-KR', 'ru-RU', 'zh-TW'
] as const

type Locale = typeof supportedLocales[number]

// 定义消息类型
interface TranslationMessages {
  [key: string]: any
}

interface Messages {
  [locale: string]: TranslationMessages
}

// 深度合并对象
function deepMerge(target: any, source: any): any {
  if (!source) return target
  if (!target) return source

  const result = { ...target }
  for (const key in source) {
    if (source[key] && typeof source[key] === 'object') {
      result[key] = deepMerge(result[key], source[key])
    } else {
      result[key] = source[key]
    }
  }
  return result
}

// 从文件路径中提取模块路径
function getModulePath(filePath: string): string {
  const path = filePath.replace('./', '').replace('.ts', '')
  return path.split('/').slice(0, -1).join('/')
}

// 从文件路径中提取语言代码
function getLocale(filePath: string): string {
  const locale = filePath.split('/').pop()?.replace('.ts', '') || ''
  // 强制使用连字符格式
  return locale.replace('_', '-')
}

// 将标准格式转换为文件名格式
function getLocaleFileName(locale: string): string {
  // 强制使用连字符格式
  return locale.replace('_', '-')
}

// 检查文件名格式
function validateLocaleFileName(filePath: string): boolean {
  const fileName = filePath.split('/').pop() || ''
  if (fileName.includes('_')) {
    console.error(`Invalid locale file name format: ${fileName}. Use hyphen (-) instead of underscore (_).`)
    return false
  }
  return true
}

// 获取所有可用的语言和模块
const availableLocales = new Set<string>()
const availableModules = new Set<string>()

// 使用 Vite 的 import.meta.glob 动态导入所有翻译文件
const modules = import.meta.glob([
  './accounting/**/*.ts',
  './audit/**/*.ts',
  './core/**/*.ts',
  './components/**/*.ts',
  './common/**/*.ts',
  './dashboard/**/*.ts',
  './error/**/*.ts',
  './generator/**/*.ts',
  './home/**/*.ts',
  './identity/**/*.ts',
  './logistics/**/*.ts',
  './signalr/**/*.ts',
  './routine/**/*.ts',
  './router/**/*.ts',
  './workflow/**/*.ts',
  './test/**/*.ts'
], { eager: true })

Object.keys(modules).forEach(path => {
  // 检查文件名格式
  if (!validateLocaleFileName(path)) {
    return
  }
  const locale = getLocale(path)
  const modulePath = getModulePath(path)
  // 只添加支持的语言
  if (supportedLocales.includes(locale as Locale)) {
    availableLocales.add(locale)
  }
  availableModules.add(modulePath)
})

// 将 Set 转换为数组并排序
const locales = Array.from(availableLocales).sort()
const modulePaths = Array.from(availableModules).sort()

// 获取初始语言设置
const getInitialLocale = () => {
  return localStorage.getItem('lang') || import.meta.env.VITE_LOCALE || 'zh-CN'
}

// 创建 i18n 实例
const i18n = createI18n({
  legacy: false,
  locale: getInitialLocale(),
  fallbackLocale: 'zh-CN',
  messages: {},
  silentTranslationWarn: true,
  missingWarn: false,
  fallbackWarn: false,
  silentFallbackWarn: true
})

// 处理翻译文件
const messages = locales.reduce<Messages>((acc, locale) => {
  acc[locale] = modulePaths.reduce<TranslationMessages>((moduleAcc, module) => {
    const path = `./${module}/${getLocaleFileName(locale)}.ts`
    if (modules[path]) {
      return deepMerge(moduleAcc, (modules[path] as any).default)
    }
    // 如果找不到翻译文件，使用后备语言
    const fallbackPath = `./${module}/${getLocaleFileName(i18n.global.fallbackLocale.value as string)}.ts`
    if (modules[fallbackPath]) {
      console.warn(`Missing translation for ${module}/${locale}, using fallback locale`)
      return deepMerge(moduleAcc, (modules[fallbackPath] as any).default)
    }
    console.warn(`Missing translation for ${module}/${locale} and no fallback available`)
    return moduleAcc
  }, {})
  return acc
}, {})

// 更新 i18n 实例的消息 - 为所有语言设置消息
locales.forEach(locale => {
  if (messages[locale]) {
    i18n.global.setLocaleMessage(locale, messages[locale])
  } else {
    console.warn(`No messages found for locale: ${locale}`)
  }
})

// 动态加载翻译数据的函数（参照示例实现）
const loadLocale = async (lang?: string) => {
  try {
    // 优先使用传入的语言参数，否则使用当前 i18n 语言
    const currentLang = lang || i18n.global.locale.value || 'zh-CN'
    console.log('Loading dynamic translations for language:', currentLang)
    const response = await getListByModuleAsync(currentLang, 0)
    const { data } = response
    
    if (data && data.length > 0) {
      // 将翻译数据转换为嵌套对象结构
      const translationMap = new Map<string, string>()
      data.forEach((translation: any) => {
        if (translation.transKey && translation.transValue) {
          translationMap.set(translation.transKey, translation.transValue)
        }
      })
      
      // 将 Map 转换为嵌套对象
      const dynamicMessages: any = {}
      translationMap.forEach((value, key) => {
        const keys = key.split('.')
        let current = dynamicMessages
        for (let i = 0; i < keys.length - 1; i++) {
          if (!current[keys[i]]) {
            current[keys[i]] = {}
          }
          current = current[keys[i]]
        }
        current[keys[keys.length - 1]] = value
      })
      
      // 合并到当前语言的翻译中（动态翻译会覆盖静态翻译）
      i18n.global.mergeLocaleMessage(currentLang, dynamicMessages)
      console.log(`Dynamic translations loaded for ${currentLang}`)
    }
  } catch (error) {
    console.error('Failed to load dynamic translations:', error)
  }
}

// 监听语言变化，重新加载翻译
watch(
  () => i18n.global.locale.value,
  (newLocale, oldLocale) => {
    if (newLocale !== oldLocale && newLocale) {
      console.log('Language changed from', oldLocale, 'to', newLocale)
      loadLocale(newLocale)
    }
  },
  { immediate: false }
)

// 初始化时加载动态翻译
loadLocale()

// 导出重新加载翻译的函数
export const reloadTranslations = loadLocale

// 导出切换语言的函数
export const setLocale = async (locale: string) => {
  // 更新 localStorage
  localStorage.setItem('lang', locale)
  // 更新 i18n 语言（这会触发 watch 监听器）
  i18n.global.locale.value = locale
}

export default i18n
