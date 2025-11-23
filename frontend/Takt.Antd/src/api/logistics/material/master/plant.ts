//===================================================================
// 项目名: Takt.Frontend
// 文件名: plant.ts
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 工厂管理API接口
//===================================================================

import {request} from '@/utils/request'
import type { 
  TaktPlant, 
  TaktPlantQuery, 
  TaktPlantCreate, 
  TaktPlantUpdate, 
  TaktPlantStatus
} from '@/types/logistics/material/master/plant'
import type { TaktSelectOption, TaktPagedResult } from '@/types/common'

/**
 * 获取工厂分页列表
 */
export function getPlantList(query: TaktPlantQuery) {
  return request<TaktPagedResult<TaktPlant>>({
    url: '/api/TaktPlant/list',
    method: 'get',
    params: query
  })
}

/**
 * 获取工厂详情
 */
export function getByIdAsync(plantId: string) {
  return request<TaktPlant>({
    url: `/api/TaktPlant/${plantId}`,
    method: 'get'
  })
}

/**
 * 根据工厂编码获取工厂详情
 */
export function getByCode(plantCode: string) {
  return request<TaktPlant>({
    url: `/api/TaktPlant/code/${plantCode}`,
    method: 'get'
  })
}

/**
 * 创建工厂
 */
export function createPlant(data: TaktPlantCreate) {
  return request<number>({
    url: '/api/TaktPlant',
    method: 'post',
    data
  })
}

/**
 * 更新工厂
 */
export function updatePlant(data: TaktPlantUpdate) {
  return request<boolean>({
    url: '/api/TaktPlant',
    method: 'put',
    data
  })
}

/**
 * 删除工厂
 */
export function deletePlant(plantId: string) {
  return request<boolean>({
    url: `/api/TaktPlant/${plantId}`,
    method: 'delete'
  })
}

/**
 * 批量删除工厂
 */
export function batchDeletePlant(plantIds: string[]) {
  return request<boolean>({
    url: '/api/TaktPlant/batch',
    method: 'delete',
    data: plantIds
  })
}

/**
 * 更新工厂状态
 */
export function updatePlantStatus(data: TaktPlantStatus) {
  return request<boolean>({
    url: '/api/TaktPlant/status',
    method: 'put',
    data
  })
}

/**
 * 导入工厂数据
 */
export function importPlant(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: '/api/TaktPlant/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出工厂数据
 */
export function exportPlant(query: TaktPlantQuery) {
  return request<Blob>({
    url: '/api/TaktPlant/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  })
}

/**
 * 获取工厂导入模板
 */
export function getTemplate() {
  return request<Blob>({
    url: '/api/TaktPlant/template',
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * 获取工厂选项列表
 */
export function getPlantOptions() {
  return request<TaktSelectOption[]>({
    url: '/api/TaktPlant/options',
    method: 'get'
  })
}

