// 导航模式类型
export type NavMode = 'side' | 'top' | 'mix'

// 系统设置状态接口
export interface SettingsState {
  navMode: NavMode
  fixedHeader: boolean
  showBreadcrumb: boolean
  showTabs: boolean
  showFooter: boolean
  autoHideHeader: boolean
  weakMode: boolean
}

// 通知设置接口
export interface NotificationSettings {
  system: boolean
  task: boolean
  message: boolean
}

// 通知项目接口
export interface NotificationItem {
  id: string
  title: string
  content: string
  type: 'system' | 'task' | 'message'
  status: 'read' | 'unread'
  createTime: string
}

/** 通知项目类型 */
export interface NotificationItemType {
  /** 唯一标识符 */
  id: string
  /** 标题 */
  title: string
  /** 内容 */
  content: string
  /** 创建时间 */
  createTime: string
  /** 状态 */
  status: 'read' | 'unread'
  /** 类型 */
  type: 'online' | 'mail' | 'notice'
  /** 额外数据 */
  data?: Record<string, any>
} 