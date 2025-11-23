<template>
  <div class="preview-container">
    <div class="preview-header">
      <h3>{{ t('generator.table.preview.entities.title') }}</h3>
      <div class="preview-actions">
        <a-button type="primary" @click="handleCopy">
          {{ t('generator.table.preview.copy') }}
        </a-button>
        <a-button @click="handleDownload">
          {{ t('generator.table.preview.download') }}
        </a-button>
        <a-switch
          v-model:checked="showLineNumbers"
          :checked-children="t('generator.table.preview.showLineNumbers')"
          :un-checked-children="t('generator.table.preview.hideLineNumbers')"
        />
      </div>
    </div>
    <div class="preview-content" ref="previewContent">
      <pre v-highlightjs="formattedCode" :class="['language-java', { 'line-numbers': showLineNumbers }]">
        <code></code>
      </pre>
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import hljs from 'highlight.js'
import 'highlight.js/styles/github.css'
import 'highlight.js/lib/languages/java'

const { t } = useI18n()

const props = defineProps<{
  code: string
}>()

const previewContent = ref<HTMLElement>()
const showLineNumbers = ref(true)

// 格式化代码，添加行号
const formattedCode = computed(() => {
  if (!showLineNumbers.value) return props.code
  
  const lines = props.code.split('\n')
  return lines.map((line, index) => {
    const lineNumber = String(index + 1).padStart(3, ' ')
    return `${lineNumber} ${line}`
  }).join('\n')
})

/** 复制代码 */
const handleCopy = () => {
  navigator.clipboard.writeText(props.code)
    .then(() => {
      message.success(t('generator.table.preview.copySuccess'))
    })
    .catch(() => {
      message.error(t('generator.table.preview.copyFailed'))
    })
}

/** 下载代码 */
const handleDownload = () => {
  const blob = new Blob([props.code], { type: 'text/plain;charset=utf-8' })
  const url = window.URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = 'Entity.java'
  document.body.appendChild(link)
  link.click()
  document.body.removeChild(link)
  window.URL.revokeObjectURL(url)
  message.success(t('generator.table.preview.downloadSuccess'))
}

// 自定义指令：代码高亮
const vHighlightjs = {
  mounted: (el: HTMLElement) => {
    const blocks = el.querySelectorAll('pre code')
    blocks.forEach((block) => {
      hljs.highlightElement(block as HTMLElement)
    })
  },
  updated: (el: HTMLElement) => {
    const blocks = el.querySelectorAll('pre code')
    blocks.forEach((block) => {
      hljs.highlightElement(block as HTMLElement)
    })
  }
}

onMounted(() => {
  if (previewContent.value) {
    const blocks = previewContent.value.querySelectorAll('pre code')
    blocks.forEach((block) => {
      hljs.highlightElement(block as HTMLElement)
    })
  }
})
</script>

<style lang="less" scoped>
.preview-container {
  height: 100%;
  display: flex;
  flex-direction: column;

  .preview-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 16px;
    padding: 0 16px;

    h3 {
      margin: 0;
    }

    .preview-actions {
      display: flex;
      gap: 8px;
      align-items: center;
    }
  }

  .preview-content {
    flex: 1;
    background-color: #f5f5f5;
    padding: 16px;
    border-radius: 4px;
    overflow: auto;

    :deep(pre) {
      margin: 0;
      padding: 0;
      background: none;
      white-space: pre-wrap;
      word-wrap: break-word;

      &.line-numbers {
        counter-reset: line;
        padding-left: 3.8em;
        position: relative;

        &::before {
          content: '';
          position: absolute;
          left: 0;
          top: 0;
          bottom: 0;
          width: 3.2em;
          background: #f0f0f0;
          border-right: 1px solid #ddd;
        }

        code {
          position: relative;
          display: block;
          padding-left: 0.5em;

          &::before {
            counter-increment: line;
            content: counter(line);
            position: absolute;
            left: -3.2em;
            width: 2.5em;
            text-align: right;
            color: #999;
            padding-right: 0.5em;
            user-select: none;
          }
        }
      }
    }

    :deep(code) {
      font-family: 'Fira Code', 'Consolas', monospace;
      font-size: 14px;
      line-height: 1.6;
    }
  }
}
</style> 