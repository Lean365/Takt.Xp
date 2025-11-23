declare module '@/types/realtime/serverMonitor' {
  /** 磁盘信息 */
  export interface TaktDiskInfo {
    /** 驱动器名称 */
    driveName: string
    /** 总空间(字节) */
    totalSpace: number
    /** 已用空间(字节) */
    usedSpace: number
    /** 可用空间(字节) */
    freeSpace: number
    /** 使用率(%) */
    usageRate: number
  }

  /** 服务器监控信息 */
  export interface TaktServerMonitorDto {
    /** CPU使用率(%) */
    cpuUsage: number
    
    /** 总内存(字节) */
    totalMemory: number
    /** 已用内存(字节) */
    usedMemory: number
    /** 内存使用率(%) */
    memoryUsage: number
    
    /** 磁盘信息列表 */
    diskInfos: TaktDiskInfo[]
    
    /** 操作系统名称 */
    osName: string
    /** 系统架构 */
    osArchitecture: string
    /** 系统版本 */
    osVersion: string
    /** 处理器名称 */
    processorName: string
    /** 处理器核心数 */
    processorCount: number
    /** 系统启动时间 */
    systemStartTime: Date
    /** 系统运行时间(天) */
    systemUptime: number
    
    /** .NET运行时版本 */
    dotNetRuntimeVersion: string
    /** CLR版本 */
    clrVersion: string
    /** .NET运行时目录 */
    dotNetRuntimeDirectory: string
  }

  /** 网络接口信息 */
  export interface TaktNetworkDto {
    /** 网卡名称 */
    adapterName: string
    /** MAC地址 */
    macAddress: string
    /** IP地址 */
    ipAddress: string
    /** IP位置 */
    ipLocation: string
    /** 发送速率(KB/s) */
    sendRate: number
    /** 接收速率(KB/s) */
    receiveRate: number
  }
} 
