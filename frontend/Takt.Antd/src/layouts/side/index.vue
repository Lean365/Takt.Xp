<template>
  <a-layout class="side-layout">
    <!-- 左侧菜单 -->
    <a-layout-sider 
      class="sider"
      :width="siderWidth"
      :theme="theme"
      :collapsed-width="collapsedSiderWidth"
      :breakpoint="'lg'"
      v-model:collapsed="collapsed"
      :trigger="null"
      :style="siderStyle"
    >
      <Takt-logo :collapsed="collapsed" />
      <Takt-sider-menu />
    </a-layout-sider>

    <!-- 右侧内容区 -->
    <a-layout :class="['main-container', { collapsed }]">
      <!-- 头部区域 -->
      <Takt-header-bar v-model:collapsed="collapsed" />

      <!-- 内容区域 -->
      <div class="content-wrapper">
        <!-- 面包屑导航 - 当显示标签页时隐藏面包屑 -->
        <Takt-breadcrumb v-if="!$route.meta.hideLayout && !settingsStore.isShowTabs" />

        <!-- 页面标签页 - 包含面包屑功能 -->
        <Takt-page-tabs v-if="!$route.meta.hideLayout && settingsStore.isShowTabs" />

        <!-- 主内容区 -->
        <a-layout-content class="content">
          <router-view v-slot="{ Component }">
            <transition name="fade" mode="out-in">
              <component :is="Component" />
            </transition>
          </router-view>
        </a-layout-content>

        <!-- 页脚 -->
        <Takt-footer-bar v-if="!$route.meta.hideLayout && settingsStore.isShowFooter" />
      </div>
    </a-layout>
  </a-layout>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useThemeStore } from '@/stores/themeStore'
import { useSettingsStore } from '@/stores/settingsStore'


// 组合式API和工具函数
const themeStore = useThemeStore()
const settingsStore = useSettingsStore()

// 响应式状态
const collapsed = ref(false)
const theme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

// 侧边栏宽度计算 - 根据菜单实际内容自动调整
const siderWidth = computed(() => {
  // 基础宽度240px，但需要根据菜单实际内容调整
  // 如果菜单左移了16px，那么实际需要的宽度就是240-16=224px
  return 224
})
const collapsedSiderWidth = computed(() => {
  // 折叠状态也需要相应调整
  return 32
})

// 侧边栏样式
const siderStyle = computed(() => {
  const sidebarColor = settingsStore.currentSidebarColor
  console.log('[SideLayout] siderStyle - 当前侧边栏颜色:', sidebarColor)
  return {
    backgroundColor: sidebarColor
  }
})
</script>

<style lang="less" scoped>
.side-layout {
  min-height: 100vh;
  background-color: var(--ant-color-bg-layout);

  .sider {
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    overflow: auto;
    z-index: 10;

    // 确保侧边栏背景色能够正确应用
    :deep(.ant-layout-sider-children) {
      background-color: inherit !important;
    }

    &::-webkit-scrollbar {
      width: 6px;
      height: 6px;
    }

    &::-webkit-scrollbar-thumb {
      border-radius: 3px;
      background: var(--ant-color-text-quaternary);
    }


  }

  .main-container {
    margin-left: 224px; // 240px - 16px (菜单左移的距离)
    transition: all 0.3s;
    background-color: var(--ant-color-bg-layout);

    &.collapsed {
      margin-left: 32px; // 48px - 16px (菜单左移的距离)
    }

    .content-wrapper {
      padding: 0;
      min-height: calc(100vh - 64px - 40px - 40px);
      margin-top: 64px;
      display: flex;
      flex-direction: column;
      background-color: var(--ant-color-bg-layout);

      .breadcrumb {
        padding: 16px 24px;
        margin-bottom: 0;
      }

      .content {
        flex: 1;
        padding: 24px;
        background-color: var(--ant-color-bg-layout);
      }
    }
  }
}

@keyframes fade-in {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

// 响应式布局
@media screen and (max-width: 768px) {
  .side-layout {
    .sider {
      position: absolute;
      transform: translateX(-100%);
      
      .collapsed & {
        transform: translateX(0);
      }
    }

    .main-container {
      margin-left: 0;

      .content-wrapper {
        padding: 0;
        
        .content {
          padding: 12px;
        }
      }
    }
  }
}

// 过渡动画
.fade-enter-active,
.fade-leave-active {
  transition: opacity var(--ant-motion-duration-fast) var(--ant-motion-ease-in-out);
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style> 
