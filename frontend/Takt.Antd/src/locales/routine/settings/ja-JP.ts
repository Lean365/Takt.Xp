export default {
  admin: {
    // システム設定
    config: {
      // 基本情報
      name: '設定名',
      key: '設定キー',
      value: '設定値',
      builtin: 'システム組み込み',
      order: '順序',
      remark: '備考',
      status: '状態',
      // 時間情報
      createTime: '作成時間',
      createBy: '作成者',
      updateTime: '更新時間',
      updateBy: '更新者',
      // 操作
      operation: '操作',
      // プロンプト情報
      placeholder: {
        name: '設定名を入力してください',
        key: '設定キーを入力してください',
        value: '設定値を入力してください',
        builtin: 'システム組み込みかどうかを選択してください',
        order: '順序番号を入力してください',
        remark: '備考を入力してください',
        status: '状態を選択してください'
      },
      // 検証情報
      validation: {
        name: {
          required: '設定名を入力してください',
          maxLength: '設定名は100文字以内で入力してください'
        },
        key: {
          required: '設定キーを入力してください',
          maxLength: '設定キーは100文字以内で入力してください',
          pattern: '設定キーは英字で始まり、英字、数字、アンダースコア、ドット、コロンのみ使用できます'
        },
        value: {
          required: '設定値を入力してください',
          maxLength: '設定値は500文字以内で入力してください'
        },
        builtin: {
          required: 'システム組み込みかどうかを選択してください'
        },
        order: {
          required: '順序番号を入力してください',
          range: '順序番号は0から9999の間で入力してください'
        },
        status: {
          required: '状態を選択してください'
        }
      },
      // 操作結果
      message: {
        addSuccess: '設定の追加に成功しました',
        editSuccess: '設定の変更に成功しました',
        deleteSuccess: '設定の削除に成功しました',
        deleteConfirm: '設定"{name}"を削除してもよろしいですか？',
        exportSuccess: '設定のエクスポートに成功しました',
        importSuccess: '設定のインポートに成功しました',
        refreshSuccess: 'キャッシュの更新に成功しました'
      }
    }
  }
} 