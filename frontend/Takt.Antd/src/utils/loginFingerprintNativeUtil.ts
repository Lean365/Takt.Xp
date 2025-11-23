/**
 * 原生设备指纹收集工具
 * 不依赖第三方库，使用原生 Web API 收集设备特征信息
 */

import type { TaktLoginFingerprint } from '@/types/common/loginFingerprint'

// 类型声明
declare global {
  interface Window {
    webkitAudioContext: typeof AudioContext
  }
}

/**
 * 设备指纹收集器类
 */
export class DeviceNativeCollector {
  private static instance: DeviceNativeCollector
  private cachedFingerprint: string | null = null
  private cachedDeviceInfo: TaktLoginFingerprint | null = null

  private constructor() {
    // 私有构造函数，确保单例模式
  }

  /**
   * 获取 DeviceNativeCollector 实例
   */
  public static getInstance(): DeviceNativeCollector {
    if (!DeviceNativeCollector.instance) {
      DeviceNativeCollector.instance = new DeviceNativeCollector()
    }
    return DeviceNativeCollector.instance
  }

  /**
   * 生成设备指纹
   * 基于多个设备特征生成唯一标识
   */
  public async generateFingerprint(): Promise<string> {
    if (this.cachedFingerprint) {
      return this.cachedFingerprint
    }

    try {
      const features = await this.collectDeviceFeatures()
      const featureString = Object.values(features).join('|')
      
      // 使用稳定的哈希算法，不包含时间戳或随机数
      let hash = 0
      for (let i = 0; i < featureString.length; i++) {
        const char = featureString.charCodeAt(i)
        hash = ((hash << 5) - hash) + char
        hash = hash & hash // Convert to 32bit integer
      }
      
      // 只使用哈希值，确保同一设备每次生成都相同
      const fingerprint = Math.abs(hash).toString(36)
      
      this.cachedFingerprint = fingerprint
      return fingerprint
    } catch (error) {
      console.error('生成设备指纹失败:', error)
      return this.getFallbackFingerprint()
    }
  }

  /**
   * 收集设备特征
   */
  private async collectDeviceFeatures(): Promise<Record<string, any>> {
    const features: Record<string, any> = {
      userAgent: navigator.userAgent,
      platform: navigator.platform,
      language: navigator.language,
      screenWidth: window.screen.width,
      screenHeight: window.screen.height,
      colorDepth: window.screen.colorDepth,
      hardwareConcurrency: navigator.hardwareConcurrency || 'unknown',
      maxTouchPoints: navigator.maxTouchPoints || '0',
      devicePixelRatio: window.devicePixelRatio || '1',
      vendor: navigator.vendor || 'unknown',
      product: navigator.product || 'unknown',
      appVersion: navigator.appVersion || 'unknown',
      timeZone: Intl.DateTimeFormat().resolvedOptions().timeZone || 'unknown',
      screenOrientation: window.screen.orientation?.type || 'unknown',
      touchSupport: this.getTouchSupportInfo(),
      webglSupport: 'WebGLRenderingContext' in window ? 'webgl' : 'no-webgl'
    }

    // 收集 Canvas 指纹
    try {
      features.canvas = await this.getCanvasFingerprint()
    } catch (error) {
      features.canvas = 'error'
    }

    // 收集 WebGL 指纹
    try {
      features.webgl = await this.getWebGLFingerprint()
    } catch (error) {
      features.webgl = 'error'
    }

    // 收集音频指纹
    try {
      features.audio = await this.getAudioFingerprint()
    } catch (error) {
      features.audio = 'error'
    }

    // 收集字体指纹
    try {
      features.fonts = await this.getFontFingerprint()
    } catch (error) {
      features.fonts = 'error'
    }

    // 收集插件信息
    try {
      features.plugins = this.getPluginFingerprint()
    } catch (error) {
      features.plugins = 'error'
    }

    return features
  }

