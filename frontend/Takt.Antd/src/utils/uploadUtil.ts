/**
 * 文件上传工具类
 * 支持大文件分片上传、断点续传、并发控制等功能
 */

import SparkMD5 from 'spark-md5'
import CryptoJS from 'crypto-js'

/**
 * 文件分块信息接口
 */
export interface ChunkInfo {
  chunk: Blob       // 分块数据
  filename: string  // 文件名
  hash: string      // 分块哈希值
  index: number     // 分块索引
}

/**
 * 上传配置接口
 */
export interface UploadConfig {
  chunkSize?: number  // 分块大小，默认2MB
  threads?: number    // 并发上传数，默认3
  retryCount?: number // 重试次数，默认3
  retryDelay?: number // 重试延迟时间，默认1000ms
}

/**
 * 文件上传器类
 * 使用单例模式确保全局只有一个实例
 */
export class FileUploader {
  private static instance: FileUploader

  /**
   * 默认上传配置
   */
  private readonly defaultConfig: Required<UploadConfig> = {
    chunkSize: 2 * 1024 * 1024,  // 默认分块大小2MB
    threads: 3,                   // 默认并发数3
    retryCount: 3,               // 默认重试3次
    retryDelay: 1000             // 默认重试延迟1秒
  }

  /**
   * 私有构造函数，防止外部直接实例化
   * @param config 上传配置
   */
  private constructor(private config: Required<UploadConfig>) {}

  /**
   * 获取单例实例
   * @param config 可选的上传配置，会与默认配置合并
   * @returns FileUploader实例
   */
  public static getInstance(config?: UploadConfig): FileUploader {
    if (!FileUploader.instance) {
      FileUploader.instance = new FileUploader({
        ...new FileUploader(null as any).defaultConfig,
        ...config
      })
    }
    return FileUploader.instance
  }

  /**
   * 计算文件的MD5值
   * @param file 要计算的文件对象
   * @param progressCallback 进度回调函数，返回0-100的进度值
   * @param errorCallback 错误回调函数
   * @returns 返回文件的MD5哈希值
   */
  public async calculateFileMD5(
    file: File,
    progressCallback?: (progress: number) => void,
    errorCallback?: (error: any) => void
  ): Promise<string> {
    return new Promise((resolve) => {
      const reader = new FileReader()
      
      // 文件加载完成后计算MD5
      reader.onload = (e) => {
        const buffer = e.target?.result as ArrayBuffer
        const wordArray = CryptoJS.lib.WordArray.create(buffer as any)
        const md5 = CryptoJS.MD5(wordArray).toString()
        resolve(md5)
      }

      // 处理读取错误
      reader.onerror = (error) => {
        errorCallback?.(error)
        resolve('')
      }

      // 更新读取进度
      reader.onprogress = (event) => {
        if (event.lengthComputable) {
          const progress = (event.loaded / event.total) * 100
          progressCallback?.(progress)
        }
      }

      // 开始读取文件
      reader.readAsArrayBuffer(file)
    })
  }

  /**
   * 将文件分割成块
   * @param file 要分割的文件对象
   * @returns 返回分块信息数组
   */
  public createFileChunks(file: File): ChunkInfo[] {
    const chunks: ChunkInfo[] = []
    let cur = 0
    
    // 根据配置的块大小分割文件
    while (cur < file.size) {
      chunks.push({
        chunk: file.slice(cur, cur + this.config.chunkSize),
        filename: file.name,
        hash: '', // 哈希值需要上传者设置
        index: Math.floor(cur / this.config.chunkSize)
      })
      cur += this.config.chunkSize
    }
    return chunks
  }

  /**
   * 上传文件分块
   * @param chunks 分块信息数组
   * @param uploadUrl 上传接口地址
   * @param headers 请求头信息
   * @param onProgress 上传进度回调函数
   * @param uploadedChunks 已上传的分块集合，用于断点续传
   */
  public async uploadChunks(
    chunks: ChunkInfo[],
    uploadUrl: string,
    headers: Record<string, string>,
    onProgress?: (progress: number) => void,
    uploadedChunks: Set<string> = new Set()
  ): Promise<void> {
    // 过滤出未上传的分块
    const pendingChunks = chunks.filter(chunk => !uploadedChunks.has(chunk.hash))
    const total = pendingChunks.length
    let completed = 0

    // 将分块分组，控制并发数
    const groups = this.groupChunks(pendingChunks, this.config.threads)
    
    // 按组上传分块
    for (const group of groups) {
      await Promise.all(
        group.map(async chunk => {
          let retries = 0
          // 失败重试机制
          while (retries < this.config.retryCount) {
            try {
              const formData = new FormData()
              formData.append('chunk', chunk.chunk)
              formData.append('hash', chunk.hash)
              formData.append('filename', chunk.filename)
              formData.append('index', chunk.index.toString())

              const response = await fetch(uploadUrl, {
                method: 'POST',
                headers,
                body: formData
              })

              if (!response.ok) {
                throw new Error(`上传失败: ${response.statusText}`)
              }

              // 更新上传进度
              uploadedChunks.add(chunk.hash)
              completed++
              onProgress?.(Math.floor((completed / total) * 100))
              break
            } catch (error) {
              retries++
              if (retries === this.config.retryCount) {
                throw error
              }
              // 延迟后重试
              await this.delay(this.config.retryDelay)
            }
          }
        })
      )
    }
  }

  /**
   * 请求合并分块
   * @param uploadUrl 上传接口地址
   * @param headers 请求头信息
   * @param params 合并参数，包含文件名、大小和分块数量
   * @returns 服务器响应
   */
  public async mergeChunks(
    uploadUrl: string,
    headers: Record<string, string>,
    params: {
      filename: string  // 文件名
      size: number     // 文件大小
      chunks: number   // 分块数量
    }
  ): Promise<Response> {
    return fetch(`${uploadUrl}/merge`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        ...headers
      },
      body: JSON.stringify(params)
    })
  }

  /**
   * 将数组分组
   * @param chunks 要分组的数组
   * @param size 每组的大小
   * @returns 分组后的二维数组
   */
  private groupChunks<T>(chunks: T[], size: number): T[][] {
    const groups: T[][] = []
    for (let i = 0; i < chunks.length; i += size) {
      groups.push(chunks.slice(i, i + size))
    }
    return groups
  }

  /**
   * 延迟执行
   * @param ms 延迟的毫秒数
   * @returns Promise
   */
  private delay(ms: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, ms))
  }
}

// 导出单例实例
export default FileUploader.getInstance() 