<template>
  <div class="icon-container">
    <!-- 使用说明 -->
    <div class="usage-section">
      <h2>图标使用说明</h2>
      <div class="usage-list">
        <div v-for="(code, title) in usage" :key="title" class="usage-item">
          <div class="usage-title">{{ title }}</div>
          <a-typography-paragraph copyable>
            <code>{{ code }}</code>
          </a-typography-paragraph>
        </div>
      </div>
    </div>

    <!-- 功能控制栏 -->
    <div class="control-bar">
      <!-- 搜索栏 -->
      <a-input-search
        v-model:value="searchText"
        placeholder="搜索图标，例如：edit"
        style="width: 300px"
        @search="handleSearch"
      />
      
      <!-- 颜色模式切换 -->
      <div class="color-controls">
        <a-radio-group v-model:value="colorMode" button-style="solid" size="small">
          <a-radio-button value="default">默认颜色</a-radio-button>
          <a-radio-button value="random">随机颜色</a-radio-button>
          <a-radio-button value="theme">主题色</a-radio-button>
          <a-radio-button value="gradient">渐变色</a-radio-button>
        </a-radio-group>
        
        <!-- 动画控制 -->
        <a-select
          v-model:value="animationMode"
          placeholder="选择动画"
          style="width: 120px"
          size="small"
          allow-clear
        >
          <a-select-option value="pulse">脉冲</a-select-option>
          <a-select-option value="spin">旋转</a-select-option>
          <a-select-option value="bounce">弹跳</a-select-option>
          <a-select-option value="shake">抖动</a-select-option>
        </a-select>
        
        <!-- 尺寸控制 -->
        <a-select
          v-model:value="sizeMode"
          placeholder="选择尺寸"
          style="width: 100px"
          size="small"
        >
          <a-select-option value="xs">超小</a-select-option>
          <a-select-option value="sm">小</a-select-option>
          <a-select-option value="md">中等</a-select-option>
          <a-select-option value="lg">大</a-select-option>
          <a-select-option value="xl">超大</a-select-option>
          <a-select-option value="xxl">特大</a-select-option>
        </a-select>
      </div>
    </div>

    <!-- 图标列表 -->
    <div class="icon-list">
      <template v-for="(icons, category) in groupedIcons" :key="category">
        <div class="category-title">{{ category }}</div>
        <div class="icon-grid">
          <div
            v-for="icon in filterIcons(icons)"
            :key="icon.name"
            class="icon-item"
            @click="handleCopy(icon.name)"
          >
            <component 
              :is="icon.name" 
              class="icon"
              v-icon-color="getIconColorConfig(icon.name)"
              v-icon-animation="animationMode"
              v-icon-size="sizeMode"
            />
            <div class="icon-name">{{ formatIconName(icon.name) }}</div>
            <div class="icon-color-info" v-if="colorMode === 'random'">
              <div class="color-preview" :style="getColorPreviewStyle(icon.name)"></div>
            </div>
          </div>
        </div>
      </template>
    </div>
  </div>
</template>

<script lang="ts">
// 导出所有图标，方便其他组件按需导入
export { default as Icons } from '@ant-design/icons-vue'
</script>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted } from 'vue'
import { message } from 'ant-design-vue'
import * as Icons from '@ant-design/icons-vue'
import { generateRandomColor } from '@/utils/iconColorUtil'

// 使用示例文档
const usage = ref<Record<string, string>>({
  '按需导入': `import { EditOutlined } from '@ant-design/icons-vue'`,
  '模板使用': `<edit-outlined />`,
  '动态组件': `<component :is="'EditOutlined'" />`,
  '随机颜色': `<edit-outlined v-icon-random="'edit-icon'" />`,
  '主题色': `<edit-outlined v-icon-theme="'primary'" />`,
  '动画效果': `<edit-outlined v-icon-animation="'pulse'" />`,
  '尺寸控制': `<edit-outlined v-icon-size="'lg'" />`
})

