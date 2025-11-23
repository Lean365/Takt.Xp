/**
 * IP地址获取工具
 * 提供多种获取客户端IP地址的方法
 */

/**
 * 通过WebRTC获取本地IP地址
 * 注意：这种方法只能获取内网IP，且需要用户授权
 */
export async function getLocalIpAddress(): Promise<string> {
  try {
    const pc = new RTCPeerConnection({
      iceServers: []
    })
    
    pc.createDataChannel('')
    const offer = await pc.createOffer()
    await pc.setLocalDescription(offer)
    
    const candidates = pc.localDescription?.sdp?.match(/candidate:.+/g) || []
    const ipAddresses = new Set<string>()
    
    candidates.forEach(candidate => {
      const match = candidate.match(/candidate:.+ (\d+\.\d+\.\d+\.\d+)/)
      if (match) {
        const ip = match[1]
        // 过滤掉内网IP
        if (isPrivateIp(ip)) {
          ipAddresses.add(ip)
        }
      }
    })
    
    pc.close()
    
    // 返回第一个找到的内网IP
    return Array.from(ipAddresses)[0] || ''
  } catch (error) {
    console.warn('[IP获取] WebRTC获取本地IP失败:', error)
    return ''
  }
}

/**
 * 通过第三方服务获取公网IP
 * 注意：需要网络连接，且可能被浏览器阻止
 */
export async function getPublicIpAddress(): Promise<string> {
  // 尝试多个第三方服务，按可靠性排序
  const services = [
    'https://httpbin.org/ip',
    'https://api.ipify.org?format=json',
    'https://api.myip.com',
    'https://ipapi.co/json'
  ]
  
  for (const service of services) {
    try {
      const controller = new AbortController()
      const timeoutId = setTimeout(() => controller.abort(), 3000)
      
      const response = await fetch(service, {
        method: 'GET',
        signal: controller.signal,
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json'
        }
      })
      
      clearTimeout(timeoutId)
      
      if (response.ok) {
        const data = await response.json()
        const ip = data.ip || data.query || data.origin
        if (ip && isValidIp(ip)) {
          console.log(`[IP获取] 成功从 ${service} 获取IP:`, ip)
          return ip
        }
      }
    } catch (error: any) {
      // 详细记录错误信息
      const errorMessage = error.name === 'AbortError' 
        ? '请求超时' 
        : error.message || '网络错误'
      
      console.warn(`[IP获取] 服务 ${service} 获取失败: ${errorMessage}`)
      
      // 如果是客户端阻止的错误，记录更详细的信息
      if (error.message?.includes('ERR_BLOCKED_BY_CLIENT')) {
        console.warn(`[IP获取] 服务 ${service} 被客户端（如广告拦截器）阻止`)
      } else if (error.message?.includes('ERR_CONNECTION_RESET')) {
        console.warn(`[IP获取] 服务 ${service} 连接被重置`)
      }
      
      continue
    }
  }
  
  console.warn('[IP获取] 所有公网IP获取服务都失败了')
  return ''
}

/**
 * 获取客户端IP地址（综合方法）
 * 优先尝试获取公网IP，失败则尝试获取内网IP
 */
export async function getClientIpAddress(): Promise<string> {
  try {
    // 1. 首先尝试获取公网IP
    console.log('[IP获取] 开始尝试获取公网IP...')
    const publicIp = await getPublicIpAddress()
    if (publicIp) {
      console.log('[IP获取] 成功获取到公网IP:', publicIp)
      return publicIp
    }
    
    // 2. 如果公网IP获取失败，尝试获取内网IP
    console.log('[IP获取] 公网IP获取失败，尝试获取内网IP...')
    const localIp = await getLocalIpAddress()
    if (localIp) {
      console.log('[IP获取] 成功获取到内网IP:', localIp)
      return localIp
    }
    
    // 3. 如果都失败，返回空字符串，让后端处理
    console.warn('[IP获取] 前端无法获取客户端IP，将依赖后端获取')
    return ''
  } catch (error) {
    console.error('[IP获取] 获取客户端IP过程中发生异常:', error)
    return ''
  }
}

/**
 * 判断是否为内网IP地址
 */
function isPrivateIp(ip: string): boolean {
  if (!isValidIp(ip)) return false
  
  // 处理IPv6回环地址
  if (ip === '::1') return true
  
  // 处理IPv4地址
  const parts = ip.split('.')
  if (parts.length !== 4) return false
  
  const numbers = parts.map(Number)
  
  // 10.0.0.0 - 10.255.255.255
  if (numbers[0] === 10) return true
  
  // 172.16.0.0 - 172.31.255.255
  if (numbers[0] === 172 && numbers[1] >= 16 && numbers[1] <= 31) return true
  
  // 192.168.0.0 - 192.168.255.255
  if (numbers[0] === 192 && numbers[1] === 168) return true
  
  // 127.0.0.0 - 127.255.255.255 (回环地址)
  if (numbers[0] === 127) return true
  
  return false
}

/**
 * 验证IP地址格式
 */
function isValidIp(ip: string): boolean {
  const ipv4Regex = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/
  const ipv6Regex = /^(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$|^::1$|^::$/
  return ipv4Regex.test(ip) || ipv6Regex.test(ip)
}

/**
 * 获取网络连接信息
 */
export function getNetworkInfo(): {
  type: string
  effectiveType?: string
  downlink?: number
  rtt?: number
} {
  if (!('connection' in navigator)) {
    return { type: 'unknown' }
  }
  
  const connection = (navigator as any).connection
  return {
    type: connection.type || 'unknown',
    effectiveType: connection.effectiveType,
    downlink: connection.downlink,
    rtt: connection.rtt
  }
}

/**
 * 通过后端API获取客户端IP（推荐方法）
 * 这是最可靠的方法，因为后端可以直接获取真实的客户端IP
 */
export async function getClientIpFromBackend(): Promise<string> {
  try {
    const response = await fetch('/api/system/client-ip', {
      method: 'GET',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      }
    })
    
    if (response.ok) {
      const data = await response.json()
      const ip = data.ip || data.clientIp
      if (ip && isValidIp(ip)) {
        console.log('[IP获取] 通过后端API获取到IP:', ip)
        return ip
      }
    }
    
    console.warn('[IP获取] 后端API获取IP失败')
    return ''
  } catch (error: any) {
    console.warn('[IP获取] 后端API获取IP出错:', error.message)
    return ''
  }
}

/**
 * 获取客户端IP地址（增强版）
 * 优先使用后端API，失败则使用前端方法
 */
export async function getClientIpAddressEnhanced(): Promise<string> {
  try {
    // 1. 首先尝试通过后端API获取（最可靠）
    console.log('[IP获取] 尝试通过后端API获取IP...')
    const backendIp = await getClientIpFromBackend()
    if (backendIp) {
      return backendIp
    }
    
    // 2. 如果后端API失败，使用前端方法
    console.log('[IP获取] 后端API失败，使用前端方法获取IP...')
    return await getClientIpAddress()
  } catch (error) {
    console.error('[IP获取] 增强版IP获取失败:', error)
    return ''
  }
} 