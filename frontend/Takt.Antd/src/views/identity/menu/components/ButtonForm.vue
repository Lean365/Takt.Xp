<template>
  <a-form
    ref="formRef"
    :model="form"
    :rules="rules"
    :label-col="{ span: 6 }"
    :wrapper-col="{ span: 16 }"
  >
    <a-form-item :label="t('identity.menu.fields.parentId.label')" name="parentId">
      <menu-tree-select
        v-model:value="form.parentId"
        :menuType="2"
        @selectMenu="onSelectMenu"
        @load="handleMenuTreeLoad"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.menuButtonName.label')" name="menuName">
      <a-input
        v-model:value="form.menuName"
        :placeholder="t('identity.menu.fields.menuButtonName.placeholder')"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.transKey.label')">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input value="button" disabled style="width:70px" />
        </a-form-item-rest>
        <span style="width:36px;display:inline-block;text-align:center;">.</span>
        <a-form-item name="transKeyModule" style="display:inline-block;margin-bottom:0;">
          <a-input v-model:value="transKeyModule" style="width:100px" :placeholder="t('identity.menu.fields.transKey.modulePlaceholder')" />
        </a-form-item>
      </a-input-group>
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.orderNum.label')" name="orderNum">
      <a-input-number
        v-model:value="form.orderNum"
        :min="0"
        style="width: 100%"
        :placeholder="t('identity.menu.fields.orderNum.placeholder')"
        :disabled="!isEdit"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.perms.label')" name="perms">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input
            :value="parentMenuPath"
            style="width: 120px"
            disabled
          />
        </a-form-item-rest>
        <span style="width: 36px; text-align: center; display: inline-block;">:</span>
        <a-form-item-rest>
          <a-input
            :value="menuPath"
            style="width: 120px"
            disabled
          />
        </a-form-item-rest>
        <span style="width: 36px; text-align: center; display: inline-block;">:</span>
        <a-form-item-rest>
          <a-input
            v-model:value="permsAction"
            style="width: 80px"
            :placeholder="t('identity.menu.fields.perms.actionPlaceholder')"
          />
        </a-form-item-rest>
      </a-input-group>
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.visible.label')" name="visible">
      <Takt-select
        v-model:value="form.visible"
        dict-type="sys_is_visible"
        type="radio"
        :show-all="false"
        :placeholder="t('identity.menu.fields.visible.placeholder')"
        allow-clear
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.menuStatus.label')" name="menuStatus">
      <Takt-select
        v-model:value="form.menuStatus"
        dict-type="sys_normal_disable"
        type="radio"
        :show-all="false"
        :placeholder="t('identity.menu.fields.menuStatus.placeholder')"
        allow-clear
      />
    </a-form-item>
  </a-form>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, RuleObject } from 'ant-design-vue/es/form'
import type { TaktMenu, TaktMenuCreate } from '@/types/identity/menu'
import { getByIdAsync, createMenu, updateMenu } from '@/api/identity/menu'
import MenuTreeSelect from '../components/MenuTreeSelect.vue'

const props = defineProps<{
  visible: boolean
  title: string
  menuId?: string
  showModal?: boolean
}>()

const { t } = useI18n()

// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<TaktMenuCreate>({
  menuName: '',
  parentId: '0',
  orderNum: 0,
  path: '',
  component: '',
  queryParams: '',
  isExternal: 0,
  isCache: 0,
  menuType: 2,
  visible: 1,
  status: 0,
  perms: '',
  icon: ''
})

const transKeyModule = ref('')
const permsAction = ref('')

// 响应式表单校验规则
const rules = {
  menuName: [
    { required: true, message: t('identity.menu.fields.menuButtonName.validation.required') },
    { min: 2, max: 50, message: t('identity.menu.fields.menuButtonName.validation.length') },
    { 
      validator: (_rule: any, value: string) => {
        if (!value) return Promise.resolve();
        // 英文：大写字母开头且全为英文（不允许重音符号）
        if (/^[A-Z][a-zA-Z]*$/.test(value)) return Promise.resolve();
        // 法语/西语：首字母大写字母或大写重音，后面允许小写/大写重音，不能有英文以外的字符
        if (/^[A-ZÀ-Ý][a-zà-ÿÀ-ÿ]*$/.test(value) && !/[a-zA-Z]/.test(value.replace(/[A-ZÀ-Ý][a-zà-ÿÀ-ÿ]*/,''))) return Promise.resolve();
        // 中文
        if (/^[\u4e00-\u9fa5]+$/.test(value)) return Promise.resolve();
        // 日文（平假名/片假名）
        if (/^[\u3040-\u309F\u30A0-\u30FF]+$/.test(value)) return Promise.resolve();
        // 韩文
        if (/^[\uAC00-\uD7AF]+$/.test(value)) return Promise.resolve();
        // 阿拉伯语
        if (/^[\u0600-\u06FF]+$/.test(value)) return Promise.resolve();
        // 俄语
        if (/^[\u0400-\u04FF]+$/.test(value)) return Promise.resolve();
        return Promise.reject(t('identity.menu.fields.menuButtonName.validation.format'));
      }
    }
  ],
  parentId: [
    { required: true, message: t('identity.menu.fields.parentId.validation.required') }
  ],
  orderNum: [
    { required: true, message: t('identity.menu.fields.orderNum.validation.required') }
  ],
  perms: [
    {
      pattern: /^[a-z][a-z0-9]{0,9}:[a-z][a-z0-9]{0,9}:[a-z](?:[a-z0-9_]{0,12}[a-z0-9])?$/,
      message: t('identity.menu.fields.perms.validation.format')
    }
  ],
  status: []
}

