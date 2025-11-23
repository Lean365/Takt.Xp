// ===================================================================
// 项目名称: Lean.Takt
// 文件名称: iconColor.ts 
// 创建日期: 2024-03-20
// 描述: 图标随机颜色工具函数
// ===================================================================

/**
 * 简单的字符串哈希函数
 * @param str 输入字符串
 * @returns 哈希值
 */
function simpleHash(str: string): number {
  let hash = 0;
  if (str.length === 0) return hash;
  
  for (let i = 0; i < str.length; i++) {
    const char = str.charCodeAt(i);
    hash = ((hash << 5) - hash) + char;
    hash = hash & hash; // 转换为32位整数
  }
  
  return Math.abs(hash);
}

/**
 * 生成随机颜色值
 * @param seed 种子值（可以是字符串或数字）
 * @returns HSL颜色对象
 */
export function generateRandomColor(seed: string | number): { hue: number; saturation: number; lightness: number } {
  const seedStr = typeof seed === 'string' ? seed : seed.toString();
  const hash = simpleHash(seedStr);
  
  // 使用黄金角度生成色相，确保颜色分布均匀
  const hue = (hash * 137.508) % 360; // 黄金角度约等于137.508度
  
  // 生成饱和度和亮度，确保颜色美观
  const saturation = 60 + (hash % 30); // 60%-90%之间
  const lightness = 40 + (hash % 20);  // 40%-60%之间
  
  return { hue, saturation, lightness };
}

/**
 * 为图标元素设置随机颜色
 * @param element 图标元素
 * @param seed 种子值（图标名称或其他标识）
 */
export function setIconRandomColor(element: HTMLElement, seed?: string): void {
  const iconSeed = seed || element.textContent || element.className || element.id || 'default';
  const color = generateRandomColor(iconSeed);
  
  // 设置CSS变量
  element.style.setProperty('--icon-hue-value', `${color.hue}deg`);
  element.style.setProperty('--icon-saturation-value', `${color.saturation}%`);
  element.style.setProperty('--icon-lightness-value', `${color.lightness}%`);
  
  // 添加随机颜色类
  element.classList.add('Takt-icon-random');
  
  // console.log(`[图标颜色] 为图标设置随机颜色:`, {
  //   种子: iconSeed,
  //   色相: color.hue,
  //   饱和度: color.saturation,
  //   亮度: color.lightness
  // });
}

/**
 * 为图标元素设置主题颜色
 * @param element 图标元素
 * @param theme 主题类型
 */
export function setIconThemeColor(element: HTMLElement, theme: 'primary' | 'success' | 'warning' | 'error' | 'info'): void {
  // 移除随机颜色类
  element.classList.remove('Takt-icon-random');
  
  // 添加主题颜色类
  element.classList.add(`Takt-icon-${theme}`);
  
  // console.log(`[图标颜色] 为图标设置主题颜色:`, theme);
}

/**
 * 为图标元素设置渐变颜色
 * @param element 图标元素
 * @param gradientType 渐变类型
 */
export function setIconGradientColor(element: HTMLElement, gradientType: 'primary' | 'success' | 'warning' | 'error'): void {
  // 移除其他颜色类
  element.classList.remove('Takt-icon-random', 'Takt-icon-primary', 'Takt-icon-success', 'Takt-icon-warning', 'Takt-icon-error', 'Takt-icon-info');
  
  // 添加渐变颜色类
  element.classList.add(`Takt-icon-gradient-${gradientType}`);
  
  // console.log(`[图标颜色] 为图标设置渐变颜色:`, gradientType);
}

/**
 * 为图标元素设置动画效果
 * @param element 图标元素
 * @param animation 动画类型
 */
export function setIconAnimation(element: HTMLElement, animation: 'pulse' | 'spin' | 'bounce' | 'shake'): void {
  // 移除其他动画类
  element.classList.remove('Takt-icon-pulse', 'Takt-icon-spin', 'Takt-icon-bounce', 'Takt-icon-shake');
  
  // 添加动画类
  element.classList.add(`Takt-icon-${animation}`);
  
  // console.log(`[图标动画] 为图标设置动画:`, animation);
}

/**
 * 为图标元素设置尺寸
 * @param element 图标元素
 * @param size 尺寸类型
 */
export function setIconSize(element: HTMLElement, size: 'xs' | 'sm' | 'md' | 'lg' | 'xl' | 'xxl'): void {
  // 移除其他尺寸类
  element.classList.remove('Takt-icon-xs', 'Takt-icon-sm', 'Takt-icon-md', 'Takt-icon-lg', 'Takt-icon-xl', 'Takt-icon-xxl');
  
  // 添加尺寸类
  element.classList.add(`Takt-icon-${size}`);
  
  // console.log(`[图标尺寸] 为图标设置尺寸:`, size);
}

