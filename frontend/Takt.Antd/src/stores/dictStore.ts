import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getDictDataByType } from '@/api/routine/dict/dictData'
import type { TaktApiResponse } from '@/types/common'

export interface DictOption {
  label: string
  value: number | string
  cssClass?: string
  listClass?: string
  extLabel?: string
  extValue?: number | string
  transKey?: string
  disabled?: boolean
}

export const useDictStore = defineStore('dict', () => {
  // 状态
  const dictCache = ref<Map<string, DictOption[]>>(new Map())
  const loadingDict = ref<Map<string, Promise<void>>>(new Map())
  const loading = ref(false)

  // 获取器
  const getDictOptions = (type: string) => {
    return dictCache.value.get(type) || []
  }

  const getDictLabel = (type: string, value: number | string): string => {
    if (value === undefined || value === null || value === '') {
      return ''
    }
    const options = getDictOptions(type)
    const option = options.find(item => {
      const itemValue = String(item.value)
      const searchValue = String(value)
      return itemValue === searchValue
    })
    return option?.label || String(value)
  }

  const getDictTransKey = (type: string, value: number | string): string => {
    const options = getDictOptions(type)
    const option = options.find(item => String(item.value) === String(value))
    return option?.transKey || ''
  }

  const getDictClass = (type: string, value: number | string): string => {
    const options = getDictOptions(type)
    const option = options.find(item => String(item.value) === String(value))
    
    // 将数字映射为样式类名，根据 dict-tag.less 中的所有样式类型
    const numberToClass: Record<number | string, string> = {
      // 基础样式
      0: 'default',
      1: 'primary',
      2: 'success',
      3: 'info',
      4: 'warning',
      5: 'error',
      6: 'disabled',

      // 流程状态
      10: 'process-draft',
      11: 'process-pending',
      12: 'process-running',
      13: 'process-completed',
      14: 'process-rejected',
      15: 'process-canceled',
      16: 'process-suspended',
      17: 'process-terminated',
      18: 'process-expired',
      19: 'process-archived',

      // 邮件状态
      20: 'mail-unread',
      21: 'mail-read',
      22: 'mail-replied',
      23: 'mail-forwarded',
      24: 'mail-starred',
      25: 'mail-spam',
      26: 'mail-deleted',
      27: 'mail-draft',
      28: 'mail-sent',
      29: 'mail-failed',

      // 通知状态
      30: 'notify-unread',
      31: 'notify-read',
      32: 'notify-urgent',
      33: 'notify-important',
      34: 'notify-normal',
      35: 'notify-system',
      36: 'notify-business',
      37: 'notify-expired',
      38: 'notify-processing',
      39: 'notify-done',

      // 审批状态
      40: 'audit-pending',
      41: 'audit-approved',
      42: 'audit-rejected',
      43: 'audit-canceled',
      44: 'audit-transferred',
      45: 'audit-withdrawn',
      46: 'audit-expired',
      47: 'audit-suspended',
      48: 'audit-terminated',
      49: 'audit-archived',

      // 任务状态
      50: 'task-pending',
      51: 'task-processing',
      52: 'task-completed',
      53: 'task-failed',
      54: 'task-canceled',
      55: 'task-suspended',
      56: 'task-expired',
      57: 'task-archived',
      58: 'task-delegated',
      59: 'task-transferred'
    }

    if (!option?.cssClass) return ''
    
    // 如果 cssClass 是数字，使用映射表
    const cssClass = String(option.cssClass)
    if (/^\d+$/.test(cssClass)) {
      return numberToClass[cssClass] || cssClass
    }
    
    // 如果 cssClass 是字符串，直接返回
    return cssClass
  }

  const getDictExtLabel = (type: string, value: number | string): string => {
    const options = getDictOptions(type)
    const option = options.find(item => String(item.value) === String(value))
    return option?.extLabel || ''
  }

  const getDictExtValue = (type: string, value: number | string): number | string | undefined => {
    const options = getDictOptions(type)
    const option = options.find(item => String(item.value) === String(value))
    return option?.extValue
  }

  // 动作
  const loadDict = async (type: string) => {
    // 如果已经有缓存，直接返回
    if (dictCache.value.has(type)) {
      return
    }

    // 如果正在加载，返回现有的 Promise
    if (loadingDict.value.has(type)) {
      return loadingDict.value.get(type)
    }

    const loadPromise = (async () => {
      try {
        const response = await getDictDataByType(type)
        
        // 添加调试日志
        //console.log(`[DictStore] 字典[${type}]响应:`, response)
        const res = response as any
        //console.log(`[DictStore] response.code:`, res?.code)
        //console.log(`[DictStore] response.data:`, res?.data)
        
        if (res && res.code === 200 && res.data) {
          const dictDataList = res.data
          const options = dictDataList.map((item: any) => ({
            label: item.dictLabel,
            value: String(item.dictValue),  // 确保值始终是字符串
            cssClass: String(item.cssClass),  // 确保 cssClass 始终是字符串
            listClass: item.listClass,
            extLabel: item.extLabel,
            extValue: item.extValue,
            transKey: item.transKey,
            disabled: item.status === 1
          }))
          dictCache.value.set(type, options)
        } else {
          dictCache.value.set(type, [])
        }
      } catch (error) {
        console.error(`[字典Store] 字典[${type}]加载失败:`, error)
        dictCache.value.set(type, [])
        throw error
      }
    })()

    loadingDict.value.set(type, loadPromise)

    try {
      await Promise.race([
        loadPromise,
        new Promise((_, reject) => 
          setTimeout(() => reject(new Error(`[字典Store] 字典[${type}]加载超时`)), 30000)
        )
      ])
    } catch (error) {
      console.error(error)
      if (error instanceof Error && !error.message.includes('加载超时')) {
        dictCache.value.set(type, [])
      }
    } finally {
      loadingDict.value.delete(type)
    }
  }

  const loadDicts = async (types: string[]) => {
    if (!types.length) return

    loading.value = true
    try {
      await Promise.allSettled(types.map(type => loadDict(type)))
    } finally {
      loading.value = false
    }
  }

  const clearCache = () => {
    dictCache.value.clear()
    loadingDict.value.clear()
  }

  return {
    // 状态
    dictCache,
    loading,

    // 获取器
    getDictOptions,
    getDictLabel,
    getDictTransKey,
    getDictClass,
    getDictExtLabel,
    getDictExtValue,

    // 动作
    loadDict,
    loadDicts,
    clearCache
  }
}) 
