// 查询字段类型定义
export type FieldType = 'input' | 'select' | 'date' | 'dateRange' | 'number' | 'radio' | 'checkbox' | 'cascader'

// 查询字段选项接口
export interface QueryFieldOption {
  label: string
  value: string | number | boolean
}

// 查询字段接口
export interface QueryField {
  name: string
  label: string | (() => string)
  type: FieldType
  placeholder?: string
  span?: number
  rules?: any[]
  options?: { label: string; value: any }[]
  disabled?: boolean
  props?: Record<string, any>
} 