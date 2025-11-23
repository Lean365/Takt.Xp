// 滑块验证码响应
export interface SliderCaptchaResponse {
  backgroundImage: string;
  sliderImage: string;
  token: string;
}

// 滑块验证请求参数
export interface SliderValidateParams {
  token: string;
  xOffset: number;
}

// 验证码结果
export interface CaptchaResult {
  success: boolean;
  message?: string;
  token?: string;
}

// 行为数据点
export interface BehaviorPoint {
  x: number;
  y: number;
  timestamp: number;
}

// 行为数据收集参数
export interface BehaviorDataParams {
  userId: string;
  mouseTrack: BehaviorPoint[];
  keyIntervals: number[];
  duration: number;
}

// 行为验证参数
export interface BehaviorValidateParams {
  token: string;
} 