<template>
  <a-button type="text" class="setting-button" @click="showDrawer = true">
    <template #icon>
      <skin-outlined />
    </template>
  </a-button>

  <a-drawer
    v-model:open="showDrawer"
    :title="t('header.settings.title')"
    placement="right"
    width="300"
    @close="onClose"
  >
    <div class="settings-content">
      <div class="settings-section">
        <h3>{{ t('settings.interface') }}</h3>
        
        <a-form layout="vertical">
          <!-- 主题模式 -->
          <a-form-item :label="t('settings.themeMode.label')">
            <a-radio-group v-model:value="settings.themeMode" button-style="solid" @change="handleThemeModeChange">
              <a-radio-button value="light">{{ t('settings.themeMode.light') }}</a-radio-button>
              <a-radio-button value="dark">{{ t('settings.themeMode.dark') }}</a-radio-button>
              <a-radio-button value="auto">{{ t('settings.themeMode.auto') }}</a-radio-button>
            </a-radio-group>
          </a-form-item>

          <a-form-item :label="t('settings.navMode.label')">
            <a-radio-group v-model:value="settings.navMode" button-style="solid" @change="handleNavModeChange">
              <a-radio-button value="side">{{ t('settings.navMode.side') }}</a-radio-button>
              <a-radio-button value="top">{{ t('settings.navMode.top') }}</a-radio-button>
              <a-radio-button value="mix">{{ t('settings.navMode.mix') }}</a-radio-button>
            </a-radio-group>
          </a-form-item>

          <a-form-item :label="t('settings.sidebarColor')">
            <div class="color-picker">
              <div 
                v-for="color in sidebarColors" 
                :key="color"
                class="color-block"
                :class="{ active: settings.sidebarColor === color }"
                :style="{ backgroundColor: color }"
                @click="handleSidebarColorChange(color)"
              ></div>
            </div>
          </a-form-item>

          <a-form-item :label="t('settings.primaryColor')">
            <div class="color-picker-wrapper" ref="colorPickerRef">
              <div 
                class="color-preview"
                :style="{ backgroundColor: tempColor }"
                @click="showColorPicker = true"
              ></div>
              <div v-show="showColorPicker" class="color-picker-container">
                <chrome-picker
                  v-model:modelValue="tempColor"
                  class="color-picker-panel"
                  @update:modelValue="handleColorChange"
                  :disableAlpha="true"
                  :swatches="themeColors"
                />
                <div class="color-picker-footer">
                  <a-space>
                    <a-button size="small" @click="cancelColorPick">取消</a-button>
                    <a-button size="small" type="primary" @click="confirmColorPick">确定</a-button>
                  </a-space>
                </div>
              </div>
            </div>
          </a-form-item>

          <a-divider />

          <a-form-item class="setting-item">
            <span>{{ t('settings.showTabs') }}</span>
            <a-switch v-model:checked="settings.showTabs" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showFooter') }}</span>
            <a-switch v-model:checked="settings.showFooter" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showWatermark') }}</span>
            <a-switch v-model:checked="settings.showWatermark" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showLogo') }}</span>
            <a-switch v-model:checked="settings.showLogo" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.animateTitle') }}</span>
            <a-switch v-model:checked="settings.animateTitle" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.keepTabs') }}</span>
            <a-switch v-model:checked="settings.keepTabs" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showTabIcon') }}</span>
            <a-switch v-model:checked="settings.showTabIcon" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.autoHideHeader') }}</span>
            <a-switch v-model:checked="settings.autoHideHeader" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.fixedHeader') }}</span>
            <a-switch v-model:checked="settings.fixedHeader" @change="handleSettingChange" />
          </a-form-item>

          <a-form-item class="setting-item">
            <span>{{ t('settings.showBreadcrumb') }}</span>
            <a-switch v-model:checked="settings.showBreadcrumb" @change="handleSettingChange" />
          </a-form-item>
        </a-form>
      </div>

      <div class="settings-footer">
        <a-space>
          <a-button @click="previewSettings">
            <template #icon><EyeOutlined /></template>
            {{ t('settings.preview') }}
          </a-button>
          <a-button @click="resetSettings">
            <template #icon><ReloadOutlined /></template>
            {{ t('settings.reset') }}
          </a-button>
          <a-button type="primary" @click="saveSettings">
            <template #icon><SaveOutlined /></template>
            {{ t('settings.save') }}
          </a-button>
        </a-space>
      </div>
    </div>
  </a-drawer>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, onUnmounted, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { SkinOutlined, ReloadOutlined, SaveOutlined, EyeOutlined } from '@ant-design/icons-vue'
