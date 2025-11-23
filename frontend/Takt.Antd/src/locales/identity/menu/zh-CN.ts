export default {
  identity: {
    menu: {
      title: '菜单管理',
      types:{
        directory: '目录',
        menu: '菜单',
        button: '按钮'
      },
      // 表单字段定义
      fields: {
        menuId: {
          label: '菜单ID'
        },
        menuName: {
          label: '菜单名称',
          placeholder: '请输入菜单名称',
          validation: {
            required: '请输入菜单名称',
            length: '菜单名称长度必须在2-50个字符之间',
            format: '菜单名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文'
          }
        },
        menuDirectoryName: {
          label: '目录名称',
          placeholder: '请输入目录名称',
          validation: {
            required: '请输入目录名称',
            length: '目录名称长度必须在2-50个字符之间',
            format: '目录名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文'
          }
        },
        menuButtonName: {
          label: '按钮名称',
          placeholder: '请输入按钮名称',
          validation: {
            required: '请输入按钮名称',
            length: '按钮名称长度必须在2-50个字符之间',
            format: '按钮名称格式错误，必须为英文（不允许重音符号）、法语/西语、中文、日文、韩文、阿拉伯文、俄文',
            menuNameExists: '菜单名称已存在'
          }
        },
        transKey: {
          label: '国际化标识',
          placeholder: '请输入国际化标识',
          validation: {
            required: '请输入国际化标识',
            maxLength: '国际化标识长度不能超过200个字符',
            pattern: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
            length: '国际化标识长度必须在1-200个字符之间',
            format: '国际化标识格式必须为xxx:xxx:xxx，只能包含小写字母和冒号，前缀部分不能包含数字',
            invalid: '请输入有效的国际化标识'
          }
        },
        parentId: {
          label: '上级菜单',
          placeholder: '请选择上级菜单',
          root: '根菜单',
          validation: {
            required: '请选择上级菜单'
          }
        },
        orderNum: {
          label: '显示排序',
          placeholder: '请输入显示排序',
          validation: {
            required: '请输入显示排序'
          }
        },
        path: {
          label: '路由地址',
          placeholder: '请输入路由地址',
          validation: {
            required: '请输入路由地址',
            length: '路由地址长度必须在2-50个字符之间',
            format: '路由地址格式错误，且只能包含小写字母'
          }
        },
        component: {
          label: '组件路径',
          placeholder: '请输入组件路径',
          validation: {
            required: '请输入组件路径'
          }
        },
        queryParams: {
          label: '路由参数',
          placeholder: '请输入路由参数'
        },
        isExternal: {
          label: '是否外链',
          placeholder: '请选择是否外链',
          options: {
            yes: '是',
            no: '否'
          }
        },
        isCache: {
          label: '是否缓存',
          placeholder: '请选择是否缓存',
          options: {
            yes: '是',
            no: '否'
          }
        },
        menuType: {
          label: '菜单类型',
          placeholder: '请选择菜单类型',
          options: {
            directory: '目录',
            menu: '菜单',
            button: '按钮'
          },
          validation: {
            required: '请选择菜单类型'
          }
        },
        visible: {
          label: '显示状态',
          placeholder: '请选择显示状态',
          options: {
            show: '显示',
            hide: '隐藏'
          }
        },
        status: {
          label: '状态',
          placeholder: '请选择菜单状态',
          options: {
            normal: '正常',
            disabled: '停用'
          }
        },
        perms: {
          label: '权限标识',
          placeholder: '请输入权限标识',
          validation: {
            required: '请输入权限标识'
          }
        },
        icon: {
          label: '菜单图标',
          placeholder: '请输入菜单图标',
          select: '选择图标',
          validation: {
            required: '请选择菜单图标'
          }
        },
      },

      // 操作按钮
      actions: {
        create: '创建菜单',
        edit: '编辑菜单',
        delete: '删除菜单',
        addDirectory: '添加目录',
        addMenu: '添加菜单',
        addButton: '添加按钮',
        selectType: '请选择菜单类型'
      },

      // 消息提示
      messages: {
        createSuccess: '菜单创建成功',
        updateSuccess: '菜单更新成功',
        deleteSuccess: '菜单删除成功',
        createFailed: '菜单创建失败',
        updateFailed: '菜单更新失败',
        deleteFailed: '菜单删除失败',
        confirmDelete: '是否确认删除该菜单?',
        noData: '暂无数据',
        loading: '加载中...'
      }
    }
  }
} 