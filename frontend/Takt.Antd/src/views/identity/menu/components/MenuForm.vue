<template>
  <a-form
    ref="formRef"
    :model="form"
    :rules="rules"
    :label-col="{ span: 6 }"
    :wrapper-col="{ span: 16 }"
    @update:visible="handleVisibleChange"
  >
    <a-form-item :label="t('identity.menu.fields.parentId.label')" name="parentId">
      <menu-tree-select
        v-model:value="form.parentId"
        :menuType="1"
        @selectMenu="onSelectMenu"
        @load="handleMenuTreeLoad"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.menuName.label')" name="menuName">
      <a-input
        v-model:value="form.menuName"
        :placeholder="t('identity.menu.fields.menuName.placeholder')"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.transKey.label')">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input value="menu" disabled style="width:70px" />
        </a-form-item-rest>
        <span style="width:36px;display:inline-block;text-align:center;">.</span>
        <a-form-item-rest>
          <a-input :value="parentMenuPath" style="width:100px" disabled />
        </a-form-item-rest>
        <span style="width:36px;display:inline-block;text-align:center;">.</span>
        <a-form-item-rest>
          <a-input :value="form.path" style="width:100px" disabled />
        </a-form-item-rest>
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
    <a-form-item :label="t('identity.menu.fields.path.label')" name="path">
      <a-input
        v-model:value="form.path"
        :placeholder="t('identity.menu.fields.path.placeholder')"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.component.label')" name="component">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input :value="parentMenuPath" style="width: 108px" disabled />
        </a-form-item-rest>
        <span style="width: 36px; display: inline-block; text-align: center;">/</span>
        <a-form-item-rest>
          <a-input :value="form.path" style="width: 108px" disabled />
        </a-form-item-rest>
        <span style="width: 36px; display: inline-block; text-align: center;">/</span>
        <a-form-item-rest>
          <a-input value="index" style="width: 80px" disabled />
        </a-form-item-rest>
      </a-input-group>
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.queryParams.label')" name="queryParams">
      <a-input
        v-model:value="form.queryParams"
        :placeholder="t('identity.menu.fields.queryParams.placeholder')"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.isExternal.label')" name="isExternal">
      <Takt-select
        v-model:value="form.isExternal"
        dict-type="sys_IsExternal"
        type="radio"
        :show-all="false"
        :placeholder="t('identity.menu.fields.isExternal.placeholder')"
        allow-clear
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.isCache.label')" name="isCache">
      <Takt-select
        v-model:value="form.isCache"
        dict-type="sys_is_cache"
        type="radio"
        :show-all="false"
        :placeholder="t('identity.menu.fields.isCache.placeholder')"
        allow-clear
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.perms.label')" name="perms">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input :value="parentMenuPath" style="width: 120px" disabled />
        </a-form-item-rest>
        <span style="width: 36px; text-align: center; display: inline-block;">:</span>
        <a-form-item-rest>
          <a-input :value="form.path" style="width: 120px" disabled />
        </a-form-item-rest>
        <span style="width: 36px; text-align: center; display: inline-block;">:</span>
        <a-form-item-rest>
          <a-input value="list" style="width: 80px" disabled />
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
    <a-form-item :label="t('identity.menu.fields.icon.label')" name="icon">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input
            v-model:value="form.icon"
            :placeholder="t('identity.menu.fields.icon.placeholder')"
            readonly
            style="width: 70%"
            :prefix="form.icon ? TaktIconComponent(form.icon) : undefined"
          />
        </a-form-item-rest>
        <a-form-item-rest>
          <a-button @click="handleOpenIconModal" style="width: 30%">
            {{ t('identity.menu.fields.icon.select') }}
          </a-button>
        </a-form-item-rest>
      </a-input-group>
    </a-form-item>
  </a-form>
  <a-modal 
    v-model:open="iconModalVisible" 
    :title="t('identity.menu.fields.icon.select')" 
    width="600px" 
    :footer="false" 
    @cancel="iconModalVisible = false"
    :destroyOnClose="true"
  >
    <Takt-menu-icon 
      :value="iconSelectTemp" 
      @update:value="handleIconChange" 
    />
    <div style="text-align:right;margin-top:12px;">
      <a-button type="primary" @click="handleIconConfirm">{{ t('common.button.confirm') }}</a-button>
      <a-button style="margin-left:8px;" @click="iconModalVisible = false">{{ t('common.actions.cancel') }}</a-button>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, RuleObject } from 'ant-design-vue/es/form'