  /**
   * 获取 Canvas 指纹
   */
  private async getCanvasFingerprint(): Promise<string> {
    try {
      const canvas = document.createElement('canvas')
      const ctx = canvas.getContext('2d')
      if (!ctx) return 'no-canvas'

      // 设置画布大小
      canvas.width = 200
      canvas.height = 200

      // 绘制一些图形和文字
      ctx.textBaseline = 'top'
      ctx.font = '14px Arial'
      ctx.fillText('Device Fingerprint', 2, 2)
      
      // 绘制矩形
      ctx.fillStyle = 'rgba(102, 204, 0, 0.7)'
      ctx.fillRect(100, 5, 90, 20)
      ctx.strokeStyle = 'rgba(204, 0, 102, 0.7)'
      ctx.strokeRect(5, 100, 90, 20)
      ctx.fillStyle = 'rgba(0, 102, 204, 0.7)'
      ctx.fillRect(100, 100, 90, 20)

      // 添加渐变
      const gradient = ctx.createLinearGradient(0, 0, 200, 200)
      gradient.addColorStop(0, 'rgba(255, 0, 0, 0.5)')
      gradient.addColorStop(1, 'rgba(0, 255, 0, 0.5)')
      ctx.fillStyle = gradient
      ctx.fillRect(0, 0, 200, 200)

      // 添加一些路径
      ctx.beginPath()
      ctx.moveTo(50, 50)
      ctx.lineTo(150, 50)
      ctx.lineTo(100, 150)
      ctx.closePath()
      ctx.strokeStyle = 'rgba(255, 255, 0, 0.8)'
      ctx.lineWidth = 2
      ctx.stroke()

      // 生成数据URL并计算哈希
      const dataURL = canvas.toDataURL()
      
      // 计算简单的哈希值而不是返回完整的数据URL
      let hash = 0
      for (let i = 0; i < dataURL.length; i++) {
        const char = dataURL.charCodeAt(i)
        hash = ((hash << 5) - hash) + char
        hash = hash & hash
      }
      
      return Math.abs(hash).toString(36)
    } catch (error) {
      console.error('获取 Canvas 指纹失败:', error)
      return 'error'
    }
  }

  /**
   * 获取 WebGL 指纹
   */
  private async getWebGLFingerprint(): Promise<string> {
    try {
      const canvas = document.createElement('canvas')
      const gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl')
      if (!gl) return 'no-webgl'

      // 确保 gl 是 WebGLRenderingContext 类型
      const webgl = gl as WebGLRenderingContext
      
      const debugInfo = webgl.getExtension('WEBGL_debug_renderer_info')
      if (!debugInfo) return 'no-debug-info'

      const vendor = webgl.getParameter(debugInfo.UNMASKED_VENDOR_WEBGL) || 'unknown'
      const renderer = webgl.getParameter(debugInfo.UNMASKED_RENDERER_WEBGL) || 'unknown'
      const version = webgl.getParameter(webgl.VERSION) || 'unknown'
      const shadingLanguageVersion = webgl.getParameter(webgl.SHADING_LANGUAGE_VERSION) || 'unknown'

      // 获取支持的扩展
      const extensions = webgl.getSupportedExtensions() || []
      const extensionsString = extensions.join(',')

      // 获取 WebGL 参数
      const parameters = [
        webgl.MAX_TEXTURE_SIZE,
        webgl.MAX_VIEWPORT_DIMS,
        webgl.MAX_VERTEX_UNIFORM_VECTORS,
        webgl.MAX_FRAGMENT_UNIFORM_VECTORS,
        webgl.MAX_VERTEX_ATTRIBS,
        webgl.MAX_VERTEX_TEXTURE_IMAGE_UNITS,
        webgl.MAX_TEXTURE_IMAGE_UNITS
      ]

      const fingerprint = [
        vendor,
        renderer,
        version,
        shadingLanguageVersion,
        extensionsString,
        parameters.join(',')
      ].join('|')

      return fingerprint
    } catch (error) {
      console.error('获取 WebGL 指纹失败:', error)
      return 'error'
    }
  }

  /**
   * 获取音频指纹
   */
  private async getAudioFingerprint(): Promise<string> {
    try {
      // 检查是否支持 Web Audio API
      if (!window.AudioContext && !(window as any).webkitAudioContext) {
        return 'no-audio-api'
      }

      const audioContext = new (window.AudioContext || (window as any).webkitAudioContext)()
      const oscillator = audioContext.createOscillator()
      const analyser = audioContext.createAnalyser()
      const gainNode = audioContext.createGain()
      const scriptProcessor = audioContext.createScriptProcessor(4096, 1, 1)

      oscillator.type = 'triangle'
      oscillator.connect(analyser)
      analyser.connect(scriptProcessor)
      scriptProcessor.connect(gainNode)
      gainNode.connect(audioContext.destination)

      oscillator.frequency.setValueAtTime(10000, audioContext.currentTime)
      gainNode.gain.setValueAtTime(0, audioContext.currentTime)

      oscillator.start(0)
      gainNode.gain.setValueAtTime(1, audioContext.currentTime)
      gainNode.gain.exponentialRampToValueAtTime(1, audioContext.currentTime + 0.001)

      // 收集音频特征
      const features = [
        audioContext.sampleRate,
        audioContext.destination.maxChannelCount || 2,
        analyser.fftSize,
        analyser.frequencyBinCount,
        gainNode.gain.value,
        oscillator.frequency.value
      ]

      oscillator.stop()
      audioContext.close()

      return features.join('|')
    } catch (error) {
      console.error('获取音频指纹失败:', error)
      return 'error'
    }
  }