import { Chrome as ChromePicker } from '@ckpack/vue-color'
import { useSettingsStore } from '@/stores/settingsStore'
import { useThemeStore } from '@/stores/themeStore'
import { THEME_COLORS, SIDEBAR_COLORS, getDefaultAppConfig, getDefaultUISettings } from '@/setting'

const { t } = useI18n()
const settingsStore = useSettingsStore()
const themeStore = useThemeStore()

// 主题相关计算属性
const isDarkMode = computed(() => themeStore.isDarkMode)
const currentTheme = computed(() => themeStore.isDarkMode ? 'dark' : 'light')

const showDrawer = ref(false)
const colorPickerRef = ref<HTMLElement | null>(null)
const showColorPicker = ref(false)
const settings = reactive({
  themeMode: 'light' as 'light' | 'dark' | 'auto',
  navMode: 'side' as 'side' | 'top' | 'mix',
  fixedHeader: true,
  showBreadcrumb: true,
  showTabs: true,
  showFooter: true,
  autoHideHeader: false,
  sidebarColor: '#001529',
  primaryColor: '#1890ff',
  showWatermark: false,
  showLogo: true,
  animateTitle: false,
  keepTabs: false,
  showTabIcon: true
})

// 使用从setting.ts导入的颜色预设
const themeColors = THEME_COLORS
const sidebarColors = SIDEBAR_COLORS

const tempColor = ref(settings.primaryColor)

// 处理点击外部关闭颜色选择器
const handleClickOutside = (event: MouseEvent) => {
  if (colorPickerRef.value && !colorPickerRef.value.contains(event.target as Node)) {
    showColorPicker.value = false
  }
}

onMounted(() => {
  document.addEventListener('click', handleClickOutside)
  
  // 从settingsStore读取当前配置到组件
  const currentSettings = settingsStore.getUISettings()
  Object.assign(settings, currentSettings)
  
  // 设置临时颜色
  tempColor.value = settings.primaryColor
  
  // 应用初始设置
  applySettingsRealtime()
  
  // 设置系统主题监听器
  setupSystemThemeListener()
  
  // 监听Store中的UI设置变化，同步到组件
  watch(() => settingsStore.uiSettings, (newSettings) => {
    Object.assign(settings, newSettings)
  }, { deep: true })
})

onUnmounted(() => {
  document.removeEventListener('click', handleClickOutside)
  
  // 清理防抖定时器
  if (debounceTimer) {
    clearTimeout(debounceTimer)
  }
})

const onClose = () => {
  showDrawer.value = false
}

const resetSettings = () => {
  // 重置到浅色主题的默认配置（而不是当前主题的默认配置）
  const defaultSettings = getDefaultUISettings(false) // 强制使用浅色主题
  Object.assign(settings, defaultSettings)
  
  // 确保重置为浅色主题模式
  settings.themeMode = 'light'
  
  // 重置临时颜色
  tempColor.value = settings.primaryColor
  
  // 应用重置的设置到界面
  applySettingsRealtime()
  
  // 显示重置提示
  message.success('设置已重置到默认值')
  
  console.log('[TaktSetting] 设置已重置到默认值（浅色主题）')
}

const saveSettings = () => {
  // 验证设置
  if (!validateSettings()) {
    return
  }
  
  console.log('[TaktSetting] 开始保存设置...')
  
  // 应用设置到界面并保存到settingsStore
  applySettingsRealtime()
  
  // 显示保存成功提示
  message.success('设置保存成功')
  
  // 关闭抽屉
  showDrawer.value = false
  
  console.log('[TaktSetting] 设置保存完成')
}

const handleColorChange = (color: any) => {
  const hexColor = color.hex
  tempColor.value = hexColor
  settings.primaryColor = hexColor
  themeStore.updateTheme({ primaryColor: hexColor })
  // 同步到settingsStore
  settingsStore.setPrimaryColor(hexColor)
}

