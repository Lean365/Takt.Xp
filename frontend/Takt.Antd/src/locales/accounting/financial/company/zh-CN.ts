export default {
  accounting: {
    financial: {
      company: {
        // 页面标题
        title: '公司管理',
        
        // 标签页
        tabs: {
          basicInfo: '基本信息',
          detailInfo: '详细信息',
          financialInfo: '财务信息'
        },

        // 字段标签
        fields: {
          companyCode: {
            label: '公司代码',
            placeholder: '请输入公司代码',
            required: '请输入公司代码'
          },
          companyName: {
            label: '公司名称',
            placeholder: '请输入公司名称',
            required: '请输入公司名称'
          },
          companyShortName: {
            label: '公司简称',
            placeholder: '请输入公司简称'
          },
          companyTaxNumber: {
            label: '税务登记号',
            placeholder: '请输入税务登记号'
          },
          companyNature: {
            label: '企业性质',
            placeholder: '请选择企业性质',
            required: '请选择企业性质'
          },
          companyIndustryType: {
            label: '行业类型',
            placeholder: '请选择行业类型'
          },
          companyCurrency: {
            label: '币种',
            placeholder: '请选择币种',
            required: '请选择币种'
          },
          companyLanguage: {
            label: '语言',
            placeholder: '请选择语言',
            required: '请选择语言'
          },
          companyPhone: {
            label: '联系电话',
            placeholder: '请输入联系电话'
          },
          companyEmail: {
            label: '邮箱地址',
            placeholder: '请输入邮箱地址'
          },
          companyWebsite: {
            label: '公司网站',
            placeholder: '请输入公司网站'
          },
          companyScale: {
            label: '公司规模',
            placeholder: '请输入公司规模'
          },
          companyAddress: {
            label: '公司地址',
            placeholder: '请输入公司地址'
          },
          companyName1: {
            label: '公司名称1',
            placeholder: '请输入公司名称1'
          },
          companyName2: {
            label: '公司名称2',
            placeholder: '请输入公司名称2'
          },
          companyName3: {
            label: '公司名称3',
            placeholder: '请输入公司名称3'
          },
          companyLegalRepresentative: {
            label: '法定代表人',
            placeholder: '请输入法定代表人'
          },
          companyRegisteredCapital: {
            label: '注册资本',
            placeholder: '请输入注册资本'
          },
          companyBusinessLicense: {
            label: '营业执照号',
            placeholder: '请输入营业执照号'
          },
          companyOrganizationCode: {
            label: '组织机构代码',
            placeholder: '请输入组织机构代码'
          },
          companyUnifiedCreditCode: {
            label: '统一社会信用代码',
            placeholder: '请输入统一社会信用代码'
          },
          companyCity: {
            label: '所在城市',
            placeholder: '请输入所在城市'
          },
          companyRegion: {
            label: '所在地区',
            placeholder: '请输入所在地区'
          },
          companyPostalCode: {
            label: '邮政编码',
            placeholder: '请输入邮政编码'
          },
          companyCountry: {
            label: '所在国家',
            placeholder: '请输入所在国家'
          },
          companyFax: {
            label: '传真号码',
            placeholder: '请输入传真号码'
          },
          establishmentDate: {
            label: '成立日期',
            placeholder: '请选择成立日期'
          },
          dissolutionDate: {
            label: '解散日期',
            placeholder: '请选择解散日期'
          },
          orderNum: {
            label: '排序号',
            placeholder: '请输入排序号'
          },
          status: {
            label: '状态',
            placeholder: '请选择状态',
            required: '请选择状态'
          },
          companyFiscalYearVariant: {
            label: '会计年度变式',
            placeholder: '请输入会计年度变式'
          },
          companyChartOfAccounts: {
            label: '会计科目表',
            placeholder: '请输入会计科目表'
          },
          companyFinancialManagementArea: {
            label: '财务管理范围',
            placeholder: '请输入财务管理范围'
          },
          companyMaxExchangeRateDeviation: {
            label: '最大汇率偏差',
            placeholder: '请输入最大汇率偏差'
          },
          companyAllocationIdentifier: {
            label: '分配标识符',
            placeholder: '请输入分配标识符'
          },
          companyRelatedCompany: {
            label: '关联公司',
            placeholder: '请输入关联公司'
          },
          companyRelatedPlant: {
            label: '关联工厂',
            placeholder: '请输入关联工厂'
          },
          companyAddressNumber: {
            label: '地址编号',
            placeholder: '请输入地址编号'
          },
          remark: {
            label: '备注',
            placeholder: '请输入备注'
          }
        },

        // 操作按钮
        actions: {
          add: '新增公司',
          edit: '编辑公司',
          delete: '删除公司',
          batchDelete: '批量删除',
          export: '导出',
          import: '导入',
          downloadTemplate: '下载模板',
          reset: '重置',
          search: '查询'
        },

        // 表格列标题
        columns: {
          companyCode: '公司代码',
          companyName: '公司名称',
          companyShortName: '公司简称',
          companyTaxNumber: '税务登记号',
          companyNature: '企业性质',
          companyIndustryType: '行业类型',
          companyCurrency: '币种',
          companyLanguage: '语言',
          companyPhone: '联系电话',
          companyEmail: '邮箱地址',
          companyWebsite: '公司网站',
          companyScale: '公司规模',
          companyAddress: '公司地址',
          companyName1: '公司名称1',
          companyName2: '公司名称2',
          companyName3: '公司名称3',
          companyLegalRepresentative: '法定代表人',
          companyRegisteredCapital: '注册资本',
          companyBusinessLicense: '营业执照号',
          companyOrganizationCode: '组织机构代码',
          companyUnifiedCreditCode: '统一社会信用代码',
          companyCity: '所在城市',
          companyRegion: '所在地区',
          companyPostalCode: '邮政编码',
          companyCountry: '所在国家',
          companyFax: '传真号码',
          establishmentDate: '成立日期',
          dissolutionDate: '解散日期',
          orderNum: '排序号',
          status: '状态',
          companyFiscalYearVariant: '会计年度变式',
          companyChartOfAccounts: '会计科目表',
          companyFinancialManagementArea: '财务管理范围',
          companyMaxExchangeRateDeviation: '最大汇率偏差',
          companyAllocationIdentifier: '分配标识符',
          companyRelatedCompany: '关联公司',
          companyRelatedPlant: '关联工厂',
          companyAddressNumber: '地址编号',
          remark: '备注',
          createBy: '创建人',
          createTime: '创建时间',
          updateBy: '更新人',
          updateTime: '更新时间',
          deleteBy: '删除人',
          deleteTime: '删除时间'
        },

        // 消息提示
        messages: {
          addSuccess: '新增公司成功',
          updateSuccess: '更新公司成功',
          deleteSuccess: '删除公司成功',
          batchDeleteSuccess: '批量删除公司成功',
          deleteConfirm: '确定要删除选中的公司吗？',
          batchDeleteConfirm: '确定要批量删除选中的公司吗？',
          loadFailed: '加载数据失败',
          submitFailed: '提交失败',
          exportSuccess: '导出成功',
          importSuccess: '导入成功',
          importFailed: '导入失败',
          downloadTemplateSuccess: '下载模板成功'
        },

        // 查询条件
        query: {
          companyCode: '公司代码',
          companyName: '公司名称',
          companyNature: '企业性质',
          companyIndustryType: '行业类型',
          companyCurrency: '币种',
          status: '状态'
        }
      }
    }
  }
}
