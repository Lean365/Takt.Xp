<template>
  <div class="header-login-bar">
    <a-space>
      <Takt-locale />
      <Takt-theme />
      <Takt-memorial />
    </a-space>
  </div>
</template>

<script lang="ts" setup>
</script>

<style lang="less" scoped>
.header-login-bar {
  position: fixed;
  top: 24px;
  right: 24px;
  z-index: 1000;
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 8px 12px;
  border-radius: 6px;
  backdrop-filter: blur(8px);
}

// 响应式布局
@media screen and (max-width: 768px) {
  .header-login-bar {
    top: 16px;
    right: 16px;
    padding: 6px 8px;
  }
}
</style> 
