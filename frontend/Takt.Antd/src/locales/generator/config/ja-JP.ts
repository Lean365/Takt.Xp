export default {
    generator: {
      config: {
        title: 'コード生成設定',  
        fields: {
            genConfigName: '設定名',
            author: '作成者',
            moduleName: 'モジュール名',
            projectName: 'プロジェクト名',
            businessName: 'ビジネス名',
            functionName: '機能名',
            genMethod: '生成方式',
            genTplType: 'テンプレートタイプ',
            genPath: 'パス',
            options: 'オプション',
            status: 'ステータス',
            dateRange: '日付範囲',
        },
        placeholder: {
            genConfigName: '設定名を入力してください',
            author: '作成者を入力してください',
            moduleName: 'モジュール名を入力してください',
            projectName: 'プロジェクト名を入力してください',
            businessName: 'ビジネス名を入力してください',
            functionName: '機能名を入力してください',
            genMethod: '生成方式を選択してください',
            genTplType: 'テンプレートタイプを選択してください',
            genPath: 'パスを入力してください',
            options: 'オプションを入力してください',
            status: 'ステータスを選択してください',
            dateRange: '日付範囲を選択してください',
            validation: {
                length: '{{field}}の長さは{{length}}を超えることはできません',
                required: '{{field}}を入力してください',
                minlength: '{{field}}の長さは{{min}}より短くすることはできません',
                maxlength: '{{field}}の長さは{{max}}より長くすることはできません',
                pattern: '{{field}}の形式が正しくありません',
            }
        },
        messages: {
            success: '操作が成功しました',
            error: '操作が失敗しました',
            warning: '操作警告',
            info: '操作情報',
        }
    }
} }