import 'axios'

declare module 'axios' {
  interface AxiosRequestConfig {
    /**
     * 跳过 Token 校验
     * 设置为 true 时，请求拦截器不会添加 Authorization 头
     */
    skipAuth?: boolean;
  }
}
