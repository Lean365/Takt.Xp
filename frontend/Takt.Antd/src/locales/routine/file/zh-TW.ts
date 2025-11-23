export default {
  routine: {
    file: {
      fileName: '檔案名稱',
      fileType: '檔案類型',
      fileSize: '檔案大小',
      filePath: '檔案路徑',
      fileUrl: '檔案URL',
      fileMd5: '檔案MD5',
      fileOriginalName: '原始檔案名稱',
      fileExtension: '檔案副檔名',
      fileStorageType: '檔案儲存類型',
      fileStorageLocation: '檔案儲存位置',
      status: '狀態',
      remark: '備註',
      createTime: '建立時間',
      updateTime: '更新時間',
      operation: '操作',
      placeholder: {
        fileName: '請輸入檔案名稱',
        fileType: '請輸入檔案類型',
        fileSize: '請輸入檔案大小',
        status: '請選擇狀態',
        remark: '請輸入備註'
      },
      validation: {
        fileName: {
          required: '請輸入檔案名稱',
          maxLength: '檔案名稱不能超過100個字元'
        },
        fileType: {
          required: '請輸入檔案類型',
          maxLength: '檔案類型不能超過50個字元'
        },
        status: {
          required: '請選擇狀態'
        },
        file: {
          required: '請選擇要上傳的檔案',
          size: '上傳檔案大小不能超過2MB!'
        }
      },
      message: {
        addSuccess: '檔案新增成功',
        editSuccess: '檔案更新成功',
        deleteSuccess: '檔案刪除成功',
        deleteConfirm: '確定要刪除檔案「{name}」嗎？',
        exportSuccess: '檔案匯出成功',
        importSuccess: '檔案匯入成功',
        uploadSuccess: '檔案上傳成功',
        downloadSuccess: '檔案下載成功'
      },
      detail: {
        title: '檔案詳情'
      },
      upload: {
        success: '檔案上傳成功',
        failed: '檔案上傳失敗',
        fileEmpty: '請選擇要上傳的檔案',
        fileType: '檔案類型不支援',
        fileSize: '檔案大小超出限制',
        fileExists: '檔案已存在',
        fileNameRequired: '檔案名稱不能為空'
      }
    }
  }
} 