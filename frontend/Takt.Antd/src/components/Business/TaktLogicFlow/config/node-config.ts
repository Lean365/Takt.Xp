/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: LogicFlow 节点配置（基于 OpenAuth.Net CreatedFlow）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 */

// 节点配置接口
export interface NodeConfig {
  type: string
  name: string
  icon: string
  belongto: string
}

// 节点默认配置接口
export interface NodeDefaultConfig {
  type: string
  width: number
  height: number
  properties: {
    type: string
    setInfo: {
      NodeRejectType: number
      NodeConfluenceType: string
      NodeDesignate: string
      NodeDesignateData: {
        users: string[]
        roles: string[]
        Texts: string
      }
    }
  }
}

// 工具配置
export const tools: NodeConfig[] = [
  {
    type: 'drag',
    icon: 'DragOutlined',
    name: '拖拽'
  },
  {
    type: 'connection',
    icon: 'ShareAltOutlined',
    name: '连线'
  }
]

// 基础节点配置
export const commonNodes: NodeConfig[] = [
  {
    type: 'start',
    name: '开始',
    icon: 'PlayCircleOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'end',
    name: '结束',
    icon: 'StopOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'node',
    name: '任务节点',
    icon: 'ToolOutlined',
    belongto: 'commonNodes'
  },
  {
    type: 'gateway',
    name: '网关',
    icon: 'ForkOutlined',
    belongto: 'commonNodes'
  }
]

// 高级节点配置
export const highNodes: NodeConfig[] = [
  {
    type: 'fork',
    name: '网关开始',
    icon: 'ForkOutlined',
    belongto: 'highNodes'
  },
  {
    type: 'join',
    name: '网关结束',
    icon: 'MergeCellsOutlined',
    belongto: 'highNodes'
  },
  {
    type: 'multiInstance',
    name: '会签',
    icon: 'TeamOutlined',
    belongto: 'highNodes'
  }
]

// 泳道节点配置
export const laneNodes: NodeConfig[] = [
  {
    type: 'x-lane',
    name: '横向泳道',
    icon: 'ColumnWidthOutlined',
    belongto: 'laneNodes'
  },
  {
    type: 'y-lane',
    name: '纵向泳道',
    icon: 'ColumnHeightOutlined',
    belongto: 'laneNodes'
  }
]

// LogicFlow 节点类型映射（使用原生类型）
export const nodeTypeMap: Record<string, string> = {
  start: 'circle', // 开始节点使用圆形
  end: 'circle', // 结束节点使用圆形
  node: 'rect', // 任务节点使用矩形
  gateway: 'diamond', // 网关节点使用菱形
  fork: 'rect', // 网关开始使用矩形
  join: 'rect', // 网关结束使用矩形
  multiInstance: 'rect' // 会签节点使用矩形
}

// 默认 setInfo
const defaultSetInfo = {
  NodeRejectType: 0,
  NodeConfluenceType: '',
  NodeDesignate: '',
  NodeDesignateData: {
    users: [],
    roles: [],
    Texts: ''
  }
}

// LogicFlow 节点默认配置（使用原生样式配置）
export const nodeDefaultConfig: Record<string, NodeDefaultConfig> = {
  start: {
    type: 'circle',
    width: 50,
    height: 50,
    properties: {
      type: 'start',
      setInfo: defaultSetInfo
    }
  },
  end: {
    type: 'circle',
    width: 50,
    height: 50,
    properties: {
      type: 'end',
      setInfo: defaultSetInfo
    }
  },
  node: {
    type: 'rect',
    width: 120,
    height: 60,
    properties: {
      type: 'node',
      setInfo: defaultSetInfo
    }
  },
  gateway: {
    type: 'diamond',
    width: 50,
    height: 50,
    properties: {
      type: 'gateway',
      setInfo: defaultSetInfo
    }
  },
  fork: {
    type: 'rect',
    width: 120,
    height: 60,
    properties: {
      type: 'fork',
      setInfo: defaultSetInfo
    }
  },
  join: {
    type: 'rect',
    width: 120,
    height: 60,
    properties: {
      type: 'join',
      setInfo: defaultSetInfo
    }
  },
  multiInstance: {
    type: 'rect',
    width: 120,
    height: 60,
    properties: {
      type: 'multiInstance',
      setInfo: defaultSetInfo
    }
  }
}

