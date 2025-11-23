<template>
  <div>
    <a-dropdown :trigger="['hover']" placement="bottom" class="user-dropdown">
      <div class="user-info">
        <a-avatar :size="32" :src="avatarUrl">
          <template #icon>
            <component :is="userIcon" />
          </template>
        </a-avatar>
      </div>
      <template #overlay>
        <a-menu class="user-menu">
          <a-menu-item key="profile" @click="handleProfile">
            <template #icon><user-outlined /></template>
            <span>{{ t('header.user.profile') }}</span>
          </a-menu-item>
          <a-menu-item key="change-password" @click="handleChangePassword">
            <template #icon><key-outlined /></template>
            <span>{{ t('header.user.changePassword') }}</span>
          </a-menu-item>
          <a-menu-item key="clear-cache" @click="handleClearCache">
            <template #icon><clear-outlined /></template>
            <span>{{ t('header.user.clearCache') }}</span>
          </a-menu-item>
          <a-menu-item v-if="isDev" key="auto-logout-test" @click="() => { showAutoLogoutTest = true; console.log('点击菜单，showAutoLogoutTest', showAutoLogoutTest) }">
            <template #icon><key-outlined /></template>
            <span>自动登出测试</span>
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="logout" @click="handleLogout">
            <template #icon><logout-outlined /></template>
            <span>{{ t('header.user.logout') }}</span>
          </a-menu-item>
        </a-menu>
      </template>
    </a-dropdown>
    
    <!-- 将抽屉组件放在外部，确保能正确渲染 -->
    <TaktAutoLogoutTest v-if="isDev" :visible="showAutoLogoutTest" @close="() => { showAutoLogoutTest = false; console.log('关闭弹窗', showAutoLogoutTest) }" />
  </div>
</template>

<script lang="ts" setup>
import { computed, ref } from 'vue'
import { useI18n } from 'vue-i18n'
import { useRouter } from 'vue-router'
import { Modal, message } from 'ant-design-vue'
import {
  UserOutlined,
  LogoutOutlined,
  ClearOutlined,
  KeyOutlined
} from '@ant-design/icons-vue'
import { useUserStore } from '@/stores/userStore'


const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// 仅在开发环境显示
const isDev = computed(() => import.meta.env.DEV)
// 控制自动登出测试抽屉显示
const showAutoLogoutTest = ref(false)

// 计算头像URL
const avatarUrl = computed(() => {
  const avatar = userStore.userInfo?.avatar
  if (!avatar) return ''
  // 如果是相对路径，添加基础URL
  if (avatar.startsWith('/')) {
    const fullUrl = import.meta.env.VITE_API_BASE_URL + avatar
    console.log('[TaktHeaderUser] 头像URL构建:', {
      原始路径: avatar,
      API基础URL: import.meta.env.VITE_API_BASE_URL,
      完整URL: fullUrl
    })
    return fullUrl
  }
  return avatar
})

// 计算用户图标
const userIcon = computed(() => {
  console.log(userStore.userInfo)
  // 如果有头像，不显示图标
  if (userStore.userInfo?.avatar) {
    return null
  }
  // 否则使用默认图标
  return UserOutlined
})

// 处理个人信息
const handleProfile = () => {
  router.push('/identity/user/profile')
}

// 处理修改密码
const handleChangePassword = () => {
  router.push('/identity/user/change-password')
}

// 处理清除缓存
const handleClearCache = () => {
  Modal.confirm({
    title: t('header.user.clearCache'),
    content: t('header.user.clearCacheConfirm'),
    okText: t('common.button.confirm'),
    cancelText: t('common.button.cancel'),
    onOk: async () => {
      try {
        // 清除路由缓存
        router.getRoutes().forEach(route => {
          if (route.meta?.keepAlive) {
            const el = document.querySelector(`[data-key="${route.path}"]`)
            if (el) {
              const vueEl = el as unknown as { __vueParentComponent: any }
              if (vueEl?.__vueParentComponent?.ctx) {
                const vnode = vueEl.__vueParentComponent
                vnode.ctx?.deactivated?.()
                vnode.ctx?.activated?.()
              }
            }
          }
        })
        
        // 清除本地存储
        localStorage.clear()
        sessionStorage.clear()
        
        message.success(t('header.user.clearCacheSuccess'))
      } catch (error) {
        console.error('清除缓存失败:', error)
        message.error(t('header.user.clearCacheFailed'))
      }
    }
  })
}

// 处理退出登录
const handleLogout = async () => {
  try {
    await userStore.logout()
    // 跳转到登录页
    router.push('/login')
  } catch (error) {
    console.error('退出登录失败:', error)
    message.error(t('header.user.logoutFailed'))
  }
}
</script>

<style lang="less" scoped>
.user-dropdown {
  margin-left: 8px;
  cursor: pointer;

  .user-info {
    display: flex;
    align-items: center;
    padding: 0 8px;
  }
}

:deep(.user-menu) {
  min-width: 160px;
  
  .ant-dropdown-menu-item {
    display: flex;
    align-items: center;
    padding: 8px 16px;
    
    .anticon {
      font-size: 16px;
      margin-right: 8px;
    }
  }
  
  .ant-dropdown-menu-item-divider {
    margin: 4px 0;
  }
}
</style> 
