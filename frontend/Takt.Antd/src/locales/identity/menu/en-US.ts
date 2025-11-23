export default {
  identity: {
    menu: {
      title: 'Menu Management',
      columns: {
        menuName: 'Menu Name',
        transKey: 'I18n Key',
        parentId: 'Parent Menu',
        orderNum: 'Order',
        path: 'Route Path',
        component: 'Component Path',
        queryParams: 'Route Params',
        isExternal: 'External Link',
        isCache: 'Cache',
        menuType: 'Menu Type',
        visible: 'Visibility',
        status: 'Status',
        perms: 'Permission Key',
        icon: 'Icon',
        id: 'ID',
        createBy: 'Created By',
        createTime: 'Created Time',
        updateBy: 'Updated By',
        updateTime: 'Updated Time',
        deleteBy: 'Deleted By',
        deleteTime: 'Deleted Time',
        isDeleted: 'Deleted',
        remark: 'Remark',
        action: 'Action'
      },
      fields: {
        menuName: {
          label: 'Menu Name',
          placeholder: 'Please enter menu name',
          validation: {
            required: 'Please enter menu name',
            length: 'Menu name length must be between 2 and 50 characters'
          }
        },
        transKey: {
          label: 'I18n Key',
          placeholder: 'Please enter i18n key'
        },
        parentId: {
          label: 'Parent Menu',
          placeholder: 'Please select parent menu',
          root: 'Root Menu'
        },
        orderNum: {
          label: 'Order',
          placeholder: 'Please enter order',
          validation: {
            required: 'Please enter order'
          }
        },
        path: {
          label: 'Route Path',
          placeholder: 'Please enter route path'
        },
        component: {
          label: 'Component Path',
          placeholder: 'Please enter component path'
        },
        queryParams: {
          label: 'Route Params',
          placeholder: 'Please enter route params'
        },
        isExternal: {
          label: 'External Link',
          placeholder: 'Please select if external link',
          options: {
            yes: 'Yes',
            no: 'No'
          }
        },
        isCache: {
          label: 'Cache',
          placeholder: 'Please select if cache',
          options: {
            yes: 'Yes',
            no: 'No'
          }
        },
        menuType: {
          label: 'Menu Type',
          options: {
            directory: 'Directory',
            menu: 'Menu',
            button: 'Button'
          },
          validation: {
            required: 'Please select menu type'
          }
        },
        visible: {
          label: 'Visibility',
          placeholder: 'Please select visibility',
          options: {
            show: 'Show',
            hide: 'Hide'
          }
        },
        status: {
          label: 'Status',
          placeholder: 'Please select status',
          options: {
            normal: 'Normal',
            disabled: 'Disabled'
          }
        },
        perms: {
          label: 'Permission Key',
          placeholder: 'Please enter permission key'
        },
        icon: {
          label: 'Menu Icon',
          placeholder: 'Please enter menu icon'
        },
      },
      dialog: {
        create: 'Add Menu',
        update: 'Edit Menu',
        delete: 'Delete Menu'
      },
      operation: {
        add: {
          title: 'Add Menu',
          success: 'Added successfully',
          failed: 'Add failed'
        },
        edit: {
          title: 'Edit Menu',
          success: 'Edited successfully',
          failed: 'Edit failed'
        },
        delete: {
          title: 'Delete Menu',
          confirm: 'Are you sure you want to delete this menu?',
          success: 'Deleted successfully',
          failed: 'Delete failed'
        }
      }
    }
  }
}