  /**
   * 获取字体指纹
   */
  private getFontFingerprint(): string {
    try {
      // 只检测5种最基本的字体
      const basicFonts = ['Arial', 'Times New Roman', 'Courier New', 'Verdana', 'Georgia']
      
      // 使用简单的测试字符串
      const testString = 'mmmmmmmmmmlli'
      const testSize = '16px'
      
      const h = document.createElement('span')
      h.style.fontSize = testSize
      h.innerHTML = testString
      h.style.position = 'absolute'
      h.style.visibility = 'hidden'
      h.style.whiteSpace = 'nowrap'
      document.body.appendChild(h)

      const detectedFonts: string[] = []
      const fallbackFonts = ['monospace', 'sans-serif', 'serif']

      // 检测每种字体
      for (const font of basicFonts) {
        let isDetected = false
        
        // 获取基准尺寸
        const baseWidths: { [key: string]: number } = {}
        const baseHeights: { [key: string]: number } = {}
        
        for (const fallbackFont of fallbackFonts) {
          h.style.fontFamily = fallbackFont
          baseWidths[fallbackFont] = h.offsetWidth
          baseHeights[fallbackFont] = h.offsetHeight
        }
        
        // 测试目标字体
        h.style.fontFamily = `${font},${fallbackFonts.join(',')}`
        const fontWidth = h.offsetWidth
        const fontHeight = h.offsetHeight
        
        // 检查是否与基准字体不同
        for (const fallbackFont of fallbackFonts) {
          if (Math.abs(fontWidth - baseWidths[fallbackFont]) > 1 || 
              Math.abs(fontHeight - baseHeights[fallbackFont]) > 1) {
            isDetected = true
            break
          }
        }
        
        if (isDetected) {
          detectedFonts.push(font)
        }
      }

      // 安全地移除元素
      if (document.body.contains(h)) {
        document.body.removeChild(h)
      }
      
      return detectedFonts.join(',')
    } catch (error) {
      console.error('获取字体指纹失败:', error)
      return 'error'
    }
  }

  /**
   * 获取系统字体列表
   */
  private getSystemFonts(): string[] {
    // 只返回5种基本字体
    return ['Arial', 'Times New Roman', 'Courier New', 'Verdana', 'Georgia']
  }

  /**
   * 获取插件指纹
   */
  private getPluginFingerprint(): string {
    try {
      if (!navigator.plugins || navigator.plugins.length === 0) {
        return 'no-plugins'
      }

      const plugins: string[] = []
      for (let i = 0; i < navigator.plugins.length; i++) {
        const plugin = navigator.plugins[i]
        if (plugin.name && plugin.description) {
          plugins.push(`${plugin.name}:${plugin.description}`)
        }
      }

      return plugins.join('|')
    } catch (error) {
      console.error('获取插件指纹失败:', error)
      return 'error'
    }
  }

  /**
   * 获取备用指纹
   */
  private getFallbackFingerprint(): string {
    const basicInfo = [
      navigator.userAgent,
      navigator.language,
      screen.width,
      screen.height,
      screen.colorDepth,
      window.devicePixelRatio,
      navigator.hardwareConcurrency,
      this.getDeviceMemoryInfo(),
      navigator.platform
    ].join('|')
    
    // 稳定的哈希函数，确保同一设备每次生成都相同
    let hash = 0
    for (let i = 0; i < basicInfo.length; i++) {
      const char = basicInfo.charCodeAt(i)
      hash = ((hash << 5) - hash) + char
      hash = hash & hash // 转换为 32 位整数
    }
    
    return Math.abs(hash).toString(36)
  }

