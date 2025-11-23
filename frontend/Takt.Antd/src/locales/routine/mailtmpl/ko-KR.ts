export default {
  routine: {
    mailtmpl: {
      // 기본 정보
      templateName: '템플릿 이름',
      templateType: '템플릿 유형',
      templateSubject: '템플릿 제목',
      templateContent: '템플릿 내용',
      templateStatus: '템플릿 상태',
      templateParams: '템플릿 매개변수',
      remark: '비고',
      createTime: '생성 시간',
      updateTime: '수정 시간',

      // 작업 버튼
      add: '템플릿 추가',
      edit: '템플릿 수정',
      delete: '템플릿 삭제',
      batchDelete: '일괄 삭제',
      export: '내보내기',
      import: '가져오기',
      downloadTemplate: '템플릿 다운로드',
      preview: '미리보기',
      send: '보내기',

      // 폼 플레이스홀더
      placeholder: {
        templateName: '템플릿 이름을 입력하세요',
        templateType: '템플릿 유형을 선택하세요',
        templateSubject: '템플릿 제목을 입력하세요',
        templateContent: '템플릿 내용을 입력하세요',
        templateStatus: '템플릿 상태를 선택하세요',
        templateParams: '템플릿 매개변수를 입력하세요',
        remark: '비고를 입력하세요',
        startTime: '시작 시간',
        endTime: '종료 시간'
      },

      // 폼 유효성 검사
      validation: {
        templateName: {
          required: '템플릿 이름을 입력하세요',
          maxLength: '템플릿 이름은 100자 이내로 입력하세요'
        },
        templateType: {
          required: '템플릿 유형을 선택하세요',
          maxLength: '템플릿 유형은 50자 이내로 입력하세요'
        },
        templateSubject: {
          required: '템플릿 제목을 입력하세요',
          maxLength: '템플릿 제목은 200자 이내로 입력하세요'
        },
        templateContent: {
          required: '템플릿 내용을 입력하세요',
          maxLength: '템플릿 내용은 4000자 이내로 입력하세요'
        },
        templateStatus: {
          required: '템플릿 상태를 선택하세요'
        }
      },

      // 작업 결과
      message: {
        success: {
          add: '추가되었습니다',
          edit: '수정되었습니다',
          delete: '삭제되었습니다',
          batchDelete: '일괄 삭제되었습니다',
          export: '내보내기되었습니다',
          import: '가져오기되었습니다',
          send: '전송되었습니다'
        },
        failed: {
          add: '추가에 실패했습니다',
          edit: '수정에 실패했습니다',
          delete: '삭제에 실패했습니다',
          batchDelete: '일괄 삭제에 실패했습니다',
          export: '내보내기에 실패했습니다',
          import: '가져오기에 실패했습니다',
          send: '전송에 실패했습니다'
        }
      },

      // 상세 페이지
      detail: {
        title: '이메일 템플릿 상세'
      }
    }
  }
}
