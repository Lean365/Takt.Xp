//===================================================================
// 项目名: Takt.Frontend
// 文件名: prodMaterial.ts
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 生产物料管理API接口
//===================================================================

import {request} from '@/utils/request'
import type { 
  TaktProdMaterial, 
  TaktProdMaterialQuery, 
  TaktProdMaterialCreate, 
  TaktProdMaterialUpdate, 
  TaktProdMaterialStatus,
  TaktProdMaterialPagedResult
} from '@/types/logistics/material/master/prodMaterial'

// 获取生产物料分页列表
export function getProdMaterialList(query: TaktProdMaterialQuery) {
  return request<TaktProdMaterialPagedResult>({
    url: '/api/TaktProdMaterial/list',
    method: 'get',
    params: query
  })
}

// 根据ID获取生产物料详情
export function getProdMaterialById(id: number) {
  return request<TaktProdMaterial>({
    url: `/api/TaktProdMaterial/${id}`,
    method: 'get'
  })
}

// 根据工厂编码和物料编码获取生产物料详情
export function getProdMaterialByPlantAndCode(plantCode: string, materialCode: string) {
  return request<TaktProdMaterial>({
    url: `/api/TaktProdMaterial/code/${plantCode}/${materialCode}`,
    method: 'get'
  })
}

// 创建生产物料
export function createProdMaterial(data: TaktProdMaterialCreate) {
  return request<number>({
    url: '/api/TaktProdMaterial',
    method: 'post',
    data
  })
}

// 更新生产物料
export function updateProdMaterial(data: TaktProdMaterialUpdate) {
  return request<boolean>({
    url: '/api/TaktProdMaterial',
    method: 'put',
    data
  })
}

// 删除生产物料
export function deleteProdMaterial(id: number) {
  return request<boolean>({
    url: `/api/TaktProdMaterial/${id}`,
    method: 'delete'
  })
}

// 批量删除生产物料
export function batchDeleteProdMaterial(ids: number[]) {
  return request<boolean>({
    url: '/api/TaktProdMaterial/batch',
    method: 'delete',
    data: ids
  })
}

// 更新生产物料状态
export function updateProdMaterialStatus(data: TaktProdMaterialStatus) {
  return request<boolean>({
    url: '/api/TaktProdMaterial/status',
    method: 'put',
    data
  })
}

// 导入生产物料数据
export function importProdMaterial(file: File, sheetName = '生产物料信息') {
  const formData = new FormData()
  formData.append('file', file)
  formData.append('sheetName', sheetName)
  
  return request<{ success: number; fail: number }>({
    url: '/api/TaktProdMaterial/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

// 导出生产物料数据
export function exportProdMaterial(query: TaktProdMaterialQuery, sheetName = '生产物料信息') {
  return request<Blob>({
    url: '/api/TaktProdMaterial/export',
    method: 'get',
    params: { ...query, sheetName },
    responseType: 'blob'
  })
}

// 获取导入模板
export function getProdMaterialTemplate(sheetName = '生产物料信息') {
  return request<Blob>({
    url: '/api/TaktProdMaterial/template',
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  })
}

// 获取生产物料选项列表
export function getProdMaterialOptions(plantCode: string, keyword?: string) {
  return request<TaktProdMaterial[]>({
    url: '/api/TaktProdMaterial/options',
    method: 'get',
    params: { plantCode, keyword }
  })
}

