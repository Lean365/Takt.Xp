export default {
  identity: {
    user: {
      title: '사용자 관리',
      profile: '개인 정보',
      changePasswordTitle: '비밀번호 변경',
      
      // 비밀번호 관련
      password: {
        old: {
          label: '기존 비밀번호',
          placeholder: '기존 비밀번호를 입력하세요',
          validation: {
            required: '기존 비밀번호는 필수입니다',
          }
        },
        new: {
          label: '새 비밀번호',
          placeholder: '새 비밀번호를 입력하세요',
          validation: {
            required: '새 비밀번호는 필수입니다',
          }
        },
        confirm: {
          label: '비밀번호 확인',
          placeholder: '비밀번호를 다시 입력하세요',
          validation: {
            required: '비밀번호 확인은 필수입니다',
          }
        },
      },
      tabs: {
        userInfo: '사용자 정보',
        organization: '조직 관계',
        avatar: '아바타'
      },
      // 폼 필드 정의
      fields: {
        userId: {
          label: '사용자 ID'
        },
        userName: {
          label: '사용자명',
          placeholder: '사용자명을 입력하세요',
          validation: {
            required: '사용자명은 필수입니다',
            format: '소문자로 시작해야 하며, 소문자, 숫자, 하이픈만 포함 가능하며, 점이나 밑줄은 불가, 길이는 4-50자'
          }
        },
        nickName: {
          label: '닉네임',
          placeholder: '닉네임을 입력하세요',
          validation: {
            required: '닉네임은 필수입니다',
            format: '2-50자, 한글, 일본어, 한국어, 영어, 숫자, 영문 마침표, 하이픈 허용, 밑줄이나 공백 불가'
          }
        },
        realName: {
          label: '실명',
          placeholder: '실명을 입력하세요',
          validation: {
            required: '실명은 필수입니다',
            format: '실명 길이는 2-20자, 한글, 영어, 숫자, 밑줄만 포함 가능'
          }
        },
        fullName: {
          label: '전체 이름',
          placeholder: '전체 이름을 입력하세요',
          validation: {
            required: '전체 이름은 필수입니다',
            format: '전체 이름 길이는 2-20자, 한글, 영어, 숫자, 밑줄만 포함 가능'
          }
        },
        englishName: {
          label: '영문명',
          placeholder: '영문명을 입력하세요',
          validation: {
            required: '영문명은 필수입니다',
            format: '영문명 길이는 2-100자, 문자로 시작해야 하며, 영문자, 공백, 하이픈, 영문 마침표만 포함 가능'
          }
        },
        password: {
          label: '비밀번호',
          placeholder: '비밀번호를 입력하세요',
          validation: {
            required: '비밀번호는 필수입니다',
            format: '비밀번호 길이는 6-20자, 영문자, 숫자, 특수문자만 포함 가능'
          }
        },
        userType: {
          label: '사용자 유형',
          placeholder: '사용자 유형을 선택하세요',
          options: {
            admin: '관리자',
            user: '일반 사용자'
          }
        },
        email: {
          label: '이메일',
          placeholder: '이메일을 입력하세요',
          validation: {
            required: '이메일은 필수입니다',
            invalid: '이메일 길이는 6-100자이고 올바른 형식이어야 합니다'
          }
        },
        phoneNumber: {
          label: '휴대폰 번호',
          placeholder: '휴대폰 번호를 입력하세요',
          validation: {
            required: '휴대폰 번호는 필수입니다',
            invalid: '올바른 휴대폰 또는 유선 전화번호 형식을 입력하세요'
          }
        },
        gender: {
          label: '성별',
          placeholder: '성별을 선택하세요',
          options: {
            male: '남성',
            female: '여성',
            unknown: '알 수 없음'
          }
        },
        avatar: {
          label: '아바타',
          upload: '아바타 업로드',
          currentAvatar: '현재 아바타',
          avatarUpload: '아바타 업로드',
          uploadSuccess: '아바타 업로드 성공',
          uploadError: '아바타 업로드 실패',
          uploading: '아바타 업로드 중...',
          onlyImage: '이미지 파일만 업로드 가능합니다!',
          sizeLimit: '이미지 크기는 5MB를 초과할 수 없습니다!'
        },
        status: {
          label: '상태',
          placeholder: '상태를 선택하세요',
          options: {
            enabled: '활성화',
            disabled: '비활성화'
          }
        },
        lastPasswordChangeTime: {
          label: '마지막 비밀번호 변경 시간'
        },
        lockEndTime: {
          label: '잠금 종료 시간'
        },
        lockReason: {
          label: '잠금 사유'
        },
        isLock: {
          label: '잠금 여부'
        },
        errorLimit: {
          label: '오류 횟수 제한'
        },
        loginCount: {
          label: '로그인 횟수'
        },
        deptName: {
          label: '소속 부서',
          placeholder: '소속 부서를 선택하세요',
          validation: {
            required: '소속 부서는 필수입니다'
          }
        },
        role: {
          label: '소속 역할',
          placeholder: '소속 역할을 선택하세요',
          validation: {
            required: '소속 역할은 필수입니다'
          }
        },
        post: {
          label: '소속 직위',
          placeholder: '소속 직위를 선택하세요',
          validation: {
            required: '소속 직위는 필수입니다'
          }
        },
        remark: {
          label: '비고',
          placeholder: '비고를 입력하세요'
        }
      },

      // 작업 버튼
      actions: {
        add: '사용자 추가',
        edit: '사용자 수정',
        'delete': '사용자 삭제',
        resetPassword: '비밀번호 재설정',
        export: '사용자 내보내기'
      },

      // 메시지 프롬프트
      messages: {
        confirmDelete: '선택한 사용자를 삭제하시겠습니까?',
        confirmResetPassword: '선택한 사용자의 비밀번호를 재설정하시겠습니까?',
        confirmToggleStatus: '이 사용자를 {action}하시겠습니까?',
        deleteSuccess: '사용자 삭제 성공',
        deleteFailed: '사용자 삭제 실패',
        saveSuccess: '사용자 정보 저장 성공',
        saveFailed: '사용자 정보 저장 실패',
        resetPasswordSuccess: '비밀번호 재설정 성공',
        resetPasswordFailed: '비밀번호 재설정 실패',
        importSuccess: '사용자 가져오기 성공',
        importFailed: '사용자 가져오기 실패',
        exportSuccess: '사용자 내보내기 성공',
        exportFailed: '사용자 내보내기 실패',
        toggleStatusSuccess: '상태 변경 성공',
        toggleStatusFailed: '상태 변경 실패',
        cannotDeleteAdmin: '관리자 사용자는 삭제할 수 없습니다!',
        cannotEditAdmin: '관리자 사용자는 수정할 수 없습니다!',
        cannotUpdateAdminStatus: '관리자 사용자 상태는 변경할 수 없습니다!',
        cannotResetAdminPassword: '관리자 사용자 비밀번호는 재설정할 수 없습니다!',
        cannotUnlockAdmin: '관리자 사용자는 잠금 해제할 수 없습니다!',
        cannotAllocateRole: '관리자 사용자는 역할 할당할 수 없습니다!',
        cannotAllocateDept: '관리자 사용자는 부서 할당할 수 없습니다!',
        cannotAllocatePost: '관리자 사용자는 직위 할당할 수 없습니다!',
        statusUpdateSuccess: '상태 업데이트 성공',
        statusUpdateFailed: '상태 업데이트 실패',
        lockStatusUpdateSuccess: '잠금 상태 업데이트 성공',
        lockStatusUpdateFailed: '잠금 상태 업데이트 실패'
      },

      // 테이블 표시 텍스트
      table: {
        notLocked: '잠금되지 않음',
        none: '없음',
        queryParams: '쿼리 매개변수',
        importResponseData: '가져오기 응답 데이터',
        parsedData: '구문 분석된 데이터',
        exportFailed: '내보내기 실패',
        downloadTemplateFailed: '템플릿 다운로드 실패',
        rowClicked: '행 클릭됨',
        toggleFullscreenState: '전체화면 상태 전환',
        getOptionsFailed: '옵션 가져오기 실패',
        createUser: '사용자 생성',
        updateUser: '사용자 업데이트'
      },

      // 가져오기 팁
      importTips: 'Excel 워크시트 이름은 "User"여야 합니다',

      // 탭
      tab: {
        basic: '기본 정보',
        profile: '개인 프로필',
        role: '역할 권한',
        dept: '부서 직위',
        other: '기타 정보',
        avatar: '아바타 설정',
        loginLog: '로그인 로그',
        operateLog: '작업 로그',
        errorLog: '예외 로그',
        taskLog: '작업 로그'
      },

      // 가져오기/내보내기
      import: {
        title: '사용자 가져오기',
        template: '템플릿 다운로드',
        success: '가져오기 성공',
        failed: '가져오기 실패',
        fileName: '사용자 데이터'
      },
      export: {
        title: '사용자 내보내기',
        fileName: '사용자 데이터',
        success: '내보내기 성공',
        failed: '내보내기 실패'
      },
      template: {
        fileName: '사용자 가져오기 템플릿',
        downloadFailed: '템플릿 다운로드 실패'
      },

      // 비밀번호 재설정
      resetPwd: '비밀번호 재설정',
      
      // 비밀번호 변경
      changePassword: {
        success: '비밀번호 변경 성공',
        failed: '비밀번호 변경 실패, 다시 시도해주세요',
        changeFailed: '비밀번호 변경 실패'
      },

      // 할당 관련
      allocateDept: '부서 할당',
      allocatePost: '직위 할당',
      allocateRole: '역할 할당',
      
      roleAllocate: {
        unallocated: '할당되지 않음',
        allocated: '할당됨',
        loadRoleListFailed: '역할 목록 로드 실패',
        startLoadUserRoles: '사용자 역할 로드 시작',
        userRolesApiResponse: '사용자 역할 API 응답',
        setSelectedRoles: '선택된 역할 설정',
        loadUserRolesFailed: '사용자 역할 로드 실패'
      },
      
      postAllocate: {
        unallocated: '할당되지 않음',
        allocated: '할당됨',
        loadPostListFailed: '직위 목록 로드 실패',
        loadUserPostsFailed: '사용자 직위 로드 실패'
      }
    }
  }
}
