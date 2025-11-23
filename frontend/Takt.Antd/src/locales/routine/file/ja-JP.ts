export default {
  routine: {
    file: {
      fileName: 'ファイル名',
      fileType: 'ファイルタイプ',
      fileSize: 'ファイルサイズ',
      filePath: 'ファイルパス',
      fileUrl: 'ファイルURL',
      fileMd5: 'ファイルMD5',
      fileOriginalName: '元のファイル名',
      fileExtension: 'ファイル拡張子',
      fileStorageType: 'ストレージタイプ',
      fileStorageLocation: 'ストレージ場所',
      status: 'ステータス',
      remark: '備考',
      createTime: '作成日時',
      updateTime: '更新日時',
      operation: '操作',
      placeholder: {
        fileName: 'ファイル名を入力してください',
        fileType: 'ファイルタイプを入力してください',
        fileSize: 'ファイルサイズを入力してください',
        status: 'ステータスを選択してください',
        remark: '備考を入力してください'
      },
      validation: {
        fileName: {
          required: 'ファイル名を入力してください',
          maxLength: 'ファイル名は100文字以内で入力してください'
        },
        fileType: {
          required: 'ファイルタイプを入力してください',
          maxLength: 'ファイルタイプは50文字以内で入力してください'
        },
        status: {
          required: 'ステータスを選択してください'
        },
        file: {
          required: 'アップロードするファイルを選択してください',
          size: 'アップロードするファイルサイズは2MB以内である必要があります！'
        }
      },
      message: {
        addSuccess: 'ファイルの追加に成功しました',
        editSuccess: 'ファイルの更新に成功しました',
        deleteSuccess: 'ファイルの削除に成功しました',
        deleteConfirm: 'ファイル「{name}」を削除してもよろしいですか？',
        exportSuccess: 'ファイルのエクスポートに成功しました',
        importSuccess: 'ファイルのインポートに成功しました',
        uploadSuccess: 'ファイルのアップロードに成功しました',
        downloadSuccess: 'ファイルのダウンロードに成功しました'
      },
      detail: {
        title: 'ファイル詳細'
      },
      upload: {
        success: 'ファイルのアップロードに成功しました',
        failed: 'ファイルのアップロードに失敗しました',
        fileEmpty: 'アップロードするファイルを選択してください',
        fileType: 'サポートされていないファイルタイプです',
        fileSize: 'ファイルサイズが制限を超えています',
        fileExists: 'ファイルは既に存在します',
        fileNameRequired: 'ファイル名は空にできません'
      }
    }
  }
} 