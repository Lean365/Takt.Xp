//===================================================================
// 项目名 : Lean.Takt
// 文件名 : datetime.ts
// 创建者 : Claude
// 创建时间: 2024-03-20
// 版本号 : v1.0.0
// 描述    : 日期时间工具函数
//===================================================================

import {
  format,
  formatDistance,
  formatRelative,
  isValid,
  parse,
  parseISO,
  isDate,
  startOfDay,
  endOfDay,
  startOfWeek,
  endOfWeek,
  startOfMonth,
  endOfMonth,
  differenceInDays,
  isWeekend as dateFnsIsWeekend,
  isSameDay as dateFnsIsSameDay,
  isWithinInterval,
  addDays,
  eachDayOfInterval,
  getDay,
  addBusinessDays
} from 'date-fns'
import { zhCN, enUS, zhTW, ja, ko, fr, es, ru, arSA } from 'date-fns/locale'
import { useAppStore } from '@/stores/appStore'
import { getCurrentInstance } from 'vue'
import { useI18n } from 'vue-i18n'

// 配置时区
const TIMEZONE_OFFSET = 8 // 北京时间 UTC+8

// 支持的语言类型
type SupportedLocale = 'zh-cn' | 'zh-tw' | 'ja' | 'ko' | 'en' | 'fr' | 'es' | 'ru' | 'ar'

// 语言包映射
const LOCALE_MAP: Record<SupportedLocale, any> = {
  'zh-cn': zhCN,     // 简体中文
  'zh-tw': zhTW,     // 繁体中文
  'ja': ja,          // 日语
  'ko': ko,          // 韩语
  'en': enUS,        // 英语
  'fr': fr,          // 法语
  'es': es,          // 西班牙语
  'ru': ru,          // 俄语
  'ar': arSA,        // 阿拉伯语
}

// 月份名称本地化配置
const MONTH_NAMES: Record<SupportedLocale, string[]> = {
  'zh-cn': ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
  'zh-tw': ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
  'ja': ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],
  'ko': ['1월', '2월', '3월', '4월', '5월', '6월', '7월', '8월', '9월', '10월', '11월', '12월'],
  'en': ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
  'fr': ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
  'es': ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
  'ru': ['Январь', 'Февраль', 'Март', 'Апрель', 'Май', 'Июнь', 'Июль', 'Август', 'Сентябрь', 'Октябрь', 'Ноябрь', 'Декабрь'],
  'ar': ['يناير', 'فبراير', 'مارس', 'أبريل', 'مايو', 'يونيو', 'يوليو', 'أغسطس', 'سبتمبر', 'أكتوبر', 'نوفمبر', 'ديسمبر']
}

// 星期名称本地化配置
const WEEKDAY_NAMES: Record<SupportedLocale, string[]> = {
  'zh-cn': ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
  'zh-tw': ['週日', '週一', '週二', '週三', '週四', '週五', '週六'],
  'ja': ['日曜日', '月曜日', '火曜日', '水曜日', '木曜日', '金曜日', '土曜日'],
  'ko': ['일요일', '월요일', '화요일', '수요일', '목요일', '금요일', '토요일'],
  'en': ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
  'fr': ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi'],
  'es': ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
  'ru': ['Воскресенье', 'Понедельник', 'Вторник', 'Среда', 'Четверг', 'Пятница', 'Суббота'],
  'ar': ['الأحد', 'الاثنين', 'الثلاثاء', 'الأربعاء', 'الخميس', 'الجمعة', 'السبت']
}

// 获取当前语言的 locale 配置
const getLocale = () => {
  const app = getCurrentInstance()
  return app?.appContext.config.globalProperties.$dateLocale || enUS
}

// 获取当前语言的月份名称
const getLocalizedMonthNames = () => {
  const appStore = useAppStore()
  return MONTH_NAMES[appStore.language as SupportedLocale] || MONTH_NAMES['en']
}

// 获取当前语言的星期名称
const getLocalizedWeekdayNames = () => {
  const appStore = useAppStore()
  return WEEKDAY_NAMES[appStore.language as SupportedLocale] || WEEKDAY_NAMES['en']
}

/**
 * 格式化日期
 * @param date - 日期
 * @param formatStr - 格式化字符串
 * @returns 格式化后的日期字符串
 */
