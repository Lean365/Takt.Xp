export default {
  common: {
    // ==================== 시스템 정보 ====================
    system: {
      title: '블랙아이스 플랫폼',
      slogan: '전문적이고 효율적이며 안전한 기업 관리 시스템',
      description: '.NET 8과 Vue 3 기반의 최신 기업 관리 시스템',
      copyright: '© 2024Takt(Claude AI). All rights reserved.'
    },

    // ==================== 오류 메시지 ====================
    error: {
      clientError: '클라이언트 요청 오류, 요청 매개변수를 확인하세요',
      systemRestart: '시스템이 유지보수 중입니다. 나중에 다시 로그인하세요',
      network: '네트워크 연결에 실패했습니다. 네트워크 설정을 확인하세요',
      unauthorized: '인증되지 않았습니다. 다시 로그인하세요',
      forbidden: '접근이 거부되었습니다',
      notFound: '요청한 리소스가 존재하지 않습니다',
      badRequest: '잘못된 요청 매개변수',
      serverError: '서버 내부 오류',
      serviceUnavailable: '서비스가 일시적으로 사용할 수 없습니다',
      badGateway: '잘못된 게이트웨이',
      gatewayTimeout: '게이트웨이 시간 초과',
      unknown: '알 수 없는 오류'
    },

    // ==================== 기본 작업 ====================
    // 기본 제목
    title: {
      list: '목록',
      detail: '상세',
      create: '새로 추가',
      edit: '편집',
      preview: '미리보기'
    },

    // ==================== 상태 정의 ====================
    status: {
      // 기본 상태
      base: {
        label: '상태',
        normal: '정상',
        disabled: '비활성화',
        placeholder: '상태를 선택하세요'
      },

      // 예/아니오 상태
      yesNo: {
        all: '모든',
        yes: '예',
        no: '아니오'
      },

      // 표시 상태
      visible: {
        show: '표시',
        hide: '숨김'
      },

      // 캐시 상태
      cache: {
        enabled: '활성화',
        disabled: '비활성화'
      },

      // 온라인 상태
      online: {
        online: '온라인',
        offline: '오프라인'
      },

      // 처리 상태
      process: {
        pending: '대기 중',
        processing: '처리 중',
        completed: '완료',
        failed: '실패'
      },

      // 검증 상태
      verify: {
        unverified: '미검증',
        verified: '검증됨',
        failed: '검증 실패'
      },

      // 잠금 상태
      lock: {
        locked: '잠금',
        unlocked: '잠금 해제'
      },

      // 게시 상태
      publish: {
        draft: '초안',
        published: '게시됨',
        unpublished: '미게시'
      },

      // 동기화 상태
      sync: {
        synced: '동기화됨',
        unsynced: '미동기화',
        syncing: '동기화 중'
      }
    },

    // ==================== 폼 작업 ====================
    form: {
      required: '필수',
      optional: '선택',
      invalid: '유효하지 않음',
      // 폼 플레이스홀더
      placeholder: {
        select: '선택하세요',
        input: '입력하세요',
        date: '날짜를 선택하세요',
        time: '시간을 선택하세요'
      },
      // 사용자 폼
      user: {
        // 기본 정보
        userId: {
          label: '사용자 ID',
          placeholder: '사용자 ID를 입력하세요'
        },
        userName: {
          label: '사용자 이름',
          placeholder: '사용자 이름을 입력하세요'
        },
        nickName: {
          label: '닉네임',
          placeholder: '닉네임을 입력하세요'
        },
        realName: {
          label: '실명',
          placeholder: '실명을 입력하세요'
        },
        englishName: {
          label: '영문 이름',
          placeholder: '영문 이름을 입력하세요'
        },
        password: {
          label: '비밀번호',
          placeholder: '비밀번호를 입력하세요'
        },
        confirmPassword: {
          label: '비밀번호 확인',
          placeholder: '비밀번호를 다시 입력하세요'
        },
        
        // 개인 정보
        avatar: {
          label: '아바타',
          placeholder: '아바타를 업로드하세요'
        },
        gender: {
          label: '성별',
          placeholder: '성별을 선택하세요',
          options: {
            male: '남성',
            female: '여성',
            other: '기타'
          }
        },
        birthday: {
          label: '생년월일',
          placeholder: '생년월일을 선택하세요'
        },
        
        // 연락처 정보
        email: {
          label: '이메일',
          placeholder: '이메일을 입력하세요'
        },
        phoneNumber: {
          label: '휴대폰',
          placeholder: '휴대폰 번호를 입력하세요'
        },
        telephone: {
          label: '전화번호',
          placeholder: '전화번호를 입력하세요'
        },
        
        // 조직 정보
        deptId: {
          label: '부서',
          placeholder: '부서를 선택하세요'
        },
        roleId: {
          label: '역할',
          placeholder: '역할을 선택하세요'
        },
        postId: {
          label: '직위',
          placeholder: '직위를 선택하세요'
        },
        
        // 계정 정보
        userType: {
          label: '사용자 유형',
          placeholder: '사용자 유형을 선택하세요',
          options: {
            system: '시스템 사용자',
            normal: '일반 사용자'
          }
        },
        status: {
          label: '상태',
          placeholder: '상태를 선택하세요'
        },
        loginIp: {
          label: '마지막 로그인 IP',
          placeholder: '마지막 로그인 IP'
        },
        loginDate: {
          label: '마지막 로그인 시간',
          placeholder: '마지막 로그인 시간'
        },
        
        // 기타 정보
        remark: {
          label: '비고',
          placeholder: '비고를 입력하세요'
        },
        sort: {
          label: '순서',
          placeholder: '순서 번호를 입력하세요'
        }
      },
      // 비고 정보
      remark: {
        label: '비고',
        placeholder: '비고를 입력하세요'
      }
    },

    // ==================== 테이블 작업 ====================
    table: {
      header: {
        operation: '작업'
      },
      config: {
        density: {
          default: '기본',
          middle: '중간',
          small: '작게'
        },
        columnSetting: '열 설정'
      },
      pagination: {
        total: '전체 {total}개',
        current: '페이지 {current}',
        pageSize: '페이지당 {pageSize}개',
        jump: '이동'
      },
      empty: '데이터 없음',
      loading: '로딩 중...',
      selectAll: '전체 선택',
      selected: '{total}개 선택됨'
    },

    // ==================== 시간 작업 ====================
    datetime: {
      date: '날짜',
      time: '시간',
      year: '년',
      month: '월',
      day: '일',
      hour: '시',
      minute: '분',
      second: '초',
      startDate: '시작일',
      endDate: '종료일',
      startTime: '시작 시간',
      endTime: '종료 시간',
      createTime: '생성 시간',
      updateTime: '업데이트 시간',
      formatError: '시간 형식 변환 실패',
      relativeTimeFormatError: '상대 시간 형식 변환 실패',
      parseError: '날짜 파싱 실패',
      rangeSeparator: ' ~ '
    },

    // ==================== 파일 작업 ====================
    // 가져오기/내보내기
    import: {
      title: '데이터 가져오기',
      file: '파일 선택',
      select: '파일을 선택하세요',
      template: '템플릿 다운로드',
      download: '템플릿을 다운로드하세요',
      note: '가져오기 설명',
      tips: '가져오기 템플릿 형식을 엄격히 따르세요. 그렇지 않으면 가져오기가 실패할 수 있습니다',
      format: 'Excel 파일만 지원됩니다!',
      size: '파일 크기는 {size}MB를 초과할 수 없습니다!',
      total: '전체 레코드 수',
      success: '성공 수',
      failed: '실패 수',
      message: '실패 이유',
      dragText: '클릭하거나 파일을 이 영역으로 드래그하여 업로드',
      dragHint: '.xlsx 형식의 Excel 파일을 지원합니다',
      sheetName: 'Excel 파일의 시트 이름이 다음인지 확인하세요: {sheetName}',
      allSuccess: '가져오기 성공 {count} 레코드, 모두 성공!',
      partialSuccess: '가져오기 성공 {success} 레코드, 실패 {fail} 레코드',
      allFailed: '모든 가져오기 실패, 총 {count} 레코드',
      noData: '데이터가 읽히지 않았습니다',
      empty: '파일이 비어 있습니다, 업로드할 수 없습니다',
      importFailed: '가져오기에 실패했습니다',
      templateFileName: 'Import_Template_{time}.xlsx',
      limits: {
        title: '가져오기 제한',
        fileCount: '파일 개수 제한: {count}개 파일',
        fileSize: '파일 크기 제한: {size}MB',
        recordCount: '레코드 수 제한: {count}개',
        fileFormat: '파일 형식: .xlsx 형식만 지원'
      },
      recordLimit: '가져올 레코드 수({actual} 레코드)가 제한({max} 레코드)을 초과합니다. 배치로 가져오세요'
    },

    // 업로드
    upload: {
      text: '파일을 드래그 앤 드롭하거나 클릭하여 업로드',
      picture: '클릭하여 이미지 업로드',
      file: '클릭하여 파일 업로드',
      icon: '클릭하여 아이콘 업로드',
      limit: {
        size: '파일 크기는 {size}를 초과할 수 없습니다',
        type: '{type} 형식만 지원됩니다'
      }
    },

    // ==================== 기능 버튼 ====================
    actions: {
      // === 기본 작업 버튼 ===
      add: '추가',           // @btn-add-color
      edit: '편집',         // @btn-edit-color
      delete: '삭제',      // @btn-delete-color
      batchDelete: '일괄 삭제', // @btn-batch-delete-color
      view: '보기',          // @btn-view-color
      clear: '지우기',       // @btn-clear-color
      forceOffline: '강제 오프라인', // @btn-force-offline-color
      onlineStatus: '온라인 상태', // @btn-online-status-color
      loginHistory: '로그인 기록', // @btn-login-history-color
      sendMail: '메일 보내기', // @btn-send-mail-color
      viewMail: '메일 보기', // @btn-view-mail-color
      mailTemplate: '메일 템플릿', // @btn-mail-template-color
      sendNotification: '알림 보내기', // @btn-send-notification-color
      viewNotification: '알림 보기', // @btn-view-notification-color
      notificationSetting: '알림 설정', // @btn-notification-setting-color
      sendMessage: '메시지 보내기', // @btn-send-message-color
      viewMessage: '메시지 보기', // @btn-view-message-color
      messageSetting: '메시지 설정', // @btn-message-setting-color

      // === 데이터 작업 버튼 ===
      import: '가져오기',       // @btn-import-color
      export: '내보내기',       // @btn-export-color
      template: '템플릿',       // @btn-template-color
      preview: '미리보기',        // @btn-preview-color
      download: '다운로드',  // @btn-download-color
      batchImport: '일괄 가져오기', // @btn-batch-import-color
      batchExport: '일괄 내보내기', // @btn-batch-export-color
      batchPrint: '일괄 인쇄', // @btn-batch-print-color
      batchEdit: '일괄 편집',  // @btn-batch-edit-color
      batchUpdate: '일괄 업데이트', // @btn-batch-update-color

      // === 상태 작업 버튼 ===
      logging: '감사',         // @btn-audit-color
      revoke: '취소',        // @btn-revoke-color
      stop: '중지',          // @btn-stop-color
      run: '실행',           // @btn-run-color
      force: '강제',         // @btn-forced-color
      start: '시작',         // @btn-start-color
      suspend: '일시정지',    // @btn-suspend-color
      resume: '재개',        // @btn-resume-color
      submit: '제출',        // @btn-submit-color
      withdraw: '철회',      // @btn-withdraw-color
      terminate: '종료',      // @btn-terminate-color

      // === 시스템 기능 버튼 ===
      generate: '생성',      // @btn-generate-color
      refresh: '새로고침',    // @btn-refresh-color
      info: '정보',      // @btn-info-color
      log: '로그',           // @btn-log-color
      chat: '채팅',          // @btn-chat-color
      copy: '복사',           // @btn-copy-color
      execute: '실행',      // @btn-execute-color
      resetPwd: '비밀번호 재설정', // @btn-reset-pwd-color
      open: '열기',           // @btn-open-color
      close: '닫기',          // @btn-close-color
      more: '더보기',             // @btn-more-color
      density: '밀도',       // @btn-density-color
      columnSetting: '열 설정', // @btn-column-setting-color

      // === 확장 기능 버튼 ===
      search: '검색',     // @btn-search-color
      filter: '필터',        // @btn-filter-color
      sort: '정렬',            // @btn-sort-color
      config: '설정',     // @btn-config-color
      save: '저장',      // @btn-save-color
      cancel: '취소',        // @btn-cancel-color
      upload: '업로드',    // @btn-upload-color
      print: '인쇄',        // @btn-print-color
      help: '도움말',             // @btn-help-color
      share: '공유',        // @btn-share-color
      lock: '잠금',      // @btn-lock-color
      sync: '동기화',     // @btn-sync-color
      expand: '확장',     // @btn-expand-color
      collapse: '축소',      // @btn-collapse-color
      approve: '승인',     // @btn-approve-color
      reject: '거절',        // @btn-reject-color
      comment: '댓글',     // @btn-comment-color
      attach: '첨부',        // @btn-attach-color

      // === 언어 지원 버튼 ===
      translate: '번역',    // @btn-translate-color
      langSwitch: '언어 전환', // @btn-lang-switch-color
      dict: '사전',     // @btn-dict-color

      // === 데이터 분석 버튼 ===
      analyze: '분석',      // @btn-analyze-color
      chart: '차트',       // @btn-chart-color
      report: '보고서',        // @btn-report-color
      dashboard: '대시보드', // @btn-dashboard-color
      statistics: '통계', // @btn-statistics-color
      forecast: '예측',    // @btn-forecast-color
      compare: '비교',      // @btn-compare-color

      // === 워크플로우 버튼 ===
      startFlow: '플로우 시작', // @btn-start-flow-color
      endFlow: '플로우 종료', // @btn-end-flow-color
      suspendFlow: '플로우 일시 중지', // @btn-suspend-flow-color
      resumeFlow: '플로우 재개', // @btn-resume-flow-color
      transfer: '전송',    // @btn-transfer-color
      delegate: '위임',      // @btn-delegate-color
      notify: '알림',        // @btn-notify-color
      urge: '촉구',             // @btn-urge-color
      sign: '서명',            // @btn-sign-color
      countersign: '합의', // @btn-countersign-color

      // === 모바일 전용 버튼 ===
      scan: '스캔',          // @btn-scan-color
      location: '위치', // @btn-location-color
      call: '통화',          // @btn-call-color
      photo: '사진 촬영', // @btn-photo-color
      voice: '음성',            // @btn-voice-color
      faceId: '얼굴 인증',      // @btn-face-id-color
      fingerPrint: '지문 인증', // @btn-finger-print-color

      // === 소셜 협업 버튼 ===
      follow: '팔로우',         // @btn-follow-color
      collect: '수집',     // @btn-collect-color
      like: '좋아요',          // @btn-like-color
      forward: '전달',    // @btn-forward-color
      at: '@',                  // @btn-at-color
      group: '그룹',          // @btn-group-color
      team: '팀',           // @btn-team-color

      // === 보안 인증 버튼 ===
      verifyCode: '인증 코드', // @btn-verify-code-color
      bind: '연동',             // @btn-bind-color
      unbind: '연동 해제',         // @btn-unbind-color
      authorize: '인증',   // @btn-authorize-color
      deauthorize: '인증 해제', // @btn-deauthorize-color

      // === 고급 기능 버튼 ===
      version: '버전',       // @btn-version-color
      history: '기록',    // @btn-history-color
      restore: '복원',     // @btn-restore-color
      archive: '보관',      // @btn-archive-color
      unarchive: '보관 해제', // @btn-unarchive-color
      merge: '병합',       // @btn-merge-color
      split: '분할',         // @btn-split-color

      // === 시스템 관리 버튼 ===
      backup: '백업',    // @btn-backup-color
      restoreSys: '시스템 복원', // @btn-restore-sys-color
      clean: '정리',        // @btn-clean-color
      optimize: '최적화',    // @btn-optimize-color
      monitor: '모니터링',    // @btn-monitor-color
      diagnose: '진단', // @btn-diagnose-color
      maintain: '유지보수'     // @btn-maintain-color
    },

    // ==================== 결과 및 메시지 ====================
    // 결과 상태
    result: {
      success: '작업 성공',
      failed: '작업 실패',
      warning: '경고',
      info: '정보',
      error: '오류'
    },

    // 메시지
    message: {
      loading: '처리 중...',
      saving: '저장 중...',
      submitting: '제출 중...',
      deleting: '삭제 중...',
      operationSuccess: '작업 성공',
      operationFailed: '작업 실패',
      deleteConfirm: '삭제하시겠습니까?',
      deleteSuccess: '삭제 성공',
      deleteFailed: '삭제 실패',
      createSuccess: '생성 성공',
      createFailed: '생성 실패',
      updateSuccess: '업데이트 성공',
      updateFailed: '업데이트 실패',
      forceOfflineConfirm: '이 사용자를 강제 오프라인으로 전환하시겠습니까?',
      forceOfflineSuccess: '강제 오프라인 성공',
      forceOfflineFailed: '강제 오프라인 실패',
      networkError: '네트워크 연결 실패, 네트워크를 확인하세요',
      systemError: '시스템 오류',
      timeout: '요청 시간 초과',
      invalidResponse: '잘못된 응답 데이터 형식',
      backendNotStarted: '백엔드 서비스가 시작되지 않았습니다. 서비스 상태를 확인하세요',
      invalidRequest: '잘못된 요청 매개변수',
      unauthorized: '인증되지 않았습니다. 다시 로그인하세요',
      forbidden: '접근이 거부되었습니다',
      notFound: '요청한 리소스가 존재하지 않습니다',
      serverError: '서버 내부 오류',
      httpError: {
        400: '잘못된 요청 매개변수',
        401: '인증되지 않았습니다. 다시 로그인하세요',
        403: '접근이 거부되었습니다',
        404: '요청한 리소스가 존재하지 않습니다',
        500: '서버 내부 오류',
        502: '잘못된 게이트웨이',
        503: '서비스가 사용할 수 없습니다',
        504: '게이트웨이 시간 초과'
      },
      loadFailed: '로딩 실패'
    },

    // ==================== 컴포넌트 텍스트 ====================
    // 탭
    tab: {
      // === 기본 정보 ===
      basic: '기본 정보',
      profile: '프로필',
      avatar: '아바타 설정',
      password: '비밀번호 설정',
      security: '보안 설정',

      // === 코드 생성 ===
      codegen: '코드 생성',
      codegenBasic: '생성 설정',
      codegenField: '필드 설정',
      codegenPreview: '생성 미리보기',
      codegenTemplate: '템플릿 설정',
      codegenImport: '가져오기 설정',
      
      // === 워크플로우 ===
      workflow: '워크플로우',
      flowDesign: '플로우 설계',
      flowDeploy: '플로우 배포',
      flowInstance: '플로우 인스턴스',
      flowTask: '작업 관리',
      flowForm: '폼 설정',
      flowNotify: '메시지 알림',
      
      // === 시스템 관리 ===
      permission: '데이터 권한',
      log: '작업 기록',
      dept: '부서 및 직위',
      role: '역할 및 권한',
      config: '매개변수 설정',
      task: '예약 작업',
      monitor: '시스템 모니터'
    },

    // 버튼 텍스트
    button: {
      submit: '제출',
      confirm: '확인',
      ok: '확인',
      cancel: '취소',
      close: '닫기',
      reset: '초기화',
      clear: '지우기',
      save: '저장',
      delete: '삭제'
    }
  },

  // ==================== 선택기 컴포넌트 ====================
  select: {
    loadMore: '더 로드',
    loading: '로딩 중...',
    notFound: '데이터 없음',
    selected: '{count}개 선택됨',
    selectedTotal: '전체 {total}개',
    clear: '지우기',
    search: '검색',
    all: '전체',
    // 오류 메시지
    loadError: '데이터 로딩 실패',
    searchError: '검색 실패',
    networkError: '네트워크 연결 실패',
    serverError: '서버 오류',
    unknownError: '알 수 없는 오류'
  }
}