import type { TaktMenu, TaktMenuCreate } from '@/types/identity/menu'
import { getByIdAsync, createMenu, updateMenu } from '@/api/identity/menu'
import MenuTreeSelect from '../components/MenuTreeSelect.vue'
import { h } from 'vue'
import { useMenuStore } from '@/stores/menuStore'
import { useRouter } from 'vue-router'

const props = defineProps<{
  visible: boolean
  title: string
  menuId?: string
  showModal?: boolean
}>()

const emit = defineEmits(['update:visible'])

const { t } = useI18n()

const router = useRouter()
const menuStore = useMenuStore()

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
  menuType: 1,
  visible: 1,
  status: 0,
  perms: '',
  icon: ''
})

const transKeyModule = ref('')
const transKeyBiz = ref('')
const componentSuffix = ref('')

// 响应式表单校验规则
const rules = {
  menuName: [
    { required: true, message: t('identity.menu.fields.menuName.validation.required') },
    { min: 2, max: 50, message: t('identity.menu.fields.menuName.validation.length') },
    { 
      validator: async (_rule: any, value: string) => {
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
        return Promise.reject(t('identity.menu.fields.menuName.validation.format'));
      }
    }
  ],
  parentId: [
    { required: true, message: t('identity.menu.fields.parentId.validation.required') }
  ],
  orderNum: [
    { required: true, message: t('identity.menu.fields.orderNum.validation.required') }
  ],
  path: [
    { required: true, message: t('identity.menu.fields.path.validation.required') },
    { min: 2, max: 50, message: t('identity.menu.fields.path.validation.length') },
    { 
      pattern: /^[a-z]+$/,
      message: t('identity.menu.fields.path.validation.format')
    }
  ],
  component: [
    { required: true, message: t('identity.menu.fields.component.validation.required') },
    {
      validator: (_rule: any, value: string) => {
        if (!value) return Promise.reject(t('identity.menu.fields.component.validation.required'))
        if (value.length < 5 || value.length > 100) {
          return Promise.reject(t('identity.menu.fields.component.validation.length'))
        }
        if (!/^([a-z0-9]+\/)*[a-z0-9]+\/index$/.test(value)) {
          return Promise.reject(t('identity.menu.fields.component.validation.format'))
        }
        return Promise.resolve()
      }
    }
  ],
  queryParams: [],
  isExternal: [],
  isCache: [],
  perms: [
    {
      pattern: /^[a-z][a-z0-9]{0,9}:[a-z][a-z0-9]{0,9}:[a-z](?:[a-z0-9_]{0,12}[a-z0-9])?$/,
      message: t('identity.menu.fields.perms.validation.format')
    }
  ],
  visible: [],
  status: [],
  icon: [],
  transKey: [
    { required: true, message: t('identity.menu.fields.transKey.validation.required') },
    {
      validator: (_rule: any, _value: string) => {
        const val = transKey.value
        if (!val) return Promise.reject(t('identity.menu.fields.transKey.validation.required'))
        if (val.length < 5 || val.length > 100) {
          return Promise.reject(t('identity.menu.fields.transKey.validation.length'))
        }
        // 只允许 menu.父path.当前path 格式，且每节只允许小写字母
        if (!/^menu\.[a-z]+\.[a-z]+$/.test(val)) {
          return Promise.reject(t('identity.menu.fields.transKey.validation.format'))
        }
        return Promise.resolve()
      }
    }
  ]
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
    if (form.transKey && form.transKey.startsWith('menu.')) {
      const prefixLen = 5 + (form.path ? form.path.length + 1 : 0)
      transKeyModule.value = form.transKey.slice(prefixLen).split('.')[0]
      transKeyBiz.value = form.transKey.slice(prefixLen).split('.')[1]
    } else {
      transKeyModule.value = form.transKey?.split('.')[0] || ''
      transKeyBiz.value = form.transKey?.split('.')[1] || ''
    }
    if (form.icon) iconSelectTemp.value = form.icon
    // 组件路径回显处理
    if (form.component && parentMenuPath.value && form.component.startsWith(parentMenuPath.value + '/')) {
      componentSuffix.value = form.component.slice(parentMenuPath.value.length + 1)
    } else {
      componentSuffix.value = form.component || ''
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  form.parentId = '0'
  form.menuName = ''
  form.orderNum = 0
  form.path = ''
  form.component = ''
  form.queryParams = ''
  form.isExternal = 0
  form.isCache = 0
  form.visible = 1
  form.menuStatus = 0
  form.perms = ''
  form.icon = ''
  formRef.value?.resetFields()
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// transKey 计算属性
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
  return parentMenu?.path || ''
})

