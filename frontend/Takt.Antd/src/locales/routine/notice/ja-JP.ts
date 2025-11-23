export default {
  routine: {
    notice: {
      // 基本情報
      noticeId: '通知ID',
      noticeType: '通知タイプ',
      noticeTitle: '通知タイトル',
      noticeContent: '通知内容',
      noticeStatus: '通知状態',
      noticeParams: '通知パラメータ',
      remark: '備考',
      createTime: '作成時間',
      updateTime: '更新時間',

      // 操作ボタン
      add: '通知追加',
      edit: '通知編集',
      delete: '通知削除',
      batchDelete: '一括削除',
      export: 'エクスポート',
      import: 'インポート',
      downloadTemplate: 'テンプレートダウンロード',
      preview: 'プレビュー',
      send: '送信',

      // フォームプレースホルダー
      placeholder: {
        noticeId: '通知IDを入力してください',
        noticeType: '通知タイプを選択してください',
        noticeTitle: '通知タイトルを入力してください',
        noticeContent: '通知内容を入力してください',
        noticeStatus: '通知状態を選択してください',
        noticeParams: '通知パラメータを入力してください',
        remark: '備考を入力してください',
        startTime: '開始時間',
        endTime: '終了時間'
      },

      // フォームバリデーション
      validation: {
        noticeId: {
          required: '通知IDを入力してください',
          maxLength: '通知IDは100文字以内で入力してください'
        },
        noticeType: {
          required: '通知タイプを選択してください',
          maxLength: '通知タイプは50文字以内で入力してください'
        },
        noticeTitle: {
          required: '通知タイトルを入力してください',
          maxLength: '通知タイトルは200文字以内で入力してください'
        },
        noticeContent: {
          required: '通知内容を入力してください',
          maxLength: '通知内容は4000文字以内で入力してください'
        },
        noticeStatus: {
          required: '通知状態を選択してください'
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
          send: '送信しました'
        },
        failed: {
          add: '追加に失敗しました',
          edit: '更新に失敗しました',
          delete: '削除に失敗しました',
          batchDelete: '一括削除に失敗しました',
          export: 'エクスポートに失敗しました',
          import: 'インポートに失敗しました',
          send: '送信に失敗しました'
        }
      },

      // 詳細ページ
      detail: {
        title: '通知詳細'
      }
    }
  }
} 