// 侧边栏颜色变更处理
const handleSidebarColorChange = (color: string) => {
  settings.sidebarColor = color
  // 实时应用侧边栏颜色
  settingsStore.setSidebarColor(color)
}

const cancelColorPick = () => {
  tempColor.value = settings.primaryColor
  showColorPicker.value = false
}

const confirmColorPick = () => {
  showColorPicker.value = false
}

// 主题模式切换处理
const handleThemeModeChange = (e: any) => {
  const value = e.target.value as 'light' | 'dark' | 'auto'
  settings.themeMode = value
  if (value === 'auto') {
    // 自动模式：根据系统主题切换
    const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches
    themeStore.updateTheme({ isDarkMode: prefersDark })
    // 更新配置以匹配主题模式
    settingsStore.updateConfigForTheme(prefersDark)
  } else {
    // 手动模式：直接设置主题
    const isDarkMode = value === 'dark'
    themeStore.updateTheme({ isDarkMode })
    // 更新配置以匹配主题模式
    settingsStore.updateConfigForTheme(isDarkMode)
  }
}

// 导航模式切换处理
const handleNavModeChange = (e: any) => {
  const value = e.target.value as 'side' | 'top' | 'mix'
  settings.navMode = value
  // 实时应用导航模式设置
  settingsStore.setNavMode(value)
}

// 实时应用设置到界面（保存到settingsStore）
const applySettingsRealtime = () => {
  // 应用主题设置
  let isDarkMode = false
  if (settings.themeMode === 'auto') {
    isDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches
    themeStore.updateTheme({ isDarkMode })
  } else {
    isDarkMode = settings.themeMode === 'dark'
    themeStore.updateTheme({ isDarkMode })
  }

  // 应用主色调设置
  themeStore.updateTheme({ primaryColor: settings.primaryColor })

  // 应用UI设置到settingsStore（会保存到localStorage）
  settingsStore.batchUpdateUISettings({
    themeMode: settings.themeMode,
    navMode: settings.navMode,
    showBreadcrumb: settings.showBreadcrumb,
    showTabs: settings.showTabs,
    showFooter: settings.showFooter,
    sidebarColor: settings.sidebarColor,
    primaryColor: settings.primaryColor,
    showWatermark: settings.showWatermark,
    showLogo: settings.showLogo,
    animateTitle: settings.animateTitle,
    keepTabs: settings.keepTabs,
    showTabIcon: settings.showTabIcon
  })

  // 更新配置以匹配主题模式
  settingsStore.updateConfigForTheme(isDarkMode)

  // 同步主色调到themeStore
  themeStore.syncPrimaryColorFromConfig()
}

// 防抖定时器
let debounceTimer: NodeJS.Timeout | null = null

// 设置变更处理
const handleSettingChange = () => {
  // 清除之前的定时器
  if (debounceTimer) {
    clearTimeout(debounceTimer)
  }
  
  // 设置新的定时器，延迟应用设置
  debounceTimer = setTimeout(() => {
    applySettingsRealtime()
  }, 300)
}

// 导出设置功能已移除

// 导入导出功能已移除

// 预览设置（临时应用，不保存到settingsStore）
const previewSettings = () => {
  // 验证设置
  if (!validateSettings()) {
    return
  }
  
  // 临时应用主题设置
  let isDarkMode = false
  if (settings.themeMode === 'auto') {
    isDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches
    themeStore.updateTheme({ isDarkMode })
  } else {
    isDarkMode = settings.themeMode === 'dark'
    themeStore.updateTheme({ isDarkMode })
  }

  // 临时应用主色调设置
  themeStore.updateTheme({ primaryColor: settings.primaryColor })

  // 临时应用UI设置到settingsStore（不保存到localStorage）
  // 直接修改uiSettings.value，不调用saveUISettings
  Object.assign(settingsStore.uiSettings, {
    themeMode: settings.themeMode,
    navMode: settings.navMode,
    fixedHeader: settings.fixedHeader,
    showBreadcrumb: settings.showBreadcrumb,
    showTabs: settings.showTabs,
    showFooter: settings.showFooter,
    autoHideHeader: settings.autoHideHeader,
    sidebarColor: settings.sidebarColor,
    primaryColor: settings.primaryColor,
    showWatermark: settings.showWatermark,
    showLogo: settings.showLogo,
    animateTitle: settings.animateTitle,
    keepTabs: settings.keepTabs,
    showTabIcon: settings.showTabIcon
  })

  // 临时更新配置以匹配主题模式（不保存到localStorage）
  const currentConfig = { ...settingsStore.config }
  const newConfig = getDefaultAppConfig(isDarkMode)
  const newUISettings = getDefaultUISettings(isDarkMode)
  
  currentConfig.theme.primaryColor = newConfig.theme.primaryColor
  Object.assign(settingsStore.config, currentConfig)
  
  // 临时更新UI设置中的侧边栏颜色和主色调
  settingsStore.uiSettings.sidebarColor = newUISettings.sidebarColor
  settingsStore.uiSettings.primaryColor = newUISettings.primaryColor
  
  // 显示预览提示
  message.success('设置预览已应用，点击保存按钮可永久保存')
}

