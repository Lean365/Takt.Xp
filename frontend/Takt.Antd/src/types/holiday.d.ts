export interface Holiday {
  id: string
  name: string
  icon: string
  theme: {
    [key: string]: string | undefined  // 支持任意颜色变量
  }
} 