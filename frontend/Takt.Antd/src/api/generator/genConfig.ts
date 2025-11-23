import {request} from '@/utils/request';
import type { TaktApiResponse } from '@/types/common';
import type { TaktGenConfig, TaktGenConfigCreate, TaktGenConfigUpdate, TaktGenConfigQuery, TaktGenConfigPageResult } from '@/types/generator/genConfig';

/**
 * 获取代码生成配置列表
 */
export function getGenConfigList(params: TaktGenConfigQuery) {
  return request<TaktGenConfigPageResult>({
    url: '/api/TaktGenConfig/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成配置详情
 */
export function getGenConfig(id: string) {
  return request<TaktGenConfig>({
    url: `/api/TaktGenConfig/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成配置
 */
export function createGenConfig(data: TaktGenConfigCreate) {
  return request<TaktGenConfig>({
    url: '/api/TaktGenConfig',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成配置
 */
export function updateGenConfig(id: string, data: TaktGenConfigUpdate) {
  return request<TaktGenConfig>({
    url: `/api/TaktGenConfig/${id}`,
    method: 'put',
    data
  });
}

/**
 * 删除代码生成配置
 */
export function deleteGenConfig(id: string) {
  return request<boolean>({
    url: `/api/TaktGenConfig/${id}`,
    method: 'delete'
  });
}

/**
 * 批量删除代码生成配置
 */
export function batchDeleteGenConfig(ids: string[]) {
  return request<boolean>({
    url: '/api/TaktGenConfig/batch',
    method: 'delete',
    data: ids
  });
}

/**
 * 导出代码生成配置
 */
export function exportGenConfig(params: TaktGenConfigQuery) {
  return request<Blob>({
    url: '/api/TaktGenConfig/export',
    method: 'get',
    params,
    responseType: 'blob'
  });
}

/**
 * 下载导入模板
 */
export function downloadTemplate() {
  return request<Blob>({
    url: '/api/TaktGenConfig/template',
    method: 'get',
    responseType: 'blob'
  });
}

/**
 * 导入代码生成配置
 */
export function importGenConfig(file: File) {
  const formData = new FormData();
  formData.append('file', file);
  return request<{ success: number; fail: number }>({
    url: '/api/TaktGenConfig/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
} 