  /**
   * 获取完整的设备信息
   */
  public async getDeviceInfo(): Promise<TaktLoginFingerprint> {
    if (this.cachedDeviceInfo) {
      return this.cachedDeviceInfo
    }

    try {
      const fingerprint = await this.generateFingerprint()
      
      // 并行执行 VPN 和代理检测
      const [isVpn, isProxy] = await Promise.all([
        this.checkVpn(),
        this.checkProxy()
      ])
      
      const deviceInfo: TaktLoginFingerprint = {
        deviceId: fingerprint, // 使用指纹作为设备ID
        deviceType: this.getDeviceType(),
        loginFingerprint: fingerprint,
        language: navigator.language,
        timezone: Intl.DateTimeFormat().resolvedOptions().timeZone,
        screenResolution: `${screen.width}x${screen.height}`,
        colorDepth: screen.colorDepth,
        pixelRatio: window.devicePixelRatio,
        canvasFingerprint: await this.getCanvasFingerprint(),
        webglFingerprint: await this.getWebGLFingerprint(),
        audioFingerprint: await this.getAudioFingerprint(),
        fontsFingerprint: await this.getFontFingerprint(),
        pluginsFingerprint: this.getPluginFingerprint(),
        touchSupport: this.getTouchSupportInfo(),
        os: this.getSystemVersion(),
        browser: this.getBrowserVersion(),
        isVpn: isVpn ? 'yes' : 'no',
        isProxy: isProxy ? 'proxy' : 'direct',
        networkType: this.getNetworkType(),
        hardwareConcurrency: navigator.hardwareConcurrency || 0,
        deviceMemory: this.getDeviceMemoryInfo(),
        platform: navigator.platform,
        cookieEnabled: navigator.cookieEnabled,
        doNotTrack: navigator.doNotTrack || 'unknown'
      }

      // 限制设备指纹数据大小，避免过大的数据导致序列化问题
      const sanitizedDeviceInfo = this.sanitizeDeviceInfo(deviceInfo)

      this.cachedDeviceInfo = sanitizedDeviceInfo
      return sanitizedDeviceInfo
    } catch (error) {
      console.error('获取设备信息失败:', error)
      return this.getBasicDeviceInfo()
    }
  }

  /**
   * 清理设备信息，保留所有字段的完整信息
   */
  private sanitizeDeviceInfo(deviceInfo: TaktLoginFingerprint): TaktLoginFingerprint {
    return {
      deviceId: deviceInfo.deviceId,
      deviceType: deviceInfo.deviceType,
      loginFingerprint: deviceInfo.loginFingerprint,
      language: deviceInfo.language,
      timezone: deviceInfo.timezone,
      screenResolution: deviceInfo.screenResolution,
      colorDepth: deviceInfo.colorDepth,
      pixelRatio: deviceInfo.pixelRatio,
      canvasFingerprint: deviceInfo.canvasFingerprint,
      webglFingerprint: deviceInfo.webglFingerprint,
      audioFingerprint: deviceInfo.audioFingerprint,
      fontsFingerprint: deviceInfo.fontsFingerprint,
      pluginsFingerprint: deviceInfo.pluginsFingerprint,
      touchSupport: deviceInfo.touchSupport,
      os: deviceInfo.os,
      browser: deviceInfo.browser,
      isVpn: deviceInfo.isVpn,
      isProxy: deviceInfo.isProxy,
      networkType: deviceInfo.networkType,
      hardwareConcurrency: deviceInfo.hardwareConcurrency,
      deviceMemory: deviceInfo.deviceMemory,
      platform: deviceInfo.platform,
      cookieEnabled: deviceInfo.cookieEnabled,
      doNotTrack: deviceInfo.doNotTrack
    }
  }

  /**
   * 截断字符串到指定长度
   */
  private truncateString(str: string | undefined, maxLength: number): string {
    if (!str) return ''
    return str.length > maxLength ? str.substring(0, maxLength) : str
  }

