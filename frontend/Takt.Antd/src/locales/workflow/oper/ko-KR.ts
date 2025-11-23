export default {
  workflow: {
    history: {
      title: '워크플로우 이력',
      list: {
        title: '워크플로우 이력 목록',
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
      detail: {
        title: '워크플로우 이력 상세',
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
      }
    }
  }
} 