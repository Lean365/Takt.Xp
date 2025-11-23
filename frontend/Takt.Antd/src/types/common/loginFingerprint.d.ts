/**
 * 登录设备指纹信息模型
 */
export interface TaktLoginFingerprint {
  /** 设备ID */
  deviceId?: string;
  /** 设备类型 */
  deviceType?: string;
  /** 设备指纹（唯一标识） */
  loginFingerprint?: string;
  /** 语言 */
  language?: string;
  /** 时区 */
  timezone?: string;
  /** 屏幕分辨率 */
  screenResolution?: string;
  /** 颜色深度 */
  colorDepth?: number;
  /** 像素比 */
  pixelRatio?: number;
  /** Canvas指纹 */
  canvasFingerprint?: string;
  /** WebGL指纹 */
  webglFingerprint?: string;
  /** 音频指纹 */
  audioFingerprint?: string;
  /** 字体指纹 */
  fontsFingerprint?: string;
  /** 插件指纹 */
  pluginsFingerprint?: string;
  /** 触摸支持信息 */
  touchSupport?: string;
  /** 操作系统 */
  os?: string;
  /** 浏览器 */
  browser?: string;
  /** 是否VPN */
  isVpn?: string;
  /** 是否代理 */
  isProxy?: string;
  /** 网络类型 */
  networkType?: string;
  /** 硬件并发数 */
  hardwareConcurrency?: number;
  /** 设备内存 */
  deviceMemory?: string;
  /** 平台 */
  platform?: string;
  /** Cookie是否启用 */
  cookieEnabled?: boolean;
  /** 不跟踪设置 */
  doNotTrack?: string;
}

