//===================================================================
// 项目名 : Lean.Takt
// 文件名 : assyOutput.ts
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 组立日报API接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { 
  TaktAssyOutput, 
  TaktAssyOutputQuery, 
  TaktAssyOutputCreate, 
  TaktAssyOutputUpdate
} from '@/types/logistics/manufacturing/execution/output/assyOutput'

// 获取组立日报分页列表
export function getAssyOutputList(query: TaktAssyOutputQuery) {
  return request<TaktPagedResult<TaktAssyOutput>>({
    url: '/api/TaktAssyOutput/list',
    method: 'get',
    params: query
  })
}

// 获取组立日报详情
export function getAssyOutput(id: number) {
  return request<TaktAssyOutput>({
    url: `/api/TaktAssyOutput/${id}`,
    method: 'get'
  })
}

// 创建组立日报
export function createAssyOutput(data: TaktAssyOutputCreate) {
  return request<TaktAssyOutput>({
    url: '/api/TaktAssyOutput',
    method: 'post',
    data
  })
}

// 更新组立日报
export function updateAssyOutput(data: TaktAssyOutputUpdate) {
  return request<TaktAssyOutput>({
    url: `/api/TaktAssyOutput/${data.assyOutputId}`,
    method: 'put',
    data
  })
}

// 删除组立日报
export function deleteAssyOutput(id: number) {
  return request<boolean>({
    url: `/api/TaktAssyOutput/${id}`,
    method: 'delete'
  })
}

// 批量删除组立日报
export function batchDeleteAssyOutput(ids: number[]) {
  return request<boolean>({
    url: '/api/TaktAssyOutput/batch',
    method: 'delete',
    data: { ids }
  })
}

// 导入组立日报
export function importAssyOutput(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktAssyOutput/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出组立日报
export function exportAssyOutput(query: TaktAssyOutputQuery) {
  return request<Blob>({
    url: '/api/TaktAssyOutput/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

// 获取导入模板
export function getTemplate() {
  return request<Blob>({
    url: '/api/TaktAssyOutput/template',
    method: 'get',
    responseType: 'blob'
  })
}

