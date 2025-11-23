export default {
  identity: {
    dept: {
      title: '部门管理',
      fields: {
        deptId: {
          label: '部门ID'
        },
        deptName: {
          label: '部门名称',
          placeholder: '请输入部门名称',
          validation: {
            required: '部门名称不能为空',
            length: '部门名称长度必须在2-20个字符之间'
          }
        },
        parentId: {
          label: '上级部门',
          placeholder: '请选择上级部门'
        },        
        orderNum: {
          label: '显示顺序',
          placeholder: '请输入显示顺序'
        },
        leader: {
          label: '负责人',
          placeholder: '请输入负责人'
        },
        phone: {
          label: '联系电话',
          placeholder: '请输入联系电话'
        },
        email: {
          label: '邮箱',
          placeholder: '请输入邮箱'
        },
        userCount: {
          label: '用户数量',
          placeholder: '请输入用户数量'
        },
        deptStatus: {
          label: '部门状态',
          placeholder: '请选择部门状态',
          options: {
            enabled: '启用',
            disabled: '禁用'
          }
        },
      },
      actions: {
        add: '新增部门',
        edit: '编辑部门',
        delete: '删除部门',
        export: '导出部门'
      },
      messages: {
        confirmDelete: '是否确认删除名称为"{name}"的部门？',
        deleteSuccess: '部门删除成功',
        deleteFailed: '部门删除失败',
        saveSuccess: '部门信息保存成功',
        saveFailed: '部门信息保存失败',
        hasChildren: '存在下级部门,不允许删除'
      }
    }
  }
} 