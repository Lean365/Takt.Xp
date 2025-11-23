<template>
  <div class="app-container">
    <!-- 欢迎卡片 -->
    <a-card class="welcome-card" :bordered="false">
      <a-row :gutter="24">
        <a-col :span="16">
          <h2>{{ welcomeMessage }}</h2>
          <p>{{ dateInfo }}</p>
          <p>
            {{ timeInfo }}{{ format(currentTime, 'HH:mm:ss') }}
            <span style="font-size:12px;color:#888;">[{{ getTimeZoneStr() }}]</span>
          </p>
        </a-col>
        <a-col :span="8">
          <div class="welcome-stats">
            <a-statistic
              :title="t('dashboard.workplace.statistics.todo')"
              :value="todoCount"
              :value-style="{ color: '#1890ff' }"
            >
              <template #prefix>
                <check-square-outlined />
              </template>
            </a-statistic>
            <a-statistic
              :title="t('dashboard.workplace.statistics.email')"
              :value="emailCount"
              :value-style="{ color: '#52c41a' }"
            >
              <template #prefix>
                <mail-outlined />
              </template>
            </a-statistic>
          </div>
        </a-col>
      </a-row>
    </a-card>

    <!-- 快捷入口卡片 -->
    <a-card class="quick-entry-card" :bordered="false">
      <template #title>
        <span><thunderbolt-outlined /> {{ t('dashboard.workplace.quickEntry.title') }}</span>
      </template>
      <template #extra>
        <a-button type="link" @click="handleCustomizeQuickEntry">
          <template #icon><setting-outlined /></template>
          {{ t('dashboard.workplace.quickEntry.customize') }}
        </a-button>
      </template>
      <a-row :gutter="12">
        <a-col :span="3" v-for="entry in quickEntries.filter(e => e && e.menuId !== undefined)" :key="entry.menuId">
          <a-card hoverable class="entry-card" @click="handleQuickEntry(entry)">
            <template #cover>
              <div class="entry-icon" :style="{ background: getRandomColor(entry.menuId) }">
                <component :is="renderMenuIcon(entry.icon)" />
              </div>
            </template>
            <a-card-meta :title="t(entry.label)" />
          </a-card>
        </a-col>
      </a-row>
    </a-card>

    <!-- 我的待办、邮件、日程 tabs -->
    <a-card class="tabs-card" :bordered="false">
      <a-tabs v-model:activeKey="activeTab">
        <a-tab-pane key="todo" :tab="t('dashboard.workplace.todo.title')">
          <a-list
            :data-source="todoList"
            :loading="todoLoading"
            :pagination="false"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>
                    <a-checkbox
                      :checked="item.completed"
                      @change="(e) => handleTodoStatusChange(item, e.target.checked)"
                    >
                      {{ item.title }}
                    </a-checkbox>
                  </template>
                  <template #description>
                    <span>{{ item.deadline }}</span>
                  </template>
                </a-list-item-meta>
                <template #actions>
                  <a @click="handleViewTodo(item)">{{ t('dashboard.workplace.todo.view') }}</a>
                </template>
              </a-list-item>
            </template>
          </a-list>
        </a-tab-pane>
        <a-tab-pane key="email" :tab="t('dashboard.workplace.email.title')">
          <a-list
            :data-source="emailList"
            :loading="emailLoading"
            :pagination="false"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>
                    <a :class="{ 'unread': !item.read }">{{ item.subject }}</a>
                  </template>
                  <template #description>
                    <span>{{ item.sender }} - {{ item.time }}</span>
                  </template>
                </a-list-item-meta>
                <template #actions>
                  <a @click="handleViewEmail(item)">{{ t('dashboard.workplace.email.view') }}</a>
                </template>
              </a-list-item>
            </template>
          </a-list>
        </a-tab-pane>
        <a-tab-pane key="schedule" :tab="t('dashboard.workplace.schedule.title')">
          <a-list
            :data-source="scheduleList"
            :loading="scheduleLoading"
            :pagination="false"
          >
            <template #renderItem="{ item }">
              <a-list-item>
                <a-list-item-meta>
                  <template #title>
                    <a>{{ item.title }}</a>
                  </template>
                  <template #description>
                    <span>{{ item.time }} - {{ item.location }}</span>
                  </template>
                </a-list-item-meta>
                <template #actions>
                  <a @click="handleViewSchedule(item)">{{ t('dashboard.workplace.schedule.view') }}</a>
                </template>
              </a-list-item>
            </template>
          </a-list>
        </a-tab-pane>
      </a-tabs>
    </a-card>

    <!-- 自定义快捷入口对话框 -->
    <a-modal
      v-model:open="customizeOpen"
      :title="t('dashboard.workplace.customize.title')"
      @ok="handleCustomizeOk"
      @cancel="handleCustomizeCancel"
    >
      <a-form :model="customizeForm">
        <a-form-item
          v-for="(entry, index) in customizeForm.entries"
          :key="index"
          :label="t('dashboard.workplace.customize.selectMenu')"
        >
          <a-tree-select
            v-model:value="entry.menuId"
            :treeData="menuOptions"
            :placeholder="t('dashboard.workplace.customize.selectMenu')"
            allow-clear
            tree-default-expand-all
            @change="val => handleMenuChange(val, entry)"
          />
          <component :is="renderMenuIcon(entry.icon)" style="margin-left:8px;font-size:20px;" />
          <a-button type="link" danger @click="handleRemoveEntry(index)">
            <delete-outlined />
          </a-button>
        </a-form-item>
        <a-form-item>
          <a-button type="dashed" block @click="handleAddEntry">
            <plus-outlined /> {{ t('dashboard.workplace.customize.add') }}
          </a-button>
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted, onUnmounted, h } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { useUserStore } from '@/stores/userStore'
import { useMenuStore } from '@/stores/menuStore'
import { 
  FolderOutlined,
  PlusOutlined,
  ReloadOutlined,
  DeleteOutlined,
  ThunderboltOutlined,
  SettingOutlined,
  CheckSquareOutlined,
  MailOutlined,
  CalendarOutlined
} from '@ant-design/icons-vue'
import { 
  format,
  getHours,
  getDayOfYear,
  getWeek,
  getMonth,
  getYear,
  getDate,
  getDay
} from 'date-fns'
import { zhCN } from 'date-fns/locale'
import * as Icons from '@ant-design/icons-vue'
import { useRouter } from 'vue-router'

