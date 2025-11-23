//===================================================================
// 项目名 : Lean.Takt
// 文件名称: UserProfile.vue
// 创建者 : Claude
// 创建时间: 2024-03-27
// 版本号 : v1.0.0
// 描述 : 用户个人信息页面
//===================================================================

<template>
  <div class="user-profile">
    <a-card :bordered="false">
      <a-tabs v-model:activeKey="activeTab">
        <!-- 基本信息 -->
        <a-tab-pane key="basic" :tab="t('identity.user.tab.basic')">
          <div class="basic-info-container">
            <!-- 左侧头像 -->
            <div class="avatar-section">
              <a-upload name="avatar" list-type="picture-card" class="avatar-uploader" :show-upload-list="false"
                :action="uploadUrl" :headers="uploadHeaders" :before-upload="beforeUpload" @change="handleAvatarChange">
                <img v-if="displayInfo.avatar" :src="displayInfo.avatar" alt="avatar" />
                <div v-else>
                  <loading-outlined v-if="uploading" />
                  <plus-outlined v-else />
                  <div class="ant-upload-text">{{ t('identity.user.fields.avatar.upload') }}</div>
                </div>
              </a-upload>
            </div>

            <!-- 右侧表单 -->
            <div class="form-section">
              <a-form ref="formRef" :model="form" :rules="rules" :label-col="{ span: 4 }" :wrapper-col="{ span: 18 }">

                <a-form-item :label="t('identity.user.fields.userName.label')" name="userName">
                  <a-input v-model:value="displayInfo.userName"
                    :placeholder="t('identity.user.fields.userName.placeholder')" disabled />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.userType.label')" name="userType">
                  <a-input v-model:value="displayInfo.userType"
                    :placeholder="t('identity.user.fields.userType.placeholder')" disabled />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.nickName.label')" name="nickName">
                  <a-input v-model:value="form.nickName"
                    :placeholder="t('identity.user.fields.nickName.placeholder')" />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.realName.label')" name="realName">
                  <a-input v-model:value="form.realName" :placeholder="t('identity.user.fields.realName.placeholder')"
                    disabled />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.fullName.label')" name="fullName">
                  <a-input v-model:value="form.fullName" :placeholder="t('identity.user.fields.fullName.placeholder')"
                    disabled />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.phoneNumber.label')" name="phoneNumber">
                  <a-input v-model:value="form.phoneNumber"
                    :placeholder="t('identity.user.fields.phoneNumber.placeholder')" />
                </a-form-item>
                <a-form-item :label="t('identity.user.fields.email.label')" name="email">
                  <a-input v-model:value="form.email" :placeholder="t('identity.user.fields.email.placeholder')" />
                </a-form-item>
                <a-form-item :wrapper-col="{ span: 18, offset: 4 }">
                  <a-button type="primary" :loading="submitting" @click="handleSubmit">
                    {{ t('common.button.submit') }}
                  </a-button>
                  <a-button style="margin-left: 8px" @click="handleReset">
                    {{ t('common.button.reset') }}
                  </a-button>
                </a-form-item>
              </a-form>
            </div>
          </div>
        </a-tab-pane>

        <!-- 登录日志 -->
        <a-tab-pane key="loginLog" :tab="t('identity.user.tab.loginLog')">
          <user-login-log :user-id="displayInfo.userId" :open="activeTab === 'loginLog'" :scroll="{ y: '540px' }" />
        </a-tab-pane>

        <!-- 操作日志 -->
        <a-tab-pane key="operateLog" :tab="t('identity.user.tab.operateLog')">
          <user-oper-log :user-id="displayInfo.userId" :open="activeTab === 'operateLog'" :scroll="{ y: '540px' }" />
        </a-tab-pane>

        <!-- 异常日志 -->
        <a-tab-pane key="errorLog" :tab="t('identity.user.tab.errorLog')">
          <user-exception-log :user-id="displayInfo.userId" :open="activeTab === 'errorLog'" :scroll="{ y: '540px' }" />
        </a-tab-pane>

        <!-- 任务日志 -->
        <a-tab-pane key="taskLog" :tab="t('identity.user.tab.taskLog')">
          <user-quartz-log :user-id="displayInfo.userId" :open="activeTab === 'taskLog'" :scroll="{ y: '540px' }" />
        </a-tab-pane>
      </a-tabs>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'
