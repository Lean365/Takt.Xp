import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getLanguageOptions } from '@/api/routine/i18n/language'
import type { TaktSelectOption, TaktApiResponse } from '@/types/common'

export const useLanguageStore = defineStore('language', () => {
  // 状态
  const languageList = ref<TaktSelectOption[]>([])
  const loading = ref(false)
  const initialized = ref(false)

  // 初始化store
  const initializeStore = async () => {
    if (initialized.value) return
    
    try {
      loading.value = true
      // console.log('[LanguageStore] 开始获取语言数据...')
      const response = await getLanguageOptions()
      
      // console.log('[LanguageStore] 原始响应:', response)
      // console.log('[LanguageStore] response.data:', response.data)
      // console.log('[LanguageStore] response.data.code:', response?.code)
      // console.log('[LanguageStore] response.data.data:', response?.data)
      
      // 现在可以直接使用 response.code, response.data, response.msg，类型安全
      if (response && response.code === 200 && response.data) {
        languageList.value = response.data
        initialized.value = true
        // console.log('[LanguageStore] 语言数据加载成功:', response.data)
      } else {
        console.warn('[LanguageStore] 语言数据格式异常:', response)
      }
    } catch (error) {
      console.error('Failed to initialize language store:', error)
    } finally {
      loading.value = false
    }
  }

  // 获取语言列表
  const getLanguageList = () => {
    return languageList.value
  }

  // 根据语言代码获取语言信息
  const getLanguageByCode = (langCode: string) => {
    return languageList.value.find(lang => lang.dictValue === langCode)
  }

  // 获取启用的语言列表（TaktSelectOption 没有 status 字段，所以返回所有）
  const getEnabledLanguages = () => {
    return languageList.value
  }

  return {
    // 状态
    languageList,
    loading,
    initialized,
    // 方法
    initializeStore,
    getLanguageList,
    getLanguageByCode,
    getEnabledLanguages
  }
})

