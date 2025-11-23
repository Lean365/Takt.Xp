<template>
  <div>
    <a-input
      v-model:value="search"
      :placeholder="'搜索图标'"
      style="margin-bottom: 8px;"
      allow-clear
    />
    <div class="icon-list">
      <div
        v-for="icon in filteredIcons"
        :key="icon.name"
        class="icon-item"
        :class="{ selected: icon.name === modelValue }"
        @click="selectIcon(icon.name)"
      >
        <component :is="icon.component" style="font-size: 22px;" />
        <div class="icon-name">{{ icon.name }}</div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import * as AntdIcons from '@ant-design/icons-vue'

const props = defineProps<{ value?: string }>()
const emit = defineEmits(['update:value'])

const search = ref('')
const modelValue = computed({
  get: () => props.value,
  set: v => emit('update:value', v)
})

const iconList = Object.keys(AntdIcons)
  .filter(name => name.endsWith('Outlined'))
  .map(name => ({ name, component: (AntdIcons as any)[name] }))

const filteredIcons = computed(() => {
  if (!search.value) return iconList
  return iconList.filter(icon => icon.name.toLowerCase().includes(search.value.toLowerCase()))
})

function selectIcon(name: string) {
  emit('update:value', name)
}
</script>

<style scoped>
.icon-list {
  display: flex;
  flex-wrap: wrap;
  gap: 12px;
  max-height: 260px;
  overflow-y: auto;
}
.icon-item {
  width: 70px;
  text-align: center;
  cursor: pointer;
  border: 1px solid #f0f0f0;
  border-radius: 6px;
  padding: 8px 0 2px 0;
  transition: border-color 0.2s;
}
.icon-item.selected {
  border-color: #1890ff;
  background: #e6f7ff;
}
.icon-name {
  font-size: 12px;
  margin-top: 2px;
  color: #888;
  word-break: break-all;
}
</style> 