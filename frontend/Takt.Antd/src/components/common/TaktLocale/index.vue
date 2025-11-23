<template>
  <a-dropdown :trigger="['hover']" placement="bottom" class="locale-dropdown">
    <a-button type="text" :loading="loading">
      <template #icon>
        <translation-outlined v-if="!loading" />
      </template>
    </a-button>
    <template #overlay>
      <a-menu @click="({ key }) => handleLocaleChange(String(key))">
        <a-menu-item v-for="lang in languageList" :key="lang.dictValue">
          <template #icon>
            <check-outlined v-if="currentLocale === lang.dictValue" />
          </template>
          <span :class="{ 'current-lang': currentLocale === lang.dictValue }">
            {{ lang.extLabel }} {{ lang.dictLabel }}
          </span>
        </a-menu-item>
        <a-menu-divider v-if="languageList.length === 0" />
        <a-menu-item v-if="languageList.length === 0" disabled>
          {{ loading ? t('common.loading') : t('common.noData') }}
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { ref, onMounted, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import type { MenuProps } from 'ant-design-vue'
import { TranslationOutlined, CheckOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'
import { useAppStore, type SupportedLocale } from '@/stores/appStore'
import { useLanguageStore } from '@/stores/languageStore'
import type { TaktSelectOption } from '@/types/common'

const { t } = useI18n()
const appStore = useAppStore()
const languageStore = useLanguageStore()
const currentLocale = ref(appStore.language)
const loading = ref(false)
const languageList = ref<TaktSelectOption[]>([])

// 处理语言切换
const handleLocaleChange = async (langCode: string) => {
  if (loading.value) return
  
  loading.value = true
  try {
    // 将后端的语言代码映射到app store支持的语言代码
    const mappedLangCode = mapLanguageCode(langCode)
    if (mappedLangCode) {
      await appStore.setLocale(mappedLangCode)
      message.success(t('common.message.operationSuccess'))
    } else {
      message.error('不支持的语言代码')
    }
  } catch (error) {
    message.error(t('common.message.operationFailure'))
  } finally {
    loading.value = false
  }
}

// 语言代码映射函数
const mapLanguageCode = (backendLangCode: string): SupportedLocale | null => {
  const mapping: Record<string, SupportedLocale> = {
    'zh': 'zh-CN',
    'zh-CN': 'zh-CN',
    'zh-TW': 'zh-TW',
    'en': 'en-US',
    'en-US': 'en-US',
    'ja': 'ja-JP',
    'ja-JP': 'ja-JP',
    'ko': 'ko-KR',
    'ko-KR': 'ko-KR',
    'fr': 'fr-FR',
    'fr-FR': 'fr-FR',
    'es': 'es-ES',
    'es-ES': 'es-ES',
    'ru': 'ru-RU',
    'ru-RU': 'ru-RU',
    'ar': 'ar-SA',
    'ar-SA': 'ar-SA'
  }
  
  return mapping[backendLangCode] || null
}

// 监听语言变化
watch(() => appStore.language, (newLocale) => {
  currentLocale.value = newLocale
})

// 监听语言列表变化
watch(() => languageStore.languageList, (newList) => {
  //console.log('[TaktLocale] 语言列表变化:', newList)
  // TaktSelectOption 类型没有 status 字段，直接使用所有语言
  languageList.value = newList
})

// 组件挂载时初始化语言列表
onMounted(async () => {
  await languageStore.initializeStore()
  // 初始化后直接使用所有语言
  if (languageStore.languageList.length > 0) {
    languageList.value = languageStore.languageList
  }
})
</script>

<style lang="less" scoped>
.locale-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-btn) {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 32px;
  width: 32px;
  padding: 0;
}

:deep(.anticon) {
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
  line-height: 1;
}

.current-lang {
  font-weight: 500;
  color: #1890ff;
}
</style> 
