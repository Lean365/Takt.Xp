<template>
  <div class="slider-captcha">
    <div class="captcha-image" ref="imageRef">
      <img v-if="captchaData" :src="captchaData.backgroundImage" :alt="t('identity.auth.captcha.slider.bgImage')" class="bg-image" @click="refresh" />
      <img v-if="captchaData" :src="captchaData.sliderImage" :alt="t('identity.auth.captcha.slider.sliderImage')" class="slider-image"
        :style="{ left: `${sliderLeft}px` }" />
      <!-- 倒计时显示在右上角 -->
      <div v-if="!verified && expireTime > 0" class="countdown-overlay">
        <a-statistic-countdown
          :value="expireTime"
          :value-style="{ color: isExpired ? '#ff7875' : '#ff4d4f', fontSize: '12px', fontWeight: '600' }"
          format="ss"
          @finish="refresh"
        />
      </div>
    </div>
    
    <!-- 自定义滑块容器 -->
    <div class="slider-track" ref="trackRef">
      <!-- 背景轨道 -->
      <div class="slider-bg"></div>
      
      <!-- 滑块 -->
      <div
        class="slider-thumb"
        :class="{ 
          'dragging': dragging, 
          'success': verified,
          'hover': !dragging && !verified && thumbLeft === 0
        }"
        :style="{ left: thumbLeft + 'px' }"
        @mousedown="onDragStart"
        @touchstart="onDragStart"
        @click="() => console.log('[滑块验证码] 滑块被点击')"
      >
        <div class="thumb-icon">
          <span v-if="!verified" class="arrow-icon">>></span>
          <span v-else class="success-icon">✓</span>
        </div>
      </div>
      
      <!-- 进度条 -->
      <div 
        class="progress-bar"
        :style="{ width: `${thumbLeft + 40}px` }"
      ></div>
      
      <!-- 主提示文字，遮罩宽度最大为hintWidth -->
      <div 
        class="slider-hint"
        ref="hintRef"
        :class="{ 'active': thumbLeft > 0 && !verified }"
        :style="{ 
          left: '50%',
          transform: 'translateX(-50%)',
          opacity: verified ? 0 : 1,
          clipPath: thumbLeft > 0 && thumbLeft <= hintWidth ? `inset(0 0 0 ${Math.max(0, thumbLeft - 200)}px)` : 'inset(0 0 0 0)'
        }"
      >
        <span>{{ t('identity.auth.captcha.slider.default') }}</span>
      </div>
      
      <!-- 左边补全文字 - 显示被遮盖的部分 -->
      <div 
        class="slider-hint-left"
        :class="{ 
          'active': thumbLeft > 0 && !verified
        }"
        :style="{ 
          left: `${thumbLeft + 50}px`,
          opacity: verified ? 0 : (thumbLeft > 200 ? 1 : 0),
          clipPath: thumbLeft > 200 ? `inset(0 0 0 0)` : 'inset(0 0 0 100%)'
        }"
      >
        
      </div>
      
      <!-- 成功文字 -->
      <div 
        class="success-text"
        :class="{ 
          'show': verified,
          'left': verified && thumbLeft >= maxLeft * 0.5,
          'right': verified && thumbLeft < maxLeft * 0.5
        }"

      >
        {{ t('identity.auth.captcha.slider.success') }}
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed, watch, onMounted, onUnmounted, nextTick } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { getCaptcha, verifyCaptcha } from '@/api/security/captcha'
import type { SliderCaptchaDto, SliderValidateDto, CaptchaResultDto } from '@/api/security/captcha'

const { t } = useI18n()
const emit = defineEmits(['success', 'error'])

const imageRef = ref<HTMLElement>()
const trackRef = ref<HTMLElement | null>(null)
const captchaData = ref<SliderCaptchaDto | null>(null)
const thumbLeft = ref(0)
const maxLeft = ref(0)
const dragging = ref(false)
const verified = ref(false)
const retryCount = ref(0)
const MAX_RETRY = 3
const CAPTCHA_EXPIRE_TIME = 30000 // 30秒有效期
const expireTimer = ref<NodeJS.Timeout | null>(null)
const expireTime = ref<number>(0)

// 新增：主提示宽度
const hintRef = ref<HTMLElement | null>(null)
const hintWidth = ref(0)

