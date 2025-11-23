export default {
  accounting: {
    financial: {
      company: {
        // Page title
        title: '会社管理',
        
        // Tabs
        tabs: {
          basicInfo: '基本情報',
          detailInfo: '詳細情報',
          financialInfo: '財務情報'
        },

        // Field labels
        fields: {
          companyCode: {
            label: '会社コード',
            placeholder: '会社コードを入力してください',
            required: '会社コードを入力してください'
          },
          companyName: {
            label: '会社名',
            placeholder: '会社名を入力してください',
            required: '会社名を入力してください'
          },
          companyShortName: {
            label: '会社略称',
            placeholder: '会社略称を入力してください'
          },
          companyTaxNumber: {
            label: '税務番号',
            placeholder: '税務番号を入力してください'
          },
          companyNature: {
            label: '企業性質',
            placeholder: '企業性質を選択してください',
            required: '企業性質を選択してください'
          },
          companyIndustryType: {
            label: '業界タイプ',
            placeholder: '業界タイプを選択してください'
          },
          companyCurrency: {
            label: '通貨',
            placeholder: '通貨を選択してください',
            required: '通貨を選択してください'
          },
          companyLanguage: {
            label: '言語',
            placeholder: '言語を選択してください',
            required: '言語を選択してください'
          },
          companyPhone: {
            label: '電話番号',
            placeholder: '電話番号を入力してください'
          },
          companyEmail: {
            label: 'メールアドレス',
            placeholder: 'メールアドレスを入力してください'
          },
          companyWebsite: {
            label: 'ウェブサイト',
            placeholder: 'ウェブサイトを入力してください'
          },
          companyScale: {
            label: '会社規模',
            placeholder: '会社規模を入力してください'
          },
          companyAddress: {
            label: '住所',
            placeholder: '住所を入力してください'
          },
          companyName1: {
            label: '会社名1',
            placeholder: '会社名1を入力してください'
          },
          companyName2: {
            label: '会社名2',
            placeholder: '会社名2を入力してください'
          },
          companyName3: {
            label: '会社名3',
            placeholder: '会社名3を入力してください'
          },
          companyLegalRepresentative: {
            label: '法定代表者',
            placeholder: '法定代表者を入力してください'
          },
          companyRegisteredCapital: {
            label: '登録資本金',
            placeholder: '登録資本金を入力してください'
          },
          companyBusinessLicense: {
            label: '営業許可証',
            placeholder: '営業許可証番号を入力してください'
          },
          companyOrganizationCode: {
            label: '組織コード',
            placeholder: '組織コードを入力してください'
          },
          companyUnifiedCreditCode: {
            label: '統一信用コード',
            placeholder: '統一信用コードを入力してください'
          },
          companyCity: {
            label: '都市',
            placeholder: '都市を入力してください'
          },
          companyRegion: {
            label: '地域',
            placeholder: '地域を入力してください'
          },
          companyPostalCode: {
            label: '郵便番号',
            placeholder: '郵便番号を入力してください'
          },
          companyCountry: {
            label: '国',
            placeholder: '国を入力してください'
          },
          companyFax: {
            label: 'FAX',
            placeholder: 'FAX番号を入力してください'
          },
          establishmentDate: {
            label: '設立日',
            placeholder: '設立日を選択してください'
          },
          dissolutionDate: {
            label: '解散日',
            placeholder: '解散日を選択してください'
          },
          orderNum: {
            label: '順序番号',
            placeholder: '順序番号を入力してください'
          },
          status: {
            label: 'ステータス',
            placeholder: 'ステータスを選択してください',
            required: 'ステータスを選択してください'
          },
          companyFiscalYearVariant: {
            label: '会計年度バリアント',
            placeholder: '会計年度バリアントを入力してください'
          },
          companyChartOfAccounts: {
            label: '勘定科目表',
            placeholder: '勘定科目表を入力してください'
          },
          companyFinancialManagementArea: {
            label: '財務管理エリア',
            placeholder: '財務管理エリアを入力してください'
          },
          companyMaxExchangeRateDeviation: {
            label: '最大為替レート偏差',
            placeholder: '最大為替レート偏差を入力してください'
          },
          companyAllocationIdentifier: {
            label: '配分識別子',
            placeholder: '配分識別子を入力してください'
          },
          companyRelatedCompany: {
            label: '関連会社',
            placeholder: '関連会社を入力してください'
          },
          companyRelatedPlant: {
            label: '関連工場',
            placeholder: '関連工場を入力してください'
          },
          companyAddressNumber: {
            label: '住所番号',
            placeholder: '住所番号を入力してください'
          },
          remark: {
            label: '備考',
            placeholder: '備考を入力してください'
          }
        },

        // Action buttons
        actions: {
          add: '会社追加',
          edit: '会社編集',
          delete: '会社削除',
          batchDelete: '一括削除',
          export: 'エクスポート',
          import: 'インポート',
          downloadTemplate: 'テンプレートダウンロード',
          reset: 'リセット',
          search: '検索'
        },

        // Table column titles
        columns: {
          companyCode: '会社コード',
          companyName: '会社名',
          companyShortName: '会社略称',
          companyTaxNumber: '税務番号',
          companyNature: '企業性質',
          companyIndustryType: '業界タイプ',
          companyCurrency: '通貨',
          companyLanguage: '言語',
          companyPhone: '電話番号',
          companyEmail: 'メールアドレス',
          companyWebsite: 'ウェブサイト',
          companyScale: '会社規模',
          companyAddress: '住所',
          companyName1: '会社名1',
          companyName2: '会社名2',
          companyName3: '会社名3',
          companyLegalRepresentative: '法定代表者',
          companyRegisteredCapital: '登録資本金',
          companyBusinessLicense: '営業許可証',
          companyOrganizationCode: '組織コード',
          companyUnifiedCreditCode: '統一信用コード',
          companyCity: '都市',
          companyRegion: '地域',
          companyPostalCode: '郵便番号',
          companyCountry: '国',
          companyFax: 'FAX',
          establishmentDate: '設立日',
          dissolutionDate: '解散日',
          orderNum: '順序番号',
          status: 'ステータス',
          companyFiscalYearVariant: '会計年度バリアント',
          companyChartOfAccounts: '勘定科目表',
          companyFinancialManagementArea: '財務管理エリア',
          companyMaxExchangeRateDeviation: '最大為替レート偏差',
          companyAllocationIdentifier: '配分識別子',
          companyRelatedCompany: '関連会社',
          companyRelatedPlant: '関連工場',
          companyAddressNumber: '住所番号',
          remark: '備考',
          createBy: '作成者',
          createTime: '作成時間',
          updateBy: '更新者',
          updateTime: '更新時間',
          deleteBy: '削除者',
          deleteTime: '削除時間'
        },

        // Messages
        messages: {
          addSuccess: '会社が正常に追加されました',
          updateSuccess: '会社が正常に更新されました',
          deleteSuccess: '会社が正常に削除されました',
          batchDeleteSuccess: '会社が正常に一括削除されました',
          deleteConfirm: '選択した会社を削除してもよろしいですか？',
          batchDeleteConfirm: '選択した会社を一括削除してもよろしいですか？',
          loadFailed: 'データの読み込みに失敗しました',
          submitFailed: '送信に失敗しました',
          exportSuccess: 'エクスポートが成功しました',
          importSuccess: 'インポートが成功しました',
          importFailed: 'インポートに失敗しました',
          downloadTemplateSuccess: 'テンプレートのダウンロードが成功しました'
        },

        // Query conditions
        query: {
          companyCode: '会社コード',
          companyName: '会社名',
          companyNature: '企業性質',
          companyIndustryType: '業界タイプ',
          companyCurrency: '通貨',
          status: 'ステータス'
        }
      }
    }
  }
}