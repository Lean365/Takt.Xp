/// <reference types="vite/client" />
/// <reference types="ant-design-vue/typings/global" />

declare module '*.vue' {
  import type { DefineComponent } from 'vue'
  const component: DefineComponent<{}, {}, any>
  export default component
}

interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string
  readonly VITE_BASE_URL: string
  readonly VITE_APP_TITLE: string
  readonly VITE_APP_ENV: string
  readonly VITE_PORT: string
  readonly VITE_USE_MOCK: string
  readonly VITE_I18N: string
  readonly VITE_LOCALE: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
} 