onMounted(() => {
  if (hintRef.value) {
    hintWidth.value = hintRef.value.offsetWidth
  }
})
watch(() => thumbLeft.value, () => {
  if (hintRef.value) {
    hintWidth.value = hintRef.value.offsetWidth
  }
})

// 计算滑块在图片中的位置
const sliderLeft = computed(() => {
  if (!imageRef.value) return 0
  const imageWidth = 400 // 后端实际图片宽度
  const sliderWidth = 48 // 滑块宽度
  return Math.round((thumbLeft.value / maxLeft.value) * (imageWidth - sliderWidth))
})

const isExpired = computed(() => {
  return expireTime.value > 0 && Date.now() > expireTime.value
})



// 清理过期定时器
const clearExpireTimer = () => {
  if (expireTimer.value) {
    clearTimeout(expireTimer.value)
    expireTimer.value = null
  }
}

// 设置过期时间
const setExpireTime = () => {
  clearExpireTimer()
  expireTime.value = Date.now() + CAPTCHA_EXPIRE_TIME
}

// 拖拽开始
const onDragStart = (e: MouseEvent | TouchEvent) => {
  console.log('[滑块验证码] 拖拽事件触发:', {
    eventType: e.type,
    verified: verified.value,
    target: e.target
  })
  
  if (verified.value) {
    console.log('[滑块验证码] 验证已完成，忽略拖拽')
    return
  }
  
  e.preventDefault()
  dragging.value = true
  
  const clientX = 'touches' in e ? e.touches[0].clientX : e.clientX
  const startX = clientX - thumbLeft.value
  
  console.log('[滑块验证码] 开始拖拽:', {
    clientX: clientX,
    startX: startX,
    currentThumbLeft: thumbLeft.value
  })
  
  const move = (ev: MouseEvent | TouchEvent) => {
    if (!dragging.value) return
    
    ev.preventDefault()
    const moveX = 'touches' in ev ? ev.touches[0].clientX : ev.clientX
    let left = moveX - startX
    left = Math.max(0, Math.min(left, maxLeft.value))
    
    // 使用 requestAnimationFrame 确保动画流畅
    requestAnimationFrame(() => {
      thumbLeft.value = left
      // 输出拖拽进度日志
      console.log('[滑块验证码] 拖拽进度:', {
        currentLeft: left,
        maxLeft: maxLeft.value,
        progress: Math.round((left / maxLeft.value) * 100) + '%'
      })
    })
  }
  
  const up = async () => {
    if (!dragging.value) return
    
    try {
      dragging.value = false
      document.removeEventListener('mousemove', move)
      document.removeEventListener('mouseup', up)
      document.removeEventListener('touchmove', move)
      document.removeEventListener('touchend', up)
      
      console.log('[滑块验证码] 滑块释放，检查位置:', {
        thumbLeft: thumbLeft.value,
        maxLeft: maxLeft.value,
        threshold: maxLeft.value - 2,
        shouldVerify: thumbLeft.value >= maxLeft.value - 2
      })
      
      // 滑块验证码：只要释放就进行验证，让后端判断位置是否正确
      console.log('[滑块验证码] 滑块释放，开始验证')
      await verifySliderCaptcha()
    } catch (error) {
      console.error('[滑块验证码] 处理滑块释放时出错:', error)
      await resetSlider()
      message.error(t('identity.auth.captcha.slider.verifyError'))
      emit('error', error)
    }
  }
  
  document.addEventListener('mousemove', move)
  document.addEventListener('mouseup', up)
  document.addEventListener('touchmove', move)
  document.addEventListener('touchend', up)
}

