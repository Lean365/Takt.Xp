export default {
  // 시스템 설정
  config: {
    // 기본 정보
    name: '설정명',
    key: '설정 키',
    value: '설정값',
    builtin: '시스템 내장',
    order: '순서',
    remark: '비고',
    status: '상태',
    // 시간 정보
    createTime: '생성 시간',
    createBy: '생성자',
    updateTime: '수정 시간',
    updateBy: '수정자',
    // 작업
    operation: '작업',
    // 프롬프트 정보
    placeholder: {
      name: '설정명을 입력하세요',
      key: '설정 키를 입력하세요',
      value: '설정값을 입력하세요',
      builtin: '시스템 내장 여부를 선택하세요',
      order: '순서 번호를 입력하세요',
      remark: '비고를 입력하세요',
      status: '상태를 선택하세요'
    },
    // 검증 정보
    validation: {
      name: {
        required: '설정명을 입력하세요',
        maxLength: '설정명은 100자 이내로 입력하세요'
      },
      key: {
        required: '설정 키를 입력하세요',
        maxLength: '설정 키는 100자 이내로 입력하세요',
        pattern: '설정 키는 영문자로 시작하고, 영문자, 숫자, 밑줄, 점, 콜론만 사용할 수 있습니다'
      },
      value: {
        required: '설정값을 입력하세요',
        maxLength: '설정값은 500자 이내로 입력하세요'
      },
      builtin: {
        required: '시스템 내장 여부를 선택하세요'
      },
      order: {
        required: '순서 번호를 입력하세요',
        range: '순서 번호는 0에서 9999 사이로 입력하세요'
      },
      status: {
        required: '상태를 선택하세요'
      }
    },
    // 작업 결과
    message: {
      addSuccess: '설정 추가 성공',
      editSuccess: '설정 수정 성공',
      deleteSuccess: '설정 삭제 성공',
      deleteConfirm: '설정 "{name}"을(를) 삭제하시겠습니까?',
      exportSuccess: '설정 내보내기 성공',
      importSuccess: '설정 가져오기 성공',
      refreshSuccess: '캐시 업데이트 성공'
    }
  }
} 