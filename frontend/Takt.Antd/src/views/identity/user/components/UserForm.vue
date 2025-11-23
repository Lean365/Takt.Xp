<template>
  <Takt-modal
    :open="visible"
    :title="title"
    :loading="loading"
    :width="800"
    @update:open="handleVisibleChange"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="form"
      :rules="rules"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 18 }"
    >
      <a-tabs>
        <!-- 用户信息 -->
        <a-tab-pane :key="'userInfo'" :tab="t('identity.user.tabs.userInfo')">
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.userName.label')" name="userName">
                <a-input
                  v-model:value="form.userName"
                  :placeholder="t('identity.user.fields.userName.placeholder')"
                  :disabled="!!props.userId"
                  show-count
                  :maxlength="50"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16" v-if="!props.userId">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.password.label')" name="password">
                <a-input-password
                  v-model:value="form.password"
                  :placeholder="t('identity.user.fields.password.placeholder')"
                  :disabled="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.nickName.label')" name="nickName">
                <a-input
                  v-model:value="form.nickName"
                  :placeholder="t('identity.user.fields.nickName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.realName.label')" name="realName">
                <a-input
                  v-model:value="form.realName"
                  :placeholder="t('identity.user.fields.realName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.fullName.label')" name="fullName">
                <a-input
                  v-model:value="form.fullName"
                  :placeholder="t('identity.user.fields.fullName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.englishName.label')" name="englishName">
                <a-input
                  v-model:value="form.englishName"
                  :placeholder="t('identity.user.fields.englishName.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>
          
          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.userType.label')" name="userType">
                <Takt-select
                  v-model:value="form.userType"
                  dict-type="identity_user_type"
                  :placeholder="t('identity.user.fields.userType.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.email.label')" name="email">
                <a-input
                  v-model:value="form.email"
                  :placeholder="t('identity.user.fields.email.placeholder')"
                  show-count
                  :maxlength="100"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.phoneNumber.label')" name="phoneNumber">
                <a-input
                  v-model:value="form.phoneNumber"
                  :placeholder="t('identity.user.fields.phoneNumber.placeholder')"
                  show-count
                  :maxlength="20"
                />
              </a-form-item>
            </a-col>
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.gender.label')" name="gender">
                <Takt-select
                  v-model:value="form.gender"
                  dict-type="identity_user_gender"
                  type="radio"
                  :placeholder="t('identity.user.fields.gender.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="12">
              <a-form-item :label="t('identity.user.fields.userStatus.label')" name="userStatus">
                <Takt-select
                  v-model:value="form.userStatus"
                  dict-type="sys_normal_disable"
                  type="radio"
                  :placeholder="t('identity.user.fields.userStatus.placeholder')"
                  :show-all="false"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.remark.label')" name="remark">
                <a-textarea
                  v-model:value="form.remark"
                  :placeholder="t('identity.user.fields.remark.placeholder')"
                  :rows="4"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 从属关系 -->
        <a-tab-pane :key="'organization'" :tab="t('identity.user.tabs.organization')">
          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.deptName.label')" name="deptIds">
                <Takt-select
                  v-model:value="form.deptIds"
                  :options="deptOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.deptName.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.role.label')" name="roleIds">
                <Takt-select
                  v-model:value="form.roleIds"
                  :options="roleOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.role.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>

          <a-row :gutter="16">
            <a-col :span="24">
              <a-form-item :label="t('identity.user.fields.post.label')" name="postIds">
                <Takt-select
                  v-model:value="form.postIds"
                  :options="postOptions"
                  mode="multiple"
                  :placeholder="t('identity.user.fields.post.placeholder')"
                />
              </a-form-item>
            </a-col>
          </a-row>
        </a-tab-pane>

        <!-- 头像 -->
        <a-tab-pane :key="'avatar'" :tab="t('identity.user.tabs.avatar')">
        <div class="avatar-container">
          <!-- 头像管理区域 -->
          <div class="avatar-management">
            <!-- 头像容器区域 -->
            <div class="avatar-containers">
              <!-- 左侧：头像预览容器 -->
              <div class="avatar-preview-section" v-if="form.avatar">
                <div class="container-label">{{ t('identity.user.fields.avatar.currentAvatar') }}</div>
                <div class="avatar-preview">
                  <a-image
                    :src="avatarFullUrl"
                    :width="80"
                    :height="80"
                    :preview="{
                      src: avatarFullUrl
                    }"
                    style="border-radius: 50%; object-fit: cover; border: 2px solid #d9d9d9;"
                  />
                </div>
              </div>
              
              <!-- 右侧：上传头像容器 -->
              <div class="avatar-upload-section">
                <div class="container-label">{{ t('identity.user.fields.avatar.avatarUpload') }}</div>
                <div class="avatar-upload-container">
                  <Takt-avatar-upload
                    :upload-url="uploadUrl"
                    save-path="uploads/avatars"
                    :max-size="5"
                    :file-name="form.userName"
                    :compress="{
                      quality: 0.8,
                      maxWidth: 200,
                      maxHeight: 200
                    }"
                    @success="handleUploadSuccess"
                    @error="handleUploadError"
                    @before-upload="handleBeforeUpload"
                    @file-selected="handleFileSelected"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
              </a-tab-pane>
      </a-tabs>
    </a-form>
  </Takt-modal>
