export default {
  workflow: {
    definition: {
      title: '워크플로우 정의',
      list: {
        title: '워크플로우 정의 목록',
        search: {
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          status: '상태'
        },
        table: {
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          status: '상태',
          createTime: '생성 시간',
          updateTime: '수정 시간',
          actions: '작업'
        },
        actions: {
          create: '생성',
          edit: '수정',
          delete: '삭제',
          view: '보기',
          deploy: '배포',
          export: '내보내기',
          import: '가져오기',
          refresh: '새로고침'
        },
        status: {
          draft: '초안',
          deployed: '배포됨',
          disabled: '비활성화'
        }
      },
      form: {
        title: {
          create: '워크플로우 정의 생성',
          edit: '워크플로우 정의 수정'
        },
        fields: {
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          description: '설명',
          status: '상태'
        },
        rules: {
          name: {
            required: '워크플로우 이름을 입력하세요',
            max: '워크플로우 이름은 50자 이내로 입력하세요'
          },
          key: {
            required: '워크플로우 키를 입력하세요',
            pattern: '워크플로우 키는 영문자, 숫자, 밑줄만 사용할 수 있습니다',
            max: '워크플로우 키는 50자 이내로 입력하세요'
          },
          version: {
            required: '버전 번호를 입력하세요',
            pattern: '버전 번호 형식이 잘못되었습니다. x.y.z 형식으로 입력하세요'
          }
        },
        buttons: {
          submit: '제출',
          cancel: '취소'
        }
      },
      detail: {
        title: '워크플로우 정의 상세',
        basic: {
          title: '기본 정보',
          name: '워크플로우 이름',
          key: '워크플로우 키',
          version: '버전',
          description: '설명',
          status: '상태',
          createTime: '생성 시간',
          updateTime: '수정 시간'
        },
        actions: {
          edit: '수정',
          back: '돌아가기'
        }
      }
    }
  }
} 