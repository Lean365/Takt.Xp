import {request} from '@/utils/request'
import type { TaktFormQuery, TaktForm, TaktFormStatus, TaktFormUpdate, TaktFormCreate } from '@/types/workflow/form'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'

// 获取表单分页列表
export function getFormList(query: TaktFormQuery) {
  return request<TaktPagedResult<TaktForm>>({
    url: '/api/TaktForm/list',
    method: 'get',
    params: query
  })
}

/**
 * 根据ID获取工作流表单
 */
export function getFormById(formId: string) {
  return request<TaktForm>({
    url: `/api/TaktForm/${formId}`,
    method: 'get'
  })
}

// 根据键获取表单定义
export function getFormByKey(formKey: string) {
  return request<TaktForm>({
    url: `/api/TaktForm/key/${formKey}`,
    method: 'get'
  })
}

// 获取我的表单列表
export function getMyForms(query: TaktFormQuery) {
  return request<TaktPagedResult<TaktForm>>({
    url: '/api/TaktForm/my',
    method: 'get',
    params: query
  })
}

// 获取表单选项列表
export function getFormOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktForm/options',
    method: 'get'
  })
}

// 创建表单
export function createForm(data: TaktFormCreate) {
  return request<number>({
    url: '/api/TaktForm',
    method: 'post',
    data
  })
}

// 更新表单
export function updateForm(data: TaktFormUpdate) {
  return request<boolean>({
    url: '/api/TaktForm',
    method: 'put',
    data
  })
}

/**
 * 删除工作流表单
 */
export function deleteForm(formId: string) {
  return request<boolean>({
    url: `/api/TaktForm/${formId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工作流表单
 */
export function batchDeleteForm(formIds: string[]) {
  return request<boolean>({
    url: '/api/TaktForm/batch',
    method: 'delete'
  })
}

// 克隆表单定义
export function cloneForm(formId: string, newFormName: string) {
  return request<number>({
    url: `/api/TaktForm/${formId}/clone`,
    method: 'post',
    data: newFormName
  })
}

// 更新表单状态
export function updateFormStatus(data: TaktFormStatus) {
  return request<boolean>({
    url: `/api/TaktForm/${data.formId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

// 获取导入模板
export function getFormTemplate() {
  return request<Blob>({
    url: '/api/TaktForm/template',
    method: 'get',
    responseType: 'blob'
  })
}

// 导入表单
export function importForm(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktForm/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出表单
export function exportForm(query: TaktFormQuery) {
  return request<Blob>({
    url: '/api/TaktForm/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}
