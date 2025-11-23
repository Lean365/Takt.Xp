export default {
  routine: {
    notice: {
      // 기본 정보
      noticeId: '공지 ID',
      noticeType: '공지 유형',
      noticeTitle: '공지 제목',
      noticeContent: '공지 내용',
      noticeStatus: '공지 상태',
      noticeParams: '공지 매개변수',
      remark: '비고',
      createTime: '생성 시간',
      updateTime: '수정 시간',

      // 작업 버튼
      add: '공지 추가',
      edit: '공지 수정',
      delete: '공지 삭제',
      batchDelete: '일괄 삭제',
      export: '내보내기',
      import: '가져오기',
      downloadTemplate: '템플릿 다운로드',
      preview: '미리보기',
      send: '전송',

      // 폼 플레이스홀더
      placeholder: {
        noticeId: '공지 ID를 입력하세요',
        noticeType: '공지 유형을 선택하세요',
        noticeTitle: '공지 제목을 입력하세요',
        noticeContent: '공지 내용을 입력하세요',
        noticeStatus: '공지 상태를 선택하세요',
        noticeParams: '공지 매개변수를 입력하세요',
        remark: '비고를 입력하세요',
        startTime: '시작 시간',
        endTime: '종료 시간'
      },

      // 폼 유효성 검사
      validation: {
        noticeId: {
          required: '공지 ID를 입력하세요',
          maxLength: '공지 ID는 100자 이내로 입력하세요'
        },
        noticeType: {
          required: '공지 유형을 선택하세요',
          maxLength: '공지 유형은 50자 이내로 입력하세요'
        },
        noticeTitle: {
          required: '공지 제목을 입력하세요',
          maxLength: '공지 제목은 200자 이내로 입력하세요'
        },
        noticeContent: {
          required: '공지 내용을 입력하세요',
          maxLength: '공지 내용은 4000자 이내로 입력하세요'
        },
        noticeStatus: {
          required: '공지 상태를 선택하세요'
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
        title: '공지 상세'
      }
    }
  }
} 