<template>
  <div class="Takt-cron">
    <el-input v-model="cronValue" placeholder="请输入 cron 表达式" @change="handleChange">
      <template #append>
        <el-button @click="showCronDialog">可视化编辑</el-button>
      </template>
    </el-input>

    <el-dialog v-model="dialogVisible" title="Cron 表达式编辑器" width="800px">
      <vcrontab-3 v-model="cronValue" @change="handleCronChange" />
      <template #footer>
        <span class="dialog-footer">
          <el-button @click="dialogVisible = false">取消</el-button>
          <el-button type="primary" @click="handleConfirm">确定</el-button>
        </span>
      </template>
    </el-dialog>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import Vcrontab3 from 'vcrontab-3'
import 'vcrontab-3/dist/vcrontab.css'

const props = defineProps<{
  modelValue: string
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: string): void
}>()

const cronValue = ref(props.modelValue)
const dialogVisible = ref(false)

const showCronDialog = () => {
  dialogVisible.value = true
}

const handleChange = (value: string) => {
  emit('update:modelValue', value)
}

const handleCronChange = (value: string) => {
  cronValue.value = value
}

const handleConfirm = () => {
  emit('update:modelValue', cronValue.value)
  dialogVisible.value = false
}
</script>

<style scoped>
.Takt-cron {
  width: 100%;
}
</style> 