export const formatDate = (date: Date | string | number, formatStr: string = 'yyyy-MM-dd'): string => {
  if (!date) return ''
  const dateObj = new Date(date)
  if (!isValid(dateObj)) return ''
  return format(dateObj, formatStr, { locale: getLocale() })
}

/**
 * 格式化相对时间
 * @param date 日期对象或字符串
 * @param baseDate 基准日期，默认为当前时间
 * @returns 相对时间字符串
 */
export function formatRelativeTime(date: Date | string | number, baseDate = new Date()): string {
  if (!date) return ''
  
  try {
    const dateObj = typeof date === 'string' ? parseISO(date) : new Date(date)
    if (!isValid(dateObj)) return ''
    
    return formatDistance(dateObj, baseDate, { 
      locale: getLocale(),
      addSuffix: true 
    })
  } catch (err) {
    const { t } = useI18n()
    console.error(`[${t('common.datetime.relativeTimeFormatError')}]:`, err)
    return ''
  }
}

/**
 * 解析日期字符串
 * @param dateStr 日期字符串
 * @param pattern 格式模式
 * @returns 日期对象
 */
export function parseDate(dateStr: string, pattern = 'yyyy-MM-dd'): Date | null {
  if (!dateStr) return null
  
  try {
    const date = parse(dateStr, pattern, new Date())
    return isValid(date) ? date : null
  } catch (err) {
    const { t } = useI18n()
    console.error(`[${t('common.datetime.parseError')}]:`, err)
    return null
  }
}

/**
 * 格式化日期范围
 * @param start 开始日期
 * @param end 结束日期
 * @param pattern 格式模式
 * @returns 格式化后的日期范围字符串
 */
export function formatDateRange(
  start: Date | string | null,
  end: Date | string | null,
  pattern = 'yyyy-MM-dd'
): string {
  if (!start || !end) return ''
  
  const startStr = formatDate(start, pattern)
  const endStr = formatDate(end, pattern)
  
  if (!startStr || !endStr) return ''
  const { t } = useI18n()
  const separator = t('common.datetime.rangeSeparator')
  return `${startStr}${separator}${endStr}`
}

/**
 * 检查是否为有效日期
 * @param date 要检查的日期
 * @returns 是否为有效日期
 */
export function isValidDate(date: any): boolean {
  return isDate(date) && isValid(date)
}

/**
 * 获取日期的开始时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getStartOfDay = (date?: Date | string | number): Date => {
  return startOfDay(date ? new Date(date) : new Date())
}

/**
 * 获取日期的结束时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getEndOfDay = (date?: Date | string | number): Date => {
  return endOfDay(date ? new Date(date) : new Date())
}

/**
 * 获取本周的开始时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getStartOfWeek = (date?: Date | string | number): Date => {
  return startOfWeek(date ? new Date(date) : new Date(), { locale: getLocale() })
}

/**
 * 获取本周的结束时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getEndOfWeek = (date?: Date | string | number): Date => {
  return endOfWeek(date ? new Date(date) : new Date(), { locale: getLocale() })
}

/**
 * 获取本月的开始时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getStartOfMonth = (date?: Date | string | number): Date => {
  return startOfMonth(date ? new Date(date) : new Date())
}

/**
 * 获取本月的结束时间
 * @param date - 日期
 * @returns Date 对象
 */
export const getEndOfMonth = (date?: Date | string | number): Date => {
  return endOfMonth(date ? new Date(date) : new Date())
}

/**
 * 计算两个日期之间的天数
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns 天数
 */
export const getDaysBetween = (start: Date | string | number, end: Date | string | number): number => {
  return differenceInDays(new Date(end), new Date(start))
}

/**
 * 判断是否为工作日（周一至周五）
 * @param date - 日期
 * @returns 是否为工作日
 */
export const isWorkday = (date?: Date | string | number): boolean => {
  const dateObj = date ? new Date(date) : new Date()
  return !dateFnsIsWeekend(dateObj)
}

/**
 * 判断是否为同一天
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一天
 */
export const isSameDay = (date1: Date | string | number, date2: Date | string | number): boolean => {
  return dateFnsIsSameDay(new Date(date1), new Date(date2))
}

/**
 * 判断日期是否在指定范围内
 * @param date - 要判断的日期
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns 是否在范围内
 */
