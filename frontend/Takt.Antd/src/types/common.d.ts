/**
 * 基础实体类型
 */
export interface TaktBaseEntity {
   /** 创建者 */
  createBy: string;
  /** 创建时间 */
  createTime: string;
  /** 更新者 */
  updateBy?: string;
  /** 更新时间 */
  updateTime?: string;
  /** 删除者 */
  deleteBy?: string;
  /** 删除时间 */
  deleteTime?: string;
  /** 是否删除(0=未删除,1=已删除) */
  isDeleted: number;
  /** 备注 */
  remark?: string;
}

/**
 * 分页查询参数
 */
export interface TaktPagedQuery {
  /** 当前页码 */
  pageIndex: number;
  /** 每页大小 */
  pageSize: number;
  /** 排序列 */
  orderByColumn?: string;
  /** 排序方向(desc/asc) */
  orderType?: string;
}

/**
 * 分页查询结果 - 对应后端 TaktPagedResult<T>
 */
export interface TaktPagedResult<T> {
  /** 总记录数 */
  totalNum: number;
  /** 当前页索引 */
  pageIndex: number;
  /** 每页大小 */
  pageSize: number;
  /** 总页数 */
  totalPage: number;
  /** 数据列表 */
  rows: T[];
}


/**
 * 树形节点
 */
export interface TaktTreeNode {
  /** 节点ID */
  id: number;
  /** 节点标签 */
  label: string;
  /** 子节点 */
  children?: TaktTreeNode[];
  /** 其他属性 */
  [key: string]: any;
}

/**
 * 选择框选项
 */
export interface TaktSelectOption {
  /** 字典标签 */
  dictLabel: string;
  /** 字典键值 */
  dictValue: string | number;
  /** 扩展标签 */
  extLabel?: string;
  /** 扩展键值 */
  extValue?: string | number;
  /** 翻译键 */
  transKey?: string;
  /** CSS类名 */
  cssClass?: number;
  /** 列表类名 */
  listClass?: number;
  /** 排序号 */
  orderNum: number;
  /** 状态（0正常 1停用） */
  status: number;
}

/**
 * 字典数据
 */
export interface TaktDictData {
  /** 字典标签 */
  dictLabel: string;
  /** 字典值 */
  dictValue: string;
  /** CSS类名 */
  cssClass?: string;
  /** 列表类名 */
  listClass?: string;
  /** 扩展标签 */
  extLabel?: string;
  /** 扩展值 */
  extValue?: string;
  /** 翻译键 */
  transKey?: string;
  /** 是否禁用 */
  disabled?: boolean;
}

/**
 * 字典类型
 */
export interface TaktDictType {
  /** 字典ID */
  dictId: number;
  /** 字典名称 */
  dictName: string;
  /** 字典类型 */
  dictType: string;
  /** 字典分类 */
  dictCategory: string;
  /** 状态 */
  status: number;
  /** 备注 */
  remark?: string;
  /** 创建时间 */
  createTime: string;
}

/**
 * 菜单类型枚举
 */
export enum TaktMenuType {
  /** 目录 */
  Directory = 0,
  /** 菜单 */
  Menu = 1,
  /** 按钮 */
  Button = 2
}

/**
 * 分页请求参数
 */
export interface TaktPageRequest {
  pageIndex: number;
  pageSize: number;
  keyword?: string;
  sortField?: string;
  sortOrder?: 'ascend' | 'descend';
  [key: string]: any;
}

/**
 * 分页响应数据
 */
export interface TaktPageResponse<T> {
  total: number;
  items: T[];
}

/**
 * 分页结果类型（通用格式）
 */
export interface TaktPageResult<T> {
  total: number;
  items: T[];
  pageIndex: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}


/**
 * 通用状态类型
 */
export enum TaktStatus {
  Disabled = 0,
  Enabled = 1,
  Deleted = -1
}

/**
 * 通用树节点类型
 */
export interface TaktTreeNode {
  id: string | number;
  parentId: string | number;
  name: string;
  children?: TaktTreeNode[];
  [key: string]: any;
}

/**
 * 统一API响应类型
 */
export interface TaktApiResponse<T = any> {
  /** 状态码 */
  code: number;
  /** 消息 */
  msg: string;
  /** 数据 */
  data: T;
}


