<template>
  <div class="calendar">
    <div class="calendar-tabs">
      <span
        v-for="tab in tabs"
        :key="tab.key"
        :class="['calendar-tab', { active: activeTab === tab.key }]"
        @click="activeTab = tab.key"
      >{{ tab.label }}</span>
    </div>
    <div class="calendar-body">
      <Takt-month-view
        v-if="activeTab === 'month'"
        :current-date="currentDate"
        :selected-date="selectedDate"
        @select="handleSelect"
        @update:currentDate="currentDate = $event"
      />
      <Takt-year-view
        v-if="activeTab === 'year'"
        :current-date="currentDate"
        :selected-date="selectedDate"
        @select="handleSelect"
        @update:currentDate="currentDate = $event"
      />
      <Takt-decade-view
        v-if="activeTab === 'decade'"
        :current-date="currentDate"
        :selected-date="selectedDate"
        @select="handleSelect"
        @update:currentDate="currentDate = $event"
      />
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import {
  format,
  addYears, subYears,
  addMonths, subMonths,
  getYear
} from 'date-fns'
import { zhCN } from 'date-fns/locale'

const { t } = useI18n()

const VIEW_MONTH = 'month'
const VIEW_YEAR = 'year'
const VIEW_DECADE = 'decade'

const tabs = [
  { key: VIEW_MONTH, label: '月' },
  { key: VIEW_YEAR, label: '年' },
  { key: VIEW_DECADE, label: '十年' }
]

const activeTab = ref('month')
const currentDate = ref(new Date())
const selectedDate = ref(new Date())

const prevLabel = computed(() => t('home.prev'))
const nextLabel = computed(() => t('home.next'))

const headerTitle = computed(() => {
  const date = currentDate.value
  switch (activeTab.value) {
    case VIEW_MONTH:
      return format(date, 'yyyy年MM月', { locale: zhCN })
    case VIEW_YEAR:
      return format(date, 'yyyy年', { locale: zhCN })
    case VIEW_DECADE: {
      const startYear = Math.floor(getYear(date) / 10) * 10
      return `${startYear} - ${startYear + 9}`
    }
    default:
      return ''
  }
})

function handlePrev() {
  switch (activeTab.value) {
    case VIEW_MONTH:
      currentDate.value = subMonths(currentDate.value, 1)
      break
    case VIEW_YEAR:
      currentDate.value = subYears(currentDate.value, 1)
      break
    case VIEW_DECADE:
      currentDate.value = subYears(currentDate.value, 10)
      break
  }
}
function handleNext() {
  switch (activeTab.value) {
    case VIEW_MONTH:
      currentDate.value = addMonths(currentDate.value, 1)
      break
    case VIEW_YEAR:
      currentDate.value = addYears(currentDate.value, 1)
      break
    case VIEW_DECADE:
      currentDate.value = addYears(currentDate.value, 10)
      break
  }
}
function handleSelect(payload: Date) {
  if (activeTab.value === VIEW_DECADE) {
    currentDate.value = payload
    activeTab.value = VIEW_YEAR
  } else if (activeTab.value === VIEW_YEAR) {
    currentDate.value = payload
    activeTab.value = VIEW_MONTH
  } else if (activeTab.value === VIEW_MONTH) {
    currentDate.value = payload
    selectedDate.value = payload
  }
}
</script>

<style lang="less" scoped>
.calendar {
  width: 100%;
}
.calendar-tabs {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  margin-bottom: 8px;
  gap: 16px;
}
.calendar-tab {
  padding: 4px 16px;
  font-size: 16px;
  color: #888;
  cursor: pointer;
  border-bottom: 2px solid transparent;
  transition: color 0.2s, border-color 0.2s;
}
.calendar-tab.active {
  color: #1976d2;
  border-bottom: 2px solid #1976d2;
  font-weight: 600;
}
.calendar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}
.calendar-title {
  font-weight: 500;
  user-select: none;
}
.calendar-body {
  width: 100%;
}
</style> 