</template>

<script lang="ts" setup>
// ==================== 导入语句 ====================
import { ref, reactive, watch, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance, Rule } from 'ant-design-vue/es/form'

import type { TaktUser, TaktUserCreate, TaktUserUpdate } from '@/types/identity/user'

import { getByIdAsync, createUser, updateUser } from '@/api/identity/user'
import { getRoleOptions } from '@/api/identity/role'
import { getPostOptions } from '@/api/identity/post'
import { getDeptOptions } from '@/api/identity/dept'
import { uploadAvatar } from '@/api/routine/document/file'

import { RegexUtils } from '@/utils/regexUtil'
import { getToken } from '@/utils/authUtil'

// ==================== 类型定义 ====================
// 使用 user.d.ts 中已定义的接口，避免重复定义
type TaktUserForm = Omit<TaktUserCreate, 'userId'> & {
  remark?: string
}

// ==================== Props和Emits ====================
const props = defineProps<{
  visible: boolean
  title: string
  userId?: string
}>()

const emit = defineEmits<{
  (e: 'update:visible', visible: boolean): void
  (e: 'success'): void
}>()

const { t } = useI18n()

// ==================== 响应式数据 ====================
// 表单引用
const formRef = ref<FormInstance>()

// 表单数据
const form = reactive<TaktUserForm>({
  userName: '',
  nickName: '',
  englishName: '',
  realName: '',
  fullName: '',
  phoneNumber: '',
  email: '',
  gender: 0,
  userType: 0,
  userStatus: 0,
  avatar: '',
  password: '123456',
  remark: '',
  roleIds: [],
  postIds: [],
  deptIds: []
})

// 计算头像完整URL
const avatarFullUrl = computed(() => {
  if (!form.avatar) return ''
  
  // 如果是相对路径，添加基础URL
  if (form.avatar.startsWith('/')) {
    const baseUrl = import.meta.env.VITE_API_BASE_URL || 'https://localhost:50081'
    return baseUrl + form.avatar
  }
  
  return form.avatar
})

// 选项数据
const roleOptions = ref<{ label: string; value: string | number }[]>([])
const postOptions = ref<{ label: string; value: string | number }[]>([])
const deptOptions = ref<{ label: string; value: string | number }[]>([])

// 加载状态
const loading = ref(false)

// 上传相关
const uploadUrl = `${import.meta.env.VITE_API_BASE_URL}/api/TaktFile/upload-avatar`

