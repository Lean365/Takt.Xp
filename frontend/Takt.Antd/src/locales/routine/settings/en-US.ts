export default {
  admin: {
    // System Configuration
    config: {
      // Basic Information
      name: 'Name',
      key: 'Key',
      value: 'Value',
      builtin: 'Built-in',
      order: 'Order',
      remark: 'Remark',
      status: 'Status',
      // Time Information
      createTime: 'Create Time',
      createBy: 'Create By',
      updateTime: 'Update Time',
      updateBy: 'Update By',
      // Operations
      operation: 'Operation',
      // Prompt Information
      placeholder: {
        name: 'Please enter configuration name',
        key: 'Please enter configuration key',
        value: 'Please enter configuration value',
        builtin: 'Please select if built-in',
        order: 'Please enter order number',
        remark: 'Please enter remark',
        status: 'Please select status'
      },
      // Validation Information
      validation: {
        name: {
          required: 'Please enter configuration name',
          maxLength: 'Configuration name cannot exceed 100 characters'
        },
        key: {
          required: 'Please enter configuration key',
          maxLength: 'Configuration key cannot exceed 100 characters',
          pattern: 'Configuration key must start with a letter and can only contain letters, numbers, underscores, dots and colons'
        },
        value: {
          required: 'Please enter configuration value',
          maxLength: 'Configuration value cannot exceed 500 characters'
        },
        builtin: {
          required: 'Please select if built-in'
        },
        order: {
          required: 'Please enter order number',
          range: 'Order number must be between 0 and 9999'
        },
        status: {
          required: 'Please select status'
        }
      },
      // Operation Results
      message: {
        addSuccess: 'Configuration added successfully',
        editSuccess: 'Configuration updated successfully',
        deleteSuccess: 'Configuration deleted successfully',
        deleteConfirm: 'Are you sure to delete configuration "{name}"?',
        exportSuccess: 'Configuration exported successfully',
        importSuccess: 'Configuration imported successfully',
        refreshSuccess: 'Cache refreshed successfully'
      }
    }
  }
} 