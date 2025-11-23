export default {
  routine: {
    core: {
      numberrule: {
        // ページタイトル
        title: '番号規則管理',
        
        // タブ
        tabs: {
          basicInfo: '基本情報',
          numberConfig: '番号設定',
          advancedConfig: '高度な設定',
          otherInfo: 'その他の情報'
        },

        // フィールドラベル
        fields: {
          companyCode: {
            label: '会社コード',
            placeholder: '会社コードを入力してください',
            required: '会社コードを入力してください',
            length: '会社コードの長さは1-20文字の間である必要があります'
          },
          numberRuleName: {
            label: '番号規則名',
            placeholder: '番号規則名を入力してください',
            required: '番号規則名を入力してください',
            length: '番号規則名の長さは1-100文字の間である必要があります'
          },
          numberRuleCode: {
            label: '番号規則コード',
            placeholder: '番号規則コードを入力してください',
            required: '番号規則コードを入力してください',
            length: '番号規則コードの長さは1-50文字の間である必要があります'
          },
          deptCode: {
            label: '部門コード',
            placeholder: '部門コードを入力してください',
            required: '部門コードを入力してください',
            length: '部門コードの長さは1-20文字の間である必要があります'
          },
          numberRuleShortCode: {
            label: '番号規則略称',
            placeholder: '番号規則略称を入力してください',
            required: '番号規則略称を入力してください',
            length: '番号規則略称の長さは1-4文字の間である必要があります'
          },
          numberRuleType: {
            label: '番号規則タイプ',
            placeholder: '番号規則タイプを選択してください',
            required: '番号規則タイプを選択してください'
          },
          status: {
            label: 'ステータス',
            placeholder: 'ステータスを選択してください',
            required: 'ステータスを選択してください'
          },
          numberRuleDescription: {
            label: '番号規則説明',
            placeholder: '番号規則説明を入力してください'
          },
          numberPrefix: {
            label: '番号プレフィックス',
            placeholder: '番号プレフィックスを入力してください'
          },
          numberSuffix: {
            label: '番号サフィックス',
            placeholder: '番号サフィックスを入力してください'
          },
          dateFormat: {
            label: '日付フォーマット',
            placeholder: '日付フォーマットを選択してください',
            required: '日付フォーマットを選択してください'
          },
          sequenceLength: {
            label: 'シーケンス長',
            placeholder: 'シーケンス長を入力してください',
            required: 'シーケンス長を入力してください'
          },
          sequenceStart: {
            label: 'シーケンス開始値',
            placeholder: 'シーケンス開始値を入力してください',
            required: 'シーケンス開始値を入力してください'
          },
          sequenceStep: {
            label: 'シーケンスステップ',
            placeholder: 'シーケンスステップを入力してください',
            required: 'シーケンスステップを入力してください'
          },
          currentSequence: {
            label: '現在のシーケンス',
            placeholder: '現在のシーケンスを入力してください'
          },
          sequenceResetRule: {
            label: 'シーケンスリセット規則',
            placeholder: 'シーケンスリセット規則を選択してください'
          },
          includeCompanyCode: {
            label: '会社コードを含む',
            placeholder: '会社コードを含むかどうかを選択してください'
          },
          includeDepartmentCode: {
            label: '部門コードを含む',
            placeholder: '部門コードを含むかどうかを選択してください'
          },
          includeRevisionNumber: {
            label: 'リビジョン番号を含む',
            placeholder: 'リビジョン番号を含むかどうかを選択してください'
          },
          includeYear: {
            label: '年を含む',
            placeholder: '年を含むかどうかを選択してください'
          },
          includeMonth: {
            label: '月を含む',
            placeholder: '月を含むかどうかを選択してください'
          },
          includeDay: {
            label: '日を含む',
            placeholder: '日を含むかどうかを選択してください'
          },
          allowDuplicate: {
            label: '重複を許可',
            placeholder: '重複を許可するかどうかを選択してください'
          },
          duplicateCheckScope: {
            label: '重複チェック範囲',
            placeholder: '重複チェック範囲を選択してください'
          },
          orderNum: {
            label: '順序番号',
            placeholder: '順序番号を入力してください'
          }
        },

        // アクションボタン
        actions: {
          add: '番号規則を追加',
          edit: '番号規則を編集',
          delete: '番号規則を削除',
          batchDelete: '一括削除',
          export: 'エクスポート',
          import: 'インポート',
          downloadTemplate: 'テンプレートをダウンロード',
          preview: 'プレビュー',
          generate: '番号を生成',
          validate: '規則を検証'
        },

        // フォームプレースホルダー
        placeholder: {
          search: '番号規則名またはコードを入力してください',
          selectAll: 'すべて選択',
          clear: 'クリア'
        },

        // 操作結果メッセージ
        message: {
          success: {
            add: '番号規則の追加に成功しました',
            edit: '番号規則の更新に成功しました',
            delete: '番号規則の削除に成功しました',
            batchDelete: '一括削除が正常に完了しました',
            export: 'エクスポートが正常に完了しました',
            import: 'インポートが正常に完了しました',
            generate: '番号の生成に成功しました',
            validate: '検証が成功しました'
          },
          failed: {
            add: '番号規則の追加に失敗しました',
            edit: '番号規則の更新に失敗しました',
            delete: '番号規則の削除に失敗しました',
            batchDelete: '一括削除に失敗しました',
            export: 'エクスポートに失敗しました',
            import: 'インポートに失敗しました',
            generate: '番号の生成に失敗しました',
            validate: '検証に失敗しました'
          },
          confirm: {
            delete: '選択した番号規則を削除してもよろしいですか？',
            batchDelete: '選択した番号規則を一括削除してもよろしいですか？'
          }
        },

        // 詳細ページ
        detail: {
          title: '番号規則詳細',
          basicInfo: '基本情報',
          numberConfig: '番号設定',
          advancedConfig: '高度な設定',
          otherInfo: 'その他の情報'
        },

        // テーブル列タイトル
        columns: {
          numberRuleName: '番号規則名',
          numberRuleCode: '番号規則コード',
          numberRuleShortCode: '番号規則略称',
          numberRuleType: '番号規則タイプ',
          deptCode: '部門コード',
          status: 'ステータス',
          createTime: '作成時間',
          updateTime: '更新時間',
          remark: '備考',
          actions: '操作'
        },

        // 番号規則タイプ
        types: {
          daily: '日常業務',
          hr: '人事',
          finance: '財務会計',
          logistics: '後方支援管理',
          production: '生産管理',
          workflow: 'ワークフロー'
        },

        // ステータス
        status: {
          normal: '正常',
          disabled: '無効'
        },

        // シーケンスリセット規則
        resetRules: {
          none: 'リセットなし',
          yearly: '年次リセット',
          monthly: '月次リセット',
          daily: '日次リセット'
        },

        // 重複チェック範囲
        checkScopes: {
          global: 'グローバル',
          yearly: '年次',
          monthly: '月次',
          daily: '日次'
        },

        // 日付フォーマットオプション
        dateFormats: {
          yyyy: 'yyyy',
          yyyyMM: 'yyyyMM',
          yyyyMMdd: 'yyyyMMdd',
          yyyyMMddHH: 'yyyyMMddHH',
          yyyyMMddHHmm: 'yyyyMMddHHmm',
          yyyyMMddHHmmss: 'yyyyMMddHHmmss'
        }
      }
    }
  }
}
