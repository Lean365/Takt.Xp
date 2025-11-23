<template>
  <div class="decade-view">
    <div class="decade-header">
      <span class="decade-title">{{ decadeTitle }}</span>
      <div class="decade-actions">
        <a-button type="link" size="small" @click="handlePrev">&lt;</a-button>
        <a-button type="link" size="small" @click="handleNext">&gt;</a-button>
      </div>
    </div>
    <div class="years-grid">
      <div v-for="(year, idx) in years" :key="idx"
           class="year-cell"
           :class="{ selected: isSelected(year), 'not-current': !isCurrentDecade(year) }"
           @click="selectYear(year)">
        <div class="year-label">{{ year }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { getYear, subYears, addYears } from 'date-fns'

const props = defineProps<{ currentDate: Date; selectedDate?: Date }>()
const emit = defineEmits(['select', 'update:currentDate'])

const baseYear = computed(() => {
  const y = getYear(props.currentDate)
  return Math.floor(y / 10) * 10
})

const years = computed(() => {
  // 显示12年，前1年，当前十年，后1年
  const start = baseYear.value - 1
  return Array.from({ length: 12 }, (_, i) => start + i)
})

const decadeTitle = computed(() => `${baseYear.value} - ${baseYear.value + 9}`)

function isSelected(year: number) {
  if (!props.selectedDate) return false
  return getYear(props.selectedDate) === year
}

function isCurrentDecade(year: number) {
  return year >= baseYear.value && year < baseYear.value + 10
}

function selectYear(year: number) {
  emit('select', new Date(year, 0, 1))
}

function handlePrev() {
  emit('update:currentDate', subYears(props.currentDate, 10))
}
function handleNext() {
  emit('update:currentDate', addYears(props.currentDate, 10))
}
</script>

<style scoped>
.decade-view {
  padding: 16px 0;
  display: flex;
  flex-direction: column;
  align-items: stretch;
}
.decade-header,
.years-grid {
  width: 264px;
  margin: 0 auto;
}
.decade-header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}
.decade-title {
  font-size: 16px;
  font-weight: 600;
}
.decade-actions {
  display: flex;
  gap: 4px;
}
.years-grid {
  display: grid;
  grid-template-columns: repeat(4, 56px);
  grid-template-rows: repeat(3, 56px);
  gap: 16px;
}
.year-cell {
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 50%;
  cursor: pointer;
  transition: background 0.2s;
  font-size: 16px;
  position: relative;
}
.year-cell.selected {
  background: #1976d2;
  color: #fff;
}
.year-cell.not-current {
  color: #bbb;
}
.year-cell:hover {
  background: #e3f2fd;
}
.year-label {
  font-weight: 500;
}
</style> 