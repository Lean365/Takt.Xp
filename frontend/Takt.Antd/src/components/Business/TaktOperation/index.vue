<!-- 
===================================================================
项目名称: Lean.Takt
文件名称: Operation/index.vue  
创建日期: 2024-03-20
描述: 通用操作按钮组件，提供常用的操作按钮，支持自定义按钮顺序
=================================================================== 
-->

<template>
  <div class="Takt-operation" :class="{ 'Takt-operation--vertical': direction === 'vertical' }">
    <!-- 动态渲染按钮，按照buttonOrder顺序排列 -->
    <template v-for="buttonConfig in orderedButtons" :key="buttonConfig.key">
      <!-- 保存按钮 -->
      <a-tooltip v-if="buttonConfig.key === 'save'" :title="t('common.actions.save')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleSave"
        class="Takt-btn-save"
      >
        <template #icon><save-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 查看按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'view'" :title="t('common.actions.view')">
      <a-button 
        :type="buttonType"
        :size="size"
        @click="handleView"
        class="Takt-btn-view"
      >
        <template #icon><eye-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 编辑按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'edit'" :title="t('common.actions.edit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleEdit"
        class="Takt-btn-edit"
      >
        <template #icon><edit-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 删除按钮 -->
    <a-popconfirm
        v-else-if="buttonConfig.key === 'delete'"
      :title="t('common.message.deleteConfirm')"
      @confirm="handleDelete"
    >
      <a-tooltip :title="t('common.actions.delete')">
        <a-button
          :type="buttonType"
          :size="size"
          danger
          class="Takt-btn-delete"
        >
          <template #icon><delete-outlined /></template>
        </a-button>
      </a-tooltip>
    </a-popconfirm>

      <!-- 复制按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'copy'" :title="t('common.actions.copy')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleCopy"
        class="Takt-btn-copy"
      >
        <template #icon><copy-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 克隆按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'clone'" :title="t('common.actions.clone')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleClone"
          class="Takt-btn-clone"
        >
          <template #icon><file-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 导入按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'import'" :title="t('common.actions.import')">
      <a-button
        :type="buttonType"
        :size="size"
          @click="handleImport"
          class="Takt-btn-import"
      >
          <template #icon><import-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 导出按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'export'" :title="t('common.actions.export')">
      <a-button
        :type="buttonType"
        :size="size"
          @click="handleExport"
          class="Takt-btn-export"
      >
          <template #icon><export-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 发布按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'publish'" :title="t('common.actions.publish')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePublish"
          class="Takt-btn-publish"
        >
          <template #icon><upload-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 审核按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'audit'" :title="t('common.actions.audit')">
      <a-button
        :type="buttonType"
        :size="size"
        @click="handleAudit"
        class="Takt-btn-audit"
      >
        <template #icon><audit-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 验证按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'validate'" :title="t('common.actions.validate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleValidate"
          class="Takt-btn-validate"
        >
          <template #icon><check-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 暂停按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'pause'" :title="t('common.actions.pause')">
      <a-button
        :type="buttonType"
        :size="size"
          @click="handlePause"
          class="Takt-btn-pause"
      >
        <template #icon><pause-circle-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 恢复按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'resume'" :title="t('common.actions.resume')">
      <a-button
        :type="buttonType"
        :size="size"
          @click="handleResume"
          class="Takt-btn-resume"
      >
          <template #icon><caret-right-outlined /></template>
      </a-button>
    </a-tooltip>

      <!-- 会计相关按钮 -->
      <!-- 记账按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'book'" :title="t('common.actions.book')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleBook"
          class="Takt-btn-book"
        >
          <template #icon><book-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 结账按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'closing'" :title="t('common.actions.closing')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleClosing"
          class="Takt-btn-closing"
        >
          <template #icon><check-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 对账按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'reconcile'" :title="t('common.actions.reconcile')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReconcile"
          class="Takt-btn-reconcile"
        >
          <template #icon><swap-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 支付按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'payment'" :title="t('common.actions.payment')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePayment"
          class="Takt-btn-payment"
        >
          <template #icon><credit-card-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 折旧按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'depreciation'" :title="t('common.actions.depreciation')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDepreciation"
          class="Takt-btn-depreciation"
        >
          <template #icon><line-chart-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 报销按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'reimburse'" :title="t('common.actions.reimburse')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReimburse"
          class="Takt-btn-reimburse"
        >
          <template #icon><money-collect-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 冲销按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'reversal'" :title="t('common.actions.reversal')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReversal"
          class="Takt-btn-reversal"
        >
          <template #icon><undo-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 计提按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'accrual'" :title="t('common.actions.accrual')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleAccrual"
          class="Takt-btn-accrual"
        >
          <template #icon><calculator-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 账期按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'period'" :title="t('common.actions.period')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePeriod"
          class="Takt-btn-period"
        >
          <template #icon><calendar-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 结转按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'carryforward'" :title="t('common.actions.carryforward')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleCarryforward"
          class="Takt-btn-carryforward"
        >
          <template #icon><forward-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 作废按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'cancel'" :title="t('common.actions.cancel')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleCancel"
          class="Takt-btn-cancel"
        >
          <template #icon><close-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 计费按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'billing'" :title="t('common.actions.billing')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleBilling"
          class="Takt-btn-billing"
        >
          <template #icon><dollar-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 认证相关按钮 -->
      <!-- 分配按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'allocate'" :title="t('common.actions.allocate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleAllocate"
          class="Takt-btn-allocate"
        >
          <template #icon><user-add-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 授权按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'authorize'" :title="t('common.actions.authorize')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleAuthorize"
          class="Takt-btn-authorize"
        >
          <template #icon><safety-certificate-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 修改密码按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'changepwd'" :title="t('common.actions.changepwd')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleChangepwd"
          class="Takt-btn-changepwd"
        >
          <template #icon><key-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 重置密码按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'resetpwd'" :title="t('common.actions.resetpwd')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleResetpwd"
          class="Takt-btn-resetpwd"
        >
          <template #icon><reload-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 重置按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'reset'" :title="t('common.actions.reset')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReset"
          class="Takt-btn-reset"
        >
          <template #icon><rest-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 代码生成相关按钮 -->
      <!-- 列管理按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'columns'" :title="t('common.actions.columns')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleColumns"
          class="Takt-btn-columns"
        >
          <template #icon><table-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 数据库按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'databases'" :title="t('common.actions.databases')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDatabases"
          class="Takt-btn-databases"
        >
          <template #icon><database-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 生成按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'generate'" :title="t('common.actions.generate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleGenerate"
          class="Takt-btn-generate"
        >
          <template #icon><thunderbolt-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 初始化按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'initialize'" :title="t('common.actions.initialize')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleInitialize"
          class="Takt-btn-initialize"
        >
          <template #icon><play-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 同步按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'sync'" :title="t('common.actions.sync')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSync"
          class="Takt-btn-sync"
        >
          <template #icon><sync-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 表管理按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'tables'" :title="t('common.actions.tables')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTables"
          class="Takt-btn-tables"
        >
          <template #icon><unordered-list-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 模板按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'template'" :title="t('common.actions.template')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTemplate"
          class="Takt-btn-template"
        >
          <template #icon><file-text-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 预览按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'preview'" :title="t('common.actions.preview')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePreview"
          class="Takt-btn-preview"
        >
          <template #icon><eye-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 打印按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'print'" :title="t('common.actions.print')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePrint"
          class="Takt-btn-print"
        >
          <template #icon><printer-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 社交互动按钮 -->
      <!-- 点赞按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'like'" :title="t('common.actions.like')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleLike"
          class="Takt-btn-like"
        >
          <template #icon><like-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消点赞按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unlike'" :title="t('common.actions.unlike')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnlike"
          class="Takt-btn-unlike"
        >
          <template #icon><dislike-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 收藏按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'favorite'" :title="t('common.actions.favorite')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleFavorite"
          class="Takt-btn-favorite"
        >
          <template #icon><heart-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消收藏按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unfavorite'" :title="t('common.actions.unfavorite')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnfavorite"
          class="Takt-btn-unfavorite"
        >
          <template #icon><heart-filled /></template>
        </a-button>
      </a-tooltip>

      <!-- 分享按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'share'" :title="t('common.actions.share')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleShare"
          class="Takt-btn-share"
        >
          <template #icon><share-alt-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消分享按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unshare'" :title="t('common.actions.unshare')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnshare"
          class="Takt-btn-unshare"
        >
          <template #icon><share-alt-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 评论按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'comment'" :title="t('common.actions.comment')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleComment"
          class="Takt-btn-comment"
        >
          <template #icon><message-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消评论按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'uncomment'" :title="t('common.actions.uncomment')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUncomment"
          class="Takt-btn-uncomment"
        >
          <template #icon><message-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 举报按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'flagging'" :title="t('common.actions.flagging')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleFlagging"
          class="Takt-btn-flagging"
        >
          <template #icon><flag-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消举报按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unflagging'" :title="t('common.actions.unflagging')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnflagging"
          class="Takt-btn-unflagging"
        >
          <template #icon><flag-filled /></template>
        </a-button>
      </a-tooltip>

      <!-- 关注按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'follow'" :title="t('common.actions.follow')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleFollow"
          class="Takt-btn-follow"
        >
          <template #icon><user-add-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 取消关注按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unfollow'" :title="t('common.actions.unfollow')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnfollow"
          class="Takt-btn-unfollow"
        >
          <template #icon><user-delete-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 工作流按钮 -->
      <!-- 加签按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'addsign'" :title="t('common.actions.addsign')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleAddsign"
          class="Takt-btn-addsign"
        >
          <template #icon><plus-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 减签按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'subsign'" :title="t('common.actions.subsign')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSubsign"
          class="Takt-btn-subsign"
        >
          <template #icon><minus-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 配置按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'config'" :title="t('common.actions.config')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleConfig"
          class="Takt-btn-config"
        >
          <template #icon><setting-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 委托按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'delegate'" :title="t('common.actions.delegate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDelegate"
          class="Takt-btn-delegate"
        >
          <template #icon><team-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 设计按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'design'" :title="t('common.actions.design')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDesign"
          class="Takt-btn-design"
        >
          <template #icon><edit-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 禁用按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'disable'" :title="t('common.actions.disable')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDisable"
          class="Takt-btn-disable"
        >
          <template #icon><stop-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 启用按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'enable'" :title="t('common.actions.enable')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleEnable"
          class="Takt-btn-enable"
        >
          <template #icon><play-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 引擎按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'engine'" :title="t('common.actions.engine')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleEngine"
          class="Takt-btn-engine"
        >
          <template #icon><thunderbolt-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 历史按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'history'" :title="t('common.actions.history')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleHistory"
          class="Takt-btn-history"
        >
          <template #icon><history-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 进度按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'progress'" :title="t('common.actions.progress')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleProgress"
          class="Takt-btn-progress"
        >
          <template #icon><bar-chart-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 启动按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'start'" :title="t('common.actions.start')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleStart"
          class="Takt-btn-start"
        >
          <template #icon><play-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 停止按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'stop'" :title="t('common.actions.stop')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleStop"
          class="Takt-btn-stop"
        >
          <template #icon><stop-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 提交按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'submit'" :title="t('common.actions.submit')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSubmit"
          class="Takt-btn-submit"
        >
          <template #icon><check-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 暂停按钮 (工作流) -->
      <a-tooltip v-else-if="buttonConfig.key === 'suspend'" :title="t('common.actions.suspend')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSuspend"
          class="Takt-btn-suspend"
        >
          <template #icon><pause-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 终止按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'terminate'" :title="t('common.actions.terminate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTerminate"
          class="Takt-btn-terminate"
        >
          <template #icon><close-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 转办按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'transfer'" :title="t('common.actions.transfer')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTransfer"
          class="Takt-btn-transfer"
        >
          <template #icon><swap-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 版本按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'version'" :title="t('common.actions.version')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleVersion"
          class="Takt-btn-version"
        >
          <template #icon><tag-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 撤回按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'withdraw'" :title="t('common.actions.withdraw')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleWithdraw"
          class="Takt-btn-withdraw"
        >
          <template #icon><rollback-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 退回按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'return'" :title="t('common.actions.return')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReturn"
          class="Takt-btn-return"
        >
          <template #icon><arrow-left-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 催办按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'urge'" :title="t('common.actions.urge')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUrge"
          class="Takt-btn-urge"
        >
          <template #icon><clock-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 表单按钮 -->
      <!-- 数据按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'data'" :title="t('common.actions.data')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleData"
          class="Takt-btn-data"
        >
          <template #icon><database-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 数据源按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'datasource'" :title="t('common.actions.datasource')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDatasource"
          class="Takt-btn-datasource"
        >
          <template #icon><link-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 字段按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'field'" :title="t('common.actions.field')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleField"
          class="Takt-btn-field"
        >
          <template #icon><form-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 权限按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'permission'" :title="t('common.actions.permission')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handlePermission"
          class="Takt-btn-permission"
        >
          <template #icon><safety-certificate-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 主题按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'theme'" :title="t('common.actions.theme')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTheme"
          class="Takt-btn-theme"
        >
          <template #icon><skin-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 文件操作按钮 -->
      <!-- 归档按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'archive'" :title="t('common.actions.archive')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleArchive"
          class="Takt-btn-archive"
        >
          <template #icon><inbox-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 销毁按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'destroy'" :title="t('common.actions.destroy')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDestroy"
          class="Takt-btn-destroy"
        >
          <template #icon><delete-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 下载按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'download'" :title="t('common.actions.download')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDownload"
          class="Takt-btn-download"
        >
          <template #icon><download-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 上传按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'upload'" :title="t('common.actions.upload')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUpload"
          class="Takt-btn-upload"
        >
          <template #icon><upload-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 系统操作按钮 -->
      <!-- 清空按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'empty'" :title="t('common.actions.empty')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleEmpty"
          class="Takt-btn-empty"
        >
          <template #icon><clear-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 刷新按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'refresh'" :title="t('common.actions.refresh')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleRefresh"
          class="Takt-btn-refresh"
        >
          <template #icon><reload-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 重启按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'restart'" :title="t('common.actions.restart')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleRestart"
          class="Takt-btn-restart"
        >
          <template #icon><redo-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 运行按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'run'" :title="t('common.actions.run')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleRun"
          class="Takt-btn-run"
        >
          <template #icon><play-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 截断按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'truncate'" :title="t('common.actions.truncate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleTruncate"
          class="Takt-btn-truncate"
        >
          <template #icon><scissor-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 其他按钮 -->
      <!-- 计算按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'calculate'" :title="t('common.actions.calculate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleCalculate"
          class="Takt-btn-calculate"
        >
          <template #icon><calculator-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 清理按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'clean'" :title="t('common.actions.clean')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleClean"
          class="Takt-btn-clean"
        >
          <template #icon><clear-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 流通按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'circulate'" :title="t('common.actions.circulate')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleCirculate"
          class="Takt-btn-circulate"
        >
          <template #icon><swap-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 确认按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'confirm'" :title="t('common.actions.confirm')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleConfirm"
          class="Takt-btn-confirm"
        >
          <template #icon><check-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 草稿按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'draft'" :title="t('common.actions.draft')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDraft"
          class="Takt-btn-draft"
        >
          <template #icon><file-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 删除草稿按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'deletedraft'" :title="t('common.actions.deletedraft')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDeletedraft"
          class="Takt-btn-deletedraft"
        >
          <template #icon><delete-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 详情按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'detail'" :title="t('common.actions.detail')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleDetail"
          class="Takt-btn-detail"
        >
          <template #icon><info-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 转发按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'forward'" :title="t('common.actions.forward')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleForward"
          class="Takt-btn-forward"
        >
          <template #icon><forward-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 列表按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'list'" :title="t('common.actions.list')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleList"
          class="Takt-btn-list"
        >
          <template #icon><unordered-list-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 管理按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'manage'" :title="t('common.actions.manage')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleManage"
          class="Takt-btn-manage"
        >
          <template #icon><setting-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 读取按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'read'" :title="t('common.actions.read')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleRead"
          class="Takt-btn-read"
        >
          <template #icon><read-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 回复按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'reply'" :title="t('common.actions.reply')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleReply"
          class="Takt-btn-reply"
        >
          <template #icon><message-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 撤销按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'revoke'" :title="t('common.actions.revoke')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleRevoke"
          class="Takt-btn-revoke"
        >
          <template #icon><undo-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 发送按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'send'" :title="t('common.actions.send')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSend"
          class="Takt-btn-send"
        >
          <template #icon><send-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 签名按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'sign'" :title="t('common.actions.sign')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleSign"
          class="Takt-btn-sign"
        >
          <template #icon><edit-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 未读按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'unread'" :title="t('common.actions.unread')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUnread"
          class="Takt-btn-unread"
        >
          <template #icon><mail-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 缺失的按钮 -->
      <!-- 审批按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'approve'" :title="t('common.actions.approve')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleApprove"
          class="Takt-btn-approve"
        >
          <template #icon><check-circle-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 创建按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'create'" :title="t('common.actions.create')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleCreate"
          class="Takt-btn-create"
        >
          <template #icon><plus-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 查询按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'query'" :title="t('common.actions.query')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleQuery"
          class="Takt-btn-query"
        >
          <template #icon><search-outlined /></template>
        </a-button>
      </a-tooltip>

      <!-- 更新按钮 -->
      <a-tooltip v-else-if="buttonConfig.key === 'update'" :title="t('common.actions.update')">
        <a-button
          :type="buttonType"
          :size="size"
          @click="handleUpdate"
          class="Takt-btn-update"
        >
          <template #icon><edit-outlined /></template>
        </a-button>
      </a-tooltip>

    </template>

    <!-- 支持自定义按钮插槽 -->
    <slot name="extra"></slot>
  </div>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/userStore'
import {
  SaveOutlined,
  EyeOutlined,
  EditOutlined,
  DeleteOutlined,
  CopyOutlined,
  FileOutlined,
  ImportOutlined,
  ExportOutlined,
  UploadOutlined,
  AuditOutlined,
  CheckCircleOutlined,
  PauseCircleOutlined,
  CaretRightOutlined,
  // 会计相关图标
  BookOutlined,
  SwapOutlined,
  CreditCardOutlined,
  LineChartOutlined,
  MoneyCollectOutlined,
  UndoOutlined,
  CalculatorOutlined,
  CalendarOutlined,
  ForwardOutlined,
  CloseCircleOutlined,
  DollarOutlined,
  // 认证相关图标
  UserAddOutlined,
  SafetyCertificateOutlined,
  KeyOutlined,
  ReloadOutlined,
  RestOutlined,
  // 代码生成相关图标
  TableOutlined,
  DatabaseOutlined,
  ThunderboltOutlined,
  PlayCircleOutlined,
  SyncOutlined,
  UnorderedListOutlined,
  FileTextOutlined,
  PrinterOutlined,
  // 社交互动相关图标
  LikeOutlined,
  DislikeOutlined,
  HeartOutlined,
  HeartFilled,
  ShareAltOutlined,
  MessageOutlined,
  FlagOutlined,
  FlagFilled,
  UserDeleteOutlined,
  // 工作流相关图标
  PlusOutlined,
  MinusOutlined,
  SettingOutlined,
  TeamOutlined,
  StopOutlined,
  HistoryOutlined,
  BarChartOutlined,
  CheckOutlined,
  TagOutlined,
  RollbackOutlined,
  ArrowLeftOutlined,
  ClockCircleOutlined,
  // 表单和其他图标
  LinkOutlined,
  FormOutlined,
  SkinOutlined,
  InboxOutlined,
  DownloadOutlined,
  ClearOutlined,
  RedoOutlined,
  ScissorOutlined,
  InfoCircleOutlined,
  ReadOutlined,
  SendOutlined,
  MailOutlined,
  // 缺失按钮的图标
  SearchOutlined
} from '@ant-design/icons-vue'

const { t } = useI18n()

// === 类型定义 ===
interface Props {
  record?: any
  // 按钮显示顺序配置
  buttonOrder?: string[]
  // 基础操作按钮
  showSave?: boolean
  savePermission?: string[]
  showView?: boolean
  viewPermission?: string[]
  showEdit?: boolean
  editPermission?: string[]
  showDelete?: boolean
  deletePermission?: string[]
  showCopy?: boolean
  copyPermission?: string[]
  showClone?: boolean
  clonePermission?: string[]
  showImport?: boolean
  importPermission?: string[]
  showExport?: boolean
  exportPermission?: string[]
  showPublish?: boolean
  publishPermission?: string[]
  showAudit?: boolean
  auditPermission?: string[]
  showValidate?: boolean
  validatePermission?: string[]
  showPause?: boolean
  pausePermission?: string[]
  showResume?: boolean
  resumePermission?: string[]
  // 会计相关按钮
  showBook?: boolean
  bookPermission?: string[]
  showClosing?: boolean
  closingPermission?: string[]
  showReconcile?: boolean
  reconcilePermission?: string[]
  showPayment?: boolean
  paymentPermission?: string[]
  showDepreciation?: boolean
  depreciationPermission?: string[]
  showReimburse?: boolean
  reimbursePermission?: string[]
  showReversal?: boolean
  reversalPermission?: string[]
  showAccrual?: boolean
  accrualPermission?: string[]
  showPeriod?: boolean
  periodPermission?: string[]
  showCarryforward?: boolean
  carryforwardPermission?: string[]
  showCancel?: boolean
  cancelPermission?: string[]
  showBilling?: boolean
  billingPermission?: string[]
  // 认证相关按钮
  showAllocate?: boolean
  allocatePermission?: string[]
  showAuthorize?: boolean
  authorizePermission?: string[]
  showChangepwd?: boolean
  changepwdPermission?: string[]
  showResetpwd?: boolean
  resetpwdPermission?: string[]
  showReset?: boolean
  resetPermission?: string[]
  // 代码生成相关按钮
  showColumns?: boolean
  columnsPermission?: string[]
  showDatabases?: boolean
  databasesPermission?: string[]
  showGenerate?: boolean
  generatePermission?: string[]
  showInitialize?: boolean
  initializePermission?: string[]
  showSync?: boolean
  syncPermission?: string[]
  showTables?: boolean
  tablesPermission?: string[]
  showTemplate?: boolean
  templatePermission?: string[]
  showPreview?: boolean
  previewPermission?: string[]
  showPrint?: boolean
  printPermission?: string[]
  // 社交互动按钮
  showLike?: boolean
  likePermission?: string[]
  showUnlike?: boolean
  unlikePermission?: string[]
  showFavorite?: boolean
  favoritePermission?: string[]
  showUnfavorite?: boolean
  unfavoritePermission?: string[]
  showShare?: boolean
  sharePermission?: string[]
  showUnshare?: boolean
  unsharePermission?: string[]
  showComment?: boolean
  commentPermission?: string[]
  showUncomment?: boolean
  uncommentPermission?: string[]
  showFlagging?: boolean
  flaggingPermission?: string[]
  showUnflagging?: boolean
  unflaggingPermission?: string[]
  showFollow?: boolean
  followPermission?: string[]
  showUnfollow?: boolean
  unfollowPermission?: string[]
  // 工作流按钮
  showAddsign?: boolean
  addsignPermission?: string[]
  showSubsign?: boolean
  subsignPermission?: string[]
  showConfig?: boolean
  configPermission?: string[]
  showDelegate?: boolean
  delegatePermission?: string[]
  showDesign?: boolean
  designPermission?: string[]
  showDisable?: boolean
  disablePermission?: string[]
  showEnable?: boolean
  enablePermission?: string[]
  showEngine?: boolean
  enginePermission?: string[]
  showHistory?: boolean
  historyPermission?: string[]
  showProgress?: boolean
  progressPermission?: string[]
  showStart?: boolean
  startPermission?: string[]
  showStop?: boolean
  stopPermission?: string[]
  showSubmit?: boolean
  submitPermission?: string[]
  showSuspend?: boolean
  suspendPermission?: string[]
  showTerminate?: boolean
  terminatePermission?: string[]
  showTransfer?: boolean
  transferPermission?: string[]
  showVersion?: boolean
  versionPermission?: string[]
  showWithdraw?: boolean
  withdrawPermission?: string[]
  showReturn?: boolean
  returnPermission?: string[]
  showUrge?: boolean
  urgePermission?: string[]
  // 表单按钮
  showData?: boolean
  dataPermission?: string[]
  showDatasource?: boolean
  datasourcePermission?: string[]
  showField?: boolean
  fieldPermission?: string[]
  showPermission?: boolean
  permissionPermission?: string[]
  showTheme?: boolean
  themePermission?: string[]
  // 文件操作按钮
  showArchive?: boolean
  archivePermission?: string[]
  showDestroy?: boolean
  destroyPermission?: string[]
  showDownload?: boolean
  downloadPermission?: string[]
  showUpload?: boolean
  uploadPermission?: string[]
  // 系统操作按钮
  showEmpty?: boolean
  emptyPermission?: string[]
  showRefresh?: boolean
  refreshPermission?: string[]
  showRestart?: boolean
  restartPermission?: string[]
  showRun?: boolean
  runPermission?: string[]
  showTruncate?: boolean
  truncatePermission?: string[]
  // 其他按钮
  showCalculate?: boolean
  calculatePermission?: string[]
  showClean?: boolean
  cleanPermission?: string[]
  showCirculate?: boolean
  circulatePermission?: string[]
  showConfirm?: boolean
  confirmPermission?: string[]
  showDraft?: boolean
  draftPermission?: string[]
  showDeletedraft?: boolean
  deletedraftPermission?: string[]
  showDetail?: boolean
  detailPermission?: string[]
  showForward?: boolean
  forwardPermission?: string[]
  showList?: boolean
  listPermission?: string[]
  showManage?: boolean
  managePermission?: string[]
  showRead?: boolean
  readPermission?: string[]
  showReply?: boolean
  replyPermission?: string[]
  showRevoke?: boolean
  revokePermission?: string[]
  showSend?: boolean
  sendPermission?: string[]
  showSign?: boolean
  signPermission?: string[]
  showUnread?: boolean
  unreadPermission?: string[]
  // 缺失的按钮
  showApprove?: boolean
  approvePermission?: string[]
  showCreate?: boolean
  createPermission?: string[]
  showQuery?: boolean
  queryPermission?: string[]
  showUpdate?: boolean
  updatePermission?: string[]
  // 通用属性
  buttonType?: 'link' | 'text' | 'default' | 'primary' | 'dashed'
  size?: 'small' | 'middle' | 'large'
  showText?: boolean
  direction?: 'horizontal' | 'vertical'
}

// === 属性定义 ===
const props = withDefaults(defineProps<Props>(), {
  record: undefined,
  buttonOrder: () => [],
  showSave: false,
  savePermission: () => [],
  showView: false,
  viewPermission: () => [],
  showEdit: false,
  editPermission: () => [],
  showDelete: false,
  deletePermission: () => [],
  showCopy: false,
  copyPermission: () => [],
  showClone: false,
  clonePermission: () => [],
  showImport: false,
  importPermission: () => [],
  showExport: false,
  exportPermission: () => [],
  showPublish: false,
  publishPermission: () => [],
  showAudit: false,
  auditPermission: () => [],
  showValidate: false,
  validatePermission: () => [],
  showPause: false,
  pausePermission: () => [],
  showResume: false,
  resumePermission: () => [],
  // 会计相关按钮默认值
  showBook: false,
  bookPermission: () => [],
  showClosing: false,
  closingPermission: () => [],
  showReconcile: false,
  reconcilePermission: () => [],
  showPayment: false,
  paymentPermission: () => [],
  showDepreciation: false,
  depreciationPermission: () => [],
  showReimburse: false,
  reimbursePermission: () => [],
  showReversal: false,
  reversalPermission: () => [],
  showAccrual: false,
  accrualPermission: () => [],
  showPeriod: false,
  periodPermission: () => [],
  showCarryforward: false,
  carryforwardPermission: () => [],
  showCancel: false,
  cancelPermission: () => [],
  showBilling: false,
  billingPermission: () => [],
  // 认证相关按钮默认值
  showAllocate: false,
  allocatePermission: () => [],
  showAuthorize: false,
  authorizePermission: () => [],
  showChangepwd: false,
  changepwdPermission: () => [],
  showResetpwd: false,
  resetpwdPermission: () => [],
  showReset: false,
  resetPermission: () => [],
  // 代码生成相关按钮默认值
  showColumns: false,
  columnsPermission: () => [],
  showDatabases: false,
  databasesPermission: () => [],
  showGenerate: false,
  generatePermission: () => [],
  showInitialize: false,
  initializePermission: () => [],
  showSync: false,
  syncPermission: () => [],
  showTables: false,
  tablesPermission: () => [],
  showTemplate: false,
  templatePermission: () => [],
  showPreview: false,
  previewPermission: () => [],
  showPrint: false,
  printPermission: () => [],
  // 社交互动按钮默认值
  showLike: false,
  likePermission: () => [],
  showUnlike: false,
  unlikePermission: () => [],
  showFavorite: false,
  favoritePermission: () => [],
  showUnfavorite: false,
  unfavoritePermission: () => [],
  showShare: false,
  sharePermission: () => [],
  showUnshare: false,
  unsharePermission: () => [],
  showComment: false,
  commentPermission: () => [],
  showUncomment: false,
  uncommentPermission: () => [],
  showFlagging: false,
  flaggingPermission: () => [],
  showUnflagging: false,
  unflaggingPermission: () => [],
  showFollow: false,
  followPermission: () => [],
  showUnfollow: false,
  unfollowPermission: () => [],
  // 工作流按钮默认值
  showAddsign: false,
  addsignPermission: () => [],
  showSubsign: false,
  subsignPermission: () => [],
  showConfig: false,
  configPermission: () => [],
  showDelegate: false,
  delegatePermission: () => [],
  showDesign: false,
  designPermission: () => [],
  showDisable: false,
  disablePermission: () => [],
  showEnable: false,
  enablePermission: () => [],
  showEngine: false,
  enginePermission: () => [],
  showHistory: false,
  historyPermission: () => [],
  showProgress: false,
  progressPermission: () => [],
  showStart: false,
  startPermission: () => [],
  showStop: false,
  stopPermission: () => [],
  showSubmit: false,
  submitPermission: () => [],
  showSuspend: false,
  suspendPermission: () => [],
  showTerminate: false,
  terminatePermission: () => [],
  showTransfer: false,
  transferPermission: () => [],
  showVersion: false,
  versionPermission: () => [],
  showWithdraw: false,
  withdrawPermission: () => [],
  showReturn: false,
  returnPermission: () => [],
  showUrge: false,
  urgePermission: () => [],
  // 表单按钮默认值
  showData: false,
  dataPermission: () => [],
  showDatasource: false,
  datasourcePermission: () => [],
  showField: false,
  fieldPermission: () => [],
  showPermission: false,
  permissionPermission: () => [],
  showTheme: false,
  themePermission: () => [],
  // 文件操作按钮默认值
  showArchive: false,
  archivePermission: () => [],
  showDestroy: false,
  destroyPermission: () => [],
  showDownload: false,
  downloadPermission: () => [],
  showUpload: false,
  uploadPermission: () => [],
  // 系统操作按钮默认值
  showEmpty: false,
  emptyPermission: () => [],
  showRefresh: false,
  refreshPermission: () => [],
  showRestart: false,
  restartPermission: () => [],
  showRun: false,
  runPermission: () => [],
  showTruncate: false,
  truncatePermission: () => [],
  // 其他按钮默认值
  showCalculate: false,
  calculatePermission: () => [],
  showClean: false,
  cleanPermission: () => [],
  showCirculate: false,
  circulatePermission: () => [],
  showConfirm: false,
  confirmPermission: () => [],
  showDraft: false,
  draftPermission: () => [],
  showDeletedraft: false,
  deletedraftPermission: () => [],
  showDetail: false,
  detailPermission: () => [],
  showForward: false,
  forwardPermission: () => [],
  showList: false,
  listPermission: () => [],
  showManage: false,
  managePermission: () => [],
  showRead: false,
  readPermission: () => [],
  showReply: false,
  replyPermission: () => [],
  showRevoke: false,
  revokePermission: () => [],
  showSend: false,
  sendPermission: () => [],
  showSign: false,
  signPermission: () => [],
  showUnread: false,
  unreadPermission: () => [],
  // 缺失按钮的默认值
  showApprove: false,
  approvePermission: () => [],
  showCreate: false,
  createPermission: () => [],
  showQuery: false,
  queryPermission: () => [],
  showUpdate: false,
  updatePermission: () => [],
  buttonType: 'link',
  size: 'middle',
  showText: true,
  direction: 'horizontal'
})

// === 事件定义 ===
const emit = defineEmits([
  'save',
  'view',
  'edit',
  'delete',
  'copy',
  'clone',
  'import',
  'export',
  'publish',
  'audit',
  'validate',
  'pause',
  'resume',
  // 会计相关事件
  'book',
  'closing',
  'reconcile',
  'payment',
  'depreciation',
  'reimburse',
  'reversal',
  'accrual',
  'period',
  'carryforward',
  'cancel',
  'billing',
  // 认证相关事件
  'allocate',
  'authorize',
  'changepwd',
  'resetpwd',
  'reset',
  // 代码生成相关事件
  'columns',
  'databases',
  'generate',
  'initialize',
  'sync',
  'tables',
  'template',
  'preview',
  'print',
  // 社交互动事件
  'like',
  'unlike',
  'favorite',
  'unfavorite',
  'share',
  'unshare',
  'comment',
  'uncomment',
  'flagging',
  'unflagging',
  'follow',
  'unfollow',
  // 工作流事件
  'addsign',
  'subsign',
  'config',
  'delegate',
  'design',
  'disable',
  'enable',
  'engine',
  'history',
  'progress',
  'start',
  'stop',
  'submit',
  'suspend',
  'terminate',
  'transfer',
  'version',
  'withdraw',
  'return',
  'urge',
  // 表单事件
  'data',
  'datasource',
  'field',
  'permission',
  'theme',
  // 文件操作事件
  'archive',
  'destroy',
  'download',
  'upload',
  // 系统操作事件
  'empty',
  'refresh',
  'restart',
  'run',
  'truncate',
  // 其他事件
  'calculate',
  'clean',
  'circulate',
  'confirm',
  'draft',
  'deletedraft',
  'detail',
  'forward',
  'list',
  'manage',
  'read',
  'reply',
  'revoke',
  'send',
  'sign',
  'unread',
  // 缺失按钮的事件
  'approve',
  'create',
  'query',
  'update'
])

// === 权限验证方法 ===
const userStore = useUserStore()

// === 计算属性：生成有序按钮配置 ===
const orderedButtons = computed(() => {
  // 默认按钮顺序（如果没有指定buttonOrder）
  const defaultOrder = [
    'edit', 'view', 'delete', 'clone', 'publish', 'audit', 'validate', 'pause', 'resume'
  ]
  
  // 使用指定的顺序，或者默认顺序
  const order = props.buttonOrder.length > 0 ? props.buttonOrder : defaultOrder
  
  // 生成按钮配置数组
  const buttonConfigs = order.map(key => {
    const showMap = {
      save: showSaveButton.value,
      view: showViewButton.value,
      edit: showEditButton.value,
      delete: showDeleteButton.value,
      copy: showCopyButton.value,
      clone: showCloneButton.value,
      import: showImportButton.value,
      export: showExportButton.value,
      publish: showPublishButton.value,
      audit: showAuditButton.value,
      validate: showValidateButton.value,
      pause: showPauseButton.value,
      resume: showResumeButton.value,
      // 会计相关按钮
      book: showBookButton.value,
      closing: showClosingButton.value,
      reconcile: showReconcileButton.value,
      payment: showPaymentButton.value,
      depreciation: showDepreciationButton.value,
      reimburse: showReimburseButton.value,
      reversal: showReversalButton.value,
      accrual: showAccrualButton.value,
      period: showPeriodButton.value,
      carryforward: showCarryforwardButton.value,
      cancel: showCancelButton.value,
      billing: showBillingButton.value,
      // 认证相关按钮
      allocate: showAllocateButton.value,
      authorize: showAuthorizeButton.value,
      changepwd: showChangepwdButton.value,
      resetpwd: showResetpwdButton.value,
      reset: showResetButton.value,
      // 代码生成相关按钮
      columns: showColumnsButton.value,
      databases: showDatabasesButton.value,
      generate: showGenerateButton.value,
      initialize: showInitializeButton.value,
      sync: showSyncButton.value,
      tables: showTablesButton.value,
      template: showTemplateButton.value,
      preview: showPreviewButton.value,
      print: showPrintButton.value,
      // 社交互动按钮
      like: showLikeButton.value,
      unlike: showUnlikeButton.value,
      favorite: showFavoriteButton.value,
      unfavorite: showUnfavoriteButton.value,
      share: showShareButton.value,
      unshare: showUnshareButton.value,
      comment: showCommentButton.value,
      uncomment: showUncommentButton.value,
      flagging: showFlaggingButton.value,
      unflagging: showUnflaggingButton.value,
      follow: showFollowButton.value,
      unfollow: showUnfollowButton.value,
      // 工作流按钮
      addsign: showAddsignButton.value,
      subsign: showSubsignButton.value,
      config: showConfigButton.value,
      delegate: showDelegateButton.value,
      design: showDesignButton.value,
      disable: showDisableButton.value,
      enable: showEnableButton.value,
      engine: showEngineButton.value,
      history: showHistoryButton.value,
      progress: showProgressButton.value,
      start: showStartButton.value,
      stop: showStopButton.value,
      submit: showSubmitButton.value,
      suspend: showSuspendButton.value,
      terminate: showTerminateButton.value,
      transfer: showTransferButton.value,
      version: showVersionButton.value,
      withdraw: showWithdrawButton.value,
      return: showReturnButton.value,
      urge: showUrgeButton.value,
      // 表单按钮
      data: showDataButton.value,
      datasource: showDatasourceButton.value,
      field: showFieldButton.value,
      permission: showPermissionButton.value,
      theme: showThemeButton.value,
      // 文件操作按钮
      archive: showArchiveButton.value,
      destroy: showDestroyButton.value,
      download: showDownloadButton.value,
      upload: showUploadButton.value,
      // 系统操作按钮
      empty: showEmptyButton.value,
      refresh: showRefreshButton.value,
      restart: showRestartButton.value,
      run: showRunButton.value,
      truncate: showTruncateButton.value,
      // 其他按钮
      calculate: showCalculateButton.value,
      clean: showCleanButton.value,
      circulate: showCirculateButton.value,
      confirm: showConfirmButton.value,
      draft: showDraftButton.value,
      deletedraft: showDeletedraftButton.value,
      detail: showDetailButton.value,
      forward: showForwardButton.value,
      list: showListButton.value,
      manage: showManageButton.value,
      read: showReadButton.value,
      reply: showReplyButton.value,
      revoke: showRevokeButton.value,
      send: showSendButton.value,
      sign: showSignButton.value,
      unread: showUnreadButton.value,
      // 缺失按钮
      approve: showApproveButton.value,
      create: showCreateButton.value,
      query: showQueryButton.value,
      update: showUpdateButton.value
    }
    
    return {
      key,
      show: showMap[key as keyof typeof showMap] || false
    }
  })
  
  // 只返回需要显示的按钮
  return buttonConfigs.filter(config => config.show)
})

// === 计算属性：根据权限判断是否显示按钮 ===
const showSaveButton = computed(() => {
  if (!props.showSave) return false
  if (!props.savePermission?.length) return false
  return props.savePermission.some(permission => userStore.permissions.includes(permission))
})

const showViewButton = computed(() => {
  if (!props.showView) return false
  if (!props.viewPermission?.length) return false
  return props.viewPermission.some(permission => userStore.permissions.includes(permission))
})

const showEditButton = computed(() => {
  if (!props.showEdit) return false
  if (!props.editPermission?.length) return false
  return props.editPermission.some(permission => userStore.permissions.includes(permission))
})

const showDeleteButton = computed(() => {
  if (!props.showDelete) return false
  if (!props.deletePermission?.length) return false
  return props.deletePermission.some(permission => userStore.permissions.includes(permission))
})

const showCopyButton = computed(() => {
  if (!props.showCopy) return false
  if (!props.copyPermission?.length) return false
  return props.copyPermission.some(permission => userStore.permissions.includes(permission))
})

const showCloneButton = computed(() => {
  if (!props.showClone) return false
  if (!props.clonePermission?.length) return false
  return props.clonePermission.some(permission => userStore.permissions.includes(permission))
})

const showImportButton = computed(() => {
  if (!props.showImport) return false
  if (!props.importPermission?.length) return false
  return props.importPermission.some(permission => userStore.permissions.includes(permission))
})

const showExportButton = computed(() => {
  if (!props.showExport) return false
  if (!props.exportPermission?.length) return false
  return props.exportPermission.some(permission => userStore.permissions.includes(permission))
})

const showPublishButton = computed(() => {
  if (!props.showPublish) return false
  if (!props.publishPermission?.length) return false
  return props.publishPermission.some(permission => userStore.permissions.includes(permission))
})

const showAuditButton = computed(() => {
  if (!props.showAudit) return false
  if (!props.auditPermission?.length) return false
  return props.auditPermission.some(permission => userStore.permissions.includes(permission))
})

const showValidateButton = computed(() => {
  if (!props.showValidate) return false
  if (!props.validatePermission?.length) return false
  return props.validatePermission.some(permission => userStore.permissions.includes(permission))
})

const showPauseButton = computed(() => {
  if (!props.showPause) return false
  if (!props.pausePermission?.length) return false
  return props.pausePermission.some(permission => userStore.permissions.includes(permission))
})

const showResumeButton = computed(() => {
  if (!props.showResume) return false
  if (!props.resumePermission?.length) return false
  return props.resumePermission.some(permission => userStore.permissions.includes(permission))
})

// 会计相关按钮权限验证
const showBookButton = computed(() => {
  if (!props.showBook) return false
  if (!props.bookPermission?.length) return false
  return props.bookPermission.some(permission => userStore.permissions.includes(permission))
})

const showClosingButton = computed(() => {
  if (!props.showClosing) return false
  if (!props.closingPermission?.length) return false
  return props.closingPermission.some(permission => userStore.permissions.includes(permission))
})

const showReconcileButton = computed(() => {
  if (!props.showReconcile) return false
  if (!props.reconcilePermission?.length) return false
  return props.reconcilePermission.some(permission => userStore.permissions.includes(permission))
})

const showPaymentButton = computed(() => {
  if (!props.showPayment) return false
  if (!props.paymentPermission?.length) return false
  return props.paymentPermission.some(permission => userStore.permissions.includes(permission))
})

const showDepreciationButton = computed(() => {
  if (!props.showDepreciation) return false
  if (!props.depreciationPermission?.length) return false
  return props.depreciationPermission.some(permission => userStore.permissions.includes(permission))
})

const showReimburseButton = computed(() => {
  if (!props.showReimburse) return false
  if (!props.reimbursePermission?.length) return false
  return props.reimbursePermission.some(permission => userStore.permissions.includes(permission))
})

const showReversalButton = computed(() => {
  if (!props.showReversal) return false
  if (!props.reversalPermission?.length) return false
  return props.reversalPermission.some(permission => userStore.permissions.includes(permission))
})

const showAccrualButton = computed(() => {
  if (!props.showAccrual) return false
  if (!props.accrualPermission?.length) return false
  return props.accrualPermission.some(permission => userStore.permissions.includes(permission))
})

const showPeriodButton = computed(() => {
  if (!props.showPeriod) return false
  if (!props.periodPermission?.length) return false
  return props.periodPermission.some(permission => userStore.permissions.includes(permission))
})

const showCarryforwardButton = computed(() => {
  if (!props.showCarryforward) return false
  if (!props.carryforwardPermission?.length) return false
  return props.carryforwardPermission.some(permission => userStore.permissions.includes(permission))
})

const showCancelButton = computed(() => {
  if (!props.showCancel) return false
  if (!props.cancelPermission?.length) return false
  return props.cancelPermission.some(permission => userStore.permissions.includes(permission))
})

const showBillingButton = computed(() => {
  if (!props.showBilling) return false
  if (!props.billingPermission?.length) return false
  return props.billingPermission.some(permission => userStore.permissions.includes(permission))
})

// 认证相关按钮权限验证
const showAllocateButton = computed(() => {
  if (!props.showAllocate) return false
  if (!props.allocatePermission?.length) return false
  return props.allocatePermission.some(permission => userStore.permissions.includes(permission))
})

const showAuthorizeButton = computed(() => {
  if (!props.showAuthorize) return false
  if (!props.authorizePermission?.length) return false
  return props.authorizePermission.some(permission => userStore.permissions.includes(permission))
})

const showChangepwdButton = computed(() => {
  if (!props.showChangepwd) return false
  if (!props.changepwdPermission?.length) return false
  return props.changepwdPermission.some(permission => userStore.permissions.includes(permission))
})

const showResetpwdButton = computed(() => {
  if (!props.showResetpwd) return false
  if (!props.resetpwdPermission?.length) return false
  return props.resetpwdPermission.some(permission => userStore.permissions.includes(permission))
})

const showResetButton = computed(() => {
  if (!props.showReset) return false
  if (!props.resetPermission?.length) return false
  return props.resetPermission.some(permission => userStore.permissions.includes(permission))
})

// 代码生成相关按钮权限验证
const showColumnsButton = computed(() => {
  if (!props.showColumns) return false
  if (!props.columnsPermission?.length) return false
  return props.columnsPermission.some(permission => userStore.permissions.includes(permission))
})

const showDatabasesButton = computed(() => {
  if (!props.showDatabases) return false
  if (!props.databasesPermission?.length) return false
  return props.databasesPermission.some(permission => userStore.permissions.includes(permission))
})

const showGenerateButton = computed(() => {
  if (!props.showGenerate) return false
  if (!props.generatePermission?.length) return false
  return props.generatePermission.some(permission => userStore.permissions.includes(permission))
})

const showInitializeButton = computed(() => {
  if (!props.showInitialize) return false
  if (!props.initializePermission?.length) return false
  return props.initializePermission.some(permission => userStore.permissions.includes(permission))
})

const showSyncButton = computed(() => {
  if (!props.showSync) return false
  if (!props.syncPermission?.length) return false
  return props.syncPermission.some(permission => userStore.permissions.includes(permission))
})

const showTablesButton = computed(() => {
  if (!props.showTables) return false
  if (!props.tablesPermission?.length) return false
  return props.tablesPermission.some(permission => userStore.permissions.includes(permission))
})

const showTemplateButton = computed(() => {
  if (!props.showTemplate) return false
  if (!props.templatePermission?.length) return false
  return props.templatePermission.some(permission => userStore.permissions.includes(permission))
})

const showPreviewButton = computed(() => {
  if (!props.showPreview) return false
  if (!props.previewPermission?.length) return false
  return props.previewPermission.some(permission => userStore.permissions.includes(permission))
})

const showPrintButton = computed(() => {
  if (!props.showPrint) return false
  if (!props.printPermission?.length) return false
  return props.printPermission.some(permission => userStore.permissions.includes(permission))
})

// 社交互动按钮权限验证
const showLikeButton = computed(() => {
  if (!props.showLike) return false
  if (!props.likePermission?.length) return false
  return props.likePermission.some(permission => userStore.permissions.includes(permission))
})

const showUnlikeButton = computed(() => {
  if (!props.showUnlike) return false
  if (!props.unlikePermission?.length) return false
  return props.unlikePermission.some(permission => userStore.permissions.includes(permission))
})

const showFavoriteButton = computed(() => {
  if (!props.showFavorite) return false
  if (!props.favoritePermission?.length) return false
  return props.favoritePermission.some(permission => userStore.permissions.includes(permission))
})

const showUnfavoriteButton = computed(() => {
  if (!props.showUnfavorite) return false
  if (!props.unfavoritePermission?.length) return false
  return props.unfavoritePermission.some(permission => userStore.permissions.includes(permission))
})

const showShareButton = computed(() => {
  if (!props.showShare) return false
  if (!props.sharePermission?.length) return false
  return props.sharePermission.some(permission => userStore.permissions.includes(permission))
})

const showUnshareButton = computed(() => {
  if (!props.showUnshare) return false
  if (!props.unsharePermission?.length) return false
  return props.unsharePermission.some(permission => userStore.permissions.includes(permission))
})

const showCommentButton = computed(() => {
  if (!props.showComment) return false
  if (!props.commentPermission?.length) return false
  return props.commentPermission.some(permission => userStore.permissions.includes(permission))
})

const showUncommentButton = computed(() => {
  if (!props.showUncomment) return false
  if (!props.uncommentPermission?.length) return false
  return props.uncommentPermission.some(permission => userStore.permissions.includes(permission))
})

const showFlaggingButton = computed(() => {
  if (!props.showFlagging) return false
  if (!props.flaggingPermission?.length) return false
  return props.flaggingPermission.some(permission => userStore.permissions.includes(permission))
})

const showUnflaggingButton = computed(() => {
  if (!props.showUnflagging) return false
  if (!props.unflaggingPermission?.length) return false
  return props.unflaggingPermission.some(permission => userStore.permissions.includes(permission))
})

const showFollowButton = computed(() => {
  if (!props.showFollow) return false
  if (!props.followPermission?.length) return false
  return props.followPermission.some(permission => userStore.permissions.includes(permission))
})

const showUnfollowButton = computed(() => {
  if (!props.showUnfollow) return false
  if (!props.unfollowPermission?.length) return false
  return props.unfollowPermission.some(permission => userStore.permissions.includes(permission))
})

// 工作流按钮权限验证
const showAddsignButton = computed(() => {
  if (!props.showAddsign) return false
  if (!props.addsignPermission?.length) return false
  return props.addsignPermission.some(permission => userStore.permissions.includes(permission))
})

const showSubsignButton = computed(() => {
  if (!props.showSubsign) return false
  if (!props.subsignPermission?.length) return false
  return props.subsignPermission.some(permission => userStore.permissions.includes(permission))
})

const showConfigButton = computed(() => {
  if (!props.showConfig) return false
  if (!props.configPermission?.length) return false
  return props.configPermission.some(permission => userStore.permissions.includes(permission))
})

const showDelegateButton = computed(() => {
  if (!props.showDelegate) return false
  if (!props.delegatePermission?.length) return false
  return props.delegatePermission.some(permission => userStore.permissions.includes(permission))
})

const showDesignButton = computed(() => {
  if (!props.showDesign) return false
  if (!props.designPermission?.length) return false
  return props.designPermission.some(permission => userStore.permissions.includes(permission))
})

const showDisableButton = computed(() => {
  if (!props.showDisable) return false
  if (!props.disablePermission?.length) return false
  return props.disablePermission.some(permission => userStore.permissions.includes(permission))
})

const showEnableButton = computed(() => {
  if (!props.showEnable) return false
  if (!props.enablePermission?.length) return false
  return props.enablePermission.some(permission => userStore.permissions.includes(permission))
})

const showEngineButton = computed(() => {
  if (!props.showEngine) return false
  if (!props.enginePermission?.length) return false
  return props.enginePermission.some(permission => userStore.permissions.includes(permission))
})

const showHistoryButton = computed(() => {
  if (!props.showHistory) return false
  if (!props.historyPermission?.length) return false
  return props.historyPermission.some(permission => userStore.permissions.includes(permission))
})

const showProgressButton = computed(() => {
  if (!props.showProgress) return false
  if (!props.progressPermission?.length) return false
  return props.progressPermission.some(permission => userStore.permissions.includes(permission))
})

const showStartButton = computed(() => {
  if (!props.showStart) return false
  if (!props.startPermission?.length) return false
  return props.startPermission.some(permission => userStore.permissions.includes(permission))
})

const showStopButton = computed(() => {
  if (!props.showStop) return false
  if (!props.stopPermission?.length) return false
  return props.stopPermission.some(permission => userStore.permissions.includes(permission))
})

const showSubmitButton = computed(() => {
  if (!props.showSubmit) return false
  if (!props.submitPermission?.length) return false
  return props.submitPermission.some(permission => userStore.permissions.includes(permission))
})

const showSuspendButton = computed(() => {
  if (!props.showSuspend) return false
  if (!props.suspendPermission?.length) return false
  return props.suspendPermission.some(permission => userStore.permissions.includes(permission))
})

const showTerminateButton = computed(() => {
  if (!props.showTerminate) return false
  if (!props.terminatePermission?.length) return false
  return props.terminatePermission.some(permission => userStore.permissions.includes(permission))
})

const showTransferButton = computed(() => {
  if (!props.showTransfer) return false
  if (!props.transferPermission?.length) return false
  return props.transferPermission.some(permission => userStore.permissions.includes(permission))
})

const showVersionButton = computed(() => {
  if (!props.showVersion) return false
  if (!props.versionPermission?.length) return false
  return props.versionPermission.some(permission => userStore.permissions.includes(permission))
})

const showWithdrawButton = computed(() => {
  if (!props.showWithdraw) return false
  if (!props.withdrawPermission?.length) return false
  return props.withdrawPermission.some(permission => userStore.permissions.includes(permission))
})

const showReturnButton = computed(() => {
  if (!props.showReturn) return false
  if (!props.returnPermission?.length) return false
  return props.returnPermission.some(permission => userStore.permissions.includes(permission))
})

const showUrgeButton = computed(() => {
  if (!props.showUrge) return false
  if (!props.urgePermission?.length) return false
  return props.urgePermission.some(permission => userStore.permissions.includes(permission))
})

// 表单按钮权限验证
const showDataButton = computed(() => {
  if (!props.showData) return false
  if (!props.dataPermission?.length) return false
  return props.dataPermission.some(permission => userStore.permissions.includes(permission))
})

const showDatasourceButton = computed(() => {
  if (!props.showDatasource) return false
  if (!props.datasourcePermission?.length) return false
  return props.datasourcePermission.some(permission => userStore.permissions.includes(permission))
})

const showFieldButton = computed(() => {
  if (!props.showField) return false
  if (!props.fieldPermission?.length) return false
  return props.fieldPermission.some(permission => userStore.permissions.includes(permission))
})

const showPermissionButton = computed(() => {
  if (!props.showPermission) return false
  if (!props.permissionPermission?.length) return false
  return props.permissionPermission.some(permission => userStore.permissions.includes(permission))
})

const showThemeButton = computed(() => {
  if (!props.showTheme) return false
  if (!props.themePermission?.length) return false
  return props.themePermission.some(permission => userStore.permissions.includes(permission))
})

// 文件操作按钮权限验证
const showArchiveButton = computed(() => {
  if (!props.showArchive) return false
  if (!props.archivePermission?.length) return false
  return props.archivePermission.some(permission => userStore.permissions.includes(permission))
})

const showDestroyButton = computed(() => {
  if (!props.showDestroy) return false
  if (!props.destroyPermission?.length) return false
  return props.destroyPermission.some(permission => userStore.permissions.includes(permission))
})

const showDownloadButton = computed(() => {
  if (!props.showDownload) return false
  if (!props.downloadPermission?.length) return false
  return props.downloadPermission.some(permission => userStore.permissions.includes(permission))
})

const showUploadButton = computed(() => {
  if (!props.showUpload) return false
  if (!props.uploadPermission?.length) return false
  return props.uploadPermission.some(permission => userStore.permissions.includes(permission))
})

// 系统操作按钮权限验证
const showEmptyButton = computed(() => {
  if (!props.showEmpty) return false
  if (!props.emptyPermission?.length) return false
  return props.emptyPermission.some(permission => userStore.permissions.includes(permission))
})

const showRefreshButton = computed(() => {
  if (!props.showRefresh) return false
  if (!props.refreshPermission?.length) return false
  return props.refreshPermission.some(permission => userStore.permissions.includes(permission))
})

const showRestartButton = computed(() => {
  if (!props.showRestart) return false
  if (!props.restartPermission?.length) return false
  return props.restartPermission.some(permission => userStore.permissions.includes(permission))
})

const showRunButton = computed(() => {
  if (!props.showRun) return false
  if (!props.runPermission?.length) return false
  return props.runPermission.some(permission => userStore.permissions.includes(permission))
})

const showTruncateButton = computed(() => {
  if (!props.showTruncate) return false
  if (!props.truncatePermission?.length) return false
  return props.truncatePermission.some(permission => userStore.permissions.includes(permission))
})

// 其他按钮权限验证
const showCalculateButton = computed(() => {
  if (!props.showCalculate) return false
  if (!props.calculatePermission?.length) return false
  return props.calculatePermission.some(permission => userStore.permissions.includes(permission))
})

const showCleanButton = computed(() => {
  if (!props.showClean) return false
  if (!props.cleanPermission?.length) return false
  return props.cleanPermission.some(permission => userStore.permissions.includes(permission))
})

const showCirculateButton = computed(() => {
  if (!props.showCirculate) return false
  if (!props.circulatePermission?.length) return false
  return props.circulatePermission.some(permission => userStore.permissions.includes(permission))
})

const showConfirmButton = computed(() => {
  if (!props.showConfirm) return false
  if (!props.confirmPermission?.length) return false
  return props.confirmPermission.some(permission => userStore.permissions.includes(permission))
})

const showDraftButton = computed(() => {
  if (!props.showDraft) return false
  if (!props.draftPermission?.length) return false
  return props.draftPermission.some(permission => userStore.permissions.includes(permission))
})

const showDeletedraftButton = computed(() => {
  if (!props.showDeletedraft) return false
  if (!props.deletedraftPermission?.length) return false
  return props.deletedraftPermission.some(permission => userStore.permissions.includes(permission))
})

const showDetailButton = computed(() => {
  if (!props.showDetail) return false
  if (!props.detailPermission?.length) return false
  return props.detailPermission.some(permission => userStore.permissions.includes(permission))
})

const showForwardButton = computed(() => {
  if (!props.showForward) return false
  if (!props.forwardPermission?.length) return false
  return props.forwardPermission.some(permission => userStore.permissions.includes(permission))
})

const showListButton = computed(() => {
  if (!props.showList) return false
  if (!props.listPermission?.length) return false
  return props.listPermission.some(permission => userStore.permissions.includes(permission))
})

const showManageButton = computed(() => {
  if (!props.showManage) return false
  if (!props.managePermission?.length) return false
  return props.managePermission.some(permission => userStore.permissions.includes(permission))
})

const showReadButton = computed(() => {
  if (!props.showRead) return false
  if (!props.readPermission?.length) return false
  return props.readPermission.some(permission => userStore.permissions.includes(permission))
})

const showReplyButton = computed(() => {
  if (!props.showReply) return false
  if (!props.replyPermission?.length) return false
  return props.replyPermission.some(permission => userStore.permissions.includes(permission))
})

const showRevokeButton = computed(() => {
  if (!props.showRevoke) return false
  if (!props.revokePermission?.length) return false
  return props.revokePermission.some(permission => userStore.permissions.includes(permission))
})

const showSendButton = computed(() => {
  if (!props.showSend) return false
  if (!props.sendPermission?.length) return false
  return props.sendPermission.some(permission => userStore.permissions.includes(permission))
})

const showSignButton = computed(() => {
  if (!props.showSign) return false
  if (!props.signPermission?.length) return false
  return props.signPermission.some(permission => userStore.permissions.includes(permission))
})

const showUnreadButton = computed(() => {
  if (!props.showUnread) return false
  if (!props.unreadPermission?.length) return false
  return props.unreadPermission.some(permission => userStore.permissions.includes(permission))
})

// 缺失按钮的权限验证
const showApproveButton = computed(() => {
  if (!props.showApprove) return false
  if (!props.approvePermission?.length) return false
  return props.approvePermission.some(permission => userStore.permissions.includes(permission))
})

const showCreateButton = computed(() => {
  if (!props.showCreate) return false
  if (!props.createPermission?.length) return false
  return props.createPermission.some(permission => userStore.permissions.includes(permission))
})

const showQueryButton = computed(() => {
  if (!props.showQuery) return false
  if (!props.queryPermission?.length) return false
  return props.queryPermission.some(permission => userStore.permissions.includes(permission))
})

const showUpdateButton = computed(() => {
  if (!props.showUpdate) return false
  if (!props.updatePermission?.length) return false
  return props.updatePermission.some(permission => userStore.permissions.includes(permission))
})

// === 事件处理 ===
const handleSave = () => emit('save', props.record)
const handleView = () => emit('view', props.record)
const handleEdit = () => emit('edit', props.record)
const handleDelete = () => emit('delete', props.record)
const handleCopy = () => emit('copy', props.record)
const handleClone = () => emit('clone', props.record)
const handleImport = () => emit('import', props.record)
const handleExport = () => emit('export', props.record)
const handlePublish = () => emit('publish', props.record)
const handleAudit = () => emit('audit', props.record)
const handleValidate = () => emit('validate', props.record)
const handlePause = () => emit('pause', props.record)
const handleResume = () => emit('resume', props.record)
// 会计相关事件处理
const handleBook = () => emit('book', props.record)
const handleClosing = () => emit('closing', props.record)
const handleReconcile = () => emit('reconcile', props.record)
const handlePayment = () => emit('payment', props.record)
const handleDepreciation = () => emit('depreciation', props.record)
const handleReimburse = () => emit('reimburse', props.record)
const handleReversal = () => emit('reversal', props.record)
const handleAccrual = () => emit('accrual', props.record)
const handlePeriod = () => emit('period', props.record)
const handleCarryforward = () => emit('carryforward', props.record)
const handleCancel = () => emit('cancel', props.record)
const handleBilling = () => emit('billing', props.record)
// 认证相关事件处理
const handleAllocate = () => emit('allocate', props.record)
const handleAuthorize = () => emit('authorize', props.record)
const handleChangepwd = () => emit('changepwd', props.record)
const handleResetpwd = () => emit('resetpwd', props.record)
const handleReset = () => emit('reset', props.record)
// 代码生成相关事件处理
const handleColumns = () => emit('columns', props.record)
const handleDatabases = () => emit('databases', props.record)
const handleGenerate = () => emit('generate', props.record)
const handleInitialize = () => emit('initialize', props.record)
const handleSync = () => emit('sync', props.record)
const handleTables = () => emit('tables', props.record)
const handleTemplate = () => emit('template', props.record)
const handlePreview = () => emit('preview', props.record)
const handlePrint = () => emit('print', props.record)
// 社交互动事件处理
const handleLike = () => emit('like', props.record)
const handleUnlike = () => emit('unlike', props.record)
const handleFavorite = () => emit('favorite', props.record)
const handleUnfavorite = () => emit('unfavorite', props.record)
const handleShare = () => emit('share', props.record)
const handleUnshare = () => emit('unshare', props.record)
const handleComment = () => emit('comment', props.record)
const handleUncomment = () => emit('uncomment', props.record)
const handleFlagging = () => emit('flagging', props.record)
const handleUnflagging = () => emit('unflagging', props.record)
const handleFollow = () => emit('follow', props.record)
const handleUnfollow = () => emit('unfollow', props.record)
// 工作流事件处理
const handleAddsign = () => emit('addsign', props.record)
const handleSubsign = () => emit('subsign', props.record)
const handleConfig = () => emit('config', props.record)
const handleDelegate = () => emit('delegate', props.record)
const handleDesign = () => emit('design', props.record)
const handleDisable = () => emit('disable', props.record)
const handleEnable = () => emit('enable', props.record)
const handleEngine = () => emit('engine', props.record)
const handleHistory = () => emit('history', props.record)
const handleProgress = () => emit('progress', props.record)
const handleStart = () => emit('start', props.record)
const handleStop = () => emit('stop', props.record)
const handleSubmit = () => emit('submit', props.record)
const handleSuspend = () => emit('suspend', props.record)
const handleTerminate = () => emit('terminate', props.record)
const handleTransfer = () => emit('transfer', props.record)
const handleVersion = () => emit('version', props.record)
const handleWithdraw = () => emit('withdraw', props.record)
const handleReturn = () => emit('return', props.record)
const handleUrge = () => emit('urge', props.record)
// 表单事件处理
const handleData = () => emit('data', props.record)
const handleDatasource = () => emit('datasource', props.record)
const handleField = () => emit('field', props.record)
const handlePermission = () => emit('permission', props.record)
const handleTheme = () => emit('theme', props.record)
// 文件操作事件处理
const handleArchive = () => emit('archive', props.record)
const handleDestroy = () => emit('destroy', props.record)
const handleDownload = () => emit('download', props.record)
const handleUpload = () => emit('upload', props.record)
// 系统操作事件处理
const handleEmpty = () => emit('empty', props.record)
const handleRefresh = () => emit('refresh', props.record)
const handleRestart = () => emit('restart', props.record)
const handleRun = () => emit('run', props.record)
const handleTruncate = () => emit('truncate', props.record)
// 其他事件处理
const handleCalculate = () => emit('calculate', props.record)
const handleClean = () => emit('clean', props.record)
const handleCirculate = () => emit('circulate', props.record)
const handleConfirm = () => emit('confirm', props.record)
const handleDraft = () => emit('draft', props.record)
const handleDeletedraft = () => emit('deletedraft', props.record)
const handleDetail = () => emit('detail', props.record)
const handleForward = () => emit('forward', props.record)
const handleList = () => emit('list', props.record)
const handleManage = () => emit('manage', props.record)
const handleRead = () => emit('read', props.record)
const handleReply = () => emit('reply', props.record)
const handleRevoke = () => emit('revoke', props.record)
const handleSend = () => emit('send', props.record)
const handleSign = () => emit('sign', props.record)
const handleUnread = () => emit('unread', props.record)
// 缺失按钮的事件处理
const handleApprove = () => emit('approve', props.record)
const handleCreate = () => emit('create', props.record)
const handleQuery = () => emit('query', props.record)
const handleUpdate = () => emit('update', props.record)
</script>

<style lang="less" scoped>
.Takt-operation {
  display: inline-flex;
  gap: 8px;
  align-items: center;

  &--vertical {
    flex-direction: column;
    align-items: flex-start;
  }

  :deep(.ant-dropdown-trigger) {
    margin-left: 8px;
  }
}
</style> 
