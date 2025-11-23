<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :footer="null"
    :maskClosable="false"
    @cancel="handleCancel"
  >
    <div style="text-align:center; margin: 16px 0;">
      <a-button-group>
        <a-button
          :type="selectedType === 0 ? 'primary' : 'default'"
          class="Takt-btn-post"
          @click="onSelect(0)"
        >{{ t('identity.menu.types.directory') }}</a-button>
        <a-button
          :type="selectedType === 1 ? 'primary' : 'default'"
          @click="onSelect(1)"
        >{{ t('identity.menu.types.menu') }}</a-button>
        <a-button
          :type="selectedType === 2 ? 'primary' : 'default'"
          @click="onSelect(2)"
        >{{ t('identity.menu.types.button') }}</a-button>
      </a-button-group>
    </div>
  </Takt-modal>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'

const props = defineProps<{
  visible: boolean
  title?: string
}>()
const emit = defineEmits(['update:visible', 'ok', 'cancel'])
const { t } = useI18n()

const selectedType = ref<number>(1)

watch(() => props.visible, (val) => {
  if (val) selectedType.value = 1 // 默认选中第一个
})

function onSelect(type: number) {
  selectedType.value = type
  emit('ok', type)
  emit('update:visible', false)
}
function handleCancel() {
  emit('cancel')
  emit('update:visible', false)
}
</script> 
