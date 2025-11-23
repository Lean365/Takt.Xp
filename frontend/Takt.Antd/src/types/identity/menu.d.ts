import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 菜单类型枚举
 */
export enum TaktMenuType {
  /** 目录 */
  Directory = 0,
  /** 菜单 */
  Menu = 1,
  /** 按钮 */
  Button = 2
}

/**
 * 菜单对象
 */
export interface TaktMenu extends TaktBaseEntity {
  /** 菜单ID */
  menuId: string
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey?: string
  /** 菜单图标 */
  icon?: string
  /** 父菜单ID */
  parentId: string
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component?: string
  /** 路由参数 */
  queryParams?: string
  /** 是否为外链（0否 1是） */
  isExternal: number
  /** 是否缓存（0否 1是） */
  isCache: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType: number
  /** 菜单状态（0显示 1隐藏） */
  visible: number
  /** 权限标识 */
  perms?: string  
  /** 菜单状态（0正常 1停用） */
  menuStatus: number
  /** 子菜单 */
  children?: TaktMenu[]
}

/**
 * 菜单查询参数
 */
export interface TaktMenuQuery extends TaktPagedQuery {
  /** 菜单名称 */
  menuName?: string
  /** 菜单状态（0正常 1停用） */
  menuStatus?: number
  /** 菜单状态（0显示 1隐藏） */
  visible?: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType?: number
  /** 是否为外链（0否 1是） */
  isExternal?: number
  /** 是否缓存（0否 1是） */
  isCache?: number

}

/**
 * 创建菜单参数
 */
export interface TaktMenuCreate {
  /** 菜单名称 */
  menuName: string
  /** 翻译Key */
  transKey?: string
  /** 父菜单ID */
  parentId: string
  /** 显示顺序 */
  orderNum: number
  /** 路由地址 */
  path: string
  /** 组件路径 */
  component?: string
  /** 路由参数 */
  queryParams?: string
  /** 是否为外链（0否 1是） */
  isExternal: number
  /** 是否缓存（0否 1是） */
  isCache: number
  /** 菜单类型（0目录 1菜单 2按钮） */
  menuType: number
  /** 菜单状态（0显示 1隐藏） */
  visible: number
  /** 菜单状态（0正常 1停用） */
  menuStatus: number
  /** 权限标识 */
  perms?: string
  /** 菜单图标 */
  icon?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新菜单参数
 */
export interface TaktMenuUpdate extends TaktMenuCreate {
  /** 菜单ID */
  menuId: string
}

/**
 * 菜单状态更新参数
 */
export interface TaktMenuStatus {
  /** 菜单ID */
  menuId: string
  /** 状态（0正常 1停用） */
  menuStatus: number
}

/**
 * 菜单排序更新参数
 */
export interface TaktMenuOrder {
  /** 菜单ID */
  menuId: string
  /** 显示顺序 */
  orderNum: number
}

/**
 * 菜单分页结果
 */
export type TaktMenuPageResult = TaktPagedResult<TaktMenu> 