  /**
   * 获取基本设备信息（备用方案）
   */
  private getBasicDeviceInfo(): TaktLoginFingerprint {
    // 进行基本的 VPN 和代理检测
    const userAgent = navigator.userAgent;
    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    const language = navigator.language;
    
    // 基本 VPN 检测
    const isVpn = userAgent.includes('VPN') || 
                  userAgent.includes('Proxy') || 
                  userAgent.includes('TOR') ||
                  ['UTC', 'GMT'].includes(timezone);
    
    // 基本代理检测
    const isProxy = userAgent.includes('Proxy') || 
                    userAgent.includes('TOR') || 
                    userAgent.includes('VPN');
    
    const basicInfo: TaktLoginFingerprint = {
      deviceId: this.getFallbackFingerprint(), // 使用备用指纹作为设备ID
      deviceType: this.getDeviceType(),
      loginFingerprint: this.getFallbackFingerprint(),
      language: navigator.language,
      timezone: Intl.DateTimeFormat().resolvedOptions().timeZone,
      screenResolution: `${screen.width}x${screen.height}`,
      colorDepth: screen.colorDepth,
      pixelRatio: window.devicePixelRatio,
      canvasFingerprint: 'unknown',
      webglFingerprint: 'unknown',
      audioFingerprint: 'unknown',
      fontsFingerprint: 'unknown',
      pluginsFingerprint: 'unknown',
      touchSupport: this.getTouchSupportInfo(),
      os: this.getSystemVersion(),
      browser: this.getBrowserVersion(),
      isVpn: isVpn ? 'yes' : 'no',
      isProxy: isProxy ? 'proxy' : 'direct',
      networkType: this.getNetworkType(),
      hardwareConcurrency: navigator.hardwareConcurrency || 0,
      deviceMemory: this.getDeviceMemoryInfo(),
      platform: navigator.platform,
      cookieEnabled: navigator.cookieEnabled,
      doNotTrack: navigator.doNotTrack || 'unknown'
    }

    // 同样清理基本设备信息
    return this.sanitizeDeviceInfo(basicInfo)
  }

  /**
   * 获取设备内存信息
   */
  private getDeviceMemoryInfo(): string {
    try {
      // 检查是否支持 deviceMemory API
      if ('deviceMemory' in navigator) {
        const memory = (navigator as any).deviceMemory
        if (memory && typeof memory === 'number' && memory > 0) {
          // deviceMemory 返回的是 GB 为单位
          if (memory >= 1) {
            return `${memory} GB`
          } else {
            // 小于 1GB 的情况，转换为 MB
            return `${Math.round(memory * 1024)} MB`
          }
        }
      }

      // 尝试通过其他方式检测内存
      // 检查是否支持 memory API
      if ('memory' in performance) {
        const memory = (performance as any).memory
        if (memory && memory.jsHeapSizeLimit) {
                  const totalMemoryMB = Math.round(memory.jsHeapSizeLimit / (1024 * 1024))
        return `${totalMemoryMB}MB-JSHeapLimit`
        }
      }

      // 检查是否支持 hardwareConcurrency 来估算
      if (navigator.hardwareConcurrency) {
        const cores = navigator.hardwareConcurrency
        // 根据核心数粗略估算内存（这是一个估算值）
        if (cores <= 2) return '2-4GB-Estimated'
        if (cores <= 4) return '4-8GB-Estimated'
        if (cores <= 8) return '8-16GB-Estimated'
        if (cores <= 16) return '16-32GB-Estimated'
        return '32PlusGB-Estimated'
      }

      return 'unknown'
    } catch (error) {
      console.error('获取设备内存信息失败:', error)
      return 'unknown'
    }
  }



  /**
   * 获取触摸支持信息
   */
  private getTouchSupportInfo(): string {
    try {
      // 方法1: 检查触摸事件支持
      const hasTouchEvents = 'ontouchstart' in window;
      
      // 方法2: 检查触摸点数量（最可靠的方法）
      const maxTouchPoints = navigator.maxTouchPoints || 0;
      
      // 方法3: 检查触摸事件监听器
      let hasTouchListeners = false;
      try {
        const testElement = document.createElement('div');
        testElement.addEventListener('touchstart', () => {}, { passive: true });
        hasTouchListeners = true;
      } catch (e) {
        hasTouchListeners = false;
      }
      
      // 方法4: 检查平台信息
      const platform = navigator.platform.toLowerCase();
      const isDesktop = platform.includes('win') || 
                       platform.includes('mac') || 
                       platform.includes('linux');
      
      // 方法5: 检查屏幕尺寸和像素密度
      const screenWidth = window.screen.width;
      const screenHeight = window.screen.height;
      const pixelRatio = window.devicePixelRatio || 1;
      
      // 综合判断逻辑 - 基于硬件能力，而不是用户代理
      if (maxTouchPoints > 0) {
        // 有明确的触摸点数量，说明支持触摸
        const deviceType = isDesktop ? 'Desktop' : 'Mobile';
        return `Touch-${maxTouchPoints}Point-${deviceType}`;
      } else if (hasTouchEvents && hasTouchListeners) {
        // 支持触摸事件和监听器，但没有触摸点信息
        if (isDesktop) {
          return 'PossibleTouch-Desktop-Events';
        } else {
          return 'Touch-Events';
        }
      } else if (isDesktop) {
        // 桌面设备且不支持触摸事件
        return 'NoTouch-Desktop';
      } else {
        // 其他情况
        return 'NoTouch';
      }
    } catch (error) {
      console.error('检测触摸支持失败:', error);
      return 'DetectionFailed';
    }
  }

