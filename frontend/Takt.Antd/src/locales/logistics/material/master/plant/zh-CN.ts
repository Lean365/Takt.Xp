export default {
  title: '工厂管理',
  tabs: {
    basicInfo: '基本信息',
    management: '管理配置'
  },
  fields: {
    plantId: {
      label: '工厂ID',
      placeholder: '请输入工厂ID'
    },
    plantCode: {
      label: '工厂编码',
      placeholder: '请输入工厂编码',
      required: '请输入工厂编码',
      length: '工厂编码长度为1-50个字符'
    },
    plantName: {
      label: '工厂名称',
      placeholder: '请输入工厂名称',
      required: '请输入工厂名称',
      length: '工厂名称长度为1-100个字符'
    },
    plantShortName: {
      label: '工厂简称',
      placeholder: '请输入工厂简称'
    },
    plantType: {
      label: '工厂类型',
      placeholder: '请选择工厂类型',
      required: '请选择工厂类型'
    },
    plantTypeName: {
      label: '工厂类型名称',
      placeholder: '请输入工厂类型名称'
    },
    status: {
      label: '状态',
      placeholder: '请选择状态',
      required: '请选择状态'
    },
    plantAddress: {
      label: '工厂地址',
      placeholder: '请输入工厂地址'
    },
    plantAddress1: {
      label: '工厂地址1',
      placeholder: '请输入工厂地址1'
    },
    plantAddress2: {
      label: '工厂地址2',
      placeholder: '请输入工厂地址2'
    },
    plantAddress3: {
      label: '工厂地址3',
      placeholder: '请输入工厂地址3'
    },
    postalCode: {
      label: '邮政编码',
      placeholder: '请输入邮政编码'
    },
    city: {
      label: '城市',
      placeholder: '请输入城市'
    },
    province: {
      label: '省份',
      placeholder: '请输入省份'
    },
    country: {
      label: '国家',
      placeholder: '请输入国家'
    },
    phone: {
      label: '联系电话',
      placeholder: '请输入联系电话',
      format: '请输入正确的手机号码'
    },
    fax: {
      label: '传真',
      placeholder: '请输入传真'
    },
    email: {
      label: '邮箱',
      placeholder: '请输入邮箱',
      format: '请输入正确的邮箱格式'
    },
    contactPerson: {
      label: '联系人',
      placeholder: '请输入联系人'
    },
    isBatchManaged: {
      label: '是否启用批次管理',
      placeholder: '请选择是否启用批次管理'
    },
    isSerialManaged: {
      label: '是否启用序列号管理',
      placeholder: '请选择是否启用序列号管理'
    },
    isQualityManaged: {
      label: '是否启用质量管理',
      placeholder: '请选择是否启用质量管理'
    },
    isCostManaged: {
      label: '是否启用成本管理',
      placeholder: '请选择是否启用成本管理'
    },
    isInventoryManaged: {
      label: '是否启用库存管理',
      placeholder: '请选择是否启用库存管理'
    },
    isProductionManaged: {
      label: '是否启用生产管理',
      placeholder: '请选择是否启用生产管理'
    },
    isPurchaseManaged: {
      label: '是否启用采购管理',
      placeholder: '请选择是否启用采购管理'
    },
    isSalesManaged: {
      label: '是否启用销售管理',
      placeholder: '请选择是否启用销售管理'
    },
    remark: {
      label: '备注',
      placeholder: '请输入备注'
    }
  },
  operations: {
    add: '新增工厂',
    edit: '编辑工厂',
    delete: '删除工厂',
    batchDelete: '批量删除',
    import: '导入工厂',
    export: '导出工厂',
    template: '下载模板',
    status: '状态',
    enable: '启用',
    disable: '停用'
  },
  messages: {
    addSuccess: '新增工厂成功',
    editSuccess: '编辑工厂成功',
    deleteSuccess: '删除工厂成功',
    batchDeleteSuccess: '批量删除成功',
    importSuccess: '导入工厂成功',
    exportSuccess: '导出工厂成功',
    confirmDelete: '确定要删除选中的工厂吗？',
    confirmBatchDelete: '确定要批量删除选中的工厂吗？',
    noSelection: '请选择要操作的工厂',
    loadDataFailed: '加载数据失败',
    operationFailed: '操作失败'
  },
  table: {
    plantId: 'ID',
    plantCode: '工厂编码',
    plantName: '工厂名称',
    plantShortName: '工厂简称',
    plantType: '工厂类型',
    plantTypeName: '工厂类型名称',
    status: '状态',
    plantAddress: '工厂地址',
    contactPerson: '联系人',
    phone: '联系电话',
    email: '邮箱',
    remark: '备注',
    createTime: '创建时间',
    updateTime: '更新时间',
    operation: '操作'
  }
}