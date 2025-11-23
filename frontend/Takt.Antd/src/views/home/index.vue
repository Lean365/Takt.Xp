<template>
  <div class="home-portal-container">
    <a-row :gutter="16" align="stretch" class="full-height-row">
      <!-- 左栏：公告通知、待办事项 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <a-card :bordered="false" class="portal-card news-card flex-card">
            <a-tabs v-model:activeKey="activeNewsTab" size="large">
              <a-tab-pane v-for="tab in newsTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="newsData[activeNewsTab]?.slice(0,5) || []" :pagination="false">
                      <template #renderItem="{ item, index }">
                        <a-list-item>
                          <span class="news-list-index">{{ index + 1 }}</span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
          <a-card :bordered="false" class="portal-card todo-card flex-card" style="margin-top:16px;">
            <a-tabs v-model:activeKey="activeTodoTab" size="large">
              <a-tab-pane v-for="tab in todoTabs" :key="tab.key" :tab="t(tab.label)">
                <a-list :data-source="todoList[tab.key]" :pagination="false" :loading="loadingTodos">
                  <template #renderItem="{ item }">
                    <a-list-item>
                      <span class="todo-title">{{ item.title }}</span>
                      <span class="todo-type">{{ item.type }}</span>
                      <span class="todo-priority" v-if="item.priority" :class="`priority-${item.priority}`">{{ item.priority }}</span>
                      <span class="todo-date">{{ item.date }}</span>
                    </a-list-item>
                  </template>
                  <template #default>
                    <a-empty :description="t('common.noData')" />
                  </template>
                </a-list>
                <div class="todo-pagination">
                  <a-button size="small">{{ t('home.prev') }}</a-button>
                  <a-button size="small">{{ t('home.next') }}</a-button>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
        </div>
      </a-col>
      <!-- 中栏：会议、文件 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <a-card :bordered="false" class="portal-card meeting-card flex-card half-card">
            <a-tabs v-model:activeKey="activeMeetingTab" size="large">
              <a-tab-pane v-for="tab in meetingTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="tab.key === 'meeting' ? (meetingList || []).slice(0,5) : (carList || []).slice(0,5)" :pagination="false">
                      <template #renderItem="{ item, index }">
                        <a-list-item>
                          <span class="news-list-index">{{ index + 1 }}</span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }} {{ item.time }}</span>
                          <a-tag v-if="item.status" :color="item.statusColor">{{ t(item.status) }}</a-tag>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
          <a-card :bordered="false" class="portal-card meeting-card flex-card half-card" style="margin-top:16px;">
            <a-tabs v-model:activeKey="activeFileTab" size="large">
              <a-tab-pane v-for="tab in fileTabs" :key="tab.key" :tab="t(tab.label)">
                <div class="news-main no-image-news">
                  <div class="news-list-block" style="width:100%;">
                    <a-list :data-source="fileList[tab.key as keyof typeof fileList].slice(0,5)" :pagination="false">
                      <template #renderItem="{ item }">
                        <a-list-item>
                          <span class="file-icon"><component :is="item.icon" /></span>
                          <span class="news-list-title">{{ item.title }}</span>
                          <span class="news-list-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </div>
              </a-tab-pane>
            </a-tabs>
          </a-card>
        </div>
      </a-col>
      <!-- 右栏：日历和日程管理 -->
      <a-col :span="8" class="full-height-col">
        <div class="column-flex">
          <div class="right-flex">
            <a-card :bordered="false" class="portal-card calendar-card flex-card half-card">
              <Takt-calendar />
            </a-card>
            <a-card :bordered="false" class="portal-card schedule-list-card flex-card half-card" style="margin-top:16px;">
              <a-tabs v-model:activeKey="activeScheduleTab" size="large">
                <a-tab-pane v-for="tab in scheduleTabs" :key="tab.key" :tab="t(tab.label)">
                  <div class="schedule-list">
                    <a-list :data-source="tab.key === 'daily' ? (scheduleList || []).slice(0,5) : (weeklyScheduleList || []).slice(0,5)" :pagination="false">
                      <template #renderItem="{ item }">
                        <a-list-item>
                          <span class="schedule-dot"></span>
                          <span class="schedule-content">{{ item.title }}</span>
                          <span class="schedule-date">{{ item.date }}</span>
                        </a-list-item>
                      </template>
                    </a-list>
                    <div class="news-list-pagination">
                      <a-button size="small">{{ t('home.prev') }}</a-button>
                      <a-button size="small">{{ t('home.next') }}</a-button>
                    </div>
                  </div>
                </a-tab-pane>
              </a-tabs>
            </a-card>
          </div>
        </div>
      </a-col>
    </a-row>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/userStore'
