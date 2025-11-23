export default {
  routine: {
    quartz: {
      // 기본 정보
      jobId: '작업 ID',
      jobName: '작업명',
      jobGroup: '작업 그룹',
      jobClass: '작업 클래스',
      jobMethod: '작업 메서드',
      jobParams: '작업 매개변수',
      cronExpression: 'Cron 표현식',
      jobStatus: '작업 상태',
      remark: '비고',
      createTime: '생성 시간',
      updateTime: '수정 시간',

      // 작업 버튼
      add: '작업 추가',
      edit: '작업 수정',
      delete: '작업 삭제',
      batchDelete: '일괄 삭제',
      export: '내보내기',
      import: '가져오기',
      downloadTemplate: '템플릿 다운로드',
      preview: '미리보기',
      execute: '실행',
      pause: '일시 중지',
      resume: '재개',

      // 폼 플레이스홀더
      placeholder: {
        jobId: '작업 ID를 입력하세요',
        jobName: '작업명을 입력하세요',
        jobGroup: '작업 그룹을 입력하세요',
        jobClass: '작업 클래스를 입력하세요',
        jobMethod: '작업 메서드를 입력하세요',
        jobParams: '작업 매개변수를 입력하세요',
        cronExpression: 'Cron 표현식을 입력하세요',
        jobStatus: '작업 상태를 선택하세요',
        remark: '비고를 입력하세요',
        startTime: '시작 시간',
        endTime: '종료 시간'
      },

      // 폼 유효성 검사
      validation: {
        jobId: {
          required: '작업 ID를 입력하세요',
          maxLength: '작업 ID는 100자 이내로 입력하세요'
        },
        jobName: {
          required: '작업명을 입력하세요',
          maxLength: '작업명은 50자 이내로 입력하세요'
        },
        jobGroup: {
          required: '작업 그룹을 입력하세요',
          maxLength: '작업 그룹은 50자 이내로 입력하세요'
        },
        jobClass: {
          required: '작업 클래스를 입력하세요',
          maxLength: '작업 클래스는 200자 이내로 입력하세요'
        },
        jobMethod: {
          required: '작업 메서드를 입력하세요',
          maxLength: '작업 메서드는 100자 이내로 입력하세요'
        },
        cronExpression: {
          required: 'Cron 표현식을 입력하세요',
          maxLength: 'Cron 표현식은 100자 이내로 입력하세요'
        },
        jobStatus: {
          required: '작업 상태를 선택하세요'
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
          execute: '실행되었습니다',
          pause: '일시 중지되었습니다',
          resume: '재개되었습니다'
        },
        failed: {
          add: '추가에 실패했습니다',
          edit: '수정에 실패했습니다',
          delete: '삭제에 실패했습니다',
          batchDelete: '일괄 삭제에 실패했습니다',
          export: '내보내기에 실패했습니다',
          import: '가져오기에 실패했습니다',
          execute: '실행에 실패했습니다',
          pause: '일시 중지에 실패했습니다',
          resume: '재개에 실패했습니다'
        }
      },

      // 상세 페이지
      detail: {
        title: '작업 상세'
      }
    }
  }
} 