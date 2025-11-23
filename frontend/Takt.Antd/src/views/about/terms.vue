<template>
  <div class="terms-container">
    <a-card :bordered="false">
      <template #title>
        <h2>使用条款</h2>
      </template>
      <a-typography>
        <a-typography-title :level="3">1. 接受条款</a-typography-title>
        <a-typography-paragraph>
          欢迎使用 Lean.Takt 系统。通过访问或使用我们的服务，您同意受这些条款的约束。如果您不同意这些条款，请不要使用我们的服务。
        </a-typography-paragraph>

        <a-typography-title :level="3">2. 服务说明</a-typography-title>
        <a-typography-paragraph>
          Lean.Takt 是一个企业级管理系统，提供包括但不限于系统管理、用户管理、权限管理等功能。我们保留随时修改、暂停或终止服务的权利。
        </a-typography-paragraph>

        <a-typography-title :level="3">3. 用户责任</a-typography-title>
        <a-typography-paragraph>
          <ul>
            <li>您必须遵守所有适用的法律法规</li>
            <li>保护您的账户安全</li>
            <li>不得滥用或破坏系统服务</li>
            <li>对您账户下的所有活动负责</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">4. 知识产权</a-typography-title>
        <a-typography-paragraph>
          系统及其原始内容、特性和功能归 Lean365 所有，受国际版权法保护。未经 Lean365 事先书面许可，不得复制、修改、分发或商业使用这些材料。
        </a-typography-paragraph>

        <a-typography-title :level="3">5. 免责声明</a-typography-title>
        <a-typography-paragraph>
          我们的服务按"现状"提供，不提供任何明示或暗示的保证。我们不保证服务的准确性、完整性、及时性或可靠性。
        </a-typography-paragraph>

        <a-typography-title :level="3">6. 条款变更</a-typography-title>
        <a-typography-paragraph>
          我们保留随时修改这些条款的权利。修改后的条款将在网站上公布。继续使用我们的服务即表示您接受修改后的条款。
        </a-typography-paragraph>

        <a-typography-title :level="3">7. 联系我们</a-typography-title>
        <a-typography-paragraph>
          如果您对这些条款有任何疑问，请联系我们：support@lean365.com
        </a-typography-paragraph>
      </a-typography>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
defineOptions({
  name: 'Terms'
})
</script>

<style lang="less" scoped>
.terms-container {
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