const transKey = computed(() => {
  return `menu.${parentMenuPath.value}.${form.path}`
})

// 图标选择相关
const iconModalVisible = ref(false)
const iconSelectTemp = ref('')

const handleIconChange = (val: string) => {
  iconSelectTemp.value = val
}

const handleIconConfirm = () => {
  form.icon = iconSelectTemp.value
  iconModalVisible.value = false
}

const handleOpenIconModal = () => {
  iconSelectTemp.value = form.icon || ''
  iconModalVisible.value = true
}

function TaktIconComponent(name: string) {
  try {
    const icons = require('@ant-design/icons-vue')
    return h(icons[name])
  } catch {
    return undefined
  }
}

const parentComponentPath = computed(() => {
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
  return parentMenu?.component || ''
})

const componentFullPath = computed(() => {
  return `${parentMenuPath.value}/${form.path}/index`
})

const selectedMenuPath = computed(() => {
  const menu = allMenuList.value.find(item => item.menuId === form.parentId)
  return menu?.path || ''
})

const perms = computed(() => {
  return `${parentMenuPath.value}:${form.path}:list`
})

function updateOrderNum() {
  const val = form.parentId
  let siblings: any[] = []
  if (val === '0') {
    siblings = allMenuList.value
  } else if (typeof val === 'string') {
    function findNode(list: any[], id: string): any | undefined {
      for (const item of list) {
        if (item.menuId === id) return item
        if (item.children && item.children.length > 0) {
          const found = findNode(item.children, id)
          if (found) return found
        }
      }
      return undefined
    }
    const parent = findNode(allMenuList.value, val)
    if (parent) {
      siblings = Array.isArray(parent.children) ? parent.children : []
    }
  }
  if (siblings && siblings.length > 0) {
    const maxOrder = Math.max(...siblings.map((item: any) => item.orderNum || 0))
    form.orderNum = maxOrder + 1
  } else {
    form.orderNum = 1
  }
}

const isEdit = computed(() => !!props.menuId)

onMounted(() => {
  if (props.menuId) {
    getInfo(props.menuId)
  }
})

function onSelectMenu(menu?: any) {
  if (form.parentId === '0') {
    const topLevelMenus = allMenuList.value.filter((item: any) => item.parentId === '0');
    if (topLevelMenus.length > 0) {
      const maxOrder = Math.max(...topLevelMenus.map((item: any) => item.orderNum ?? 0));
      form.orderNum = maxOrder + 1;
    } else {
      form.orderNum = 1;
    }
    return;
  }
  if (menu && Array.isArray(menu.children) && menu.children.length > 0) {
    const maxOrder = Math.max(...menu.children.map((item: any) => item.orderNum ?? 0));
    form.orderNum = maxOrder + 1;
  } else {
    form.orderNum = 1;
  }
}

// 监听componentFullPath变化，自动赋值给form.component
watch(componentFullPath, (val) => {
  form.component = val
})

defineExpose({
  async submitForm() {
    try {
      await formRef.value?.validate()
      let res
      const submitData = {
        ...form,
        transKey: transKey.value,
        component: form.component,
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
      // 更新菜单store
      await menuStore.reloadMenus(router)
      return res
    } catch (error: any) {
      if (error.message?.includes('MenuName=')) {
        message.error(t('identity.menu.validation.menuNameExists'))
      } else {
        message.error(error.message || t('common.failed'))
      }
      throw error
    }
  }
})
</script>

