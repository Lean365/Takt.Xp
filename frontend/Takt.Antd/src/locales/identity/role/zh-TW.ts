export default {
  identity: {
    role: {
      title: '角色管理',
      fields: {
        roleId: {
          label: '角色ID'
        },
        roleName: {
          label: '角色名稱',
          placeholder: '請輸入角色名稱',
          validation: {
            required: '角色名稱不能為空',
            length: '角色名稱長度必須在2-20個字符之間'
          }
        },
        roleKey: {
          label: '角色標識',
          placeholder: '請輸入角色標識',
          validation: {
            required: '角色標識不能為空',
            length: '角色標識長度必須在2-100個字符之間'
          }
        },
        roleSort: {
          label: '排序號',
          placeholder: '請輸入排序號'
        },
        dataScope: {
          label: '數據範圍',
          placeholder: '請選擇數據範圍'
        },
        userCount: {
          label: '用戶數量',
          placeholder: '請輸入用戶數量'
        },
        status: {
          label: '狀態',
          placeholder: '請選擇狀態',
          options: {
            enabled: '啟用',
            disabled: '禁用'
          }
        }
      },
      actions: {
        add: '新增角色',
        edit: '編輯角色',
        delete: '刪除角色',
        export: '導出角色',
        authorize: '菜單授權',
        deptAuthorize: '部門授權'
      },
      messages: {
        confirmDelete: '確認刪除選中的角色嗎？',
        deleteSuccess: '角色刪除成功',
        deleteFailed: '角色刪除失敗',
        saveSuccess: '角色信息保存成功',
        saveFailed: '角色信息保存失敗',
        cannotEditSystemAdmin: '系統管理員角色不允許修改！',
        cannotDeleteSystemAdmin: '系統管理員角色不允許刪除！',
        cannotUpdateSystemAdminStatus: '系統管理員角色狀態不允許修改！',
        cannotAllocateSystemAdminMenu: '系統管理員角色不允許進行菜單分配！',
        cannotAllocateSystemAdminDept: '系統管理員角色不允許進行部門分配！'
      }
    }
  }
}