// 验证设置
const validateSettings = (): boolean => {
  // 验证主题模式
  if (!['light', 'dark', 'auto'].includes(settings.themeMode)) {
    console.error('无效的主题模式')
    return false
  }
  
  // 验证导航模式
  if (!['side', 'top', 'mix'].includes(settings.navMode)) {
    console.error('无效的导航模式')
    return false
  }
  
  // 验证颜色值
  const colorRegex = /^#[0-9A-F]{6}$/i
  if (!colorRegex.test(settings.primaryColor) || !colorRegex.test(settings.sidebarColor)) {
    console.error('无效的颜色值')
    return false
  }
  
  return true
}

// 监听系统主题变化
const setupSystemThemeListener = () => {
  const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)')
  const handleSystemThemeChange = (e: MediaQueryListEvent) => {
    if (settings.themeMode === 'auto') {
      themeStore.updateTheme({ isDarkMode: e.matches })
    }
  }
  
  mediaQuery.addEventListener('change', handleSystemThemeChange)
  
  // 清理监听器
  onUnmounted(() => {
    mediaQuery.removeEventListener('change', handleSystemThemeChange)
  })
}
</script>

<style lang="less" scoped>
.settings-dropdown {
  display: flex;
  align-items: center;
  justify-content: center;
}

:deep(.ant-dropdown-trigger) {
  display: flex;
  align-items: center;
}

:deep(.anticon) {
  font-size: 16px;
  line-height: 1;
}

.settings-content {
  display: flex;
  flex-direction: column;
  height: 100%;

  .settings-section {
    flex: 1;
    padding: 12px 24px;

    h3 {
      margin-bottom: 12px;
      color: var(--ant-color-text);
      font-weight: 500;
      font-size: 15px;
    }

    :deep(.ant-form-item) {
      margin-bottom: 4px;

      .ant-form-item-label {
        padding-bottom: 4px;
        
        label {
          color: var(--ant-color-text);
        }
      }

      &.setting-item {
        margin: 0;
        padding: 8px 0;
        border-bottom: 1px solid var(--ant-border-color-split);

        &:last-child {
          border-bottom: none;
        }
      }
    }

    .ant-divider {
      margin: 12px 0;
      border-color: var(--ant-border-color-split);
    }
  }

  .settings-footer {
    padding: 16px 24px;
    text-align: right;
    border-top: 1px solid var(--ant-border-color-split);
    background: var(--ant-color-bg-container);

    :deep(.ant-btn) {
      display: inline-flex;
      align-items: center;
      height: 32px;
      padding: 4px 15px;
    }
  }
}

.color-picker {
  display: grid;
  grid-template-columns: repeat(7, 24px);
  gap: 6px;
  margin-top: 4px;

  .color-block {
    width: 24px;
    height: 24px;
    border-radius: 2px;
    cursor: pointer;
    border: 2px solid transparent;
    transition: all 0.2s ease-in-out;
    position: relative;
    box-sizing: border-box;

    &:hover {
      transform: scale(1.1);
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    }

    &.active {
      border-color: #fff;
      box-shadow: 0 0 0 2px var(--ant-color-primary);
      transform: scale(1.1);

      &::after {
        content: '';
        position: absolute;
        width: 6px;
        height: 6px;
        border-radius: 50%;
        background: #fff;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        box-shadow: 0 0 2px rgba(0, 0, 0, 0.3);
      }
    }
  }
}

