export default {
  accounting: {
    financial: {
      company: {
        // 頁面標題
        title: '公司管理',
        
        // 標籤頁
        tabs: {
          basicInfo: '基本資訊',
          detailInfo: '詳細資訊',
          financialInfo: '財務資訊'
        },

        // 欄位標籤
        fields: {
          companyCode: {
            label: '公司代碼',
            placeholder: '請輸入公司代碼',
            required: '請輸入公司代碼'
          },
          companyName: {
            label: '公司名稱',
            placeholder: '請輸入公司名稱',
            required: '請輸入公司名稱'
          },
          companyShortName: {
            label: '公司簡稱',
            placeholder: '請輸入公司簡稱'
          },
          companyTaxNumber: {
            label: '稅務登記號',
            placeholder: '請輸入稅務登記號'
          },
          companyNature: {
            label: '企業性質',
            placeholder: '請選擇企業性質',
            required: '請選擇企業性質'
          },
          companyIndustryType: {
            label: '行業類型',
            placeholder: '請選擇行業類型'
          },
          companyCurrency: {
            label: '幣種',
            placeholder: '請選擇幣種',
            required: '請選擇幣種'
          },
          companyLanguage: {
            label: '語言',
            placeholder: '請選擇語言',
            required: '請選擇語言'
          },
          companyPhone: {
            label: '聯繫電話',
            placeholder: '請輸入聯繫電話'
          },
          companyEmail: {
            label: '郵箱地址',
            placeholder: '請輸入郵箱地址'
          },
          companyWebsite: {
            label: '公司網站',
            placeholder: '請輸入公司網站'
          },
          companyScale: {
            label: '公司規模',
            placeholder: '請輸入公司規模'
          },
          companyAddress: {
            label: '公司地址',
            placeholder: '請輸入公司地址'
          },
          companyName1: {
            label: '公司名稱1',
            placeholder: '請輸入公司名稱1'
          },
          companyName2: {
            label: '公司名稱2',
            placeholder: '請輸入公司名稱2'
          },
          companyName3: {
            label: '公司名稱3',
            placeholder: '請輸入公司名稱3'
          },
          companyLegalRepresentative: {
            label: '法定代表人',
            placeholder: '請輸入法定代表人'
          },
          companyRegisteredCapital: {
            label: '註冊資本',
            placeholder: '請輸入註冊資本'
          },
          companyBusinessLicense: {
            label: '營業執照號',
            placeholder: '請輸入營業執照號'
          },
          companyOrganizationCode: {
            label: '組織機構代碼',
            placeholder: '請輸入組織機構代碼'
          },
          companyUnifiedCreditCode: {
            label: '統一社會信用代碼',
            placeholder: '請輸入統一社會信用代碼'
          },
          companyCity: {
            label: '所在城市',
            placeholder: '請輸入所在城市'
          },
          companyRegion: {
            label: '所在地區',
            placeholder: '請輸入所在地區'
          },
          companyPostalCode: {
            label: '郵政編碼',
            placeholder: '請輸入郵政編碼'
          },
          companyCountry: {
            label: '所在國家',
            placeholder: '請輸入所在國家'
          },
          companyFax: {
            label: '傳真號碼',
            placeholder: '請輸入傳真號碼'
          },
          establishmentDate: {
            label: '成立日期',
            placeholder: '請選擇成立日期'
          },
          dissolutionDate: {
            label: '解散日期',
            placeholder: '請選擇解散日期'
          },
          orderNum: {
            label: '排序號',
            placeholder: '請輸入排序號'
          },
          status: {
            label: '狀態',
            placeholder: '請選擇狀態',
            required: '請選擇狀態'
          },
          companyFiscalYearVariant: {
            label: '會計年度變式',
            placeholder: '請輸入會計年度變式'
          },
          companyChartOfAccounts: {
            label: '會計科目表',
            placeholder: '請輸入會計科目表'
          },
          companyFinancialManagementArea: {
            label: '財務管理範圍',
            placeholder: '請輸入財務管理範圍'
          },
          companyMaxExchangeRateDeviation: {
            label: '最大匯率偏差',
            placeholder: '請輸入最大匯率偏差'
          },
          companyAllocationIdentifier: {
            label: '分配標識符',
            placeholder: '請輸入分配標識符'
          },
          companyRelatedCompany: {
            label: '關聯公司',
            placeholder: '請輸入關聯公司'
          },
          companyRelatedPlant: {
            label: '關聯工廠',
            placeholder: '請輸入關聯工廠'
          },
          companyAddressNumber: {
            label: '地址編號',
            placeholder: '請輸入地址編號'
          },
          remark: {
            label: '備註',
            placeholder: '請輸入備註'
          }
        },

        // 操作按鈕
        actions: {
          add: '新增公司',
          edit: '編輯公司',
          delete: '刪除公司',
          batchDelete: '批量刪除',
          export: '導出',
          import: '導入',
          downloadTemplate: '下載模板',
          reset: '重置',
          search: '查詢'
        },

        // 表格列標題
        columns: {
          companyCode: '公司代碼',
          companyName: '公司名稱',
          companyShortName: '公司簡稱',
          companyTaxNumber: '稅務登記號',
          companyNature: '企業性質',
          companyIndustryType: '行業類型',
          companyCurrency: '幣種',
          companyLanguage: '語言',
          companyPhone: '聯繫電話',
          companyEmail: '郵箱地址',
          companyWebsite: '公司網站',
          companyScale: '公司規模',
          companyAddress: '公司地址',
          companyName1: '公司名稱1',
          companyName2: '公司名稱2',
          companyName3: '公司名稱3',
          companyLegalRepresentative: '法定代表人',
          companyRegisteredCapital: '註冊資本',
          companyBusinessLicense: '營業執照號',
          companyOrganizationCode: '組織機構代碼',
          companyUnifiedCreditCode: '統一社會信用代碼',
          companyCity: '所在城市',
          companyRegion: '所在地區',
          companyPostalCode: '郵政編碼',
          companyCountry: '所在國家',
          companyFax: '傳真號碼',
          establishmentDate: '成立日期',
          dissolutionDate: '解散日期',
          orderNum: '排序號',
          status: '狀態',
          companyFiscalYearVariant: '會計年度變式',
          companyChartOfAccounts: '會計科目表',
          companyFinancialManagementArea: '財務管理範圍',
          companyMaxExchangeRateDeviation: '最大匯率偏差',
          companyAllocationIdentifier: '分配標識符',
          companyRelatedCompany: '關聯公司',
          companyRelatedPlant: '關聯工廠',
          companyAddressNumber: '地址編號',
          remark: '備註',
          createBy: '創建人',
          createTime: '創建時間',
          updateBy: '更新人',
          updateTime: '更新時間',
          deleteBy: '刪除人',
          deleteTime: '刪除時間'
        },

        // 消息提示
        messages: {
          addSuccess: '新增公司成功',
          updateSuccess: '更新公司成功',
          deleteSuccess: '刪除公司成功',
          batchDeleteSuccess: '批量刪除公司成功',
          deleteConfirm: '確定要刪除選中的公司嗎？',
          batchDeleteConfirm: '確定要批量刪除選中的公司嗎？',
          loadFailed: '加載數據失敗',
          submitFailed: '提交失敗',
          exportSuccess: '導出成功',
          importSuccess: '導入成功',
          importFailed: '導入失敗',
          downloadTemplateSuccess: '下載模板成功'
        },

        // 查詢條件
        query: {
          companyCode: '公司代碼',
          companyName: '公司名稱',
          companyNature: '企業性質',
          companyIndustryType: '行業類型',
          companyCurrency: '幣種',
          status: '狀態'
        }
      }
    }
  }
}
