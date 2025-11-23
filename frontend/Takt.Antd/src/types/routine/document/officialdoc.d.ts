/** 公文文档相关类型定义 */

import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'

/** 公文文档查询参数 */
export interface TaktOfficialDocumentQuery extends TaktPagedQuery {
  documentCode?: string
  documentTitle?: string
  documentType?: number
  documentLevel?: number
  documentStatus?: number
  importanceLevel?: number
  isMandatory?: number
  isPublic?: number
  needTraining?: number
  needExam?: number
  issuingDepartment?: string
  draftPerson?: string
  publisher?: string
  publishMethod?: number
  isTop?: number
  isRecommended?: number
  keywords?: string
  startTime?: Date
  endTime?: Date
}

/** 公文文档数据传输对象 */
export interface TaktOfficialDocument extends TaktBaseEntity {
  officialDocumentId: number
  parentId?: number
  orderNum: number
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentDescription?: string
  documentContent?: string
  documentPdfPath?: string
  documentVersion: string
  revisionVersion?: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  publishDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  draftDate?: Date
  publisher?: string
  publishMethod: number
  publishScope?: string
  relatedDocuments?: string
  relatedFiles?: string
  keywords?: string
  readCount: number
  downloadCount: number
  documentStatus: number
  isTop: number
  isRecommended: number
  parent?: TaktOfficialDocument
  children?: TaktOfficialDocument[]
}

/** 公文文档创建参数 */
export interface TaktOfficialDocumentCreate {
  parentId?: number
  orderNum: number
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentDescription?: string
  documentContent?: string
  documentPdfPath?: string
  documentVersion: string
  revisionVersion?: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  expiryDate?: Date
  publishDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  draftDate?: Date
  publisher?: string
  publishMethod: number
  publishScope?: string
  relatedDocuments?: string
  relatedFiles?: string
  keywords?: string
  documentStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 公文文档更新参数 */
export interface TaktOfficialDocumentUpdate extends TaktOfficialDocumentCreate {
  officialDocumentId: number
}

/** 公文文档删除参数 */
export interface TaktOfficialDocumentDelete {
  officialDocumentId: number
}

/** 公文文档批量删除参数 */
export interface TaktOfficialDocumentBatchDelete {
  officialDocumentIds: number[]
}

/** 公文文档导入参数 */
export interface TaktOfficialDocumentImport {
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentDescription?: string
  documentVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  documentStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 公文文档导出参数 */
export interface TaktOfficialDocumentExport {
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentDescription?: string
  documentVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  documentStatus: number
  isTop: number
  isRecommended: number
  readCount: number
  downloadCount: number
  createTime: Date
}

/** 公文文档导入模板参数 */
export interface TaktOfficialDocumentTemplate {
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentDescription?: string
  documentVersion: string
  issuingDepartment?: string
  issueDate?: Date
  effectiveDate?: Date
  importanceLevel: number
  isMandatory: number
  isPublic: number
  needTraining: number
  needExam: number
  draftPerson?: string
  publisher?: string
  publishMethod: number
  keywords?: string
  documentStatus: number
  isTop: number
  isRecommended: number
  remark?: string
}

/** 公文文档分页结果 */
export type TaktOfficialDocumentPagedResult = TaktPagedResult<TaktOfficialDocument>

/** 公文文档树节点 */
export interface TaktOfficialDocumentTreeNode {
  officialDocumentId: number
  label: string
  parentId?: number
  orderNum: number
  documentCode: string
  documentTitle: string
  documentType: number
  documentLevel: number
  documentStatus: number
  children?: TaktOfficialDocumentTreeNode[]
} 
