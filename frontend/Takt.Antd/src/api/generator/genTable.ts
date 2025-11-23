import {request} from '@/utils/request';
import type { TaktGenTable, TaktGenTableQuery, TaktGenTablePageResult } from '@/types/generator/genTable';
import type { TaktGenColumn } from '@/types/generator/genColumn';
import type { TaktPagedResult } from '@/types/common';

/**
 * 获取代码生成表分页列表
 */
export function getTableList(params: TaktGenTableQuery) {
  return request<TaktPagedResult<TaktGenTable>>({
    url: '/api/TaktGenTable/list',
    method: 'get',
    params
  });
}

/**
 * 获取代码生成表详情
 */
export function getTable(id: string) {
  return request<TaktGenTable>({
    url: `/api/TaktGenTable/${id}`,
    method: 'get'
  });
}

/**
 * 获取表字段列表
 */
export function getColumns(tableId: string) {
  return request<TaktGenColumn[]>({
    url: `/api/TaktGenTable/columns/${tableId}`,
    method: 'get'
  });
}

/**
 * 创建代码生成表
 */
export function createTable(data: TaktGenTable) {
  return request<TaktGenTable>({
    url: '/api/TaktGenTable',
    method: 'post',
    data
  });
}

/**
 * 更新代码生成表
 */
export function updateTable(data: TaktGenTable) {
  return request<TaktGenTable>({
    url: '/api/TaktGenTable',
    method: 'put',
    data
  });
}

/**
 * 删除代码生成表
 */
export function deleteTable(id: string) {
  return request<boolean>({
    url: `/api/TaktGenTable/${id}`,
    method: 'delete'
  });
}

/**
 * 从数据库导入表结构
 */
export function importTable(input: { tableNames: string[] }) {
  return request<TaktGenTable[]>({
    url: '/api/TaktGenTable/import',
    method: 'post',
    data: input
  });
}

/**
 * 获取数据库列表
 */
export function getDatabasesByDb() {
  return request<string[]>({
    url: '/api/TaktGenTable/databases',
    method: 'get'
  });
}

/**
 * 获取数据库表列表
 */
export function getTablesByDb(databaseName: string) {
  return request<Array<{ tableName: string; tableComment: string }>>({
    url: `/api/TaktGenTable/tables/${databaseName}`,
    method: 'get'
  });
}

/**
 * 获取数据库表列信息
 */
export function getTableColumnsByDb(databaseName: string, tableName: string) {
  return request<TaktGenColumn[]>({
    url: `/api/TaktGenTable/columns/${databaseName}/${tableName}`,
    method: 'get'
  });
}

/**
 * 导入表及其列信息
 */
export function importTableAndColumns(databaseName: string, tableName: string) {
  return request<TaktGenTable>({
    url: `/api/TaktGenTable/import-table-and-columns/${databaseName}/${tableName}`,
    method: 'post',
    timeout: 60000 // 增加超时时间到60秒
  });
}

/**
 * 同步表结构到数据库
 */
export function syncTable(id: string) {
  return request<boolean>({
    url: `/api/TaktGenTable/sync/${id}`,
    method: 'post'
  });
}

/**
 * 预览生成的代码
 */
export function previewCode(id: string) {
  return request<Record<string, string>>({
    url: `/api/TaktGenTable/preview/${id}`,
    method: 'get'
  });
}

/**
 * 生成代码
 */
export function generateCode(id: string) {
  return request<boolean>({
    url: `/api/TaktGenTable/generate/${id}`,
    method: 'post'
  });
}

/**
 * 批量生成代码
 */
export function batchGenerateCode(ids: string[]) {
  return request<string>({
    url: '/api/TaktGenTable/generate/batch',
    method: 'post',
    data: ids
  });
}

/**
 * 下载生成的代码
 */
export function downloadCode(id: string) {
  return request<Blob>({
    url: `/api/TaktGenTable/download/${id}`,
    method: 'get',
    responseType: 'blob'
  });
}

