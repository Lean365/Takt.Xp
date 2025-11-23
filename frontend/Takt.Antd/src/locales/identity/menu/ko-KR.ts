export default {
  identity: {
    menu: {
      title: '메뉴 관리',
      columns: {
        menuName: '메뉴 이름',
        transKey: 'I18n 키',
        parentId: '상위 메뉴',
        orderNum: '순서',
        path: '라우트 경로',
        component: '컴포넌트 경로',
        queryParams: '라우트 파라미터',
        isExternal: '외부 링크',
        isCache: '캐시',
        menuType: '메뉴 유형',
        visible: '표시 상태',
        status: '상태',
        perms: '권한 키',
        icon: '아이콘',
        id: 'ID',
        createBy: '생성자',
        createTime: '생성 시간',
        updateBy: '수정자',
        updateTime: '수정 시간',
        deleteBy: '삭제자',
        deleteTime: '삭제 시간',
        isDeleted: '삭제됨',
        remark: '비고',
        action: '작업'
      },
      fields: {
        menuName: {
          label: '메뉴 이름',
          placeholder: '메뉴 이름을 입력하세요',
          validation: {
            required: '메뉴 이름을 입력하세요',
            length: '메뉴 이름은 2~50자여야 합니다'
          }
        },
        transKey: {
          label: 'I18n 키',
          placeholder: 'I18n 키를 입력하세요'
        },
        parentId: {
          label: '상위 메뉴',
          placeholder: '상위 메뉴를 선택하세요',
          root: '루트 메뉴'
        },
        orderNum: {
          label: '순서',
          placeholder: '순서를 입력하세요',
          validation: {
            required: '순서를 입력하세요'
          }
        },
        path: {
          label: '라우트 경로',
          placeholder: '라우트 경로를 입력하세요'
        },
        component: {
          label: '컴포넌트 경로',
          placeholder: '컴포넌트 경로를 입력하세요'
        },
        queryParams: {
          label: '라우트 파라미터',
          placeholder: '라우트 파라미터를 입력하세요'
        },
        isExternal: {
          label: '외부 링크',
          placeholder: '외부 링크 여부를 선택하세요',
          options: {
            yes: '예',
            no: '아니오'
          }
        },
        isCache: {
          label: '캐시',
          placeholder: '캐시 여부를 선택하세요',
          options: {
            yes: '예',
            no: '아니오'
          }
        },
        menuType: {
          label: '메뉴 유형',
          options: {
            directory: '디렉터리',
            menu: '메뉴',
            button: '버튼'
          },
          validation: {
            required: '메뉴 유형을 선택하세요'
          }
        },
        visible: {
          label: '표시 상태',
          placeholder: '표시 상태를 선택하세요',
          options: {
            show: '표시',
            hide: '숨김'
          }
        },
        status: {
          label: '상태',
          placeholder: '상태를 선택하세요',
          options: {
            normal: '정상',
            disabled: '비활성'
          }
        },
        perms: {
          label: '권한 키',
          placeholder: '권한 키를 입력하세요'
        },
        icon: {
          label: '메뉴 아이콘',
          placeholder: '메뉴 아이콘을 입력하세요'
        },
        }
      },
      dialog: {
        create: '메뉴 추가',
        update: '메뉴 수정',
        delete: '메뉴 삭제'
      },
      operation: {
        add: {
          title: '메뉴 추가',
          success: '추가 성공',
          failed: '추가 실패'
        },
        edit: {
          title: '메뉴 수정',
          success: '수정 성공',
          failed: '수정 실패'
        },
        delete: {
          title: '메뉴 삭제',
          confirm: '이 메뉴를 삭제하시겠습니까?',
          success: '삭제 성공',
          failed: '삭제 실패'
        }
      }
    }
  }
