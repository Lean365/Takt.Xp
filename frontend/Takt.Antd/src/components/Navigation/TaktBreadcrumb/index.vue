<template>
  <div v-if="!hideBreadcrumb" class="breadcrumb">
    <a-breadcrumb>
      <a-breadcrumb-item v-for="item in breadcrumbItems" :key="item.path">
        <router-link :to="item.path">
          <component :is="item.icon" v-if="item.icon && settingsStore.isShowTabIcon" :key="item.icon" />
          <span>{{ item.title }}</span>
        </router-link>
      </a-breadcrumb-item>
    </a-breadcrumb>
  </div>
</template>

<script lang="ts" setup>
import { computed, watch } from 'vue'
import { useRoute } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { useSettingsStore } from '@/stores/settingsStore'
import * as Icons from '@ant-design/icons-vue'

const { t } = useI18n()
const route = useRoute()
const settingsStore = useSettingsStore()

// 检查是否隐藏面包屑
const hideBreadcrumb = computed(() => {
  return route.meta?.hideBreadcrumb === true
})

// 监听showTabIcon配置变化
watch(
  () => settingsStore.isShowTabIcon,
  (newShowTabIcon) => {
    console.log('[TaktBreadcrumb] showTabIcon配置变化:', newShowTabIcon)
  }
)

// 计算面包屑项
const breadcrumbItems = computed(() => {
  // 获取当前路由的匹配信息
  const matched = route.matched.filter(item => item.meta && (item.meta.title || item.meta.transKey))
  
  console.log('[TaktBreadcrumb] 路由匹配信息:', {
    currentPath: route.path,
    routeMatched: route.matched,
    filteredMatched: matched.map(item => ({
      path: item.path,
      title: item.meta?.title,
      transKey: item.meta?.transKey
    }))
  })
  
  return matched.map(item => {
    // 获取标题
    const titleKey = item.meta?.transKey || item.meta?.title
    
    console.log('[TaktBreadcrumb] 处理面包屑项:', {
      path: item.path,
      metaTitle: item.meta?.title,
      metaTransKey: item.meta?.transKey,
      selectedTitleKey: titleKey
    })
    
    let title = ''
    
    if (titleKey) {
      try {
        const translatedTitle = t(titleKey as string)
        console.log('[TaktBreadcrumb] 翻译结果:', {
          originalKey: titleKey,
          translatedResult: translatedTitle,
          isTranslationSuccessful: translatedTitle !== titleKey
        })
        
        title = String(translatedTitle)
        // 如果翻译失败，使用原始值
        if (title === titleKey) {
          console.log('[TaktBreadcrumb] 翻译失败，使用原始key:', titleKey)
          title = titleKey
        }
      } catch (error) {
        console.error('[TaktBreadcrumb] 翻译异常:', error)
        title = String(titleKey)
      }
    } else {
      title = String(item.path)
    }
    
    console.log('[TaktBreadcrumb] 最终标题:', {
      path: item.path,
      finalTitle: title
    })
    
    return {
      path: item.path,
      title: title,
      icon: item.meta?.icon ? Icons[item.meta.icon as keyof typeof Icons] : undefined
    }
  })
})
</script>

<style lang="less" scoped>
.breadcrumb {
  padding: 8px 16px;
  background: var(--ant-color-bg-container);
  border-bottom: 1px solid var(--ant-border-color-split);

  :deep(.ant-breadcrumb) {
    color: var(--ant-color-text);
  }

  :deep(.ant-breadcrumb-link) {
    color: var(--ant-color-text);
  }

  :deep(.ant-breadcrumb-separator) {
    color: var(--ant-color-text-secondary);
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .breadcrumb {
    padding: 8px;
  }
}
</style>