  /**
   * 获取设备类型
   */
  public getDeviceType(): string {
    try {
      // 方法1: 检查触摸点数量（最可靠）
      const maxTouchPoints = navigator.maxTouchPoints || 0;
      
      // 方法2: 检查触摸事件支持
      const hasTouchEvents = 'ontouchstart' in window;
      
      // 方法3: 检查平台信息
      const platform = navigator.platform.toLowerCase();
      const isWindows = platform.includes('win');
      const isMac = platform.includes('mac');
      const isLinux = platform.includes('linux');
      
      // 方法4: 检查屏幕特征
      const screenWidth = window.screen.width;
      const screenHeight = window.screen.height;
      const pixelRatio = window.devicePixelRatio || 1;
      
      // 方法5: 检查用户代理（仅作为辅助）
      const userAgent = navigator.userAgent.toLowerCase();
      const hasMobileUA = userAgent.includes('mobile');
      const hasTabletUA = userAgent.includes('tablet');
      
      // 综合判断逻辑
      if (maxTouchPoints > 0) {
        // 有触摸能力，根据平台判断
        if (isWindows) {
          return 'TouchScreen-Windows';
        } else if (isMac) {
          return 'TouchScreen-Mac';
        } else if (isLinux) {
          return 'TouchScreen-Linux';
        } else {
          return 'TouchDevice';
        }
      } else if (hasTouchEvents) {
        // 支持触摸事件但没有触摸点信息
        if (isWindows) {
          return 'PossibleTouch-Windows';
        } else if (isMac) {
          return 'PossibleTouch-Mac';
        } else if (isLinux) {
          return 'PossibleTouch-Linux';
        } else {
          return 'TouchEventDevice';
        }
      } else if (isWindows || isMac || isLinux) {
        // 标准桌面设备
        if (isWindows) {
          return 'Windows-Desktop';
        } else if (isMac) {
          return 'macOS-Desktop';
        } else {
          return 'Linux-Desktop';
        }
      } else if (hasMobileUA || hasTabletUA) {
        // 移动设备
        if (hasMobileUA) {
          return 'Mobile';
        } else {
          return 'Tablet';
        }
      } else {
        // 其他情况
        return 'Unknown';
      }
    } catch (error) {
      console.error('检测设备类型失败:', error);
      return 'DetectionFailed';
    }
  }

  /**
   * 获取系统版本
   */
  private getSystemVersion(): string {
    try {
      const userAgent = navigator.userAgent;
      
      // Windows 系统版本检测
      if (userAgent.includes('Windows NT')) {
        const match = userAgent.match(/Windows NT (\d+\.\d+)/);
        if (match) {
          const version = match[1];
          switch (version) {
            case '10.0': return 'Windows 10/11';
            case '6.3': return 'Windows 8.1';
            case '6.2': return 'Windows 8';
            case '6.1': return 'Windows 7';
            case '6.0': return 'Windows Vista';
            default: return `Windows NT ${version}`;
          }
        }
      }
      
      // macOS 系统版本检测
      if (userAgent.includes('Mac OS X')) {
        const match = userAgent.match(/Mac OS X (\d+[._]\d+)/);
        if (match) {
          const version = match[1].replace('_', '.');
          return `macOS ${version}`;
        }
      }
      
      // Linux 系统检测
      if (userAgent.includes('Linux')) {
        return 'Linux';
      }
      
      // Android 系统检测
      if (userAgent.includes('Android')) {
        const match = userAgent.match(/Android (\d+\.\d+)/);
        if (match) {
          return `Android ${match[1]}`;
        }
        return 'Android';
      }
      
      // iOS 系统检测
      if (userAgent.includes('iPhone') || userAgent.includes('iPad')) {
        const match = userAgent.match(/OS (\d+[._]\d+)/);
        if (match) {
          const version = match[1].replace('_', '.');
          return `iOS ${version}`;
        }
        return 'iOS';
      }
      
      return 'UnknownOS';
    } catch (error) {
      console.error('获取系统版本失败:', error);
      return 'Unknown';
    }
  }