import { getMyInstances } from '@/api/workflow/instance'
import type { TaktInstance, TaktInstanceQuery } from '@/types/workflow/instance'
import { FileTextOutlined, FileExcelOutlined, FileImageOutlined, FilePptOutlined, FileZipOutlined, FileWordOutlined, VideoCameraOutlined } from '@ant-design/icons-vue'
import { getNewsList, getHotNews, getRecommendedNews } from '@/api/routine/news/news'
import { getNoticeList } from '@/api/routine/notice/notice'
import { getMeetingList, getMyMeetingList } from '@/api/routine/metting/meeting'
import { getVehicleList } from '@/api/routine/vehicle/vehicle'
import { getScheduleList, getMyScheduleList } from '@/api/routine/schedule/schedule'
import { getMailList } from '@/api/routine/email/mail'

const { t } = useI18n()
const userStore = useUserStore()
const currentUser = computed(() => userStore.userInfo)

const activeNewsTab = ref('company')
const newsTabs = computed(() => [
  { key: 'company', label: t('home.newstab.company.title') },
  { key: 'notice', label: t('home.newstab.notice.title') },
  { key: 'rule', label: t('home.newstab.rule.title') }
])

const meetingTabs = computed(() => [
  { key: 'meeting', label: t('home.meetingtab.meeting.title') },
  { key: 'car', label: t('home.meetingtab.car.title') }
])

const fileTabs = computed(() => [
  { key: 'file', label: t('home.filetab.myFile.title') },
  { key: 'cloud', label: t('home.filetab.myCloud.title') }
])

const scheduleTabs = computed(() => [
  { key: 'daily', label: t('home.scheduletab.dailySchedule.title') },
  { key: 'weekly', label: t('home.scheduletab.weeklySchedule.title') }
])

// 新闻数据
const newsData = ref<{ [key: string]: { title: string; date: string }[] }>({
  company: [],
  notice: [],
  rule: []
})

const loadingNews = ref(false)

// 获取新闻数据
const loadNewsData = async () => {
  loadingNews.value = true
  try {
    // 获取公司新闻
    const companyNewsRes = await getNewsList({ pageIndex: 1, pageSize: 5 })
    if (companyNewsRes.data) {
      newsData.value.company = companyNewsRes.data.rows.map((item: any) => ({
        title: item.newsTitle,
        date: new Date(item.createTime).toLocaleDateString()
      }))
    }

    // 获取通知
    const noticeRes = await getNoticeList({ pageIndex: 1, pageSize: 5 })
    if (noticeRes.data) {
      newsData.value.notice = noticeRes.data.rows.map((item: any) => ({
        title: item.noticeTitle,
        date: new Date(item.createTime).toLocaleDateString()
      }))
    }

    // 获取制度（使用推荐新闻作为制度）
    const ruleRes = await getRecommendedNews(5)
    if (ruleRes.data) {
      newsData.value.rule = ruleRes.data.map((item: any) => ({
        title: item.newsTitle,
        date: new Date(item.createTime).toLocaleDateString()
      }))
    }
  } catch (error) {
    console.error('获取新闻数据失败:', error)
  } finally {
    loadingNews.value = false
  }
}

// 待办相关数据
const activeTodoTab = ref('pending')
const todoTabs = computed(() => [
  { key: 'pending', label: t('home.todotab.myWait.title'), status: 0 }, // 未处理状态
  { key: 'processing', label: t('home.todotab.processing'), status: 1 }, // 处理中状态
  { key: 'completed', label: t('home.todotab.myDone.title'), status: 2 }, // 已完成状态
  { key: 'overdue', label: t('home.todotab.overdue'), urgeType: 'overdue' }, // 已逾期任务
  { key: 'dueSoon', label: t('home.todotab.dueSoon'), urgeType: 'duesoon' }, // 即将到期任务
  { key: 'highPriority', label: t('home.todotab.highPriority'), urgeType: 'highpriority' } // 高优先级任务
])

const todoList = ref<{ [key: string]: { title: string; type: string; date: string; priority?: string }[] }>({
  pending: [],
  processing: [],
  completed: [],
  overdue: [],
  dueSoon: [],
  highPriority: []
})

