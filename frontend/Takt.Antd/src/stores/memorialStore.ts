import { defineStore } from 'pinia'
import { ref, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { SolarDay } from 'tyme4ts'

interface HolidayTheme {
  id: string
  name: string
  icon: string
  theme: {
    token: {
      colorPrimary: string
      colorBgContainer?: string
      colorBgLayout?: string
      colorText?: string
      colorTextSecondary?: string
      colorBorder?: string
      colorSplit?: string
      filter?: string
      filter_css?: string
    }
  }
  region: string
}

interface HolidayThemes {
  [key: string]: HolidayTheme
}

interface Holiday {
  id: string
  name: string
  region: string
  icon: string
  date: string
}

type HolidayId = 
  | 'test'
  | 'chineseNewYear'
  | 'qingming'
  | 'laborDay'
  | 'dragonBoat'
  | 'midAutumn'
  | 'nationalDay'
  | 'newYear'
  | 'valentines'
  | 'easter'
  | 'halloween'
  | 'thanksgiving'
  | 'christmas'
  | 'springFestival'
  | 'lanternFestival'
  | 'chuseok'
  | 'shogatsu'
  | 'memorial'

interface HolidayMap {
  [key: string]: Holiday
}

// 计算复活节日期
const getEasterDate = (year: number): Date => {
  const a = year % 19
  const b = Math.floor(year / 100)
  const c = year % 100
  const d = Math.floor(b / 4)
  const e = b % 4
  const f = Math.floor((b + 8) / 25)
  const g = Math.floor((b - f + 1) / 3)
  const h = (19 * a + b - d - g + 15) % 30
  const i = Math.floor(c / 4)
  const k = c % 4
  const l = (32 + 2 * e + 2 * i - h - k) % 7
  const m = Math.floor((a + 11 * h + 22 * l) / 451)
  const month = Math.floor((h + l - 7 * m + 114) / 31)
  const day = ((h + l - 7 * m + 114) % 31) + 1
  return new Date(year, month - 1, day)
}

// 计算农历节日日期
const getLunarFestivalDate = (year: number, month: number, day: number): Date => {
  // 使用tyme4ts计算农历日期
  const solar = SolarDay.fromYmd(year, month, day)
  return new Date(solar.getYear(), solar.getMonth() - 1, solar.getDay())
}

// 计算农历春节日期
const getChineseNewYearDate = (year: number): Date => {
  return getLunarFestivalDate(year, 1, 1)
}

// 计算农历端午节日期
const getDragonBoatDate = (year: number): Date => {
  return getLunarFestivalDate(year, 5, 5)
}

// 计算农历中秋节日期
const getMidAutumnDate = (year: number): Date => {
  return getLunarFestivalDate(year, 8, 15)
}

// 默认主题配置
const defaultTheme = {
  token: {
    colorPrimary: '#1890ff',
    colorBgContainer: '#ffffff',
    colorBgLayout: '#f0f2f5',
    colorText: 'rgba(0, 0, 0, 0.88)',
    colorTextSecondary: 'rgba(0, 0, 0, 0.45)',
    colorBorder: '#d9d9d9',
    colorSplit: '#f0f0f0'
  }
}

export const useMemorialStore = defineStore('memorial', () => {
  const { t } = useI18n()
  
  // 状态
  const isMemorialMode = ref(false)
  const isAutoMode = ref(false)
  const currentHoliday = ref<HolidayId | null>(null)
  const customTheme = ref<HolidayTheme | null>(null)

  // 纪念模式主题配置
  const memorialTheme = computed(() => ({
    token: {
      colorPrimary: '#8c8c8c',
      colorBgContainer: '#262626',
      colorBgLayout: '#1f1f1f',
      colorText: '#d9d9d9',
      colorTextSecondary: '#8c8c8c',
      colorBorder: '#434343',
      colorSplit: '#434343',
      filter: 'grayscale(100%)',
      filter_css: 'grayscale(100%) contrast(90%) brightness(90%)'
    }
  }))

  // 节日主题配置
  const holidayThemes = computed<HolidayThemes>(() => ({
    // 测试主题
    test: {
      id: 'test',
      name: '测试主题',
      icon: '🔴',
      region: 'TEST',
      theme: {
        token: {
          colorPrimary: '#FF0000',      // 主题主色调（红色）
          colorBgContainer: '#fff1f0',  // 组件容器背景色（浅红色）
          colorBgLayout: '#000000',     // 整个页面背景色（黑色）
          colorText: '#BE1205',         // 主要文字颜色（深红色）
          colorTextSecondary: '#E54335',// 次要文字颜色（红色）
          colorBorder: '#ffccc7',       // 边框颜色（浅红色）
          colorSplit: '#ffd8bf'         // 分割线颜色（浅红色）
        }
      }
    },
    // 中国节日
    springFestival: {
      id: 'springFestival',
      name: t('memorial.chineseNewYear.name'),
      icon: '🏮',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#f5222d',
          colorBgContainer: '#fff1f0',
          colorBgLayout: '#fff1f0',
          colorText: '#cf1322',
          colorTextSecondary: '#f5222d',
          colorBorder: '#ffa39e',
          colorSplit: '#ffd6e7'
        }
      }
    },
    lanternFestival: {
      id: 'lanternFestival',
      name: t('memorial.lanternFestival.name'),
      icon: '🏮',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#faad14',
          colorBgContainer: '#fffbe6',
          colorBgLayout: '#fffbe6',
          colorText: '#d48806',
          colorTextSecondary: '#faad14',
          colorBorder: '#ffe58f',
          colorSplit: '#fff1b8'
        }
      }
    },
    qingming: {
      id: 'qingming',
      name: t('memorial.qingming.name'),
      icon: '🌱',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#52c41a',
          colorBgContainer: '#f6ffed',
          colorBgLayout: '#f6ffed',
          colorText: '#389e0d',
          colorTextSecondary: '#52c41a',
          colorBorder: '#b7eb8f',
          colorSplit: '#d9f7be'
        }
      }
    },
    laborDay: {
      id: 'laborDay',
      name: t('memorial.laborDay.name'),
      icon: '⚒️',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#1890ff',
          colorBgContainer: '#e6f7ff',
          colorBgLayout: '#e6f7ff',
          colorText: '#096dd9',
          colorTextSecondary: '#1890ff',
          colorBorder: '#91d5ff',
          colorSplit: '#bae7ff'
        }
      }
    },
    dragonBoat: {
      id: 'dragonBoat',
      name: t('memorial.dragonBoat.name'),
      icon: '🚣',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#52c41a',
          colorBgContainer: '#f6ffed',
          colorBgLayout: '#f6ffed',
          colorText: '#389e0d',
          colorTextSecondary: '#52c41a',
          colorBorder: '#b7eb8f',
          colorSplit: '#d9f7be'
        }
      }
    },
    midAutumn: {
      id: 'midAutumn',
      name: t('memorial.midAutumn.name'),
      icon: '🌕',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#faad14',
          colorBgContainer: '#fffbe6',
          colorBgLayout: '#fff7e6',
          colorText: '#d48806',
          colorTextSecondary: '#faad14',
          colorBorder: '#ffe58f',
          colorSplit: '#fff1b8'
        }
      }
    },
    nationalDay: {
      id: 'nationalDay',
      name: t('memorial.nationalDay.name'),
      icon: '🇨🇳',
      region: 'CN',
      theme: {
        token: {
          colorPrimary: '#f5222d',
          colorBgContainer: '#fff1f0',
          colorBgLayout: '#fff1f0',
          colorText: '#cf1322',
          colorTextSecondary: '#f5222d',
          colorBorder: '#ffa39e',
          colorSplit: '#ffd6e7'
        }
      }
    },
    // 日本节日
    shogatsu: {
      id: 'shogatsu',
      name: t('memorial.shogatsu.name'),
      icon: '🎍',
      region: 'JP',
      theme: {
        token: {
          colorPrimary: '#722ed1',
          colorBgContainer: '#f9f0ff',
          colorBgLayout: '#f9f0ff',
          colorText: '#531dab',
          colorTextSecondary: '#722ed1',
          colorBorder: '#d3adf7',
          colorSplit: '#efdbff'
        }
      }
    },
    // 韩国节日
    chuseok: {
      id: 'chuseok',
      name: t('memorial.chuseok.name'),
      icon: '🍁',
      region: 'KR',
      theme: {
        token: {
          colorPrimary: '#eb2f96',
          colorBgContainer: '#fff0f6',
          colorBgLayout: '#fff0f6',
          colorText: '#c41d7f',
          colorTextSecondary: '#eb2f96',
          colorBorder: '#ffadd2',
          colorSplit: '#ffd6e7'
        }
      }
    },
    // 全球节日
    newYear: {
      id: 'newYear',
      name: t('memorial.newYear.name'),
      icon: '🎊',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#1890ff',
          colorBgContainer: '#e6f7ff',
          colorBgLayout: '#e6f7ff',
          colorText: '#096dd9',
          colorTextSecondary: '#1890ff',
          colorBorder: '#91d5ff',
          colorSplit: '#bae7ff'
        }
      }
    },
    valentines: {
      id: 'valentines',
      name: t('memorial.valentines.name'),
      icon: '💝',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#eb2f96',
          colorBgContainer: '#fff0f6',
          colorBgLayout: '#fff0f6',
          colorText: '#c41d7f',
          colorTextSecondary: '#eb2f96',
          colorBorder: '#ffadd2',
          colorSplit: '#ffd6e7'
        }
      }
    },
    easter: {
      id: 'easter',
      name: t('memorial.easter.name'),
      icon: '🥚',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#13c2c2',
          colorBgContainer: '#e6fffb',
          colorBgLayout: '#e6fffb',
          colorText: '#08979c',
          colorTextSecondary: '#13c2c2',
          colorBorder: '#87e8de',
          colorSplit: '#b5f5ec'
        }
      }
    },
    halloween: {
      id: 'halloween',
      name: t('memorial.halloween.name'),
      icon: '🎃',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#fa8c16',
          colorBgContainer: '#fff7e6',
          colorBgLayout: '#fff1f0',
          colorText: '#d46b08',
          colorTextSecondary: '#ffa940',
          colorBorder: '#ffd591',
          colorSplit: '#ffe7ba'
        }
      }
    },
    thanksgiving: {
      id: 'thanksgiving',
      name: t('memorial.thanksgiving.name'),
      icon: '🦃',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#d48806',
          colorBgContainer: '#fffbe6',
          colorBgLayout: '#fffbe6',
          colorText: '#ad6800',
          colorTextSecondary: '#d48806',
          colorBorder: '#ffe58f',
          colorSplit: '#fff1b8'
        }
      }
    },
    christmas: {
      id: 'christmas',
      name: t('memorial.christmas.name'),
      icon: '🎄',
      region: 'GLOBAL',
      theme: {
        token: {
          colorPrimary: '#389e0d',
          colorBgContainer: '#f6ffed',
          colorBgLayout: '#fff7e6',
          colorText: '#237804',
          colorTextSecondary: '#52c41a',
          colorBorder: '#b7eb8f',
          colorSplit: '#d9f7be'
        }
      }
    }
  }))

  // 计算属性
  const allHolidays = computed<HolidayMap>(() => ({
    test: {
      id: 'test',
      name: '测试主题',
      region: 'TEST',
      icon: '🔴',
      date: '04-06'
    },
    springFestival: {
      id: 'springFestival',
      name: t('memorial.chineseNewYear.name'),
      region: 'CN',
      icon: '🏮',
      date: '02-10'
    },
    lanternFestival: {
      id: 'lanternFestival',
      name: t('memorial.lanternFestival.name'),
      region: 'CN',
      icon: '🏮',
      date: '02-15'
    },
    qingming: {
      id: 'qingming',
      name: t('memorial.qingming.name'),
      region: 'CN',
      icon: '🌱',
      date: '04-05'
    },
    laborDay: {
      id: 'laborDay',
      name: t('memorial.laborDay.name'),
      region: 'CN',
      icon: '⚒️',
      date: '05-01'
    },
    dragonBoat: {
      id: 'dragonBoat',
      name: t('memorial.dragonBoat.name'),
      region: 'CN',
      icon: '🚣',
      date: '06-22'
    },
    midAutumn: {
      id: 'midAutumn',
      name: t('memorial.midAutumn.name'),
      region: 'CN',
      icon: '🌕',
      date: '09-29'
    },
    nationalDay: {
      id: 'nationalDay',
      name: t('memorial.nationalDay.name'),
      region: 'CN',
      icon: '🇨🇳',
      date: '10-01'
    },
    shogatsu: {
      id: 'shogatsu',
      name: t('memorial.shogatsu.name'),
      region: 'JP',
      icon: '🎍',
      date: '01-01'
    },
    chuseok: {
      id: 'chuseok',
      name: t('memorial.chuseok.name'),
      region: 'KR',
      icon: '🍁',
      date: '09-29'
    },
    newYear: {
      id: 'newYear',
      name: t('memorial.newYear.name'),
      region: 'GLOBAL',
      icon: '🎊',
      date: '01-01'
    },
    valentines: {
      id: 'valentines',
      name: t('memorial.valentines.name'),
      region: 'GLOBAL',
      icon: '💝',
      date: '02-14'
    },
    easter: {
      id: 'easter',
      name: t('memorial.easter.name'),
      region: 'GLOBAL',
      icon: '🥚',
      date: '03-31'
    },
    halloween: {
      id: 'halloween',
      name: t('memorial.halloween.name'),
      region: 'GLOBAL',
      icon: '🎃',
      date: '10-31'
    },
    thanksgiving: {
      id: 'thanksgiving',
      name: t('memorial.thanksgiving.name'),
      region: 'GLOBAL',
      icon: '🦃',
      date: '11-28'
    },
    christmas: {
      id: 'christmas',
      name: t('memorial.christmas.name'),
      region: 'GLOBAL',
      icon: '🎄',
      date: '12-25'
    }
  }))

  const currentTheme = computed(() => {
    // console.log('计算当前主题:', {
    //   isMemorialMode: isMemorialMode.value,
    //   currentHoliday: currentHoliday.value,
    //   holidayThemes: holidayThemes.value,
    //   allHolidays: allHolidays.value
    // })
    
    // 纪念模式优先级最高
    if (isMemorialMode.value) {
      // console.log('纪念模式开启，使用纪念主题')
      return memorialTheme.value
    }

    // 检查是否有当前节日
    if (currentHoliday.value) {
      // console.log('使用节日主题:', currentHoliday.value)
      const theme = holidayThemes.value[currentHoliday.value]
      if (theme) {
        // console.log('找到节日主题:', theme)
        return theme.theme
      }
    }

    // 如果没有节日主题，使用默认主题
    //console.log('使用默认主题')
    return defaultTheme
  })

  // 监听主题变化
  watch(currentTheme, (newTheme) => {
    //console.log('主题发生变化:', {
    //  theme: newTheme,
    //  currentHoliday: currentHoliday.value,
    //  isMemorialMode: isMemorialMode.value
    //})
  }, { immediate: true })

  // 方法
  const setMemorialMode = (value: boolean) => {
    isMemorialMode.value = value
  }

  const enableMemorialMode = () => {
    isMemorialMode.value = true
    isAutoMode.value = false
  }

  const disableMemorialMode = () => {
    isMemorialMode.value = false
    isAutoMode.value = true
    checkHolidays()
  }

  const checkHolidays = () => {
    if (!isAutoMode.value) return

    const today = new Date()
    const currentDate = `${String(today.getMonth() + 1).padStart(2, '0')}-${String(today.getDate()).padStart(2, '0')}`
    
    //console.log('检查节日:', {
    //  currentDate,
    //  allHolidays: allHolidays.value,
    //  isAutoMode: isAutoMode.value
    //})

    // 检查所有节假日
    for (const [holidayId, holiday] of Object.entries(allHolidays.value)) {
      // console.log('检查节日:', {
      //   holidayId,
      //   holidayDate: holiday.date,
      //   currentDate,
      //   isMatch: holiday.date === currentDate
      // })
      
      if (holiday.date === currentDate) {
        //console.log('找到匹配的节日:', holidayId)
        currentHoliday.value = holidayId as HolidayId
        return
      }
    }

    //console.log('没有找到匹配的节日')
    currentHoliday.value = null
  }

  const initMemorialMode = () => {
    // 从 localStorage 获取状态
    const state = localStorage.getItem('memorialState')
    if (state) {
      const { isMemorialMode: memorialMode } = JSON.parse(state)
      isMemorialMode.value = memorialMode
      isAutoMode.value = !memorialMode // 如果不在纪念模式，则自动模式开启
    } else {
      // 默认开启自动模式
      isAutoMode.value = true
    }

    // 如果启用了自动模式，检查节日
    if (isAutoMode.value) {
      checkHolidays()
    }
  }

  // 初始化
  initMemorialMode()

  // 每天检查一次节日
  setInterval(checkHolidays, 24 * 60 * 60 * 1000)

  return {
    isMemorialMode,
    isAutoMode,
    currentHoliday,
    allHolidays,
    currentTheme,
    setMemorialMode,
    enableMemorialMode,
    disableMemorialMode,
    initMemorialMode,
    checkHolidays
  }
}) 