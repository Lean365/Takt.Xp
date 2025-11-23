export default {
  identity: {
    user: {
      title: '使用者管理',
      profile: '個人資訊',
      changePasswordTitle: '修改密碼',
      
      // 密碼相關
      password: {
        old: {
          label: '舊密碼',
          placeholder: '請輸入舊密碼',
          validation: {
            required: '舊密碼不能為空',
          }
        },
        new: {
          label: '新密碼',
          placeholder: '請輸入新密碼',
          validation: {
            required: '新密碼不能為空',
          }
        },
        confirm: {
          label: '確認密碼',
          placeholder: '請輸入確認密碼',
          validation: {
            required: '確認密碼不能為空',
          }
        },
      },
      tabs: {
        userInfo: '使用者資訊',
        organization: '組織關係',
        avatar: '頭像'
      },
      // 表單欄位定義
      fields: {
        userId: {
          label: '使用者ID'
        },
        userName: {
          label: '使用者名稱',
          placeholder: '請輸入使用者名稱',
          validation: {
            required: '使用者名稱不能為空',
            format: '必須以小寫字母開頭，只能包含小寫字母、數字和連字符，不允許點和下劃線，長度在4-50位之間'
          }
        },
        nickName: {
          label: '暱稱',
          placeholder: '請輸入暱稱',
          validation: {
            required: '暱稱不能為空',
            format: '2-50位字符，允許中文、日語、韓語、英文、數字、英文句點和連字符，不允許下劃線和空格'
          }
        },
        realName: {
          label: '真實姓名',
          placeholder: '請輸入真實姓名',
          validation: {
            required: '真實姓名不能為空',
            format: '真實姓名長度必須在2-20位之間，只能包含中文、英文、數字和下劃線'
          }
        },
        fullName: {
          label: '全名',
          placeholder: '請輸入全名',
          validation: {
            required: '全名不能為空',
            format: '全名長度必須在2-20位之間，只能包含中文、英文、數字和下劃線'
          }
        },
        englishName: {
          label: '英文名',
          placeholder: '請輸入英文名',
          validation: {
            required: '英文名不能為空',
            format: '英文名長度必須在2-100位之間，必須以字母開頭，只能包含英文字母、空格、連字符和英文句點'
          }
        },
        password: {
          label: '密碼',
          placeholder: '請輸入密碼',
          validation: {
            required: '密碼不能為空',
            format: '密碼長度必須在6-20位之間，只能包含英文字母、數字和特殊字符'
          }
        },
        userType: {
          label: '使用者類型',
          placeholder: '請選擇使用者類型',
          options: {
            admin: '管理員',
            user: '一般使用者'
          }
        },
        email: {
          label: '電子郵件',
          placeholder: '請輸入電子郵件',
          validation: {
            required: '電子郵件不能為空',
            invalid: '電子郵件長度必須在6-100位之間，且格式正確'
          }
        },
        phoneNumber: {
          label: '手機號碼',
          placeholder: '請輸入手機號碼',
          validation: {
            required: '手機號碼不能為空',
            invalid: '請輸入正確的手機號碼或座機號碼格式'
          }
        },
        gender: {
          label: '性別',
          placeholder: '請選擇性別',
          options: {
            male: '男',
            female: '女',
            unknown: '未知'
          }
        },
        avatar: {
          label: '頭像',
          upload: '上傳頭像',
          currentAvatar: '目前頭像',
          avatarUpload: '頭像上傳',
          uploadSuccess: '頭像上傳成功',
          uploadError: '頭像上傳失敗',
          uploading: '正在上傳頭像...',
          onlyImage: '只能上傳圖片檔案！',
          sizeLimit: '圖片大小不能超過 5MB！'
        },
        status: {
          label: '狀態',
          placeholder: '請選擇狀態',
          options: {
            enabled: '啟用',
            disabled: '停用'
          }
        },
        lastPasswordChangeTime: {
          label: '最後密碼修改時間'
        },
        lockEndTime: {
          label: '鎖定結束時間'
        },
        lockReason: {
          label: '鎖定原因'
        },
        isLock: {
          label: '是否鎖定'
        },
        errorLimit: {
          label: '錯誤次數上限'
        },
        loginCount: {
          label: '登入次數'
        },
        deptName: {
          label: '所屬部門',
          placeholder: '請選擇所屬部門',
          validation: {
            required: '所屬部門不能為空'
          }
        },
        role: {
          label: '所屬角色',
          placeholder: '請選擇所屬角色',
          validation: {
            required: '所屬角色不能為空'
          }
        },
        post: {
          label: '所屬職位',
          placeholder: '請選擇所屬職位',
          validation: {
            required: '所屬職位不能為空'
          }
        },
        remark: {
          label: '備註',
          placeholder: '請輸入備註資訊'
        }
      },

      // 操作按鈕
      actions: {
        add: '新增使用者',
        edit: '編輯使用者',
        'delete': '刪除使用者',
        resetPassword: '重設密碼',
        export: '匯出使用者'
      },

      // 訊息提示
      messages: {
        confirmDelete: '是否確認刪除選中的使用者？',
        confirmResetPassword: '是否確認重設所選使用者的密碼？',
        confirmToggleStatus: '是否確認{action}該使用者？',
        deleteSuccess: '使用者刪除成功',
        deleteFailed: '使用者刪除失敗',
        saveSuccess: '使用者資訊儲存成功',
        saveFailed: '使用者資訊儲存失敗',
        resetPasswordSuccess: '密碼重設成功',
        resetPasswordFailed: '密碼重設失敗',
        importSuccess: '使用者匯入成功',
        importFailed: '使用者匯入失敗',
        exportSuccess: '使用者匯出成功',
        exportFailed: '使用者匯出失敗',
        toggleStatusSuccess: '狀態修改成功',
        toggleStatusFailed: '狀態修改失敗',
        cannotDeleteAdmin: '管理使用者不允許刪除！',
        cannotEditAdmin: '管理使用者不允許修改！',
        cannotUpdateAdminStatus: '管理使用者狀態不允許修改！',
        cannotResetAdminPassword: '管理使用者密碼不允許重設！',
        cannotUnlockAdmin: '管理使用者不允許解鎖！',
        cannotAllocateRole: '管理使用者不允許進行角色分配！',
        cannotAllocateDept: '管理使用者不允許進行部門分配！',
        cannotAllocatePost: '管理使用者不允許進行職位分配！',
        statusUpdateSuccess: '狀態更新成功',
        statusUpdateFailed: '狀態更新失敗',
        lockStatusUpdateSuccess: '鎖定狀態更新成功',
        lockStatusUpdateFailed: '鎖定狀態更新失敗'
      },

      // 表格顯示文字
      table: {
        notLocked: '未鎖定',
        none: '無',
        queryParams: '查詢參數',
        importResponseData: '匯入回應資料',
        parsedData: '解析後的資料',
        exportFailed: '匯出失敗',
        downloadTemplateFailed: '下載範本失敗',
        rowClicked: '行點擊',
        toggleFullscreenState: '切換全螢幕狀態',
        getOptionsFailed: '取得選項失敗',
        createUser: '建立使用者',
        updateUser: '更新使用者'
      },

      // 匯入提示
      importTips: 'Excel工作表名稱必須為「User」',

      // 標籤頁
      tab: {
        basic: '基本資訊',
        profile: '個人資料',
        role: '角色權限',
        dept: '部門職位',
        other: '其他資訊',
        avatar: '頭像設定',
        loginLog: '登入日誌',
        operateLog: '操作日誌',
        errorLog: '異常日誌',
        taskLog: '任務日誌'
      },

      // 匯入匯出
      import: {
        title: '匯入使用者',
        template: '下載範本',
        success: '匯入成功',
        failed: '匯入失敗',
        fileName: '使用者資料'
      },
      export: {
        title: '匯出使用者',
        fileName: '使用者資料',
        success: '匯出成功',
        failed: '匯出失敗'
      },
      template: {
        fileName: '使用者匯入範本',
        downloadFailed: '下載範本失敗'
      },

      // 重設密碼
      resetPwd: '重設密碼',
      
      // 修改密碼
      changePassword: {
        success: '密碼修改成功',
        failed: '密碼修改失敗，請重試',
        changeFailed: '修改密碼失敗'
      },

      // 分配相關
      allocateDept: '分配部門',
      allocatePost: '分配職位',
      allocateRole: '分配角色',
      
      roleAllocate: {
        unallocated: '未分配',
        allocated: '已分配',
        loadRoleListFailed: '載入角色清單失敗',
        startLoadUserRoles: '開始載入使用者角色',
        userRolesApiResponse: '使用者角色API回應',
        setSelectedRoles: '設定已選角色',
        loadUserRolesFailed: '載入使用者角色失敗'
      },
      
      postAllocate: {
        unallocated: '未分配',
        allocated: '已分配',
        loadPostListFailed: '載入職位清單失敗',
        loadUserPostsFailed: '載入使用者職位失敗'
      }
    }
  }
}
