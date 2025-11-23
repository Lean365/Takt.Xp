<template>
  <a-modal
    :open="visible"
    :title="t('generator.table.preview.title')"
    width="1000px"
    :footer="null"
    @cancel="handleCancel"
  >
    <a-tabs v-model:activeKey="activeTab">
      <a-tab-pane :key="'java'" :tab="t('generator.table.preview.tab.java')">
        <a-tabs v-model:activeKey="activeJavaTab">
          <a-tab-pane :key="'domain'" :tab="t('generator.table.preview.tab.domain')">
            <entities-preview :code="previewData.domain" />
          </a-tab-pane>
          <a-tab-pane :key="'mapper'" :tab="t('generator.table.preview.tab.mapper')">
            <pre><code>{{ previewData.mapper }}</code></pre>
          </a-tab-pane>
          <a-tab-pane :key="'mapperXml'" :tab="t('generator.table.preview.tab.mapperXml')">
            <pre><code>{{ previewData.mapperXml }}</code></pre>
          </a-tab-pane>
          <a-tab-pane :key="'service'" :tab="t('generator.table.preview.tab.service')">
            <services-preview :code="previewData.service" />
          </a-tab-pane>
          <a-tab-pane :key="'serviceImpl'" :tab="t('generator.table.preview.tab.serviceImpl')">
            <pre><code>{{ previewData.serviceImpl }}</code></pre>
          </a-tab-pane>
          <a-tab-pane :key="'controller'" :tab="t('generator.table.preview.tab.controller')">
            <controllers-preview :code="previewData.controller" />
          </a-tab-pane>
        </a-tabs>
      </a-tab-pane>
      <a-tab-pane :key="'vue'" :tab="t('generator.table.preview.tab.vue')">
        <a-tabs v-model:activeKey="activeVueTab">
          <a-tab-pane :key="'api'" :tab="t('generator.table.preview.tab.api')">
            <pre><code>{{ previewData.api }}</code></pre>
          </a-tab-pane>
          <a-tab-pane :key="'index'" :tab="t('generator.table.preview.tab.index')">
            <vue-preview :code="previewData.index" />
          </a-tab-pane>
          <a-tab-pane :key="'form'" :tab="t('generator.table.preview.tab.form')">
            <vue-preview :code="previewData.form" />
          </a-tab-pane>
        </a-tabs>
      </a-tab-pane>
      <a-tab-pane :key="'sql'" :tab="t('generator.table.preview.tab.sql')">
        <pre><code>{{ previewData.sql }}</code></pre>
      </a-tab-pane>
    </a-tabs>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import EntitiesPreview from './preview/EntitiesPreview.vue'
import ServicesPreview from './preview/ServicesPreview.vue'
import ControllersPreview from './preview/ControllersPreview.vue'
import VuePreview from './preview/VuePreview.vue'

const { t } = useI18n()

defineProps<{
  visible: boolean
  loading: boolean
  previewData: Record<string, string>
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
}>()

// 当前激活的标签页
const activeTab = ref('java')
const activeJavaTab = ref('domain')
const activeVueTab = ref('api')

/** 取消按钮点击事件 */
const handleCancel = () => {
  emit('update:visible', false)
}
</script>

<style lang="less" scoped>
:deep(.ant-tabs-content) {
  height: 600px;
  overflow-y: auto;
}

:deep(.ant-tabs-tabpane) {
  padding: 16px 0;
}
</style> 