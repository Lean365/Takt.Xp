import { maskPassword, maskCustom } from './maskUtil'

/**
 * 密码加密工具
 * 使用 PBKDF2 算法，与后端保持一致
 * 使用 Web Crypto API 实现
 */
export class PasswordEncryptor {
  private static readonly SALT_SIZE = 32        // 盐值长度(字节) - 与后端一致
  private static readonly HASH_SIZE = 32        // 哈希长度(字节)
  private static readonly ITERATIONS = 600000   // 迭代次数 - 与后端一致

  /**
   * 生成随机盐值
   * @returns 32字节的随机盐值(Base64编码)
   */
  static async generateSalt(): Promise<string> {
    const salt = crypto.getRandomValues(new Uint8Array(this.SALT_SIZE))
    return btoa(String.fromCharCode(...salt))
  }

  /**
   * 使用 PBKDF2 加密密码
   * 与后端 TaktPasswordUtils 保持一致
   */
  static async hashPassword(password: string, salt: string): Promise<string> {
    try {
      console.log('[PBKDF2] 开始加密')
      console.log('[PBKDF2] 输入密码:', maskPassword(password))
      console.log('[PBKDF2] 输入盐值:', maskCustom(salt, 8, 8))
      console.log('[PBKDF2] 盐值长度:', salt.length)

      // 1. 将密码转换为 ArrayBuffer
      const passwordBuffer = new TextEncoder().encode(password)
      
      // 2. 解析Base64盐值
      const saltBuffer = Uint8Array.from(atob(salt), c => c.charCodeAt(0))
      
      // 3. 使用 PBKDF2 加密
      const key = await crypto.subtle.importKey(
        'raw',
        passwordBuffer,
        'PBKDF2',
        false,
        ['deriveBits']
      )
      
      const hash = await crypto.subtle.deriveBits(
        {
          name: 'PBKDF2',
          salt: saltBuffer,
          iterations: this.ITERATIONS,
          hash: 'SHA-256'
        },
        key,
        8 * this.HASH_SIZE * 8  // 与后端一致：8 * 32 * 8 = 2048 bits
      )
      
      // 4. 转换为Base64（只取前32字节，与后端一致）
      const hashArray = new Uint8Array(hash)
      const first32Bytes = hashArray.slice(0, this.HASH_SIZE) // 只取前32字节
      const result = btoa(String.fromCharCode(...first32Bytes))
      console.log('[PBKDF2] 加密结果:', maskCustom(result, 8, 8))
      console.log('[PBKDF2] 加密结果长度:', result.length)
      console.log('[PBKDF2] 原始哈希长度:', hashArray.length, '字节，截取后:', first32Bytes.length, '字节')
      
      return result
    } catch (error) {
      // console.error('[PBKDF2] 加密失败:', error)
      throw error
    }
  }

  /**
   * 验证密码
   */
  static async verifyPassword(password: string, hashedPassword: string, salt: string): Promise<boolean> {
    try {
      const computedHash = await this.hashPassword(password, salt)
      return computedHash === hashedPassword
    } catch (error) {
      // console.error('[密码验证] 失败:', error)
      return false
    }
  }

  /**
   * 测试 PBKDF2 加密
   */
  static async testEncryption() {
    // console.group('=== PBKDF2 加密测试 ===')
    
    const password = '123456'
    const salt = await this.generateSalt()
    const hash = await this.hashPassword(password, salt)
    
    // console.log('测试结果:', {
    //   输入密码: maskPassword(password),
    //   生成盐值: maskCustom(salt, 8, 8),
    //   加密结果: maskCustom(hash, 8, 8)
    // })
    
    // 验证密码
    const isValid = await this.verifyPassword(password, hash, salt)
    // console.log('验证结果:', isValid)
    
    // console.groupEnd()
  }
}

// 添加全局测试方法
declare global {
  interface Window {
    testPasswordEncryption: () => Promise<void>;
  }
}

// 暴露测试方法到全局
window.testPasswordEncryption = async () => {
  // console.group('=== PBKDF2 密码加密测试 ===')
  
  try {
    // 测试1：完整加密流程
    // console.group('测试1：完整加密流程')
    const password = '123456'
    const salt = await PasswordEncryptor.generateSalt()
    const hash = await PasswordEncryptor.hashPassword(password, salt)
    
    // console.log('加密结果：', {
    //   输入密码: maskPassword(password),
    //   生成盐值: maskCustom(salt, 8, 8),
    //   加密结果: maskCustom(hash, 8, 8)
    // })
    // console.groupEnd()

    // 测试2：密码验证
    // console.group('测试2：密码验证')
    const isValid = await PasswordEncryptor.verifyPassword(password, hash, salt)
    const isInvalid = await PasswordEncryptor.verifyPassword('wrongpassword', hash, salt)
    
    // console.log('验证结果：', {
    //   正确密码验证: isValid,
    //   错误密码验证: isInvalid
    // })
    // console.groupEnd()

    // 测试3：性能测试
    // console.group('测试3：性能测试')
    const startTime = performance.now()
    await PasswordEncryptor.hashPassword('testpassword', salt)
    const endTime = performance.now()
    
    // console.log('性能结果：', {
    //   加密耗时: `${(endTime - startTime).toFixed(2)}ms`,
    //   安全级别: 'PBKDF2-SHA256'
    // })
    // console.groupEnd()

  } catch (error) {
    // console.error('测试失败:', error)
  }
  
  // console.groupEnd()
}
