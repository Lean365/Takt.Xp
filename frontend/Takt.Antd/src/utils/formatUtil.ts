import { format } from 'date-fns'

/**
 * 格式化日期时间
 * @param date 日期对象或日期字符串
 * @param formatStr 格式化字符串，默认为 'yyyy-MM-dd HH:mm:ss'
 * @returns 格式化后的日期字符串
 */
export function formatDateTime(date: Date | string, formatStr = 'yyyy-MM-dd HH:mm:ss'): string {
  return format(new Date(date), formatStr)
}

/**
 * 格式化文件大小
 * @param bytes 文件大小（字节）
 * @returns 格式化后的文件大小字符串
 */
export function formatFileSize(bytes: number): string {
  if (bytes === 0) return '0 B'
  const k = 1024
  const sizes = ['B', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB']
  const i = Math.floor(Math.log(bytes) / Math.log(k))
  return `${parseFloat((bytes / Math.pow(k, i)).toFixed(2))} ${sizes[i]}`
}

/**
 * 格式化时长
 * @param minutes 分钟数
 * @returns 格式化后的时长字符串
 */
export function formatDuration(minutes: number): string {
  const days = Math.floor(minutes / (24 * 60))
  const hours = Math.floor((minutes % (24 * 60)) / 60)
  const mins = Math.floor(minutes % 60)
  
  const parts = []
  if (days > 0) parts.push(`${days}天`)
  if (hours > 0) parts.push(`${hours}小时`)
  if (mins > 0) parts.push(`${mins}分钟`)
  
  return parts.length > 0 ? parts.join(' ') : '0分钟'
} 