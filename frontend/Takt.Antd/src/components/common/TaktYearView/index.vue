<template>
  <div class="year-view">
    <div class="year-header">
      <span class="year-title">{{ yearTitle }}</span>
      <div class="year-actions">
        <a-button type="link" size="small" @click="handlePrev">&lt;</a-button>
        <a-button type="link" size="small" @click="handleNext">&gt;</a-button>
      </div>
    </div>
    <div class="months-grid">
      <div v-for="(month, idx) in months" :key="idx"
           class="month-cell"
           :class="{ selected: isSelected(month.value) }"
           @click="selectMonth(month.value)">
        <div class="month-label">{{ month.label }}</div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { format, setMonth, getMonth, getYear, subYears, addYears } from 'date-fns'

const props = defineProps<{ currentDate: Date; selectedDate?: Date }>()
const emit = defineEmits(['select', 'update:currentDate'])

const months = computed(() => {
  const base = new Date(props.currentDate)
  return Array.from({ length: 12 }, (_, i) => ({
    value: setMonth(base, i),
    label: format(setMonth(base, i), 'MMM')
  }))
})

const yearTitle = computed(() => format(props.currentDate, 'yyyy年'))

function isSelected(date: Date) {
  if (!props.selectedDate) return false
  return getYear(date) === getYear(props.selectedDate) && getMonth(date) === getMonth(props.selectedDate)
}

function selectMonth(date: Date) {
  emit('select', date)
}

function handlePrev() {
  emit('update:currentDate', subYears(props.currentDate, 1))
}
function handleNext() {
  emit('update:currentDate', addYears(props.currentDate, 1))
}
</script>

<style scoped>
.year-view {
  padding: 16px 0;
  display: flex;
  flex-direction: column;
  align-items: stretch;
}
.year-header,
.months-grid {
  width: 264px;
  margin: 0 auto;
}
.year-header {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 8px;
}
.year-title {
  font-size: 16px;
  font-weight: 600;
}
.year-actions {
  display: flex;
  gap: 4px;
}
.months-grid {
  display: grid;
  grid-template-columns: repeat(4, 56px);
  grid-template-rows: repeat(3, 56px);
  gap: 16px;
}
.month-cell {
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 50%;
  cursor: pointer;
  transition: background 0.2s;
  font-size: 16px;
  position: relative;
}
.month-cell.selected {
  background: #1976d2;
  color: #fff;
}
.month-cell:hover {
  background: #e3f2fd;
}
.month-label {
  font-weight: 500;
}
</style> 