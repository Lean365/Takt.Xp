export default {
  identity: {
    role: {
      title: '角色管理',
      fields: {
        roleId: {
          label: '角色ID'
        },
        roleName: {
          label: '角色名称',
          placeholder: '请输入角色名称',
          validation: {
            required: '角色名称不能为空',
            length: '角色名称长度必须在2-20个字符之间'
          }
        },
        roleKey: {
          label: '角色标识',
          placeholder: '请输入角色标识',
          validation: {
            required: '角色标识不能为空',
            length: '角色标识长度必须在2-100个字符之间'
          }
        },
        roleSort: {
          label: '排序号',
          placeholder: '请输入排序号'
        },
        dataScope: {
          label: '数据范围',
          placeholder: '请选择数据范围'
        },
        userCount: {
          label: '用户数量',
          placeholder: '请输入用户数量'
        },
        status: {
          label: '状态',
          placeholder: '请选择状态',
          options: {
            enabled: '启用',
            disabled: '禁用'
          }
        }
      },
      actions: {
        add: '新增角色',
        edit: '编辑角色',
        delete: '删除角色',
        export: '导出角色',
        authorize: '菜单授权',
        deptAuthorize: '部门授权'
      },
      messages: {
        confirmDelete: '确认删除选中的角色吗？',
        deleteSuccess: '角色删除成功',
        deleteFailed: '角色删除失败',
        saveSuccess: '角色信息保存成功',
        saveFailed: '角色信息保存失败',
        cannotEditSystemAdmin: '系统管理员角色不允许修改！',
        cannotDeleteSystemAdmin: '系统管理员角色不允许删除！',
        cannotUpdateSystemAdminStatus: '系统管理员角色状态不允许修改！',
        cannotAllocateSystemAdminMenu: '系统管理员角色不允许进行菜单分配！',
        cannotAllocateSystemAdminDept: '系统管理员角色不允许进行部门分配！'
      }
    }
  }
}