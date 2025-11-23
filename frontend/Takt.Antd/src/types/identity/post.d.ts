import type { TaktBaseEntity, TaktPagedQuery, TaktPagedResult } from '@/types/common'
import type { User } from '@/types/identity/user'


/**
 * 岗位实体
 */
export interface TaktPost extends TaktBaseEntity {
  /** 岗位ID */
  postId: string
  /** 岗位编码 */
  postCode: string
  /** 岗位名称 */
  postName: string
  /** 岗位等级 */
  rank: number
  /** 用户数量 */
  userCount: number
  /** 显示顺序 */
  orderNum: number
  /** 状态（0正常 1停用） */
  postStatus: number

}

/**
 * 岗位查询参数
 */
export interface TaktPostQuery extends TaktPagedQuery {
  /** 岗位编码 */
  postCode?: string
  /** 岗位名称 */
  postName?: string
  /** 状态 */
  postStatus?: number
  /** 开始时间 */
  beginTime?: string
  /** 结束时间 */
  endTime?: string
}

/**
 * 岗位创建参数
 */
export interface TaktPostCreate  {

    /** 岗位编码 */
    postCode: string
    /** 岗位名称 */
    postName: string
    /** 岗位等级 */
    rank: number
    /** 用户数量 */
    userCount: number
    /** 显示顺序 */
    orderNum: number
    /** 状态（0正常 1停用） */
    postStatus: number
    /** 备注 */
    remark?: string
}

/**
 * 岗位更新参数
 */
export interface TaktPostUpdate extends TaktPostCreate{
  /** 岗位ID */
  postId: string
}



/**
 * 岗位状态更新参数
 */
export interface TaktPostStatusUpdate {
  /** 岗位ID */
  postId: string
  /** 状态 */
  postStatus: number
}

/**
 * 岗位分页结果
 */
export interface TaktPostPagedResult extends TaktPagedResult<TaktPost> {}