/**
 * 为图标元素设置状态
 * @param element 图标元素
 * @param status 状态类型
 */
export function setIconStatus(element: HTMLElement, status: 'disabled' | 'loading' | 'selected' | 'highlight'): void {
  // 移除其他状态类
  element.classList.remove('Takt-icon-disabled', 'Takt-icon-loading', 'Takt-icon-selected', 'Takt-icon-highlight');
  
  // 添加状态类
  element.classList.add(`Takt-icon-${status}`);
  
  // console.log(`[图标状态] 为图标设置状态:`, status);
}

/**
 * 清除图标的所有样式
 * @param element 图标元素
 */
export function clearIconStyles(element: HTMLElement): void {
  // 移除所有自定义类
  const classesToRemove = [
    'Takt-icon-random', 'Takt-icon-primary', 'Takt-icon-success', 'Takt-icon-warning', 'Takt-icon-error', 'Takt-icon-info',
    'Takt-icon-gradient-primary', 'Takt-icon-gradient-success', 'Takt-icon-gradient-warning', 'Takt-icon-gradient-error',
    'Takt-icon-pulse', 'Takt-icon-spin', 'Takt-icon-bounce', 'Takt-icon-shake',
    'Takt-icon-xs', 'Takt-icon-sm', 'Takt-icon-md', 'Takt-icon-lg', 'Takt-icon-xl', 'Takt-icon-xxl',
    'Takt-icon-disabled', 'Takt-icon-loading', 'Takt-icon-selected', 'Takt-icon-highlight'
  ];
  
  classesToRemove.forEach(className => {
    element.classList.remove(className);
  });
  
  // 清除CSS变量
  element.style.removeProperty('--icon-hue-value');
  element.style.removeProperty('--icon-saturation-value');
  element.style.removeProperty('--icon-lightness-value');
  
  // console.log(`[图标样式] 清除图标所有样式`);
}

/**
 * 批量设置图标随机颜色
 * @param selector 选择器
 * @param seedPrefix 种子前缀
 */
export function setIconsRandomColor(selector: string, seedPrefix?: string): void {
  const elements = document.querySelectorAll(selector);
  elements.forEach((element, index) => {
    if (element instanceof HTMLElement) {
      const seed = seedPrefix ? `${seedPrefix}-${index}` : `icon-${index}`;
      setIconRandomColor(element, seed);
    }
  });
  
  // console.log(`[图标颜色] 批量设置随机颜色:`, {
  //   选择器: selector,
  //   元素数量: elements.length,
  //   种子前缀: seedPrefix
  // });
}

/**
 * 监听DOM变化，为新添加的图标设置随机颜色
 */
export function watchIconChanges(): MutationObserver {
  const observer = new MutationObserver((mutations) => {
    mutations.forEach((mutation) => {
      mutation.addedNodes.forEach((node) => {
        if (node instanceof HTMLElement) {
          // 检查新添加的元素是否包含图标
          const icons = node.querySelectorAll('.anticon, .Takt-icon-random');
          icons.forEach((icon) => {
            if (icon instanceof HTMLElement) {
              const iconName = icon.className || icon.textContent?.trim();
              setIconRandomColor(icon, iconName);
            }
          });
        }
      });
    });
  });
  
  observer.observe(document.body, {
    childList: true,
    subtree: true
  });
  
  console.log(`[图标监听] 开始监听DOM变化，自动为新图标设置随机颜色`);
  return observer;
}

/**
 * 获取图标的当前颜色信息
 * @param element 图标元素
 * @returns 颜色信息对象
 */
export function getIconColorInfo(element: HTMLElement): {
  hue: number;
  saturation: number;
  lightness: number;
  color: string;
  classes: string[];
} {
  const computedStyle = window.getComputedStyle(element);
  const color = computedStyle.color;
  
  // 解析HSL颜色
  const hslMatch = color.match(/hsl\(([^)]+)\)/);
  let hue = 0, saturation = 0, lightness = 0;
  
  if (hslMatch) {
    const [h, s, l] = hslMatch[1].split(',').map(v => parseFloat(v));
    hue = h;
    saturation = s;
    lightness = l;
  }
  
  return {
    hue,
    saturation,
    lightness,
    color,
    classes: Array.from(element.classList)
  };
}

// 默认导出主要函数
export default {
  setIconRandomColor,
  setIconThemeColor,
  setIconGradientColor,
  setIconAnimation,
  setIconSize,
  setIconStatus,
  setIconsRandomColor,
  watchIconChanges,
  getIconColorInfo
}; 
