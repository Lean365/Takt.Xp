export default {
  workflow: {
    instance: {
      title: '워크플로우 인스턴스',
      list: {
        title: '워크플로우 인스턴스 목록',
        search: {
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간'
        },
        table: {
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간',
          duration: '실행 시간',
          actions: '작업'
        },
        actions: {
          view: '보기',
          delete: '삭제',
          terminate: '종료',
          export: '내보내기',
          import: '가져오기',
          refresh: '새로고침'
        },
        status: {
          running: '실행 중',
          completed: '완료',
          terminated: '종료',
          failed: '실패'
        }
      },
      form: {
        title: {
          create: '워크플로우 인스턴스 생성',
          import: '워크플로우 인스턴스 가져오기'
        },
        fields: {
          workflowDefinitionId: '워크플로우 정의',
          variables: '변수 설정'
        },
        rules: {
          workflowDefinitionId: {
            required: '워크플로우 정의를 선택하세요'
          }
        },
        buttons: {
          submit: '제출',
          cancel: '취소'
        }
      },
      detail: {
        title: '워크플로우 인스턴스 상세',
        basic: {
          title: '기본 정보',
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간',
          duration: '실행 시간'
        },
        nodes: {
          title: '노드 정보',
          name: '노드 이름',
          type: '노드 유형',
          status: '상태',
          startTime: '시작 시간',
          endTime: '종료 시간',
          duration: '실행 시간',
          input: '입력',
          output: '출력',
          error: '오류'
        },
        actions: {
          back: '돌아가기'
        }
      },
      terminate: {
        title: '워크플로우 인스턴스 종료',
        confirm: '이 인스턴스를 종료하시겠습니까?',
        reason: '종료 사유',
        buttons: {
          submit: '제출',
          cancel: '취소'
        }
      }
    }
  }
} 