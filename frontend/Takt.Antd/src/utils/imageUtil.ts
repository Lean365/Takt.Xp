/**
 * 图片压缩选项接口
 */
export interface ImageCompressOptions {
  quality?: number // 压缩质量，取值0-1，默认0.8
  maxWidth?: number // 最大宽度限制，超过会等比缩放，默认1920
  maxHeight?: number // 最大高度限制，超过会等比缩放，默认1080
  type?: string // 输出图片格式，默认保持原格式
}

/**
 * 图片尺寸接口
 */
export interface ImageDimensions {
  width: number // 图片宽度
  height: number // 图片高度
}

/**
 * 图片处理工具类
 * 使用单例模式确保全局只有一个实例
 */
export class ImageProcessor {
  private static instance: ImageProcessor
  
  /**
   * 默认压缩选项
   */
  private readonly defaultOptions: Required<ImageCompressOptions> = {
    quality: 0.8, // 默认压缩质量
    maxWidth: 1920, // 默认最大宽度
    maxHeight: 1080, // 默认最大高度
    type: 'image/jpeg' // 默认输出格式
  }

  /**
   * 私有构造函数，防止外部直接实例化
   */
  private constructor() {}

  /**
   * 获取单例实例
   * @returns ImageProcessor实例
   */
  public static getInstance(): ImageProcessor {
    if (!ImageProcessor.instance) {
      ImageProcessor.instance = new ImageProcessor()
    }
    return ImageProcessor.instance
  }

  /**
   * 压缩图片
   * @param file 要压缩的图片文件
   * @param options 压缩选项，可选
   * @returns 返回压缩后的图片文件
   */
  public async compress(file: File, options?: ImageCompressOptions): Promise<File> {
    // 合并默认选项和用户选项
    const finalOptions = {
      ...this.defaultOptions,
      type: file.type || this.defaultOptions.type,
      ...options
    }

    return new Promise((resolve, reject) => {
      // 创建文件读取器
      const reader = new FileReader()
      reader.readAsDataURL(file)
      
      reader.onload = (e) => {
        // 创建图片对象
        const img = new Image()
        img.src = e.target?.result as string
        
        img.onload = () => {
          // 创建画布
          const canvas = document.createElement('canvas')
          let { width, height } = img
          
          // 计算缩放比例，保持宽高比
          if (width > finalOptions.maxWidth || height > finalOptions.maxHeight) {
            const ratio = Math.min(
              finalOptions.maxWidth / width,
              finalOptions.maxHeight / height
            )
            width *= ratio
            height *= ratio
          }

          // 设置画布尺寸
          canvas.width = width
          canvas.height = height
          const ctx = canvas.getContext('2d')
          
          // 将图片绘制到画布上
          ctx?.drawImage(img, 0, 0, width, height)

          // 将画布内容转换为Blob对象
          canvas.toBlob(
            (blob) => {
              if (blob) {
                // 创建新的文件对象
                const newFile = new File([blob], file.name, {
                  type: finalOptions.type,
                  lastModified: Date.now()
                })
                resolve(newFile)
              } else {
                reject(new Error('图片压缩失败'))
              }
            },
            finalOptions.type,
            finalOptions.quality
          )
        }
        
        img.onerror = () => reject(new Error('图片加载失败'))
      }
      
      reader.onerror = () => reject(new Error('图片读取失败'))
    })
  }

  /**
   * 获取图片的Base64编码
   * @param file 图片文件或Blob对象
   * @returns 返回图片的Base64字符串
   */
  public getBase64(file: File | Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => resolve(reader.result as string)
      reader.onerror = (error) => reject(error)
    })
  }

  /**
   * 获取图片的尺寸信息
   * @param file 图片文件
   * @returns 返回包含宽高的对象
   */
  public getImageDimensions(file: File): Promise<ImageDimensions> {
    return new Promise((resolve, reject) => {
      const img = new Image()
      img.onload = () => {
        resolve({
          width: img.width,
          height: img.height
        })
      }
      img.onerror = () => {
        reject(new Error('图片加载失败'))
      }
      // 使用 URL.createObjectURL 创建临时URL
      img.src = URL.createObjectURL(file)
    })
  }
}

// 导出单例实例
export default ImageProcessor.getInstance()