export const isDateInRange = (
  date: Date | string | number,
  start: Date | string | number,
  end: Date | string | number
): boolean => {
  return isWithinInterval(new Date(date), {
    start: new Date(start),
    end: new Date(end)
  })
}

/**
 * 添加工作日
 * @param date - 起始日期
 * @param days - 要添加的工作日天数
 * @returns Date 对象
 */
export const addWorkdays = (date: Date | string | number, days: number): Date => {
  return addBusinessDays(new Date(date), days)
}

/**
 * 获取日期范围内的所有日期
 * @param start - 开始日期
 * @param end - 结束日期
 * @returns Date 对象数组
 */
export const getDatesBetween = (start: Date | string | number, end: Date | string | number): Date[] => {
  return eachDayOfInterval({
    start: new Date(start),
    end: new Date(end)
  })
}

/**
 * 格式化日期时间
 * @param time 日期时间
 * @param pattern 格式化模式，默认为 yyyy-MM-dd HH:mm:ss
 * @returns 格式化后的日期时间字符串
 */
export function formatDateTime(time?: string | number | Date, pattern = 'yyyy-MM-dd HH:mm:ss'): string {
  if (!time) {
    return ''
  }
  try {
    const date = new Date(time)
    return format(date, pattern, { locale: getLocale() })
  } catch (err) {
    const { t } = useI18n()
    console.error(`[${t('common.datetime.formatError')}]:`, err)
    return ''
  }
}

/**
 * 格式化时间
 * @param time 时间
 * @param pattern 格式化模式，默认为 HH:mm:ss
 * @returns 格式化后的时间字符串
 */
export function formatTime(time?: string | number | Date, pattern = 'HH:mm:ss'): string {
  if (!time) {
    return ''
  }
  return format(new Date(time), pattern, { locale: getLocale() })
}

/**
 * 获取今天的开始时间
 * @returns Date 对象
 */
export const getTodayStart = (): Date => {
  return startOfDay(new Date())
}

/**
 * 获取今天的结束时间
 * @returns Date 对象
 */
export const getTodayEnd = (): Date => {
  return endOfDay(new Date())
}

/**
 * 获取昨天的开始时间
 * @returns Date 对象
 */
export const getYesterdayStart = (): Date => {
  return startOfDay(new Date().setDate(new Date().getDate() - 1))
}

/**
 * 获取昨天的结束时间
 * @returns Date 对象
 */
export const getYesterdayEnd = (): Date => {
  return endOfDay(new Date().setDate(new Date().getDate() - 1))
}

/**
 * 获取本周的开始时间
 * @returns Date 对象
 */
export const getThisWeekStart = (): Date => {
  return startOfWeek(new Date(), { locale: getLocale() })
}

/**
 * 获取本周的结束时间
 * @returns Date 对象
 */
export const getThisWeekEnd = (): Date => {
  return endOfWeek(new Date(), { locale: getLocale() })
}

/**
 * 获取上周的开始时间
 * @returns Date 对象
 */
export const getLastWeekStart = (): Date => {
  return startOfWeek(new Date().setDate(new Date().getDate() - 7), { locale: getLocale() })
}

/**
 * 获取上周的结束时间
 * @returns Date 对象
 */
export const getLastWeekEnd = (): Date => {
  return endOfWeek(new Date().setDate(new Date().getDate() - 7), { locale: getLocale() })
}

/**
 * 获取本月的开始时间
 * @returns Date 对象
 */
export const getThisMonthStart = (): Date => {
  return startOfMonth(new Date())
}

/**
 * 获取本月的结束时间
 * @returns Date 对象
 */
export const getThisMonthEnd = (): Date => {
  return endOfMonth(new Date())
}

/**
 * 获取上月的开始时间
 * @returns Date 对象
 */
export const getLastMonthStart = (): Date => {
  return startOfMonth(new Date().setMonth(new Date().getMonth() - 1))
}

/**
 * 获取上月的结束时间
 * @returns Date 对象
 */
export const getLastMonthEnd = (): Date => {
  return endOfMonth(new Date().setMonth(new Date().getMonth() - 1))
}

/**
 * 获取最近一周的开始时间
 * @returns Date 对象
 */
export const getLastWeekRangeStart = (): Date => {
  return startOfDay(new Date().setDate(new Date().getDate() - 7))
}