  /**
   * 获取浏览器版本
   */
  private getBrowserVersion(): string {
    try {
      const userAgent = navigator.userAgent;
      
      // Edge 浏览器（必须在 Chrome 之前检测）
      if (userAgent.includes('Edg/')) {
        const match = userAgent.match(/Edg\/(\d+\.\d+\.\d+)/);
        if (match) {
          return `Edge ${match[1]}`;
        }
      }
      
      // Chrome 浏览器
      if (userAgent.includes('Chrome/')) {
        const match = userAgent.match(/Chrome\/(\d+\.\d+\.\d+)/);
        if (match) {
          return `Chrome ${match[1]}`;
        }
      }
      
      // Firefox 浏览器
      if (userAgent.includes('Firefox/')) {
        const match = userAgent.match(/Firefox\/(\d+\.\d+)/);
        if (match) {
          return `Firefox ${match[1]}`;
        }
      }
      
      // Safari 浏览器
      if (userAgent.includes('Safari/') && !userAgent.includes('Chrome')) {
        const match = userAgent.match(/Version\/(\d+\.\d+)/);
        if (match) {
          return `Safari ${match[1]}`;
        }
        return 'Safari';
      }
      
      // Opera 浏览器
      if (userAgent.includes('OPR/') || userAgent.includes('Opera/')) {
        const match = userAgent.match(/(?:OPR|Opera)\/(\d+\.\d+)/);
        if (match) {
          return `Opera ${match[1]}`;
        }
      }
      
      // IE 浏览器
      if (userAgent.includes('MSIE ') || userAgent.includes('Trident/')) {
        const match = userAgent.match(/MSIE (\d+\.\d+)/);
        if (match) {
          return `Internet Explorer ${match[1]}`;
        }
        return 'Internet Explorer';
      }
      
      return 'UnknownBrowser';
    } catch (error) {
      console.error('获取浏览器版本失败:', error);
      return 'Unknown';
    }
  }

  /**
   * 获取网络类型
   */
  private getNetworkType(): string {
    try {
      // 检查是否支持网络信息 API
      if ('connection' in navigator) {
        const connection = (navigator as any).connection;
        if (connection && connection.effectiveType) {
          return connection.effectiveType;
        }
      }
      
      // 检查是否支持网络状态 API
      if ('onLine' in navigator) {
        return navigator.onLine ? 'online' : 'offline';
      }
      
      return 'unknown';
    } catch (error) {
      console.error('获取网络类型失败:', error);
      return 'unknown';
    }
  }

  /**
   * 检查是否使用 VPN
   */
  private async checkVpn(): Promise<boolean> {
    try {
      // 方法1: 检查 WebRTC 泄露真实 IP
      const hasWebRTCLeak = await this.checkWebRTCLeak();
      if (hasWebRTCLeak) {
        return true; // 检测到 IP 泄露，可能是 VPN
      }

      // 方法2: 检查时区是否异常
      const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
      const suspiciousTimezones = ['UTC', 'GMT'];
      if (suspiciousTimezones.includes(timezone)) {
        return true; // 可能是 VPN
      }

      // 方法3: 检查用户代理中的 VPN 标识
      const userAgent = navigator.userAgent;
      if (userAgent.includes('VPN') || userAgent.includes('Proxy') || userAgent.includes('TOR')) {
        return true;
      }

      // 方法4: 检查语言与时区是否匹配
      const languages = navigator.languages || [navigator.language];
      const timezoneLower = timezone.toLowerCase();
      
      // 检查是否有明显的语言与时区不匹配
      const hasMismatch = languages.some(lang => {
        const langLower = lang.toLowerCase();
        if (langLower.startsWith('zh') && !timezoneLower.includes('asia')) return true;
        if (langLower.startsWith('en') && !timezoneLower.includes('america') && !timezoneLower.includes('europe') && !timezoneLower.includes('australia')) return true;
        if (langLower.startsWith('ja') && !timezoneLower.includes('asia')) return true;
        return false;
      });
      
      if (hasMismatch) {
        return true; // 语言与时区不匹配，可能是 VPN
      }

      return false;
    } catch (error) {
      console.error('检查 VPN 失败:', error);
      return false;
    }
  }