.ant-divider {
  margin: 16px 0;
  border-color: var(--ant-border-color-split);
}

.setting-item {
  position: relative;
  padding: 10px 24px;
  border-bottom: 1px solid var(--ant-border-color-split);

  &:last-child {
    border-bottom: none;
  }

  span {
    color: var(--ant-color-text);
    font-size: 14px;
  }

  :deep(.ant-switch) {
    position: absolute;
    right: 24px;
    top: 50%;
    transform: translateY(-50%);
  }
}

.color-picker-wrapper {
  position: relative;
  
  .color-preview {
    width: 100%;
    height: 32px;
    border-radius: 6px;
    border: 1px solid var(--ant-border-color);
    cursor: pointer;
    transition: all 0.3s;

    &:hover {
      border-color: var(--ant-color-primary);
    }
  }

  .color-picker-container {
    position: absolute;
    top: 100%;
    left: 0;
    margin-top: 8px;
    z-index: 1000;
    background: var(--ant-color-bg-container);
    border-radius: 8px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    border: 1px solid var(--ant-border-color-split);
    
    .color-picker-panel {
      padding: 8px;
    }

    .color-picker-footer {
      padding: 8px;
      text-align: right;
      border-top: 1px solid var(--ant-border-color-split);
    }
  }
}

// 暗色主题适配
[data-theme='dark'] {
  .settings-content {
    .settings-section {
      h3 {
        color: var(--ant-color-text);
      }
    }
  }

  .setting-item {
    span {
      color: var(--ant-color-text);
    }
  }

  .color-picker-container {
    background: var(--ant-color-bg-container);
    border-color: var(--ant-border-color-split);
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.3);
  }

  .color-picker-footer {
    border-top-color: var(--ant-border-color-split);
  }

  .color-block {
    &:hover {
      box-shadow: 0 2px 8px rgba(255, 255, 255, 0.1);
    }

    &.active {
      &::after {
        box-shadow: 0 0 2px rgba(255, 255, 255, 0.5);
      }
    }
  }

  // 暗色主题下的抽屉样式
  :deep(.ant-drawer) {
    .ant-drawer-content {
      background: var(--ant-color-bg-container);
    }

    .ant-drawer-header {
      background: var(--ant-color-bg-container);
      border-bottom-color: var(--ant-border-color-split);

      .ant-drawer-title {
        color: var(--ant-color-text);
      }
    }

    .ant-drawer-body {
      background: var(--ant-color-bg-container);
    }
  }

  // 暗色主题下的表单样式
  :deep(.ant-form-item-label > label) {
    color: var(--ant-color-text);
  }
}

// 亮色主题适配
[data-theme='light'] {
  .settings-content {
    .settings-section {
      h3 {
        color: var(--ant-color-text);
      }
    }
  }

  .setting-item {
    span {
      color: var(--ant-color-text);
    }
  }

  .color-picker-container {
    background: var(--ant-color-bg-container);
    border-color: var(--ant-border-color-split);
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
  }

  .color-picker-footer {
    border-top-color: var(--ant-border-color-split);
  }

  // 亮色主题下的抽屉样式
  :deep(.ant-drawer) {
    .ant-drawer-content {
      background: var(--ant-color-bg-container);
    }

    .ant-drawer-header {
      background: var(--ant-color-bg-container);
      border-bottom-color: var(--ant-border-color-split);

      .ant-drawer-title {
        color: var(--ant-color-text);
      }
    }

    .ant-drawer-body {
      background: var(--ant-color-bg-container);
    }
  }

  // 亮色主题下的表单样式
  :deep(.ant-form-item-label > label) {
    color: var(--ant-color-text);
  }
}

// 全局主题适配
:deep(.ant-drawer) {
  .ant-drawer-content {
    background: var(--ant-color-bg-container);
  }

  .ant-drawer-header {
    background: var(--ant-color-bg-container);
    border-bottom-color: var(--ant-border-color-split);

    .ant-drawer-title {
      color: var(--ant-color-text);
    }
  }

  .ant-drawer-body {
    background: var(--ant-color-bg-container);
  }
}

:deep(.ant-form-item-label > label) {
  color: var(--ant-color-text);
}
</style> 