const { t } = useI18n()
const userStore = useUserStore()
const menuStore = useMenuStore()
const router = useRouter()

// 欢迎信息
const welcomeMessage = computed(() => {
  const hour = getHours(new Date())
  let greeting = ''
  if (hour < 6) {
    greeting = t('dashboard.workplace.welcome.night')
  } else if (hour < 9) {
    greeting = t('dashboard.workplace.welcome.morning')
  } else if (hour < 12) {
    greeting = t('dashboard.workplace.welcome.lateMorning')
  } else if (hour < 14) {
    greeting = t('dashboard.workplace.welcome.afternoon')
  } else if (hour < 17) {
    greeting = t('dashboard.workplace.welcome.lateAfternoon')
  } else if (hour < 19) {
    greeting = t('dashboard.workplace.welcome.evening')
  } else {
    greeting = t('dashboard.workplace.welcome.night')
  }
  return `${greeting}，${userStore.userInfo?.userName || ''}`
})

// 日期信息
const dateInfo = computed(() => {
  const now = new Date()
  const weekdays = [
    t('dashboard.workplace.welcome.weekday.sun'),
    t('dashboard.workplace.welcome.weekday.mon'),
    t('dashboard.workplace.welcome.weekday.tue'),
    t('dashboard.workplace.welcome.weekday.wed'),
    t('dashboard.workplace.welcome.weekday.thu'),
    t('dashboard.workplace.welcome.weekday.fri'),
    t('dashboard.workplace.welcome.weekday.sat')
  ]
  const dayOfYear = getDayOfYear(now)
  const weekOfYear = getWeek(now)
  const quarter = Math.ceil((getMonth(now) + 1) / 3)
  return t('dashboard.workplace.welcome.dateInfo', {
    year: getYear(now),
    month: getMonth(now) + 1,
    day: getDate(now),
    weekday: weekdays[getDay(now)],
    dayOfYear,
    weekOfYear,
    quarter
  })
})

// 时间信息
const timeInfo = computed(() => {
  const now = currentTime.value
  return `${t('dashboard.workplace.welcome.currentTime')}（${getUtcOffsetStr(now)}） `
})

// 动态更新时间
const currentTime = ref(new Date())
let timer: number | null = null

