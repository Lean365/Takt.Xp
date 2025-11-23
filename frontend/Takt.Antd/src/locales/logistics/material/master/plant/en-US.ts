export default {
  title: 'Plant Management',
  tabs: {
    basicInfo: 'Basic Information',
    management: 'Management Configuration'
  },
  fields: {
    plantId: {
      label: 'Plant ID',
      placeholder: 'Please enter plant ID'
    },
    plantCode: {
      label: 'Plant Code',
      placeholder: 'Please enter plant code',
      required: 'Please enter plant code',
      length: 'Plant code length should be 1-50 characters'
    },
    plantName: {
      label: 'Plant Name',
      placeholder: 'Please enter plant name',
      required: 'Please enter plant name',
      length: 'Plant name length should be 1-100 characters'
    },
    plantShortName: {
      label: 'Plant Short Name',
      placeholder: 'Please enter plant short name'
    },
    plantType: {
      label: 'Plant Type',
      placeholder: 'Please select plant type',
      required: 'Please select plant type'
    },
    plantTypeName: {
      label: 'Plant Type Name',
      placeholder: 'Please enter plant type name'
    },
    status: {
      label: 'Status',
      placeholder: 'Please select status',
      required: 'Please select status'
    },
    plantAddress: {
      label: 'Plant Address',
      placeholder: 'Please enter plant address'
    },
    plantAddress1: {
      label: 'Plant Address 1',
      placeholder: 'Please enter plant address 1'
    },
    plantAddress2: {
      label: 'Plant Address 2',
      placeholder: 'Please enter plant address 2'
    },
    plantAddress3: {
      label: 'Plant Address 3',
      placeholder: 'Please enter plant address 3'
    },
    postalCode: {
      label: 'Postal Code',
      placeholder: 'Please enter postal code'
    },
    city: {
      label: 'City',
      placeholder: 'Please enter city'
    },
    province: {
      label: 'Province',
      placeholder: 'Please enter province'
    },
    country: {
      label: 'Country',
      placeholder: 'Please enter country'
    },
    phone: {
      label: 'Phone',
      placeholder: 'Please enter phone number',
      format: 'Please enter a valid phone number'
    },
    fax: {
      label: 'Fax',
      placeholder: 'Please enter fax'
    },
    email: {
      label: 'Email',
      placeholder: 'Please enter email',
      format: 'Please enter a valid email format'
    },
    contactPerson: {
      label: 'Contact Person',
      placeholder: 'Please enter contact person'
    },
    isBatchManaged: {
      label: 'Batch Management',
      placeholder: 'Please select batch management'
    },
    isSerialManaged: {
      label: 'Serial Management',
      placeholder: 'Please select serial management'
    },
    isQualityManaged: {
      label: 'Quality Management',
      placeholder: 'Please select quality management'
    },
    isCostManaged: {
      label: 'Cost Management',
      placeholder: 'Please select cost management'
    },
    isInventoryManaged: {
      label: 'Inventory Management',
      placeholder: 'Please select inventory management'
    },
    isProductionManaged: {
      label: 'Production Management',
      placeholder: 'Please select production management'
    },
    isPurchaseManaged: {
      label: 'Purchase Management',
      placeholder: 'Please select purchase management'
    },
    isSalesManaged: {
      label: 'Sales Management',
      placeholder: 'Please select sales management'
    },
    remark: {
      label: 'Remark',
      placeholder: 'Please enter remark'
    }
  },
  operations: {
    add: 'Add Plant',
    edit: 'Edit Plant',
    delete: 'Delete Plant',
    batchDelete: 'Batch Delete',
    import: 'Import Plant',
    export: 'Export Plant',
    template: 'Download Template',
    status: 'Status',
    enable: 'Enable',
    disable: 'Disable'
  },
  messages: {
    addSuccess: 'Plant added successfully',
    editSuccess: 'Plant updated successfully',
    deleteSuccess: 'Plant deleted successfully',
    batchDeleteSuccess: 'Batch delete successful',
    importSuccess: 'Plant imported successfully',
    exportSuccess: 'Plant exported successfully',
    confirmDelete: 'Are you sure to delete the selected plant?',
    confirmBatchDelete: 'Are you sure to batch delete the selected plants?',
    noSelection: 'Please select plants to operate',
    loadDataFailed: 'Failed to load data',
    operationFailed: 'Operation failed'
  },
  table: {
    plantId: 'ID',
    plantCode: 'Plant Code',
    plantName: 'Plant Name',
    plantShortName: 'Plant Short Name',
    plantType: 'Plant Type',
    plantTypeName: 'Plant Type Name',
    status: 'Status',
    plantAddress: 'Plant Address',
    contactPerson: 'Contact Person',
    phone: 'Phone',
    email: 'Email',
    remark: 'Remark',
    createTime: 'Create Time',
    updateTime: 'Update Time',
    operation: 'Operation'
  }
}