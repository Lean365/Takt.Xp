import { HubConnection, LogLevel, RetryContext, HubConnectionBuilder, HttpTransportType, IHttpConnectionOptions } from '@microsoft/signalr'
import { getToken } from '@/utils/authUtil'
import { getDeviceInfo } from '@/utils/loginFingerprintNativeUtil'
import { message } from 'ant-design-vue'
import { maskConnectionId } from '@/utils/maskUtil'

// SignalR 全局配置
export const signalRConfig = {
  logLevel: LogLevel.Information,
  reconnectInterval: 5000,
  maxRetries: 5,
  heartbeatInterval: 30000,  // 与后端 KeepAliveInterval 和 HeartbeatIntervalSeconds 匹配
  heartbeatTimeout: 60000,   // 与后端 ClientTimeoutInterval 匹配
  baseUrl: '/signalr/Takthub',
  transport: HttpTransportType.LongPolling, // 首先使用 LongPolling
  debug: import.meta.env.DEV, // 仅在开发环境启用调试
  skipNegotiation: false,    // 允许协商过程
  serverTimeoutInMilliseconds: 30000, // 服务器超时时间，与心跳间隔保持一致
  keepAliveIntervalInMilliseconds: 30000, // 保持连接活跃的间隔，与后端保持一致
  maximumReceiveMessageSize: 10485760, // 10MB，与后端 MaximumReceiveMessageSize 匹配
  streamBufferCapacity: 10,  // 与后端 StreamBufferCapacity 匹配
  handshakeTimeout: 15000,   // 与后端 HandshakeTimeout 匹配
  closeTimeout: 5000,        // 与后端 WebSockets.CloseTimeout 匹配
  enableDetailedErrors: import.meta.env.DEV // 仅在开发环境启用详细错误
}

// 重试策略配置
export const retryPolicy = {
  maxRetries: 5,
  maxDelay: 30000, // 30秒
  baseDelay: 1000, // 1秒
  nextRetryDelayInMilliseconds: (retryContext: RetryContext) => {
    if (retryContext.previousRetryCount >= retryPolicy.maxRetries) {
      return null // 停止重试
    }
    
    // 指数退避算法
    const delay = Math.min(
      retryPolicy.baseDelay * Math.pow(2, retryContext.previousRetryCount),
      retryPolicy.maxDelay
    )
    
    console.log(`[SignalR] 第 ${retryContext.previousRetryCount + 1} 次重试,延迟 ${delay}ms`)
    return delay
  }
}

// 自定义日志记录器
const customLogger = {
  log(logLevel: LogLevel, message: string) {
    const now = new Date()
    const localTime = now.toLocaleString('zh-CN', {
      year: 'numeric',
      month: '2-digit',
      day: '2-digit',
      hour: '2-digit',
      minute: '2-digit',
      second: '2-digit',
      hour12: false,
      timeZone: 'Asia/Shanghai'
    })
    
    // 如果消息中包含UTC时间戳，则不输出
    if (message.match(/^\[\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{3}Z\]/)) {
      return
    }
    
    console.log(`[${localTime}] ${LogLevel[logLevel]}: ${message}`)
  }
}

// 获取连接头信息
export const getConnectionHeaders = async (): Promise<Record<string, string>> => {
  const deviceInfo = await getDeviceInfo()
  console.log('[SignalR] 获取设备信息:', deviceInfo)

  // 设备ID现在总是存在，因为我们在getDeviceInfo中确保设置了该字段

  const headers: Record<string, string> = {
    'X-Device-Id': deviceInfo.deviceId || '',
    'X-Device-Type': deviceInfo.deviceType || '',
    'X-Device-Fingerprint': deviceInfo.loginFingerprint || '',
    'X-OS': deviceInfo.os || '',
    'X-Browser': deviceInfo.browser || '',
    'X-Resolution': deviceInfo.screenResolution || '',
    'X-Language': deviceInfo.language || navigator.language || '',
    'X-Timezone': deviceInfo.timezone || Intl.DateTimeFormat().resolvedOptions().timeZone || '',
    'X-User-Agent': navigator.userAgent,
    'X-Platform': deviceInfo.platform || ''
  }

  console.log('[SignalR] 生成的连接头信息:', headers)
  return headers
}