// 加载状态
const loading = ref(false)

// 新增：完整菜单数据（含按钮），由 MenuTreeSelect.vue 传递
const allMenuList = ref<any[]>([])

// MenuTreeSelect 加载完成后回调，接收完整菜单树
function handleMenuTreeLoad(data: any[]) {
  allMenuList.value = data
  updateOrderNum()
}

// 获取菜单信息
const getInfo = async (menuId: string) => {
  try {
    loading.value = true
    const data = await getByIdAsync(menuId)
    Object.assign(form, data)
    if (form.transKey && form.transKey.startsWith('button')) {
      transKeyModule.value = form.transKey.slice(6)
    }
    // 权限回显处理
    if (form.perms) {
      const parts = form.perms.split(':')
      if (parts.length === 3) {
        permsAction.value = parts[2]
      }
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 处理弹窗显示状态变化
const handleVisibleChange = (visible: boolean) => {
  if (!visible) {
    formRef.value?.resetFields()
  }
}

// transKey 计算属性
const transKey = computed(() => {
  return `button${transKeyModule.value}`
})

// 初始化
onMounted(() => {
  if (props.menuId) {
    getInfo(props.menuId)
  }
})

const parentMenuPath = computed(() => {
  function findMenuById(list: any[], menuId: string | undefined): any | undefined {
    for (const item of list) {
      if (item.menuId === menuId) return item
      if (item.children && item.children.length > 0) {
        const found = findMenuById(item.children, menuId)
        if (found) return found
      }
    }
    return undefined
  }
  const parentMenu = findMenuById(allMenuList.value, form.parentId)
  if (!parentMenu) return ''
  if (parentMenu.parentId === undefined || parentMenu.parentId === 0) return ''
  const grandParentMenu = findMenuById(allMenuList.value, parentMenu.parentId)
  return grandParentMenu?.path || ''
})

const menuPath = computed(() => {
  function findMenuById(list: any[], menuId: string | undefined): any | undefined {
    for (const item of list) {
      if (item.menuId === menuId) return item
      if (item.children && item.children.length > 0) {
        const found = findMenuById(item.children, menuId)
        if (found) return found
      }
    }
    return undefined
  }
  const parentMenu = findMenuById(allMenuList.value, form.parentId)
  return parentMenu?.path || ''
})

const perms = computed(() => {
  return `${parentMenuPath.value}:${menuPath.value}:${permsAction.value}`
})

function updateOrderNum() {
  const flatList: any[] = []
  function flatten(list: any[]) {
    list.forEach(item => {
      flatList.push(item)
      if (item.children && item.children.length > 0) {
        flatten(item.children)
      }
    })
  }
  flatten(allMenuList.value)
  const buttons = flatList.filter((item: any) => item.parentId === form.parentId && item.menuType === 2)
  if (buttons.length > 0) {
    const maxOrder = Math.max(...buttons.map((item: any) => item.orderNum || 0))
    form.orderNum = maxOrder + 1
  } else {
    form.orderNum = 1
  }
}

const isEdit = computed(() => !!props.menuId)

function onSelectMenu(menu?: any) {
  if (menu && Array.isArray(menu.children) && menu.children.length > 0) {
    const buttons = menu.children.filter((item: any) => item.menuType === 2)
    if (buttons.length > 0) {
      const maxOrder = Math.max(...buttons.map((item: any) => item.orderNum ?? 0))
      form.orderNum = maxOrder + 1
    } else {
      form.orderNum = 1
    }
  } else {
    form.orderNum = 1
  }
}

defineExpose({
  async submitForm() {
    await formRef.value?.validate()
    let res
    const submitData = {
      ...form,
      transKey: transKey.value,
      perms: perms.value
    }
    if (props.menuId) {
      const { data } = await updateMenu({
        ...submitData,
        menuId: props.menuId
      })
      res = data
    } else {
      const { data } = await createMenu(submitData)
      res = data
    }
    if (typeof res === 'number' && res > 0) {
      // 创建成功，返回ID
      return res
    } else if (typeof res === 'boolean' && res) {
      // 更新成功
      return res
    } else {
      throw new Error('保存失败')
    }
    return res
  }
})
</script> 
