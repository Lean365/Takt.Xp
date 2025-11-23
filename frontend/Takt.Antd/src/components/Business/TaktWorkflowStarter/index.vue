<template>
  <div class="workflow-starter">
    <a-dropdown :trigger="['click']" placement="bottomRight">
      <a-button type="primary" :icon="PlayCircleOutlined">
        启动工作流
        <DownOutlined />
      </a-button>
      <template #overlay>
        <a-menu @click="handleMenuClick">
          <a-menu-item key="start">
            <template #icon>
              <PlayCircleOutlined />
            </template>
            启动工作流
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="leave">
            <template #icon>
              <CalendarOutlined />
            </template>
            请假申请
          </a-menu-item>
          <a-menu-item key="expense">
            <template #icon>
              <FileTextOutlined />
            </template>
            报销申请
          </a-menu-item>
          <a-menu-item key="purchase">
            <template #icon>
              <ShoppingCartOutlined />
            </template>
            采购申请
          </a-menu-item>
          <a-menu-item key="maintenance">
            <template #icon>
              <ToolOutlined />
            </template>
            设备维护
          </a-menu-item>
        </a-menu>
      </template>
    </a-dropdown>
  </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router'
import { 
  PlayCircleOutlined, 
  CalendarOutlined, 
  FileTextOutlined, 
  ShoppingCartOutlined, 
  ToolOutlined,
  DownOutlined 
} from '@ant-design/icons-vue'

const router = useRouter()

// 处理菜单点击
const handleMenuClick = (info: any) => {
  const key = String(info.key)
  switch (key) {
    case 'start':
      router.push('/workflow/manager/start')
      break
    case 'leave':
      router.push('/workflow/manager/start?type=leave')
      break
    case 'expense':
      router.push('/workflow/manager/start?type=expense')
      break
    case 'purchase':
      router.push('/workflow/manager/start?type=purchase')
      break
    case 'maintenance':
      router.push('/workflow/manager/start?type=maintenance')
      break
  }
}
</script>

<style lang="less" scoped>
.workflow-starter {
  display: inline-block;
}
</style> 