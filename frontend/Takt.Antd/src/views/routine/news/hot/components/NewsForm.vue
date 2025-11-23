<template>
  <a-modal
    :open="visible"
    :title="title"
    :width="800"
    :confirm-loading="loading"
    @ok="handleSubmit"
    @cancel="handleCancel"
  >
    <a-form
      ref="formRef"
      :model="formData"
      :rules="rules"
      :label-col="{ span: 4 }"
      :wrapper-col="{ span: 19 }"
    >
      <a-form-item label="新闻标题" name="newsTitle">
        <a-input
          v-model:value="formData.newsTitle"
          placeholder="请输入新闻标题"
          :maxlength="200"
          show-count
        />
      </a-form-item>

      <a-form-item label="新闻副标题" name="newsSubtitle">
        <a-input
          v-model:value="formData.newsSubtitle"
          placeholder="请输入新闻副标题"
          :maxlength="100"
          show-count
        />
      </a-form-item>

      <a-form-item label="新闻分类" name="newsCategory">
        <a-input
          v-model:value="formData.newsCategory"
          placeholder="请输入新闻分类"
          :maxlength="50"
        />
      </a-form-item>

      <a-form-item label="新闻标签" name="newsTags">
        <a-input
          v-model:value="formData.newsTags"
          placeholder="请输入新闻标签，多个标签用逗号分隔"
          :maxlength="200"
        />
      </a-form-item>

      <a-form-item label="新闻关键词" name="newsKeywords">
        <a-input
          v-model:value="formData.newsKeywords"
          placeholder="请输入新闻关键词，多个关键词用逗号分隔"
          :maxlength="200"
        />
      </a-form-item>

      <a-form-item label="新闻封面" name="newsCover">
        <a-upload
          v-model:file-list="fileList"
          :custom-request="handleUpload"
          :show-upload-list="false"
          accept="image/*"
          list-type="picture-card"
        >
          <div v-if="!formData.newsCover">
            <plus-outlined />
            <div style="margin-top: 8px">上传封面</div>
          </div>
          <img
            v-else
            :src="formData.newsCover"
            style="width: 100%; height: 100%; object-fit: cover"
          />
        </a-upload>
      </a-form-item>

      <a-form-item label="新闻内容" name="newsContent">
        <a-textarea
          v-model:value="formData.newsContent"
          placeholder="请输入新闻内容"
          :rows="8"
          :maxlength="10000"
          show-count
        />
      </a-form-item>

      <a-form-item label="新闻摘要" name="newsSummary">
        <a-textarea
          v-model:value="formData.newsSummary"
          placeholder="请输入新闻摘要"
          :rows="3"
          :maxlength="500"
          show-count
        />
      </a-form-item>

      <a-form-item label="新闻来源" name="newsSource">
        <a-input
          v-model:value="formData.newsSource"
          placeholder="请输入新闻来源"
          :maxlength="100"
        />
      </a-form-item>

      <a-form-item label="来源链接" name="newsSourceUrl">
        <a-input
          v-model:value="formData.newsSourceUrl"
          placeholder="请输入来源链接"
          :maxlength="500"
        />
      </a-form-item>

      <a-form-item label="作者" name="authorName">
        <a-input
          v-model:value="formData.authorName"
          placeholder="请输入作者姓名"
          :maxlength="50"
        />
      </a-form-item>

      <a-form-item label="发布部门" name="publishDepartment">
        <a-input
          v-model:value="formData.publishDepartment"
          placeholder="请输入发布部门"
          :maxlength="100"
        />
      </a-form-item>

      <a-form-item label="发布时间" name="publishTime">
        <a-date-picker
          v-model:value="formData.publishTime"
          show-time
          format="YYYY-MM-DD HH:mm:ss"
          placeholder="请选择发布时间"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="过期时间" name="expiryTime">
        <a-date-picker
          v-model:value="formData.expiryTime"
          show-time
          format="YYYY-MM-DD HH:mm:ss"
          placeholder="请选择过期时间"
          style="width: 100%"
        />
      </a-form-item>

      <a-form-item label="状态" name="status">
        <a-select
          v-model:value="formData.status"
          placeholder="请选择状态"
          style="width: 100%"
        >
          <a-select-option :value="0">草稿</a-select-option>
          <a-select-option :value="1">已发布</a-select-option>
          <a-select-option :value="2">已下线</a-select-option>
        </a-select>
      </a-form-item>

      <a-form-item label="置顶" name="isTop">
        <a-switch
          v-model:checked="formData.isTop"
          checked-children="是"
          un-checked-children="否"
        />
      </a-form-item>

      <a-form-item label="推荐" name="isRecommend">
        <a-switch
          v-model:checked="formData.isRecommend"
          checked-children="是"
          un-checked-children="否"
        />
      </a-form-item>

      <a-form-item label="热门" name="isHot">
        <a-switch
          v-model:checked="formData.isHot"
          checked-children="是"
          un-checked-children="否"
        />
      </a-form-item>

      <a-form-item label="原创" name="isOriginal">
        <a-switch
          v-model:checked="formData.isOriginal"
          checked-children="是"
          un-checked-children="否"
        />
      </a-form-item>

      <a-form-item label="突发" name="isBreaking">
        <a-switch
          v-model:checked="formData.isBreaking"
          checked-children="是"
          un-checked-children="否"
        />
      </a-form-item>

      <a-form-item label="SEO标题" name="seoTitle">
        <a-input
          v-model:value="formData.seoTitle"
          placeholder="请输入SEO标题"
          :maxlength="200"
        />
      </a-form-item>

      <a-form-item label="SEO关键词" name="seoKeywords">
        <a-input
          v-model:value="formData.seoKeywords"
          placeholder="请输入SEO关键词"
          :maxlength="500"
        />
      </a-form-item>

      <a-form-item label="SEO描述" name="seoDescription">
        <a-textarea
          v-model:value="formData.seoDescription"
          placeholder="请输入SEO描述"
          :rows="3"
          :maxlength="500"
          show-count
        />
      </a-form-item>

      <a-form-item label="备注" name="remark">
        <a-textarea
          v-model:value="formData.remark"
          placeholder="请输入备注"
          :rows="3"
          :maxlength="500"
          show-count
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script lang="ts" setup>
import { ref, reactive, watch, computed } from 'vue'
import { message } from 'ant-design-vue'
import { PlusOutlined } from '@ant-design/icons-vue'
import type { FormInstance } from 'ant-design-vue'
import type { TaktNewsCreate, TaktNewsUpdate } from '@/types/routine/news/news'
import { getNewsById, createNews, updateNews } from '@/api/routine/news/news'