  /**
   * 检查 WebRTC 泄露真实 IP
   */
  private async checkWebRTCLeak(): Promise<boolean> {
    try {
      // 创建 RTCPeerConnection 来检测 IP 泄露
      const pc = new RTCPeerConnection({
        iceServers: [{ urls: 'stun:stun.l.google.com:19302' }]
      });

      return new Promise((resolve) => {
        let hasLocalIP = false;
        
        pc.onicecandidate = (event) => {
          if (event.candidate) {
            const candidate = event.candidate.candidate;
            // 检查是否包含本地 IP 地址
            if (candidate.includes('192.168.') || 
                candidate.includes('10.') || 
                candidate.includes('172.16.') ||
                candidate.includes('172.17.') ||
                candidate.includes('172.18.') ||
                candidate.includes('172.19.') ||
                candidate.includes('172.20.') ||
                candidate.includes('172.21.') ||
                candidate.includes('172.22.') ||
                candidate.includes('172.23.') ||
                candidate.includes('172.24.') ||
                candidate.includes('172.25.') ||
                candidate.includes('172.26.') ||
                candidate.includes('172.27.') ||
                candidate.includes('172.28.') ||
                candidate.includes('172.29.') ||
                candidate.includes('172.30.') ||
                candidate.includes('172.31.')) {
              hasLocalIP = true;
            }
          }
        };

        // 创建数据通道触发 ICE 收集
        pc.createDataChannel('test');
        pc.createOffer()
          .then(offer => pc.setLocalDescription(offer))
          .catch(() => resolve(false));

        // 5秒后检查结果
        setTimeout(() => {
          pc.close();
          resolve(hasLocalIP);
        }, 5000);
      });
    } catch (error) {
      console.error('WebRTC 检测失败:', error);
      return false;
    }
  }

  /**
   * 检查是否使用代理
   */
  private async checkProxy(): Promise<boolean> {
    try {
      // 方法1: 检查 WebRTC 泄露真实 IP
      const hasWebRTCLeak = await this.checkWebRTCLeak();
      if (hasWebRTCLeak) {
        return true; // 检测到 IP 泄露，可能是代理
      }

      // 方法2: 检查用户代理中的代理标识
      const userAgent = navigator.userAgent;
      if (userAgent.includes('Proxy') || userAgent.includes('TOR') || userAgent.includes('VPN')) {
        return true;
      }

      // 方法3: 检查是否在私有网络
      const connection = (navigator as any).connection;
      if (connection && connection.type === 'none') {
        return true; // 可能是代理
      }

      // 方法4: 检查语言与时区是否匹配
      const languages = navigator.languages || [navigator.language];
      const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
      const timezoneLower = timezone.toLowerCase();
      
      // 检查是否有明显的语言与时区不匹配
      const hasMismatch = languages.some(lang => {
        const langLower = lang.toLowerCase();
        if (langLower.startsWith('zh') && !timezoneLower.includes('asia')) return true;
        if (langLower.startsWith('en') && !timezoneLower.includes('america') && !timezoneLower.includes('europe') && !timezoneLower.includes('australia')) return true;
        if (langLower.startsWith('ja') && !timezoneLower.includes('asia')) return true;
        return false;
      });
      
      if (hasMismatch) {
        return true; // 语言与时区不匹配，可能是代理
      }

      return false;
    } catch (error) {
      console.error('检查代理失败:', error);
      return false;
    }
  }

  /**
   * 检查是否支持设备指纹收集
   */
  public isSupported(): boolean {
    return typeof window !== 'undefined' && 
           typeof navigator !== 'undefined' && 
           typeof screen !== 'undefined'
  }

  /**
   * 清除缓存
   */
  public clearCache(): void {
    this.cachedFingerprint = null
    this.cachedDeviceInfo = null
  }

  /**
   * 获取当前缓存的设备信息
   */
  public getCachedDeviceInfo(): TaktLoginFingerprint | null {
    return this.cachedDeviceInfo
  }
}

/**
 * 导出单例实例
 */
export const deviceNativeCollector = DeviceNativeCollector.getInstance()

/**
 * 便捷函数：获取登录指纹
 */
export const getLoginFingerprint = () => deviceNativeCollector.generateFingerprint()

/**
 * 便捷函数：获取设备信息
 */
export const getDeviceInfo = () => deviceNativeCollector.getDeviceInfo()

/**
 * 便捷函数：获取设备指纹组件
 */
export const getDeviceComponents = () => deviceNativeCollector.getDeviceInfo()