/**
 * 获取最近一周的结束时间
 * @returns Date 对象
 */
export const getLastWeekRangeEnd = (): Date => {
  return endOfDay(new Date())
}

/**
 * 获取最近一月的开始时间
 * @returns Date 对象
 */
export const getLastMonthRangeStart = (): Date => {
  const now = new Date()
  const lastMonth = new Date(now.getFullYear(), now.getMonth() - 1, 1)
  return startOfDay(lastMonth)
}

/**
 * 获取最近一月的结束时间
 * @returns Date 对象
 */
export const getLastMonthRangeEnd = (): Date => {
  const now = new Date()
  const lastMonth = new Date(now.getFullYear(), now.getMonth() - 1, getDaysInMonth(now))
  return endOfDay(lastMonth)
}

/**
 * 获取最近三月的开始时间
 * @returns Date 对象
 */
export const getLastThreeMonthsStart = (): Date => {
  const now = new Date()
  return startOfDay(new Date(now.getFullYear(), now.getMonth() - 3, 1))
}

/**
 * 获取最近三月的结束时间
 * @returns Date 对象
 */
export const getLastThreeMonthsEnd = (): Date => {
  return endOfDay(new Date(new Date().getFullYear(), new Date().getMonth(), 0))
}

/**
 * 获取本季度的开始时间
 * @returns Date 对象
 */
export const getThisQuarterStart = (): Date => {
  return startOfDay(new Date(new Date().getFullYear(), Math.floor(new Date().getMonth() / 3) * 3, 1))
}

/**
 * 获取本季度的结束时间
 * @returns Date 对象
 */
export const getThisQuarterEnd = (): Date => {
  return endOfDay(new Date(new Date().getFullYear(), Math.floor(new Date().getMonth() / 3) * 3 + 2, 31))
}

/**
 * 获取上季度的开始时间
 * @returns Date 对象
 */
export const getLastQuarterStart = (): Date => {
  return startOfDay(new Date(new Date().getFullYear(), Math.floor(new Date().getMonth() / 3) * 3 - 3, 1))
}

/**
 * 获取上季度的结束时间
 * @returns Date 对象
 */
export const getLastQuarterEnd = (): Date => {
  return endOfDay(new Date(new Date().getFullYear(), Math.floor(new Date().getMonth() / 3) * 3 + 2, 31))
}

/**
 * 获取本年的开始时间
 * @returns Date 对象
 */
export const getThisYearStart = (): Date => {
  return startOfDay(new Date(new Date().getFullYear(), 0, 1))
}

/**
 * 获取本年的结束时间
 * @returns Date 对象
 */
export const getThisYearEnd = (): Date => {
  return endOfDay(new Date(new Date().getFullYear(), 11, 31))
}

/**
 * 获取上年的开始时间
 * @returns Date 对象
 */
export const getLastYearStart = (): Date => {
  return startOfDay(new Date(new Date().getFullYear() - 1, 0, 1))
}

/**
 * 获取上年的结束时间
 * @returns Date 对象
 */
export const getLastYearEnd = (): Date => {
  return endOfDay(new Date(new Date().getFullYear() - 1, 11, 31))
}

/**
 * 获取指定日期范围
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns [开始时间, 结束时间]
 */
export const getDateRange = (start: Date | string | number, end: Date | string | number): [Date, Date] => {
  return [new Date(start), new Date(end)]
}

/**
 * 获取两个日期之间的月数
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns 月数
 */
export const getMonthsBetween = (start: Date | string | number, end: Date | string | number): number => {
  const startDate = new Date(start)
  const endDate = new Date(end)
  return (endDate.getFullYear() - startDate.getFullYear()) * 12 + (endDate.getMonth() - startDate.getMonth())
}

/**
 * 获取两个日期之间的年数
 * @param start - 开始时间
 * @param end - 结束时间
 * @returns 年数
 */
export const getYearsBetween = (start: Date | string | number, end: Date | string | number): number => {
  const startDate = new Date(start)
  const endDate = new Date(end)
  return endDate.getFullYear() - startDate.getFullYear()
}

/**
 * 判断是否为同一月
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一月
 */
