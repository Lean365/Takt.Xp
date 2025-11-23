<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: SettingsDetail.vue
创建日期: 2024-03-20
描述: 通用设置详情组件
=================================================================== 
-->

<template>
  <Takt-modal
    :open="open"
    :title="t('core.settings.title')"
    :width="800"
    @update:open="handleOpenChange"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item :label="t('core.settings.fields.settingsName.label')" :span="1">
        {{ form.settingsName }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.settingsKey.label')" :span="1">
        {{ form.settingsKey }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.settingsValue.label')" :span="2">
        {{ form.settingsValue }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.settingsType.label')" :span="1">
        <Takt-dict-tag dict-type="sys_settings_type" :value="form.settingsType" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.isBuiltin.label')" :span="1">
        <Takt-dict-tag :dict-type="'sys_yes_no'" :value="form.isBuiltin" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.orderNum.label')" :span="1">
        {{ form.orderNum }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('core.settings.fields.status.label')" :span="1">
        <Takt-dict-tag :dict-type="'sys_normal_disable'" :value="form.status" />
      </a-descriptions-item>
      <a-descriptions-item :label="t('table.columns.remark')" :span="2">
        {{ form.remark }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('table.columns.createTime')" :span="1">
        {{ form.createTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('table.columns.updateTime')" :span="1">
        {{ form.updateTime }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('table.columns.createBy')" :span="1">
        {{ form.createBy }}
      </a-descriptions-item>
      <a-descriptions-item :label="t('table.columns.updateBy')" :span="1">
        {{ form.updateBy }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktGeneralSettings } from '@/types/routine/settings/settings'
import { getByIdAsync } from '@/api/routine/settings/settings'

const { t } = useI18n()

const props = defineProps<{
  open: boolean
  settingsId?: string
}>()

const emit = defineEmits<{
  (e: 'update:open', open: boolean): void
}>()

// 表单数据
const form = reactive<TaktGeneralSettings>({
  settingsId: '',
  settingsName: '',
  settingsKey: '',
  settingsValue: '',
  settingsType: 1,
  isBuiltin: 0,
  orderNum: 0,
  remark: '',
  status: 0,
  createTime: '',
  updateTime: '',
  createBy: '',
  updateBy: '',
  isDeleted: 0,
  isEncrypted: 0
})

// 获取设置信息
const getInfo = async (settingsId: string) => {
  try {
    const res = await getByIdAsync(settingsId)
    Object.assign(form, res.data)
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 处理对话框显示状态变化
const handleOpenChange = (val: boolean) => {
  emit('update:open', val)
}

// 监听设置ID变化
watch(
  () => props.settingsId,
  (val) => {
    if (val) {
      getInfo(val)
    }
  }
)
</script>

<style lang="less" scoped>
:deep(.ant-descriptions) {
  padding: 24px;
}

:deep(.ant-descriptions-item-label) {
  width: 120px;
  text-align: right;
  background-color: #fafafa;
}

:deep(.ant-descriptions-item-content) {
  padding: 12px 24px;
}
</style> 
