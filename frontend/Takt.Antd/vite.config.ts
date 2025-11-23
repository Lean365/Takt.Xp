import { fileURLToPath, URL } from 'node:url'
import { defineConfig, loadEnv, ConfigEnv, UserConfig, Plugin } from 'vite'
import vue from '@vitejs/plugin-vue'
import AutoImport from 'unplugin-auto-import/vite'
import Components from 'unplugin-vue-components/vite'
import { AntDesignVueResolver } from 'unplugin-vue-components/resolvers'
import * as http from 'node:http'
import * as https from 'node:https'

// 定义支持的语言
const SUPPORTED_LOCALES = {
  'zh-CN': {
    title: '\n  极限节拍前端开发服务器启动\n',
    separator: '  ' + '='.repeat(50),
    backendNotRunning: '  ❌ 后端服务未启动',
    backendRunning: '  ✅ 后端服务已连接',
    backendUrl: (url: string) => `  📡 后端服务地址: ${url}`,
    startBackend: '  💡 请先启动后端服务',
    continueInfo: '  ℹ️ 前端服务继续运行中，但部分功能可能无法使用',
    readyInfo: '  ✨ 所有服务已就绪，可以开始使用了',
    localUrl: (url: string) => `  🌐 本地访问: ${url}`,
    networkUrl: (url: string) => `  🔗 网络访问: ${url}`,
    helpInfo: '  ❓ 按 h + enter 显示帮助信息\n'
  },
  'en-US': {
    title: '\n  Takt.Xp Frontend Dev Server Started\n',
    separator: '  ' + '='.repeat(50),
    backendNotRunning: '  ❌ Backend Service Not Running',
    backendRunning: '  ✅ Backend Service Connected',
    backendUrl: (url: string) => `  📡 Backend URL: ${url}`,
    startBackend: '  💡 Please Start Backend Service First',
    continueInfo: '  ℹ️ Frontend Service Continues, But Some Features May Not Work',
    readyInfo: '  ✨ All Services Ready, You Can Start Using Now',
    localUrl: (url: string) => `  🌐 Local: ${url}`,
    networkUrl: (url: string) => `  🔗 Network: ${url}`,
    helpInfo: '  ❓ Press h + enter for help\n'
  }
}

/**
 * 创建检测后端服务状态的 Vite 插件
 * @param proxyTarget - 后端服务地址
 * @param locale - 语言设置，默认中文
 * @returns Vite 插件对象
 */
function backendStatusPlugin(proxyTarget: string, locale: string = 'zh-CN'): Plugin {
  return {
    name: 'backend-status',
    configureServer(server) {
      // 只显示环境变量调试信息，不检查后端状态
    }
  }
}

