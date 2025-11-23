<template>
  <div class="error-page">
    <div class="error-content">
      <a-result status="500">
        <template #icon>
          <ErrorAnimation type="500" />
        </template>
        <template #title>
          <h2>{{ t('error.500.title') }}</h2>
        </template>
        <template #subTitle>
          <p>{{ t('error.500.description') }}</p>
        </template>
        <template #extra>
          <a-space>
            <a-button type="primary" @click="retry">
              {{ t('error.actions.retry') }}
            </a-button>
            <a-button @click="goBack">
              {{ t('error.actions.back') }}
            </a-button>
            <a-button @click="goHome">
              {{ t('error.actions.home') }}
            </a-button>
          </a-space>
        </template>
      </a-result>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'


const { t } = useI18n()
const router = useRouter()

const retry = () => {
  window.location.reload()
}

const goBack = () => {
  router.back()
}

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