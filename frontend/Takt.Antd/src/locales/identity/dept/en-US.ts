export default {
  identity: {
    dept: {
      title: 'Department Management',
      fields: {
        deptId: {
          label: 'Department ID'
        },
        parentId: {
          label: 'Parent Department',
          placeholder: 'Please select parent department'
        },
        deptName: {
          label: 'Department Name',
          placeholder: 'Please enter department name',
          validation: {
            required: 'Department name cannot be empty',
            length: 'Department name length must be between 2-20 characters'
          }
        },
        orderNum: {
          label: 'Display Order',
          placeholder: 'Please enter display order'
        },
        leader: {
          label: 'Leader',
          placeholder: 'Please enter leader name'
        },
        phone: {
          label: 'Phone',
          placeholder: 'Please enter phone number'
        },
        email: {
          label: 'Email',
          placeholder: 'Please enter email'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        },
        createTime: 'Create Time'
      },
      actions: {
        add: 'Add Department',
        edit: 'Edit Department',
        delete: 'Delete Department',
        export: 'Export Departments'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the department "{name}"?',
        deleteSuccess: 'Department deleted successfully',
        deleteFailed: 'Failed to delete department',
        saveSuccess: 'Department information saved successfully',
        saveFailed: 'Failed to save department information',
        hasChildren: 'Cannot delete: department has sub-departments'
      }
    }
  }
} 