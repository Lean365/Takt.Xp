export default {
  identity: {
    role: {
      title: 'ロール管理',
      fields: {
        roleId: {
          label: 'ロールID'
        },
        roleName: {
          label: 'ロール名',
          placeholder: 'ロール名を入力してください',
          validation: {
            required: 'ロール名は必須です',
            length: 'ロール名の長さは2-20文字である必要があります'
          }
        },
        roleKey: {
          label: 'ロールキー',
          placeholder: 'ロールキーを入力してください',
          validation: {
            required: 'ロールキーは必須です',
            length: 'ロールキーの長さは2-100文字である必要があります'
          }
        },
        roleSort: {
          label: '表示順序',
          placeholder: '表示順序を入力してください'
        },
        dataScope: {
          label: 'データ範囲',
          placeholder: 'データ範囲を選択してください'
        },
        userCount: {
          label: 'ユーザー数',
          placeholder: 'ユーザー数を入力してください'
        },
        status: {
          label: 'ステータス',
          placeholder: 'ステータスを選択してください',
          options: {
            enabled: '有効',
            disabled: '無効'
          }
        }
      },
      actions: {
        add: 'ロール追加',
        edit: 'ロール編集',
        delete: 'ロール削除',
        export: 'ロールエクスポート',
        authorize: 'メニュー認可',
        deptAuthorize: '部門認可'
      },
      messages: {
        confirmDelete: '選択されたロールを削除してもよろしいですか？',
        deleteSuccess: 'ロールが正常に削除されました',
        deleteFailed: 'ロールの削除に失敗しました',
        saveSuccess: 'ロール情報が正常に保存されました',
        saveFailed: 'ロール情報の保存に失敗しました',
        cannotEditSystemAdmin: 'システム管理者ロールは編集できません！',
        cannotDeleteSystemAdmin: 'システム管理者ロールは削除できません！',
        cannotUpdateSystemAdminStatus: 'システム管理者ロールのステータスは変更できません！',
        cannotAllocateSystemAdminMenu: 'システム管理者ロールにメニューを割り当てることはできません！',
        cannotAllocateSystemAdminDept: 'システム管理者ロールに部門を割り当てることはできません！'
      }
    }
  }
}