const todoStats = ref({
  pendingCount: 0,
  processingCount: 0,
  completedCount: 0,
  overdueCount: 0,
  dueSoonCount: 0,
  highPriorityCount: 0,
  totalCount: 0
})

const loadingTodos = ref(false)

// 获取用户待办数据（使用真实的工作流API）
const loadUserTodos = async () => {
  if (!currentUser.value?.userId) return
  
  loadingTodos.value = true
  try {
    // 查询参数
    const query: TaktInstanceQuery = {
      pageIndex: 1,
      pageSize: 50
    }
    
    // 获取我的工作流实例
    const instancesRes = await getMyInstances(query)
    
    // 调试日志
    console.log('工作流API响应:', {
      instancesRes: instancesRes.data
    })
    
    // 处理工作流实例数据
    const instancesData = instancesRes.data ? instancesRes.data.rows || [] : []
    
    console.log('处理后的数据:', {
      instancesData: instancesData.length
    })
    
    // 转换数据格式
    const convertToTodoItem = (item: any) => ({
      title: item.instanceTitle || `流程实例 ${item.instanceId}`,
      type: '工作流',
      date: new Date(item.createTime).toLocaleDateString(),
      priority: item.priority === 3 ? '高' : item.priority === 2 ? '中' : '低'
    })
    
    // 按状态分类数据
    todoList.value = {
      pending: instancesData.filter((item: any) => item.status === 1).slice(0, 5).map(convertToTodoItem), // 运行中
      processing: instancesData.filter((item: any) => item.status === 1).slice(0, 5).map(convertToTodoItem), // 运行中
      completed: instancesData.filter((item: any) => item.status === 2).slice(0, 5).map(convertToTodoItem), // 已完成
      overdue: [], // 暂时为空，后续可根据时间判断
      dueSoon: [], // 暂时为空，后续可根据时间判断
      highPriority: instancesData.filter((item: any) => item.priority >= 3).slice(0, 5).map(convertToTodoItem) // 高优先级
    }
    
    // 更新统计数据
    todoStats.value = {
      pendingCount: instancesData.filter((item: any) => item.status === 1).length, // 运行中
      processingCount: instancesData.filter((item: any) => item.status === 1).length, // 运行中
      completedCount: instancesData.filter((item: any) => item.status === 2).length, // 已完成
      overdueCount: 0, // 暂时为0
      dueSoonCount: 0, // 暂时为0
      highPriorityCount: instancesData.filter((item: any) => item.priority >= 3).length, // 高优先级
      totalCount: instancesData.length
    }
    
  } catch (error) {
    console.error('获取用户待办失败:', error)
  } finally {
    loadingTodos.value = false
  }
}

// 页面加载时获取数据
onMounted(() => {
  loadUserTodos()
  loadNewsData()
  loadMeetingData()
  loadVehicleData()
  loadScheduleData()
  loadWeeklyScheduleData()
})

const activeFileTab = ref('file')
const fileList = computed(() => ({
  file: [
    { icon: FileTextOutlined, title: t('home.filetab.myFile.file1'), date: '2019-05-08' },
    { icon: FileExcelOutlined, title: t('home.filetab.myFile.file2'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file3'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file4'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myFile.file5'), date: '2019-05-08' },

  ],
  cloud: [
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud1'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud2'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud3'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud4'), date: '2019-05-08' },
    { icon: FileImageOutlined, title: t('home.filetab.myCloud.cloud5'), date: '2019-05-08' }
  ]
}))
// 会议数据
const meetingList = ref<{ title: string; date: string; time: string; status: string; statusColor: string }[]>([])
const loadingMeetings = ref(false)

// 获取会议数据
const loadMeetingData = async () => {
  loadingMeetings.value = true
  try {
    const meetingRes = await getMeetingList({ pageIndex: 1, pageSize: 5 })
    if (meetingRes.data) {
      meetingList.value = meetingRes.data.rows.map((item: any) => ({
        title: item.meetingTitle,
        date: new Date(item.startTime).toLocaleDateString(),
        time: `${new Date(item.startTime).toLocaleTimeString()} - ${new Date(item.endTime).toLocaleTimeString()}`,
        status: item.meetingStatus === 0 ? 'home.status.wait' : item.meetingStatus === 1 ? 'home.status.agree' : 'home.status.done',
        statusColor: item.meetingStatus === 0 ? 'orange' : item.meetingStatus === 1 ? 'blue' : 'gray'
      }))
    }
  } catch (error) {
    console.error('获取会议数据失败:', error)
  } finally {
    loadingMeetings.value = false
  }
}
// 日程数据
const scheduleList = ref<{ title: string; date: string }[]>([])
const loadingSchedules = ref(false)

