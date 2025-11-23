<template>
  <a-modal
    :open="true"
    title="设备日志详情"
    width="800px"
    :footer="null"
    @cancel="$emit('close')"
  >
    <a-descriptions :column="2" bordered>
      <a-descriptions-item label="ID" span="1">
        {{ deviceLog?.deviceLogId }}
      </a-descriptions-item>
      <a-descriptions-item label="用户ID" span="1">
        {{ deviceLog?.userId }}
      </a-descriptions-item>
      <a-descriptions-item label="设备标识" span="2">
        {{ deviceLog?.deviceId }}
      </a-descriptions-item>
      <a-descriptions-item label="设备类型" span="1">
        <a-tag :color="getDeviceTypeColor(deviceLog?.deviceType)">
          {{ getDeviceTypeText(deviceLog?.deviceType) }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="设备令牌" span="1">
        {{ deviceLog?.deviceToken }}
      </a-descriptions-item>
      <a-descriptions-item label="提供者" span="1">
        {{ deviceLog?.providerDisplayName }}
      </a-descriptions-item>
      <a-descriptions-item label="网络类型" span="1">
        {{ deviceLog?.networkType }}
      </a-descriptions-item>
      <a-descriptions-item label="时区" span="1">
        {{ deviceLog?.timeZone }}
      </a-descriptions-item>
      <a-descriptions-item label="语言" span="1">
        {{ deviceLog?.language }}
      </a-descriptions-item>
      <a-descriptions-item label="VPN" span="1">
        <a-tag :color="deviceLog?.isVpn === 1 ? 'orange' : 'default'">
          {{ deviceLog?.isVpn === 1 ? 'VPN' : '直连' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="代理" span="1">
        <a-tag :color="deviceLog?.isProxy === 1 ? 'orange' : 'default'">
          {{ deviceLog?.isProxy === 1 ? '代理' : '直连' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="登录类型" span="1">
        <Takt-dict-tag dict-type="audit_login_type" :value="deviceLog?.loginType" />
      </a-descriptions-item>
      <a-descriptions-item label="登录来源" span="1">
        <Takt-dict-tag dict-type="audit_login_source" :value="deviceLog?.loginSource" />
      </a-descriptions-item>
      <a-descriptions-item label="登录提供者" span="2">
        {{ deviceLog?.loginProvider }}
      </a-descriptions-item>
      <a-descriptions-item label="首次登录时间" span="1">
        {{ formatDateTime(deviceLog?.firstLoginTime) }}
      </a-descriptions-item>
      <a-descriptions-item label="首次登录IP" span="1">
        {{ deviceLog?.firstLoginIp }}
      </a-descriptions-item>
      <a-descriptions-item label="首次登录地点" span="2">
        {{ deviceLog?.firstLoginLocation }}
      </a-descriptions-item>
      <a-descriptions-item label="最后登录时间" span="1">
        {{ formatDateTime(deviceLog?.lastLoginTime) }}
      </a-descriptions-item>
      <a-descriptions-item label="最后登录IP" span="1">
        {{ deviceLog?.lastLoginIp }}
      </a-descriptions-item>
      <a-descriptions-item label="最后登录地点" span="2">
        {{ deviceLog?.lastLoginLocation }}
      </a-descriptions-item>
      <a-descriptions-item label="最后在线时间" span="1">
        {{ formatDateTime(deviceLog?.lastOnlineTime) }}
      </a-descriptions-item>
      <a-descriptions-item label="最后离线时间" span="1">
        {{ formatDateTime(deviceLog?.lastOfflineTime) }}
      </a-descriptions-item>
      <a-descriptions-item label="登录次数" span="1">
        {{ deviceLog?.loginCount }}
      </a-descriptions-item>
      <a-descriptions-item label="连续登录天数" span="1">
        {{ deviceLog?.continuousLoginDays }}
      </a-descriptions-item>
      <a-descriptions-item label="状态" span="1">
        <a-tag :color="deviceLog?.deviceStatus === 0 ? 'success' : 'error'">
          {{ deviceLog?.deviceStatus === 0 ? '正常' : '停用' }}
        </a-tag>
      </a-descriptions-item>
      <a-descriptions-item label="备注" span="2">
        {{ deviceLog?.remark || '无' }}
      </a-descriptions-item>
      <a-descriptions-item label="创建人" span="1">
        {{ deviceLog?.createBy }}
      </a-descriptions-item>
      <a-descriptions-item label="创建时间" span="1">
        {{ formatDateTime(deviceLog?.createTime) }}
      </a-descriptions-item>
      <a-descriptions-item label="更新人" span="1">
        {{ deviceLog?.updateBy }}
      </a-descriptions-item>
      <a-descriptions-item label="更新时间" span="1">
        {{ formatDateTime(deviceLog?.updateTime) }}
      </a-descriptions-item>
    </a-descriptions>

    <div class="modal-footer">
      <a-button @click="$emit('close')">关闭</a-button>
    </div>
  </a-modal>
</template>

<script setup lang="ts">
import type { TaktDeviceLog } from '@/types/audit/deviceLog'

interface Props {
  deviceLog: TaktDeviceLog | null
}

interface Emits {
  (e: 'close'): void
}

defineProps<Props>()
defineEmits<Emits>()

// 格式化日期时间
const formatDateTime = (dateTime: string | null | undefined) => {
  if (!dateTime) return '无'
  return new Date(dateTime).toLocaleString()
}

// 获取设备类型颜色
const getDeviceTypeColor = (deviceType: string | undefined) => {
  switch (deviceType) {
    case 'Desktop': return 'blue'
    case 'Mobile': return 'green'
    case 'Tablet': return 'orange'
    default: return 'default'
  }
}

// 获取设备类型文本
const getDeviceTypeText = (deviceType: string | undefined) => {
  switch (deviceType) {
    case 'Desktop': return 'PC'
    case 'Mobile': return '移动设备'
    case 'Tablet': return '平板'
    default: return deviceType || '未知'
  }
}
</script>

<style lang="less" scoped>
.modal-footer {
  margin-top: 24px;
  text-align: right;
}
</style>

