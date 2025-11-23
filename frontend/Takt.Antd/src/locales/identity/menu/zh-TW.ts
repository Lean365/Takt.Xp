export default {
  identity: {
    menu: {
      title: '選單管理',
      columns: {
        menuName: '選單名稱',
        transKey: '國際化標識',
        parentId: '上級選單',
        orderNum: '排序',
        path: '路由地址',
        component: '組件路徑',
        queryParams: '路由參數',
        isExternal: '是否外鏈',
        isCache: '是否快取',
        menuType: '選單類型',
        visible: '顯示狀態',
        status: '狀態',
        perms: '權限標識',
        icon: '圖示',
        id: '主鍵',
        createBy: '建立者',
        createTime: '建立時間',
        updateBy: '更新者',
        updateTime: '更新時間',
        deleteBy: '刪除者',
        deleteTime: '刪除時間',
        isDeleted: '是否刪除',
        remark: '備註',
        action: '操作'
      },
      fields: {
        menuName: {
          label: '選單名稱',
          placeholder: '請輸入選單名稱',
          validation: {
            required: '請輸入選單名稱',
            length: '選單名稱長度必須在2-50個字元之間'
          }
        },
        transKey: {
          label: '國際化標識',
          placeholder: '請輸入國際化標識'
        },
        parentId: {
          label: '上級選單',
          placeholder: '請選擇上級選單',
          root: '根選單'
        },
        orderNum: {
          label: '顯示排序',
          placeholder: '請輸入顯示排序',
          validation: {
            required: '請輸入顯示排序'
          }
        },
        path: {
          label: '路由地址',
          placeholder: '請輸入路由地址'
        },
        component: {
          label: '組件路徑',
          placeholder: '請輸入組件路徑'
        },
        queryParams: {
          label: '路由參數',
          placeholder: '請輸入路由參數'
        },
        isExternal: {
          label: '是否外鏈',
          placeholder: '請選擇是否外鏈',
          options: {
            yes: '是',
            no: '否'
          }
        },
        isCache: {
          label: '是否快取',
          placeholder: '請選擇是否快取',
          options: {
            yes: '是',
            no: '否'
          }
        },
        menuType: {
          label: '選單類型',
          options: {
            directory: '目錄',
            menu: '選單',
            button: '按鈕'
          },
          validation: {
            required: '請選擇選單類型'
          }
        },
        visible: {
          label: '顯示狀態',
          placeholder: '請選擇顯示狀態',
          options: {
            show: '顯示',
            hide: '隱藏'
          }
        },
        status: {
          label: '狀態',
          placeholder: '請選擇選單狀態',
          options: {
            normal: '正常',
            disabled: '停用'
          }
        },
        perms: {
          label: '權限標識',
          placeholder: '請輸入權限標識'
        },
        icon: {
          label: '選單圖示',
          placeholder: '請輸入選單圖示'
        },
        }
      },
      dialog: {
        create: '新增選單',
        update: '編輯選單',
        delete: '刪除選單'
      },
      operation: {
        add: {
          title: '新增選單',
          success: '新增成功',
          failed: '新增失敗'
        },
        edit: {
          title: '編輯選單',
          success: '修改成功',
          failed: '修改失敗'
        },
        delete: {
          title: '刪除選單',
          confirm: '是否確認刪除此選單？',
          success: '刪除成功',
          failed: '刪除失敗'
        }
      }
    }
  }
