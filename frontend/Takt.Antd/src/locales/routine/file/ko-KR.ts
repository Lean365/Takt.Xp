export default {
  routine: {
    file: {
      fileName: '파일명',
      fileType: '파일 유형',
      fileSize: '파일 크기',
      filePath: '파일 경로',
      fileUrl: '파일 URL',
      fileMd5: '파일 MD5',
      fileOriginalName: '원본 파일명',
      fileExtension: '파일 확장자',
      fileStorageType: '저장 유형',
      fileStorageLocation: '저장 위치',
      status: '상태',
      remark: '비고',
      createTime: '생성일',
      updateTime: '수정일',
      operation: '작업',
      placeholder: {
        fileName: '파일명을 입력하세요',
        fileType: '파일 유형을 입력하세요',
        fileSize: '파일 크기를 입력하세요',
        status: '상태를 선택하세요',
        remark: '비고를 입력하세요'
      },
      validation: {
        fileName: {
          required: '파일명을 입력하세요',
          maxLength: '파일명은 100자 이내여야 합니다'
        },
        fileType: {
          required: '파일 유형을 입력하세요',
          maxLength: '파일 유형은 50자 이내여야 합니다'
        },
        status: {
          required: '상태를 선택하세요'
        },
        file: {
          required: '업로드할 파일을 선택하세요',
          size: '업로드 파일 크기는 2MB를 초과할 수 없습니다!'
        }
      },
      message: {
        addSuccess: '파일이 성공적으로 추가되었습니다',
        editSuccess: '파일이 성공적으로 수정되었습니다',
        deleteSuccess: '파일이 성공적으로 삭제되었습니다',
        deleteConfirm: '파일 "{name}"을(를) 삭제하시겠습니까?',
        exportSuccess: '파일이 성공적으로 내보내졌습니다',
        importSuccess: '파일이 성공적으로 가져와졌습니다',
        uploadSuccess: '파일이 성공적으로 업로드되었습니다',
        downloadSuccess: '파일이 성공적으로 다운로드되었습니다'
      },
      detail: {
        title: '파일 상세정보'
      },
      upload: {
        success: '파일이 성공적으로 업로드되었습니다',
        failed: '파일 업로드에 실패했습니다',
        fileEmpty: '업로드할 파일을 선택하세요',
        fileType: '지원되지 않는 파일 유형입니다',
        fileSize: '파일 크기가 제한을 초과했습니다',
        fileExists: '파일이 이미 존재합니다',
        fileNameRequired: '파일명은 비워둘 수 없습니다'
      }
    }
  }
} 