<template>
  <a-tree-select
    :value="modelValue"
    :tree-data="filteredTreeData"
    :field-names="{
      children: 'children',
      label: 'menuName',
      value: 'menuId'
    }"
    :placeholder="t('identity.menu.fields.parentId.placeholder')"
    :dropdown-style="{ maxHeight: '400px', overflow: 'auto' }"
    allow-clear
    tree-default-expand-all
    @change="handleChange"
  />
</template>

<script setup lang="ts">
import { ref, onMounted, watch, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TaktMenu } from '@/types/identity/menu'
import type { TaktApiResponse } from '@/types/common'
import { getMenuTree } from '@/api/identity/menu'

const props = defineProps<{
  modelValue?: number,
  menuType?: number // 新增菜单类型 prop
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value?: number): void
  (e: 'selectMenu', menu?: any): void
  (e: 'load', data: TaktMenu[]): void
}>()

const { t } = useI18n()

// 树形数据
const treeData = ref<TaktMenu[]>([])

// 过滤后的树形数据
const filteredTreeData = computed(() => {
  if (props.menuType === 0) {
    // 只显示 parentId=0 的菜单，加上根菜单，且 children 置空
    return [
      {
        menuId: 0,
        menuName: t('identity.menu.fields.parentId.root') || '顶级菜单',
        children: []
      },
      ...treeData.value
        .filter(menu => menu.parentId === '0' || menu.parentId === null)
        .map(menu => ({ ...menu, children: [] }))
    ]
  }
  if (props.menuType === 1) {
    // 只显示 parentId=0 的菜单，不加虚拟根节点
    return treeData.value
      .filter(menu => menu.parentId === '0' || menu.parentId === null)
      .map(menu => ({ ...menu, children: [] }))
  }
  if (props.menuType === 2) {
    // 显示所有 menuType=1 的菜单（不分层级，平铺），不包含按钮
    function flattenMenus(list: TaktMenu[]): TaktMenu[] {
      let result: TaktMenu[] = []
      list.forEach((item: TaktMenu) => {
        if (item.menuType === 1) {
          result.push({ ...item, children: [] })
        }
        if (item.children && item.children.length > 0) {
          result = result.concat(flattenMenus(item.children))
        }
      })
      return result
    }
    return flattenMenus(treeData.value)
  }
  // 其它类型暂时返回全部
  return treeData.value
})

// 加载菜单树数据
const loadTreeData = async () => {
  try {
    const res = await getMenuTree()
    if (res.data) {
      treeData.value = res.data
      emit('load', res.data) // 传递原始菜单数组
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 初始化
onMounted(() => {
  loadTreeData()
})

function handleChange(value: number | undefined) {
  emit('update:modelValue', value)
  // 查找选中的菜单对象
  const findMenu = (list: any[], id: number | undefined): any | undefined => {
    for (const item of list) {
      if (item.menuId === id) return item
      if (item.children && item.children.length > 0) {
        const found = findMenu(item.children, id)
        if (found) return found
      }
    }
    return undefined
  }
  const selectedMenu = findMenu(treeData.value, value)
  emit('selectMenu', selectedMenu)
}
</script>