// 图标分类
const iconCategories = {
  '方向类图标': [
    'LeftOutlined', 'RightOutlined', 'UpOutlined', 'DownOutlined',
    'ArrowLeftOutlined', 'ArrowRightOutlined', 'ArrowUpOutlined', 'ArrowDownOutlined'
  ],
  '提示类图标': [
    'QuestionOutlined', 'QuestionCircleOutlined', 'PlusOutlined', 'PlusCircleOutlined',
    'PlusSquareOutlined', 'MinusOutlined', 'MinusCircleOutlined', 'MinusSquareOutlined',
    'InfoOutlined', 'InfoCircleOutlined', 'ExclamationOutlined', 'ExclamationCircleOutlined',
    'CloseOutlined', 'CloseCircleOutlined', 'CloseSquareOutlined', 'CheckOutlined',
    'CheckCircleOutlined', 'CheckSquareOutlined'
  ],
  '编辑类图标': [
    'EditOutlined', 'FormOutlined', 'CopyOutlined', 'ScissorOutlined', 'DeleteOutlined',
    'SnippetsOutlined', 'DiffOutlined', 'HighlightOutlined'
  ],
  '数据类图标': [
    'SearchOutlined', 'FilterOutlined', 'TableOutlined', 'BarsOutlined',
    'StarOutlined', 'HeartOutlined'
  ],
  '文件类图标': [
    'FileOutlined', 'FileTextOutlined', 'FileAddOutlined', 'FileExcelOutlined',
    'FileWordOutlined', 'FilePdfOutlined', 'FileImageOutlined', 'FileZipOutlined',
    'FileUnknownOutlined'
  ],
  '其他常用图标': [
    'UserOutlined', 'TeamOutlined', 'SettingOutlined', 'ToolOutlined', 'AppstoreOutlined',
    'CloudOutlined', 'CloudUploadOutlined', 'CloudDownloadOutlined', 'CloudSyncOutlined',
    'ReloadOutlined', 'RedoOutlined', 'UndoOutlined', 'LoginOutlined', 'LogoutOutlined',
    'PoweroffOutlined', 'MenuFoldOutlined', 'MenuUnfoldOutlined', 'FullscreenOutlined',
    'FullscreenExitOutlined', 'EyeOutlined', 'EyeInvisibleOutlined', 'LockOutlined',
    'UnlockOutlined', 'ExportOutlined', 'ImportOutlined', 'SaveOutlined', 'PrinterOutlined',
    'ShareAltOutlined', 'DownloadOutlined', 'UploadOutlined', 'SyncOutlined', 'HomeOutlined',
    'FolderOutlined', 'FolderOpenOutlined', 'CalendarOutlined', 'BellOutlined',
    'MailOutlined', 'PhoneOutlined', 'GlobalOutlined', 'LoadingOutlined'
  ]
}

const searchText = ref('')
const colorMode = ref('default')
const animationMode = ref('')
const sizeMode = ref('md')

// 将图标按分类组织
const groupedIcons = computed(() => {
  const result: Record<string, { name: string; component: any }[]> = {}
  
  Object.entries(iconCategories).forEach(([category, iconNames]) => {
    result[category] = iconNames.map(name => ({
      name,
      component: (Icons as any)[name]
    }))
  })
  
  return result
})

// 获取图标颜色配置
const getIconColorConfig = (iconName: string) => {
  switch (colorMode.value) {
    case 'random':
      return { type: 'random', seed: iconName }
    case 'theme':
      return { type: 'theme', value: 'primary' }
    case 'gradient':
      return { type: 'gradient', value: 'primary' }
    default:
      return null
  }
}

// 获取颜色预览样式
const getColorPreviewStyle = (iconName: string) => {
  if (colorMode.value === 'random') {
    const color = generateRandomColor(iconName)
    return {
      backgroundColor: `hsl(${color.hue}, ${color.saturation}%, ${color.lightness}%)`,
      width: '16px',
      height: '16px',
      borderRadius: '50%',
      border: '1px solid #d9d9d9'
    }
  }
  return {}
}

// 过滤图标
const filterIcons = (icons: { name: string; component: any }[]) => {
  if (!searchText.value) return icons
  return icons.filter(icon => 
    icon.name.toLowerCase().includes(searchText.value.toLowerCase())
  )
}

// 格式化图标名称
const formatIconName = (name: string) => {
  return name.replace('Outlined', '')
}

// 搜索图标
const handleSearch = (value: string) => {
  searchText.value = value
}

