export default {
  identity: {
    menu: {
      title: 'メニュー管理',
      columns: {
        menuName: 'メニュー名',
        transKey: 'I18nキー',
        parentId: '親メニュー',
        orderNum: '順序',
        path: 'ルートパス',
        component: 'コンポーネントパス',
        queryParams: 'ルートパラメータ',
        isExternal: '外部リンク',
        isCache: 'キャッシュ',
        menuType: 'メニュータイプ',
        visible: '表示状態',
        status: '状態',
        perms: '権限キー',
        icon: 'アイコン',
        id: 'ID',
        createBy: '作成者',
        createTime: '作成日時',
        updateBy: '更新者',
        updateTime: '更新日時',
        deleteBy: '削除者',
        deleteTime: '削除日時',
        isDeleted: '削除済み',
        remark: '備考',
        action: '操作'
      },
      fields: {
        menuName: {
          label: 'メニュー名',
          placeholder: 'メニュー名を入力してください',
          validation: {
            required: 'メニュー名を入力してください',
            length: 'メニュー名は2～50文字で入力してください'
          }
        },
        transKey: {
          label: 'I18nキー',
          placeholder: 'I18nキーを入力してください'
        },
        parentId: {
          label: '親メニュー',
          placeholder: '親メニューを選択してください',
          root: 'ルートメニュー'
        },
        orderNum: {
          label: '順序',
          placeholder: '順序を入力してください',
          validation: {
            required: '順序を入力してください'
          }
        },
        path: {
          label: 'ルートパス',
          placeholder: 'ルートパスを入力してください'
        },
        component: {
          label: 'コンポーネントパス',
          placeholder: 'コンポーネントパスを入力してください'
        },
        queryParams: {
          label: 'ルートパラメータ',
          placeholder: 'ルートパラメータを入力してください'
        },
        isExternal: {
          label: '外部リンク',
          placeholder: '外部リンクかどうかを選択してください',
          options: {
            yes: 'はい',
            no: 'いいえ'
          }
        },
        isCache: {
          label: 'キャッシュ',
          placeholder: 'キャッシュするかどうかを選択してください',
          options: {
            yes: 'はい',
            no: 'いいえ'
          }
        },
        menuType: {
          label: 'メニュータイプ',
          options: {
            directory: 'ディレクトリ',
            menu: 'メニュー',
            button: 'ボタン'
          },
          validation: {
            required: 'メニュータイプを選択してください'
          }
        },
        visible: {
          label: '表示状態',
          placeholder: '表示状態を選択してください',
          options: {
            show: '表示',
            hide: '非表示'
          }
        },
        status: {
          label: '状態',
          placeholder: '状態を選択してください',
          options: {
            normal: '正常',
            disabled: '無効'
          }
        },
        perms: {
          label: '権限キー',
          placeholder: '権限キーを入力してください'
        },
        icon: {
          label: 'メニューアイコン',
          placeholder: 'メニューアイコンを入力してください'
        },
        }
      },
      dialog: {
        create: 'メニュー追加',
        update: 'メニュー編集',
        delete: 'メニュー削除'
      },
      operation: {
        add: {
          title: 'メニュー追加',
          success: '追加に成功しました',
          failed: '追加に失敗しました'
        },
        edit: {
          title: 'メニュー編集',
          success: '編集に成功しました',
          failed: '編集に失敗しました'
        },
        delete: {
          title: 'メニュー削除',
          confirm: 'このメニューを削除してもよろしいですか？',
          success: '削除に成功しました',
          failed: '削除に失敗しました'
        }
      }
    }
  }