// 验证滑块验证码
const verifySliderCaptcha = async () => {
  try {
    if (!captchaData.value) {
      console.warn('[验证码] 无验证码数据')
      return
    }

    if (verified.value) {
      return
    }

    // 计算滑块在图片中的位置（基于400x100图片尺寸，直接使用后端坐标系）
    const imageWidth = 400 // 后端实际图片宽度
    const sliderWidth = 48 // 滑块宽度
    
    // 直接计算滑块在400x100图片中的位置（后端坐标系）
    const sliderPositionInImage = Math.round((thumbLeft.value / maxLeft.value) * (imageWidth - sliderWidth))
    
    const params: SliderValidateDto = {
      token: captchaData.value.token,
      xOffset: sliderPositionInImage // 滑块在图片内的位置
    }
    
    console.log('[验证码] 发送验证参数:', {
      token: params.token,
      sliderPositionInImage: params.xOffset, // 滑块在图片内的位置
      thumbLeft: thumbLeft.value, // 滑块组件值
      maxLeft: maxLeft.value, // 滑块最大值
      displayPosition: sliderLeft.value, // 显示位置（图片居中）
      containerSize: '400x100',
      imageSize: '400x100 (后端实际尺寸，居中显示)',
      sliderWidth: 48,
      maxOffset: 400 - 48,
      sliderRange: `0-${400 - 48}`, // 滑块在图片内的可移动范围
      coordinateSystem: '滑块位置基于400x100图片内部坐标（后端坐标系）'
    })
    
    const { data } = await verifyCaptcha(params)
    console.log('[验证码] 验证响应:', data)
    
    if (data.success) {
      verified.value = true
      retryCount.value = 0
      clearExpireTimer() // 验证成功时清理定时器
      
      emit('success', {
        token: captchaData.value.token,
        xOffset: sliderPositionInImage
      })
    } else {
      verified.value = false
      retryCount.value++
      await resetSlider()
      
      message.error(data.message || t('identity.auth.captcha.slider.failed'))
      if (retryCount.value >= MAX_RETRY) {
        message.error(t('identity.auth.captcha.slider.maxRetryReached'))
        emit('error', 'MAX_RETRY_REACHED')
      }
    }
  } catch (error) {
    console.error('[验证码] 验证请求出错:', error)
    verified.value = false
    retryCount.value++
    await resetSlider()
    
    message.error(t('identity.auth.captcha.slider.verifyError'))
    if (retryCount.value >= MAX_RETRY) {
      message.error(t('identity.auth.captcha.slider.maxRetryReached'))
      emit('error', 'MAX_RETRY_REACHED')
    }
  }
}

// 重置滑块
const resetSlider = async () => {
  // 添加重置动画
  const startLeft = thumbLeft.value
  const startTime = Date.now()
  const duration = 400 // 增加动画时长，让动画更丝滑
  
  const animate = () => {
    const elapsed = Date.now() - startTime
    const progress = Math.min(elapsed / duration, 1)
    
    // 使用更平滑的缓动函数
    const easeOutCubic = 1 - Math.pow(1 - progress, 3)
    const easeOutQuart = 1 - Math.pow(1 - progress, 4)
    const easeOut = (easeOutCubic + easeOutQuart) / 2 // 混合缓动函数
    
    thumbLeft.value = startLeft * (1 - easeOut)
    
    if (progress < 1) {
      requestAnimationFrame(animate)
    }
  }
  
  animate()
}

// 初始化验证码
const initCaptcha = async () => {
  console.log('[验证码] 开始获取验证码')
  try {
    const { data } = await getCaptcha()
    console.log('[验证码] 原始响应:', data)

    const { backgroundImage, sliderImage, token } = data
    if (!backgroundImage || !sliderImage || !token) {
      console.error('[验证码] 验证码数据不完整:', {
        hasBackgroundImage: !!backgroundImage,
        hasSliderImage: !!sliderImage,
        hasToken: !!token
      })
      emit('error', t('identity.auth.captcha.slider.error.invalidResponse'))
      return
    }

    // 设置验证码数据
    captchaData.value = {
      backgroundImage,
      sliderImage,
      token
    }

    // 重置状态
    thumbLeft.value = 0
    verified.value = false
    retryCount.value = 0

    // 设置过期时间
    setExpireTime()

    console.log('[验证码] 验证码数据已设置:', {
      hasBackgroundImage: !!captchaData.value.backgroundImage,
      hasSliderImage: !!captchaData.value.sliderImage,
      tokenLength: captchaData.value.token.length,
      expireTime: new Date(expireTime.value).toLocaleString()
    })
  } catch (error) {
    console.error('[验证码] 获取验证码失败:', error)
    emit('error', t('identity.auth.captcha.slider.error.loadFailed'))
  }
}

// 刷新验证码
const refresh = async () => {
  clearExpireTimer() // 清理过期定时器
  captchaData.value = null
  thumbLeft.value = 0
  await initCaptcha()
}

