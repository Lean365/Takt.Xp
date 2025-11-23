import {request} from '@/utils/request';
import type { TaktPagedResult } from '@/types/common';
import type { 
  TaktGenTemplateQuery, 
  TaktGenTemplate,
  TaktGenTemplateCreate,
  TaktGenTemplateUpdate
} from '@/types/generator/genTemplate';

/**
 * 获取代码生成模板分页列表
 */
export function getGenTemplateList(query: TaktGenTemplateQuery) {
  return request<TaktPagedResult<TaktGenTemplate>>({
    url: '/api/TaktGenTemplate/list',
    method: 'get',
    params: query
  });
}

/**
 * 获取代码生成模板详情
 */
export function getGenTemplate(id: string) {
  return request<TaktGenTemplate>({
    url: `/api/TaktGenTemplate/${id}`,
    method: 'get'
  });
}

/**
 * 创建代码生成模板
 */
export function createGenTemplate(data: TaktGenTemplateCreate) {
  return request({
    url: '/api/TaktGenTemplate',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成模板
 */
export function updateGenTemplate(data: TaktGenTemplateUpdate) {
  return request({
    url: '/api/TaktGenTemplate',
    method: 'put',
    data
  });
}

/**
 * 删除代码生成模板
 */
export function deleteGenTemplate(id: string) {
  return request({
    url: `/api/TaktGenTemplate/${id}`,
    method: 'delete'
  });
}

/**
 * 批量删除代码生成模板
 */
export function batchDeleteGenTemplate(ids: string[]) {
  return request({
    url: '/api/TaktGenTemplate/batch',
    method: 'delete',
    data: ids
  });
}

/**
 * 导出代码生成模板数据
 */
export function exportGenTemplate(query: TaktGenTemplateQuery) {
  return request({
    url: '/api/TaktGenTemplate/export',
    method: 'get',
    params: query,
    responseType: 'blob'
  });
}

/**
 * 导入代码生成模板数据
 */
export function importGenTemplate(file: File) {
  const formData = new FormData();
  formData.append('file', file);
  return request<{ success: number; fail: number }>({
    url: '/api/TaktGenTemplate/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  });
}

/**
 * 下载代码生成模板导入模板
 */
export function downloadTemplate() {
  return request({
    url: '/api/TaktGenTemplate/template',
    method: 'get',
    responseType: 'blob'
  });
}
