//===================================================================
// 项目名 : Lean.Takt
// 文件名 : assyOutputDetail.ts
// 创建者 : Claude
// 创建时间: 2024-12-19
// 版本号 : v1.0.0
// 描述    : 生产明细API接口
//===================================================================

import {request} from '@/utils/request'
import type { TaktPagedResult } from '@/types/common'
import type { 
  TaktAssyOutputDetail, 
  TaktAssyOutputDetailCreate, 
  TaktAssyOutputDetailUpdate,
  TaktAssyOutputDetailQuery
} from '@/types/logistics/manufacturing/execution/output/assyOutputDetail'

// 获取生产明细分页列表
export function getAssyOutputDetailList(query: TaktAssyOutputDetailQuery) {
  return request<TaktPagedResult<TaktAssyOutputDetail>>({
    url: '/api/TaktAssyOutputDetail/list',
    method: 'get',
    params: query
  })
}

// 获取生产明细详情
export function getAssyOutputDetail(id: number) {
  return request<TaktAssyOutputDetail>({
    url: `/api/TaktAssyOutputDetail/${id}`,
    method: 'get'
  })
}

// 创建生产明细
export function createAssyOutputDetail(data: TaktAssyOutputDetailCreate) {
  return request<TaktAssyOutputDetail>({
    url: '/api/TaktAssyOutputDetail',
    method: 'post',
    data
  })
}

// 更新生产明细
export function updateAssyOutputDetail(data: TaktAssyOutputDetailUpdate) {
  return request<TaktAssyOutputDetail>({
    url: `/api/TaktAssyOutputDetail/${data.assyOutputDetailId}`,
    method: 'put',
    data
  })
}

// 删除生产明细
export function deleteAssyOutputDetail(id: number) {
  return request<boolean>({
    url: `/api/TaktAssyOutputDetail/${id}`,
    method: 'delete'
  })
}

// 批量删除生产明细
export function batchDeleteAssyOutputDetail(ids: number[]) {
  return request<boolean>({
    url: '/api/TaktAssyOutputDetail/batch',
    method: 'delete',
    data: { ids }
  })
}

