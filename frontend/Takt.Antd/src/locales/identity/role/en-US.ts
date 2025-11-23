export default {
  identity: {
    role: {
      title: 'Role Management',
      fields: {
        roleId: {
          label: 'Role ID'
        },
        roleName: {
          label: 'Role Name',
          placeholder: 'Please enter role name',
          validation: {
            required: 'Role name cannot be empty',
            length: 'Role name length must be between 2-20 characters'
          }
        },
        roleKey: {
          label: 'Role Key',
          placeholder: 'Please enter role key',
          validation: {
            required: 'Role key cannot be empty',
            length: 'Role key length must be between 2-100 characters'
          }
        },
        roleSort: {
          label: 'Display Order',
          placeholder: 'Please enter display order'
        },
        dataScope: {
          label: 'Data Scope',
          placeholder: 'Please select data scope'
        },
        userCount: {
          label: 'User Count',
          placeholder: 'Please enter user count'
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            enabled: 'Enabled',
            disabled: 'Disabled'
          }
        }
      },
      actions: {
        add: 'Add Role',
        edit: 'Edit Role',
        delete: 'Delete Role',
        export: 'Export Roles',
        authorize: 'Menu Authorization',
        deptAuthorize: 'Department Authorization'
      },
      messages: {
        confirmDelete: 'Are you sure you want to delete the selected role(s)?',
        deleteSuccess: 'Role deleted successfully',
        deleteFailed: 'Failed to delete role',
        saveSuccess: 'Role information saved successfully',
        saveFailed: 'Failed to save role information',
        cannotEditSystemAdmin: 'System administrator role cannot be edited!',
        cannotDeleteSystemAdmin: 'System administrator role cannot be deleted!',
        cannotUpdateSystemAdminStatus: 'System administrator role status cannot be updated!',
        cannotAllocateSystemAdminMenu: 'System administrator role cannot be assigned menus!',
        cannotAllocateSystemAdminDept: 'System administrator role cannot be assigned departments!'
      }
    }
  }
}