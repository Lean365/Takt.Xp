import { LogLevel } from '@microsoft/signalr'
import moment from 'moment-timezone'

// 自定义日志函数
export const customLogger = (logLevel: LogLevel, message: string) => {
  const time = moment().tz('Asia/Shanghai').format('YYYY-MM-DD HH:mm:ss.SSS ZZ')
  const level = LogLevel[logLevel]
  
  // 移除SignalR内部的时间戳
  const cleanMessage = message.replace(/\[\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z\]\s*/, '')
  
  console.log(`[${time}] [SignalR] ${level}: ${cleanMessage}`)
}

export { LogLevel } 