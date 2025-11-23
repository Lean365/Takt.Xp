<template>
  <div class="error-page">
    <div class="error-content">
      <a-result status="403">
        <template #icon>
          <ErrorAnimation type="401" />
        </template>
        <template #title>
          <h2>{{ t('error.401.title') }}</h2>
        </template>
        <template #subTitle>
          <p>{{ t('error.401.description') }}</p>
        </template>
        <template #extra>
          <a-space>
            <a-button type="primary" @click="goToLogin">
              {{ t('error.actions.login') }}
            </a-button>
            <a-button @click="goBack">
              {{ t('error.actions.back') }}
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

const goToLogin = () => {
  router.push({
    path: '/login',
    query: { redirect: router.currentRoute.value.fullPath }
  })
}

const goBack = () => {
  router.back()
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