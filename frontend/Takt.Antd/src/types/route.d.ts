import type { RouteRecordRaw } from 'vue-router'

interface TaktRouteMeta {
  title?: string
  icon?: string
  hidden?: boolean
  keepAlive?: boolean
  permission?: string
  requiresAuth?: boolean
  menuId?: string
  orderNum?: number
}

export interface TaktRouteRecordRaw {
  path: string
  name?: string | symbol
  redirect?: string
  component?: RouteRecordRaw['component']
  meta?: TaktRouteMeta
  children?: TaktRouteRecordRaw[]
}

declare module 'vue-router' {
  interface RouteMeta {
    title?: string
    icon?: string
    hidden?: boolean
    keepAlive?: boolean
    permission?: string
    requiresAuth?: boolean
    menuId?: string
    orderNum?: number
  }
} 
