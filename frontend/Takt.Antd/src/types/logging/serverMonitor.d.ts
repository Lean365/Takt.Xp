/**
 * 服务器监控信息
 */
declare module '@/types/audit/serverMonitor' {
  export interface TaktServerMonitorDto {
    // CPU信息
    cpuUsage: number
    
    // 内存信息
    totalMemory: number
    usedMemory: number
    memoryUsage: number
    
    // 磁盘信息
    totalDiskSpace: number
    usedDiskSpace: number
    diskUsage: number
    
    // 系统信息
    osName: string
    osArchitecture: string
    osVersion: string
    processorName: string
    processorCount: number
    systemStartTime: Date
    systemUptime: number
    
    // .NET运行时信息
    dotNetRuntimeVersion: string
    clrVersion: string
    dotNetRuntimeDirectory: string
  }

  export interface TaktProcessDto {
    processId: number
    processName: string
    description: string
    cpuUsage: number
    memoryUsage: number
    startTime: Date
    runningTime: number
  }

  export interface TaktNetworkDto {
    adapterName: string
    macAddress: string
    ipAddress: string
    ipLocation: string
    sendRate: number
    receiveRate: number
  }

  export interface TaktServiceDto {
    serviceName: string
    displayName: string
    serviceType: string
    status: number
    startType: string
    account: string
  }
} 
