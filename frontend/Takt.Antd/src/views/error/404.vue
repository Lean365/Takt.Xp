//===================================================================
// 项目名 : Lean.Takt
// 文件名 : 404.vue
// 创建者 : Claude
// 创建时间: 2024-02-19
// 描述   : 404 页面未找到
//===================================================================

<template>
  <div class="error-page">
    <div class="error-content">
      <a-result status="404">
        <template #icon>
          <ExclamationCircleOutlined style="font-size: 120px; color: #ff4d4f;" />
        </template>
        <template #title>
          <h2>{{ t('error.404.title') }}</h2>
        </template>
        <template #subTitle>
          <p>{{ t('error.404.description') }}</p>
        </template>
        <template #extra>
          <a-button type="primary" @click="goHome">
            {{ t('error.actions.home') }}
          </a-button>
        </template>
      </a-result>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { ExclamationCircleOutlined } from '@ant-design/icons-vue'

const { t } = useI18n()
const router = useRouter()

const goHome = () => {
  router.push('/')
}
</script>

<style lang="less" scoped>
.error-page {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100%;
  padding: 24px;

  .error-content {
    max-width: 600px;
    padding: 32px;
    text-align: center;

    h2 {
      font-size: 24px;
      margin: 8px 0;
    }

    p {
      font-size: 16px;
      margin: 8px 0 16px;
    }
  }
}
</style> 
