import {request} from '@/utils/request'
import type { TaktServerMonitorDto, TaktNetworkDto } from '@/types/realtime/serverMonitor'

/**
 * 获取服务器基本信息
 */
export function getServerInfo() {
  return request<TaktServerMonitorDto>({
    url: '/api/TaktServerMonitor/info',
    method: 'get'
  })
}

/**
 * 获取网络信息
 */
export function getNetworkInfo() {
  return request<TaktNetworkDto[]>({
    url: '/api/TaktServerMonitor/network',
    method: 'get'
  })
} 
