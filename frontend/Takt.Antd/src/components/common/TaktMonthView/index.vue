<template>
  <div class="month-view">
    <div class="month-header">
      <span class="month-title">{{ monthTitle }}</span>
      <div class="month-actions">
        <a-button type="link" size="small" @click="handlePrev">&lt;</a-button>
        <a-button type="link" size="small" @click="handleNext">&gt;</a-button>
      </div>
    </div>
    <div class="calendar-grid">
      <div class="calendar-header-row">
        <div v-for="(day, idx) in weekDays" :key="idx" class="calendar-header-cell">
          {{ day }}
        </div>
      </div>
      <div class="calendar-body">
        <div v-for="(week, weekIndex) in getMonthDays" :key="weekIndex" class="calendar-row">
          <div v-for="day in week" :key="day.date.toISOString()"
               class="calendar-cell"
               :class="[{ 'selected': isSelected(day.date) }]"
               @click="selectDay(day.date)">
            <span class="calendar-date">{{ day.day }}</span>
            <span v-if="day.isOtherMonth" class="month-label">{{ day.monthLabel }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import {
  format,
  startOfMonth,
  endOfMonth,
  startOfWeek,
  endOfWeek,
  eachDayOfInterval,
  isSameMonth,
  isSameDay,
  getMonth,
  getYear,
  subMonths,
  addMonths
} from 'date-fns'

const props = defineProps<{ currentDate: Date; selectedDate?: Date }>()
const emit = defineEmits(['select', 'update:currentDate'])

const weekDays = ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa']

const monthTitle = computed(() => format(props.currentDate, 'yyyy年MM月'))

interface CalendarDay {
  date: Date
  day: string
  currentMonth: boolean
  isOtherMonth: boolean
  monthLabel?: string
}

const getMonthDays = computed(() => {
  const start = startOfMonth(props.currentDate)
  const end = endOfMonth(props.currentDate)
  const startDay = startOfWeek(start, { weekStartsOn: 0 })
  const endDay = endOfWeek(end, { weekStartsOn: 0 })

  const days = eachDayOfInterval({ start: startDay, end: endDay })
  const weeks: CalendarDay[][] = []
  let currentWeek: CalendarDay[] = []

  days.forEach(day => {
    const currentMonth = isSameMonth(day, props.currentDate)
    const isOtherMonth = !currentMonth
    let monthLabel = ''
    if (isOtherMonth && day.getDate() === 1) {
      monthLabel = format(day, 'MMM') + ' '
    }
    currentWeek.push({
      date: day,
      day: format(day, 'd'),
      currentMonth,
      isOtherMonth,
      monthLabel: monthLabel ? monthLabel + day.getDate() : undefined
    })
    if (currentWeek.length === 7) {
      weeks.push(currentWeek)
      currentWeek = []
    }
  })
  return weeks
})

function isSelected(date: Date) {
  if (!props.selectedDate) return false
  return isSameDay(date, props.selectedDate)
}

function selectDay(date: Date) {
  emit('select', date)
}

function handlePrev() {
  emit('update:currentDate', subMonths(props.currentDate, 1))
}
function handleNext() {
  emit('update:currentDate', addMonths(props.currentDate, 1))
}
</script>

<style lang="less" scoped>
.month-view {
  padding: 8px;
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
  font-size: 14px;
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
  position: relative;
  cursor: pointer;
  border-radius: 50%;
  transition: background 0.2s;
}

.calendar-cell.current-month {
  color: #222;
}

.calendar-cell.not-current {
  color: #bbb;
}

.calendar-cell.selected {
  background: #1976d2;
  color: #fff;
}

.calendar-cell:hover {
  background: #e3f2fd;
}

.calendar-date {
  font-size: 14px;
  font-weight: 500;
}

.month-label {
  position: absolute;
  top: 2px;
  left: 2px;
  font-size: 10px;
}

.month-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}
.month-title {
  font-size: 16px;
  font-weight: 600;
}
.month-actions {
  display: flex;
  gap: 4px;
}
</style> 