<template>
  <Takt-modal
    v-model:open="visible"
    title="登录详情"
    :width="800"
    :footer="null"
  >
    <a-descriptions bordered :column="2">
      <a-descriptions-item label="用户名">
        {{ loginInfo?.userName }}
      </a-descriptions-item>
      <a-descriptions-item label="登录状态">
        <a-tag :color="loginInfo?.loginStatus === 1 ? 'success' : 'error'">
          {{ loginInfo?.loginStatus === 1 ? '成功' : '失败' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="登录类型">
        {{ getLoginTypeName(loginInfo?.loginType) }}
      </a-descriptions-item>
      <a-descriptions-item label="登录来源">
        {{ loginInfo?.loginSource }}
      </a-descriptions-item>
      <a-descriptions-item label="IP地址">
        {{ loginInfo?.ipAddress }}
      </a-descriptions-item>
      <a-descriptions-item label="登录地点">
        {{ loginInfo?.deviceInfo?.location }}
      </a-descriptions-item>
      <a-descriptions-item label="浏览器">
        {{ loginInfo?.deviceInfo?.browserType === 0 ? 'Chrome' :
           loginInfo?.deviceInfo?.browserType === 1 ? 'Firefox' :
           loginInfo?.deviceInfo?.browserType === 2 ? 'Edge' :
           loginInfo?.deviceInfo?.browserType === 3 ? 'Safari' :
           loginInfo?.deviceInfo?.browserType === 4 ? 'IE' : '其他'
        }}
        {{ loginInfo?.deviceInfo?.browserVersion }}
      </a-descriptions-item>
      <a-descriptions-item label="操作系统">
        {{ loginInfo?.deviceInfo?.osType === 0 ? 'Windows' :
           loginInfo?.deviceInfo?.osType === 1 ? 'Android' :
           loginInfo?.deviceInfo?.osType === 2 ? 'iOS' :
           loginInfo?.deviceInfo?.osType === 3 ? 'MacOS' :
           loginInfo?.deviceInfo?.osType === 4 ? 'Linux' : '其他'
        }}
        {{ loginInfo?.deviceInfo?.osVersion }}
      </a-descriptions-item>
      <a-descriptions-item label="用户代理">
        {{ loginInfo?.userAgent }}
      </a-descriptions-item>
      <a-descriptions-item label="消息" :span="2">
        {{ loginInfo?.message }}
      </a-descriptions-item>
      <a-descriptions-item label="登录时间" :span="2">
        {{ loginInfo?.createTime }}
      </a-descriptions-item>
    </a-descriptions>
  </Takt-modal>
</template>

<script lang="ts" setup>
import { ref, defineProps, defineExpose, watch } from 'vue'
import type { TaktLoginLogDto } from '@/types/audit/loginLog'

const props = defineProps<{
  loginInfo?: TaktLoginLogDto
}>()

const visible = ref(false)

/** 获取登录类型名称 */
const getLoginTypeName = (type?: number) => {
  const types: { [key: number]: string } = {
    0: '账号密码',
    1: '手机验证码',
    2: '邮箱验证码',
    3: '第三方登录'
  }
  return types[type || 0] || '未知'
}

/** 获取浏览器类型名称 */
const getBrowserTypeName = (type?: number) => {
  const types: { [key: number]: string } = {
    0: 'Chrome',
    1: 'Firefox',
    2: 'Edge',
    3: 'Safari',
    4: 'IE',
    5: '其他'
  }
  return types[type || 5] || '未知'
}

/** 获取操作系统类型名称 */
const getOsTypeName = (type?: number) => {
  const types: { [key: number]: string } = {
    0: 'Windows',
    1: 'Android',
    2: 'iOS',
    3: 'MacOS',
    4: 'Linux',
    5: '其他'
  }
  return types[type || 5] || '未知'
}

/** 打开弹窗 */
const open = () => {
  visible.value = true
}

/** 关闭弹窗 */
const close = () => {
  visible.value = false
}

// 暴露方法给父组件
defineExpose({
  open,
  close
})
</script> 
