//===================================================================
// 项目名 : Lean.Takt
// 文件名 : genTableDefine.ts
// 创建者 : Claude
// 创建时间: 2024-04-25
// 版本号 : V0.0.1
// 描述    : 代码生成表定义和列定义API
//===================================================================

import {request} from '@/utils/request';
import type { 
  TaktGenTableDefine, 
  TaktGenTableDefineQuery, 
  TaktGenTableDefinePageResult, 
  TaktGenTableDefineCreate, 
  TaktGenTableDefineUpdate
} from '@/types/generator/genTableDefine';
import type {
  TaktGenColumnDefine,
  TaktGenColumnDefineQuery,
  TaktGenColumnDefineCreate,
  TaktGenColumnDefineUpdate
} from '@/types/generator/genColumnDefine';
import type { TaktPagedResult, TaktApiResponse } from '@/types/common';
import { AxiosResponse } from 'axios';

// API基础路径
const baseUrl = '/api/TaktGenTableDefine';

/**
 * 获取表定义分页列表
 */
export function getTableDefineList(params: TaktGenTableDefineQuery) {
  return request<TaktPagedResult<TaktGenTableDefine>>({
    url: `${baseUrl}/tables/list`,
    method: 'get',
    params
  });
}

/**
 * 获取表定义详情
 */
export function getTableDefineInfo(id: string) {
  return request<TaktGenTableDefine>({
    url: `${baseUrl}/tables/get/${id}`,
    method: 'get'
  });
}

/**
 * 创建表定义
 */
export function createTableDefine(data: TaktGenTableDefineCreate) {
  return request<number>({
    url: `${baseUrl}/tables/create`,
    method: 'post',
    data
  });
}

/**
 * 更新表定义
 */
export function updateTableDefine(data: TaktGenTableDefineUpdate) {
  return request<boolean>({
    url: `${baseUrl}/tables/update`,
    method: 'put',
    data
  });
}

/**
 * 删除表定义
 */
export function deleteTableDefine(id: string) {
  return request<boolean>({
    url: `${baseUrl}/tables/delete/${id}`,
    method: 'delete'
  });
}

/**
 * 批量删除表定义
 */
export function batchDeleteTableDefine(ids: string[]) {
  return request<boolean>({
    url: `${baseUrl}/tables/batch/delete/${ids.join(',')}`,
    method: 'delete'
  });
}

/**
 * 导出表定义
 */
export function exportTableDefine(params?: TaktGenTableDefineQuery) {
  return request<Blob>({
    url: `${baseUrl}/tables/export`,
    method: 'get',
    params,
    responseType: 'blob'
  });
}

/**
 * 导入表定义
 */
export function importTableDefine(file: File) {
  const formData = new FormData()
  formData.append('file', file)
  return request<{ success: number; fail: number }>({
    url: `${baseUrl}/tables/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

/**
 * 获取表定义模板
 */
export function getTableDefineTemplate() {
  return request<Blob>({
    url: `${baseUrl}/tables/template`,
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 初始化表结构
 */
export function initializeTableDefine(data: TaktGenTableDefineCreate) {
  return request<boolean>({
    url: `${baseUrl}/tables/initialize`,
    method: 'post',
    data
  });
}

/**
 * 同步表结构
 */
export function syncTableDefine(id: string) {
  return request<boolean>({
    url: `${baseUrl}/tables/sync/${id}`,
    method: 'post'
  });
}

/**
 * 获取列定义列表
 * @param params 查询参数
 * @returns 分页结果
 */
export function getColumnDefineList(params: TaktGenColumnDefineQuery) {
  return request<TaktPagedResult<TaktGenColumnDefine>>({
    url: `${baseUrl}/columns/list`,
    method: 'get',
    params
  });
}

/**
 * 创建列定义
 * @param data 创建参数
 * @returns 创建结果
 */
export function createColumnDefine(data: TaktGenColumnDefineCreate) {
  return request<TaktGenColumnDefine>({
    url: `${baseUrl}/columns/create`,
    method: 'post',
    data
  });
}

/**
 * 更新列定义
 * @param data 更新参数
 * @returns 更新结果
 */
export function updateColumnDefine(data: TaktGenColumnDefineUpdate) {
  return request<TaktGenColumnDefine>({
    url: `${baseUrl}/columns/update`,
    method: 'put',
    data
  });
}

/**
 * 删除列定义
 * @param id 列定义ID
 * @returns 删除结果
 */
export function deleteColumnDefine(id: string): Promise<AxiosResponse<TaktApiResponse<boolean>>> {
  return request({
    url: `${baseUrl}/columns/delete/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除列定义
 * @param ids 列定义ID数组
 * @returns 删除结果
 */
export function batchDeleteColumnDefine(ids: string[]) {
  return request<boolean>({
    url: `${baseUrl}/columns/batch/delete/${ids.join(',')}`,
    method: 'delete'
  });
}

/**
 * 导入列定义
 * @param file 文件对象
 * @param sheetName 工作表名称
 * @returns 导入结果
 */
export function importColumnDefine(file: File, sheetName: string = 'Sheet1') {
  const formData = new FormData();
  formData.append('file', file);
  formData.append('sheetName', sheetName);
  
  return request<TaktGenColumnDefine[]>({
    url: `${baseUrl}/columns/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

/**
 * 导出列定义
 * @param tableId 表ID
 * @param sheetName 工作表名称
 * @returns 导出结果
 */
export function exportColumnDefine(tableId: string, sheetName: string = 'Sheet1') {
  return request<Blob>({
    url: `${baseUrl}/columns/export`,
    method: 'get',
    params: { tableId, sheetName },
    responseType: 'blob'
  });
}

/**
 * 获取列定义模板
 * @param sheetName 工作表名称
 * @returns 模板文件
 */
export function getColumnDefineTemplate(sheetName: string = 'Sheet1') {
  return request<Blob>({
    url: `${baseUrl}/columns/template`,
    method: 'get',
    params: { sheetName },
    responseType: 'blob'
  });
}
