export default {
  routine: {
    mail: {
      // 基本情報
      mailId: 'メールID',
      mailType: 'メールタイプ',
      mailSubject: 'メール件名',
      mailContent: 'メール内容',
      mailStatus: 'メール状態',
      mailParams: 'メールパラメータ',
      remark: '備考',
      createTime: '作成時間',
      updateTime: '更新時間',

      // 操作ボタン
      add: 'メール追加',
      edit: 'メール編集',
      delete: 'メール削除',
      batchDelete: '一括削除',
      export: 'エクスポート',
      import: 'インポート',
      downloadTemplate: 'テンプレートダウンロード',
      preview: 'プレビュー',
      send: '送信',

      // フォームプレースホルダー
      placeholder: {
        mailId: 'メールIDを入力してください',
        mailType: 'メールタイプを選択してください',
        mailSubject: 'メール件名を入力してください',
        mailContent: 'メール内容を入力してください',
        mailStatus: 'メール状態を選択してください',
        mailParams: 'メールパラメータを入力してください',
        remark: '備考を入力してください',
        startTime: '開始時間',
        endTime: '終了時間'
      },

      // フォームバリデーション
      validation: {
        mailId: {
          required: 'メールIDを入力してください',
          maxLength: 'メールIDは100文字以内で入力してください'
        },
        mailType: {
          required: 'メールタイプを選択してください',
          maxLength: 'メールタイプは50文字以内で入力してください'
        },
        mailSubject: {
          required: 'メール件名を入力してください',
          maxLength: 'メール件名は200文字以内で入力してください'
        },
        mailContent: {
          required: 'メール内容を入力してください',
          maxLength: 'メール内容は4000文字以内で入力してください'
        },
        mailStatus: {
          required: 'メール状態を選択してください'
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
        title: 'メール詳細'
      }
    }
  }
} 