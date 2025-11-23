export default {
  routine: {
    mail: {
      // 기본 정보
      mailId: '메일 ID',
      mailType: '메일 유형',
      mailSubject: '메일 제목',
      mailContent: '메일 내용',
      mailStatus: '메일 상태',
      mailParams: '메일 매개변수',
      remark: '비고',
      createTime: '생성 시간',
      updateTime: '수정 시간',

      // 작업 버튼
      add: '메일 추가',
      edit: '메일 수정',
      delete: '메일 삭제',
      batchDelete: '일괄 삭제',
      export: '내보내기',
      import: '가져오기',
      downloadTemplate: '템플릿 다운로드',
      preview: '미리보기',
      send: '전송',

      // 폼 플레이스홀더
      placeholder: {
        mailId: '메일 ID를 입력하세요',
        mailType: '메일 유형을 선택하세요',
        mailSubject: '메일 제목을 입력하세요',
        mailContent: '메일 내용을 입력하세요',
        mailStatus: '메일 상태를 선택하세요',
        mailParams: '메일 매개변수를 입력하세요',
        remark: '비고를 입력하세요',
        startTime: '시작 시간',
        endTime: '종료 시간'
      },

      // 폼 유효성 검사
      validation: {
        mailId: {
          required: '메일 ID를 입력하세요',
          maxLength: '메일 ID는 100자 이내로 입력하세요'
        },
        mailType: {
          required: '메일 유형을 선택하세요',
          maxLength: '메일 유형은 50자 이내로 입력하세요'
        },
        mailSubject: {
          required: '메일 제목을 입력하세요',
          maxLength: '메일 제목은 200자 이내로 입력하세요'
        },
        mailContent: {
          required: '메일 내용을 입력하세요',
          maxLength: '메일 내용은 4000자 이내로 입력하세요'
        },
        mailStatus: {
          required: '메일 상태를 선택하세요'
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
        title: '메일 상세'
      }
    }
  }
} 