import { useUserStore } from '@/stores/userStore'
import { PlusOutlined, LoadingOutlined } from '@ant-design/icons-vue'
import { getInfo } from '@/api/identity/auth/auth'
import { updateUserProfile, getByIdAsync } from '@/api/identity/user'
import type { TaktUserProfileUpdate, TaktUserAvatarUpdate } from '@/types/identity/user'
import type { UserInfoResponse } from '@/stores/userStore'
import { getToken } from '@/utils/authUtil'
import UserLoginLog from '@/views/logging/loginlog/components/UserLoginLog.vue'
import UserOperLog from '@/views/logging/operlog/components/UserOperLog.vue'
import UserExceptionLog from '@/views/logging/exceptionlog/components/UserExceptionLog.vue'
import UserQuartzLog from '@/views/logging/quartzlog/components/UserQuartzLog.vue'

const { t } = useI18n()
const userStore = useUserStore()

// 当前激活的页签
const activeTab = ref('basic')

// === 基本信息表单相关 ===
// 表单引用
const formRef = ref<FormInstance>()

// 表单数据 - 直接使用 TaktUserProfileUpdate 接口
const form = reactive<TaktUserProfileUpdate>({
  userId: '',
  nickName: '',
  englishName: '',
  fullName: '',
  realName: '',
  phoneNumber: '',
  email: '',
  gender: 0,
  oldPassword: '',
  newPassword: ''
})

// 其他需要显示的字段（只读）
const displayInfo = reactive({
  userId: '',
  userName: '',
  userType: 0,
  avatar: undefined as string | undefined,
  status: 1,
})

// 表单校验规则
const rules: Record<string, Rule[]> = {
  nickName: [
    { required: true, message: t('identity.user.nickName.validation.required') },
    { min: 2, max: 30, message: t('identity.user.nickName.validation.length') }
  ],
  email: [
    { required: true, message: t('identity.user.email.validation.required') },
    { type: 'email', message: t('identity.user.email.validation.invalid') }
  ],
  phoneNumber: [
    { required: true, message: t('identity.user.phoneNumber.validation.required') },
    { pattern: /^1[3-9]\d{9}$/, message: t('identity.user.phoneNumber.validation.invalid') }
  ]
}

// 上传状态
const uploading = ref(false)
const submitting = ref(false)

// 上传配置
const uploadUrl = '/api/TaktFile/upload'
const uploadHeaders = {
  Authorization: `Bearer ${getToken()}`
}

// 获取用户信息
const getUserInfo = async () => {
  try {
    // 获取基本信息
    const res = await getInfo()
    console.log('getInfo响应:', res)

    if (res && res.data) {
      const userInfo = res.data
      console.log('解析后的用户信息:', userInfo)

      // 检查userId是否存在
      if (!userInfo.userId) {
        console.error('用户ID不存在:', userInfo)
        message.error('获取用户信息失败：用户ID不存在')
        return
      }

      // 设置只读显示信息
      Object.assign(displayInfo, {
        userId: String(userInfo.userId),
        userName: userInfo.userName,
        userType: userInfo.userType,
        avatar: userInfo.avatar
      })

      // 设置表单数据（从基本信息获取）
      Object.assign(form, {
        userId: String(userInfo.userId),
        nickName: userInfo.nickName || '',
        englishName: userInfo.englishName || '',
        fullName: userInfo.fullName || '',
        realName: userInfo.realName || ''
      })

      // 尝试获取详细信息（如果失败则使用基本信息）
      try {
        console.log('准备获取用户详细信息，userId:', userInfo.userId)
        const detailRes = await getByIdAsync(userInfo.userId)
        console.log('getUser响应:', detailRes)

        if (detailRes && detailRes.data) {
          const userDetail = detailRes.data
          // 更新表单数据
          Object.assign(form, {
            phoneNumber: userDetail.phoneNumber || form.phoneNumber || '',
            email: userDetail.email || form.email || '',
            gender: userDetail.gender || form.gender || 0
          })

          // 设置只读显示信息
          Object.assign(displayInfo, {
            status: userDetail.userStatus || 1
          })
        }
      } catch (detailError) {
        console.warn('获取用户详细信息失败，使用基本信息:', detailError)
        // 不显示错误消息，因为基本信息已经获取成功
        // 表单中的其他字段保持默认值
      }
    } else {
      console.error('getInfo响应无效:', res)
      message.error(t('common.failed'))
    }
  } catch (error) {
    console.error('获取用户信息失败:', error)
    message.error(t('common.failed'))
  }
}