// 获取日程数据
const loadScheduleData = async () => {
  loadingSchedules.value = true
  try {
    const scheduleRes = await getMyScheduleList({ pageIndex: 1, pageSize: 5 })
    if (scheduleRes.data) {
      scheduleList.value = scheduleRes.data.rows.map((item: any) => ({
        title: item.title,
        date: `${new Date(item.startTime).toLocaleDateString()} ${new Date(item.startTime).toLocaleTimeString()}-${new Date(item.endTime).toLocaleTimeString()}`
      }))
    }
  } catch (error) {
    console.error('获取日程数据失败:', error)
  } finally {
    loadingSchedules.value = false
  }
}
const activeScheduleTab = ref('daily')
// 周程数据
const weeklyScheduleList = ref<{ title: string; date: string }[]>([])
const loadingWeeklySchedules = ref(false)

// 获取周程数据
const loadWeeklyScheduleData = async () => {
  loadingWeeklySchedules.value = true
  try {
    // 获取本周的日程
    const now = new Date()
    const startOfWeek = new Date(now.setDate(now.getDate() - now.getDay()))
    const endOfWeek = new Date(now.setDate(now.getDate() - now.getDay() + 6))
    
    const weeklyRes = await getMyScheduleList({ 
      pageIndex: 1, 
      pageSize: 5,
      startTime: startOfWeek.toISOString(),
      endTime: endOfWeek.toISOString()
    })
    
    if (weeklyRes.data) {
      weeklyScheduleList.value = weeklyRes.data.rows.map((item: any) => ({
        title: item.title,
        date: `${new Date(item.startTime).toLocaleDateString()} 至 ${new Date(item.endTime).toLocaleDateString()}`
      }))
    }
  } catch (error) {
    console.error('获取周程数据失败:', error)
  } finally {
    loadingWeeklySchedules.value = false
  }
}
const activeMeetingTab = ref('meeting')
// 车辆数据
const carList = ref<{ title: string; date: string; time: string; status: string; statusColor: string }[]>([])
const loadingVehicles = ref(false)

// 获取车辆数据
const loadVehicleData = async () => {
  loadingVehicles.value = true
  try {
    const vehicleRes = await getVehicleList({ pageIndex: 1, pageSize: 5 })
    if (vehicleRes.data) {
      carList.value = vehicleRes.data.rows.map((item: any) => ({
        title: `${item.brand || ''} ${item.model || ''} (${item.plateNumber})`,
        date: new Date(item.createTime).toLocaleDateString(),
        time: '全天',
        status: item.status === 0 ? 'home.status.wait' : item.status === 1 ? 'home.status.agree' : 'home.status.done',
        statusColor: item.status === 0 ? 'orange' : item.status === 1 ? 'blue' : 'gray'
      }))
    }
  } catch (error) {
    console.error('获取车辆数据失败:', error)
  } finally {
    loadingVehicles.value = false
  }
}
</script>