interface Props {
  visible: boolean
  title: string
  newsId?: number
}

interface Emits {
  (e: 'update:visible', value: boolean): void
  (e: 'success'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const formRef = ref<FormInstance>()
const loading = ref(false)
const fileList = ref<any[]>([])

// 表单数据
const formData = reactive<TaktNewsCreate & { newsId?: number }>({
  newsTitle: '',
  newsSubtitle: '',
  newsContent: '',
  newsSummary: '',
  newsCategory: '',
  newsTags: '',
  newsKeywords: '',
  newsCover: '',
  newsImages: '',
  newsVideo: '',
  newsAudio: '',
  newsAttachments: '',
  newsSource: '',
  newsSourceUrl: '',
  newsAuthor: '',
  authorId: undefined,
  authorName: '',
  authorAvatar: '',
  publishDepartment: '',
  publishTime: undefined,
  expiryTime: undefined,
  status: 0,
  isTop: 0,
  isRecommend: 0,
  isHot: 0,
  isOriginal: 0,
  isBreaking: 0,
  seoTitle: '',
  seoKeywords: '',
  seoDescription: '',
  remark: ''
})

// 表单验证规则
const rules = {
  newsTitle: [
    { required: true, message: '请输入新闻标题', trigger: 'blur' },
    { max: 200, message: '新闻标题不能超过200个字符', trigger: 'blur' }
  ],
  newsContent: [
    { required: true, message: '请输入新闻内容', trigger: 'blur' },
    { max: 10000, message: '新闻内容不能超过10000个字符', trigger: 'blur' }
  ],
  newsCategory: [
    { required: true, message: '请输入新闻分类', trigger: 'blur' },
    { max: 50, message: '新闻分类不能超过50个字符', trigger: 'blur' }
  ],
  authorName: [
    { required: true, message: '请输入作者姓名', trigger: 'blur' },
    { max: 50, message: '作者姓名不能超过50个字符', trigger: 'blur' }
  ],
  publishDepartment: [
    { required: true, message: '请输入发布部门', trigger: 'blur' },
    { max: 100, message: '发布部门不能超过100个字符', trigger: 'blur' }
  ]
}

// 监听visible变化，加载数据
watch(
  () => props.visible,
  async (visible) => {
    if (visible && props.newsId) {
      await loadNewsData()
    } else if (!visible) {
      resetForm()
    }
  }
)

// 加载新闻数据
const loadNewsData = async () => {
  if (!props.newsId) return
  
  try {
    loading.value = true
    const res = await getNewsById(props.newsId)
    if (res.data.code === 200) {
      const news = res.data.data
      Object.assign(formData, {
        newsId: news.newsId,
        newsTitle: news.newsTitle,
        newsSubtitle: news.newsSubtitle,
        newsContent: news.newsContent,
        newsSummary: news.newsSummary,
        newsCategory: news.newsCategory,
        newsTags: news.newsTags,
        newsKeywords: news.newsKeywords,
        newsCover: news.newsCover,
        newsImages: news.newsImages,
        newsVideo: news.newsVideo,
        newsAudio: news.newsAudio,
        newsAttachments: news.newsAttachments,
        newsSource: news.newsSource,
        newsSourceUrl: news.newsSourceUrl,
        newsAuthor: news.newsAuthor,
        authorId: news.authorId,
        authorName: news.authorName,
        authorAvatar: news.authorAvatar,
        publishDepartment: news.publishDepartment,
        publishTime: news.publishTime ? new Date(news.publishTime) : undefined,
        expiryTime: news.expiryTime ? new Date(news.expiryTime) : undefined,
        status: news.status,
        isTop: news.isTop,
        isRecommend: news.isRecommend,
        isHot: news.isHot,
        isOriginal: news.isOriginal,
        isBreaking: news.isBreaking,
        seoTitle: news.seoTitle,
        seoKeywords: news.seoKeywords,
        seoDescription: news.seoDescription,
        remark: news.remark
      })
    } else {
      message.error(res.data.msg || '获取新闻数据失败')
    }
  } catch (error) {
    console.error('[新闻表单] 加载数据出错:', error)
    message.error('获取新闻数据失败')
  } finally {
    loading.value = false
  }
}

// 重置表单
const resetForm = () => {
  Object.assign(formData, {
    newsId: undefined,
    newsTitle: '',
    newsSubtitle: '',
    newsContent: '',
    newsSummary: '',
    newsCategory: '',
    newsTags: '',
    newsKeywords: '',
    newsCover: '',
    newsImages: '',
    newsVideo: '',
    newsAudio: '',
    newsAttachments: '',
    newsSource: '',
    newsSourceUrl: '',
    newsAuthor: '',
    authorId: undefined,
    authorName: '',
    authorAvatar: '',
    publishDepartment: '',
    publishTime: undefined,
    expiryTime: undefined,
    status: 0,
    isTop: 0,
    isRecommend: 0,
    isHot: 0,
    isOriginal: 0,
    isBreaking: 0,
    seoTitle: '',
    seoKeywords: '',
    seoDescription: '',
    remark: ''
  })
  fileList.value = []
  formRef.value?.clearValidate()
}

// 处理文件上传
const handleUpload = (options: any) => {
  const { file, onSuccess, onError } = options
  
  // 这里应该调用实际的文件上传API
  // 暂时模拟上传成功
  setTimeout(() => {
    const url = URL.createObjectURL(file)
    formData.newsCover = url
    onSuccess()
  }, 1000)
}

// 提交表单
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    const submitData = { ...formData }
    
    // 转换布尔值为数字
    submitData.isTop = submitData.isTop ? 1 : 0
    submitData.isRecommend = submitData.isRecommend ? 1 : 0
    submitData.isHot = submitData.isHot ? 1 : 0
    submitData.isOriginal = submitData.isOriginal ? 1 : 0
    submitData.isBreaking = submitData.isBreaking ? 1 : 0
    
    let res
    if (props.newsId) {
      // 更新
      res = await updateNews(submitData as TaktNewsUpdate)
    } else {
      // 新增
      res = await createNews(submitData as TaktNewsCreate)
    }
    
    if (res.data.code === 200) {
      message.success(props.newsId ? '更新成功' : '创建成功')
      emit('success')
      handleCancel()
    } else {
      message.error(res.data.msg || (props.newsId ? '更新失败' : '创建失败'))
    }
  } catch (error) {
    console.error('[新闻表单] 提交出错:', error)
    message.error('提交失败')
  } finally {
    loading.value = false
  }
}

// 取消
const handleCancel = () => {
  emit('update:visible', false)
}
</script>

<style scoped>
.ant-upload-select-picture-card {
  width: 120px;
  height: 80px;
}
</style> 
