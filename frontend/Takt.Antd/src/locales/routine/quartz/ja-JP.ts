export default {
  routine: {
    quartz: {
      // 基本情報
      jobId: 'ジョブID',
      jobName: 'ジョブ名',
      jobGroup: 'ジョブグループ',
      jobClass: 'ジョブクラス',
      jobMethod: 'ジョブメソッド',
      jobParams: 'ジョブパラメータ',
      cronExpression: 'Cron式',
      jobStatus: 'ジョブ状態',
      remark: '備考',
      createTime: '作成時間',
      updateTime: '更新時間',

      // 操作ボタン
      add: 'ジョブ追加',
      edit: 'ジョブ編集',
      delete: 'ジョブ削除',
      batchDelete: '一括削除',
      export: 'エクスポート',
      import: 'インポート',
      downloadTemplate: 'テンプレートダウンロード',
      preview: 'プレビュー',
      execute: '実行',
      pause: '一時停止',
      resume: '再開',

      // フォームプレースホルダー
      placeholder: {
        jobId: 'ジョブIDを入力してください',
        jobName: 'ジョブ名を入力してください',
        jobGroup: 'ジョブグループを入力してください',
        jobClass: 'ジョブクラスを入力してください',
        jobMethod: 'ジョブメソッドを入力してください',
        jobParams: 'ジョブパラメータを入力してください',
        cronExpression: 'Cron式を入力してください',
        jobStatus: 'ジョブ状態を選択してください',
        remark: '備考を入力してください',
        startTime: '開始時間',
        endTime: '終了時間'
      },

      // フォームバリデーション
      validation: {
        jobId: {
          required: 'ジョブIDを入力してください',
          maxLength: 'ジョブIDは100文字以内で入力してください'
        },
        jobName: {
          required: 'ジョブ名を入力してください',
          maxLength: 'ジョブ名は50文字以内で入力してください'
        },
        jobGroup: {
          required: 'ジョブグループを入力してください',
          maxLength: 'ジョブグループは50文字以内で入力してください'
        },
        jobClass: {
          required: 'ジョブクラスを入力してください',
          maxLength: 'ジョブクラスは200文字以内で入力してください'
        },
        jobMethod: {
          required: 'ジョブメソッドを入力してください',
          maxLength: 'ジョブメソッドは100文字以内で入力してください'
        },
        cronExpression: {
          required: 'Cron式を入力してください',
          maxLength: 'Cron式は100文字以内で入力してください'
        },
        jobStatus: {
          required: 'ジョブ状態を選択してください'
        }
      },

      // 操作結果
      message: {
        success: {
          add: '追加しました',
          edit: '更新しました',
          delete: '削除しました',
          batchDelete: '一括削除しました',
          export: 'エクスポートしました',
          import: 'インポートしました',
          execute: '実行しました',
          pause: '一時停止しました',
          resume: '再開しました'
        },
        failed: {
          add: '追加に失敗しました',
          edit: '更新に失敗しました',
          delete: '削除に失敗しました',
          batchDelete: '一括削除に失敗しました',
          export: 'エクスポートに失敗しました',
          import: 'インポートに失敗しました',
          execute: '実行に失敗しました',
          pause: '一時停止に失敗しました',
          resume: '再開に失敗しました'
        }
      },

      // 詳細ページ
      detail: {
        title: 'ジョブ詳細'
      }
    }
  }
} 