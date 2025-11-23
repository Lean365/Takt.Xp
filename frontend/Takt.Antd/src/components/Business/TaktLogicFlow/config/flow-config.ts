/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: LogicFlow 流程配置（基于 LogicFlow 官方 API 文档）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 * @see https://logicflow.cn/api/detail/constructor
 * @see https://logicflow.cn/api/theme
 * @see https://logicflow.cn/api/type
 */
import type { Options, Theme } from '@logicflow/core'

// 快捷键配置接口
export interface ShortcutConfig {
  code: number
  codeName: string
  shortcutName: string
}

// 右键菜单项接口
export interface MenuItem {
  fnHandler?: string
  icoName: string
  btnName: string
  children?: MenuItem[]
}

// 右键菜单配置接口
export interface ContextMenuConfig {
  menuName: string
  axis: {
    x: number | null
    y: number | null
  }
  menulists: MenuItem[]
}

// LogicFlow 基础配置（基于官方 API 文档）
// @see https://logicflow.cn/api/detail/constructor
export const logicFlowConfig: Partial<Options> = {
  grid: {
    size: 20,
    visible: true,
    type: 'dot',
    config: {
      color: '#ebebeb',
      thickness: 1
    }
  },
  background: {
    backgroundColor: '#fafafa',
    backgroundImage: '',
    backgroundRepeat: 'repeat',
    backgroundPosition: '0% 0%',
    backgroundSize: 'auto'
  },
  keyboard: {
    enabled: true
  },
  // 主题配置（基于官方 API 文档）
  // @see https://logicflow.cn/api/theme
  style: {
    rect: {
      rx: 5,
      ry: 5,
      strokeWidth: 1,
      fill: '#f4f6fc',
      stroke: '#777'
    },
    circle: {
      strokeWidth: 1,
      fill: '#f4f6fc',
      stroke: '#777'
    },
    diamond: {
      strokeWidth: 1,
      fill: '#f4f6fc',
      stroke: '#777'
    },
    polygon: {
      strokeWidth: 1
    },
    ellipse: {
      strokeWidth: 1
    }
  },
  edgeType: 'polyline',
  adjustEdgeStartAndEnd: true,
  hoverOutline: false,
  // 编辑配置（基于官方 API 文档）
  // @see https://logicflow.cn/api/model/edit-config-model
  isSilentMode: false,
  stopScrollGraph: false,
  stopZoomGraph: false,
  stopMoveGraph: false,
  // 允许的操作
  allowEdge: true,
  allowNode: true,
  allowBlank: true,
  allowRotation: false,
  // 历史记录
  allowHistory: true,
  // 辅助线
  snapline: true
}

// 主题配置（基于官方 API 文档）
// @see https://logicflow.cn/api/theme
// @see https://logicflow.cn/api/type/theme
export const themeConfig: Theme = {
  baseNode: {
    fill: '#f4f6fc',
    stroke: '#777',
    strokeWidth: 1
  },
  baseEdge: {
    stroke: '#2a2929',
    strokeWidth: 2
  },
  rect: {
    fill: '#f4f6fc',
    stroke: '#777',
    strokeWidth: 1,
    rx: 5,
    ry: 5
  },
  circle: {
    fill: '#f4f6fc',
    stroke: '#777',
    strokeWidth: 1
  },
  diamond: {
    fill: '#f4f6fc',
    stroke: '#777',
    strokeWidth: 1
  },
  polyline: {
    stroke: '#2a2929',
    strokeWidth: 2,
    hover: {
      stroke: '#1890ff',
      strokeWidth: 3
    }
  },
  bezier: {
    stroke: '#2a2929',
    strokeWidth: 2,
    hover: {
      stroke: '#1890ff',
      strokeWidth: 3
    }
  }
}

// 流程状态
export const flowStatus = {
  CREATE: 'create',
  MODIFY: 'modify',
  LOADING: 'loading',
  SAVE: 'save'
} as const

// 快捷键配置
export const shortcut: Record<string, ShortcutConfig> = {
  multiple: {
    code: 16, // SHIFT
    codeName: 'SHIFT',
    shortcutName: '多选'
  },
  dragContainer: {
    code: 32, // SPACE
    codeName: 'SPACE',
    shortcutName: '拖动画布'
  },
  scaleContainer: {
    code: 17, // CTRL
    codeName: 'CTRL',
    shortcutName: '画布缩放'
  },
  dragTool: {
    code: 68, // D
    codeName: 'D',
    shortcutName: '拖拽工具'
  },
  connTool: {
    code: 67, // C
    codeName: 'C',
    shortcutName: '连线工具'
  },
  zoomInTool: {
    code: 187, // +
    codeName: '+',
    shortcutName: '放大工具'
  },
  zoomOutTool: {
    code: 189, // -
    codeName: '-',
    shortcutName: '缩小工具'
  },
  settingModal: {
    code: 83, // S
    codeName: 'S',
    shortcutName: '设置'
  },
  testModal: {
    code: 84, // T
    codeName: 'T',
    shortcutName: '打开测试页面'
  }
}

// 右键菜单配置
export const contextMenu: Record<string, ContextMenuConfig> = {
  container: {
    menuName: 'flow-menu',
    axis: {
      x: null,
      y: null
    },
    menulists: [
      {
        fnHandler: 'paste',
        icoName: 'edit',
        btnName: '粘贴'
      },
      {
        fnHandler: 'selectAll',
        icoName: 'edit',
        btnName: '全选'
      },
      {
        icoName: 'edit',
        btnName: '对齐方式',
        children: [
          {
            icoName: 'edit',
            fnHandler: 'verticaLeft',
            btnName: '垂直左对齐'
          },
          {
            icoName: 'edit',
            fnHandler: 'verticalCenter',
            btnName: '垂直居中'
          },
          {
            icoName: 'edit',
            fnHandler: 'verticalRight',
            btnName: '垂直右对齐'
          },
          {
            icoName: 'edit',
            fnHandler: 'levelUp',
            btnName: '水平上对齐'
          },
          {
            icoName: 'edit',
            fnHandler: 'levelCenter',
            btnName: '水平居中'
          },
          {
            icoName: 'edit',
            fnHandler: 'levelDown',
            btnName: '水平下对齐'
          }
        ]
      }
    ]
  },
  node: {
    menuName: 'node-menu',
    axis: {
      x: null,
      y: null
    },
    menulists: [
      {
        fnHandler: 'copyNode',
        icoName: 'edit',
        btnName: '复制'
      },
      {
        fnHandler: 'deleteNode',
        icoName: 'edit',
        btnName: '删除'
      }
    ]
  },
  sl: {
    menuName: 'link-menu',
    axis: {
      x: null,
      y: null
    },
    menulists: [
      {
        fnHandler: 'deleteLink',
        icoName: 'edit',
        btnName: '删除连线'
      }
    ]
  }
}

// 默认样式配置
export const defaultStyle = {
  containerScale: {
    init: 1,
    min: 0.1,
    max: 3,
    onceEnlarge: 0.1,
    onceNarrow: 0.1
  },
  alignGridPX: [20, 20] as [number, number],
  alignSpacing: {
    vertical: 20,
    level: 20
  },
  alignDuration: 300,
  movePx: 10,
  isOpenAuxiliaryLine: true,
  showAuxiliaryLineDistance: 5
} as const