<style lang="less" scoped>
.home-portal-container {
  padding: 0;
  background: var(--ant-color-bg-layout);
  height: calc(100vh - 148px - 48px - 40px);
}
.full-height-row {
  height: 100%;
}
.full-height-col {
  height: 100%;
}
.column-flex {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.flex-card {
  flex: 1 1 0;
  min-height: 0;
  display: flex;
  flex-direction: column;
}
.half-card {
  flex: 1 1 0;
  min-height: 0;
}
.right-flex {
  display: flex;
  flex-direction: column;
  height: 100%;
}
.right-flex > .half-card {
  flex: 1 1 0;
  min-height: 0;
}
.portal-card {
  margin-bottom: 8px;
}
.news-main {
  display: flex;
  gap: 8px;
}
.news-image-block {
  width: 320px;
  flex-shrink: 0;
}
.news-image {
  width: 100%;
  height: 160px;
  object-fit: cover;
  border-radius: 8px;
}
.news-image-info {
  margin-top: 8px;
}
.news-image-title {
  font-weight: bold;
  font-size: 16px;
  margin-bottom: 4px;
}
.news-image-desc {
  color: #888;
  font-size: 13px;
  margin-bottom: 4px;
}
.news-image-tag {
  color: #f5222d;
  font-size: 12px;
  margin-right: 8px;
}
.news-image-date {
  color: #aaa;
  font-size: 12px;
}
.news-list-block {
  flex: 1;
}
.news-list-index {
  margin-right: 8px;
}
.news-list-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.news-list-date {
  font-size: 12px;
  white-space: nowrap;
}
.news-list-pagination {
  margin-top: 8px;
  text-align: right;
}
.todo-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.todo-type {
  margin-right: 8px;
  white-space: nowrap;
}
.todo-priority {
  margin-right: 8px;
  white-space: nowrap;
}
.todo-date {
  font-size: 12px;
  white-space: nowrap;
}
.todo-pagination {
  margin-top: 8px;
  text-align: right;
}
.priority-高 {
  color: #ff4d4f;
  font-weight: bold;
}
.priority-中 {
  color: #faad14;
  font-weight: bold;
}
.priority-低 {
  color: #52c41a;
  font-weight: bold;
}
.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  margin-bottom: 8px;
}
.meeting-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.meeting-date {
  margin-right: 8px;
  white-space: nowrap;
}
.meeting-time {
  margin-right: 8px;
  white-space: nowrap;
}
.schedule-list {
  padding-left: 8px;
}
.schedule-title {
  font-weight: bold;
  margin-bottom: 8px;
}
.schedule-dot {
  display: inline-block;
  width: 6px;
  height: 6px;
  border-radius: 50%;
  margin-right: 8px;
}
.schedule-content {
  margin-right: 8px;
  flex: 1;
}
.schedule-date {
  font-size: 12px;
  white-space: nowrap;
}
.file-icon {
  margin-right: 8px;
  font-size: 18px;
  display: flex;
  align-items: center;
}
.file-title {
  font-weight: 500;
  margin-right: 8px;
  flex: 1;
}
.file-date {
  font-size: 12px;
  white-space: nowrap;
}

.list-pagination {
  margin-top: 8px;
  text-align: right;
}

:deep(.ant-list-item) {
  display: flex;
  align-items: center;
  padding: 8px 0;
  border-bottom: 1px solid #f0f0f0;
}

:deep(.ant-list-item:last-child) {
  border-bottom: none;
}

:deep(.ant-list) {
  padding: 0 16px;
}

.week-view {
  padding: 8px;
}

.week-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.week-date {
  font-weight: 500;
}

.week-grid {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  gap: 8px;
}

.week-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.week-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.week-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.year-view {
  padding: 8px;
}

.year-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.year-date {
  font-weight: 500;
}

.year-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 16px;
}

.year-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.year-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.year-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.calendar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.calendar-date {
  font-weight: 500;
}

.quarter-view {
  padding: 8px;
}

.quarter-grid {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 16px;
}

.quarter-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
}

.quarter-item-header {
  padding: 8px;
  text-align: center;
  border-bottom: 1px solid #f0f0f0;
  font-weight: 500;
}

.quarter-item-content {
  padding: 8px;
  text-align: center;
  font-size: 12px;
}

.calendar-grid {
  width: 100%;
}

.calendar-header-row {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  text-align: center;
  font-weight: 500;
  border-bottom: 1px solid #f0f0f0;
}

.calendar-header-cell {
  padding: 8px;
}

.calendar-body {
  margin-top: 8px;
}

.calendar-row {
  display: grid;
  grid-template-columns: repeat(7, 1fr);
  text-align: center;
}

.calendar-cell {
  padding: 8px;
  min-height: 32px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.calendar-date {
  font-size: 14px;
}

.current-month {
  font-weight: 500;
}

.calendar-cell:not(.current-month) {
  opacity: 0.5;
}

.year-content {
  padding: 8px;
}

.year-title {
  font-size: 16px;
  font-weight: 500;
  text-align: center;
  margin-bottom: 8px;
}

.year-quarters {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 8px;
}

.quarter-item {
  border: 1px solid #f0f0f0;
  border-radius: 4px;
  padding: 8px;
}

.quarter-title {
  font-weight: 500;
  margin-bottom: 4px;
}

.quarter-date {
  font-size: 12px;
  color: #666;
}
</style> 
