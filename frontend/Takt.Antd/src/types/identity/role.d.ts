import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/**
 * 角色实体
 */
export interface TaktRole extends TaktBaseEntity {
  /** 角色ID */
  roleId: string
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number
  /** 用户数 */
  userCount: number
  /** 角色状态（0正常 1停用） */
  roleStatus: number
}

/**
 * 角色创建参数
 */
export interface TaktRoleCreate {
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number
  /** 用户数 */
  userCount: number
  /** 状态（0正常 1停用） */
  roleStatus: number
  /** 备注 */
  remark?: string
  /** 菜单ID列表 */
  menuIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
}

/**
 * 角色更新参数
 */
export interface TaktRoleUpdate extends TaktRoleCreate {
  /** 角色ID */
  roleId: string
}

/**
 * 角色查询参数
 */
export interface TaktRoleQuery extends TaktPagedQuery {
  /** 角色名称 */
  roleName?: string
  /** 角色标识 */
  roleKey?: string
  /** 状态（0正常 1停用） */
  roleStatus?: number
}

/**
 * 角色分页结果
 */
export interface TaktRolePagedResult extends TaktPagedResult<TaktRole> {}

/**
 * 角色状态更新参数
 */
export interface TaktRoleStatus {
  /** 角色ID */
  roleId: string
  /** 状态（0正常 1停用） */
  roleStatus: number
}

/**
 * 角色导入参数
 */
export interface TaktRoleImport {
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: string
  /** 状态（0正常 1停用） */
  roleStatus: number
}

/**
 * 角色导入模板
 */
export interface TaktRoleTemplate {
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number
  /** 状态（0正常 1停用） */
  roleStatus: number
}

/**
 * 角色导出参数
 */
export interface TaktRoleExport {
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number
  /** 状态（0正常 1停用） */
  roleStatus: number
  /** 创建时间 */
  createTime: string
}

/**
 * 角色DTO
 */
export interface TaktRoleDto {
  /** 角色ID */
  roleId: string
  /** 角色名称 */
  roleName: string
  /** 角色标识 */
  roleKey: string
  /** 排序号 */
  orderNum: number
  /** 数据范围（1：全部数据权限 2：自定数据权限 3：本部门数据权限 4：本部门及以下数据权限 5：仅本人数据权限） */
  dataScope: number
  /** 用户数 */
  userCount: number
  /** 状态（0正常 1停用） */
  roleStatus: number
  /** 备注 */
  remark?: string
  /** 创建时间 */
  createTime: string
  /** 创建者 */
  createBy: string
  /** 更新时间 */
  updateTime: string
  /** 更新者 */
  updateBy: string
  /** 是否删除 */
  isDeleted: number
  /** 删除时间 */
  deleteTime: string
  /** 删除者 */
  deleteBy: string
  /** 菜单ID列表 */
  menuIds?: string[]
  /** 部门ID列表 */
  deptIds?: string[]
}

/**
 * 角色选项
 */
export interface TaktRoleOption {
  /** 标签 */
  label: string;
  /** 值 */
  value: string;
}

/**
 * 角色部门DTO
 */
export interface TaktRoleDeptDto {
  id: string;
  roleId: string;
  deptId: string;
  deptName: string;
  deptCode: string;
  createTime: string;
  createBy: string;
}

/**
 * 角色菜单DTO
 */
export interface TaktRoleMenuDto {
  id: string;
  roleId: string;
  menuId: string;
  menuName: string;
  menuCode: string;
  createTime: string;
  createBy: string;
} 