// 完全重置验证码状态
const resetCaptcha = async () => {
  console.log('[滑块验证码] 开始完全重置')
  
  // 重置所有状态
  verified.value = false
  retryCount.value = 0
  thumbLeft.value = 0
  dragging.value = false
  
  // 清理过期定时器
  clearExpireTimer()
  
  // 重新初始化验证码
  await initCaptcha()
  
  console.log('[滑块验证码] 完全重置完成')
}

// 暴露方法
defineExpose({
  initCaptcha,
  refresh,
  resetCaptcha,
  captchaData
})

// 组件挂载时自动初始化验证码
onMounted(() => {
  console.log('[滑块验证码] 组件挂载，自动初始化')
  
  // 初始化滑块轨道尺寸
  nextTick(() => {
    if (trackRef.value) {
      const trackEl = trackRef.value as HTMLElement
      const thumbWidth = 40
      // 使用 clientWidth 而不是 offsetWidth，因为我们需要的是内容区域的宽度
      maxLeft.value = trackEl.clientWidth - thumbWidth
      console.log('[滑块验证码] 滑块轨道尺寸初始化:', {
        trackWidth: trackEl.clientWidth,
        thumbWidth: thumbWidth,
        maxLeft: maxLeft.value
      })
    }
  })
  
  initCaptcha()
})

// 组件卸载时清理定时器
onUnmounted(() => {
  console.log('[滑块验证码] 组件卸载，清理定时器')
  clearExpireTimer()
})
</script>

<style lang="less" scoped>
.slider-captcha {
  width: 100%;
  max-width: 400px;
  margin: 0 auto;
  background: var(--ant-color-bg-container);
  border-radius: 8px;
  overflow: hidden;
}

.captcha-image {
  position: relative;
  width: 400px;
  height: 100px;
  //background: #f5f5f5;
  overflow: hidden;
  text-align: center;
  border-radius: 8px;

  img {
    display: inline-block;
    width: 400px; // 后端实际图片宽度
    height: 100px; // 后端实际图片高度
    object-fit: none;
    vertical-align: middle;
    border-radius: 8px;
  }

  .bg-image {
    cursor: pointer;
    transition: opacity 0.2s ease;
    
    &:hover {
      opacity: 0.8;
    }
  }

  .slider-image {
    position: absolute;
    top: 50%;
    left: 0;
    width: 48px;
    height: 48px;
    transform: translateY(-50%);
    transition: all 0.05s linear;
    will-change: left;
    pointer-events: none;
    // 确保滑块位置基于背景图片的实际尺寸
    z-index: 10;
    // 滑块尺寸会根据容器动态调整
    border-radius: 4px;
  }

  // 倒计时覆盖层
  .countdown-overlay {
    position: absolute;
    top: 8px;
    right: 8px;
    z-index: 20;
    pointer-events: none;
    background: rgba(0, 0, 0, 0.6);
    border-radius: 4px;
    padding: 4px 8px;
    
    :deep(.ant-statistic) {
      .ant-statistic-content {
        color: #fff;
        font-size: 12px;
        font-weight: 600;
        text-shadow: 0 1px 2px rgba(0, 0, 0, 0.5);
      }
    }
  }
}

