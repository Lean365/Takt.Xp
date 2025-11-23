import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { TaktRole } from '@/types/identity/role'
import type { TaktPost } from '@/types/identity/post'
import type { TaktDept } from '@/types/identity/dept'
import type { TaktUserRole } from '@/types/identity/userRole'
import type { TaktUserDept } from '@/types/identity/userDept'
import type { TaktUserPost } from '@/types/identity/userPost'

/**
 * 用户实体
 */
export interface TaktUser extends TaktBaseEntity {
  /** 用户ID */
  userId: string
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName?: string
  /** 全名 */
  fullName?: string
  /** 真实姓名 */
  realName?: string
  /** 英文名称 */
  englishName?: string
  /** 用户类型（0系统用户 1普通用户 2管理员） */
  userType: number
  /** 密码 */
  password: string
  /** 盐值 */
  salt: string
  /** 密码迭代次数 */
  iterations: number
  /** 邮箱 */
  email?: string
  /** 手机号 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  userStatus: number
  /** 最后修改密码时间 */
  lastPasswordChangeTime?: string
  /** 锁定结束时间 */
  lockEndTime?: string
  /** 锁定原因 */
  lockReason?: string
  /** 是否锁定（0否 1是） */
  isLock: number
  /** 错误次数限制 */
  errorLimit: number
  /** 登录次数 */
  loginCount: number
  /** 状态变更加载状态 */
  userStatusLoading?: boolean
  /** 锁定状态变更加载状态 */
  lockStatusLoading?: boolean
}

/**
 * 用户查询参数
 */
export interface TaktUserQuery extends TaktPagedQuery {
  /** 用户名 */
  userName?: string
  /** 昵称 */
  nickName?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 邮箱 */
  email?: string
  /** 状态（0正常 1停用） */
  userStatus?: number
  /** 用户类型（0系统用户 1普通用户 2管理员） */
  userType?: number
  /** 性别（0未知 1男 2女） */
  gender?: number
  /** 部门ID */
  deptId?: string
}

/**
 * 用户创建参数
 */
export interface TaktUserCreate {
  /** 用户ID */
  userId: string
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 密码 */
  password: string
  /** 真实姓名 */
  realName: string
  /** 全名 */
  fullName: string
  /** 英文名称 */
  englishName: string
  /** 用户类型（0系统用户 1普通用户 2管理员） */
  userType: number
  /** 邮箱 */
  email?: string
  /** 手机号码 */
  phoneNumber?: string
  /** 性别（0未知 1男 2女） */
  gender: number
  /** 头像 */
  avatar?: string
  /** 状态（0正常 1停用） */
  userStatus: number

  /** 角色ID列表 */
  roleIds: string[]
  /** 岗位ID列表 */
  postIds: string[]
  /** 部门ID列表 */
  deptIds: string[]
  /** 备注 */
  remark?: string
}

/**
 * 用户更新参数
 */
export interface TaktUserUpdate extends TaktUserCreate {
  /** 用户ID */
  userId: string
}

/**
 * 用户状态更新参数
 */
export interface TaktUserStatus {
  /** 用户ID */
  userId: string
  /** 状态（0正常 1停用） */
  userStatus: number
}

/**
 * 用户密码重置参数
 */
export interface TaktUserPasswordReset {
  /** 用户ID */
  userId: string
  /** 新密码 */
  newPassword: string
}

/**
 * 用户个人信息更新参数
 */
export interface TaktUserProfileUpdate {
  /** 用户ID */
  userId: string
  /** 昵称 */
  nickName: string
  /** 英文名称 */
  englishName: string
  /** 全名 */
  fullName: string
  /** 真实姓名 */
  realName: string
  /** 手机号 */
  phoneNumber: string
  /** 邮箱 */
  email: string
  /** 性别 */
  gender: number
  /** 旧密码 */
  oldPassword?: string
  /** 新密码 */
  newPassword?: string
}

/**
 * 用户头像更新参数
 */
export interface TaktUserAvatarUpdate {
  /** 用户ID */
  userId: string
  /** 头像URL */
  avatar: string
}

/**
 * 用户密码修改参数
 */
export interface TaktUserPasswordChange {
  /** 用户ID */
  userId: string
  /** 旧密码 */
  oldPassword: string
  /** 新密码 */
  newPassword: string
}

/**
 * 用户登录信息
 */
export interface TaktUserLoginInfo {
  /** 用户ID */
  userId: string
  /** 用户名 */
  userName: string
  /** 昵称 */
  nickName: string
  /** 邮箱 */
  email: string
  /** 手机号 */
  phoneNumber: string
  /** 头像 */
  avatar: string
  /** 性别 */
  sex: number
  /** 状态 */
  userStatus: number
  /** 登录IP */
  loginIp: string
  /** 登录时间 */
  loginDate: string
  /** 角色列表 */
  roles: string[]
  /** 权限列表 */
  permissions: string[]
}

/**
 * 用户统计信息
 */
export interface TaktUserStatistics {
  /** 总用户数 */
  totalUsers: number
  /** 在线用户数 */
  onlineUsers: number
  /** 今日新增用户数 */
  todayNewUsers: number
  /** 本月新增用户数 */
  monthNewUsers: number
  /** 用户状态分布 */
  statusDistribution: {
    /** 正常用户数 */
    normal: number
    /** 停用用户数 */
    disabled: number
  }
  /** 性别分布 */
  sexDistribution: {
    /** 男性用户数 */
    male: number
    /** 女性用户数 */
    female: number
    /** 未知性别用户数 */
    unknown: number
  }
}
/**
 * 用户分页结果
 */
export interface TaktUserPagedResult extends TaktPagedResult<TaktUser> {}








