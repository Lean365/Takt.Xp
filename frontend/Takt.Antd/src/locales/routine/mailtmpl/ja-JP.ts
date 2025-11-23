export default {
  routine: {
    mailtmpl: {
      // 基本情報
      templateName: 'テンプレート名',
      templateType: 'テンプレートタイプ',
      templateSubject: 'テンプレート件名',
      templateContent: 'テンプレート内容',
      templateStatus: 'テンプレート状態',
      templateParams: 'テンプレートパラメータ',
      remark: '備考',
      createTime: '作成日時',
      updateTime: '更新日時',

      // 操作ボタン
      add: 'テンプレート追加',
      edit: 'テンプレート編集',
      delete: 'テンプレート削除',
      batchDelete: '一括削除',
      export: 'エクスポート',
      import: 'インポート',
      downloadTemplate: 'テンプレートダウンロード',
      preview: 'プレビュー',
      send: '送信',

      // フォームプレースホルダー
      placeholder: {
        templateName: 'テンプレート名を入力してください',
        templateType: 'テンプレートタイプを選択してください',
        templateSubject: 'テンプレート件名を入力してください',
        templateContent: 'テンプレート内容を入力してください',
        templateStatus: 'テンプレート状態を選択してください',
        templateParams: 'テンプレートパラメータを入力してください',
        remark: '備考を入力してください',
        startTime: '開始日時',
        endTime: '終了日時'
      },

      // フォームバリデーション
      validation: {
        templateName: {
          required: 'テンプレート名を入力してください',
          maxLength: 'テンプレート名は100文字以内で入力してください'
        },
        templateType: {
          required: 'テンプレートタイプを選択してください',
          maxLength: 'テンプレートタイプは50文字以内で入力してください'
        },
        templateSubject: {
          required: 'テンプレート件名を入力してください',
          maxLength: 'テンプレート件名は200文字以内で入力してください'
        },
        templateContent: {
          required: 'テンプレート内容を入力してください',
          maxLength: 'テンプレート内容は4000文字以内で入力してください'
        },
        templateStatus: {
          required: 'テンプレート状態を選択してください'
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
        title: 'メールテンプレート詳細'
      }
    }
  }
}