// https://vitejs.dev/config/
export default defineConfig(({ mode }: ConfigEnv): UserConfig => {
  const env = loadEnv(mode, process.cwd())
  
  // 调试环境变量
  console.log('\n🔍 [Vite Config] 环境变量调试:')
  console.log('  - mode:', mode)
  console.log('  - cwd:', process.cwd())
  console.log('  - env.VITE_API_BASE_URL:', env.VITE_API_BASE_URL)
  console.log('  - env.VITE_PROXY_TARGET:', env.VITE_PROXY_TARGET)
  
  console.log('  - 直接使用环境变量')
  console.log('')
  
  
  // 获取系统语言设置或使用环境变量中的语言设置
  const locale = process.env.LOCALE || Intl.DateTimeFormat().resolvedOptions().locale
  
  return {
    plugins: [
      vue({
        template: {
          compilerOptions: {
            // 告诉 Vue 这些是自定义元素，不需要解析为 Vue 组件
            isCustomElement: (tag) => tag.startsWith('cropper-')
          }
        }
      }),
      backendStatusPlugin(env.VITE_PROXY_TARGET, locale),
      AutoImport({
        imports: [
          'vue',
          'vue-router',
          'vue-i18n',
          'pinia',
          '@vueuse/core',
          {
            'ant-design-vue': [
              'message',
              'Modal',
              'notification'
            ]
          }
        ],
        dts: 'src/auto-imports.d.ts',
        dirs: ['src/composables', 'src/stores'],
        vueTemplate: true,
        defaultExportByFilename: true,
        eslintrc: {
          enabled: true,
        }
      }),
      Components({
        resolvers: [
          AntDesignVueResolver({
            importStyle: false, // 禁用自动导入样式，避免watermark样式文件不存在的问题
            resolveIcons: true
          })
        ],
        dirs: ['src/components'],
        dts: 'src/components.d.ts',
      })
    ],
    css: {
      preprocessorOptions: {
        less: {
          javascriptEnabled: true
        }
      }
    },
    resolve: {
      alias: {
        '@': fileURLToPath(new URL('./src', import.meta.url))
      },
      extensions: ['.mjs', '.js', '.ts', '.jsx', '.tsx', '.json', '.d.ts', '.vue']
    },
    define: {
      // 注入版本信息
      __APP_VERSION__: JSON.stringify(process.env.npm_package_version || '0.0.0'),
      __APP_NAME__: JSON.stringify(process.env.npm_package_name || 'Takt.Xp'),
      __BUILD_TIME__: JSON.stringify(new Date().toISOString())
    },
    optimizeDeps: {
      include: [
        'vue',
        'vue-router',
        'pinia',
        '@vueuse/core',
        'ant-design-vue',
        '@ant-design/icons-vue',
        'cropperjs'
      ],
      exclude: [
        'ant-design-vue/es/style',
        'ant-design-vue/es/config-provider/style',
        'ant-design-vue/es/locale/zh_CN',
        'ant-design-vue/es/locale/en_US',
        'ant-design-vue/es/table/style',
        'ant-design-vue/es/dropdown/style',
        'ant-design-vue/es/menu/style',
        'ant-design-vue/es/tooltip/style',
        'ant-design-vue/es/form/style',
        'ant-design-vue/es/row/style',
        'ant-design-vue/es/col/style',
        'ant-design-vue/es/cascader/style',
        'ant-design-vue/es/checkbox/style',
        'ant-design-vue/es/radio/style',
        'ant-design-vue/es/input-number/style',
        'ant-design-vue/es/date-picker/style',
        'ant-design-vue/es/input/style',
        'ant-design-vue/es/pagination/style',
        'ant-design-vue/es/watermark/style'
      ]
    },
    server: {
      host: '0.0.0.0',
      port: Number(env.VITE_PORT) || 60080,
      https: null,
      hmr: {
        overlay: false,
        timeout: 30000
      },
      proxy: {
        '/api': {
          target: env.VITE_API_BASE_URL || 'https://localhost:50081',
          changeOrigin: true,
          secure: false,
          ws: true,
          rewrite: (path) => path.replace(/^\/api/, ''),
          configure: (proxy, options) => {
            console.log('🔧 [Vite Proxy] API代理配置:')
            console.log('  - target:', options.target)
            console.log('  - changeOrigin:', options.changeOrigin)
            console.log('  - secure:', options.secure)
            console.log('  - rewrite: /api -> "" (移除/api前缀)')
            console.log('')
            
            proxy.on('proxyReq', (proxyReq, req, res) => {
              console.log('🔄 [Vite Proxy] API代理请求:', req.method, req.url, '->', options.target + req.url)
            })
            
            proxy.on('proxyRes', (proxyRes, req, res) => {
              console.log('✅ [Vite Proxy] API代理响应:', req.url, '->', proxyRes.statusCode)
            })
            
            proxy.on('error', (err, req, res) => {
              console.log('❌ [Vite Proxy] API代理错误:', req.url, '->', err.message)
            })
          }
        },
        '/signalr': {
          target: env.VITE_API_BASE_URL || 'https://localhost:50081',
          changeOrigin: true,
          secure: false,
          ws: true,
          configure: (proxy, options) => {
            console.log('🔧 [Vite Proxy] SignalR代理配置:')
            console.log('  - target:', options.target)
            console.log('  - changeOrigin:', options.changeOrigin)
            console.log('  - secure:', options.secure)
            console.log('  - rewrite: 无 (保持/signalr前缀)')
            console.log('')
            
            proxy.on('proxyReq', (proxyReq, req, res) => {
              console.log('🔄 [Vite Proxy] SignalR代理请求:', req.method, req.url, '->', options.target + req.url)
            })
            
            proxy.on('proxyRes', (proxyRes, req, res) => {
              console.log('✅ [Vite Proxy] SignalR代理响应:', req.url, '->', proxyRes.statusCode)
            })
            
            proxy.on('error', (err, req, res) => {
              console.log('❌ [Vite Proxy] SignalR代理错误:', req.url, '->', err.message)
            })
          }
        }
      },
      fs: {
        strict: true,
        allow: ['..']
      }
    },
    build: {
      outDir: 'dist',
      assetsDir: 'assets',
      sourcemap: false,
      chunkSizeWarningLimit: 1500,
      rollupOptions: {
        output: {
          manualChunks: {
            vue: ['vue', 'vue-router', 'pinia', '@vueuse/core'],
            antd: ['ant-design-vue', '@ant-design/icons-vue']
          }
        }
      }
    }
  }
})

