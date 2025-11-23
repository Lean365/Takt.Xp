import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 部门对象
 */
export interface TaktDept extends TaktBaseEntity {
  /** 部门ID */
  deptId: string
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: string
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader?: string
  /** 联系电话 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 用户数 */
  userCount: number
  /** 部门状态（0正常 1停用） */
  deptStatus: number
  /** 子部门列表 */
  children?: TaktDept[]
}

/**
 * 部门查询参数
 */
export interface TaktDeptQuery extends TaktPagedQuery {
  deptName?: string
  parentId?: string
  deptStatus?: number
}

/**
 * 创建部门参数
 */
export interface TaktDeptCreate {
  /** 部门名称 */
  deptName: string
  /** 父部门ID */
  parentId: string
  /** 显示顺序 */
  orderNum: number
  /** 负责人 */
  leader?: string
  /** 联系电话 */
  phone?: string
  /** 邮箱 */
  email?: string
  /** 用户数 */
  userCount: number
  /** 部门状态（0正常 1停用） */
  deptStatus: number
  /** 备注 */
  remark?: string
}

/**
 * 更新部门参数
 */
export interface TaktDeptUpdate extends TaktDeptCreate {
  /** 部门ID */
  deptId: string
}

/**
 * 部门状态更新参数
 */
export interface TaktDeptStatus {
  /** 部门ID */
  deptId: string
  /** 状态（0正常 1停用） */
  deptStatus: number
}

/**
 * 部门选项
 */
export interface TaktDeptOption {
  label: string;
  value: string;
  children?: TaktDeptOption[];
} 

/**
 * 部门分页结果
 */
export type TaktDeptPageResult = TaktPagedResult<TaktDept>