onMounted(() => {
  console.log('menuStore.menuList:', menuStore.menuList)
  // 恢复快捷入口
  const saved = localStorage.getItem('quickEntries')
  if (saved) {
    try {
      const arr = JSON.parse(saved)
      quickEntries.value = Array.isArray(arr) ? arr.filter(e => e && typeof e.menuId === 'number') : []
    } catch {}
  }
  fetchTodoList()
  fetchEmailList()
  fetchScheduleList()
  timer = window.setInterval(() => {
    currentTime.value = new Date()
  }, 1000)
})

onUnmounted(() => {
  if (timer) {
    clearInterval(timer)
    timer = null
  }
})

// 统计数据
const todoCount = ref(0)
const emailCount = ref(0)

// 待办列表
const todoLoading = ref(false)
const todoList = ref([])

// 邮件列表
const emailLoading = ref(false)
const emailList = ref([])

// 日程列表
const scheduleLoading = ref(false)
const scheduleList = ref([])

// 获取菜单列表
function filterMenus(list: any[], parentPath = ''): any[] {
  return list.flatMap((menu: any) => {
    const fullPath = parentPath
      ? `${parentPath.replace(/\/$/, '')}/${menu.path.replace(/^\//, '')}`
      : menu.path.startsWith('/') ? menu.path : `/${menu.path}`;
    if (menu.menuType === 1) {
      return [{
        title: t(menu.transKey),
        value: menu.menuId,
        key: menu.menuId,
        icon: menu.icon,
        path: fullPath
      }]
    } else if (menu.children && Array.isArray(menu.children)) {
      return [{
        title: t(menu.transKey),
        value: menu.menuId,
        key: menu.menuId,
        icon: menu.icon,
        path: fullPath,
        children: filterMenus(menu.children, fullPath)
      }]
    }
    return []
  })
}

const menuOptions = computed(() => {
  return filterMenus((menuStore.menuList as unknown) as any[])
})

function findMenuById(list: any[], id: number): any | null {
  for (const item of list) {
    if (item.value === id) return item
    if (item.children) {
      const found: any | null = findMenuById(item.children, id)
      if (found) return found
    }
  }
  return null
}

function handleMenuChange(val: number, entry: any) {
  const menu = findMenuById(menuOptions.value, val)
  if (menu) {
    entry.icon = menu.icon
    entry.label = menu.title
    entry.path = menu.path
  }
}

// 快捷入口配置
const quickEntries = ref<{ menuId: number, label: string, icon?: string, path?: string }[]>([])

// 自定义快捷入口
const customizeOpen = ref(false)
const customizeForm = ref({
  entries: [] as { menuId: number, label: string, icon?: string, path?: string }[]
})

// 当前激活的 tab
const activeTab = ref('todo')

// 当前激活的菜单项
const activeMenuKey = ref('todo')

/** 刷新事件 */
const handleRefresh = () => {
  fetchTodoList()
  fetchEmailList()
  fetchScheduleList()
}

/** 获取待办列表 */
const fetchTodoList = async () => {
  todoLoading.value = true
  try {
    // TODO: 实现获取待办列表
    todoList.value = []
    todoCount.value = 0
  } catch (error) {
    message.error(t('dashboard.workplace.todo.fetchError'))
  } finally {
    todoLoading.value = false
  }
}

/** 获取邮件列表 */
const fetchEmailList = async () => {
  emailLoading.value = true
  try {
    // TODO: 实现获取邮件列表
    emailList.value = []
    emailCount.value = 0
  } catch (error) {
    message.error(t('dashboard.workplace.email.fetchError'))
  } finally {
    emailLoading.value = false
  }
}

/** 获取日程列表 */
const fetchScheduleList = async () => {
  scheduleLoading.value = true
  try {
    // TODO: 实现获取日程列表
    scheduleList.value = []
  } catch (error) {
    message.error(t('dashboard.workplace.schedule.fetchError'))
  } finally {
    scheduleLoading.value = false
  }
}

/** 查看所有待办 */
const handleViewAllTodo = () => {
  // TODO: 实现查看所有待办
}

/** 查看所有邮件 */
const handleViewAllEmail = () => {
  // TODO: 实现查看所有邮件
}

/** 查看所有日程 */
const handleViewAllSchedule = () => {
  // TODO: 实现查看所有日程
}

/** 查看待办 */
const handleViewTodo = (todo: any) => {
  // TODO: 实现查看待办
}

