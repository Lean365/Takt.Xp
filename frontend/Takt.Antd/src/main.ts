//===================================================================
// 项目名 : Lean.Takt
// 文件名 : main.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 应用程序入口文件
//===================================================================

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import Antd from 'ant-design-vue'
import FcDesigner from '@form-create/antd-designer'
import 'ant-design-vue/dist/reset.css'
import '@/assets/styles/components/icon.less'

// 全局引入 Cropper 组件
import CropperElement from '@cropper/element'
import CropperCanvas from '@cropper/element-canvas'
import CropperImage from '@cropper/element-image'
import CropperShade from '@cropper/element-shade'
import CropperHandle from '@cropper/element-handle'
import CropperSelection from '@cropper/element-selection'
import CropperGrid from '@cropper/element-grid'
import CropperCrosshair from '@cropper/element-crosshair'
import CropperViewer from '@cropper/element-viewer'

// 注册 Cropper Web Components
if (!customElements.get('cropper-element')) {
  customElements.define('cropper-element', CropperElement)
}
if (!customElements.get('cropper-canvas')) {
  customElements.define('cropper-canvas', CropperCanvas)
}
if (!customElements.get('cropper-image')) {
  customElements.define('cropper-image', CropperImage)
}
if (!customElements.get('cropper-shade')) {
  customElements.define('cropper-shade', CropperShade)
}
if (!customElements.get('cropper-handle')) {
  customElements.define('cropper-handle', CropperHandle)
}
if (!customElements.get('cropper-selection')) {
  customElements.define('cropper-selection', CropperSelection)
}
if (!customElements.get('cropper-grid')) {
  customElements.define('cropper-grid', CropperGrid)
}
if (!customElements.get('cropper-crosshair')) {
  customElements.define('cropper-crosshair', CropperCrosshair)
}
if (!customElements.get('cropper-viewer')) {
  customElements.define('cropper-viewer', CropperViewer)
}

import App from './App.vue'
import router from './router'
import i18n from './locales'
import { useAppStore } from '@/stores/appStore'
import { useSettingsStore } from '@/stores/settingsStore'
import { setupPermission } from './directives/permission'
import { setupIconColor } from './directives/iconColor'
import setupIcons from '@/utils/iconsUtil'
import { useUserStore } from './stores/userStore'
import { useMenuStore } from './stores/menuStore'
import { getToken } from './utils/authUtil'
import { enUS } from 'date-fns/locale'

// 初始化函数
async function bootstrap() {
  try {
    const app = createApp(App)
    const pinia = createPinia()

    // 初始化状态管理
    app.use(pinia)
    
    // 初始化应用配置
    const appStore = useAppStore()
    const settingsStore = useSettingsStore()
    
    // 初始化应用设置（包括语言设置）
    await appStore.initialize()
    await settingsStore.initialize()
    
    // 注册 Ant Design Vue
    app.use(Antd)
    app.use(FcDesigner)
    
    // 配置form-create全局选项
    const formCreateOptions = {
      upload: {
        props: {
          onSuccess: (response: any, file: any, fileList: any) => {
            console.log('文件上传成功:', response, file)
            return response
          },
          onError: (error: any, file: any, fileList: any) => {
            console.error('文件上传失败:', error, file)
            return error
          }
        }
      }
    }
    
    app.use(FcDesigner.formCreate, formCreateOptions)

    // 设置图标
    setupIcons(app)

    // 注册国际化
    app.use(i18n)
    
    // 设置默认语言包
    app.config.globalProperties.$dateLocale = enUS

    // 注册权限指令
    setupPermission(app)
    
    // 注册图标颜色指令
    setupIconColor(app)

    // 如果有token，预加载用户信息和菜单
    const token = getToken()
    if (token) {
      try {
        // 获取 store 实例
        const userStore = useUserStore()
        const menuStore = useMenuStore()

        // 加载用户信息
        await userStore.getUserInfo()
        
        // 加载菜单并注册路由
        await menuStore.reloadMenus(router)
      } catch (error) {
        console.error('[应用] 预加载失败:', error)
      }
    }

    // 注册路由（确保在动态路由加载完成后再注册）
    app.use(router)

    // 挂载应用
    app.mount('#app')
  } catch (error) {
    console.error('[应用] 初始化失败:', error)
  }
}

// 启动应用
bootstrap()