// ==================== 表单验证规则 ====================
const rules: Record<string, Rule[]> = {
  userName: [
    { required: true, message: t('identity.user.fields.userName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidUsername(value)) {
          return Promise.reject(t('identity.user.fields.userName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  nickName: [
    { required: true, message: t('identity.user.fields.nickName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidNickname(value)) {
          return Promise.reject(t('identity.user.fields.nickName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  englishName: [
    { required: true, message: t('identity.user.fields.englishName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidEnglishName(value)) {
          return Promise.reject(t('identity.user.fields.englishName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  fullName: [
    { required: true, message: t('identity.user.fields.fullName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidFullName(value)) {
          return Promise.reject(t('identity.user.fields.fullName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  realName: [
    { required: true, message: t('identity.user.fields.realName.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.isValidRealName(value)) {
          return Promise.reject(t('identity.user.fields.realName.validation.format'));
        }
        return Promise.resolve();
      }
    }
  ],
  userType: [
    { required: true, message: t('identity.user.fields.userType.validation.required') }
  ],
  email: [
    { required: true, message: t('identity.user.fields.email.validation.required') },
    { 
      validator: (_, value) => {
        if (value && !RegexUtils.isValidEmail(value)) {
          return Promise.reject(t('identity.user.fields.email.validation.invalid'))
        }
        return Promise.resolve()
      }
    }
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.fields.phoneNumber.validation.required') },
    {
      validator: (_, value) => {
        if (value && !RegexUtils.PHONE.test(value)) {
          return Promise.reject(t('identity.user.fields.phoneNumber.validation.invalid'))
        }
        return Promise.resolve()
      }
    }
  ],
  gender: [
    { required: true, message: t('identity.user.fields.gender.validation.required') }
  ],
  userStatus: [
    { required: true, message: t('identity.user.fields.userStatus.validation.required') }
  ],
  roleIds: [
    { required: true, type: 'array', message: t('identity.user.fields.role.validation.required') }
  ],
  postIds: [
    { required: true, type: 'array', message: t('identity.user.fields.post.validation.required') }
  ],
  deptIds: [
    { required: true, type: 'array', message: t('identity.user.fields.deptName.validation.required') }
  ]
}

// ==================== 方法函数 ====================
// 获取用户信息
const getInfo = async (userId: string) => {
  try {
    const res = await getByIdAsync(userId)
    if (res && res.data) {
      const userData = res.data as any // 使用 any 类型避免类型检查错误
      
      // 确保多选字段是数组格式
      if (userData.roleIds && !Array.isArray(userData.roleIds)) {
        userData.roleIds = String(userData.roleIds).split(',').map((id: any) => Number(id.trim())).filter((id: number) => !isNaN(id))
      }
      if (userData.postIds && !Array.isArray(userData.postIds)) {
        userData.postIds = String(userData.postIds).split(',').map((id: any) => Number(id.trim())).filter((id: number) => !isNaN(id))
      }
      if (userData.deptIds && !Array.isArray(userData.deptIds)) {
        userData.deptIds = String(userData.deptIds).split(',').map((id: any) => Number(id.trim())).filter((id: number) => !isNaN(id))
      }
      
      // 如果没有数据，设置为空数组
      if (!userData.roleIds) userData.roleIds = []
      if (!userData.postIds) userData.postIds = []
      if (!userData.deptIds) userData.deptIds = []
      
      // 直接赋值，保持响应式更新
      Object.assign(form, userData)
    } else {
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error(error)
    message.error(t('common.failed'))
  }
}

// 获取选项数据
const fetchOptions = async () => {
  try {
    const [roles, posts, depts] = await Promise.all([
      getRoleOptions(),
      getPostOptions(),
      getDeptOptions()
    ])
    
    // 通用转换函数：从 TaktSelectOption 转换为 { label, value } 格式
    const convertToOption = (item: any) => ({
      label: item.dictLabel,
      value: item.dictValue
    })
    
    roleOptions.value = roles.data?.map(convertToOption) || []
    postOptions.value = posts.data?.map(convertToOption) || []
    deptOptions.value = depts.data?.map(convertToOption) || []
    
  } catch (error) {
    console.error(t('identity.user.table.getOptionsFailed'), error)
  }
}

// 重置表单
const resetForm = () => {  
  Object.assign(form, {
    userName: '',
    nickName: '',
    realName: '',
    fullName: '',
    englishName: '',
    userType: 0,
    password: '123456',
    email: '',
    phoneNumber: '',
    gender: 0,
    avatar: '',
    userStatus: 0,
    remark: '',
    roleIds: [],
    postIds: [],
    deptIds: []
  })
}

// 处理弹窗显示状态变化
const handleVisibleChange = (val: boolean) => {
  emit('update:visible', val)
  if (!val) {
    resetForm()
  }
}

// 处理取消
const handleCancel = () => {
  emit('update:visible', false)
  resetForm() // 取消时也要重置表单
}

// 统一的错误处理方法
const handleError = (error: any, operation: string) => {
  console.error(`${operation}失败:`, error)
  
  // 如果是取消的请求（重定向中），不显示错误信息
  if (error.message === '页面重定向中，请求已取消') {
    return
  }
  
  // 显示具体的错误信息或通用错误信息
  if (error.response?.data?.msg) {
    message.error(error.response.data.msg)
  } else {
    message.error(t('common.failed'))
  }
}

// 提交表单
const handleSubmit = () => {
  formRef.value?.validate().then(async () => {
    try {
      let res
      if (props.userId) {
        // 更新用户
        const updateData: TaktUserUpdate = {
          ...form,
          userId: props.userId,
          password: '' // 更新时不修改密码
        }
        
        loading.value = true
        res = await updateUser(updateData)
        
        if (res) {
          message.success(t('common.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(t('common.failed'))
        }
      } else {
        // 创建用户
        const createData: TaktUserCreate = {
          ...form,
          userId: ''
        }
        
        res = await createUser(createData)
        
        if (res) {
          message.success(t('common.success'))
          emit('success')
          handleVisibleChange(false)
        } else {
          message.error(t('common.failed'))
        }
      }
    } catch (error: any) {
      handleError(error, props.userId ? t('identity.user.table.updateUser') : t('identity.user.table.createUser'))
    } finally {
      loading.value = false
    }
  })
}

// 上传相关方法
const handleUploadSuccess = (result: any) => {
  form.avatar = result.url
  message.success(t('identity.user.avatar.uploadSuccess'))
}

// 文件选择事件处理
const handleFileSelected = async (file: File) => {
  try {
    // 显示上传中状态
    message.loading(t('identity.user.fields.avatar.uploading'), 0)
    
    // 调用头像上传API
    const response = await uploadAvatar(file)
    
    // 关闭加载提示
    message.destroy()
    
    // 检查响应状态
    if (response && response.data) {
      // 上传成功，更新表单
      const responseData = response.data as any
      let avatarUrl = ''
      
      // 使用正确的大写字段名
      if (responseData.Data && responseData.Data.url) {
        // 从Data字段中获取url
        avatarUrl = responseData.Data.url
      } else if (responseData.Data && responseData.Data.fileUrl) {
        // 尝试fileUrl字段
        avatarUrl = responseData.Data.fileUrl
      } else if (responseData.url) {
        // 直接返回业务数据的情况
        avatarUrl = responseData.url
      } else if (responseData.fileUrl) {
        // 尝试其他可能的字段
        avatarUrl = responseData.fileUrl
      }
      
      form.avatar = avatarUrl
      message.success(t('identity.user.fields.avatar.uploadSuccess'))
    } else {
      message.error(t('identity.user.fields.avatar.uploadError'))
    }
  } catch (error) {
    // 关闭加载提示
    message.destroy()
    
    console.error(t('identity.user.fields.avatar.uploadError'), error)
    message.error(t('identity.user.fields.avatar.uploadError'))
  }
}

const handleUploadError = (error: any) => {
  message.error(t('identity.user.avatar.uploadError'))
}

// 上传前处理
const handleBeforeUpload = (file: any) => {
  // 验证文件类型
  const isImage = file.type.startsWith('image/')
  if (!isImage) {
    message.error(t('identity.user.fields.avatar.onlyImage'))
    return false
  }
  
  // 验证文件大小 (5MB)
  const isLt5M = file.size / 1024 / 1024 < 5
  if (!isLt5M) {
    message.error(t('identity.user.fields.avatar.sizeLimit'))
    return false
  }
  
  return true
}

// ==================== 监听器 ====================
// 监听nickName变化，统一自动赋值realName、fullName、englishName（仅新建时且字段为空）
watch(
  () => form.nickName,
  (val) => {
    // 只在新建用户时自动赋值
    if (!props.userId && val) {
      // 如果字段为空，则自动赋值
      if (!form.realName) form.realName = val
      if (!form.fullName) form.fullName = val
      if (!form.englishName) form.englishName = val
    }
  }
)

// 监听userName变化，自动赋值englishName（仅为空时，避免与nickName重复）
watch(
  () => form.userName,
  (val) => {
    if (!props.userId && (!form.englishName || form.englishName.trim() === '') && !form.nickName) {
      form.englishName = val
    }
  }
)

// 监听用户ID变化
watch(
  () => props.userId,
  (val) => {
    if (val) {
      // 只有在弹窗打开且有userId时才获取用户信息
      if (props.visible) {
        getInfo(val)
      }
    } else {
      // 新建时同步赋值
      if (form.nickName) {
        form.realName = form.nickName
        form.fullName = form.nickName
        form.englishName = form.nickName
      }
    }
  }
)

// 监听visible变化，确保弹窗打开时获取必要数据
watch(
  () => props.visible,
  (val) => {
    if (val) {
      // 弹窗打开时获取选项数据
      fetchOptions()
      
      // 如果是编辑模式，获取用户信息
      if (props.userId) {
        getInfo(props.userId)
      }
    }
  }
)

// ==================== 生命周期钩子 ====================
// 组件挂载时不预加载数据，只在弹窗打开时加载
onMounted(() => {
  // 移除预加载，避免重复请求
})
</script>

<style lang="less" scoped>
.avatar-container {
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 24px;
}

.avatar-management {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.avatar-containers {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 20px;
}

.avatar-preview-section {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.avatar-upload-section {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.avatar-label {
  margin-bottom: 8px;
  text-align: center;
  width: 100%;
  font-size: 16px;
  color: #262626;
}

.avatar-preview-wrapper {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-bottom: 20px;
}

.avatar-preview {
  width: 128px; /* 整个预览框容器大小 */
  height: 128px; /* 整个预览框容器大小 */
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px solid #d9d9d9;
  border-radius: 8px;
  background-color: #fafafa;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.avatar-info {
  margin-top: 15px;
  text-align: center;
  
  p {
    margin: 5px 0;
    
    &:first-child {
      font-weight: 600;
      color: #262626;
    }
    
    &:last-child {
      font-size: 0.85em;
      color: #8c8c8c;
      word-break: break-all;
      max-width: 200px;
    }
  }
}

.avatar-upload-container {
  width: 128px; /* 与预览头像大小一致 */
  height: 128px; /* 与预览头像大小一致 */
  display: flex;
  justify-content: center;
  align-items: center;
  border: 1px dashed #d9d9d9;
  border-radius: 8px;
  background-color: #fafafa;
  overflow: hidden; /* 确保图片不会溢出容器 */
  
  /* 深度选择器来修改上传组件的样式 */
  :deep(.Takt-avatar-upload) {
    width: 100%;
    height: 100%;
    
    .Takt-upload-list {
      width: 100%;
      height: 100%;
      
      .ant-upload-list-picture-card {
        width: 100%;
        height: 100%;
        
        .ant-upload-list-item {
          width: 100% !important;
          height: 100% !important;
          margin: 0 !important;
          border: none !important;
          border-radius: 8px;
          
          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 8px;
          }
        }
        
        .ant-upload-select {
          width: 100% !important;
          height: 100% !important;
          margin: 0 !important;
          border: none !important;
          border-radius: 8px;
          display: flex;
          flex-direction: column;
          justify-content: center;
          align-items: center;
          
          .ant-upload-drag-container {
            width: 100%;
            height: 100%;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
          }
        }
      }
    }
  }
}
</style> 
