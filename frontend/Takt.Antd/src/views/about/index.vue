<template>
  <div class="about-container">
    <a-card :bordered="false">
      <template #title>
        <h2>关于 Takt.Xp</h2>
      </template>
      <a-typography>
        <a-typography-title :level="3">系统简介</a-typography-title>
        <a-typography-paragraph>
          Takt.Xp 是一个现代化的企业级管理系统，采用前后端分离架构，基于 .NET 8.0 和 Vue 3 开发。系统提供了丰富的功能模块，包括日常事务、会计核算、后勤管理、人力资源、工作流程、身份认证、代码生成、审计日志、实时通信等，帮助企业高效管理各项业务。
        </a-typography-paragraph>

        <a-typography-title :level="3">技术架构</a-typography-title>
        <a-typography-paragraph>
          <a-row :gutter="24">
            <a-col :span="12">
              <strong>前端技术栈：</strong>
              <ul>
                <li>Vue 3.5.19 - 渐进式 JavaScript 框架</li>
                <li>TypeScript 5.9.2 - 类型安全的 JavaScript 超集</li>
                <li>Ant Design Vue 4.2.6 - 企业级 UI 组件库</li>
                <li>Vite 7.1.3 - 下一代前端构建工具</li>
                <li>Pinia 3.0.3 - Vue 状态管理库</li>
                <li>Vue Router 4.5.1 - 官方路由管理器</li>
                <li>Vue I18n 11.1.11 - 国际化支持</li>
              </ul>
            </a-col>
            <a-col :span="12">
              <strong>后端技术栈：</strong>
              <ul>
                <li>.NET 8.0 - 跨平台的高性能框架</li>
                <li>ASP.NET Core - Web 应用框架</li>
                <li>SqlSugar - 轻量级 ORM 框架</li>
                <li>Mapster - 对象映射工具</li>
                <li>NLog - 日志记录框架</li>
                <li>Swagger - API 文档生成</li>
                <li>本地化支持 - 多语言国际化</li>
              </ul>
            </a-col>
          </a-row>
        </a-typography-paragraph>

        <a-typography-title :level="3">主要特性</a-typography-title>
        <a-typography-paragraph>
          <ul>
            <li>完善的权限管理：基于 RBAC 的权限控制</li>
            <li>优雅的界面设计：遵循 Ant Design 设计规范，提供舒适的用户体验</li>
            <li>丰富的组件：内置大量实用组件，快速开发业务功能</li>
            <li>国际化支持：内置多语言支持，轻松切换不同语言</li>
            <li>主题定制：支持明暗主题切换，可自定义主题色</li>
            <li>安全性：采用 JWT 认证</li>
            <li>实时通信：基于 SignalR 的实时消息推送</li>
            <li>代码生成：自动化代码生成，提高开发效率</li>
            <li>审计追踪：完整的操作日志记录和审计功能</li>
          </ul>
        </a-typography-paragraph>

        <a-typography-title :level="3">版本信息</a-typography-title>
        <a-typography-paragraph>
          <a-descriptions :column="1" bordered size="small">
            <a-descriptions-item label="应用版本">{{ versionInfo.version }}</a-descriptions-item>
            <a-descriptions-item label="应用名称">{{ versionInfo.name }}</a-descriptions-item>
            <a-descriptions-item label="构建时间">{{ versionInfo.buildTime }}</a-descriptions-item>
            <a-descriptions-item label="前端框架">Vue 3.5.19 + TypeScript 5.9.2</a-descriptions-item>
            <a-descriptions-item label="后端框架">.NET 8.0 + ASP.NET Core</a-descriptions-item>
            <a-descriptions-item label="数据库">支持 SQL Server / MySQL / PostgreSQL</a-descriptions-item>
            <a-descriptions-item label="缓存">Redis 缓存支持</a-descriptions-item>
          </a-descriptions>
        </a-typography-paragraph>

        <a-typography-title :level="3">开发团队</a-typography-title>
        <a-typography-paragraph>
          本系统由 Lean365 团队开发维护，采用现代化的技术架构和最佳实践，致力于为企业提供高效、安全、易用的管理解决方案。
        </a-typography-paragraph>

        <a-typography-title :level="3">相关链接</a-typography-title>
        <a-space>
          <a-button type="primary" href="https://github.com/Lean365" target="_blank">
            <template #icon><github-outlined /></template>
            GitHub
          </a-button>
          <a-button type="default" href="https://lean365.cn" target="_blank">
            <template #icon><global-outlined /></template>
            官方网站
          </a-button>
        </a-space>
      </a-typography>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { getVersionInfo } from '@/utils/versionUtil'
import {
  GithubOutlined,
  GlobalOutlined
} from '@ant-design/icons-vue'

const versionInfo = computed(() => getVersionInfo())
</script>

<style lang="less" scoped>
.about-container {
  padding: 24px;
  max-width: 1200px;
  margin: 0 auto;
  
  .ant-card {
    margin-bottom: 24px;
  }
  
  .ant-typography {
    .ant-typography-title {
      margin-top: 24px;
      margin-bottom: 16px;
      
      &:first-child {
        margin-top: 0;
      }
    }
    
    .ant-typography-paragraph {
      margin-bottom: 16px;
      
      ul {
        margin-bottom: 16px;
        
        li {
          margin-bottom: 8px;
          
          ul {
            margin-top: 8px;
            margin-bottom: 0;
          }
        }
      }
    }
  }
  
  .ant-descriptions {
    margin: 16px 0;
  }
  
  .ant-space {
    margin-top: 16px;
  }
}
</style>

