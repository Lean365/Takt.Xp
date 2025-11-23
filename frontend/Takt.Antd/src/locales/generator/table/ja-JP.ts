export default {
  generator: {
    table: {
      title: 'コード生成',
      list: {
        title: 'コード生成リスト',
        search: {
          name: 'テーブル名',
          comment: 'テーブルコメント'
        },
        table: {
          tableId: 'テーブルID',
          databaseName: 'データベース名',
          tableName: 'テーブル名',
          tableComment: 'テーブルコメント',
          className: 'クラス名',
          namespace: '名前空間',
          baseNamespace: '基本名前空間',
          csharpTypeName: 'C#型名',
          parentTableName: '親テーブル名',
          parentTableFkName: '親テーブル外部キー',
          status: '状態',
          templateType: 'テンプレートタイプ',
          moduleName: 'モジュール名',
          businessName: 'ビジネス名',
          functionName: '機能名',
          author: '作成者',
          genMode: '生成モード',
          genPath: '生成パス',
          options: 'オプション',
          createBy: '作成者',
          createTime: '作成日時',
          updateBy: '更新者',
          updateTime: '更新日時',
          remark: '備考',
          isDeleted: '削除済み'
        },
        actions: {
          create: '新規作成',
          edit: '編集',
          delete: '削除',
          view: '表示',
          generate: 'コード生成',
          sync: 'テーブル同期',
          import: 'インポート',
          export: 'エクスポート',
          template: 'テンプレートダウンロード',
          refresh: '更新'
        },
        status: {
          enabled: '有効',
          disabled: '無効'
        }
      },
      form: {
        title: 'コード生成フォーム',
        tab: {
          basic: '基本情報',
          generate: '生成情報',
          field: 'フィールド情報'
        },
        basic: {
          title: '基本情報',
          tableName: 'テーブル名',
          tableComment: 'テーブルコメント',
          className: 'クラス名',
          namespace: '名前空間',
          baseNamespace: '基本名前空間',
          csharpTypeName: 'C#型名',
          parentTableName: '親テーブル名',
          parentTableFkName: '親テーブル外部キー',
          status: '状態',
          author: '作成者',
          remark: '備考'
        },
        generate: {
          title: '生成情報',
          moduleName: 'モジュール名',
          packageName: 'パッケージパス',
          businessName: 'ビジネス名',
          functionName: '機能名',
          parentMenuId: '親メニュー',
          tplCategory: 'テンプレートタイプ',
          genPath: '生成パス',
          options: '生成オプション',
          tplCategoryOptions: {
            crud: '単一テーブル（CRUD）',
            tree: 'ツリーテーブル（CRUD）',
            sub: 'マスターディテールテーブル（CRUD）'
          },
          optionsItems: {
            treeCode: 'ツリーコードフィールド',
            treeParentCode: 'ツリー親コードフィールド',
            treeName: 'ツリー名フィールド',
            parentMenuId: '親メニュー',
            query: '検索',
            add: '追加',
            edit: '編集',
            delete: '削除',
            import: 'インポート',
            export: 'エクスポート'
          }
        },
        field: {
          title: 'フィールド情報',
          columnName: 'カラム名',
          columnComment: 'カラムコメント',
          columnType: 'カラムタイプ',
          csharpType: 'C#型',
          csharpField: 'C#フィールド',
          isRequired: '必須',
          isInsert: '挿入',
          isEdit: '編集',
          isList: 'リスト',
          isQuery: '検索',
          queryType: '検索タイプ',
          htmlType: '表示タイプ',
          dictType: '辞書タイプ',
          queryTypeOptions: {
            EQ: '等しい',
            NE: '等しくない',
            GT: 'より大きい',
            GTE: '以上',
            LT: 'より小さい',
            LTE: '以下',
            LIKE: '含む',
            BETWEEN: '範囲',
            IN: '含まれる'
          },
          htmlTypeOptions: {
            input: '入力フィールド',
            textarea: 'テキストエリア',
            select: 'ドロップダウン',
            radio: 'ラジオボタン',
            checkbox: 'チェックボックス',
            datetime: '日時',
            imageUpload: '画像アップロード',
            fileUpload: 'ファイルアップロード',
            editor: 'リッチテキストエディタ'
          }
        },
        buttons: {
          submit: '送信',
          cancel: 'キャンセル'
        },
        name: 'テーブル名',
        comment: 'テーブルコメント',
        className: 'クラス名',
        namespace: '名前空間',
        baseNamespace: '基本名前空間',
        csharpTypeName: 'C#型名',
        parentTableName: '親テーブル名',
        parentTableFkName: '親テーブル外部キー',
        moduleName: 'モジュール名',
        businessName: 'ビジネス名',
        functionName: '機能名',
        author: '作成者',
        genMode: '生成モード',
        genPath: '生成パス',
        options: 'オプション'
      },
      detail: {
        title: 'コード生成詳細',
        basic: {
          title: '基本情報',
          tableName: 'テーブル名',
          tableComment: 'テーブルコメント',
          className: 'クラス名',
          namespace: '名前空間',
          baseNamespace: '基本名前空間',
          csharpTypeName: 'C#型名',
          parentTableName: '親テーブル名',
          parentTableFkName: '親テーブル外部キー',
          status: '状態',
          createTime: '作成日時',
          updateTime: '更新日時'
        },
        generate: {
          title: '生成情報',
          moduleName: 'モジュール名',
          packageName: 'パッケージパス',
          businessName: 'ビジネス名',
          functionName: '機能名',
          parentMenuId: '親メニュー',
          tplCategory: 'テンプレートタイプ',
          genPath: '生成パス',
          options: '生成オプション'
        },
        field: {
          title: 'フィールド情報',
          columnName: 'カラム名',
          columnComment: 'カラムコメント',
          columnType: 'カラムタイプ',
          csharpType: 'C#型',
          csharpField: 'C#フィールド',
          isRequired: '必須',
          isInsert: '挿入',
          isEdit: '編集',
          isList: 'リスト',
          isQuery: '検索',
          queryType: '検索タイプ',
          htmlType: '表示タイプ',
          dictType: '辞書タイプ'
        },
        actions: {
          edit: '編集',
          back: '戻る'
        },
        columnInfo: 'カラム情報',
        javaType: 'Java型',
        javaField: 'Javaフィールド',
        yes: 'はい',
        no: 'いいえ'
      },
      name: 'テーブル名',
      comment: 'テーブルコメント',
      databaseName: 'データベース名',
      className: 'クラス名',
      namespace: '名前空間',
      baseNamespace: '基本名前空間',
      csharpTypeName: 'C#型名',
      parentTableName: '親テーブル名',
      parentTableFkName: '親テーブル外部キー',
      status: '状態',
      templateType: 'テンプレートタイプ',
      moduleName: 'モジュール名',
      businessName: 'ビジネス名',
      functionName: '機能名',
      author: '作成者',
      genMode: '生成モード',
      genPath: '生成パス',
      options: 'オプション',
      createBy: '作成者',
      createTime: '作成日時',
      updateBy: '更新者',
      updateTime: '更新日時',
      remark: '備考',
      isDeleted: '削除済み',
      placeholder: {
        name: 'テーブル名を入力してください',
        comment: 'テーブルコメントを入力してください'
      },
      preview: {
        title: 'コードプレビュー',
        copy: 'コードをコピー',
        download: 'コードをダウンロード',
        showLineNumbers: '行番号を表示',
        hideLineNumbers: '行番号を非表示',
        copySuccess: 'コピー成功',
        copyFailed: 'コピー失敗',
        downloadSuccess: 'ダウンロード成功',
        downloadFailed: 'ダウンロード失敗',
        tab: {
          java: 'Javaコード',
          vue: 'Vueコード',
          sql: 'SQLコード',
          domain: 'エンティティクラス',
          mapper: 'Mapperインターフェース',
          mapperXml: 'Mapper XML',
          service: 'サービスインターフェース',
          serviceImpl: 'サービス実装',
          controller: 'コントローラー',
          api: 'APIインターフェース',
          index: 'リストページ',
          form: 'フォームページ'
        },
        entities: {
          title: 'エンティティクラスコード'
        },
        services: {
          title: 'サービスインターフェースコード'
        },
        controllers: {
          title: 'コントローラーコード'
        },
        vue: {
          title: 'Vueコード'
        },
        dtos: {
          title: 'DTOコード'
        },
        types: {
          title: '型定義コード'
        },
        locales: {
          title: '翻訳コード'
        }
      },
      import: {
        title: 'テーブルインポート',
        database: 'データベース',
        table: {
          name: 'テーブル名',
          comment: 'テーブルコメント',
          action: '操作'
        },
        column: {
          title: 'カラムインポート',
          tableName: 'テーブル名',
          tableId: 'テーブルID',
          columnName: 'カラム名',
          propertyName: 'プロパティ名',
          columnType: 'カラムタイプ',
          propertyType: 'プロパティタイプ',
          isNullable: 'NULL許可',
          isPrimaryKey: '主キー',
          isAutoIncrement: '自動増分',
          defaultValue: 'デフォルト値',
          columnComment: 'カラムコメント',
          value: '値',
          decimalDigits: '小数点桁数',
          scale: 'スケール',
          isArray: '配列',
          isJson: 'Json',
          isUnsigned: '符号なし',
          createTableFieldSort: 'テーブルフィールドソート',
          insertServerTime: 'サーバー挿入時間',
          insertSql: '挿入SQL',
          updateServerTime: 'サーバー更新時間',
          updateSql: '更新SQL',
          sqlParameterDbType: 'SQLパラメータDBタイプ'
        }
      },
      message: {
        generateSuccess: 'コード生成成功',
        generateFailed: 'コード生成失敗',
        syncSuccess: 'テーブル同期成功',
        syncFailed: 'テーブル同期失敗',
        importSuccess: 'インポート成功',
        importFailed: 'インポート失敗',
        exportSuccess: 'エクスポート成功',
        exportFailed: 'エクスポート失敗',
        templateSuccess: 'テンプレートダウンロード成功',
        templateFailed: 'テンプレートダウンロード失敗',
        selectDatabase: '先にデータベースを選択してください',
        selectTable: 'インポートするテーブルを選択してください',
        tableNameRequired: 'テーブル名は必須です',
        importTimeout: 'インポートタイムアウト、後でもう一度お試しください'
      },
      tab: {
        basic: '基本情報',
        generate: '生成情報',
        field: 'フィールド情報'
      },
      required: {
        name: 'テーブル名を入力してください',
        comment: 'テーブルコメントを入力してください',
        className: 'クラス名を入力してください',
        namespace: '名前空間を入力してください',
        baseNamespace: '基本名前空間を入力してください',
        csharpTypeName: 'C#型名を入力してください',
        moduleName: 'モジュール名を入力してください',
        businessName: 'ビジネス名を入力してください',
        functionName: '機能名を入力してください',
        author: '作成者名を入力してください',
        genMode: '生成モードを選択してください',
        genPath: '生成パスを入力してください'
      }
    }
  }
} 