export const isSameMonth = (date1: Date | string | number, date2: Date | string | number): boolean => {
  const startDate = new Date(date1)
  const endDate = new Date(date2)
  return startDate.getFullYear() === endDate.getFullYear() && startDate.getMonth() === endDate.getMonth()
}

/**
 * 判断是否为同一年
 * @param date1 - 日期1
 * @param date2 - 日期2
 * @returns 是否为同一年
 */
export const isSameYear = (date1: Date | string | number, date2: Date | string | number): boolean => {
  const startDate = new Date(date1)
  const endDate = new Date(date2)
  return startDate.getFullYear() === endDate.getFullYear()
}

/**
 * 判断是否为周末
 * @param date - 日期
 * @returns 是否为周末
 */
export const isWeekend = (date: Date | string | number): boolean => {
  const dateObj = new Date(date)
  return dateFnsIsWeekend(dateObj)
}

/**
 * 获取月份中的天数
 * @param date - 日期
 * @returns 天数
 */
export const getDaysInMonth = (date: Date | string | number): number => {
  const dateObj = new Date(date)
  return new Date(dateObj.getFullYear(), dateObj.getMonth() + 1, 0).getDate()
}

/**
 * 获取日期所在季度的第一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getQuarterStart = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return startOfDay(new Date(dateObj.getFullYear(), Math.floor(dateObj.getMonth() / 3) * 3, 1))
}

/**
 * 获取日期所在季度的最后一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getQuarterEnd = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return endOfDay(new Date(dateObj.getFullYear(), Math.floor(dateObj.getMonth() / 3) * 3 + 2, 31))
}

/**
 * 获取日期所在周的第一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getWeekStart = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return startOfDay(dateObj.setDate(dateObj.getDate() - dateObj.getDay()))
}

/**
 * 获取日期所在周的最后一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getWeekEnd = (date: Date | string | number): Date => {
 const dateObj = new Date(date)
  return endOfDay(dateObj.setDate(dateObj.getDate() + (6 - dateObj.getDay())))
}

/**
 * 获取日期所在月的第一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getMonthStart = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return startOfDay(new Date(dateObj.getFullYear(), dateObj.getMonth(), 1))
}

/**
 * 获取日期所在月的最后一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getMonthEnd = (date: Date | string | number): Date => {
 const dateObj = new Date(date)
  return endOfDay(new Date(dateObj.getFullYear(), dateObj.getMonth() + 1, 0))
}

/**
 * 获取日期所在年的第一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getYearStart = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return startOfDay(new Date(dateObj.getFullYear(), 0, 1))
}

/**
 * 获取日期所在年的最后一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getYearEnd = (date: Date | string | number): Date => {
 const dateObj = new Date(date)
  return endOfDay(new Date(dateObj.getFullYear(), 11, 31))
}

/**
 * 获取相对时间描述（例如：刚刚、1分钟前等）
 * @param date - 日期
 * @returns 相对时间描述
 */
export const getRelativeTime = (date: Date | string | number): string => {
  const dateObj = typeof date === 'string' ? new Date(date) : date
  return formatDistance(dateObj, new Date(), { locale: getLocale(), addSuffix: true })
}

/**
 * 获取指定月份的所有日期
 * @param year - 年份
 * @param month - 月份（1-12）
 * @returns 日期数组
 */
export const getDatesInMonth = (year: number, month: number): Date[] => {
  const start = new Date(year, month - 1, 1)
  const end = new Date(year, month, 0)
  return getDatesBetween(start, end)
}

/**
 * 获取指定年份的所有月份的第一天
 * @param year - 年份
 * @returns 日期数组
 */
export const getMonthsInYear = (year: number): Date[] => {
  const months: Date[] = []
  for (let i = 0; i < 12; i++) {
    months.push(new Date(year, i, 1))
  }
  return months
}

/**
 * 获取指定日期的农历信息
 * 注意：需要额外安装 dayjs/plugin/lunar 插件
 * @param date - 日期
 * @returns 农历日期字符串
 */
export const getLunarDate = (date: Date | string | number): string => {
  // TODO: 实现农历转换
  return ''
}

/**
 * 获取指定日期是当年的第几周
 * @param date - 日期
 * @returns 周数
 */