export const createHubConnection = async (): Promise<HubConnection> => {
    try {
        console.log('[SignalR] 开始创建连接...');
        
        console.log('[SignalR] 获取设备信息...');
        const deviceInfo = await getDeviceInfo();
        // 设备ID现在总是存在，因为我们在getDeviceInfo中确保设置了该字段
        console.log('[SignalR] 设备信息:', deviceInfo);

        console.log('[SignalR] 获取访问令牌...');
        const token = await getToken();
        if (!token) {
            throw new Error('未找到访问令牌');
        }
        console.log('[SignalR] 访问令牌已获取');

        const url = signalRConfig.baseUrl;
        console.log('[SignalR] 连接URL:', url);
        
        // 注意：后端连接测试已移除，因为 SignalR 连接已经成功
        // 如果 SignalR 协商成功，说明后端连接正常

        // 测试 SignalR 协商端点
        try {
          const negotiateUrl = url + '/negotiate';
          console.log('[SignalR] 测试协商端点:', negotiateUrl);
          const negotiateResponse = await fetch(negotiateUrl, {
            method: 'POST',
            headers: {
              'Authorization': `Bearer ${token}`,
              'Content-Type': 'application/json'
            }
          });
          console.log('[SignalR] 协商端点测试结果:', negotiateResponse.status, negotiateResponse.statusText);
          if (negotiateResponse.ok) {
            const negotiateData = await negotiateResponse.text();
            console.log('[SignalR] 协商响应数据:', negotiateData.substring(0, 200) + '...');
          }
        } catch (error) {
          console.error('[SignalR] 协商端点测试失败:', error);
        }

        // 使用 accessTokenFactory 而不是 URL 参数
        const finalUrl = url;
        console.log('[SignalR] 最终连接URL:', finalUrl);

        console.log('[SignalR] 开始创建连接，配置信息:');
        console.log('  - URL:', finalUrl);
        console.log('  - Transport:', signalRConfig.transport);
        console.log('  - withCredentials:', false);
        console.log('  - skipNegotiation:', signalRConfig.skipNegotiation);
        console.log('  - accessTokenFactory: 已设置');
        console.log('  - Headers:', { 'Authorization': `Bearer ${token.substring(0, 20)}...` });

        const connection = new HubConnectionBuilder()
            .withUrl(finalUrl, {
                transport: signalRConfig.transport,
                withCredentials: false,
                skipNegotiation: signalRConfig.skipNegotiation,
                accessTokenFactory: () => token,
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            })
            .withAutomaticReconnect({
                nextRetryDelayInMilliseconds: (retryContext) => {
                    if (retryContext.previousRetryCount >= retryPolicy.maxRetries) {
                        return null;
                    }
                    const delay = Math.min(
                        retryPolicy.baseDelay * Math.pow(2, retryContext.previousRetryCount),
                        retryPolicy.maxDelay
                    );
                    console.log(`[SignalR] 第 ${retryContext.previousRetryCount + 1} 次重试,延迟 ${delay}ms`);
                    return delay;
                }
            })
            .configureLogging(customLogger)
            .withServerTimeout(signalRConfig.serverTimeoutInMilliseconds)
            .withKeepAliveInterval(signalRConfig.keepAliveIntervalInMilliseconds)
            .build();

        // 设置连接事件处理
        connection.onclose((error) => {
            console.error('[SignalR] 连接已关闭:', error);
            message.error('连接已断开，正在尝试重新连接...');
        });

        connection.onreconnecting((error) => {
            console.warn('[SignalR] 正在重新连接:', error);
            message.warning('正在重新连接...');
        });

        connection.onreconnected((connectionId) => {
            console.log('[SignalR] 重新连接成功:', connectionId ? maskConnectionId(connectionId) : '未知');
            message.success('重新连接成功');
        });

        console.log('[SignalR] 连接对象创建成功');
        return connection;
    } catch (error) {
        console.error('[SignalR] 创建连接失败:', error);
        console.error('[SignalR] 错误详情:', {
            name: (error as Error).name,
            message: (error as Error).message,
            stack: (error as Error).stack
        });
        throw error;
    }
};

// 启动连接
export const startConnection = async (connection: HubConnection): Promise<void> => {
  try {
    await connection.start()
    console.log('SignalR连接已建立')
    message.success('连接成功')
  } catch (error) {
    console.error('启动SignalR连接失败:', error)
    throw error
  }
}

// 停止连接
export const stopConnection = async (connection: HubConnection): Promise<void> => {
  try {
    await connection.stop()
    console.log('SignalR连接已停止')
  } catch (error) {
    console.error('停止SignalR连接失败:', error)
    throw error
  }
}

// 心跳检测
export const startHeartbeat = (connection: HubConnection) => {
  let heartbeatInterval: NodeJS.Timeout | null = null

  const start = () => {
    if (heartbeatInterval) {
      clearInterval(heartbeatInterval)
    }

    heartbeatInterval = setInterval(async () => {
      try {
        if (connection.state === 'Connected') {
          await connection.invoke('SendHeartbeat')
          console.log('[SignalR] 心跳检测成功')
        } else {
          console.log('[SignalR] 跳过心跳检测，当前连接状态:', connection.state)
        }
      } catch (error) {
        console.error('[SignalR] 心跳检测失败:', error)
      }
    }, signalRConfig.heartbeatInterval)
  }

  const stop = () => {
    if (heartbeatInterval) {
      clearInterval(heartbeatInterval)
      heartbeatInterval = null
    }
  }

  return { start, stop }
}

// 错误处理
export const handleConnectionError = (error: Error) => {
  console.error('[SignalR] 连接错误:', error)
  if (error.message.includes('401')) {
    // 认证错误处理
    return 'AUTH_ERROR'
  }
  return 'CONNECTION_ERROR'
} 