// 复制图标名称
const handleCopy = (iconName: string) => {
  // 根据当前模式提供不同的复制格式
  let copyText = `<${iconName} />`
  
  if (colorMode.value === 'random') {
    copyText = `<${iconName} v-icon-random="'${iconName.toLowerCase()}'" />`
  } else if (colorMode.value === 'theme') {
    copyText = `<${iconName} v-icon-theme="'primary'" />`
  } else if (colorMode.value === 'gradient') {
    copyText = `<${iconName} v-icon-color="{ type: 'gradient', value: 'primary' }" />`
  }
  
  if (animationMode.value) {
    copyText = copyText.replace(' />', ` v-icon-animation="'${animationMode.value}'" />`)
  }
  
  if (sizeMode.value && sizeMode.value !== 'md') {
    copyText = copyText.replace(' />', ` v-icon-size="'${sizeMode.value}'" />`)
  }
  
  // 提供两种复制格式：组件标签和导入语句
  const options = [
    { text: copyText, desc: '组件标签' },
    { text: `import { ${iconName} } from '@ant-design/icons-vue'`, desc: '导入语句' }
  ]
  
  const menu = document.createElement('div')
  menu.style.position = 'fixed'
  menu.style.zIndex = '1000'
  menu.style.backgroundColor = '#fff'
  menu.style.boxShadow = '0 2px 8px rgba(0,0,0,0.15)'
  menu.style.borderRadius = '4px'
  menu.style.padding = '4px 0'
  
  options.forEach(({ text, desc }) => {
    const item = document.createElement('div')
    item.style.padding = '8px 12px'
    item.style.cursor = 'pointer'
    item.style.transition = 'all 0.3s'
    item.innerText = desc
    
    item.addEventListener('mouseover', () => {
      item.style.backgroundColor = '#f5f5f5'
    })
    
    item.addEventListener('mouseout', () => {
      item.style.backgroundColor = '#fff'
    })
    
    item.addEventListener('click', () => {
      navigator.clipboard.writeText(text).then(() => {
        message.success(`已复制: ${text}`)
        document.body.removeChild(menu)
      }).catch(() => {
        message.error('复制失败')
      })
    })
    
    menu.appendChild(item)
  })
  
  // 点击其他区域关闭菜单
  const handleClickOutside = (e: MouseEvent) => {
    if (!menu.contains(e.target as Node)) {
      document.body.removeChild(menu)
      document.removeEventListener('click', handleClickOutside)
    }
  }
  
  document.addEventListener('click', handleClickOutside)
  document.body.appendChild(menu)
  
  // 定位菜单
  const event = window.event as MouseEvent
  menu.style.left = `${event.clientX}px`
  menu.style.top = `${event.clientY}px`
}

// 监听颜色模式变化，更新使用说明
const updateUsageExamples = () => {
  if (colorMode.value === 'random') {
    usage.value['随机颜色'] = `<edit-outlined v-icon-random="'edit-icon'" />`
  } else if (colorMode.value === 'theme') {
    usage.value['主题色'] = `<edit-outlined v-icon-theme="'primary'" />`
  } else if (colorMode.value === 'gradient') {
    usage.value['渐变色'] = `<edit-outlined v-icon-color="{ type: 'gradient', value: 'primary' }" />`
  }
}
</script>

<style lang="less" scoped>
.icon-container {
  padding: 24px;
  
  .usage-section {
    margin-bottom: 32px;
    
    h2 {
      margin-bottom: 16px;
      font-size: 20px;
      font-weight: 500;
    }
    
    .usage-list {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
      gap: 16px;
      
      .usage-item {
        padding: 16px;
        //background-color: #fafafa;
        border-radius: 4px;
        
        .usage-title {
          font-weight: 500;
          margin-bottom: 8px;
        }
        
        code {
          display: block;
          padding: 8px;
          //background-color: #f5f5f5;
          border-radius: 4px;
          font-family: monospace;
        }
      }
    }
  }
  
  .control-bar {
    margin-bottom: 24px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex-wrap: wrap;
    gap: 16px;
    
    .color-controls {
      display: flex;
      align-items: center;
      gap: 12px;
      flex-wrap: wrap;
    }
  }
  
  .icon-list {
    .category-title {
      font-size: 18px;
      font-weight: 500;
      margin: 16px 0;
      padding-left: 8px;
      border-left: 4px solid var(--ant-primary-color);
    }
    
    .icon-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(140px, 1fr));
      gap: 16px;
      margin-bottom: 32px;
      
      .icon-item {
        display: flex;
        flex-direction: column;
        align-items: center;
        padding: 16px 8px;
        border-radius: 8px;
        cursor: pointer;
        transition: all 0.3s;
        position: relative;
        
        &:hover {
          background-color: var(--ant-primary-1);
          transform: translateY(-2px);
          box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        }
        
        .icon {
          font-size: 24px;
          margin-bottom: 8px;
          transition: all 0.3s;
        }
        
        .icon-name {
          font-size: 12px;
          color: rgba(0, 0, 0, 0.65);
          text-align: center;
          margin-bottom: 4px;
        }
        
        .icon-color-info {
          display: flex;
          align-items: center;
          gap: 4px;
          
          .color-preview {
            transition: all 0.3s;
            
            &:hover {
              transform: scale(1.2);
            }
          }
        }
      }
    }
  }
}

// 响应式设计
@media (max-width: 768px) {
  .icon-container {
    .control-bar {
      flex-direction: column;
      align-items: stretch;
      
      .color-controls {
        justify-content: center;
      }
    }
    
    .icon-grid {
      grid-template-columns: repeat(auto-fill, minmax(100px, 1fr));
    }
  }
}
</style> 