import { defineStore } from 'pinia'
import { ref } from 'vue'
import { theme } from 'ant-design-vue'
import { useSettingsStore } from './settingsStore'

interface ThemeState {
  isDarkMode: boolean
  primaryColor: string
}

export const useThemeStore = defineStore('theme', () => {
  const isDarkMode = ref(false)
  const primaryColor = ref(theme.defaultConfig.token?.colorPrimary)

  const initTheme = () => {
    const savedTheme = localStorage.getItem('theme')
    if (savedTheme === 'dark') {
      isDarkMode.value = true
    }
    
    // 优先从settingsStore读取主色调设置
    try {
      const settingsStore = useSettingsStore()
      if (settingsStore.currentPrimaryColor) {
        primaryColor.value = settingsStore.currentPrimaryColor
      } else {
        // 如果settingsStore中没有，则从localStorage读取
        const savedColor = localStorage.getItem('primaryColor')
        if (savedColor) {
          primaryColor.value = savedColor
        }
      }
    } catch (error) {
      // 如果settingsStore还未初始化，则从localStorage读取
      const savedColor = localStorage.getItem('primaryColor')
      if (savedColor) {
        primaryColor.value = savedColor
      }
    }
  }

  const toggleTheme = () => {
    isDarkMode.value = !isDarkMode.value
    localStorage.setItem('theme', isDarkMode.value ? 'dark' : 'light')
  }

  const updateTheme = (options: Partial<ThemeState>) => {
    if (options.isDarkMode !== undefined) {
      isDarkMode.value = options.isDarkMode
      localStorage.setItem('theme', options.isDarkMode ? 'dark' : 'light')
    }
    if (options.primaryColor) {
      primaryColor.value = options.primaryColor
      localStorage.setItem('primaryColor', options.primaryColor)
    }
  }

  // 同步settingsStore中的主色调设置
  const syncPrimaryColorFromConfig = () => {
    try {
      const settingsStore = useSettingsStore()
      if (settingsStore.currentPrimaryColor && settingsStore.currentPrimaryColor !== primaryColor.value) {
        primaryColor.value = settingsStore.currentPrimaryColor
        localStorage.setItem('primaryColor', settingsStore.currentPrimaryColor)
      }
    } catch (error) {
      console.warn('无法同步settingsStore中的主色调设置:', error)
    }
  }

  return {
    isDarkMode,
    primaryColor,
    initTheme,
    toggleTheme,
    updateTheme,
    syncPrimaryColorFromConfig
  }
})