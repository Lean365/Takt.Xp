export default {
  common: {
    // ==================== システム情報 ====================
    system: {
      title: 'ブラックアイスプラットフォーム',
      slogan: 'プロフェッショナルで効率的で安全な企業管理システム',
      description: '.NET 8とVue 3に基づく最新の企業管理システム',
      copyright: '© 2024Takt(Claude AI). All rights reserved.'
    },

    // ==================== エラーメッセージ ====================
    error: {
      clientError: 'クライアントリクエストエラー、リクエストパラメータを確認してください',
      systemRestart: 'システムはメンテナンス中です。後で再度ログインしてください',
      network: 'ネットワーク接続に失敗しました。ネットワーク設定を確認してください',
      unauthorized: '認証されていません。再度ログインしてください',
      forbidden: 'アクセスが拒否されました',
      notFound: '要求されたリソースが存在しません',
      badRequest: '無効なリクエストパラメータ',
      serverError: 'サーバー内部エラー',
      serviceUnavailable: 'サービスが一時的に利用できません',
      badGateway: '不正なゲートウェイ',
      gatewayTimeout: 'ゲートウェイタイムアウト',
      unknown: '不明なエラー'
    },

    // ==================== 基本操作 ====================
    // 基本タイトル
    title: {
      list: '一覧',
      detail: '詳細',
      create: '新規追加',
      edit: '編集',
      preview: 'プレビュー'
    },

    // ==================== 状態定義 ====================
    status: {
      // 基本状態
      base: {
        label: '状態',
        normal: '正常',
        disabled: '無効',
        placeholder: '状態を選択してください'
      },

      // はい/いいえ状態
      yesNo: {
        all: 'すべて',
        yes: 'はい',
        no: 'いいえ'
      },

      // 表示状態
      visible: {
        show: '表示',
        hide: '非表示'
      },

      // キャッシュ状態
      cache: {
        enabled: '有効',
        disabled: '無効'
      },

      // オンライン状態
      online: {
        online: 'オンライン',
        offline: 'オフライン'
      },

      // 処理状態
      process: {
        pending: '保留中',
        processing: '処理中',
        completed: '完了',
        failed: '失敗'
      },

      // 検証状態
      verify: {
        unverified: '未検証',
        verified: '検証済み',
        failed: '検証失敗'
      },

      // ロック状態
      lock: {
        locked: 'ロック中',
        unlocked: 'ロック解除'
      },

      // 公開状態
      publish: {
        draft: '下書き',
        published: '公開済み',
        unpublished: '未公開'
      },

      // 同期状態
      sync: {
        synced: '同期済み',
        unsynced: '未同期',
        syncing: '同期中'
      }
    },

    // ==================== フォーム操作 ====================
    form: {
      required: '必須',
      optional: '任意',
      invalid: '無効',
      // フォームプレースホルダー
      placeholder: {
        select: '選択してください',
        input: '入力してください',
        date: '日付を選択してください',
        time: '時間を選択してください'
      },
      // ユーザーフォーム
      user: {
        // 基本情報
        userId: {
          label: 'ユーザーID',
          placeholder: 'ユーザーIDを入力してください'
        },
        userName: {
          label: 'ユーザー名',
          placeholder: 'ユーザー名を入力してください'
        },
        nickName: {
          label: 'ニックネーム',
          placeholder: 'ニックネームを入力してください'
        },
        realName: {
          label: '本名',
          placeholder: '本名を入力してください'
        },
        englishName: {
          label: '英語名',
          placeholder: '英語名を入力してください'
        },
        password: {
          label: 'パスワード',
          placeholder: 'パスワードを入力してください'
        },
        confirmPassword: {
          label: 'パスワード確認',
          placeholder: 'パスワードを再入力してください'
        },
        
        // 個人情報
        avatar: {
          label: 'アバター',
          placeholder: 'アバターをアップロードしてください'
        },
        gender: {
          label: '性別',
          placeholder: '性別を選択してください',
          options: {
            male: '男性',
            female: '女性',
            other: 'その他'
          }
        },
        birthday: {
          label: '生年月日',
          placeholder: '生年月日を選択してください'
        },
        
        // 連絡先情報
        email: {
          label: 'メールアドレス',
          placeholder: 'メールアドレスを入力してください'
        },
        phoneNumber: {
          label: '携帯電話',
          placeholder: '携帯電話番号を入力してください'
        },
        telephone: {
          label: '電話番号',
          placeholder: '電話番号を入力してください'
        },
        
        // 組織情報
        deptId: {
          label: '部署',
          placeholder: '部署を選択してください'
        },
        roleId: {
          label: '役割',
          placeholder: '役割を選択してください'
        },
        postId: {
          label: '職位',
          placeholder: '職位を選択してください'
        },
        
        // アカウント情報
        userType: {
          label: 'ユーザータイプ',
          placeholder: 'ユーザータイプを選択してください',
          options: {
            system: 'システムユーザー',
            normal: '一般ユーザー'
          }
        },
        status: {
          label: '状態',
          placeholder: '状態を選択してください'
        },
        loginIp: {
          label: '最終ログインIP',
          placeholder: '最終ログインIP'
        },
        loginDate: {
          label: '最終ログイン日時',
          placeholder: '最終ログイン日時'
        },
        
        // その他の情報
        remark: {
          label: '備考',
          placeholder: '備考を入力してください'
        },
        sort: {
          label: '順序',
          placeholder: '順序番号を入力してください'
        }
      },
      // 備考情報
      remark: {
        label: '備考',
        placeholder: '備考を入力してください'
      }
    },

    // ==================== テーブル操作 ====================
    table: {
      header: {
        operation: '操作'
      },
      config: {
        density: {
          default: 'デフォルト',
          middle: '中',
          small: '小'
        },
        columnSetting: '列設定'
      },
      pagination: {
        total: '全{total}件',
        current: 'ページ{current}',
        pageSize: '1ページ{pageSize}件',
        jump: '移動'
      },
      empty: 'データなし',
      loading: '読み込み中...',
      selectAll: 'すべて選択',
      selected: '{total}件選択中'
    },

    // ==================== 時間操作 ====================
    datetime: {
      date: '日付',
      time: '時間',
      year: '年',
      month: '月',
      day: '日',
      hour: '時',
      minute: '分',
      second: '秒',
      startDate: '開始日',
      endDate: '終了日',
      startTime: '開始時間',
      endTime: '終了時間',
      createTime: '作成日時',
      updateTime: '更新日時',
      formatError: '日時フォーマットに失敗しました',
      relativeTimeFormatError: '相対時間フォーマットに失敗しました',
      parseError: '日付の解析に失敗しました',
      rangeSeparator: ' から '
    },

    // ==================== ファイル操作 ====================
    // インポート/エクスポート
    import: {
      title: 'データインポート',
      file: 'ファイル選択',
      select: 'ファイルを選択',
      template: 'テンプレート',
      download: 'テンプレートをダウンロード',
      note: 'インポート説明',
      tips: 'インポートテンプレートの形式に厳密に従ってください。そうしないとインポートに失敗する可能性があります',
      format: 'Excelファイルのみ対応しています！',
      size: 'ファイルサイズは{size}MB以下にしてください！',
      total: '総レコード数',
      success: '成功数',
      failed: '失敗数',
      message: '失敗理由',
      dragText: 'クリックまたはファイルをこのエリアにドラッグしてアップロード',
      dragHint: '.xlsx形式のExcelファイルをサポート',
      sheetName: 'Excelファイルのシート名が以下であることを確認してください：{sheetName}',
      allSuccess: 'インポート成功{count}レコード、すべて成功！',
      partialSuccess: 'インポート成功{success}レコード、失敗{fail}レコード',
      allFailed: 'すべてのインポートが失敗、総{count}レコード',
      noData: 'データが読み取られませんでした',
      empty: 'ファイルが空です、アップロードできません',
      importFailed: 'インポートに失敗しました',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: 'インポート制限',
        fileCount: 'ファイル数制限：{count}ファイル',
        fileSize: 'ファイルサイズ制限：{size}MB',
        recordCount: 'レコード数制限：{count}件',
        fileFormat: 'ファイル形式：.xlsx形式のみ対応'
      },
      recordLimit: 'インポートするレコード数（{actual}レコード）が制限（{max}レコード）を超えています。バッチでインポートしてください'
    },

    // アップロード
    upload: {
      text: 'ファイルをドラッグ＆ドロップまたはクリックしてアップロード',
      picture: 'クリックして画像をアップロード',
      file: 'クリックしてファイルをアップロード',
      icon: 'クリックしてアイコンをアップロード',
      limit: {
        size: 'ファイルサイズは{size}以下にしてください',
        type: '{type}形式のみ対応しています'
      }
    },

    // ==================== 機能ボタン ====================
    actions: {
      // === 基本操作ボタン ===
      add: '追加',           // @btn-add-color
      edit: '編集',         // @btn-edit-color
      delete: '削除',      // @btn-delete-color
      batchDelete: '一括削除', // @btn-batch-delete-color
      view: '表示',          // @btn-view-color
      clear: 'クリア',       // @btn-clear-color
      forceOffline: '強制オフライン', // @btn-force-offline-color
      onlineStatus: 'オンライン状態', // @btn-online-status-color
      loginHistory: 'ログイン履歴', // @btn-login-history-color
      sendMail: 'メール送信', // @btn-send-mail-color
      viewMail: 'メール表示', // @btn-view-mail-color
      mailTemplate: 'メールテンプレート', // @btn-mail-template-color
      sendNotification: '通知送信', // @btn-send-notification-color
      viewNotification: '通知表示', // @btn-view-notification-color
      notificationSetting: '通知設定', // @btn-notification-setting-color
      sendMessage: 'メッセージ送信', // @btn-send-message-color
      viewMessage: 'メッセージ表示', // @btn-view-message-color
      messageSetting: 'メッセージ設定', // @btn-message-setting-color

      // === データ操作ボタン ===
      import: 'インポート',       // @btn-import-color
      export: 'エクスポート',       // @btn-export-color
      template: 'テンプレート',       // @btn-template-color
      preview: 'プレビュー',        // @btn-preview-color
      download: 'ダウンロード',  // @btn-download-color
      batchImport: '一括インポート', // @btn-batch-import-color
      batchExport: '一括エクスポート', // @btn-batch-export-color
      batchPrint: '一括印刷', // @btn-batch-print-color
      batchEdit: '一括編集',  // @btn-batch-edit-color
      batchUpdate: '一括更新', // @btn-batch-update-color

      // === 状態操作ボタン ===
      logging: '審査',         // @btn-audit-color
      revoke: '取り消し',     // @btn-revoke-color
      stop: '停止',          // @btn-stop-color
      run: '実行',           // @btn-run-color
      force: '強制',         // @btn-forced-color
      start: '開始',         // @btn-start-color
      suspend: '一時停止',    // @btn-suspend-color
      resume: '再開',        // @btn-resume-color
      submit: '提出',        // @btn-submit-color
      withdraw: '撤回',      // @btn-withdraw-color
      terminate: '終了',      // @btn-terminate-color

      // === システム機能ボタン ===
      generate: '生成',      // @btn-generate-color
      refresh: '更新',    // @btn-refresh-color
      info: '情報',      // @btn-info-color
      log: 'ログ',           // @btn-log-color
      chat: 'チャット',          // @btn-chat-color
      copy: 'コピー',           // @btn-copy-color
      execute: '実行',      // @btn-execute-color
      resetPwd: 'パスワードリセット', // @btn-reset-pwd-color
      open: '開く',           // @btn-open-color
      close: '閉じる',          // @btn-close-color
      more: 'もっと見る',             // @btn-more-color
      density: '密度',       // @btn-density-color
      columnSetting: '列設定', // @btn-column-setting-color

      // === 拡張機能ボタン ===
      search: '検索',     // @btn-search-color
      filter: 'フィルター',        // @btn-filter-color
      sort: 'ソート',            // @btn-sort-color
      config: '設定',     // @btn-config-color
      save: '保存',      // @btn-save-color
      cancel: 'キャンセル',        // @btn-cancel-color
      upload: 'アップロード',    // @btn-upload-color
      print: '印刷',        // @btn-print-color
      help: 'ヘルプ',             // @btn-help-color
      share: '共有',        // @btn-share-color
      lock: 'ロック',      // @btn-lock-color
      sync: '同期',     // @btn-sync-color
      expand: '展開',     // @btn-expand-color
      collapse: '折りたたみ',      // @btn-collapse-color
      approve: '承認',     // @btn-approve-color
      reject: '却下',        // @btn-reject-color
      comment: 'コメント',     // @btn-comment-color
      attach: '添付',        // @btn-attach-color

      // === 言語サポートボタン ===
      translate: '翻訳',    // @btn-translate-color
      langSwitch: '言語切り替え', // @btn-lang-switch-color
      dict: '辞書',     // @btn-dict-color

      // === データ分析ボタン ===
      analyze: '分析',      // @btn-analyze-color
      chart: 'グラフ',       // @btn-chart-color
      report: 'レポート',        // @btn-report-color
      dashboard: 'ダッシュボード', // @btn-dashboard-color
      statistics: '統計', // @btn-statistics-color
      forecast: '予測',    // @btn-forecast-color
      compare: '比較',      // @btn-compare-color

      // === ワークフローボタン ===
      startFlow: 'フロー開始', // @btn-start-flow-color
      endFlow: 'フロー終了', // @btn-end-flow-color
      suspendFlow: 'フロー中断', // @btn-suspend-flow-color
      resumeFlow: 'フロー再開', // @btn-resume-flow-color
      transfer: '転送',    // @btn-transfer-color
      delegate: '委任',      // @btn-delegate-color
      notify: '通知',        // @btn-notify-color
      urge: '督促',             // @btn-urge-color
      sign: '署名',            // @btn-sign-color
      countersign: '副署', // @btn-countersign-color

      // === モバイル専用ボタン ===
      scan: 'スキャン',          // @btn-scan-color
      location: '位置情報', // @btn-location-color
      call: '通話',          // @btn-call-color
      photo: '写真撮影', // @btn-photo-color
      voice: '音声',            // @btn-voice-color
      faceId: '顔認証',      // @btn-face-id-color
      fingerPrint: '指紋認証', // @btn-finger-print-color

      // === ソーシャルコラボレーションボタン ===
      follow: 'フォロー',         // @btn-follow-color
      collect: '収集',     // @btn-collect-color
      like: 'いいね',          // @btn-like-color
      forward: '転送',    // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: 'グループ',          // @btn-group-color
      team: 'チーム',           // @btn-team-color

      // === セキュリティ認証ボタン ===
      verifyCode: '認証コード', // @btn-verify-code-color
      bind: '連携',             // @btn-bind-color
      unbind: '連携解除',         // @btn-unbind-color
      authorize: '認可',   // @btn-authorize-color
      deauthorize: '認可解除', // @btn-deauthorize-color

      // === 高度な機能ボタン ===
      version: 'バージョン',       // @btn-version-color
      history: '履歴',    // @btn-history-color
      restore: '復元',     // @btn-restore-color
      archive: 'アーカイブ',      // @btn-archive-color
      unarchive: 'アーカイブ解除', // @btn-unarchive-color
      merge: 'マージ',       // @btn-merge-color
      split: '分割',         // @btn-split-color

      // === システム管理ボタン ===
      backup: 'バックアップ',    // @btn-backup-color
      restoreSys: 'システム復元', // @btn-restore-sys-color
      clean: 'クリーンアップ',        // @btn-clean-color
      optimize: '最適化',    // @btn-optimize-color
      monitor: 'モニタリング',    // @btn-monitor-color
      diagnose: '診断', // @btn-diagnose-color
      maintain: 'メンテナンス'     // @btn-maintain-color
    },

    // ==================== 結果とメッセージ ====================
    // 結果状態
    result: {
      success: '操作成功',
      failed: '操作失敗',
      warning: '警告',
      info: '情報',
      error: 'エラー'
    },

    // メッセージ
    message: {
      loading: '処理中...',
      saving: '保存中...',
      submitting: '送信中...',
      deleting: '削除中...',
      operationSuccess: '操作成功',
      operationFailed: '操作失敗',
      deleteConfirm: '削除してもよろしいですか？',
      deleteSuccess: '削除成功',
      deleteFailed: '削除失敗',
      createSuccess: '作成成功',
      createFailed: '作成失敗',
      updateSuccess: '更新成功',
      updateFailed: '更新失敗',
      networkError: 'ネットワーク接続に失敗しました。ネットワークを確認してください',
      systemError: 'システムエラー',
      timeout: 'リクエストタイムアウト',
      invalidResponse: '無効なレスポンスデータ形式',
      backendNotStarted: 'バックエンドサービスが起動していません。サービスの状態を確認してください',
      invalidRequest: '無効なリクエストパラメータ',
      unauthorized: '認証されていません。再度ログインしてください',
      forbidden: 'アクセスが拒否されました',
      notFound: '要求されたリソースが存在しません',
      serverError: 'サーバー内部エラー',
      httpError: {
        400: '無効なリクエストパラメータ',
        401: '認証されていません。再度ログインしてください',
        403: 'アクセスが拒否されました',
        404: '要求されたリソースが存在しません',
        500: 'サーバー内部エラー',
        502: '不正なゲートウェイ',
        503: 'サービスが利用できません',
        504: 'ゲートウェイタイムアウト'
      },
      loadFailed: '読み込み失敗',
      forceOfflineConfirm: 'このユーザーを強制オフラインにしますか？',
      forceOfflineSuccess: '強制オフライン成功',
      forceOfflineFailed: '強制オフライン失敗'
    },

    // ==================== コンポーネントテキスト ====================
    // タブ
    tab: {
      // === 基本情報 ===
      basic: '基本情報',
      profile: 'プロフィール',
      avatar: 'アバター設定',
      password: 'パスワード設定',
      security: 'セキュリティ設定',

      // === コード生成 ===
      codegen: 'コード生成',
      codegenBasic: '生成設定',
      codegenField: 'フィールド設定',
      codegenPreview: '生成プレビュー',
      codegenTemplate: 'テンプレート設定',
      codegenImport: 'インポート設定',
      
      // === ワークフロー ===
      workflow: 'ワークフロー',
      flowDesign: 'フロー設計',
      flowDeploy: 'フロー展開',
      flowInstance: 'フローインスタンス',
      flowTask: 'タスク管理',
      flowForm: 'フォーム設定',
      flowNotify: 'メッセージ通知',
      
      // === システム管理 ===
      permission: 'データ権限',
      log: '操作ログ',
      dept: '部署と職位',
      role: '役割と権限',
      config: 'パラメータ設定',
      task: 'スケジュールタスク',
      monitor: 'システムモニター'
    },

    // ボタンテキスト
    button: {
      submit: '送信',
      confirm: '確認',
      ok: 'OK',
      cancel: 'キャンセル',
      close: '閉じる',
      reset: 'リセット',
      clear: 'クリア',
      save: '保存',
      delete: '削除'
    }
  },

  tabs: {
    closeOthers: '他を閉じる',
    closeRight: '右側を閉じる',
    closeAll: 'すべて閉じる',
    // タブ制限メッセージ
    maxTabsReached: 'タブ数が上限に達しました（{count}）。新しいページを開く前にいくつかのタブを閉じてください。タブの右側のドロップダウンメニューを使用して複数のタブをすばやく閉じることができます。',
    tabsTruncated: '{total}個のタブが検出されました。最初の{count}個のタブが自動的に保持されました'
  },

  // ==================== セレクターコンポーネント ====================
  select: {
    loadMore: 'もっと読み込む',
    loading: '読み込み中...',
    notFound: 'データなし',
    selected: '{count}件選択中',
    selectedTotal: '全{total}件',
    clear: 'クリア',
    search: '検索',
    all: 'すべて',
    // エラーメッセージ
    loadError: 'データ読み込みに失敗しました',
    searchError: '検索に失敗しました',
    networkError: 'ネットワーク接続に失敗しました',
    serverError: 'サーバーエラー',
    unknownError: '不明なエラー'
  }
}


