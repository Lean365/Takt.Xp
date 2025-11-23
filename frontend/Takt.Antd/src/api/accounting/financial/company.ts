import {request} from '@/utils/request'
import type { TaktCompanyQuery, TaktCompany, TaktCompanyStatus, TaktCompanyUpdate, TaktCompanyCreate } from '@/types/accounting/financial/company'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'

/**
 * 获取公司分页列表
 */
export function getCompanyList(query: TaktCompanyQuery) {
  return request<TaktPagedResult<TaktCompany>>({
    url: '/api/TaktCompany/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取公司详情
 */
export function getByIdAsync(companyId: string) {
  return request<TaktCompany>({
    url: `/api/TaktCompany/${companyId}`,
    method: 'get'
  })
}

/**
 * 获取公司选项列表
 */
export function getCompanyOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktCompany/options',
    method: 'get'
  })
}

/**
 * 创建公司
 */
export function createCompany(data: TaktCompanyCreate) {
  return request<number>({
    url: '/api/TaktCompany',
    method: 'post',
    data
  })
}

/**
 * 更新公司
 */
export function updateCompany(data: TaktCompanyUpdate) {
  return request<boolean>({
    url: '/api/TaktCompany',
    method: 'put',
    data
  })
}

/**
 * 删除公司
 */
export function deleteCompany(companyId: string) {
  return request<boolean>({
    url: `/api/TaktCompany/${companyId}`,
    method: 'delete'
  })
}

/**
 * 批量删除公司
 */
export function batchDeleteCompany(companyIds: string[]) {
  return request<boolean>({
    url: '/api/TaktCompany/batch',
    method: 'delete',
    data: companyIds
  })
}

/**
 * 更新公司状态
 */
export function updateCompanyStatus(data: TaktCompanyStatus) {
  return request<boolean>({
    url: `/api/TaktCompany/${data.companyId}/status`,
    method: 'put',
    params: {
      status: data.status
    }
  })
}

/**
 * 导入公司数据
 */
export function importCompany(file: File, sheetName: string = 'Company') {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: `/api/TaktCompany/import?sheetName=${sheetName}`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出公司数据
 */
export function exportCompany(query: TaktCompanyQuery, sheetName: string = 'Company') {
  return request<Blob>({
    url: `/api/TaktCompany/export?sheetName=${sheetName}`,
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取公司导入模板
 */
export function getTemplate(sheetName: string = 'Company') {
  return request<Blob>({
    url: `/api/TaktCompany/template?sheetName=${sheetName}`,
    method: 'get',
    responseType: 'blob'
  })
}
