export default {
  routine: {
    file: {
      // 基础信息
      fileName: '文件名',
      fileType: '文件类型',
      fileSize: '文件大小',
      filePath: '文件路径',
      fileUrl: '文件URL',
      fileMd5: '文件MD5',
      fileOriginalName: '原始文件名',
      fileExtension: '文件扩展名',
      fileStorageType: '文件存储类型',
      fileStorageLocation: '文件存储位置',
      status: '状态',
      // 时间信息
      createTime: '创建时间',
      updateTime: '更新时间',
      // 操作
      operation: '操作',
      // 提示信息
      placeholder: {
        fileName: '请输入文件名',
        fileType: '请输入文件类型',
        fileSize: '请输入文件大小',
        status: '请选择状态',
      },
      // 验证信息
      validation: {
        fileName: {
          required: '请输入文件名',
          maxLength: '文件名不能超过100个字符'
        },
        fileType: {
          required: '请输入文件类型',
          maxLength: '文件类型不能超过50个字符'
        },
        status: {
          required: '请选择状态'
        },
        file: {
          required: '请选择要上传的文件',
          size: '上传文件大小不能超过 2MB!'
        }
      },
      // 操作结果
      message: {
        addSuccess: '文件添加成功',
        editSuccess: '文件更新成功',
        deleteSuccess: '文件删除成功',
        deleteConfirm: '确定要删除文件"{name}"吗？',
        exportSuccess: '文件导出成功',
        importSuccess: '文件导入成功',
        uploadSuccess: '文件上传成功',
        downloadSuccess: '文件下载成功'
      },
      // 详情
      detail: {
        title: '文件详情'
      },
      upload: {
        success: '文件上传成功',
        failed: '文件上传失败',
        fileEmpty: '请选择要上传的文件',
        fileType: '文件类型不支持',
        fileSize: '文件大小超出限制',
        fileExists: '文件已存在',
        fileNameRequired: '文件名不能为空'
      }
    }
  }
} 