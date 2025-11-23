<template>
  <div class="behavior-slider-container">
    <div class="slider-track" ref="trackRef">
      <!-- 背景轨道 -->
      <div class="slider-bg"></div>
      
      <!-- 滑块 -->
      <div
        class="slider-thumb"
        :class="{ 
          'dragging': dragging, 
          'success': success,
          'hover': !dragging && !success && thumbLeft === 0
        }"
        :style="{ left: thumbLeft + 'px' }"
        @mousedown="onDragStart"
        @touchstart="onDragStart"
      >
                 <div class="thumb-icon">
           <span v-if="!success" class="arrow-icon">>></span>
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
        :class="{ 'active': thumbLeft > 0 && !success }"
        :style="{ 
          left: '50%',
          transform: 'translateX(-50%)',
          opacity: success ? 0 : 1,
          clipPath: thumbLeft > 0 && thumbLeft <= hintWidth ? `inset(0 0 0 ${Math.max(0, thumbLeft - 200)}px)` : 'inset(0 0 0 0)'
        }"
      >
        <span>{{ t('identity.auth.captcha.behavior.default') }}</span>
      </div>
      
      <!-- 成功文字 - 始终显示在左边 -->
      <div 
        class="success-text"
        :class="{ 'show': success }"
      >
        {{ t('identity.auth.captcha.behavior.success') }}
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, nextTick, computed, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import { collectBehavior, validateBehavior } from '@/api/security/captcha'

const { t } = useI18n()

const emit = defineEmits(['success', 'error'])
const trackRef = ref<HTMLElement | null>(null)
const thumbLeft = ref(0)
const maxLeft = ref(0)
const dragging = ref(false)
const success = ref(false)
const track = ref<Array<{ x: number; y: number; t: number }>>([])
const userId = '' // 可根据实际登录场景传递

// 新增：主提示宽度
const hintRef = ref<HTMLElement | null>(null)
const hintWidth = ref(0)


// 计算进度百分比
const progressPercent = computed(() => {
  return maxLeft.value > 0 ? (thumbLeft.value / maxLeft.value) * 100 : 0
})

const onDragStart = (e: MouseEvent | TouchEvent) => {
  if (success.value) return
  
  e.preventDefault()
  dragging.value = true
  track.value = []
  
  const clientX = 'touches' in e ? e.touches[0].clientX : e.clientX
  const clientY = 'touches' in e ? e.touches[0].clientY : e.clientY
  const startX = clientX - thumbLeft.value
  track.value.push({ x: thumbLeft.value, y: clientY, t: Date.now() })
  
  const move = (ev: MouseEvent | TouchEvent) => {
    if (!dragging.value) return
    
    ev.preventDefault()
    const moveX = 'touches' in ev ? ev.touches[0].clientX : ev.clientX
    const moveY = 'touches' in ev ? ev.touches[0].clientY : ev.clientY
    let left = moveX - startX
    left = Math.max(0, Math.min(left, maxLeft.value))
    
    // 使用 requestAnimationFrame 确保动画流畅
    requestAnimationFrame(() => {
      thumbLeft.value = left
    })
    
    track.value.push({ x: left, y: moveY, t: Date.now() })
  }
  
  const up = async () => {
    if (!dragging.value) return
    
    try {
      dragging.value = false
      document.removeEventListener('mousemove', move)
      document.removeEventListener('mouseup', up)
      document.removeEventListener('touchmove', move)
      document.removeEventListener('touchend', up)
      
      // 检查是否拖到最右边（允许2px的误差）
      if (thumbLeft.value >= maxLeft.value - 2) {
        // 发送轨迹到后端
        try {
          // 计算按键间隔（模拟用户操作）
          const keyIntervals = []
          if (track.value.length > 1) {
            for (let i = 1; i < track.value.length; i++) {
              const interval = track.value[i].t - track.value[i - 1].t
              if (interval > 30 && interval < 200) { // 记录合理的间隔范围
                keyIntervals.push(interval)
              }
            }
          }
          
          const { data: token } = await collectBehavior({
            userId,
            mouseTrack: track.value.map(p => ({ 
              x: Math.round(p.x), 
              y: Math.round(p.y), // 使用真实的Y坐标
              timestamp: p.t 
            })),
            keyIntervals: keyIntervals,
            duration: track.value.length > 1 ? track.value[track.value.length - 1].t - track.value[0].t : 0
          })
          
          // 校验行为token
          const { data } = await validateBehavior(token)
          if (data.success) {
            success.value = true
            emit('success', { token })
          } else {
            // 验证失败，重置滑块
            await resetSlider()
            message.error(t('identity.auth.captcha.behavior.failed'))
            emit('error', 'fail')
          }
        } catch (err) {
          console.error('[行为验证码] 验证失败:', err)
          await resetSlider()
          message.error(t('identity.auth.captcha.behavior.verifyError'))
          emit('error', err)
        }
      } else {
        // 未拖到最右边，重置滑块
        await resetSlider()
      }
    } catch (error) {
      console.error('[行为验证码] 处理滑块释放时出错:', error)
      await resetSlider()
      message.error(t('identity.auth.captcha.behavior.verifyError'))
      emit('error', error)
    }
  }
  
  document.addEventListener('mousemove', move)
  document.addEventListener('mouseup', up)
  document.addEventListener('touchmove', move)
  document.addEventListener('touchend', up)
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

// 完全重置验证码状态
const resetCaptcha = async () => {
  console.log('[行为验证码] 开始完全重置')
  
  // 重置所有状态
  success.value = false
  thumbLeft.value = 0
  dragging.value = false
  track.value = []
  
  console.log('[行为验证码] 完全重置完成')
}

// 暴露方法
defineExpose({
  resetCaptcha
})

onMounted(() => {
  nextTick(() => {
    if (trackRef.value) {
      const trackEl = trackRef.value as HTMLElement
      const thumbWidth = 40
      maxLeft.value = trackEl.offsetWidth - thumbWidth
    }
    if (hintRef.value) {
      hintWidth.value = hintRef.value.offsetWidth
    }
  })
})

// 监听滑块位置变化，更新提示文字宽度
watch(() => thumbLeft.value, () => {
  if (hintRef.value) {
    hintWidth.value = hintRef.value.offsetWidth
  }
})


</script>

<style lang="less" scoped>
.behavior-slider-container {
  width: 320px;
  margin: 0 auto;
  user-select: none;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
}

.slider-track {
  position: relative;
  height: 40px;
  background: #f5f5f5;
  border: 1px solid #e8e8e8;
  border-radius: 4px;
  transition: all 0.3s ease;
  
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
  
  // 始终显示在左边
  left: 10px;
  right: auto;
  justify-content: flex-start;
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

// 响应式设计
@media (max-width: 480px) {
  .behavior-slider-container {
    width: 280px;
  }
  
  .slider-text {
    font-size: 13px;
  }
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
  

  
     .slider-hint {
     color: #d9d9d9;
     
     &.active {
       color: #fff;
     }
   }
  
  .progress-bar {
     background: #fa8c16;
     border-radius: 4px 0 0 4px;
   }
   
   .success-text {
     color: #fa8c16;
   }
  
  .slider-hint {
    color: #d9d9d9;
    
    &.active {
      color: #fff;
    }
  }
}
</style> 