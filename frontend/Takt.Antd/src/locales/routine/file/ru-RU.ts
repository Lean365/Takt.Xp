export default {
  routine: {
    file: {
      fileName: 'Имя файла',
      fileType: 'Тип файла',
      fileSize: 'Размер файла',
      filePath: 'Путь к файлу',
      fileUrl: 'URL файла',
      fileMd5: 'Файл MD5',
      fileOriginalName: 'Оригинальное имя файла',
      fileExtension: 'Расширение файла',
      fileStorageType: 'Тип хранения',
      fileStorageLocation: 'Место хранения',
      status: 'Статус',
      remark: 'Примечание',
      createTime: 'Время создания',
      updateTime: 'Время обновления',
      operation: 'Операция',
      placeholder: {
        fileName: 'Пожалуйста, введите имя файла',
        fileType: 'Пожалуйста, введите тип файла',
        fileSize: 'Пожалуйста, введите размер файла',
        status: 'Пожалуйста, выберите статус',
        remark: 'Пожалуйста, введите примечание'
      },
      validation: {
        fileName: {
          required: 'Пожалуйста, введите имя файла',
          maxLength: 'Имя файла не может превышать 100 символов'
        },
        fileType: {
          required: 'Пожалуйста, введите тип файла',
          maxLength: 'Тип файла не может превышать 50 символов'
        },
        status: {
          required: 'Пожалуйста, выберите статус'
        },
        file: {
          required: 'Пожалуйста, выберите файл для загрузки',
          size: 'Размер загружаемого файла не может превышать 2 МБ!'
        }
      },
      message: {
        addSuccess: 'Файл успешно добавлен',
        editSuccess: 'Файл успешно обновлен',
        deleteSuccess: 'Файл успешно удален',
        deleteConfirm: 'Вы уверены, что хотите удалить файл "{name}"?',
        exportSuccess: 'Файл успешно экспортирован',
        importSuccess: 'Файл успешно импортирован',
        uploadSuccess: 'Файл успешно загружен',
        downloadSuccess: 'Файл успешно скачан'
      },
      detail: {
        title: 'Детали файла'
      },
      upload: {
        success: 'Файл успешно загружен',
        failed: 'Ошибка загрузки файла',
        fileEmpty: 'Пожалуйста, выберите файл для загрузки',
        fileType: 'Тип файла не поддерживается',
        fileSize: 'Размер файла превышает лимит',
        fileExists: 'Файл уже существует',
        fileNameRequired: 'Имя файла не может быть пустым'
      }
    }
  }
} 