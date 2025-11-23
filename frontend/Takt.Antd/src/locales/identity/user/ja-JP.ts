export default {
  identity: {
    user: {
      title: 'ユーザー管理',
      profile: '個人情報',
      changePasswordTitle: 'パスワード変更',
      
      // パスワード関連
      password: {
        old: {
          label: '旧パスワード',
          placeholder: '旧パスワードを入力してください',
          validation: {
            required: '旧パスワードは必須です',
          }
        },
        new: {
          label: '新パスワード',
          placeholder: '新パスワードを入力してください',
          validation: {
            required: '新パスワードは必須です',
          }
        },
        confirm: {
          label: 'パスワード確認',
          placeholder: 'パスワードを再入力してください',
          validation: {
            required: 'パスワード確認は必須です',
          }
        },
      },
      tabs: {
        userInfo: 'ユーザー情報',
        organization: '組織関係',
        avatar: 'アバター'
      },
      // フォームフィールド定義
      fields: {
        userId: {
          label: 'ユーザーID'
        },
        userName: {
          label: 'ユーザー名',
          placeholder: 'ユーザー名を入力してください',
          validation: {
            required: 'ユーザー名は必須です',
            format: '小文字で始まり、小文字、数字、ハイフンのみ含むことができ、ドットやアンダースコアは不可、長さは4-50文字'
          }
        },
        nickName: {
          label: 'ニックネーム',
          placeholder: 'ニックネームを入力してください',
          validation: {
            required: 'ニックネームは必須です',
            format: '2-50文字、中国語、日本語、韓国語、英語、数字、英語ピリオド、ハイフンを許可、アンダースコアやスペースは不可'
          }
        },
        realName: {
          label: '実名',
          placeholder: '実名を入力してください',
          validation: {
            required: '実名は必須です',
            format: '実名の長さは2-20文字、中国語、英語、数字、アンダースコアのみ含むことができます'
          }
        },
        fullName: {
          label: 'フルネーム',
          placeholder: 'フルネームを入力してください',
          validation: {
            required: 'フルネームは必須です',
            format: 'フルネームの長さは2-20文字、中国語、英語、数字、アンダースコアのみ含むことができます'
          }
        },
        englishName: {
          label: '英語名',
          placeholder: '英語名を入力してください',
          validation: {
            required: '英語名は必須です',
            format: '英語名の長さは2-100文字、文字で始まり、英語文字、スペース、ハイフン、英語ピリオドのみ含むことができます'
          }
        },
        password: {
          label: 'パスワード',
          placeholder: 'パスワードを入力してください',
          validation: {
            required: 'パスワードは必須です',
            format: 'パスワードの長さは6-20文字、英語文字、数字、特殊文字のみ含むことができます'
          }
        },
        userType: {
          label: 'ユーザータイプ',
          placeholder: 'ユーザータイプを選択してください',
          options: {
            admin: '管理者',
            user: '一般ユーザー'
          }
        },
        email: {
          label: 'メールアドレス',
          placeholder: 'メールアドレスを入力してください',
          validation: {
            required: 'メールアドレスは必須です',
            invalid: 'メールアドレスの長さは6-100文字で正しい形式である必要があります'
          }
        },
        phoneNumber: {
          label: '電話番号',
          placeholder: '電話番号を入力してください',
          validation: {
            required: '電話番号は必須です',
            invalid: '正しい携帯電話または固定電話の形式を入力してください'
          }
        },
        gender: {
          label: '性別',
          placeholder: '性別を選択してください',
          options: {
            male: '男性',
            female: '女性',
            unknown: '不明'
          }
        },
        avatar: {
          label: 'アバター',
          upload: 'アバターアップロード',
          currentAvatar: '現在のアバター',
          avatarUpload: 'アバターアップロード',
          uploadSuccess: 'アバターアップロード成功',
          uploadError: 'アバターアップロード失敗',
          uploading: 'アバターアップロード中...',
          onlyImage: '画像ファイルのみアップロード可能です！',
          sizeLimit: '画像サイズは5MBを超えることはできません！'
        },
        status: {
          label: 'ステータス',
          placeholder: 'ステータスを選択してください',
          options: {
            enabled: '有効',
            disabled: '無効'
          }
        },
        lastPasswordChangeTime: {
          label: '最終パスワード変更時間'
        },
        lockEndTime: {
          label: 'ロック終了時間'
        },
        lockReason: {
          label: 'ロック理由'
        },
        isLock: {
          label: 'ロック状態'
        },
        errorLimit: {
          label: 'エラー回数上限'
        },
        loginCount: {
          label: 'ログイン回数'
        },
        deptName: {
          label: '所属部門',
          placeholder: '所属部門を選択してください',
          validation: {
            required: '所属部門は必須です'
          }
        },
        role: {
          label: '所属ロール',
          placeholder: '所属ロールを選択してください',
          validation: {
            required: '所属ロールは必須です'
          }
        },
        post: {
          label: '所属ポスト',
          placeholder: '所属ポストを選択してください',
          validation: {
            required: '所属ポストは必須です'
          }
        },
        remark: {
          label: '備考',
          placeholder: '備考を入力してください'
        }
      },

      // アクションボタン
      actions: {
        add: 'ユーザー追加',
        edit: 'ユーザー編集',
        'delete': 'ユーザー削除',
        resetPassword: 'パスワードリセット',
        export: 'ユーザーエクスポート'
      },

      // メッセージプロンプト
      messages: {
        confirmDelete: '選択したユーザーを削除しますか？',
        confirmResetPassword: '選択したユーザーのパスワードをリセットしますか？',
        confirmToggleStatus: 'このユーザーを{action}しますか？',
        deleteSuccess: 'ユーザー削除成功',
        deleteFailed: 'ユーザー削除失敗',
        saveSuccess: 'ユーザー情報保存成功',
        saveFailed: 'ユーザー情報保存失敗',
        resetPasswordSuccess: 'パスワードリセット成功',
        resetPasswordFailed: 'パスワードリセット失敗',
        importSuccess: 'ユーザーインポート成功',
        importFailed: 'ユーザーインポート失敗',
        exportSuccess: 'ユーザーエクスポート成功',
        exportFailed: 'ユーザーエクスポート失敗',
        toggleStatusSuccess: 'ステータス変更成功',
        toggleStatusFailed: 'ステータス変更失敗',
        cannotDeleteAdmin: '管理者ユーザーは削除できません！',
        cannotEditAdmin: '管理者ユーザーは編集できません！',
        cannotUpdateAdminStatus: '管理者ユーザーのステータスは変更できません！',
        cannotResetAdminPassword: '管理者ユーザーのパスワードはリセットできません！',
        cannotUnlockAdmin: '管理者ユーザーはロック解除できません！',
        cannotAllocateRole: '管理者ユーザーはロール割り当てできません！',
        cannotAllocateDept: '管理者ユーザーは部門割り当てできません！',
        cannotAllocatePost: '管理者ユーザーはポスト割り当てできません！',
        statusUpdateSuccess: 'ステータス更新成功',
        statusUpdateFailed: 'ステータス更新失敗',
        lockStatusUpdateSuccess: 'ロックステータス更新成功',
        lockStatusUpdateFailed: 'ロックステータス更新失敗'
      },

      // テーブル表示テキスト
      table: {
        notLocked: 'ロックされていません',
        none: 'なし',
        queryParams: 'クエリパラメータ',
        importResponseData: 'インポート応答データ',
        parsedData: '解析されたデータ',
        exportFailed: 'エクスポート失敗',
        downloadTemplateFailed: 'テンプレートダウンロード失敗',
        rowClicked: '行がクリックされました',
        toggleFullscreenState: 'フルスクリーン状態切り替え',
        getOptionsFailed: 'オプション取得失敗',
        createUser: 'ユーザー作成',
        updateUser: 'ユーザー更新'
      },

      // インポートのヒント
      importTips: 'Excelワークシート名は「User」である必要があります',

      // タブ
      tab: {
        basic: '基本情報',
        profile: '個人プロフィール',
        role: 'ロール権限',
        dept: '部門ポスト',
        other: 'その他の情報',
        avatar: 'アバター設定',
        loginLog: 'ログインログ',
        operateLog: '操作ログ',
        errorLog: '例外ログ',
        taskLog: 'タスクログ'
      },

      // インポート/エクスポート
      import: {
        title: 'ユーザーインポート',
        template: 'テンプレートダウンロード',
        success: 'インポート成功',
        failed: 'インポート失敗',
        fileName: 'ユーザーデータ'
      },
      export: {
        title: 'ユーザーエクスポート',
        fileName: 'ユーザーデータ',
        success: 'エクスポート成功',
        failed: 'エクスポート失敗'
      },
      template: {
        fileName: 'ユーザーインポートテンプレート',
        downloadFailed: 'テンプレートダウンロード失敗'
      },

      // パスワードリセット
      resetPwd: 'パスワードリセット',
      
      // パスワード変更
      changePassword: {
        success: 'パスワード変更成功',
        failed: 'パスワード変更失敗、再試行してください',
        changeFailed: 'パスワード変更失敗'
      },

      // 割り当て関連
      allocateDept: '部門割り当て',
      allocatePost: 'ポスト割り当て',
      allocateRole: 'ロール割り当て',
      
      roleAllocate: {
        unallocated: '未割り当て',
        allocated: '割り当て済み',
        loadRoleListFailed: 'ロールリスト読み込み失敗',
        startLoadUserRoles: 'ユーザーロール読み込み開始',
        userRolesApiResponse: 'ユーザーロールAPI応答',
        setSelectedRoles: '選択されたロールを設定',
        loadUserRolesFailed: 'ユーザーロール読み込み失敗'
      },
      
      postAllocate: {
        unallocated: '未割り当て',
        allocated: '割り当て済み',
        loadPostListFailed: 'ポストリスト読み込み失敗',
        loadUserPostsFailed: 'ユーザーポスト読み込み失敗'
      }
    }
  }
}
