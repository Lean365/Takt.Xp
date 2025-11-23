export default {
    generator: {
      template: {
        title: 'コード生成テンプレート',  
        fields: {
            templateName: 'テンプレート名',
            templateOrmType: 'ORMフレームワーク',
            templateCodeType: 'コードタイプ',
            templateCategory: 'テンプレートカテゴリ',
            templateLanguage: 'プログラミング言語',
            templateVersion: 'バージョン番号',
            fileName: 'ファイル名',
            templateContent: 'テンプレート内容',
            status: 'ステータス',
            remark: '備考',
        },
        placeholder: {
            templateName: 'テンプレート名を入力してください',
            templateOrmType: 'ORMフレームワークを選択してください',
            templateCodeType: 'コードタイプを選択してください',
            templateCategory: 'テンプレートカテゴリを選択してください',
            templateLanguage: 'プログラミング言語を選択してください',
            templateVersion: 'バージョン番号を入力してください',
            fileName: 'ファイル名を入力してください',
            templateContent: 'テンプレート内容を入力してください',
            remark: '備考を入力してください',
            validation: {
                required: '{field}を入力してください',
                length: '{field}の長さは{length}文字を超えることはできません',
                minLength: '{field}の長さは{min}文字より短くすることはできません',
                pascalCase: '{field}はパスカルケース（最初の文字は大文字、文字のみ）を使用する必要があります'
            }
        },
        dialog: {
            create: 'テンプレート追加',
            update: 'テンプレート修正'
        },
        messages: {
            success: '操作が成功しました',
            error: '操作が失敗しました',
            warning: '操作警告',
            info: '操作情報',
            confirm: '確認',
        },
    }
} }