// 上传前校验
const beforeUpload = (file: File) => {
  const isJpgOrPng = file.type === 'image/jpeg' || file.type === 'image/png'
  if (!isJpgOrPng) {
    message.error(t('identity.user.fields.avatar.type'))
    return false
  }
  const isLt2M = file.size / 1024 / 1024 < 2
  if (!isLt2M) {
    message.error(t('identity.user.fields.avatar.size'))
    return false
  }
  return true
}

// 处理头像变更
const handleAvatarChange = async (info: any) => {
  if (info.file.status === 'uploading') {
    uploading.value = true
    return
  }
  if (info.file.status === 'done') {
    uploading.value = false
    if (info.file.response.code === 200) {
      const avatarUrl = info.file.response.data.url

      // 使用头像更新接口
      const avatarParams: TaktUserAvatarUpdate = {
        userId: form.userId,
        avatar: avatarUrl
      }

      try {
        // TODO: 调用头像更新API
        // await updateUserAvatar(avatarParams)

        displayInfo.avatar = avatarUrl
        message.success('头像更新成功')
      } catch (error) {
        console.error('头像更新失败:', error)
        message.error('头像更新失败，请重试')
      }
    } else {
      message.error(info.file.response.msg || t('common.upload.failed'))
    }
  }
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    const formData = { ...form }

    // 使用个人信息更新接口
    const profileParams: TaktUserProfileUpdate = {
      userId: formData.userId,
      nickName: formData.nickName,
      englishName: formData.englishName,
      fullName: formData.fullName,
      realName: formData.realName,
      phoneNumber: formData.phoneNumber,
      email: formData.email,
      gender: formData.gender,
      oldPassword: formData.oldPassword,
      newPassword: formData.newPassword
    }

    // TODO: 调用个人信息更新API
    // await updateUserProfile(profileParams)

    message.success('个人信息更新成功')
    // 重新获取用户信息
    await userStore.getUserInfo()
  } catch (error) {
    console.error('更新个人信息失败:', error)
    message.error('更新个人信息失败，请重试')
  }
}

// 重置表单
const handleReset = () => {
  formRef.value?.resetFields()
  getUserInfo()
}

// 组件挂载时获取用户信息
onMounted(() => {
  getUserInfo()
})
</script>

<style lang="less" scoped>
.user-profile {
  padding: 24px;

  .basic-info-container {
    display: flex;
    gap: 48px;
    padding: 24px;
    max-width: 900px; // 限制最大宽度
    margin: 0 auto; // 水平居中
    justify-content: center; // flex容器内部居中

    .avatar-section {
      flex: 0 0 auto;

      .avatar-uploader {
        :deep(.ant-upload) {
          width: 180px;
          height: 180px;
          margin: 0;

          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
          }
        }
      }
    }

    .form-section {
      flex: 1;
      min-width: 400px; // 设置最小宽度
      max-width: 600px; // 设置最大宽度

      :deep(.ant-form-item) {
        margin-bottom: 24px;
      }

      :deep(.ant-btn) {
        min-width: 100px;
      }
    }
  }

  :deep(.ant-table-wrapper) {
    margin-top: 16px;
  }
}
</style>
