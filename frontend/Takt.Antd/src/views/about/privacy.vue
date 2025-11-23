<template>
  <div class="privacy-container">
    <a-card :bordered="false">
      <template #title>
        <h2>隐私政策</h2>
      </template>
      <a-typography>
        <a-typography-title :level="3">1. 信息收集</a-typography-title>
        <a-typography-paragraph>
          我们收集的信息包括：
          <ul>
            <li>账户信息（用户名、电子邮件等）</li>
            <li>日志数据（IP地址、浏览器类型、访问时间等）</li>
            <li>设备信息（操作系统、设备标识符等）</li>
            <li>使用数据（功能使用情况、操作记录等）</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">2. 信息使用</a-typography-title>
        <a-typography-paragraph>
          我们使用收集的信息用于：
          <ul>
            <li>提供、维护和改进我们的服务</li>
            <li>处理您的请求和响应您的询问</li>
            <li>发送系统通知和更新信息</li>
            <li>防止欺诈和提高安全性</li>
            <li>进行数据分析和系统优化</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">3. 信息共享</a-typography-title>
        <a-typography-paragraph>
          我们不会出售或出租您的个人信息。仅在以下情况下可能共享您的信息：
          <ul>
            <li>经您明确同意</li>
            <li>遵守法律要求</li>
            <li>保护我们的权利和财产</li>
            <li>与我们的服务提供商合作（他们需遵守保密义务）</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">4. 信息安全</a-typography-title>
        <a-typography-paragraph>
          我们采取适当的技术和组织措施来保护您的个人信息，包括：
          <ul>
            <li>数据加密传输和存储</li>
            <li>访问控制和认证机制</li>
            <li>定期安全审计和评估</li>
            <li>员工培训和保密协议</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">5. 您的权利</a-typography-title>
        <a-typography-paragraph>
          您对您的个人信息拥有以下权利：
          <ul>
            <li>访问和获取您的个人信息副本</li>
            <li>更正不准确的个人信息</li>
            <li>要求删除您的个人信息</li>
            <li>反对或限制我们处理您的个人信息</li>
            <li>数据可携带权</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">6. Cookie 使用</a-typography-title>
        <a-typography-paragraph>
          我们使用 Cookie 和类似技术来提供和改善我们的服务。您可以通过浏览器设置控制 Cookie 的使用。
        </a-typography-paragraph>

        <a-typography-title :level="3">7. 隐私政策更新</a-typography-title>
        <a-typography-paragraph>
          我们可能会不时更新本隐私政策。重大变更时，我们会通过适当方式通知您。继续使用我们的服务即表示您接受更新后的隐私政策。
        </a-typography-paragraph>

        <a-typography-title :level="3">8. 联系我们</a-typography-title>
        <a-typography-paragraph>
          如果您对我们的隐私政策有任何疑问，请联系我们：privacy@lean365.com
        </a-typography-paragraph>
      </a-typography>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
defineOptions({
  name: 'Privacy'
})
</script>

<style lang="less" scoped>
.privacy-container {
  padding: 24px;
  max-width: 1200px;
  margin: 0 auto;

  :deep(.ant-typography) {
    color: var(--ant-text-color);
  }

  :deep(ul) {
    padding-left: 20px;
  }
}
</style> 