/** 查看邮件 */
const handleViewEmail = (email: any) => {
  // TODO: 实现查看邮件
}

/** 查看日程 */
const handleViewSchedule = (schedule: any) => {
  // TODO: 实现查看日程
}

/** 更新待办状态 */
const handleTodoStatusChange = async (todo: any, completed: boolean) => {
  try {
    // TODO: 实现更新待办状态
    message.success(t('dashboard.workplace.todo.updateSuccess'))
    fetchTodoList()
  } catch (error) {
    message.error(t('dashboard.workplace.todo.updateError'))
  }
}

/** 快捷入口点击事件 */
const handleQuickEntry = (entry: { menuId: number, label: string, icon?: string, path?: string }) => {
  if (entry.path) {
    // 使用路由导航到对应的菜单
    router.push(entry.path)
  }
}

/** 自定义快捷入口 */
const handleCustomizeQuickEntry = () => {
  customizeForm.value.entries = quickEntries.value.map(entry => ({
    menuId: entry.menuId,
    label: entry.label,
    icon: entry.icon,
    path: entry.path
  }))
  customizeOpen.value = true
}

/** 添加快捷入口 */
const handleAddEntry = () => {
  customizeForm.value.entries.push({
    menuId: 0,
    label: '',
    icon: undefined,
    path: undefined
  })
}

/** 移除快捷入口 */
const handleRemoveEntry = (index: number) => {
  customizeForm.value.entries.splice(index, 1)
}

/** 确认自定义 */
const handleCustomizeOk = () => {
  quickEntries.value = customizeForm.value.entries.map(entry => ({
    menuId: entry.menuId,
    label: entry.label,
    icon: entry.icon,
    path: entry.path
  }))
  // 持久化到 localStorage
  localStorage.setItem('quickEntries', JSON.stringify(quickEntries.value))
  customizeOpen.value = false
  message.success(t('dashboard.workplace.customize.success'))
}

/** 取消自定义 */
const handleCustomizeCancel = () => {
  customizeOpen.value = false
}

/** 菜单选择事件 */
const handleMenuSelect = (info: { key: string }) => {
  activeMenuKey.value = info.key
  activeTab.value = info.key
}

function renderMenuIcon(iconName: string | undefined) {
  return iconName && Icons[iconName as keyof typeof Icons]
    ? () => h(Icons[iconName as keyof typeof Icons])
    : () => h(Icons['AppstoreOutlined']);
}

function getRandomColor(key: number | undefined) {
  if (typeof key !== 'number' || isNaN(key)) return '#1890ff';
  const colors = [
    '#1890ff', '#52c41a', '#faad14', '#eb2f96', '#13c2c2', '#722ed1', '#f5222d', '#fa541c', '#2f54eb', '#a0d911'
  ];
  return colors[key % colors.length];
}

function getUtcOffsetStr(date: Date) {
  const offset = -date.getTimezoneOffset(); // 东八区为-480，需取反
  const sign = offset >= 0 ? '+' : '-';
  const absOffset = Math.abs(offset);
  const hours = String(Math.floor(absOffset / 60)).padStart(2, '0');
  const minutes = String(absOffset % 60).padStart(2, '0');
  return `UTC${sign}${hours}:${minutes}`;
}

function getTimeZoneStr() {
  return Intl.DateTimeFormat().resolvedOptions().timeZone || '';
}
</script>

<style lang="less" scoped>
.app-container {
  padding: 4px;
  background-color: var(--ant-color-bg-layout);
}

.welcome-card {
  margin-bottom: 4px;
  
  h2 {
    margin-bottom: 16px;
    //color: rgba(0, 0, 0, 0.85);
  }
  
  p {
    margin-bottom: 8px;
    //color: rgba(0, 0, 0, 0.45);
  }
}

.welcome-stats {
  display: flex;
  justify-content: space-around;
  padding: 24px 0;
}

.quick-entry-card,
.tabs-card {
  margin-bottom: 24px;
}

.entry-card {
  text-align: center;
  cursor: pointer;
  transition: all 0.3s;
  width: 128px;
  height: 128px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;

  &:hover {
    transform: translateY(-5px);
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
  }
}

.entry-icon {
  width: 48px;
  height: 48px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 32px;
  color: #fff;
  border-radius: 8px;
  margin: 0 auto;
}

.unread {
  font-weight: bold;
  color: #1890ff;
}
</style> 