.slider-track {
  position: relative;
  height: 40px;
  background: #f5f5f5;
  border: 1px solid #e8e8e8;
  border-radius: 4px;
  transition: all 0.3s ease;
  margin: 8px 16px 16px;
  
  &:hover {
    border-color: #d9d9d9;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  }
}

.slider-bg {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: linear-gradient(90deg, #f0f0f0 0%, #e8e8e8 100%);
  border-radius: 4px;
}

.slider-thumb {
  position: absolute;
  top: -1px;
  width: 40px;
  height: 40px;
  background: #fff;
  border: 1px solid #d9d9d9;
  border-radius: 4px;
  box-sizing: border-box;
  color: #666;
  text-align: center;
  cursor: pointer;
  transition: all 0.15s cubic-bezier(0.4, 0, 0.2, 1);
  z-index: 10;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  will-change: transform, left;
  
  &.hover {
    border-color: #40a9ff;
    box-shadow: 0 2px 8px rgba(64, 169, 255, 0.3);
    transform: scale(1.02);
  }
  
  &.dragging {
    background: rgba(24, 144, 255, 0.8);
    border-color: #1890ff;
    color: #fff;
    box-shadow: 0 4px 12px rgba(24, 144, 255, 0.4);
    transform: scale(1.05);
    transition: none; // 拖拽时禁用过渡，确保跟随鼠标
  }
  
  &.success {
    background: #fa8c16;
    border-color: #fa8c16;
    color: #fff;
    box-shadow: 0 2px 8px rgba(250, 140, 22, 0.3);
  }
  
  .thumb-icon {
    display: flex;
    align-items: center;
    justify-content: center;
    width: 100%;
    height: 100%;
    
    .arrow-icon {
      font-family: monospace;
      font-size: 16px;
      font-weight: bold;
      color: #333;
      letter-spacing: -2px;
      transition: transform 0.15s cubic-bezier(0.4, 0, 0.2, 1);
    }
    
    .success-icon {
      font-size: 18px;
      font-weight: bold;
      color: #fff;
      transition: transform 0.15s cubic-bezier(0.4, 0, 0.2, 1);
    }
  }
  
  &:hover .thumb-icon .arrow-icon,
  &:hover .thumb-icon .success-icon {
    transform: scale(1.05);
  }
  
  &.dragging .thumb-icon .arrow-icon {
    color: #fff;
  }
}



.success-text {
  position: absolute;
  top: 0;
  bottom: 0;
  display: flex;
  align-items: center;
  color: #fa8c16;
  font-size: 14px;
  font-weight: 600;
  pointer-events: none;
  opacity: 0;
  transform: scale(0.8);
  transition: all 0.25s cubic-bezier(0.4, 0, 0.2, 1);
  will-change: transform, opacity;
  z-index: 5;
  
  &.show {
    opacity: 1;
    transform: scale(1);
  }
  
  &.left {
    left: 10px;
    right: auto;
    justify-content: flex-start;
  }
  
  &.right {
    left: auto;
    right: 10px;
    justify-content: flex-end;
  }
}

  .slider-hint {
    position: absolute;
    top: 0;
    bottom: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #666;
    font-size: 13px;
    font-weight: 500;
    pointer-events: none;
    transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
    will-change: left, opacity, transform;
    z-index: 2;
    white-space: nowrap;
    text-align: center;
    
    &.active {
      color: #333;
      font-weight: 600;
    }
  }
  
  .slider-hint-left {
    position: absolute;
    top: 0;
    bottom: 0;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    color: #666;
    font-size: 13px;
    font-weight: 500;
    pointer-events: none;
    transition: all 0.2s cubic-bezier(0.4, 0, 0.2, 1);
    will-change: opacity, clip-path;
    z-index: 2;
    white-space: nowrap;
    text-align: left;
    
    &.active {
      color: #333;
      font-weight: 600;
    }
  }

.progress-bar {
  position: absolute;
  top: 0;
  left: 0;
  height: 100%;
  background: #67c23a;
  border-radius: 4px 0 0 4px;
  transition: width 0.05s cubic-bezier(0.4, 0, 0.2, 1);
  z-index: 1;
  will-change: width;
}

.slider-text {
  margin-top: 6px;
  text-align: center;
  color: var(--ant-color-text-secondary);
  font-size: 14px;
}

// 暗色主题支持
@media (prefers-color-scheme: dark) {
  .slider-track {
    background: #262626;
    border-color: #434343;
    border-radius: 4px;
    
    &:hover {
      border-color: #595959;
    }
  }
  
  .slider-bg {
    background: linear-gradient(90deg, #262626 0%, #1f1f1f 100%);
    border-radius: 4px;
  }
  
  .slider-thumb {
    background: #434343;
    border-color: #595959;
    color: #d9d9d9;
    border-radius: 4px;
    
    &.hover {
      border-color: #177ddc;
    }
    
    &.dragging {
      background: rgba(23, 125, 220, 0.8);
      border-color: #177ddc;
    }
    
    .thumb-icon .arrow-icon {
      color: #d9d9d9;
    }
  }
  

  
  .progress-bar {
    background: #fa8c16;
    border-radius: 4px 0 0 4px;
  }
  
  .slider-hint {
    color: #d9d9d9;
    
    &.active {
      color: #fff;
    }
    
    &.left-hint,
    &.right-hint {
      color: #177ddc;
    }
  }
  
  .slider-hint-left {
    color: #d9d9d9;
    
    &.active {
      color: #fff;
    }
    
    &.left-hint,
    &.right-hint {
      color: #177ddc;
    }
  }
}
</style> 