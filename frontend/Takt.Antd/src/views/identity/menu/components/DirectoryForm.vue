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
        :menuType="0"
        @selectMenu="onSelectMenu"
        @load="handleMenuTreeLoad"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.menuDirectoryName.label')" name="menuName">
      <a-input
        v-model:value="form.menuName"
        :placeholder="t('identity.menu.fields.menuDirectoryName.placeholder')"
      />
    </a-form-item>
    <a-form-item :label="t('identity.menu.fields.transKey.label')">
      <a-input-group compact>
        <a-form-item-rest>
          <a-input value="menu" disabled style="width:70px" />
        </a-form-item-rest>
        <span style="width:36px;display:inline-block;text-align:center;">.</span>
        <a-form-item-rest>
          <a-input
            v-if="form.parentId === '0'"
            v-model:value="form.path"
            style="width:100px"
            :placeholder="t('identity.menu.fields.transKey.modulePlaceholder')"
            disabled
          />
          <a-input
            v-else
            :value="parentPath"
            style="width:100px"
            disabled
          />
        </a-form-item-rest>
        <template v-if="form.parentId !== '0'">
          <span style="width:36px;display:inline-block;text-align:center;">.</span>
          <a-form-item-rest>
            <a-input
              v-model:value="form.path"
              style="width:100px"
              :placeholder="t('identity.menu.fields.transKey.modulePlaceholder')"
              disabled
            />
          </a-form-item-rest>
        </template>
        <span style="width:36px;display:inline-block;text-align:center;">.</span>
        <a-form-item-rest>
          <a-input value="_self" disabled style="width:60px" />
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
import { ref, reactive, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, RuleObject } from 'ant-design-vue/es/form'
import type { TaktMenu, TaktMenuCreate } from '@/types/identity/menu'
import { getByIdAsync, createMenu, updateMenu } from '@/api/identity/menu'
import MenuTreeSelect from '../components/MenuTreeSelect.vue'
import { h } from 'vue'

const props = defineProps<{
  visible: boolean
  title: string
  menuId?: string
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
  menuType: 0,
  visible: 1,
  status: 0,
  perms: '',
  icon: ''
})

const transKeyModule = ref('')
const transKeyBiz = ref('')

// 响应式表单校验规则
const rules = {
  menuName: [
    { required: true, message: t('identity.menu.fields.menuDirectoryName.validation.required') },
    { min: 2, max: 50, message: t('identity.menu.fields.menuDirectoryName.validation.length') },
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
        return Promise.reject(t('identity.menu.fields.menuDirectoryName.validation.format'));
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
  visible: [],
  status: [],
  icon: [
    { required: true, message: t('identity.menu.fields.icon.validation.required') }
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
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  } finally {
    loading.value = false
  }
}

// transKey 计算属性
const transKey = computed(() => {
  let second
  if (form.parentId === '0') {
    second = form.path
    return `menu.${second}._self`
  } else {
    const parent = allMenuList.value.find(item => item.menuId === form.parentId)
    second = parent ? parent.path : ''
    const third = form.path
    return `menu.${second}.${third}._self`
  }
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

function updateOrderNum() {
  const val = form.parentId
  let siblings: any[] = []
  if (val === '0') {
    siblings = allMenuList.value
  } else if (typeof val === 'number') {
    function findNode(list: any[], id: number): any | undefined {
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

function onSelectMenu(menu?: any) {
  if (Number(form.parentId) === 0) {
    const topLevelMenus = allMenuList.value.filter((item: any) => item.parentId === 0);
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

const parentPath = computed(() => {
  const parent = allMenuList.value.find(item => item.menuId === form.parentId)
  return parent ? parent.path : ''
})

defineExpose({
  async submitForm() {
    await formRef.value?.validate()
    // 校验transKey格式
    const key = transKey.value.toLowerCase()
    // menu.字母._self 或 menu.字母.字母._self
    const reg = /^menu\.[a-z]+(_self)?$|^menu\.[a-z]+\.[a-z]+\._self$/
    const reg2 = /^menu\.[a-z]+\._self$|^menu\.[a-z]+\.[a-z]+\._self$/
    if (!reg2.test(key)) {
      throw new Error('翻译key格式错误，必须为 menu.xxx._self 或 menu.xxx.xxx._self，且中间为字母')
    }
    let res
    const submitData = {
      ...form,
      transKey: key
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

onMounted(() => {
  if (props.menuId) {
    getInfo(props.menuId)
  }
})
</script> 
