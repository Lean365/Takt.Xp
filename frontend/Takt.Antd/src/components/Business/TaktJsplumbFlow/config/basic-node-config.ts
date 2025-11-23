/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 定义流程节点类型（基于 OpenAuth.Net CreatedFlow）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 */

// 工具配置接口
export interface ToolConfig {
  type: string
  icon: string
  defaultIcon: string
  name: string
}

// 节点配置接口
export interface NodeConfig {
  type: string
  name: string
  icon: string
  defaultIcon: string
  belongto: string
}

// 工具配置
export const tools: ToolConfig[] = [
  {
    type: 'drag',
    icon: 'DragOutlined',
    defaultIcon: 'DragOutlined',
    name: '拖拽'
  },
  {
    type: 'connection',
    icon: 'ShareAltOutlined',
    defaultIcon: 'ShareAltOutlined',
    name: '连线'
  }
]

// 基础节点配置
export const commonNodes: NodeConfig[] = [
  {
    type: 'start',
    name: '开始',
    icon: 'PlayCircleOutlined',
    defaultIcon: 'PlayCircleOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'end',
    name: '结束',
    icon: 'StopOutlined',
    defaultIcon: 'StopOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'node',
    name: '任务节点',
    icon: 'ToolOutlined',
    defaultIcon: 'ToolOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'fork',
    name: '网关开始',
    icon: 'ForkOutlined',
    defaultIcon: 'ForkOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'join',
    name: '网关结束',
    icon: 'MergeCellsOutlined',
    defaultIcon: 'MergeCellsOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'multiInstance',
    name: '会签节点',
    icon: 'TeamOutlined',
    defaultIcon: 'TeamOutlined',
    belongto: 'commonNodes'
  }
]

// 高级节点配置
export const highNodes: NodeConfig[] = [
  {
    type: 'child-flow',
    name: '子流程',
    icon: 'ApiOutlined',
    defaultIcon: 'ApiOutlined',
    belongto: 'highNodes'
  }
]

// 泳道节点配置
export const laneNodes: NodeConfig[] = [
  {
    type: 'x-lane',
    name: '横向泳道',
    icon: 'ColumnWidthOutlined',
    defaultIcon: 'ColumnWidthOutlined',
    belongto: 'laneNodes'
  },
  {
    type: 'y-lane',
    name: '纵向泳道',
    icon: 'ColumnHeightOutlined',
    defaultIcon: 'ColumnHeightOutlined',
    belongto: 'laneNodes'
  }
]

