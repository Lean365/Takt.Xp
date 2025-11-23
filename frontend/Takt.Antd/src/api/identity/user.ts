import {request} from '@/utils/request'
import type { TaktUserQuery, TaktUser, TaktUserStatus, TaktUserPasswordReset, TaktUserUpdate, TaktUserCreate } from '@/types/identity/user'
import type { TaktUserRole } from '@/types/identity/userRole'
import type { TaktUserDept } from '@/types/identity/userDept'
import type { TaktUserPost } from '@/types/identity/userPost'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'

/**
 * 获取用户分页列表
 */
export function getUserList(query: TaktUserQuery) {
  return request<TaktPagedResult<TaktUser>>({
    url: '/api/TaktUser/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取用户详情
 */
export function getByIdAsync(userId: string) {
  return request<TaktUser>({
    url: `/api/TaktUser/${userId}`,
    method: 'get'
  })
}

/**
 * 创建用户
 */
export function createUser(data: TaktUserCreate) {
  return request<number>({
    url: '/api/TaktUser',
    method: 'post',
    data
  })
}

/**
 * 更新用户
 */
export function updateUser(data: TaktUserUpdate) {
  return request<boolean>({
    url: '/api/TaktUser',
    method: 'put',
    data
  })
}

/**
 * 删除用户
 */
export function deleteUser(userId: string) {
  return request<boolean>({
    url: `/api/TaktUser/${userId}`,
    method: 'delete'
  })
}

/**
 * 批量删除用户
 */
export function batchDeleteUser(userIds: string[]) {
  return request<boolean>({
    url: '/api/TaktUser/batch',
    method: 'delete',
    data: userIds
  })
}

/**
 * 更新用户状态
 */
export function updateUserStatus(data: TaktUserStatus) {
  return request<boolean>({
    url: `/api/TaktUser/${data.userId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 重置用户密码
 */
export function resetPassword(data: TaktUserPasswordReset) {
  return request<boolean>({
    url: '/api/TaktUser/reset-password',
    method: 'put',
    data
  })
}

/**
 * 修改用户密码
 */
export function changePassword(data: { oldPassword: string; newPassword: string }) {
  return request<boolean>({
    url: '/api/TaktUser/change-password',
    method: 'put',
    data
  })
}

/**
 * 解锁用户
 */
export function unlockUser(userId: string) {
  return request<boolean>({
    url: `/api/TaktUser/${userId}/unlock`,
    method: 'put'
  })
}

/**
 * 更新用户个人信息
 */
export function updateUserProfile(data: { userId: string; nickName: string; englishName: string; fullName: string; realName: string; phoneNumber: string; email: string; gender: number; oldPassword?: string; newPassword?: string }) {
  return request<boolean>({
    url: '/api/TaktUser/profile',
    method: 'put',
    data
  })
}

/**
 * 更新用户头像
 */
export function updateUserAvatar(data: { userId: string; avatar: string }) {
  return request<boolean>({
    url: '/api/TaktUser/avatar',
    method: 'put',
    data
  })
}

/**
 * 获取用户选项列表
 */
export function getUserOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktUser/options',
    method: 'get'
  })
}

/**
 * 获取用户角色列表
 */
export function getUserRoles(userId: string) {
  return request<TaktUserRole[]>({
    url: `/api/TaktUser/${userId}/roles`,
    method: 'get'
  })
}

/**
 * 获取用户部门列表
 */
export function getUserDepts(userId: string) {
  return request<TaktUserDept[]>({
    url: `/api/TaktUser/${userId}/depts`,
    method: 'get'
  })
}

/**
 * 获取用户岗位列表
 */
export function getUserPosts(userId: string) {
  return request<TaktUserPost[]>({
    url: `/api/TaktUser/${userId}/posts`,
    method: 'get'
  })
}


/**
 * 分配用户角色
 */
export function assignUserRoles(userId: string, roleIds: string[]) {
  return request<boolean>({
    url: `/api/TaktUser/${userId}/roles`,
    method: 'put',
    data: roleIds
  })
}

/**
 * 分配用户部门
 */
export function assignUserDepts(userId: string, deptIds: string[]) {
  return request<boolean>({
    url: `/api/TaktUser/${userId}/depts`,
    method: 'put',
    data: deptIds
  })
}

/**
 * 分配用户岗位
 */
export function assignUserPosts(userId: string, postIds: string[]) {
  return request<boolean>({
    url: `/api/TaktUser/${userId}/posts`,
    method: 'put',
    data: postIds
  })
}


/**
 * 导入用户数据
 */
export function importUser(file: File, sheetName?: string) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktUser/import',
    method: 'post',
    data: formData,
    params: { sheetName },
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出用户数据
 */
export function exportUser(query: TaktUserQuery, sheetName?: string, fileName?: string) {
  return request({
    url: '/api/TaktUser/export',
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 获取用户导入模板
 */
export function getTemplate(sheetName?: string, fileName?: string) {
  return request({
    url: '/api/TaktUser/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}