export const getWeekOfYear = (date: Date | string | number): number => {
  const dateObj = new Date(date)
  const startOfYear = new Date(dateObj.getFullYear(), 0, 1)
  const dayOfYear = Math.floor((dateObj.getTime() - startOfYear.getTime()) / (24 * 60 * 60 * 1000))
  const weekOfYear = Math.ceil((dayOfYear + ((startOfYear.getDay() + 1) % 7)) / 7)
  return weekOfYear
}

/**
 * 获取指定日期是当月的第几周
 * @param date - 日期
 * @returns 周数
 */
export const getWeekOfMonth = (date: Date | string | number): number => {
  const dateObj = new Date(date)
  const startOfMonth = new Date(dateObj.getFullYear(), dateObj.getMonth(), 1)
  const dayOfMonth = Math.floor((dateObj.getTime() - startOfMonth.getTime()) / (24 * 60 * 60 * 1000))
  const weekOfMonth = Math.ceil((dayOfMonth + ((startOfMonth.getDay() + 1) % 7)) / 7)
  return weekOfMonth
}

/**
 * 获取指定日期的星期名称
 * @param date - 日期
 * @returns 星期名称
 */
export const getWeekdayName = (date: Date | string | number): string => {
 const dateObj = new Date(date)
  return getLocalizedWeekdayNames()[dateObj.getDay()]
}

/**
 * 获取指定日期的月份名称
 * @param date - 日期
 * @returns 月份名称
 */
export const getMonthName = (date: Date | string | number): string => {
  const dateObj = new Date(date)
  return getLocalizedMonthNames()[dateObj.getMonth()]
}

/**
 * 获取指定日期的下一个工作日
 * @param date - 日期
 * @returns Date 对象
 */
export const getNextWorkday = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return addBusinessDays(dateObj, 1)
}

/**
 * 获取指定日期的上一个工作日
 * @param date - 日期
 * @returns Date 对象
 */
export const getPreviousWorkday = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return addBusinessDays(dateObj, -1)
}

/**
 * 获取指定日期所在月份的第一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getFirstDayOfMonth = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return new Date(dateObj.getFullYear(), dateObj.getMonth(), 1)
}

/**
 * 获取指定日期所在月份的最后一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getLastDayOfMonth = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return new Date(dateObj.getFullYear(), dateObj.getMonth() + 1, 0)
}

/**
 * 获取指定日期的下一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getNextDay = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return addDays(dateObj, 1)
}

/**
 * 获取指定日期的前一天
 * @param date - 日期
 * @returns Date 对象
 */
export const getPreviousDay = (date: Date | string | number): Date => {
  const dateObj = new Date(date)
  return addDays(dateObj, -1)
}

export default {
  getTodayStart,
  getTodayEnd,
  getYesterdayStart,
  getYesterdayEnd,
  getThisWeekStart,
  getThisWeekEnd,
  getLastWeekStart,
  getLastWeekEnd,
  getThisMonthStart,
  getThisMonthEnd,
  getLastMonthStart,
  getLastMonthEnd,
  getLastWeekRangeStart,
  getLastWeekRangeEnd,
  getLastMonthRangeStart,
  getLastMonthRangeEnd,
  getLastThreeMonthsStart,
  getLastThreeMonthsEnd,
  getThisQuarterStart,
  getThisQuarterEnd,
  getLastQuarterStart,
  getLastQuarterEnd,
  getThisYearStart,
  getThisYearEnd,
  getLastYearStart,
  getLastYearEnd,
  getDateRange,
  getDaysBetween,
  getMonthsBetween,
  getYearsBetween,
  isSameDay,
  isSameMonth,
  isSameYear,
  isWorkday,
  isWeekend,
  getDaysInMonth,
  getQuarterStart,
  getQuarterEnd,
  getWeekStart,
  getWeekEnd,
  getMonthStart,
  getMonthEnd,
  getYearStart,
  getYearEnd,
  formatDateTime,
  formatTime,
  formatDate,
  parseDate,
  getRelativeTime,
  isDateInRange,
  getDatesBetween,
  getDatesInMonth,
  getMonthsInYear,
  getLunarDate,
  getWeekOfYear,
  getWeekOfMonth,
  getWeekdayName,
  getMonthName,
  formatRelativeTime,
  formatDateRange,
  isValidDate,
  getNextWorkday,
  getPreviousWorkday,
  getFirstDayOfMonth,
  getLastDayOfMonth,
  getNextDay,
  getPreviousDay
} 
