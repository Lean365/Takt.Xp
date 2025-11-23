// Cropper.js 2.0 类型定义
declare module 'cropperjs' {
  // CropperElement 基础接口
  export interface CropperElement extends HTMLElement {
    $define(): void
  }

  // CropperCanvas 接口
  export interface CropperCanvas extends CropperElement {
    // 背景相关
    background?: boolean
    backgroundColor?: string
    
    // 缩放相关
    scaleStep?: number
    minScale?: number
    maxScale?: number
    wheelZoomRatio?: number
    
    // 视图相关
    viewMode?: number
    dragMode?: 'crop' | 'move' | 'none'
    
    // 方法
    getCroppedCanvas(): HTMLCanvasElement
    $zoom(ratio: number): void
    $zoomTo(ratio: number): void
    $resetZoom(): void
    $setDragMode(mode: string): void
    $setViewMode(mode: number): void
  }

  // CropperImage 接口
  export interface CropperImage extends CropperElement {
    // 基本属性
    src: string
    alt?: string
    crossorigin?: string
    
    // 变换相关
    translatable?: boolean
    rotatable?: boolean
    scalable?: boolean
    zoomable?: boolean
    
    // 变换值
    translateX?: number
    translateY?: number
    rotate?: number
    scaleX?: number
    scaleY?: number
    
    // 方法
    $rotate(degree: number): void
    $scale(ratio: number): void
    $translate(x: number, y: number): void
    $resetTransform(): void
    $setTransform(transform: {
      translateX?: number
      translateY?: number
      rotate?: number
      scaleX?: number
      scaleY?: number
    }): void
  }

  // CropperShade 接口
  export interface CropperShade extends CropperElement {
    hidden?: boolean
  }

  // CropperSelection 接口
  export interface CropperSelection extends CropperElement {
    // 尺寸和比例
    aspectRatio?: number
    initialAspectRatio?: number
    initialCoverage?: number
    minWidth?: number
    minHeight?: number
    maxWidth?: number
    maxHeight?: number
    
    // 交互控制
    movable?: boolean
    resizable?: boolean
    rotatable?: boolean
    scalable?: boolean
    
    // 位置和尺寸
    x?: number
    y?: number
    width?: number
    height?: number
    
    // 样式相关
    borderColor?: string
    borderWidth?: number
    borderStyle?: string
    backgroundColor?: string
    
    // 方法
    $reset(): void
    $toCanvas(): Promise<HTMLCanvasElement>
    $setPosition(x: number, y: number): void
    $setSize(width: number, height: number): void
    $setAspectRatio(ratio: number): void
    $setStyle(style: Partial<CSSStyleDeclaration>): void
    $getData(): {
      x: number
      y: number
      width: number
      height: number
      rotate: number
      scaleX: number
      scaleY: number
    }
  }

  // CropperGrid 接口
  export interface CropperGrid extends CropperElement {
    // 网格样式
    bordered?: boolean
    covered?: boolean
    borderColor?: string
    borderWidth?: number
    gridColor?: string
    gridWidth?: number
    
    // 网格密度
    rows?: number
    cols?: number
    
    // 方法
    $show(): void
    $hide(): void
    $setGrid(rows: number, cols: number): void
    $setStyle(style: Partial<CSSStyleDeclaration>): void
  }

  // CropperCrosshair 接口
  export interface CropperCrosshair extends CropperElement {
    // 十字线样式
    centered?: boolean
    borderColor?: string
    borderWidth?: number
    borderStyle?: string
    
    // 十字线尺寸
    size?: number | string
    width?: number | string
    height?: number | string
    
    // 方法
    $show(): void
    $hide(): void
    $setPosition(x: number, y: number): void
    $setStyle(style: Partial<CSSStyleDeclaration>): void
  }

  // CropperHandle 接口
  export interface CropperHandle extends CropperElement {
    // 操作类型
    action: string
    
    // 样式相关
    plain?: boolean
    themeColor?: string
    
    // 布局相关
    position?: 'top' | 'bottom' | 'left' | 'right' | 'top-left' | 'top-right' | 'bottom-left' | 'bottom-right' | 'center'
    
    // 尺寸相关
    size?: number | string
    width?: number | string
    height?: number | string
    
    // 外观相关
    shape?: 'circle' | 'square' | 'diamond'
    borderWidth?: number
    borderColor?: string
    backgroundColor?: string
    
    // 交互相关
    disabled?: boolean
    draggable?: boolean
    
    // 方法
    $show(): void
    $hide(): void
    $enable(): void
    $disable(): void
    $setPosition(x: number, y: number): void
    $setSize(width: number, height: number): void
    $setTheme(color: string): void
  }

  // 导出所有组件
  export const CropperCanvas: {
    new (): CropperCanvas
    $define(): void
  }

  export const CropperImage: {
    new (): CropperImage
    $define(): void
  }

  export const CropperShade: {
    new (): CropperShade
    $define(): void
  }

  export const CropperSelection: {
    new (): CropperSelection
    $define(): void
  }

  export const CropperGrid: {
    new (): CropperGrid
    $define(): void
  }

  export const CropperCrosshair: {
    new (): CropperCrosshair
    $define(): void
  }

  export const CropperHandle: {
    new (): CropperHandle
    $define(): void
  }
}

export {} 