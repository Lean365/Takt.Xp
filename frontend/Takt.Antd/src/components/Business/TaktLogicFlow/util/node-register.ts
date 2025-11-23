/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: LogicFlow 自定义节点注册（基于 LogicFlow 官方 API 文档）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 * @see https://logicflow.cn/api/model/node-model
 * @see https://logicflow.cn/api/type/main-types
 */
import { RectNode, RectNodeModel, CircleNode, CircleNodeModel, DiamondNode, DiamondNodeModel } from '@logicflow/core'
import type { LogicFlow, NodeConfig } from '@logicflow/core'

// 开始节点视图
class StartNodeView extends CircleNode {}

// 开始节点模型（基于官方 API 文档）
// @see https://logicflow.cn/api/model/node-model
class StartNodeModel extends CircleNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 50
    this.height = 50
    this.text = {
      value: (data.text as any)?.value || data.text || '开始',
      x: data.x,
      y: data.y + 5
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    return style
  }

  // 设置默认锚点（基于官方 API 文档）
  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 结束节点视图
class EndNodeView extends CircleNode {}

// 结束节点模型（基于官方 API 文档）
class EndNodeModel extends CircleNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 50
    this.height = 50
    this.text = {
      value: (data.text as any)?.value || data.text || '结束',
      x: data.x,
      y: data.y + 5
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 任务节点视图
class TaskNodeView extends RectNode {}

// 任务节点模型（基于官方 API 文档）
class TaskNodeModel extends RectNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 120
    this.height = 60
    this.text = {
      value: (data.text as any)?.value || data.text || '任务节点',
      x: data.x,
      y: data.y
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    style.rx = 5
    style.ry = 5
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 网关节点视图
class GatewayNodeView extends DiamondNode {}

// 网关节点模型（基于官方 API 文档）
class GatewayNodeModel extends DiamondNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 50
    this.height = 50
    this.text = {
      value: (data.text as any)?.value || data.text || '网关',
      x: data.x,
      y: data.y + 5
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 网关开始节点视图
class ForkNodeView extends RectNode {}

// 网关开始节点模型（基于官方 API 文档）
class ForkNodeModel extends RectNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 120
    this.height = 60
    this.text = {
      value: (data.text as any)?.value || data.text || '网关开始',
      x: data.x,
      y: data.y
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    style.rx = 5
    style.ry = 5
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 网关结束节点视图
class JoinNodeView extends RectNode {}

// 网关结束节点模型（基于官方 API 文档）
class JoinNodeModel extends RectNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 120
    this.height = 60
    this.text = {
      value: (data.text as any)?.value || data.text || '网关结束',
      x: data.x,
      y: data.y
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    style.rx = 5
    style.ry = 5
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

// 会签节点视图
class MultiInstanceNodeView extends RectNode {}

// 会签节点模型（基于官方 API 文档）
class MultiInstanceNodeModel extends RectNodeModel {
  initNodeData(data: NodeConfig) {
    super.initNodeData(data)
    this.width = 120
    this.height = 60
    this.text = {
      value: (data.text as any)?.value || data.text || '会签',
      x: data.x,
      y: data.y
    }
  }

  getNodeStyle() {
    const style = super.getNodeStyle()
    style.fill = '#f4f6fc'
    style.stroke = '#777'
    style.strokeWidth = 1
    style.rx = 5
    style.ry = 5
    return style
  }

  getDefaultAnchor() {
    const { x, y, width, height } = this
    return [
      {
        x: x,
        y: y - height / 2,
        id: 'top'
      },
      {
        x: x + width / 2,
        y: y,
        id: 'right'
      },
      {
        x: x,
        y: y + height / 2,
        id: 'bottom'
      },
      {
        x: x - width / 2,
        y: y,
        id: 'left'
      }
    ]
  }
}

/**
 * 注册所有自定义节点
 * @param lf LogicFlow 实例
 */
export function registerNodes(lf: LogicFlow) {
  // 注册开始节点
  lf.register({
    type: 'start',
    view: StartNodeView,
    model: StartNodeModel
  })

  // 注册结束节点
  lf.register({
    type: 'end',
    view: EndNodeView,
    model: EndNodeModel
  })

  // 注册任务节点
  lf.register({
    type: 'task',
    view: TaskNodeView,
    model: TaskNodeModel
  })

  // 注册网关节点
  lf.register({
    type: 'gateway',
    view: GatewayNodeView,
    model: GatewayNodeModel
  })

  // 注册网关开始节点
  lf.register({
    type: 'fork',
    view: ForkNodeView,
    model: ForkNodeModel
  })

  // 注册网关结束节点
  lf.register({
    type: 'join',
    view: JoinNodeView,
    model: JoinNodeModel
  })

  // 注册会签节点
  lf.register({
    type: 'multiInstance',
    view: MultiInstanceNodeView,
    model: MultiInstanceNodeModel
  })
}

