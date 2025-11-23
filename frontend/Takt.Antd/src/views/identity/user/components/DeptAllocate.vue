<template>
  <a-modal
    :open="visible"
    :title="t('identity.user.allocateDept')"
    :width="500"
    :mask-closable="false"
    @cancel="handleCancel"
    @ok="handleSubmit"
  >
    <a-form :model="formState" :rules="rules" ref="formRef">
      <a-form-item name="deptIds" :label="t('identity.user.deptIds')">
        <a-tree-select
          v-model:value="formState.deptIds"
          :tree-data="deptTree"
          :show-checked-strategy="TreeSelect.SHOW_CHILD"
          :placeholder="t('identity.user.deptIds.placeholder')"
          :default-expand-all="true"
          :tree-default-expand-all="true"
          multiple
          tree-checkable
          :max-tag-count="10"
          style="width: 100%"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, watch, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { TreeSelectProps } from 'ant-design-vue'
import { TreeSelect } from 'ant-design-vue'
import { getUserDepts, assignUserDepts } from '@/api/identity/user'
import { getDeptTree } from '@/api/identity/dept'

const { t } = useI18n()

const props = defineProps<{
  visible: boolean
  userId: string
}>()

const emit = defineEmits(['update:visible', 'success'])

const formRef = ref()
const deptTree = ref<TreeSelectProps['treeData']>([])

const formState = reactive({
  deptIds: [] as number[]
})

const rules = {
  deptIds: [{ required: true, message: t('identity.user.deptIds.required') }]
}

// 获取部门树
const fetchDeptTree = async () => {
  try {
    console.log('开始获取部门树数据')
    const res = await getDeptTree({ pageIndex: 1, pageSize: 1000 })
    console.log('部门树接口返回数据:', res)
    if (res.data) {
      const convertToTreeData = (data: any[]): TreeSelectProps['treeData'] => {
        return data.map(item => ({
          title: item.deptName,
          value: Number(item.deptId),
          key: Number(item.deptId),
          isLeaf: !item.children || item.children.length === 0,
          children: item.children ? convertToTreeData(item.children) : undefined
        }))
      }
      deptTree.value = convertToTreeData(res.data)
      console.log('转换后的部门树数据:', deptTree.value)
      return true
    }
    return false
  } catch (error) {
    console.error('获取部门树失败:', error)
    message.error(t('common.failed'))
    return false
  }
}

// 获取用户部门
const fetchUserDepts = async () => {
  try {
    console.log('开始获取用户部门数据, userId:', props.userId)
    const res = await getUserDepts(props.userId)
    console.log('用户部门接口返回数据:', res)
    if (res.data) {
      const deptIds = res.data.map((item: any) => Number(item.deptId))
      console.log('转换后的用户部门ID:', deptIds)
      return deptIds
    }
    return []
  } catch (error) {
    console.error('获取用户部门失败:', error)
    message.error(t('common.failed'))
    return []
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}

// 提交
const handleSubmit = async () => {
  try {
    console.log('开始提交部门分配')
    await formRef.value.validate()
    console.log('表单验证通过')
    
    // 确保 deptIds 不为空
    if (!formState.deptIds || formState.deptIds.length === 0) {
      console.warn('部门ID为空')
      message.warning(t('identity.user.deptIds.required'))
      return
    }
    
    console.log('准备提交的部门ID:', formState.deptIds)
    const res = await assignUserDepts(props.userId, formState.deptIds.map(id => id.toString()))
    console.log('分配部门接口返回:', res)
    
    if (res.data) {
      message.success(t('common.success'))
      emit('success')
      formState.deptIds = []
      handleCancel()
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('分配部门失败:', error)
    message.error(t('common.failed'))
  }
}

// 监听弹窗显示
watch(
  () => props.visible,
  async (val) => {
    console.log('弹窗显示状态变化:', val)
    if (val) {
      // 先获取部门树
      const treeLoaded = await fetchDeptTree()
      console.log('部门树加载状态:', treeLoaded)
      if (!treeLoaded) {
        message.error(t('common.failed'))
        return
      }
      
      // 等待树渲染完成
      await nextTick()
      console.log('树渲染完成')
      
      // 获取用户部门并设置选中值
      const deptIds = await fetchUserDepts()
      console.log('获取到的用户部门ID:', deptIds)
      if (deptIds.length > 0) {
        formState.deptIds = deptIds
        console.log('设置选中部门:', formState.deptIds)
      }
    } else {
      formState.deptIds = []
      console.log('清空选中部门')
    }
  },
  { immediate: true }
)
</script> 