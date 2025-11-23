<template>
  <div class="day-content">
    <h2>{{ format(currentDate, 'yyyy年MM月dd日') }}</h2>
    <div>这里是当天的详细内容（如事件、日程等）</div>
  </div>
</template>

<script lang="ts" setup>
import { defineProps } from 'vue'
import { format } from 'date-fns'

const props = defineProps<{ currentDate: Date }>()
</script>

<style scoped>
.day-content {
  padding: 24px;
  text-align: center;
}
</style> 