/*
 * @Author: TaktXp Team
 * @Date: 2024-12-01
 * @Description: 流程设计器配置参数（基于 OpenAuth.Net CreatedFlow）
 * Copyright (c) 2024 by TaktXp Team, All Rights Reserved.
 */

// jsPlumb 实例配置接口
export interface JsPlumbInsConfig {
  Connector: [string, Record<string, unknown>]
  ConnectionOverlays: Array<[string, Record<string, unknown>]>
  PaintStyle: {
    stroke: string
    strokeWidth: number
  }
  HoverPaintStyle: {
    stroke: string
    strokeWidth: number
  }
  EndpointStyle: {
    fill: string
    stroke: string
    strokeWidth: number
    radius: number
  }
  EndpointHoverStyle: {
    fill: string
  }
}

// jsPlumb 配置接口
export interface JsPlumbConfig {
  anchor: {
    default: string[]
  }
  conn: {
    isDetachable: boolean
  }
  makeSourceConfig: {
    filter: string
    filterExclude: boolean
    maxConnections: number
    endpoint: [string, Record<string, unknown>]
    anchor: string[]
  }
  makeTargetConfig: {
    filter: string
    filterExclude: boolean
    maxConnections: number
    endpoint: [string, Record<string, unknown>]
    anchor: string[]
  }
}

// 默认样式配置接口
export interface DefaultStyleConfig {
  dragOpacity: number
  alignGridPX: [number, number]
  alignSpacing: {
    level: number
    vertical: number
  }
  alignDuration: number
  containerScale: {
    init: number
    min: number
    max: number
    onceNarrow: number
    onceEnlarge: number
  }
  isOpenAuxiliaryLine: boolean
  showAuxiliaryLineDistance: number
  movePx: number
  photoBlankDistance: number
}

// 流程状态接口
export interface FlowStatus {
  CREATE: string
  SAVE: string
  MODIFY: string
  LOADING: string
}

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
    left: number | null
    top: number | null
  }
  menulists: MenuItem[]
}

// ID 类型
export type IdType = 'uuid' | 'time_stamp' | ['time_stamp_and_sequence', number] | ['custom', () => string]

// 流程配置接口
export interface FlowConfig {
  jsPlumbInsConfig: JsPlumbInsConfig
  jsPlumbConfig: JsPlumbConfig
  defaultStyle: DefaultStyleConfig
  idType: IdType
  flowStatus: FlowStatus
  shortcut: Record<string, ShortcutConfig>
  contextMenu: {
    container: ContextMenuConfig
    node: ContextMenuConfig
    sl: ContextMenuConfig
  }
}

// 流程配置
export const flowConfig: FlowConfig = {
  jsPlumbInsConfig: {
    Connector: [
      'Flowchart',
      {
        gap: 5,
        cornerRadius: 8,
        alwaysRespectStubs: true
      }
    ],
    ConnectionOverlays: [
      [
        'Arrow',
        {
          width: 10,
          length: 10,
          location: 1
        }
      ]
    ],
    PaintStyle: {
      stroke: '#2a2929',
      strokeWidth: 2
    },
    HoverPaintStyle: {
      stroke: '#1890ff',
      strokeWidth: 3
    },
    EndpointStyle: {
      fill: '#456',
      stroke: '#2a2929',
      strokeWidth: 1,
      radius: 3
    },
    EndpointHoverStyle: {
      fill: '#1890ff'
    }
  },
  jsPlumbConfig: {
    anchor: {
      default: ['Bottom', 'Right', 'Top', 'Left']
    },
    conn: {
      isDetachable: false
    },
    makeSourceConfig: {
      filter: 'a',
      filterExclude: true,
      maxConnections: -1,
      endpoint: ['Dot', { radius: 7 }],
      anchor: ['Bottom', 'Right', 'Top', 'Left']
    },
    makeTargetConfig: {
      filter: 'a',
      filterExclude: true,
      maxConnections: -1,
      endpoint: ['Dot', { radius: 7 }],
      anchor: ['Bottom', 'Right', 'Top', 'Left']
    }
  },
  defaultStyle: {
    dragOpacity: 0.7,
    alignGridPX: [5, 5],
    alignSpacing: {
      level: 100,
      vertical: 100
    },
    alignDuration: 300,
    containerScale: {
      init: 1,
      min: 0.5,
      max: 3,
      onceNarrow: 0.1,
      onceEnlarge: 0.1
    },
    isOpenAuxiliaryLine: true,
    showAuxiliaryLineDistance: 20,
    movePx: 5,
    photoBlankDistance: 200
  },
  // ID的生成类型。1.uuid uuid 2.time_stamp 时间戳 3.sequence 序列 4.time_stamp_and_sequence 时间戳加序列 5.custom 自定义
  idType: 'uuid',
  flowStatus: {
    CREATE: '0',
    SAVE: '1',
    MODIFY: '2',
    LOADING: '3'
  },
  shortcut: {
    multiple: {
      code: 17,
      codeName: 'CTRL',
      shortcutName: '多选'
    },
    dragContainer: {
      code: 32,
      codeName: 'SPACE',
      shortcutName: '画布拖拽'
    },
    scaleContainer: {
      code: 18,
      codeName: 'ALT(firefox下为SHIFT)',
      shortcutName: '画布缩放'
    },
    dragTool: {
      code: 68,
      codeName: 'D',
      shortcutName: '拖拽工具'
    },
    connTool: {
      code: 76,
      codeName: 'L',
      shortcutName: '连线工具'
    },
    zoomInTool: {
      code: 190,
      codeName: '<',
      shortcutName: '放大工具'
    },
    zoomOutTool: {
      code: 188,
      codeName: '>',
      shortcutName: '缩小工具'
    },
    leftMove: {
      code: 37,
      codeName: '←',
      shortcutName: '左移'
    },
    upMove: {
      code: 38,
      codeName: '↑',
      shortcutName: '上移'
    },
    rightMove: {
      code: 39,
      codeName: '→',
      shortcutName: '右移'
    },
    downMove: {
      code: 40,
      codeName: '↓',
      shortcutName: '下移'
    },
    settingModal: {
      code: 83,
      codeName: 'CTRL+ALT+S',
      shortcutName: '打开设置页面'
    },
    testModal: {
      code: 84,
      codeName: 'CTRL+ALT+T',
      shortcutName: '打开测试页面'
    }
  },
  contextMenu: {
    container: {
      menuName: 'flow-menu',
      axis: {
        left: null,
        top: null
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
        left: null,
        top: null
      },
      menulists: [
        {
          fnHandler: 'copyNode',
          icoName: 'edit',
          btnName: '复制节点'
        },
        {
          fnHandler: 'deleteNode',
          icoName: 'edit',
          btnName: '删除节点'
        }
      ]
    },
    sl: {
      menuName: 'link-menu',
      axis: {
        left: null,
        top: null
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
}

